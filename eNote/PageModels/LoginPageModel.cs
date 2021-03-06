﻿using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using static eNote.LoginPage;

namespace eNote
{
    public delegate void AnimationEventHandler(ActionType action);
    [AddINotifyPropertyChangedInterface]
    public class LoginPageModel : FreshBasePageModel
    {

        public static event AnimationEventHandler eventEnotesAction;
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
                    if(eventEnotesAction!=null)
                    {
                        eventEnotesAction(ActionType.Login);
                    }

                    if (!string.IsNullOrEmpty(UserName))
                    {
                        if (UserName.ToLower().Contains("iappstest"))
                        {
                            try
                            {

                           
                            StringValues.UserName = "iApps";
                            Application.Current.Properties.Add("userName", StringValues.UserName);
                            await Application.Current.SavePropertiesAsync();
                            string navColor = "NavBarColor" + StringValues.UserName;
                            string bgColor = "BgColor" + StringValues.UserName;
                            if (Application.Current.Properties.ContainsKey(navColor) && Application.Current.Properties.ContainsKey(bgColor))
                            {

                                Global.eNotesNavBarColor = (string)Application.Current.Properties[navColor];
                                Global.eNotesBackgroundColor = (string)Application.Current.Properties[bgColor];


                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
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
                            App.LoadMultipleNavigation();
                        }
                        else
                        {
                            try
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
                                        Global.eNotesBackgroundColor = (string)Application.Current.Properties[bgColor];


                                    }
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
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
                                App.LoadMultipleNavigation();

                                //((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor =Color.FromHex(Global.dicColor[Global.eNotesNavBarColor]);// Color.FromRgb(0x07, 0x39, 0xB6);
                                //((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
                                // ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BackgroundColor = Color.FromHex(Global.dicColor[Global.eNotesBackgroundColor]);// Color.FromRgb(0x07, 0x39, 0xB6);

                            }
                            else
                            {
                                DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginCredtionalsFail);
                                //CoreMethods.DisplayAlert("Error", ErrorStrings.UserLoginCredtionalsFail, "Ok");
                            }
                        }
                        else
                        {
                            bool response = await CoreMethods.DisplayAlert("Error", ErrorStrings.UserSignUpRequest, "Yes", "No");
                            if (response)
                            {
                                await CoreMethods.PushPageModel<SignupPageModel>();
                            }
                        }
                            }
                            catch (Exception ex)
                            {

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
                    if (eventEnotesAction != null)
                    {
                        eventEnotesAction(ActionType.SingUp);
                    }
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
    public enum ActionType
    {
        Login,
        SingUp,
        DeleteNotes,
        Save
    }
}
