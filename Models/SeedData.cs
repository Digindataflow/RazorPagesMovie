using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RazorPagesMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<RazorPagesMovieContext>>()))
        {
            if (context == null || context.Movie == null)
            {
                throw new ArgumentNullException("Null RazorPagesMovieContext");
            }

            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "R"
                },

                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M,
                    Rating = "PG-13"
                },

                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M,
                    Rating = "PG-13"
                },

                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18"
                },

                new Movie
                {
                    Title = "Movie 2",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18"
                },

                new Movie
                {
                    Title = "Movie 3",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18"
                },

                new Movie
                {
                    Title = "Movie 4",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18"
                },

                new Movie
                {
                    Title = "Movie 5",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18"
                }
            );
            context.SaveChanges();

            var actors = new Actor[]
            {
                new Actor{FirstMidName="Carson",LastName="Alexander",BirthDate=DateTime.Parse("2019-09-01")},
                new Actor{FirstMidName="Meredith",LastName="Alonso",BirthDate=DateTime.Parse("2017-09-01")},
                new Actor{FirstMidName="Arturo",LastName="Anand",BirthDate=DateTime.Parse("2018-09-01")},
                new Actor{FirstMidName="Gytis",LastName="Barzdukas",BirthDate=DateTime.Parse("2017-09-01")},
                new Actor{FirstMidName="Yan",LastName="Li",BirthDate=DateTime.Parse("2017-09-01")},
                new Actor{FirstMidName="Peggy",LastName="Justice",BirthDate=DateTime.Parse("2016-09-01")},
                new Actor{FirstMidName="Laura",LastName="Norman",BirthDate=DateTime.Parse("2018-09-01")},
                new Actor{FirstMidName="Nino",LastName="Olivetto",BirthDate=DateTime.Parse("2019-09-01")}
            };

            context.Actor.AddRange(actors);
            context.SaveChanges();

            var ActorMoviePairs = new ActorMoviePair[]
            {
                new ActorMoviePair{ActorId=1,MovieId=2,},
                new ActorMoviePair{ActorId=1,MovieId=3,},
                new ActorMoviePair{ActorId=1,MovieId=4,},
                new ActorMoviePair{ActorId=2,MovieId=2,},
                new ActorMoviePair{ActorId=2,MovieId=5,},
                new ActorMoviePair{ActorId=2,MovieId=6,},
                new ActorMoviePair{ActorId=3,MovieId=1},
                new ActorMoviePair{ActorId=4,MovieId=1},
                new ActorMoviePair{ActorId=4,MovieId=3,},
                new ActorMoviePair{ActorId=5,MovieId=4,},
                new ActorMoviePair{ActorId=6,MovieId=5},
                new ActorMoviePair{ActorId=7,MovieId=6,},
            };

            context.ActorMoviePair.AddRange(ActorMoviePairs);
            context.SaveChanges();
        }
    }
}