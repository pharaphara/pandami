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
        public DateTime DebutHeure { get; set; }

        [DataType(DataType.Time)]
        public DateTime FinHeure { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValiditeDebutDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValiditeFinDate { get; set; }

        //une disponibilité possède un jour de la semaine
        public JourDeLaSemaine Jour { get; set; }
    }
}
