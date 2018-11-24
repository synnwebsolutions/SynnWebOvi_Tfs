using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;

namespace SynnWebOvi
{
    internal static class ThemeHelper
    {
        public static IDatabaseProvider DBController = SynnDataProvider.DbProvider;

        internal static void Apply(SynnWebFormBase page)
        {
            string key = "ServerControlScript";
            //DBController.DbLog.Add(new ThemeScript
            //{
            //    CssAttribute = "background-color",
            //    CssValue = "#007BFF",
            //    ElementIdentifier = "body"
            //});
            //List<ThemeScript> scripyts = DBController.DbLog.GetThemes(new ThemeSearchParameters { });
            //foreach (var themeSc in scripyts)
            //    ScriptManager.RegisterStartupScript(page, page.GetType(), key, themeSc.ToString(), true);
        
            //string scriptContent = @"$('body').css('background-image', 'url(@https://images.pexels.com/photos/1292838/pexels-photo-1292838.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940)')";
        }

     

    }

    public class ThemeScript : IDbLoadable
    {
        public string ElementIdentifier { get; set; }
        public string CssAttribute { get; set; }

        public string CssValue { get; set; }
        public int Id { get; set; }
        public bool IsDummy { get;  set; }

        public string ScriptText { get { return ToString(); } }
        public ThemeScript()
        {

        }
        public ThemeScript(IDataReader data)
        {
            Load(data);
        }
        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            ElementIdentifier = DataAccessUtility.LoadNullable<string>(reader, "ElementIdentifier");
            CssAttribute = DataAccessUtility.LoadNullable<string>(reader, "CssAttribute");
            CssValue = DataAccessUtility.LoadNullable<string>(reader, "CssValue");
        }
        public override string ToString()
        {
            return string.Format("$('{0}').css('{1}', '{2}')", ElementIdentifier, CssAttribute,CssValue);
        }
    }
}