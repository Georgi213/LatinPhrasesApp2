using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LatinPhrasesApp.ViewModels
{
    public class AuthorsListViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
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
            // Initialize the Authors property
            LoadAuthors();
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

        public Command LoadAuthorsCommand { get; }
        public Command SearchCommand { get; }
        public Command AuthorTappedCommand { get; }

        public AuthorsListViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Authors = new ObservableCollection<Author>();

            LoadAuthorsCommand = new Command(async () => await ExecuteLoadAuthorsCommand());
            SearchCommand = new Command<string>(async (searchText) => await ExecuteSearchCommand(searchText));
            AuthorTappedCommand = new Command<Author>(OnAuthorTapped);
        }
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

        private async Task ExecuteLoadAuthorsCommand()
        {
            IsBusy = true;

            try
            {
                var authors = await _dataService.GetAuthorsAsync();
                Authors.Clear();
                foreach (var author in authors)
                {
                    Authors.Add(author);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteSearchCommand(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                await ExecuteLoadAuthorsCommand();
                return;
            }

            IsBusy = true;

            try
            {
                var authors = await _dataService.SearchAuthorsAsync(searchText);
                Authors.Clear();
                foreach (var author in authors)
                {
                    Authors.Add(author);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnAuthorTapped(Author author)
        {
            if (author == null)
                return;

            SelectedAuthor = null;
        }

        public async Task OnAppearing()
        {
            await ExecuteLoadAuthorsCommand();
        }
    }
}
