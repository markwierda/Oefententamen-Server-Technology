using System.ComponentModel.DataAnnotations;

namespace Oefententamen.Models
{
    public class Reservering
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Naam { get; set; }

        public int? KlantId { get; set; }
        public bool Bezet { get; set; }
        public Klant Klant { get; set; }
    }
}