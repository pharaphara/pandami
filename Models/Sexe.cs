using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pandami.Data;

namespace Pandami.Models
{
    public class Sexe
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string NomSexe { get; set; }

        



    }
}
