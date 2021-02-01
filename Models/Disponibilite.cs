using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class Disponibilite
    {
        public int Id { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Début")]
        public DateTime DebutHeure { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Fin")]
        public DateTime FinHeure { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dispo du")]
        public DateTime ValiditeDebutDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Au")]
        public DateTime? ValiditeFinDate { get; set; }

        //une disponibilité possède un jour de la semaine
        public JourDeLaSemaine Jour { get; set; }

        //possède un créateur
        public Membre membre { get; set; }
    }
}
