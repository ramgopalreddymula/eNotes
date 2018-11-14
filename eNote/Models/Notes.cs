using System;
using SQLite;

namespace eNote
{
    [Table("Notes")]
    public class Notes : BaseTable
    {
        public string NotesTitle { get; set; }
        public string NotesDescription { get; set; }
        public string Image { get; set; }
        
        public string PIN { get; set; }
        
        public string UserName { get; set; }
    }
}
