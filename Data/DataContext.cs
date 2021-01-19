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

        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<CategorieAide> CategorieAides { get; set; }
        public DbSet<Disponibilite> Disponibilites { get; set; }
        public DbSet<Feat> Feats { get; set; }
        public DbSet<JourDeLaSemaine> JourDeLaSemaines { get; set; }
        public DbSet<Litige> Litiges { get; set; }
        public DbSet<Materiel> Materiels { get; set; }
        public DbSet<Membre> Membres { get; set; }
        public DbSet<PreferenceAide> PreferenceAides { get; set; }
        public DbSet<Repondre> Repondres { get; set; }
        public DbSet<Sexe> Sexes { get; set; }
        public DbSet<Suspension> Suspensions { get; set; }
        public DbSet<TypeAide> TypeAides { get; set; }
        public DbSet<TypeLitige> TypeLitiges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membre>(entity =>
            {
                entity.Property(e => e.Inscription)
                    .HasColumnType("date")
                   // .HasDefaultValueSql("(getdate())")
                    .ValueGeneratedOnAdd();
                
            });


            //Creation clé de la classe intermédiaire Repondre
            modelBuilder.Entity<Repondre>().HasKey(t => new { t.IdMembre, t.IdFeat });
            //Création des relations Many-to-Many avec la classe intermédiaire repondre
            modelBuilder.Entity<Repondre>()
            .HasOne(t => t.Membre)
            .WithMany(t => t.Inscriptions)
            .HasForeignKey(t => t.IdMembre);
            modelBuilder.Entity<Repondre>()
                        .HasOne(t => t.Feat)
                        .WithMany(t => t.Reponses)
                        .HasForeignKey(t => t.IdFeat);

            //Creation clé de la classe intermédiaire Prefeence
            modelBuilder.Entity<PreferenceAide>().HasKey(t => new { t.IdMembre, t.IdTypeAide });
            //Création des relations Many-to-Many avec la classe intermédiaire Preference aide
            modelBuilder.Entity<PreferenceAide>()
            .HasOne(t => t.Membre)
            .WithMany(t => t.PreferenceAides)
            .HasForeignKey(t => t.IdMembre);
            modelBuilder.Entity<PreferenceAide>()
                        .HasOne(t => t.TypeAide)
                        .WithMany(t => t.PreferenceAides)
                        .HasForeignKey(t => t.IdTypeAide);

        }
    }
}
