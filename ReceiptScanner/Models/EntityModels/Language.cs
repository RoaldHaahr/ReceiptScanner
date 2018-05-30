using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReceiptScanner.Models.EntityModels
{
    public class Language
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<User> User { get; set; }
    }
}