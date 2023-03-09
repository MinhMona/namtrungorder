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
    public partial class HistorySendWalletDetail : System.Web.UI.Page
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
                    if (ac.RoleID == 0 || ac.RoleID == 2 || ac.RoleID == 7)
                    {
                        loaddata();
                    }
                    else
                        Response.Redirect("/trang-chu");
                }
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var h = AdminSendUserWalletController.GetByID(id);
                if (h != null)
                {
                    ViewState["ID"] = id;
                    ViewState["UID"] = h.UID;
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    int role = ac.RoleID.ToString().ToInt();

                    if (role == 0 || role == 2 || role == 7)
                        pbadmin.Visible = true;
                    else
                        pbadmin.Visible = false;
                    //if (role == 0 || role == 2)
                    //    pbadmin.Visible = true;
                    //else
                    //    pbadmin.Visible = false;
                    if (h.Status == 1)
                        ddlStatus.Enabled = true;
                    else
                        ddlStatus.Enabled = false;

                    lblUsername.Text = h.Username;
                    pContent.Content = h.TradeContent;
                    pWallet.Value = h.Amount;
                    ddlStatus.SelectedValue = h.Status.ToString();
                }
                else
                    Response.Redirect("/manager/HistorySendWallet.aspx");
            }
            else
                Response.Redirect("/manager/HistorySendWallet.aspx");
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
            int id = ViewState["ID"].ToString().ToInt(0);
            DateTime currentdate = DateTime.Now;
            string content = pContent.Content;
            var h = AdminSendUserWalletController.GetByID(id);
            string BackLink = "/manager/HistorySendWallet.aspx";
            if (h != null)
            {
                if (h.Status == 2 || h.Status == 3)
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
                }
                else
                {
                    if (money > 0)
                    {
                        if (user_wallet != null)
                        {
                            double wallet = Convert.ToDouble(user_wallet.Wallet);
                            wallet = wallet + money;
                            if (role == 0 || role == 2 || role == 7)
                            {
                                if (status == 2)
                                {
                                    AdminSendUserWalletController.UpdateStatus(id, status, content, currentdate, username_current);
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
                                }
                                else
                                {
                                    AdminSendUserWalletController.UpdateStatus(id, status, content, currentdate, username_current);
                                }
                            }
                            //else
                            //{
                            //    AdminSendUserWalletController.Insert(user_wallet.ID, user_wallet.Username, money, 1, currentdate, username_current);
                            //}
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
                            //Response.Redirect("/Admin/HistorySendWallet.aspx");
                        }
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập số tiền lớn hơn 0.", "e", true, Page);
                    }
                }
            }

        }
    }
}