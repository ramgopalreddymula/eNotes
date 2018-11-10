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

        #region Commands
        public Command LoginCommand
        {
            get
            {
                return new Command(() => {
                    if (App.database.IsValidUser(UserName, Password))
                    {
                        //CoreMethods.PopToRoot(false);
                        //Application.Current.MainPage = App.LoadFOTabbedNav();

                    }
                    else
                        CoreMethods.DisplayAlert("Error", ErrorStrings.UserLoginCredtionalsFail, "Ok");
                });
            }
        }
        public Command CloseCommand
        {
            get
            {
                return new Command(() => {
                    string loginPage = "TestData";
                    CoreMethods.PopPageModel(loginPage);
                });
            }
        }
        #endregion

    }
}
