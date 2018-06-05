using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB_WebApp.Models
{
    public class AssignedActor
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}