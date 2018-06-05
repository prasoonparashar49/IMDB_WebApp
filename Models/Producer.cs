using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_WebApp.Models
{

    public class Producer
    {
        #region Properties
        [Key]
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Bio { get; set; }
        #endregion
        //List of all movies the producer is associated to
        public virtual ICollection<Movie> Movies { get; set; }

    }
}