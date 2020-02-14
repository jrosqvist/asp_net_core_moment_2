using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Moment2.Models
{
    public class Movie
    {

        public Movie()
        {
            // Genererar ett nytt id vid instansiering
            this.Id = GenerateID();
        }

        [Key]
        public string Id { get; set; }

        [Display(Name = "Titel")]
        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [MaxLength(128, ErrorMessage ="Max 128 tecken!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        public string Genre { get; set; }

        [Display(Name = "Speltid (minuter)")]
        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Bara siffror tack!")]
        [Range(1, 600, ErrorMessage = "Här godkänns bara filmer som är mellan 1 och 600 minuter!")]
        public int? PlayTime { get; set; }

        [Display(Name = "Utgivningsår")]
        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Bara siffror tack!")]
        [Range(1895, 2100, ErrorMessage ="Måste vara mellan 1895 och 2100")]
        public int? ReleaseYear { get; set; }


        [Display(Name = "Betyg")]
        [Required]
        [Range(1, 5, ErrorMessage = "Välj ett alternativ (1 - 5)!")]
        public int Ratings { get; set; }

        // Metod som genererar ett unikt id
        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }

    }

}
