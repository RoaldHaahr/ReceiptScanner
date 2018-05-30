using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptScanner.Models.EntityModels
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Code { get; set; }
        public string Symbol { get; set; }
        [Required]
        public double Exchange_Rate { get; set; }

        // Navigation properties
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Account> Account { get; set; }
    }
}