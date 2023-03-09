using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class VinhAnhMaster : System.Web.UI.MasterPage
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
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                string email = confi.EmailSupport;
                string hotline = confi.Hotline;
                ltrConfig.Text += "<p class=\"info\">Tỷ giá: <span class=\"hl-txt\"> " + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>";
                ltrConfig.Text += "<p class=\"info\"><i class=\"fas fa fa-clock-o\"></i> Giờ làm việc: " + confi.TimeWork + "</p>";
                ltrConfig.Text += "<a href=\"mailto:" + email + "\" class=\"info\"><i class=\"fas fa-fw fa-envelope\"></i> Email: " + email + "</a>";
                ltrConfig.Text += "<a href=\"tel:" + hotline + "\" class=\"info\"><i class=\"fa fa-phone-square\"></i> " + hotline + "</a>";

                ltrSocial.Text += "<div class=\"icon-fb\"><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-fw fa-facebook-f\"></i></a></div>";
                ltrSocial.Text += "<div class=\"icon-tw\"><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-fw fa-twitter\"></i></a></div>";
                ltrSocial.Text += "<div class=\"icon-gg\"><a href=\"" + confi.GooglePlus + "\" target=\"_blank\"><i class=\"fab fa-fw fa-google-plus-g\"></i></a></div>";
                ltrSocial.Text += "<div class=\"icon-pi\"><a href=\"" + confi.Pinterest + "\" target=\"_blank\"><i class=\"fab fa-fw fa-pinterest-p\"></i></a></div>";
                ltrSocial.Text += "<div class=\"icon-ib\"><a href=\"mailto:" + email + "\"><i class=\"fas fa-fw fa-envelope\"></i></a></div>";


                //ltrCurrency.Text = string.Format("{0:N0}", Convert.ToDouble(confi.Currency));
                //ltrConfig1.Text += "<a href=\"javascript:;\" class=\"info\"><i class=\"fa fa-clock-o\"></i> " + confi.TimeWork + "</a>";
                //ltrConfig1.Text += "<a href=\"tel:" + hotline + "\" class=\"info\"><i class=\"fa fa-phone-square\"></i> " + hotline + "</a>";
                //ltrConfig1.Text += "<a href=\"mailto:" + email + "\" class=\"info\"><i class=\"fa fa-envelope-o\"></i> " + email + "</a>";

                //ltrConfig2.Text += "<div class=\"ft-ct\">";
                //ltrConfig2.Text += "<p class=\"contact-method__sub\">Hotline:</p>";
                //ltrConfig2.Text += "<p class=\"contact-method__main\"><a href=\"tel:" + hotline + "\">" + hotline + "</a></p></div>";
                //ltrConfig2.Text += "<div class=\"ft-ct\">";
                //ltrConfig2.Text += "<p class=\"contact-method__sub\">Email:</p>";
                //ltrConfig2.Text += "<a class=\"contact-method__main\" href=\"mailto:" + email + "\">" + email + "</a>";
                //ltrConfig2.Text += "</div>";
                //ltrConfig2.Text += "<div class=\"ft-ct\">";
                //ltrConfig2.Text += "<p class=\"contact-method__sub\">Địa chỉ:</p>";
                //ltrConfig2.Text += "<p class=\"contact-method__main\">" + confi.Address + "</p>";
                //ltrConfig2.Text += "</div>";
            }
            if (Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    var ordershoptemp = OrderShopTempController.GetByUID(acc.ID);
                    int count = 0;
                    if (ordershoptemp.Count > 0)
                        count = ordershoptemp.Count;
                    ltrLogin.Text += "<a href=\"/thong-tin-nguoi-dung\" class=\"auth__log\">" + username + "</a> | <a href=\"/gio-hang\" class=\"auth__reg\">Giỏ hàng (" + count + ")</a>";
                    #region phần thông báo       
                    var notis = NotificationController.GetByReceivedID(acc.ID);
                    //ltrConfig1.Text += "  <a href=\"/thong-bao-cua-ban\" class=\"info\"><i class=\"fa fa-bell\"></i> Thông báo (" + notis.Count + ")</a>";
                    decimal levelID = Convert.ToDecimal(acc.LevelID);
                    int levelID1 = Convert.ToInt32(acc.LevelID);
                    string level = "Vip 0";
                    var userLevel = UserLevelController.GetByID(levelID1);
                    if (userLevel != null)
                    {
                        level = userLevel.LevelName;
                    }

                    decimal countLevel = UserLevelController.GetAll("").Count();
                    decimal te = levelID / countLevel;
                    te = Math.Round(te, 2, MidpointRounding.AwayFromZero);
                    decimal tile = te * 100;

                    ltrLogin.Text += "<div class=\"status-wrap\">";
                    ltrLogin.Text += "  <div class=\"status\">";
                    ltrLogin.Text += "      <header><h4>" + level + "</h4></header>";
                    ltrLogin.Text += "      <main>";
                    ltrLogin.Text += "          <section class=\"level\">";
                    ltrLogin.Text += "              <div class=\"level__info\">";
                    ltrLogin.Text += "                  <p>Level</p>";
                    ltrLogin.Text += "                  <p class=\"rank\">" + level + "</p>";
                    ltrLogin.Text += "              </div>";
                    ltrLogin.Text += "              <div class=\"process\">";
                    ltrLogin.Text += "                  <span style=\"width: " + tile + "%\"></span>";
                    ltrLogin.Text += "              </div>";
                    ltrLogin.Text += "          </section>";
                    ltrLogin.Text += "          <section class=\"balance\">";
                    ltrLogin.Text += "              <p>Số dư:</p>";
                    ltrLogin.Text += "              <div class=\"balance__number\">";
                    ltrLogin.Text += "                  <p class=\"vnd\">" + string.Format("{0:N0}", acc.Wallet) + " vnđ</p>";
                    //ltrLogin.Text += "                  <p class=\"cny\">2450Y</p>";
                    ltrLogin.Text += "              </div>";
                    ltrLogin.Text += "          </section>";
                    if (acc.RoleID != 1)
                    {
                        ltrLogin.Text += "          <section class=\"links\">";
                        ltrLogin.Text += "              <a href=\"/admin/login\">Quản trị<i class=\"fa fa-caret-right\"></i></a>";
                        ltrLogin.Text += "          </section>";
                    }
                    ltrLogin.Text += "          <section class=\"links\">";
                    ltrLogin.Text += "              <a href=\"/thong-tin-nguoi-dung\">Thông tin tài khoản<i class=\"fa fa-caret-right\"></i></a>";
                    ltrLogin.Text += "          </section>";
                    ltrLogin.Text += "          <section class=\"links\">";
                    ltrLogin.Text += "              <a href=\"/danh-sach-don-hang\">Đơn hàng của bạn<i class=\"fa fa-caret-right\"></i></a>";
                    ltrLogin.Text += "          </section>";
                    ltrLogin.Text += "          <section class=\"links\"><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch<i class=\"fa fa-caret-right\"></i></a></section>";
                    ltrLogin.Text += "      </main>";
                    ltrLogin.Text += "      <footer><a href=\"/dang-xuat\" class=\"btn btn-3\">ĐĂNG XUẤT</a></footer>";
                    ltrLogin.Text += "  </div>";
                    ltrLogin.Text += "</div>";
                    #endregion
                }
            }
            else
            {
                ltrLogin.Text += "<a href=\"/dang-nhap\" class=\"auth__log\">Đăng nhập</a> | <a href=\"/dang-ky\" class=\"auth__reg\">Đăng kí</a>";
            }

        }
    }
}