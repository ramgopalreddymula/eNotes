﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

using System.Threading.Tasks;
using Android.Content;
using System.IO;

namespace eNote.Droid
{
    [Activity(Label = "eNote", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Exported = true,
          Name = "com.iappssolution.enotes.MainActivity")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity mContext;
        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           
            base.OnCreate(savedInstanceState);
            App.Width = Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density;
            App.Height = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AppCenter.Start("2d977a37-f7f3-4a2a-8d1c-d3009c990ab2", typeof(Analytics), typeof(Crashes));

            LoadApplication(new App());
             mContext = this;
            if (!string.IsNullOrEmpty(Intent?.Data?.LastPathSegment))
            {
                MessagingCenter.Send(
           string.Empty,
           "eNotesShortcutInvoked",
           Intent?.Data?.LastPathSegment);
            }

            //App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}