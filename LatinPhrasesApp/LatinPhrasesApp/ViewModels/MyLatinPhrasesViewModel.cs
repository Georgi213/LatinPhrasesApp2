using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LatinPhrasesApp.ViewModels
{
    public class MyLatinPhrasesViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;

        public ObservableCollection<LatinPhrase> MyLatinPhrases { get; set; }

        public Command LoadMyLatinPhrasesCommand { get; set; }

        public MyLatinPhrasesViewModel(IDataService dataService = null)
        {
            _dataService = dataService ?? new DataService();
            MyLatinPhrases = new ObservableCollection<LatinPhrase>();
            LoadMyLatinPhrasesCommand = new Command(async () => await ExecuteLoadMyLatinPhrasesCommand());

            LoadMyLatinPhrasesCommand.Execute(null);
        }

        public async Task ExecuteLoadMyLatinPhrasesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                MyLatinPhrases.Clear();
                var myLatinPhrases = await _dataService.GetMyLatinPhrasesAsync();
                foreach (var latinPhrase in myLatinPhrases)
                {
                    MyLatinPhrases.Add(latinPhrase);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
       

    }
}
