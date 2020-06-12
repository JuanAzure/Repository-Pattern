using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public class AccountResulDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string AccountType { get; set; }                
    }
}
