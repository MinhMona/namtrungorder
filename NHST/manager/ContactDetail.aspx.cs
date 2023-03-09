using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;

namespace NHST.manager
{
    public partial class ContactDetail : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
                LoadNews();

            }
        }
        public void LoadNews()
        {
            string username = Session["userLoginSystem"].ToString();
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = ContactController.GetByID(id);
                if (news != null)
                {
                    ContactController.Update(id, true, DateTime.Now, username);
                    txtFullName.Text = news.Fullname;
                    txtEmail.Text = news.Email;
                    txtContent.Text = news.ContactContent;
                }
            }
        }
    }
}