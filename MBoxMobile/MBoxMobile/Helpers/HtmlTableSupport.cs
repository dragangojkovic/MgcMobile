﻿using MBoxMobile.Models;
using System.Collections.Generic;
using System.Text;

namespace MBoxMobile.Helpers
{
    public static class HtmlTableSupport
    {
        #region HtmlTableMarkup
        private static string HtmlTableMarkup = @"
<!DOCTYPE html>
<html>
<head>
<script type='text/javascript'>
    onload = function()
    {
        if(!document.getElementsByTagName || !document.createTextNode) return;
        var rows = document.getElementById('mbox').getElementsByTagName('tr');
        for(i = 0; i < rows.length; i++)
        {
            rows[i].onclick = function() {
                HighLightTR(this,'#30a0ff','#ffffff');
                var id = this.cells[0].innerText;
			    window.open('http://localhost?id='+id);
            }
        }
        for(var i = 0; i < rows.length; i++)
	    {
		    rows[i].bgColor = (i%2==0) ? '#f1f4f7' : '#e9eaec' ;
	    }
    }
    var preEl;
    var orgBColor;
    var orgTColor;
    function HighLightTR(el, backColor,textColor){
      if(typeof(preEl)!='undefined') {
         preEl.bgColor=orgBColor;
	     preEl.style.color = orgTColor;
         try{ChangeTextColor(preEl,orgTColor);}catch(e){;}
      }
      orgBColor = el.bgColor;
      orgTColor = el.style.color;
      el.bgColor = backColor;
      el.style.color = textColor;

      try{ChangeTextColor(el,textColor);}catch(e){;}
      preEl = el;
    }
    function ChangeTextColor(a_obj,a_color){
       for (i=0;i<a_obj.cells.length;i++)
        a_obj.cells(i).style.color=a_color;
    }
</script>
<style>
table, th, td {
    border: 1px solid #aaa;
    border-collapse: collapse;
}
table {
    margin-right: 20px;
}
th {
    padding: 5px;
    text-align: center;
}
td {
    padding: 5px;
}
table th {
    background-color: white;
    color: gray;
}
.center-text {
	text-align: center;
}
.right-text {
	text-align: right;
}
</style>
</head>
<body>

<table>
    <thead>
        {#TableHeader}
    </thead>
    <tbody id='mbox'>
        {#TableBody}
    </tbody>
</table>

</body>
</html>";
        #endregion

        #region UptimePage Headers

        public static string Uptime_Locations_TableHeader()
        {
            string html = @"
<tr>
    <th colspan=""2""></th>
    <th colspan=""3"" style=""white-space:nowrap"">{#Equipment}</th>
</tr>
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">{#Name}</th>
    <th><div style=""width:60px;"">{#Uptime}</div></th>
    <th><div style=""width:60px;"">{#On}</div></th>
    <th><div style=""width:60px;"">{#Off}</div></th>
    <th><div style=""width:60px;"">{#Errors}</div></th>
</tr>
            ";

            html = html.Replace("{#Equipment}", App.CurrentTranslation["Uptime_LocationsTableEquipment"]);
            html = html.Replace("{#Name}", App.CurrentTranslation["Uptime_Locations"]);
            html = html.Replace("{#Uptime}", App.CurrentTranslation["Uptime_Uptime"]);
            html = html.Replace("{#On}", App.CurrentTranslation["Uptime_On"]);
            html = html.Replace("{#Off}", App.CurrentTranslation["Uptime_Off"]);
            html = html.Replace("{#Errors}", App.CurrentTranslation["Uptime_Errors"]);

            return html;
        }

        public static string Uptime_Medium_TableHeader(string tableName)
        {
            string html = @"
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">{#Name}</th>
    <th><div style=""width:60px;"">{#Uptime}</div></th>
    <th><div style=""width:60px;"">{#On}</div></th>
    <th><div style=""width:60px;"">{#Off}</div></th>
    <th><div style=""width:60px;"">{#Errors}</div></th>
</tr>
            ";

            switch (tableName)
            {
                case "Department":
                    html = html.Replace("{#Name}", App.CurrentTranslation["Uptime_Departments"]);
                    break;
                case "SubDepartment":
                    html = html.Replace("{#Name}", App.CurrentTranslation["Uptime_SubDepartments"]);
                    break;
                case "Equipment":
                    html = html.Replace("{#Name}", App.CurrentTranslation["Uptime_Equipment"]);
                    break;
                case "EquipmentGroup":
                    html = html.Replace("{#Name}", App.CurrentTranslation["Uptime_EquipmentGroup"]);
                    break;
            }
            html = html.Replace("{#Uptime}", App.CurrentTranslation["Uptime_Uptime"]);
            html = html.Replace("{#On}", App.CurrentTranslation["Uptime_On"]);
            html = html.Replace("{#Off}", App.CurrentTranslation["Uptime_Off"]);
            html = html.Replace("{#Errors}", App.CurrentTranslation["Uptime_Errors"]);

            return html;
        }

        public static string Uptime_Small_TableHeader()
        {
            string html = @"
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">Auxiliary equipment</th>
    <th><div style=""width:60px;"">{#On}</div></th>
</tr>
            ";

            html = html.Replace("{#On}", App.CurrentTranslation["Uptime_On"]);

            return html;
        }

        public static string Uptime_Details_TableHeader()
        {
            string html = @"
<tr>
    <th colspan=""2""></th>
    <th colspan=""2"" style=""white-space:nowrap"">{#Current}</th>
    <th colspan=""2"" style=""white-space:nowrap"">{#OffTime}</th>
    <th colspan=""7""></th>
</tr>
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th><div style=""width:60px;"">{#Uptime}</div></th>
    <th><div style=""width:60px;"">{#Status}</div></th>
    <th>{#RunTime}</th>
    <th><div style=""width:60px;"">{#Stops}</div></th>
    <th>{#StopTime}</th>
    <th style=""white-space:nowrap"">{#Group}</th>
    <th style=""white-space:nowrap"">{#Location}</th>
    <th style=""white-space:nowrap"">{#Department}</th>
    <th style=""white-space:nowrap"">{#SubDepartment}</th>
    <th style=""white-space:nowrap"">{#Type}</th>
    <th style=""white-space:nowrap"">{#Remark}</th>
    <th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
            ";

            html = html.Replace("{#Current}", App.CurrentTranslation["UptimeDetails_Current"]);
            html = html.Replace("{#OffTime}", App.CurrentTranslation["UptimeDetails_OffTime"]);
            html = html.Replace("{#Uptime}", App.CurrentTranslation["Uptime_Uptime"]);
            html = html.Replace("{#Status}", App.CurrentTranslation["UptimeDetails_Status"]);
            html = html.Replace("{#RunTime}", App.CurrentTranslation["UptimeDetails_RunTime"]);
            html = html.Replace("{#Stops}", App.CurrentTranslation["UptimeDetails_Stops"]);
            html = html.Replace("{#StopTime}", App.CurrentTranslation["UptimeDetails_StopTime"]);
            html = html.Replace("{#Group}", App.CurrentTranslation["UptimeDetails_Group"]);
            html = html.Replace("{#Location}", App.CurrentTranslation["Uptime_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["UptimeDetails_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["UptimeDetails_SubDepartment"]);
            html = html.Replace("{#Type}", App.CurrentTranslation["UptimeDetails_Type"]);
            html = html.Replace("{#Remark}", App.CurrentTranslation["UptimeDetails_Remark"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["UptimeDetails_SystemData"]);

            return html;
        }

        public static string Uptime_AuxiliaryEquipments_TableHeader()
        {
            string html = @"
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th style=""white-space:nowrap"">{#Name}</th>
    <th><div style=""width:60px;"">{#Type}</div></th>
    <th style=""white-space:nowrap"">{#Group}</th>
    <th><div style=""width:60px;"">{#On}</div></th>
    <th style=""white-space:nowrap"">{#Location}</th>
    <th style=""white-space:nowrap"">{#Department}</th>
    <th style=""white-space:nowrap"">{#SubDepartment}</th>
    <th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
            ";

            html = html.Replace("{#Name}", App.CurrentTranslation["UptimeDetails_Name"]);
            html = html.Replace("{#Type}", App.CurrentTranslation["UptimeDetails_Type"]);
            html = html.Replace("{#Group}", App.CurrentTranslation["UptimeDetails_Group"]);
            html = html.Replace("{#On}", App.CurrentTranslation["Uptime_On"]);
            html = html.Replace("{#Location}", App.CurrentTranslation["Uptime_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["UptimeDetails_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["UptimeDetails_SubDepartment"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["UptimeDetails_SystemData"]);

            return html;
        }

        #endregion

        #region UptimePage Contents

        public static string Uptime_Medium_TableContent(IEnumerable<EfficiencyModel> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (EfficiencyModel el in source)
                {
                    string template = @"
<tr>
    <td style=""display:none"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td class=""right-text"">{#2}</td>
	<td class=""right-text"">{#3}</td>
	<td class=""right-text"">{#4}</td>
	<td class=""right-text"">{#5}</td>
</tr>";
                    template = template.Replace("{#0}", el.ID.ToString());
                    template = template.Replace("{#1}", el.Name);
                    template = template.Replace("{#2}", el.EfficiencyPercent != null ? el.EfficiencyPercent.ToString() + "%" : "");
                    template = template.Replace("{#3}", el.On.ToString());
                    template = template.Replace("{#4}", el.Off.ToString());
                    template = template.Replace("{#5}", el.Errors.ToString());

                    html += template;
                }

            }
            return html;
        }

        public static string Uptime_Small_TableContent(IEnumerable<EfficiencyModel> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (EfficiencyModel el in source)
                {
                    string template = @"
<tr>
    <td style=""display:none"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td class=""right-text"">{#2}</td>
</tr>";
                    template = template.Replace("{#0}", el.ID.ToString());
                    template = template.Replace("{#1}", el.Name);
                    template = template.Replace("{#2}", el.EfficiencyPercent != null ? el.EfficiencyPercent.ToString() + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        public static string Uptime_Details_TableContent(IEnumerable<EfficiencyMachine> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (EfficiencyMachine el in source)
                {
                    string template = @"
<tr>
    <td class=""right-text"">{#0}</td>
	<td class=""right-text"">{#1}</td>
	<td class=""center-text"">{#2}</td>
	<td class=""center-text"">{#3}</td>
	<td class=""right-text"">{#4}</td>
	<td class=""center-text"">{#5}</td>
    <td style=""white-space:nowrap"">{#6}</td>
    <td style=""white-space:nowrap"">{#7}</td>
    <td style=""white-space:nowrap"">{#8}</td>
    <td style=""white-space:nowrap"">{#9}</td>
    <td style=""white-space:nowrap"">{#10}</td>
    <td style=""white-space:nowrap"">{#11}</td>
    <td class=""right-text"">{#12}</td>
</tr>";
                    template = template.Replace("{#0}", el.MachineNumber);
                    template = template.Replace("{#1}", el.EfficiencyPercent != null ? el.EfficiencyPercent.ToString() + "%" : "");
                    template = template.Replace("{#2}", el.Status);
                    template = template.Replace("{#3}", el.RunTime);
                    template = template.Replace("{#4}", el.Stops);
                    template = template.Replace("{#5}", el.StopTime);
                    template = template.Replace("{#6}", el.EquipmentGroup);
                    template = template.Replace("{#7}", el.Location);
                    template = template.Replace("{#8}", el.Department);
                    template = template.Replace("{#9}", el.DepartmentSubName);
                    template = template.Replace("{#10}", "");
                    template = template.Replace("{#11}", "");
                    template = template.Replace("{#12}", el.SystemData != null ? el.SystemData + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        public static string Uptime_AuxiliaryEquipments_TableContent(IEnumerable<EfficiencyAuxiliaryEquipment> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (EfficiencyAuxiliaryEquipment el in source)
                {
                    string template = @"
<tr>
    <td class=""right-text"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td class=""right-text"">{#2}</td>
	<td style=""white-space:nowrap"">{#3}</td>
	<td class=""right-text"">{#4}</td>
	<td style=""white-space:nowrap"">{#5}</td>
    <td style=""white-space:nowrap"">{#6}</td>
    <td style=""white-space:nowrap"">{#7}</td>
    <td class=""right-text"">{#8}</td>
</tr>";
                    template = template.Replace("{#0}", el.MachineNumber);
                    template = template.Replace("{#1}", el.MachineName);
                    template = template.Replace("{#2}", el.MachineType);
                    template = template.Replace("{#3}", el.MachineGroup);
                    template = template.Replace("{#4}", el.EfficiencyPercent != null ? el.EfficiencyPercent.ToString() + "%" : "");
                    template = template.Replace("{#5}", el.Location);
                    template = template.Replace("{#6}", el.Department);
                    template = template.Replace("{#7}", el.SubDepartment);
                    template = template.Replace("{#8}", el.SystemData != null ? el.SystemData + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        #endregion

        #region ElectricityUsage Headers

        public static string ElectricityUsage_Medium_TableHeader(string tableName)
        {
            string html = @"
<tr>
<th style=""display:none"">Id</th>
<th style=""white-space:nowrap"">{#Name}</th>
<th><div style=""width:60px;"">{#Current}</div></th>
<th><div style=""width:60px;"">{#Total}</div></th>
<th{#NoDetailsVisibility}><div style=""width:60px;"">{#NoDetails}</div></th>
<th><div style=""width:60px;"">{#Waste}</div></th>
<th><div style=""width:60px;"">{#WastePer}</div></th>
</tr>
	";

            switch (tableName)
            {
                case "Location":
                    html = html.Replace("{#Name}", App.CurrentTranslation["ElectricityUsage_Locations"]);
                    html = html.Replace("{#NoDetailsVisibility}", string.Empty);
                    break;
                case "Area":
                    html = html.Replace("{#Name}", App.CurrentTranslation["ElectricityUsage_Areas"]);
                    html = html.Replace("{#NoDetailsVisibility}", string.Empty);
                    break;
                case "Department":
                    html = html.Replace("{#Name}", App.CurrentTranslation["ElectricityUsage_Departments"]);
                    html = html.Replace("{#NoDetailsVisibility}", @" style=""display: none; """);
                    break;
                case "SubDepartment":
                    html = html.Replace("{#Name}", App.CurrentTranslation["ElectricityUsage_SubDepartments"]);
                    html = html.Replace("{#NoDetailsVisibility}", @" style=""display: none; """);
                    break;
            }
            html = html.Replace("{#Current}", App.CurrentTranslation["ElectricityUsage_Current"]);
            html = html.Replace("{#Total}", App.CurrentTranslation["ElectricityUsage_Total"]);
            html = html.Replace("{#NoDetails}", App.CurrentTranslation["ElectricityUsage_NoDetails"]);
            html = html.Replace("{#Waste}", App.CurrentTranslation["ElectricityUsage_Waste"]);
            html = html.Replace("{#WastePer}", App.CurrentTranslation["ElectricityUsage_Waste"] + "%");

            return html;
        }

        public static string ElectricityUsage_Large_TableHeader()
        {
            string html = @"
<tr>
<th><div style=""width:60px;"">M#</div></th>
<th><div style=""width:60px;"">{#Group}</div></th>
<th><div style=""width:60px;"">{#Eff}</div></th>
<th style=""white-space:nowrap"">{#Status}</th>
<th><div style=""width:60px;"">{#Current}</div></th>
<th><div style=""width:60px;"">{#Total}</div></th>
<th><div style=""width:60px;"">{#Waste}</div></th>
<th><div style=""width:60px;"">{#WastePer}</div></th>
<th style=""white-space:nowrap"">{#Location}</th>
<th style=""white-space:nowrap"">{#Department}</th>
<th style=""white-space:nowrap"">{#SubDepartment}</th>
<th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
	";

            html = html.Replace("{#Group}", App.CurrentTranslation["ElectricityUsage_Group"]);
            html = html.Replace("{#Eff}", App.CurrentTranslation["ElectricityUsage_Eff"]);
            html = html.Replace("{#Status}", App.CurrentTranslation["ElectricityUsage_Status"]);
            html = html.Replace("{#Current}", App.CurrentTranslation["ElectricityUsage_Current"]);
            html = html.Replace("{#Total}", App.CurrentTranslation["ElectricityUsage_Total"]);
            html = html.Replace("{#Waste}", App.CurrentTranslation["ElectricityUsage_Waste"]);
            html = html.Replace("{#WastePer}", App.CurrentTranslation["ElectricityUsage_Waste"] + "%");
            html = html.Replace("{#Location}", App.CurrentTranslation["ElectricityUsage_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["ElectricityUsage_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["ElectricityUsage_SubDepartment"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["ElectricityUsage_SystemData"]);

            return html;
        }

        #endregion

        #region ElectricityUsage Contents

        public static string ElectricityUsage_Medium_TableContent(IEnumerable<ElectricityModel> source, string tableName)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (ElectricityModel el in source)
                {
                    string template = @"
<tr>
    <td style=""display:none"">{#ID}</td>
	<td style=""white-space:nowrap"">{#Name}</td>
	<td class=""right-text"">{#Current}</td>
	<td class=""right-text"">{#Total}</td>
    <td class=""right-text""{#NoDetailsVisibility}>{#NoDetails}</td>
	<td class=""right-text"">{#Waste}</td>
	<td class=""right-text"">{#WastePer}</td>
</tr>";
                    template = template.Replace("{#ID}", el.ID.ToString());
                    template = template.Replace("{#Name}", el.Name);
                    template = template.Replace("{#Current}", el.Current);
                    template = template.Replace("{#Total}", el.Total);
                    template = template.Replace("{#NoDetails}", el.NoDetails);
                    template = template.Replace("{#Waste}", el.Waste);
                    template = template.Replace("{#WastePer}", el.WastePer != null ? el.Waste + "%" : "");

                    if (tableName == "Location" || tableName == "Area")
                        template = template.Replace("{#NoDetailsVisibility}", string.Empty);
                    if (tableName == "Department" || tableName == "SubDepartment")
                        template = template.Replace("{#NoDetailsVisibility}", @" style=""display: none; """);

                    html += template;
                }

            }
            return html;
        }

        public static string ElectricityUsage_Large_TableContent(IEnumerable<ElectricityMachine> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (ElectricityMachine el in source)
                {
                    string template = @"
<tr>
<td class=""right-text"">{#MachineNumber}</td>
<td style=""white-space:nowrap"">{#MachineGroupName}</td>
<td class=""right-text"">{#EfficiencyPer}</td>
<td style=""white-space:nowrap"">{#StatusText}</td>
<td class=""right-text"">{#Current}</td>
<td class=""right-text"">{#Total}</td>
<td class=""right-text"">{#Waste}</td>
<td class=""right-text"">{#WastePer}</td>
<td style=""white-space:nowrap"">{#Location}</td>
<td style=""white-space:nowrap"">{#DepartmentName}</td>
<td style=""white-space:nowrap"">{#SubDepartmentName}</td>
<td style=""white-space:nowrap"">{#SystemData}</td>
</tr>";
                    template = template.Replace("{#MachineNumber}", el.MachineNumber.ToString());
                    template = template.Replace("{#MachineGroupName}", el.MachineGroupName);
                    template = template.Replace("{#EfficiencyPer}", el.EfficiencyPer != null ? el.EfficiencyPer + "%" : "");
                    template = template.Replace("{#StatusText}", el.StatusText);
                    template = template.Replace("{#Current}", el.Current);
                    template = template.Replace("{#Total}", el.Total);
                    template = template.Replace("{#Waste}", el.Waste);
                    template = template.Replace("{#WastePer}", el.WastePer != null ? el.Waste + "%" : "");
                    template = template.Replace("{#Location}", el.Location);
                    template = template.Replace("{#DepartmentName}", el.DepartmentName);
                    template = template.Replace("{#SubDepartmentName}", el.SubDepartmentName);
                    template = template.Replace("{#SystemData}", el.SystemData);

                    html += template;
                }

            }
            return html;
        }

        #endregion

        #region ProductionPage Headers

        public static string Production_Equipment_TableHeader()
        {
            string html = @"
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">{#Equipment}</th>
    <th><div style=""width:60px;"">{#Eff}</div></th>
    <th><div style=""width:60px;"">{#On}</div></th>
    <th><div style=""width:60px;"">{#Off}</div></th>
</tr>
            ";

            html = html.Replace("{#Equipment}", App.CurrentTranslation["Production_Equipment"]);
            html = html.Replace("{#Eff}", App.CurrentTranslation["Production_Eff"]);
            html = html.Replace("{#On}", App.CurrentTranslation["Production_On"]);
            html = html.Replace("{#Off}", App.CurrentTranslation["Production_Off"]);

            return html;
        }

        public static string ProductionDetail_Moulding_TableHeader()
        {
            string html = @"
<tr>
    <th colspan=""6""></th>
    <th colspan=""2"" style=""white-space:nowrap"">{#Current}</th>
    <th colspan=""2"" style=""white-space:nowrap"">{#OffTime}</th>
    <th colspan=""6""></th>
</tr>
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th style=""white-space:nowrap"">{#EqGroup}</th>
    <th style=""white-space:nowrap"">{#ProductionType}</th>
    <th><div style=""width:60px;"">{#Eff}</div></th>
    <th style=""white-space:nowrap"">{#Mould}</th>
    <th><div style=""width:60px;"">{#CycleTime}</div></th>  
    <th><div style=""width:60px;"">{#Status}</div></th>
    <th>{#RunTime}</th>
    <th><div style=""width:60px;"">{#Stops}</div></th>
    <th>{#StopTime}</th>
    <th><div style=""width:60px;"">{#kWH}</div></th>
    <th style=""white-space:nowrap"">{#Operator}</th>
    <th style=""white-space:nowrap"">{#Location}</th>
    <th style=""white-space:nowrap"">{#Department}</th>
    <th style=""white-space:nowrap"">{#SubDepartment}</th>
    <th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
            ";

            html = html.Replace("{#Current}", App.CurrentTranslation["ProductionDetails_Current"]);
            html = html.Replace("{#OffTime}", App.CurrentTranslation["ProductionDetails_OffTime"]);
            html = html.Replace("{#EqGroup}", App.CurrentTranslation["ProductionDetails_EqGroup"]);
            html = html.Replace("{#ProductionType}", App.CurrentTranslation["ProductionDetails_ProductionType"]);
            html = html.Replace("{#Eff}", App.CurrentTranslation["ProductionDetails_Eff"]);
            html = html.Replace("{#Mould}", App.CurrentTranslation["ProductionDetails_Mould"]);
            html = html.Replace("{#CycleTime}", App.CurrentTranslation["ProductionDetails_CycleTime"]);
            html = html.Replace("{#Status}", App.CurrentTranslation["ProductionDetails_Status"]);
            html = html.Replace("{#RunTime}", App.CurrentTranslation["ProductionDetails_RunTime"]);
            html = html.Replace("{#Stops}", App.CurrentTranslation["ProductionDetails_Stops"]);
            html = html.Replace("{#StopTime}", App.CurrentTranslation["ProductionDetails_StopTime"]);
            html = html.Replace("{#kWH}", App.CurrentTranslation["ProductionDetails_kWH"]);
            html = html.Replace("{#Operator}", App.CurrentTranslation["ProductionDetails_Operator"]);
            html = html.Replace("{#Location}", App.CurrentTranslation["ProductionDetails_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["ProductionDetails_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["ProductionDetails_SubDepartment"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["ProductionDetails_SystemData"]);

            return html;
        }

        public static string ProductionDetail_CNC_TableHeader()
        {
            string html = @"
<tr>
    <th colspan=""4""></th>
    <th colspan=""2"" style=""white-space:nowrap"">{#Current}</th>
    <th colspan=""2"" style=""white-space:nowrap"">{#OffTime}</th>
    <th colspan=""6""></th>
</tr>
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th style=""white-space:nowrap"">{#EqGroup}</th>
    <th><div style=""width:60px;"">{#Eff}</div></th>
    <th><div style=""width:60px;"">{#Product}</div></th>
    <th><div style=""width:60px;"">{#Status}</div></th>
    <th>{#RunTime}</th>
    <th><div style=""width:60px;"">{#Stops}</div></th>
    <th>{#StopTime}</th>
    <th><div style=""width:60px;"">{#kWH}</div></th>
    <th style=""white-space:nowrap"">{#Operator}</th>
    <th style=""white-space:nowrap"">{#Location}</th>
    <th style=""white-space:nowrap"">{#Department}</th>
    <th style=""white-space:nowrap"">{#SubDepartment}</th>
    <th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
            ";

            html = html.Replace("{#Current}", App.CurrentTranslation["ProductionDetails_Current"]);
            html = html.Replace("{#OffTime}", App.CurrentTranslation["ProductionDetails_OffTime"]);
            html = html.Replace("{#EqGroup}", App.CurrentTranslation["ProductionDetails_EqGroup"]);
            html = html.Replace("{#Eff}", App.CurrentTranslation["ProductionDetails_Eff"]);
            html = html.Replace("{#Product}", App.CurrentTranslation["ProductionDetails_Product"]);
            html = html.Replace("{#Status}", App.CurrentTranslation["ProductionDetails_Status"]);
            html = html.Replace("{#RunTime}", App.CurrentTranslation["ProductionDetails_RunTime"]);
            html = html.Replace("{#Stops}", App.CurrentTranslation["ProductionDetails_Stops"]);
            html = html.Replace("{#StopTime}", App.CurrentTranslation["ProductionDetails_StopTime"]);
            html = html.Replace("{#kWH}", App.CurrentTranslation["ProductionDetails_kWH"]);
            html = html.Replace("{#Operator}", App.CurrentTranslation["ProductionDetails_Operator"]);
            html = html.Replace("{#Location}", App.CurrentTranslation["ProductionDetails_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["ProductionDetails_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["ProductionDetails_SubDepartment"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["ProductionDetails_SystemData"]);

            return html;
        }

        public static string ProductionDetail_Welding_TableHeader()
        {
            string html = @"
<tr>
    <th colspan=""6""></th>
    <th colspan=""2"" style=""white-space:nowrap"">{#Current}</th>
    <th colspan=""2"" style=""white-space:nowrap"">{#OffTime}</th>
    <th colspan=""6""></th>
</tr>
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th style=""white-space:nowrap"">{#EqGroup}</th>
    <th style=""white-space:nowrap"">{#Product}</th>
    <th><div style=""width:60px;"">{#AverageWelds}</div></th>
    <th><div style=""width:60px;"">{#AverageTime}</div></th>
    <th><div style=""width:60px;"">{#Eff}</div></th>
    <th><div style=""width:60px;"">{#Status}</div></th>
    <th>{#RunTime}</th>
    <th><div style=""width:60px;"">{#Stops}</div></th>
    <th>{#StopTime}</th>
    <th><div style=""width:60px;"">{#kWH}</div></th>
    <th style=""white-space:nowrap"">{#Operator}</th>
    <th style=""white-space:nowrap"">{#Location}</th>
    <th style=""white-space:nowrap"">{#Department}</th>
    <th style=""white-space:nowrap"">{#SubDepartment}</th>
    <th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
            ";

            html = html.Replace("{#Current}", App.CurrentTranslation["ProductionDetails_Current"]);
            html = html.Replace("{#OffTime}", App.CurrentTranslation["ProductionDetails_OffTime"]);
            html = html.Replace("{#EqGroup}", App.CurrentTranslation["ProductionDetails_EqGroup"]);
            html = html.Replace("{#Product}", App.CurrentTranslation["ProductionDetails_Product"]);
            html = html.Replace("{#AverageWelds}", App.CurrentTranslation["ProductionDetails_AverageWelds"]);
            html = html.Replace("{#AverageTime}", App.CurrentTranslation["ProductionDetails_AverageTime"]);
            html = html.Replace("{#Eff}", App.CurrentTranslation["ProductionDetails_Eff"]);
            html = html.Replace("{#Status}", App.CurrentTranslation["ProductionDetails_Status"]);
            html = html.Replace("{#RunTime}", App.CurrentTranslation["ProductionDetails_RunTime"]);
            html = html.Replace("{#Stops}", App.CurrentTranslation["ProductionDetails_Stops"]);
            html = html.Replace("{#StopTime}", App.CurrentTranslation["ProductionDetails_StopTime"]);
            html = html.Replace("{#kWH}", App.CurrentTranslation["ProductionDetails_kWH"]);
            html = html.Replace("{#Operator}", App.CurrentTranslation["ProductionDetails_Operator"]);
            html = html.Replace("{#Location}", App.CurrentTranslation["ProductionDetails_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["ProductionDetails_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["ProductionDetails_SubDepartment"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["ProductionDetails_SystemData"]);

            return html;
        }

        public static string ProductionDetail_EDMWireCut_TableHeader()
        {
            string html = @"
<tr>
    <th colspan=""4""></th>
    <th colspan=""2"" style=""white-space:nowrap"">{#Current}</th>
    <th colspan=""2"" style=""white-space:nowrap"">{#OffTime}</th>
    <th colspan=""7""></th>
</tr>
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th style=""white-space:nowrap"">{#EqGroup}</th>
    <th><div style=""width:60px;"">{#Eff}</div></th>
    <th><div style=""width:60px;"">{#Product}</div></th>
    <th><div style=""width:60px;"">{#Status}</div></th>
    <th>{#RunTime}</th>
    <th><div style=""width:60px;"">{#Stops}</div></th>
    <th>{#StopTime}</th>
    <th><div style=""width:60px;"">{#kWH}</div></th>
    <th style=""white-space:nowrap"">{#Operator}</th>
    <th style=""white-space:nowrap"">{#Location}</th>
    <th style=""white-space:nowrap"">{#Department}</th>
    <th style=""white-space:nowrap"">{#SubDepartment}</th>
    <th style=""white-space:nowrap"">{#WireUseUnit}</th>
    <th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
            ";

            html = html.Replace("{#Current}", App.CurrentTranslation["ProductionDetails_Current"]);
            html = html.Replace("{#OffTime}", App.CurrentTranslation["ProductionDetails_OffTime"]);
            html = html.Replace("{#EqGroup}", App.CurrentTranslation["ProductionDetails_EqGroup"]);
            html = html.Replace("{#Eff}", App.CurrentTranslation["ProductionDetails_Eff"]);
            html = html.Replace("{#Product}", App.CurrentTranslation["ProductionDetails_Product"]);
            html = html.Replace("{#Status}", App.CurrentTranslation["ProductionDetails_Status"]);
            html = html.Replace("{#RunTime}", App.CurrentTranslation["ProductionDetails_RunTime"]);
            html = html.Replace("{#Stops}", App.CurrentTranslation["ProductionDetails_Stops"]);
            html = html.Replace("{#StopTime}", App.CurrentTranslation["ProductionDetails_StopTime"]);
            html = html.Replace("{#kWH}", App.CurrentTranslation["ProductionDetails_kWH"]);
            html = html.Replace("{#Operator}", App.CurrentTranslation["ProductionDetails_Operator"]);
            html = html.Replace("{#Location}", App.CurrentTranslation["ProductionDetails_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["ProductionDetails_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["ProductionDetails_SubDepartment"]);
            html = html.Replace("{#WireUseUnit}", App.CurrentTranslation["ProductionDetails_WireUseUnit"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["ProductionDetails_SystemData"]);

            return html;
        }

        #endregion

        #region ProductionPage Contents

        public static string Production_Equipment_TableContent(IEnumerable<ProductionGeneral> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (ProductionGeneral el in source)
                {
                    string template = @"
<tr>
    <td style=""display:none"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td class=""right-text"">{#2}</td>
	<td class=""right-text"">{#3}</td>
    <td class=""right-text"">{#4}</td>
</tr>";
                    template = template.Replace("{#0}", el.EquipmentType.ToString() + "#" + el.EquipmentTypeName + "#" + el.DescCH);
                    template = template.Replace("{#1}", el.EquipmentTypeName);
                    template = template.Replace("{#2}", el.Efficiency != null ? el.Efficiency.ToString() + "%" : "");
                    template = template.Replace("{#3}", el.On.ToString());
                    template = template.Replace("{#4}", el.Off.ToString());

                    html += template;
                }

            }
            return html;
        }

        public static string ProductionDetail_Moulding_TableContent(IEnumerable<ProductionDetail> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (ProductionDetail el in source)
                {
                    string template = @"
<tr>
    <td class=""right-text"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td style=""white-space:nowrap"">{#2}</td>
	<td class=""right-text"">{#3}</td>
	<td style=""white-space:nowrap"">{#4}</td>
	<td class=""right-text"">{#5}</td>
    <td class=""center-text"">{#6}</td>
	<td class=""center-text"">{#7}</td>
	<td class=""right-text"">{#8}</td>
	<td class=""center-text"">{#9}</td>
    <td class=""right-text"">{#10}</td>
    <td style=""white-space:nowrap"">{#11}</td>
    <td style=""white-space:nowrap"">{#12}</td>
    <td style=""white-space:nowrap"">{#13}</td>
    <td style=""white-space:nowrap"">{#14}</td>
    <td class=""right-text"">{#15}</td>
</tr>";
                    template = template.Replace("{#0}", el.MachineNumber);
                    template = template.Replace("{#1}", el.MachineGroupName);
                    template = template.Replace("{#2}", el.ProductionType);
                    template = template.Replace("{#3}", el.Efficiency != null ? el.Efficiency.ToString() + "%" : "");
                    template = template.Replace("{#4}", el.SpecCode);
                    template = template.Replace("{#5}", el.CycleTime.ToString());
                    template = template.Replace("{#6}", el.Status);
                    template = template.Replace("{#7}", el.Runtime);
                    template = template.Replace("{#8}", el.CurrentStop.ToString());
                    template = template.Replace("{#9}", el.TotalStop);
                    template = template.Replace("{#10}", el.Kwh.ToString());
                    template = template.Replace("{#11}", el.Operator);
                    template = template.Replace("{#12}", el.Location);
                    template = template.Replace("{#13}", el.Department);
                    template = template.Replace("{#14}", el.SubDepartment);
                    template = template.Replace("{#15}", el.SystemData != null ? el.SystemData + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        public static string ProductionDetail_CNC_TableContent(IEnumerable<ProductionDetail> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (ProductionDetail el in source)
                {
                    string template = @"
<tr>
    <td class=""right-text"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td class=""right-text"">{#2}</td>
    <td style=""white-space:nowrap"">{#5}</td>
    <td class=""center-text"">{#6}</td>
	<td class=""center-text"">{#7}</td>
	<td class=""right-text"">{#8}</td>
	<td class=""center-text"">{#9}</td>
    <td class=""right-text"">{#10}</td>
    <td style=""white-space:nowrap"">{#11}</td>
    <td style=""white-space:nowrap"">{#12}</td>
    <td style=""white-space:nowrap"">{#13}</td>
    <td style=""white-space:nowrap"">{#14}</td>
    <td class=""right-text"">{#15}</td>
</tr>";
                    template = template.Replace("{#0}", el.MachineNumber);
                    template = template.Replace("{#1}", el.MachineGroupName);
                    template = template.Replace("{#2}", el.Efficiency != null ? el.Efficiency.ToString() + "%" : "");
                    template = template.Replace("{#5}", el.SpecCode);
                    template = template.Replace("{#6}", el.Status);
                    template = template.Replace("{#7}", el.Runtime);
                    template = template.Replace("{#8}", el.CurrentStop.ToString());
                    template = template.Replace("{#9}", el.TotalStop);
                    template = template.Replace("{#10}", el.Kwh.ToString());
                    template = template.Replace("{#11}", el.Operator);
                    template = template.Replace("{#12}", el.Location);
                    template = template.Replace("{#13}", el.Department);
                    template = template.Replace("{#14}", el.SubDepartment);
                    template = template.Replace("{#15}", el.SystemData != null ? el.SystemData + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        public static string ProductionDetail_Welding_TableContent(IEnumerable<ProductionDetail> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (ProductionDetail el in source)
                {
                    string template = @"
<tr>
    <td class=""right-text"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td style=""white-space:nowrap"">{#2}</td>
	<td class=""right-text"">{#3}</td>
	<td style=""white-space:nowrap"">{#4}</td>
	<td class=""right-text"">{#5}</td>
    <td class=""center-text"">{#6}</td>
	<td class=""center-text"">{#7}</td>
	<td class=""right-text"">{#8}</td>
	<td class=""center-text"">{#9}</td>
    <td class=""right-text"">{#10}</td>
    <td style=""white-space:nowrap"">{#11}</td>
    <td style=""white-space:nowrap"">{#12}</td>
    <td style=""white-space:nowrap"">{#13}</td>
    <td style=""white-space:nowrap"">{#14}</td>
    <td class=""right-text"">{#15}</td>
</tr>";
                    template = template.Replace("{#0}", el.MachineNumber);
                    template = template.Replace("{#1}", el.MachineGroupName);
                    template = template.Replace("{#2}", el.SpecCode);
                    template = template.Replace("{#3}", el.AverageWeld.ToString());
                    template = template.Replace("{#4}", el.AverageWeldTime.ToString());
                    template = template.Replace("{#5}", el.Efficiency != null ? el.Efficiency.ToString() + "%" : "");
                    template = template.Replace("{#6}", el.Status);
                    template = template.Replace("{#7}", el.Runtime);
                    template = template.Replace("{#8}", el.CurrentStop.ToString());
                    template = template.Replace("{#9}", el.TotalStop);
                    template = template.Replace("{#10}", el.Kwh.ToString());
                    template = template.Replace("{#11}", el.Operator);
                    template = template.Replace("{#12}", el.Location);
                    template = template.Replace("{#13}", el.Department);
                    template = template.Replace("{#14}", el.SubDepartment);
                    template = template.Replace("{#15}", el.SystemData != null ? el.SystemData + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        public static string ProductionDetail_EDMWireCut_TableContent(IEnumerable<ProductionDetail> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (ProductionDetail el in source)
                {
                    string template = @"
<tr>
    <td class=""right-text"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td class=""right-text"">{#2}</td>
    <td style=""white-space:nowrap"">{#5}</td>
    <td class=""center-text"">{#6}</td>
	<td class=""center-text"">{#7}</td>
	<td class=""right-text"">{#8}</td>
	<td class=""center-text"">{#9}</td>
    <td class=""right-text"">{#10}</td>
    <td style=""white-space:nowrap"">{#11}</td>
    <td style=""white-space:nowrap"">{#12}</td>
    <td style=""white-space:nowrap"">{#13}</td>
    <td style=""white-space:nowrap"">{#14}</td>
    <td style=""white-space:nowrap"">{#15}</td>
    <td class=""right-text"">{#16}</td>
</tr>";
                    template = template.Replace("{#0}", el.MachineNumber);
                    template = template.Replace("{#1}", el.MachineGroupName);
                    template = template.Replace("{#2}", el.Efficiency != null ? el.Efficiency.ToString() + "%" : "");
                    template = template.Replace("{#5}", el.SpecCode);
                    template = template.Replace("{#6}", el.Status);
                    template = template.Replace("{#7}", el.Runtime);
                    template = template.Replace("{#8}", el.CurrentStop.ToString());
                    template = template.Replace("{#9}", el.TotalStop);
                    template = template.Replace("{#10}", el.Kwh.ToString());
                    template = template.Replace("{#11}", el.Operator);
                    template = template.Replace("{#12}", el.Location);
                    template = template.Replace("{#13}", el.Department);
                    template = template.Replace("{#14}", el.SubDepartment);
                    template = template.Replace("{#15}", el.WireUnit);
                    template = template.Replace("{#16}", el.SystemData != null ? el.SystemData + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        #endregion

        #region AuxiliaryEquipmentPage Headers

        public static string AuxiliaryEquipment_Type_TableHeader()
        {
            string html = @"
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">{#Equipment}</th>
    <th><div style=""width:60px;"">{#On}</div></th>
    <th><div style=""width:60px;"">{#QTY}</div></th>
</tr>
            ";

            html = html.Replace("{#Equipment}", App.CurrentTranslation["AuxiliaryEquipment_Equipment"]);
            html = html.Replace("{#On}", App.CurrentTranslation["AuxiliaryEquipment_On"]);
            html = html.Replace("{#QTY}", App.CurrentTranslation["AuxiliaryEquipment_QTY"]);

            return html;
        }

        public static string AuxiliaryEquipment_Details_TableHeader()
        {
            string html = @"
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th style=""white-space:nowrap"">{#Group}</th>
    <th><div style=""width:60px;"">{#On}</div></th>
    <th style=""white-space:nowrap"">{#Location}</th>
    <th style=""white-space:nowrap"">{#Department}</th>
    <th style=""white-space:nowrap"">{#SubDepartment}</th>
    <th style=""white-space:nowrap"">{#SystemData}</th>
</tr>
            ";

            html = html.Replace("{#Group}", App.CurrentTranslation["AuxiliaryEquipmentDetails_Group"]);
            html = html.Replace("{#On}", App.CurrentTranslation["AuxiliaryEquipment_On"]);
            html = html.Replace("{#Location}", App.CurrentTranslation["AuxiliaryEquipmentDetails_Location"]);
            html = html.Replace("{#Department}", App.CurrentTranslation["AuxiliaryEquipmentDetails_Department"]);
            html = html.Replace("{#SubDepartment}", App.CurrentTranslation["AuxiliaryEquipmentDetails_SubDepartment"]);
            html = html.Replace("{#SystemData}", App.CurrentTranslation["AuxiliaryEquipmentDetails_SystemData"]);

            return html;
        }

        #endregion

        #region AuxiliaryEquipmentPage Contents

        public static string AuxiliaryEquipment_Type_TableContent(IEnumerable<AuxiliaryType> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (AuxiliaryType el in source)
                {
                    string template = @"
<tr>
    <td style=""display:none"">{#0}</td>
	<td style=""white-space:nowrap"">{#1}</td>
	<td class=""right-text"">{#2}</td>
	<td class=""right-text"">{#3}</td>
</tr>";
                    template = template.Replace("{#0}", el.EquipmentType.ToString());
                    template = template.Replace("{#1}", el.EquipmentTypeName);
                    template = template.Replace("{#2}", el.On != null ? el.On.ToString() + "%" : "");
                    template = template.Replace("{#3}", el.Qty.ToString());

                    html += template;
                }

            }
            return html;
        }

        public static string AuxiliaryEquipment_Details_TableContent(IEnumerable<AuxiliaryEquipment> source)
        {
            string html = string.Empty;
            if (source != null)
            {
                foreach (AuxiliaryEquipment el in source)
                {
                    string template = @"
<tr>
    <td class=""right-text"">{#0}</td>
	<td style=""white-space:nowrap"">{#3}</td>
	<td class=""right-text"">{#4}</td>
	<td style=""white-space:nowrap"">{#5}</td>
    <td style=""white-space:nowrap"">{#6}</td>
    <td style=""white-space:nowrap"">{#7}</td>
    <td class=""right-text"">{#8}</td>
</tr>";
                    template = template.Replace("{#0}", el.MachineNumber.ToString());
                    template = template.Replace("{#3}", el.EquipmentGroup);
                    template = template.Replace("{#4}", el.On != null ? el.On.ToString() + "%" : "");
                    template = template.Replace("{#5}", el.Location);
                    template = template.Replace("{#6}", el.Department);
                    template = template.Replace("{#7}", el.SubDepartment);
                    template = template.Replace("{#8}", el.SystemData != null ? el.SystemData + "%" : "");

                    html += template;
                }

            }
            return html;
        }

        #endregion

        public static string InsertHeaderAndBodyToHtmlTable(string headerHtml, string bodyHtml)
        {
            StringBuilder html = new StringBuilder(HtmlTableMarkup);
            html = html.Replace("{#TableHeader}", headerHtml);
            html = html.Replace("{#TableBody}", bodyHtml);

            return html.ToString();
        }
    }
}
