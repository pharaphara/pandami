using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class Feat
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime RealisationDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime HeureDebut { get; set; }

        [DataType(DataType.Time)]
        public DateTime HeureFin { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AcceptationDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime? EnCoursRealisation { get; set; }

        [DataType(DataType.Time)]
        public DateTime? SurPlace { get; set; }

        [DataType(DataType.Time)]
        public DateTime? FinFeatHelper { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ClotureDate { get; set; }

        [DataType(DataType.Currency)]
        public DateTime? SommePrevoir { get; set; }

        [DataType(DataType.Currency)]
        public float? SommeAvancee { get; set; }

        [DataType(DataType.Date)]
        public float? SommeRembourseeDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AnnulationDate { get; set; }

    //posède un créateur
    public Membre Createur { get; set; }

        //collection de réponse
        public IList<Repondre> Reponses { get; set; }

    //posède un type d'aide
    public TypeAide Type { get; set; }

    //possède une adresse
    public Adresse Adresse { get; set; }

        //possède une collection de litige
        public ICollection<Litige> Litiges { get; set; }








    }
}
