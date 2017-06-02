using System.Linq;
using MBoxMobile.Interfaces;
using MBoxMobile.Droid.Helpers;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSecureStorage))]
namespace MBoxMobile.Droid.Helpers
{
    public class AndroidSecureStorage : ISecureStorage
    {
        public AndroidSecureStorage() { }
        private const string AppName = "MBox";

        public string Get()
        {
            var account = AccountStore.Create(Forms.Context).FindAccountsForService(AppName).FirstOrDefault();
            return (account != null) ? string.Format("{0}#{1}#{2}", account.Username, account.Properties["Password"], account.Properties["Server"]) : null;
        }

        public void Save(string server, string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                Account account = new Account
                {
                    Username = username
                };
                account.Properties.Add("Password", password);
                account.Properties.Add("Server", server);
                AccountStore.Create(Forms.Context).Save(account, AppName);
            }
        }

        public void Delete()
        {
            var account = AccountStore.Create(Forms.Context).FindAccountsForService(AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create(Forms.Context).Delete(account, AppName);
            }
        }
    }
}
 