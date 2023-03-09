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
namespace NHST.manager
{
    public partial class PayOrder_Internal : System.Web.UI.Page
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
            int Site = Convert.ToInt32(ddlSite.SelectedValue);
            var la = MainOrderController.GetOrderbyPaying(tSearchName.Text.Trim(), Status,Site, ispaying, txtCreatedBy.Text);
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
                            CurrentCYN =Convert.ToDouble( o.CurrentCNYVN);
                            shipfeeVND = Convert.ToDouble(o.FeeShipCN);
                            feeshipcyn = shipfeeVND / CurrentCYN;
                        }
                        totalpricecyn = Convert.ToDouble( o.TotalPriceRealCYN);
                        rs.ID = o.ID;
                        rs.anhsanpham = o.anhsanpham;
                        rs.Site = o.Site;
                        rs.Uname = o.Uname;
                        rs.dathang = o.dathang;
                        rs.OrderTransactionCode = TranOrder;
                        rs.MainOrderCode = o.MainOrderCode;
                        rs.TotalPriceCYN =  Math.Round(totalpricecyn,2).ToString();
                        rs.statusstring = o.statusstring;
                        rs.IsPaying = o.IsPaying;
                        rs.CreatedDate = o.CreatedDate;
                       
                        rs_gr.Add(rs);
                    }

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

        [WebMethod]
        public static string UpdateIsPaying(int ID, bool IsPaying)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    var mo = MainOrderController.GetAllByID(ID);
                    var c = MainOrderController.UpdatePaying(ID, IsPaying, currentDate,username);
                    if (IsPaying == true)
                    {
                        if (mo.Status < 6 )
                        {
                            MainOrderController.UpdateStatus_DateTime(ID, mo.UID.ToString().ToInt(), 5, currentDate);
                        }
                        HistoryOrderChangeController.Insert(ID, user.ID, user.Username, user.Username +
                                      " đã đổi trạng thái của đơn hàng ID là: " + ID + ", sang: " + "Đã thanh toán cho shop" + "", 0, currentDate);

                        HistoryOrderChangeController.Insert(ID, user.ID, user.Username, user.Username +
                                      " đã đổi trạng thái của đơn hàng ID là: " + ID + ", sang: " + "Chờ shop phát hàng" + "", 0, currentDate);
                    }
                    if (IsPaying == false)
                    {
                        if (mo.Status < 6)
                        {
                            MainOrderController.UpdateStatus(ID, mo.UID.ToString().ToInt(), 2);
                        }
                        HistoryOrderChangeController.Insert(ID, user.ID, user.Username, user.Username +
                                      " đã đổi trạng thái của đơn hàng ID là: " + ID + ", sang: " + "Đang mua hàng" + "", 0, currentDate);

                       
                    }

                    if (c.ToInt(0) > 0)
                    {
                        return c;
                    }
                    else return "none";
                }
            }
            return "none";
        }

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
            public string MainOrderCode { get; set; }
            public string saler { get; set; }
            public string dathangstr { get; set; }
            public string salerstr { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
            public string hasSmallpackage { get; set; }
        }
    }
}