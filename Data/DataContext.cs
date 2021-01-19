using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pandami.Models;

namespace Pandami.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Membre> Membres { get; set; }
        public DbSet<Feat> Feats { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=PandamiV1;Trusted_Connection=True;MultipleActiveResultSets=true;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membre>(entity =>
            {
                entity.Property(e => e.Inscription)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });


            //Creation clé de la classe intermédiaire Repondre
            modelBuilder.Entity<Repondre>().HasKey(t => new { t.IdMembre, t.IdFeat });

            //Création des relations Many-to-Many
            modelBuilder.Entity<Repondre>()
            .HasOne(t => t.Membre)
            .WithMany(t => t.Inscriptions)
            .HasForeignKey(t => t.IdMembre);

            modelBuilder.Entity<Repondre>()
                        .HasOne(t => t.Feat)
                        .WithMany(t => t.Reponses)
                        .HasForeignKey(t => t.IdFeat);

        }
    }
}
