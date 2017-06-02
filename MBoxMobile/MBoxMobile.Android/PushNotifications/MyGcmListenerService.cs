using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Gcm;
using Android.Util;
using Android.Media;
using static Android.App.Notification;

namespace MBoxMobile.Droid.PushNotifications
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
    public class MyGcmListenerService : GcmListenerService
    {
        public override void OnMessageReceived(string from, Bundle data)
        {
            string message = data.GetString("alert");

            Log.Debug("MyGcmListenerService", "From:    " + from);
            Log.Debug("MyGcmListenerService", "Message: " + message);

            var intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("PushMessage", message);
            intent.PutExtra("PushPayload", data);
            //intent.PutExtra("Id", data.GetString("Inputstable_AlterID"));
            //intent.PutExtra("MachineNumber", data.GetString("machine_num"));
            //intent.PutExtra("MachineName", data.GetString("MachineName"));
            //intent.PutExtra("DateTime", data.GetString("record_date"));
            //intent.PutExtra("EquipmentTypeName", data.GetString("EquipTypeName"));
            //intent.PutExtra("EquipmentGroupName", data.GetString("EquipGroupName"));
            //intent.PutExtra("Kwh", data.GetString("Kwh"));
            //intent.PutExtra("Operator", data.GetString("Operator"));
            //intent.PutExtra("Product", data.GetString("Product"));
            //intent.PutExtra("Notification", data.GetString("Notification"));
            //intent.PutExtra("Location", data.GetString("Location"));
            //intent.PutExtra("Department", data.GetString("Department"));
            //intent.PutExtra("SubDepartment", data.GetString("SubDepartment"));
            //intent.PutExtra("AlterType", data.GetString("AlterType)"));
            //intent.PutExtra("AlterReply", data.GetString("AlterReply)"));
            //intent.PutExtra("NotType", data.GetString("NotType)"));
            //intent.PutExtra("AlterEquipmentType", data.GetString("AlterEquipType)"));

            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle("MBox")
                .SetStyle(new BigTextStyle().BigText(message))
                .SetContentText(message)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}