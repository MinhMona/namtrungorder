using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class nap_tien_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadDLL();
            }
        }

        public void LoadDLL()
        {
            List<Bank> lb = new List<Bank>();
            var bank = BankController.GetAll();
            if (bank.Count > 0)
            {
                foreach (var item in bank)
                {
                    Bank nb = new Bank();
                    nb.ID = item.ID;
                    nb.BankInfo = item.BankName + " - " + item.AccountHolder + " - " + item.BankNumber + " - " + item.Branch;
                    lb.Add(nb);
                }
            }

            if (lb.Count > 0)
            {
                ddlBank.DataSource = lb;
                ddlBank.DataBind();
            }
        }

        public class Bank
        {
            public int ID { get; set; }
            public string BankInfo { get; set; }
        }

        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt(0);

            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    ViewState["UID"] = UID;
                    pnMobile.Visible = true;
                    var user = AccountController.GetByID(UID);
                    if (user != null)
                    {
                        //ltrBalance.Text = string.Format("{0:N0}", user.Wallet) + " vnđ";
                        ltrIfn.Text = user.Username;
                    }
                }
                else
                {
                    pnShowNoti.Visible = true;
                }
            }
            else
            {
                pnShowNoti.Visible = true;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            DateTime currentDate = DateTime.Now;
            int UID = ViewState["UID"].ToString().ToInt(0);
            var u = AccountController.GetByID(UID);
            if (u != null)
            {
                string kq = AdminSendUserWalletController.Insert(u.ID, u.Username, Convert.ToDouble(pAmount.Value), 1, txtNote.Text, currentDate, u.Username);
                if (kq.ToInt(0) > 0)
                {
                    var setNoti = SendNotiEmailController.GetByID(3);
                    if (setNoti != null)
                    {
                        if (setNoti.IsSentNotiAdmin == true)
                        {

                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    NotificationsController.Inser(admin.ID, admin.Username, kq.ToInt(), "Có yêu cầu nạp tiền mới.", 2, currentDate, u.Username, false);
                                    //string strPathAndQuery = Request.Url.PathAndQuery;
                                    //string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    //string datalink = "" + strUrl + "manager/HistorySendWallet/";
                                    //PJUtils.PushNotiDesktop(admin.ID, " có yên cầu nạp tiền mới.", datalink);
                                }
                            }

                            var managers = AccountController.GetAllByRoleID(2);
                            if (managers.Count > 0)
                            {
                                foreach (var manager in managers)
                                {


                                    NotificationsController.Inser(manager.ID, manager.Username, kq.ToInt(), "Có yêu cầu nạp tiền mới.",
                                    1, currentDate, u.Username, false);
                                    //string strPathAndQuery = Request.Url.PathAndQuery;
                                    //string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    //string datalink = "" + strUrl + "manager/HistorySendWallet/";
                                    //PJUtils.PushNotiDesktop(manager.ID, " có yên cầu nạp tiền mới.", datalink);
                                }
                            }
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
                                            "Thông báo tại Nhập Hàng TQ.", "Có yêu cầu nạp tiền mới.", "");
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
                                            "Thông báo tại Nhập Hàng TQ.", "Có yêu cầu nạp tiền mới.", "");
                                    }
                                    catch { }
                                }
                            }

                        }
                    }
                    PJUtils.ShowMessageBoxSwAlert("Gửi thông tin thành công, vui lòng chờ admin kiểm duyệt", "s", true, Page);
                }
            }
        }
    }
}