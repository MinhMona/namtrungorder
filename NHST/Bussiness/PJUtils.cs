using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;
using System.Web.Caching;
using System.Text;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.Security.Cryptography;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web.Script.Serialization;
using WebUI.Business;
using MB.Extensions;
using NHST.Controllers;
using NHST.Models;
using Supremes;
using System.Drawing;
using System.Drawing.Text;

namespace NHST.Bussiness
{
    public class PJUtils
    {
        public static string Encrypt(string key, string data)
        {
            data = data.Trim();
            byte[] keydata = Encoding.ASCII.GetBytes(key);
            string md5String = BitConverter.ToString(new
            MD5CryptoServiceProvider().ComputeHash(keydata)).Replace("-", "").ToLower();
            byte[] tripleDesKey = Encoding.ASCII.GetBytes(md5String.Substring(0, 24));
            TripleDES tripdes = TripleDESCryptoServiceProvider.Create();
            tripdes.Mode = CipherMode.ECB;
            tripdes.Key = tripleDesKey;
            tripdes.GenerateIV();
            MemoryStream ms = new MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, tripdes.CreateEncryptor(),
            CryptoStreamMode.Write);
            encStream.Write(Encoding.ASCII.GetBytes(data), 0,
            Encoding.ASCII.GetByteCount(data));
            encStream.FlushFinalBlock();
            byte[] cryptoByte = ms.ToArray();
            ms.Close();
            encStream.Close();
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0)).Trim();
        }

        public static string Decrypt(string key, string data)
        {
            byte[] keydata = System.Text.Encoding.ASCII.GetBytes(key);
            string md5String = BitConverter.ToString(new
            MD5CryptoServiceProvider().ComputeHash(keydata)).Replace("-", "").ToLower();
            byte[] tripleDesKey = Encoding.ASCII.GetBytes(md5String.Substring(0, 24));
            TripleDES tripdes = TripleDESCryptoServiceProvider.Create();
            tripdes.Mode = CipherMode.ECB;
            tripdes.Key = tripleDesKey;
            byte[] cryptByte = Convert.FromBase64String(data);
            MemoryStream ms = new MemoryStream(cryptByte, 0, cryptByte.Length);
            ICryptoTransform cryptoTransform = tripdes.CreateDecryptor();
            CryptoStream decStream = new CryptoStream(ms, cryptoTransform,
            CryptoStreamMode.Read);
            StreamReader read = new StreamReader(decStream);
            return (read.ReadToEnd());
        }

        public static bool SendMail(string strFrom, string pass, string strTo, string strSubject, string strMsg, string cc)
        {
            try
            {
                // Create the mail message
                MailMessage objMailMsg = new MailMessage(strFrom, strTo);

                objMailMsg.BodyEncoding = Encoding.UTF8;
                objMailMsg.Subject = strSubject;
                objMailMsg.CC.Add(cc);
                objMailMsg.IsBodyHtml = true;
                objMailMsg.Body = strMsg;
                SmtpClient objSMTPClient = new SmtpClient();

                objSMTPClient.Host = "202.43.110.136";
                objSMTPClient.Port = 25;
                objSMTPClient.EnableSsl = false;
                objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                objSMTPClient.Credentials = new NetworkCredential(strFrom, pass);
                objSMTPClient.Timeout = 20000;
                objSMTPClient.Send(objMailMsg);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool SendMailGmail(string strFrom, string pass, string strTo, string strSubject, string strMsg, string cc)
        {
            //try
            //{
            string fromAddress = strFrom;
            string mailPassword = pass;       // Mail id password from where mail will be sent.
            string messageBody = strMsg;

            // Create smtp connection.
            SmtpClient client = new SmtpClient();
            client.Port = 587;//outgoing port for the mail.
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);


            // Fill the mail form.
            var send_mail = new MailMessage();
            send_mail.IsBodyHtml = true;
            //address from where mail will be sent.
            send_mail.From = new MailAddress(strFrom);
            //address to which mail will be sent.           
            send_mail.To.Add(new MailAddress(strTo));
            //subject of the mail.
            send_mail.Subject = strSubject;
            send_mail.Body = messageBody;
            client.Send(send_mail);

            return true;

        }
        //public static bool SendMailGmail(string strFrom, string pass, string strTo, string strSubject, string strMsg, string cc)
        //{
        //    try
        //    {
        //        string fromAddress = strFrom;
        //        string mailPassword = pass;       // Mail id password from where mail will be sent.
        //        string messageBody = strMsg;

        //        // Create smtp connection.
        //        SmtpClient client = new SmtpClient();
        //        client.Port = 587;//outgoing port for the mail.
        //        client.Host = "103.15.48.25";
        //        client.EnableSsl = false;
        //        client.Timeout = 10000;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);


        //        // Fill the mail form.
        //        var send_mail = new MailMessage();
        //        send_mail.IsBodyHtml = true;
        //        //address from where mail will be sent.
        //        send_mail.From = new MailAddress(strFrom);
        //        //address to which mail will be sent.           
        //        send_mail.To.Add(new MailAddress(strTo));
        //        //subject of the mail.
        //        send_mail.Subject = strSubject;
        //        send_mail.Body = messageBody;
        //        client.Send(send_mail);



        //        // Create the mail message
        //        //MailMessage objMailMsg = new MailMessage(strFrom, strTo);

        //        //objMailMsg.BodyEncoding = Encoding.UTF8;
        //        //objMailMsg.Subject = strSubject;
        //        ////objMailMsg.CC.Add(cc);
        //        //objMailMsg.IsBodyHtml = true;
        //        //objMailMsg.Body = strMsg;
        //        //SmtpClient objSMTPClient = new SmtpClient();

        //        //objSMTPClient.Host = "smtp.gmail.com";
        //        //objSMTPClient.Port = 587;
        //        //objSMTPClient.EnableSsl = true;
        //        //objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //        //objSMTPClient.Credentials = new NetworkCredential(strFrom, pass);
        //        //objSMTPClient.Timeout = 20000;
        //        //objSMTPClient.Send(objMailMsg);
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {

                string filename = "ProjectReport_" + DateTime.Now.Date + ".xls";

                string excelHeader = "Project Report";

                System.IO.StringWriter tw = new System.IO.StringWriter();

                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                DataGrid dgGrid = new DataGrid();

                dgGrid.DataSource = dt;

                dgGrid.DataBind();

                // Report Header
                hw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                hw.WriteLine("<b><u><font size='3'> " + excelHeader + " </font></u></b>");

                //Get the HTML for the control.

                dgGrid.RenderControl(hw);

                //Write the HTML back to the browser.

                //Response.ContentType = “application/vnd.ms-excel”;

                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //this.EnableViewState = false;

                HttpContext.Current.Response.Write(tw.ToString());

                HttpContext.Current.Response.End();
            }
        }
        public static string ReturnStatusPayHelp(int status)
        {
            if (status == 2)
            {
                return "<span class='bg-black'>Đã hủy</span>";
            }
            else if (status == 0)
            {
                return "<span class='bg-red'>Chưa thanh toán</span>";
            }
            else if (status == 1)
            {
                return "<span class='bg-blue'>Đã thanh toán</span>";
            }
            else if (status == 3)
            {
                return "<span class='bg-green'>Đã hoàn thành</span>";
            }
            else
            {
                return "<span class='bg-yellow'>Đã xác nhận</span>";
            }
        }
        public static string ReturnIsNotComplete(bool check)
        {
            if (check == true)
            {
                return "<input type=\"checkbox\" disabled checked>";
            }
            else
                return "<input type=\"checkbox\" disabled>";
        }
        public static void ShowMsg(string txt, bool? isRefresh, System.Web.UI.Page page)
        {
            //isRefresh = isRefresh == null;
            var content = txt;
            var _type = string.Empty;
            switch (txt.Trim().ToLower())
            {
                case "100":
                    content = "Tên hoặc mã đã được sử dụng";
                    _type = "i";
                    isRefresh = false;
                    break;
                case "101":
                    content = "Không tìm thấy đối tượng";
                    _type = "i";
                    isRefresh = false;
                    break;
                case "102":
                    content = "Thực hiện thành công !";
                    _type = "";
                    isRefresh = true;
                    break;
                case "103":
                    content = "Thực hiện thất bại !";
                    _type = "e";
                    isRefresh = false;
                    break;
            }
            ShowMessageBoxSwAlert(content, _type, isRefresh, page);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="type">e: Error,i: warning, default: succes</param>
        /// <param name="isRefresh"></param>
        /// <param name="page"></param>
        /// 
        public static void ShowMessageBoxSwAlert(string txt, string type, bool? isRefresh, System.Web.UI.Page page)
        {
            txt = new JavaScriptSerializer().Serialize(txt);
            string p;
            switch (type)
            {

                case "e":
                    p = "error";
                    break;
                case "i":
                    p = "warning";
                    break;

                default:
                    p = "success";
                    break;
            }
            JavaScript.AfterPageLoad(page).ExecuteCustomScript("swal({ title: 'Thông báo',text:' " + txt + "',type: '" + p + "'}" + (Convert.ToBoolean(isRefresh.ToString()) ? ", function () { window.location.replace(window.location.href); });" : ");"));
        }
        public static void ShowMessageBoxSwAlertBackToLink(string txt, string type, bool? isRefresh, string BackLink, System.Web.UI.Page page)
        {
            txt = new JavaScriptSerializer().Serialize(txt);
            string p;
            switch (type)
            {
                case "e":
                    p = "error";
                    break;
                case "i":
                    p = "warning";
                    break;

                default:
                    p = "success";
                    break;
            }
            JavaScript.AfterPageLoad(page).ExecuteCustomScript("swal({ title: 'Thông báo',text:' " + txt + "',type: '" + p + "'}" + (Convert.ToBoolean(isRefresh.ToString()) ? ", function () { window.location.replace('" + BackLink + "'); });" : ");"));
        }
        public static string GetIcon(object o)
        {
            if (o == null)
                return "/NO-IMAGE.jpg";
            if (!string.IsNullOrEmpty(o.ToString()))
                return o.ToString();
            return "/NO-IMAGE.jpg";
        }
        public static string SubString(string title, int length)
        {
            if (string.IsNullOrEmpty(title))
                return "";

            if (!title.Contains(" "))
            {
                if (title.Length > length)
                    title = title.Substring(0, length - 1) + "...";
            }
            else if (title.Length >= length)
            {
                int i = length - 1;
                while (title.Substring(i--, 1) != " " && i > 0) ;
                if (i == 0)
                    return title.Substring(0, length - 4) + " ...";
                else
                    return title.Substring(0, i + 1) + " ...";
            }

            return title;
        }
        public static string RandomString(int numberrandom)
        {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = "0123456789";
            var stringChars = new char[numberrandom];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public static string RandomStringWithText(int numberrandom)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //var chars = "0123456789";
            var stringChars = new char[numberrandom];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public static bool ConvertStringToBool(string i)
        {
            if (!string.IsNullOrEmpty(i))
            {
                i = i.ToLower();
                if (i == "1" || i == "true")
                    return true;
                else return false;
            }
            else return false;

        }
        public static string StatusToRequest(object i)
        {
            if (i != null)
            {
                if (i.ToString() == "1")
                {
                    return "<span class='yellow'>Chưa kích hoạt</span>";
                }
                else if (i.ToString() == "2")
                {
                    return "<span class='blue'>Đã kích hoạt</span>";
                }
                else
                {
                    return "<span class='red'>Đang bị khóa</span>";
                }

            }
            else return "<span class='red'>Đang bị khóa</span>";
        }
        public static string IntToRequestAdmin(int i)
        {
            if (i == 0)
                return "<span class=\"bg-red\">Chờ đặt cọc</span>";
            else if (i == 1)
                return "<span class=\"bg-black\">Hủy đơn hàng</span>";
            else if (i == 2)
                return "<span class=\"bg-bronze\">Chờ mua hàng</span>";
            else if (i == 3)
                return "<span class=\"bg-green\">Chờ duyệt đơn</span>";
            else if (i == 4)
                return "<span class=\"bg-green\">Đã duyệt đơn</span>";
            else if (i == 5)
                return "<span class=\"bg-green\">Chờ shop TQ phát hàng</span>";
            else if (i == 6)
                return "<span class=\"bg-green\">Đã về kho TQ</span>";
            else if (i == 7)
                return "<span class=\"bg-orange\">Đã về kho VN</span>";
            else if (i == 8)
                return "<span class=\"bg-yellow\">Chờ thanh toán</span>";
            else if (i == 9)
                return "<span class=\"bg-blue\">Khách đã thanh toán</span>";
            else if (i == 10)
                return "<span class=\"bg-blue\">Đã hoàn thành</span>";
            else if (i == 11)
                return "<span class=\"bg-pink\">Shop đã phát hàng</span>";
            else if (i == 12)
                return "<span class=\"bg-red\">Đang mua hàng</span>";
            else if (i == 13)
                return "<span class=\"bg-green\">Chờ shop TQ phát hàng</span>";
            else
                return "";

        }
        public static string IntToRequestAdminReturnBG(int i)
        {
            if (i == 0)
                return "bg-red";
            else if (i == 1)
                return "bg-black";
            else if (i == 2)
                return "bg-bronze";
            else if (i == 3)
                return "bg-green";
            else if (i == 4)
                return "bg-green";
            else if (i == 5)
                return "bg-green";
            else if (i == 6)
                return "bg-green";
            else if (i == 7)
                return "bg-orange";
            else if (i == 8)
                return "bg-yellow";
            else if (i == 9)
                return "bg-blue";
            else if (i == 10)
                return "bg-blue";
            else
                return "";

        }
        public static string IntToRequestClient(int i)
        {
            //if (i == 0)
            //    return "<span class=\"bg-red\">Chờ đặt cọc</span>";
            //else if (i == 1)
            //    return "<span class=\"bg-black\">Hủy đơn hàng</span>";
            //else if (i == 2)
            //    return "<span class=\"bg-bronze\">Chờ mua hàng</span>";
            //else if (i >= 3 && i < 8)
            //    return "<span class=\"bg-green\">Đang xử lý</span>";
            //else if (i == 8)
            //    return "<span class=\"bg-yellow\">Chờ thanh toán</span>";
            //else if (i == 9)
            //    return "<span class=\"bg-blue\">Đã xong</span>";
            //else if (i == 10)
            //    return "<span class=\"bg-blue\">Đã giao hàng</span>";
            //else
            //    return "";
            if (i == 0)
                return "<span class=\"bg-red\">Chờ đặt cọc</span>";
            else if (i == 1)
                return "<span class=\"bg-black\">Hủy đơn hàng</span>";
            else if (i == 2)
                return "<span class=\"bg-bronze\">Chờ mua hàng</span>";
            else if (i == 3)
                return "<span class=\"bg-green\">Chờ duyệt đơn</span>";
            else if (i == 4)
                return "<span class=\"bg-green\">Đã duyệt đơn</span>";
            else if (i == 5)
                return "<span class=\"bg-green\">Chờ shop TQ phát hàng</span>";
            else if (i == 6)
                return "<span class=\"bg-green\">Đang về Việt Nam</span>";
            else if (i == 7)
                return "<span class=\"bg-green\">Đã nhận hàng tại kho đích</span>";
            else if (i == 8)
                return "<span class=\"bg-yellow\">Chờ thanh toán</span>";
            else if (i == 9)
                return "<span class=\"bg-blue\">Khách đã thanh toán</span>";
            else if (i == 10)
                return "<span class=\"bg-blue\">Đã hoàn thành</span>";
            else
                return "";
        }
        public static string generateTransportationStatus(int i)
        {
            if (i == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (i == 1)
                return "<span class=\"bg-red\">Chờ duyệt</span>";
            else if (i == 2)
                return "<span class=\"bg-bronze\">Đã duyệt</span>";
            else if (i == 3)
                return "<span class=\"bg-green\">Đang xử lý</span>";
            else if (i == 4)
                return "<span class=\"bg-green\">Đang vận chuyển về kho đích</span>";
            else if (i == 5)
                return "<span class=\"bg-green\">Đã về kho đích</span>";
            else if (i == 6)
                return "<span class=\"bg-green\">Đã thanh toán</span>";
            else
                return "<span class=\"bg-green\">Đã hoàn thành</span>";
        }
        public static string BoolToRequest(object i)
        {
            if (i != null)
            {
                return ConvertStringToBool(i.ToString()) == true ? "<span class='red'>Đang yêu cầu</span>" : "<span class='blue'>Không</span>";
            }
            else return "<span class='blue'>Không</span>";
        }
        public static string ShowStatusPayHistory(int status)
        {
            if (status == 2)
                return "<span class=\"bg-bronze\">Đặt cọc</span>";
            else if (status == 3)
                return "<span class=\"bg-yellow\">Đặt cọc</span>";
            else if (status == 12)
                return "<span class=\"bg-red\">Sản phẩm hết hàng hoặc giảm giá trả lại cọc</span>";
            else
                return "<span class=\"bg-blue\">Thanh toán</span>";
        }
        public static int CheckRoleShowRosePrice()
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                if (ac.RoleID == 6 || ac.RoleID == 4 || ac.RoleID == 5 || ac.RoleID == 8)
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }
        public static string BoolToStatus(string i)
        {
            return ConvertStringToBool(i) == true ? "<span class='show-stat-s'>Hiện</span>" : "<span class='show-stat-w'>Ẩn</span>";

        }
        public static string GetTradeType(int TradeType)
        {
            if (TradeType == 1)
            {
                return "Đặt cọc";
            }
            else if (TradeType == 2)
            {
                return "Nhận lại tiền đặt cọc";
            }
            else if (TradeType == 3)
            {
                return "Thanh toán hóa đơn";
            }
            else if (TradeType == 4)
            {
                return "Admin chuyển tiền";
            }
            else if (TradeType == 5)
            {
                return "Rút tiền";
            }
            else if (TradeType == 6)
            {
                return "Hủy lệnh rút tiền";
            }
            else
            {
                return "...";
            }


        }
        public static string BoolToStatusShow(string i)
        {
            if (!string.IsNullOrEmpty(i))
                return ConvertStringToBool(i) == true ? "<span class='show-stat-w'>Ẩn</span>" : "<span class='show-stat-s'>Hiện</span>";
            else
                return "<span class='show-stat-s'>Hiện</span>";

        }
        public static string ReturnStatusWithdraw(int status)
        {
            if (status == 1)
            {
                return "<span class='bg-red'>Đang chờ duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='bg-blue'>Đã duyệt</span>";
            }
            else
            {
                return "<span class='bg-black'>Hủy lệnh</span>";
            }
        }


        public static string ReturnStatusWithdraw_TruyVan(int status)
        {
            if (status == 1)
            {
                return "<span class='bg-red'>Chưa xử lý</span>";
            }
            else if (status == 2)
            {
                return "<span class='bg-blue'>Đã xử lý</span>";
            }
            else if (status == 3)
            {
                return "<span class='bg-black'>Hủy</span>";
            }
            else
            {
                return "<span class='bg-black'>Không xác định</span>";
            }
        }
        public static string ReturnPosition(int BenefitSide)
        {
            if (BenefitSide == 1)
            {
                return "Trái";
            }
            else
            {
                return "Phải";
            }
        }
        public static string ReturnPlace(int Place)
        {
            if (Place == 1)
            {
                return "Hà Nội";
            }
            else
            {
                return "Việt Trì";
            }
        }
        public static string ReturnHidden(bool IsHidden)
        {
            if (IsHidden == true)
            {
                return "<span class=\"red\">Ẩn</span>";
            }
            else
            {
                return "<span class=\"blue\">Hiện</span>";
            }
        }
        public static string ReturnRoleName(string name)
        {
            if (name == "Store")
            {
                return "<span class='yellow'>Cửa hàng</span>";
            }
            else if (name == "Customer")
            {
                return "<span class=''>Người dùng</span>";
            }
            return name;
        }
        public static string ReturnSymbol(int Type)
        {
            if (Type == 1)
            {
                return "-";
            }
            else
                return "+";
        }
        public static string ReturnStatusComplainRequest(int status)
        {
            if (status == 1)
            {
                return "<span class='bg-red'>Khiếu nại mới</span>";
            }
            else if (status == 2)
            {
                return "<span class='bg-blue'>Đang xử lý</span>";
                //return "<span class='bg-red'>Đang xử lý</span>";
            }
            else if (status == 3)
            {
                return "<span class='bg-yellow'>Chờ hàng về thêm</span>";
            }
            else if (status == 4)
            {
                return "<span class='bg-aqua'>Chờ shop hoàn tiền</span>";
            }
            else if (status == 5)
            {
                return "<span class='bg-green'>Đang đổi trả</span>";
            }
            else if (status == 6)
            {
                return "<span class='bg-bronze'>GD xử lý</span>";
            }
            else if (status == 7)
            {
                return "<span class='bg-orange'>Kế toán xử lý</span>";
            }
            //else if (status == 8)
            //{
            //    return "<span class='bg-red'>Đã hoàn thành</span>";
            //}
            else if (status == 9)
            {
                return "<span class='bg-bronze'>Chờ CSKH hoàn thành</span>";


            }
            else if (status == 10)
            {
                return "<span class='bg-blue'>CSKH xử lý</span>";
            }
            else
            {
                return "<span class='bg-red'>Kết thúc khiếu nại</span>";
            }
        }





        public static string ReturnStatusShippingRequest(int status)
        {
            if (status == 1)
            {
                return "<span class='bg-red'>Chưa duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='bg-yellow'>Đã duyệt</span>";
            }
            else if (status == 3)
            {
                return "<span class='bg-orange'>Đang giao</span>";
            }
            else if (status == 4)
            {
                return "<span class='bg-blue'>Đã giao</span>";
            }
            else
            {
                return "<span class='bg-black'>Đã hủy</span>";
            }
        }
        public static string ReturnStatusRequest(string status)
        {
            if (status == "1")
            {
                return "<span class='red'>Đang chờ</span>";
            }
            else if (status == "2")
            {
                return "<span class='blue'>Đã xử lý</span>";
            }
            else
            {
                return "<span class='orange'>Đã hủy</span>";
            }

        }

        public static string GetFirstProductIMG(string MainorderID)
        {
            var orders = OrderController.GetByMainOrderID(MainorderID.ToInt(0));
            string IMG = "";
            if (orders.Count > 0)
            {
                IMG = orders[0].image_origin;
            }
            return IMG;
        }
        public static List<countries> loadprefix()
        {
            string file = HttpContext.Current.Server.MapPath("~/Models/phonecode.json");
            //deserialize JSON from file  
            string Json = System.IO.File.ReadAllText(file);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var personlist = ser.Deserialize<List<countries>>(Json);
            List<countries> cs = new List<countries>();
            foreach (var item in personlist)
            {
                countries c = new countries();
                c.name = item.name;
                c.dial_code = item.dial_code;
                c.code = item.code;
                cs.Add(c);
            }
            return cs;
        }
        public class countries
        {
            public string name { get; set; }
            public string dial_code { get; set; }
            public string code { get; set; }
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        public static string GetRandomStringByDateTime()
        {
            DateTime dt = DateTime.Now;
            string returnD = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString()
                           + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + dt.Millisecond.ToString();
            return returnD;
        }
        public static string ShowFirstNameByUID(int ID)
        {
            var ui = AccountInfoController.GetByUserID(ID);
            if (ui != null)
            {
                return ui.FirstName;
            }
            else
                return "";
        }
        public static string ShowLastNameByUID(int ID)
        {
            var ui = AccountInfoController.GetByUserID(ID);
            if (ui != null)
            {
                return ui.LastName;
            }
            else
                return "";
        }
        public static string IntToStringStatusPackage(int status)
        {
            if (status == 0)
                return "<span class=\"bg-bronze\">Mới tạo</span>";
            else if (status == 1)
                return "<span class=\"bg-green\">Đang chuyển về VN</span>";
            else if (status == 2)
                return "<span class=\"bg-blue\">Đã nhận hàng tại kho đích</span>";
            else
                return "<span class=\"bg-red\">Đã hủy</span>";
        }
        public static string IntToStringStatusSmallPackage(int status)
        {
            if (status == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"bg-red\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"bg-orange\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"bg-green\">Đã về kho đích</span>";
            else
                return "<span class=\"bg-blue\">Đã giao cho khách</span>";
        }
        public static string IntToStringStatusSmallPackageWithBG(int status)
        {
            if (status == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"bg-red\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"bg-yellow\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"bg-green\">Đã về kho đích</span>";
            else
                return "<span class=\"bg-blue\">Đã giao cho khách</span>";
        }
        public static string ShowPhoneByUID(int ID)
        {
            var ui = AccountInfoController.GetByUserID(ID);
            if (ui != null)
            {
                return ui.MobilePhonePrefix + ui.MobilePhone;
            }
            else
                return "";
        }
        public static string RemoveHTMLTags(string content)
        {
            var cleaned = string.Empty;
            try
            {
                StringBuilder textOnly = new StringBuilder();
                using (var reader = XmlNodeReader.Create(new System.IO.StringReader("<xml>" + content + "</xml>")))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Text)
                            textOnly.Append(reader.ReadContentAsString());
                    }
                }
                cleaned = textOnly.ToString();
            }
            catch
            {
                //A tag is probably not closed. fallback to regex string clean.
                string textOnly = string.Empty;
                Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                Regex compressSpaces = new Regex(@"[\s\r\n]+");
                textOnly = tagRemove.Replace(content, string.Empty);
                textOnly = compressSpaces.Replace(textOnly, " ");
                cleaned = textOnly;
            }

            return cleaned;
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public static bool CheckUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            bool check = false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (text.Contains(arr1[i]))
                    check = true;
            }
            return check;
        }
        public static string TranslateText(string input, string languagePair)
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
            request.Method = "GET";
            var content = String.Empty;
            HttpStatusCode statusCode;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var contentType = response.ContentType;
                Encoding encoding = null;
                if (contentType != null)
                {
                    var match = Regex.Match(contentType, @"(?<=charset\=).*");
                    if (match.Success)
                        encoding = Encoding.GetEncoding(match.ToString());
                }

                encoding = encoding ?? Encoding.UTF8;

                statusCode = ((HttpWebResponse)response).StatusCode;
                using (var reader = new StreamReader(stream, encoding))
                    content = reader.ReadToEnd();
            }
            var doc = Dcsoup.Parse(content);
            var scoreDiv = doc.Select("html").Select("span[id=result_box]").Html;
            return scoreDiv;
        }
        public static string ReturnPlaceVCH(int Place)
        {
            if (Place == 1)
            {
                return "Hà Nội";
            }
            else
                return "Việt Trì";
        }
        public static string ReturnTypeFastSlow(int type)
        {
            if (type == 1)
            {
                return "<span class='red'>Đi bay</span>";
            }
            else
                return "<span class='blue'>Đi tàu</span>";
        }
        public static string ReturnStatusBigpackage(int status)
        {
            if (status == 1)
            {
                return "Quảng Châu";
            }
            else if (status == 2)
            {
                return "Hà Nội";
            }
            else
            {
                return "Việt Trì";
            }

        }
        public static string ReturnStatusPayment(int status)
        {
            if (status == 0)
            {
                return "<span class='bg-red'>Chưa thanh toán</span>";
            }
            else
            {
                return "<span class='bg-blue'>Đã thanh toán</span>";
            }

        }
        public static string ReturnStatusReceivePackage(int status)
        {
            if (status == 0)
            {
                return "<span class='bg-red'>Chưa nhận hàng</span>";
            }
            else
            {
                return "<span class='bg-blue'>Đã nhận hàng</span>";
            }

        }
        public static string GetTradeTypeCYN(int TradeType)
        {
            if (TradeType == 1)
            {
                return "Thanh toán tiền mua hộ";
            }
            else
            {
                return "Nhận lại tiền mua hộ";
            }
        }
        public static Bitmap CreateBarcode1(string data)
        {
            Bitmap barCode = new Bitmap(1, 1);
            Font threeOfNine = new Font("Free 3 of 9", 60, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Graphics graphics = Graphics.FromImage(barCode);
            SizeF dataSize = graphics.MeasureString(data, threeOfNine);
            barCode = new Bitmap(barCode, dataSize.ToSize());
            graphics = Graphics.FromImage(barCode);
            graphics.Clear(Color.White);
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics.DrawString(data, threeOfNine, new SolidBrush(Color.Black), 0, 0);
            graphics.Flush();
            threeOfNine.Dispose();
            graphics.Dispose();
            return barCode;
        }
        public static string ShowIMG(string i)
        {
            return "<img src=\"" + i + "\">";
        }
        public static string BoolToStatusShowNoti(string i)
        {
            return ConvertStringToBool(i) == false ? "<span class='show-stat-w'>Không</span>" : "<span class='show-stat-s'>Có</span>";

        }

     
        public static void Writelog(string controller, string action, int uid, string contenterror)
        {
            string name = DateTime.Now.ToString("dd-MM-yyyy");
            string content = "{ Controller: " + controller + ",    Action: " + action + ",    LoginUID: " + uid + ",    MessageError: " + contenterror + "    " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " }" + Environment.NewLine;
            string filePath = HttpContext.Current.Server.MapPath("~/FileLog/log " + name + ".txt");
            File.AppendAllText(filePath, content);
        }
        public class jsonNoti
        {
            public string title { get; set; }
            public string message { get; set; }
            public string icon { get; set; }
            public string link { get; set; }
        }
        public static string GeneralTransportationOrderNewStatusApp(int status)
        {
            if (status == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"bg-red\">Đơn hàng mới</span>";
            else if (status == 2)
                return "<span class=\"bg-orange\">Đã duyệt</span>";
            else if (status == 3)
                return "<span class=\"bg-green\">Đã về kho TQ</span>";
            else if (status == 4)
                return "<span class=\"bg-green\">Đã về kho đích</span>";
            else if (status == 5)
                return "<span class=\"bg-blue\">Đã yêu cầu</span>";
            else
                return "<span class=\"bg-blue\">Đã giao cho khách</span>";
        }
        public static string requestOutStockStatus(int status)
        {
            if (status == 1)
                return "<span class=\"statusre bg-red\">Chưa xuất</span>";
            else
                return "<span class=\"statusre bg-blue\">Đã xuất</span>";
        }


        public static string ReturnLoaiDinhKhoan(int status)
        {
            if (status == 1)
            {
                return "<span class='bg-red'>Trừ quỹ</span>";
            }
            else
            {
                return "<span class='bg-blue'>Cộng quỹ</span>";
            }

        }

        public static string ReturnDinhKhoanCha(int status)
        {
            

            if (status == 1)
            {
                return "<span class='bg-red'>Các khoản chi</span>";
            }
            else if (status == 2)
            {
                 return "<span class='bg-blue'>Chuyển quỹ</span>";
            }
            else if (status == 3)
            {
                return "<span class='bg-yellow'>Chi phí quản trị doanh nghiệp</span>";
            }
            else if (status == 4)
            {
                return "<span class='bg-aqua'>Chi phí văn phòng</span>";
            }
            else if (status == 5)
            {
                return "<span class='bg-orange'>Lương thưởng</span>";
            }
            else if (status == 6)
            {
                return "<span class='bg-bronze'>Chi phí bán hàng</span>";
            }
            else 
            {
                return "<span class='bg-green'>Chi phí giá vốn</span>";
            }

        }

        public static string ReturnTrangThai(int status)
        {
            if (status == 1)
            {
                return "<span class='bg-red'>Hoạt động</span>";
            }
            else
            {
                return "<span class='bg-black'>Không Hoạt động</span>";
            }

        }


        public static string ConnectApi(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = ASCIIEncoding.ASCII;

                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                    {
                        string responseText = reader.ReadToEnd();
                        return responseText;
                    }
                }
            }
            catch
            {
                return null;
            }
        }



    }
}