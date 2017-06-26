using MBoxMobile.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
            if (CrossConnectivity.Current.IsConnected)
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
            else
            {
                App.LastErrorMessage = App.CurrentTranslation["Common_ErrorMsgNoNetwork"];
                return default(TResult);
            }
        }

        public static async Task Authenticate(CustomerDetail customer)
        {
            UserInfoWrapper returnedObj =
                await GetObjectOrObjectList<UserInfoWrapper>("", BaseUri + string.Format("MgcApi.svc/Login?serverid={0}&username={1}&password={2}&platform={3}&devicetoken={4}", customer.ServerId, customer.Username, customer.Password, customer.Platform, customer.DeviceToken));
            if (returnedObj == null)
                App.LoggedUser = new UserInfo();
            else
                App.LoggedUser = returnedObj.LoggedUser;

            //string localToken = AccessToken;

            //NativeMessageHandler handler = new NativeMessageHandler();
            //handler.AllowAutoRedirect = true;
            //handler.UseCookies = true;
            //HttpClient client = new HttpClient(handler);
            //client.BaseAddress = BaseUri;
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + localToken);
            //var j = JsonConvert.SerializeObject(new { serverid = customer.ServerId, username = customer.Username, password = customer.Password, platform = customer.Platform, devicetoken = customer.DeviceToken });
            //var stringContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");
            //HttpResponseMessage response = new HttpResponseMessage();
            //try
            //{
            //    response = await client.PostAsync(BaseUri + "MgcApi.svc/Login", stringContent);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        HttpContent content = response.Content;
            //        var result = await content.ReadAsStringAsync();
            //        UserInfo returnedObj = JsonConvert.DeserializeObject<UserInfo>(result);
            //        App.LoggedUser = returnedObj;
            //        App.LastErrorMessage = string.Empty;
            //    }
            //    else
            //    {
            //        App.LastErrorMessage = response.StatusCode.ToString();
            //    }
            //}
            //catch (Exception e)
            //{
            //    App.LastErrorMessage = e.ToString();
            //}
        }

        #region Filter
        public static async Task<PersonalFilter> GetPersonalFilter()
        {
            PersonalFilterWrapper returnedObj =
                await GetObjectOrObjectList<PersonalFilterWrapper>("", BaseUri + string.Format("MgcApi.svc/GetPersonalFilterList?userid={0}", App.LoggedUser.login.RecordId));
            if (returnedObj == null)
                return new PersonalFilter();
            else
                return returnedObj.MyPersonalFilter;
        }

        public static async Task<bool> SetSelectedPersonalFilter(int filterid)
        {
            int returnedObj =
                await GetObjectOrObjectList<int>("", BaseUri + string.Format("MgcApi.svc/SetSelectedPersonalFilter?userid={0}&filterid={1}", App.LoggedUser.login.RecordId, filterid));
            if (returnedObj == 0)
                return false;
            else
                return true;
        }
                
        public static async Task<bool> SetPersonalFilterOnOff(bool bOnOff)
        {
            int returnedObj =
                await GetObjectOrObjectList<int>("", BaseUri + string.Format("MgcApi.svc/SetPersonalFilterOnOff?userid={0}&bOn={1}", App.LoggedUser.login.RecordId, bOnOff));
            if (returnedObj == 0)
                return false;
            else
                return true;
        }

        #endregion

        #region Uptime
        public static async Task<List<EfficiencyLocation>> GetEfficiencyPerLocation(int? filterId, int periodId)
        {
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            EfficiencyLocationList returnedObj = 
                await GetObjectOrObjectList<EfficiencyLocationList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByLocation?belongToLocationID={0}&filterid={1}&periodid={2}", App.LoggedUser.login.BelongToLocationID, sFilterId, periodId));
            if (returnedObj == null)
                return new List<EfficiencyLocation>();
            else
                return returnedObj.EfficiencyLocations;
        }

        public static async Task<List<EfficiencyDepartment>> GetEfficiencyPerDepartment(int? locationId, int? filterId, int periodId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            EfficiencyDepartmentList returnedObj =
                await GetObjectOrObjectList<EfficiencyDepartmentList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByDepartment?belongToLocationID={0}&locid={1}&filterid={2}&periodid={3}", App.LoggedUser.login.BelongToLocationID, sLocationId, sFilterId, periodId));
            if (returnedObj == null)
                return new List<EfficiencyDepartment>();
            else
                return returnedObj.EfficiencyDepartments;
        }

        public static async Task<List<EfficiencySubDepartment>> GetEfficiencyPerSubDepartment(int? locationId, int? departmentId, int? filterId, int periodId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            EfficiencySubDepartmentList returnedObj =
                await GetObjectOrObjectList<EfficiencySubDepartmentList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyBySubDepartment?belongToLocationID={0}&locid={1}&depid={2}&filterid={3}&periodid={4}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sFilterId, periodId));
            if (returnedObj == null)
                return new List<EfficiencySubDepartment>();
            else
                return returnedObj.EfficiencySubDepartments;
        }

        public static async Task<List<EfficiencyEquipmentType>> GetEfficiencyPerEquipmentType(int? locationId, int? departmentId, int? subDepartmentId, int? filterId, int periodId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sSubDepartmentId = (subDepartmentId == null) ? "" : subDepartmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            EfficiencyEquipmentTypeList returnedObj =
                await GetObjectOrObjectList<EfficiencyEquipmentTypeList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByEquipType?belongToLocationID={0}&locid={1}&depid={2}&subdepid={3}&filterid={4}&periodid={5}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sSubDepartmentId, sFilterId, periodId));
            if (returnedObj == null)
                return new List<EfficiencyEquipmentType>();
            else
                return returnedObj.EfficiencyEquipmentTypes;
        }

        public static async Task<List<EfficiencyEquipmentGroup>> GetEfficiencyPerEquipmentGroup(int? locationId, int? departmentId, int? subDepartmentId, int? filterId, int periodId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sSubDepartmentId = (subDepartmentId == null) ? "" : subDepartmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            EfficiencyEquipmentGroupList returnedObj =
                await GetObjectOrObjectList<EfficiencyEquipmentGroupList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByMachineGroup?belongToLocationID={0}&locid={1}&depid={2}&subdepid={3}&filterid={4}&periodid={5}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sSubDepartmentId, sFilterId, periodId));
            if (returnedObj == null)
                return new List<EfficiencyEquipmentGroup>();
            else
                return returnedObj.EfficiencyEquipmentGroups;
        }

        public static async Task<List<EfficiencyAuxiliary>> GetEfficiencyPerAuxiliaryType(int? locationId, int? departmentId, int? subDepartmentId, int? filterId, int periodId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sSubDepartmentId = (subDepartmentId == null) ? "" : subDepartmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            EfficiencyAuxiliaryList returnedObj =
                await GetObjectOrObjectList<EfficiencyAuxiliaryList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByAuxiliaryType?belongToLocationID={0}&locid={1}&depid={2}&subdepid={3}&filterid={4}&periodid={5}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sSubDepartmentId, sFilterId, periodId));
            if (returnedObj == null)
                return new List<EfficiencyAuxiliary>();
            else
                return returnedObj.EfficiencyAuxiliaries;
        }

        public static async Task<List<EfficiencyMachine>> GetEfficiencyPerMachine(int? locationId, int? departmentId, int? subDepartmentId, int? filterId, int periodId, int? equipmentTypeId, int? equipmentGroupId, int mode)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sSubDepartmentId = (subDepartmentId == null) ? "" : subDepartmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();
            string sEquipmentTypeId = (equipmentTypeId == null) ? "" : equipmentTypeId.ToString();
            string sEquipmentGroupId = (equipmentGroupId == null) ? "" : equipmentGroupId.ToString();

            EfficiencyMachineList returnedObj =
                await GetObjectOrObjectList<EfficiencyMachineList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByMachine?belongToLocationID={0}&locid={1}&depid={2}&subdepid={3}&filterid={4}&periodid={5}&eqtype={6}&eqgroup={7}&mode={8}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sSubDepartmentId, sFilterId, periodId, sEquipmentTypeId, sEquipmentGroupId, mode));
            if (returnedObj == null)
                return new List<EfficiencyMachine>();
            else
                return returnedObj.EfficiencyMachines;
        }

        public static async Task<List<EfficiencyAuxiliaryEquipment>> GetEfficiencyPerAuxMachine(int? locationId, int? departmentId, int? subDepartmentId, int? filterId, int periodId, int? auxType)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sSubDepartmentId = (subDepartmentId == null) ? "" : subDepartmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();
            string sAuxType = (auxType == null) ? "" : auxType.ToString();

            EfficiencyAuxiliaryEquipmentList returnedObj =
                await GetObjectOrObjectList<EfficiencyAuxiliaryEquipmentList>("", BaseUri + string.Format("MgcApi.svc/GetEfficiencyByAuxMachine?belongToLocationID={0}&locid={1}&depid={2}&subdepid={3}&filterid={4}&periodid={5}&auxtype={6}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sSubDepartmentId, sFilterId, periodId, sAuxType));
            if (returnedObj == null)
                return new List<EfficiencyAuxiliaryEquipment>();
            else
                return returnedObj.EfficiencyAuxiliaryEquipments;
        }
        #endregion

        #region ElectricityUsage
        public static async Task<List<ElectricityLocation>> GetElectricityUsagePerLocation(int? filterId)
        {
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ElectricityLocationList returnedObj =
                await GetObjectOrObjectList<ElectricityLocationList>("", BaseUri + string.Format("MgcApi.svc/GetElectricityByLocation?belongToLocationID={0}&filterid={1}", App.LoggedUser.login.BelongToLocationID, sFilterId));
            if (returnedObj == null)
                return new List<ElectricityLocation>();
            else
                return returnedObj.ElectricityLocations;
        }

        public static async Task<List<ElectricityArea>> GetElectricityUsagePerArea(int? locationId, int? filterId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ElectricityAreaList returnedObj =
                await GetObjectOrObjectList<ElectricityAreaList>("", BaseUri + string.Format("MgcApi.svc/GetElectricityByArea?belongToLocationID={0}&locid={1}&filterid={2}", App.LoggedUser.login.BelongToLocationID, sLocationId, sFilterId));
            if (returnedObj == null)
                return new List<ElectricityArea>();
            else
                return returnedObj.ElectricityAreas;
        }

        public static async Task<List<ElectricityDepartment>> GetElectricityUsagePerDepartment(int? locationId, int? filterId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ElectricityDepartmentList returnedObj =
                await GetObjectOrObjectList<ElectricityDepartmentList>("", BaseUri + string.Format("MgcApi.svc/GetElectricityByDepartment?belongToLocationID={0}&locid={1}&filterid={2}", App.LoggedUser.login.BelongToLocationID, sLocationId, sFilterId));
            if (returnedObj == null)
                return new List<ElectricityDepartment>();
            else
                return returnedObj.ElectricityDepartments;
        }

        public static async Task<List<ElectricitySubDepartment>> GetElectricityUsagePerSubDepartment(int? locationId, int? departmentId, int? filterId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ElectricitySubDepartmentList returnedObj =
                await GetObjectOrObjectList<ElectricitySubDepartmentList>("", BaseUri + string.Format("MgcApi.svc/GetElectricityBySubDepartment?belongToLocationID={0}&locid={1}&depid={2}&filterid={3}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sFilterId));
            if (returnedObj == null)
                return new List<ElectricitySubDepartment>();
            else
                return returnedObj.ElectricitySubDepartments;
        }

        public static async Task<List<ElectricityMachine>> GetElectricityUsagePerWasting(int? locationId, int? departmentId, int? subDepartmentId, int? filterId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sSubDepartmentId = (subDepartmentId == null) ? "" : subDepartmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ElectricityMachineList returnedObj =
                await GetObjectOrObjectList<ElectricityMachineList>("", BaseUri + string.Format("MgcApi.svc/GetElectricityWasting?belongToLocationID={0}&locid={1}&depid={2}&subdepid={3}&filterid={4}", App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sSubDepartmentId, sFilterId));
            if (returnedObj == null)
                return new List<ElectricityMachine>();
            else
                return returnedObj.ElectricityMachines;
        }

        public static async Task<List<ElectricityMachine>> GetElectricityUsagePerMachine(int mode, int? locationId, int? departmentId, int? subDepartmentId, int? filterId)
        {
            string sLocationId = (locationId == null) ? "" : locationId.ToString();
            string sDepartmentId = (departmentId == null) ? "" : departmentId.ToString();
            string sSubDepartmentId = (subDepartmentId == null) ? "" : subDepartmentId.ToString();
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ElectricityMachineList returnedObj =
                await GetObjectOrObjectList<ElectricityMachineList>("", BaseUri + string.Format("MgcApi.svc/GetElectricityByMachine?mode={0}&belongToLocationID={1}&locid={2}&depid={3}&subdepid={4}&filterid={5}", mode, App.LoggedUser.login.BelongToLocationID, sLocationId, sDepartmentId, sSubDepartmentId, sFilterId));
            if (returnedObj == null)
                return new List<ElectricityMachine>();
            else
                return returnedObj.ElectricityMachines;            
        }
        #endregion

        #region Production
        public static async Task<List<ProductionGeneral>> GetProductionPerEquipmentType(int? filterId, int periodId)
        {
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ProductionGeneralList returnedObj =
                await GetObjectOrObjectList<ProductionGeneralList>("", BaseUri + string.Format("MgcApi.svc/GetProductionByEquipType?belongToLocationID={0}&filterid={1}&periodid={2}", App.LoggedUser.login.BelongToLocationID, sFilterId, periodId));
            if (returnedObj == null)
                return new List<ProductionGeneral>();
            else
                return returnedObj.ProductionGenerals;
        }

        public static async Task<List<ProductionDetail>> GetProductionPerMachine(int? filterId, int periodId, string descCN, int eqType, int mode)
        {
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            ProductionDetailList returnedObj =
                await GetObjectOrObjectList<ProductionDetailList>("", BaseUri + string.Format("MgcApi.svc/GetProductionByMachine?belongToLocationID={0}&filterid={1}&periodid={2}&descch={3}&eqtype={4}&mode={5}", App.LoggedUser.login.BelongToLocationID, sFilterId, periodId, descCN, eqType, mode));
            if (returnedObj == null)
                return new List<ProductionDetail>();
            else
                return returnedObj.ProductionDetails;
        }
        #endregion

        #region AuxiliaryEquipment
        public static async Task<List<AuxiliaryType>> GetAuxiliaryTypes(int? filterId)
        {
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            AuxiliaryTypeList returnedObj =
                await GetObjectOrObjectList<AuxiliaryTypeList>("", BaseUri + string.Format("MgcApi.svc/GetAuxiliaryTypes?belongToLocationID={0}&filterid={1}", App.LoggedUser.login.BelongToLocationID, sFilterId));
            if (returnedObj == null)
                return new List<AuxiliaryType>();
            else
                return returnedObj.AuxiliaryTypes;
        }

        public static async Task<List<AuxiliaryEquipment>> GetAuxiliaryEquipments(int? filterId, int equipmentTypeId)
        {
            string sFilterId = (filterId == null) ? "" : filterId.ToString();

            AuxiliaryEquipmentList returnedObj =
                await GetObjectOrObjectList<AuxiliaryEquipmentList>("", BaseUri + string.Format("MgcApi.svc/GetAuxiliaryEquips?belongToLocationID={0}&filterid={1}&equiptype={2}", App.LoggedUser.login.BelongToLocationID, sFilterId, equipmentTypeId));
            if (returnedObj == null)
                return new List<AuxiliaryEquipment>();
            else
                return returnedObj.AuxiliaryEquipments;
        }
        #endregion

        #region InputNotification
        public static async Task<List<MaterialModel>> GetElectricityWasteCauseList()
        {
            MaterialModelList returnedObj = await GetObjectOrObjectList<MaterialModelList>("", BaseUri + "MgcApi.svc/GetElectricityWasteCauseList");
            if (returnedObj == null)
                return new List<MaterialModel>();
            else
                return returnedObj.Materials;
        }

        public static async Task<List<MaterialModel>> GetNotificationTypeList(int alterEquipmentType)
        {
            MaterialModelList returnedObj = await GetObjectOrObjectList<MaterialModelList>("", BaseUri + string.Format("MgcApi.svc/GetNotificationTypeList?alterEquipmentType={0}", alterEquipmentType));
            if (returnedObj == null)
                return new List<MaterialModel>();
            else
                return returnedObj.Materials;
        }

        public static async Task<List<MaterialModel>> GetSolutionCauseList(int notificationId)
        {
            MaterialModelList returnedObj = await GetObjectOrObjectList<MaterialModelList>("", BaseUri + string.Format("MgcApi.svc/GetSolutionCauseList?notificationID={0}", notificationId));
            if (returnedObj == null)
                return new List<MaterialModel>();
            else
                return returnedObj.Materials;
        }

        public static async Task<bool> InputAcknowledge(int notificationId, string description)
        {
            int returnedObj = await GetObjectOrObjectList<int>("", BaseUri + string.Format("MgcApi.svc/InputAcknowledge?notificationID={0}&userID={1}&description={2}", notificationId, App.LoggedUser.login.RecordId, description));
            if (returnedObj == 10000)
                return true;
            else
                return false;
        }

        public static async Task<bool> InputKwh(int notificationId, string description, int causeId)
        {
            int returnedObj = await GetObjectOrObjectList<int>("", BaseUri + string.Format("MgcApi.svc/InputKwh?notificationID={0}&userID={1}&description={2}&causeID={3}", notificationId, App.LoggedUser.login.RecordId, description, causeId));
            if (returnedObj == 10000)
                return true;
            else
                return false;
        }

        public static async Task<bool> InputDescription(int notificationId, string description, int alterDescription, int alterEquipType)
        {
            int returnedObj = await GetObjectOrObjectList<int>("", BaseUri + string.Format("MgcApi.svc/InputDescription?notificationID={0}&userID={1}&description={2}&alterDescription={3}&alterEquipType={4}", notificationId, App.LoggedUser.login.RecordId, description, alterDescription, alterEquipType));
            if (returnedObj == 10000)
                return true;
            else
                return false;
        }

        public static async Task<bool> InputSolution(int notificationId, string solution, int? solutionCauseId)
        {
            string sSolutionCauseId = (solutionCauseId == null) ? "" : solutionCauseId.ToString();

            int returnedObj = await GetObjectOrObjectList<int>("", BaseUri + string.Format("MgcApi.svc/InputSolution?notificationID={0}&userID={1}&solution={2}&solutionCauseID={3}", notificationId, App.LoggedUser.login.RecordId, solution, sSolutionCauseId));
            if (returnedObj == 10000)
                return true;
            else
                return false;
        }

        //public async void InputAcknowledge(int notificationID, string desc, StatusResponse status)
        //{
        //    string url = "MgcApi.svc/InputAcknowledge?notificationID={notificationID}&userID={userID}&description={description}";
        //    var request = new RestRequest(url, Method.GET);

        //    request.AddHeader("Content-Type", "application/json");

        //    try
        //    {
        //        var authInfo = LocalDataStore.authInfo;
        //        if (authInfo != null)
        //        {
        //            request.AddUrlSegment("notificationID", notificationID);
        //            request.AddUrlSegment("userID", authInfo.RecordId);
        //            request.AddUrlSegment("description", desc);

        //            IRestResponse response = await restClient.Execute(request);
        //            var result = response.Content;
        //            ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

        //            if (res.d == 10000)
        //            {
        //                status(true, "success");
        //            }
        //            else
        //            {
        //                status(false, "failed");
        //            }
        //        }
        //        else
        //        {
        //            status(false, "User not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status(false, ex.Message);
        //    }
        //}

        //public async void InputKwh(int notificationID, string desc, int causeID, StatusResponse status)
        //{
        //    string url = "MgcApi.svc/InputKwh?notificationID={notificationID}&userID={userID}&description={description}&causeID={causeID}";
        //    var request = new RestRequest(url, Method.GET);

        //    request.AddHeader("Content-Type", "application/json");

        //    try
        //    {
        //        var authInfo = LocalDataStore.authInfo;
        //        if (authInfo != null)
        //        {
        //            request.AddUrlSegment("notificationID", notificationID);
        //            request.AddUrlSegment("userID", authInfo.RecordId);
        //            request.AddUrlSegment("description", desc);
        //            request.AddUrlSegment("causeID", causeID);

        //            IRestResponse response = await restClient.Execute(request);
        //            var result = response.Content;
        //            ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

        //            if (res.d == 10000)
        //            {
        //                status(true, "success");
        //            }
        //            else
        //            {
        //                status(false, "failed");
        //            }
        //        }
        //        else
        //        {
        //            status(false, "User not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status(false, ex.Message);
        //    }
        //}

        //public async void InputDescription(int notificationID, string desc, int alterDesc, int equiptype, StatusResponse status)
        //{
        //    string url = "MgcApi.svc/InputDescription?notificationID={notificationID}&userID={userID}&description={description}&alterDescription={alterDescription}&alterEquipType={alterEquipType}";
        //    var request = new RestRequest(url, Method.GET);

        //    request.AddHeader("Content-Type", "application/json");

        //    try
        //    {
        //        var authInfo = LocalDataStore.authInfo;
        //        if (authInfo != null)
        //        {
        //            request.AddUrlSegment("notificationID", notificationID);
        //            request.AddUrlSegment("userID", authInfo.RecordId);
        //            request.AddUrlSegment("description", desc);
        //            request.AddUrlSegment("alterDescription", alterDesc);
        //            request.AddUrlSegment("alterEquipType", equiptype);

        //            IRestResponse response = await restClient.Execute(request);
        //            var result = response.Content;
        //            ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

        //            if (res.d == 10000)
        //            {
        //                status(true, "success");
        //            }
        //            else
        //            {
        //                status(false, "failed");
        //            }
        //        }
        //        else
        //        {
        //            status(false, "User not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status(false, ex.Message);
        //    }
        //}

        //public async void InputSolution(int notificationID, string solution, int? solutionCauseID, StatusResponse status)
        //{
        //    string url = "MgcApi.svc/InputSolution?notificationID={notificationID}&userID={userID}&solution={solution}&solutionCauseID={solutionCauseID}";
        //    var request = new RestRequest(url, Method.GET);

        //    request.AddHeader("Content-Type", "application/json");

        //    try
        //    {
        //        var authInfo = LocalDataStore.authInfo;
        //        if (authInfo != null)
        //        {
        //            request.AddUrlSegment("notificationID", notificationID);
        //            request.AddUrlSegment("userID", authInfo.RecordId);
        //            request.AddUrlSegment("solution", solution);
        //            request.AddUrlSegment("solutionCauseID", solutionCauseID);

        //            IRestResponse response = await restClient.Execute(request);
        //            var result = response.Content;
        //            ResponseResult<int> res = JsonConvert.DeserializeObject<ResponseResult<int>>(result);

        //            if (res.d == 10000)
        //            {
        //                status(true, "success");
        //            }
        //            else
        //            {
        //                status(false, "failed");
        //            }
        //        }
        //        else
        //        {
        //            status(false, "User not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status(false, ex.Message);
        //    }
        //}
        #endregion
    }
}
