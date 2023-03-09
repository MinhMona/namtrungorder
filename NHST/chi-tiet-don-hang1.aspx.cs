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

namespace NHST
{
    public partial class chi_tiet_don_hang : System.Web.UI.Page
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

                //if (obj_user.RoleID == 0)
                //    ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" />";
                //else
                //    ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/user-icon.png\" width=\"100%\" />";
                int uid = obj_user.ID;

                double UL_CKFeeBuyPro = 0;
                double UL_CKFeeWeight = 0;

                UL_CKFeeBuyPro = UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro.ToString().ToFloat(0);
                UL_CKFeeWeight = UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight.ToString().ToFloat(0);

                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        var config = ConfigurationController.GetByTop1();
                        double currency = 0;
                        double currency1 = 0;
                        if (config != null)
                        {
                            double currencyconfig = 0;
                            if (!string.IsNullOrEmpty(config.Currency))
                                currencyconfig = Convert.ToDouble(config.Currency);
                            currency = Math.Floor(currencyconfig);
                            currency1 = Math.Floor(currencyconfig);
                        }
                        ViewState["OID"] = id;
                        if (o.IsHidden == true)
                        {
                            Response.Redirect("/danh-sach-don-hang");
                        }
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
                        var adminfeebuypro = FeeBuyProController.GetAll();
                        if (adminfeebuypro.Count > 0)
                        {
                            foreach (var item in adminfeebuypro)
                            {
                                if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                {
                                    servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                }
                            }
                        }
                        ltrshopinfo.Text = "<span class=\"shop-info\">" + o.ShopName + " - Mã đơn hàng: " + o.ID + " </span>";
                        //if (pricepro < 1000000)
                        //{
                        //    servicefee = 0.05;
                        //}
                        //else if (pricepro >= 1000000 && pricepro < 30000000)
                        //{
                        //    servicefee = 0.04;
                        //}
                        //else if (pricepro >= 30000000 && pricepro < 50000000)
                        //{
                        //    servicefee = 0.035;
                        //}
                        //else if (pricepro >= 50000000 && pricepro < 100000000)
                        //{
                        //    servicefee = 0.03;
                        //}
                        //else if (pricepro >= 100000000 && pricepro < 200000000)
                        //{
                        //    servicefee = 0.025;
                        //}
                        //else if (pricepro >= 200000000)
                        //{
                        //    servicefee = 0.02;
                        //}
                        double feebpnotdc = pricepro * servicefee;
                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        double userdadeposit = 0;
                        if (o.Deposit != null)
                            userdadeposit = Convert.ToDouble(o.Deposit);
                        //Kiểm tra đơn hàng hiển thị button hủy đơn hàng
                        if (o.Status == 0)
                        {
                            pn.Visible = true;
                        }
                        else if (o.Status == 1)
                        {
                            //pn_sendcomment.Visible = false;
                        }
                        else if (o.Status == 7)
                        {
                            if (obj_user.Wallet >= (Convert.ToDouble(o.TotalPriceVND) - userdadeposit))
                            {
                                pnthanhtoan.Visible = true;
                            }
                        }
                        #region Lấy thông tin đơn hàng và thông tin người đặt                       
                        ltrOrderFee.Text += "<div class=\"order-panel\">";
                        ltrOrderFee.Text += " <div class=\"title\">Thông tin đơn hàng</div>";
                        ltrOrderFee.Text += " <div class=\"cont\">";
                        ltrOrderFee.Text += "     <dl>";
                        ltrOrderFee.Text += "         <dt class=\"full-width\"><strong class=\"title-fee\">Phí cố định</strong></dt>";
                        ltrOrderFee.Text += "         <dt>Trạng thái đơn hàng</dt><dd>" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + "</dd>";
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
                                    ltrOrderFee.Text += "         <dt>Phí mua hàng (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " vnđ)</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                                else
                                    ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                            }
                            else
                                ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>Đang cập nhật</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>Đang cập nhật</dd>";
                        ltrOrderFee.Text += "         <dt>Tổng cân nặng</dt><dd>" + o.OrderWeight.ToFloat(0) + " KG</dd>";
                        if (UL_CKFeeWeight > 0)
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN (Đã CK " + UL_CKFeeWeight + "% : " + string.Format("{0:N0}", o.FeeWeightCK.ToFloat(0)) + " vnđ)</dt><dd>" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN</dt><dd>" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " vnđ</dd>";
                        //if (!string.IsNullOrEmpty(o.FeeWeight))
                        //{
                        //    double fw = Convert.ToDouble(o.FeeWeight);
                        //    if (fw > 0)
                        //        ltrOrderFee.Text += "         <dt>Phí cân nặng</dt><dd>" + string.Format("{0:N0}", fw) + " vnđ</dd>";
                        //    else
                        //        ltrOrderFee.Text += "         <dt>Phí cân nặng</dt><dd>Đang cập nhật</dd>";
                        //}
                        //else
                        //    ltrOrderFee.Text += "         <dt>Phí cân nặng</dt><dd>Đang cập nhật</dd>";
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
                        //if (obj_user.Wallet > 0 && o.Status == 0)
                        //{
                        //    ltrOrderFee.Text += "         <dt><a class=\"btn pill-btn primary-btn\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a></dt><dd></dd>";
                        //}


                        if (!string.IsNullOrEmpty(o.AmountDeposit))
                            ltrOrderFee.Text += "         <dt>Số tiền phải đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit)) + " vnđ</dd>";
                        if (!string.IsNullOrEmpty(o.Deposit))
                            ltrOrderFee.Text += "         <dt>Tiền đã đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.Deposit)) + " vnđ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Tiền đã đặt cọc</dt><dd>0 vnđ</dd>";

                        ltrOrderFee.Text += "             <dt>Số tiền còn lại phải đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit) - Convert.ToDouble(o.Deposit)) + " vnđ</dd>";



                        if (o.Status == 0 && Convert.ToDouble(o.Deposit) < Convert.ToDouble(o.AmountDeposit) && Convert.ToDouble(o.TotalPriceVND) > 0)
                        {
                            //ltrOrderFee.Text += "         <dt><a class=\"btn pill-btn primary-btn\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a></dt><dd></dd>";
                            ltrbtndeposit.Text += "         <a class=\"btn pill-btn primary-btn\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a>";
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
                                    ltrOrderFee.Text += "         <dt>Phí mua hàng (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " vnđ)</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                                else
                                    ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                                //ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>" + string.Format("{0:N0}", bp) + " vnđ</dd>";
                            }
                            else
                                ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>Đang cập nhật</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>Đang cập nhật</dd>";
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
                            ltrProducts.Text += "<table class=\"tbl-subtotal full-width  mar-top2 mar-bot2\">";
                            ltrProducts.Text += "     <tbody>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Trạng thái đơn hàng:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + " </td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Tiền hàng:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " vnđ (¥ " + o.PriceCNY + ")</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";

                            if (!string.IsNullOrEmpty(o.FeeBuyPro))
                            {
                                double bp = Convert.ToDouble(o.FeeBuyPro);
                                if (bp > 0)
                                {
                                    if (UL_CKFeeBuyPro > 0)
                                    {
                                        ltrProducts.Text += "             <td class=\"float-left\">Phí mua hàng (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " vnđ):</td>";
                                        ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", bp) + " VNĐ</td>";
                                    }
                                    else
                                    {
                                        ltrProducts.Text += "             <td class=\"float-left\">Phí mua hàng :</td>";
                                        ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", bp) + " VNĐ</td>";
                                    }
                                }
                                else
                                {
                                    ltrProducts.Text += "             <td class=\"float-left\">Phí mua hàng :</td>";
                                    ltrProducts.Text += "             <td class=\"float-right\">Đang cập nhật</td>";
                                }
                            }
                            else
                            {
                                ltrProducts.Text += "             <td class=\"float-left\">Phí mua hàng :</td>";
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
                            ltrProducts.Text += "             <td class=\"float-left\">Tổng cộng:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND)) + " VNĐ</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts.Text += "             <td class=\"float-left\">Số tiền cần đặt cọc:</td>";
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit)) + " VNĐ</td>";
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
                            ltrProducts.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND) - deposit) + " VNĐ</td>";
                            ltrProducts.Text += "         </tr>";
                            ltrProducts.Text += "     </tbody>";
                            ltrProducts.Text += "</table>";
                            ltrProducts.Text += " </div>";

                        }
                        #endregion
                        #region Lấy bình luận
                        ltrComment.Text += "<div class=\"comment mar-bot2\">";
                        ltrComment.Text += "     <div class=\"comment_content\" seller=\"" + o.ShopID + "\" order=\"" + o.ID + "\" >";
                        var shopcomments = OrderCommentController.GetByOrderIDAndType(o.ID, 1, 1);
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
                                }
                                else
                                {
                                    ltrComment.Text += "         <span class=\"user-comment green\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> : <span class=\"green\">" + item.Comment + "</span><br>";

                                }
                            }
                        }
                        else
                        {
                            ltrComment.Text += "         <span class=\"user-comment\">Chưa có ghi chú.</span>";
                        }
                        ltrComment.Text += "     </div>";
                        ltrComment.Text += "     <div class=\"comment_action\" style=\"padding-bottom: 4px; padding-top: 4px;\">";
                        ltrComment.Text += "         <input shop_code=\"" + o.ID + "\" type=\"text\" class=\"comment-text\" order=\"188083\" seller=\"" + o.ShopID + "\" placeholder=\"Nội dung\">";
                        ltrComment.Text += "         <a id=\"sendnotecomment\" onclick=\"postcomment($(this))\" order=\"" + o.ID + "\" class=\"btn pill-btn primary-btn\" href=\"javascript:;\">Gửi</a>";
                        ltrComment.Text += "     </div>";
                        ltrComment.Text += "</div>";

                        //var cs = OrderCommentController.GetByOrderIDAndType(o.ID, 1);
                        //if (cs != null)
                        //{
                        //    if (cs.Count > 0)
                        //    {
                        //        foreach (var item in cs)
                        //        {
                        //            string fullname = "";
                        //            int role = 0;
                        //            var user = AccountController.GetByID(Convert.ToInt32(item.CreatedBy));
                        //            if (user != null)
                        //            {
                        //                role = Convert.ToInt32(user.RoleID);
                        //                var userinfo = AccountInfoController.GetByUserID(user.ID);
                        //                if (userinfo != null)
                        //                {
                        //                    fullname = userinfo.FirstName + " " + userinfo.LastName;
                        //                }
                        //            }
                        //            ltr_comment.Text += "<li class=\"item\">";
                        //            ltr_comment.Text += "   <div class=\"item-left\">";
                        //            if (role == 0)
                        //            {
                        //                ltr_comment.Text += "       <span class=\"avata circle\"><img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" /></span>";
                        //            }
                        //            else
                        //            {
                        //                ltr_comment.Text += "       <span class=\"avata circle\"><img src=\"/App_Themes/NHST/images/user-icon.png\" width=\"100%\" /></span>";
                        //            }
                        //            ltr_comment.Text += "   </div>";
                        //            ltr_comment.Text += "   <div class=\"item-right\">";
                        //            ltr_comment.Text += "       <strong class=\"item-username\">" + fullname + "</strong>";
                        //            ltr_comment.Text += "       <span class=\"item-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</span>";
                        //            ltr_comment.Text += "       <p class=\"item-comment\">";
                        //            ltr_comment.Text += item.Comment;
                        //            ltr_comment.Text += "       </p>";
                        //            ltr_comment.Text += "   </div>";
                        //            ltr_comment.Text += "</li>";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        ltr_comment.Text += "Hiện chưa có đánh giá nào.";
                        //    }
                        //}
                        //else
                        //{
                        //    ltr_comment.Text += "Hiện chưa có đánh giá nào.";
                        //}
                        #endregion
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
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

                            double orderdeposited = o.Deposit.ToFloat(0);
                            double amountdeposit = Convert.ToDouble(o.AmountDeposit);
                            double userwallet = Convert.ToDouble(obj_user.Wallet);
                            if (userwallet > 0)
                            {
                                if (orderdeposited > 0)
                                {
                                    double depleft = amountdeposit - orderdeposited;
                                    if (userwallet >= depleft)
                                    {
                                        double wallet = userwallet - depleft;
                                        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                        //Cập nhật lại MainOrder                                
                                        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, depleft, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, depleft, 2, currentDate, obj_user.Username);
                                    }
                                    else
                                    {
                                        double walletleft = depleft - userwallet;
                                        AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                                        double newpay = orderdeposited + userwallet;
                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, newpay.ToString());
                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                                    }
                                }
                                else
                                {
                                    if (userwallet >= amountdeposit)
                                    {
                                        //Cập nhật lại Wallet User
                                        double wallet = userwallet - amountdeposit;
                                        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                        //Cập nhật lại MainOrder                                
                                        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);
                                    }
                                    else
                                    {
                                        double paid = amountdeposit - userwallet;
                                        // Cập nhật lại Wallet User
                                        AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                                        //Cập nhật lại MainOrder                            
                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, userwallet.ToString());
                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                                    }
                                }

                                Page.Response.Redirect(Page.Request.Url.ToString(), true);
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
                        double moneyleft = o.TotalPriceVND.ToFloat(0) - deposit;

                        if (wallet >= moneyleft)
                        {
                            double walletLeft = wallet - moneyleft;
                            double payalll = deposit + moneyleft;
                            MainOrderController.UpdateStatus(o.ID, uid, 9);
                            AccountController.updateWallet(uid, walletLeft, currentDate, username);

                            HistoryOrderChangeController.Insert(o.ID, uid, username, username +
                                        " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                            HistoryPayWalletController.Insert(uid, username, o.ID, moneyleft, username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username);
                            MainOrderController.UpdateDeposit(id, uid, payalll.ToString());
                            PayOrderHistoryController.Insert(id, uid, 9, payalll, 2, currentDate, username);
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
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

                        string comment = hdfCommentText.Value;
                        string kq = OrderCommentController.Insert(id, comment, true, 1, DateTime.Now, uid,1);
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
    }
}