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
    public partial class Danh_sach_don_hang_cho_giao : System.Web.UI.Page
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
                //  LoadData();
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
            int Stt = Convert.ToInt32(ddlStatus.SelectedValue);
            int totalRow = MainOrderController.GetTotal(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower(), Stt);
            int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : RadGrid2.PageSize;
            RadGrid2.VirtualItemCount = totalRow;
            int Page = (ShouldApplySortFilterOrGroup()) ? 0 : RadGrid2.CurrentPageIndex;

            double tongsoluong = 0;
            double tongcannang = 0;
            double tongtienhang = 0;
            double tongtientra = 0;
            double tongtienconlai = 0;

            var ListOrder = MainOrderController.GetAllOrderByWarehouseVN(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower(), Stt, Page, maximumRows);
            RadGrid2.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
            List<MainOrderWarehouseVN> rs_gr = new List<MainOrderWarehouseVN>();
            if (ListOrder.Count > 0)
            {
                double TotalPay = 0;
                foreach (var o in ListOrder)
                {
                    string Status = "";
                    MainOrderWarehouseVN rs = new MainOrderWarehouseVN();
                    string TranOrder = "";
                    double TotalWeight = 0;
                    double TotalQuantity = 0;

                    var smallpack = SmallPackageController.GetByMainOrderID(Convert.ToInt32(o.ID)).Where(x=>x.Status == 3).ToList();
                    if (smallpack.Count > 0)
                    {
                        foreach (var item in smallpack)
                        {
                            rs.TotalQuantity += 1;

                            double weigthQD = 0;

                            double pDai = Convert.ToDouble(item.Length);
                            double pRong = Convert.ToDouble(item.Width);
                            double pCao = Convert.ToDouble(item.Height);
                            if (pDai > 0 && pRong > 0 && pCao > 0)
                            {
                                weigthQD = (pDai * pRong * pCao) / 6000;
                            }
                            double cantinhtien = weigthQD;
                            if (Convert.ToDouble(item.Weight) > weigthQD)
                            {
                                cantinhtien = Convert.ToDouble(item.Weight);
                            }
                            TotalWeight += cantinhtien;

                            TranOrder += item.OrderTransactionCode + "</br>";
                        }
                    }
                    var order = OrderController.GetByMainOrderID(o.ID);
                    if (order.Count > 0)
                    {
                        foreach (var itemo in order)
                        {
                            TotalQuantity += Convert.ToDouble(itemo.quantity);
                        }
                    }
                    double totalpriceVND = 0;
                    double deposited = 0;
                    if (o.TotalPriceVND.ToFloat(0) > 0)
                    {
                        totalpriceVND = Convert.ToDouble(o.TotalPriceVND);
                    }
                    if (o.Deposit.ToFloat(0) > 0)
                    {
                        deposited = Convert.ToDouble(o.Deposit);
                    }


                    TotalPay = totalpriceVND - deposited;

                    if (o.Status == 7)
                    {
                        Status = "Đã về kho VN";
                    }
                    if (o.Status == 8)
                    {
                        Status = "Chờ thanh toán";
                    }
                    if (o.Status == 9)
                    {
                        Status = "Khách đã thanh toán";
                    }
                    var ac = AccountController.GetByID(Convert.ToInt32(o.UID));

                    rs.ID = o.ID;
                    rs.Username = ac.Username;
                    rs.TranOrder = TranOrder;

                    rs.TotalWeight = Math.Round(Convert.ToDouble(TotalWeight), 2);

                    rs.CreatedDate = Convert.ToDateTime(o.CreatedDate);
                    rs.Status = Status;
                    rs.TotalDeposit = string.Format("{0:N0}", deposited);
                    rs.totalPriceOrder = string.Format("{0:N0}", totalpriceVND);
                    rs.TotalPay = string.Format("{0:N0}", TotalPay);
                    rs.Phone = o.Phone;
                    rs.Address = o.Address;
                    rs_gr.Add(rs);


                    //var smallpack1 = SmallPackageController.GetByMainOrderID(Convert.ToInt32(o.ID));
                    //if (smallpack1.Count > 0)
                    //{
                    //    foreach (var item in smallpack1)
                    //    {
                    //        if (item.Status >= 3)
                    //        {
                              

                    //        }
                    //    }
                    //}

                    tongsoluong += Convert.ToDouble(rs.TotalQuantity);
                    tongcannang += Math.Round(Convert.ToDouble(rs.TotalWeight), 1);
                    tongtienhang += Convert.ToDouble(rs.totalPriceOrder);
                    tongtientra += Convert.ToDouble(rs.TotalDeposit);
                    tongtienconlai += Convert.ToDouble(rs.TotalPay);


                }
                lbltongsoluong.Text = string.Format("{0:N0}", tongsoluong);
                lbltongcannang.Text = tongcannang.ToString();
                lbltongtienhang.Text = string.Format("{0:N0}", tongtienhang);
                lbltongtientra.Text = string.Format("{0:N0}", tongtientra);
                lbltongtienconlai.Text = string.Format("{0:N0}", tongtienconlai);

                RadGrid2.DataSource = rs_gr;
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            int Stt = Convert.ToInt32(ddlStatus.SelectedValue);
            int totalRow = MainOrderController.GetTotal(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower(), Stt);
            int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : RadGrid2.PageSize;
            RadGrid2.VirtualItemCount = totalRow;
            int Page = (ShouldApplySortFilterOrGroup()) ? 0 : RadGrid2.CurrentPageIndex;
            var ListOrder = MainOrderController.GetAllOrderByWarehouseVN(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), txtUsername.Text.Trim().ToLower(), Stt, Page, maximumRows);
            RadGrid2.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
            List<MainOrderWarehouseVN> rs_gr = new List<MainOrderWarehouseVN>();
            if (ListOrder.Count > 0)
            {
                double tongsoluong = 0;
                double tongcannang = 0;
                double tongtienhang = 0;
                double tongtientra = 0;
                double tongtienconlai = 0;
                double TotalPay = 0;
                double SoSuVi = 0;

                var listUID = new List<int?>();

                foreach (var o in ListOrder)
                {
                    string Status = "";


                    MainOrderWarehouseVN rs = new MainOrderWarehouseVN();
                    string TranOrder = "";
                    double TotalWeight = 0;
                    double TotalQuantity = 0;

                    var smallpack = SmallPackageController.GetByMainOrderID(Convert.ToInt32(o.ID)).Where(x => x.Status == 3).ToList();
                    if (smallpack.Count > 0)
                    {
                        foreach (var item in smallpack)
                        {
                            rs.TotalQuantity += 1;

                            double weigthQD = 0;

                            double pDai = Convert.ToDouble(item.Length);
                            double pRong = Convert.ToDouble(item.Width);
                            double pCao = Convert.ToDouble(item.Height);
                            if (pDai > 0 && pRong > 0 && pCao > 0)
                            {
                                weigthQD = (pDai * pRong * pCao) / 6000;
                            }
                            double cantinhtien = weigthQD;
                            if (Convert.ToDouble(item.Weight) > weigthQD)
                            {
                                cantinhtien = Convert.ToDouble(item.Weight);
                            }
                            TotalWeight += cantinhtien;

                            TranOrder += item.OrderTransactionCode + "</br>";
                        }
                    }
                    var order = OrderController.GetByMainOrderID(o.ID);
                    if (order.Count > 0)
                    {
                        foreach (var itemo in order)
                        {
                            TotalQuantity += Convert.ToDouble(itemo.quantity);
                        }
                    }
                    double totalpriceVND = 0;
                    double deposited = 0;
                    if (o.TotalPriceVND.ToFloat(0) > 0)
                    {
                        totalpriceVND = Convert.ToDouble(o.TotalPriceVND);
                    }
                    if (o.Deposit.ToFloat(0) > 0)
                    {
                        deposited = Convert.ToDouble(o.Deposit);
                    }


                    TotalPay = totalpriceVND - deposited;

                    if (o.Status == 7)
                    {
                        Status = "Đã về kho VN";
                    }
                    if (o.Status == 8)
                    {
                        Status = "Chờ thanh toán";
                    }
                    if (o.Status == 9)
                    {
                        Status = "Khách đã thanh toán";
                    }
                    var ac = AccountController.GetByID(Convert.ToInt32(o.UID));

                    rs.ID = o.ID;
                    rs.Username = ac.Username;
                    rs.TranOrder = TranOrder;
                    rs.TotalWeight = Math.Round(Convert.ToDouble(TotalWeight), 2);

                    rs.CreatedDate = Convert.ToDateTime(o.CreatedDate);
                    rs.Status = Status;
                    rs.TotalDeposit = string.Format("{0:N0}", deposited);
                    rs.totalPriceOrder = string.Format("{0:N0}", totalpriceVND);
                    rs.TotalPay = string.Format("{0:N0}", TotalPay);
                    rs.Phone = o.Phone;
                    rs.Address = o.Address;
                    rs_gr.Add(rs);


                    //var smallpack1 = SmallPackageController.GetByMainOrderID(Convert.ToInt32(o.ID));
                    //if (smallpack1.Count > 0)
                    //{
                    //    foreach (var item in smallpack1)
                    //    {
                    //        if (item.Status >= 3)
                    //        {
                    //            rs.TotalQuantity = TotalQuantity;

                    //        }
                    //    }
                    //}

                    tongsoluong += Convert.ToDouble(rs.TotalQuantity);
                    tongcannang += Math.Round(Convert.ToDouble(rs.TotalWeight), 1);
                    tongtienhang += Convert.ToDouble(rs.totalPriceOrder);
                    tongtientra += Convert.ToDouble(rs.TotalDeposit);
                    tongtienconlai += Convert.ToDouble(rs.TotalPay);

                    if (!listUID.Contains(o.UID))
                    {
                        listUID.Add(o.UID);
                        SoSuVi += (double)ac.Wallet;
                    }
                   
                }
                lbltongsoluong.Text = string.Format("{0:N0}", tongsoluong);
                lbltongcannang.Text = tongcannang.ToString();
                lbltongtienhang.Text = string.Format("{0:N0}", tongtienhang);
                lbltongtientra.Text = string.Format("{0:N0}", tongtientra);
                lbltongtienconlai.Text = string.Format("{0:N0}", tongtienconlai);

                lblSoduvi.Text = string.Format("{0:N0}", SoSuVi);

                lblSodusauxk.Text = string.Format("{0:N0}", SoSuVi - tongtienconlai);

                RadGrid2.DataSource = rs_gr;
                //RadGrid2.DataSource = rs_gr;
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


        public class MainOrderWarehouseVN
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double TotalWeight { get; set; }
            public double TotalQuantity { get; set; }
            public string Phone { get; set; }
            public string Status { get; set; }
            public string TotalDeposit { get; set; }
            public string TotalPay { get; set; }
            public string totalPriceOrder { get; set; }
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