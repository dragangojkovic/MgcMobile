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
    [Activity(Label = "MBoxMobile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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
                    NotificationPayload payloadObj = JsonConvert.DeserializeObject<NotificationPayload>(apsInfo.GetString("data"));

                    var selectedNotification = new NotificationModel();
                    selectedNotification.ID = int.Parse(payloadObj.Inputstable_AlterID);
                    selectedNotification.MachineNumber = payloadObj.machine_num;
                    selectedNotification.MachineName = payloadObj.MachineName;
                    selectedNotification.DateTime = payloadObj.record_date;
                    selectedNotification.EquipmentTypeName = payloadObj.EquipTypeName;
                    selectedNotification.EquipmentGroupName = payloadObj.EquipGroupName;
                    selectedNotification.Kwh = payloadObj.Kwh;
                    selectedNotification.Operator = payloadObj.Operator;
                    selectedNotification.Product = payloadObj.Product;
                    selectedNotification.Notification = payloadObj.Notification;
                    selectedNotification.Location = payloadObj.Location;
                    selectedNotification.Department = payloadObj.Department;
                    selectedNotification.SubDepartment = payloadObj.SubDepartment;
                    selectedNotification.AlterType = int.Parse(payloadObj.AlterType);
                    selectedNotification.AlterReply = int.Parse(payloadObj.AlterReply);
                    selectedNotification.NotType = int.Parse(payloadObj.NotType);
                    selectedNotification.AlterEquipmentType = int.Parse(payloadObj.AlterEquipType);
                    //var selectedNotification = new NotificationModel();
                    //selectedNotification.ID = 1877108;// int.Parse(apsInfo.GetString("Inputstable_AlterID"));
                    //selectedNotification.MachineNumber = "5";// apsInfo.GetString("machine_num");
                    //selectedNotification.MachineName = "5"; //apsInfo.GetString("MachineName");
                    //selectedNotification.DateTime = @"5\/25\/2017 2:24:44 PM";// apsInfo.GetString("record_date");
                    //selectedNotification.EquipmentTypeName = "Injection molding machine";// apsInfo.GetString("EquipTypeName");
                    //selectedNotification.EquipmentGroupName = "120T";// apsInfo.GetString("EquipGroupName");
                    //selectedNotification.Kwh = "";// apsInfo.GetString("Kwh");
                    //selectedNotification.Operator = "";// apsInfo.GetString("Operator");
                    //selectedNotification.Product = "";// apsInfo.GetString("Product");
                    //selectedNotification.Notification = "Off-Production Card Unknown";// apsInfo.GetString("Notification");
                    //selectedNotification.Location = "MGC GZ";// apsInfo.GetString("Location");
                    //selectedNotification.Department = @"注塑 \/ Moulding"; //apsInfo.GetString("Department");
                    //selectedNotification.SubDepartment = @"3楼 \/ Third floor";// apsInfo.GetString("SubDepartment");
                    //selectedNotification.AlterType = 6549;// int.Parse(apsInfo.GetString("AlterType"));
                    //selectedNotification.AlterReply = 6552;// int.Parse(apsInfo.GetString("AlterReply"));
                    //selectedNotification.NotType = 6727;// int.Parse(apsInfo.GetString("NotType"));
                    //selectedNotification.AlterEquipmentType = 6571;// int.Parse(apsInfo.GetString("AlterEquipType"));

                    if (App.NotificationsForHandling == null)
                        App.NotificationsForHandling = new System.Collections.Generic.List<NotificationModel>();
                    App.NotificationsForHandling.Add(selectedNotification);
                }
                catch
                {
                    Android.Widget.Toast.MakeText(this, "Error in parsing payload!", Android.Widget.ToastLength.Long).Show();
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
                    Log.Info("Main Activity", GoogleApiAvailability.Instance.GetErrorString(resultCode));
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