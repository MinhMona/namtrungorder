using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;

namespace NHST.manager
{
    public partial class SmsForward : System.Web.UI.Page
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
                        //loaddata();
                    }
                    else
                        Response.Redirect("/trang-chu");

                    if (ac.RoleID != 0)
                    {
                        btnExcel.Visible = false;
                    }
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = SmsForwardController.GetAllBySQL(tContent.Text, tBank.Text, rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString());
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
                }
            }

            //double total = SmsForwardController.GetTotalPrice(tContent.Text, tBank.Text, rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString());
            //lblTotalPrice.Text = string.Format("{0:N0}", total);

        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion
        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion


        #region Webservice
        [WebMethod]
        public static string GetData(int ID)
        {
            var nap = SmsForwardController.GetByID(ID);
            if (nap != null)
            {
                NaptienInfo n = new NaptienInfo();
                n.ID = nap.ID;
                double Amount = Convert.ToDouble(nap.so_tien);
                n.ten_bank = nap.ten_bank;
                n.so_tien = string.Format("{0:N0}", Amount);
                if (!string.IsNullOrEmpty(nap.noi_dung))
                    n.noi_dung = nap.noi_dung;
                //DateTime currentDate = DateTime.Now;
                //string CreateDate = "Ngày " + currentDate.Day + " tháng " + currentDate.Month + " năm " + currentDate.Year;
                //n.CreateDate = CreateDate;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(n);
            }
            return "null";
        }


        public class NaptienInfo
        {
            public string noi_dung { get; set; }
            public string so_tien { get; set; }
            public string ten_bank { get; set; }
            public int ID { get; set; }
        }
        #endregion


        [WebMethod]
        public static string CheckUsername(string Username, int ID)
        {
            DateTime currentdate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);

                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 7)
                    {

                        var u = AccountController.GetByUsername(Username);
                        if (u != null)
                        {
                            var e = SmsForwardController.GetByID(ID);
                            if (e != null)
                            {

                                var checkphone = AccountInfoController.GetByUID(u.ID);
                                if (checkphone != null)
                                {
                                    double money = Convert.ToDouble(e.so_tien);
                                    double wallet = Math.Round(Convert.ToDouble(u.Wallet), 0);
                                    money = Math.Round(money, 0);
                                    wallet = wallet + money;
                                    wallet = Math.Round(wallet, 0);


                                    SmsForwardController.updateStatus(e.ID, 2, 1);
                                    AdminSendUserWalletController.InsertNew(u.ID, u.Username, money, 2, e.noi_dung, currentdate, user_check.Username, e.ID);
                                    AccountController.updateWallet(u.ID, wallet, currentdate, user_check.Username);

                                    HistoryPayWalletController.Insert(u.ID, u.Username, 0, money, u.Username + " đã được nạp tiền vào tài khoản.", wallet, 2, 4, currentdate, user_check.Username);

                                    NotificationController.Inser(u.ID, u.Username,
                                                Convert.ToInt32(u.ID),
                                               u.Username, 0,
                                               "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.</a>", 0,
                                               2, currentdate, user_check.Username, false);

                                    var setNoti = SendNotiEmailController.GetByID(3);
                                    if (setNoti != null)
                                    {
                                        if (setNoti.IsSentNotiUser == true)
                                        {
                                            NotificationsController.Inser(Convert.ToInt32(u.ID),
                                                                    u.Username, 0,
                                                                    "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.</a>",
                                                                    2, currentdate, user_check.Username, false);
                                        }

                                        if (setNoti.IsSendEmailUser == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", u.Email,
                                                    "Thông báo tại namtrungorder.com.", "Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.", "");
                                            }
                                            catch { }
                                        }
                                    }

                                }
                                return "success";
                            }
                            else
                                return "notrightSMS";

                        }
                        else
                            return "notrightusername";

                    }
                    else
                        return "none";
                }
                else
                {
                    return "none";
                }
            }
            else
            {
                return "none";
            }
        }


        [WebMethod]
        public static string Cancel1(int ID)
        {
            DateTime currentdate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);

                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 7)
                    {


                        var e = SmsForwardController.GetByID(ID);
                        if (e != null)
                        {
                            SmsForwardController.updateStatus(e.ID, 3, 0);

                            return "success";
                        }
                        else
                            return "notrightSMS";



                    }
                    else
                        return "none";
                }
                else
                {
                    return "none";
                }
            }
            else
            {
                return "none";
            }
        }



        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac.RoleID == 0)
            {
                var la = SmsForwardController.GetAll(tBank.Text, tContent.Text);
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ID</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ngân hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số tiền</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Thời gian nhận tiền</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Thời gian hệ thống</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Nội dung</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in la)
                {
                    string symbol = "-";
                    if (item.Type == 1)
                        symbol = "";
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.ID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.ten_bank + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + symbol + string.Format("{0:N0}", item.so_tien) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.thoi_gian + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + PJUtils.ReturnStatusWithdraw_TruyVan(Convert.ToInt32(item.Status)) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.noi_dung + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Truy-Van-Nap-Tien.xls";
                string strcontentType = "application/vnd.ms-excel";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.ContentType = strcontentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.Write(StrExport.ToString());
                Response.Flush();
                //Response.Close();
                Response.End();
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Bạn không có quyền xuất file excel!", "e", false, Page);
            }


        }

    }
}