using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Pandami.Models
{
    public class Suspension
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DebutDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FinDate { get; set; }

    }
}
