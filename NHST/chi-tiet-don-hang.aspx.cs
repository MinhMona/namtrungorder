using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;
using System.Web.Script.Serialization;

namespace NHST
{
    public partial class chi_tiet_don_hang1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    loaddata();
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }
        public void loaddata()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                hdfID.Value = obj_user.ID.ToString();
                int uid = obj_user.ID;
                #region Update Trước
                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double totalprice = Convert.ToDouble(o.TotalPriceVND);
                        double feeinwarehouse = 0;
                        if (o.FeeInWareHouse != null)
                            feeinwarehouse = Convert.ToDouble(o.FeeInWareHouse);

                        double totalPay = totalprice + feeinwarehouse;
                        double deposited = Convert.ToDouble(o.Deposit);
                        double leftpay = totalPay - deposited;
                        if (leftpay > 0)
                        {
                            if (o.Status > 7)
                            {
                                MainOrderController.UpdateStatus(o.ID, uid, 7);
                            }
                        }
                    }
                }
                #endregion

                double usercurrency = 0;
                if (!string.IsNullOrEmpty(obj_user.Currency))
                {
                    if (obj_user.Currency.ToFloat(0) > 0)
                    {
                        usercurrency = Convert.ToDouble(obj_user.Currency);
                    }
                }

                double UL_CKFeeBuyPro = 0;
                double UL_CKFeeWeight = 0;

                UL_CKFeeBuyPro = UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro.ToString().ToFloat(0);
                UL_CKFeeWeight = UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight.ToString().ToFloat(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        hdfOrderID.Value = o.ID.ToString();
                        var config = ConfigurationController.GetByTop1();
                        double currency = 0;
                        double currency1 = 0;
                        if (config != null)
                        {
                            double currencyconfig = 0;
                            if (!string.IsNullOrEmpty(config.Currency))
                                currencyconfig = Convert.ToDouble(config.Currency);
                            currency = Math.Floor(Convert.ToDouble(o.CurrentCNYVN));
                            currency1 = Math.Floor(Convert.ToDouble(o.CurrentCNYVN));
                        }
                        if (usercurrency > 0)
                        {
                            currency = usercurrency;
                            currency1 = usercurrency;
                        }

                        ViewState["OID"] = id;

                        #region lấy tất cả kiện
                        var smallpackages = SmallPackageController.GetByMainOrderID(o.ID);
                        if (smallpackages.Count > 0)
                        {
                            foreach (var s in smallpackages)
                            {
                                double weigthQD = 0;
                                double pDai = Convert.ToDouble(s.Length);
                                double pRong = Convert.ToDouble(s.Width);
                                double pCao = Convert.ToDouble(s.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    weigthQD = (pDai * pRong * pCao) / 6000;
                                }
                                double cantinhtien = weigthQD;
                                if (Convert.ToDouble(s.Weight) > weigthQD)
                                {
                                    cantinhtien = Convert.ToDouble(s.Weight);
                                }
                                ltrSmallPackages.Text += "<tr>";
                                ltrSmallPackages.Text += "  <td class=\"pro\">" + s.OrderTransactionCode + "</td>";
                                ltrSmallPackages.Text += "  <td class=\"pro\">" + Math.Round(Convert.ToDouble(s.Weight), 2) + "</td>";
                                ltrSmallPackages.Text += "  <td class=\"pro\">" + pDai + " x " + pRong + " x " + pCao + "</td>";
                                ltrSmallPackages.Text += "  <td class=\"pro\">" + Math.Round(weigthQD, 2) + "</td>";
                                ltrSmallPackages.Text += "  <td class=\"pro\">" + Math.Round(cantinhtien, 2) + "</td>";
                                ltrSmallPackages.Text += "  <td class=\"pro\">" + PJUtils.IntToStringStatusSmallPackageWithBG(Convert.ToInt32(s.Status)) + "</td>";
                                ltrSmallPackages.Text += "  <td class=\"price\">" + s.Description + "</td>";
                                ltrSmallPackages.Text += "</tr>";
                            }
                        }
                        #endregion
                        #region Lịch sử thanh toán
                        var PayorderHistory = PayOrderHistoryController.GetAllByMainOrderID(o.ID);
                        if (PayorderHistory.Count > 0)
                        {
                            rptPayment.DataSource = PayorderHistory;
                            rptPayment.DataBind();
                        }
                        else
                        {
                            ltrpa.Text = "<tr>Chưa có lịch sử thanh toán nào.</tr>";
                        }
                        #endregion
                        double pricepro = Convert.ToDouble(o.PriceVND);
                        double servicefee = 0;

                        if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                        {
                            if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                            {
                                servicefee = Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                            }
                            else
                            {
                                var adminfeebuypro = FeeBuyProController.GetAll();
                                if (adminfeebuypro.Count > 0)
                                {
                                    foreach (var item in adminfeebuypro)
                                    {
                                        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                        {
                                            servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var adminfeebuypro = FeeBuyProController.GetAll();
                            if (adminfeebuypro.Count > 0)
                            {
                                foreach (var item in adminfeebuypro)
                                {
                                    if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                    {
                                        servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                        break;
                                    }
                                }
                            }
                        }


                        ltrshopinfo.Text = "<span class=\"shop-info\">" + o.ShopName + " - Mã đơn hàng: " + o.ID + " </span>";

                        double feebpnotdc = pricepro * servicefee;
                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        double userdadeposit = 0;
                        if (o.Deposit != null)
                            userdadeposit = Convert.ToDouble(o.Deposit);
                        //Kiểm tra đơn hàng hiển thị button hủy đơn hàng
                        int oStatus = Convert.ToInt32(o.Status);
                        if (oStatus == 0)
                        {
                            pn.Visible = true;
                        }
                        else if (oStatus == 1)
                        {
                            //pn_sendcomment.Visible = false;
                        }

                        double feewarehouse = 0;
                        if (o.FeeInWareHouse != null)
                            feewarehouse = Convert.ToDouble(o.FeeInWareHouse);
                        double totalPrice = Convert.ToDouble(o.TotalPriceVND);
                        double totalPay = totalPrice + feewarehouse;
                        double totalleft = totalPay - userdadeposit;
                        if (totalleft > 0)
                        {
                            if (obj_user.Wallet >= totalleft)
                            {
                                if (o.Status > 6)
                                    pnthanhtoan.Visible = true;
                            }
                        }

                        #region Generate Trạng thái


                        StringBuilder htmlProcess = new StringBuilder();
                        if (oStatus == 0)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ shop TQ phát hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho TQ</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho VN</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");

                        }
                        else if (oStatus == 2)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ shop TQ phát hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho TQ</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho VN</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 5)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green-1\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ shop TQ phát hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho TQ</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho VN</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 6)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green-1\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ shop TQ phát hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho TQ</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho VN</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 7)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green-1\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ shop TQ phát hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho TQ</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-orange\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho VN</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 9)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green-1\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ shop TQ phát hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho TQ</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-orange\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho VN</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-blue\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 10)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green-1\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chờ shop TQ phát hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho TQ</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-orange\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã về kho VN</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-blue\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-blue\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        ltrstep.Text = htmlProcess.ToString();
                        #endregion

                        #region Lấy thông tin đơn hàng và thông tin người đặt                       
                        ltrOrderFee.Text += "<div class=\"order-panel\">";
                        ltrOrderFee.Text += " <div class=\"title\">Thông tin đơn hàng</div>";
                        ltrOrderFee.Text += " <div class=\"cont\">";
                        ltrOrderFee.Text += "     <dl>";
                        ltrOrderFee.Text += "         <dt class=\"full-width\"><strong class=\"title-fee\">Phí cố định</strong></dt>";
                        if (o.OrderType == 3)
                        {
                            if (o.IsCheckNotiPrice == true)
                            {
                                ltrOrderFee.Text += "         <dt>Trạng thái đơn hàng</dt><dd><span class=\"bg-yellow-gold\">Chờ báo giá</span></dd>";
                            }
                            else
                            {
                                ltrOrderFee.Text += "         <dt>Trạng thái đơn hàng</dt><dd>" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + "</dd>";
                            }
                        }
                        else
                        {
                            ltrOrderFee.Text += "         <dt>Trạng thái đơn hàng</dt><dd>" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + "</dd>";
                        }

                        ltrOrderFee.Text += "         <dt>Tiền hàng trên web</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " vnđ (<i class=\"fa fa-yen\"></i>" + string.Format("{0:#.##}", Convert.ToDouble(o.PriceVND) / o.CurrentCNYVN.ToFloat()) + ")</dd>";
                        if (!string.IsNullOrEmpty(o.FeeShipCN))
                        {
                            double scn = Convert.ToDouble(o.FeeShipCN);
                            if (scn > 0)
                                ltrOrderFee.Text += "         <dt>Phí ship Trung Quốc</dt><dd>" + string.Format("{0:N0}", scn) + " vnđ (<i class=\"fa fa-yen\"></i>" + string.Format("{0:#.##}", scn / o.CurrentCNYVN.ToFloat()) + ")</dd>";
                            else
                                ltrOrderFee.Text += "         <dt>Phí ship Trung Quốc</dt><dd>Đang cập nhật</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí ship Trung Quốc</dt><dd>Đang cập nhật</dd>";

                        if (!string.IsNullOrEmpty(o.FeeBuyPro))
                        {
                            double bp = Convert.ToDouble(o.FeeBuyPro);
                            if (bp > 0)
                            {
                                if (UL_CKFeeBuyPro > 0)
                                    ltrOrderFee.Text += "         <dt>Phí dịch vụ (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " vnđ)</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                                else
                                    ltrOrderFee.Text += "         <dt>Phí dịch vụ</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                            }
                            else
                                ltrOrderFee.Text += "         <dt>Phí dịch vụ</dt><dd>Đang cập nhật</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí dịch vụ</dt><dd>Đang cập nhật</dd>";
                        ltrOrderFee.Text += "         <dt>Tổng cân nặng</dt><dd>" + o.OrderWeight.ToFloat(0) + " KG</dd>";
                        if (UL_CKFeeWeight > 0)
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN (Đã CK " + UL_CKFeeWeight + "% : " + string.Format("{0:N0}", o.FeeWeightCK.ToFloat(0)) + " vnđ)</dt><dd>" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN</dt><dd>" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " vnđ</dd>";
                        ltrOrderFee.Text += "             <dt>Nhận hàng tại</dt><dd>" + o.ReceivePlace + "</dd>";
                        ltrOrderFee.Text += "         <dt class=\"full-width\"><strong class=\"title-fee\">Phí tùy chọn</strong></dt>";
                        if (o.IsCheckProduct == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked  disabled=\"disabled\" /> Phí kiểm đếm</dt><dd>" + string.Format("{0:N0}", o.IsCheckProductPrice.ToFloat(0)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\" /> Phí kiểm đếm</dt><dd>" + string.Format("{0:N0}", o.IsCheckProductPrice.ToFloat(0)) + " vnđ</dd>";
                        if (o.IsPacked == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked=\"" + o.IsPacked + "\" disabled=\"disabled\"  /> Phí đóng gỗ</dt><dd>" + string.Format("{0:N0}", o.IsPackedPrice.ToFloat(0)) + " vnđ (<i class=\"fa fa-yen\"></i>" + string.Format("{0:#.##}", o.IsPackedPrice.ToFloat(0) / o.CurrentCNYVN.ToFloat()) + ")</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\"  /> Phí đóng gỗ</dt><dd>" + string.Format("{0:N0}", o.IsPackedPrice.ToFloat(0)) + " vnđ (<i class=\"fa fa-yen\"></i>" + string.Format("{0:#.##}", o.IsPackedPrice.ToFloat(0) / o.CurrentCNYVN.ToFloat()) + ")</dd>";
                        if (o.IsFastDelivery == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked=\"" + o.IsFastDelivery + "\" disabled=\"disabled\"  /> Phí ship giao hàng tận nhà</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastDeliveryPrice)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\"  /> Phí ship giao hàng tận nhà</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastDeliveryPrice)) + " vnđ</dd>";
                        if (o.IsFast == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked=\"" + o.IsFast + "\" disabled=\"disabled\"  /> Phí đơn hàng hỏa tốc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastPrice)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\"  /> Phí đơn hàng hỏa tốc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastPrice)) + " vnđ</dd>";


                        ltrOrderFee.Text += "         <dt class=\"full-width\"><strong class=\"title-fee\">Thanh toán</strong></dt>";
                        if (!string.IsNullOrEmpty(o.AmountDeposit))
                            ltrOrderFee.Text += "         <dt>Số tiền phải đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit)) + " vnđ</dd>";
                        if (!string.IsNullOrEmpty(o.Deposit))
                            ltrOrderFee.Text += "         <dt>Tiền đã đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.Deposit)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Tiền đã đặt cọc</dt><dd>0 vnđ</dd>";

                        ltrOrderFee.Text += "             <dt>Số tiền còn lại phải đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit) - Convert.ToDouble(o.Deposit)) + " vnđ</dd>";


                        if (o.OrderType == 3)
                        {
                            if (o.IsCheckNotiPrice == true)
                            {

                            }
                            else
                            {
                                if (o.Status == 0 && Convert.ToDouble(o.Deposit) < Convert.ToDouble(o.AmountDeposit) && Convert.ToDouble(o.TotalPriceVND) > 0)
                                {
                                    //ltrOrderFee.Text += "         <dt><a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a></dt><dd></dd>";
                                    ltrbtndeposit.Text += "         <a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a>";
                                }
                            }
                        }
                        else
                        {
                            if (o.Status == 0 && Convert.ToDouble(o.Deposit) < Convert.ToDouble(o.AmountDeposit) && Convert.ToDouble(o.TotalPriceVND) > 0)
                            {
                                //ltrOrderFee.Text += "         <dt><a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a></dt><dd></dd>";
                                ltrbtndeposit.Text += "         <a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a>";
                            }
                        }



                        ltrOrderFee.Text += "     </dl>";
                        ltrOrderFee.Text += " </div>";
                        ltrOrderFee.Text += "</div>";

                        ltrOrderFee.Text += "<div class=\"order-panel  bg-red-nhst print4\">";
                        ltrOrderFee.Text += "   <div class=\"title\">Tổng tiền khách hàng cần thanh toán</div>";
                        ltrOrderFee.Text += "   <div class=\"cont\">";
                        ltrOrderFee.Text += "     <dl>";
                        ltrOrderFee.Text += "         <dt>Tiền hàng</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " vnđ</dd>";
                        ltrOrderFee.Text += "         <dt>Phí Ship nội địa</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.FeeShipCN)) + " vnđ</dd>";
                        if (!string.IsNullOrEmpty(o.FeeBuyPro))
                        {
                            double bp = Convert.ToDouble(o.FeeBuyPro);
                            if (bp > 0)
                            {
                                if (UL_CKFeeBuyPro > 0)
                                    ltrOrderFee.Text += "         <dt>Phí dịch vụ (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " vnđ)</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                                else
                                    ltrOrderFee.Text += "         <dt>Phí dịch vụ</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                                //ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                            }
                            else
                                ltrOrderFee.Text += "         <dt>Phí dịch vụ</dt><dd>Đang cập nhật</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí dịch vụ</dt><dd>Đang cập nhật</dd>";
                        double totalFee = o.IsCheckProductPrice.ToFloat(0) + o.IsPackedPrice.ToFloat(0) + o.IsFastDeliveryPrice.ToFloat(0) + o.IsFastPrice.ToFloat(0);
                        ltrOrderFee.Text += "         <dt>Phí tùy chọn</dt><dd>" + string.Format("{0:N0}", totalFee) + " vnđ</dd>";

                        if (UL_CKFeeWeight > 0)
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN (Đã CK " + UL_CKFeeWeight + "% : " + string.Format("{0:N0}", o.FeeWeightCK.ToFloat(0)) + " vnđ)</dt><dd>" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN</dt><dd>" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " vnđ</dd>";

                        ltrOrderFee.Text += "         <dt class=\"full-width-sepearate\"></dt>";
                        ltrOrderFee.Text += "         <dt class=\"line-special\"><strong class=\"color-white\">Tổng chi phí</strong></dt><dd class=\"line-special\">" + string.Format("{0:N0}", o.TotalPriceVND.ToFloat(0)) + " vnđ</dd>";
                        ltrOrderFee.Text += "         <dt class=\"line-special\"><strong class=\"color-white\">Đã thanh toán</strong></dt><dd class=\"line-special\">" + string.Format("{0:N0}", o.Deposit.ToFloat(0)) + " vnđ</dd>";
                        ltrOrderFee.Text += "         <dt class=\"line-special\"><strong class=\"color-white\">Tiền còn thiếu</strong></dt><dd class=\"line-special\">" + string.Format("{0:N0}", o.TotalPriceVND.ToFloat(0) - o.Deposit.ToFloat(0)) + " vnđ</dd>";
                        ltrOrderFee.Text += "     </dl>";
                        ltrOrderFee.Text += "   </div>";
                        ltrOrderFee.Text += "</div>";

                        var ui = AccountInfoController.GetByUserID(uid);
                        if (ui != null)
                        {
                            string phone = ui.MobilePhonePrefix + ui.MobilePhone;
                            txt_Fullname.Text = ui.FirstName + " " + ui.LastName;
                            txt_Address.Text = ui.Address;
                            txt_Email.Text = ui.Email;
                            txt_Phone.Text = phone;
                            //txt_DNote.Text = o.Note;
                        }
                        #endregion
                        #region Lấy sản phẩm

                        List<tbl_Order> lo = new List<tbl_Order>();
                        lo = OrderController.GetByMainOrderID(o.ID);
                        double totalQuantity = 0;
                        if (lo.Count > 0)
                        {
                            //rpt.DataSource = lo;
                            //rpt.DataBind();
                            int stt = 1;
                            foreach (var item in lo)
                            {
                                double currentcyt = item.CurrentCNYVN.ToFloat(0);
                                double price = 0;
                                double pricepromotion = item.price_promotion.ToFloat(0);
                                double priceorigin = item.price_origin.ToFloat(0);
                                totalQuantity += item.quantity.ToInt(0);
                                if (pricepromotion > 0)
                                {
                                    if (priceorigin > pricepromotion)
                                    {
                                        price = pricepromotion;
                                    }
                                    else
                                    {
                                        price = priceorigin;
                                    }
                                }
                                else
                                {
                                    price = priceorigin;
                                }
                                double vndprice = price * currentcyt;
                                ltrProducts.Text += "<tr>";
                                ltrProducts.Text += "<td class=\"pro\">" + stt + "</td>";
                                ltrProducts.Text += "<td class=\"pro\">";
                                ltrProducts.Text += "<div class=\"thumb-product\">";
                                ltrProducts.Text += "<div class=\"pd-img\"><a href=\"" + item.link_origin + "\" target=\"_blank\"><img src=\"" + item.image_origin + "\" alt=\"\"></a></div>";
                                ltrProducts.Text += "<div class=\"info\"><a href=\"" + item.link_origin + "\" target=\"_blank\">" + item.title_origin + "</a></div>";
                                ltrProducts.Text += "</div>";
                                ltrProducts.Text += "</td>";
                                ltrProducts.Text += "<td class=\"pro\">" + item.property + "</td>";
                                ltrProducts.Text += "<td class=\"qty\">" + item.quantity + "</td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">¥" + string.Format("{0:0.##}", price) + "</p></td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", vndprice) + " vnđ</p></td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">" + item.brand + "</p></td>";
                                if (!string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                {
                                    if (item.ProductStatus == 1)
                                        ltrProducts.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                    else
                                        ltrProducts.Text += "<td class=\"price\"><p class=\"\">Hết hàng</p></td>";
                                }
                                else
                                    ltrProducts.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                ltrProducts.Text += "</tr>";
                                stt++;
                            }
                            ltrProducts.Text += " <div class=\"rest-table\">";
                            ltrProducts.Text += "<table class=\"tbl-subtotal width-60-per  mar-top2 mar-bot2\">";
                            ltrProducts.Text += "     <tbody>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            if (o.OrderType == 3)
                            {
                                if (o.IsCheckNotiPrice == true)
                                {
                                    ltrProducts.Text += "             <td class=\"float-left\">Trạng thái đơn hàng:</td>";
                                    ltrProducts.Text += "             <td class=\"float-right\"><span class=\"bg-yellow-gold\">Chờ báo giá</span> </td>";
                                }
                                else
                                {
                                    ltrProducts.Text += "             <td class=\"float-left\">Trạng thái đơn hàng:</td>";
                                    ltrProducts.Text += "             <td class=\"float-right\">" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + " </td>";
                                }
                            }
                            else
                            {
                                ltrProducts.Text += "             <td class=\"float-left\">Trạng thái đơn hàng:</td>";
                                ltrProducts.Text += "             <td class=\"float-right\">" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + " </td>";
                            }

                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Tiền hàng:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " vnđ (¥ " + Convert.ToDouble(o.PriceVND) / Convert.ToDouble(o.CurrentCNYVN) + ")</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Tồng số lượng SP:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + totalQuantity + "</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";

                            if (!string.IsNullOrEmpty(o.FeeBuyPro))
                            {
                                double bp = Convert.ToDouble(o.FeeBuyPro);
                                if (bp > 0)
                                {
                                    if (UL_CKFeeBuyPro > 0)
                                    {
                                        ltrProducts.Text += "             <td class=\"float-left\">Phí dịch vụ (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " vnđ):</td>";
                                        ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", bp) + " VNĐ</td>";
                                    }
                                    else
                                    {
                                        ltrProducts.Text += "             <td class=\"float-left\">Phí dịch vụ :</td>";
                                        ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", bp) + " VNĐ</td>";
                                    }
                                }
                                else
                                {
                                    ltrProducts.Text += "             <td class=\"float-left\">Phí dịch vụ :</td>";
                                    ltrProducts.Text += "             <td class=\"float-right\">Đang cập nhật</td>";
                                }
                            }
                            else
                            {
                                ltrProducts.Text += "             <td class=\"float-left\">Phí dịch vụ :</td>";
                                ltrProducts.Text += "             <td class=\"float-right\">Đang cập nhật</td>";
                            }

                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Phí kiểm đếm:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", o.IsCheckProductPrice.ToFloat(0)) + " VNĐ</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Phí ship nội địa TQ:</td>";
                            if (!string.IsNullOrEmpty(o.FeeShipCN))
                            {
                                double fscn = Math.Floor(Convert.ToDouble(o.FeeShipCN));
                                double phhinoidiate = fscn / currency1;
                                ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.FeeShipCN)) + " VNĐ (¥ " + phhinoidiate + ")</td>";
                            }
                            else
                                ltrProducts.Text += "             <td class=\"float-right\">Đang cập nhật</td>";
                            ltrProducts.Text += "         </tr>";

                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            if (UL_CKFeeWeight > 0)
                            {
                                ltrProducts.Text += "             <td class=\"float-left\">Phí cân nặng (Đã CK " + UL_CKFeeWeight + "% : " + string.Format("{0:N0}", o.FeeWeightCK.ToFloat(0)) + " VNĐ):</td>";
                                if (!string.IsNullOrEmpty(o.OrderWeight))
                                    ltrProducts.Text += "             <td class=\"float-right\">Trọng lượng: " + o.OrderWeight + " kg - " + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</td>";
                                else
                                    ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</td>";
                            }
                            else
                            {
                                ltrProducts.Text += "             <td class=\"float-left\">Phí cân nặng:</td>";
                                if (!string.IsNullOrEmpty(o.OrderWeight))
                                    ltrProducts.Text += "             <td class=\"float-right\">Trọng lượng: " + o.OrderWeight + " kg - " + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</td>";
                                else
                                    ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</td>";
                            }

                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Phí đóng gỗ:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.IsPackedPrice)) + " VNĐ</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Phí giao tận nhà:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastDeliveryPrice)) + " VNĐ</td>";
                            ltrProducts.Text += "         </tr>";

                            double feeinwarehouse = 0;
                            if (o.FeeInWareHouse != null)
                            {
                                feeinwarehouse = Convert.ToDouble(o.FeeInWareHouse);
                            }
                            if (feeinwarehouse > 0)
                            {
                                ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                                ltrProducts.Text += "             <td class=\"float-left\">Phí lưu kho:</td>";
                                ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", feeinwarehouse) + " VNĐ</td>";
                                ltrProducts.Text += "         </tr>";
                            }
                            if (!string.IsNullOrEmpty(o.AmountDeposit))
                            {
                                ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                                ltrProducts.Text += "             <td class=\"float-left\">Số tiền phải đặt cọc:</td>";
                                ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit)) + " VNĐ</td>";
                                ltrProducts.Text += "         </tr>";
                            }
                            ltrProducts.Text += "     </tbody>";
                            ltrProducts.Text += "</table>";
                            ltrProducts.Text += "<table class=\"tbl-subtotal width-35-per  mar-top2 mar-bot2\">";
                            ltrProducts.Text += "     <tbody>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";

                            ltrProducts.Text += "         <tr class=\"black b font-size-25\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Tổng Đơn:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND) + feeinwarehouse) + " VNĐ</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Đã thanh toán:</td>";
                            double deposit = 0;
                            if (!string.IsNullOrEmpty(o.Deposit))
                            {
                                deposit = Convert.ToDouble(o.Deposit);
                                ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", deposit) + " VNĐ</td>";
                            }
                            else
                                ltrProducts.Text += "             <td class=\"float-right\">Chưa đặt cọc</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20 color-orange\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Cần thanh toán:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND) + feeinwarehouse - deposit) + " VNĐ</td>";
                            ltrProducts.Text += "         </tr>";


                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\"></td>";
                            ltrProducts.Text += "             <td class=\"float-right\"></td>";
                            ltrProducts.Text += "         </tr>";

                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td style=\"color:red\" class=\"float-left\">Ghi chú đơn hàng:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + o.Note + " </td>";
                            ltrProducts.Text += "         </tr>";


                            ltrProducts.Text += "     </tbody>";
                            ltrProducts.Text += "</table>";
                            ltrProducts.Text += " </div>";
                        }
                        #endregion
                        #region Lấy bình luận
                        //ltrComment.Text += "<div class=\"comment mar-bot2\">";
                        //ltrComment.Text += "     <div class=\"comment_content\" seller=\"" + o.ShopID + "\" order=\"" + o.ID + "\" >";
                        var shopcomments = OrderCommentController.GetByOrderIDAndType(o.ID, 1,1);
                        if (shopcomments.Count > 0)
                        {
                            foreach (var item in shopcomments)
                            {
                                string fullname = "";
                                int role = 0;
                                var user = AccountController.GetByID(Convert.ToInt32(item.CreatedBy));
                                if (user != null)
                                {
                                    role = Convert.ToInt32(user.RoleID);
                                    var userinfo = AccountInfoController.GetByUserID(user.ID);
                                    if (userinfo != null)
                                    {
                                        fullname = userinfo.FirstName + " " + userinfo.LastName;
                                    }
                                }
                                if (role == 1)
                                {
                                    ltrComment.Text += "         <span class=\"user-comment\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> : " + item.Comment + "<br>";
                                    if (!string.IsNullOrEmpty(item.Link))
                                    {
                                        ltrComment.Text += "         <span class=\"user-comment\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> :<br/> <a href=\"" + item.Link + "\" target=\"_blank\"><img src=\"" + item.Link + "\"/></a><br>";
                                    }
                                }
                                else
                                {
                                    ltrComment.Text += "         <span class=\"user-comment green\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> : <span class=\"green\">" + item.Comment + "</span><br>";
                                    if (!string.IsNullOrEmpty(item.Link))
                                    {
                                        ltrComment.Text += "         <span class=\"user-comment green\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> :<br/> <a href=\"" + item.Link + "\" target=\"_blank\"><img src=\"" + item.Link + "\"/></a><br>";
                                    }
                                }
                            }
                        }
                        else
                        {
                            ltrComment.Text += "         <span class=\"user-comment\">Chưa có ghi chú.</span>";
                        }
                        //ltrComment.Text += "     </div>";
                        //ltrComment.Text += "     <div class=\"comment_action\" style=\"padding-bottom: 4px; padding-top: 4px;\">";
                        //ltrComment.Text += "        <label>";
                        //ltrComment.Text += "	        <span>Thêm ảnh</span>";
                        //ltrComment.Text += "	        <input style=\"float: left;border:none\" id=\"images\" multiple type=\"file\" onchange=\"readFiles(this,'customercomment');\">";
                        //ltrComment.Text += "        </label>";
                        //ltrComment.Text += "        <ul class=\"row-package customercomment\"></ul>";
                        //ltrComment.Text += "     </div>";
                        //ltrComment.Text += "     <div class=\"comment_action\" style=\"padding-bottom: 4px; padding-top: 4px;\">";
                        //ltrComment.Text += "         <input shop_code=\"" + o.ID + "\" type=\"text\" class=\"comment-text\" order=\"188083\" seller=\"" + o.ShopID + "\" placeholder=\"Nội dung\">";
                        ////ltrComment.Text += "         <a id=\"sendnotecomment\" onclick=\"postcomment($(this))\" order=\"" + o.ID + "\" class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" style=\"min-width:10px;\">Gửi</a>";
                        //ltrComment.Text += "         <a id=\"sendnotecomment\" order=\"" + o.ID + "\" class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" style=\"min-width:10px;\">Gửi</a>";
                        //ltrComment.Text += "     </div>";
                        //ltrComment.Text += "</div>";

                        #endregion
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);

            int uid = obj_user.ID;
            var id = RouteData.Values["id"].ToString().ToInt(0);
            var o = MainOrderController.GetAllByUIDAndID(uid, id);
            List<tbl_Order> lo = new List<tbl_Order>();
            lo = OrderController.GetByMainOrderID(o.ID);
            double totalQuantity = 0;
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Link sản phẩm</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Thuộc tính</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số lượng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Đơn giá(¥)</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Đơn giá(VNĐ)</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ghi chú</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in lo)
            {
                double currentcyt = item.CurrentCNYVN.ToFloat(0);
                double price = 0;
                double pricepromotion = item.price_promotion.ToFloat(0);
                double priceorigin = item.price_origin.ToFloat(0);
                totalQuantity += item.quantity.ToInt(0);
                if (pricepromotion > 0)
                {
                    if (priceorigin > pricepromotion)
                    {
                        price = pricepromotion;
                    }
                    else
                    {
                        price = priceorigin;
                    }
                }
                else
                {
                    price = priceorigin;
                }
                double vndprice = price * currentcyt;
                var a = AccountController.GetByID(Convert.ToInt32(item.UID));
                StrExport.Append("  <tr>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + a.Username + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.link_origin + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.property + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.quantity + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", price) + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", vndprice) + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.brand + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "danh-sach-san-pham.xls";
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
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double wallet = obj_user.Wallet.ToString().ToFloat();
                        wallet = wallet + Convert.ToDouble(o.Deposit);
                        AccountController.updateWallet(obj_user.ID, wallet, DateTime.Now, obj_user.Username);
                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, "0");
                        string kq = MainOrderController.UpdateStatus(id, uid, 1);
                        if (kq == "ok")
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    }
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        //string kq = OrderCommentController.Insert(id, txtComment.Text, true, 1, DateTime.Now, uid);
                        //if (Convert.ToInt32(kq) > 0)
                        //    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    }
                }
            }
        }

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                if (obj_user.Wallet > 0)
                {
                    int OID = ViewState["OID"].ToString().ToInt();
                    if (OID > 0)
                    {
                        var o = MainOrderController.GetAllByID(OID);
                        if (o != null)
                        {
                            double orderdeposited = 0;
                            if (!string.IsNullOrEmpty(o.Deposit))
                                orderdeposited = Convert.ToDouble(o.Deposit);
                            double amountdeposit = Convert.ToDouble(o.AmountDeposit);
                            double leftDeposit = amountdeposit - orderdeposited;
                            if (leftDeposit > 0)
                            {
                                double userwallet = Convert.ToDouble(obj_user.Wallet);
                                if (userwallet > 0)
                                {
                                    if (userwallet >= leftDeposit)
                                    {
                                        using (NHSTEntities productDbContext = new NHSTEntities())
                                        {
                                            using (var transaction = productDbContext.Database.BeginTransaction())
                                            {
                                                try
                                                {
                                                    double wallet = userwallet - leftDeposit;
                                                    AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                                    //Cập nhật lại MainOrder                                
                                                    MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                                    MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                                    MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                                    HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                                    //PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);
                                                    PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, leftDeposit, 2, currentDate, obj_user.Username);
                                                    transaction.Commit();
                                                }
                                                catch (Exception exception)
                                                {
                                                    transaction.Rollback();
                                                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                                }
                                            }
                                        }
                                        //Cập nhật lại Wallet User

                                    }
                                    else
                                    {
                                        ltr_info.Text = "Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.";
                                        ltr_info.ForeColor = System.Drawing.Color.Red;
                                        ltr_info.Visible = true;
                                    }
                                    //if (orderdeposited > 0)
                                    //{
                                    //    double depleft = amountdeposit - orderdeposited;
                                    //    if (userwallet >= depleft)
                                    //    {
                                    //        double wallet = userwallet - depleft;
                                    //        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                    //        //Cập nhật lại MainOrder                                
                                    //        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                    //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                    //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, depleft, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                    //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, depleft, 2, currentDate, obj_user.Username);
                                    //    }
                                    //    else
                                    //    {
                                    //        double walletleft = depleft - userwallet;
                                    //        AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                                    //        double newpay = orderdeposited + userwallet;
                                    //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, newpay.ToString());
                                    //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                                    //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (userwallet >= amountdeposit)
                                    //    {
                                    //        //Cập nhật lại Wallet User
                                    //        double wallet = userwallet - amountdeposit;
                                    //        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                    //        //Cập nhật lại MainOrder                                
                                    //        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                    //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                    //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                    //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);
                                    //    }
                                    //    else
                                    //    {
                                    //        double paid = amountdeposit - userwallet;
                                    //        // Cập nhật lại Wallet User
                                    //        AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                                    //        //Cập nhật lại MainOrder                            
                                    //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, userwallet.ToString());
                                    //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                                    //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                                    //    }
                                    //}

                                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                                }
                            }

                        }
                    }
                }
                else
                {
                    ltr_info.Text = "Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.";
                    ltr_info.ForeColor = System.Drawing.Color.Red;
                    ltr_info.Visible = true;
                }
            }
        }

        protected void btnPayAll_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = ViewState["OID"].ToString().ToInt(0);
                DateTime currentDate = DateTime.Now;
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double deposit = o.Deposit.ToFloat(0);
                        double wallet = obj_user.Wallet.ToString().ToFloat(0);
                        double feewarehouse = 0;
                        if (o.FeeInWareHouse != null)
                            feewarehouse = Convert.ToDouble(o.FeeInWareHouse);
                        double moneyleft = (o.TotalPriceVND.ToFloat(0) + feewarehouse) - deposit;

                        if (wallet >= moneyleft)
                        {
                            using (NHSTEntities productDbContext = new NHSTEntities())
                            {
                                using (var transaction = productDbContext.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        double walletLeft = wallet - moneyleft;
                                        double payalll = deposit + moneyleft;
                                        MainOrderController.UpdateStatus(o.ID, uid, 9);
                                        AccountController.updateWallet(uid, walletLeft, currentDate, username);

                                        HistoryOrderChangeController.Insert(o.ID, uid, username, username +
                                                    " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                                        HistoryPayWalletController.Insert(uid, username, o.ID, moneyleft, username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username);

                                        MainOrderController.UpdateDeposit(id, uid, payalll.ToString());
                                        if (o.PayAllDate == null)
                                            MainOrderController.UpdatePayAllDate(id, uid, currentDate);
                                        PayOrderHistoryController.Insert(id, uid, 9, moneyleft, 2, currentDate, username);

                                        MainOrderRequestShipController.UpdateMainOrderStatusByMainOrderID(id, 9);


                                        Page.Response.Redirect(Page.Request.Url.ToString(), true);

                                        transaction.Commit();
                                    }
                                    catch (Exception exception)
                                    {
                                        transaction.Rollback();
                                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ltr_info.Text = "Số tiền trong tài khoản của bạn không đủ để thanh toán đơn hàng.";
                            ltr_info.ForeColor = System.Drawing.Color.Red;
                            ltr_info.Visible = true;
                        }
                    }
                }
            }

        }
        protected void btnPostComment_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                //var id = RouteData.Values["id"].ToString().ToInt(0);
                int id = hdfShopID.Value.ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        int salerID = Convert.ToInt32(o.SalerID);
                        int dathangID = Convert.ToInt32(o.DathangID);
                        int khotqID = Convert.ToInt32(o.KhoTQID);
                        int khovnID = Convert.ToInt32(o.KhoVNID);

                        if (salerID > 0)
                        {
                            var saler = AccountController.GetByID(salerID);
                            if (saler != null)
                            {
                                NotificationsController.Inser(salerID,
                                    saler.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", id,
                                    currentDate, obj_user.Username, false);
                            }
                        }
                        if (dathangID > 0)
                        {
                            var dathang = AccountController.GetByID(dathangID);
                            if (dathang != null)
                            {
                                NotificationsController.Inser(dathangID,
                                    dathang.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", id,
                                    currentDate, obj_user.Username, false);
                            }
                        }
                        if (khotqID > 0)
                        {
                            var khotq = AccountController.GetByID(khotqID);
                            if (khotq != null)
                            {
                                NotificationsController.Inser(khotqID,
                                    khotq.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", id,
                                    currentDate, obj_user.Username, false);
                            }
                        }
                        if (khovnID > 0)
                        {
                            var khovn = AccountController.GetByID(khovnID);
                            if (khovn != null)
                            {
                                NotificationsController.Inser(khovnID,
                                    khovn.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", id,
                                    currentDate, obj_user.Username, false);
                            }
                        }

                        var admins = AccountController.GetAllByRoleID(0);
                        if (admins.Count > 0)
                        {
                            foreach (var admin in admins)
                            {
                                NotificationsController.Inser(admin.ID,
                                                                   admin.Username, id,
                                                                   "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", id,
                                                                   currentDate, obj_user.Username, false);
                            }
                        }

                        var managers = AccountController.GetAllByRoleID(2);
                        if (managers.Count > 0)
                        {
                            foreach (var manager in managers)
                            {
                                NotificationsController.Inser(manager.ID,
                                                                   manager.Username, id,
                                                                   "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", id,
                                                                   currentDate, obj_user.Username, false);
                            }
                        }


                        string comment = hdfCommentText.Value;
                        string kq = OrderCommentController.Insert(id, comment, true, 1, currentDate, uid, 1);
                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                        hubContext.Clients.All.addNewMessageToPage("", "");
                        PJUtils.ShowMessageBoxSwAlert("Gửi nội dung thành công", "s", true, Page);
                    }
                }
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (u != null)
            {
                int UID = u.ID;
                int ID = ViewState["OID"].ToString().ToInt(0);
                string orderCodeshop = Request.QueryString["ordershopcode"];
                var s = MainOrderController.GetAllByUIDAndID(UID, ID);
                if (s != null)
                {
                    //MainOrderController.UpdateNote(s.ID, txt_DNote.Text);
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật ghi chú thành công", "s", true, Page);
                }
            }
        }

        //[WebMethod]
        //public static string PostComment(string commentext, string shopid, string img)
        //{
        //    string username = HttpContext.Current.Session["userLoginSystem"].ToString();
        //    var obj_user = AccountController.GetByUsername(username);
        //    DateTime currentDate = DateTime.Now;
        //    if (obj_user != null)
        //    {

        //        int uid = obj_user.ID;
        //        //var id = RouteData.Values["id"].ToString().ToInt(0);
        //        int id = shopid.ToInt(0);
        //        if (id > 0)
        //        {
        //            var o = MainOrderController.GetAllByUIDAndID(uid, id);
        //            if (o != null)
        //            {


        //                string message = "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem";
        //                int salerID = Convert.ToInt32(o.SalerID);
        //                int dathangID = Convert.ToInt32(o.DathangID);
        //                int khotqID = Convert.ToInt32(o.KhoTQID);
        //                int khovnID = Convert.ToInt32(o.KhoVNID);

        //                if (salerID > 0)
        //                {
        //                    var saler = AccountController.GetByID(salerID);
        //                    if (saler != null)
        //                    {
        //                        NotificationController.Inser(obj_user.ID, obj_user.Username, salerID,
        //                            saler.Username, id,
        //                            message, 0, 1,
        //                            currentDate, obj_user.Username, false);
        //                    }
        //                }
        //                if (dathangID > 0)
        //                {
        //                    var dathang = AccountController.GetByID(dathangID);
        //                    if (dathang != null)
        //                    {
        //                        NotificationController.Inser(obj_user.ID, obj_user.Username, dathangID,
        //                            dathang.Username, id,
        //                            message, 0, 1,
        //                            currentDate, obj_user.Username, false);
        //                    }
        //                }
        //                if (khotqID > 0)
        //                {
        //                    var khotq = AccountController.GetByID(khotqID);
        //                    if (khotq != null)
        //                    {
        //                        NotificationController.Inser(obj_user.ID, obj_user.Username, khotqID,
        //                            khotq.Username, id,
        //                            message, 0, 1,
        //                            currentDate, obj_user.Username, false);
        //                    }
        //                }
        //                if (khovnID > 0)
        //                {
        //                    var khovn = AccountController.GetByID(khovnID);
        //                    if (khovn != null)
        //                    {
        //                        NotificationController.Inser(obj_user.ID, obj_user.Username, khovnID,
        //                            khovn.Username, id,
        //                            message, 0, 1,
        //                            currentDate, obj_user.Username, false);
        //                    }
        //                }

        //                var admins = AccountController.GetAllByRoleID(0);
        //                if (admins.Count > 0)
        //                {
        //                    foreach (var admin in admins)
        //                    {
        //                        NotificationController.Inser(obj_user.ID, obj_user.Username, admin.ID,
        //                                                           admin.Username, id,
        //                                                           message, 0, 1,
        //                                                           currentDate, obj_user.Username, false);
        //                    }
        //                }

        //                var managers = AccountController.GetAllByRoleID(2);
        //                if (managers.Count > 0)
        //                {
        //                    foreach (var manager in managers)
        //                    {
        //                        NotificationController.Inser(obj_user.ID, obj_user.Username, manager.ID,
        //                                                           manager.Username, id,
        //                                                           message, 0, 1,
        //                                                           currentDate, obj_user.Username, false);
        //                    }
        //                }
        //                string link = "";
        //                if (!string.IsNullOrEmpty(img))
        //                {
        //                    string[] listIMG = img.Split('|');
        //                    for (int i = 0; i < listIMG.Length - 1; i++)
        //                    {
        //                        string imageData = listIMG[i];
        //                        string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/comment/");
        //                        string date = DateTime.Now.ToString("dd-MM-yyyy");
        //                        string time = DateTime.Now.ToString("hh:mm tt");
        //                        Page page = (Page)HttpContext.Current.Handler;
        //                        //  TextBox txtCampaign = (TextBox)page.FindControl("txtCampaign");
        //                        string k = i.ToString();
        //                        string fileNameWitPath = path + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
        //                        string linkIMG = "/Uploads/comment/" + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
        //                        link = linkIMG;
        //                        //   string fileNameWitPath = path + s + ".png";
        //                        byte[] data;
        //                        string convert;
        //                        using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
        //                        {
        //                            using (BinaryWriter bw = new BinaryWriter(fs))
        //                            {
        //                                if (imageData.Contains("data:image/png"))
        //                                {
        //                                    convert = imageData.Replace("data:image/png;base64,", String.Empty);
        //                                    data = Convert.FromBase64String(convert);
        //                                    bw.Write(data);
        //                                }
        //                                else if (imageData.Contains("data:image/jpeg"))
        //                                {
        //                                    convert = imageData.Replace("data:image/jpeg;base64,", String.Empty);
        //                                    data = Convert.FromBase64String(convert);
        //                                    bw.Write(data);
        //                                }
        //                                else if (imageData.Contains("data:image/gif"))
        //                                {
        //                                    convert = imageData.Replace("data:image/gif;base64,", String.Empty);
        //                                    data = Convert.FromBase64String(convert);
        //                                    bw.Write(data);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                //string kq = OrderCommentController.Insert(id, commentext, true, 1, 
        //                //    currentDate, uid);
        //                //string kq = OrderCommentController.InsertWithIMG(id, commentext, true, 1,
        //                //    currentDate, uid, link);
        //                //if (kq.ToInt(0) > 0)
        //                //{
        //                //    return kq + "|" + message;
        //                //}
        //                //else
        //                //    return "0";

        //            }
        //            else
        //                return "0";
        //        }
        //        else
        //            return "0";
        //    }
        //    else return "0";
        //}

        [WebMethod]
        public static string PostComment(string commentext, string shopid, string urlIMG, string real)
        {
            var listLink = urlIMG.Split('|').ToList();
            if (listLink.Count > 0)
            {
                listLink.RemoveAt(listLink.Count - 1);
            }
            var listComment = real.Split('|').ToList();
            if (listComment.Count > 0)
            {
                listComment.RemoveAt(listComment.Count - 1);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                //var id = RouteData.Values["id"].ToString().ToInt(0);
                int id = shopid.ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        string message = "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem";
                        int salerID = Convert.ToInt32(o.SalerID);
                        int dathangID = Convert.ToInt32(o.DathangID);
                        int khotqID = Convert.ToInt32(o.KhoTQID);
                        int khovnID = Convert.ToInt32(o.KhoVNID);
                        var setNoti = SendNotiEmailController.GetByID(12);

                        if (setNoti != null)
                        {
                            if (setNoti.IsSentNotiAdmin == true)
                            {
                                if (salerID > 0)
                                {
                                    var saler = AccountController.GetByID(salerID);
                                    if (saler != null)
                                    {
                                        NotificationsController.Inser(salerID,
                                            saler.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                    }
                                }
                                if (dathangID > 0)
                                {
                                    var dathang = AccountController.GetByID(dathangID);
                                    if (dathang != null)
                                    {
                                        NotificationsController.Inser(dathangID,
                                            dathang.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                    }
                                }
                                if (khotqID > 0)
                                {
                                    var khotq = AccountController.GetByID(khotqID);
                                    if (khotq != null)
                                    {
                                        NotificationsController.Inser(khotqID,
                                            khotq.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                    }
                                }
                                if (khovnID > 0)
                                {
                                    var khovn = AccountController.GetByID(khovnID);
                                    if (khovn != null)
                                    {
                                        NotificationsController.Inser(khovnID,
                                            khovn.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                    }
                                }

                                var admins = AccountController.GetAllByRoleID(0);
                                if (admins.Count > 0)
                                {
                                    foreach (var admin in admins)
                                    {
                                        NotificationsController.Inser(admin.ID,
                                                                           admin.Username, id,
                                                                           message, 1,
                                                                           currentDate, obj_user.Username, false);
                                    }
                                }

                                var managers = AccountController.GetAllByRoleID(2);
                                if (managers.Count > 0)
                                {
                                    foreach (var manager in managers)
                                    {
                                        NotificationsController.Inser(manager.ID,
                                                                           manager.Username, id,
                                                                           message, 1,
                                                                           currentDate, obj_user.Username, false);
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < listLink.Count; i++)
                        {
                            string kqq = OrderCommentController.InsertNew(id, listLink[i], listComment[i], true, 1, DateTime.Now, uid, 1);
                        }
                        if (!string.IsNullOrEmpty(commentext))
                        {
                            string kq = OrderCommentController.Insert(id, commentext, true, 1, currentDate, uid, 1);
                            ChatHub ch = new ChatHub();
                            ch.SendMessenger(uid, id, commentext, listLink, listComment);
                            CustomerComment dataout = new CustomerComment();
                            dataout.UID = uid;
                            dataout.OrderID = id;
                            StringBuilder showIMG = new StringBuilder();
                            if (!string.IsNullOrEmpty(commentext))
                            {
                                showIMG.Append("   <span class=\"user-comment \">" + AccountInfoController.GetByUserID(uid).FirstName + " " + AccountInfoController.GetByUserID(uid).LastName + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", currentDate) + "]</b> : <span class=\"green\">" + commentext + "</span><br>");
                            }
                            for (int i = 0; i < listLink.Count; i++)
                            {                              
                                if (!string.IsNullOrEmpty(listLink[i]))
                                {
                                    showIMG.Append("       <span class=\"user-comment\">" + AccountInfoController.GetByUserID(uid).FirstName + " " + AccountInfoController.GetByUserID(uid).LastName + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", currentDate) + "]</b> :<br/> <a href=\"" + listLink[i] + "\" target=\"_blank\"><img src=\"" + listLink[i] + "\"/></a><br>");
                                }
                            }


                       
                            dataout.Comment = showIMG.ToString();
                            return serializer.Serialize(dataout);
                        }
                        else
                        {
                            if (listComment.Count > 0)
                            {
                                ChatHub ch = new ChatHub();
                                ch.SendMessenger(uid, id, commentext, listLink, listComment);
                                CustomerComment dataout = new CustomerComment();
                                StringBuilder showIMG = new StringBuilder();                             
                                for (int i = 0; i < listLink.Count; i++)
                                {                                   
                                    if (!string.IsNullOrEmpty(listLink[i]))
                                    {
                                        showIMG.Append("       <span class=\"user-comment\">" + AccountInfoController.GetByUserID(uid).FirstName + " " + AccountInfoController.GetByUserID(uid).LastName + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", currentDate) + "]</b> :<br/> <a href=\"" + listLink[i] + "\" target=\"_blank\"><img src=\"" + listLink[i] + "\"/></a><br>");
                                    }
                                }
                                dataout.UID = uid;
                                dataout.OrderID = id;
                                dataout.Comment = showIMG.ToString();
                                return serializer.Serialize(dataout);
                            }
                        }

                    }

                }

            }
            return serializer.Serialize(null);
        }
        public partial class CustomerComment
        {
            public int UID { get; set; }
            public int OrderID { get; set; }
            public string Comment { get; set; }
            public List<string> Link { get; set; }
            public List<string> CommentName { get; set; }
        }

        [WebMethod]
        public static string sendrequest(int ID, string FullName, string Email, string Phone, string Note, string Address)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                int statusM = 0;
                var mainorder = MainOrderController.GetAllByID(ID);
                if (mainorder != null)
                {
                    statusM = Convert.ToInt32(mainorder.Status);
                    if (mainorder.UID == UID)
                    {
                        //MainOrderRequestShipController.Insert(ID, UID, FullName, Email, Phone, Note,
                        //    Address, 1, statusM, currentDate, username);
                        return "ok";
                    }
                    else
                    {
                        return "saidonhang";
                    }
                }
                else
                {
                    return "khongtimthaydonhang";
                }
            }
            else
                return "notuser";
        }


    }
}