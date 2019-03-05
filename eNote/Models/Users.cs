﻿using System;
using SQLite;

namespace eNote
{
    [Table("Users")]
    public class Users : BaseTable
    {
        public string FullName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string Passcode { get; set; }
        
        public bool IsPinEnable { get; set; }

    }
}
