using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieTrivia
{
    /// An Etensions class to allow for migrations to be automatically applied
    /// We can also seed any test or static data into the DB from here when required
    internal static class ApplicationDbContextExtensions
    {
        public static void Seed(this ApplicationDbContext context)
        {
            EnsureDBIsCreated(context);
        }

        private static void EnsureDBIsCreated(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
