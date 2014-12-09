using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieCa2s00133340.Models;


namespace MovieCa2s00133340.Controllers
{
    public class HomeController : Controller
    {
       
        movieDb db = new movieDb();
        //
        // GET: /Home/

        public ActionResult Index(string sortOrder)
        {
            //using viewbag for pie chart
            ViewBag.genreAction = db.movie.Count(mov => mov.genre == "Action");
            ViewBag.genreAnimation = db.movie.Count(mov => mov.genre == "Animation");
            ViewBag.genreComedy = db.movie.Count(mov => mov.genre == "Comedy");
            ViewBag.genreCrime = db.movie.Count(mov => mov.genre == "Crime");
            ViewBag.genreHorror = db.movie.Count(mov => mov.genre == "Horror");
            ViewBag.genreSciFi = db.movie.Count(mov => mov.genre == "SciFi");
            ViewBag.genreRomantic = db.movie.Count(mov => mov.genre == "Romantic");
   

            ViewBag.PageTitle = "Total number of actors is " + db.actor.Count() + " in all movies";
            if (sortOrder == null) sortOrder = "ascNumber";
            ViewBag.numberOrder = (sortOrder == "ascNumber") ? "descNumber" : "ascNumber";

            IQueryable<movie> movies = db.movie;
            //switch for sorting order of the number of actors in movies
            switch (sortOrder)
            {
                case "descNumber":
                    ViewBag.numberOrder = "ascNumber";
                    movies = movies.OrderByDescending(c => c.actors.Count).Include("actors");
                    break;
                case "ascNumber":
                    ViewBag.numberOrder = "descNumber";
                    movies = movies.OrderBy(c => c.actors.Count).Include("actors");
                    break;
                default:
                    ViewBag.numberOrder = "ascNumber";
                    movies = movies.OrderBy(c => c.actors.Count).Include("actors");
                    break;
            }
            return View(movies.ToList());

            
        }

       

        //
        // GET: /Home/Details/5


        //partial view for actors in movies (not displaying)
        public PartialViewResult ActorsByID(int id)
        {

            return PartialView("_ActorsInMovie", db.movie.Find(id).actors);
        }

        //details of movies
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var q = db.movie.Find(id);
            if (q == null)
            {
                Debug.WriteLine("Movie not found");
                ViewBag.PageTitle = String.Format("sorry record not found {0}", id);
            }
            else
            {
                //title of in
                ViewBag.PageTitle = "Details of" + q.movieName + ", " + q.movielength + ", " + q.genre + "(" + ((q.actors.Count == 0) ? "None" : q.actors.Count.ToString()) + ')';
            }
            return View(q);
        }

        
        public ActionResult Create() //view for creating movie
        {
            ViewBag.actorslist = db.actor.ToList();
            return View();
        }
        public ActionResult CreateActor() //view for creating actor
        {
            return View();
        }

        [HttpPost, ActionName("CreateActor")] //how to create actor
        public ActionResult CreateActor2(actor incomingActor)
        {
            if (ModelState.IsValid) //checking validation
            {
                db.actor.Add(incomingActor); // add to actor list
                db.SaveChanges(); // save changes to the database
                return RedirectToAction("Index"); //redirect to index
            }
            return null;
        }

        [HttpPost]
        public ActionResult Create(movie incomingMovie) //creating movie
        {
            try //using catch blocks for exceptions
            {
                if (ModelState.IsValid)
                {
                    using (var dba = new movieDb())
                    {
                        db.movie.Add(incomingMovie); // add to movie list
                        db.SaveChanges();
                    }
                    return RedirectToAction("index");
                }
                return View(); //return view
            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpPost]
        public HttpStatusCodeResult UpdateActor(actor actors) //making changes to the actor
        {
            try
            {
                db.Entry(actors).State = EntityState.Modified; //modifying actor
                db.SaveChanges(); 
                return new HttpStatusCodeResult(HttpStatusCode.OK); //making statuscode ok
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        public PartialViewResult EditMoviesID(int id) //editing the movie with partial view
        {
            return PartialView("_EditMovies", db.movie.Find(id)); //finding the movie id
        }

        [HttpPost, ActionName("EditMoviesID")]
        public ActionResult Edit(movie editMovie)//editing the movie
        {
            try
            {
                db.Entry(editMovie).State = EntityState.Modified; //modifying the movie
                db.SaveChanges();
                return RedirectToAction("Index"); //returning to the index
            }
            catch (Exception)
            {

                return View();
            }
        }

        public PartialViewResult MoviesByID(int id)//partial view for deleting the movie
        {
            return PartialView("_DeleteMovies", db.movie.Find(id)); //getting the id of the movie
        }

        [HttpPost, ActionName("MoviesByID")] //getting the action name
        public ActionResult DeleteConfirmed(int id)
        {
            db.movie.Remove(db.movie.Find(id)); //removing the movie by getting its id
            db.SaveChanges();
            return RedirectToAction("Index"); //returning to index
        }

        public ActionResult DeleteActors(int id) //deleting actors
        {
            return View(db.actor.Find(id));
           
        }

        [HttpPost, ActionName("Delete Actors")] //delete the actors
        public ActionResult DeleteConfirmed2(int id)//need different variable name than deleteconfirmed
        {
            db.actor.Remove(db.actor.Find(id)); //removimg the actor 
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EditActor(int id) //editing the actor
        {
            return View(db.actor.Find(id));
        }
        [HttpPost, ActionName("EditActor")]
        public ActionResult EditActor(actor editActor)
        {
            try
            {
                db.Entry(editActor).State = EntityState.Modified;//modifiying the actor
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                
                return View();
            }
        }

    }
}
