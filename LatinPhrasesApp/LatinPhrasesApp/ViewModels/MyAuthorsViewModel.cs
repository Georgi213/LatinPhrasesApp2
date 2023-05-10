using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using LatinPhrasesApp.Models;
using LatinPhrasesApp.Views;
using MvvmHelpers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using System.Linq;
using Command = Xamarin.Forms.Command;

namespace LatinPhrasesApp.ViewModels
{
    public class MyAuthorsViewModel : BaseViewModel
    {
        public ObservableCollection<LatinPhrase> Phrases
        {
            get => _phrases;
            set => SetProperty(ref _phrases, value);
        }
        public ICommand AddPhraseCommand { get; set; }
        private IEnumerable<LatinPhrase> _allPhrases;
        private ObservableCollection<LatinPhrase> _phrases;
        private ObservableCollection<LatinPhrase> _authors;
        private IEnumerable<LatinPhrase> _allAuthors;
        private string _searchText;
        public ICommand EditPhraseCommand { get; set; }
        public ICommand FilterPhrasesCommand { get; set; }
        public ICommand CopyPhraseCommand { get; }
        private readonly MyLatinPhrasesPage _page;
        public ICommand ShareCommand { get; }
        public ICommand DeletePhraseCommand { get; set; }
        private MyLatinPhrasesViewModel _viewModel;
        public MyAuthorsViewModel()
        {
            LoadPhrases();
            FilterPhrasesCommand = new Xamarin.Forms.Command<string>(FilterAuthors);
            AddPhraseCommand = new Command(async () =>
            {
                var addPhrasePage = new AddAuthorPage(AddPhrase);
                DeletePhraseCommand = new Xamarin.Forms.Command<LatinPhrase>(DeletePhrase);

                var addPhraseNavigationPage = new NavigationPage(addPhrasePage);
                await Application.Current.MainPage.Navigation.PushModalAsync(addPhraseNavigationPage);
            });
            ShareCommand = new Xamarin.Forms.Command<LatinPhrase>(SharePhrase);
            EditPhraseCommand = new Xamarin.Forms.Command<LatinPhrase>(async (phrase) => await EditPhrase(phrase));
        }
        private void SavePhrases()
        {
            var json = JsonConvert.SerializeObject(Phrases);
            Preferences.Set("Phrases", json);
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterAuthors(_searchText);
                }
            }
        }
        public void SearchAuthors(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Phrases = new ObservableCollection<LatinPhrase>(_allAuthors);
            }
            else
            {
                searchText = searchText.ToLowerInvariant();
                var filteredAuthors = _allAuthors.Where(a => a.Name.ToLowerInvariant().Contains(searchText));
                Phrases = new ObservableCollection<LatinPhrase>(filteredAuthors);
            }
        }
        public void CopyPhraseToClipboard(string phrase)
        {
            Xamarin.Essentials.Clipboard.SetTextAsync(phrase);
        }
        private ObservableCollection<LatinPhrase> LoadPhrasesFromStorage()
        {
            var json = Preferences.Get("Phrases", string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<ObservableCollection<LatinPhrase>>(json);
            }
            return null;
        }


        public void UpdatePhrase(LatinPhrase phrase)
        {
            var index = Phrases.IndexOf(phrase);
            if (index != -1)
            {
                Phrases[index] = phrase;
                SavePhrases();
            }
        }
        public async void DeletePhrase(LatinPhrase phrase)
        {
            bool confirmDelete = await Application.Current.MainPage.DisplayAlert("Delete Author", "Are you sure you want to delete this author?", "Yes", "No");

            if (confirmDelete)
            {

                Phrases.Remove(phrase);
                SavePhrases();
            }
        }
        public void FilterAuthors(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Phrases = new ObservableCollection<LatinPhrase>(_allAuthors);
            }
            else
            {
                Phrases = new ObservableCollection<LatinPhrase>(_allAuthors.Where(author => author.Name.ToLower().Contains(searchTerm.ToLower())));
            }
        }
        public async Task LoadPhrases()
        {
            _allPhrases = LoadPhrasesFromStorage();
            if (_allAuthors == null || _allPhrases.Count() == 0)
            {
                _allAuthors = new ObservableCollection<LatinPhrase>
        {
             new LatinPhrase
            {
                Name = "Horace",
                Portrait = "horace_portrait.jpg",
                Latin = "Carpe diem"
            },
            new LatinPhrase
        {
            Name = "Julius Caesar",
            Portrait = "julius_caesar_portrait.jpg",
            Latin = "Veni, vidi, vici"
        },

        };
            }

            Phrases = new ObservableCollection<LatinPhrase>(_allAuthors);
            OnPropertyChanged(nameof(Phrases));
        }
        public void AddPhrase(LatinPhrase newPhrase)
        {

            Phrases.Add(newPhrase);
            SavePhrases();
        }
        private async void SharePhrase(LatinPhrase phrase)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = $"{phrase.Name} - {phrase.Latin}",
                Title = "Share Latin Author "
            });
        }

        public async Task EditPhrase(LatinPhrase phrase)
        {
            var editAuthorPage = new EditAuthorPage(phrase, this);
            await Application.Current.MainPage.Navigation.PushModalAsync(editAuthorPage);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
