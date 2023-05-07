using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace LatinPhrasesApp.ViewModels
{
    public class LatinPhrasesListViewModel : BaseViewModel
    {
        
        public ICommand AddFavoriteCommand { get; set; }
        private FavoriteLatinPhrasesViewModel _favoriteViewModel;
        private IEnumerable<Phrase> _allPhrases;
        public ICommand ShareCommand { get; }
        private string _searchText;

        private ObservableCollection<Phrase> _phrases;
        public string SelectedLatinPhrase { get; set; }

        public ObservableCollection<Phrase> Phrases
        {
            get => _phrases;
            set => SetProperty(ref _phrases, value);
        }

        public LatinPhrasesListViewModel(FavoriteLatinPhrasesViewModel favoriteViewModel, string selectedPhrase = null)
        {
            _favoriteViewModel = favoriteViewModel;
            AddFavoriteCommand = new Command<Phrase>(AddFavorite);
            ShareCommand = new Command<Phrase>(SharePhrase);

            LoadPhrases(selectedPhrase);

        }
        private async void SharePhrase(Phrase phrase)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = $"{phrase.Latin} - {phrase.Estonian}",
                Title = "Share Latin Phrase"
            });
        }
        private void AddFavorite(Phrase phrase)
        {
            if (!_favoriteViewModel.FavoritePhrases.Any(p => p.Latin == phrase.Latin && p.Estonian == phrase.Estonian))
            {
                _favoriteViewModel.FavoritePhrases.Add(phrase);
            }
        }
        private void LoadPhrases(string selectedPhrase = null)
        {

            _allPhrases = new ObservableCollection<Phrase>
        {
            new Phrase
            {
                Latin = "Carpe diem",
                Estonian = "Võtke päev kinni"
            },
            new Phrase
        {
            Latin = "Veni, vidi, vici",
             Estonian = "Tulin, nägin, võitsin"
        },
        new Phrase
        {
             Latin = "Alea iacta est",
              Estonian = "Stants on heidetud"
        },
         new Phrase
        {
            Latin = "Calamitas virtutis occasio",
             Estonian = "Katastroof on võimalus vooruseks."
        },
          new Phrase
        {
            Latin = "Dant gaudea vires",
             Estonian = "Rõõmus annab jõudu"
        },
           new Phrase
        {
            Latin = "Fabricando fit faber",
             Estonian = "Meister on loodud tööjõuga"
        },
            new Phrase
        {
            Latin = "Jactantius maerent, qui minus dolent",
             Estonian = "Kurbus, mis näitab vähest kurbust"
        },
             new Phrase
        {
            Latin = "Rebus in adversis meliora sperare memento",
             Estonian = "Ebaõnnestumises looda parimat"
        },
              new Phrase
        {
            Latin = "Tamdiu discendum est, quamdiu vivas",
             Estonian = "Kui palju sa elad, nii palju sa õpid"
        },
               new Phrase
        {
            Latin = "Beate vivere est honeste vivere",
             Estonian = "Elada õnnelikult tähendab elada ilusti"
        },
                new Phrase
        {
            Latin = "Ubi Concordia, Ibi Victoria",
             Estonian = "Kus on kokkulepe, seal on võit."
        },
                 new Phrase
        {
            Latin = "Vae victoribus",
             Estonian = "Häda võitjatele"
        },
                  new Phrase
        {
            Latin = "Laus propria sordet",
             Estonian = "Kiitus teie kasuks on rõve"
        },
                   new Phrase
        {
            Latin = "In legibus salus",
             Estonian = "Pääste seaduses"
        },
                    new Phrase
        {
            Latin = "Errando discimus",
             Estonian = "Vead õpetavad"
        },
        };


            Phrases = new ObservableCollection<Phrase>(_allPhrases);

            OnPropertyChanged(nameof(Phrases));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SearchPhrases(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Phrases = new ObservableCollection<Phrase>(_allPhrases);
            }
            else
            {
                searchText = searchText.ToLowerInvariant();
                var filteredPhrases = _allPhrases.Where(a => a.Latin.ToLowerInvariant().Contains(searchText));
                Phrases = new ObservableCollection<Phrase>(filteredPhrases);
            }
        }

        public void FilterPhrases(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Phrases = new ObservableCollection<Phrase>(_allPhrases);
            }
            else
            {
                Phrases = new ObservableCollection<Phrase>(_allPhrases.Where(phrase => phrase.Latin.ToLower().Contains(searchTerm.ToLower())));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchPhrases(value);
            }
        }
    }

    
}
