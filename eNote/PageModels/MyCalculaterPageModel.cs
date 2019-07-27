using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class MyCalculaterPageModel : FreshBasePageModel
    {
        public double PrincipleAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double NewSalary { get; set; }
        public double OldSalary { get; set; }
        public double HikePercent { get; set; }
        public double InterestBalance { get; set; }
        public double Days { get; set; }
        public double Years { get; set; }
        public double TotalAmount { get; set; }
        public double HikeAmount { get; set; }
        public DateTime MinDate { get; set; }

        public DateTime MaxDate
        {
            get; set;
        }

        public string SalaryHikeButtonText { get; set; }
        public string SalaryHikeAmountButtonText { get; set; }
        public string RateOfInterstButtonText { get; set; }
        public string PrincipleAmountLableText { get; set; }
        public string InterestRateLableText { get; set; }
        public string NewSalaryLableText { get; set; }
        public string OldSalaryLableText { get; set; }
        public string HikePercentLableText { get; set; }
        public string HikeAmountLableText { get; set; }
        public string InterestBalanceLableText { get; set; }
        public string DaysLableText { get; set; }
        public string TotalAmountLableText { get; set; }
        public string StartDateLableText { get; set; }
        public string EndDateLableText { get; set; }

        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }

        public string CalculateButtonText { get; set; }
        public string ResetButtonText { get; set; }
        public string ViewButtonText { get; set; }

        public bool IsSalaryHikeInter { get; set; }
        public bool IsRateOfInterst { get; set; }
        public bool IsSalaryHikeAmount { get; set; }

        public MyCalculaterPageModel()
        {
            CalculateButtonText = "CALCULATE";
            ResetButtonText = "RESET";
            ViewButtonText = "VIEW";
            SalaryHikeButtonText = "Salary Hike Percent";
            SalaryHikeAmountButtonText = "Salary Hike Amount";
            RateOfInterstButtonText = "Rate Of Interst";
            PrincipleAmountLableText = "Principle Amount";
            InterestRateLableText = "Interest Rate ";
            NewSalaryLableText = "New Salary Amount";
            OldSalaryLableText = "Old Salary Amount";
            HikePercentLableText = "Hike Percent";
            HikeAmountLableText = "Hike Amount";
            InterestBalanceLableText = "Total Interest ";
            DaysLableText = "Days";
            TotalAmountLableText = "Total Amount";
            StartDateLableText = "Start Date";
            EndDateLableText = "End Date";
            MinDate = DateTime.UtcNow.AddYears(-3);
            MaxDate = DateTime.UtcNow.AddYears(100);
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;
            //IsSalaryHikeInter = true;
            IsRateOfInterst = true;
            //IsSalaryHikeAmount = true;
            ResetView();
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
        }
        private void ResetView()
        {
            Days = 0;
            HikeAmount = 0;
            HikePercent = 0;
            NewSalary = 0;
            Years = 0;
            InterestRate = 0;
            InterestBalance = 0;
            TotalAmount = 0;
            OldSalary = 0;
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
        }
        private void GetSalaryIncPer()
        {
            HikePercent = ((NewSalary - OldSalary) / OldSalary) * 100;
        }
        private void GetSalaryIncAmount()
        {
            HikeAmount = (OldSalary * (HikePercent/100));
            NewSalary = HikeAmount + OldSalary;
        }
        private void GetRateOfInterAmount()
        {
            TimeSpan timeDiff = EndDate - StartDate;
            Days = timeDiff.TotalDays;
            Years = Days / 365;
            InterestBalance=(PrincipleAmount * InterestRate * Days) / 3000;
            TotalAmount = PrincipleAmount + InterestBalance;
        }
        public Command SalaryHikeEnableCommand
        {
            get
            {
                return new Command(() => {
                    IsSalaryHikeInter = true;
                    IsRateOfInterst = false;
                    IsSalaryHikeAmount = false;
                });
            }
        }
        public Command SalaryIncAmountEnableCommand
        {
            get
            {
                return new Command(() => {
                    IsSalaryHikeInter = false;
                    IsRateOfInterst = false;
                    IsSalaryHikeAmount = true;


                });
            }
        }
        public Command RateOfInterstEnableCommand
        {
            get
            {
                return new Command(() => {
                    IsSalaryHikeInter = false;
                    IsRateOfInterst = true;
                    IsSalaryHikeAmount = false;
                });
            }
        }

        public Command SalaryHikePerCommand
        {
            get
            {
                return new Command(() => {
                    GetSalaryIncPer();
                });
            }
        }
        public Command SalaryIncAmountCommand
        {
            get
            {
                return new Command(() => {
                    GetSalaryIncAmount();


                });
            }
        }
        public Command RateOfInterstCommand
        {
            get
            {
                return new Command(() => {
                    GetRateOfInterAmount();
                });
            }
        }

        public Command ViewCommand
        {
            get
            {
                return new Command(() => {
                    GetSalaryIncPer();
                });
            }
        }
        public Command ResetCommand
        {
            get
            {
                return new Command(() => {
                    ResetView();
                });
            }
        }
    }
}
