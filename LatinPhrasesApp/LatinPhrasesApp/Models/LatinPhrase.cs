using System;
using System.Collections.Generic;
using System.Text;

namespace LatinPhrasesApp.Models
{
    public class LatinPhrase
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Phrase { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }


        public string Translation { get; set; }

        public string Image { get; set; }
    }

}
