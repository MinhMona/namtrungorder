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
    public partial class add_withdraw : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            if(Request.QueryString["u"]!=null)
            {
                int id = Request.QueryString["u"].ToInt(0);

                var a = AccountController.GetByID(id);
                if (a != null)
                {
                    lblUsername.Text = a.Username;
               
                 
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            int role = 0;
            var u_loginin = AccountController.GetByUsername(username_current);
            if (u_loginin != null)
            {
                role = u_loginin.RoleID.ToString().ToInt(0);
            }
            string username_bitru = lblUsername.Text;
            var acc = AccountController.GetByUsername(username_bitru);
            string BackLink = "/manager/Withdraw-List.aspx";
            if (acc != null)
            {
                int UID = acc.ID;
                double wallet = Convert.ToDouble(acc.Wallet);
                double amount = Convert.ToDouble(pAmount.Value);
                //int status = ddlStatus.SelectedValue.ToInt();
                DateTime currentDate = DateTime.Now;
                if (wallet >= amount)
                {
                    //Cho rút
                    double leftwallet = wallet - amount;

                    //Cập nhật lại ví
                    AccountController.updateWallet(UID, leftwallet, currentDate, username_current);

                    //Thêm vào History Pay wallet
                    HistoryPayWalletController.Insert(UID, username_bitru, 0, amount, "Rút tiền", leftwallet, 1, 5, currentDate, username_current);

                    //Thêm vào lệnh rút tiền
                    WithdrawController.Insert(UID, username_bitru, amount, 2, txtNote.Text, currentDate, username_current);
                    NotificationController.Inser(u_loginin.ID, u_loginin.Username,
                                    Convert.ToInt32(acc.ID),
                                   acc.Username, 0,
                                   "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được rút " + string.Format("{0:N0}", amount) + " VNĐ ra khỏi tài khoản.</a>", 0,
                                   3, currentDate, u_loginin.Username, false);
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo lệnh rút tiền thành công", "s", true, BackLink, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số tiền không đủ để tạo lệnh rút", "e", true, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Không tồn tại tài khoản trên", "e", true, Page);
            }
        }
    }
}