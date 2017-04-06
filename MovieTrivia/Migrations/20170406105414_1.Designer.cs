using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MovieTrivia;

namespace MovieTrivia.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170406105414_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("MovieTrivia.Model.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MovieTrivia.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("MovieTrivia.Model.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Counter");

                    b.Property<long?>("MovieId");

                    b.Property<int>("PlayerOneAnswer");

                    b.Property<int>("PlayerTwoAnswer");

                    b.Property<int?>("TriviaId");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("TriviaId");

                    b.ToTable("Round");
                });

            modelBuilder.Entity("MovieTrivia.Model.Trivia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PlayerOneId");

                    b.Property<int?>("PlayerTwoId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("Trivia");
                });

            modelBuilder.Entity("MovieTrivia.Model.Round", b =>
                {
                    b.HasOne("MovieTrivia.Model.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("MovieTrivia.Model.Trivia")
                        .WithMany("Rounds")
                        .HasForeignKey("TriviaId");
                });

            modelBuilder.Entity("MovieTrivia.Model.Trivia", b =>
                {
                    b.HasOne("MovieTrivia.Model.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId");

                    b.HasOne("MovieTrivia.Model.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId");
                });
        }
    }
}
