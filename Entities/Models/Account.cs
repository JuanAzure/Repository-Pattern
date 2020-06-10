using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace Entities.Models
{

    [Table("account")]
    public class Account
    {

        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Date created requerid")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "AccountType requerid")]
        public string AccountType { get; set; }

        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}
