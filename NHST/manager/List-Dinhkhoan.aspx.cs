using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System.Text;
using Telerik.Web.UI;
namespace NHST.manager
{
    public partial class List_Dinhkhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7 && obj_user.RoleID != 6)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
             
            }

        }
       
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            //var acc = Session["userLoginSystem"].ToString();
            LoadGrid();
            gr.Rebind();

        }

        public void LoadGrid()
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
           
            var la = DinhkhoanController.GetAll(tSearchName.Text.Trim());

            if (la.Count > 0)
            {
                gr.DataSource = la;

            }
        }

        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadGrid();
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid();
            gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            gr.Rebind();
        }
        #endregion
    }
}