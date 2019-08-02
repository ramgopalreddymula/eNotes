using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class ScannerPageModel: FreshBasePageModel
    {
        public ScannerPageModel()
        {
        }
        public string ScannerText { get; set; }
        public Command ScannerCommand
        {
            get
            {
                return new Command(async() => {
                    try
                    {

                        var scanner = DependencyService.Get<IQrScanningService>();
                        var result = await scanner.ScanAsync();
                        if (result != null)
                        {
                            ScannerText = result;
                        }
                        else
                        {
                            ScannerText = "No Data Found,Please try again!";
                        }


                    }
                    catch (Exception ex)
                    {
                        ScannerText = "Scanning Failed,Please try again!";
                    }
                });
            }
        }
    
}
}
