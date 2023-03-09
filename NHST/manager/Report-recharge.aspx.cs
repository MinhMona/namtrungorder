using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class Report_recharge : System.Web.UI.Page
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
            //var phone = Session["userLoginSystem"].ToString();
            //var prefix = Session["userLoginSystemPrefix"].ToString();
            //string currentUser = prefix + phone;
            //var usercurrent = AccountController.GetByPhone(phone, prefix);
            //var trans = TransactionController.GetAllByFilter(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate), Convert.ToInt32(ddlTransactionType.SelectedValue));
            //if (trans.Count > 0)
            //{
            //    foreach (var item in trans)
            //    {
            //        if (item.UserIDLost == currentUser || item.UserIDReceive == currentUser)
            //        {
            //            ltrHistory.Text += "<tr>";
            //            ltrHistory.Text += "<td>" + item.ID + "</td>";
            //            ltrHistory.Text += "<td>" + item.UserIDLost + "</td>";
            //            ltrHistory.Text += "<td>" + item.UserIDReceive + "</td>";
            //            ltrHistory.Text += "<td>" + string.Format("{0:N0}", item.Price) + " VNĐ</td>";
            //            ltrHistory.Text += "<td>" + item.TransactionTypeName + "</td>";
            //            if (item.IsSMS == true)
            //                ltrHistory.Text += "<td>1,000 VNĐ</td>";
            //            else
            //                ltrHistory.Text += "<td>0</td>";
            //            ltrHistory.Text += "<td>" + string.Format("{0:N0}", item.Discount) + "</td>";
            //            ltrHistory.Text += "<td>" + string.Format("{0:dd/MM/yy hh:mm}", item.CreatedDate) + "</td>";
            //            if (item.UserIDLost == currentUser)
            //            {
            //                //ltrHistory.Text += "<td>"+ string.Format("{0:N0}", TransactionUtils.GetTotalUserAmountPay(currentUser)) + "</td>";
            //                ltrHistory.Text += "<td>-</td>";
            //            }
            //            else if (item.UserIDReceive == currentUser)
            //            {
            //                //ltrHistory.Text += "<td>" + string.Format("{0:N0}", TransactionUtils.GetTotalUserAmountReceive(currentUser)) + "</td>";
            //                ltrHistory.Text += "<td>+</td>";
            //            }
            //            ltrHistory.Text += "</tr>";
            //        }

            //    }
            //}
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            var acc = Session["userLoginSystem"].ToString();
            #region Thống kê thanh toán
            var history = AdminSendUserWalletController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
            if (history.Count > 0)
            {
                //List<PayHistory> ro_gr1 = new List<PayHistory>();
                //foreach (var o in history)
                //{
                //    int stt = Convert.ToInt32(o.Status);
                //    string status = "";
                //    PayHistory r = new PayHistory();
                //    r.MainOrderID = o.MainOrderID.ToString().ToInt();
                //    r.Username = AccountController.GetByID(o.UID.ToString().ToInt(1)).Username;
                //    if (stt == 2)
                //        status = "Đặt cọc đơn hàng";
                //    else
                //        status = "Thanh toán đơn hàng";
                //    r.Status = status;
                //    r.Amount = string.Format("{0:N0}", o.Amount.ToString().ToFloat(0)) + " VNĐ";
                //    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                //    r.CreatedBy = AccountController.GetByUsername(o.CreatedBy).Username;
                //    ro_gr1.Add(r);
                //}
                double totalprice = 0;
                foreach (var item in history)
                {
                    totalprice += Convert.ToDouble(item.Amount);
                }
                lblDeposit.Text = string.Format("{0:N0}", totalprice);
                RadGrid1.DataSource = history;
                RadGrid1.DataBind();
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

                    //if (stt > 2 && stt < 9)
                    //{                     

                    //}
                    //else if (stt < 2)
                    //{
                    //    currentOrderPriceLeft = o.TotalPriceVND.ToFloat(0);
                    //}

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

                //gr.DataSource = ro_gr;
                //gr.DataBind();
            }
        }
        public void LoadGrid1()
        {
            var history = AdminSendUserWalletController.GetAll("");
            if (history.Count > 0)
            {
                var outhis = history.Where(l => l.Status == 2).ToList();
                double totalprice = 0;
                foreach (var item in outhis)
                {
                    totalprice += Convert.ToDouble(item.Amount);
                }
                lblDeposit.Text = string.Format("{0:N0}", totalprice);
                RadGrid1.DataSource = outhis;
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
            //gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            //gr.Rebind();
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
    }
}