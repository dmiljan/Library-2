using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Writer { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public int PagesNumber { get; set; }
        [Required]
        public int Available { get; set; }
    }
}
