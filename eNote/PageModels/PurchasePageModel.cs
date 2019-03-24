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
        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }
        public bool IsDelete { get; set; }
        public int PurchaseId { get; set; }
        public PurchasePageModel()
        {
            PurchaseList = new ObservableCollection<Purchase>();
           
            PurchaseList = new ObservableCollection<Purchase>(App.database.GetAllPurchaseList(StringValues.UserName));

            DeleteButtonText = "Delete";
            ClearButtonText = "Clear";
            AddButtonText = "Add";
            MinDate = DateTime.UtcNow;
            MaxDate = DateTime.UtcNow.AddYears(100);
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;
            IsDelete = false;
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
                    if (!string.IsNullOrEmpty(StringValues.UserName))
                    {
                        Purchase purchaseItems = new Purchase();
                        purchaseItems.AutoMessage = AutoMessage;
                        purchaseItems.ExpectedAmount = ExpectedAmount;
                        purchaseItems.ExpectedPurchaseDate = ExpectedPurchaseDate;
                        purchaseItems.ItemName = ItemName;
                        purchaseItems.Reminder = Reminder;
                        purchaseItems.AutoMessage = AutoMessage;
                        purchaseItems.ReasonForPurchasing = ReasonForPurchasing;
                        purchaseItems.PhoneNumber = PhoneNumber;
                        purchaseItems.UserName = StringValues.UserName;
                        purchaseItems.ModifiedDate = DateTime.UtcNow.Date;
                        purchaseItems.CreateDate = DateTime.UtcNow.Date;
                        if(IsDelete)
                        {
                            purchaseItems.Id = PurchaseId;
                        }
                        if (App.database.AddOrUpdatePurchaseDetails(purchaseItems))
                        {
                            PurchaseList = new ObservableCollection<Purchase>(App.database.GetAllPurchaseList(StringValues.UserName));
                            ClearValues();
                        }
                        else
                        {
                            DependencyService.Get<IToast>().Show(ErrorStrings.PurchaseSave);
                        }
                    }
                    

                });
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command(async () => {
                    if (PurchaseId != 0)
                    {
                        bool resp = App.database.DeletePurchase(StringValues.UserName, PurchaseId);
                        if (resp)
                        {
                            PurchaseList = new ObservableCollection<Purchase>(App.database.GetAllPurchaseList(StringValues.UserName));
                            ClearValues();
                            DependencyService.Get<IToast>().Show("Deleted");
                        }
                        else
                        {
                            DependencyService.Get<IToast>().Show("Failed, Please try again");
                        }
                    }
                });
            }
        }
        private void ClearValues()
        {
            AutoMessage = false;
            ExpectedAmount = 0;
            ExpectedPurchaseDate = DateTime.UtcNow;
            ItemName = string.Empty;
            Reminder = DateTime.UtcNow;
            AutoMessage = false;
            ReasonForPurchasing = string.Empty;
            PhoneNumber = string.Empty;
            IsDelete = false;
            AddButtonText = "Add";
            PurchaseId = 0;
        }
        public Command ClearCommand
        {
            get
            {
                return new Command(async () => {
                    ClearValues();
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
                    var action = await CoreMethods.DisplayAlert("Delete/View!", "You want to Delete or  View?", "Delete", "View");
                    switch (action)
                    {
                        case true:
                            try
                            {
                                bool resp=App.database.DeletePurchase(users.UserName, users.Id);
                                if(resp)
                                {
                                    PurchaseList = new ObservableCollection<Purchase>(App.database.GetAllPurchaseList(StringValues.UserName));

                                    DependencyService.Get<IToast>().Show("Deleted");
                                }
                                else
                                {
                                    DependencyService.Get<IToast>().Show("Failed, Please try again");
                                }
                            }
                            catch (Exception ex)
                            {
                                DependencyService.Get<IToast>().Show("Failed, Please try again");
                            }

                            break;
                        case false:
                            {
                                IsDelete = true;
                                AddButtonText = "Update";
                                AutoMessage =users.AutoMessage;
                                ExpectedAmount=users.ExpectedAmount ;
                                ExpectedPurchaseDate=users.ExpectedPurchaseDate ;
                                ItemName=users.ItemName ;
                                Reminder=users.Reminder ;
                                AutoMessage=users.AutoMessage ;
                                ReasonForPurchasing=users.ReasonForPurchasing ;
                                PhoneNumber=users.PhoneNumber;
                                PurchaseId = users.Id;
                            }
                            break;
                        default:
                            break;
                    }
                });
            }
        }
        #endregion
    }
}
