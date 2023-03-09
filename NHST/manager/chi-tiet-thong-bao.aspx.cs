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
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class chi_tiet_thong_bao : System.Web.UI.Page
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
                var news = SendNotiEmailController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtNotiName.Text = news.NotiName;

                    IsSentNotiAdmin.Checked = Convert.ToBoolean(news.IsSentNotiAdmin);
                    IsSentNotiUser.Checked = Convert.ToBoolean(news.IsSentNotiUser);
                    IsSentEmailAdmin.Checked = Convert.ToBoolean(news.IsSentEmailAdmin);
                    IsSendEmailUser.Checked = Convert.ToBoolean(news.IsSendEmailUser);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();

            int ID = ViewState["NID"].ToString().ToInt(0);

            string BackLink = "/manager/thiet-lap-thong-bao.aspx";
            bool NotiAdmin = Convert.ToBoolean(IsSentNotiAdmin.Checked);
            bool NotiUser = Convert.ToBoolean(IsSentNotiUser.Checked);
            bool EmailAdmin = Convert.ToBoolean(IsSentEmailAdmin.Checked);
            bool EmailUser = Convert.ToBoolean(IsSendEmailUser.Checked);
            SendNotiEmailController.Update(ID, NotiAdmin, NotiUser, EmailAdmin, EmailUser);
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
            //
            //if (kq.ToInt(0) > 0)
            //{
            //    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
            //}
            //else
            //{
            //    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Cập nhật. Vui lòng thử lại.", "e", true, Page);
            //}
        }
    }
}