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
        public DbSet<ReponseHelper> ReponseHelpers { get; set; }
        public DbSet<Sexe> Sexes { get; set; }
        public DbSet<Suspension> Suspensions { get; set; }
        public DbSet<TypeAide> TypeAides { get; set; }
        public DbSet<TypeLitige> TypeLitiges { get; set; }
<<<<<<< HEAD
        public DbSet<RayonAction> RayonActions { get; set; }
=======
        public DbSet<Negociation> Negociations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membre>().HasMany(t => t.DemandesDeNegociation)
                .WithOne(g => g.Demandeur)
                .HasForeignKey(g => g.DemandeurId);
            modelBuilder.Entity<Membre>().HasMany(t => t.ReponsesDeNegociation)
                .WithOne(g => g.Repondeur)
                .HasForeignKey(g => g.RepondeurId).OnDelete(DeleteBehavior.Restrict);
        }
>>>>>>> 1642c2d7313e89d60735a9969fd5a7aa68ccbce3

    }
}
