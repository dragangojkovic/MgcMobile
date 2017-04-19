using Android.App;
using MBoxMobile.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(MBoxMobile.Droid.Helpers.CloseApplication))]
namespace MBoxMobile.Droid.Helpers
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApplicationHandler()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
            //Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}