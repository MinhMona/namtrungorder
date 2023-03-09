using MB.Extensions;
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "nvkinhdoanh";
                LoadData();
            }
        }
        public void LoadData()
        {
            var newNoti = PageController.GetByPagetTypeID(14);
            if (newNoti.Count > 0)
            {
                rptNotiNews.DataSource = newNoti.OrderByDescending(o => o.ID).Take(3);
                rptNotiNews.DataBind();
            }
            var service = ServiceController.GetAll();
            if (service.Count > 0)
            {
                rptService.DataSource = service;
                rptService.DataBind();
            }

            var partners = PartnersController.GetAll("");
            if (partners.Count > 0)
            {
                rptPartner.DataSource = partners;
                rptPartner.DataBind();
            }

            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                if (!string.IsNullOrEmpty(confi.Address))
                {
                    ltrContactFooter.Text += "<div class=\"dt-info\">";
                    ltrContactFooter.Text += "  <i class=\"fas fa-fw fa-home\" aria-hidden=\"true\"></i> ";
                    ltrContactFooter.Text += confi.Address;
                    ltrContactFooter.Text += "</div>";
                }
                if (!string.IsNullOrEmpty(confi.Address2))
                {
                    ltrContactFooter.Text += "<div class=\"dt-info\">";
                    ltrContactFooter.Text += "  <i class=\"fas fa-fw fa-home\" aria-hidden=\"true\"></i> ";
                    ltrContactFooter.Text += confi.Address2;
                    ltrContactFooter.Text += "</div>";
                }

                ltrContactFooter.Text += "<div class=\"dt-info\">";
                ltrContactFooter.Text += "  <i class=\"fas fa-fw fa-mobile-alt\" aria-hidden=\"true\"></i>";
                ltrContactFooter.Text += "  Hotline: <a href=\"tel:" + confi.Hotline + "\">" + confi.Hotline + "</a>";
                ltrContactFooter.Text += "</div>";
                ltrContactFooter.Text += "<div class=\"dt-info\">";
                ltrContactFooter.Text += "  <i class=\"fas fa-fw fa-envelope\" aria-hidden=\"true\"></i>";
                ltrContactFooter.Text += "  Email: <a href=\"mailto:" + confi.EmailContact + "\">" + confi.EmailContact + "</a>";
                ltrContactFooter.Text += "</div>";
            }
        }
        [WebMethod]
        public static string closewebinfo()
        {
            HttpContext.Current.Session["infoclose"] = "ok";
            return "ok";
        }
        [WebMethod]
        public static string getPopup()
        {
            if (HttpContext.Current.Session["notshowpopup"] == null)
            {
                var conf = ConfigurationController.GetByTop1();
                string popup = conf.NotiPopup;
                if (!string.IsNullOrEmpty(popup))
                {
                    NotiInfo n = new NotiInfo();
                    n.NotiTitle = conf.NotiPopupTitle;
                    n.NotiEmail = conf.NotiPopupEmail;
                    n.NotiContent = conf.NotiPopup;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(n);
                }
                else
                    return "null";
            }
            else
                return null;

        }
        [WebMethod]
        public static string checkisreadnoti(int ID)
        {
            var notit = NotificationController.GetByID(ID);
            if (notit != null)
            {
                NotificationController.UpdateStatus(ID, 1, DateTime.Now, HttpContext.Current.Session["userLoginSystem"].ToString());
                return "ok";
            }
            else return null;
        }
        [WebMethod]
        public static void setNotshow()
        {
            HttpContext.Current.Session["notshowpopup"] = "1";
        }
        public class NotiInfo
        {
            public string NotiTitle { get; set; }
            public string NotiContent { get; set; }
            public string NotiEmail { get; set; }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string username = "khách";
            if (Session["userLoginSystem"] != null)
            {
                username = Session["userLoginSystem"].ToString();
            }
            ContactController.Insert(txtFullName.Text, txtEmail.Text, txtPhone.Text, txtContent.Text, false, DateTime.Now, username);
            PJUtils.ShowMessageBoxSwAlert("Gửi liên hệ thành công", "s", true, Page);
        }
    }
}