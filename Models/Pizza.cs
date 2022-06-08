using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models

{
    [Table("Pizza")]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(15, ErrorMessage = "Il nome non può avere più di 15 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [MiaValidazione.CinqueParole]
        public string Descrizione { get; set; }

        [Column("Immagine")]
        public string? ImgPath { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(1, 20, ErrorMessage = "Il prezzo può variare da 1 a 20 euro")]

        public string Prezzo { get; set; }

        [NotMapped()]
        public IFormFile File { get; set; }
    }

    
}
