using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace eNote
{
    public partial class NotesListPage : ContentPage
    {
        public NotesListPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            this.SetDynamicResource(Xamarin.Forms.NavigationPage.StyleProperty, "navigationBarStyle");
            App.Current.Resources["barBackgroundColor"] = new Binding("{Binding Status,Converter ={ StaticResource dynamicColor}");
       

        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            if (Global.isLoginThrough)
                return false;
            else
                return false;
            
        }//end onBackPressed()
    }
}
