using System;
using System.ComponentModel.DataAnnotations;

namespace EmekAkademisi.Models
{
    public class SalaryChart
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = "";

        public string ImagePath { get; set; } = "";

        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
