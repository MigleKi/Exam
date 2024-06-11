using Exam.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Database
{
    public class MainDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=Exam;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure the User-Person relationship
            modelBuilder.Entity<User>()
                .HasMany(e => e.Persons)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the Person-Address relationship
            modelBuilder.Entity<Person>()
                .HasOne(e => e.Address)
                .WithOne(e => e.Person)
                .HasForeignKey<Address>(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<User>()
                .HasIndex(e => e.Username)
                .IsUnique();


        }

    }
}
