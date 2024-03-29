﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatinPhrasesApp.Models;
using LatinPhrasesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyAuthorsPage : ContentPage
	{
        private readonly MyAuthorsViewModel _viewModel;
        public MyAuthorsViewModel MyAuthorsViewModel { get; }
        public MyAuthorsPage (MyAuthorsViewModel viewModel)
		{
            AddAboutToolbarItem();
            _viewModel = viewModel;

            BindingContext = _viewModel;

            MessagingCenter.Subscribe<AddAuthorPage, LatinPhrase>(this, "AddPhrase", (sender, phrase) =>
            {
                _viewModel.Phrases.Add(phrase);
            });
            InitializeComponent ();
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
        private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AddPhrasePage, LatinPhrase>(this, "AddPhrase");
        }
        private async void OnSearchClicked(object sender, EventArgs e)
        {
            var searchTerm = AuthorSearchBar.Text;
            _viewModel.FilterAuthors(searchTerm);
        }
        private async void OnRefreshing(object sender, EventArgs e)
        {
            await _viewModel.LoadPhrases();
            PhrasesListView.IsRefreshing = false;
        }
        private void OnSearchTextChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchBar.Text))
            {
                (BindingContext as MyAuthorsViewModel).FilterAuthors((sender as SearchBar).Text);
            }
        }
    }
}