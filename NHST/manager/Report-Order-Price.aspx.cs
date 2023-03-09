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
    public partial class Report_Order_Price : System.Web.UI.Page
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
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");
                }
            }
        }
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                string fromdate = rdatefrom.SelectedDate.ToString();
                string todate = rdateto.SelectedDate.ToString();
                if(!string.IsNullOrEmpty(rdatefrom.SelectedDate.ToString()) ||
                   !string.IsNullOrEmpty(rdateto.SelectedDate.ToString()))
                {
                    var la = MainOrderController.GetFromDateToDateAndFromStatus(fromdate, todate, 10);
                    if (la.Count > 0)
                    {
                        List<MainOrderReport> mos = new List<MainOrderReport>();
                        foreach (var o in la)
                        {
                            MainOrderReport m = new MainOrderReport();
                            m.OrderID = o.ID;
                            double totalOrder = 0;
                            double totalOrderRealPrice = 0;
                            if (o.TotalPriceVND.ToFloat(0) > 0)
                                totalOrder = Convert.ToDouble(o.TotalPriceVND);
                            if (o.TotalPriceReal.ToFloat(0) > 0)
                                totalOrderRealPrice = Convert.ToDouble(o.TotalPriceReal);
                            m.TotalOrder = string.Format("{0:N0}", totalOrder);
                            m.TotalOrderRealPrice = string.Format("{0:N0}", totalOrderRealPrice);
                            m.Status = PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status));
                            m.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", o.CreatedDate);
                            double totalIncome = totalOrder - totalOrderRealPrice;
                            m.TotalIncome = string.Format("{0:N0}", totalIncome);
                            mos.Add(m);
                        }
                        if (mos.Count > 0)
                        {
                            gr.DataSource = mos;
                        }
                    }
                }
                else
                {
                    var la1 = MainOrderController.GetFromStatus(10);
                    if (la1.Count > 0)
                    {
                        List<MainOrderReport> mos = new List<MainOrderReport>();
                        foreach (var o in la1)
                        {
                            MainOrderReport m = new MainOrderReport();
                            m.OrderID = o.ID;
                            double totalOrder = 0;
                            double totalOrderRealPrice = 0;
                            if (o.TotalPriceVND.ToFloat(0) > 0)
                                totalOrder = Convert.ToDouble(o.TotalPriceVND);
                            if (o.TotalPriceReal.ToFloat(0) > 0)
                                totalOrderRealPrice = Convert.ToDouble(o.TotalPriceReal);
                            m.TotalOrder = string.Format("{0:N0}",totalOrder);
                            m.TotalOrderRealPrice = string.Format("{0:N0}", totalOrderRealPrice);
                            m.Status = PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status));
                            m.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", o.CreatedDate);
                            double totalIncome = totalOrder - totalOrderRealPrice;
                            m.TotalIncome = string.Format("{0:N0}", totalIncome);
                            mos.Add(m);
                        }
                        if (mos.Count > 0)
                        {
                            gr.DataSource = mos;
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }

        public class MainOrderReport
        {
            public int OrderID { get; set; }
            public string TotalOrder { get; set; }
            public string TotalOrderRealPrice { get; set; }
            public string TotalIncome { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}