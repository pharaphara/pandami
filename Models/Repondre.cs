using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class Repondre
    {
        public Membre Membre { get; set; } //propriété de navigation
        public int IdMembre { get; set; } //clé étreangère

        public Feat Feat { get; set; } //propriété de navigation
        public int IdFeat { get; set; } //clé étreangère

        [DataType(DataType.Date)]
        public DateTime AcceptationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DesistementDate { get; set; }
    }
}
