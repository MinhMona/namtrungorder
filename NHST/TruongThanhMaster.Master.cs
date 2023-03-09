using NHST.Bussiness;
using NHST.Controllers;
using Supremes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class TruongThanhMaster : System.Web.UI.MasterPage
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
                string timework = confi.TimeWork;

                ltrTopLeft.Text += "<div class=\"hdt__info\">";
                ltrTopLeft.Text += "    <div class=\"hdt__info-block\">";
                ltrTopLeft.Text += "        <div class=\"img\"><img src=\"/App_Themes/TruongThanh/images/hd-top-icon1.png\" alt=\"\"></div>";
                ltrTopLeft.Text += "        <div class=\"ct\"><p class=\"tt\">Tỉ giá tiền tệ:</p><p>" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + " vnđ</p></div>";
                ltrTopLeft.Text += "    </div>";
                ltrTopLeft.Text += "    <div class=\"hdt__info-block\">";
                ltrTopLeft.Text += "        <div class=\"img\"><img src=\"/App_Themes/TruongThanh/images/hd-top-icon2.png\" alt=\"\"></div>";
                ltrTopLeft.Text += "        <div class=\"ct\"><p class=\"tt\">Địa chỉ:</p><p>"+ confi.Address + "</p></div>";
                ltrTopLeft.Text += "    </div>";
                ltrTopLeft.Text += "    <div class=\"hdt__info-block\">";
                ltrTopLeft.Text += "        <div class=\"img\"><img src=\"/App_Themes/TruongThanh/images/hd-top-icon3.png\" alt=\"\"></div>";
                //<p class=\"tt\">Thứ sáu - Thứ bảy</p><p>08:00 am - 21:00 pm</p>
                ltrTopLeft.Text += "        <div class=\"ct\">" + timework + "</div>";
                ltrTopLeft.Text += "    </div>";
                ltrTopLeft.Text += "</div>";


                //ltrTopLeft.Text += "<div class=\"hdt__left\">";

                //ltrTopLeft.Text += "    <div>Tỉ giá ¥1 = " + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</div>";
                //ltrTopLeft.Text += "    <div>CSKH: <a href=\"tel:" + hotline + "\" >" + hotline + "</a></div>";
                //ltrTopLeft.Text += "    <div>Giờ hoạt động: " + timework + "</div>";
                //ltrTopLeft.Text += "    <div>Email: <a href=\"mailto:" + email + "\">" + email + "</a></div>";
                //ltrTopLeft.Text += "</div>";

                //ltrAddress.Text = confi.Address;
                //ltrHotline.Text = "<a href=\"tel:" + hotline + "\">" + hotline + "</a>";
                //ltrEmail.Text = "<a href=\"mailto:" + email + "\">" + email + "</a>";
                ltrSocial.Text += "<a href=\"" + confi.YoutubeLink + "\" target=\"_blank\" class=\"btn-media\"><i class=\"fab fa-youtube\"></i></a>";
                ltrSocial.Text += "<a href=\"" + confi.LinkedIn + "\" target=\"_blank\" class=\"btn-media\"><i class=\"fab fa-linkedin-in\"></i></a>";
                ltrSocial.Text += "<a href=\"" + confi.GooglePlus + "\" target=\"_blank\" class=\"btn-media\"><i class=\"fab fa-google-plus-g\"></i></a>";
                ltrSocial.Text += "<a href=\"" + confi.Facebook + "\" target=\"_blank\" class=\"btn-media\"><i class=\"fab fa-facebook-f\"></i></a>";
                ltrSocial.Text += "<a href=\"" + confi.Twitter + "\" target=\"_blank\" class=\"btn-media\"><i class=\"fab fa-twitter\"></i></a>";
                string infocontent = confi.InfoContent;
                if (Session["infoclose"] == null)
                {
                    if (!string.IsNullOrEmpty(infocontent))
                    {
                        ltr_infor.Text += "<div class=\"sec webinfo\">";
                        ltr_infor.Text += "<div class=\"all-info\">";
                        ltr_infor.Text += "<div class=\"main-info\">";
                        ltr_infor.Text += "<div class=\"textcontent\">";
                        ltr_infor.Text += "<span>" + infocontent + "</span>";
                        ltr_infor.Text += "<a href=\"javascript:;\" onclick=\"closewebinfo()\" class=\"icon-close-info\"><i class=\"fa fa-times\"></i></a>";
                        ltr_infor.Text += "</div></div></div></div>";
                    }
                }
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

                    //ltrLogin.Text += "<div class=\"account\">";
                    var notis = NotificationController.GetByReceivedID(acc.ID);
                    //ltrLogin.Text += "<div class=\"cart\">";
                    //if (acc.RoleID != 1)
                    //{
                    //    ltrLogin.Text += "  <a href=\"/manager/admin-noti\" class=\"info\"><i class=\"fa fa-bell\"></i> Thông báo (" + notis.Count + ")</a>";
                    //}
                    //else
                    //    ltrLogin.Text += "  <a href=\"/thong-bao-cua-ban\" class=\"info\"><i class=\"fa fa-bell\"></i> Thông báo (" + notis.Count + ")</a>";

                    //ltrLogin.Text += "</div>";
                    ltrLogin.Text += "  <div class=\"acc-info\">";
                    ltrLogin.Text += "      <a href=\"#\" class=\"login\"><i class=\"fas fa-user\"></i> " + username + "</a>";
                    ltrLogin.Text += "          <div class=\"status\">";
                    ltrLogin.Text += "              <div class=\"status-wrap\">";
                    ltrLogin.Text += "                 <div class=\"status__header\"><h4>" + level + "</h4></div>";
                    ltrLogin.Text += "                 <div class=\"status__body\">";
                    ltrLogin.Text += "                      <section class=\"level\">";
                    ltrLogin.Text += "                          <div class=\"level__info\">";
                    ltrLogin.Text += "                              <p>Level</p>";
                    ltrLogin.Text += "                              <p class=\"rank\">" + level + "</p>";
                    ltrLogin.Text += "                          </div>";
                    ltrLogin.Text += "                          <div class=\"level__process\">";
                    ltrLogin.Text += "                              <span style=\"width: " + tile + "%\"></span>";
                    ltrLogin.Text += "                          </div>";
                    ltrLogin.Text += "                      </section>";
                    ltrLogin.Text += "                      <section class=\"balance\">";
                    ltrLogin.Text += "                          <p>Số dư:</p>";
                    ltrLogin.Text += "                          <div class=\"balance__number\">";
                    ltrLogin.Text += "                              <p class=\"vnd\">" + string.Format("{0:N0}", acc.Wallet) + " vnđ</p>";
                    //ltrLogin.Text += "                            <p class=\"cny\">2450Y</p>";
                    ltrLogin.Text += "                          </div>";
                    ltrLogin.Text += "                      </section>";
                    if (acc.RoleID != 1)
                    {
                        ltrLogin.Text += "                  <div class=\"links\">";
                        ltrLogin.Text += "                      <a href=\"/manager/login\">Quản trị<i class=\"fa fa-caret-right\"></i></a>";
                        ltrLogin.Text += "                  </div>";
                    }
                    ltrLogin.Text += "                      <div class=\"links\">";
                    ltrLogin.Text += "                          <a href=\"/thong-tin-nguoi-dung\">Thông tin tài khoản<i class=\"fa fa-caret-right\"></i></a>";
                    ltrLogin.Text += "                      </div>";
                    ltrLogin.Text += "                      <div class=\"links\">";
                    ltrLogin.Text += "                          <a href=\"/danh-sach-don-hang?t=1\">Đơn hàng của bạn<i class=\"fa fa-caret-right\"></i></a>";
                    ltrLogin.Text += "                      </div>";
                    ltrLogin.Text += "                      <div class=\"links\"><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrLogin.Text += "              </div>";
                    ltrLogin.Text += "              <div class=\"status__footer\"><a href=\"/dang-xuat\" class=\"ft-btn\">ĐĂNG XUẤT</a></div>";
                    ltrLogin.Text += "          </div>";
                    ltrLogin.Text += "      </div>";
                    ltrLogin.Text += "</div>";
                    //ltrLogin.Text += "  <div class=\"cart\">";
                    //ltrLogin.Text += "  <a href=\"/gio-hang\" class=\"link__item\"><i class=\"fas fa-shopping-cart\"></i> Giỏ hàng (" + count + ")</a>";
                    //ltrLogin.Text += "</div>";
                    //ltrLogin.Text += "</div>";

                    #endregion
                }
            }
            else
            {
                ltrLogin.Text += "<div class=\"hdt__auth-block\">";
                ltrLogin.Text += "  <a href=\"/dang-nhap\">Đăng nhập</a> / ";
                ltrLogin.Text += "  <a href=\"/dang-ky\">Đăng ký</a>";
                ltrLogin.Text += "</div>";
            }
        }
    }
}