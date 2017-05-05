using MBoxMobile.Models;
using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class HtmlTableSupport
    {
        private static string HtmlTableMarkup = @"
<!DOCTYPE html>
<html>
<head>
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
table tbody tr:nth-child(even) {
    background-color: #f1f4f7;
}
table tbody tr:nth-child(odd) {
   background-color:#e9eaec;
}
table th {
    background-color: white;
    color: gray;
}
.left-text {
	text-align: left;
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
    <tbody>
        [#TableBody]
    </tbody>
</table>

</body>
</html>";

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
    <th>Locations</th>
    <th style=""width:60px;"">[#Uptime]</th>
    <th style=""width:60px;"">[#On]</th>
    <th style=""width:60px;"">[#Off]</th>
    <th style=""width:60px;"">[#Errors]</th>
</tr>
            ";

            html = html.Replace("[#Equipment]", App.CurrentTranslation["Uptime_LocationsTableEquipment"]);
            html = html.Replace("[#Uptime]", App.CurrentTranslation["Uptime_LocationsTableUptime"]);
            html = html.Replace("[#On]", App.CurrentTranslation["Uptime_LocationsTableOn"]);
            html = html.Replace("[#Off]", App.CurrentTranslation["Uptime_LocationsTableOff"]);
            html = html.Replace("[#Errors]", App.CurrentTranslation["Uptime_LocationsTableErrors"]);

            return html;
        }

        #endregion

        #region UptimePage Bodies

        public static string Uptime_Locations_TableBody(List<EfficiencyLocation> source)
        {
            string html = string.Empty;
            foreach (EfficiencyLocation el in source)
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
                template = template.Replace("{#2}", el.EfficiencyPercent.ToString() + "%");
                template = template.Replace("{#3}", el.On.ToString());
                template = template.Replace("{#4}", el.Off.ToString());
                template = template.Replace("{#5}", el.Errors.ToString());

                html += template;
            }

            return html;
        }

        #endregion

        public static string InsertHeaderAndBodyToHtmlTable(string headerHtml, string bodyHtml)
        {
            HtmlTableMarkup = HtmlTableMarkup.Replace("[#TableHeader]", headerHtml);
            HtmlTableMarkup = HtmlTableMarkup.Replace("[#TableBody]", bodyHtml);

            return HtmlTableMarkup;
        }
    }
}
