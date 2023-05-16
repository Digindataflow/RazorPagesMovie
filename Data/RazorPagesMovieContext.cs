using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Data
{
    public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesMovie.Models.Movie>? Movie { get; set; }
        public DbSet<RazorPagesMovie.Models.Actor>? Actor { get; set; }
        public DbSet<RazorPagesMovie.Models.ActorMoviePair>? ActorMoviePair { get; set; }
        public DbSet<RazorPagesMovie.Models.Studio>? Studio { get; set; }
        public DbSet<RazorPagesMovie.Models.Director>? Director { get; set; }
        public DbSet<RazorPagesMovie.Models.Home>? Home { get; set; }

        /**
        * the fluent API can specify most of the formatting, 
        * validation, and mapping rules that can be done with attributes.
        * Attributes and the fluent API can be mixed. 
        * Use the chosen approach consistently as much as possible.
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            // add many-to-many relationship
            modelBuilder.Entity<Movie>().ToTable(nameof(Movie))
                .HasMany(c => c.Directors)
                .WithMany(i => i.Movies);
            modelBuilder.Entity<Studio>().ToTable(nameof(Studio));
            modelBuilder.Entity<Actor>().ToTable(nameof(Actor));
            modelBuilder.Entity<ActorMoviePair>().ToTable("ActorMoviePair");
            modelBuilder.Entity<Director>().ToTable(nameof(Director));
            modelBuilder.Entity<Home>().ToTable(nameof(Home));
        }
    }
}
