
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Webengage.Sdk.Android;

namespace WebEngageTest
{
    [Activity(Label = "NextActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class NextActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Next);
        }

        protected override void OnStart()
        {
            base.OnStart();
            Log.Debug("WebEngageTest", "Next Activity started");

            // Tracking next screen
            Dictionary<string, Java.Lang.Object> attributes = new Dictionary<string, Java.Lang.Object>();
            attributes.Add("name", "Next");
            attributes.Add("launcher", false);
            WebEngage.Get().Analytics().ScreenNavigated("Next", attributes);
        }
    }
}
