using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Support.V4.Content;
using Android.Util;
using static Android.App.Notification;

namespace MBoxMobile.Droid.PushNotifications
{
	[BroadcastReceiver(Enabled = true, Exported = false)]
	[IntentFilter(new[] { "pushy.me" })]
	public class PushReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
            string message = string.Empty;

            // Attempt to extract the "message" property from the payload
            if (intent.GetStringExtra("message") != null)
            {
                message = intent.GetStringExtra("message");
            }

            string a = intent.GetStringExtra("AlterReply");
            string b = intent.GetStringExtra("Inputstable_AlterID");
            string c = intent.GetStringExtra("SubDepartment");

            ///////////////////////////////////////////////////////////////////////////////////////

            string wrapper = intent.Extras.GetString("aps");
            Org.Json.JSONObject jsonObj = new Org.Json.JSONObject(wrapper);
            if (string.IsNullOrEmpty(message)) message = jsonObj.GetString("alert");

            var myIntent = new Intent(context, typeof(MainActivity));
            myIntent.PutExtra("PushPayload", intent.Extras);

            ///////////////////////////////////////////////////////////////////////////////////////

            //intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(context, 0, myIntent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(context)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle("MBox")
                .SetStyle(new BigTextStyle().BigText(message))
                .SetContentText(message)
                //	    .SetColor(ContextCompat.GetColor(context, Resource.Color.colorPrimary))
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
	}
}
