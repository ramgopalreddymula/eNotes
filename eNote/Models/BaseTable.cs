using System;
using SQLite;

namespace eNote
{
    
    public class BaseTable
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
