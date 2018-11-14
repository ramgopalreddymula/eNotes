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
            MenuItem ii = new MenuItem();
            ii.Text = "Logout";
            ii.Text = "Login";
            
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
            
        }//end onBackPressed()
    }
}
