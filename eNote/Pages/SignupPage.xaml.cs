using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eNote
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            Title = "Registration";

            NavigationPage.SetHasBackButton(this, true);

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0x07,0x39,0xB6);
            // ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
    }
}
