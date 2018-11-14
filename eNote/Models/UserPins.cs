using System;
using SQLite;

namespace eNote
{
    [Table("UserPins")]
    public class UserPins : BaseTable
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string Hint { get; set; }
        
        public bool IsPinEnable { get; set; }


    }
}
