using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using System.Text;

namespace NHST
{
    public partial class chi_tiet_don_hang_van_chuyen_ho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "vu221092";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            string username_current = Session["userLoginSystem"].ToString();
            double currency = 0;
            var obj_user = AccountController.GetByUsername(username_current);
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                 currency = Convert.ToDouble(config.Currency);
            }
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    ViewState["ID"] = id;
                    var t = TransportationOrderController.GetByIDAndUID(id, UID);
                    if (t != null)
                    {
                        double totalPrice = Convert.ToDouble(t.TotalPrice);
                        double deposited = Convert.ToDouble(t.Deposited);
                        double warehouseFee = 0;
                        if (t.WarehouseFee != null)
                        {
                            warehouseFee = Convert.ToDouble(t.WarehouseFee);
                        }
                        double totalmustpayleft = totalPrice + warehouseFee - deposited;
                        double totalPay = totalPrice + warehouseFee;
                        string createdDate = string.Format("{0:dd/MM/yyyy}", t.CreatedDate);
                        double totalWeight = 0;
                        double totalPackage = 0;
                        int stt = Convert.ToInt32(t.Status);
                        string status = PJUtils.generateTransportationStatus(stt);
                        string khoTQ = "";
                        string khoDich = "";
                        string shippingTypeName = "";

                        if (t.IsCheckProduct != null)
                            chkCheck.Checked = t.IsCheckProduct.ToString().ToBool();
                        if (t.IsPacked != null)
                            chkPackage.Checked = t.IsPacked.ToString().ToBool();
                        if (t.IsFastDelivery != null)
                            chkShiphome.Checked = t.IsFastDelivery.ToString().ToBool();

                        double checkproductprice = Convert.ToDouble(t.IsCheckProductPrice);
                        pCheck.Value = checkproductprice;
                        pCheckNDT.Value = checkproductprice / currency;

                        double packagedprice = Convert.ToDouble(t.IsPackedPrice);
                        pPacked.Value = packagedprice;
                        pPackedNDT.Value = packagedprice / currency;
                        pShipHome.Value = Convert.ToDouble(t.IsFastDeliveryPrice);



                        int tID = t.ID;
                        var wareTQ = WarehouseFromController.GetByID(Convert.ToInt32(t.WarehouseFromID));
                        if (wareTQ != null)
                        {
                            khoTQ = wareTQ.WareHouseName;
                        }
                        var wareDich = WarehouseController.GetByID(Convert.ToInt32(t.WarehouseID));
                        if (wareDich != null)
                        {
                            khoDich = wareDich.WareHouseName;
                        }
                        var shippingType = ShippingTypeToWareHouseController.GetByID(Convert.ToInt32(t.ShippingTypeID));
                        if (shippingType != null)
                        {
                            shippingTypeName = shippingType.ShippingTypeName;
                        }
                        if (stt == 0)
                        {
                            var tD = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                            if (tD.Count > 0)
                            {
                                totalPackage = tD.Count;
                                StringBuilder htmlPackages = new StringBuilder();
                                foreach (var s in tD)
                                {
                                    double weight = Convert.ToDouble(s.Weight);
                                    htmlPackages.Append("<tr>");
                                    htmlPackages.Append("   <td>" + s.TransportationOrderCode + "</td>");
                                    htmlPackages.Append("   <td>" + weight + "</td>");
                                    htmlPackages.Append("   <td><span class=\"bg-black\">Đã hủy</span></td>");
                                    htmlPackages.Append("</tr>");
                                    totalWeight += Convert.ToDouble(weight);
                                }
                                ltrListPackage.Text = htmlPackages.ToString();
                            }
                            //string btn = "";
                            //btn += "<a href=\"javascript:;\" onclick=\"cancelOrder()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Hủy đơn hàng</a>";
                            //btn += "<a href=\"javascript:;\" onclick=\"payOrder()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Thanh toán</a>";
                            //btn += "<a href=\"/danh-sach-don-van-chuyen-ho\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Trở về</a>";

                        }
                        else if (stt == 1)
                        {
                            var tD = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                            if (tD.Count > 0)
                            {
                                totalPackage = tD.Count;
                                StringBuilder htmlPackages = new StringBuilder();
                                foreach (var s in tD)
                                {
                                    double weight = Convert.ToDouble(s.Weight);
                                    htmlPackages.Append("<tr>");
                                    htmlPackages.Append("   <td>" + s.TransportationOrderCode + "</td>");
                                    htmlPackages.Append("   <td>" + weight + "</td>");
                                    htmlPackages.Append("   <td><span class=\"bg-red\">Chờ duyệt</span></td>");
                                    htmlPackages.Append("</tr>");
                                    totalWeight += Convert.ToDouble(weight);
                                }
                                ltrListPackage.Text = htmlPackages.ToString();
                            }
                            ltrBtn.Text += "<a href=\"javascript:;\" onclick=\"cancelOrder()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Hủy đơn hàng</a>";
                            btnHuy.Visible = true;
                            btnHuy.Attributes.Add("style", "display:none");
                        }
                        else
                        {
                            var packages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (packages.Count > 0)
                            {
                                totalPackage = packages.Count;
                                StringBuilder htmlPackages = new StringBuilder();
                                foreach (var s in packages)
                                {
                                    double weight = Convert.ToDouble(s.Weight);
                                    htmlPackages.Append("<tr>");
                                    htmlPackages.Append("   <td>" + s.OrderTransactionCode + "</td>");
                                    htmlPackages.Append("   <td>" + weight + "</td>");
                                    htmlPackages.Append("   <td>" + PJUtils.IntToStringStatusSmallPackageWithBG(Convert.ToInt32(s.Status)) + "</td>");
                                    htmlPackages.Append("</tr>");
                                    totalWeight += weight;
                                }
                                ltrListPackage.Text = htmlPackages.ToString();
                                if (totalPrice > 0)
                                {
                                    double feeinwarehouse = 0;
                                    if (t.WarehouseFee != null)
                                        feeinwarehouse = Convert.ToDouble(t.WarehouseFee);

                                    double leftPrice = (totalPrice + feeinwarehouse) - deposited;
                                    if (leftPrice > 0)
                                    {
                                        ltrBtn.Text += "<a href=\"javascript:;\" onclick=\"payOrder()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Thanh toán</a>";
                                        btnPay.Visible = true;
                                        btnPay.Attributes.Add("style", "display:none");
                                    }
                                }
                            }
                        }
                        #region Lấy thông tin
                        StringBuilder html = new StringBuilder();
                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Tên đăng nhập:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + username_current + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Ngày tạo:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + createdDate + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Trạng thái:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + status + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Kho TQ:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + khoTQ + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Kho Đích:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + khoDich + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Phương thức vận chuyển:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + shippingTypeName + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Tổng số kiện:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + totalPackage + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Tổng cân nặng:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + totalWeight + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Tiền lưu kho:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + string.Format("{0:N0}", warehouseFee) + " vnđ</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");

                        if (totalPrice > 0)
                        {
                            html.Append("<div class=\"form-row marbot2\">");
                            html.Append("   <div class=\"row-left\">");
                            html.Append("       <div class=\"lb\">Tổng tiền vận chuyển:</div>");
                            html.Append("   </div>");
                            html.Append("   <div class=\"row-right\">");
                            html.Append("       <strong>" + string.Format("{0:N0}", totalPrice) + " vnđ</strong>");
                            html.Append("   </div>");
                            html.Append("</div>");
                            html.Append("<div class=\"form-row marbot2\">");
                            html.Append("   <div class=\"row-left\">");
                            html.Append("       <div class=\"lb\">Tổng tiền:</div>");
                            html.Append("   </div>");
                            html.Append("   <div class=\"row-right\">");
                            html.Append("       <strong>" + string.Format("{0:N0}", totalPay) + " vnđ</strong>");
                            html.Append("   </div>");
                            html.Append("</div>");
                            if (totalmustpayleft >= deposited)
                            {
                                html.Append("<div class=\"form-row marbot2\">");
                                html.Append("   <div class=\"row-left\">");
                                html.Append("       <div class=\"lb\">Đã thanh toán:</div>");
                                html.Append("   </div>");
                                html.Append("   <div class=\"row-right\">");
                                html.Append("       <strong>" + string.Format("{0:N0}", deposited) + " vnđ</strong>");
                                html.Append("   </div>");
                                html.Append("</div>");

                                double leftMoney = totalPay - deposited;
                                html.Append("<div class=\"form-row marbot2\">");
                                html.Append("   <div class=\"row-left\">");
                                html.Append("       <div class=\"lb\">Còn lại:</div>");
                                html.Append("   </div>");
                                html.Append("   <div class=\"row-right\">");
                                html.Append("       <strong>" + string.Format("{0:N0}", leftMoney) + " vnđ</strong>");
                                html.Append("   </div>");
                                html.Append("</div>");
                            }
                        }

                        html.Append("<div class=\"form-row marbot2\">");
                        html.Append("   <div class=\"row-left\">");
                        html.Append("       <div class=\"lb\">Ghi chú:</div>");
                        html.Append("   </div>");
                        html.Append("   <div class=\"row-right\">");
                        html.Append("       <strong>" + t.Description + "</strong>");
                        html.Append("   </div>");
                        html.Append("</div>");


                        ltrInfor.Text = html.ToString();
                        #endregion
                    }
                }
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                var id = ViewState["ID"].ToString().ToInt(0);
                if (id > 0)
                {
                    var t = TransportationOrderController.GetByIDAndUID(id, UID);
                    if (t != null)
                    {
                        TransportationOrderController.UpdateStatus(t.ID, 0, DateTime.Now, username_current);
                        PJUtils.ShowMessageBoxSwAlert("Hủy đơn hàng thành công", "s", true, Page);
                    }
                }
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                double wallet = Convert.ToDouble(obj_user.Wallet);
                int UID = obj_user.ID;
                var id = ViewState["ID"].ToString().ToInt(0);
                if (id > 0)
                {
                    var t = TransportationOrderController.GetByIDAndUID(id, UID);
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
                        if(t.WarehouseFee!=null)
                        {
                            warehouseFee = Convert.ToDouble(t.WarehouseFee);
                        }
                        double deposited = Convert.ToDouble(t.Deposited);
                        double totalPrice = price * totalWeight * currency + warehouseFee;
                        double leftMoney = totalPrice - deposited;
                        if (leftMoney <= wallet)
                        {
                            double walletLeft = wallet - leftMoney;
                            using (NHSTEntities productDbContext = new NHSTEntities())
                            {
                                using (var transaction = productDbContext.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        TransportationOrderController.UpdateStatusAndDeposited(t.ID, totalPrice, 6, currentDate, username_current);
                                        AccountController.updateWallet(UID, walletLeft, currentDate, username_current);
                                        HistoryPayWalletController.InsertTransportation(UID, username_current, 0, leftMoney, username_current + " đã thanh toán đơn hàng vận chuyển hộ: " + t.ID + ".", walletLeft, 1, 8, currentDate, username_current, t.ID);
                                        PJUtils.ShowMessageBoxSwAlert("Thanh toán đơn thành công", "s", true, Page);
                                        transaction.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                    }
                                }
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số tiền trong tài khoản của bạn không đủ để thanh toán đơn hàng này.", "e", true, Page);
                        }
                    }
                }
            }
        }
    }
}