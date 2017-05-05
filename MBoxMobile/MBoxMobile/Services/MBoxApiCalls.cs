using MBoxMobile.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MBoxMobile.Services
{
    public static class MBoxApiCalls
    {
        static Uri BaseUri = new Uri("http://121.33.199.84:200/");
        static string AccessToken = "";

        public static async Task<TResult> GetObjectOrObjectList<TResult>(string serializedParameters, string requestUri, bool isPostMethod = false)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            handler.UseCookies = true;
            HttpClient client = new HttpClient(handler);
            client.BaseAddress = BaseUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            var stringContent = serializedParameters != string.Empty ? new StringContent(serializedParameters, Encoding.UTF8, "application/json") : null;
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (isPostMethod)
                    response = await client.PostAsync(requestUri, stringContent);
                else
                    response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    string result = await content.ReadAsStringAsync();
                    TResult returnedObjs = JsonConvert.DeserializeObject<TResult>(result);
                    App.LastErrorMessage = string.Empty;
                    return returnedObjs;
                }
                else
                {
                    App.LastErrorMessage = response.StatusCode.ToString();
                    return default(TResult);
                }
            }
            catch (Exception e)
            {
                App.LastErrorMessage = e.ToString();
                return default(TResult);
            }
        }

        public static async Task Authenticate(CustomerDetail customer)
        {
            string localToken = AccessToken;

            NativeMessageHandler handler = new NativeMessageHandler();
            handler.AllowAutoRedirect = true;
            handler.UseCookies = true;
            HttpClient client = new HttpClient(handler);
            client.BaseAddress = BaseUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + localToken);
            var j = JsonConvert.SerializeObject(new { serverid = customer.ServerId, username = customer.Username, password = customer.Password, platform = customer.Platform, devicetoken = customer.DeviceToken });
            var stringContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.PostAsync(BaseUri + "MgcApi.svc/Login", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    var result = await content.ReadAsStringAsync();
                    UserInfo returnedObj = JsonConvert.DeserializeObject<UserInfo>(result);
                    App.LoggedUser = returnedObj;
                    App.LastErrorMessage = string.Empty;
                }
                else
                {
                    App.LastErrorMessage = response.StatusCode.ToString();
                }
            }
            catch (Exception e)
            {
                App.LastErrorMessage = e.ToString();
            }
        }

        #region Uptime

        public static async Task<List<EfficiencyLocation>> GetEfficiencyByLocation(string belongToLocationId, string filterId, string periodId)
        {
            EfficiencyLocationList returnedObj = 
                await GetObjectOrObjectList<EfficiencyLocationList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByLocation?belongToLocationID={0}&filterid={1}&periodid={2}", belongToLocationId, filterId, periodId));
            if (returnedObj == null)
            {
                return new List<EfficiencyLocation>();
            }
            else
            {
                return returnedObj.EfficiencyLocations;
            }
        }

        #endregion
    }
}
