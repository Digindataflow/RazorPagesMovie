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

        public DbSet<RazorPagesMovie.Models.Movie> Movie { get; set; } = null!;
        public DbSet<RazorPagesMovie.Models.Actor> Actor { get; set; } = null!;
        public DbSet<RazorPagesMovie.Models.ActorMoviePair> ActorMoviePair { get; set; } = null!;
        public DbSet<RazorPagesMovie.Models.Studio> Studio { get; set; } = null!;
        public DbSet<RazorPagesMovie.Models.Director> Director { get; set; } = null!;
        public DbSet<RazorPagesMovie.Models.Home> Home { get; set; } = null!;

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
            modelBuilder.Entity<Studio>().ToTable(nameof(Studio))
                .Property(d => d.ConcurrencyToken)
                .IsConcurrencyToken();
            // by convention they are already generated 
            // modelBuilder.Entity<Studio>().ToTable(nameof(Studio))
            //     .HasMany(i => i.Movies)
            //     .WithOne(i => i.Studio)
            //     .HasForeignKey(e => e.StudioID)
            //     .IsRequired();;
            modelBuilder.Entity<Actor>().ToTable(nameof(Actor));
            modelBuilder.Entity<ActorMoviePair>().ToTable(nameof(ActorMoviePair));

            // by convention they are already generated 
            // modelBuilder.Entity<Director>().ToTable(nameof(Director))
            //     .HasOne(c => c.Home)
            //     .WithOne(i => i.Director);
            // modelBuilder.Entity<Director>().ToTable(nameof(Director))
            //     .HasOne(c => c.Studio)
            //     .WithOne(i => i.Director);

            modelBuilder.Entity<Home>().ToTable(nameof(Home));
        }
    }
}
