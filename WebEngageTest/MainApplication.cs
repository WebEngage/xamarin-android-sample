using System;
using Android.App;
using Android.Runtime;
using Android.Util;
using Firebase.Iid;

using Com.Webengage.Sdk.Android;

namespace WebEngageTest
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication()
        {
        }

        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            WebEngageConfig config = new WebEngageConfig.Builder()
                                                        .SetWebEngageKey("YOUR-LICENSE-CODE")
                                                        .SetDebugMode(true)
                                                        .Build();
            RegisterActivityLifecycleCallbacks(new WebEngageActivityLifeCycleCallbacks(this, config));

            string token = FirebaseInstanceId.Instance.Token;
            WebEngage.Get().SetRegistrationID(token);
        }
    }
}
