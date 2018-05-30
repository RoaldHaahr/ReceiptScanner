using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptScanner.Models.EntityModels
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int Street_Number { get; set; }
        public string City { get; set; }
        public string ZIP_Code { get; set; }
        public string State { get; set; }

        // Foreign keys
        [ForeignKey("Country")]
        public string Country_Code { get; set; }

        // Navigation properties
        public virtual Country Country { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}