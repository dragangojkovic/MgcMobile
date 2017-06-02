using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Iid;
using Plugin.Settings;
using Android.Gms.Gcm;

namespace MBoxMobile.Droid.PushNotifications
{
    [Service(Exported = false)]
    public class RegistrationIntentService : IntentService
    {
        static object locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info("RegistrationIntentService", "Calling InstanceID.GetToken");
                lock (locker)
                {
                    var instanceID = InstanceID.GetInstance(this);
                    var token = instanceID.GetToken("265413417694", GoogleCloudMessaging.InstanceIdScope, null);

                    CrossSettings.Current.AddOrUpdateValue("DEVICE_TOKEN", token);
                    SendRegistrationToAppServer(token);

                    Log.Info("RegistrationIntentService", "GCM Registration Token: " + token);
                }
            }
            catch (Exception e)
            {
                Log.Debug("RegistrationIntentService", "Failed to get a registration token");
                return;
            }
        }

        void SendRegistrationToAppServer(string token)
        {
            // Add custom implementation here as needed.
        }
    }
}