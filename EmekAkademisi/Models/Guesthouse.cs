using System;
using System.ComponentModel.DataAnnotations;

namespace EmekAkademisi.Models
{
    public class Guesthouse
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = "";

        [Required]
        [RegularExpression(@"^(\+90|0)?5\d{9}$", ErrorMessage = "Lütfen Türkiyeye Ait Bir Telefon Girin.")]
        [StringLength(20)]
        public string Phone { get; set; } = "";
    }
}
