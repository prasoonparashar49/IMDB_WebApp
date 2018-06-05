using IMDB_WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB_WebApp.Controllers
{
    public class MovieController : Controller
    {
        public DbContextIMDB db = new DbContextIMDB();
        private ICollection<AssignedActor> FetchAllActors()
        {
            var actors = db.Actors;
            var assignedActors = new List<AssignedActor>();

            foreach (var actor in actors)
            {
                assignedActors.Add(new AssignedActor
                {
                    ActorId = actor.Id,
                    Name = actor.Name,
                    Assigned = false
                });
            }
            return assignedActors;
        }
        // GET: Movie
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return View(db.Movies.Include("Producer").ToList());
        }
        private void AddActors(Movie movie, IEnumerable<AssignedActor> assignedActors)
        {
            foreach(var assignedActor in assignedActors)
            {
                if(assignedActor.Assigned)
                {
                    var actor = db.Actors.FirstOrDefault(a => a.Id == assignedActor.ActorId);
                    movie.Actors.Add(actor);
                }
            }
        }
        //GET: Movie/CreateMovie
        public ActionResult CreateMovie()
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerId");
            var movieViewModel = new MovieViewModel { Actors = FetchAllActors() };
            return View(movieViewModel);
        }

        //GET: Movie/Details
        public ActionResult Details(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var movie = db.Movies.Include("Producer").FirstOrDefault(m => m.Id == id);
            db.Entry(movie).Collection(m => m.Actors).Load();
            MovieDetailsViewModel viewModel = new MovieDetailsViewModel {
                Name = movie.Name,
                ProducerName = movie.Producer.Name,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Bio,
                Photo = movie.Photo
            };
            foreach(var actor in movie.Actors)
            {
                viewModel.Actors += "'" + actor.Name + "' ";
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateMovie(MovieViewModel movieViewModel)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(movieViewModel.ImageFile.FileName);
                string extension = Path.GetExtension(movieViewModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;//to avoid duplicacy in file name
                movieViewModel.ImagePath = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"),fileName);
                movieViewModel.ImageFile.SaveAs(fileName);
                Movie movie = new Movie
                {
                    Name = movieViewModel.Name,
                    ReleaseDate = movieViewModel.ReleaseDate,
                    ProducerId = movieViewModel.ProducerId,
                    Photo = movieViewModel.ImagePath,
                    AlternateText = "There is no image for this movie"
                };
                AddActors(movie, movieViewModel.Actors);
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerId");
            movieViewModel.Actors = FetchAllActors();
            return View(movieViewModel);
        }
               
        // GET: Movie/Edit/5
        public ActionResult EditMovie(int id)
        {
            db = new DbContextIMDB();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerId");
            var data = db.Movies
                        .Where(movie => movie.Id == id)
                        .Select(movie => new
                        {
                            ViewModel = new MovieEditViewModel
                            {
                                Id = movie.Id,
                                Name = movie.Name,
                                ReleaseDate = movie.ReleaseDate,
                                Photo = movie.Photo
                            },
                            ActorIds = movie.Actors.Select(a => a.Id)
                        })
                        .SingleOrDefault();
            if (data == null)
            {
                return HttpNotFound();
            }
            data.ViewModel.Actors = db.Actors
                            .Select(a => new AssignedActor
                            {
                                ActorId = a.Id,
                                Name = a.Name
                            })
                            .ToList();
            foreach(var actor in data.ViewModel.Actors)
            {
                actor.Assigned = data.ActorIds.Contains(actor.ActorId);
            }
            return View(data.ViewModel);
        }

        [HttpPost]
        public ActionResult EditMovie(MovieEditViewModel viewModel)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                var movie = db.Movies.FirstOrDefault(m => m.Id == viewModel.Id);
                if(movie == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Entry(movie).Collection(m => m.Actors).Load();
                    movie.Name = viewModel.Name;
                    movie.ReleaseDate = viewModel.ReleaseDate;
                    movie.ProducerId = viewModel.ProducerId;
                    foreach(var actor in viewModel.Actors)
                    {
                        if (actor.Assigned)
                        {
                            if(!movie.Actors.Any(a => a.Id == actor.ActorId))
                            {
                                var toAddActor = db.Actors.FirstOrDefault(a => a.Id == actor.ActorId);
                                movie.Actors.Add(toAddActor);
                            }
                        }
                        else
                        {
                            var removeActor = movie.Actors.SingleOrDefault(a => a.Id == actor.ActorId);
                            if(removeActor != null)
                            {
                                movie.Actors.Remove(removeActor);
                            }
                        }
                    }
                    //code to edit image
                    string fileName = Path.GetFileNameWithoutExtension(viewModel.ImageFile.FileName);
                    string extension = Path.GetExtension(viewModel.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;//to avoid duplicacy in file name
                    viewModel.ImagePath = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    viewModel.ImageFile.SaveAs(fileName);
                    movie.Photo = viewModel.ImagePath;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerId");
            return View();
        }
    }
}
