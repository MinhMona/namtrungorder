using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class Report_User_Use_Wallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0)
                        {
                            btnExcel.Visible = false;
                        }
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
                //LoadGrid1();
            }
        }
        public void LoadData()
        {
            rdatefrom.SelectedDate = DateTime.Now;
            rdateto.SelectedDate = DateTime.Now.AddDays(30);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            //int UID = Request.QueryString["i"].ToInt();
            var listhist = HistoryPayWalletController.GetFromDateTodate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));

            gr.DataSource = listhist;
            gr.DataBind();
        }
        public void LoadGrid()
        {
            //int UID = Request.QueryString["i"].ToInt();
            var listhist = HistoryPayWalletController.GetFromDateTodate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));

            gr.DataSource = listhist;
            //gr.DataBind();
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadGrid();
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid();
            //gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            //gr.Rebind();
        }
        #endregion

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var listhist = HistoryPayWalletController.GetFromDateTodate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ngày giờ</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Nội dung</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số tiền</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Loại giao dịch</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số dư</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in listhist)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.HContent + "</td>");
                    if (item.Type == 1)
                    {
                        StrExport.Append("      <td style=\"mso-number-format:'\\@'\">-" + string.Format("{0:N0}", Convert.ToDouble(item.Amount)) + " VNĐ</td>");
                    }
                    else
                    {
                        StrExport.Append("      <td style=\"mso-number-format:'\\@'\">+" + string.Format("{0:N0}", Convert.ToDouble(item.Amount)) + " VNĐ</td>");
                    }

                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + PJUtils.GetTradeType(Convert.ToInt32(item.TradeType)) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", Convert.ToDouble(item.MoneyLeft)) + " VNĐ</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "thong-ke-su-dung-vi.xls";
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