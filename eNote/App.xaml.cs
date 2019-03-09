using System;
using System.Collections.Generic;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace eNote
{
    public partial class App : Application
    {
        public static EnotesDatabase database;
        public App()
        {
            InitializeComponent();
            //Global.Properties = Properties;
            database = new EnotesDatabase();
            if (Application.Current.Properties.ContainsKey("userName"))
            {

                var parameter = (string)Application.Current.Properties["userName"];
                StringValues.UserName = parameter;
                HomeNavigation();
            }
            else
                WelcomeNavigation();
            //MainPage = new LoginPage();

         
        }
        private void WelcomeNavigation()
        {
            // To set MainPage for the Application  
            var loginpage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var mainNavContainer = new FreshNavigationContainer(loginpage, "LoginPageNav");
            MainPage = mainNavContainer;
            //((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0xE7, 0xE7, 0xE7);
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0x07, 0x39, 0xB6);

            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.Black;


        }
        private void HomeNavigation()
        {
            Dictionary<string, string> dicColor = new Dictionary<string, string>();
            dicColor.Add("DeepSea", "#117864");
            dicColor.Add("SkyBlue", "#85C1E9");
            dicColor.Add("Maroon", "#6E2C00");
            dicColor.Add("Blue", "#1F618D");
            dicColor.Add("ShadesBlue", "#212F3C");
            dicColor.Add("Gray", "#B3B6B7");
            dicColor.Add("Peach", "#DAF7A6");
            dicColor.Add("Cyan", "#7E5109");
            dicColor.Add("aquamarine2", "#76eec6");
            dicColor.Add("eNotes", "00C59B");
            // To set MainPage for the Application  
            var mainpage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
            var mainNavContainer = new FreshNavigationContainer(mainpage, "NotesListPageNav");
            MainPage = mainNavContainer;
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex(dicColor["DeepSea"]);// Color.FromRgb(0x07, 0x39, 0xB6);
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.Black;


        }
        public static FreshNavigationContainer LoginThroughHome()
        {
            var mainpage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
            var mainNavContainer = new FreshNavigationContainer(mainpage, "NotesListPageNav");
            return mainNavContainer;
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
