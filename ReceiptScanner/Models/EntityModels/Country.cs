using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptScanner.Models.EntityModels
{

    public class Country
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        // Foreign keys
        [ForeignKey("Currency")]
        public string Currency_Code { get; set; }

        // Navigation properties
        public virtual ICollection<Store> Stores { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}