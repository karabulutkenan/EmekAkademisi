using System;
using System.ComponentModel.DataAnnotations;

namespace EmekAkademisi.Models
{
    public class PetitionSample
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(255)]
        public string Title { get; set; } = "";

        public string FilePath { get; set; } = "";

       

        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
