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
using System.Web.Services;

namespace NHST.manager
{
    public partial class OrderListSetStaff : System.Web.UI.Page
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
                    if (ac != null)
                        if (ac.RoleID != 0 && ac.RoleID != 2)
                            Response.Redirect("/trang-chu");
                    if (ac.RoleID == 0)
                        btnExcel.Visible = true;
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
                string fromdate = rFD.SelectedDate.ToString();
                string todate = rTD.SelectedDate.ToString();
                string status1 = hdfStatus.Value;
                int status = ddlStatus.SelectedValue.ToInt(-1);
                int orderType = 1;
                if (Request.QueryString["ot"] != null)
                {
                    orderType = Request.QueryString["ot"].ToInt(1);
                }

                if (string.IsNullOrEmpty(s) && priceFrom == 0 && priceTo == 0
                    && string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate)
                    && status1 == "-1" && chkIsnotcode.Checked == false)
                {
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    int StaffID = ac.ID;

                    int totalRow = MainOrderController.getOrderByRoleIDStaffID_SQL(ac.RoleID.ToString().ToInt(), ac.ID);
                    int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : gr.PageSize;
                    gr.VirtualItemCount = totalRow;
                    int Page = (ShouldApplySortFilterOrGroup()) ? 0 : gr.CurrentPageIndex;
                    var lo = MainOrderController.GetByUserInSQLHelper_nottextnottypeWithstatusSet(ac.RoleID.ToString().ToInt(), orderType, ac.ID, Page, maximumRows);
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
                    //btn0.Text = "Chờ đặt cọc (" + stt0.Count + ")";
                    //btn1.Text = "Hủy đơn hàng (" + stt1.Count + ")";
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
                    var la = MainOrderController.GetByUserInViewFilterWithStatusString1(ac.RoleID.ToString().ToInt(), orderType, ac.ID, tSearchName.Text.Trim(),
                        ddlType.SelectedValue.ToString().ToInt(1));
                    if (priceTo > 0)
                    {
                        if (!string.IsNullOrEmpty(rFD.SelectedDate.ToString()))
                        {
                            DateTime fd = Convert.ToDateTime(rFD.SelectedDate);
                            if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                            {
                                DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                                o.CreatedDate >= fd && o.CreatedDate <= td).ToList();
                            }
                            else
                            {
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                                o.CreatedDate >= fd).ToList();
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                            {
                                DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo
                                                && o.CreatedDate <= td).ToList();
                            }
                            else
                            {
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo).ToList();
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(rFD.SelectedDate.ToString()))
                        {
                            DateTime fd = Convert.ToDateTime(rFD.SelectedDate);
                            if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                            {
                                DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                                o.CreatedDate >= fd && o.CreatedDate <= td).ToList();
                            }
                            else
                            {
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                                o.CreatedDate >= fd).ToList();
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                            {
                                DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && o.CreatedDate <= td).ToList();
                            }
                            else
                            {
                                la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom).ToList();
                            }
                        }
                    }

                    if (status1 != "-1")
                    {
                        var la1 = new List<View_OrderListFilterWithStatusString>();
                        string[] sts = status1.Split(',');
                        for (int i = 0; i < sts.Length; i++)
                        {
                            int stat = sts[i].ToInt();
                            if (stat > -1)
                            {
                                var la2 = new List<View_OrderListFilterWithStatusString>();
                                if (stat == 11)
                                {
                                    la2 = la.Where(o => o.Status == 5 && o.IsShopSendGoods == true).ToList();
                                    if (la2.Count > 0)
                                    {
                                        foreach (var item in la2)
                                        {
                                            StringBuilder dathangstring = new StringBuilder();
                                            if (ac.RoleID == 0 || ac.RoleID == 2)
                                            {
                                                var listdathang = AccountController.GetAllByRoleID(3);
                                                if (listdathang.Count > 0)
                                                {
                                                    dathangstring.Append("<select class=\"form-control\" onchange=\"updateDathang($(this))\" data-mainorderid=\"" + item.ID + "\">");
                                                    dathangstring.Append("<option value=\"0\" selected>-----------</option>");
                                                    foreach (var d in listdathang)
                                                    {
                                                        if (d.ID == item.DathangID)
                                                        {
                                                            dathangstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                        }
                                                        else
                                                        {
                                                            dathangstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                        }
                                                    }
                                                    dathangstring.Append("</select>");
                                                }
                                            }
                                            else
                                            {
                                                dathangstring.Append(item.dathang);
                                            }
                                            StringBuilder salerstring = new StringBuilder();
                                            if (ac.RoleID == 0 || ac.RoleID == 2)
                                            {
                                                var listsaler = AccountController.GetAllByRoleID(6);
                                                if (listsaler.Count > 0)
                                                {
                                                    salerstring.Append("<select class=\"form-control\" onchange=\"updateSaler($(this))\" data-mainorderid=\"" + item.ID + "\"\">");
                                                    salerstring.Append("<option value=\"0\" selected>-----------</option>");
                                                    foreach (var d in listsaler)
                                                    {
                                                        if (d.ID == item.SalerID)
                                                        {
                                                            salerstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                        }
                                                        else
                                                        {
                                                            salerstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                        }
                                                    }
                                                    salerstring.Append("</select>");
                                                }
                                            }
                                            else
                                            {
                                                salerstring.Append(item.saler);
                                            }
                                            item.dathang = dathangstring.ToString();
                                            item.saler = salerstring.ToString();
                                            la1.Add(item);
                                        }
                                    }
                                }
                                else
                                {
                                    la2 = la.Where(o => o.Status == stat).ToList();
                                    if (la2.Count > 0)
                                    {
                                        foreach (var item in la2)
                                        {
                                            StringBuilder dathangstring = new StringBuilder();
                                            if (ac.RoleID == 0 || ac.RoleID == 2)
                                            {
                                                var listdathang = AccountController.GetAllByRoleID(3);
                                                if (listdathang.Count > 0)
                                                {
                                                    dathangstring.Append("<select class=\"form-control\" onchange=\"updateDathang($(this))\" data-mainorderid=\"" + item.ID + "\">");
                                                    dathangstring.Append("<option value=\"0\" selected>-----------</option>");
                                                    foreach (var d in listdathang)
                                                    {
                                                        if (d.ID == item.DathangID)
                                                        {
                                                            dathangstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                        }
                                                        else
                                                        {
                                                            dathangstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                        }
                                                    }
                                                    dathangstring.Append("</select>");
                                                }
                                            }
                                            else
                                            {
                                                dathangstring.Append(item.dathang);
                                            }
                                            StringBuilder salerstring = new StringBuilder();
                                            if (ac.RoleID == 0 || ac.RoleID == 2)
                                            {
                                                var listsaler = AccountController.GetAllByRoleID(6);
                                                if (listsaler.Count > 0)
                                                {
                                                    salerstring.Append("<select class=\"form-control\" onchange=\"updateSaler($(this))\" data-mainorderid=\"" + item.ID + "\"\">");
                                                    salerstring.Append("<option value=\"0\" selected>-----------</option>");
                                                    foreach (var d in listsaler)
                                                    {
                                                        if (d.ID == item.SalerID)
                                                        {
                                                            salerstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                        }
                                                        else
                                                        {
                                                            salerstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                        }
                                                    }
                                                    salerstring.Append("</select>");
                                                }
                                            }
                                            else
                                            {
                                                salerstring.Append(item.saler);
                                            }
                                            item.dathang = dathangstring.ToString();
                                            item.saler = salerstring.ToString();
                                            la1.Add(item);
                                        }
                                    }
                                }

                            }
                        }
                        if (chkIsnotcode.Checked == true)
                        {
                            var la3 = new List<View_OrderListFilterWithStatusString>();
                            foreach (var item in la1)
                            {
                                int oid = item.ID;
                                var smallpackas = SmallPackageController.GetByMainOrderID(oid);
                                if (smallpackas.Count == 0)
                                {
                                    StringBuilder dathangstring = new StringBuilder();
                                    if (ac.RoleID == 0 || ac.RoleID == 2)
                                    {
                                        var listdathang = AccountController.GetAllByRoleID(3);
                                        if (listdathang.Count > 0)
                                        {
                                            dathangstring.Append("<select class=\"form-control\" onchange=\"updateDathang($(this))\" data-mainorderid=\"" + item.ID + "\">");
                                            dathangstring.Append("<option value=\"0\" selected>-----------</option>");
                                            foreach (var d in listdathang)
                                            {
                                                if (d.ID == item.DathangID)
                                                {
                                                    dathangstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                }
                                                else
                                                {
                                                    dathangstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                }
                                            }
                                            dathangstring.Append("</select>");
                                        }
                                    }
                                    else
                                    {
                                        dathangstring.Append(item.dathang);
                                    }
                                    StringBuilder salerstring = new StringBuilder();
                                    if (ac.RoleID == 0 || ac.RoleID == 2)
                                    {
                                        var listsaler = AccountController.GetAllByRoleID(6);
                                        if (listsaler.Count > 0)
                                        {
                                            salerstring.Append("<select class=\"form-control\" onchange=\"updateSaler($(this))\" data-mainorderid=\"" + item.ID + "\"\">");
                                            salerstring.Append("<option value=\"0\" selected>-----------</option>");
                                            foreach (var d in listsaler)
                                            {
                                                if (d.ID == item.SalerID)
                                                {
                                                    salerstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                }
                                                else
                                                {
                                                    salerstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                }
                                            }
                                            salerstring.Append("</select>");
                                        }
                                    }
                                    else
                                    {
                                        salerstring.Append(item.saler);
                                    }
                                    item.dathang = dathangstring.ToString();
                                    item.saler = salerstring.ToString();
                                    la3.Add(item);
                                }
                            }

                            gr.VirtualItemCount = la3.Count;
                            gr.DataSource = la3;
                        }
                        else
                        {
                            gr.VirtualItemCount = la1.Count;
                            gr.DataSource = la1;
                        }
                    }
                    else
                    {
                        if (chkIsnotcode.Checked == true)
                        {
                            var la2 = new List<View_OrderListFilterWithStatusString>();
                            foreach (var item in la)
                            {
                                int oid = item.ID;
                                var smallpackas = SmallPackageController.GetByMainOrderID(oid);
                                if (smallpackas.Count == 0)
                                {
                                    StringBuilder dathangstring = new StringBuilder();
                                    if (ac.RoleID == 0 || ac.RoleID == 2)
                                    {
                                        var listdathang = AccountController.GetAllByRoleID(3);
                                        if (listdathang.Count > 0)
                                        {
                                            dathangstring.Append("<select class=\"form-control\" onchange=\"updateDathang($(this))\" data-mainorderid=\"" + item.ID + "\">");
                                            dathangstring.Append("<option value=\"0\" selected>-----------</option>");
                                            foreach (var d in listdathang)
                                            {
                                                if (d.ID == item.DathangID)
                                                {
                                                    dathangstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                }
                                                else
                                                {
                                                    dathangstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                }
                                            }
                                            dathangstring.Append("</select>");
                                        }
                                    }
                                    else
                                    {
                                        dathangstring.Append(item.dathang);
                                    }
                                    StringBuilder salerstring = new StringBuilder();
                                    if (ac.RoleID == 0 || ac.RoleID == 2)
                                    {
                                        var listsaler = AccountController.GetAllByRoleID(6);
                                        if (listsaler.Count > 0)
                                        {
                                            salerstring.Append("<select class=\"form-control\" onchange=\"updateSaler($(this))\" data-mainorderid=\"" + item.ID + "\"\">");
                                            salerstring.Append("<option value=\"0\" selected>-----------</option>");
                                            foreach (var d in listsaler)
                                            {
                                                if (d.ID == item.SalerID)
                                                {
                                                    salerstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                                }
                                                else
                                                {
                                                    salerstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                                }
                                            }
                                            salerstring.Append("</select>");
                                        }
                                    }
                                    else
                                    {
                                        salerstring.Append(item.saler);
                                    }
                                    item.dathang = dathangstring.ToString();
                                    item.saler = salerstring.ToString();
                                    la2.Add(item);
                                }
                            }
                            gr.VirtualItemCount = la2.Count;
                            gr.DataSource = la2;
                        }
                        else
                        {
                            foreach (var item in la)
                            {
                                StringBuilder dathangstring = new StringBuilder();
                                if (ac.RoleID == 0 || ac.RoleID == 2)
                                {
                                    var listdathang = AccountController.GetAllByRoleID(3);
                                    if (listdathang.Count > 0)
                                    {
                                        dathangstring.Append("<select class=\"form-control\" onchange=\"updateDathang($(this))\" data-mainorderid=\"" + item.ID + "\">");
                                        dathangstring.Append("<option value=\"0\" selected>-----------</option>");
                                        foreach (var d in listdathang)
                                        {
                                            if (d.ID == item.DathangID)
                                            {
                                                dathangstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                            }
                                            else
                                            {
                                                dathangstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                            }
                                        }
                                        dathangstring.Append("</select>");
                                    }
                                }
                                else
                                {
                                    dathangstring.Append(item.dathang);
                                }
                                StringBuilder salerstring = new StringBuilder();
                                if (ac.RoleID == 0 || ac.RoleID == 2)
                                {
                                    var listsaler = AccountController.GetAllByRoleID(6);
                                    if (listsaler.Count > 0)
                                    {
                                        salerstring.Append("<select class=\"form-control\" onchange=\"updateSaler($(this))\" data-mainorderid=\"" + item.ID + "\"\">");
                                        salerstring.Append("<option value=\"0\" selected>-----------</option>");
                                        foreach (var d in listsaler)
                                        {
                                            if (d.ID == item.SalerID)
                                            {
                                                salerstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                                            }
                                            else
                                            {
                                                salerstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                                            }
                                        }
                                        salerstring.Append("</select>");
                                    }
                                }
                                else
                                {
                                    salerstring.Append(item.saler);
                                }
                                item.dathang = dathangstring.ToString();
                                item.saler = salerstring.ToString();
                            }
                            gr.VirtualItemCount = la.Count;
                            gr.DataSource = la;
                        }
                    }
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
            //string textsearch = tSearchName.Text.Trim();
            //if (!string.IsNullOrEmpty(textsearch))
            //{
            //    Response.Redirect("/admin/orderlist.aspx?type=" + ddlType.SelectedValue + "&s=" + textsearch + "");
            //}
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
            public string dathangstr { get; set; }
            public string salerstr { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac.RoleID == 0)
            {
                var la = MainOrderController.GetAll();
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>OrderID</strong></th>");
                StrExport.Append("      <th><strong>Người đặt</strong></th>");
                StrExport.Append("      <th><strong>Sản phẩm</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in la)
                {
                    string htmlproduct = "";
                    string username = "";
                    var ui = AccountController.GetByID(item.UID.ToString().ToInt(1));
                    if (ui != null)
                    {
                        username = ui.Username;
                    }
                    var products = OrderController.GetByMainOrderID(item.ID);
                    foreach (var p in products)
                    {
                        string image_src = p.image_origin;
                        if (!image_src.Contains("http:") && !image_src.Contains("https:"))
                            htmlproduct += "https:" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
                        else
                            htmlproduct += "" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
                    }
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.ID + "</td>");
                    StrExport.Append("      <td>" + username + "</td>");
                    StrExport.Append("      <td>" + htmlproduct + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:N0}", Math.Floor(item.TotalPriceVND.ToFloat())) + "</td>");
                    StrExport.Append("      <td>" + PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status)) + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "ExcelReportOrderList.xls";
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

        #region Button status
        protected void btn0_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 0;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
            ddlStatus.SelectedValue = "0";
            hdfStatus.Value = "0";
            gr.Rebind();
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 1;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
            ddlStatus.SelectedValue = "1";
            hdfStatus.Value = "1";
            gr.Rebind();
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 2;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
            ddlStatus.SelectedValue = "2";
            hdfStatus.Value = "2";
            gr.Rebind();
        }

        protected void btn5_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 5;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
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
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 6;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
            ddlStatus.SelectedValue = "6";
            hdfStatus.Value = "6";
            gr.Rebind();
        }

        protected void btn7_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 7;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
            ddlStatus.SelectedValue = "7";
            gr.Rebind();
        }

        protected void btn9_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 9;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
            ddlStatus.SelectedValue = "9";
            hdfStatus.Value = "9";
            gr.Rebind();
        }

        protected void btn10_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = 10;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
            ddlStatus.SelectedValue = "10";
            hdfStatus.Value = "10";
            gr.Rebind();
        }

        protected void bttnAll_Click(object sender, EventArgs e)
        {
            //string ty = "1";
            //if (Request.QueryString["t"] != null)
            //    ty = Request.QueryString["t"];
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //int status = -1;
            //Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
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
        [WebMethod]
        public static string updatedathang(int dathangID, int mID)
        {
            var main = MainOrderController.GetAllByID(mID);
            if (main != null)
            {
                string kq = MainOrderController.UpdateDathang(mID, dathangID);
                return kq.ToInt(0).ToString();
            }
            return "";
        }
        [WebMethod]
        public static string updatesaler(int salerID, int mID)
        {
            var main = MainOrderController.GetAllByID(mID);
            if (main != null)
            {
                string kq = MainOrderController.UpdateSaler(mID, salerID);
                return kq.ToInt(0).ToString();
            }
            return "";
        }

       
    }
}