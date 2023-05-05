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


            // Bind the ViewModel to the passed LatinPhrase
            BindingContext = _viewModel = new LatinPhraseDetailViewModel(_latinPhraseData.LatinPhrases[0]);
        }
       
       
    }

}