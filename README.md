# WebEngage Xamarin Android Sample

This is Xamarin Android sample application to demonstrate usage of Xamarin binding library of WebEngage Android SDK.


## Installation

1. Download [WebEngage Xamarin Android Library](https://s3-us-west-2.amazonaws.com/webengage-sdk/xamarin/android/0.1.0.0/WebEngageXamarinAndroid.dll).

2. To consume this downloaded .DLL in your Xamarin.Android app, you must first add a reference to the Bindings Library by right-clicking on the References node of your project and select Add Reference.


## Initialization

Initialize WebEngage SDK with your license code from onCreate callback of your Application class as shown below.

```csharp
using Com.Webengage.Sdk.Android;
...

    [Application]
    public class YourApplication : Application
    {
    	...

    	public override void OnCreate()
        {
            base.OnCreate();

            WebEngageConfig config = new WebEngageConfig.Builder()
                                                        .SetWebEngageKey(YOUR-LICENSE-CODE)
                                                        .SetDebugMode(true)
                                                        .Build();
            RegisterActivityLifecycleCallbacks(new WebEngageActivityLifeCycleCallbacks(this, config));

            ...
        }
    }
```

**Note:** Replace YOUR-LICENSE-CODE with your own license code.


## Tracking Users

1. Login and logout users as shown below.

```csharp
using Com.Webengage.Sdk.Android;
...

	// User login
	WebEngage.Get().User().Login("userId");

	// User logout
	WebEngage.Get().User().Logout();
```

2. Set system user attributes as shown below.

```csharp
using Com.Webengage.Sdk.Android;
using Com.Webengage.Sdk.Android.Utils;
...

	// Set user first name
	WebEngage.Get().User().SetFirstName("John");

	// Set user last name
	WebEngage.Get().User().SetLastName("Doe");	

	// Set user email
	WebEngage.Get().User().SetEmail("john.doe@email.com");

	// Set user gender
	WebEngage.Get().User().SetGender(Gender.Male);

	// Set user birth-date
	WebEngage.Get().User().SetBirthDate("1994-04-29");

	// Set user company
	WebEngage.Get().User().SetCompany("Google");
```

3. Set custom user attributes as shown below.

```csharp
using Com.Webengage.Sdk.Android;
using Java.Lang;
...

    // Set custom user attribute
    WebEngage.Get().User().SetAttribute("age", (Java.Lang.Integer)23);
    WebEngage.Get().User().SetAttribute("premium", (Boolean)true);

    // Set complex custom user attributes
    IDictionary<string, Object> customAttributes = new Dictionary<string, Object>();
    customAttributes.Add("Twitter Email", "john.twitter@mail.com");
    customAttributes.Add("Subscribed", true);
    WebEngage.Get().User().SetAttributes(customAttributes);
```

**Note:** WebEngage SDK only supports the following data-types: string, Java.Lang.Boolean, Java.Util.Date, Java.Lang.Number, IList<Java.Lang.Object> and IDictionary<string, Java.Lang.Object>.

4. Delete custom user attributes as shown below.

```csharp
using Com.Webengage.Sdk.Android;
...

	// Delete age attribute
	WebEngage.Get().User().DeleteAttribute("age");
```


## Tracking Events

Track custom events as shown below.

```csharp
using Com.Webengage.Sdk.Android;
using Java.Lang;
...
	
    // Track simple event
    WebEngage.Get().Analytics().Track("Searched");

    // Track event with attributes
    IDictionary<string, Object> attributes = new Dictionary<string, Object>();
    attributes.Add("id", "~123");
    attributes.Add("price", 100);
    attributes.Add("discount", true);
    WebEngage.Get().Analytics().Track("Added to cart", attributes);

    // Track events with reporting priority
    WebEngage.Get().Analytics().Track("Added to cart", attributes, new Analytics.Options().SetHighReportingPriority(true));
```

**Note:** WebEngage SDK only supports the following data-types: string, Java.Lang.Boolean, Java.Util.Date, Java.Lang.Number, IList<Java.Lang.Object> and IDictionary<string, Java.Lang.Object>.


## Tracking Screens

Track screen data as shown below.

```csharp
using Com.Webengage.Sdk.Android;
using Java.Lang;
...

    public class YourActivity : Activity
    {
    	...
    	protected override void OnStart()
        {
            base.OnStart();

	    // Track screen data
            IDictionary<string, Object> attributes = new Dictionary<string, Object>();
            attributes.Add("name", "Home");
            attributes.Add("launcher", true);
            WebEngage.Get().Analytics().ScreenNavigated("Home", attributes);
            ...
        }
    }
```


## Push Notifications

1. Integrate Xamarin.Firebase.Messaging Nuget package with your Xamarin.Android app

2. Send FCM Registration token to WebEngage from your FirebaseInstanceIdService class as shown below.

```csharp
using Firebase.Iid;
using Com.Webengage.Sdk.Android;
...

    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.INSTANCE_ID_EVENT"
    })]
    public class YourFirebaseInstanceIdService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendRegistrationToServer(refreshedToken);
        }

        void SendRegistrationToServer(string token)
        {
            WebEngage.Get().SetRegistrationID(token);
        }
    }

```

It is recommended to also send this token to WebEngage from your Application class as shown below.

```csharp
using Firebase.Iid;
using Com.Webengage.Sdk.Android;
...
	
    [Application]
    public class YourApplication : Application
    {
        ...

        public override void OnCreate()
        {
            base.OnCreate();
            ...

            string token = FirebaseInstanceId.Instance.Token;
            WebEngage.Get().SetRegistrationID(token);
        }
    }
```

3. Send the notification message data to WebEngage from your FirebaseMessagingService class as shown below.

```csharp
using Com.Webengage.Sdk.Android;
...

    [Service]  
    [IntentFilter(new[] {
        "com.google.firebase.MESSAGING_EVENT"
    })]
    public class YourFirebaseMessagingService : FirebaseMessagingService
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

```

4. Log in to your WebEngage dashboard and navigate to Integrations > Channels. Select Push tab and paste the your FCM/GCM server key under the field labeled “GCM/FCM Server Key” under Android section. Enter your application package name under the field labeled “Package Name” and click Save.


## In-app Notifications

No additional integration steps are required for receiving in-app notifications. You can create in-app notifications by logging into your WebEngage Dashboard and navigate to In-app section.
