using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB_WebApp.Models
{
    public class MovieEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Photo { get; set; }
        public int ProducerId { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public IEnumerable<AssignedActor> Actors { get; set; }
    }
}