using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace Pandami.Models
{
    public class TypeAide
    {
        public int Id { get; set; }


        public string NomAide { get; set; }


        //collection de de feat
        public IList<Feat> Feats { get; set; }

        //possède une catégorie
        public CategorieAide CategorieAide { get; set; }

        //posède un matériel

        public Materiel Materiel { get; set; }

        //possède une collection de préférence
        public ICollection<PreferenceAide> PreferenceAides { get; set; }
    }
}
