using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LatinPhrasesApp.ViewModels
{
    public class LatinPhrasesListViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private Author _selectedAuthor;

        public ObservableCollection<LatinPhrase> LatinPhrases { get; set; }
        public Command LoadLatinPhrasesCommand { get; set; }

        public LatinPhrasesListViewModel(Author selectedAuthor, IDataService dataService)
        {
            _selectedAuthor = selectedAuthor;
            _dataService = dataService;
            LatinPhrases = new ObservableCollection<LatinPhrase>();
            LoadLatinPhrasesCommand = new Command(async () => await LoadLatinPhrasesAsync());
        }

        private async Task LoadLatinPhrasesAsync()
        {
            IEnumerable<LatinPhrase> latinPhrases;

            if (_selectedAuthor == null)
            {
                // Load all Latin phrases if no author is specified
                latinPhrases = await _dataService.GetLatinPhrasesAsync();
            }
            else
            {
                // Load Latin phrases for the selected author
                latinPhrases = await _dataService.GetLatinPhrasesByAuthorAsync(_selectedAuthor.Name);
            }

            LatinPhrases.Clear();
            foreach (var latinPhrase in latinPhrases)
            {
                LatinPhrases.Add(latinPhrase);
            }
        }
}  }
