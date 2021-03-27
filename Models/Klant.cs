using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oefententamen.Models
{
    public class Klant
    {
        [Key]
        public int Id { get; set; }

        [MinLength(2)]
        public string Naam { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Woonplaats { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [NotMapped]
        public int Prijs { get; set; }

        public ICollection<Reservering> Reserveringen { get; set; }
    }
}