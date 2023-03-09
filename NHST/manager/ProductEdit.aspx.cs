using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        protected readonly NHSTEntities _context = new NHSTEntities();
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
                    //string username_current = Session["userLoginSystem"].ToString();
                    //tbl_Account ac = AccountController.GetByUsername(username_current);
                    //if (ac.RoleID != 0 && ac.RoleID != 3)
                    //    Response.Redirect("/trang-chu");
                    Loaddata();
                }

            }

        }
        public void Loaddata()
        {
            int MainOrderID = 0;
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    var mainorder = MainOrderController.GetAllByID(o.MainOrderID.ToString().ToInt());

                    int khachhangID = Convert.ToInt32(mainorder.UID);
                    var khachhang = AccountController.GetByID(khachhangID);
                    double khachhangCurrency = 0;
                    if (khachhang != null)
                    {
                        if (!string.IsNullOrEmpty(khachhang.Currency))
                        {
                            if (khachhang.Currency.ToFloat(0) > 0)
                            {
                                khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                            }
                        }
                    }
                    var config = ConfigurationController.GetByTop1();
                    double currency = 0;
                    if (config != null)
                    {
                        hdfcurrent.Value = mainorder.CurrentCNYVN.ToString();
                        currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    }
                    if (khachhangCurrency > 0)
                    {
                        hdfcurrent.Value = khachhangCurrency.ToString();
                        currency = khachhangCurrency;
                    }
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 3 && ac.RoleID != 2 && ac.RoleID != 6)
                    {
                        Response.Redirect("/manager/OrderDetail.aspx?id=" + o.MainOrderID + "");
                    }

                    if (mainorder.Status > 0 && ac.RoleID == 6)
                    {
                        Response.Redirect("/manager/OrderDetail.aspx?id=" + o.MainOrderID + "");
                    }
                    //else
                    //{
                    //    if (ac.RoleID == 3)
                    //    {
                    //        if (mainorder.Status >= 5)
                    //            btncreateuser.Visible = false;
                    //        pProductPriceOriginal.Enabled = false;
                    //        pRealPrice.Enabled = true;
                    //    }
                    //}
                    lblBrandname.Text = o.brand;
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (!string.IsNullOrEmpty(o.price_promotion))
                        pricepromotion = Convert.ToDouble(o.price_promotion);
                    if (!string.IsNullOrEmpty(o.price_origin))
                        priceorigin = Convert.ToDouble(o.price_origin);

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
                    ViewState["productprice"] = price;
                    pProductPriceOriginal.Value = price;
                    if (!string.IsNullOrEmpty(o.quantity))
                        pQuanity.Value = Convert.ToDouble(o.quantity);
                    else pQuanity.Value = 0;
                    pRealPrice.Value = Convert.ToDouble(o.RealPrice);
                    txtproducbrand.Text = o.brand;
                    ltrback.Text += "<a href=\"/manager/OrderDetail.aspx?id=" + o.MainOrderID + "\" class=\"btn primary-btn\">Trở về</a>";
                    string productstatus = "";
                    if (!string.IsNullOrEmpty(o.ProductStatus.ToString()))
                        ddlStatus.SelectedValue = o.ProductStatus.ToString();
                    else
                        ddlStatus.SelectedValue = "1";
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {

            Repository<tbl_MainOder> tbMainOrder = new Repository<tbl_MainOder>(_context);

            Repository<tbl_Account> tbAccount = new Repository<tbl_Account>(_context);

            Repository<tbl_Order> tbOrder = new Repository<tbl_Order>(_context);
            Repository<tbl_HistoryOrderChange> tbOrderChange = new Repository<tbl_HistoryOrderChange>(_context);

            Repository<tbl_PayOrderHistory> tbPayHistory = new Repository<tbl_PayOrderHistory>(_context);

            Repository<tbl_HistoryPayWallet> tbPayWalletHistory = new Repository<tbl_HistoryPayWallet>(_context);
            Repository<tbl_Notifications> tbNotis = new Repository<tbl_Notifications>(_context);

            Repository<tbl_Notification> tbNoti = new Repository<tbl_Notification>(_context);

            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            int status = ddlStatus.SelectedValue.ToString().ToInt(1);


            using (var trans = _context.Database.BeginTransaction())
            {
                //Update lại giá sản phẩm
                var id = Request.QueryString["id"].ToInt(0);

                int MainOrderID = 0;
                try
                {
                    var listorder = new List<tbl_Order>();


                    if (id > 0)
                    {
                        var o = OrderController.GetAllByID(id);
                        if (o != null)
                        {
                            MainOrderID = Convert.ToInt32(o.MainOrderID);


                            var mainorder = MainOrderController.GetAllByID(MainOrderID);

                            if (mainorder.IsPaying == true && mainorder.Status >= 2 && obj_user.RoleID == 3)
                            {
                                trans.Rollback();
                                Response.Redirect("/manager/orderlist2?t=1");
                            }

                            listorder = OrderController.GetByMainOrderID(MainOrderID);

                            double pprice = Convert.ToDouble(ViewState["productprice"].ToString());
                            double price = 0;
                            double pricepromotion = 0;
                            double priceorigin = 0;
                            if (!string.IsNullOrEmpty(o.price_promotion))
                                pricepromotion = Convert.ToDouble(o.price_promotion);

                            if (!string.IsNullOrEmpty(o.price_origin))
                                priceorigin = Convert.ToDouble(o.price_origin);

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


                            double quantity = 0;
                            if (status == 2)
                            {
                                price = 0;
                                quantity = 0;
                                var od = MainOrderController.GetAllByID(MainOrderID);
                                if (od != null)
                                {
                                    int userdathangID = Convert.ToInt32(od.UID);
                                    var userdathang = AccountController.GetByID(userdathangID);
                                    if (userdathang != null)
                                    {
                                        //NotificationController.Inser(obj_user.ID, obj_user.Username, userdathang.ID, userdathang.Username, MainOrderID,
                                        //                       "Đơn hàng: " + MainOrderID + " có sản phẩm bị hết hàng.", 0,
                                        //                       1, DateTime.Now, obj_user.Username, false);

                                        var Notis = new tbl_Notifications();
                                        Notis.SenderID = obj_user.ID;
                                        Notis.SenderUsername = obj_user.Username;
                                        Notis.ReceivedID = userdathang.ID;
                                        Notis.ReceivedUsername = userdathang.Username;
                                        Notis.OrderID = MainOrderID;
                                        Notis.Message = "Đơn hàng: " + MainOrderID + " có sản phẩm bị hết hàng.";
                                        Notis.Status = 0;
                                        Notis.NotifType = 1;
                                        Notis.CreatedDate = currentDate;
                                        Notis.CreatedBy = obj_user.Username;
                                        Notis.PushNotiApp = false;
                                        tbNotis.insert(Notis);
                                    }
                                }
                                if (price.ToString() != pProductPriceOriginal.Value.ToString())
                                {
                                    //HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                    //                " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                    //                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);


                                    var historyChange = new tbl_HistoryOrderChange();
                                    historyChange.MainOrderID = MainOrderID;
                                    historyChange.UID = obj_user.ID;
                                    historyChange.Username = obj_user.Username;
                                    historyChange.HistoryContent = obj_user.Username + " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "";

                                    historyChange.Type = 1;
                                    historyChange.CreatedDate = currentDate;
                                    tbOrderChange.insert(historyChange);

                                }
                                if (o.quantity != quantity.ToString())
                                {
                                    //HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                    //                " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                    //                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);

                                    var historyChange = new tbl_HistoryOrderChange();
                                    historyChange.MainOrderID = MainOrderID;
                                    historyChange.UID = obj_user.ID;
                                    historyChange.Username = obj_user.Username;
                                    historyChange.HistoryContent = obj_user.Username +
                                                    " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + o.quantity + ", sang: "
                                                    + quantity + "";

                                    historyChange.Type = 1;
                                    historyChange.CreatedDate = currentDate;
                                    tbOrderChange.insert(historyChange);

                                }
                                //OrderController.UpdateQuantity(id, quantity.ToString());
                                //OrderController.UpdateProductStatus(id, status);
                                //OrderController.UpdatePricePriceReal(id, "0", "0");
                                //OrderController.UpdatePricePromotion(id, "0");

                                o.quantity = quantity.ToString();
                                o.ProductStatus = status;
                                o.price_origin = "0";
                                o.RealPrice = "0";
                                o.price_promotion = "0";

                            }
                            else
                            {
                                quantity = Convert.ToDouble(pQuanity.Value);
                                if (price.ToString() != pProductPriceOriginal.Value.ToString())
                                {
                                    //HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                    //                " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                    //                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);


                                    var historyChange = new tbl_HistoryOrderChange();
                                    historyChange.MainOrderID = MainOrderID;
                                    historyChange.UID = obj_user.ID;
                                    historyChange.Username = obj_user.Username;
                                    historyChange.HistoryContent = obj_user.Username +
                                                    " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                                    + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "";

                                    historyChange.Type = 1;
                                    historyChange.CreatedDate = currentDate;
                                    tbOrderChange.insert(historyChange);
                                }
                                if (o.quantity != pQuanity.Value.ToString())
                                {
                                    //HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                    //                " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                    //                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);

                                    var historyChange = new tbl_HistoryOrderChange();
                                    historyChange.MainOrderID = MainOrderID;
                                    historyChange.UID = obj_user.ID;
                                    historyChange.Username = obj_user.Username;
                                    historyChange.HistoryContent = obj_user.Username +
                                                    " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + o.quantity + ", sang: "
                                                    + pQuanity.Value.ToString() + "";

                                    historyChange.Type = 1;
                                    historyChange.CreatedDate = currentDate;
                                    tbOrderChange.insert(historyChange);
                                }
                                //OrderController.UpdateQuantity(id, quantity.ToString());
                                //OrderController.UpdateProductStatus(id, status);
                                //OrderController.UpdatePricePriceReal(id, pProductPriceOriginal.Value.ToString(), pRealPrice.Value.ToString());
                                //OrderController.UpdatePricePromotion(id, pProductPriceOriginal.Value.ToString());

                                o.quantity = quantity.ToString();
                                o.ProductStatus = status;
                                o.price_origin = pProductPriceOriginal.Value.ToString();
                                o.RealPrice = pRealPrice.Value.ToString();
                                o.price_promotion = pProductPriceOriginal.Value.ToString();

                            }
                            o.brand = txtproducbrand.Text.Trim();
                            //OrderController.UpdateBrand(id, txtproducbrand.Text.Trim());

                            //Update lại order;
                            tbOrder.update(o);

                            var itemRemove = listorder.Single(r => r.ID == id);
                            listorder.Remove(itemRemove);
                            listorder.Add(o);


                            //Update lại giá của đơn hàng, lấy từng sản phẩm thuộc đơn hàng để lấy giá xác định rồi tổng lại rồi cộng các phí



                            if (mainorder != null)
                            {
                                double current = Convert.ToDouble(mainorder.CurrentCNYVN);
                                int khachhangID = Convert.ToInt32(mainorder.UID);
                                var khachhang = AccountController.GetByID(khachhangID);
                                double khachhangCurrency = 0;
                                if (khachhang != null)
                                {
                                    if (!string.IsNullOrEmpty(khachhang.Currency))
                                    {
                                        if (khachhang.Currency.ToFloat(0) > 0)
                                        {
                                            khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                                        }
                                    }
                                }
                                if (khachhangCurrency > 0)
                                {
                                    current = khachhangCurrency;
                                }
                                if (listorder != null)
                                {
                                    if (listorder.Count > 0)
                                    {
                                        double pricevnd = 0;
                                        double pricecyn = 0;
                                        foreach (var item in listorder)
                                        {
                                            double originprice = Convert.ToDouble(item.price_origin);
                                            double promotionprice = Convert.ToDouble(item.price_promotion);
                                            double oprice = 0;
                                            if (promotionprice > 0)
                                            {
                                                if (promotionprice < originprice)
                                                {
                                                    pricecyn += promotionprice;
                                                    oprice = promotionprice * Convert.ToDouble(item.quantity) * current;
                                                }
                                                else
                                                {
                                                    pricecyn += originprice;
                                                    oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                                }
                                            }
                                            else
                                            {
                                                pricecyn += originprice;
                                                oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                            }
                                            //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * Convert.ToDouble(item.CurrentCNYVN) + Convert.ToDouble(item.PriceChange);

                                            //pricecyn += item.price_origin.ToFloat();
                                            //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * current;
                                            pricevnd += oprice;
                                        }
                                        /// MainOrderController.UpdatePriceNotFee(MainOrderID, pricevnd.ToString());
                                        ///MainOrderController.UpdatePriceCYN(MainOrderID, pricecyn.ToString());  -Công sửa
                                        double Deposit = Convert.ToDouble(mainorder.Deposit);
                                        double FeeShipCN = Convert.ToDouble(mainorder.FeeShipCN);
                                        double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                                        if (FeeBuyPro < 0)
                                            FeeBuyPro = 0;
                                        double FeeWeight = Convert.ToDouble(mainorder.FeeWeight);
                                        //double FeeShipCNToVN = Convert.ToDouble(mainorder.FeeShipCNToVN);

                                        double IsCheckProductPrice = 0;
                                        if (mainorder.IsCheckProduct == true)
                                        {
                                            double total = 0;
                                            double counpros = 0;
                                            if (listorder.Count > 0)
                                            {
                                                foreach (var item in listorder)
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
                                            IsCheckProductPrice = total;
                                        }
                                        else
                                            IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);

                                        double IsPackedPrice = 0;
                                        IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);

                                        double IsFastDeliveryPrice = 0;
                                        IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);


                                        double TotalPriceVND = FeeShipCN + FeeBuyPro
                                                                + FeeWeight + IsCheckProductPrice
                                                                + IsPackedPrice + IsFastDeliveryPrice
                                                                + Convert.ToDouble(mainorder.IsFastPrice) + pricevnd;
                                        double newdeposit = 0;


                                        #region phần chỉnh sửa giá
                                        double totalo = 0;
                                        var ui = AccountController.GetByID(mainorder.UID.ToString().ToInt());
                                        double UL_CKFeeBuyPro = 0;
                                        double UL_CKFeeWeight = 0;
                                        double LessDeposito = 0;
                                        if (ui != null)
                                        {
                                            UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeBuyPro);
                                            UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeWeight);
                                            LessDeposito = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).LessDeposit);
                                        }
                                        double fastprice = 0;
                                        double pricepro = pricevnd;
                                        double servicefee = 0;
                                        bool getFeeFromUser = false;
                                        if (!string.IsNullOrEmpty(ui.FeeBuyPro))
                                        {
                                            if (ui.FeeBuyPro.ToFloat(0) > 0)
                                            {
                                                servicefee = Convert.ToDouble(ui.FeeBuyPro) / 100;
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

                                        double feebpnotdc = pricepro * servicefee;
                                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                        //double feebp = feebpnotdc - subfeebp;
                                        //feebp = Math.Round(feebp, 0);
                                        double feebp = 0;
                                        //double feebuyproUser = 0;
                                        //if (!string.IsNullOrEmpty(ui.FeeBuyPro))
                                        //{
                                        //    if (ui.FeeBuyPro.ToFloat(0) > 0)
                                        //    {
                                        //        feebuyproUser = Convert.ToDouble(ui.FeeBuyPro);
                                        //    }
                                        //    feebp = feebuyproUser;
                                        //}
                                        //else
                                        //{
                                        //    feebp = feebpnotdc - subfeebp; ;
                                        //}
                                        feebp = feebpnotdc - subfeebp;
                                        feebp = Math.Round(feebp, 0);
                                        if (feebp < 10000)
                                            feebp = 10000;
                                        if (mainorder.IsFast == true)
                                        {
                                            fastprice = (pricepro * 5 / 100);
                                        }
                                        totalo = fastprice + pricepro;
                                        double FeeCNShip = FeeShipCN;
                                        double FeeBuyPros = feebp;
                                        double FeeCheck = IsCheckProductPrice;
                                        //totalo = totalo + FeeCNShip + FeeBuyPros + FeeCheck;
                                        totalo = fastprice + pricepro + FeeCNShip + FeeBuyPros + FeeCheck + FeeWeight + IsFastDeliveryPrice;
                                        double AmountDeposit = Math.Floor((totalo * LessDeposito) / 100);
                                        //cập nhật lại giá phải deposit của đơn hàng
                                        //  MainOrderController.UpdateAmountDeposit(MainOrderID, AmountDeposit.ToString()); -Công sửa

                                        //giá hỏa tốc, giá sản phẩm, phí mua sản phẩm, phí ship cn, phí kiểm tra hàng
                                        newdeposit = AmountDeposit;

                                        //nếu đã đặt cọc rồi thì trả phí lại cho người ta
                                        if (Deposit > 0)
                                        {
                                            if (Deposit > newdeposit)
                                            {
                                                double drefund = Deposit - newdeposit;
                                                double userwallet = 0;
                                                if (ui.Wallet.ToString() != null)
                                                {
                                                    userwallet = Convert.ToDouble(ui.Wallet.ToString());

                                                }


                                                double wallet = userwallet + drefund;

                                                //update ví khách hàng
                                                ui.Wallet = wallet;
                                                ui.ModifiedDate = currentDate;
                                                ui.ModifiedBy = obj_user.Username;
                                                tbAccount.update(ui);

                                                //  AccountController.updateWallet(ui.ID, wallet, currentDate, obj_user.Username);

                                                // PayOrderHistoryController.Insert(MainOrderID, obj_user.ID, 12, drefund, 2, currentDate, obj_user.Username);
                                                var PayOrderHistory = new tbl_PayOrderHistory();
                                                PayOrderHistory.MainOrderID = MainOrderID;
                                                PayOrderHistory.UID = obj_user.ID;
                                                PayOrderHistory.Status = 12;
                                                PayOrderHistory.Amount = drefund;
                                                PayOrderHistory.Type = 2;
                                                PayOrderHistory.CreatedDate = currentDate;
                                                PayOrderHistory.CreatedBy = obj_user.Username;
                                                tbPayHistory.insert(PayOrderHistory);


                                                // HistoryOrderChangeController.Insert(mainorder.ID, obj_user.ID, username, username +
                                                //" đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Đã xong.", 1, currentDate);
                                                if (status == 2)
                                                {
                                                    //HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.", wallet, 2, 2, currentDate, obj_user.Username);

                                                    var payWallletHistory = new tbl_HistoryPayWallet();
                                                    payWallletHistory.UID = ui.ID;
                                                    payWallletHistory.UserName = ui.Username;
                                                    payWallletHistory.MainOrderID = MainOrderID;
                                                    payWallletHistory.Amount = drefund;
                                                    payWallletHistory.HContent = "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.";
                                                    payWallletHistory.MoneyLeft = wallet;
                                                    payWallletHistory.Type = 2;
                                                    payWallletHistory.TradeType = 2;
                                                    payWallletHistory.CreatedDate = currentDate;
                                                    payWallletHistory.CreatedBy = obj_user.Username;
                                                    tbPayWalletHistory.insert(payWallletHistory);

                                                }
                                                else
                                                {
                                                    //HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.", wallet, 2, 2, currentDate, obj_user.Username);
                                                    var payWallletHistory = new tbl_HistoryPayWallet();
                                                    payWallletHistory.UID = ui.ID;
                                                    payWallletHistory.UserName = ui.Username;
                                                    payWallletHistory.MainOrderID = MainOrderID;
                                                    payWallletHistory.Amount = drefund;
                                                    payWallletHistory.HContent = "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.";
                                                    payWallletHistory.MoneyLeft = wallet;
                                                    payWallletHistory.Type = 2;
                                                    payWallletHistory.TradeType = 2;
                                                    payWallletHistory.CreatedDate = currentDate;
                                                    payWallletHistory.CreatedBy = obj_user.Username;
                                                    tbPayWalletHistory.insert(payWallletHistory);

                                                }


                                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(mainorder.UID), ui.Username
                                                //    , mainorder.ID, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem", 0,
                                                //    1, currentDate, obj_user.Username, false);
                                                //Insert thông báo
                                                var Notis = new tbl_Notifications();
                                                Notis.SenderID = obj_user.ID;
                                                Notis.SenderUsername = obj_user.Username;
                                                Notis.ReceivedID = Convert.ToInt32(mainorder.UID);
                                                Notis.ReceivedUsername = ui.Username;
                                                Notis.OrderID = mainorder.ID;
                                                Notis.Message = "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem";
                                                Notis.Status = 0;
                                                Notis.NotifType = 1;
                                                Notis.CreatedDate = currentDate;
                                                Notis.CreatedBy = obj_user.Username;
                                                Notis.PushNotiApp = false;
                                                tbNotis.insert(Notis);


                                                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                                hubContext.Clients.All.addNewMessageToPage("", "");

                                                var setNoti = SendNotiEmailController.GetByID(19);
                                                if (setNoti != null)
                                                {
                                                    if (setNoti.IsSentNotiUser == true)
                                                    {
                                                        NotificationsController.Inser(Convert.ToInt32(mainorder.UID),
                                                                ui.Username, mainorder.ID, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem",
                                                                1, currentDate, obj_user.Username, false);
                                                        var noTi = new tbl_Notification();
                                                        noTi.ReceivedID = Convert.ToInt32(mainorder.UID);
                                                        noTi.ReceivedUsername = ui.Username;
                                                        noTi.OrderID = mainorder.ID;
                                                        noTi.Message = "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem";
                                                        noTi.NotifType = 1;
                                                        noTi.CreatedDate = currentDate;
                                                        noTi.CreatedBy = obj_user.Username;
                                                        noTi.PushNotiApp = false;

                                                        tbNoti.insert(noTi);
                                                    }

                                                    if (setNoti.IsSendEmailUser == true)
                                                    {
                                                        try
                                                        {
                                                            var info = AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID));
                                                            if (info != null)
                                                            {
                                                                PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", info.Email,
                                                               "Thông báo tại Nhập Hàng TQ", "Đã có cập nhật mới về đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem", "");
                                                            }

                                                        }
                                                        catch
                                                        {

                                                        }
                                                    }
                                                }
                                                //try
                                                //{
                                                //    PJUtils.SendMailGmail("cskh@1688pgs.vn", "1688pegasus", AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID)).Email,
                                                //        "Thông báo tại 1688PGS", "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                                //}
                                                //catch
                                                //{

                                                //}
                                                //newdeposit = Deposit;
                                                //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                                            }
                                            else
                                            {
                                                if (Deposit < newdeposit)
                                                {
                                                    MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                                }
                                                else if (Deposit == newdeposit)
                                                {
                                                    //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                                                }
                                                newdeposit = Deposit;

                                            }
                                        }
                                        else
                                        {
                                            MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                            newdeposit = 0;
                                        }
                                        if (totalo == 0)
                                        {
                                            MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                        }
                                        //if (status == 2)
                                        //{

                                        //}
                                        //else
                                        //{
                                        //    newdeposit = Deposit;
                                        //}
                                        #endregion



                                        //Update lại mainorder
                                        var mainOrderUpdate = tbMainOrder.GetById(MainOrderID);
                                        mainOrderUpdate.AmountDeposit = AmountDeposit.ToString();
                                        mainOrderUpdate.PriceCNY = pricecyn.ToString();
                                        mainOrderUpdate.PriceVND = pricevnd.ToString();
                                        mainOrderUpdate.Deposit = newdeposit.ToString();

                                        mainOrderUpdate.FeeShipCN = FeeCNShip.ToString();
                                        mainOrderUpdate.FeeBuyPro = FeeBuyPros.ToString();
                                        mainOrderUpdate.FeeWeight = FeeWeight.ToString();
                                        mainOrderUpdate.IsCheckProductPrice = FeeCheck.ToString();
                                        mainOrderUpdate.IsPackedPrice = IsPackedPrice.ToString();
                                        mainOrderUpdate.IsFastDeliveryPrice = IsFastDeliveryPrice.ToString();
                                        mainOrderUpdate.TotalPriceVND = totalo.ToString();

                                        

                                        tbMainOrder.update(mainOrderUpdate);

                                        //var a = tbMainOrder.GetById(0);
                                        //a.Address = "";

                                        //MainOrderController.UpdateFee(MainOrderID, newdeposit.ToString(), FeeCNShip.ToString(), FeeBuyPros.ToString(), FeeWeight.ToString(),
                                        //    FeeCheck.ToString(), IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), totalo.ToString());
                                    }
                                }
                            }

                        }




                    }
                    trans.Commit();
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thông tin thành công.", "s", true, "/manager/OrderDetail.aspx?id=" + MainOrderID, Page);
                }
                catch (Exception ex)
                {

                    trans.Rollback();
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thông tin không thành công. Vui lòng thử lại", "e", true, "/manager/ProductEdit.aspx?id=" + MainOrderID, Page);
                }


            }


        }
    }
}