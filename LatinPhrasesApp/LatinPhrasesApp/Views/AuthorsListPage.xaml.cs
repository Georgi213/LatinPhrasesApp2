using LatinPhrasesApp.Models;
using LatinPhrasesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatinPhrasesApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using LatinPhrasesApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LatinPhrasesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorsListPage : ContentPage
    {
        
            private AuthorsListViewModel _viewModel;

            public AuthorsListPage()
            {
            InitializeComponent();

            BindingContext = _viewModel = new AuthorsListViewModel();
        }

            protected override void OnAppearing()
            {
                base.OnAppearing();
                _viewModel.OnAppearing();
            }

            private async void OnAuthorTapped(object sender, ItemTappedEventArgs e)
            {
                if (e.Item != null)
                {
                    await Navigation.PushAsync(new LatinPhrasesListPage(e.Item as Author));
                }
            }

            private async void OnSearchClicked(object sender, EventArgs e)
            {
            var searchTerm = AuthorSearchBar.Text;
            _viewModel.FilterAuthors(searchTerm);
           }
         

    }
}