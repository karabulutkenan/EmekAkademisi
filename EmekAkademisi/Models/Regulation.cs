using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmekAkademisi.Models
{
    public class Regulation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = "";

        public string FilePath { get; set; } = "";

        public DateTime UploadDate { get; set; } = DateTime.Now; // Varsayılan olarak şu anki tarihi ayarlar
    }
}
