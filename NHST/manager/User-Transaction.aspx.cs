using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using MB.Extensions;


namespace NHST.manager
{
    public partial class User_Transaction : System.Web.UI.Page
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
                    if (ac.RoleID == 0 || ac.RoleID == 7 || ac.RoleID == 6 || ac.RoleID == 2)
                        LoadData();
                    else Response.Redirect("/trang-chu");
                }
            }
        }
        public void LoadData()
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            int UID = Request.QueryString["i"].ToInt();
            var a = AccountController.GetByID(UID);
            if (a.SaleID == ac.ID || ac.RoleID  == 0 || ac.RoleID == 7 || ac.RoleID == 2 || a.DathangID == ac.ID)
            {
                if (a != null)
                {
                    lblUsername.Text = a.Username;
                    lblWallet.Text = string.Format("{0:N0}", a.Wallet) + " VNĐ";
                }
            }
            else Response.Redirect("/manager/saler-customer-list");
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            int UID = Request.QueryString["i"].ToInt();
            var a = AccountController.GetByID(UID);
            if (a.SaleID == ac.ID || ac.RoleID == 0 || ac.RoleID == 7 || ac.RoleID == 2 || a.DathangID == ac.ID)
            {
                var listhist = HistoryPayWalletController.GetByUID(UID);
                gr.DataSource = listhist;
            }
            else Response.Redirect("/manager/saler-customer-list");

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
            gr.Rebind();
        }
        #endregion
    }
}