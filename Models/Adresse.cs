using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandami.Models
{
    public class Adresse
    {
        public int Id { get; set; }
        public string NumeroDeVoie { get; set; }
        public string NomDeVoie { get; set; }
        public string CodePostale { get; set; }
        public string Ville { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
    }
}
