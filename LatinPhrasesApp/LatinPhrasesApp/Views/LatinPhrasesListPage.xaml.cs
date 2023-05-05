using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using LatinPhrasesApp.ViewModels;
using LatinPhrasesApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LatinPhrasesListPage : ContentPage
    {
        private readonly LatinPhrasesListViewModel _viewModel;
        public List<LatinPhrase> LatinPhrases { get; set; }
        public ObservableCollection<Author> Authors { get; set; }

        public LatinPhrasesListPage(Author selectedAuthor = null)
        {
            InitializeComponent();
            InitializeLatinPhrases();

            var dataService = App.ServiceProvider.GetService<IDataService>();
            BindingContext = _viewModel = new LatinPhrasesListViewModel(selectedAuthor, dataService);
        }
        private void InitializeLatinPhrases()
        {
            LatinPhrases = new List<LatinPhrase>
        {
              new LatinPhrase { Phrase = "Carpe diem", Translation = "Seize the day", Author = "Horace", Image = "phrase_papyrus1.png" },
               new LatinPhrase { Phrase = "Veni, vidi, vici", Translation = "I came, I saw, I conquered", Author = "Julius Caesar", Image = "phrase_papyrus2.png"},
             new LatinPhrase { Phrase = "Audentes fortuna iuvat", Translation = "Fortune favors the bold", Author = "Virgil", Image = "phrase_papyrus3.png"},
            // ... other Latin phrases
        };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadLatinPhrasesCommand.Execute(null);
        }
    }
}