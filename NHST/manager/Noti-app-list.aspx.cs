using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class Noti_app_list : System.Web.UI.Page
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

                    if (ac.RoleID == 0 || ac.RoleID == 2)
                    {
                        ltrAddNew.Text += "<a type=\"button\" class=\"btn primary-btn\" href=\"/manager/push-noti-app.aspx\">Thêm thông báo</a>";
                        ltrAddNew.Text += "<a type=\"button\" style=\"margin-left:5px;\" class=\"btn primary-btn\" href=\"/manager/push-noti-app-user.aspx\">Thêm thông báo cá nhân</a>";
                    }
                    else
                    {
                        ltrAddNew.Text = "<a type=\"button\" class=\"btn primary-btn\" href=\"/manager/push-noti-app-user.aspx\">Thêm thông báo cá nhân</a>";
                    }

                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = AppPushNotiController.GetAll();
            if (la != null)
            {
                if (la.Count > 0)
                {
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


        #endregion
    }
}