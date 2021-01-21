using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Pandami.Data;
using Pandami.Models;

namespace Pandami.Remplissage_des_tables
{
    public class RemplissageReferentiels
    {
public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataContext>>()))
            {
                
                if (context.Sexes.Any())
                {
                    return;   // DB has been seeded
                }

                context.Sexes.AddRange(
                    new Sexe
                    {
                        NomSexe = "Homme"
                       },

                    new Sexe
                    {
                        NomSexe = "Femme"
                    }
                );

                if (context.JourDeLaSemaines.Any())
                {
                    return;   // DB has been seeded
                }

                context.JourDeLaSemaines.AddRange(
                    new JourDeLaSemaine
                    {
                        NomDuJour = "Lundi"

                    },

                    new JourDeLaSemaine
                    {
                        NomDuJour = "Mardi"

                    },

                    new JourDeLaSemaine
                    {
                        NomDuJour = "Mercredi"

                    },

                    new JourDeLaSemaine
                    {
                        NomDuJour = "Jeudi"

                    },

                    new JourDeLaSemaine
                    {
                        NomDuJour = "Vendredi"

                    },

                    new JourDeLaSemaine
                    {
                        NomDuJour = "Samedi"

                    },

                    new JourDeLaSemaine
                    {
                        NomDuJour = "Dimanche"

                    }
                );

                if (context.CategorieAides.Any())
                {
                    return;   // DB has been seeded
                }

                context.CategorieAides.AddRange(
                    new CategorieAide
                    {
                        Categorie = "Extérieur"
                    },

                    new CategorieAide
                    {
                        Categorie = "Intérieur"
                    }
                );

                if (context.TypeAides.Any())
                {
                    return;   // DB has been seeded
                }

                context.TypeAides.AddRange(
                    new TypeAide
                    {
                        NomAide = "Jardiange"
                        
                    },

                    new TypeAide
                    {
                        NomAide = "Course"
                    },

                    new TypeAide
                    {
                        NomAide = "Récupération de colis"
                    },

                    new TypeAide
                    {
                        NomAide = "Promenade des animaux"
                    },

                    new TypeAide
                    {
                        NomAide = "Deplacer une charge lourde"
                    },

                    new TypeAide
                    {
                        NomAide = "Bricolage"
                    }
                );


                context.SaveChanges();
            }
        }
    }
}