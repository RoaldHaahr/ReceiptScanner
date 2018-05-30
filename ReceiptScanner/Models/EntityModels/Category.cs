using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReceiptScanner.Models.EntityModels
{

    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual ICollection<Crop> Crops { get; set; }
    }
}