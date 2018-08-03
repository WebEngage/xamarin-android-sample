using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;

using Com.Webengage.Sdk.Android;
using Com.Webengage.Sdk.Android.Utils;
using Android.Content;
using System.Collections.Generic;
using Java.Lang;

namespace WebEngageTest
{
    [Activity(Label = "Xamarin", MainLauncher = true, Icon = "@mipmap/ic_launcher", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        string userId = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("TEST_PREF", FileCreationMode.Private);


            // User
            EditText userIdEditText = FindViewById<EditText>(Resource.Id.userIdEditText);
            userId = prefs.GetString("userid", "");
            userIdEditText.Text = userId;

            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);

            if (userId.Equals("")) {
                loginButton.Text = "LOGIN";
            } else {
                loginButton.Text = "LOGOUT";
            }

            loginButton.Click += delegate {
                if (loginButton.Text.Equals("LOGIN")) {
                    // login
                    userId = userIdEditText.Text.ToString();

                    if (!userId.Equals(""))
                    {
                        ISharedPreferencesEditor editor = prefs.Edit();
                        editor.PutString("userid", userId);
                        editor.Apply();

                        loginButton.Text = "LOGOUT";

                        WebEngage.Get().User().Login(userId);
                    }
                } else {
                    // logout
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("userid", "");
                    editor.Apply();

                    loginButton.Text = "LOGIN";

                    userIdEditText.Text = "";

                    WebEngage.Get().User().Logout();
                }
            };


            // System user attributes
            EditText emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            Button emailButton = FindViewById<Button>(Resource.Id.emailButton);
            emailButton.Click += delegate
            {
                string email = emailEditText.Text.ToString();
                WebEngage.Get().User().SetEmail(email);

                Toast.MakeText(this.ApplicationContext, "Email set successfully", ToastLength.Long).Show();
            };

            Spinner genderSpinner = FindViewById<Spinner>(Resource.Id.genderSpinner);
            var adapter = ArrayAdapter.CreateFromResource(
            this, Resource.Array.gender_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            genderSpinner.Adapter = adapter;

            Button genderButton = FindViewById<Button>(Resource.Id.genderButton);
            genderButton.Click += delegate
            {
                int spinnerPosition = genderSpinner.SelectedItemPosition;
                Gender gender = Gender.Male;
                if (spinnerPosition == 0) {
                    gender = Gender.Male;
                } else if (spinnerPosition == 1) {
                    gender = Gender.Female;
                } else if (spinnerPosition == 2) {
                    gender = Gender.Other;
                }
                WebEngage.Get().User().SetGender(gender);

                Toast.MakeText(this.BaseContext, "Gender set successfully", ToastLength.Long).Show();
            };

            //WebEngage.Get().User().SetBirthDate("01-01-2001");
            //WebEngage.Get().User().SetFirstName("John");
            //WebEngage.Get().User().SetLastName("Doe");


            // Custom user attributes
            //Number age = (Java.Lang.Integer)23;
            //WebEngage.Get().User().SetAttribute("age", age);

            //IDictionary<string, Object> customAttributes = new Dictionary<string, Object>();
            //customAttributes.Add("Twitter Email", "john.twitter@mail.com");
            //customAttributes.Add("Subscribed", true);
            //WebEngage.Get().User().SetAttributes(customAttributes);


            // Event
            EditText eventEditText = FindViewById<EditText>(Resource.Id.eventEditText);

            Button trackButton = FindViewById<Button>(Resource.Id.trackButton);
            trackButton.Click += delegate
            {
                string eventName = eventEditText.Text.ToString();

                IDictionary<string, Object> attributes = new Dictionary<string, Object>();
                attributes.Add("id", "~123");
                attributes.Add("price", 100);
                attributes.Add("discount", true);
                WebEngage.Get().Analytics().Track(eventName, attributes);

                Toast.MakeText(this.BaseContext, "Event tracked successfully", ToastLength.Long).Show();
            };
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Screen
            IDictionary<string, Object> attributes = new Dictionary<string, Object>();
            attributes.Add("name", "Home");
            attributes.Add("launcher", true);
            WebEngage.Get().Analytics().ScreenNavigated("Home", attributes);
        }
    }
}
