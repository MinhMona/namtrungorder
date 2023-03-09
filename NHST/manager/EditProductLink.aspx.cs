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
    public partial class EditProductLink : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                    LoadDLL();
                    LoadData();
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
        public void LoadData()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = ProductLinkController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtSitename.Text = news.ProductName;
                    txtProductLink.Text = news.ProductLink;
                    chkIshidden.Checked = Convert.ToBoolean(news.IsHidden);
                    ddlPageType.SelectedValue = news.ProductCategoryID.ToString();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();

            int NewsID = ViewState["NID"].ToString().ToInt(0);
            DateTime currentDate = DateTime.Now;
            var news = ProductLinkController.GetByID(NewsID);
            if (news != null)
            {
                string kq = ProductLinkController.Update(NewsID, ddlPageType.SelectedValue.ToInt(0), txtSitename.Text, txtProductLink.Text, chkIshidden.Checked, currentDate, Email);
                if (kq == "ok")
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật danh mục. Vui lòng thử lại.", "e", true, Page);
                }
            }
        }
    }
}