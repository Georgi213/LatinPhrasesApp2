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

        public MyLatinPhrasesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MyLatinPhrasesViewModel();
        }

        
    }
}