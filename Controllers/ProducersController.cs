using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMDB_WebApp;
using IMDB_WebApp.Models;

namespace IMDB_WebApp.Controllers
{
    public class ProducersController : Controller
    {
        private DbContextIMDB db = new DbContextIMDB();


        // GET: Producers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProducerId,Name,Sex,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Producers.Add(producer);
                db.SaveChanges();
                return RedirectToAction("Index", "Movie");
            }

            return View(producer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
