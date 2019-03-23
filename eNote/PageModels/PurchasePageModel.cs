using System;
using System.Collections.ObjectModel;
using eNote.Models;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class PurchasePageModel : FreshBasePageModel
    {
        public PurchasePageModel()
        {
            PurchaseList = new ObservableCollection<Purchase>();
            PurchaseList.Add(new Purchase() { ItemName = "gdjfhgfjdfgdjhfg", ExpectedPurchaseDate = DateTime.Now, ReasonForPurchasing = "Small_Microphone", ExpectedAmount = 600.00 , Reminder =DateTime.Now, RemindMe =false, AutoMessage =false,PhoneNumber="2212"});
            PurchaseList.Add(new Purchase() { ItemName = "gdjfhgfjdfgdjhfg", ExpectedPurchaseDate = DateTime.Now, ReasonForPurchasing = "Small_Microphone", ExpectedAmount = 600.00, Reminder = DateTime.Now, RemindMe = false, AutoMessage = false, PhoneNumber = "2212" });
            PurchaseList.Add(new Purchase() { ItemName = "gdjfhgfjdfgdjhfg", ExpectedPurchaseDate = DateTime.Now, ReasonForPurchasing = "Small_Microphone", ExpectedAmount = 600.00, Reminder = DateTime.Now, RemindMe = false, AutoMessage = false, PhoneNumber = "2212" });


            DeleteButtonText = "Delete";
            ClearButtonText = "Clear";
            AddButtonText = "Add/Update";
            MinDate = DateTime.UtcNow;
            MaxDate = DateTime.UtcNow.AddYears(100);
        }

        #region Properties
        public string ItemText { get; set; }
        public string ExpectedAmountText { get; set; }
        public string ReasonForPurchasingText { get; set; }
        public string ExpectedPurchaseDateText { get; set; }
        public string ReminderText { get; set; }
        public string DeleteButtonText { get; set; }
        public string AddButtonText { get; set; }
        public string ClearButtonText { get; set; }
        public string AutoMessageText { get; set; }
        public string PhoneNumberText { get; set; }

        public string ErrorResponce { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        public string ItemName { get; set; }
        public DateTime ExpectedPurchaseDate { get; set; }
        public string ReasonForPurchasing { get; set; }
        public double ExpectedAmount { get; set; }
        public DateTime Reminder { get; set; }
        public bool AutoMessage { get; set; }
        public string PhoneNumber { get; set; }

        #endregion

        #region OverrideMethods
        public override void Init(object initData)
        {
            if (initData != null)
            {
                var Users = (Users)initData;

            }

        }

        #endregion

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

        }
        #region Commands
        public Command AddCommand
        {
            get
            {
                return new Command(async () => {
                    /*if (!string.IsNullOrEmpty(UserName))
                    {
                        if (App.database.IsUserExist(UserName.ToLower()))
                        {
                            if (App.database.IsValidUser(UserName.ToLower(), Password))
                            {


                                Global.isLoginThrough = true;
                                await CoreMethods.PushPageModel<NotesListPageModel>();
                               
                            }
                            else
                            {
                                DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginCredtionalsFail);

                            }
                        }
                        else
                        {
                           
                        }
                    }
                    else
                        DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginCredtionalsFail);*/

                });
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command(async () => {

                });
            }
        }
        public Command LoginWithGmailCommand
        {
            get
            {
                return new Command(async () => {

                });
            }
        }

        public ObservableCollection<Purchase> PurchaseList { get; set; }

        Purchase _selectedItem;

        public Purchase SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                if (value != null)
                    ListItemSelected.Execute(value);
            }
        }
        public Command<Purchase> ListItemSelected
        {
            get
            {
                return new Command<Purchase>(async (users) => {
                    _selectedItem = null;
                    await CoreMethods.PushPageModel<NotesDetailPageModel>(users);
                });
            }
        }
        #endregion
    }
}
