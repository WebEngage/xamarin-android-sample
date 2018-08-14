using System;
using Firebase.Messaging;
using Android.Graphics;
using Android.App;
using Android.Util;

using Com.Webengage.Sdk.Android;
using System.Collections.Generic;
using Android.Content;

namespace WebEngageTest
{
    [Service]  
    [IntentFilter(new[] {
        "com.google.firebase.MESSAGING_EVENT"
    })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            IDictionary<string, string> data = message.Data;
            if (data.ContainsKey("source") && "webengage".Equals(data["source"])) {
                WebEngage.Get().Receive(data);
            }
        }
    }
}
