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
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(0xB9,0xE7,0xDB);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
    }
}
