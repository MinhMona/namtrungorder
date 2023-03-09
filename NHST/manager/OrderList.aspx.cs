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
using MB.Extensions;
using System.Text;

namespace NHST.manager
{
    public partial class OrderList : System.Web.UI.Page
    {
        private readonly NHSTEntities _context = new NHSTEntities();
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
                    if (ac != null)
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");
                    //if (ac.RoleID == 0)
                    //    btnExcel.Visible = true;

                    if (ac.RoleID == 3)
                        btnTakeOrder.Visible = true;
                }
            }
        }
        #region grid event


        public bool ShouldApplySortFilterOrGroup()
        {
            return gr.MasterTableView.FilterExpression != "" ||
                (gr.MasterTableView.GroupByExpressions.Count > 0 || isGrouping) ||
                gr.MasterTableView.SortExpressions.Count > 0;
        }
        bool isGrouping = false;
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                string s = tSearchName.Text.Trim();
                int type = ddlType.SelectedValue.ToString().ToInt(1);
                double priceFrom = Convert.ToDouble(rPriceFrom.Value);
                double priceTo = Convert.ToDouble(rPriceTo.Value);
                string datefromDK = fromDK.SelectedDate.ToString();
                string datetoDK = toDK.SelectedDate.ToString();
                string fromdate = rFD.SelectedDate.ToString();
                string todate = rTD.SelectedDate.ToString();
                string status1 = hdfStatus.Value;
                int status = ddlStatus.SelectedValue.ToInt(-1);
                int orderType = 1;
                if (Request.QueryString["ot"] != null)
                {
                    orderType = Request.QueryString["ot"].ToInt(1);
                }

                if (string.IsNullOrEmpty(s) && priceFrom == 0 && priceTo == 0 && string.IsNullOrEmpty(datefromDK) && string.IsNullOrEmpty(datetoDK)
                    && string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate)
                    && status1 == "-1" && chkIsnotcode.Checked == false)
                {
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    int StaffID = ac.ID;

                    int totalRow = MainOrderController.getOrderByRoleIDStaffID_SQL(ac.RoleID.ToString().ToInt(), ac.ID);
                    int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : gr.PageSize;
                    gr.VirtualItemCount = totalRow;
                    int Page = (ShouldApplySortFilterOrGroup()) ? 0 : gr.CurrentPageIndex;
                    var lo = MainOrderController.GetByUserInSQLHelper_nottextnottypeWithstatus(ac.RoleID.ToString().ToInt(), orderType, ac.ID, Page, maximumRows);
                    gr.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
                    gr.DataSource = lo;
                    var os = MainOrderController.getOrderByRoleIDStaffID1_SQL(ac.RoleID.ToString().ToInt(), ac.ID);
                    var stt0 = os.Where(o => o.Status == 0).ToList();
                    var stt1 = os.Where(o => o.Status == 1).ToList();
                    var stt2 = os.Where(o => o.Status == 2).ToList();
                    var stt5 = os.Where(o => o.Status == 5).ToList();
                    var stt11 = os.Where(o => o.Status == 5 && o.IsShopSendGoods == true).ToList();
                    var stt12 = os.Where(o => o.Status == 2 && o.IsBuying == true).ToList();
                    var stt13 = os.Where(o => o.Status == 2 && o.IsPaying == true).ToList();
                    var stt6 = os.Where(o => o.Status == 6).ToList();
                    var stt7 = os.Where(o => o.Status == 7).ToList();
                    var stt9 = os.Where(o => o.Status == 9).ToList();
                    var stt10 = os.Where(o => o.Status == 10).ToList();
                    bttnAll.Text = "Tất cả (" + os.Count + ")";
                    btn0.Text = "Chờ đặt cọc (" + stt0.Count + ")";
                    btn1.Text = "Hủy đơn hàng (" + stt1.Count + ")";
                    btn2.Text = "Chờ mua hàng (" + stt2.Count + ")";
                    btn5.Text = "Chờ shop TQ phát hàng (" + stt5.Count + ")";
                    btn11.Text = "Shop đã phát hàng (" + stt11.Count + ")";
                    btn12.Text = "Đang mua hàng (" + stt12.Count + ")";
                    //btn13.Text = "Đã thanh toán cho shop (" + stt13.Count + ")";
                    btn6.Text = "Đã về kho TQ (" + stt6.Count + ")";
                    btn7.Text = "Đã về kho VN (" + stt7.Count + ")";
                    btn9.Text = "Khách đã thanh toán (" + stt9.Count + ")";
                    btn10.Text = "Đã hoàn thành (" + stt10.Count + ")";
                }
                else
                {
                    //var la = MainOrderController.GetByUserInViewFilterWithStatusString(ac.RoleID.ToString().ToInt(), orderType, ac.ID, tSearchName.Text.Trim(),
                    //    ddlType.SelectedValue.ToString().ToInt(1));
                    //if (priceTo > 0)
                    //{
                    //    if (!string.IsNullOrEmpty(rFD.SelectedDate.ToString()))
                    //    {
                    //        DateTime fd = Convert.ToDateTime(rFD.SelectedDate);
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                    //            o.CreatedDate >= fd && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                    //            o.CreatedDate >= fd).ToList();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo
                    //                            && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo).ToList();
                    //        }
                    //    }

                    //    if (!string.IsNullOrEmpty(fromDK.SelectedDate.ToString()))
                    //    {
                    //        DateTime fdDK = Convert.ToDateTime(fromDK.SelectedDate);
                    //        if (!string.IsNullOrEmpty(toDK.SelectedDate.ToString()))
                    //        {
                    //            DateTime tdDK = Convert.ToDateTime(toDK.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                    //            o.ExpectedDate >= fdDK && o.ExpectedDate <= tdDK).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                    //            o.ExpectedDate >= fdDK).ToList();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(toDK.SelectedDate.ToString()))
                    //        {
                    //            DateTime tdDK = Convert.ToDateTime(toDK.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo
                    //                            && o.ExpectedDate <= tdDK).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo).ToList();
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(rFD.SelectedDate.ToString()))
                    //    {
                    //        DateTime fd = Convert.ToDateTime(rFD.SelectedDate);
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                    //            o.CreatedDate >= fd && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                    //            o.CreatedDate >= fd).ToList();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom).ToList();
                    //        }
                    //    }

                    //    if (!string.IsNullOrEmpty(fromDK.SelectedDate.ToString()))
                    //    {
                    //        DateTime fdDK = Convert.ToDateTime(fromDK.SelectedDate);
                    //        if (!string.IsNullOrEmpty(toDK.SelectedDate.ToString()))
                    //        {
                    //            DateTime tdDK = Convert.ToDateTime(toDK.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                    //            o.ExpectedDate >= fdDK && o.ExpectedDate <= tdDK).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                    //            o.ExpectedDate >= fdDK).ToList();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(toDK.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(toDK.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && o.ExpectedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom).ToList();
                    //        }
                    //    }

                    //}

                    //if (status1 != "-1")
                    //{
                    //    var la1 = new List<View_OrderListFilterWithStatusString>();
                    //    string[] sts = status1.Split(',');
                    //    for (int i = 0; i < sts.Length; i++)
                    //    {
                    //        int stat = sts[i].ToInt();
                    //        if (stat > -1)
                    //        {
                    //            var la2 = new List<View_OrderListFilterWithStatusString>();
                    //            if (stat == 11)
                    //            {
                    //                la2 = la.Where(o => o.Status == 5 && o.IsShopSendGoods == true).ToList();
                    //                if (la2.Count > 0)
                    //                {
                    //                    foreach (var item in la2)
                    //                    {
                    //                        la1.Add(item);
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                la2 = la.Where(o => o.Status == stat).ToList();
                    //                if (la2.Count > 0)
                    //                {
                    //                    foreach (var item in la2)
                    //                    {
                    //                        la1.Add(item);
                    //                    }
                    //                }
                    //            }

                    //        }
                    //    }
                    //    if (chkIsnotcode.Checked == true)
                    //    {
                    //        var la3 = new List<View_OrderListFilterWithStatusString>();
                    //        foreach (var item in la1)
                    //        {
                    //            int oid = item.ID;
                    //            var smallpackas = SmallPackageController.GetByMainOrderID(oid);
                    //            if (smallpackas.Count == 0)
                    //            {
                    //                la3.Add(item);
                    //            }
                    //        }

                    //        gr.VirtualItemCount = la3.Count;
                    //        gr.DataSource = la3;
                    //    }
                    //    else
                    //    {
                    //        gr.VirtualItemCount = la1.Count;
                    //        gr.DataSource = la1;
                    //    }
                    //}
                    //else
                    //{
                    //    if (chkIsnotcode.Checked == true)
                    //    {
                    //        var la2 = new List<View_OrderListFilterWithStatusString>();
                    //        foreach (var item in la)
                    //        {
                    //            int oid = item.ID;
                    //            var smallpackas = SmallPackageController.GetByMainOrderID(oid);
                    //            if (smallpackas.Count == 0)
                    //            {
                    //                la2.Add(item);
                    //            }
                    //        }
                    //        gr.VirtualItemCount = la2.Count;
                    //        gr.DataSource = la2;
                    //    }
                    //    else
                    //    {
                    //        gr.VirtualItemCount = la.Count;
                    //        gr.DataSource = la;
                    //    }
                    //}
                   
                    //if (Request.QueryString["s"] != null)
                    //{
                    //    s = Request.QueryString["s"];

                    //    tSearchName.Text = s;
                    //}

                    
                   
                    //if (Request.QueryString["type"] != null)
                    //{
                    //    type = Request.QueryString["type"].ToString().ToInt(1);

                    //    ddlType.SelectedValue = type.ToString();
                    //}

                   
                    //if (Request.QueryString["priceFrom"] != null)
                    //{
                    //    priceFrom = Request.QueryString["priceFrom"].ToString().ToFloat(0);

                    //    rPriceFrom.Value = priceFrom;
                    //}



                   
                    //if (Request.QueryString["priceTo"] != null)
                    //{
                    //    priceTo = Request.QueryString["priceTo"].ToString().ToFloat(0);

                    //    rPriceTo.Value = priceTo;
                    //}


                   
                    //if (Request.QueryString["datefromDK"] != null)
                    //{
                    //    datefromDK = Request.QueryString["datefromDK"].ToString();

                    //    fromDK.SelectedDate = DateTime.ParseExact(datefromDK, "dd/MM/yyyy HH:mm", null);
                    //}

                    
                    //if (Request.QueryString["datetoDK"] != null)
                    //{
                    //    datetoDK = Request.QueryString["datetoDK"].ToString();

                    //    toDK.SelectedDate = DateTime.ParseExact(datetoDK, "dd/MM/yyyy HH:mm", null);
                    //}

                   
                    //if (Request.QueryString["fd"] != null)
                    //{
                    //    fromdate = Request.QueryString["fd"].ToString();

                    //    rFD.SelectedDate = DateTime.ParseExact(fromdate, "dd/MM/yyyy HH:mm", null);
                    //}

                    //if (Request.QueryString["td"] != null)
                    //{
                    //    todate = Request.QueryString["td"].ToString();

                    //    rTD.SelectedDate = DateTime.ParseExact(todate, "dd/MM/yyyy HH:mm", null);
                    //}

                    
                    //if (Request.QueryString["st"] != null)
                    //{
                    //    status = Request.QueryString["st"].ToString().ToInt(-1);

                    //    ddlStatus.SelectedValue = status.ToString();
                    //}

                 
                    //if (Request.QueryString["ot"] != null)
                    //{
                    //    orderType = Request.QueryString["ot"].ToInt(1);
                    //}

                    int page = 0;
                   // Int32 Page = GetIntFromQueryString("Page");
                    //if (Page > 0)
                    //{
                    //    page = Page - 1;
                    //}
                    bool coMVD = false;

                    int mainID = 0;

                    int SalerID = 0;
                    int DatHang = 0;
                    var totalrow = 0;

                    TableSql tb = new TableSql(_context);

                    var la = tb.LoadOrderList(orderType, s, type, priceFrom, priceTo, fromdate, todate, datefromDK, datetoDK, status, coMVD, ac.RoleID, ac.ID, mainID, SalerID, DatHang, 1, 0);

                    

                    if (la.Count > 0)
                    {
                        totalrow = la[0].TotalRow.Value;
                    }

                    int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalrow : gr.PageSize;

                    gr.VirtualItemCount = totalrow;
                    int Page = (ShouldApplySortFilterOrGroup()) ? 0 : gr.CurrentPageIndex;

                    la = tb.LoadOrderList(orderType, s, type, priceFrom, priceTo, fromdate, todate, datefromDK, datetoDK, status, coMVD, ac.RoleID, ac.ID, mainID, SalerID, DatHang, 20, Page * 20);

                    gr.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
                    gr.DataSource = la;

                   




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
        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public int STT { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
            public string dathang { get; set; }
            public string kinhdoanh { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
        }

        //protected void btnExcel_Click(object sender, EventArgs e)
        //{
        //    var la = MainOrderController.GetAll();
        //    StringBuilder StrExport = new StringBuilder();
        //    StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        //    StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        //    StrExport.Append("<DIV  style='font-size:12px;'>");
        //    StrExport.Append("<table border=\"1\">");
        //    StrExport.Append("  <tr>");
        //    StrExport.Append("      <th><strong>OrderID</strong></th>");
        //    StrExport.Append("      <th><strong>Người đặt</strong></th>");
        //    StrExport.Append("      <th><strong>Sản phẩm</strong></th>");
        //    StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
        //    StrExport.Append("      <th><strong>Trạng thái</strong></th>");
        //    StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
        //    StrExport.Append("  </tr>");
        //    foreach (var item in la)
        //    {
        //        string htmlproduct = "";
        //        string username = "";
        //        var ui = AccountController.GetByID(item.UID.ToString().ToInt(1));
        //        if (ui != null)
        //        {
        //            username = ui.Username;
        //        }
        //        var products = OrderController.GetByMainOrderID(item.ID);
        //        foreach (var p in products)
        //        {
        //            string image_src = p.image_origin;
        //            if (!image_src.Contains("http:") && !image_src.Contains("https:"))
        //                htmlproduct += "https:" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
        //            else
        //                htmlproduct += "" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
        //        }
        //        StrExport.Append("  <tr>");
        //        StrExport.Append("      <td>" + item.ID + "</td>");
        //        StrExport.Append("      <td>" + username + "</td>");
        //        StrExport.Append("      <td>" + htmlproduct + "</td>");
        //        StrExport.Append("      <td>" + string.Format("{0:N0}", Math.Floor(item.TotalPriceVND.ToFloat())) + "</td>");
        //        StrExport.Append("      <td>" + PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status)) + "</td>");
        //        StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
        //        StrExport.Append("  </tr>");
        //    }
        //    StrExport.Append("</table>");
        //    StrExport.Append("</div></body></html>");
        //    string strFile = "ExcelReportOrderList.xls";
        //    string strcontentType = "application/vnd.ms-excel";
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.BufferOutput = true;
        //    Response.ContentType = strcontentType;
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        //    Response.Write(StrExport.ToString());
        //    Response.Flush();
        //    //Response.Close();
        //    Response.End();
        //}

        #region Button status
        protected void btn0_Click(object sender, EventArgs e)
        {
           
            ddlStatus.SelectedValue = "0";
            hdfStatus.Value = "0";
            gr.Rebind();
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
           
            ddlStatus.SelectedValue = "1";
            hdfStatus.Value = "1";
            gr.Rebind();
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
          
            ddlStatus.SelectedValue = "2";
            hdfStatus.Value = "2";
            gr.Rebind();
        }

        protected void btn5_Click(object sender, EventArgs e)
        {
          
            ddlStatus.SelectedValue = "5";
            hdfStatus.Value = "5";
            gr.Rebind();
        }
        protected void btn11_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "11";
            hdfStatus.Value = "11";
            gr.Rebind();
        }
        protected void btn12_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "12";
            hdfStatus.Value = "12";
            gr.Rebind();
        }

        protected void btn13_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "13";
            hdfStatus.Value = "13";
            gr.Rebind();
        }
        protected void btn6_Click(object sender, EventArgs e)
        {
         
            ddlStatus.SelectedValue = "6";
            hdfStatus.Value = "6";
            gr.Rebind();
        }

        protected void btn7_Click(object sender, EventArgs e)
        {
            
            ddlStatus.SelectedValue = "7";
            gr.Rebind();
        }

        protected void btn9_Click(object sender, EventArgs e)
        {
           
            ddlStatus.SelectedValue = "9";
            hdfStatus.Value = "9";
            gr.Rebind();
        }

        protected void btn10_Click(object sender, EventArgs e)
        {
           
            ddlStatus.SelectedValue = "10";
            hdfStatus.Value = "10";
            gr.Rebind();
        }

        protected void bttnAll_Click(object sender, EventArgs e)
        {
          
            ddlStatus.SelectedValue = "-1";
            hdfStatus.Value = "-1";
            gr.Rebind();
        }
        #endregion

        protected void btnDespi_Click(object sender, EventArgs e)
        {
            int OrderID = hdfOrderID.Value.ToInt(0);
            if (OrderID > 0)
            {
                var mainorder = MainOrderController.GetAllByID(OrderID);
                if (mainorder != null)
                {
                    int UID = Convert.ToInt32(mainorder.UID);
                    var obj_user = AccountController.GetByID(UID);
                    DateTime currentDate = DateTime.Now;
                    if (obj_user != null)
                    {
                        if (obj_user.Wallet > 0)
                        {
                            int OID = OrderID;
                            if (OID > 0)
                            {
                                var o = MainOrderController.GetAllByID(OID);
                                if (o != null)
                                {
                                    double orderdeposited = 0;
                                    if (!string.IsNullOrEmpty(o.Deposit))
                                        orderdeposited = Convert.ToDouble(o.Deposit);
                                    double amountdeposit = Convert.ToDouble(o.AmountDeposit);
                                    double userwallet = Convert.ToDouble(obj_user.Wallet);
                                    if (userwallet > 0)
                                    {
                                        if (userwallet >= amountdeposit)
                                        {
                                            using (NHSTEntities productDbContext = new NHSTEntities())
                                            {
                                                using (var transaction = productDbContext.Database.BeginTransaction())
                                                {
                                                    try
                                                    {
                                                        double wallet = userwallet - amountdeposit;
                                                        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                                        //Cập nhật lại MainOrder
                                                        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                                        MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);
                                                        transaction.Commit();
                                                        PJUtils.ShowMessageBoxSwAlert("Đặt cọc thành công", "s", true, Page);
                                                    }
                                                    catch (Exception exception)
                                                    {
                                                        transaction.Rollback();
                                                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                                    }
                                                }
                                            }
                                            //Cập nhật lại Wallet User

                                        }
                                        else
                                        {
                                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, Page);
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, Page);
                        }
                    }
                }
            }
        }

        protected void btnPayall_Click(object sender, EventArgs e)
        {
            int OrderID = hdfOrderID.Value.ToInt(0);
            if (OrderID > 0)
            {
                var mainorder = MainOrderController.GetAllByID(OrderID);
                if (mainorder != null)
                {
                    int UID = Convert.ToInt32(mainorder.UID);
                    var obj_user = AccountController.GetByID(UID);
                    if (obj_user != null)
                    {
                        string username = obj_user.Username;
                        int uid = obj_user.ID;
                        var id = OrderID;
                        DateTime currentDate = DateTime.Now;
                        if (id > 0)
                        {
                            var o = MainOrderController.GetAllByUIDAndID(uid, id);
                            if (o != null)
                            {
                                double deposit = o.Deposit.ToFloat(0);
                                double wallet = obj_user.Wallet.ToString().ToFloat(0);
                                double feewarehouse = 0;
                                if (o.FeeInWareHouse != null)
                                    feewarehouse = Convert.ToDouble(o.FeeInWareHouse);
                                double moneyleft = (o.TotalPriceVND.ToFloat(0) + feewarehouse) - deposit;

                                if (wallet >= moneyleft)
                                {
                                    using (NHSTEntities productDbContext = new NHSTEntities())
                                    {
                                        using (var transaction = productDbContext.Database.BeginTransaction())
                                        {
                                            try
                                            {
                                                double walletLeft = wallet - moneyleft;
                                                double payalll = deposit + moneyleft;
                                                MainOrderController.UpdateStatus(o.ID, uid, 9);
                                                AccountController.updateWallet(uid, walletLeft, currentDate, username);

                                                HistoryOrderChangeController.Insert(o.ID, uid, username, username +
                                                            " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                                                HistoryPayWalletController.Insert(uid, username, o.ID, moneyleft, username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username);
                                                MainOrderController.UpdateDeposit(id, uid, payalll.ToString());
                                                MainOrderController.UpdatePayAllDate(id, uid, currentDate);
                                                PayOrderHistoryController.Insert(id, uid, 9, moneyleft, 2, currentDate, username);
                                                transaction.Commit();
                                                PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                                            }
                                            catch (Exception exception)
                                            {
                                                transaction.Rollback();
                                                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Số tiền trong tài khoản của khách không đủ để thanh toán đơn hàng.", "e", true, Page);
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void btnTakeOrder_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username);
            if (ac != null)
            {
                if (ac.RoleID == 3)
                {
                    DateTime currentDate = DateTime.Now;
                    int DathangID = ac.ID;
                    try
                    {
                        int totalmax = Convert.ToInt32(ac.NumberTake) * Convert.ToInt32(ac.NumberOrder);
                        var la = MainOrderController.GetNumberTakeDate(ac.ID, currentDate);
                        if (totalmax > la)
                        {
                            var lo = MainOrderController.GetListTakeOrder(Convert.ToDouble(ac.MaxOrderPrice), Convert.ToInt32(ac.NumberOrder), Convert.ToInt32(ac.SiteType));
                            if (lo.Count > 0)
                            {
                                foreach (var item in lo)
                                {
                                    var mo = MainOrderController.GetAllByID(item.ID);
                                    if (mo != null)
                                    {
                                        int ID = mo.ID;
                                        double feebp = Convert.ToDouble(mo.FeeBuyPro);
                                        if (feebp < 10000)
                                        {
                                            feebp = 10000;
                                        }
                                        DateTime CreatedDate = Convert.ToDateTime(mo.CreatedDate);
                                        double salepercent = 0;
                                        double salepercentaf3m = 0;
                                        double dathangpercent = 0;
                                        var config = ConfigurationController.GetByTop1();
                                        if (config != null)
                                        {
                                            salepercent = Convert.ToDouble(config.SalePercent);
                                            salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                                            dathangpercent = Convert.ToDouble(config.DathangPercent);
                                        }
                                        string salerName = "";
                                        string dathangName = "";

                                        int salerID_old = Convert.ToInt32(mo.SalerID);
                                        int dathangID_old = Convert.ToInt32(mo.DathangID);
                                        #region Đặt hàng
                                        if (DathangID > 0)
                                        {
                                            if (DathangID == dathangID_old)
                                            {
                                                var staff = StaffIncomeController.GetByMainOrderIDUID(ID, dathangID_old);
                                                if (staff != null)
                                                {
                                                    if (staff.Status == 1)
                                                    {
                                                        //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                                        double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                                        double totalRealPrice = 0;
                                                        if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                                            totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                                        if (totalRealPrice > 0)
                                                        {
                                                            double totalpriceloi = totalPrice - totalRealPrice;

                                                            dathangpercent = Convert.ToDouble(staff.PercentReceive);
                                                            double income = totalpriceloi * dathangpercent / 100;
                                                            //double income = totalpriceloi;
                                                            StaffIncomeController.Update(staff.ID, totalRealPrice.ToString(), dathangpercent.ToString(), 1,
                                                                        income.ToString(), false, currentDate, username);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    var dathang = AccountController.GetByID(DathangID);
                                                    if (dathang != null)
                                                    {
                                                        dathangName = dathang.Username;
                                                        //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                                        double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                                        double totalRealPrice = 0;
                                                        if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                                            totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                                        if (totalRealPrice > 0)
                                                        {
                                                            double totalpriceloi = totalPrice - totalRealPrice;
                                                            double income = totalpriceloi * dathangpercent / 100;
                                                            //double income = totalpriceloi;
                                                            StaffIncomeController.Insert(ID, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                                                income.ToString(), false, CreatedDate, currentDate, username);
                                                        }
                                                        else
                                                        {
                                                            StaffIncomeController.Insert(ID, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                                            CreatedDate, currentDate, username);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var staff = StaffIncomeController.GetByMainOrderIDUID(ID, dathangID_old);
                                                if (staff != null)
                                                {
                                                    StaffIncomeController.Delete(staff.ID);
                                                }
                                                var dathang = AccountController.GetByID(DathangID);
                                                if (dathang != null)
                                                {
                                                    dathangName = dathang.Username;
                                                    //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                                    double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                                    double totalRealPrice = 0;
                                                    if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                                        totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                                    if (totalRealPrice > 0)
                                                    {
                                                        double totalpriceloi = totalPrice - totalRealPrice;
                                                        double income = totalpriceloi * dathangpercent / 100;
                                                        //double income = totalpriceloi;

                                                        StaffIncomeController.Insert(ID, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                                            income.ToString(), false, CreatedDate, currentDate, username);
                                                    }
                                                    else
                                                    {
                                                        StaffIncomeController.Insert(ID, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                                        CreatedDate, currentDate, username);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        MainOrderController.UpdateDathang(ID, DathangID);
                                        MainOrderController.UpdateNumberTakeDate(ID, currentDate);
                                    }
                                }
                                PJUtils.ShowMsg("Nhận đơn thành công.", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Không có đơn nào để nhận", "e", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Bạn đã được nhận tối đa số lượng đơn trong ngày", "e", true, Page);
                        }
                    }
                    catch (Exception)
                    {
                        PJUtils.ShowMessageBoxSwAlert("Bạn chưa được cấu hình số tiền nhận đơn tối đa", "e", true, Page);
                    }

                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Tài khoản không phải nhân viên đặt hàng", "e", true, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Tài khoản không tồn tại", "e", true, Page);
            }
        }
    }
}