using Android.App;
using Android.Content;
using Android.OS;

namespace WebEngageTest
{
    [IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
              DataScheme = "http",
              DataHost = "www.webengage.com",
              AutoVerify = true)]
    [Activity(Label = "Deeplink", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DeeplinkActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Deeplink);
        }
    }
}
