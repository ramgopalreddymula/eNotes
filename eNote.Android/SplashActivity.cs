﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace eNote.Droid
{
  
    [Activity(Label = "eNote", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    [MetaData("android.app.shortcuts", Resource = "@xml/shortcuts")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            if (Intent.Flags.HasFlag(Android.Content.ActivityFlags.BroughtToFront) /*Sometimes your app in foreground and user pressing on channel notification that cause same issue*/)
            {
                Finish();
                return;
            }
            // Create your application here
        }
    }
}
