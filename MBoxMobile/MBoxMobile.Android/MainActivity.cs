using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using Android.Gms.Common;
using Android.Util;
using Firebase;

namespace MBoxMobile.Droid
{
    [Activity(Label = "MBoxMobile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private FirebaseApp firebaseApp = null;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug("Main Activity", "Key: {0} Value: {1}", key, value);
                }
            }
            IsPlayServicesAvailable();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

            if (Intent.Extras == null && firebaseApp == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId("1:111985073585:android:de9814163ea88ce8")
                    .SetApiKey("AIzaSyAJCUo4UQE29U68RJIorheQcGDkaU6Ms-w")
                    .SetDatabaseUrl("https://monitorbox-d76ae.firebaseio.com")
                    .SetGcmSenderId("111985073585")
                    .SetStorageBucket("monitorbox-d76ae.appspot.com")
                    .Build();

                firebaseApp = FirebaseApp.InitializeApp(this, options);
            }

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Log.Info("Main Activity", GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Log.Info("Main Activity", "This device is not supported");
                    //Finish();
                }
                return false;
            }
            else
            {
                Log.Info("Main Activity", "Google Play Services is available.");
                return true;
            }
        }
    }
}