using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class Sexe
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string NomSexe { get; set; }
        
       

    }
}
