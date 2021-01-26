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
        public DbSet<Accepter> Accepters { get; set; }
        public DbSet<Sexe> Sexes { get; set; }
        public DbSet<Suspension> Suspensions { get; set; }
        public DbSet<TypeAide> TypeAides { get; set; }
        public DbSet<TypeLitige> TypeLitiges { get; set; }

        
    }
}
