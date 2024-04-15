using CMS.FormEngine.Web.UI;
using CMS.Helpers;
using Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSApp.CMSModules.Events
{
    public partial class EventLocationFilter : FormEngineUserControl
    {
        public override object Value
        {
            get
            {
                return drpLocations.SelectedValue;
            }
            set
            {
                drpLocations.SelectedValue = ValidationHelper.GetString(value, "");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(!IsPostBack)
            {
                var locations = LocationInfo.Provider.Get().OrderBy("LocationName");

                drpLocations.DataSource = locations;
                drpLocations.DataBind();

                drpLocations.Items.Insert(0, new ListItem("(all)", ""));
            }
        }

        public override string GetWhereCondition()
        {
            var selectedValue = Value as string;

            if(String.IsNullOrEmpty(selectedValue))
            {
                return "";
            }

            return $"(LocationGuid = N'{selectedValue}')";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}