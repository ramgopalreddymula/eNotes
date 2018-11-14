using System;
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
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0xE7, 0xE7, 0xE7);
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.Black;


        }
        private void HomeNavigation()
        {
            // To set MainPage for the Application  
            var mainpage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
            var mainNavContainer = new FreshNavigationContainer(mainpage, "NotesListPageNav");
            MainPage = mainNavContainer;
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0xE7, 0xE7, 0xE7);
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.Black;

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
