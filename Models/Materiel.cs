using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandami.Models
{
    public class Materiel
    {
        public int Id { get; set; }


        [Display(Name = "Matériel nécessaire")]
        public string NomMateriel { get; set; }

        public ICollection<Feat>feats { get; set; }

    }
}
