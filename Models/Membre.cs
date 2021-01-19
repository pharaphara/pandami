﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class Membre
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Nom { get; set; }

        [MaxLength(50)]
        public string Prenom { get; set; }

        [DataType(DataType.Date)]
        public DateTime Naissance { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }


        
        public DateTime Inscription { get; set; }

        [DataType(DataType.Password)]
        public string Mdp { get; set; }

        //Ici commence les relations

        //Possède une collection d'inscription
        public IList<Repondre> Inscriptions { get; set; }

        //Possède une liste de feat
        public ICollection<Feat> Feats { get; set; }

        //Un membre posède un sexe
        public Sexe Sexe { get; set; }

        //Un membre possède une collection de suspensions
        public ICollection<Suspension> Suspensions { get; set; }

        //Un membre possède une collection de disponibilités
        public ICollection<Disponibilite> Disponibilites { get; set; }

        //possède une collection de préférence d'aide
        public ICollection<PreferenceAide> PreferenceAides { get; set; }

        //possède une collection de litige
        public ICollection<Litige> Litiges { get; set; }

        //possède une adresse
        public Adresse Adresse { get; set; }
    }
}
