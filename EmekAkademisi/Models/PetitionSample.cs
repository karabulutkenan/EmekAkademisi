using System;
using System.ComponentModel.DataAnnotations;

namespace EmekAkademisi.Models
{
    public class PetitionSample
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = "";

        public string FilePath { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string FileType { get; set; } = "";

        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
