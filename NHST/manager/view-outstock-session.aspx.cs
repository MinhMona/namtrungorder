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
    public partial class view_outstock_session : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 7 && ac.RoleID != 2)
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
                    ltrIDS.Text = "#" + id;
                    var os = OutStockSessionController.GetByID(id);
                    if (os != null)
                    {
                        bool isShowButton = true;
                        double totalPriceMustPay = 0;
                        double Wallet = 0;
                        int UID = Convert.ToInt32(os.UID);
                        var acc = AccountController.GetByID(UID);
                        if (acc != null)
                        {
                            Wallet = Convert.ToDouble(acc.Wallet);
                        }
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
                                                double weight = 0;
                                                double weightthucte = Convert.ToDouble(sm.Weight);



                                                double canQD = (Convert.ToDouble(sm.Length) * Convert.ToDouble(sm.Height) * Convert.ToDouble(sm.Width) / 6000);

                                                if (weightthucte >= canQD)
                                                {
                                                    weight = weightthucte;
                                                }
                                                else
                                                {
                                                    weight = canQD;
                                                }
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
                        string listMainorder = "";
                        string listtransportationorder = "";
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
                                    {
                                        html.Append("   <div class=\"heading\"><h3 class=\"lb\" style=\"color:#000!important\">Đơn hàng mua hộ: #" + o.OrderID + "</h3></div>");

                                    }
                                    else
                                    {
                                        html.Append("   <div class=\"heading\" style=\"background:red!important\"><h3 class=\"lb\" style=\"color:#000!important\">Đơn hàng mua hộ: #" + o.OrderID + "</h3></div>");
                                        listMainorder += o.OrderID + "|";
                                    }
                                }
                                else
                                {
                                    if (isPay == true)
                                        html.Append("   <div class=\"heading\"><h3 class=\"lb\" style=\"color:#000!important\">Đơn hàng VC hộ: #" + o.OrderID + "</h3></div>");
                                    else
                                    {
                                        html.Append("   <div class=\"heading\" style=\"background:red!important\"><h3 class=\"lb\" style=\"color:#000!important\">Đơn hàng VC hộ: #" + o.OrderID + "</h3></div>");
                                        listtransportationorder += o.OrderID + "|";
                                    }
                                }

                                html.Append("   <article class=\"pane-primary\">");
                                html.Append("       <table class=\"normal-table full-width\">");
                                html.Append("           <tr>");
                                html.Append("               <th style=\"color:#000!important\">Mã kiện</th>");
                                html.Append("               <th style=\"color:#000!important\">Cân nặng (kg)</th>");
                                html.Append("               <th style=\"color:#000!important\">Ngày lưu kho (ngày)</th>");
                                html.Append("               <th style=\"color:#000!important\">Trạng thái</th>");
                                html.Append("               <th style=\"color:#000!important\">Tiền lưu kho</th>");
                                html.Append("           </tr>");
                                var listpackages = o.smallpackages;
                                foreach (var p in listpackages)
                                {
                                    html.Append("           <tr>");
                                    html.Append("               <td>" + p.packagecode + "</td>");
                                    html.Append("               <td>" + Math.Round(p.weight,2) + "</td>");
                                    html.Append("               <td>" + p.DateInWare + "</td>");
                                    html.Append("               <td>" + PJUtils.IntToStringStatusSmallPackage(p.Status) + "</td>");
                                    html.Append("               <td>" + string.Format("{0:N0}", p.payInWarehouse) + " vnđ</td>");
                                    html.Append("           </tr>");
                                }

                                html.Append("           <tr style=\"font-size: 15px; text-transform: uppercase\">");
                                html.Append("               <td colspan=\"4\" class=\"text-align-right\">Tổng tiền lưu kho</td>");
                                html.Append("               <td>" + string.Format("{0:N0}", o.totalPrice) + " vnđ</td>");
                                html.Append("           </tr>");
                                html.Append("           <tr style=\"font-size: 15px; text-transform: uppercase\">");
                                html.Append("               <td colspan=\"4\" class=\"text-align-right\">Trạng thái</td>");
                                html.Append("               <td style=\"color:#000!important\">" + status + "</td>");
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
                                htmlPrint.Append("       <table class=\"rgMasterTable normal-table full-width\" style=\"text-align:center;color:#000;\">");
                                htmlPrint.Append("           <tr>");
                                htmlPrint.Append("               <th style=\"color:#000!important\">Mã kiện</th>");
                                htmlPrint.Append("               <th style=\"color:#000!important\">Cân nặng (kg)</th>");
                                htmlPrint.Append("               <th style=\"color:#000!important\">Ngày lưu kho (ngày)</th>");
                                htmlPrint.Append("               <th style=\"color:#000!important\">Tiền lưu kho</th>");
                                htmlPrint.Append("           </tr>");

                                foreach (var p in listpackages)
                                {
                                    htmlPrint.Append("           <tr>");
                                    htmlPrint.Append("               <td>" + p.packagecode + "</td>");
                                    htmlPrint.Append("               <td>" + Math.Round(p.weight,2) + "</td>");
                                    htmlPrint.Append("               <td>" + p.DateInWare + "</td>");
                                    htmlPrint.Append("               <td style=\"color:#000!important\">" + string.Format("{0:N0}", p.payInWarehouse) + " vnđ</td>");
                                    htmlPrint.Append("           </tr>");
                                }

                                htmlPrint.Append("           <tr style=\"font-size: 15px; text-transform: uppercase\">");
                                htmlPrint.Append("               <td colspan=\"3\" class=\"text-align-right\">Tổng tiền lưu kho</td>");
                                htmlPrint.Append("               <td>" + string.Format("{0:N0}", o.totalPrice) + " vnđ</td>");
                                htmlPrint.Append("           </tr>");
                                htmlPrint.Append("       </table>");
                                htmlPrint.Append("   </article>");
                                htmlPrint.Append("</article>");
                            }
                            if (totalPriceMustPay > 0)
                            {
                                OutStockSessionController.updateTotalPay(id, totalPriceMustPay);
                            }

                            txtTotalPrice.Text = string.Format("{0:N0}", totalPriceMustPay);
                            ltrList.Text = html.ToString();
                            if (totalPriceMustPay > 0)
                            {
                                btncreateuser.Visible = false;
                            }
                            else
                            {
                                btncreateuser.Visible = false;
                            }
                            ViewState["totalPricePay"] = totalPriceMustPay;
                            ViewState["listmID"] = listMainorder;
                            ViewState["listtrans"] = listtransportationorder;
                            //ViewState["content"] = htmlPrint.ToString();
                            ViewState["content"] = html.ToString();
                        }
                        #endregion
                        if (totalPriceMustPay > 0)
                        {
                            if (Wallet >= totalPriceMustPay)
                            {
                                btnPayall.Visible = true;
                            }
                            else
                            {
                                btnPayall.Visible = false;
                            }
                        }
                        else
                        {
                            btnPayall.Visible = false;
                        }
                    }
                }
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = "";
            string username_current = Session["userLoginSystem"].ToString();
            string address = "";
            int UID_Admin = 0;
            var userAdmin = AccountController.GetByUsername(username_current);
            if (userAdmin != null)
            {
                UID_Admin = userAdmin.ID;
            }
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                var ots = OutStockSessionController.GetByID(id);
                if (ots != null)
                {
                    username = ots.Username;
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
                    double totalweight = 0;
                    double canQD = 0;
                    double canTT = 0;
                    var otp = OutStockSessionPackageController.GetAllByOutStockSessionID(ots.ID);
                    if (otp.Count > 0)
                    {
                        foreach (var item in otp)
                        {
                            var sm = SmallPackageController.GetByID(item.SmallPackageID.Value);
                            if (sm != null)
                            {
                                if (Convert.ToDouble(sm.Weight) > (Convert.ToDouble(sm.Height) * Convert.ToDouble(sm.Width) * Convert.ToDouble(sm.Length) / 6000))
                                {
                                    canTT += Convert.ToDouble(sm.Weight);
                                }
                                else
                                {
                                    canTT += (Convert.ToDouble(sm.Height) * Convert.ToDouble(sm.Width) * Convert.ToDouble(sm.Length) / 6000);
                                }

                                //totalweight += Convert.ToDouble(sm.Weight);
                                //canQD += (Convert.ToDouble(sm.Height) * Convert.ToDouble(sm.Width) * Convert.ToDouble(sm.Length) / 6000);

                            }
                        }
                    }
                    int totalpackage = otp.Count;
                    wallet = Math.Round(wallet, 0);
                    int UID = Convert.ToInt32(ots.UID);
                    string mIDsString = ViewState["listmID"].ToString();
                    string lIDs = ViewState["listtrans"].ToString();
                    double totalPay = Convert.ToDouble(ViewState["totalPricePay"]);

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
                    html += "<div style=\"display:flex;\">";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Khách hàng: </label>";
                    html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                    html += "       </div>";
                    html += "       </div>";
                    html += "<div style=\"display:flex;\">";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Username: </label>";
                    html += "           <label class=\"row-info\">" + username + "</label>";
                    html += "       </div>";
                    html += "       </div>";
                    html += "<div style=\"display:flex;\">";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Điện thoại: </label>";
                    html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                    html += "       </div>";
                    html += "       </div>";

                    html += "<div style=\"display:flex;\">";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Địa chỉ: </label>";
                    html += "           <label class=\"row-info\">" + address + "</label>";
                    html += "       </div>";
                    html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Tài khoản nhận tiền: </label>";
                    //html += "           <label class=\"row-info\">" + username + "</label>";
                    //html += "       </div>";
                    html += "<div style=\"display:flex;\">";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\" style=\"width:30%\">a. Tổng số tiền cần thanh toán: </label>";
                    html += "           <label class=\"row-info\">" + string.Format("{0:N0}", totalPay) + " VNĐ</label>";
                    html += "       </div>";
                    html += "       </div>";

                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\" style=\"width:30%\">b. Số dư hiện tại: </label>";
                    //html += "           <label class=\"row-info\">" + string.Format("{0:N0}", wallet) + " VNĐ</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\" style=\"width:30%\">Số dư sau khi xuất kho: </label>";
                    //html += "           <label class=\"row-info\">" + string.Format("{0:N0}", wallet - totalPay) + " VNĐ</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\" style=\"width:30%\">Tổng số kiện: </label>";
                    //html += "           <label class=\"row-info\">" + totalpackage + " kiện</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\" style=\"width:30%\">Tổng cân nặng: </label>";
                    //html += "           <label class=\"row-info\">" + string.Format("{0:N0}", totalweight) + " Kg</label>";
                    //html += "       </div>";



                    html += "<div style=\"display:flex;\">";
                    html += "       <div class=\"bill-row\" style=\"width:50%\">";
                    html += "           <label class=\"row-name\" style=\"width:30%\">b. Số dư hiện tại: </label>";
                    html += "           <label class=\"row-info\">" + string.Format("{0:N0}", wallet) + " VNĐ</label>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row\" style=\"width:50%;margin-left: 5px;\">";
                    html += "           <label class=\"row-name\" style=\"width:30%\">Tổng số kiện: </label>";
                    html += "           <label class=\"row-info\">" + totalpackage + " kiện</label>";
                    html += "       </div>";
                    html += "</div>";

                    html += "<div style=\"display:flex;\">";
                    html += "       <div class=\"bill-row\" style=\"width:50%\">";
                    html += "           <label class=\"row-name\" style=\"width:30%\">Số dư sau XK: </label>";
                    html += "           <label class=\"row-info\">" + string.Format("{0:N0}", wallet - totalPay) + " VNĐ</label>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row\" style=\"width:50%;margin-left: 5px;\">";
                    html += "           <label class=\"row-name\" style=\"width:30%\">Tổng cân nặng: </label>";
                    html += "           <label class=\"row-info\">" + Math.Round(canTT, 2) + " Kg</label>";
                    html += "       </div>";
                    html += "</div>";


                    html += "       <div class=\"bill-row\" style=\"border:none\">";
                    html += "           <label class=\"row-name\">Danh sách kiện: </label>";
                    html += "           <label class=\"row-info\"></label>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row\" style=\"border:none;color:#000\">";
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
                    html += "           <strong>KHÁCH HÀNG</strong>";
                    html += "           <span class=\"note\">(Ký, họ tên)</span>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row-two\">";
                    html += "           <strong>NHÂN VIÊN XUẤT KHO</strong>";
                    html += "           <span class=\"note\">(Ký, họ tên)</span>";
                    //html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
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
                    //PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                }
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = "";
            string username_current = Session["userLoginSystem"].ToString();
            int UID_Admin = 0;
            var userAdmin = AccountController.GetByUsername(username_current);
            if (userAdmin != null)
            {
                UID_Admin = userAdmin.ID;
            }
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                var ots = OutStockSessionController.GetByID(id);
                if (ots != null)
                {
                    username = ots.Username;
                    int UID = Convert.ToInt32(ots.UID);
                    string mIDsString = ViewState["listmID"].ToString();
                    string lIDs = ViewState["listtrans"].ToString();
                    double totalPay = 0;
                    if (ViewState["totalPricePay"] != null)
                    {
                        totalPay = Convert.ToDouble(ViewState["totalPricePay"].ToString());
                    }
                    if (totalPay > 0)
                    {
                        var user_wallet = AccountController.GetByID(UID);
                        if (user_wallet != null)
                        {
                            double wallet = Convert.ToDouble(user_wallet.Wallet);
                            wallet = wallet + totalPay;
                            string contentin = user_wallet.Username + " đã được nạp tiền vào tài khoản.";
                            //AdminSendUserWalletController.UpdateStatus(id, 2, contentin, currentDate, username_current);
                            AdminSendUserWalletController.Insert(user_wallet.ID, user_wallet.Username, totalPay, 2, contentin, currentDate, username_current);
                            AccountController.updateWallet(user_wallet.ID, wallet, currentDate, username_current);
                            HistoryPayWalletController.Insert(user_wallet.ID, user_wallet.Username, 0, totalPay, user_wallet.Username + " đã được nạp tiền vào tài khoản.", wallet, 2, 4, currentDate, username_current);
                        }
                        if (!string.IsNullOrEmpty(mIDsString))
                        {
                            string[] mIDs = mIDsString.Split('|');
                            if (mIDs.Length - 1 > 0)
                            {
                                for (int i = 0; i < mIDs.Length - 1; i++)
                                {
                                    int mID = mIDs[i].ToInt(0);
                                    var o = MainOrderController.GetAllByUIDAndID(UID, mID);
                                    if (o != null)
                                    {
                                        var obj_user = AccountController.GetByID(UID);
                                        if (obj_user != null)
                                        {
                                            double deposited = 0;
                                            if (o.Deposit != null)
                                                deposited = Convert.ToDouble(o.Deposit);
                                            double totalPrice = Convert.ToDouble(o.TotalPriceVND);
                                            double totalPriceInwarehouse = 0;
                                            if (o.FeeInWareHouse != null)
                                                totalPriceInwarehouse = Convert.ToDouble(o.FeeInWareHouse);
                                            double finalPrice = totalPrice + totalPriceInwarehouse;
                                            double leftpay = finalPrice - deposited;
                                            //MainOrderController.UpdateDeposit(m.ID, Convert.ToInt32(m.UID), totalPrice.ToString());
                                            double wallet = obj_user.Wallet.ToString().ToFloat(0);

                                            if (wallet >= leftpay)
                                            {
                                                double walletLeft = wallet - leftpay;
                                                MainOrderController.UpdateStatus(o.ID, UID, 9);
                                                AccountController.updateWallet(UID, walletLeft, currentDate, username_current);

                                                HistoryOrderChangeController.Insert(o.ID, UID_Admin, username_current, username_current +
                                                            " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Đã về kho đích, sang: Khách đã thanh toán.", 1, currentDate);

                                                HistoryPayWalletController.Insert(UID, obj_user.Username, o.ID, leftpay, obj_user.Username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username_current);
                                                string kq = MainOrderController.UpdateDeposit(o.ID, UID, finalPrice.ToString());
                                                PayOrderHistoryController.Insert(id, UID, 9, leftpay, 2, currentDate, username_current);
                                                //using (NHSTEntities productDbContext = new NHSTEntities())
                                                //{
                                                //    using (var transaction = productDbContext.Database.BeginTransaction())
                                                //    {
                                                //        try
                                                //        {

                                                //            double walletLeft = wallet - leftpay;
                                                //            MainOrderController.UpdateStatus(o.ID, UID, 9);
                                                //            AccountController.updateWallet(UID, walletLeft, currentDate, username_current);

                                                //            HistoryOrderChangeController.Insert(o.ID, UID_Admin, username_current, username_current +
                                                //                        " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Đã về kho đích, sang: Khách đã thanh toán.", 1, currentDate);

                                                //            HistoryPayWalletController.Insert(UID, obj_user.Username, o.ID, leftpay, obj_user.Username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username_current);
                                                //            MainOrderController.UpdateDeposit(id, UID, finalPrice.ToString());
                                                //            MainOrderController.UpdateStatus(id, UID, 9);
                                                //            PayOrderHistoryController.Insert(id, UID, 9, leftpay, 2, currentDate, username_current);
                                                //            transaction.Commit();
                                                //        }
                                                //        catch (Exception exception)
                                                //        {
                                                //            transaction.Rollback();
                                                //            //PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                                //        }
                                                //    }
                                                //}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(lIDs))
                        {
                            string[] tsString = lIDs.Split('|');
                            if (tsString.Length - 1 > 0)
                            {
                                for (int i = 0; i < tsString.Length - 1; i++)
                                {
                                    int tID = tsString[i].ToInt(0);
                                    if (tID > 0)
                                    {
                                        var t = TransportationOrderController.GetByIDAndUID(tID, UID);
                                        if (t != null)
                                        {
                                            double currency = Convert.ToDouble(t.Currency);
                                            double totalWeight = 0;

                                            int wareFrom = Convert.ToInt32(t.WarehouseFromID);
                                            int wareTo = Convert.ToInt32(t.WarehouseID);
                                            int shippingType = Convert.ToInt32(t.ShippingTypeID);
                                            double price = 0;
                                            var packages = SmallPackageController.GetByTransportationOrderID(t.ID);
                                            if (packages.Count > 0)
                                            {
                                                foreach (var p in packages)
                                                {
                                                    totalWeight += Convert.ToDouble(p.Weight);
                                                }
                                            }
                                            var WarehouseFee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(wareFrom, wareTo, shippingType, true);
                                            if (WarehouseFee.Count > 0)
                                            {
                                                foreach (var item in WarehouseFee)
                                                {
                                                    if (item.WeightFrom < totalWeight && totalWeight <= item.WeightTo)
                                                    {
                                                        price = Convert.ToDouble(item.Price);
                                                    }
                                                }
                                            }
                                            double warehouseFee = 0;
                                            if (t.WarehouseFee != null)
                                            {
                                                warehouseFee = Convert.ToDouble(t.WarehouseFee);
                                            }
                                            double deposited = Convert.ToDouble(t.Deposited);
                                            double totalPrice = price * totalWeight * currency + warehouseFee;
                                            double leftMoney = totalPrice - deposited;
                                            var acc_user = AccountController.GetByID(Convert.ToInt32(t.UID));
                                            if (acc_user != null)
                                            {
                                                double wallet = Convert.ToDouble(acc_user.Wallet);
                                                if (leftMoney <= wallet)
                                                {
                                                    double walletLeft = wallet - leftMoney;
                                                    TransportationOrderController.UpdateStatusAndDeposited(t.ID, totalPrice, 6, currentDate, username_current);
                                                    AccountController.updateWallet(UID, walletLeft, currentDate, username_current);
                                                    HistoryPayWalletController.InsertTransportation(UID, username_current, 0, leftMoney,
                                                        username_current + " đã thanh toán đơn hàng vận chuyển hộ: " + t.ID + ".",
                                                        walletLeft, 1, 8, currentDate, username_current, t.ID);
                                                    //using (NHSTEntities productDbContext = new NHSTEntities())
                                                    //{
                                                    //    using (var transaction = productDbContext.Database.BeginTransaction())
                                                    //    {
                                                    //        try
                                                    //        {
                                                    //            TransportationOrderController.UpdateStatusAndDeposited(t.ID, totalPrice, 6, currentDate, username_current);
                                                    //            AccountController.updateWallet(UID, walletLeft, currentDate, username_current);
                                                    //            HistoryPayWalletController.InsertTransportation(UID, username_current, 0, leftMoney,
                                                    //                username_current + " đã thanh toán đơn hàng vận chuyển hộ: " + t.ID + ".",
                                                    //                walletLeft, 1, 8, currentDate, username_current, t.ID);
                                                    //            transaction.Commit();
                                                    //        }
                                                    //        catch (Exception ex)
                                                    //        {
                                                    //            transaction.Rollback();
                                                    //        }
                                                    //    }
                                                    //}
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        var sessionpack = OutStockSessionPackageController.GetAllByOutStockSessionID(id);
                        if (sessionpack.Count > 0)
                        {
                            List<Main> mo = new List<Main>();
                            List<Trans> to = new List<Trans>();
                            foreach (var item in sessionpack)
                            {
                                SmallPackageController.UpdateStatus(Convert.ToInt32(item.SmallPackageID), 4, currentDate, username_current);
                               // SmallPackageController.UpdateDateOutWarehouse(Convert.ToInt32(item.SmallPackageID), username_current, currentDate);

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

                    }

                    AccountantOutStockPaymentController.Insert(ots.ID, totalPay, Convert.ToInt32(ots.UID), ots.Username, "Thanh toán bằng tiền mặt", currentDate, username_current);
                    OutStockSessionController.updateInfo(id, txtFullname.Text, txtPhone.Text);
                    //OutStockSessionController.updateStatus(id, 2, currentDate, username_current);
                    //string content = ViewState["content"].ToString();
                    //var html = "";
                    //html += "<div class=\"print-bill\">";
                    //html += "   <div class=\"top\">";
                    //html += "       <div class=\"left\">";
                    //html += "           <span class=\"company-info\">Nhập Hàng TQ</span>";
                    //html += "           <span class=\"company-info\">Địa chỉ: Ngõ 95 Hoàng Cầu, Đống Đa, Hà Nội.</span>";
                    //html += "       </div>";
                    //html += "       <div class=\"right\">";
                    //html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                    //html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                    //html += "       </div>";
                    //html += "   </div>";
                    //html += "   <div class=\"bill-title\">";
                    //html += "       <h1>BIÊN NHẬN</h1>";
                    //html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                    //html += "   </div>";
                    //html += "   <div class=\"bill-content\">";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Họ và tên người đóng tiền: </label>";
                    //html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Số ĐT người đóng tiền: </label>";
                    //html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Tài khoản nhận tiền: </label>";
                    //html += "           <label class=\"row-info\">" + username + "</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Số tiền: </label>";
                    //html += "           <label class=\"row-info\">" + string.Format("{0:N0}", totalPay) + " VNĐ</label>";
                    //html += "       </div>";
                    //html += "   </div>";
                    //html += "   <div class=\"bill-footer\">";
                    //html += "       <div class=\"bill-row-two\">";
                    //html += "           <strong>Người thu tiền</strong>";
                    //html += "           <span class=\"note\">(Ký, họ tên)</span>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row-two\">";
                    //html += "           <strong>Người đóng tiền</strong>";
                    //html += "           <span class=\"note\">(Ký, họ tên)</span>";

                    //html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
                    //html += "       </div>";
                    //html += "   </div>";
                    //html += "</div>";

                    //StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append(@"<script language='javascript'>");

                    //sb.Append(@"VoucherPrint('" + html + "')");
                    //sb.Append(@"</script>");

                    /////hàm để đăng ký javascript và thực thi đoạn script trên
                    //if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                    //}
                    PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                }
            }
        }
        protected void btnPayall_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = "";
            string username_current = Session["userLoginSystem"].ToString();
            int UID_Admin = 0;
            var userAdmin = AccountController.GetByUsername(username_current);
            if (userAdmin != null)
            {
                UID_Admin = userAdmin.ID;
            }
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                var ots = OutStockSessionController.GetByID(id);
                if (ots != null)
                {
                    username = ots.Username;
                    int UID = Convert.ToInt32(ots.UID);
                    string mIDsString = ViewState["listmID"].ToString();
                    string lIDs = ViewState["listtrans"].ToString();
                    double totalPay = 0;
                    if (ViewState["totalPricePay"] != null)
                    {
                        totalPay = Convert.ToDouble(ViewState["totalPricePay"].ToString());
                    }
                    if (totalPay > 0)
                    {
                        var user_wallet = AccountController.GetByID(UID);
                        if (!string.IsNullOrEmpty(mIDsString))
                        {
                            string[] mIDs = mIDsString.Split('|');
                            if (mIDs.Length - 1 > 0)
                            {
                                for (int i = 0; i < mIDs.Length - 1; i++)
                                {
                                    int mID = mIDs[i].ToInt(0);
                                    var o = MainOrderController.GetAllByUIDAndID(UID, mID);
                                    if (o != null)
                                    {
                                        var obj_user = AccountController.GetByID(UID);
                                        if (obj_user != null)
                                        {
                                            double deposited = 0;
                                            if (o.Deposit.ToFloat(0) > 0)
                                                deposited = Convert.ToDouble(o.Deposit);
                                            double totalPrice = Convert.ToDouble(o.TotalPriceVND);
                                            double totalPriceInwarehouse = 0;
                                            if (o.FeeInWareHouse > 0)
                                                totalPriceInwarehouse = Convert.ToDouble(o.FeeInWareHouse);
                                            double finalPrice = totalPrice + totalPriceInwarehouse;
                                            double leftpay = finalPrice - deposited;
                                            //MainOrderController.UpdateDeposit(m.ID, Convert.ToInt32(m.UID), totalPrice.ToString());

                                            double wallet = 0;
                                            if (obj_user.Wallet.ToString().ToFloat(0) > 0)
                                                wallet = Convert.ToDouble(obj_user.Wallet);

                                            if (wallet >= leftpay)
                                            {
                                                double walletLeft = wallet - leftpay;
                                                MainOrderController.UpdateStatus(o.ID, UID, 9);
                                                AccountController.updateWallet(UID, walletLeft, currentDate, username_current);

                                                HistoryOrderChangeController.Insert(o.ID, UID_Admin, username_current, username_current +
                                                            " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Đã về kho đích, sang: Khách đã thanh toán.", 1, currentDate);

                                                HistoryPayWalletController.Insert(UID, obj_user.Username, o.ID, leftpay, obj_user.Username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username_current);
                                                string kq = MainOrderController.UpdateDeposit(o.ID, UID, finalPrice.ToString());
                                                PayOrderHistoryController.Insert(id, UID, 9, leftpay, 2, currentDate, username_current);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(lIDs))
                        {
                            string[] tsString = lIDs.Split('|');
                            if (tsString.Length - 1 > 0)
                            {
                                for (int i = 0; i < tsString.Length - 1; i++)
                                {
                                    int tID = tsString[i].ToInt(0);
                                    if (tID > 0)
                                    {
                                        var t = TransportationOrderController.GetByIDAndUID(tID, UID);
                                        if (t != null)
                                        {
                                            double currency = Convert.ToDouble(t.Currency);
                                            double totalWeight = 0;

                                            int wareFrom = Convert.ToInt32(t.WarehouseFromID);
                                            int wareTo = Convert.ToInt32(t.WarehouseID);
                                            int shippingType = Convert.ToInt32(t.ShippingTypeID);
                                            double price = 0;
                                            var packages = SmallPackageController.GetByTransportationOrderID(t.ID);
                                            if (packages.Count > 0)
                                            {
                                                foreach (var p in packages)
                                                {
                                                    double weight = 0;
                                                    double weightCN = Convert.ToDouble(p.Weight);
                                                    double weightKT = 0;
                                                    double dai = 0;
                                                    double rong = 0;
                                                    double cao = 0;
                                                    if (p.Length != null)
                                                        dai = Convert.ToDouble(p.Length);
                                                    if (p.Width != null)
                                                        rong = Convert.ToDouble(p.Width);
                                                    if (p.Height != null)
                                                        cao = Convert.ToDouble(p.Height);

                                                    if (dai > 0 && rong > 0 && cao > 0)
                                                        weightKT = dai * rong * cao / 6000;
                                                    if (weightKT > 0)
                                                    {
                                                        if (weightKT > weightCN)
                                                        {
                                                            weight = weightKT;
                                                        }
                                                        else
                                                        {
                                                            weight = weightCN;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        weight = weightCN;
                                                    }
                                                    weight = Math.Round(weight, 1);
                                                    totalWeight += weight;
                                                    //totalWeight += Convert.ToDouble(p.Weight);
                                                }
                                            }
                                            var WarehouseFee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(wareFrom, wareTo, shippingType, true);
                                            if (WarehouseFee.Count > 0)
                                            {
                                                foreach (var item in WarehouseFee)
                                                {
                                                    if (item.WeightFrom < totalWeight && totalWeight <= item.WeightTo)
                                                    {
                                                        price = Convert.ToDouble(item.Price);
                                                    }
                                                }
                                            }
                                            double warehouseFee = 0;
                                            if (t.WarehouseFee != null)
                                            {
                                                warehouseFee = Convert.ToDouble(t.WarehouseFee);
                                            }
                                            double deposited = Convert.ToDouble(t.Deposited);
                                            //double totalPrice = price * totalWeight * currency + warehouseFee;
                                            //double totalPrice = price * totalWeight + warehouseFee;
                                            double totalPriceO = Convert.ToDouble(t.TotalPrice);
                                            double totalPrice = totalPriceO + warehouseFee;
                                            double leftMoney = totalPrice - deposited;
                                            var acc_user = AccountController.GetByID(Convert.ToInt32(t.UID));
                                            if (acc_user != null)
                                            {
                                                double wallet = Convert.ToDouble(acc_user.Wallet);
                                                if (leftMoney <= wallet)
                                                {
                                                    double walletLeft = wallet - leftMoney;
                                                    TransportationOrderController.UpdateStatusAndDeposited(t.ID, totalPrice, 6, currentDate, username_current);
                                                    AccountController.updateWallet(UID, walletLeft, currentDate, username_current);
                                                    HistoryPayWalletController.InsertTransportation(UID, username_current, 0, leftMoney,
                                                        username_current + " đã thanh toán đơn hàng vận chuyển hộ: " + t.ID + ".",
                                                        walletLeft, 1, 8, currentDate, username_current, t.ID);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        var sessionpack = OutStockSessionPackageController.GetAllByOutStockSessionID(id);
                        if (sessionpack.Count > 0)
                        {
                            List<Main> mo = new List<Main>();
                            List<Trans> to = new List<Trans>();
                            foreach (var item in sessionpack)
                            {
                                SmallPackageController.UpdateStatus(Convert.ToInt32(item.SmallPackageID), 4, currentDate, username_current);
                              //  SmallPackageController.UpdateDateOutWarehouse(Convert.ToInt32(item.SmallPackageID), username_current, currentDate);

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
                                                MainOrderController.UpdateCompleteDate(m.ID,Convert.ToInt32(m.UID), currentDate);
                                            }
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

                    }

                    AccountantOutStockPaymentController.Insert(ots.ID, totalPay, Convert.ToInt32(ots.UID), ots.Username, "Thanh toán bằng ví điện tử", currentDate, username_current);
                    OutStockSessionController.updateInfo(id, txtFullname.Text, txtPhone.Text);
                    //string content = ViewState["content"].ToString();
                    //var html = "";
                    //html += "<div class=\"print-bill\">";
                    //html += "   <div class=\"top\">";
                    //html += "       <div class=\"left\">";
                    //html += "           <span class=\"company-info\">Nam Trung</span>";
                    //html += "           <span class=\"company-info\">Địa chỉ: Ngõ 95 Hoàng Cầu, Đống Đa, Hà Nội.</span>";
                    //html += "       </div>";
                    //html += "       <div class=\"right\">";
                    //html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                    //html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                    //html += "       </div>";
                    //html += "   </div>";
                    //html += "   <div class=\"bill-title\">";
                    //html += "       <h1>BIÊN NHẬN</h1>";
                    //html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                    //html += "   </div>";
                    //html += "   <div class=\"bill-content\">";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Họ và tên người đóng tiền: </label>";
                    //html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Số ĐT người đóng tiền: </label>";
                    //html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Tài khoản nhận tiền: </label>";
                    //html += "           <label class=\"row-info\">" + username + "</label>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row\">";
                    //html += "           <label class=\"row-name\">Số tiền: </label>";
                    //html += "           <label class=\"row-info\">" + string.Format("{0:N0}", totalPay) + " VNĐ</label>";
                    //html += "       </div>";
                    //html += "   </div>";
                    //html += "   <div class=\"bill-footer\">";
                    //html += "       <div class=\"bill-row-two\">";
                    //html += "           <strong>Người thu tiền</strong>";
                    //html += "           <span class=\"note\">(Ký, họ tên)</span>";
                    //html += "       </div>";
                    //html += "       <div class=\"bill-row-two\">";
                    //html += "           <strong>Người đóng tiền</strong>";
                    //html += "           <span class=\"note\">(Ký, họ tên)</span>";
                    //html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
                    //html += "       </div>";
                    //html += "   </div>";
                    //html += "</div>";

                    //StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append(@"<script language='javascript'>");

                    //sb.Append(@"VoucherPrint('" + html + "')");
                    //sb.Append(@"</script>");

                    /////hàm để đăng ký javascript và thực thi đoạn script trên
                    //if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                    //}
                    PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
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

        protected void btnExportWarehouse_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                OutStockSessionController.update(id, txtFullname.Text, txtPhone.Text, 2, currentDate, username_current);
                var sessionpack = OutStockSessionPackageController.GetAllByOutStockSessionID(id);
                if (sessionpack.Count > 0)
                {
                    foreach (var item in sessionpack)
                    {
                        SmallPackageController.UpdateStatus(Convert.ToInt32(item.SmallPackageID), 4, currentDate, username_current);
                    }
                }
                //string content = ViewState["content"].ToString();
                //var html = "";
                //html += "<div class=\"print-bill\">";
                //html += "   <div class=\"top\">";
                //html += "       <div class=\"left\">";
                //html += "           <span class=\"company-info\">Nhập Hàng TQ</span>";
                //html += "           <span class=\"company-info\">Địa chỉ: Ngõ 95 Hoàng Cầu, Đống Đa, Hà Nội.</span>";
                //html += "       </div>";
                //html += "       <div class=\"right\">";
                //html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                //html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                //html += "       </div>";
                //html += "   </div>";
                //html += "   <div class=\"bill-title\">";
                //html += "       <h1>PHIẾU XUẤT KHO</h1>";
                //html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                //html += "   </div>";
                //html += "   <div class=\"bill-content\">";
                //html += "       <div class=\"bill-row\">";
                //html += "           <label class=\"row-name\">Họ và tên người đến nhận: </label>";
                //html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                //html += "       </div>";
                //html += "       <div class=\"bill-row\">";
                //html += "           <label class=\"row-name\">Số ĐT người đến nhận: </label>";
                //html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                //html += "       </div>";
                //html += "       <div class=\"bill-row\" style=\"border:none\">";
                //html += "           <label class=\"row-name\">Danh sách kiện: </label>";
                //html += "           <label class=\"row-info\"></label>";
                //html += "       </div>";
                //html += "       <div class=\"bill-row\" style=\"border:none\">";
                //html += content;
                //html += "       </div>";
                //html += "   </div>";
                //html += "   <div class=\"bill-footer\">";
                //html += "       <div class=\"bill-row-two\">";
                //html += "           <strong>Người xuất hàng</strong>";
                //html += "           <span class=\"note\">(Ký, họ tên)</span>";
                //html += "       </div>";
                //html += "       <div class=\"bill-row-two\">";
                //html += "           <strong>Người nhận hàng</strong>";
                //html += "           <span class=\"note\">(Ký, họ tên)</span>";

                //html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
                //html += "       </div>";
                //html += "   </div>";
                //html += "</div>";

                //StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append(@"<script language='javascript'>");

                //sb.Append(@"VoucherPrint('" + html + "')");
                //sb.Append(@"</script>");

                /////hàm để đăng ký javascript và thực thi đoạn script trên
                //if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                //}
            }
        }
    }
}