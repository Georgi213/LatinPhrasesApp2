using LatinPhrasesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteLatinPhrasesPage : ContentPage
    {
        private readonly FavoriteLatinPhrasesViewModel _viewModel;

        public FavoriteLatinPhrasesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FavoriteLatinPhrasesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}