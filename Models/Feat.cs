﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pandami.Models
{
    public class Feat
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "JOUR DU FEAT")]
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
        public float? SommePrevoir { get; set; }

        [DataType(DataType.Currency)]
        public float? SommeAvancee { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SommeRembourseeDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AnnulationDate { get; set; }

        public Boolean EchangeMonetaire { get; set; }

        //posède un créateur
        [Display(Name ="GIFTEE")]
        public Membre Createur { get; set; }

        //collection de réponse
        public IList<ReponseHelper> Reponses { get; set; }

        //posède un type d'aide
        [Display(Name = "TYPE DE FEAT")]
        public TypeAide Type { get; set; }

        //possède un matériel
        public Materiel Materiel { get; set; }

        //possède une adresse
        public Adresse Adresse { get; set; }

        //possède une collection de litiges
        public ICollection<Litige> Litiges { get; set; }

       //possède une collection de négociations
       public ICollection<Negociation> Negociations { get; set; }

    }
}
