using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using LatinPhrasesApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Command = Xamarin.Forms.Command;

namespace LatinPhrasesApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MyLatinPhrasesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LatinPhrase> Phrases { get; set; }
        public ICommand AddPhraseCommand { get; set; }
        private IEnumerable<LatinPhrase> _allPhrases;
        public ICommand EditPhraseCommand { get; set; }
        private readonly MyLatinPhrasesPage _page;
        public ICommand DeletePhraseCommand { get; set; }
        private MyLatinPhrasesViewModel _viewModel;
        public MyLatinPhrasesViewModel()
        {
            LoadPhrases();
            AddPhraseCommand = new Command(async () =>
            {
                var addPhrasePage = new AddPhrasePage(AddPhrase);
                DeletePhraseCommand = new Xamarin.Forms.Command<LatinPhrase>(DeletePhrase);

                var addPhraseNavigationPage = new NavigationPage(addPhrasePage);
                await Application.Current.MainPage.Navigation.PushModalAsync(addPhraseNavigationPage);
            });

            EditPhraseCommand = new Xamarin.Forms.Command<LatinPhrase>(async (phrase) => await EditPhrase(phrase));
        }
        private void SavePhrases()
        {
            var json = JsonConvert.SerializeObject(Phrases);
            Preferences.Set("Phrases", json);
        }

        private void LoadPhrasesFromStorage()
        {
            var json = Preferences.Get("Phrases", string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                Phrases = JsonConvert.DeserializeObject<ObservableCollection<LatinPhrase>>(json);
            }
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
            bool confirmDelete = await Application.Current.MainPage.DisplayAlert("Delete Phrase", "Are you sure you want to delete this phrase?", "Yes", "No");
            
            if (confirmDelete)
            {
                
                Phrases.Remove(phrase);
                SavePhrases();
            }
        }

        public async Task LoadPhrases()
        {
            LoadPhrasesFromStorage();

            if (Phrases == null || Phrases.Count == 0)
            {
                _allPhrases = new ObservableCollection<LatinPhrase>
        {
            new LatinPhrase
            {
                Latin = "Carpe diem",
                Estonian = "Haara päevast"
            },
            new LatinPhrase
            {
                Latin = "Veni, vidi, vici",
                Estonian = "Tulin, nägin, võitsin"
            },
            new LatinPhrase
            {
                Latin = "Alea iacta est",
                Estonian = "Täring on veeretatud"
            },
            new LatinPhrase
            {
                Latin = "Calamitas virtutis occasio",
                Estonian = "Katastroof on võimalus vooruseks."
            },
            new LatinPhrase
            {
                Latin = "Dant gaudea vires",
                Estonian = "Rõõmus annab jõudu"
            },
            new LatinPhrase
            {
                Latin = "Fabricando fit faber",
                Estonian = "Meister on loodud tööjõuga"
            }
        };

                Phrases = new ObservableCollection<LatinPhrase>(_allPhrases);
            }

            OnPropertyChanged(nameof(Phrases));
        }
        public void AddPhrase(LatinPhrase newPhrase)
        {
           
                Phrases.Add(newPhrase);
                SavePhrases();
        }


        public async Task EditPhrase(LatinPhrase phrase)
        {
            var editPhrasePage = new EditPhrasePage(phrase, this);
            await Application.Current.MainPage.Navigation.PushModalAsync(editPhrasePage);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
