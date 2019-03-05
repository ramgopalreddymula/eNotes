using System;
using SQLite;

namespace eNote
{
    [Table("AudioFiles")]
    public class AudioFiles : BaseTable
    {
       
        public string AudioPath { get; set; }
        public string AudioFileName { get; set; }
        [Ignore]
        public string Image { get; set; }
        public string UserName { get; set; }

    }
}
