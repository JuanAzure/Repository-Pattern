using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("account")]
    public class Account
    {
        [Column("AccountId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date created requerid")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "AccountType requerid")]
        public string AccountType { get; set; }

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }

        public Owner owner { get; set; }
    }
}
