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
    public partial class EditSupportBuyProduct : System.Web.UI.Page
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
                var news = SupportBuyProductController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtSupportName.Text = news.SupportName;
                    txtPhone.Text = news.SupportPhone;
                    txtEmail.Text = news.SupportEmail;
                    pSupportIndex.Value = news.SupportIndex;
                    ddlSupportPlace.SelectedValue = news.SupportPlace.ToString();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            string BackLink = "/manager/SupportBuyProductList.aspx";
            int ID = ViewState["NID"].ToString().ToInt(0);
            string kq = SupportBuyProductController.Update(ID, txtSupportName.Text, txtPhone.Text, txtEmail.Text, ddlSupportPlace.SelectedValue.ToInt(),
               Convert.ToInt32(pSupportIndex.Value), DateTime.Now, Username);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "e", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Cập nhật. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}