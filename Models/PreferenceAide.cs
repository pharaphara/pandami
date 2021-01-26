using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class PreferenceAide
    {
        public int Id { get; set; }

        public Membre Membre { get; set; } 
        
        public ICollection<TypeAide> ListTypeAide { get; set; } 

        public float RayonActionKm { get; set; }


        [DataType(DataType.Date)]
        public DateTime ValiditeDebut { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValiditeFin { get; set; }
    }
}