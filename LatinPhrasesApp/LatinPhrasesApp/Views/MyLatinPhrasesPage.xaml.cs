using LatinPhrasesApp.Models;
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
    public partial class MyLatinPhrasesPage : ContentPage
    {
        private readonly MyLatinPhrasesViewModel _viewModel;
        public MyLatinPhrasesViewModel MyLatinPhrasesViewModel { get; }

        public MyLatinPhrasesPage(MyLatinPhrasesViewModel viewModel)
        {
            InitializeComponent();
            AddAboutToolbarItem();
            _viewModel = viewModel;

            BindingContext = _viewModel;

            MessagingCenter.Subscribe<AddPhrasePage, LatinPhrase>(this, "AddPhrase", (sender, phrase) =>
            {
                _viewModel.Phrases.Add(phrase);
            });
        }

        
        private void AddAboutToolbarItem()
        {
            var aboutToolbarItem = new ToolbarItem
            {
                Text = "Umbes",
                IconImageSource = "about_icon.png", // Optional, add your about icon image
                Order = ToolbarItemOrder.Secondary, // Set the order for the action overflow menu
                Priority = 0 // Adjust the priority as needed
            };

            aboutToolbarItem.Clicked += AboutToolbarItem_Clicked;
            this.ToolbarItems.Add(aboutToolbarItem);
        }

        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AddPhrasePage, LatinPhrase>(this, "AddPhrase");
        }
        private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is LatinPhrase tappedPhrase)
            {
                var action = await DisplayActionSheet("Choose an action", "Cancel", null, "Edit", "Delete");

                switch (action)
                {
                    case "Edit":
                        _viewModel.EditPhrase(tappedPhrase);
                        break;
                    case "Delete":
                        _viewModel.DeletePhrase(tappedPhrase);
                        break;
                }
            }
        }
       
        private async void OnRefreshing(object sender, EventArgs e)
        {
            await _viewModel.LoadPhrases();
            PhrasesListView.IsRefreshing = false;
        }
        

    }
}