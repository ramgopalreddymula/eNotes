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
                                    Application.Current.Properties.Add("userName", StringValues.UserName);
                                    await Application.Current.SavePropertiesAsync();
                                    string navColor = "NavBarColor" + StringValues.UserName;
                                    string bgColor = "BgColor" + StringValues.UserName;

                                    if (Application.Current.Properties.ContainsKey(navColor) && Application.Current.Properties.ContainsKey(bgColor))
                                    {
                                       
                                        Global.eNotesNavBarColor = (string)Application.Current.Properties[navColor];
                                        Global.eNotesBackgroundColor =(string)Application.Current.Properties[bgColor];


                                    }
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(async() => { 
                                        Application.Current.Properties.Add(navColor, Global.eNotesNavBarColor.ToString());
                                        await Application.Current.SavePropertiesAsync();
                                        Application.Current.Properties.Add(bgColor, Global.eNotesBackgroundColor.ToString());
                                        await Application.Current.SavePropertiesAsync();
                                        });
                                    }

                                }
                                catch (Exception ex)
                                {

                                }
                                                               //Global.IsLogin = true;
                                //CoreMethods.PopToRoot(false);
                                //Application.Current.MainPage = App.LoadFOTabbedNav();

                                //await CoreMethods.PushPageModel<NotesListPageModel>();
                                Application.Current.MainPage = App.LoginThroughHome();
                                string eNotesNav = Global.eNotesNavBarColor;
                                string eNotesBackground = Global.eNotesBackgroundColor;
                                string eNotesNavColor = string.Empty;
                                string eNotesBgColor = string.Empty;
                                var getColor = eNotesNav.Split(',');
                                var getBgColor = eNotesBackground.Split(',');
                                if (getColor.Length>=1)
                                {
                                    eNotesNavColor = getColor[1];
                                }
                                else
                                {
                                    eNotesNavColor = "#00C59B";
                                }
                                if (getBgColor.Length >= 1)
                                {
                                    eNotesBgColor = getBgColor[1];
                                }
                                else
                                {
                                    eNotesBgColor = "#FFFFFF";
                                }
                                ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor =Color.FromHex(eNotesNavColor);// Color.FromRgb(0x07, 0x39, 0xB6);
                                ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
                                ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BackgroundColor = Color.FromHex(eNotesBgColor);// Color.FromRgb(0x07, 0x39, 0xB6);

                            }
                            else
                            {
                                DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginCredtionalsFail);
                                //CoreMethods.DisplayAlert("Error", ErrorStrings.UserLoginCredtionalsFail, "Ok");
                            }
                        }
                        else{
                            bool response=await CoreMethods.DisplayAlert("Error", ErrorStrings.UserSignUpRequest, "Yes","No");
                            if(response)
                            {
                                await CoreMethods.PushPageModel<SignupPageModel>();
                            }
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
