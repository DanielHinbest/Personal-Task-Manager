using Microsoft.EntityFrameworkCore;
using Personal_Task_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Personal_Task_Manager.Models.Task;

namespace Personal_Task_Manager.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=tasks.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Priority).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();

                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Tasks)
                      .HasForeignKey(e => e.CategoryId);
            });
        }
    }
}
