using MB.Extensions;
using NHST.Bussiness;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Controllers;

namespace NHST.manager
{
    public partial class AddWeightPrice : System.Web.UI.Page
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

            string id = WeightController.Insert(Convert.ToDouble(pWeightFrom.Value), Convert.ToDouble(pWeightTo.Value),
                ddlType.SelectedValue.ToInt(), ddlfs.SelectedValue.ToInt(), Convert.ToDouble(pVip1.Value), Convert.ToDouble(pVip2.Value),
                Convert.ToDouble(pVip3.Value), Convert.ToDouble(pVip4.Value), Convert.ToDouble(pVip5.Value), Convert.ToDouble(pVip6.Value),
                DateTime.Now, Username);
            int UID = Convert.ToInt32(id);
            if (UID > 0)
            {
                PJUtils.ShowMsg("Tạo phí thành công.", true, Page);
            }
        }
    }
}