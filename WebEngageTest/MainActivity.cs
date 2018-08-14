﻿using Android.App;
using Android.Widget;
using Android.OS;

using Com.Webengage.Sdk.Android;
using Com.Webengage.Sdk.Android.Utils;
using Android.Content;
using System.Collections.Generic;
using Java.Lang;
using Java.Util;

using Com.Webengage.Sdk.Android.Actions.Database;
using Android.Runtime;

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


            // Tracking Users
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
                    userId = userIdEditText.Text.ToString();

                    if (!userId.Equals(""))
                    {
                        ISharedPreferencesEditor editor = prefs.Edit();
                        editor.PutString("userid", userId);
                        editor.Apply();

                        loginButton.Text = "LOGOUT";

                        // Login
                        WebEngage.Get().User().Login(userId);
                    }
                } else {
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("userid", "");
                    editor.Apply();

                    loginButton.Text = "LOGIN";

                    userIdEditText.Text = "";

                    // Logout
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

            //WebEngage.Get().User().SetBirthDate("1994-04-29");
            //WebEngage.Get().User().SetFirstName("John");
            //WebEngage.Get().User().SetLastName("Doe");
            //WebEngage.Get().User().SetCompany("WebEngage");
            //WebEngage.Get().User().SetPhoneNumber("+551155256325");
            //WebEngage.Get().User().SetHashedPhoneNumber("e0ec043b3f9e198ec09041687e4d4e8d");
            //WebEngage.Get().User().SetHashedEmail("144e0424883546e07dcd727057fd3b62");


            // Custom user attributes
            //WebEngage.Get().User().SetAttribute("age", (Java.Lang.Integer)23);
            //WebEngage.Get().User().SetAttribute("premium", (Boolean)true);
            //WebEngage.Get().User().SetAttribute("last_seen", new Date("2018-12-25"));

            //IDictionary<string, Object> customAttributes = new Dictionary<string, Object>();
            //customAttributes.Add("Twitter Email", "john.twitter@mail.com");
            //customAttributes.Add("Subscribed", true);
            //WebEngage.Get().User().SetAttributes(customAttributes);

            //WebEngage.Get().User().DeleteAttribute("age");
            //WebEngage.Get().SetLocationTrackingStrategy(LocationTrackingStrategy.AccuracyCity);
            //WebEngage.Get().User().SetLocation(12.23, 12.45);


            // Tracking Events
            EditText eventEditText = FindViewById<EditText>(Resource.Id.eventEditText);

            Button trackButton = FindViewById<Button>(Resource.Id.trackButton);
            trackButton.Click += delegate
            {
                string eventName = eventEditText.Text.ToString();

                IDictionary<string, Object> attributes = new Dictionary<string, Object>();
                attributes.Add("id", "~123");
                attributes.Add("price", 100);
                attributes.Add("discount", true);
                WebEngage.Get().Analytics().Track(eventName, attributes, new Analytics.Options().SetHighReportingPriority(false));

                Toast.MakeText(this.BaseContext, "Event tracked successfully", ToastLength.Long).Show();
            };

            Button shopButton = FindViewById<Button>(Resource.Id.shopButton);
            shopButton.Click += delegate
            {
                // Complex Events Tracking
                IDictionary<string, Object> product1 = new JavaDictionary<string, Object>();
                product1.Add("SKU Code", "UHUH799");
                product1.Add("Product Name", "Armani Jeans");
                product1.Add("Price", 300.49);

                JavaDictionary<string, Object> detailsProduct1 = new JavaDictionary<string, Object>();
                detailsProduct1.Add("Size", "L");
                product1.Add("Details", detailsProduct1);

                IDictionary<string, Object> product2 = new JavaDictionary<string, Object>();
                product2.Add("SKU Code", "FBHG746");
                product2.Add("Product Name", "Hugo Boss Jacket");
                product2.Add("Price", 507.99);

                JavaDictionary<string, Object> detailsProduct2 = new JavaDictionary<string, Object>();
                detailsProduct2.Add("Size", "L");
                product2.Add("Details", detailsProduct2);

                IDictionary<string, Object> deliveryAddress = new JavaDictionary<string, Object>();
                deliveryAddress.Add("City", "San Francisco");
                deliveryAddress.Add("ZIP", "94121");

                JavaDictionary<string, Object> orderPlacedAttributes = new JavaDictionary<string, Object>();
                JavaList<Object> products = new JavaList<Object>();
                products.Add(product1);
                products.Add(product2);

                JavaList<string> coupons = new JavaList<string>();
                coupons.Add("BOGO17");

                orderPlacedAttributes.Add("Products", products);
                orderPlacedAttributes.Add("Delivery Address", deliveryAddress);
                orderPlacedAttributes.Add("Coupons Applied", coupons);

                WebEngage.Get().Analytics().Track("Order Placed", orderPlacedAttributes, new Analytics.Options().SetHighReportingPriority(false));

                Toast.MakeText(this.BaseContext, "Order Placed successfully", ToastLength.Long).Show();
            };
        }

        protected override void OnStart()
        {
            base.OnStart();

            WebEngage.Get().Analytics().Installed(new Intent());

            // Tracking Screens
            IDictionary<string, Object> attributes = new Dictionary<string, Object>();
            attributes.Add("name", "Home");
            attributes.Add("launcher", true);
            WebEngage.Get().Analytics().ScreenNavigated("Home", attributes);
        }
    }
}
