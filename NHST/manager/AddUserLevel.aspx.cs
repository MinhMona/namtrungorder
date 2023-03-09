using MB.Extensions;
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

namespace NHST.manager
{
    public partial class AddUserLevel : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();

            string id = UserLevelController.Insert(txtLevelName.Text.Trim(), Convert.ToDouble(pFeeBuyPro.Value), Convert.ToDouble(pFeeWeight.Value),
                Convert.ToDouble(pLessDeposit.Value), 1, DateTime.Now, Username);
            int UID = Convert.ToInt32(id);
            string BackLink = "/manager/User-Level.aspx";
            if (UID > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo cấp người dùng thành công.", "s", true, BackLink, Page);
            }
        }
    }
}