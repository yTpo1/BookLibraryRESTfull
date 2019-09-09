using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Models
{
    public class MyBookStoreContext : DbContext
    {
        public MyBookStoreContext(DbContextOptions<MyBookStoreContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<BooksRead> BooksRead { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Books>(entity =>
        //    {
        //        entity.Property(e=>e.)
        //    });
        //}
    }
}
