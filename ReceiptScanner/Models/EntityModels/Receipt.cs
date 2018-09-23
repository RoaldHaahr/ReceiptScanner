using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptScanner.Models.EntityModels
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        //[Required]
        public string Content { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }
        //[Required]
        public string Base64_Image { get; set; }

        // Foreign keys
        //[ForeignKey("Store")]
        //public int Store_Id { get; set; }
        //[Required]
        //[ForeignKey("Currency")]
        //public string Currency_Id { get; set; }
        //[Required]
        //[ForeignKey("Account")]
        //public int Account_Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int User_Id { get; set; }

        // Navigation properties
        //public virtual Account Account { get; set; }
        //public virtual Store Store { get; set; }
        //public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
        //public virtual ICollection<Crop> Crops { get; set; }
    }
}