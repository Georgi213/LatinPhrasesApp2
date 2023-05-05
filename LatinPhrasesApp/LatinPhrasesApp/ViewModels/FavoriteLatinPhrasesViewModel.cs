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
    public class FavoriteLatinPhrasesViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        public ObservableCollection<LatinPhrase> FavoriteLatinPhrases { get; set; }
        public Command LoadFavoritesCommand { get; set; }

        public FavoriteLatinPhrasesViewModel(IDataService dataService = null)
        {
            _dataService = dataService ?? new DataService();
            FavoriteLatinPhrases = new ObservableCollection<LatinPhrase>();
            LoadFavoritesCommand = new Command(async () => await LoadFavoriteLatinPhrases());
        }

        public async Task LoadFavoriteLatinPhrases()
        {
            IsBusy = true;

            try
            {
                FavoriteLatinPhrases.Clear();
                var favorites = await _dataService.GetFavoriteLatinPhrasesAsync();

                foreach (var phrase in favorites)
                {
                    FavoriteLatinPhrases.Add(phrase);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            if (FavoriteLatinPhrases.Count == 0)
            {
                LoadFavoritesCommand.Execute(null);
            }
        }
}   }
