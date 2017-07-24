using MBoxMobile.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(MBoxMobile.Droid.Helpers.AndroidVersion))]
namespace MBoxMobile.Droid.Helpers
{
    public class AndroidVersion : IVersion
    {
        public string Version
        {
            get
            {
                var context = Forms.Context;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            }
        }
    }
}