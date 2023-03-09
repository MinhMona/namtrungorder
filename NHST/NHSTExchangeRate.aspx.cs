using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class NHSTExchangeRate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadConfiguration();

        }
        public void LoadConfiguration()
        {
            var c = ConfigurationController.GetByTop1();
            if (c != null)
            {
                Response.Write(c.Currency);
            }
        }
    }
}