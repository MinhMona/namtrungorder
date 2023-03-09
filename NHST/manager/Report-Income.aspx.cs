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
    public partial class Report_Income : System.Web.UI.Page
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
            var acc = Session["userLoginSystem"].ToString();
            #region Thống kê thanh toán
            var history = PayOrderHistoryController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
            if (history.Count > 0)
            {
                List<PayHistory> ro_gr1 = new List<PayHistory>();
                foreach (var o in history)
                {
                    int stt = Convert.ToInt32(o.Status);
                    string status = "";
                    PayHistory r = new PayHistory();
                    r.MainOrderID = o.MainOrderID.ToString().ToInt();
                    string uname = "";
                    int UID = 0;
                    if (o.UID != null)
                    {
                        UID = Convert.ToInt32(o.UID);
                    }
                    if (AccountController.GetByID(UID) != null)
                    {
                        uname = AccountController.GetByID(UID).Username;
                    }
                    r.Username = uname;
                    if (stt == 2)
                        status = "Đặt cọc đơn hàng";
                    else
                        status = "Thanh toán đơn hàng";
                    r.Status = status;
                    r.Amount = string.Format("{0:N0}", o.Amount.ToString().ToFloat(0)) + "";
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    //r.CreatedBy = AccountController.GetByUsername(o.CreatedBy).Username;
                    r.CreatedBy = o.CreatedBy;
                    ro_gr1.Add(r);
                }
                RadGrid1.DataSource = ro_gr1;
                RadGrid1.DataBind();
            }
            #endregion
            #region Thống kê đơn hàng
            var ListOrder = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
            if (ListOrder.Count > 0)
            {
                List<ReportOrder> ro = new List<ReportOrder>();

                double Damuahang = 0;
                double Dahoanthanh = 0;
                double Datcocdenhoanthanh = 0;

                var ListOrder_Damuahang = ListOrder.Where(o => o.Status == 5).ToList();
                if (ListOrder_Damuahang.Count > 0)
                {
                    foreach (var od in ListOrder_Damuahang)
                    {
                        Damuahang += Convert.ToDouble(od.AmountDeposit);
                    }
                }
                var ListOrder_Dahoanthanh = ListOrder.Where(o => o.Status == 10).ToList();
                if (ListOrder_Dahoanthanh.Count > 0)
                {
                    foreach (var od in ListOrder_Dahoanthanh)
                    {
                        Dahoanthanh += Convert.ToDouble(od.AmountDeposit);
                    }
                }
                var ListOrder_Datcocdenhoanthanh = ListOrder.Where(o => o.Status > 1).ToList();
                if (ListOrder_Datcocdenhoanthanh.Count > 0)
                {
                    foreach (var od in ListOrder_Datcocdenhoanthanh)
                    {
                        Datcocdenhoanthanh += Convert.ToDouble(od.AmountDeposit);
                    }
                }

                var ListOrder_Datcoc_VeVN = ListOrder.Where(o => o.Status > 1 && o.Status < 9).ToList();
                var ListOrder_Dathanhtoan_Dahoanthanh = ListOrder.Where(o => o.Status > 7).ToList();

                double FeeShipCN1 = 0;
                double FeeShipCN2 = 0;
                double FeeBuyPro1 = 0;
                double FeeBuyPro2 = 0;
                double FeeWeight1 = 0;
                double FeeWeight2 = 0;
                double FeeCheckPro1 = 0;
                double FeeCheckPro2 = 0;
                double FeePackage1 = 0;
                double FeePackage2 = 0;

                if (ListOrder_Datcoc_VeVN.Count > 0)
                {
                    foreach (var item in ListOrder_Datcoc_VeVN)
                    {
                        FeeShipCN1 += Convert.ToDouble(item.FeeShipCN);
                        FeeBuyPro1 += Convert.ToDouble(item.FeeBuyPro);
                        FeeWeight1 += Convert.ToDouble(item.FeeWeight);
                        FeeCheckPro1 += Convert.ToDouble(item.IsCheckProductPrice);
                        FeePackage1 += Convert.ToDouble(item.IsPackedPrice);
                    }
                }
                if (ListOrder_Dathanhtoan_Dahoanthanh.Count > 0)
                {
                    foreach (var item in ListOrder_Dathanhtoan_Dahoanthanh)
                    {
                        FeeShipCN2 += Convert.ToDouble(item.FeeShipCN);
                        FeeBuyPro2 += Convert.ToDouble(item.FeeBuyPro);
                        FeeWeight2 += Convert.ToDouble(item.FeeWeight);
                        FeeCheckPro2 += Convert.ToDouble(item.IsCheckProductPrice);
                        FeePackage2 += Convert.ToDouble(item.IsPackedPrice);
                    }
                }
                lblDamuahang.Text = string.Format("{0:N0}", Damuahang) + " VNĐ";
                lblDahoanthanh.Text = string.Format("{0:N0}", Dahoanthanh) + " VNĐ";
                lbldatcocdenhoanthanh.Text = string.Format("{0:N0}", Datcocdenhoanthanh) + " VNĐ";

                lblFeeShipCN1.Text = string.Format("{0:N0}", FeeShipCN1) + " VNĐ";
                lblFeeBuyPro1.Text = string.Format("{0:N0}", FeeBuyPro1) + " VNĐ";
                lblFeeWeight1.Text = string.Format("{0:N0}", FeeWeight1) + " VNĐ";
                lblIsCheckProductPrice1.Text = string.Format("{0:N0}", FeeCheckPro1) + " VNĐ";
                lblIsPackedPrice1.Text = string.Format("{0:N0}", FeePackage1) + " VNĐ";

                lblFeeShipCN2.Text = string.Format("{0:N0}", FeeShipCN2) + " VNĐ";
                lblFeeBuyPro2.Text = string.Format("{0:N0}", FeeBuyPro2) + " VNĐ";
                lblFeeWeight2.Text = string.Format("{0:N0}", FeeWeight2) + " VNĐ";
                lblIsCheckProductPrice2.Text = string.Format("{0:N0}", FeeCheckPro2) + " VNĐ";
                lblIsPackedPrice2.Text = string.Format("{0:N0}", FeePackage2) + " VNĐ";

                double Total = 0;
                double Deposit = 0;
                double NotPay = 0;
                double OrderFast = 0;
                double FeeShipCN = 0;
                double FeeBuyPro = 0;
                double FeeWeight = 0;
                double IsCheckProductPrice = 0;
                double IsPackedPrice = 0;
                double IsFastDeliveryPrice = 0;

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
                    double currentOrderPriceLeft = 0;


                    Total += or_TotalPriceVND;

                    int stt = Convert.ToInt32(o.Status);
                    currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;
                    Deposit += o.Deposit.ToFloat();
                    NotPay += currentOrderPriceLeft;
                    OrderFast += or_IsFastPrice;
                    FeeShipCN += or_FeeShipCN;
                    FeeBuyPro += or_FeeBuyPro;
                    FeeWeight += or_FeeWeight;
                    IsCheckProductPrice += or_IsCheckProductPrice;
                    IsPackedPrice += or_IsPackedPrice;
                    IsFastDeliveryPrice += or_IsFastDeliveryPrice;
                    string username = "";
                    string saler = "";
                    if (o.UID != null)
                    {
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                    }
                    if (o.SalerID != null)
                    {
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;
                    }


                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Username = username;
                    r.Saler = saler;
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
                    ro.Add(r);
                }
                lblTotal.Text = string.Format("{0:N0}", Total) + " VNĐ";
                lblDeposit.Text = string.Format("{0:N0}", Deposit) + " VNĐ";
                lblNotPay.Text = string.Format("{0:N0}", NotPay) + " VNĐ";
                lblOrderFast.Text = string.Format("{0:N0}", OrderFast) + " VNĐ";
                lblFeeShipCN.Text = string.Format("{0:N0}", FeeShipCN) + " VNĐ";
                lblFeeBuyPro.Text = string.Format("{0:N0}", FeeBuyPro) + " VNĐ";
                lblFeeWeight.Text = string.Format("{0:N0}", FeeWeight) + " VNĐ";
                lblIsCheckProductPrice.Text = string.Format("{0:N0}", IsCheckProductPrice) + " VNĐ";
                lblIsPackedPrice.Text = string.Format("{0:N0}", IsPackedPrice) + " VNĐ";
                lblIsFastDeliveryPrice.Text = string.Format("{0:N0}", IsFastDeliveryPrice) + " VNĐ";
                pninfo.Visible = true;

                gr.DataSource = ro;
                gr.DataBind();
            }
            #endregion
        }

        public void LoadGrid()
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


                    string username = "";
                    string nvdathang = "";
                    string saler = "";
                    if (o.UID != null)
                    {
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                    }
                    if (o.SalerID != null)
                    {
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;
                    }
                    if (o.DathangID != null)
                    {
                        if (o.DathangID > 0)
                        {
                            var udathangr = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                            if (udathangr != null)
                                nvdathang = udathangr.Username;
                        }
                    }

                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Username = username;
                    r.Saler = saler;
                    r.DatHang = nvdathang;
                    r.PriceVND = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND));
                    double realPrice = 0;
                    if (o.TotalPriceReal.ToFloat(0) > 0)
                        realPrice = Convert.ToDouble(o.TotalPriceReal);
                    r.RealPriceVND = string.Format("{0:N0}", realPrice);
                    r.Currency = string.Format("{0:N0}", Convert.ToDouble(o.CurrentCNYVN));
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

                gr.DataSource = ro_gr;
                //gr.DataBind();
            }
        }
        public void LoadGrid1()
        {
            var ListOrder = PayOrderHistoryController.GetAll();
            //ltrHistory.Text = "";

            if (ListOrder.Count > 0)
            {
                List<PayHistory> ro_gr = new List<PayHistory>();
                foreach (var o in ListOrder)
                {
                    int stt = Convert.ToInt32(o.Status);

                    //if (stt > 2 && stt < 9)
                    //{                     

                    //}
                    //else if (stt < 2)
                    //{
                    //    currentOrderPriceLeft = o.TotalPriceVND.ToFloat(0);
                    //}
                    string status = "";
                    PayHistory r = new PayHistory();
                    r.MainOrderID = o.MainOrderID.ToString().ToInt();
                    string uname = "";
                    int UID = 0;
                    if (o.UID != null)
                    {
                        UID = Convert.ToInt32(o.UID);
                    }
                    if (AccountController.GetByID(UID) != null)
                    {
                        uname = AccountController.GetByID(UID).Username;
                    }
                    r.Username = uname;
                    if (stt == 2)
                        status = "Đặt cọc đơn hàng";
                    else
                        status = "Thanh toán đơn hàng";
                    r.Status = status;
                    r.Amount = string.Format("{0:N0}", o.Amount.ToString().ToFloat(0)) + "";
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    r.CreatedBy = o.CreatedBy;
                    ro_gr.Add(r);
                }

                RadGrid1.DataSource = ro_gr;
                //gr.DataBind();
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
            public string Username { get; set; }
            public string Saler { get; set; }
            public string DatHang { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string ShipCN { get; set; }
            public string ShipCNCYN { get; set; }
            public string ShipCYN { get; set; }
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
            public string Currency { get; set; }
            public string PriceVND { get; set; }
            public string PriceCYN { get; set; }
            public string RealPriceVND { get; set; }
            public string RealPriceCYN { get; set; }
            public string Ngaydatcoc { get; set; }
            public string Ngaymuahang { get; set; }
            public string NgayvekhoTQ { get; set; }
            public string NgayvekhoVN { get; set; }
            public string Ngaythanhtoan { get; set; }
            public string TongLink { get; set; }

            public string OrderWeight { get; set; }

            public string WareHouseName { get; set; }
            public string WareHouseFromName { get; set; }
        }
        public class PayHistory
        {
            public int MainOrderID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public string Status { get; set; }
            public string Amount { get; set; }
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
        }

        protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid1();
            RadGrid1.Rebind();
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid1();
            RadGrid1.Rebind();
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder = PayOrderHistoryController.GetAll();
                //ltrHistory.Text = "";
                List<PayHistory> ro_gr = new List<PayHistory>();
                if (ListOrder.Count > 0)
                {

                    foreach (var o in ListOrder)
                    {
                        int stt = Convert.ToInt32(o.Status);
                        string status = "";
                        PayHistory r = new PayHistory();
                        r.MainOrderID = o.MainOrderID.ToString().ToInt();
                        string uname = "";
                        int UID = 0;
                        if (o.UID != null)
                        {
                            UID = Convert.ToInt32(o.UID);
                        }
                        if (AccountController.GetByID(UID) != null)
                        {
                            uname = AccountController.GetByID(UID).Username;
                        }
                        r.Username = uname;
                        if (stt == 2)
                            status = "Đặt cọc đơn hàng";
                        else
                            status = "Thanh toán đơn hàng";
                        r.Status = status;
                        r.Amount = string.Format("{0:N0}", o.Amount.ToString().ToFloat(0)) + "";
                        r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                        r.CreatedBy = o.CreatedBy;
                        ro_gr.Add(r);
                    }
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th><strong>Loại thanh toán</strong></th>");
                StrExport.Append("      <th><strong>Số tiền</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("      <th><strong>Người tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.MainOrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.Amount + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("      <td>" + item.CreatedBy + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "bao-cao-thanh-toan.xls";
                string strcontentType = "application/vnd.ms-excel";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.ContentType = strcontentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.Write(StrExport.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Bạn không có quyền xuất file excel!", "e", false, Page);
            }


        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                //var ListOrder = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = MainOrderController.GetFromDateToDateSQL(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString());
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {

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
                        string username = "";
                        string saler = "";
                        string nvdathang = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }

                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }

                        if (o.DathangID != null)
                        {
                            if (o.DathangID > 0)
                            {
                                var udathangr = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                                if (udathangr != null)
                                    nvdathang = udathangr.Username;
                            }
                        }

                        string ngaydatcoc = "";
                        string ngaymuahang = "";
                        string ngayvekhotq = "";
                        string ngayvekhovn = "";
                        string ngaythanhtoan = "";
                        double currency = Convert.ToDouble(o.CurrentCNYVN);

                        if (o.DepostiDate != null)
                        {
                            ngaydatcoc = string.Format("{0:dd/MM/yyyy}", o.DepostiDate);
                        }
                        if (o.BuyProductDate != null)
                        {
                            ngaymuahang = string.Format("{0:dd/MM/yyyy}", o.BuyProductDate);
                        }
                        if (o.TQWarehouseDate != null)
                        {
                            ngayvekhotq = string.Format("{0:dd/MM/yyyy}", o.TQWarehouseDate);
                        }
                        if (o.VNWarehouseDate != null)
                        {
                            ngayvekhovn = string.Format("{0:dd/MM/yyyy}", o.VNWarehouseDate);
                        }
                        if (o.PayAllDate != null)
                        {
                            ngaythanhtoan = string.Format("{0:dd/MM/yyyy}", o.PayAllDate);
                        }

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
                        r.Email = o.Email;
                        r.Phone = o.Phone;
                        r.DatHang = nvdathang;
                        r.PriceVND = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND));
                        r.PriceCYN = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND) / currency);
                        double realPrice = 0;
                        if (o.TotalPriceReal.ToFloat(0) > 0)
                            realPrice = Convert.ToDouble(o.TotalPriceReal);

                        double realPriceCYN = 0;
                        realPriceCYN = realPrice / currency;
                        r.RealPriceVND = string.Format("{0:N0}", realPrice);
                        r.Currency = string.Format("{0:N0}", currency);
                        r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                        r.ShipCNCYN = string.Format("{0:N0}", or_FeeShipCN / currency);
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
                        r.Currency = string.Format("{0:N0}", o.CurrentCNYVN);
                        r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                        r.Ngaydatcoc = ngaydatcoc;
                        r.Ngaymuahang = ngaymuahang;
                        r.NgayvekhoTQ = ngayvekhotq;
                        r.NgayvekhoVN = ngayvekhovn;
                        r.Ngaythanhtoan = ngaythanhtoan;
                        r.TongLink = string.Format("{0:N0}", o.orderlinks);
                        r.OrderWeight = o.OrderWeight;
                        r.WareHouseName = o.WareHouseName;
                        r.WareHouseFromName = o.WareHouseFromName;
                        ro_gr.Add(r);
                    }
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>NV Mua hàng</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Tiền hàng trên web VNĐ</strong></th>");
                StrExport.Append("      <th><strong>Tiền hàng trên web RMB</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền mua thật VNĐ</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền mua thật RMB</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ VNĐ</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ RMB</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th><strong>Cân nặng</strong></th>");
                StrExport.Append("      <th><strong>Kho TQ</strong></th>");
                StrExport.Append("      <th><strong>Kho nhận</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Tỷ giá</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("      <th><strong>Ngày đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Chờ shop TQ phát hàng</strong></th>");
                StrExport.Append("      <th><strong>Ngày về kho TQ</strong></th>");
                StrExport.Append("      <th><strong>Ngày về kho VN</strong></th>");
                StrExport.Append("      <th><strong>Ngày thanh toán</strong></th>");
                StrExport.Append("      <th><strong>Tổng số links</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    //StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.DatHang + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.PriceVND + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.PriceCYN + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.RealPriceVND + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.RealPriceCYN + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCNCYN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");

                    StrExport.Append("      <td>" + item.OrderWeight + "</td>");
                    StrExport.Append("      <td>" + item.WareHouseFromName + "</td>");
                    StrExport.Append("      <td>" + item.WareHouseName + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.Currency + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("      <td>" + item.Ngaydatcoc + "</td>");
                    StrExport.Append("      <td>" + item.Ngaymuahang + "</td>");
                    StrExport.Append("      <td>" + item.NgayvekhoTQ + "</td>");
                    StrExport.Append("      <td>" + item.NgayvekhoVN + "</td>");
                    StrExport.Append("      <td>" + item.Ngaythanhtoan + "</td>");
                    StrExport.Append("      <td>" + item.TongLink + "</td>");
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
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Bạn không có quyền xuất file excel!", "e", false, Page);
            }

        }

        protected void btnExcelDondamuahang_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)

            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status == 5).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }

                        string ngaydatcoc = "";
                        string ngaymuahang = "";
                        string ngayvekhotq = "";
                        string ngayvekhovn = "";
                        string ngaythanhtoan = "";
                        double currency = Convert.ToDouble(o.CurrentCNYVN);

                        if (o.DepostiDate != null)
                        {
                            ngaydatcoc = string.Format("{0:dd/MM/yyyy}", o.DepostiDate);
                        }
                        if (o.BuyProductDate != null)
                        {
                            ngaymuahang = string.Format("{0:dd/MM/yyyy}", o.BuyProductDate);
                        }
                        if (o.TQWarehouseDate != null)
                        {
                            ngayvekhotq = string.Format("{0:dd/MM/yyyy}", o.TQWarehouseDate);
                        }
                        if (o.VNWarehouseDate != null)
                        {
                            ngayvekhovn = string.Format("{0:dd/MM/yyyy}", o.VNWarehouseDate);
                        }
                        if (o.PayAllDate != null)
                        {
                            ngaythanhtoan = string.Format("{0:dd/MM/yyyy}", o.PayAllDate);
                        }




                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                        r.PriceCYN = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND) / currency);
                        double realPriceCYN = 0;
                        double realPrice = 0;
                        if (o.TotalPriceReal.ToFloat(0) > 0)
                            realPrice = Convert.ToDouble(o.TotalPriceReal);
                        realPriceCYN = realPrice / currency;
                        r.Ngaydatcoc = ngaydatcoc;
                        r.Ngaymuahang = ngaymuahang;
                        r.NgayvekhoTQ = ngayvekhotq;
                        r.NgayvekhoVN = ngayvekhovn;
                        r.Ngaythanhtoan = ngaythanhtoan;
                        ro_gr.Add(r);
                    }
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-mua-hang.xls";
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

        protected void btnExcelDahoanthanh_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status == 10).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }
                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-hoan-thanh.xls";
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

        protected void btnExcelDatcocdenhoanthanh_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }

                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-dat-coc-den-hoan-thanh.xls";
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

        protected void btnExcelDeposit_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }

                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }



                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-dat-coc-den-hoan-thanh.xls";
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

        protected void BtnExcelNotPay_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }




                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-chua-thanh-toan.xls";
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

        protected void btnExcelOrderFast_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }




                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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

        protected void btnExcelFeeShipCN_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }




                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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

        protected void btnExcelFeeShipCN1_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;
                        }




                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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

        protected void btnExcelFeeShipCN2_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }



                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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

        protected void btnExcelFeeBuyPro_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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

        protected void btnExcelFeeBuyPro1_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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

        protected void btnExcelFeeBuyPro2_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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

        protected void btnExcelFeeWeight_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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

        protected void btnExcelFeeWeight1_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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

        protected void btnExcelFeeWeight2_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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

        protected void btnExcelIsCheckProductPrice_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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

        protected void btnExcelIsCheckProductPrice1_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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

        protected void btnExcelIsCheckProductPrice2_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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

        protected void btnExcelIsPackedPrice_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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

        protected void btnExcelIsPackedPrice1_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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

        protected void btnExcelIsPackedPrice2_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder1 = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
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
                        string username = "";
                        string saler = "";
                        if (o.UID != null)
                        {
                            var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                            if (udathang != null)
                                username = udathang.Username;
                        }
                        if (o.SalerID != null)
                        {
                            var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                            if (usalert != null)
                                saler = usalert.Username;

                        }


                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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