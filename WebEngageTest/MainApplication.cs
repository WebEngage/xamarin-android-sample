using System;
using Android.App;
using Android.Runtime;
using Firebase.Iid;

using Com.Webengage.Sdk.Android;
using Com.Webengage.Sdk.Android.Actions.Database;
using Com.Webengage.Sdk.Android.Callbacks;

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

            // Initialize WebEngage
            WebEngageConfig config = new WebEngageConfig.Builder()
                                                        .SetWebEngageKey("YOUR-WEBENGAGE-LICENSE-CODE")
                                                        .SetDebugMode(true)
                                                        .SetEventReportingStrategy(ReportingStrategy.Buffer)
                                                        .SetLocationTrackingStrategy(LocationTrackingStrategy.AccuracyCity)
                                                        .Build();
            RegisterActivityLifecycleCallbacks(new WebEngageActivityLifeCycleCallbacks(this, config));

            // Send the latest push token to WebEngage
            string token = FirebaseInstanceId.Instance.Token;
            WebEngage.Get().SetRegistrationID(token);

            // Register callbacks
            WebEngage.RegisterPushNotificationCallback(new MyPushNotificationCallbacks());
            WebEngage.RegisterInAppNotificationCallback(new MyInAppNotificationCallbacks());
        }
    }
}
