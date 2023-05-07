using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LatinPhrasesApp.ViewModels
{
    public class AuthorsListViewModel : BaseViewModel
    {
        public ICommand AuthorSelectedCommand { get; }
        private string _searchText;
        

        private ObservableCollection<Author> _authors;

        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set => SetProperty(ref _authors, value);
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchAuthors(value);
            }
        }
        private Author _selectedAuthor;
        private IEnumerable<Author> _allAuthors;

       


        public AuthorsListViewModel()
        {
           
            LoadAuthors();
            AuthorTappedCommand = new Command<Author>(OnAuthorTapped);
        }


        private void LoadAuthors()
        {

            _allAuthors = new ObservableCollection<Author>
        {
            new Author
            {
                Name = "Horace",
                Portrait = "horace_portrait.jpg",
                LegendaryQuote = "Carpe diem"
            },
            new Author
        {
            Name = "Julius Caesar",
            Portrait = "julius_caesar_portrait.jpg",
            LegendaryQuote = "Veni, vidi, vici"
        },
        new Author
        {
            Name = "Virgil",
            Portrait = "virgil_portrait.jpg",
            LegendaryQuote = "Audentes fortuna iuvat"
        },
        new Author
        {
            Name = "Ovid",
            Portrait = "ovid_portrait.jpg",
            LegendaryQuote = "Tempus edax rerum"
        },
        new Author
        {
            Name = "Seneca",
            Portrait = "seneca_portrait.jpg",
            LegendaryQuote = "Errare humanum est, perseverare diabolicum"
        },
        new Author
        {
            Name = "Cicero",
            Portrait = "cicero_portrait.jpg",
            LegendaryQuote = "Summum ius, summa iniuria"
        }
        };
            Authors = new ObservableCollection<Author>(_allAuthors);

            OnPropertyChanged(nameof(Authors));
        }

        public void SearchAuthors(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Authors = new ObservableCollection<Author>(_allAuthors);
            }
            else
            {
                searchText = searchText.ToLowerInvariant();
                var filteredAuthors = _allAuthors.Where(a => a.Name.ToLowerInvariant().Contains(searchText));
                Authors = new ObservableCollection<Author>(filteredAuthors);
            }
        }
        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set => SetProperty(ref _selectedAuthor, value);
        }
        private void OnAuthorTapped(Author author)
        {
            if (author == null)
                return;

            SelectedAuthor = null;
        }

        
        public Command LoadAuthorsCommand { get; }
        public Command SearchCommand { get; }
        public Command AuthorTappedCommand { get; }

        
        public void FilterAuthors(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Authors = new ObservableCollection<Author>(_allAuthors);
            }
            else
            {
                Authors = new ObservableCollection<Author>(_allAuthors.Where(author => author.Name.ToLower().Contains(searchTerm.ToLower())));
            }
        }

        

        

       
    }
}
