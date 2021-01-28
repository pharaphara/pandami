using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class RayonAction
    {
        public int Id { get; set; }

        public float Rayon { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValiditeDebut { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValiditeFin { get; set; }

        public Membre Membre { get; set; }
    }
}
