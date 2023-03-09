using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class nap_tien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            var page = PageController.GetByID(37);
            if(page!=null)
            {
                ltrInfo.Text = page.PageContent;
            }
            string username = Session["userLoginSystem"].ToString();
            string html = "";
            html += username;

            var user = AccountController.GetByUsername(username);
            if (user != null)
            {
                lblAccount.Text = string.Format("{0:N0}", user.Wallet) + " vnđ";
                var userinfo = AccountInfoController.GetByUserID(user.ID);
                //html += userinfo.Phone;
            }
            //lblIDUser.Text = html;
        }
    }
}