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

            // Look for any Directors.
            if (context.Director.Any())
            {
                return;   // DB has been seeded
            }

            var directors = new Director[]
            {
                new Director{FirstMidName="Carson",LastName="Alexander",HireDate=DateTime.Parse("2019-09-01")},
                new Director{FirstMidName="Meredith",LastName="Alonso",HireDate=DateTime.Parse("2007-09-01")},
                new Director{FirstMidName="Arturo",LastName="Anand",HireDate=DateTime.Parse("2018-09-01")},
                new Director{FirstMidName="Gytis",LastName="Barzdukas",HireDate=DateTime.Parse("2007-09-01")},
                new Director{FirstMidName="Yan",LastName="Li",HireDate=DateTime.Parse("2011-09-01")},
                new Director{FirstMidName="Peggy",LastName="Justice",HireDate=DateTime.Parse("2000-09-01")},
                new Director{FirstMidName="Laura",LastName="Norman",HireDate=DateTime.Parse("2018-09-01")},
                new Director{FirstMidName="Nino",LastName="Olivetto",HireDate=DateTime.Parse("2001-09-01")}
            };
            if (context.Director != null) {
                context.Director.AddRange(directors);
                context.SaveChanges();
            }

            var studios = new Studio[] {
                new Studio{Name = "Maker First", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), Director = directors[0]},
                new Studio{Name = "Maker Second", Budget = 150000, StartDate = DateTime.Parse("2001-09-01"), Director = directors[3]},
                new Studio{Name = "Maker if", Budget = 35000000, StartDate = DateTime.Parse("1900-09-01"), Director = directors[4]},
                new Studio{Name = "Maker logos", Budget = 640000, StartDate = DateTime.Parse("1947-09-01"), Director = directors[5]},
            };
            if (context.Studio != null) {
                context.Studio.AddRange(studios);
                context.SaveChanges();
            }

            var movies = new Movie[] {
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "R",
                    Studio = studios[0]
                },

                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M,
                    Rating = "PG-13",
                    Studio = studios[0]
                },

                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M,
                    Rating = "PG-13",
                    Studio = studios[1]
                },

                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18",
                    Studio = studios[1]
                },

                new Movie
                {
                    Title = "Movie 2",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18",
                    Studio = studios[2]
                },

                new Movie
                {
                    Title = "Movie 3",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18",
                    Studio = studios[2]
                },

                new Movie
                {
                    Title = "Movie 4",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18",
                    Studio = studios[3]
                },

                new Movie
                {
                    Title = "Movie 5",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "PG-18",
                    Studio = studios[3]
                }
            };
            context.Movie.AddRange(movies);
            context.SaveChanges();

            var actors = new Actor[]
            {
                new Actor{FirstMidName="Carson",LastName="Alexander",BirthDate=DateTime.Parse("1978-09-01")},
                new Actor{FirstMidName="Meredith",LastName="Alonso",BirthDate=DateTime.Parse("1926-09-01")},
                new Actor{FirstMidName="Arturo",LastName="Anand",BirthDate=DateTime.Parse("1955-09-01")},
                new Actor{FirstMidName="Gytis",LastName="Barzdukas",BirthDate=DateTime.Parse("1988-09-01")},
                new Actor{FirstMidName="Yan",LastName="Li",BirthDate=DateTime.Parse("2000-09-01")},
                new Actor{FirstMidName="Peggy",LastName="Justice",BirthDate=DateTime.Parse("1998-09-01")},
                new Actor{FirstMidName="Laura",LastName="Norman",BirthDate=DateTime.Parse("1993-09-01")},
                new Actor{FirstMidName="Nino",LastName="Olivetto",BirthDate=DateTime.Parse("1985-09-01")}
            };
            if (context.Actor != null) {
                context.Actor.AddRange(actors);
                context.SaveChanges();
            }

            var ActorMoviePairs = new ActorMoviePair[]
            {
                new ActorMoviePair{Actor=actors[1],Movie=movies[2],},
                new ActorMoviePair{Actor=actors[1],Movie=movies[3],},
                new ActorMoviePair{Actor=actors[1],Movie=movies[4],},
                new ActorMoviePair{Actor=actors[2],Movie=movies[2],},
                new ActorMoviePair{Actor=actors[2],Movie=movies[5],},
                new ActorMoviePair{Actor=actors[2],Movie=movies[6],},
                new ActorMoviePair{Actor=actors[3],Movie=movies[1]},
                new ActorMoviePair{Actor=actors[4],Movie=movies[1]},
                new ActorMoviePair{Actor=actors[4],Movie=movies[3],},
                new ActorMoviePair{Actor=actors[5],Movie=movies[4],},
                new ActorMoviePair{Actor=actors[6],Movie=movies[5]},
                new ActorMoviePair{Actor=actors[7],Movie=movies[6],},
            };

            if (context.ActorMoviePair != null) {
                context.ActorMoviePair.AddRange(ActorMoviePairs);
                context.SaveChanges();
            }

            var Homes = new Home[]
            {
                new Home {
                    DirectorID = directors[0].ID,
                    Director = directors[0],
                    Location = "Smith 17" },
                new Home {
                    DirectorID = directors[1].ID,
                    Director = directors[1],
                    Location = "Gowan 27" },
                new Home {
                    DirectorID = directors[2].ID,
                    Director = directors[2],
                    Location = "Thompson 304" }
            };
            if (context.Home != null) {
                context.Home.AddRange(Homes);
                context.SaveChanges();
            }


        }
    }
}