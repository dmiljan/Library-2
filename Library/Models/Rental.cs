using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Rental
    {
        public int Id { get; set; }
        [Required]
        public String RentingDate { get; set; }
        //public DateTime RentingDate { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}
