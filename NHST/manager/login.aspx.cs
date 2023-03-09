using Newtonsoft.Json;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class login : System.Web.UI.Page
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

                }
            }

        }
        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=6Lcr1nkUAAAAAPd1N6e9wewM_KwHLnnZrBT_rfYr&response=" + response;
            string abc = new WebClient().DownloadString(url);
            var result = JsonConvert.DeserializeObject<captcha>(abc);
            if (result.success == "true")
            {
                HttpContext.Current.Session["captcha"] = "1";
            }
            else
            {
                HttpContext.Current.Session["captcha"] = "0";
            }
            return abc;
        }
        public class captcha
        {
            public string success { get; set; }
            public string challenge_ts { get; set; }
            public string hostname { get; set; }
        }
        [WebMethod]
        public static string Login(string username, string password)
        {
            tbl_Account ac = AccountController.Login(username, password);
            tbl_Account acm = AccountController.LoginEmail(username, password);
            if (ac != null)
            {
                var ai = AccountInfoController.GetByUserID(ac.ID);
                if (ai != null)
                {
                    if (ac.Status == 1)
                    {
                        return "0";
                    }
                    else if (ac.Status == 2)
                    {
                        //Đã kích hoạt
                        HttpContext.Current.Session["userLoginSystem"] = ac.Username;
                        return "ok";
                    }
                    else if (ac.Status == 3)
                    {
                        return "1";
                    }
                }
                else
                {
                    return "2";
                }
            }
            else if (acm != null)
            {
                var ai = AccountInfoController.GetByUserID(acm.ID);
                if (ai != null)
                {
                    if (acm.Status == 1)
                    {
                        return "0";
                    }
                    else if (acm.Status == 2)
                    {
                        //Đã kích hoạt
                        HttpContext.Current.Session["userLoginSystem"] = acm.Username;
                        return "ok";
                        //HttpContext.Current.Response.Redirect("/trang-chu");
                    }
                    else if (acm.Status == 3)
                    {
                        return "1";
                    }
                }
                else
                {
                    return "2";
                }
            }
            else
            {
                return "2";
            }
            return "2";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                #region Check Captcha Telerik
                string text = RadCaptcha1.CaptchaImage.Text;
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
                            int role = Convert.ToInt32(ac.RoleID);
                            if (role != 1)
                            {
                                if (role == 4)
                                    Response.Redirect("/manager/TQWareHouse.aspx");
                                else if (role == 5)
                                    Response.Redirect("/manager/VNWarehouse");
                                else
                                    Response.Redirect("/manager/orderlist2?t=1");
                            }
                            else
                            {
                                Response.Redirect("/danh-sach-don-hang?t=1");
                            }
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
                #endregion
            }
        }
    }
}