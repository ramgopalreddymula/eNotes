using System;
using Android.Widget;
using eNote;
[assembly: Xamarin.Forms.Dependency(typeof(ToastAndroid))]
namespace eNote
{   
    public class ToastAndroid : IToast  
    {  
       
        public void Show(string message)
        {   
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
        public void ToggleScreenLock()
        {

        }
    }  
}
