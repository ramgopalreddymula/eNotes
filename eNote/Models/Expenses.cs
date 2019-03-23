using System;
using SQLite;

namespace eNote.Models
{
    [Table("Expenses")]
    public class Expenses : BaseTable
    {
        public double TotalAmount { get; set; }
        public double SpendingAmount { get; set; }
        public string Description { get; set; }
        public double CurrentBalance { get; set; }

    }

    [Table("Purchase")]
    public class Purchase : BaseTable
    {
        public string ItemName { get; set; }
        public DateTime ExpectedPurchaseDate { get; set; }
        public string ReasonForPurchasing { get; set; }
        public double ExpectedAmount { get; set; }
        public DateTime Reminder { get; set; }
        public bool RemindMe { get; set; }
        public bool AutoMessage { get; set; }
        public string PhoneNumber { get; set; }
    }
}
