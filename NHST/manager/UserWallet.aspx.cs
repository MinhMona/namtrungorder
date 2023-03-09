using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;

namespace NHST.manager
{
    public partial class UserWallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID == 0 || ac.RoleID == 7 || ac.RoleID == 2)
                    {

                    }
                    else
                        Response.Redirect("/trang-chu");
                }
                loaddata();
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int role = ac.RoleID.ToString().ToInt();

                if (role == 0 || role == 2 || ac.RoleID == 7)
                    pbadmin.Visible = true;
                else
                    pbadmin.Visible = false;

                ViewState["UID"] = id;
                var a = AccountController.GetByID(id);
                if (a != null)
                {
                    lblUsername.Text = a.Username;
                    pContent.Content = a.Username + " đã được nạp tiền vào tài khoản.";
                }
                else
                {
                    Response.Redirect("/manager/Home.aspx");
                }
            }
        }


        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            int role = 0;
            var u_loginin = AccountController.GetByUsername(username_current);
            if (u_loginin != null)
            {
                role = u_loginin.RoleID.ToString().ToInt(0);
            }
            double money = Convert.ToDouble(pWallet.Value);
            int UID = ViewState["UID"].ToString().ToInt(0);
            var user_wallet = AccountController.GetByID(UID);
            int status = ddlStatus.SelectedValue.ToString().ToInt(1);
            string content = pContent.Content;
            DateTime currentdate = DateTime.Now;
            string BackLink = "";
            if (role == 7)
                BackLink = "/manager/historysendwalletaccountant.aspx";
            else
                BackLink = "/manager/HistorySendWallet.aspx";
            if (money > 0)
            {
                if (user_wallet != null)
                {
                    double wallet = Convert.ToDouble(user_wallet.Wallet);
                    wallet = wallet + money;
                   
                    #region cách mới
                    if (status == 2)
                    {
                        AdminSendUserWalletController.Insert(user_wallet.ID, user_wallet.Username, money, status, content, currentdate, username_current);
                        AccountController.updateWallet(user_wallet.ID, wallet, currentdate, username_current);
                        if (string.IsNullOrEmpty(content))
                            HistoryPayWalletController.Insert(user_wallet.ID, user_wallet.Username, 0, money, user_wallet.Username + " đã được nạp tiền vào tài khoản.", wallet, 2, 4, currentdate, username_current);
                        else
                            HistoryPayWalletController.Insert(user_wallet.ID, user_wallet.Username, 0, money, content, wallet, 2, 4, currentdate, username_current);

                        NotificationController.Inser(u_loginin.ID, u_loginin.Username, 
                                    Convert.ToInt32(user_wallet.ID),
                                   user_wallet.Username, 0, 
                                   "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.</a>", 0,
                                   2, currentdate, u_loginin.Username, false);

                        var setNoti = SendNotiEmailController.GetByID(3);
                        if (setNoti != null)
                        {
                            if (setNoti.IsSentNotiUser == true)
                            {
                                NotificationsController.Inser(Convert.ToInt32(user_wallet.ID),
                                                        user_wallet.Username, 0,
                                                        "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.</a>",
                                                        2, currentdate, u_loginin.Username, false);
                            }

                            if (setNoti.IsSendEmailUser == true)
                            {
                                try
                                {
                                    PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", user_wallet.Email,
                                        "Thông báo tại namtrungorder.com.", "Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.", "");
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        AdminSendUserWalletController.Insert(user_wallet.ID, user_wallet.Username, money, status, content, currentdate, username_current);
                    }
                    #endregion

                    PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo lệnh nạp tiền thành công.", "s", true, BackLink, Page);
                    //if(role == 7)
                    //    Response.Redirect("/Admin/historysendwalletaccountant.aspx");
                    //else
                    //    Response.Redirect("/Admin/HistorySendWallet.aspx");
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập số tiền lớn hơn 0.", "e", true, Page);
            }
        }
    }
}