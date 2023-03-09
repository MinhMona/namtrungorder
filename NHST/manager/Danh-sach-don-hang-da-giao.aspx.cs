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
using Telerik.Web.UI;
using ZLADIPJ.Business;
using System.Data;
using WebUI.Business;
using System.Text;

namespace NHST.manager
{
    public partial class Danh_sach_don_hang_da_giao : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7 && obj_user.RoleID != 5)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                //LoadData();
            }
        }

        //public void LoadData()
        //{
        //    rdatefrom.SelectedDate = DateTime.Now;
        //    rdateto.SelectedDate = DateTime.Now.AddDays(30);
        //}
        public bool ShouldApplySortFilterOrGroup()
        {
            return RadGrid2.MasterTableView.FilterExpression != "" ||
                (RadGrid2.MasterTableView.GroupByExpressions.Count > 0 || isGrouping) ||
                RadGrid2.MasterTableView.SortExpressions.Count > 0;
        }
        bool isGrouping = false;

        public void LoadGrid2()
        {

            int totalRow = OutStockSessionController.GetTotal(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower());
            int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : RadGrid2.PageSize;
            RadGrid2.VirtualItemCount = totalRow;
            int Page = (ShouldApplySortFilterOrGroup()) ? 0 : RadGrid2.CurrentPageIndex;

            var ListOut = OutStockSessionController.GetAllOutStock(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower(), Page, maximumRows);
            RadGrid2.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
            List<OutStockSession> rs_gr = new List<OutStockSession>();
            if (ListOut.Count > 0)
            {

                foreach (var o in ListOut)
                {
                    string Status = "";
                    string addresss = "";
                    double TotalPay = 0;
                    OutStockSession rs = new OutStockSession();
                    string TranOrder = "";
                    double TotalWeight = 0;
                    int TotalPackages = 0;
                    var ac = AccountInfoController.GetByUserID(Convert.ToInt32(o.UID));
                    if (ac.ID > 0)
                    {
                        addresss = ac.Address;
                    }

                    var re = OutStockSessionPackageController.GetAllByOutStockSessionID(o.ID);
                    if (re.Count > 0)
                    {

                        TotalPackages = re.Count;
                        foreach (var item in re)
                        {
                            var smallpack = SmallPackageController.GetByID(Convert.ToInt32(item.SmallPackageID));
                            if (smallpack != null)
                            {
                                TotalWeight += Convert.ToDouble(smallpack.Weight);
                                TranOrder += smallpack.OrderTransactionCode + "</br>";

                            }
                        }
                    }
                    if (o.TotalPay > 0)
                        TotalPay = Convert.ToDouble(o.TotalPay);
                    if (o.Status == 0)
                    {
                        Status = "Đã yêu cầu";
                    }
                    if (o.Status == 2)
                    {
                        Status = "Đã hoàn thành";
                    }

                    rs.ID = o.ID;
                    rs.Username = o.Username;
                    rs.TranOrder = TranOrder;
                    rs.TotalWeight = TotalWeight;
                    rs.CreatedDate = Convert.ToDateTime(o.CreatedDate);
                    rs.Status = Status;
                    rs.TotalPay = string.Format("{0:N0}", TotalPay);
                    rs.Phone = o.Phone;
                    rs.Address = addresss;
                    rs_gr.Add(rs);
                }

                RadGrid2.DataSource = rs_gr;
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            int totalRow = OutStockSessionController.GetTotal(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower());
            int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : RadGrid2.PageSize;
            RadGrid2.VirtualItemCount = totalRow;
            int Page = (ShouldApplySortFilterOrGroup()) ? 0 : RadGrid2.CurrentPageIndex;

            var ListOut = OutStockSessionController.GetAllOutStock(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower(), Page, maximumRows);
            RadGrid2.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
            List<OutStockSession> rs_gr = new List<OutStockSession>();
            if (ListOut.Count > 0)
            {

                foreach (var o in ListOut)
                {
                    string Status = "";
                    string addresss = "";
                    double TotalPay = 0;
                    OutStockSession rs = new OutStockSession();
                    string TranOrder = "";
                    double TotalWeight = 0;
                    int TotalPackages = 0;
                    var ac = AccountInfoController.GetByUserID(Convert.ToInt32(o.UID));
                    if (ac.ID > 0)
                    {
                        addresss = ac.Address;
                    }

                    var re = OutStockSessionPackageController.GetAllByOutStockSessionID(o.ID);
                    if (re.Count > 0)
                    {

                        TotalPackages = re.Count;
                        foreach (var item in re)
                        {
                            var smallpack = SmallPackageController.GetByID(Convert.ToInt32(item.SmallPackageID));
                            if (smallpack != null)
                            {
                                TotalWeight += Convert.ToDouble(smallpack.Weight);
                                TranOrder += smallpack.OrderTransactionCode + "</br>";

                            }
                        }
                    }
                    if (o.TotalPay > 0)
                        TotalPay = Convert.ToDouble(o.TotalPay);
                    if (o.Status == 0)
                    {
                        Status = "Đã yêu cầu";
                    }
                    if (o.Status == 2)
                    {
                        Status = "Đã hoàn thành";
                    }

                    rs.ID = o.ID;
                    rs.Username = o.Username;
                    rs.TranOrder = TranOrder;
                    rs.TotalWeight = TotalWeight;
                    rs.CreatedDate = Convert.ToDateTime(o.CreatedDate);
                    rs.Status = Status;
                    rs.TotalPay = string.Format("{0:N0}", TotalPay);
                    rs.Phone = o.Phone;
                    rs.Address = addresss;
                    rs_gr.Add(rs);
                }

                RadGrid2.DataSource = rs_gr;
                RadGrid2.DataBind();
            }
        }



        protected void RadGrid2_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            LoadGrid2();
            //RadGrid2.Rebind();
        }

        protected void RadGrid2_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void RadGrid2_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            LoadGrid2();
            RadGrid2.Rebind();
        }

        protected void RadGrid2_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            LoadGrid2();
            RadGrid2.Rebind();
        }


        public class OutStockSession
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double TotalWeight { get; set; }
            public string Phone { get; set; }
            public string Status { get; set; }
            public string TotalPay { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Address { get; set; }
            public string TranOrder { get; set; }

        }

        public class ReporStaff
        {
            public int ID { get; set; }
            public int MainOrderID { get; set; }
            public int UID { get; set; }
            public string FullName { get; set; }
            public string Username { get; set; }
            public int RoleID { get; set; }
            public string Position { get; set; }
            public string OrderTotalPrice { get; set; } //tổng tiền hàng 1 đơn hàng
            public string TotalPriceReceive { get; set; }//tổng hoa hồng nhận 1 đơn hàng
            public string TotalIncomUnpaid { get; set; }//tổng hoa hồng nhận 1 đơn hàng
            public string TotalIncompaid { get; set; }//tổng hoa hồng nhận 1 đơn hàng
            public string PercentReceive { get; set; }
            public int quantity { get; set; }// số lượng
            public int Status { get; set; }
            public int CreatedDateInt { get; set; }
            public string CreatedDateStr { get; set; }
            public string CreatedDate { get; set; }

        }
    }

}