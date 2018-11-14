using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class SignupPageModel : FreshBasePageModel
    {
        #region Properties  
        public string FullName { get; set; }
        public string RegisterTitleText { get; set; }
        public string UsernameText { get; set; }
        public string PasswordText { get; set; }
        public string ConfPasswordText { get; set; }
        public string RegisterButtonText { get; set; }
        public string CancelButtonText { get; set; }

        public string ErrorResponce { get; set; }
        #endregion

        #region Constructor
        public SignupPageModel()
        {
            //var resp = App.database.GetAllUserDetails();
            //Console.WriteLine(resp.Count);
            //App.database.DeleteUser("ram9988");
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
                    if (!string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(UsernameText) && !string.IsNullOrEmpty(PasswordText) && !string.IsNullOrEmpty(ConfPasswordText))
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
                                    await CoreMethods.PushPageModel<LoginPageModel>();
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
                return new Command(() =>
                {
                    CoreMethods.PopToRoot(false);
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
