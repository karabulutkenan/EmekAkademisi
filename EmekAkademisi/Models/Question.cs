using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmekAkademisi.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(1000)]
        public string ShortAnswer { get; set; } = "";

        public string DetailAnswer { get; set; } = "";

        [Required]
        [StringLength(255)]
        public string SourcePerson { get; set; } = "";

        public string AnsweredBy { get; set; } = "";

        public string References { get; set; } = "";

        public string Status { get; set; } = "";

        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
