using LatinPhrasesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatinPhrasesApp.Behaviors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteLatinPhrasesPage : ContentPage
    {
        private readonly FavoriteLatinPhrasesViewModel _viewModel;

        public FavoriteLatinPhrasesPage(FavoriteLatinPhrasesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
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