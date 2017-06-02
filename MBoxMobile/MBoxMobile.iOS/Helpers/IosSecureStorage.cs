using MBoxMobile.Interfaces;
using MBoxMobile.iOS.Helpers;
using System.Linq;
using Xamarin.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(IosSecureStorage))]
namespace MBoxMobile.iOS.Helpers
{
    public class IosSecureStorage : ISecureStorage
    {
        private const string AppName = "MBox";

        public string Get()
        {
            var account = AccountStore.Create().FindAccountsForService(AppName).FirstOrDefault();
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
                AccountStore.Create().Save(account, AppName);
            }
        }

        public void Delete()
        {
            Account account = AccountStore.Create().FindAccountsForService(AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create().Delete(account, AppName);
            }
        }
    }
}
