using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AccountForUpdateDto
    {
        public DateTime DateCreated { get; set; }
        public string AccountType { get; set; }
        public int OwnerId { get; set; }
       public IEnumerable<OwnerForCreationDto> owners { get; set; }

    }
}
