using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace eNote
{
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage() : base()
        {
            InitializeComponent();
            ZXingScannerPage scanPage;
            ScannerPageModel viewModel = new ScannerPageModel();
            // Create our custom overlay


            btnScanDefault.Clicked += async delegate
            {
                try
                {
                    scanPage = new ZXingScannerPage();
                    scanPage.OnScanResult += (result) => {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PopAsync();
                        if (result != null)
                            txtBarcode.Text = result.Text;
                        else
                            txtBarcode.Text = "No Data Found! Please try again";
                    });
                };

                await Navigation.PushAsync(scanPage);
                }
                catch (Exception ex)
                {
                    txtBarcode.Text = "No Data Found! Please try again";
                }

            };
            btnScan.Clicked += async delegate
            {
                try
                {

                
                var customOverlay = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                var torch = new Button
                {
                    Text = "Scan the QR Code"
                };
                customOverlay.Children.Add(torch);
                scanPage = new ZXingScannerPage(customOverlay: customOverlay);
                scanPage.OnScanResult += (result) =>
                {

                    scanPage.IsScanning = false;
                    scanPage.Title = "Scan the QR Code";

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        if (result != null)
                        {
                            txtBarcode.Text = result.Text;
                           // spanText.Text = result.Text;
                        }
                        else
                            txtBarcode.Text = "No Data Found! Please try again";

                    });
                };
                torch.Clicked += delegate
                {
                    scanPage.ToggleTorch();

                };

                await Navigation.PushAsync(scanPage);
                }
                catch (Exception ex)
                {
                    txtBarcode.Text = "No Data Found! Please try again";
                }
            }; 
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    Device.OpenUri(new System.Uri(txtBarcode.Text));
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("", "Invalid URL", "Ok");
            }

        }
    }
}
