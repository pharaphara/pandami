using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandami.Models
{
    public class Litige
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClotureDate { get; set; }
        public string Commentaire { get; set; }

        //possède un type 
        public TypeLitige TypeLitige { get; set; }

        //possède un créateur
        public Membre Createur { get; set; }

        //possède un feat
        public Feat Feat { get; set; }






    }
}
