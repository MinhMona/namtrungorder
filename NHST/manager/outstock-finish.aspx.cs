using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using MB.Extensions;
using System.Text;

namespace NHST.manager
{
    public partial class outstock_finish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 5 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            if (Request.QueryString["id"] != null)
            {
                int id = Request.QueryString["id"].ToInt(0);
                if (id > 0)
                {
                    ViewState["id"] = id;
                    var os = OutStockSessionController.GetByID(id);
                    if (os != null)
                    {
                        bool isShowButton = true;
                        double totalPriceMustPay = 0;
                        List<OrderPackage> ops = new List<OrderPackage>();
                        #region Đơn hàng mua hộ
                        var listmainorder = OutStockSessionPackageController.GetByOutStockSessionIDGroupByMainOrderID(id);
                        if (listmainorder.Count > 0)
                        {
                            foreach (var m in listmainorder)
                            {
                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(m));
                                if (mainorder != null)
                                {
                                    int mID = mainorder.ID;
                                    double totalPay = 0;
                                    OrderPackage op = new OrderPackage();
                                    op.OrderID = Convert.ToInt32(m);
                                    op.OrderType = 1;
                                    List<SmallpackageGet> sms = new List<SmallpackageGet>();
                                    var packsmain = OutStockSessionPackageController.GetAllByOutStockSessionIDAndMainOrderID(id, Convert.ToInt32(m));
                                    if (packsmain.Count > 0)
                                    {
                                        foreach (var p in packsmain)
                                        {
                                            var sm = SmallPackageController.GetByID(Convert.ToInt32(p.SmallPackageID));
                                            if (sm != null)
                                            {
                                                SmallpackageGet pg = new SmallpackageGet();
                                                if (sm.Status != 3)
                                                {
                                                    isShowButton = false;
                                                }
                                                double weight = Convert.ToDouble(sm.Weight);

                                                string packagecode = sm.OrderTransactionCode;
                                                int Status = Convert.ToInt32(sm.Status);
                                                double payInWarehouse = 0;

                                                pg.ID = sm.ID;
                                                pg.weight = weight;

                                                pg.packagecode = packagecode;
                                                pg.Status = Status;
                                                var feeweightinware = InWarehousePriceController.GetAll();
                                                double payperday = 0;
                                                double maxday = 0;
                                                foreach (var item in feeweightinware)
                                                {
                                                    if (item.WeightFrom < weight && weight <= item.WeightTo)
                                                    {
                                                        maxday = Convert.ToDouble(item.MaxDay);
                                                        payperday = Convert.ToDouble(item.PricePay);
                                                        break;
                                                    }
                                                }
                                                double totalDays = 0;
                                                if (sm.DateInLasteWareHouse != null)
                                                {
                                                    DateTime diw = Convert.ToDateTime(sm.DateInLasteWareHouse);
                                                    TimeSpan ts = currentDate.Subtract(diw);
                                                    if (ts.TotalDays > 0)
                                                        totalDays = Math.Floor(ts.TotalDays);
                                                }

                                                double dayin = totalDays - maxday;
                                                if (dayin > 0)
                                                {
                                                    payInWarehouse = dayin * payperday * weight;
                                                }
                                                pg.DateInWare = totalDays;
                                                totalPay += payInWarehouse;
                                                pg.payInWarehouse = payInWarehouse;
                                                sms.Add(pg);
                                                SmallPackageController.UpdateWarehouseFeeDateOutWarehouse(sm.ID, payInWarehouse, currentDate);
                                                OutStockSessionPackageController.update(p.ID, currentDate, totalDays, payInWarehouse);
                                            }

                                        }
                                    }
                                    op.totalPrice = totalPay;
                                    op.smallpackages = sms;
                                    double mustpay = 0;
                                    bool isPay = false;
                                    MainOrderController.UpdateFeeWarehouse(mID, totalPay);
                                    var ma = MainOrderController.GetAllByID(mID);
                                    if (ma != null)
                                    {
                                        double totalPriceVND = Convert.ToDouble(ma.TotalPriceVND);
                                        double deposited = Convert.ToDouble(ma.Deposit);
                                        double totalmustpay = totalPriceVND + totalPay;
                                        double totalleftpay = totalmustpay - deposited;
                                        if (totalmustpay <= deposited)
                                        {
                                            isPay = true;
                                        }
                                        else
                                        {
                                            MainOrderController.UpdateStatus(mID, Convert.ToInt32(ma.UID), 7);
                                            mustpay = totalleftpay;
                                        }
                                    }
                                    if (isShowButton == true)
                                    {
                                        if (isPay == false)
                                        {
                                            isShowButton = false;
                                        }
                                    }
                                    op.totalMustPay = mustpay;
                                    op.isPay = isPay;
                                    ops.Add(op);
                                }
                            }
                        }
                        #endregion
                        #region Đơn hàng VC hộ
                        var listtransportation = OutStockSessionPackageController.GetByOutStockSessionIDGroupByTransportationID(id);
                        if (listtransportation.Count > 0)
                        {
                            foreach (var t in listtransportation)
                            {
                                int tID = Convert.ToInt32(t);
                                var tran = TransportationOrderController.GetByID(tID);
                                if (tran != null)
                                {
                                    double totalPay = 0;
                                    OrderPackage op = new OrderPackage();
                                    op.OrderID = tID;
                                    op.OrderType = 2;
                                    List<SmallpackageGet> sms = new List<SmallpackageGet>();
                                    var packsmain = OutStockSessionPackageController.GetAllByOutStockSessionIDAndTransporationID(id, tID);
                                    if (packsmain.Count > 0)
                                    {
                                        foreach (var p in packsmain)
                                        {
                                            var sm = SmallPackageController.GetByID(Convert.ToInt32(p.SmallPackageID));
                                            if (sm != null)
                                            {
                                                SmallpackageGet pg = new SmallpackageGet();
                                                if (sm.Status != 3)
                                                {
                                                    isShowButton = false;
                                                }
                                                double weight = Convert.ToDouble(sm.Weight);
                                                string packagecode = sm.OrderTransactionCode;
                                                int Status = Convert.ToInt32(sm.Status);
                                                double payInWarehouse = 0;

                                                pg.ID = sm.ID;
                                                pg.weight = weight;

                                                pg.packagecode = packagecode;
                                                pg.Status = Status;
                                                var feeweightinware = InWarehousePriceController.GetAll();
                                                double payperday = 0;
                                                double maxday = 0;
                                                foreach (var item in feeweightinware)
                                                {
                                                    if (item.WeightFrom < weight && weight <= item.WeightTo)
                                                    {
                                                        maxday = Convert.ToDouble(item.MaxDay);
                                                        payperday = Convert.ToDouble(item.PricePay);
                                                        break;
                                                    }
                                                }
                                                double totalDays = 0;
                                                if (sm.DateInLasteWareHouse != null)
                                                {
                                                    DateTime diw = Convert.ToDateTime(sm.DateInLasteWareHouse);
                                                    TimeSpan ts = currentDate.Subtract(diw);
                                                    if (ts.TotalDays > 0)
                                                        totalDays = Math.Floor(ts.TotalDays);
                                                }
                                                double dayin = totalDays - maxday;
                                                if (dayin > 0)
                                                {
                                                    payInWarehouse = dayin * payperday * weight;
                                                }
                                                totalPay += payInWarehouse;
                                                pg.DateInWare = totalDays;
                                                pg.payInWarehouse = payInWarehouse;
                                                sms.Add(pg);
                                                SmallPackageController.UpdateWarehouseFeeDateOutWarehouse(sm.ID, payInWarehouse, currentDate);
                                                OutStockSessionPackageController.update(p.ID, currentDate, totalDays, payInWarehouse);
                                            }
                                        }
                                    }
                                    op.totalPrice = totalPay;
                                    op.smallpackages = sms;
                                    double mustpay = 0;
                                    bool isPay = false;
                                    TransportationOrderController.UpdateWarehouseFee(tID, totalPay);
                                    var tr = TransportationOrderController.GetByID(tID);
                                    if (tr != null)
                                    {
                                        double totalPriceVND = Convert.ToDouble(tr.TotalPrice);
                                        double deposited = Convert.ToDouble(tr.Deposited);
                                        double totalmustpay = totalPriceVND + totalPay;
                                        double totalleftpay = totalmustpay - deposited;
                                        if (totalmustpay <= deposited)
                                        {
                                            isPay = true;
                                        }
                                        else
                                        {
                                            TransportationOrderController.UpdateStatus(tID, 5, currentDate, username_current);
                                            mustpay = totalleftpay;
                                        }
                                    }
                                    if (isShowButton == true)
                                    {
                                        if (isPay == false)
                                        {
                                            isShowButton = false;
                                        }
                                    }
                                    op.totalMustPay = mustpay;
                                    op.isPay = isPay;
                                    ops.Add(op);
                                }
                            }
                        }
                        #endregion
                        #region Render Data
                        txtFullname.Text = os.FullName;
                        txtPhone.Text = os.Phone;
                        if (isShowButton == true)
                        {
                            pnButton.Visible = true;
                        }
                        else
                        {
                            //pnButton.Visible = true;
                            pnrefresh.Visible = true;
                        }
                        StringBuilder html = new StringBuilder();
                        StringBuilder htmlPrint = new StringBuilder();
                        if (ops.Count > 0)
                        {
                            foreach (var o in ops)
                            {
                                int orderType = o.OrderType;
                                bool isPay = o.isPay;
                                string status = "<span class=\"bg-blue\">Đã thanh toán</span>";
                                if (o.isPay == false)
                                {
                                    status = "<span class=\"bg-red\">Chưa thanh toán</span>";
                                }

                                html.Append("<article class=\"pane-primary\">");
                                if (orderType == 1)
                                {
                                    if (isPay == true)
                                        html.Append("   <div class=\"heading\"><h3 class=\"lb\" style=\"color:#000\">Đơn hàng mua hộ: #" + o.OrderID + "</h3></div>");
                                    else
                                        html.Append("   <div class=\"heading\" style=\"background:red!important\"><h3 class=\"lb\" style=\"color:#000\">Đơn hàng mua hộ: #" + o.OrderID + "</h3></div>");
                                }
                                else
                                {
                                    if (isPay == true)
                                        html.Append("   <div class=\"heading\"><h3 class=\"lb\" style=\"color:#000\">Đơn hàng VC hộ: #" + o.OrderID + "</h3></div>");
                                    else
                                        html.Append("   <div class=\"heading\" style=\"background:red!important\"><h3 class=\"lb\" style=\"color:#000\">Đơn hàng VC hộ: #" + o.OrderID + "</h3></div>");
                                }

                                html.Append("   <article class=\"pane-primary\">");
                                html.Append("       <table class=\"normal-table full-width\">");
                                html.Append("           <tr>");
                                html.Append("               <th style=\"color:#000\">Mã kiện</th>");
                                html.Append("               <th style=\"color:#000\">Cân nặng (kg)</th>");
                                html.Append("               <th style=\"color:#000\">Ngày lưu kho (ngày)</th>");
                                html.Append("               <th style=\"color:#000\">Trạng thái</th>");
                                html.Append("               <th style=\"color:#000\">Tiền lưu kho</th>");
                                html.Append("           </tr>");
                                var listpackages = o.smallpackages;
                                foreach (var p in listpackages)
                                {
                                    html.Append("           <tr>");
                                    html.Append("               <td>" + p.packagecode + "</td>");
                                    html.Append("               <td>" + p.weight + "</td>");
                                    html.Append("               <td>" + p.DateInWare + "</td>");
                                    html.Append("               <td style=\"color:#000\">" + PJUtils.IntToStringStatusSmallPackage(p.Status) + "</td>");
                                    html.Append("               <td>" + string.Format("{0:N0}", p.payInWarehouse) + " vnđ</td>");
                                    html.Append("           </tr>");
                                }

                                html.Append("           <tr style=\"font-size: 15px; text-transform: uppercase\">");
                                html.Append("               <td colspan=\"4\" class=\"text-align-right\">Tổng tiền lưu kho</td>");
                                html.Append("               <td>" + string.Format("{0:N0}", o.totalPrice) + " vnđ</td>");
                                html.Append("           </tr>");
                                html.Append("           <tr style=\"font-size: 15px; text-transform: uppercase\">");
                                html.Append("               <td colspan=\"4\" class=\"text-align-right\">Trạng thái</td>");
                                html.Append("               <td style=\"color:#000\">" + status + "</td>");
                                html.Append("           </tr>");
                                html.Append("           <tr style=\"font-size: 18px; text-transform: uppercase\">");
                                html.Append("               <td colspan=\"4\" class=\"text-align-right\">Tiền cần thanh toán</td>");
                                html.Append("               <td>" + string.Format("{0:N0}", o.totalMustPay) + " vnđ</td>");
                                html.Append("           </tr>");
                                html.Append("       </table>");
                                html.Append("   </article>");
                                html.Append("</article>");
                                totalPriceMustPay += o.totalMustPay;

                                htmlPrint.Append("<article class=\"pane-primary\" style=\"color:#000\">");
                                if (orderType == 1)
                                {
                                    htmlPrint.Append("   <div class=\"heading\"><h3 class=\"lb\" style=\"color:#000\">Đơn hàng mua hộ: <span style=\"text-align:right\">#" + o.OrderID + "</span></h3></div>");
                                }
                                else
                                {
                                    htmlPrint.Append("   <div class=\"heading\"><h3 class=\"lb\" style=\"color:#000\">Đơn hàng VC hộ: <span style=\"text-align:right\">#" + o.OrderID + "</span></h3></div>");
                                }

                                htmlPrint.Append("   <article class=\"pane-primary\">");
                                htmlPrint.Append("       <table class=\"rgMasterTable normal-table full-width\" style=\"text-align:center\">");
                                htmlPrint.Append("           <tr>");
                                htmlPrint.Append("               <th style=\"color:#000\">Mã kiện</th>");
                                htmlPrint.Append("               <th style=\"color:#000\">Cân nặng (kg)</th>");
                                htmlPrint.Append("               <th style=\"color:#000\">Ngày lưu kho (ngày)</th>");
                                htmlPrint.Append("               <th style=\"color:#000\">Tiền lưu kho</th>");
                                htmlPrint.Append("           </tr>");

                                foreach (var p in listpackages)
                                {
                                    htmlPrint.Append("           <tr>");
                                    htmlPrint.Append("               <td>" + p.packagecode + "</td>");
                                    htmlPrint.Append("               <td>" + p.weight + "</td>");
                                    htmlPrint.Append("               <td>" + p.DateInWare + "</td>");
                                    htmlPrint.Append("               <td>" + string.Format("{0:N0}", p.payInWarehouse) + " vnđ</td>");
                                    htmlPrint.Append("           </tr>");
                                }

                                htmlPrint.Append("           <tr style=\"font-size: 15px; text-transform: uppercase\">");
                                htmlPrint.Append("               <td colspan=\"3\" class=\"text-align-right\">Tổng tiền lưu kho</td>");
                                htmlPrint.Append("               <td style=\"color:#000\">" + string.Format("{0:N0}", o.totalPrice) + " vnđ</td>");
                                htmlPrint.Append("           </tr>");
                                htmlPrint.Append("       </table>");
                                htmlPrint.Append("   </article>");
                                htmlPrint.Append("</article>");
                            }
                            ltrList.Text = html.ToString();
                            ViewState["content"] = htmlPrint.ToString();
                        }
                        #endregion
                        if (totalPriceMustPay > 0)
                        {
                            OutStockSessionController.updateTotalPay(id, totalPriceMustPay);
                        }
                    }
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            int id = ViewState["id"].ToString().ToInt(0);
            string address = "";
            if (id > 0)
            {
                var ots = OutStockSessionController.GetByID(id);
                if (ots != null)
                {
                    string username = ots.Username;
                    double wallet = 0;
                    var account = AccountController.GetByUsername(username);
                    if (account != null)
                    {
                        wallet = Convert.ToDouble(account.Wallet);
                        var ai = AccountInfoController.GetByUserID(account.ID);
                        if (ai != null)
                        {
                            address = ai.Address;
                        }
                    }
                }
                OutStockSessionController.update(id, txtFullname.Text, txtPhone.Text, 2, currentDate, username_current);
                var sessionpack = OutStockSessionPackageController.GetAllByOutStockSessionID(id);
                
                if (sessionpack.Count > 0)
                {
                    //foreach (var item in sessionpack)
                    //{
                    //    SmallPackageController.UpdateStatus(Convert.ToInt32(item.SmallPackageID), 4, currentDate, username_current);
                    //}

                    List<Main> mo = new List<Main>();
                    List<Trans> to = new List<Trans>();
                    foreach (var item in sessionpack)
                    {
                        SmallPackageController.UpdateStatus(Convert.ToInt32(item.SmallPackageID), 4, currentDate, username_current);
                        //SmallPackageController.UpdateDateOutWarehouse(Convert.ToInt32(item.SmallPackageID), username_current, currentDate);

                        if (item.MainOrderID > 0)
                        {
                            bool check = mo.Any(x => x.MainOrderID == Convert.ToInt32(item.MainOrderID));
                            if (check != true)
                            {
                                Main m = new Main();
                                m.MainOrderID = Convert.ToInt32(item.MainOrderID);
                                mo.Add(m);
                            }
                        }
                        else
                        {
                            bool check = to.Any(x => x.TransportationOrderID == Convert.ToInt32(item.TransportationID));
                            if (check != true)
                            {
                                Trans t = new Trans();
                                t.TransportationOrderID = Convert.ToInt32(item.TransportationID);
                                to.Add(t);
                            }
                        }
                    }
                    if (mo.Count > 0)
                    {
                        foreach (var item in mo)
                        {
                            var m = MainOrderController.GetAllByID(item.MainOrderID);
                            if (m != null)
                            {
                                bool checkIsChinaCome = true;
                                var packages = SmallPackageController.GetByMainOrderID(item.MainOrderID);
                                if (packages.Count > 0)
                                {
                                    foreach (var p in packages)
                                    {
                                        if (p.Status < 4)
                                            checkIsChinaCome = false;
                                    }
                                }
                                if (checkIsChinaCome == true)
                                {
                                    MainOrderController.UpdateStatus(item.MainOrderID, Convert.ToInt32(m.UID), 10);
                                    if (m.CompleteDate == null)
                                    {
                                        MainOrderController.UpdateCompleteDate(m.ID,Convert.ToInt32( m.UID), currentDate);
                                    }


                                    //int cusID = UID;
                                    //var cust = AccountController.GetByID(cusID);
                                    //if (cust != null)
                                    //{
                                    //    var cus_orders = MainOrderController.GetSuccessByCustomer(cust.ID);
                                    //    double totalpay = 0;
                                    //    if (cus_orders.Count > 0)
                                    //    {
                                    //        foreach (var od in cus_orders)
                                    //        {
                                    //            double ttpricenvd = 0;
                                    //            if (od.TotalPriceVND.ToFloat(0) > 0)
                                    //                ttpricenvd = Convert.ToDouble(od.TotalPriceVND);
                                    //            totalpay += ttpricenvd;
                                    //        }

                                    //        if (totalpay >= 100000000 && totalpay < 300000000)
                                    //        {
                                    //            if (cust.LevelID == 1)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 2, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //        else if (totalpay >= 300000000 && totalpay < 800000000)
                                    //        {
                                    //            if (cust.LevelID == 2)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 3, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //        else if (totalpay >= 800000000 && totalpay < 1500000000)
                                    //        {
                                    //            if (cust.LevelID == 3)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 4, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //        else if (totalpay >= 1500000000 && totalpay < 2500000000)
                                    //        {
                                    //            if (cust.LevelID == 4)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 5, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //        else if (totalpay >= 2500000000 && totalpay < 5000000000)
                                    //        {
                                    //            if (cust.LevelID == 5)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 11, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //        else if (totalpay >= 5000000000 && totalpay < 10000000000)
                                    //        {
                                    //            if (cust.LevelID == 11)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 12, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //        else if (totalpay >= 10000000000 && totalpay < 20000000000)
                                    //        {
                                    //            if (cust.LevelID == 12)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 13, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //        else if (totalpay >= 20000000000)
                                    //        {
                                    //            if (cust.LevelID == 13)
                                    //            {
                                    //                AccountController.updateLevelID(cusID, 14, currentDate, cust.Username);
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                }
                            }
                        }
                    }

                    if (to.Count > 0)
                    {
                        foreach (var item in to)
                        {
                            bool checkIsChinaCome = true;
                            var trans = SmallPackageController.GetByTransportationOrderID(item.TransportationOrderID);
                            if (trans.Count > 0)
                            {
                                foreach (var p in trans)
                                {
                                    if (p.Status < 4)
                                        checkIsChinaCome = false;
                                }
                            }
                            if (checkIsChinaCome == true)
                            {
                                TransportationOrderController.UpdateStatus(item.TransportationOrderID, 7, DateTime.UtcNow.AddHours(7), username_current);
                            }

                        }
                    }

                }
                string content = ViewState["content"].ToString();
                var html = "";
                html += "<div class=\"print-bill\">";
                html += "   <div class=\"top\">";
                html += "       <div class=\"left\">";
                html += "           <span class=\"company-info\"></span>";
                html += "           <span class=\"company-info\"><img src=\"/App_Themes/NamTrung/images/logo-header.png\" alt=\"\"/></span>";
                html += "       </div>";
                //html += "       <div class=\"right\">";
                //html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                //html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                //html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-title\">";
                html += "       <h1>PHIẾU XUẤT KHO</h1>";
                html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                html += "   </div>";
                html += "   <div class=\"bill-content\">";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Họ và tên người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Số ĐT người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Địa chỉ: </label>";
                html += "           <label class=\"row-info\">" + address + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += "           <label class=\"row-name\">Danh sách kiện: </label>";
                html += "           <label class=\"row-info\"></label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += content;
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-footer\">";
                html += "       <div style=\"float: left; width: 100%; margin-bottom: 50px;\">";
                html += "	        Khi nhận hàng quý khách lưu ý:<br />";
                html += "	        - Quý khách vui lòng kiểm tra lại cân nặng và kích thước, tình trạng kiện hàng cùng với nhân viên giao hàng.<br />";
                html += "	        - Quý khách vui lòng kiểm tra lại cân nặng và kích thước, tình trạng kiện hàng cùng với nhân viên giao hàng.<br />";
                html += "	        - Quý khách tạo khiếu nại trên hệ thống, vui lòng ghi rõ thông tin cần khiếu nại và gửi kèm hình ảnh: chụp bao bì kiện ";
                html += "	        hàng, hình ảnh Bill của chuyển phát shop dán trên kiện hàng, hình ảnh chụp cân nặng thực trạng của kiện hàng trước khi ";
                html += "	        mở bao bì, ảnh chụp sản phẩm/hàng hóa khiếu nại, thông tin mã đơn hàng, mã shop, mã link, danh sách liệt kê hàng hóa mua của nhà cung cấp gửi kèm.< br />";
                html += "	        - Khi cần hỗ trợ Quý khách vui lòng liên hệ theo hotline: 0962.111.688.<br />";
                html += "	        - Khi cần hỗ trợ giao hàng Quý khách vui lòng liên hệ hotline: Kho HN: 0345.96.1688; 0345.83.1688, Kho HCM: 0988.909.804";
                html += "	        <br />";
                html += "	        <br />";
                html += "	        <br />";
                html += "	        Khách hàng xác nhận tình trạng bao hàng hoặc kiện hàng khi nhận hàng:<br/>";
                html += "           Tôi xác nhận đã nhận ............... kiện hàng, tình trạng kiện hàng nguyên vẹn, không móc rách.<br/>";
                html += "	        <div style=\"width: 100%; height: 45px; border-bottom: dotted 1px #000;\"></div>";
                html += "	        <div style=\"width: 100%; height: 45px; border-bottom: dotted 1px #000;\"></div>";
                html += "       </div>";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người xuất hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "       </div>";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người nhận hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";

                html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
                html += "       </div>";
                html += "   </div>";
                html += "</div>";

                StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");

                sb.Append(@"VoucherPrint('" + html + "')");
                sb.Append(@"</script>");

                ///hàm để đăng ký javascript và thực thi đoạn script trên
                if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                }
            }
        }

        public class OrderPackage
        {
            public int OrderID { get; set; }
            public int OrderType { get; set; }
            public List<SmallpackageGet> smallpackages { get; set; }
            public double totalPrice { get; set; }
            public bool isPay { get; set; }
            public double totalMustPay { get; set; }
        }
        public class SmallpackageGet
        {
            public int ID { get; set; }
            public string packagecode { get; set; }
            public double weight { get; set; }
            public double DateInWare { get; set; }
            public int Status { get; set; }
            public double payInWarehouse { get; set; }

        }

        public class Main
        {
            public int MainOrderID { get; set; }
        }

        public class Trans
        {
            public int TransportationOrderID { get; set; }
        }


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            int id = ViewState["id"].ToString().ToInt(0);
            string address = "";
            if (id > 0)
            {
                var ots = OutStockSessionController.GetByID(id);
                if (ots != null)
                {
                    string username = ots.Username;
                    double wallet = 0;
                    var account = AccountController.GetByUsername(username);
                    if (account != null)
                    {
                        wallet = Convert.ToDouble(account.Wallet);
                        var ai = AccountInfoController.GetByUserID(account.ID);
                        if (ai != null)
                        {
                            address = ai.Address;
                        }
                    }
                }
                string content = ViewState["content"].ToString();
                var html = "";
                html += "<div class=\"print-bill\">";
                html += "   <div class=\"top\">";
                html += "       <div class=\"left\">";
                html += "           <span class=\"company-info\"></span>";
                html += "           <span class=\"company-info\"><img src=\"/App_Themes/NamTrung/images/logo-header.png\" alt=\"\"/></span>";
                html += "       </div>";
                //html += "       <div class=\"right\">";
                //html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                //html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                //html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-title\">";
                html += "       <h1>PHIẾU XUẤT KHO</h1>";
                html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                html += "   </div>";
                html += "   <div class=\"bill-content\">";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Họ và tên người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Số ĐT người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Địa chỉ: </label>";
                html += "           <label class=\"row-info\">" + address + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += "           <label class=\"row-name\">Danh sách kiện: </label>";
                html += "           <label class=\"row-info\"></label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += content;
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-footer\">";
                html += "       <div style=\"float: left; width: 100%; margin-bottom: 50px;\">";
                html += "	        Khi nhận hàng quý khách lưu ý:<br />";
                html += "	        - Quý khách vui lòng kiểm tra lại cân nặng và kích thước, tình trạng kiện hàng cùng với nhân viên giao hàng.<br />";
                html += "	        - Quý khách vui lòng kiểm tra lại cân nặng và kích thước, tình trạng kiện hàng cùng với nhân viên giao hàng.<br />";
                html += "	        - Quý khách tạo khiếu nại trên hệ thống, vui lòng ghi rõ thông tin cần khiếu nại và gửi kèm hình ảnh: chụp bao bì kiện ";
                html += "	        hàng, hình ảnh Bill của chuyển phát shop dán trên kiện hàng, hình ảnh chụp cân nặng thực trạng của kiện hàng trước khi ";
                html += "	        mở bao bì, ảnh chụp sản phẩm/hàng hóa khiếu nại, thông tin mã đơn hàng, mã shop, mã link, danh sách liệt kê hàng hóa mua của nhà cung cấp gửi kèm.< br />";
                html += "	        - Khi cần hỗ trợ Quý khách vui lòng liên hệ theo hotline: 0962.111.688.<br />";
                html += "	        - Khi cần hỗ trợ giao hàng Quý khách vui lòng liên hệ hotline: Kho HN: 0345.96.1688; 0345.83.1688, Kho HCM: 0988.909.804";
                html += "	        <br />";
                html += "	        <br />";
                html += "	        <br />";
                html += "	        Khách hàng xác nhận tình trạng bao hàng hoặc kiện hàng khi nhận hàng:<br/>";
                html += "           Tôi xác nhận đã nhận ............... kiện hàng, tình trạng kiện hàng nguyên vẹn, không móc rách.<br/>";
                html += "	        <div style=\"width: 100%; height: 45px; border-bottom: dotted 1px #000;\"></div>";
                html += "	        <div style=\"width: 100%; height: 45px; border-bottom: dotted 1px #000;\"></div>";
                html += "       </div>";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người xuất hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "       </div>";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người nhận hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
                html += "       </div>";
                html += "   </div>";
                html += "</div>";

                StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");

                sb.Append(@"VoucherPrint('" + html + "')");
                sb.Append(@"</script>");

                ///hàm để đăng ký javascript và thực thi đoạn script trên
                if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                }
            }
        }
    }
}