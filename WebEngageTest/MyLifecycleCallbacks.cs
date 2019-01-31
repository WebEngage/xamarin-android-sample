using System;
using Android.Content;
using Com.Webengage.Sdk.Android.Callbacks;

namespace WebEngageTest
{
    public class MyLifecycleCallbacks : Java.Lang.Object, ILifeCycleCallbacks
    {
        public void OnAppInstalled(Context p0, Intent p1)
        {
            // ...
        }

        public void OnAppUpgraded(Context p0, int p1, int p2)
        {
            // ...
        }

        public void OnGCMMessageReceived(Context p0, Intent p1)
        {
            // ...
        }

        public void OnGCMRegistered(Context p0, string p1)
        {
            // ...
        }
    }
}
