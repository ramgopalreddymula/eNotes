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
        public static Dictionary<string, string> dicColor = new Dictionary<string, string>();

        public App()
        {
            dicColor.Add("DeepSea", "#117864");
            dicColor.Add("SkyBlue", "#85C1E9");
            dicColor.Add("Maroon", "#6E2C00");
            dicColor.Add("Blue", "#1F618D");
            dicColor.Add("ShadesBlue", "#212F3C");
            dicColor.Add("Gray", "#B3B6B7");
            dicColor.Add("Peach", "#DAF7A6");
            dicColor.Add("Cyan", "#7E5109");
            dicColor.Add("White", "#FFFFFF");
            dicColor.Add("aquamarine2", "#76eec6");
            dicColor.Add("eNotes", "#00C59B");
            InitializeComponent();
            //Global.Properties = Properties;
            database = new EnotesDatabase();
            //Global.Properties = new Dictionary<string, object>();
            if (Application.Current.Properties.ContainsKey("userName"))
            {

                var parameter = (string)Application.Current.Properties["userName"];
                StringValues.UserName = parameter;
                string navColor = "NavBarColor" + StringValues.UserName;
                string bgColor = "BgColor" + StringValues.UserName;

                if (Application.Current.Properties.ContainsKey(navColor) && Application.Current.Properties.ContainsKey(bgColor))
                {
                   
                    Global.eNotesNavBarColor =(string)Application.Current.Properties[navColor];
                    Global.eNotesBackgroundColor = (string)Application.Current.Properties[bgColor];

                }
                else
                {
                    Global.eNotesNavBarColor= "NavBarColor,#117864";
                    Global.eNotesBackgroundColor= "BgColor,#FFFFFF";
                }
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
            Global.eNotesNavBarColor = "NavBarColor,#117864";
            Global.eNotesBackgroundColor = "BgColor,#FFFFFF";
            //((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0xE7, 0xE7, 0xE7);
            BackgrounfTheam();

        }
        private void BackgrounfTheam()
        {

            string eNotesNav = Global.eNotesNavBarColor;
            string eNotesBackground = Global.eNotesBackgroundColor;
            string eNotesNavColor = string.Empty;
            string eNotesBgColor = string.Empty;
            var getColor = eNotesNav.Split(',');
            var getBgColor = eNotesBackground.Split(',');
            if (getColor.Length >= 1)
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
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex(eNotesNavColor);// Color.FromRgb(0x07, 0x39, 0xB6);

            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BackgroundColor = Color.FromHex(eNotesBgColor);// Color.FromRgb(0x07, 0x39, 0xB6);

        }
        private void HomeNavigation()
        {
           
            // To set MainPage for the Application  
            var mainpage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
            var mainNavContainer = new FreshNavigationContainer(mainpage, "NotesListPageNav");
            MainPage = mainNavContainer;
            BackgrounfTheam();


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
