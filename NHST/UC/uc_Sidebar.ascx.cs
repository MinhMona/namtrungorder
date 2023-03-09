using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST.UC
{
    public partial class uc_Sidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }
        public void loadData()
        {
            var listpagetype = PageTypeController.GetAll();
            if (listpagetype.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                foreach (var t in listpagetype)
                {
                    html.Append("<div class=\"new-write\">");
                    html.Append("<div class=\"sidebar-img\"><img src=\"/App_Themes/pdv/assets/images/sv-icon.png\" alt=\"#\"></div>");
                    html.Append("<div class=\"sidebar-info\"><p><a href=\"" + t.NodeAliasPath + "\">" + t.PageTypeName + "</a></p></div>");
                    html.Append("</div>");
                }
                ltrCategory.Text = html.ToString();
            }

            var ListPages = PageController.GetAll("");
            var lps = ListPages.Take(6).ToList();
            if (lps.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                foreach (var p in lps)
                {
                    int pagetypeid = Convert.ToInt32(p.PageTypeID);
                    html.Append("<div class=\"new-write\">");
                    html.Append("<div class=\"sidebar-img\"><img src=\"/App_Themes/pdv/assets/images/sv-icon.png\" alt=\"#\"></div>");
                    html.Append("<div class=\"sidebar-info\"><p><a href=\"" + p.NodeAliasPath + "\">" + p.Title + "</a></p></div>");
                    html.Append("</div>");
                }
                ltrList.Text = html.ToString();
            }
        }
    }
}