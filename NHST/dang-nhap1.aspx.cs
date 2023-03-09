using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;

namespace NHST
{
    public partial class dang_nhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    LoadSEO();

                }
            }

        }
        public void LoadSEO()
        {
            var home = PageSEOController.GetByID(4);
            string weblink = "https://vanchuyendaquocgia.com";
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (home != null)
            {
                HtmlHead objHeader = (HtmlHead)Page.Header;

                //we add meta description
                HtmlMeta objMetaFacebook = new HtmlMeta();

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "fb:app_id");
                objMetaFacebook.Content = "676758839172144";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:url");
                objMetaFacebook.Content = url;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:type");
                objMetaFacebook.Content = "website";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:title");
                objMetaFacebook.Content = home.ogtitle;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:description");
                objMetaFacebook.Content = home.ogdescription;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image");
                if (string.IsNullOrEmpty(home.ogimage))
                    objMetaFacebook.Content = weblink + "/App_Themes/vcdqg/images/main-logo.png";
                else
                    objMetaFacebook.Content = weblink + home.ogimage;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image:width");
                objMetaFacebook.Content = "200";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image:height");
                objMetaFacebook.Content = "500";
                objHeader.Controls.Add(objMetaFacebook);

                this.Title = home.metatitle;
                HtmlMeta meta = new HtmlMeta();
                meta = new HtmlMeta();
                meta.Attributes.Add("name", "description");
                meta.Content = home.metadescription;
                objHeader.Controls.Add(meta);

                meta = new HtmlMeta();
                meta.Attributes.Add("name", "keyword");
                meta.Content = home.metakeyword;
                objHeader.Controls.Add(meta);

            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            tbl_Account ac = AccountController.Login(txtUsername.Text.Trim(), txtpass.Text.Trim());
            tbl_Account acm = AccountController.LoginEmail(txtUsername.Text.Trim(), txtpass.Text.Trim());
            if (ac != null)
            {
                var ai = AccountInfoController.GetByUserID(ac.ID);
                if (ai != null)
                {
                    if (ac.Status == 1)
                    {
                        string prefix = ai.MobilePhonePrefix;
                        string phone = ai.MobilePhone;
                        string fullphone = prefix + phone;
                        //Chưa kích hoạt
                        Session["userNotActive"] = ac.Username;
                        string otpreturn = OTPUtils.ResetAndCreateOTP(ac.ID, prefix, phone, 1);
                        if (otpreturn != null)
                        {
                            string message = MessageController.GetByType(1).Message + " " + otpreturn;
                            ESMS.Send(fullphone, message);
                            Response.Redirect("/OTP");
                        }
                    }
                    else if (ac.Status == 2)
                    {
                        //Đã kích hoạt
                        Session["userLoginSystem"] = ac.Username;
                        Response.Redirect("/trang-chu");
                    }
                    else if (ac.Status == 3)
                    {
                        //Đã Block
                        lblError.Text = "Tài khoản của bạn đang bị khóa, vui lòng liên hệ với Admin để biết thêm chi tiết.";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Tài khoản hiện không tồn tại trong hệ thống.";
                    lblError.Visible = true;
                }
            }
            else if (acm != null)
            {
                var ai = AccountInfoController.GetByUserID(acm.ID);
                if (ai != null)
                {
                    if (acm.Status == 1)
                    {
                        string prefix = ai.MobilePhonePrefix;
                        string phone = ai.MobilePhone;
                        string fullphone = prefix + phone;
                        //Chưa kích hoạt
                        Session["userNotActive"] = acm.Username;
                        string otpreturn = OTPUtils.ResetAndCreateOTP(acm.ID, prefix, phone, 1);
                        if (otpreturn != null)
                        {
                            lblError.Text = "Tài khoản của bạn chưa được kích hoạt, vui lòng liên hệ với Admin để biết thêm chi tiết.";
                            lblError.Visible = true;
                            //string message = MessageController.GetByType(1).Message + " " + otpreturn;
                            //ESMS.Send(fullphone, message);
                            //Response.Redirect("/OTP");
                        }
                    }
                    else if (acm.Status == 2)
                    {
                        //Đã kích hoạt
                        Session["userLoginSystem"] = acm.Username;
                        Response.Redirect("/trang-chu");
                    }
                    else if (acm.Status == 3)
                    {
                        //Đã Block
                        lblError.Text = "Tài khoản của bạn đang bị khóa, vui lòng liên hệ với Admin để biết thêm chi tiết.";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Tài khoản hiện không tồn tại trong hệ thống.";
                    lblError.Visible = true;
                }
            }
            else
            {
                //if (Session["userloginfail"] != null)
                //{
                //    int r = Convert.ToInt32(Session["userloginfail"].ToString());
                //    if (r >= 3)
                //    {
                //        hdfUserLoginFail.Value = "showcap";
                //    }
                //    else
                //    {
                //        int q = r + 1;
                //        Session["userloginfail"] = q;
                //        hdfUserLoginFail.Value = "hidecap";
                //    }
                //}
                //else
                //{
                //    Session["userloginfail"] = 1;
                //    hdfUserLoginFail.Value = "hidecap";
                //}
                lblError.Text = "Đăng nhập không thành công, vui lòng kiểm tra lại.";
                lblError.Visible = true;
            }
        }
    }
}