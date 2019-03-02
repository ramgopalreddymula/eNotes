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
            
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            if (Global.isLoginThrough)
                return true;
            else
                return false;
            
        }//end onBackPressed()
    }
}
