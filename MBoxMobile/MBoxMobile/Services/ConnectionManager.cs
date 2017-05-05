using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace MBoxMobile.Services
{
    public class ConnectionManager
    {
        public static string siteUri = "http://121.33.199.84:200/";
        //public static string siteUri = "http://erp.mgc.tm/AppServices/";
        public static string site_token = "";//NXWWY25De1M_t8yW7zn2
        public static string crash_report_token = "";

        public IRestClient restClient;

        private static ConnectManager _instance;
        private static object _lockCreate = new object();
        public delegate void LogHandler(string message);
        public delegate void StatusResponse(bool status, string message);
        public delegate void StatusResponseList<T>(bool status, string message, List<T> nameobject);
        public delegate void StatusResponseObject<T>(bool status, string message, T nameobject);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MBoxPCL.ConnectManager"/> class.
        /// </summary>
        public ConnectManager()
        {
            // initialize REST Client
            restClient = new RestClient(siteUri);
            restClient.Timeout = TimeSpan.FromSeconds(15);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        public static ConnectManager GetInstance()
        {
            lock (_lockCreate)
            {
                if (_instance == null)
                {
                    _instance = new ConnectManager();
                }
            }
            return _instance;
        }

        public async void GetServerList(StatusResponseList<DBServer> status)
        {
            string url = "MgcApi.svc/GetServerList?";
            var request = new RestRequest(url, Method.GET);

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<DBServer>> res = JsonConvert.DeserializeObject<ResponseResult<List<DBServer>>>(result);

                    if (res != null && res.d.Count > 0)
                    {
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, Localise.GetString(I18N.AuthInfoIncorrect), null);
                    }
                }
                else
                {
                    status(false, response.StatusCode.ToString(), null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void Authenticate(CustomerDetail detail, StatusResponse status, bool isRemember = false)
        {
            string url = "MgcApi.svc/Login?", paramData = "";
            var request = new RestRequest(url, Method.GET);

            //// Set Data (Key,Value)
            foreach (KeyValuePair<string, string> data in detail)
            {
                if (!String.IsNullOrEmpty(paramData))
                {
                    paramData += "&";
                }
                paramData += data.Key + "={" + data.Key + "}";
            }

            request.Resource = url + paramData;

            foreach (KeyValuePair<string, string> data in detail)
            {
                request.AddUrlSegment(data.Key, data.Value);
            }

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<DBUserInfo> res = JsonConvert.DeserializeObject<ResponseResult<DBUserInfo>>(result);

                    if (isRemember)
                    {
                        var username = detail.Where(s => s.Key == ApiParam.CUSTOMER.username.ToString()).FirstOrDefault();
                        var password = detail.Where(s => s.Key == ApiParam.CUSTOMER.password.ToString()).FirstOrDefault();
                        LocalDataStore.GetInstance().AddUserInfo(res.d.login);
                        LocalDataStore.GetInstance().AddUserForLogin(new DBUserAutoLogin
                        {
                            user = username.Value,
                            password = password.Value
                        });
                    }

                    if (res.d.status == 10000)
                    {
                        LocalDataStore.authInfo = res.d.login;
                        status(true, "Authenticate complete");
                    }
                    else
                    {
                        status(false, Localise.GetString(I18N.AuthInfoIncorrect));
                    }
                }
                else
                {
                    status(false, response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        #region Filter
        public async void GetPersonalFilter(StatusResponseObject<PersonalFilterModel> status)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //	status(false, "No Internet Connection", null);
            //	return;
            //}

            string url = "MgcApi.svc/GetPersonalFilterList?userid={userid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("userid", LocalDataStore.authInfo.RecordId);
                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<PersonalFilterModel> res = JsonConvert.DeserializeObject<ResponseResult<PersonalFilterModel>>(result);
                        //LocalDataStore.GetInstance().AddPersonalFilter(res.d);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
                else
                {
                    status(false, "user does not exist", null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void SetSelectedPersonalFilter(int filterid, StatusResponseObject<int> status)
        {
            string url = "MgcApi.svc/SetSelectedPersonalFilter?userid={userid}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("userid", LocalDataStore.authInfo.RecordId);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                        if (res.d == 10000)
                        {
                            status(true, "success", filterid);
                        }
                        else
                        {
                            status(false, "failed", filterid);
                        }
                    }
                    else
                    {
                        status(false, "Http Error", filterid);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, filterid);
            }
        }

        public async void SetSelectedNotificationFilter(int filterid, StatusResponseObject<int> status)
        {
            string url = "MgcApi.svc/SetSelectedNotificationFilter?userid={userid}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("userid", LocalDataStore.authInfo.RecordId);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                        if (res.d == 10000)
                        {
                            status(true, "success", filterid);
                        }
                        else
                        {
                            status(false, "failed", filterid);
                        }
                    }
                    else
                    {
                        status(false, "Http Error", filterid);
                    }
                }
                else
                {
                    status(false, "user not found", filterid);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, filterid);
            }
        }

        public async void SetPersonalFilterOnOff(bool bOnOff, StatusResponse status)
        {
            string url = "MgcApi.svc/SetPersonalFilterOnOff?userid={userid}&bOn={bOn}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("userid", LocalDataStore.authInfo.RecordId);
                    request.AddUrlSegment("bOn", bOnOff);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                        if (res.d == 10000)
                        {
                            status(true, "success");
                        }
                        else
                        {
                            status(false, "failed");
                        }
                    }
                    else
                    {
                        status(false, "Http Error");
                    }
                }
                else
                {
                    status(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        public async void SetNotificationFilterOnOff(bool bOnOff, StatusResponse status)
        {
            string url = "MgcApi.svc/SetNotificationFilterOnOff?userid={userid}&bOn={bOn}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("userid", LocalDataStore.authInfo.RecordId);
                    request.AddUrlSegment("bOn", bOnOff);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                        if (res.d == 10000)
                        {
                            status(true, "success");
                        }
                        else
                        {
                            status(false, "failed");
                        }
                    }
                    else
                    {
                        status(false, "Http Error");
                    }
                }
                else
                {
                    status(false, "user not found");
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        #endregion

        #region Notification
        public async void GetElectricityWasteCauseList(StatusResponseList<MaterialModel> status)
        {
            string url = "MgcApi.svc/GetElectricityWasteCauseList";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<MaterialModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<MaterialModel>>>(result);
                    status(true, "success", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetNotificationTypeList(int alterEquipmentType, StatusResponseList<MaterialModel> status)
        {
            string url = "MgcApi.svc/GetNotificationTypeList?alterEquipmentType={alterEquipmentType}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                request.AddUrlSegment("alterEquipmentType", alterEquipmentType);

                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<MaterialModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<MaterialModel>>>(result);
                    status(true, "success", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetSolutionCauseList(int notificationID, StatusResponseList<MaterialModel> status)
        {
            string url = "MgcApi.svc/GetSolutionCauseList?notificationID={notificationID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                request.AddUrlSegment("notificationID", notificationID);

                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<MaterialModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<MaterialModel>>>(result);
                    status(true, "success", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void InputAcknowledge(int notificationID, string desc, StatusResponse status)
        {
            string url = "MgcApi.svc/InputAcknowledge?notificationID={notificationID}&userID={userID}&description={description}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("notificationID", notificationID);
                    request.AddUrlSegment("userID", authInfo.RecordId);
                    request.AddUrlSegment("description", desc);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                    if (res.d == 10000)
                    {
                        status(true, "success");
                    }
                    else
                    {
                        status(false, "failed");
                    }
                }
                else
                {
                    status(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        public async void InputKwh(int notificationID, string desc, int causeID, StatusResponse status)
        {
            string url = "MgcApi.svc/InputKwh?notificationID={notificationID}&userID={userID}&description={description}&causeID={causeID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("notificationID", notificationID);
                    request.AddUrlSegment("userID", authInfo.RecordId);
                    request.AddUrlSegment("description", desc);
                    request.AddUrlSegment("causeID", causeID);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                    if (res.d == 10000)
                    {
                        status(true, "success");
                    }
                    else
                    {
                        status(false, "failed");
                    }
                }
                else
                {
                    status(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        public async void InputDescription(int notificationID, string desc, int alterDesc, int equiptype, StatusResponse status)
        {
            string url = "MgcApi.svc/InputDescription?notificationID={notificationID}&userID={userID}&description={description}&alterDescription={alterDescription}&alterEquipType={alterEquipType}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("notificationID", notificationID);
                    request.AddUrlSegment("userID", authInfo.RecordId);
                    request.AddUrlSegment("description", desc);
                    request.AddUrlSegment("alterDescription", alterDesc);
                    request.AddUrlSegment("alterEquipType", equiptype);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                    if (res.d == 10000)
                    {
                        status(true, "success");
                    }
                    else
                    {
                        status(false, "failed");
                    }
                }
                else
                {
                    status(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        public async void InputSolution(int notificationID, string solution, int? solutionCauseID, StatusResponse status)
        {
            string url = "MgcApi.svc/InputSolution?notificationID={notificationID}&userID={userID}&solution={solution}&solutionCauseID={solutionCauseID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("notificationID", notificationID);
                    request.AddUrlSegment("userID", authInfo.RecordId);
                    request.AddUrlSegment("solution", solution);
                    request.AddUrlSegment("solutionCauseID", solutionCauseID);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                    if (res.d == 10000)
                    {
                        status(true, "success");
                    }
                    else
                    {
                        status(false, "failed");
                    }
                }
                else
                {
                    status(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        public async void Approve(int notificationID, int mode, StatusResponse status)
        {
            string url = "MgcApi.svc/Approve?notificationID={notificationID}&userID={userID}&mode={mode}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("notificationID", notificationID);
                    request.AddUrlSegment("userID", authInfo.RecordId);
                    request.AddUrlSegment("mode", mode);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

                    if (res.d == 10000)
                    {
                        status(true, "success");
                    }
                    else
                    {
                        status(false, "failed");
                    }
                }
                else
                {
                    status(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        public async void GetNotificationFilterList(StatusResponseObject<NotificationFilterModel> status)
        {
            string url = "MgcApi.svc/GetNotificationFilterList?userid={userid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("userid", LocalDataStore.authInfo.RecordId);
                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<NotificationFilterModel> res = JsonConvert.DeserializeObject<ResponseResult<NotificationFilterModel>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetElectricityWasteNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetElectricityWasteNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    //restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.authInfo;
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEquipProblemNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetEquipProblemNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    //restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.authInfo;
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetQualityProblemNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetQualityProblemNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.authInfo;
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetQualityControlNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetQualityControlNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.GetInstance().GetUserInfo();
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetPlanningNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetPlanningNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.GetInstance().GetUserInfo();
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetOperationalNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetOperationalNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    //restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.GetInstance().GetUserInfo();
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetApprovedNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetApprovedNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    //restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.GetInstance().GetUserInfo();
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetReportedNotifications(string mainFilterID, string notiFilterID, int periodid, StatusResponseList<NotificationModel> status)
        {
            string url = "MgcApi.svc/GetReportedNotifications?mainFilterID={mainFilterID}&notFilterID={notFilterID}&periodID={periodID}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //using (var restClient = new RestClient(siteUri))
                {
                    //restClient.Timeout = TimeSpan.FromSeconds(10);
                    //var authInfo = LocalDataStore.GetInstance().GetUserInfo();
                    //if (authInfo != null)
                    //{
                    request.AddUrlSegment("mainFilterID", mainFilterID);
                    request.AddUrlSegment("notFilterID", notiFilterID);
                    request.AddUrlSegment("periodID", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<NotificationModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<NotificationModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }
        #endregion

        #region Push NOtification
        public async void DeviceInfo(string oldToken, string newToken, StatusResponseObject<string> status)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.Resource = String.Format("deviceinfo.ashx?DevicePlatform=ios&OldToken={0}&newToken={1}&Type=UpdateToken", String.IsNullOrEmpty(oldToken) ? "" : oldToken.Replace(" ", ""), newToken.Replace(" ", ""));

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<bool> res = JsonConvert.DeserializeObject<ResponseResult<bool>>(result);

                    if (res.d)
                    {
                        status(true, "device token updated", newToken);
                    }
                    else
                    {
                        status(false, "failed to update device token", newToken);
                    }
                }
                else
                {
                    status(false, response.StatusCode.ToString(), newToken);
                }
            }
            catch (Exception ex)
            {
                status(false, "Exception occured", ex.Message);
            }
        }
        #endregion

        public async void SendFeedback(string messageId, string solution, StatusResponse status)
        {
            var request = new RestRequest(Method.GET);

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.Resource = String.Format("MgcApi.svc/ReportFeedback?alterid={0}&solution={1}&userid={2}", messageId, solution, LocalDataStore.authInfo.RecordId);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<bool> res = JsonConvert.DeserializeObject<ResponseResult<bool>>(result);

                        if (res.d == true)
                        {
                            status(true, "success feedback");
                        }
                        else
                        {
                            status(true, "failed to send feedback");
                        }
                    }
                    else
                    {
                        status(false, response.StatusCode.ToString());
                    }
                }
                //request.AddHeader("Content-Type", "application/json");
            }
            catch (Exception ex)
            {
                status(false, ex.Message);
            }
        }

        #region Filter (Reserved)
        public async void GetLocationList(int userId, StatusResponseObject<DBFilter> status)
        {
            string url = "MgcApi.svc/GetLocationFilters?recordid={recordId}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddUrlSegment("recordId", userId);

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<DBFilter> res = JsonConvert.DeserializeObject<ResponseResult<DBFilter>>(result);
                    status(true, "Get Location complete", (DBFilter)res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetDepartmentList(int userId, string locationlist, StatusResponseList<DBDepartment> status)
        {
            string url = "MgcApi.svc/GetDepartmentFilters?recordid={recordId}&locationlist={locationList}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddUrlSegment("recordId", userId);
            request.AddUrlSegment("locationList", locationlist);

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<DBDepartment>> res = JsonConvert.DeserializeObject<ResponseResult<List<DBDepartment>>>(result);
                    status(true, "Get Depart complete", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, "Exception occured", null);
            }
        }

        public async void GetSubDepartmentList(int userId, string departlist, StatusResponseList<DBSubDepartment> status)
        {
            string url = "MgcApi.svc/GetSubDepartmentFilters?recordid={recordid}&departmentList={departlist}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddUrlSegment("recordid", userId);
            request.AddUrlSegment("departlist", departlist);

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<DBSubDepartment>> res = JsonConvert.DeserializeObject<ResponseResult<List<DBSubDepartment>>>(result);
                    status(true, "Get SubDepart complete", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, "Exception occured", null);
            }
        }

        public async void UpdateFilterList(CustomerDetail detail, StatusResponse status)
        {
            string url = "MgcApi.svc/UpdateUserFilters", paramData = "";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            foreach (KeyValuePair<string, string> data in detail)
            {
                if (!String.IsNullOrEmpty(paramData))
                {
                    paramData += "&";
                }
                paramData += data.Key + "={" + data.Key + "}";
            }

            request.Resource = url + paramData;

            foreach (KeyValuePair<string, string> data in detail)
            {
                request.AddUrlSegment(data.Key, data.Value);
            }

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string res = JsonConvert.DeserializeObject<string>(result);
                    status(true, "Update complete");
                }
                else
                {
                    status(false, "Http Error");
                }
            }
            catch (Exception ex)
            {
                status(false, "Exception occured");
            }
        }
        #endregion

        #region Efficiency
        public async void GetEfficiencyByLocation(int? filterid, int periodid, StatusResponseList<EfficiencyLocation> status)
        {
            string url = "MgcApi.svc/GetEfficiencyByLocation?belongToLocationID={belongToLocationID}&filterid={filterid}&periodid={periodid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", authInfo.BelongToLocationID);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencyLocation>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencyLocation>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEfficiencyByDepartment(int? locid, int? filterid, int periodid, StatusResponseList<EfficiencyDepartment> status)
        {
            string url = "MgcApi.svc/GetEfficiencyByDepartment?belongToLocationID={belongToLocationID}&locid={locid}&filterid={filterid}&periodid={periodid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencyDepartment>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencyDepartment>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEfficiencyBySubDepartment(int? locid, int? depid, int? filterid, int periodid, StatusResponseList<EfficiencySubDepartment> status)
        {
            string url = "MgcApi.svc/GetEfficiencyBySubDepartment?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&filterid={filterid}&periodid={periodid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencySubDepartment>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencySubDepartment>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEfficiencyByEquipType(int? locid, int? depid, int? subdepid, int? filterid, int periodid, StatusResponseList<EfficiencyEquipmentType> status)
        {
            string url = "MgcApi.svc/GetEfficiencyByEquipType?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&subdepid={subdepid}&filterid={filterid}&periodid={periodid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("subdepid", subdepid);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencyEquipmentType>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencyEquipmentType>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEfficiencyByMachineGroup(int? locid, int? depid, int? subdepid, int? filterid, int periodid, StatusResponseList<EfficiencyEquipmentGroup> status)
        {
            string url = "MgcApi.svc/GetEfficiencyByMachineGroup?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&subdepid={subdepid}&filterid={filterid}&periodid={periodid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("subdepid", subdepid);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencyEquipmentGroup>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencyEquipmentGroup>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEfficiencyByAuxiliaryType(int? locid, int? depid, int? subdepid, int? filterid, int periodid, StatusResponseList<EfficiencyAuxiliary> status)
        {
            string url = "MgcApi.svc/GetEfficiencyByAuxiliaryType?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&subdepid={subdepid}&filterid={filterid}&periodid={periodid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("subdepid", subdepid);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencyAuxiliary>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencyAuxiliary>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEfficiencyByMachine(int? locid, int? depid, int? subdepid, int? filterid, int periodid, int? eqtypeid, int? eqgroupid, int mode, StatusResponseList<EfficiencyMachine> status)
        {
            string url = "MgcApi.svc/GetEfficiencyByMachine?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&subdepid={subdepid}&filterid={filterid}&periodid={periodid}&eqtype={eqtype}&eqgroup={eqgroup}&mode={mode}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("subdepid", subdepid);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);
                    request.AddUrlSegment("eqtype", eqtypeid);
                    request.AddUrlSegment("eqgroup", eqgroupid);
                    request.AddUrlSegment("mode", mode);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencyMachine>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencyMachine>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetEfficiencyByAuxMachine(int? locid, int? depid, int? subdepid, int? filterid, int periodid, int? auxtype, StatusResponseList<EfficiencyAuxiliaryEquipment> status)
        {
            string url = "MgcApi.svc/GetEfficiencyByAuxMachine?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&subdepid={subdepid}&filterid={filterid}&periodid={periodid}&auxtype={auxtype}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("subdepid", subdepid);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);
                    request.AddUrlSegment("auxtype", auxtype);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<EfficiencyAuxiliaryEquipment>> res = JsonConvert.DeserializeObject<ResponseResult<List<EfficiencyAuxiliaryEquipment>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }
        #endregion

        #region Auxiliary Equipment
        public async void GetAuxiliaryTypes(int? filterid, StatusResponseList<AuxiliaryType> status)
        {
            string url = "MgcApi.svc/GetAuxiliaryTypes?belongToLocationID={belongToLocationID}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<AuxiliaryType>> res = JsonConvert.DeserializeObject<ResponseResult<List<AuxiliaryType>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetAuxiliaryEquips(int? filterid, int equiptype, StatusResponseList<AuxiliaryEquip> status)
        {
            string url = "MgcApi.svc/GetAuxiliaryEquips?belongToLocationID={belongToLocationID}&filterid={filterid}&equiptype={equiptype}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("equiptype", equiptype);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<AuxiliaryEquip>> res = JsonConvert.DeserializeObject<ResponseResult<List<AuxiliaryEquip>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        #endregion

        #region Efficiency (Old)
        public async void GetEquipmentList(string department, StatusResponseList<DBEquipment> status)
        {
            string url = "MgcApi.svc/GetEquipmentFilters?departmentList={departmentList}";
            var request = new RestRequest(url, Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddUrlSegment("departmentList", department);

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<DBEquipment>> res = JsonConvert.DeserializeObject<ResponseResult<List<DBEquipment>>>(result);

                    status(true, "Get Equip complete", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, "Exception occured", null);
            }
        }

        public async void GetEquipmentGroupList(StatusResponseList<DBEquipmentGroup> status)
        {
            string url = "MgcApi.svc/GetEquipmentGroupFilters";
            var request = new RestRequest(url, Method.POST);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<List<DBEquipmentGroup>> res = JsonConvert.DeserializeObject<ResponseResult<List<DBEquipmentGroup>>>(result);

                    status(true, "Get EquipGroup complete", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, "Exception occured", null);
            }
        }

        public async void GetEfficienyData(UserDetail userDetail, StatusResponseObject<DBEfficiency> status)
        {
            string url = "MgcApi.svc/GetEfficiency?", paramData = "";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            foreach (KeyValuePair<string, string> data in userDetail)
            {
                if (!String.IsNullOrEmpty(paramData))
                {
                    paramData += "&";
                }
                paramData += data.Key + "={" + data.Key + "}";
            }

            request.Resource = url + paramData;

            foreach (KeyValuePair<string, string> data in userDetail)
            {
                request.AddUrlSegment(data.Key, data.Value);
            }

            try
            {
                IRestResponse response = await restClient.Execute(request);
                var result = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseResult<DBEfficiency> res = JsonConvert.DeserializeObject<ResponseResult<DBEfficiency>>(result);

                    status(true, "GetEfficiencyData complete", res.d);
                }
                else
                {
                    status(false, "Http Error", null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }
        #endregion

        #region Electricity
        public async void GetElectricityByLocation(int? filterid, StatusResponseList<ElectricityLocation> status)
        {
            string url = "MgcApi.svc/GetElectricityByLocation?belongToLocationID={belongToLocationID}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ElectricityLocation>> res = JsonConvert.DeserializeObject<ResponseResult<List<ElectricityLocation>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetElectricityByArea(int? locid, int? filterid, StatusResponseList<ElectricityArea> status)
        {
            string url = "MgcApi.svc/GetElectricityByArea?belongToLocationID={belongToLocationID}&locid={locid}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ElectricityArea>> res = JsonConvert.DeserializeObject<ResponseResult<List<ElectricityArea>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetElectricityByDepartment(int? locid, int? filterid, StatusResponseList<ElectricityDepartment> status)
        {
            string url = "MgcApi.svc/GetElectricityByDepartment?belongToLocationID={belongToLocationID}&locid={locid}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ElectricityDepartment>> res = JsonConvert.DeserializeObject<ResponseResult<List<ElectricityDepartment>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetElectricityBySubDepartment(int? locid, int? depid, int? filterid, StatusResponseList<ElectricitySubDepartment> status)
        {
            string url = "MgcApi.svc/GetElectricityBySubDepartment?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ElectricitySubDepartment>> res = JsonConvert.DeserializeObject<ResponseResult<List<ElectricitySubDepartment>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetElectricityByWasting(int? locid, int? depid, int? subdepid, int? filterid, StatusResponseList<ElectricityMachine> status)
        {
            string url = "MgcApi.svc/GetElectricityWasting?belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&subdepid={subdepid}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("subdepid", subdepid);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ElectricityMachine>> res = JsonConvert.DeserializeObject<ResponseResult<List<ElectricityMachine>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetElectricityByMachine(int mode, int? locid, int? depid, int? subdepid, int? filterid, StatusResponseList<ElectricityMachine> status)
        {
            string url = "MgcApi.svc/GetElectricityByMachine?mode={mode}&belongToLocationID={belongToLocationID}&locid={locid}&depid={depid}&subdepid={subdepid}&filterid={filterid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                if (LocalDataStore.authInfo != null)
                {
                    request.AddUrlSegment("mode", mode);
                    request.AddUrlSegment("belongToLocationID", LocalDataStore.authInfo.BelongToLocationID);
                    request.AddUrlSegment("locid", locid);
                    request.AddUrlSegment("depid", depid);
                    request.AddUrlSegment("subdepid", subdepid);
                    request.AddUrlSegment("filterid", filterid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ElectricityMachine>> res = JsonConvert.DeserializeObject<ResponseResult<List<ElectricityMachine>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        #endregion

        #region Production
        public async void GetProductionByEquipType(int? filterid, int periodid, StatusResponseList<ProductionGeneralModel> status)
        {
            string url = "MgcApi.svc/GetProductionByEquipType?belongToLocationID={belongToLocationID}&filterid={filterid}&periodid={periodid}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                //var authInfo = LocalDataStore.GetInstance().GetUserInfo();
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", authInfo.BelongToLocationID);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ProductionGeneralModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<ProductionGeneralModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
                else
                {
                    status(false, "Authentication failed", null);
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }

        public async void GetProductionByMachine(int? filterid, int periodid, string descch, int eqtype, int mode, StatusResponseList<ProductionDetailModel> status)
        {
            string url = "MgcApi.svc/GetProductionByMachine?belongToLocationID={belongToLocationID}&filterid={filterid}&periodid={periodid}&descch={descch}&eqtype={eqtype}&mode={mode}";
            var request = new RestRequest(url, Method.GET);

            request.AddHeader("Content-Type", "application/json");

            try
            {
                var authInfo = LocalDataStore.authInfo;
                if (authInfo != null)
                {
                    request.AddUrlSegment("belongToLocationID", authInfo.BelongToLocationID);
                    request.AddUrlSegment("filterid", filterid);
                    request.AddUrlSegment("periodid", periodid);
                    request.AddUrlSegment("descch", descch);
                    request.AddUrlSegment("eqtype", eqtype);
                    request.AddUrlSegment("mode", mode);

                    IRestResponse response = await restClient.Execute(request);
                    var result = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseResult<List<ProductionDetailModel>> res = JsonConvert.DeserializeObject<ResponseResult<List<ProductionDetailModel>>>(result);
                        status(true, "success", res.d);
                    }
                    else
                    {
                        status(false, "Http Error", null);
                    }
                }
            }
            catch (Exception ex)
            {
                status(false, ex.Message, null);
            }
        }
        #endregion
    }
}
