using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB_WebApp.Models
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ProducerId { get; set; }
        public virtual ICollection<AssignedActor> Actors { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        
    }
}