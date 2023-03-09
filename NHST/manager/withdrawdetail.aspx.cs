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
    public partial class withdrawdetail : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 7 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                    loaddata();
                }
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var w = WithdrawController.GetByID(id);
                if (w != null)
                {
                    ViewState["ID"] = id;
                    int status = w.Status.ToString().ToInt(1);
                    lblUsername.Text = w.Username;
                    pAmount.Value = w.Amount;
                    ddlStatus.SelectedValue = status.ToString();
                    txtNote.Text = w.Note;
                    if (status == 1)
                        ddlStatus.Enabled = true;
                    else
                        ddlStatus.Enabled = false;
                }
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            int role = 0;
            var u_loginin = AccountController.GetByUsername(username);
            if (u_loginin != null)
            {
                role = u_loginin.RoleID.ToString().ToInt(0);
            }
            DateTime currentDate = DateTime.Now;
            int id = ViewState["ID"].ToString().ToInt();
            var w = WithdrawController.GetByID(id);
            string BackLink = "/manager/Withdraw-List.aspx";
            if (w != null)
            {
                if (w.Status < 3)
                {
                    int status = ddlStatus.SelectedValue.ToString().ToInt(1);
                    if (status == 2)
                    {
                        double amount = Convert.ToDouble(w.Amount.ToString());
                        var acc = AccountController.GetByUsername(username);
                        //if (acc.RoleID == 7)
                        //{
                        //    PJUtils.ShowMessageBoxSwAlertBackToLink("Bạn không có quyền duyệt yêu cầu này", "e", true, BackLink, Page);
                        //}
                        //else if (acc.RoleID == 0)
                        //{
                            WithdrawController.UpdateStatus(id, status, currentDate, username);
                            NotificationController.Inser(u_loginin.ID, u_loginin.Username, Convert.ToInt32(w.UID), w.Username, 0,
                                   "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được rút " + string.Format("{0:N0}", amount) + " VNĐ ra khỏi tài khoản.</a>", 0,
                                   3, currentDate, u_loginin.Username, false);
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                       // }
                    }
                    else if (status == 3)
                    {
                        int uid_rut = w.UID.ToString().ToInt();
                        var user_rut = AccountController.GetByID(uid_rut);
                        if (user_rut != null)
                        {
                            double wallet = Convert.ToDouble(user_rut.Wallet.ToString());
                            double amount = Convert.ToDouble(w.Amount.ToString());

                            double newwallet = wallet + amount;

                            //Cập nhật lại ví
                            AccountController.updateWallet(uid_rut, newwallet, currentDate, username);

                            //Thêm vào History Pay wallet
                            HistoryPayWalletController.Insert(uid_rut, username, 0, amount, "Admin Hủy lệnh Rút tiền", newwallet, 2, 6, currentDate, username);

                            //Thêm vào lệnh rút tiền
                            WithdrawController.UpdateStatus(id, 3, currentDate, username);
                        }
                        WithdrawController.UpdateStatus(id, status, currentDate, username);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                    }
                    else
                    {
                        WithdrawController.UpdateStatus(id, status, currentDate, username);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                    }
                }
            }
        }
    }
}