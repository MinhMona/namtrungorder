using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Web.Services;
using MB.Extensions;
using System.Text;

namespace NHST.manager
{
    public partial class Report_PayOrder_Internal : System.Web.UI.Page
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
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var ispaying = Convert.ToBoolean(chkIsnotcode.Checked);
            int Status = Convert.ToInt32(ddlStatus.SelectedValue);
            string fromdate = rFD.SelectedDate.ToString();
            string todate = rTD.SelectedDate.ToString();
            int Site = Convert.ToInt32(ddlSite.SelectedValue);

            double tongtiente = 0;

            var la = MainOrderController.Report_GetOrderbyPaying(tSearchName.Text.Trim(), txtPerformStaff.Text.Trim(), Status, Site, ispaying, txtCreatedBy.Text, fromdate, todate);
            if (la != null)
            {
                List<OrderGetSQL> rs_gr = new List<OrderGetSQL>();
                if (la.Count > 0)
                {
                    foreach (var o in la)
                    {
                        OrderGetSQL rs = new OrderGetSQL();
                        string TranOrder = "";
                        double totalpricecyn = 0;
                        double feeshipcyn = 0;
                        double CurrentCYN = 0;
                        double shipfeeVND = 0;
                        var smallpack = SmallPackageController.GetByMainOrderID(Convert.ToInt32(o.ID));
                        if (smallpack.Count > 0)
                        {
                            foreach (var item in smallpack)
                            {
                                TranOrder += item.OrderTransactionCode + "</br>";
                            }
                        }

                        if (!string.IsNullOrEmpty(o.CurrentCNYVN))
                        {
                            CurrentCYN = Convert.ToDouble(o.CurrentCNYVN);
                            shipfeeVND = Convert.ToDouble(o.FeeShipCN);
                            feeshipcyn = shipfeeVND / CurrentCYN;
                        }
                        totalpricecyn = Convert.ToDouble(o.TotalPriceRealCYN);

                        rs.ID = o.ID;
                        rs.anhsanpham = o.anhsanpham;
                        rs.Site = o.Site;
                        rs.Uname = o.Uname;
                        rs.dathang = o.dathang;
                        rs.OrderTransactionCode = TranOrder;
                        rs.TotalPriceCYN =  Math.Round(totalpricecyn,2).ToString();
                        rs.statusstring = o.statusstring;
                        rs.MainOrderCode = o.MainOrderCode;
                        rs.PerformStaff = o.PerformStaff;
                        rs.PayingDate = o.PayingDate;
                        tongtiente += totalpricecyn;

                        rs_gr.Add(rs);
                    }
                    lblTongTien.Text = string.Format("{0:#,0.00}", tongtiente); 


                    gr.DataSource = rs_gr;

                }
            }
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



        public class OrderGetSQL
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string anhsanpham { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string orderlinks { get; set; }
            public string Site { get; set; }
            public string TotalPriceVND { get; set; }
            public string TotalPriceRealCYN { get; set; }
            public string TotalPriceCYN { get; set; }
            public string PriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
            public string ExpectedDate { get; set; }
            public string PerformStaff { get; set; }
            public string MainOrderCode { get; set; }
            public string PayingDate { get; set; }
            public string statusstring { get; set; }
            public int OrderType { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public bool IsShopSendGoods { get; set; }
            public bool IsBuying { get; set; }
            public bool IsPaying { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }
            public string CurrentCNYVN { get; set; }
            public string FeeShipCN { get; set; }
            public string Uname { get; set; }
            public string dathang { get; set; }
            public string saler { get; set; }
            public string dathangstr { get; set; }
            public string salerstr { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
            public string hasSmallpackage { get; set; }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ispaying = Convert.ToBoolean(chkIsnotcode.Checked);
                int Status = Convert.ToInt32(ddlStatus.SelectedValue);
                string fromdate = rFD.SelectedDate.ToString();
                string todate = rTD.SelectedDate.ToString();
                int Site = Convert.ToInt32(ddlSite.SelectedValue);
                var la = MainOrderController.Report_GetOrderbyPaying(tSearchName.Text.Trim(), txtPerformStaff.Text.Trim(), Status, Site, ispaying, txtCreatedBy.Text, fromdate, todate);
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ID</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Website</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>NV đặt hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tổng tiện (tệ)</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Trạng thái đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Người thực hiện</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ngày thực hiện</strong></th>");

                StrExport.Append("  </tr>");
                foreach (var item in la)
                {
                    string TranOrder = "";
                    double totalpricecyn = 0;
                    double feeshipcyn = 0;
                    double CurrentCYN = 0;
                    double shipfeeVND = 0;
                    var smallpack = SmallPackageController.GetByMainOrderID(Convert.ToInt32(item.ID));
                    if (smallpack.Count > 0)
                    {
                        foreach (var o in smallpack)
                        {
                            TranOrder += o.OrderTransactionCode + "</br>";
                        }
                    }

                    if (!string.IsNullOrEmpty(item.CurrentCNYVN))
                    {
                        CurrentCYN = Convert.ToDouble(item.CurrentCNYVN);
                        shipfeeVND = Convert.ToDouble(item.FeeShipCN);
                        feeshipcyn = shipfeeVND / CurrentCYN;
                    }
                    totalpricecyn = Convert.ToDouble(item.TotalPriceRealCYN);


                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.ID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.Site + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.Uname + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.MainOrderCode + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.dathang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + Math.Round(totalpricecyn, 2) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.statusstring + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.PerformStaff + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:dd/MM/yyyy}", item.PayingDate) + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "thong-ke-ds-thanh-toan-don-noi-bo.xls";
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