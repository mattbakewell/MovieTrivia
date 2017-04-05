using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTrivia.Model;

namespace MovieTrivia.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Trivia> Trivia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            //dotnet ef migrations add 1



            //RPC over HTTP, but trying to use HTTP verbs appropriately".
            //RPC = Remote Procedure Call - basically calling 
            //code across the network.
            //Configure the SQLLite Database
           options.UseSqlite("Filename=./movietrivia.db");
        }
    }
}
