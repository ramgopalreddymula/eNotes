using System;
using SQLite;

namespace eNote
{
    [Table("Expenses")]
    public class Expenses : BaseTable
    {
        public double TotalAmount { get; set; }
        public double SpendingAmount { get; set; }
        public string Description { get; set; }
        public double CurrentBalance { get; set; }
        public string UserName { get; set; }
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
        public string UserName { get; set; }
        [Ignore]
        public string ExpectedDate
        {
            get
            {
                return "ExpectedDate";
            }
        }
        [Ignore]
        public string ReminderDate
        {
            get
            {
                if((Reminder - DateTime.UtcNow.Date).TotalDays<=2)
                {
                    return "ReminderExpDate";
                }
                else
                {
                    return "ReminderDate";
                }

            }
        }


    }
}
