using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("owner")]
    public class Owner
    {
        [Column("OwnerId")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is requerid")]
        [StringLength(60,ErrorMessage ="Name can`t be than 60 characters")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Date of birth is requerid")]        
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is requerid")]
        [StringLength(100, ErrorMessage = "Name can`t be than 100 characters")]
        public string Address { get; set; }

        public ICollection<Account>Accounts { get; set; }
    }
}
