using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteDemoRazor.Models
{
    public class Personne
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }

        [Required]
        [StringLength(50, MinimumLength =2)]
        public string Prenom { get; set; }

        [Required]
        [Range(18,99)]
        public int Age { get; set; }

        public string MoyenDeLocomotion { get; set; }
        public List<string> Sports { get; set; }
    }
}