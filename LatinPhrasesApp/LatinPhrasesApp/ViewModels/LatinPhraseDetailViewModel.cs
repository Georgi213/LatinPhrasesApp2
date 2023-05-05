using LatinPhrasesApp.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatinPhrasesApp.ViewModels
{
    public class LatinPhraseDetailViewModel : BaseViewModel
    {
        private LatinPhrase _latinPhrase;
        public List<LatinPhrase> LatinPhrases { get; set; }

        public LatinPhrase LatinPhrase
        {
            get => _latinPhrase;
            set => SetProperty(ref _latinPhrase, value);
        }

        public LatinPhraseDetailViewModel(LatinPhrase latinPhrase)
        {
            Title = "Latin Phrase Details";
            LatinPhrase = latinPhrase;
        }
       
}   }
