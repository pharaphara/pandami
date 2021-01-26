using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class Accepter
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime AcceptationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DesistementDate { get; set; }

        public Membre Membre { get; set; }

        public Feat Feat { get; set; }
    }
}
