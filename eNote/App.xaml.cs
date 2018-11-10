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
            database = new EnotesDatabase();
            WelcomeNavigation();
            //MainPage = new LoginPage();

         
        }
        private void WelcomeNavigation()
        {
            // To set MainPage for the Application  
            var loginpage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var mainNavContainer = new FreshNavigationContainer(loginpage, "LoginPageNav");
            MainPage = mainNavContainer;
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
