using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptScanner.Models.EntityModels
{
    public class Crop
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int X_Coordinate { get; set; }
        [Required]
        public int Y_Coordinate { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Height { get; set; }

        // Foreign keys
        [Required]
        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        [Required]
        [ForeignKey("Receipt")]
        public int Receipt_Id { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}