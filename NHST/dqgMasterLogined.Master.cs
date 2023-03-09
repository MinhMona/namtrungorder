using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class dqgMasterLogined : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                lblPhone.Text = config.Hotline;
            }
            if (Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    ltrLogin.Text += "<li class=\"it menu-item-has-children\"><a href=\"/thong-tin-nguoi-dung\"><i class=\"m-color fa fa-user-circle\"></i> " + acc.Username + "</a>";
                    ltrLogin.Text += "<ul class=\"sub-menu\">";
                    ltrLogin.Text += "  <li><a href=\"/bang-tich-luy-diem\">Cấp độ tài khoản</a></li>";
                    ltrLogin.Text += "  <li><a href=\"/dang-xuat\">Đăng xuất</a></li>";
                    ltrLogin.Text += "</ul>";
                    ltrLogin.Text += "</li>";
                    if (acc.RoleID != 1)
                    {
                        ltrLogin.Text += "<li class=\"it\"><a href=\"/admin/login.aspx\"><i class=\"m-color fa fa-cog\"></i> Quản trị</a></li>";
                    }
                    ltrLogin.Text += "<li class=\"it\"><a href=\"/lich-su-giao-dich\"><i class=\"m-color fa fa-money\"></i> Số dư: " + string.Format("{0:N0}", acc.Wallet).Replace(",", ".") + " vnđ</a></li>";
                    ltrLogin.Text += "<li class=\"it\"><a href=\"javascript:;\">Tỷ giá: ¥ 1 = " + string.Format("{0:N0}", config.Currency) + " vnđ</a></li>";
                    
                    ltrCart.Text += "<li class=\"it\"><a href=\"/gio-hang\"><i class=\"fa fa-shopping-cart\"></i> GIỎ HÀNG</a></li>";

                    #region phần thông báo       
                    var notis = NotificationController.GetByReceivedID(acc.ID);
                    ltrLogin.Text += "<li class=\"it menu-item-has-children\">";
                    ltrLogin.Text += "  <a href=\"javascript:;\"><i class=\"fa fa-bell\"></i> Thông báo (" + notis.Count + ")</a>";

                    if (notis.Count > 0)
                    {
                        ltrLogin.Text += "  <ul class=\"sub-menu\" style=\"width: 340px;height: 300px;overflow-y: scroll;\">";
                        foreach (var item in notis)
                        {
                            ltrLogin.Text += "          <li><a href=\"javascript:;\" onclick=\"updatestatusnoti('" + item.ID + "','" + acc.Username + "','" + item.OrderID + "')\">" + item.Message + "</a></li>";
                        }
                        ltrLogin.Text += "  </ul>";
                    }



                    ltrLogin.Text += "</li>";
                    #endregion
                }
                pnLoginFooter.Visible = true;
            }
            else
            {
                ltrLogin.Text += "<li class=\"it\"><a href=\"/dang-nhap\"><i class=\"m-color fa fa-sign-in\"></i> Đăng nhập</a></li>";
                ltrLogin.Text += "<li class=\"it\"><a href=\"/dang-ky\"><i class=\"m-color fa fa-user\"></i> Đăng ký</a></li>";
            }
        }
    }
}