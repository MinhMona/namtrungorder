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
    public partial class KT1688Master : System.Web.UI.MasterPage
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
                ltrTopLeft.Text += "    <p><i class=\"fas fa-yen-sign\"></i>Tỉ giá: <span>" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>";
                ltrTopLeft.Text += "    <p><i class=\"fas fa-mobile-alt\"></i><a href=\"tel:" + hotline + "\">" + hotline + "</a></p>";
                //ltrTopLeft.Text += "    <p>Tỉ giá ¥ = <span class=\"color4\">" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>";
                //ltrTopLeft.Text += "    <p>CSKH: <a href=\"tel:" + hotline + "\" class=\"color4\">" + hotline + "</a></p>";
                //ltrTopLeft.Text += "    <p>Email: <a href=\"mailto:" + email + "\" class=\"color4\">" + email + "</a></p>";
                //ltrTopLeft.Text += "    <p>Giờ hoạt động: <span class=\"color4\">" + timework + "</span></p>";
                ltrTopLeft.Text += "</div>";
                ltrSC.Text += "<a href=\"" + confi.YoutubeLink + "\" target=\"_blank\"><i class=\"fab fa-youtube\"></i></a>";
                ltrSC.Text += "<a href=\"" + confi.LinkedIn + "\" target=\"_blank\"><i class=\"fab fa-linkedin-in\"></i></a>";
                ltrSC.Text += "<a href=\"" + confi.GooglePlus + "\" target=\"_blank\"><i class=\"fab fa-google-plus-g\"></i></a>";
                ltrSC.Text += "<a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-facebook-f\"></i></a>";
                ltrSC.Text += "<a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-twitter\"></i></a>";
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
                    ltrLogin.Text += "<div class=\"cart\">";
                    if (acc.RoleID != 1)
                    {
                        ltrLogin.Text += "  <a href=\"/manager/admin-noti\" class=\"info\"><i class=\"fa fa-bell\"></i> Thông báo (" + notis.Count + ")</a>";
                    }
                    else
                        ltrLogin.Text += "  <a href=\"/thong-bao-cua-ban\" class=\"info\"><i class=\"fa fa-bell\"></i> Thông báo (" + notis.Count + ")</a>";

                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "  <div class=\"acc-info\">";
                    ltrLogin.Text += "      <a href=\"#\" class=\"login\"><i class=\"fas fa-user\"></i>" + username + "</a>";
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
                    ltrLogin.Text += "  <div class=\"cart\">";
                    ltrLogin.Text += "  <a href=\"/gio-hang\" class=\"link__item\"><i class=\"fas fa-shopping-cart\"></i> Giỏ hàng (" + count + ")</a>";
                    ltrLogin.Text += "</div>";
                    //ltrLogin.Text += "</div>";

                    #endregion
                }
            }
            else
            {
                ltrLogin.Text += "<div class=\"reset\"><a href=\"/quen-mat-khau\"><i class=\"fas fa-unlock\"></i>Quên mật khẩu ?</a></div>";
                ltrLogin.Text += "<div class=\"login\"><a href=\"/dang-nhap\"><i class=\"far fa-arrow-alt-circle-right\"></i>Đăng nhập</a></div>";
                ltrLogin.Text += "<div class=\"signup\"><a href=\"/dang-ky\"><i class=\"far fa-user\"></i>Đăng ký</a></div>";
            }
        }
        protected void btnsearchpro_Click(object sender, EventArgs e)
        {
            string text = hdfTextSearch.Value;
            if (!string.IsNullOrEmpty(text))
            {
                string a = PJUtils.TranslateText(text, "vi|zh");
                string page = hdfWebSearch.Value;
                SearchPage(page, PJUtils.RemoveHTMLTags(a));
            }
        }
        #region Translate And Run
        public string TranslateText(string input, string languagePair)
        {
            try
            {
                string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
                request.Method = "GET";
                var content = String.Empty;
                HttpStatusCode statusCode;
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var contentType = response.ContentType;
                    Encoding encoding = null;
                    if (contentType != null)
                    {
                        var match = Regex.Match(contentType, @"(?<=charset\=).*");
                        if (match.Success)
                            encoding = Encoding.GetEncoding(match.ToString());
                    }

                    encoding = encoding ?? Encoding.UTF8;

                    statusCode = ((HttpWebResponse)response).StatusCode;
                    using (var reader = new StreamReader(stream, encoding))
                        content = reader.ReadToEnd();
                }
                var doc = Dcsoup.Parse(content);
                var scoreDiv = doc.Select("html").Select("span[id=result_box]").Html;
                return scoreDiv;
            }
            catch
            {
                return "";
            }

        }

        public void SearchPage(string page, string text)
        {
            string linkgo = "";
            if (page == "tmall")
            {
                string a = text;
                string textsearch_tmall = GetHashString(a);
                //string fullLinkSearch_tmall = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
                linkgo = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
            }
            else if (page == "taobao")
            {
                string a = text;
                string textsearch_taobao = GetHashString(a);
                //string fullLinkSearch_taobao = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                linkgo = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                //https://world.taobao.com/search/search.htm?q=%B9%AB%BC%A6&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1
            }
            else if (page == "1688")
            {
                string a = text;
                string textsearch_1688 = GetHashString(a);
                //string fullLinkSearch_1688 = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
                linkgo = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
            }
            Response.Redirect(linkgo);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "redirect('" + linkgo + "')", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "redirect('" + linkgo + "');", true);
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(inputString);
            return bytes;
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append("%" + b.ToString("X2"));

            return sb.ToString();
        }
        #endregion
    }
}