using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTrivia.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trivia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerOneId = table.Column<int>(nullable: true),
                    PlayerTwoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trivia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trivia_Player_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trivia_Player_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Round",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Counter = table.Column<int>(nullable: false),
                    MovieId = table.Column<long>(nullable: true),
                    PlayerOneAnswer = table.Column<int>(nullable: false),
                    PlayerTwoAnswer = table.Column<int>(nullable: false),
                    TriviaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Round_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Round_Trivia_TriviaId",
                        column: x => x.TriviaId,
                        principalTable: "Trivia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Round_MovieId",
                table: "Round",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_TriviaId",
                table: "Round",
                column: "TriviaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trivia_PlayerOneId",
                table: "Trivia",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Trivia_PlayerTwoId",
                table: "Trivia",
                column: "PlayerTwoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Round");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Trivia");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
