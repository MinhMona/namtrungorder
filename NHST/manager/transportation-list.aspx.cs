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
    public partial class transportation_list : System.Web.UI.Page
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

                    LoadDDL();
                }
            }
        }
        #region grid event
        public void LoadDDL()
        {
            ddlWarehouseFrom.Items.Clear();
            ddlWarehouseFrom.Items.Insert(0, new ListItem("---Tất cả---", "0"));
            var warehousefrom = WarehouseFromController.GetAllWithIsHidden(false);
            if (warehousefrom.Count > 0)
            {
                foreach (var item in warehousefrom)
                {
                    ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlWarehouseFrom.Items.Add(listitem);
                }
            }
            ddlWarehouseFrom.DataBind();

            ddlWarehouseTo.Items.Clear();
            ddlWarehouseTo.Items.Insert(0, new ListItem("---Tất cả---", "0"));
            var warehouse = WarehouseController.GetAllWithIsHidden(false);
            if (warehouse.Count > 0)
            {
                foreach (var item in warehouse)
                {
                    ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlWarehouseTo.Items.Add(listitem);
                }
            }
            ddlWarehouseTo.DataBind();


            ddlShippingType.Items.Clear();
            ddlShippingType.Items.Insert(0, new ListItem("---Tất cả---", "0"));
            var shippingtype = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            if (shippingtype.Count > 0)
            {
                foreach (var item in shippingtype)
                {
                    ListItem listitem = new ListItem(item.ShippingTypeName, item.ID.ToString());
                    ddlShippingType.Items.Add(listitem);
                }
            }
            ddlShippingType.DataBind();
        }

        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                string s = tSearchName.Text.Trim();
                int wfrom = ddlWarehouseFrom.SelectedValue.ToInt();
                int wto = ddlWarehouseTo.SelectedValue.ToInt();
                int shippingtype = ddlShippingType.SelectedValue.ToInt();
                double priceFrom = Convert.ToDouble(rPriceFrom.Value);
                double priceTo = Convert.ToDouble(rPriceTo.Value);
                string fromdate = rFD.SelectedDate.ToString();
                string todate = rTD.SelectedDate.ToString();
                string status1 = hdfStatus.Value;
                List<tbl_TransportationOrder> tList = new List<tbl_TransportationOrder>();
                var ts = TransportationOrderController.GetAll("");
                if (!string.IsNullOrEmpty(s))
                {
                    foreach (var t in ts)
                    {
                        int tID = t.ID;
                        var check = false;
                        var transportationDetails = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                        if (transportationDetails.Count > 0)
                        {
                            foreach (var d in transportationDetails)
                            {
                                if (d.TransportationOrderCode == s)
                                {
                                    check = true;
                                }
                            }
                        }
                        if (check == false)
                        {
                            var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (smallpackages.Count > 0)
                            {
                                foreach (var small in smallpackages)
                                {
                                    if (small.OrderTransactionCode == s)
                                    {
                                        check = true;
                                    }
                                }
                            }
                        }
                        if (check == true)
                        {
                            tList.Add(t);
                        }
                    }
                }
                else
                {
                    tList = ts;
                }
                if (wfrom > 0)
                {
                    tList = tList.Where(t => t.WarehouseFromID == wfrom).ToList();
                }
                if (wto > 0)
                {
                    tList = tList.Where(t => t.WarehouseID == wto).ToList();
                }
                if (shippingtype > 0)
                {
                    tList = tList.Where(t => t.ShippingTypeID == shippingtype).ToList();
                }
                if (priceTo > 0)
                {
                    tList = tList.Where(t => t.TotalPrice >= priceFrom && t.TotalPrice <= priceTo).ToList();
                }
                if (!string.IsNullOrEmpty(fromdate))
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime fd = DateTime.Parse(fromdate);
                        DateTime td = DateTime.Parse(todate);
                        tList = tList.Where(t => t.CreatedDate >= fd && t.CreatedDate <= td).ToList();
                    }
                    else
                    {
                        DateTime fd = DateTime.Parse(fromdate);
                        tList = tList.Where(t => t.CreatedDate >= fd).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime td = DateTime.Parse(todate);
                        tList = tList.Where(t => t.CreatedDate <= td).ToList();
                    }
                }
                if (status1 != "-1")
                {
                    var la1 = new List<tbl_TransportationOrder>();
                    string[] sts = status1.Split(',');
                    for (int i = 0; i < sts.Length; i++)
                    {
                        int stat = sts[i].ToInt();
                        if (stat > -1)
                        {
                            var la2 = new List<tbl_TransportationOrder>();
                            la2 = tList.Where(o => o.Status == stat).ToList();
                            if (la2.Count > 0)
                            {
                                foreach (var item in la2)
                                {
                                    la1.Add(item);
                                }
                            }
                        }
                    }
                    la1 = la1.OrderByDescending(o => o.ID).ToList();
                    gr.VirtualItemCount = la1.Count;
                    gr.DataSource = la1;
                }
                else
                {
                    if(tList.Count>0)
                    {
                        gr.VirtualItemCount = tList.Count;
                        gr.DataSource = tList;
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
    }
}