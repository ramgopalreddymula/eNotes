using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eNote.Models;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class MenuPageModel : FreshBasePageModel
    
    {
        public ObservableCollection<MasterPageItem> MenuList { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }
        private void EnotesAppShortcutInvokedHandler(string sender, string intentDataLastPathSegment)
        {
            if(intentDataLastPathSegment== "calc")
            {
                App.masterDetailsMultiple.IsPresented = false;
                var MenuPageView = FreshPageModelResolver.ResolvePageModel<MyCalculaterPageModel>();
                var detailPageArea = new FreshNavigationContainer(MenuPageView, "Calculator");
                if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                {
                    string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                    detailPageArea.BarBackgroundColor = Color.FromHex(navSelectedColor);
                    if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                    {
                        string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                        detailPageArea.BackgroundColor = Color.FromHex(bgSelectedColor);
                    }
                }
                App.masterDetailsMultiple.Detail = detailPageArea;
            }
        }




        public MenuPageModel()
        {
            MessagingCenter.Subscribe<string, string>(
        string.Empty,
        "eNotesShortcutInvoked",
        EnotesAppShortcutInvokedHandler);
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;
            var resp=App.database.GetSelectedUser(StringValues.UserName);
            if (resp != null)
            {
                FullName = resp.FullName.ToUpper();
                }
            else
            {
                FullName = "No Name";
            }
            UserName = StringValues.UserName;

            MenuList = new ObservableCollection<MasterPageItem>();

            MenuList.Add(new MasterPageItem { Id = MenuItemType.Home, Title = "Home", Image = "ic_home.png" });
           // MenuList.Add(new MasterPageItem { Id = MenuItemType.CreateNote, Title = "Notes", Image = "ic_ass_circle.png" });
            MenuList.Add(new MasterPageItem { Id = MenuItemType.Accounts, Title = "Accounts", Image = "ic_accounts.png" });
            MenuList.Add(new MasterPageItem {Id = MenuItemType.PurchaseNotes, Title="Purchase Notes" ,Image= "ic_pr.png"});
            //MenuList.Add(new MasterPageItem {Id = MenuItemType.ExpensesNotes, Title="Expenses Notes" ,Image= "ic_notes2.png"});
           // MenuList.Add(new MasterPageItem {Id = MenuItemType.AudioRecording, Title="Audio" ,Image= "ic_mic.png"});
            MenuList.Add(new MasterPageItem {Id = MenuItemType.CreateAccount, Title="New Account" ,Image= "ic_new_account.png"});
            MenuList.Add(new MasterPageItem { Id = MenuItemType.Calculator, Title = "Calculator", Image = "ic_pr.png" });
            MenuList.Add(new MasterPageItem {Id = MenuItemType.Settings, Title="Settings" ,Image= "ic_settings.png"});
            MenuList.Add(new MasterPageItem {Id = MenuItemType.Help, Title="Help" ,Image= "ic_help.png"});
            MenuList.Add(new MasterPageItem {Id = MenuItemType.ComingFeatures, Title="Coming Features" ,Image= "ic_coming.png"});
            MenuList.Add(new MasterPageItem {Id = MenuItemType.Version, Title="Version : 1.5" ,Image= "ic_version.png"});
            MenuList.Add(new MasterPageItem {Id = MenuItemType.Logout, Title="Logout",Image ="ic_logout.png" });
            App.masterDetailsMultiple.IsPresentedChanged -= MasterDetailsMultiple_IsPresentedChanged;

            App.masterDetailsMultiple.IsPresentedChanged+= MasterDetailsMultiple_IsPresentedChanged;

        }

        void MasterDetailsMultiple_IsPresentedChanged(object sender, EventArgs e)
        {
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;
        }


        public override void Init(object initData)
        {

        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;


        }

        MasterPageItem _selectedItem;

        public MasterPageItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                if (value != null)
                    ListItemSelected.Execute(value);
            }
        }
        public Command<MasterPageItem> ListItemSelected
        {
            get
            {
                return new Command<MasterPageItem>(async (menus) => {
                    _selectedItem = null;
                    switch (menus.Id)
                    {
                        case MenuItemType.Home:
                            //await CoreMethods.PushPageModel<NotesListPageModel>(null, true,false);
                            App.masterDetailsMultiple.IsPresented = false;
                            var MenuPageView1 = FreshPageModelResolver.ResolvePageModel<NotesListPageModel>();
                            var detailPageArea1 = new FreshNavigationContainer(MenuPageView1, "Notes");

                            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                detailPageArea1.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                {
                                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                    detailPageArea1.BackgroundColor = Color.FromHex(bgSelectedColor);
                                }
                            }

                            App.masterDetailsMultiple.Detail = detailPageArea1;
                            break;
                        case MenuItemType.CreateNote:
                            App.masterDetailsMultiple.IsPresented = false;
                            var createNotes = FreshPageModelResolver.ResolvePageModel<NotesDetailPageModel>();
                            var createNotesNav = new FreshNavigationContainer(createNotes, "CreateNotes");
                            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                createNotesNav.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                {
                                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                    createNotesNav.BackgroundColor = Color.FromHex(bgSelectedColor);
                                }
                            }
                            App.masterDetailsMultiple.Detail = createNotesNav;
                            break;
                        case MenuItemType.PurchaseNotes: { 
                            App.masterDetailsMultiple.IsPresented = false;
                            var MenuPageView = FreshPageModelResolver.ResolvePageModel<PurchasePageModel>();
                            var detailPageArea = new FreshNavigationContainer(MenuPageView, "Purchase");
                            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                detailPageArea.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                {
                                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                    detailPageArea.BackgroundColor = Color.FromHex(bgSelectedColor);
                                }
                            }
                            App.masterDetailsMultiple.Detail = detailPageArea;
                    }

                            break;
                        case MenuItemType.Calculator:
                            {
                                App.masterDetailsMultiple.IsPresented = false;
                                var MenuPageView = FreshPageModelResolver.ResolvePageModel<MyCalculaterPageModel>();
                                var detailPageArea = new FreshNavigationContainer(MenuPageView, "Calculator");
                                if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                                {
                                    string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                    detailPageArea.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                    if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                    {
                                        string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                        detailPageArea.BackgroundColor = Color.FromHex(bgSelectedColor);
                                    }
                                }
                                App.masterDetailsMultiple.Detail = detailPageArea;

                            }
                            break;
                        case MenuItemType.ExpensesNotes:

                            App.masterDetailsMultiple.IsPresented = false;
                            var expenses = FreshPageModelResolver.ResolvePageModel<ExpensesPageModel>();
                            var expensesNav = new FreshNavigationContainer(expenses, "Expenses");
                            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                expensesNav.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                {
                                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                    expensesNav.BackgroundColor = Color.FromHex(bgSelectedColor);
                                }
                            }
                            App.masterDetailsMultiple.Detail = expensesNav;
                            break;
                        case MenuItemType.AudioRecording:

                            App.masterDetailsMultiple.IsPresented = false;
                            var audioRec = FreshPageModelResolver.ResolvePageModel<AudioRecordPageModel>();
                            var audioRecNav = new FreshNavigationContainer(audioRec, "Audio Recording");
                            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                audioRecNav.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                {
                                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                    audioRecNav.BackgroundColor = Color.FromHex(bgSelectedColor);
                                }
                            }
                            App.masterDetailsMultiple.Detail = audioRecNav;
                            break;
                        case MenuItemType.Settings:

                            App.masterDetailsMultiple.IsPresented = false;
                            var settings = FreshPageModelResolver.ResolvePageModel<SettingsPageModel>();
                            var settingsNav = new FreshNavigationContainer(settings, "Settings");
                            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                settingsNav.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                {
                                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                    settingsNav.BackgroundColor = Color.FromHex(bgSelectedColor);
                                }
                            }
                            App.masterDetailsMultiple.Detail = settingsNav;
                            break;
                        case MenuItemType.Accounts:
                            {
                                try
                                {
                                    var resp = App.database.GetAllUserNames(StringValues.UserName);
                                    if (resp != null && resp.Count > 0)
                                    {
                                        List<string> list = new List<string>();
                                        foreach (var item in resp)
                                        {
                                            list.Add(item.UserName);
                                        }
                                        string[] resut = list.ToArray();
                                        await CoreMethods.DisplayActionSheet("Accounts!", "Ok", "Cancel", resut);

                                    }
                                    else
                                    {
                                      
                                        await CoreMethods.DisplayAlert("No Accounts Found!", "", "Ok");

                                    }
                                }
                                catch (Exception ex)
                                {

                                }

                            }
                            break;
                        case MenuItemType.Help:

                            App.masterDetailsMultiple.IsPresented = false;
                            var Help = FreshPageModelResolver.ResolvePageModel<AboutPageModel>();
                            var HelpNav = new FreshNavigationContainer(Help, "Help");
                            if (Global.dicColor.ContainsKey(Global.eNotesNavBarColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                HelpNav.BarBackgroundColor = Color.FromHex(navSelectedColor);
                                if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                                {
                                    string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                    HelpNav.BackgroundColor = Color.FromHex(bgSelectedColor);
                                }
                            }
                            App.masterDetailsMultiple.Detail = HelpNav;
                            break;
                        case MenuItemType.ComingFeatures:
                            await CoreMethods.DisplayAlert("New Features!", "Audio Recording?", "Ok");

                            break;
                        case MenuItemType.Version:

                            break;
                        case MenuItemType.Logout:
                            {
                            
                            var action = await CoreMethods.DisplayAlert("Confirmation!", "Are you sure you want to logout?", "Yes", "No");
                                switch (action)
                                {
                                    case true:
                                        try
                                        {
                                            Application.Current.Properties.Remove("userName");
                                            App.Current.Properties.Remove("userName");
                                            await App.Current.SavePropertiesAsync();
                                           Global.eNotesNavBarColor = "DeepSea";
                                           Global.eNotesBackgroundColor = "White";
                                            await CoreMethods.PushPageModel<LoginPageModel>();
                                            var loginpage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                                            var mainNavContainer = new FreshNavigationContainer(loginpage, "LoginPageNav");
                                            App.masterDetailsMultiple.IsPresented = false;
                                            Application.Current.MainPage= mainNavContainer;
                                        }
                                        catch (Exception ex)
                                        {
                                            DependencyService.Get<IToast>().Show("Failed, Please try again");
                                        }

                                        break;
                                    case false:

                                        break;
                                    default:
                                        break;
                                }
                                        
                                }
                                break;
                        default:
                            break;
                    }

                });
            }
        }
        public Command Notes
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<NotesListPageModel>();
                });
            }
        }

        public Command Purchase
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<PurchasePageModel>();
                });
            }
        }
        public Command Expenses
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<ExpensesPageModel>();
                });
            }
        }
        public Command Settings
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<SettingsPageModel>();
                });
            }
        }
        public Command AudioRecording
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<AudioRecordPageModel>();
                });
            }
        }
        public Command About
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<AboutPageModel>();
                });
            }
        }
    }
}
