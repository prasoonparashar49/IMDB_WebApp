using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_WebApp.Models
{
    public class Movie
    {
        public Movie()
        {
            Actors = new List<Actor>();
        }
        #region Properties
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName ="datetime2")]
        public DateTime ReleaseDate { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
        public string AlternateText { get; set; }
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer{ get; set; }
        #endregion
        //List of all actors in the movie
        public virtual ICollection<Actor> Actors { get; set; }
    }
}