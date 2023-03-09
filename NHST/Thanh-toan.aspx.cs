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
using static NHST.WebService1;
using Newtonsoft.Json;

namespace NHST
{
    public partial class Thanh_toan1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] != null)
                {
                    if (Session["PayAllTempOrder"] == null)
                    {
                        Response.Redirect("/gio-hang");
                    }
                    else
                    {
                        List<string> orderId = (List<string>)Session["PayAllTempOrder"];
                        if (orderId != null)
                        {
                            if (orderId.Count > 0)
                            {
                                UpdateCheck(orderId);
                            }
                            else
                            {
                                Response.Redirect("/gio-hang");
                            }
                        }
                        else
                        {
                            Response.Redirect("/gio-hang");
                        }

                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                LoadReceiPlace();
            }
        }
        public void LoadReceiPlace()
        {
            var dt = WarehouseController.GetAllWithIsHidden(false);
            ddlPlace.Items.Clear();
            ddlPlace.Items.Insert(0, new ListItem("Chọn kho VN", "0"));
            if (dt.Count > 0)
            {
                foreach (var item in dt)
                {
                    ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlPlace.Items.Add(listitem);
                }
            }
            ddlPlace.DataBind();
        }
        public void UpdateCheck(List<string> orderId)
        {
            double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
            //Load User Info
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                if (orderId.Count > 0)
                {
                    for (int i = 0; i < orderId.Count; i++)
                    {
                        int ID = orderId[i].ToInt(0);
                        if (ID > 0)
                        {
                            var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                            if (shop != null)
                            {
                                if (shop.IsCheckProduct == true)
                                {
                                    double total = 0;
                                    var listpro = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                                    double counpros = 0;
                                    if (listpro.Count > 0)
                                    {
                                        foreach (var item in listpro)
                                        {
                                            counpros += item.quantity.ToInt(1);
                                            
                                        }
                                    }
                                    //var count = listpro.Count;
                                    if (counpros >= 1 && counpros <= 2)
                                    {
                                        total = total + (5000 * counpros);
                                    }
                                    else if (counpros > 2 && counpros <= 10)
                                    {
                                        total = total + (3500 * counpros);
                                    }
                                    else if (counpros > 10 && counpros <= 100)
                                    {
                                        total = total + (2000 * counpros);
                                    }
                                    else if (counpros > 100 && counpros <= 500)
                                    {
                                        total = total + (1500 * counpros);
                                    }
                                    else if (counpros > 500)
                                    {
                                        total = total + (1000 * counpros);
                                    }
                                    total = Math.Round(total, 0);
                                    OrderShopTempController.UpdateCheckProductPrice(shop.ID, total.ToString());
                                }
                            }
                        }
                    }
                }
            }
            LoadData(orderId);
        }
        public void LoadData(List<string> orderId)
        {
            try
            {
                Session.Remove("orderitem");
                double current = 0;

                //Load User Info
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var warehouses = WarehouseController.GetAllWithIsHidden(false);
                var obj_user = AccountController.GetByUsername(username);
                if (obj_user != null)
                {
                    double userCurrency = 0;
                    if (!string.IsNullOrEmpty(obj_user.Currency))
                    {
                        if (obj_user.Currency.ToFloat(0) > 0)
                        {
                            userCurrency = Convert.ToDouble(obj_user.Currency);
                        }
                    }
                    if (userCurrency > 0)
                    {
                        current = userCurrency;
                    }
                    else
                    {
                        current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                    }
                    int khoTQ = Convert.ToInt32(obj_user.WarehouseFrom);
                    int khoVN = Convert.ToInt32(obj_user.WarehouseTo);
                    var ui = AccountInfoController.GetByUserID(obj_user.ID);
                    double FeeBuyProduct = 0;
                    double UL_CKFeeBuyPro = 0;
                    double UL_CKFeeWeight = 0;
                    if (ui != null)
                    {
                        txt_Fullname.Text = ui.FirstName + " " + ui.LastName;
                        txt_DFullname.Text = ui.FirstName + " " + ui.LastName;
                        txt_Address.Text = ui.Address;
                        txt_DAddress.Text = ui.Address;
                        txt_Email.Text = ui.Email;
                        txt_DEmail.Text = ui.Email;
                        txt_Phone.Text = ui.MobilePhonePrefix + ui.MobilePhone;
                        txt_DPhone.Text = ui.MobilePhonePrefix + ui.MobilePhone;
                        UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                    }
                    if (orderId.Count > 0)
                    {
                        double totalfinal = 0;
                        for (int i = 0; i < orderId.Count; i++)
                        {
                            int ID = orderId[i].ToInt(0);
                            var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                            if (shop != null)
                            {
                                double fastprice = 0;
                                double pricepro = Convert.ToDouble(shop.PriceVND);
                                double priceproCYN = Convert.ToDouble(shop.PriceCNY);

                                //if (shop.IsFast == true)
                                //{
                                //    fastprice = (pricepro * 5 / 100);
                                //}

                                double total = fastprice + pricepro;

                                //double FeeCNShip = 10 * current;
                                double FeeCNShip = 0;
                                double FeeBuyPro = 0;
                                double FeeCheck = shop.IsCheckProductPrice.ToFloat(0);

                                double totalFee_CountFee = total + FeeCNShip + FeeCheck;
                                double servicefee = 0;
                                double serviceFeeMoney = 0;

                                bool getFeeFromUser = false;
                                if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                                {
                                    if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                                    {
                                        servicefee = Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                                        getFeeFromUser = true;
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
                                                    //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
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
                                                //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                                break;
                                            }
                                        }
                                    }
                                }
                                //double feebpnotdc = (pricepro * servicefee + serviceFeeMoney) * current;
                                double feebpnotdc = pricepro * servicefee;
                                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                double feebp = feebpnotdc - subfeebp;

                                #region tính theo tiền
                                //double feebp = totalFee_CountFee * UL_CKFeeBuyPro / 100;
                                //double feebuyproUser = 0;
                                //if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                                //{
                                //    if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                                //    {
                                //        feebuyproUser = Convert.ToDouble(obj_user.FeeBuyPro);
                                //    }
                                //    FeeBuyPro = feebuyproUser;
                                //    getFeeFromUser = true;
                                //}
                                //else
                                //{
                                //    FeeBuyPro = feebp;
                                //}
                                #endregion


                                if (feebp < 10000)
                                {
                                    feebp = 10000;
                                }
                                FeeBuyPro = feebp;
                                total = total + FeeCNShip + FeeBuyPro + FeeCheck;

                                totalfinal += total;
                                ltr_pro.Text += "<div class=\"order-detail\">";
                                ltr_pro.Text += "   <table class=\"ordershoptem\" data-id=\"" + shop.ID + "\">";
                                ltr_pro.Text += "       <tr class=\"borderbtm\">";
                                ltr_pro.Text += "           <td colspan=\"3\"><h4 class=\"title\">" + shop.ShopName + "</h4></td>";
                                ltr_pro.Text += "       </tr>";
                                var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);

                                if (proOrdertemp != null)
                                {
                                    if (proOrdertemp.Count > 0)
                                    {
                                        foreach (var item in proOrdertemp)
                                        {
                                            int quantity = Convert.ToInt32(item.quantity);
                                            double originprice = Convert.ToDouble(item.price_origin);
                                            double promotionprice = Convert.ToDouble(item.price_promotion);
                                            double u_pricecbuy = 0;
                                            double u_pricevn = 0;
                                            double e_pricebuy = 0;
                                            double e_pricevn = 0;
                                            double e_pricetemp = 0;
                                            double e_totalproduct = 0;
                                            if (promotionprice > 0)
                                            {
                                                if (promotionprice < originprice)
                                                {
                                                    u_pricecbuy = promotionprice;
                                                    u_pricevn = promotionprice * current;
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }
                                            }
                                            else
                                            {
                                                u_pricecbuy = originprice;
                                                u_pricevn = originprice * current;
                                            }


                                            e_pricebuy = u_pricecbuy * quantity;
                                            e_pricevn = u_pricevn * quantity;
                                            string image = item.image_origin;
                                            if (image.Contains("%2F"))
                                            {
                                                image = image.Replace("%2F", "/");
                                            }
                                            if (image.Contains("%3A"))
                                            {
                                                image = image.Replace("%3A", ":");
                                            }
                                            ltr_pro.Text += "       <tr class=\"borderbtm\">";
                                            ltr_pro.Text += "           <td colspan=\"2\">";
                                            ltr_pro.Text += "               <div class=\"thumb-product\">";
                                            ltr_pro.Text += "                   <div class=\"pd-img\">";
                                            ltr_pro.Text += "                       <img src=\"" + image + "\" alt=\"\"><span class=\"badge\">" + item.quantity + "</span>";
                                            ltr_pro.Text += "                   </div>";
                                            ltr_pro.Text += "                   <div class=\"info\"><a href=\"" + item.link_origin + "\">" + item.title_origin + "</a></div>";
                                            ltr_pro.Text += "               </div>";
                                            ltr_pro.Text += "           </td>";
                                            ltr_pro.Text += "           <td>";
                                            ltr_pro.Text += "               <strong>" + string.Format("{0:N0}", e_pricevn) + "vnđ</strong>";
                                            ltr_pro.Text += "           </td>";
                                            ltr_pro.Text += "       </tr>";

                                           

                                        }
                                    }
                                }

                                ltr_pro.Text += "       <tr>";
                                ltr_pro.Text += "           <td>Phí ship Trung Quốc</td>";
                                ltr_pro.Text += "           <td></td>";
                                //ltr_pro.Text += "           <td style=\"width:20%;\"><strong>" + string.Format("{0:N0}", FeeCNShip) + " vnđ</strong></td>";
                                ltr_pro.Text += "           <td style=\"width:20%;\"><strong>Chờ cập nhật</strong></td>";
                                ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "       <tr>";
                                if (UL_CKFeeBuyPro > 0)
                                    ltr_pro.Text += "           <td>Phí mua hàng (CK: " + UL_CKFeeBuyPro + "%)</td>";
                                else
                                    ltr_pro.Text += "           <td>Phí mua hàng</td>";
                                ltr_pro.Text += "           <td></td>";
                                ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", FeeBuyPro) + " vnđ</strong></td>";
                                ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "       <tr>";
                                ltr_pro.Text += "       <tr>";
                                if (UL_CKFeeWeight > 0)
                                    ltr_pro.Text += "           <td>Phí vận chuyển TQ - VN (CK: " + UL_CKFeeWeight + "%)</td>";
                                else
                                    ltr_pro.Text += "           <td>Phí vận chuyển TQ - VN</td>";
                                ltr_pro.Text += "           <td></td>";
                                ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                                ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "       <tr>";
                                ltr_pro.Text += "           <td>Phí kiểm đếm</td>";
                                ltr_pro.Text += "           <td></td>";
                                if (shop.IsCheckProduct == true)
                                    ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", FeeCheck) + "</strong></td>";
                                else
                                    ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                                ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "       <tr>";
                                ltr_pro.Text += "           <td>Phí đóng gỗ</td>";
                                ltr_pro.Text += "           <td></td>";
                                if (shop.IsPacked == true)
                                    ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                                else
                                    ltr_pro.Text += "           <td><strong>...</strong></td>";
                                ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "       <tr>";
                                ltr_pro.Text += "           <td>Phí ship giao hàng tận nhà</td>";
                                ltr_pro.Text += "           <td></td>";
                                if (shop.IsFastDelivery == true)
                                    ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                                else
                                    ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                                ltr_pro.Text += "       </tr>";
                                //ltr_pro.Text += "       <tr class=\"borderbtm\">";
                                //ltr_pro.Text += "           <td>Phí đơn hàng hỏa tốc</td>";
                                //ltr_pro.Text += "           <td></td>";
                                //if (shop.IsFast == true)
                                //    ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", fastprice) + "vnđ</strong></td>";
                                //else
                                //    ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                                //ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "       <tr>";
                                ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng tiền</strong></td>";
                                ltr_pro.Text += "           <td></td>";
                                ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", total) + "vnđ</strong></td>";
                                ltr_pro.Text += "       </tr>";

                                ltr_pro.Text += "       <tr style=\"display:none\">";
                                ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Chọn kho TQ</strong></td>";
                                ltr_pro.Text += "           <td colspan=\"2\">";
                                ltr_pro.Text += "               <select class=\"form-control warehosefromselect\">";
                                var warehouseTQ = WarehouseFromController.GetAllWithIsHidden(false);
                                if (warehouseTQ.Count > 0)
                                {
                                    foreach (var w in warehouseTQ)
                                    {
                                        if (khoTQ == w.ID)
                                            ltr_pro.Text += "<option value=\"" + w.ID + "\" selected>" + w.WareHouseName + "</option>";
                                        else
                                            ltr_pro.Text += "<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>";
                                    }
                                }
                                ltr_pro.Text += "                   ";
                                ltr_pro.Text += "               </select>";
                                ltr_pro.Text += "           </td>";
                                ltr_pro.Text += "       </tr>";
                                //ltr_pro.Text += "       <tr>";
                                //ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Chuyển về kho</strong></td>";
                                //ltr_pro.Text += "           <td colspan=\"2\">";
                                //ltr_pro.Text += "               <select class=\"form-control warehoseselect\">";
                                //if (warehouses.Count > 0)
                                //{
                                //    foreach (var w in warehouses)
                                //    {
                                //        if (khoVN == w.ID)
                                //            ltr_pro.Text += "<option value=\"" + w.ID + "\" selected>" + w.WareHouseName + "</option>";
                                //        else
                                //            ltr_pro.Text += "<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>";
                                //    }
                                //}
                                //ltr_pro.Text += "                   ";
                                //ltr_pro.Text += "               </select>";
                                //ltr_pro.Text += "           </td>";
                                //ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "       <tr style=\"display:none\">";
                                ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Phương thức vận chuyển</strong></td>";
                                ltr_pro.Text += "           <td colspan=\"2\">";
                                ltr_pro.Text += "               <select class=\"form-control shippingtypesselect\">";
                                var shippingType = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
                                if (shippingType.Count > 0)
                                {
                                    foreach (var item in shippingType)
                                    {
                                        ltr_pro.Text += "                   <option value=\"" + item.ID + "\">" + item.ShippingTypeName + "</option>";
                                    }
                                }
                                ltr_pro.Text += "               </select>";
                                ltr_pro.Text += "           </td>";
                                ltr_pro.Text += "       </tr>";
                                ltr_pro.Text += "   </table>";
                                ltr_pro.Text += "</div>";
                            }
                        }
                        ltr_pro.Text += "<div class=\"order-detail\">";
                        ltr_pro.Text += "   <table>";
                        ltr_pro.Text += "       <tr>";
                        ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng hóa đơn</strong></td>";
                        ltr_pro.Text += "           <td></td>";
                        ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", totalfinal) + "vnđ</strong></td>";
                        ltr_pro.Text += "       </tr>";
                        ltr_pro.Text += "   </table>";
                        ltr_pro.Text += "</div>";
                    }
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
            catch
            {
                Response.Redirect("/gio-hang");
            }

        }

        protected void btn_saveOrder_Click(object sender, EventArgs e)
        {
            if (chk_DK.Checked)
            {
                double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                if (Session["PayAllTempOrder"] != null)
                {
                    string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(username);
                    List<string> rq = (List<string>)Session["PayAllTempOrder"];
                    if (obj_user != null)
                    {
                        int salerID = obj_user.SaleID.ToString().ToInt(0);
                        int dathangID = obj_user.DathangID.ToString().ToInt(0);
                        int UID = obj_user.ID;
                        int receivePlace = Convert.ToInt32(ddlPlace.SelectedValue);
                        //double percent_User = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LevelPercent);
                        double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                        double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);
                        string wareship = hdfTeamWare.Value;
                        if (rq != null)
                        {
                            if (rq.Count > 0)
                            {
                                double totalfinal = 0;
                                for (int i = 0; i < rq.Count; i++)
                                {
                                    int ID = rq[i].ToInt(0);

                                    var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                                    if (shop != null)
                                    {
                                        int warehouseFromID = 0;
                                        int warehouseID = 0;
                                        int w_shippingType = 0;
                                        string[] w = wareship.Split('|');
                                        if (w.Length - 1 > 0)
                                        {
                                            for (int j = 0; j < w.Length - 1; j++)
                                            {
                                                int shoptempID = (w[j].Split(':')[0]).ToInt(0);
                                                string[] wsinfor = w[j].Split(':')[1].Split('-');
                                                int wareID = (wsinfor[0]).ToInt(1);
                                                int shippingtype = (wsinfor[1]).ToInt(1);
                                                if (ID == shoptempID)
                                                {
                                                    warehouseID = receivePlace;
                                                    //warehouseID = wareID;
                                                    //w_shippingType = shippingtype;
                                                    w_shippingType = 1;
                                                    warehouseFromID = (wsinfor[1]).ToInt(2);
                                                }
                                            }
                                        }


                                        double total = 0;
                                        double fastprice = 0;
                                        //double pricepro = Convert.ToDouble(shop.PriceVND);
                                        //double priceproCYN = Convert.ToDouble(shop.PriceCNY);
                                        double pricepro = 0;
                                        double priceproCYN = 0;

                                        //Lấy ra từng ordertemp trong shop
                                        var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                                        if (proOrdertemp.Count > 0)
                                        {
                                            foreach (var item in proOrdertemp)
                                            {
                                                int quantity = Convert.ToInt32(item.quantity);
                                                double originprice = Convert.ToDouble(item.price_origin);
                                                double promotionprice = Convert.ToDouble(item.price_promotion);

                                                double u_pricecbuy = 0;
                                                double u_pricevn = 0;
                                                double e_pricebuy = 0;
                                                double e_pricevn = 0;

                                                if (promotionprice > 0)
                                                {
                                                    if (promotionprice < originprice)
                                                    {
                                                        u_pricecbuy = promotionprice;
                                                        u_pricevn = promotionprice * current;
                                                    }
                                                    else
                                                    {
                                                        u_pricecbuy = originprice;
                                                        u_pricevn = originprice * current;
                                                    }
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }

                                                e_pricebuy = u_pricecbuy * quantity;
                                                e_pricevn = u_pricevn * quantity;

                                                pricepro += e_pricevn;
                                                priceproCYN += e_pricebuy;
                                                //pricecynallproduct += e_pricebuy;
                                            }
                                        }

                                        //double servicefee = 0;
                                        //var adminfeebuypro = FeeBuyProController.GetAll();
                                        //if (adminfeebuypro.Count > 0)
                                        //{
                                        //    foreach (var item in adminfeebuypro)
                                        //    {
                                        //        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                        //        {
                                        //            servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                        //        }
                                        //    }
                                        //}
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

                                        //double feebpnotdc = pricepro * servicefee;
                                        //double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                        //double feebp = 0;
                                        //feebp = feebpnotdc - subfeebp;
                                        double feecnship = 0;
                                        //feecnship = 10 * current;

                                        if (shop.IsFast == true)
                                        {
                                            fastprice = (pricepro * 5 / 100);
                                        }
                                        //total = fastprice + pricepro + feebp + feecnship;
                                        string ShopID = shop.ShopID;
                                        string ShopName = shop.ShopName;
                                        string Site = shop.Site;
                                        bool IsForward = Convert.ToBoolean(shop.IsForward);
                                        string IsForwardPrice = shop.IsForwardPrice;
                                        bool IsFastDelivery = Convert.ToBoolean(shop.IsFastDelivery);
                                        string IsFastDeliveryPrice = shop.IsFastDeliveryPrice;
                                        bool IsCheckProduct = Convert.ToBoolean(shop.IsCheckProduct);
                                        string IsCheckProductPrice = shop.IsCheckProductPrice;
                                        bool IsPacked = Convert.ToBoolean(shop.IsPacked);
                                        string IsPackedPrice = shop.IsPackedPrice;
                                        bool IsFast = Convert.ToBoolean(shop.IsFast);
                                        string IsFastPrice = fastprice.ToString();
                                        double pricecynallproduct = 0;

                                        double totalFee_CountFee = fastprice + pricepro + feecnship + shop.IsCheckProductPrice.ToFloat(0);
                                        double servicefee = 0;
                                        double servicefeeMoney = 0;

                                        bool getFeeFromUser = false;
                                        if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                                        {
                                            if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                                            {
                                                servicefee = Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                                                getFeeFromUser = true;
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
                                                            //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
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
                                                        //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                                        break;
                                                    }
                                                }
                                            }
                                        }

                                        //double feebpnotdc = (pricepro * servicefee + servicefeeMoney) * current;
                                        double feebpnotdc = pricepro * servicefee;
                                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                        double feebp = 0;

                                        //double feebuyproUser = 0;
                                        //bool getFeeFromUser = false;
                                        //if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                                        //{
                                        //    if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                                        //    {
                                        //        feebuyproUser = Convert.ToDouble(obj_user.FeeBuyPro);
                                        //    }
                                        //    feebp = feebuyproUser;
                                        //    getFeeFromUser = true;
                                        //}
                                        //else
                                        //{
                                        //    feebp = feebpnotdc - subfeebp;
                                        //}
                                        feebp = feebpnotdc - subfeebp;
                                        feebp = Math.Round(feebp, 0);

                                        //double feebp = totalFee_CountFee * UL_CKFeeBuyPro / 100;
                                        if (feebp < 10000)
                                        {
                                            feebp = 10000;
                                        }

                                        total = fastprice + pricepro + feebp + feecnship + shop.IsCheckProductPrice.ToFloat(0);

                                        string PriceVND = pricepro.ToString();
                                        string PriceCNY = priceproCYN.ToString();
                                        //string FeeShipCN = (10 * current).ToString();
                                        string FeeShipCN = feecnship.ToString();
                                        string FeeBuyPro = feebp.ToString();
                                        string FeeWeight = shop.FeeWeight;
                                        string Note = shop.Note;
                                        string FullName = txt_DFullname.Text.Trim();
                                        string Address = txt_DAddress.Text.Trim();
                                        string Email = txt_DEmail.Text.Trim();
                                        string Phone = txt_DPhone.Text.Trim();
                                        int Status = 0;
                                        string Deposit = "0";
                                        string CurrentCNYVN = current.ToString();
                                        string TotalPriceVND = total.ToString();
                                        string AmountDeposit = (total * LessDeposit / 100).ToString();
                                        DateTime CreatedDate = DateTime.Now;
                                        string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice, IsCheckProduct, IsCheckProductPrice,
                                            IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro, FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN,
                                            TotalPriceVND, salerID, dathangID, CreatedDate, UID, AmountDeposit, 1);
                                        int idkq = Convert.ToInt32(kq);
                                        if (idkq > 0)
                                        {
                                            foreach (var item in proOrdertemp)
                                            {
                                                int quantity = Convert.ToInt32(item.quantity);
                                                double originprice = Convert.ToDouble(item.price_origin);
                                                double promotionprice = Convert.ToDouble(item.price_promotion);
                                                double u_pricecbuy = 0;
                                                double u_pricevn = 0;
                                                double e_pricebuy = 0;
                                                double e_pricevn = 0;
                                                if (promotionprice > 0)
                                                {
                                                    if (promotionprice < originprice)
                                                    {
                                                        u_pricecbuy = promotionprice;
                                                        u_pricevn = promotionprice * current;
                                                    }
                                                    else
                                                    {
                                                        u_pricecbuy = originprice;
                                                        u_pricevn = originprice * current;
                                                    }
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }


                                                e_pricebuy = u_pricecbuy * quantity;
                                                e_pricevn = u_pricevn * quantity;

                                                pricecynallproduct += e_pricebuy;

                                                string image = item.image_origin;
                                                if (image.Contains("%2F"))
                                                {
                                                    image = image.Replace("%2F", "/");
                                                }
                                                if (image.Contains("%3A"))
                                                {
                                                    image = image.Replace("%3A", ":");
                                                }
                                                string ret = OrderController.Insert(UID, item.title_origin, item.title_translated, item.price_origin, item.price_promotion, item.property_translated,
                                                item.property, item.data_value, image, image, item.shop_id, item.shop_name, item.seller_id, item.wangwang, item.quantity,
                                                item.stock, item.location_sale, item.site, item.comment, item.item_id, item.link_origin, item.outer_id, item.error, item.weight, item.step, item.stepprice, item.brand,
                                                item.category_name, item.category_id, item.tool, item.version, Convert.ToBoolean(item.is_translate), Convert.ToBoolean(item.IsForward), "0",
                                                Convert.ToBoolean(item.IsFastDelivery), "0", Convert.ToBoolean(item.IsCheckProduct), "0", Convert.ToBoolean(item.IsPacked), "0", Convert.ToBoolean(item.IsFast),
                                                fastprice.ToString(), pricepro.ToString(), PriceCNY, item.Note, txt_DFullname.Text.Trim(), txt_DAddress.Text.Trim(), txt_DEmail.Text.Trim(),
                                                txt_DPhone.Text.Trim(), 0, "0", current.ToString(), total.ToString(), idkq, DateTime.Now, UID, item.Link);

                                                if (item.price_promotion.ToFloat(0) > 0)
                                                    OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_promotion);
                                                else
                                                    OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_origin);
                                            }
                                            MainOrderController.UpdateReceivePlace(idkq, UID, warehouseID.ToString(), w_shippingType);
                                            MainOrderController.UpdateFromPlace(idkq, UID, warehouseFromID, w_shippingType);
                                            var admins = AccountController.GetAllByRoleID(0);
                                            if (admins.Count > 0)
                                            {
                                                foreach (var admin in admins)
                                                {
                                                    NotificationController.Inser(UID, username, admin.ID,
                                                                                       admin.Username, idkq,
                                                                                       "Có đơn hàng mới ID là: " + idkq, 0,
                                                                                       1, CreatedDate, username, false);
                                                }
                                            }

                                            var managers = AccountController.GetAllByRoleID(2);
                                            if (managers.Count > 0)
                                            {
                                                foreach (var manager in managers)
                                                {
                                                    NotificationController.Inser(UID, username, manager.ID,
                                                                                       manager.Username, 0,
                                                                                       "Có đơn hàng mới ID là: " + idkq, 0,
                                                                                       1, CreatedDate, username, false);
                                                }
                                            }

                                            //var sale = AccountController.GetAllBySaleID(salerID);
                                            //if (sale.Count > 0)
                                            //{
                                            //    foreach (var manager in sale)
                                            //    {
                                            //        NotificationController.Inser(UID, username, manager.ID,
                                            //                                           manager.Username, 0,
                                            //                                           "Có đơn hàng mới ID là: " + idkq, 0,
                                            //                                           1, CreatedDate, username, false);
                                            //    }
                                            //}
                                        }
                                        double salepercent = 0;
                                        double salepercentaf3m = 0;
                                        double dathangpercent = 0;
                                        var config = ConfigurationController.GetByTop1();
                                        if (config != null)
                                        {
                                            salepercent = Convert.ToDouble(config.SalePercent);
                                            salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                                            dathangpercent = Convert.ToDouble(config.DathangPercent);
                                        }
                                        string salerName = "";
                                        string dathangName = "";

                                        if (salerID > 0)
                                        {
                                            var sale = AccountController.GetByID(salerID);
                                            if (sale != null)
                                            {
                                                salerName = sale.Username;
                                                var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                                int d = CreatedDate.Subtract(createdDate).Days;
                                                if (d > 90)
                                                {
                                                    double per = feebp * salepercentaf3m / 100;
                                                    StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                    CreatedDate, CreatedDate, username);
                                                }
                                                else
                                                {
                                                    double per = feebp * salepercent / 100;
                                                    StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                    CreatedDate, CreatedDate, username);
                                                }
                                            }
                                        }
                                        if (dathangID > 0)
                                        {
                                            var dathang = AccountController.GetByID(dathangID);
                                            if (dathang != null)
                                            {
                                                dathangName = dathang.Username;
                                                StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                                                    CreatedDate, CreatedDate, username);
                                                NotificationController.Inser(UID, username, dathang.ID,
                                                                               dathang.Username, idkq,
                                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                                                                               1, CreatedDate, username, false);
                                            }
                                        }
                                        //Xóa Shop temp và order temp
                                        OrderShopTempController.Delete(shop.ID);
                                    }
                                }
                                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                hubContext.Clients.All.addNewMessageToPage("", "");
                                Session.Remove("PayAllTempOrder");
                                Response.Redirect("/danh-sach-don-hang?t=1");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }

                }
                else
                {
                    Response.Redirect("/gio-hang");
                }

            }
            else
            {
                lblCheckckd.Visible = true;
            }
        }
    }
}