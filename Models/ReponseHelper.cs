using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class ReponseHelper
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime AcceptationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DesistementDate { get; set; }

        public Membre Helper { get; set; }

        public Feat Feat { get; set; }
    }
}
