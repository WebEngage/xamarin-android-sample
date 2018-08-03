using System;
using Android.Content;
using Firebase.Iid;
using Android.Util;
using Android.App;

using Com.Webengage.Sdk.Android;

namespace WebEngageTest
{
    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.INSTANCE_ID_EVENT"
    })]
    public class MyFirebaseInstanceIdService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }

        void SendRegistrationToServer(string token)
        {
            WebEngage.Get().SetRegistrationID(token);
        }
    }
}
