using System;
using System.Collections.ObjectModel;
using eNote.Models;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsPageModel : FreshBasePageModel
    {
        public ObservableCollection<NaviColors> NaviColorsList { get; set; }
        public ObservableCollection<BackgroundColors> BackgroundColorsList { get; set; }
        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }
        public SettingsPageModel()
        {
             NaviColorsList = new ObservableCollection<NaviColors>();

            NaviColorsList.Add(new NaviColors() { ColorName = "DeepSea" });
            NaviColorsList.Add(new NaviColors() { ColorName = "SkyBlue" });
            NaviColorsList.Add(new NaviColors() { ColorName = "Maroon" });
            NaviColorsList.Add(new NaviColors() { ColorName = "Blue" });
            NaviColorsList.Add(new NaviColors() { ColorName = "ShadesBlue" });
            NaviColorsList.Add(new NaviColors() { ColorName = "Gray" });
            NaviColorsList.Add(new NaviColors() { ColorName = "Peach" });
            NaviColorsList.Add(new NaviColors() { ColorName = "Cyan" });
            NaviColorsList.Add(new NaviColors() { ColorName = "aquamarine2" });
            NaviColorsList.Add(new NaviColors() { ColorName = "eNotes" });

            BackgroundColorsList = new ObservableCollection<BackgroundColors>();
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "DeepSea" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "SkyBlue" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "Maroon" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "Blue" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "ShadesBlue" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "Gray" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "Peach" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "Cyan" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "aquamarine2" });
            BackgroundColorsList.Add(new BackgroundColors() { ColorName = "White" });
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;

        }


        private NaviColors _selectedNavColor;
        public NaviColors SelectedNavColor
        {
            get => _selectedNavColor;
            set
            {
                _selectedNavColor = value;
                if (value != null)
                {
                    NvColor1 = _selectedNavColor.ColorName;
                }

            }
        }

        private BackgroundColors _selectedBgColor;
        public BackgroundColors SelectedBgColor
        {
            get => _selectedBgColor;
            set
            {
                _selectedBgColor = value;
                if (value != null)
                {
                    BgColor1 = _selectedBgColor.ColorName;
                }
               
            }
        }
        public Command ApplayCommand
        {
            get
            {
                return new Command(async () =>
                {
                    string navColor = "NavBarColor" + StringValues.UserName;
                    string bgColor = "BgColor" + StringValues.UserName;
                    Global.eNotesNavBarColor = NvColor1;
                    Global.eNotesBackgroundColor = BgColor1;
                    Device.BeginInvokeOnMainThread(async () => {

                        if (!string.IsNullOrEmpty(NvColor1) && !string.IsNullOrEmpty(BgColor1))
                        {
                            if (Application.Current.Properties.ContainsKey(navColor) && Application.Current.Properties.ContainsKey(bgColor))
                            {
                                try
                                {
                                    Application.Current.Properties.Remove(navColor);
                                    Application.Current.Properties.Remove(bgColor);
                                    await App.Current.SavePropertiesAsync();

                                    Application.Current.Properties.Add(navColor, Global.eNotesNavBarColor.ToString());
                                    await Application.Current.SavePropertiesAsync();
                                    Application.Current.Properties.Add(bgColor, Global.eNotesBackgroundColor.ToString());
                                    await Application.Current.SavePropertiesAsync();
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                                try
                                {
                                    Application.Current.Properties.Add(navColor, Global.eNotesNavBarColor.ToString());
                                    await Application.Current.SavePropertiesAsync();
                                    Application.Current.Properties.Add(bgColor, Global.eNotesBackgroundColor.ToString());
                                    await Application.Current.SavePropertiesAsync();
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            if (Global.dicColor.ContainsKey(Global.eNotesBackgroundColor))
                            {
                                string navSelectedColor = Global.dicColor[Global.eNotesNavBarColor];
                                string bgSelectedColor = Global.dicColor[Global.eNotesBackgroundColor];
                                Application.Current.MainPage.BackgroundColor = Color.FromHex(navSelectedColor);
                            }
                            DependencyService.Get<IToast>().Show("Theme applied Sucessfully!");
                            // ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex(navSelectedColor);// Color.FromRgb(0x07, 0x39, 0xB6);
                            // ((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
                            //((Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage).BackgroundColor = Color.FromHex(bgSelectedColor);// Color.FromRgb(0x07, 0x39, 0xB6);
                        }
                    });
                });
            }
        }
                }
}
