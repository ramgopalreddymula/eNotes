using System;
using Android.Media;
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
        protected MediaPlayer player;
        public void StartPlayer(String filePath)
        {
            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                player.Reset();
                player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
            }
        }
        public void StopPlayer()
        {
            if (player != null)
            {
                player.Stop();
            }

        }
        public void PusePlayer()
        {
            if (player != null)
            {
                player.Pause();
            }

        }
        public void ResetPlayer()
        {
            if (player != null)
            {
                player.Release();
            }

        }
    }  
}
