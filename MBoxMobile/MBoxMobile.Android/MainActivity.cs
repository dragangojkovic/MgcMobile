using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using Android.Gms.Common;
using Android.Util;
//using Firebase;
using MBoxMobile.Droid.PushNotifications;
using Android.Content;
using MBoxMobile.Models;
using Newtonsoft.Json;

namespace MBoxMobile.Droid
{
    [Activity(Label = "M-Box", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //private FirebaseApp firebaseApp = null;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            if (Intent.Extras != null)
            {
                try
                {
                    Bundle apsInfo = Intent.GetBundleExtra("PushPayload");
                    string wrapper = apsInfo.GetString("aps");
                    Org.Json.JSONObject jsonObj = new Org.Json.JSONObject(wrapper);
                    NotificationPayload payloadObj = JsonConvert.DeserializeObject<NotificationPayload>(jsonObj.GetString("data"));

                    var selectedNotification = new NotificationModel();
                    selectedNotification.ID = int.Parse(payloadObj.Inputstable_AlterID);
                    selectedNotification.MachineNumber = payloadObj.machine_num;
                    selectedNotification.MachineGroupNameID = payloadObj.MachineName != "null" && payloadObj.MachineName != "" ? int.Parse(payloadObj.MachineName) : (int?)null;
                    selectedNotification.RecordDate = payloadObj.record_date;
                    selectedNotification.EquipTypeText = payloadObj.EquipTypeName;
                    selectedNotification.EquipGroup = payloadObj.EquipGroupName;
                    selectedNotification.Kwh = payloadObj.Kwh != "null" && payloadObj.Kwh != "" ? (float)double.Parse(payloadObj.Kwh) : (float?)null;
                    selectedNotification.Operator = payloadObj.Operator;
                    selectedNotification.Product = payloadObj.Product;
                    selectedNotification.AlterDescription = payloadObj.Notification;
                    selectedNotification.SentToCompany = payloadObj.Location;
                    selectedNotification.Department = payloadObj.Department;
                    selectedNotification.DepartmentSubName = payloadObj.SubDepartment;
                    selectedNotification.AlterType = int.Parse(payloadObj.AlterType);
                    selectedNotification.AlterReply = int.Parse(payloadObj.AlterReply);
                    selectedNotification.AlterDescriptionID = int.Parse(payloadObj.NotType);
                    selectedNotification.EquipmentType = int.Parse(payloadObj.AlterEquipType);

                    if (App.NotificationsForHandling == null)
                        App.NotificationsForHandling = new System.Collections.Generic.List<NotificationModel>();
                    App.NotificationsForHandling.Add(selectedNotification);
                }
                catch
                {
                    //Android.Widget.Toast.MakeText(this, "Error in parsing payload!", Android.Widget.ToastLength.Long).Show();
                }
            }

            if (IsPlayServicesAvailable())
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

            //if (Intent.Extras == null && firebaseApp == null)
            //{
            //    var options = new FirebaseOptions.Builder()
            //        .SetApplicationId("1:111985073585:android:de9814163ea88ce8")
            //        .SetApiKey("AIzaSyAJCUo4UQE29U68RJIorheQcGDkaU6Ms-w")
            //        .SetDatabaseUrl("https://monitorbox-d76ae.firebaseio.com")
            //        .SetGcmSenderId("111985073585")
            //        .SetStorageBucket("monitorbox-d76ae.appspot.com")
            //        .Build();

            //    firebaseApp = FirebaseApp.InitializeApp(this, options);
            //}

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
                {
                    Log.Info("Main Activity", GoogleApiAvailability.Instance.GetErrorString(resultCode));
                }
                else
                {
                    Log.Info("Main Activity", "This device is not supported");
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