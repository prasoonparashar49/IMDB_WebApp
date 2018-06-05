using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IMDB_WebApp.Models;

namespace IMDB_WebApp
{
    public class IMDBInitializer : DropCreateDatabaseAlways<DbContextIMDB>
    {
        protected override void Seed(DbContextIMDB context)
        {
            base.Seed(context);

            Actor akshay = new Actor { Id = 1, Name="Akshay Kumar",Bio = "Rajeev Bhatia from a middle class in Delhi married to Twinkle Khanna", DOB = new System.DateTime(1987, 8, 12), Sex = "Male" };
            Actor salman = new Actor { Id = 2, Name="Salman Khan", Bio = "A Brad son but a true superstar who have a huge fan following", DOB = new System.DateTime(1986, 8, 22), Sex = "Male" };
            Producer anurag = new Producer { ProducerId = 1, Name = "Anurag Kashyap", Bio = "A legendary producer that produced some evergreen movies", Sex = "Male" };
            Producer rohan = new Producer { ProducerId = 2, Name = "Rohan Bhatnagar", Bio = "Born in Punjab and is a struggling now", Sex = "Male" };
            context.Actors.Add(akshay);
            context.Actors.Add(salman);
            context.Producers.Add(anurag);
            context.Producers.Add(rohan);
        }
    }
}