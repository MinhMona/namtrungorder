using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using MB.Extensions;

namespace NHST.manager
{
    public partial class RequestRechargeCYN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID == 1 || ac.RoleID == 3)
                        Response.Redirect("/trang-chu");
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string s = Request.QueryString["s"];
            int status = Request.QueryString["status"].ToInt(0);
            tSearchName.Text = s;
            ddlStatus.SelectedValue = status.ToString();
            var la = WithdrawController.GetAllByType(tSearchName.Text, 3);
            if (la != null)
            {
                if (la.Count > 0)
                {
                    if (status > 0)
                        la = la.Where(l => l.Status == status).ToList();
                    gr.DataSource = la;
                }
            }

        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion
        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //gr.Rebind();
            Response.Redirect("/manager/requestrechargeCYN.aspx?s=" + tSearchName.Text.Trim() + "&status=" + ddlStatus.SelectedValue + "");
        }
        #endregion
    }
}