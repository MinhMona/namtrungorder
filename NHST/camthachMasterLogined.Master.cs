using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class camthachMasterLogined : System.Web.UI.MasterPage
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

                ltrTopLeft.Text += "<p>Tỷ giá ¥: <span>" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>"
                                + "  <a href=\"\"><i class=\"fas fa-phone\"></i>" + hotline + "</a>"
                                + "  <p>(Thời gian làm việc: " + confi.TimeWork + ")</p>";
                ltrWebname.Text = confi.Websitename;
                ltrHotline2.Text = "<a href=\"tel:" + hotline + "\">" + hotline + "</a>";
                ltrEmail2.Text = "<a href=\"mailto:" + email + "\">" + email + "</a>";
                ltrAddress.Text = confi.Address; ;
                ltrSocial.Text += "<div class=\"social__item fb\"><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-facebook-f\"></i></a></div>";
                ltrSocial.Text += "<div class=\"social__item tw\"><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-twitter\"></i></a></div>";
                ltrSocial.Text += "<div class=\"social__item ins\"><a href=\"" + confi.Instagram + "\" target=\"_blank\"><i class=\"fab fa-instagram\"></i></a></div>";
                ltrSocial.Text += "<div class=\"social__item sky\"><a href=\"" + confi.Skype + "\" target=\"_blank\"><i class=\"fab fa-skype\"></i></a></div>";
                //ltrConfig.Text += "<p class=\"info\">Tỷ giá: <span class=\"hl-txt\"> " + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>";
                //ltrConfig.Text += "<p class=\"info\"><i class=\"fas fa fa-clock-o\"></i> Giờ làm việc: " + confi.TimeWork + "</p>";
                //ltrConfig.Text += "<a href=\"mailto:" + email + "\" class=\"info\"><i class=\"fas fa-fw fa-envelope\"></i> Email: " + email + "</a>";
                //ltrConfig.Text += "<a href=\"tel:" + hotline + "\" class=\"info\"><i class=\"fa fa-phone-square\"></i> " + hotline + "</a>";

                //ltrSocial.Text += "<div class=\"icon-fb\"><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-fw fa-facebook-f\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-tw\"><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-fw fa-twitter\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-gg\"><a href=\"" + confi.GooglePlus + "\" target=\"_blank\"><i class=\"fab fa-fw fa-google-plus-g\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-pi\"><a href=\"" + confi.Pinterest + "\" target=\"_blank\"><i class=\"fab fa-fw fa-pinterest-p\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-ib\"><a href=\"mailto:" + email + "\"><i class=\"fas fa-fw fa-envelope\"></i></a></div>";               
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
                    #region phần thông báo                                               
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
                    ltrLogin.Text += "<span href=\"\" class=\"hover-acc\">";
                    ltrLogin.Text += "<a href=\"#\" class=\"link__item\"><i class=\"fas fa-sign-out-alt\"></i>" + username + "</a>";
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
                    ltrLogin.Text += "</span>";
                    ltrLogin.Text += "<a href=\"/gio-hang\" class=\"link__item\"><i class=\"fas fa-shopping-cart\"></i>Giỏ hàng (" + count + ")</a>";
                    #endregion
                }
            }
            else
            {
                ltrLogin.Text += "<a href=\"/quen-mat-khau\" class=\"link__item\"><i class=\"fas fa-lock\"></i>Quên mật khẩu</a>";
                ltrLogin.Text += "<span class=\"hover-acc\">";
                ltrLogin.Text += "<a href=\"/dang-nhap\" class=\"link__item\"><i class=\"fas fa-sign-out-alt\"></i>Đăng nhập</a>";
                ltrLogin.Text += "</span>";
                ltrLogin.Text += "<a href=\"/dang-ky\" class=\"link__item\"><i class=\"fas fa-user-plus\"></i>Đăng ký</a>";
            }

        }
    }
}