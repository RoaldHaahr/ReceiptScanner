using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptScanner.Models.EntityModels
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Foreign keys
        [Required]
        [ForeignKey("Currency")]
        public string Currency_Code { get; set; }

        // Navigation properties
        public virtual Currency Currency { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}