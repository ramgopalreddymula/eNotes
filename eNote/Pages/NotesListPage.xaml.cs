using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage1 = Xamarin.Forms.NavigationPage;

namespace eNote
{
    public partial class NotesListPage : ContentPage
    {
        
        public NotesListPage()
        {
            InitializeComponent();
            
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            string compareDateForFriendship = @"2019-08-03";
            string compareDateForFriendship1 = @"2019-08-03";
            string compareDateForRaksha = @"2019-08-15";
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {

            
            if (currentDate == compareDateForFriendship || currentDate == compareDateForFriendship1)
            {
                var resp = App.database.GetSelectedUser(StringValues.UserName);
                string FullName = string.Empty;
                if (resp != null)
                {
                    FullName = resp.FullName.ToUpper();
                }
                else
                {
                    FullName = "No Name";
                }
                int[] codes = new int[3] { 0x1F6B4, 0x1F600, 0x1F601 };
                Emoji bikingEmoji = new Emoji(codes);
                lblScroll.Text = "Happy friendship day my dear friend " + bikingEmoji;
                MarqueeAnimation();
            }
            else if(currentDate == compareDateForRaksha)
            {
                var resp = App.database.GetSelectedUser(StringValues.UserName);
                string FullName = string.Empty;
                if (resp != null)
                {
                    FullName = resp.FullName.ToUpper();
                }

                lblScroll.Text = FullName+ " Happy Independence Day & Happy Raksha Bandhan ";
                MarqueeAnimation();
            }
            else
            {
                lblScroll.IsVisible = false;
            }
            }
            catch (Exception ex)
            {
                lblScroll.IsVisible = false;
            }
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

        }
        private async void MarqueeAnimation()
        {
            try
            {

            
            do
            {
                await lblScroll.TranslateTo(100, 0, 2000);
                lblScroll.TextColor = Color.Blue;
                await Task.Delay(1000);
                await lblScroll.TranslateTo(-100, 0, 2000);
                lblScroll.TextColor = Color.Green;
                await lblScroll.TranslateTo(0, 500, 2500);
                lblScroll.TextColor = Color.SeaGreen;
                await Task.Delay(500);
                await lblScroll.TranslateTo(0, -500, 2500);
                lblScroll.TextColor = Color.Chocolate;
                await lblScroll.TranslateTo(100, 0, 2000);
                lblScroll.TextColor = Color.Cyan;
                await lblScroll.TranslateTo(0, 80, 2000);
                lblScroll.TextColor = Color.Black;
                await lblScroll.TranslateTo(0, 0, 2000);
            } while (true);
            }
            catch (Exception ex)
            {

            }
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

                return false;
            
        }//end onBackPressed()
        private bool isOpen = false;
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                isOpen = true;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(100);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Show FloatMenuItem1
                FloatMenuItem1.IsVisible = true;
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);

                //Show FloatMenuItem2
                FloatMenuItem2.IsVisible = true;
                await FloatMenuItem2.TranslateTo(0, 0, 100);
                await FloatMenuItem2.TranslateTo(0, -20, 100);
                await FloatMenuItem2.TranslateTo(0, 0, 200);

                //Show FloatMenuItem3
                FloatMenuItem3.IsVisible = true;
                await FloatMenuItem3.TranslateTo(0, 0, 100);
                await FloatMenuItem3.TranslateTo(0, -20, 100);
                await FloatMenuItem3.TranslateTo(0, 0, 200);
            }
            else
            {
                isOpen = false;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(100);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Hide FloatMenuItem1
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);
                FloatMenuItem1.IsVisible = false;

                //Hide FloatMenuItem2
                await FloatMenuItem2.TranslateTo(0, 0, 100);
                await FloatMenuItem2.TranslateTo(0, -20, 100);
                await FloatMenuItem2.TranslateTo(0, 0, 200);
                FloatMenuItem2.IsVisible = false;

                //Hide FloatMenuItem3
                await FloatMenuItem3.TranslateTo(0, 0, 100);
                await FloatMenuItem3.TranslateTo(0, -20, 100);
                await FloatMenuItem3.TranslateTo(0, 0, 200);
                FloatMenuItem3.IsVisible = false;
            }

        }

        private void FloatMenuItem1Tap_OnTapped(object sender, EventArgs e)
        {
            LabelStatus.Text = " 1";
        }

        private void FloatMenuItem2Tap_OnTapped(object sender, EventArgs e)
        {
            LabelStatus.Text = "Menu 3";
        }

        private void FloatMenuItem3Tap_OnTapped(object sender, EventArgs e)
        {
            LabelStatus.Text = "Menu 3";
        }

    }
}
