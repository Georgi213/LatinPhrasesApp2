using System;
using System.Collections.Generic;
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
    public partial class EditAuthorPage : ContentPage
    {
        private LatinPhrase _phrase;
        private MyAuthorsViewModel _viewModel;

        public EditAuthorPage(LatinPhrase phrase, MyAuthorsViewModel viewModel)
        {
            InitializeComponent();

            _phrase = phrase;
            _viewModel = viewModel;

            // Populate Entry controls with the existing Latin and Estonian phrases\\
            Name.Text = phrase.Name;
            Latin.Text = phrase.Latin;
           
        }
        private async void OnSelectImageButtonClicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
            if (photo != null)
            {
                AuthorImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }
        private async void OnSaveButtonClicked(object sender, System.EventArgs e)
        {
            // Get the user input from the Entry controls
            string name = Name.Text;
            string latin = Latin.Text;
           

            // Update the phrase object with the new values
            _phrase.Latin = latin;
            _phrase.Name = name;


            _viewModel.UpdatePhrase(_phrase);
            // Navigate back to the previous page
            await Navigation.PopModalAsync();
        }
    }
}