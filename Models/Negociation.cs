using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandami.Models
{
    public class Negociation
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreationNegociation { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateClotureNegociation { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date du Feat proposée")]
        public DateTime NewDateProposee { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Début proposé")]
        public DateTime HeureDbtProposee { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Fin proposée")]
        public DateTime HeureFinProposee { get; set; }

        public bool IsAccepted { get; set; }



        //possède un Feat
        public Feat feat { get; set; }

        //possède un demandeur
        public Membre Demandeur { get; set; }
        public int DemandeurId { get; set; }

        //possède un repondeur
        public Membre Repondeur { get; set; }
        public int RepondeurId { get; set; }
    }
}
