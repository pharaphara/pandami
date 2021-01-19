using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pandami.Models
{
    public class Membre
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        public string Nom { get; set; }
        public string Prenom { get; set; }

        [DataType(DataType.Date)]
        public DateTime Naissance { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [DataType(DataType.Date)]
        public DateTime Inscription { get; set; }

        [DataType(DataType.Password)]
        public string Mdp { get; set; }

        //Realation pour répondre
        public IList<Repondre> Inscriptions { get; set; } //collection d'inscription
       


    }
}
