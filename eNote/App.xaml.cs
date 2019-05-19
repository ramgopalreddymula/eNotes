using System;
using System.Collections.Generic;
using FreshMvvm;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace eNote
{
    public partial class App : Application
    {
        public static EnotesDatabase database;
        //public static Dictionary<string, string> dicColor = new Dictionary<string, string>();
        public static double Height { get; set; }
        public static double Width { get; set; }
        public App()
        {
            Global.dicColor = new Dictionary<string, string>();
            Global.dicColor.Add("DeepSea", "#117864");
            Global.dicColor.Add("SkyBlue", "#85C1E9");
            Global.dicColor.Add("Maroon", "#6E2C00");
            Global.dicColor.Add("Blue", "#1F618D");
            Global.dicColor.Add("ShadesBlue", "#212F3C");
            Global.dicColor.Add("Gray", "#B3B6B7");
            Global.dicColor.Add("Peach", "#DAF7A6");
            Global.dicColor.Add("Cyan", "#7E5109");
            Global.dicColor.Add("White", "#FFFFFF");
            Global.dicColor.Add("aquamarine2", "#76eec6");
            Global.dicColor.Add("eNotes", "#00C59B");

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
                     Global.eNotesNavBarColor= "DeepSea";
                     Global.eNotesBackgroundColor= "White";
                 }
                 HomeNavigation();
             }
             else
                 WelcomeNavigation();
            //var loginpage = FreshPageModelResolver.ResolvePageModel<CollectionViewPageModel>();
            //var mainNavContainer = new FreshNavigationContainer(loginpage, "LoginPageNav");
            //MainPage = mainNavContainer;
            //MainPage = new LoginPage();


        }
        private void WelcomeNavigation()
        {
            // To set MainPage for the Application  
            var loginpage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var mainNavContainer = new FreshNavigationContainer(loginpage, "LoginPageNav");
            MainPage = mainNavContainer;
            Global.eNotesNavBarColor = "DeepSea";
            Global.eNotesBackgroundColor = "White";
            //((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0xE7, 0xE7, 0xE7);
            BackgrounfTheam();

        }
        private void BackgrounfTheam()
        {

            string eNotesNav = Global.eNotesNavBarColor;
            string eNotesBackground = Global.eNotesBackgroundColor;

            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex(Global.dicColor[Global.eNotesNavBarColor]);// Color.FromRgb(0x07, 0x39, 0xB6);

            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
            ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BackgroundColor = Color.FromHex(Global.dicColor[Global.eNotesBackgroundColor]);// Color.FromRgb(0x07, 0x39, 0xB6);

        }
        private void HomeNavigation()
        {
           
            // To set MainPage for the Application  
          //  var mainpage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
           // var mainNavContainer = new FreshNavigationContainer(mainpage, "NotesListPageNav");
           
            LoadMultipleNavigation();
           // BackgrounfTheam();


        }
        public static FreshNavigationContainer LoginThroughHome()
        {
            var mainpage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
            var mainNavContainer = new FreshNavigationContainer(mainpage, "NotesListPageNav");
            return mainNavContainer;
        }
        public static FreshNavigationContainer notesHome()
        {
            var mainpage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
            var mainNavContainer = new FreshNavigationContainer(mainpage, "NotesListPageNav");
            return mainNavContainer;
        }
        public static FreshMasterDetailNavigationContainer masterDetailNav;

        public static void LoadMasterDetail()
        {
             masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Measure(130, 130, MeasureFlags.IncludeMargins);
            // masterDetailNav.Icon = "hamburger.png";
            //masterDetailNav.Title = "eNotes";
           
            masterDetailNav.Init("iApps Solutions", "hamburger.png");
            masterDetailNav.AddPage<PurchasePageModel>("PurChase Notes", null);
            masterDetailNav.AddPage<ExpensesPageModel>("Expenses Notes", null);
            masterDetailNav.AddPage<SettingsPageModel>("Settings", null);

             var mainpage = FreshPageModelResolver.ResolvePageModel<MenuPageModel>();
            masterDetailNav.Detail = new FreshNavigationContainer(mainpage, "NotesListPageNav");
            //var mainpageq = FreshPageModelResolver.ResolvePageModel<MasterPageModel>();
           // masterDetailNav.Master =  mainpageq;
            App.Current.MainPage = masterDetailNav;

           
        }
        public static MasterDetailPage masterDetailsMultiple;
        public static void LoadMultipleNavigation()
        {
            masterDetailsMultiple = new MasterDetailPage(); //generic master detail page
            masterDetailsMultiple.MinimumWidthRequest = 330;
            masterDetailsMultiple.ForceLayout();
            masterDetailsMultiple.MasterBehavior = MasterBehavior.Default;
            masterDetailsMultiple.WidthRequest = 330;
            //we setup the first navigation container with ContactList
            var MenuPageView = FreshPageModelResolver.ResolvePageModel<MenuPageModel>();
            MenuPageView.Title = "Contact List";
            //we setup the first navigation container with name MasterPageArea
            var masterPageArea = new FreshNavigationContainer(MenuPageView, "MasterPageArea");
            masterPageArea.Title = "Menu";
           
            masterDetailsMultiple.Master = masterPageArea; //set the first navigation container to the Master

            //we setup the second navigation container with the QuoteList 
            var quoteListPage = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
            quoteListPage.Title = "Quote List";
            //we setup the second navigation container with name DetailPageArea
            var detailPageArea = new FreshNavigationContainer(quoteListPage, "DetailPageArea");
            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
            {
                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
               
                detailPageArea.BarBackgroundColor = Color.FromHex(navSelectedColor);
                masterPageArea.BarBackgroundColor = Color.FromHex(navSelectedColor);
                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                {
                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                    detailPageArea.BackgroundColor = Color.FromHex(bgSelectedColor);
                    masterPageArea.BackgroundColor = Color.FromHex(bgSelectedColor);
                }
            }
           
            masterDetailsMultiple.Detail = detailPageArea; //set the second navigation container to the Detail
                                                           // var masterDetailNav = new FreshMasterDetailNavigationContainer();
                                                           // masterDetailNav.Measure(130, 130, MeasureFlags.IncludeMargins);
                                                           //  masterDetailNav.Master = masterDetailsMultiple.Master;
                                                           // masterDetailNav.Detail = detailPageArea;
            masterDetailsMultiple.IsPresented = false;
            /* masterDetailsMultiple.IsPresentedChanged+= (sender, e) => {
                 if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                 {
                     string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];

                     detailPageArea.BarBackgroundColor = Color.FromHex(navSelectedColor);
                     masterPageArea.BarBackgroundColor = Color.FromHex(navSelectedColor);
                     if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                     {
                         string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                         detailPageArea.BackgroundColor = Color.FromHex(bgSelectedColor);
                         masterPageArea.BackgroundColor = Color.FromHex(bgSelectedColor);
                     }
                 }

             };*/
            App.Current.MainPage = masterDetailsMultiple;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("ios=036ff35a-736e-49da-92fd-56ce7be48b41;android=2d977a37-f7f3-4a2a-8d1c-d3009c990ab2", typeof(Analytics), typeof(Crashes));
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
