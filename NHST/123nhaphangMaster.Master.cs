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
    public partial class _123nhaphangMaster : System.Web.UI.MasterPage
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
                ltrTopLeft.Text += "<div class=\"hdt__left\">";
                ltrTopLeft.Text += "    <p>Tỉ giá ¥ = <span class=\"color\">" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>";
                ltrTopLeft.Text += "    <p>CSKH: <a href=\"tel:" + hotline + "\" class=\"color\">" + hotline + "</a></p>";
                ltrTopLeft.Text += "    <p>Email: <a href=\"mailto:" + email + "\" class=\"color\">" + email + "</a></p>";
                ltrTopLeft.Text += "    <p>Giờ hoạt động: <span class=\"color\">" + timework + "</span></p>";
                ltrTopLeft.Text += "</div>";                
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
                    ltrLogin.Text += "<div class=\"cart\">";
                    ltrLogin.Text += "  <a href=\"/thong-bao-cua-ban\" class=\"info\"><i class=\"fa fa-bell\"></i> Thông báo (" + notis.Count + ")</a>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "  <div class=\"acc-info\">";
                    ltrLogin.Text += "      <a href=\"#\" class=\"login\">" + username + "</a>";
                    ltrLogin.Text += "          <div class=\"status\">";
                    ltrLogin.Text += "              <div class=\"status-wrap\">";
                    ltrLogin.Text += "                 <div class=\"status__header\"><h4>" + level + "</h4></div>";
                    ltrLogin.Text += "                  <div class=\"status__body\">";
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
                        ltrLogin.Text += "          <div class=\"links\">";
                        ltrLogin.Text += "              <a href=\"/manager/login\">Quản trị<i class=\"fa fa-caret-right\"></i></a>";
                        ltrLogin.Text += "          </div>";
                    }
                    ltrLogin.Text += "          <div class=\"links\">";
                    ltrLogin.Text += "              <a href=\"/thong-tin-nguoi-dung\">Thông tin tài khoản<i class=\"fa fa-caret-right\"></i></a>";
                    ltrLogin.Text += "          </div>";
                    ltrLogin.Text += "          <div class=\"links\">";
                    ltrLogin.Text += "              <a href=\"/danh-sach-don-hang\">Đơn hàng của bạn<i class=\"fa fa-caret-right\"></i></a>";
                    ltrLogin.Text += "          </div>";
                    ltrLogin.Text += "          <div class=\"links\"><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrLogin.Text += "      </div>";
                    ltrLogin.Text += "       <div class=\"status__footer\"><a href=\"/dang-xuat\" class=\"ft-btn\">ĐĂNG XUẤT</a></div>";
                    ltrLogin.Text += "  </div>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "  </div>";
                    ltrLogin.Text += " / <div class=\"cart\">";
                    ltrLogin.Text += "  <a href=\"/gio-hang\" class=\"link__item\">Giỏ hàng (" + count + ")</a>";
                    ltrLogin.Text += "</div>";
                    //ltrLogin.Text += "</div>";

                    #endregion
                }
            }
            else
            {
                ltrLogin.Text += "<div class=\"login\"><a href=\"/dang-nhap\">Đăng nhập </a></div>";
                ltrLogin.Text += "/ ";
                ltrLogin.Text += "<div class=\"signup\"><a href=\"/dang-ky\"> Đăng kí</a></div>";
            }
        }
    }
}