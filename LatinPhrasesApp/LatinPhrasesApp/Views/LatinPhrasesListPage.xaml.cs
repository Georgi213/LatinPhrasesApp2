using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using LatinPhrasesApp.ViewModels;
using LatinPhrasesApp.Behaviors;
using LatinPhrasesApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LatinPhrasesListPage : ContentPage
    {
        private readonly LatinPhrasesListViewModel _viewModel;
        private readonly FavoriteLatinPhrasesViewModel _favoriteViewModel;

        public LatinPhrasesListPage(LatinPhrasesListViewModel viewModel, FavoriteLatinPhrasesViewModel favoriteViewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;

            _favoriteViewModel = favoriteViewModel;
            BindingContext = _viewModel;
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            var searchTerm = PhraseSearchBar.Text;
            _viewModel.FilterPhrases(searchTerm);
        }
        private void HeartButton_Pressed(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.TextColor = Color.DarkRed;
            }
        }

        private void HeartButton_Released(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.TextColor = Color.Default;
            }
        }
    }
}