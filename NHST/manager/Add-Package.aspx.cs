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
using MB.Extensions;
using System.Text;


namespace NHST.manager
{
    public partial class Add_Package : System.Web.UI.Page
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
                    if (ac != null)
                        if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 2)
                            Response.Redirect("/trang-chu");
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            string code = txtPackageCode.Text.Trim();
            var check = BigPackageController.GetByPackageCode(code);
            string BackLink = "/manager/Add-Package.aspx";
            if (check != null)
            {
                PJUtils.ShowMessageBoxSwAlert("Mã bao hàng đã tồn tại.", "e", false, Page);
            }
            else
            {
                string kq = BigPackageController.Insert(code, pWeight.Value.ToString().ToFloat(0), pVolume.Value.ToString().ToFloat(0), 1, DateTime.Now, username_current);
                if (kq.ToInt(0) > 0)
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo bao hàng thành công", "s", true, BackLink, Page);
                else
                    PJUtils.ShowMessageBoxSwAlert("Lỗi khi tạo bao hàng", "e", true, Page);
            }
        }
    }
}