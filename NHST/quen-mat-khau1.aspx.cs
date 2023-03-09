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

namespace NHST
{
    public partial class quen_mat_khau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btngetpass_Click(object sender, EventArgs e)
        {
            var user = AccountInfoController.GetByEmailFP(txtEmail.Text.Trim());
            if (user != null)
            {
                string password = PJUtils.RandomStringWithText(10);
                //Send Email pass


                //Cập nhật pass mới cho user
                string kq = AccountController.UpdatePassword(Convert.ToInt32(user.UID), password);
                if (kq == "1")
                {
                    try
                    {
                        PJUtils.SendMailGmail("cskh@1688pgs.vn", "1688pegasus", txtEmail.Text.Trim(), "Reset Mật khẩu trên 1688PGS", "Mật khẩu mới của bạn trên hệ thống 1688PGS: <strong>" + password + "</strong>", "");
                    }
                    catch
                    {

                    }
                    PJUtils.ShowMsg("Hệ thống đã gửi 1 email mật khẩu mới cho bạn, vui lòng kiểm tra email và đăng nhập lại.", true, Page);
                    Response.Redirect("/dang-nhap");
                }
            }
            else
            {
                lblError.Text = "Email không tồn tại trong hệ thống.";
                lblError.Visible = true;
            }
        }
    }
}