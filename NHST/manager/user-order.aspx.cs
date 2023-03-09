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
using static NHST.Controllers.MainOrderController;

namespace NHST.manager
{
    public partial class user_order : System.Web.UI.Page
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
                        if (ac.RoleID != 2 && ac.RoleID != 0 && ac.RoleID != 6)
                            Response.Redirect("/trang-chu");
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
            int uID = 0;
            if (Request.QueryString["uid"] != null)
            {
                uID = Request.QueryString["uid"].ToInt(0);
            }
            if (uID > 0)
            {
                var a = AccountController.GetByID(uID);
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                if (a.SaleID == ac.ID || ac.RoleID == 0 || ac.RoleID == 2)
                {
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

                        if (string.IsNullOrEmpty(s) && priceFrom == 0 && priceTo == 0 && string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate) &&
                            status1 == "-1" && chkIsnotcode.Checked == false)
                        {
                            int RoleID = Convert.ToInt32(ac.RoleID);
                            int StaffID = ac.ID;

                            //int totalRow = MainOrderController.getOrderByUID_SQL(uID);
                            int totalRow = 0;
                            var totalOrders = MainOrderController.GetByUserIDInSQLHelper_WithNoPaging(uID);
                            if (totalOrders.Count > 0)
                            {
                                double totalPriceAll = 0;
                                double depositAll = 0;
                                double totalLeft = 0;
                                totalRow = totalOrders.Count;
                                foreach (var o in totalOrders)
                                {
                                    double totalprice = 0;
                                    double deposit = 0;
                                    if (o.TotalPriceVND.ToFloat(0) > 0)
                                        totalprice = Convert.ToDouble(o.TotalPriceVND);
                                    if (o.Deposit.ToFloat(0) > 0)
                                        deposit = Convert.ToDouble(o.Deposit);

                                    totalPriceAll += totalprice;
                                    depositAll += deposit;
                                }
                                totalLeft = totalPriceAll - depositAll;
                                lblTotalPrice.Text = string.Format("{0:N0}", totalPriceAll) + " VNĐ";
                                lblTotaldeposit.Text = string.Format("{0:N0}", depositAll) + " VNĐ";
                                lblTotalNotPay.Text = string.Format("{0:N0}", totalLeft) + " VNĐ";
                            }
                            int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : gr.PageSize;
                            gr.VirtualItemCount = totalRow;
                            int Page = (ShouldApplySortFilterOrGroup()) ? 0 : gr.CurrentPageIndex;
                            var lo = MainOrderController.GetByUserIDInSQLHelper_WithPaging(uID, Page, maximumRows);
                            gr.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
                            gr.DataSource = lo;
                        }
                        else
                        {
                            #region Cách mới
                            string fd = rFD.SelectedDate.ToString();
                            string td = rTD.SelectedDate.ToString();
                            var la = MainOrderController.GetByUserIDInSQLHelperWithFilter(uID, tSearchName.Text.Trim(),
                               ddlType.SelectedValue.ToString().ToInt(1), fd, td, priceFrom, priceTo, chkIsnotcode.Checked);
                            double totalPriceAll = 0;
                            double depositAll = 0;
                            double totalLeft = 0;
                            if (la.Count > 0)
                            {
                                if (status1 != "-1")
                                {
                                    var la1 = new List<OrderGetSQL>();
                                    string[] sts = status1.Split(',');
                                    for (int i = 0; i < sts.Length; i++)
                                    {
                                        int stat = sts[i].ToInt();
                                        if (stat > -1)
                                        {
                                            var la2 = new List<OrderGetSQL>();
                                            la2 = la.Where(o => o.Status == stat).ToList();
                                            if (la2.Count > 0)
                                            {
                                                foreach (var item in la2)
                                                {
                                                    la1.Add(item);
                                                }
                                            }
                                        }
                                    }
                                    foreach (var o in la1)
                                    {
                                        double totalprice = 0;
                                        double deposit = 0;
                                        if (o.TotalPriceVND.ToFloat(0) > 0)
                                            totalprice = Convert.ToDouble(o.TotalPriceVND);
                                        if (o.Deposit.ToFloat(0) > 0)
                                            deposit = Convert.ToDouble(o.Deposit);

                                        totalPriceAll += totalprice;
                                        depositAll += deposit;
                                    }
                                    totalLeft = totalPriceAll - depositAll;
                                    gr.VirtualItemCount = la1.Count;
                                    gr.DataSource = la1;
                                }
                                else
                                {
                                    foreach (var o in la)
                                    {
                                        double totalprice = 0;
                                        double deposit = 0;
                                        if (o.TotalPriceVND.ToFloat(0) > 0)
                                            totalprice = Convert.ToDouble(o.TotalPriceVND);
                                        if (o.Deposit.ToFloat(0) > 0)
                                            deposit = Convert.ToDouble(o.Deposit);

                                        totalPriceAll += totalprice;
                                        depositAll += deposit;
                                    }
                                    totalLeft = totalPriceAll - depositAll;
                                    gr.VirtualItemCount = la.Count;
                                    gr.DataSource = la;

                                }

                            }
                            else
                            {
                                gr.VirtualItemCount = la.Count;
                                gr.DataSource = la;
                            }
                            lblTotalPrice.Text = string.Format("{0:N0}", totalPriceAll) + " VNĐ";
                            lblTotaldeposit.Text = string.Format("{0:N0}", depositAll) + " VNĐ";
                            lblTotalNotPay.Text = string.Format("{0:N0}", totalLeft) + " VNĐ";
                            #endregion
                        }
                    }
                }
                else Response.Redirect("/manager/saler-customer-list");
            }
            else
            {
                Response.Redirect("/manager/home.aspx");
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
    }
}