using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class LoginPageModel : FreshBasePageModel
    {
        public LoginPageModel()
        {
        }
        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }

        #endregion

        #region OverrideMethods
        public override void Init(object initData)
        {
            if (initData != null)
            {
                var Users = (Users)initData;
                UserName = Users.UserName;
            }

        }

        #endregion
       
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
           
        }
        #region Commands
        public Command LoginCommand
        {
            get
            {
                return new Command(async() => {
                    if (!string.IsNullOrEmpty(UserName))
                    {
                        if (App.database.IsUserExist(UserName.ToLower()))
                        {
                            if (App.database.IsValidUser(UserName.ToLower(), Password))
                            {
                                StringValues.UserName = UserName.ToLower();
                               
                                try
                                {
                                    App.Current.Properties.Add("userName", StringValues.UserName);
                                    await App.Current.SavePropertiesAsync();
                                }
                                catch (Exception ex)
                                {

                                }
                                                               //Global.IsLogin = true;
                                //CoreMethods.PopToRoot(false);
                                //Application.Current.MainPage = App.LoadFOTabbedNav();
                                Global.isLoginThrough=true;
                                await CoreMethods.PushPageModel<NotesListPageModel>();
                            }
                            else
                            {
                                DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginCredtionalsFail);
                                //CoreMethods.DisplayAlert("Error", ErrorStrings.UserLoginCredtionalsFail, "Ok");
                            }
                        }
                        else{
                            CoreMethods.DisplayAlert("Error", ErrorStrings.UserSignUpRequest, "Ok");
                        }
                    }
                    else
                        DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginCredtionalsFail);
                    //CoreMethods.DisplayAlert("Error", ErrorStrings.UserLoginCredtionalsFail, "Ok");
                });
            }
        }

        public Command SignupCommand
        {
            get
            {
                return new Command(async() => {
                    //string loginPage = "TestData";
                   // CoreMethods.PopPageModel(loginPage);
                    await CoreMethods.PushPageModel<SignupPageModel>();
                });
            }
        }
        public Command LoginWithGmailCommand
        {
            get
            {
                return new Command(async () => {
                    //string loginPage = "TestData";
                    // CoreMethods.PopPageModel(loginPage);
                    DependencyService.Get<IToast>().Show(ErrorStrings.ComingSoon);
                    //await CoreMethods.DisplayAlert(ErrorStrings.ComingSoon, "", "Ok");
                });
            }
        }
        #endregion

    }
}
