using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReceiptScanner.Models.EntityModels
{
    public class UserAccount
    {
        [Key, Column(Order = 0), ForeignKey("Account")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Account_Id { get; set; }
        [Key, Column(Order = 1), ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int User_Id { get; set; }
        public bool Is_Admin { get; set; }

        // Foreign keys
        public virtual Account Account { get; set; }
        public virtual User User { get; set; }
    }
}