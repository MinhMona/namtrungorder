using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Controllers;
using System.Text;

namespace NHST
{
    public partial class PDVMasterNotLogin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkuser();
                LoadConfiguration(); LoadMenuFooter();
            }
        }
        public void checkuser()
        {
            if (Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    ltrUser.Text += "<div class=\"login\">";
                    ltrUser.Text += "<p><a href=\"/thong-tin-nguoi-dung\">Xin chào: " + u.Username.ToLower() + "</a><span class=\"span1\">|</span>";
                    ltrUser.Text += "<a href=\"/lich-su-giao-dich\">Số dư: " + string.Format("{0:N0}", u.Wallet).Replace(",", ".") + " VNĐ</a></p>";
                    ltrUser.Text += "</p>";
                    ltrUser.Text += "</div>";
                    ltrUser.Text += "<div class=\"store\">";
                    ltrUser.Text += "<p>";
                    ltrUser.Text += "<a href=\"/gio-hang\">";
                    ltrUser.Text += "<img src=\"/App_Themes/pdv/assets/images/store.png\" alt=\"\">giỏ hàng</a>";
                    ltrUser.Text += "</p>";
                    ltrUser.Text += "</div>";
                    //ltrUser.Text += "<div class=\"store\">";
                    //ltrUser.Text += "<p>";
                    //ltrUser.Text += "<a href=\"/dang-xuat\">";
                    //ltrUser.Text += "Đăng xuất</a>";
                    //ltrUser.Text += "</p>";
                    //ltrUser.Text += "</div>";
                }
                else
                {
                    ltrUser.Text = "<div class=\"login\"><p><a href=\"/dang-nhap\">đăng nhập</a> <span class=\"span1\">|</span> <a href=\"/dang-ky\">đăng ký</a></p></div>";
                }
            }
            else
            {
                ltrUser.Text = "<div class=\"login\"><p><a href=\"/dang-nhap\">đăng nhập</a> <span class=\"span1\">|</span> <a href=\"/dang-ky\">đăng ký</a></p></div>";
            }
        }
        public void LoadConfiguration()
        {
            var c = ConfigurationController.GetByTop1();
            if (c != null)
            {
                ltrConfig.Text += "";
                ltrConfig.Text += "<p class=\"infomation\">";
                ltrConfig.Text += "Tỷ giá: " + string.Format("{0:N0}", Convert.ToDouble(c.Currency)) + " vnd/ndt ";
                ltrConfig.Text += "<span class=\"span1\">| </span>";
                ltrConfig.Text += "Hotline: <a href=\"tel:+" + c.Hotline + "\">" + c.Hotline + "</a> ";
                ltrHotline.Text = "<a href=\"tel:" + c.Hotline + "\">" + c.Hotline + "</a>";
                //ltrConfig.Text += "<span> | </span>";
                //ltrConfig.Text += "<a href=\"tel:+046671888\">04.6671.888</a>";
                ltrConfig.Text += "</p>";

                //string infocontent = c.InfoContent;
                //if (Session["infoclose"] == null)
                //{
                //    if (!string.IsNullOrEmpty(infocontent))
                //    {
                //        ltr_infor.Text += "<div class=\"sec webinfo\">";
                //        ltr_infor.Text += "<div class=\"all width-100-percent\">";
                //        ltr_infor.Text += "<div class=\"main\">";
                //        ltr_infor.Text += "<div class=\"textcontent\">";
                //        ltr_infor.Text += "<span>" + infocontent + "</span>";
                //        ltr_infor.Text += "<a href=\"javascript:;\" onclick=\"closewebinfo()\" class=\"icon-close-info\"><i class=\"fa fa-times\"></i></a>";
                //        ltr_infor.Text += "</div></div></div></div>";
                //    }
                //}
                if (c.LogoIMG != null)
                    ltrLogo.Text = "<img src=\"" + c.LogoIMG + "\" alt=\"#\">";
                ltrLinkExtension.Text += "";
                ltrLinkExtension.Text += "<a href=\"" + c.ChromeExtensionLink + "\" target=\"_blank\">";
                ltrLinkExtension.Text += "<img src=\"/App_Themes/pdv/assets/images/chrome.png\" alt=\"#\"></a>";
                ltrLinkExtension.Text += "<a href=\"" + c.CocCocExtensionLink + "\" target =\"_blank\">";
                ltrLinkExtension.Text += "<img src=\"/App_Themes/pdv/assets/images/cococ.png\" alt=\"#\"></a>";
                ltrAboutUs.Text = c.AboutText;
                ltrAddress.Text += "";
                ltrAddress.Text += c.Address;
                ltrAddress.Text += c.Address2;
                ltrAddress.Text += c.Address3;

                string facebooklink = c.Facebook;
                string tweet = c.Twitter;
                string goog = c.GooglePlus;
                ltrLinkSocial1.Text += "<a href=\""+ facebooklink + "\"><img src=\"/App_Themes/pdv/assets/images/icon-face.png\" alt=\"#\"></a>";
                ltrLinkSocial1.Text += "<a href=\"" + tweet + "\"><img src=\"/App_Themes/pdv/assets/images/icon-tweet.png\" alt=\"#\"></a>";
                ltrLinkSocial1.Text += "<a href=\"" + goog + "\"><img src=\"/App_Themes/pdv/assets/images/icon-gp.png\" alt=\"#\"></a>";

                ltrLinkSocial2.Text += "<li><a href=\"" + facebooklink + "\"><img src=\"/App_Themes/pdv/assets/images/face.png\" alt=\"#\"></a></li>";
                ltrLinkSocial2.Text += "<li><a href=\"" + tweet + "\"><img src=\"/App_Themes/pdv/assets/images/g.png\" alt=\"#\"></a></li>";
                ltrLinkSocial2.Text += "<li><a href=\"" + goog + "\"><img src=\"/App_Themes/pdv/assets/images/tweet.png\" alt=\"#\"></a></li>";

                ltrfooter.Text += c.FooterLeft;
                ltrfooter.Text += c.FooterRight;
            }

        }
        public void LoadMenuFooter()
        {
            var pageService = PageController.GetByPagetTypeID(6).Take(2).ToList();
            var pageChinhsach = PageController.GetByPagetTypeID(5).Take(2).ToList();
            int PageTypeServiceID = 6;
            int PageTypeChinhsachID = 5;

            string PageTypeName_Service = "";
            string PageTypeName_Chinhsach = "";
            var pagetypeSerice = PageTypeController.GetByID(PageTypeServiceID);
            var pagetypeChinhsach = PageTypeController.GetByID(PageTypeChinhsachID);

            if (pagetypeSerice != null)
            {
                PageTypeName_Service = pagetypeSerice.PageTypeName;
            }
            if (pagetypeChinhsach != null)
            {
                PageTypeName_Chinhsach = pagetypeChinhsach.PageTypeName;
            }
            StringBuilder html = new StringBuilder();
            if (pageService.Count > 0)
            {
                foreach (var p in pageService)
                {
                    html.Append("<p><a href=\"" + p.NodeAliasPath + "\"><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i>" + p.Title + "</a></p>");
                }
            }
            if (pageChinhsach.Count > 0)
            {
                foreach (var p in pageChinhsach)
                {
                    html.Append("<p><a href=\"" + p.NodeAliasPath + "\"><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i>" + p.Title + "</a></p>");
                }
            }
            ltrFooterMenu.Text = html.ToString();
        }
    }
}