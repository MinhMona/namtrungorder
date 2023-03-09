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
    public partial class AddProductLink : System.Web.UI.Page
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
                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.RoleID != 0 && ac.RoleID == 2)
                        Response.Redirect("/trang-chu");
                    LoadDLL();
                }
            }
        }
        public void LoadDLL()
        {
            var pt = ProductCategoryController.GetAll("");
            if (pt != null)
            {
                if (pt.Count > 0)
                {
                    ddlPageType.DataSource = pt;
                    ddlPageType.DataBind();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;

            string kq = ProductLinkController.Insert(ddlPageType.SelectedValue.ToInt(0), txtSitename.Text, txtProductLink.Text, chkIshidden.Checked, currentDate, Email);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlert("Tạo mới thành công.", "s", true, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}