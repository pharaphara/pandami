using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class PreferenceAide
    {
        public Membre Membre { get; set; } //propriété de navigation
        public int IdMembre { get; set; } //clé étreangère

        public TypeAide TypeAide { get; set; } //propriété de navigation
        public int IdTypeAide { get; set; } //clé étreangère

        [DataType(DataType.Date)]
        public DateTime ValiditeDebut { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValiditeFin { get; set; }
    }
}
