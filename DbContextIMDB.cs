using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IMDB_WebApp.Models;

namespace IMDB_WebApp
{
    public class DbContextIMDB : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public DbContextIMDB() :base("name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().
                HasMany(movie => movie.Actors)
                .WithMany(actor => actor.Movies)
                .Map(mc =>
                {
                    mc.ToTable("MovieActors");
                    mc.MapLeftKey("MovieId");
                    mc.MapRightKey("ActorId");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}