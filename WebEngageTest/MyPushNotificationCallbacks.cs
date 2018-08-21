using Android.Content;
using Android.Util;
using Com.Webengage.Sdk.Android.Actions.Render;
using Com.Webengage.Sdk.Android.Callbacks;

namespace WebEngageTest
{
    public class MyPushNotificationCallbacks : Java.Lang.Object, IPushNotificationCallbacks
    {
        public MyPushNotificationCallbacks()
        {
        }

        public bool OnPushNotificationActionClicked(Context p0, PushNotificationData p1, string p2)
        {
            Log.Debug("WebEngageTest", "Push notification action clicked");
            return false;
        }

        public bool OnPushNotificationClicked(Context p0, PushNotificationData p1)
        {
            Log.Debug("WebEngageTest", "Push notification clicked");
            return false;
        }

        public void OnPushNotificationDismissed(Context p0, PushNotificationData p1)
        {
            Log.Debug("WebEngageTest", "Push notification dismissed");
        }

        public PushNotificationData OnPushNotificationReceived(Context p0, PushNotificationData p1)
        {
            Log.Debug("WebEngageTest", "Push notification received");
            return p1;
        }

        public void OnPushNotificationShown(Context p0, PushNotificationData p1)
        {
            Log.Debug("WebEngageTest", "Push notification shown");
        }
    }
}
