﻿using LatinPhrasesApp.ViewModels;
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

        public MyLatinPhrasesPage()
        {
            InitializeComponent();
            AddAboutToolbarItem();

            BindingContext = _viewModel = new MyLatinPhrasesViewModel();
        }
        private void AddAboutToolbarItem()
        {
            var aboutToolbarItem = new ToolbarItem
            {
                Text = "About",
                IconImageSource = "about_icon.png", // Optional, add your about icon image
                Order = ToolbarItemOrder.Secondary, // Set the order for the action overflow menu
                Priority = 0 // Adjust the priority as needed
            };

            aboutToolbarItem.Clicked += AboutToolbarItem_Clicked;
            this.ToolbarItems.Add(aboutToolbarItem);
        }

        private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

    }
}