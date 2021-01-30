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

        [Display(Name = "Aide demandée")]
        public string NomAide { get; set; }




        //possède une catégorie
        public CategorieAide CategorieAide { get; set; }


        //possède une collection de préférence
        public ICollection<PreferenceAide> PreferenceAides { get; set; }

        //Possède une liste de feat
        public ICollection<Feat> Feats { get; set; }
    }
}
