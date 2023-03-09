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
    public partial class HistoryEscrow : System.Web.UI.Page
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
            var la = KyquyController.GetAllBySQL(tCreateBy.Text.Trim(), rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString());
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
                }
            }

            //double total = AdminSendUserWalletController.GetTotalPrice(tSearchName.Text.Trim(), tCreateBy.Text.Trim(), rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString());
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
            var nap = AdminSendUserWalletController.GetByID(ID);
            if (nap != null)
            {
                NaptienInfo n = new NaptienInfo();
                int UID = Convert.ToInt32(nap.UID);
                double Amount = Convert.ToDouble(nap.Amount);
                var ai = AccountInfoController.GetByUserID(UID);
                if (ai != null)
                {
                    n.FullName = ai.FirstName + " " + ai.LastName;
                    n.Address = ai.Address;
                }
                n.Money = string.Format("{0:N0}", Amount);
                if (!string.IsNullOrEmpty(nap.TradeContent))
                    n.Note = nap.TradeContent;
                DateTime currentDate = DateTime.Now;
                string CreateDate = "Ngày " + currentDate.Day + " tháng " + currentDate.Month + " năm " + currentDate.Year;
                n.CreateDate = CreateDate;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(n);
            }
            return "null";
        }
        public class NaptienInfo
        {
            public string FullName { get; set; }
            public string Address { get; set; }
            public string Money { get; set; }
            public string Note { get; set; }
            public string CreateDate { get; set; }
        }
        #endregion

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac.RoleID == 0)
            {
                var la = KyquyController.GetAllBySQL(tCreateBy.Text.Trim(), rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString());
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ID</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Người tạo</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Mã định khoản</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên định khoản</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số tiền</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Nội dung</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ngày tạo</strong></th>");

                StrExport.Append("  </tr>");
                foreach (var item in la)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.ID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.CreatedBy + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.MaDinhKhoan + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.TenDinhKhoan + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", item.Amount) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.KyQuyContent + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "lich-su-ky-quy.xls";
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