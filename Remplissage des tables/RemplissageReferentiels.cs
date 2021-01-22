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
                context.SaveChanges();

                var exterieur = (from m in context.CategorieAides
                                               where m.Id.Equals(2)
                                               select m).FirstOrDefault();

                var interieur = (from m in context.CategorieAides
                                 where m.Id.Equals(1)
                                 select m).FirstOrDefault();
             

                context.TypeAides.AddRange(
                    new TypeAide
                    {
                        NomAide = "Jardinage",
                        CategorieAide = exterieur

                    },

                    new TypeAide
                    {
                        NomAide = "Course",
                        CategorieAide = exterieur
                    },

                    new TypeAide
                    {
                        NomAide = "Récupération de colis",
                        CategorieAide = exterieur
                    },

                    new TypeAide
                    {
                        NomAide = "Promenade des animaux",
                        CategorieAide = exterieur
                    },

                    new TypeAide
                    {
                        NomAide = "Déplacer une charge lourde",
                        CategorieAide = interieur
                    },

                    new TypeAide
                    {
                        NomAide = "Bricolage",
                        CategorieAide = interieur
                    },

                    new TypeAide
                    {
                         NomAide = "Aide pour se déplacer",
                         CategorieAide = exterieur
                    },

                    new TypeAide
                    {
                        NomAide = "Ménage",
                        CategorieAide = interieur
                    },

                    new TypeAide
                    {
                        NomAide = "Administratif",
                        CategorieAide = interieur
                    },

                    new TypeAide
                    {
                        NomAide = "Informatique",
                        CategorieAide = interieur
                    },

                    new TypeAide
                    {
                        NomAide = "Visite de courtoisie",
                        CategorieAide = interieur
                    },

                    new TypeAide
                    {
                        NomAide = "Soutien scolaire",
                        CategorieAide = interieur
                    }


                );
               




                context.SaveChanges();
            }
        }
    }
}