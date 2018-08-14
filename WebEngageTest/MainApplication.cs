using System;
using Android.App;
using Android.Runtime;
using Firebase.Iid;

using Com.Webengage.Sdk.Android;
using Com.Webengage.Sdk.Android.Actions.Database;

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
                                                        .SetWebEngageKey("~13410522d")
                                                        .SetDebugMode(true)
                                                        .SetEventReportingStrategy(ReportingStrategy.Buffer)
                                                        .SetLocationTrackingStrategy(LocationTrackingStrategy.AccuracyCity)
                                                        .Build();
            RegisterActivityLifecycleCallbacks(new WebEngageActivityLifeCycleCallbacks(this, config));

            string token = FirebaseInstanceId.Instance.Token;
            WebEngage.Get().SetRegistrationID(token);
        }
    }
}
