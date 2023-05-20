﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RazorPagesMovie.Data;

#nullable disable

namespace RazorPagesMovie.Migrations
{
    [DbContext(typeof(RazorPagesMovieContext))]
    partial class RazorPagesMovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.Property<int>("DirectorsID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoviesID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DirectorsID", "MoviesID");

                    b.HasIndex("MoviesID");

                    b.ToTable("DirectorMovie");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Actor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.ActorMoviePair", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActorID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Performance")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ActorID");

                    b.HasIndex("MovieID");

                    b.ToTable("ActorMoviePair");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Director", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Director");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Home", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DirectorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("DirectorID")
                        .IsUnique();

                    b.ToTable("Home");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudioID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("StudioID");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Studio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<int?>("DirectorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("DirectorID")
                        .IsUnique();

                    b.ToTable("Studio", (string)null);
                });

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.HasOne("RazorPagesMovie.Models.Director", null)
                        .WithMany()
                        .HasForeignKey("DirectorsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RazorPagesMovie.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RazorPagesMovie.Models.ActorMoviePair", b =>
                {
                    b.HasOne("RazorPagesMovie.Models.Actor", "Actor")
                        .WithMany("ActorMoviePairs")
                        .HasForeignKey("ActorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RazorPagesMovie.Models.Movie", "Movie")
                        .WithMany("ActorMoviePairs")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Home", b =>
                {
                    b.HasOne("RazorPagesMovie.Models.Director", "Director")
                        .WithOne("Home")
                        .HasForeignKey("RazorPagesMovie.Models.Home", "DirectorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Movie", b =>
                {
                    b.HasOne("RazorPagesMovie.Models.Studio", "Studio")
                        .WithMany("Movies")
                        .HasForeignKey("StudioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Studio");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Studio", b =>
                {
                    b.HasOne("RazorPagesMovie.Models.Director", "Director")
                        .WithOne("Studio")
                        .HasForeignKey("RazorPagesMovie.Models.Studio", "DirectorID");

                    b.Navigation("Director");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Actor", b =>
                {
                    b.Navigation("ActorMoviePairs");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Director", b =>
                {
                    b.Navigation("Home");

                    b.Navigation("Studio");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Movie", b =>
                {
                    b.Navigation("ActorMoviePairs");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Studio", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
