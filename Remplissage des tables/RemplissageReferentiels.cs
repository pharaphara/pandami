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

                if (context.Adresses.Any())
                {
                    return;   // DB has been seeded
                }

                context.Adresses.AddRange(
                    new Adresse
                    {
                        NumeroDeVoie = "27",
                        NomDeVoie = "Avenue des Calquières",
                        CodePostale="12150",
                        Ville="Severac le Chateau",
                        latitude=12.451f,
                        longitude=11.4568f,


                    },

                    new Adresse
                    {
                        NumeroDeVoie = "28",
                        NomDeVoie = "Avenue des Calquières",
                        CodePostale = "12150",
                        Ville = "Severac le Chateau",
                        latitude = 12.451f,
                        longitude = 11.4568f,
                    },

                    new Adresse
                    {
                        NumeroDeVoie = "29",
                        NomDeVoie = "Avenue des Calquières",
                        CodePostale = "12150",
                        Ville = "Severac le Chateau",
                        latitude = 12.451f,
                        longitude = 11.4568f,
                    },

                    new Adresse
                    {
                        NumeroDeVoie = "30",
                        NomDeVoie = "Avenue des Calquières",
                        CodePostale = "12150",
                        Ville = "Severac le Chateau",
                        latitude = 12.451f,
                        longitude = 11.4568f,
                    },

                    new Adresse
                    {
                        NumeroDeVoie = "31",
                        NomDeVoie = "Avenue des Calquières",
                        CodePostale = "12150",
                        Ville = "Severac le Chateau",
                        latitude = 12.451f,
                        longitude = 11.4568f,
                    }
                );

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


                context.SaveChanges();
            }
        }
    }
}