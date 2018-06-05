using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB_WebApp.Models
{
    public class MovieDetailsViewModel
    {
        public string Name { get; set; }
        public string ProducerName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string AlternateText { get; set; }
        public string Actors { get; set; }
    }
}