using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Domain.Context
{
    public class ApplicationContext : DbContext
    {
       
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.SocailMedias)
                .WithOne(s => s.Person)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public virtual DbSet<Talent> Talents { get; set; }
        public virtual DbSet<SocailMedia> SocailMedias { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Preference> Preferences { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }

    }
}
