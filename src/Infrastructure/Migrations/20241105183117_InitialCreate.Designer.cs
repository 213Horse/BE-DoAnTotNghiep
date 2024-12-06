﻿// <auto-generated />
using System;
using CleanMinimalApi.Infrastructure.Databases.Tourism;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanMinimalApi.Infrastructure.Migrations
{
    [DbContext(typeof(TourismDbContext))]
    [Migration("20241105183117_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ReviewAuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReviewedMovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReviewAuthorId");

                    b.HasIndex("ReviewedMovieId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("CleanMinimalApi.Infrastructure.Databases.Tourism.Models.Destination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Review", b =>
                {
                    b.HasOne("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Author", "ReviewAuthor")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Movie", "ReviewedMovie")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewedMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReviewAuthor");

                    b.Navigation("ReviewedMovie");
                });

            modelBuilder.Entity("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Author", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("CleanMinimalApi.Infrastructure.Databases.MoviesReviews.Models.Movie", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
