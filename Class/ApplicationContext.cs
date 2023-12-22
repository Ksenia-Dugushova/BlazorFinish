using BlazorApp1.Pages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BlazorApp1.Class
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Person> Actors => Set<Person>();
        public DbSet<Tag> Tags => Set<Tag>();


        public ApplicationContext()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=trying.db");
            optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Property(m => m.movieRating);
            modelBuilder.Entity<Movie>()
                   .HasMany(m => m.actors)
                   .WithMany();
            //.WithMany(a => a.Movies);
            modelBuilder.Entity<Movie>()
               .HasMany(m => m.tag)
             .WithMany().UsingEntity(j => j.ToTable("TagToMovie"));
            //.WithMany(a => a.Movies);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.director)
                .WithMany();

            modelBuilder.Entity<Movie>()
                .Property(m => m.codes_of_top10);
        }
    }
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


