using System;
using FreshMvvm;
using PropertyChanged;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class ExpensesPageModel : FreshBasePageModel
    {
        #region Properties  

        public string TotalAmountTitleText { get; set; }
        public string SpendingAmountText { get; set; }
        public string DescriptionText { get; set; }
        public string CurrentBalanceText { get; set; }
        public string TotalSpendingAmountText { get; set; }
        public string DeleteButtonText { get; set; }
        public string AddButtonText { get; set; }
        public string ErrorResponce { get; set; }

        public double TotalAmount { get; set; }
        public double SpendingAmount { get; set; }
        public string Description { get; set; }
        public double CurrentBalance { get; set; }
        public double TotalSpendingAmount { get; set; }
        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }
        #endregion

        public ExpensesPageModel()
        {
        }
    }
}
