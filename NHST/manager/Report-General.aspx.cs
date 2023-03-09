using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System.Text;
using Telerik.Web.UI;
using ZLADIPJ.Business;
using static NHST.Controllers.MainOrderController;

namespace NHST.manager
{
    public partial class Report_General : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            rdatefrom.SelectedDate = DateTime.Now;
            rdateto.SelectedDate = DateTime.Now.AddDays(30);
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            var ListReport = MainOrderController.GetFromDateToDate_RepostGeneral(Convert.ToString(rdatefrom.SelectedDate), Convert.ToString(rdateto.SelectedDate));

            if (ListReport.Count > 0)
            {
                //  List<ReportGeneral> ro_gr = new List<ReportGeneral>();
                double tongDT = 0;
                double Tongdon = 0;
                double trashopCNY = 0;
                double trashopVND = 0;
                double doanhthuDV = 0;
                double phiMH = 0;
                double tienMC = 0;
                double tongcan = 0;
                double phican = 0;
                double tongGTN = 0;
                double GiatriTB = 0;
                double TygiaTB = 0;
                double tongCurrentCNYVN = 0;
                foreach (var o in ListReport)
                {
                    tongDT = Convert.ToDouble(o.tongdoanhthu);
                    Tongdon = Convert.ToDouble(o.tongdon);
                    trashopCNY = Convert.ToDouble(o.tongtientrashopCNY);
                    trashopVND = Convert.ToDouble(o.tongtientrashopVND);
                    doanhthuDV = Convert.ToDouble(o.doanhthudichvumuahnag);
                    phiMH = Convert.ToDouble(o.phimuahang);
                    tienMC = Convert.ToDouble(o.tienmacca);
                    tongcan = Convert.ToDouble(o.tongcannang);
                    tongGTN = Convert.ToDouble(o.tongphigiaotannha);
                    tongCurrentCNYVN = Convert.ToDouble(o.CurrentCNYVN);

                }
                GiatriTB = tongDT / Tongdon;
                TygiaTB = tongCurrentCNYVN / Tongdon;
                lblgiatriTB.Text = string.Format("{0:N0}", GiatriTB);
                lbltygiaTB.Text = string.Format("{0:N0}", TygiaTB);
                lbltongdoanhthu.Text = string.Format("{0:N0}", tongDT);
                lbltongdon.Text = string.Format("{0:N0}", Tongdon);
                lbltongtientrashopCNY.Text = string.Format("{0:N0}", trashopCNY);
                lbltongtientrashopVND.Text = string.Format("{0:N0}", trashopVND);
                lbldoanhthudichvumuahnag.Text = string.Format("{0:N0}", doanhthuDV);
                lblphimuahang.Text = string.Format("{0:N0}", phiMH);
                lbltienmacca.Text = string.Format("{0:N0}", tienMC);
                lbltongcannang.Text = string.Format("{0:N0}", tongcan);
                lbltongphigiaotannha.Text = string.Format("{0:N0}", tongGTN);
                gr.DataSource = ListReport;
                gr.DataBind();

            }

        }
        public void LoadGrid()
        {
            var ListReport = MainOrderController.GetFromDateToDate_RepostGeneral(Convert.ToString(rdatefrom.SelectedDate), Convert.ToString(rdateto.SelectedDate));

            if (ListReport.Count > 0)
            {

                gr.DataSource = ListReport;

            }
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
            gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            gr.Rebind();
        }
        #endregion


        public class ReportOrder
        {
            public int OrderID { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string ShipCN { get; set; }
            public string BuyPro { get; set; }
            public string FeeWeight { get; set; }
            public string ShipHome { get; set; }
            public string CheckProduct { get; set; }
            public string Package { get; set; }
            public string IsFast { get; set; }
            public string Total { get; set; }
            public string Deposit { get; set; }
            public string PayLeft { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                //ltrHistory.Text = "";

                if (ListOrder.Count > 0)
                {
                    List<ReportOrder> ro_gr = new List<ReportOrder>();
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = o.TotalPriceVND.ToFloat(0);
                        double or_Deposit = o.Deposit.ToFloat(0);
                        double or_IsFastPrice = o.IsFastPrice.ToFloat(0);
                        double or_FeeShipCN = o.FeeShipCN.ToFloat(0);
                        double or_FeeBuyPro = o.FeeBuyPro.ToFloat(0);
                        double or_FeeWeight = o.FeeWeight.ToFloat(0);
                        double or_IsCheckProductPrice = o.IsCheckProductPrice.ToFloat(0);
                        double or_IsPackedPrice = o.IsPackedPrice.ToFloat(0);
                        double or_IsFastDeliveryPrice = o.IsFastDeliveryPrice.ToFloat(0);
                        double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;



                        int stt = Convert.ToInt32(o.Status);

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Email = o.Email;
                        r.Phone = o.Phone;
                        r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                        r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                        r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
                        r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
                        r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
                        r.Package = string.Format("{0:N0}", or_IsPackedPrice);
                        r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
                        r.Total = string.Format("{0:N0}", or_TotalPriceVND);
                        r.Deposit = string.Format("{0:N0}", or_Deposit);
                        r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
                        r.Status = PJUtils.IntToRequestAdmin(stt);
                        r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                        ro_gr.Add(r);
                    }

                    StringBuilder StrExport = new StringBuilder();
                    StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                    StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                    StrExport.Append("<DIV  style='font-size:12px;'>");
                    StrExport.Append("<table border=\"1\">");
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                    StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ShopID</strong></th>");
                    StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                    StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                    StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Còn lại</strong></th>");
                    StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                    StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                    StrExport.Append("  </tr>");
                    foreach (var item in ro_gr)
                    {
                        StrExport.Append("  <tr>");
                        StrExport.Append("      <td>" + item.OrderID + "</td>");
                        StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShopID + "</td>");
                        StrExport.Append("      <td>" + item.Total + "</td>");
                        StrExport.Append("      <td>" + item.Deposit + "</td>");
                        StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.PayLeft + "</td>");
                        StrExport.Append("      <td>" + item.Status + "</td>");
                        StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                        StrExport.Append("  </tr>");
                    }
                    StrExport.Append("</table>");
                    StrExport.Append("</div></body></html>");
                    string strFile = "bao-cao-don-hang-" + string.Format("{0:dd/MM/yyyy}", rdatefrom.SelectedDate) + "-" + string.Format("{0:dd/MM/yyyy}", rdateto.SelectedDate) + ".xls";
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


                    //gr.DataSource = ro_gr;
                    //gr.DataBind();
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Bạn không có quyền xuất file excel!", "e", false, Page);
            }

        }
    }
}