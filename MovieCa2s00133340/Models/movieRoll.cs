using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieCa2s00133340.Models
{
    public class movie
    {
        [Key]
        public int movieID { get; set; }
        [Display(Name = "Movie Name"), Required]
        public string movieName { get; set; }
        [DisplayName("Movie Genre"), Required]
        public string genre { get; set; }
        [DisplayName("Movie Length"), Required]
        public double movielength { get; set; }
        [DisplayName("Number of Actors")]
        public int noOfActors { get; set; }

        //public virtual List<MovieActor> MovieActor { get; set; }


        public virtual List<actor> actors { get; set; }
         


    }

    public class actor
    {
        public int actorID { get; set; }
        public string actorName { get; set; }
        public int actorAge { get; set; }
        public movie movie { get; set; }
        public int movieID { get; set; }

        //public virtual List<MovieActor> ActorMovie { get; set; }

        //public virtual List<actor> actors
        //{
        //    get { return (ActorMovie == null) ? null : ActorMovie.Select(MAmp => MAmp.actor).ToList(); }
        //}
    }

    public class movieDb : DbContext
    {
        public DbSet<movie> movie { get; set; }
        public DbSet<actor> actor { get; set; }
      //  public DbSet<MovieActor> MovieActors { get; set; }
        public movieDb()
            : base("movieDb")
        { }
    }

    //public class MovieActor
    //{
    //    [Key, Column(Order = 0)]    // Composite key (first key)
    //    public int movieID { get; set; }
    //    [Key, Column(Order = 1)]        // Composite key (second key)
    //    public int actorID { get; set; }
    //    public bool Discounted { get; set; }    // Additional Property for relationship
    //    // Nav Properties
    //    public virtual actor actor { get; set; }
    //    public virtual movie movie { get; set; }
    //}

    class movieInitaliser : DropCreateDatabaseAlways<movieDb>
    {
        protected override void Seed(movieDb context)
        {
         /*   var actors = new List<actor>()
            {
                new actor() {actorName = "Will Ferell", actorAge = 45},
                new actor() {actorName = "Liam Neeson", actorAge = 67},
                new actor() {actorName = "Ray Romano", actorAge = 38},
                new actor() {actorName = "Vince Vanghan", actorAge = 45},
                new actor() {actorName = "Ben Stiller", actorAge = 45},
                new actor() {actorName = "Vin Disel", actorAge = 45},
                new actor() {actorName = "Paul Walker", actorAge = 45},
                new actor() {actorName = "Danziel Washington", actorAge = 50},
                new actor() {actorName = "Chris Pratt", actorAge = 50},
                new actor() {actorName = "Bradley Cooper", actorAge = 45}
            };
            actors.ForEach(act => context.actor.Add(act));
            context.SaveChanges();
            */
            var movies = new List<movie>()
            {
                new movie() 
                {
                    movieName = "The Lego Movie",
                    movielength = 100,
                    genre = "Animation",
                    actors = new List<actor>()
                    {
                        new actor()
                        {
                            actorName = "Will Ferell",
                            actorAge = 45
                        },
                        new actor()
                        {
                            actorName = "Liam Neeson",
                            actorAge = 45
                        }
                    },
                    //MovieActor = new List<MovieActor>()
                    
                    // actor = context.Actors.Where(act => act.actorName == "Will Ferell" && act.actorName == "Liam Nesson").FirstOrDefault()                    
                },
                new movie()
                {
                    movieName = "Ice Age 4",
                    movielength = 100,
                    genre = "Animation",
                    actors = new List<actor>()
                    {
                        new actor()
                        {
                            actorName = "Ray Romano",
                            actorAge = 40
                        }
                    },
                    //actor = context.Actors.Where(act => act.actorName == "Ray Romano").FirstOrDefault(),
                    //MovieActor = new List<MovieActor>()
                   
                },
             
             
                    new movie()
                {
                    movieName = "Dodgeball",
                    movielength = 100,
                    genre = "Comedy",
                    actors = new List<actor>()
                    {
                        new actor()
                        {
                            actorName = "Ben Stiller",
                            actorAge = 45
                        },
                        new actor()
                        {
                            actorName = "Vince Vaghan",
                            actorAge = 45
                        }
                    },
                    //MovieActor = new List<MovieActor>()
                },
                    new movie()
                {
                    movieName = "Furious 7",
                    movielength = 100,
                    genre = "Crime",
                    actors = new List<actor>()
                    {
                        new actor()
                        {
                            actorName = "Vin Disel",
                            actorAge = 45
                        },
                        new actor()
                        {
                            actorName = "Paul Walker",
                            actorAge = 45
                        }
                    },
                    //MovieActor = new List<MovieActor>()
                },
              
                    new movie()
                {
                    movieName = "Guardians of the Galaxy",
                    movielength = 100,
                    genre = "SciFi",
                        actors = new List<actor>()
                    {
                        new actor()
                        {
                            actorName = "Vin Diesel",
                            actorAge = 40
                        },
                        new actor()
                        {
                            actorName = "Chris Pratt",
                            actorAge = 40
                        },
                        new actor()
                        {
                            actorName = "Bradley Cooper",
                            actorAge = 40
                        }
                    },
                   // MovieActor = new List<MovieActor>()
                }
            };
            movies.ForEach(mov => context.movie.Add(mov));
            context.SaveChanges();

                //movies.ForEach(mov => (actors).ForEach(act => mov.MovieActor.Add(
                //    new MovieActor()
                //    {
                //        movieID =mov.movieID,
                //        actorID = act.actorID,
                //    })));
                //context.SaveChanges();
        }
    }
}