﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AccountForCreationDto
    {
        public DateTime DateCreated { get; set; }
        public string AccountType { get; set; }
        public int OwnerId { get; set; }
       public IEnumerable<AccountDto> Accounts { get; set; }

    }
}
