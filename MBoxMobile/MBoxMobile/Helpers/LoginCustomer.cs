using MBoxMobile.Models;
using MBoxMobile.Services;
using System.Threading.Tasks;

namespace MBoxMobile.Helpers
{
    public static class LoginCustomer
    {
        public static async Task<int> GetLoginStatus(CustomerDetail customer)
        {
            //int status = 0;
            //if (customer.ServerId == -1) status = 10006;
            //else if (customer.Username == string.Empty) status = 10001;
            //else if (customer.Password == string.Empty) status = 10002;
            //else if (customer.DeviceToken == string.Empty) status = 10004;

            //if (status == 0)
            //{
            //    await MBoxApiCalls.Authenticate(customer);
            //    status = App.LoggedUser.status;
            //}

            //string errorMessage = string.Empty;
            //switch (status)
            //{
            //    case 10000:
            //        break;
            //    case 10001:
            //        errorMessage = App.CurrentTranslation["Login_ErrorUsername"];
            //        break;
            //    case 10002:
            //        errorMessage = App.CurrentTranslation["Login_ErrorPassword"];
            //        break;
            //    case 10003:
            //        errorMessage = App.CurrentTranslation["Login_ErrorPlatform"];
            //        break;
            //    case 10004:
            //        errorMessage = App.CurrentTranslation["Login_ErrorDeviceToken"];
            //        break;
            //    case 10005:
            //        errorMessage = App.CurrentTranslation["Login_ErrorInvalidLogin"];
            //        break;
            //    case 10006:
            //        errorMessage = App.CurrentTranslation["Login_ErrorServer"];
            //        break;
            //    case 10007:
            //        errorMessage = App.CurrentTranslation["Login_ErrorInvalidServer"];
            //        break;
            //}

            //if (status != 0) App.LastErrorMessage = errorMessage;
            //return status;

            //response example from Authenticate method
            UserInfo uInfo = new UserInfo();
            uInfo.status = 10000;
            uInfo.login = new UserLogin()
            {
                BelongToLocationID = "853,885,913,1981",
                BelongToTableID = 539,
                EfficiencyWorkHours = false.ToString(),
                EquipmentDepartmentFilter = "0",
                EquipmentLocationFilter = "0",
                FirstName = "Saša",
                FunctionFilter = "6069",
                IsFreeze1 = "4644",
                LoginID = "salemih@gmail.com",
                MainFilter = true.ToString(),
                MenuLanguage = "1",
                NotificationFilter = true.ToString(),
                RecordId = 9148,
                SelectedNotificationFilter = "0",
                SelectedPersonalFilter = "0",
                ServerIPAddress = "121.33.199.84",
                TInitials = "Mbox_developer",
                Title = "Customer",
                UserGroup = "Customer"
            };
            App.LoggedUser = uInfo;
            return 10000;
        }
    }
}
