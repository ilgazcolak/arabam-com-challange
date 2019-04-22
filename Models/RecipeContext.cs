using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arabam.Com.Models
{
    public class RecipeContext: DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options):base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .Property<string>("ExtendedDataStr")
                .HasField("_extendedData");

            modelBuilder.Entity<Recipe>()
            .Property(e => e.Categories)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        
    }
}
