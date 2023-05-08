using LatinPhrasesApp.Models;
using LatinPhrasesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatinPhrasesApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using LatinPhrasesApp.Views;
using LatinPhrasesApp.Data;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LatinPhraseDetailPage : ContentPage
    {
        private LatinPhraseDetailViewModel _viewModel;
        private LatinPhraseData _latinPhraseData;
        public List<LatinPhrase> LatinPhrases { get; set; }

        public LatinPhraseDetailPage(LatinPhrase latinPhraseData)
        {
            InitializeComponent();
            AddAboutToolbarItem();


            // Bind the ViewModel to the passed LatinPhrase
            BindingContext = _viewModel = new LatinPhraseDetailViewModel(_latinPhraseData.LatinPhrases[0]);
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

        private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

    }

}