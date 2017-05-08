using MBoxMobile.Models;
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
        [#TableHeader]
    </thead>
    <tbody id='mbox'>
        [#TableBody]
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
    <th colspan=""3"" style=""white-space:nowrap"">[#Equipment]</th>
</tr>
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">[#Name]</th>
    <th><div style=""width:60px;"">[#Uptime]</div></th>
    <th><div style=""width:60px;"">[#On]</div></th>
    <th><div style=""width:60px;"">[#Off]</div></th>
    <th><div style=""width:60px;"">[#Errors]</div></th>
</tr>
            ";

            html = html.Replace("[#Equipment]", App.CurrentTranslation["Uptime_LocationsTableEquipment"]);
            html = html.Replace("[#Name]", App.CurrentTranslation["Uptime_Locations"]);
            html = html.Replace("[#Uptime]", App.CurrentTranslation["Uptime_Uptime"]);
            html = html.Replace("[#On]", App.CurrentTranslation["Uptime_On"]);
            html = html.Replace("[#Off]", App.CurrentTranslation["Uptime_Off"]);
            html = html.Replace("[#Errors]", App.CurrentTranslation["Uptime_Errors"]);

            return html;
        }

        public static string Uptime_Large_TableHeader(string tableName)
        {
            string html = @"
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">[#Name]</th>
    <th><div style=""width:60px;"">[#Uptime]</div></th>
    <th><div style=""width:60px;"">[#On]</div></th>
    <th><div style=""width:60px;"">[#Off]</div></th>
    <th><div style=""width:60px;"">[#Errors]</div></th>
</tr>
            ";

            switch (tableName)
            {
                case "Department":
                    html = html.Replace("[#Name]", App.CurrentTranslation["Uptime_Departments"]);
                    break;
                case "SubDepartment":
                    html = html.Replace("[#Name]", App.CurrentTranslation["Uptime_SubDepartments"]);
                    break;
                case "Equipment":
                    html = html.Replace("[#Name]", App.CurrentTranslation["Uptime_Equipment"]);
                    break;
                case "EquipmentGroup":
                    html = html.Replace("[#Name]", App.CurrentTranslation["Uptime_EquipmentGroup"]);
                    break;
            }
            html = html.Replace("[#Uptime]", App.CurrentTranslation["Uptime_Uptime"]);
            html = html.Replace("[#On]", App.CurrentTranslation["Uptime_On"]);
            html = html.Replace("[#Off]", App.CurrentTranslation["Uptime_Off"]);
            html = html.Replace("[#Errors]", App.CurrentTranslation["Uptime_Errors"]);

            return html;
        }

        public static string Uptime_Small_TableHeader()
        {
            string html = @"
<tr>
    <th style=""display:none"">Id</th>
    <th style=""white-space:nowrap"">Auxiliary equipment</th>
    <th><div style=""width:60px;"">[#On]</div></th>
</tr>
            ";

            html = html.Replace("[#On]", App.CurrentTranslation["Uptime_On"]);

            return html;
        }

        public static string Uptime_Details_TableHeader()
        {
            string html = @"
<tr>
    <th colspan=""2""></th>
    <th colspan=""2"" style=""white-space:nowrap"">[#Current]</th>
    <th colspan=""2"" style=""white-space:nowrap"">[#OffTime]</th>
    <th colspan=""7""></th>
</tr>
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th><div style=""width:60px;"">[#Uptime]</div></th>
    <th><div style=""width:60px;"">[#Status]</div></th>
    <th>[#RunTime]</th>
    <th><div style=""width:60px;"">[#Stops]</div></th>
    <th>[#StopTime]</th>
    <th style=""white-space:nowrap"">[#Group]</div></th>
    <th style=""white-space:nowrap"">[#Location]</div></th>
    <th style=""white-space:nowrap"">[#Department]</div></th>
    <th style=""white-space:nowrap"">[#SubDepartment]</div></th>
    <th style=""white-space:nowrap"">[#Type]</div></th>
    <th style=""white-space:nowrap"">[#Remark]</div></th>
    <th style=""white-space:nowrap"">[#SystemData]</div></th>
</tr>
            ";

            html = html.Replace("[#Current]", App.CurrentTranslation["UptimeDetails_Current"]);
            html = html.Replace("[#OffTime]", App.CurrentTranslation["UptimeDetails_OffTime"]);
            html = html.Replace("[#Uptime]", App.CurrentTranslation["Uptime_Uptime"]);
            html = html.Replace("[#Status]", App.CurrentTranslation["UptimeDetails_Status"]);
            html = html.Replace("[#RunTime]", App.CurrentTranslation["UptimeDetails_RunTime"]);
            html = html.Replace("[#Stops]", App.CurrentTranslation["UptimeDetails_Stops"]);
            html = html.Replace("[#StopTime]", App.CurrentTranslation["UptimeDetails_StopTime"]);
            html = html.Replace("[#Group]", App.CurrentTranslation["UptimeDetails_Group"]);
            html = html.Replace("[#Location]", App.CurrentTranslation["Uptime_Location"]);
            html = html.Replace("[#Department]", App.CurrentTranslation["UptimeDetails_Department"]);
            html = html.Replace("[#SubDepartment]", App.CurrentTranslation["UptimeDetails_SubDepartment"]);
            html = html.Replace("[#Type]", App.CurrentTranslation["UptimeDetails_Type"]);
            html = html.Replace("[#Remark]", App.CurrentTranslation["UptimeDetails_Remark"]);
            html = html.Replace("[#SystemData]", App.CurrentTranslation["UptimeDetails_SystemData"]);

            return html;
        }

        public static string Uptime_AuxiliaryEquipments_TableHeader()
        {
            string html = @"
<tr>
    <th><div style=""width:60px;"">M#</div></th>
    <th style=""white-space:nowrap"">[#Name]</div></th>
    <th><div style=""width:60px;"">[#Type]</div></th>
    <th style=""white-space:nowrap"">[#Group]</div></th>
    <th><div style=""width:60px;"">[#On]</div></th>
    <th style=""white-space:nowrap"">[#Location]</div></th>
    <th style=""white-space:nowrap"">[#Department]</div></th>
    <th style=""white-space:nowrap"">[#SubDepartment]</div></th>
    <th style=""white-space:nowrap"">[#SystemData]</div></th>
</tr>
            ";

            html = html.Replace("[#Name]", App.CurrentTranslation["UptimeDetails_Name"]);
            html = html.Replace("[#Type]", App.CurrentTranslation["UptimeDetails_Type"]);
            html = html.Replace("[#Group]", App.CurrentTranslation["UptimeDetails_Group"]);
            html = html.Replace("[#On]", App.CurrentTranslation["Uptime_On"]);
            html = html.Replace("[#Location]", App.CurrentTranslation["Uptime_Location"]);
            html = html.Replace("[#Department]", App.CurrentTranslation["UptimeDetails_Department"]);
            html = html.Replace("[#SubDepartment]", App.CurrentTranslation["UptimeDetails_SubDepartment"]);
            html = html.Replace("[#SystemData]", App.CurrentTranslation["UptimeDetails_SystemData"]);

            return html;
        }

        #endregion

        #region UptimePage Bodies

        public static string Uptime_Large_TableBody(IEnumerable<EfficiencyModel> source)
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

        public static string Uptime_Small_TableBody(IEnumerable<EfficiencyModel> source)
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

        public static string Uptime_Details_TableBody(IEnumerable<EfficiencyMachine> source)
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

        public static string Uptime_AuxiliaryEquipments_TableBody(IEnumerable<EfficiencyAuxiliaryEquipment> source)
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

        public static string InsertHeaderAndBodyToHtmlTable(string headerHtml, string bodyHtml)
        {
            StringBuilder html = new StringBuilder(HtmlTableMarkup);
            html = html.Replace("[#TableHeader]", headerHtml);
            html = html.Replace("[#TableBody]", bodyHtml);

            return html.ToString();
        }
    }
}
