using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class feedback_detail : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                LoadData();

            }
        }
        public void LoadData()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var f = ContactController.GetByID(id);
                if (f != null)
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    ContactController.Update(id, true, DateTime.Now, username_current);
                    lblFullname.Text = f.Fullname;
                    lblEmail.Text = f.Email;
                    lblPhone.Text = f.Phone;
                    txtContent.Text = f.ContactContent;
                }
            }
        }
    }
}