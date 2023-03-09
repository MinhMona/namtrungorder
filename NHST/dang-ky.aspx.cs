using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
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
    public partial class dang_ky2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadPrefix();
                LoadSEO();
                LoadDDL();
            }
        }
        public void LoadSEO()
        {
            var home = PageSEOController.GetByID(5);
            string weblink = "https://1688express.com";
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
        public void loadPrefix()
        {
            //var listpre = PJUtils.loadprefix();
            //ddlPrefix.Items.Clear();
            //foreach (var item in listpre)
            //{
            //    ListItem listitem = new ListItem(item.dial_code, item.dial_code);
            //    ddlPrefix.Items.Add(listitem);
            //}

            //ddlPrefix.DataBind();
            //ddlPrefix.SelectedValue = "+84";
        }
        public void LoadDDL()
        {
            ddlReceivePlace.Items.Clear();
            ddlReceivePlace.Items.Insert(0, "Chọn kho đích");

            ddlWarehouseFrom.Items.Clear();
            ddlWarehouseFrom.Items.Insert(0, "Chọn kho TQ");
            
            var warehousefrom = WarehouseFromController.GetAllWithIsHidden(false);
            if (warehousefrom.Count > 0)
            {
                ddlWarehouseFrom.DataSource = warehousefrom;
                ddlWarehouseFrom.DataBind();
            }


            var warehouse = WarehouseController.GetAllWithIsHidden(false);
            if (warehouse.Count > 0)
            {
                ddlReceivePlace.DataSource = warehouse;
                ddlReceivePlace.DataBind();
            }
            
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text.Trim().ToLower();
            string Email = txtEmail.Text.Trim();
            var checkuser = AccountController.GetByUsername(Username);
            var checkemail = AccountController.GetByEmail(Email);
            string Phone = txtPhone.Text.Trim().Replace(" ", "");
            var getaccountinfor = AccountInfoController.GetByPhone(Phone);
            bool checkusernamebool = false;
            bool checkemailbool = false;
            bool checkphonebool = false;
            string error = "";
            bool check = PJUtils.CheckUnicode(Username);
            DateTime currentDate = DateTime.Now;
            DateTime bir = DateTime.Now;
            int nvkdID = 0;
            string nvkd = txtSalerUsername.Text;
            var setNoti = SendNotiEmailController.GetByID(1);

            if (!string.IsNullOrEmpty(nvkd))
            {
                var acckinhdoanh = AccountController.GetByUsername(nvkd);
                if(acckinhdoanh!=null)
                {
                    nvkdID = acckinhdoanh.ID;
                }
            }
            if (!string.IsNullOrEmpty(rBirthday.SelectedDate.ToString()))
            {
                bir = Convert.ToDateTime(rBirthday.SelectedDate);
            }
            if (Username.Contains(" "))
            {
                lblcheckemail.Visible = true;
                lblcheckemail.Text = "Tên đăng nhập không được có dấu cách.";
            }
            else if (check == true)
            {
                lblcheckemail.Visible = true;
                lblcheckemail.Text = "Tên đăng nhập không được có dấu tiếng Việt.";
            }
            else
            {
                if (checkuser != null)
                {
                    //lblcheckemail.Visible = true;
                    error += "Tên đăng nhập / Nickname đã được sử dụng vui lòng chọn Tên đăng nhập / Nickname khác.<br/>";
                    checkusernamebool = true;
                }
                if (checkemail != null)
                {
                    //lblcheckemail.Visible = true;
                    error += "Email đã được sử dụng vui lòng chọn Email khác.<br/>";
                    checkemailbool = true;
                }
                if (getaccountinfor != null)
                {
                    //lblcheckemail.Visible = true;
                    error += "Số điện thoại đã được sử dụng vui lòng chọn Số điện thoại khác.<br/>";
                    checkphonebool = true;
                }
                if (checkusernamebool == false && checkemailbool == false && checkphonebool == false)
                {
                    string id = AccountController.Insert(Username, Email, txtpass.Text.Trim(), 1, 1, 1, 2, nvkdID, 0, DateTime.Now, "", DateTime.Now, "");
                    //string id = acc.ID.ToString();
                    if (Convert.ToInt32(id) > 0)
                    {
                        AccountController.updatewarehouseFromwarehouseTo(id.ToInt(0), ddlWarehouseFrom.SelectedValue.ToInt(0),
                            ddlReceivePlace.SelectedValue.ToInt(0));
                        int UID = Convert.ToInt32(id);
                        string idai = AccountInfoController.Insert(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), "",
                            Phone, Email, txtPhone.Text.Trim(), txtAddress.Text, "", "", bir, ddlGender.SelectedValue.ToString().ToInt(1),
                            DateTime.Now, "", DateTime.Now, "");
                        if (idai == "1")
                        {
                            tbl_Account ac = AccountController.Login(Username, txtpass.Text.Trim());
                            if (ac != null)
                            {
                                var ai = AccountInfoController.GetByUserID(ac.ID);
                                if (ac.Status == 1)
                                {
                                    //Chưa kích hoạt
                                    //Session["userNotActive"] = ac.Username;
                                    //if (ai != null)
                                    //{
                                    //    string prefix = ai.MobilePhonePrefix;
                                    //    string phone = ai.MobilePhone;
                                    //    string otpreturn = OTPUtils.ResetAndCreateOTP(ac.ID, prefix, phone, 1);
                                    //    if (otpreturn != null)
                                    //    {
                                    //        string message = MessageController.GetByType(1).Message + " " + otpreturn;
                                    //        string kq = ESMS.Send(prefix + phone, message);
                                    //        Response.Redirect("/OTP");
                                    //    }
                                    //}
                                }
                                else if (ac.Status == 2)
                                {
                                    //Đã kích hoạt
                                    Session["userLoginSystem"] = ac.Username;
                                    if (setNoti != null)
                                    {
                                        if (setNoti.IsSentNotiAdmin == true)
                                        {
                                            var admins = AccountController.GetAllByRoleID(0);
                                            if (admins.Count > 0)
                                            {
                                                foreach (var admin in admins)
                                                {
                                                    NotificationsController.Inser(admin.ID,
                                                                                       admin.Username, 0,
                                                                                       "Khách hàng mới có username là: " + ac.Username,
                                                                                       6, currentDate, ac.Username, false);
                                                }
                                            }

                                            var managers = AccountController.GetAllByRoleID(2);
                                            if (managers.Count > 0)
                                            {
                                                foreach (var manager in managers)
                                                {
                                                    NotificationsController.Inser(manager.ID,
                                                                                       manager.Username, 0,
                                                                                       "Khách hàng mới có username là: " + ac.Username,
                                                                                       6, currentDate, ac.Username, false);
                                                }
                                            }
                                        }

                                        if (setNoti.IsSentNotiUser == true)
                                        {
                                            NotificationsController.Inser(ac.ID, ac.Username, 0,
                                                                                       "<a href=\"/thong-tin-nguoi-dung\" target=\"_blank\">Thông tin tài khoản.</a>",
                                                                                       6, currentDate, ac.Username, false);
                                        }

                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            var admins = AccountController.GetAllByRoleID(0);
                                            if (admins.Count > 0)
                                            {
                                                foreach (var admin in admins)
                                                {
                                                    try
                                                    {
                                                        PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", admin.Email,
                                                            "Thông báo tại Nhập Hàng TQ", "Khách hàng mới có username là: " + ac.Username, "");
                                                    }
                                                    catch { }
                                                }
                                            }

                                            var managers = AccountController.GetAllByRoleID(2);
                                            if (managers.Count > 0)
                                            {
                                                foreach (var manager in managers)
                                                {
                                                    try
                                                    {
                                                        PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", manager.Email,
                                                            "Thông báo tại Nhập Hàng TQ", "Khách hàng mới có username là: " + ac.Username, "");
                                                    }
                                                    catch { }
                                                }
                                            }
                                        }

                                        if (setNoti.IsSendEmailUser == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", ac.Email,
                                                    "Thông báo tại Nhập Hàng TQ", "Chào mừng bạn đã đến với Nhập Hàng TQ", "");
                                            }
                                            catch { }
                                        }
                                    }
                                    //var admins = AccountController.GetAllByRoleID(0);
                                    //if (admins.Count > 0)
                                    //{
                                    //    foreach (var admin in admins)
                                    //    {
                                    //        NotificationController.Inser(ac.ID, ac.Username, admin.ID,
                                    //                                           admin.Username, 0,
                                    //                                           "Khách hàng mới có username là: " + ac.Username, 0,
                                    //                                           6, currentDate, ac.Username);
                                    //    }
                                    //}

                                    //var managers = AccountController.GetAllByRoleID(2);
                                    //if (managers.Count > 0)
                                    //{
                                    //    foreach (var manager in managers)
                                    //    {
                                    //        NotificationController.Inser(ac.ID, ac.Username, manager.ID,
                                    //                                           manager.Username, 0,
                                    //                                           "Khách hàng mới có username là: " + ac.Username, 0,
                                    //                                           6, currentDate, ac.Username);
                                    //    }
                                    //}
                                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                    hubContext.Clients.All.addNewMessageToPage("", "");
                                    Response.Redirect("/trang-chu");
                                }
                                else if (ac.Status == 3)
                                {
                                    //Đã Block
                                }
                            }
                            else
                            {
                                //lblError.Text = "Đăng nhập không thành công, vui lòng kiểm tra lại.";
                                //lblError.Visible = true;
                            }
                            //Response.Redirect("/Login.aspx");
                        }
                    }
                }
                else
                {
                    lblError.Text = error;
                    lblError.Visible = true;
                }
            }

        }
    }
}