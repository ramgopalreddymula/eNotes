using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class SignupPageModel : FreshBasePageModel
    {
        #region Test CustomView
        public ICommand TappedCommand { get; set; }
        public ICommand SlideOpenCommand { get; set; }
        public double DefaultHeight { get; set; }
        public double DefaultWidth { get; set; }

        public bool IsSlide { get; set; }

       

        private void CloseMenu()
        {
            IsSlide = false;
        }

        private void SlideOpen()
        {
            if (IsSlide)
            {
                IsSlide = false;
            }
            else
            {
                IsSlide = true;
            }
        }
        #endregion
        #region Properties  
        public string FullName { get; set; }
        public string RegisterTitleText { get; set; }
        public string UsernameText { get; set; }
        public string PasswordText { get; set; }
        public string ConfPasswordText { get; set; }
        public string RegisterButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }
        public string ErrorResponce { get; set; }
        public List<Users> UsersList { get; set; }
        #endregion

        #region Constructor
        public SignupPageModel()
        {
           
            //Console.WriteLine(resp.Count);
            //App.database.DeleteUser("ram9988");
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;
            //#region TestView
            //TappedCommand = new Command(CloseMenu);
            //SlideOpenCommand = new Command(SlideOpen);
            //DefaultHeight = App.Height;
            //DefaultWidth = App.Width;
            //IsSlide = false;
            //GetList();
            //#endregion
        }
        private async void GetList()
        {
           UsersList = App.database.GetAllUserDetails();
        }
        #endregion

        #region Default Override functions  
        public override void Init(object initData)
        {
            base.Init(initData);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData != null)
            {
                var temp = (string)returnedData;
                CoreMethods.DisplayAlert("Response", temp, "Ok");
            }

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            ClearValues();
        }
        #endregion

        #region Commands  
        public Command RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(FullName) && !string.IsNullOrWhiteSpace(FullName) && !string.IsNullOrWhiteSpace(UsernameText) && !string.IsNullOrEmpty(UsernameText) && !string.IsNullOrWhiteSpace(PasswordText) && !string.IsNullOrEmpty(PasswordText) && !string.IsNullOrEmpty(ConfPasswordText))
                    {
                        if (PasswordText == ConfPasswordText)
                        {
                            if (!App.database.IsUserExist(UsernameText.ToLower()))
                            {
                                Users users = new Users()
                                {
                                    FullName = FullName,
                                    UserName = UsernameText.ToLower(),
                                    Password = PasswordText,
                                    CreateDate = DateTime.Now.ToUniversalTime()
                                };
                                var resp = SaveUserRegisterDetails(users);
                                if (resp)
                                    await CoreMethods.PushPageModel<LoginPageModel>(users);
                                else
                                    await CoreMethods.DisplayAlert("Error", ErrorStrings.AddingRegisterDetailsFail, "Ok");
                            }
                            else{
                                await CoreMethods.DisplayAlert("Error", ErrorStrings.UserExist, "Ok");
                            }
                        }
                        else{
                            await CoreMethods.DisplayAlert("Error", ErrorStrings.UserPasswordMatch, "Ok");
                        }
                    }
                    else{
                        await CoreMethods.DisplayAlert("Error", ErrorStrings.UserSignupCredtionalsMand, "Ok");
                    }

                });
            }
        }
        public Command CancelCommand
        {
            get
            {
                return new Command(async() =>
                {
                    await CoreMethods.PushPageModel<LoginPageModel>();
                });
            }
        }


        #endregion

        #region Private Methods
        private bool SaveUserRegisterDetails(Users items)
        {
            var resp = App.database.AddOrUpdateUserDetails(items);
            return resp;
        }
        private void ClearValues()
        {
            FullName = String.Empty;
            UsernameText = String.Empty;
            PasswordText = String.Empty;
            ConfPasswordText = String.Empty;
        }
        #endregion
    }
}
