using Android.Content;
using Android.Util;
using Com.Webengage.Sdk.Android.Actions.Render;
using Com.Webengage.Sdk.Android.Callbacks;

namespace WebEngageTest
{
    public class MyInAppNotificationCallbacks : Java.Lang.Object, IInAppNotificationCallbacks
    {
        public MyInAppNotificationCallbacks()
        {
        }

        public bool OnInAppNotificationClicked(Context p0, InAppNotificationData p1, string p2)
        {
            Log.Debug("WebEngageTest", "In-app notification clicked");
            return false;
        }

        public void OnInAppNotificationDismissed(Context p0, InAppNotificationData p1)
        {
            Log.Debug("WebEngageTest", "In-app notification dismissed");
        }

        public InAppNotificationData OnInAppNotificationPrepared(Context p0, InAppNotificationData p1)
        {
            Log.Debug("WebEngageTest", "In-app notification prepared");
            return p1;
        }

        public void OnInAppNotificationShown(Context p0, InAppNotificationData p1)
        {
            Log.Debug("WebEngageTest", "In-app notification shown");
        }
    }
}
