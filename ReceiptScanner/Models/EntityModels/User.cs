using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptScanner.Models.EntityModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Default_Account { get; set; }

        // Foreign keys
        [ForeignKey("Language")]
        public string Language_Code { get; set; }

        // Navigation properties
        public virtual Language Language { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}