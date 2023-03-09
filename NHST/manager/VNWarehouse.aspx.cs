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
using System.IO;

namespace NHST.manager
{
    public partial class VNWarehouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userLoginSystem"] = "phuongnguyen";
            if (!IsPostBack)
            {

                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 5 && ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
            }
        }

        #region Code WS cũ
        [WebMethod]
        public static string GetCode(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                SmallPackageController.UpdateWeightStatus(package.ID, Convert.ToDouble(package.Weight), 3,
                    Convert.ToInt32(package.BigPackageID), 0, 0, 0);
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    OrderGet o = new OrderGet();
                    o.ID = package.ID;
                    o.BarCode = package.OrderTransactionCode;
                    o.TotalWeight = package.Weight.ToString();
                    o.Status = Convert.ToInt32(package.Status);
                    o.MainorderID = Convert.ToInt32(package.MainOrderID);
                    o.Fullname = mainorder.FullName;
                    o.Phone = mainorder.Phone;
                    o.Email = mainorder.Email;
                    o.Address = mainorder.Address;
                    if (mainorder.IsCheckProduct == true)
                        o.Kiemdem = "Có";
                    else
                        o.Kiemdem = "Không";
                    if (mainorder.IsPacked == true)
                        o.Donggo = "Có";
                    else
                        o.Donggo = "Không";

                    bool checkIsChinaCome = true;
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 3)
                                checkIsChinaCome = false;
                        }
                    }
                    bool checkIsAllVN = true;
                    int bigpackid = Convert.ToInt32(package.BigPackageID);
                    var bigpacage = SmallPackageController.GetBuyBigPackageID(bigpackid, "");
                    if (bigpacage.Count > 0)
                    {
                        foreach (var item in bigpacage)
                        {
                            if (item.Status < 3)
                                checkIsAllVN = false;
                        }
                    }

                    DateTime currentDate = DateTime.Now;
                    var accChangeData = AccountController.GetByUsername(username_current);
                    if (accChangeData != null)
                    {
                        HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái của mã vận đơn: <strong>" + barcode + "</strong> của đơn hàng ID là: " + mainorder.ID
                                               + ", là: Đã về kho đích", 8, currentDate);

                        if (checkIsAllVN == true)
                        {
                            BigPackageController.UpdateStatus(bigpackid, 2, DateTime.Now, username_current);
                        }
                        if (checkIsChinaCome == true)
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 7);
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(o);
                }
                else return "none";
            }
            else return "none";
        }
        [WebMethod]
        public static string GetCodeInfo(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var user = AccountController.GetByUsername(username_current);
            if (user != null)
            {
                int userRole = Convert.ToInt32(user.RoleID);
                if (userRole == 0 || userRole == 2 || userRole == 5)
                {
                    var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
                    List<OrderGet> ogs = new List<OrderGet>();
                    if (package != null)
                    {
                        var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                        if (mainorder != null)
                        {
                            OrderGet o = new OrderGet();
                            o.ID = package.ID;
                            o.BarCode = package.OrderTransactionCode;
                            o.TotalWeight = package.Weight.ToString();
                            o.Status = Convert.ToInt32(package.Status);
                            o.MainorderID = Convert.ToInt32(package.MainOrderID);
                            o.Fullname = mainorder.FullName;
                            o.Phone = mainorder.Phone;
                            o.Email = mainorder.Email;
                            o.Address = mainorder.Address;
                            if (mainorder.IsCheckProduct == true)
                                o.Kiemdem = "Có";
                            else
                                o.Kiemdem = "Không";
                            if (mainorder.IsPacked == true)
                                o.Donggo = "Có";
                            else
                                o.Donggo = "Không";
                            ogs.Add(o);
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(o);
                        }
                    }
                    else
                    {
                        var baolon = BigPackageController.GetByPackageCode(barcode);
                        if (baolon != null)
                        {
                            var smalls = SmallPackageController.GetBuyBigPackageID(baolon.ID, "");

                            if (smalls.Count > 0)
                            {

                                foreach (var package1 in smalls)
                                {
                                    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package1.MainOrderID));
                                    if (mainorder != null)
                                    {
                                        OrderGet o = new OrderGet();
                                        o.ID = package1.ID;
                                        o.BarCode = package1.OrderTransactionCode;
                                        o.TotalWeight = package1.Weight.ToString();
                                        o.Status = Convert.ToInt32(package1.Status);
                                        o.MainorderID = Convert.ToInt32(package1.MainOrderID);
                                        o.Fullname = mainorder.FullName;
                                        o.Phone = mainorder.Phone;
                                        o.Email = mainorder.Email;
                                        o.Address = mainorder.Address;
                                        if (mainorder.IsCheckProduct == true)
                                            o.Kiemdem = "Có";
                                        else
                                            o.Kiemdem = "Không";
                                        if (mainorder.IsPacked == true)
                                            o.Donggo = "Có";
                                        else
                                            o.Donggo = "Không";

                                        ogs.Add(o);
                                    }
                                }
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(ogs);
                        }
                    }
                }
            }
            return "none";
        }

        [WebMethod]
        public static string GetCodeInfo_old(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    OrderGet o = new OrderGet();
                    o.ID = package.ID;
                    o.BarCode = package.OrderTransactionCode;
                    o.TotalWeight = package.Weight.ToString();
                    o.Status = Convert.ToInt32(package.Status);
                    o.MainorderID = Convert.ToInt32(package.MainOrderID);
                    o.Fullname = mainorder.FullName;
                    o.Phone = mainorder.Phone;
                    o.Email = mainorder.Email;
                    o.Address = mainorder.Address;
                    if (mainorder.IsCheckProduct == true)
                        o.Kiemdem = "Có";
                    else
                        o.Kiemdem = "Không";
                    if (mainorder.IsPacked == true)
                        o.Donggo = "Có";
                    else
                        o.Donggo = "Không";

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(o);
                }
                else return "none";
            }
            else return "none";
        }
        [WebMethod]
        public static string GetWallet(int UID)
        {
            var user = AccountController.GetByID(UID);
            if (user != null)
                return user.Wallet.ToString();
            else return "0";
        }
        [WebMethod]
        public static string SetFinish(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    if (mainorder.Status == 9)
                    {
                        SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                        return "ok";
                        //int bigbackageID = Convert.ToInt32(package.BigPackageID);                        
                    }
                    else return "none";
                }
                else return "none";
            }
            //var p = OrderShopController.GetByBarCode(barcode);
            //if (p != null)
            //{
            //    if (p.Status < 7)
            //    {
            //        double deposit = Convert.ToDouble(p.Deposit);
            //        double totalprice = Convert.ToDouble(p.TotalPriceVND);
            //        if (deposit < totalprice)
            //        {
            //            int UID = Convert.ToInt32(p.UID);
            //            var u = AccountController.GetByID(Convert.ToInt32(UID));
            //            if (u != null)
            //            {
            //                double sotienphaitra = totalprice - deposit;
            //                double wallet = Convert.ToDouble(u.Wallet);
            //                if (wallet >= sotienphaitra)
            //                {
            //                    double wallet_conlai = wallet - sotienphaitra;
            //                    AccountController.updateWallet(UID, wallet_conlai);
            //                    HistoryPayWalletController.Insert(UID, u.Username, p.ID, p.OrderShopCode, sotienphaitra,
            //                        "Xác nhận đã giao đơn hàng: " + p.OrderShopCode + ".", wallet_conlai, 1, 3, "", currentDate, username_current);
            //                    OrderShopController.UpdateDeposit(p.ID, totalprice.ToString());
            //                    OrderShopController.UpdateStatus(p.ID, 7);
            //                    OrderShopController.UpdateOrderDateExport(p.ID, DateTime.Now);
            //                    return "ok";
            //                }
            //                else
            //                {
            //                    double wallet_conlai = sotienphaitra - wallet;
            //                    return string.Format("{0:N0}", wallet_conlai).Replace(",", ".");
            //                }
            //            }
            //            else
            //            {
            //                return "none";
            //            }
            //        }
            //        else
            //        {
            //            OrderShopController.UpdateStatus(p.ID, 7);
            //            OrderShopController.UpdateOrderDateExport(p.ID, DateTime.Now);
            //            return "ok";
            //        }
            //    }
            //    else return "ok";
            //}
            //else return "none";
            return "none";
        }
        [WebMethod]
        public static string UpdateStatus_NotWeight(string barcode, int status)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                SmallPackageController.UpdateWeightStatus(package.ID, Convert.ToDouble(package.Weight), status,
                    Convert.ToInt32(package.BigPackageID), 0, 0, 0);
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    bool checkIsChinaCome = true;
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 3)
                                checkIsChinaCome = false;

                        }
                    }
                    if (checkIsChinaCome == true)
                        MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 7);
                    return "1";
                }
                else return "none";
            }

            return "none";
        }
        [WebMethod]
        public static string UpdateQuantity(string barcode, string quantity, int status, int BigPackageID)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                SmallPackageController.UpdateWeightStatus(package.ID, quantity.ToFloat(0), status, BigPackageID, 0, 0, 0);
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    int orderID = mainorder.ID;
                    int warehouse = mainorder.ReceivePlace.ToInt(1);
                    int shipping = Convert.ToInt32(mainorder.ShippingType);

                    bool checkIsWarehouseCome = false;
                    double totalweight = quantity.ToFloat(0);
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status == 3)
                                checkIsWarehouseCome = true;
                            if (p.OrderTransactionCode != barcode)
                            {
                                totalweight += Convert.ToDouble(p.Weight);
                            }
                        }
                    }
                    var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));

                    double FeeWeight = 0;
                    double FeeWeightDiscount = 0;
                    double ckFeeWeight = 0;
                    ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
                    double returnprice = 0;
                    var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
                    if (smallpackage.Count > 0)
                    {
                        double totalWeight = 0;
                        bool checkoutweight = false;
                        foreach (var item in smallpackage)
                        {
                            double weight = Convert.ToDouble(item.Weight);
                            if (weight >= 20)
                            {
                                checkoutweight = true;
                            }
                            totalWeight += Convert.ToDouble(item.Weight);
                        }
                        if (warehouse != 4)
                        {
                            var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
                            if (fee.Count > 0)
                            {
                                foreach (var f in fee)
                                {
                                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    {
                                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (checkoutweight == false)
                            {
                                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
                                foreach (var f in fee)
                                {
                                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    {
                                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    }
                                }
                            }
                            else
                            {
                                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
                                foreach (var f in fee)
                                {
                                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    {
                                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    }
                                }
                            }
                        }
                    }
                    double currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    FeeWeight = returnprice * currency;
                    FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
                    FeeWeight = FeeWeight - FeeWeightDiscount;

                    double FeeShipCN = Math.Floor(Convert.ToDouble(mainorder.FeeShipCN));
                    double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                    double IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);
                    double IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);
                    double IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);
                    double isfastprice = 0;
                    if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
                        isfastprice = Convert.ToDouble(mainorder.IsFastPrice);
                    double pricenvd = 0;
                    if (!string.IsNullOrEmpty(mainorder.PriceVND))
                        pricenvd = Convert.ToDouble(mainorder.PriceVND);
                    double Deposit = Convert.ToDouble(mainorder.Deposit);

                    double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                 + IsFastDeliveryPrice + isfastprice + pricenvd;

                    MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
                        IsCheckProductPrice.ToString(),
                        IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
                    MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
                    var accChangeData = AccountController.GetByUsername(username_current);
                    if (accChangeData != null)
                    {
                        if (status == 3)
                        {

                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                           " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                           + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);


                        }
                        if (checkIsWarehouseCome == true)
                        {

                            var smallpackages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                            if (smallpackages.Count > 0)
                            {
                                bool isChuaVekhoTQ = true;
                                var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status == 3).ToList();
                                var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status == 3).ToList();
                                double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                if (che >= sp_main.Count)
                                {
                                    isChuaVekhoTQ = false;
                                }
                                if (isChuaVekhoTQ == false)
                                {
                                    MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 7);
                                }
                            }


                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);

                            NotificationController.Inser(accChangeData.ID, accChangeData.Username, usercreate.ID,
                                                        usercreate.Username, mainorder.ID,
                                                        "Đơn hàng: " + mainorder.ID + " đã về kho bạn yêu cầu nhận.", 0,
                                                       1, currentDate, accChangeData.Username, false);
                        }
                    }

                    return "1";
                }
                else return "none";
            }

            return "none";
        }
        #endregion

        #region Code WS mới
        [WebMethod]
        public static string GetListPackage(string barcode)
        {
            DateTime currentDate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 5)
                    {
                        var bigpackage = BigPackageController.GetByPackageCode(barcode);
                        if (bigpackage != null)
                        {
                            int bID = bigpackage.ID;
                            BigPackageItem bi = new BigPackageItem();
                            bi.BigpackageID = bID;
                            bi.BigpackageCode = bigpackage.PackageCode;
                            bi.BigpackageType = 1;
                            List<smallpackageitem> sis = new List<smallpackageitem>();
                            var smallpackages = SmallPackageController.GetBuyBigPackageID(bID, "");
                            if (smallpackages.Count > 0)
                            {
                                foreach (var item in smallpackages)
                                {
                                    smallpackageitem si = new smallpackageitem();
                                    int mID = Convert.ToInt32(item.MainOrderID);
                                    int tID = Convert.ToInt32(item.TransportationOrderID);
                                    si.IMG = item.ListIMG;
                                    si.Note = item.Description;
                                    si.ID = item.ID;
                                    if (mID > 0)
                                    {
                                        si.OrderType = "Đơn hàng mua hộ";
                                        si.MainorderID = mID;
                                        si.TransportationID = 0;
                                        var mainorder = MainOrderController.GetAllByID(mID);
                                        if (mainorder != null)
                                        {
                                            int UID = Convert.ToInt32(mainorder.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                si.OrderShopCode = mainorder.MainOrderCode;
                                                if (mainorder.IsCheckProduct == true)
                                                    si.Kiemdem = "Có";
                                                else
                                                    si.Kiemdem = "Không";

                                                if (mainorder.IsPacked == true)
                                                    si.Donggo = "Có";
                                                else
                                                    si.Donggo = "Không";

                                                var orders = OrderController.GetByMainOrderID(mID);
                                                si.Soloaisanpham = orders.Count.ToString();
                                                double totalProductQuantity = 0;
                                                if (orders.Count > 0)
                                                {
                                                    foreach (var p in orders)
                                                    {
                                                        totalProductQuantity += Convert.ToDouble(p.quantity);
                                                    }
                                                }
                                                si.Soluongsanpham = totalProductQuantity.ToString();
                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    si.Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                            }
                                        }
                                    }
                                    else if (tID > 0)
                                    {
                                        si.OrderType = "Đơn hàng VC hộ";
                                        si.MainorderID = 0;
                                        si.TransportationID = tID;
                                        var trans = TransportationOrderController.GetByID(tID);
                                        if (trans != null)
                                        {
                                            int UID = Convert.ToInt32(trans.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    si.Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                        si.Username = string.Empty;
                                    }
                                    si.Weight = Convert.ToDouble(item.Weight);
                                    si.BarCode = item.OrderTransactionCode;
                                    si.Status = Convert.ToInt32(item.Status);
                                    si.Description = item.Description;
                                    si.Description = item.Description;
                                    si.BigPackageID = bigpackage.ID;
                                    si.IsTemp = Convert.ToBoolean(item.IsTemp);
                                    if (item.IsLost != null)
                                        si.IsThatlac = Convert.ToBoolean(item.IsLost);
                                    else
                                        si.IsThatlac = false;
                                    if (item.IsHelpMoving != null)
                                        si.IsVCH = Convert.ToBoolean(item.IsHelpMoving);
                                    else
                                        si.IsVCH = false;
                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (item.Length != null)
                                    {
                                        dai = Convert.ToDouble(item.Length);
                                    }
                                    if (item.Width != null)
                                    {
                                        rong = Convert.ToDouble(item.Width);
                                    }
                                    if (item.Height != null)
                                    {
                                        cao = Convert.ToDouble(item.Height);
                                    }
                                    si.dai = dai;
                                    si.rong = rong;
                                    si.cao = cao;

                                    sis.Add(si);
                                }
                            }
                            bi.BigpackageSmallPackageCount = smallpackages.Count;
                            bi.Smallpackages = sis;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bi);
                        }
                        else
                        {
                            BigPackageItem bi = new BigPackageItem();
                            bi.BigpackageID = 0;
                            bi.BigpackageCode = "";
                            bi.BigpackageType = 2;
                            List<smallpackageitem> sis = new List<smallpackageitem>();
                            var smallpackages = SmallPackageController.GetListByOrderTransactionCode(barcode);
                            if (smallpackages.Count > 0)
                            {
                                foreach (var item in smallpackages)
                                {
                                    SmallPackageController.UpdateStatusAndIsLostAndDateInKhoDich(item.ID, 3, false, currentDate, currentDate, username);
                                    int bID = Convert.ToInt32(item.BigPackageID);
                                    if (bID > 0)
                                    {
                                        var big = BigPackageController.GetByID(bID);
                                        if (big != null)
                                        {
                                            bool checkIschua = false;
                                            var smalls = SmallPackageController.GetBuyBigPackageID(bID, "");
                                            if (smalls.Count > 0)
                                            {
                                                foreach (var s in smalls)
                                                {
                                                    if (s.Status < 3)
                                                        checkIschua = true;
                                                }
                                                if (checkIschua == false)
                                                {
                                                    BigPackageController.UpdateStatus(bID, 2, currentDate, username);
                                                }
                                            }
                                        }
                                    }

                                    smallpackageitem si = new smallpackageitem();
                                    int mID = Convert.ToInt32(item.MainOrderID);
                                    int tID = Convert.ToInt32(item.TransportationOrderID);
                                    si.ID = item.ID;
                                    si.IMG = item.ListIMG;
                                    si.Note = item.Description;
                                    if (mID > 0)
                                    {
                                        si.OrderType = "Đơn hàng mua hộ";
                                        si.MainorderID = mID;
                                        si.TransportationID = 0;
                                        var mainorder = MainOrderController.GetAllByID(mID);
                                        if (mainorder != null)
                                        {
                                            int UID = Convert.ToInt32(mainorder.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                si.OrderShopCode = mainorder.MainOrderCode;
                                                if (mainorder.IsCheckProduct == true)
                                                    si.Kiemdem = "Có";
                                                else
                                                    si.Kiemdem = "Không";

                                                if (mainorder.IsPacked == true)
                                                    si.Donggo = "Có";
                                                else
                                                    si.Donggo = "Không";

                                                var orders = OrderController.GetByMainOrderID(mID);
                                                si.Soloaisanpham = orders.Count.ToString();
                                                double totalProductQuantity = 0;
                                                if (orders.Count > 0)
                                                {
                                                    foreach (var p in orders)
                                                    {
                                                        totalProductQuantity += Convert.ToDouble(p.quantity);
                                                    }
                                                }
                                                si.Soluongsanpham = totalProductQuantity.ToString();
                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    si.Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                            }
                                        }
                                    }
                                    else if (tID > 0)
                                    {
                                        si.OrderType = "Đơn hàng VC hộ";
                                        si.MainorderID = 0;
                                        si.TransportationID = tID;
                                        var trans = TransportationOrderController.GetByID(tID);
                                        if (trans != null)
                                        {
                                            int UID = Convert.ToInt32(trans.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    si.Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                        si.Username = string.Empty;
                                    }
                                    si.Weight = Convert.ToDouble(item.Weight);
                                    si.BarCode = item.OrderTransactionCode;
                                    si.Status = 3;
                                    si.Description = item.Description;
                                    si.BigPackageID = 0;
                                    si.IsTemp = Convert.ToBoolean(item.IsTemp);
                                    if (item.IsLost != null)
                                        si.IsThatlac = Convert.ToBoolean(item.IsLost);
                                    else
                                        si.IsThatlac = false;
                                    if (item.IsHelpMoving != null)
                                        si.IsVCH = Convert.ToBoolean(item.IsHelpMoving);
                                    else
                                        si.IsVCH = false;
                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (item.Length != null)
                                    {
                                        dai = Convert.ToDouble(item.Length);
                                    }
                                    if (item.Width != null)
                                    {
                                        rong = Convert.ToDouble(item.Width);
                                    }
                                    if (item.Height != null)
                                    {
                                        cao = Convert.ToDouble(item.Height);
                                    }
                                    si.dai = dai;
                                    si.rong = rong;
                                    si.cao = cao;
                                    sis.Add(si);
                                }
                            }
                            bi.BigpackageSmallPackageCount = smallpackages.Count;
                            bi.Smallpackages = sis;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bi);
                        }
                    }
                }
            }


            return "none";
        }
        [WebMethod]
        public static string UpdateQuantityNew(string barcode, string quantity, int status, int BigPackageID, int packageID, double dai, double rong, double cao, string base64, string note)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            //var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {

                if (status == 0)
                {
                    SmallPackageController.UpdateWeightStatus(package.ID, 0, status, BigPackageID, 0, 0, 0);
                    return "1";
                }
                else
                {
                    double quantityD = 0;
                    if (quantity.ToFloat(0) > 0)
                        quantityD = Convert.ToDouble(quantity);
                    quantityD = Math.Round(quantityD, 1);

                    SmallPackageController.UpdateWeightStatusAndDateInLasteWareHouseIsLost(package.ID,
                        quantityD, status,
                        currentDate, false, dai, rong, cao);
                    SmallPackageController.UpdateNote(package.ID, note);

                    string dbIMG = package.ListIMG;
                    string[] listk = { };
                    if (!string.IsNullOrEmpty(package.ListIMG))
                    {
                        listk = dbIMG.Split('|');
                    }



                    string value = base64;
                    string link = "";
                    if (!string.IsNullOrEmpty(value))
                    {
                        string[] listIMG = value.Split('|');
                        for (int i = 0; i < listIMG.Length - 1; i++)
                        {
                            string imageData = listIMG[i];
                            bool ch = listk.Any(x => x == imageData);
                            if (ch == true)
                            {
                                link += imageData + "|";
                            }
                            else
                            {
                                string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/smallpackageIMG/");
                                string date = DateTime.Now.ToString("dd-MM-yyyy");
                                string time = DateTime.Now.ToString("hh:mm tt");
                                Page page = (Page)HttpContext.Current.Handler;
                                //  TextBox txtCampaign = (TextBox)page.FindControl("txtCampaign");
                                string k = i.ToString();
                                string fileNameWitPath = path + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                                string linkIMG = "/Uploads/smallpackageIMG/" + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                                link += linkIMG + "|";
                                //   string fileNameWitPath = path + s + ".png";
                                byte[] data;
                                string convert;
                                using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                                {
                                    using (BinaryWriter bw = new BinaryWriter(fs))
                                    {
                                        if (imageData.Contains("data:image/png"))
                                        {
                                            convert = imageData.Replace("data:image/png;base64,", String.Empty);
                                            data = Convert.FromBase64String(convert);
                                            bw.Write(data);
                                        }
                                        else if (imageData.Contains("data:image/jpeg"))
                                        {
                                            convert = imageData.Replace("data:image/jpeg;base64,", String.Empty);
                                            data = Convert.FromBase64String(convert);
                                            bw.Write(data);
                                        }
                                        else if (imageData.Contains("data:image/gif"))
                                        {
                                            convert = imageData.Replace("data:image/gif;base64,", String.Empty);
                                            data = Convert.FromBase64String(convert);
                                            bw.Write(data);
                                        }
                                    }
                                }
                            }


                        }
                    }

                    SmallPackageController.UpdateIMG(package.ID, link, DateTime.Now, username_current);

                    int bID = Convert.ToInt32(package.BigPackageID);
                    if (bID > 0)
                    {
                        var big = BigPackageController.GetByID(bID);
                        if (big != null)
                        {
                            bool checkIschua = false;
                            var smalls = SmallPackageController.GetBuyBigPackageID(bID, "");
                            if (smalls.Count > 0)
                            {
                                foreach (var s in smalls)
                                {
                                    if (s.Status < 3)
                                        checkIschua = true;
                                }
                                if (checkIschua == false)
                                {
                                    BigPackageController.UpdateStatus(bID, 2, currentDate, username_current);
                                }
                            }
                        }
                    }
                    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                    if (mainorder != null)
                    {
                        int orderID = mainorder.ID;
                        int warehouse = mainorder.ReceivePlace.ToInt(1);
                        int shipping = Convert.ToInt32(mainorder.ShippingType);
                        int warehouseFrom = Convert.ToInt32(mainorder.FromPlace);

                        bool checkIsChinaCome = true;
                        double totalweight = quantityD;
                        var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                        if (packages.Count > 0)
                        {
                            foreach (var p in packages)
                            {
                                if (p.Status < 3)
                                    checkIsChinaCome = false;
                                if (p.OrderTransactionCode != barcode)
                                {
                                    totalweight += Convert.ToDouble(p.Weight);
                                }
                            }
                        }
                        var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));

                        double FeeWeight = 0;
                        double FeeWeightDiscount = 0;
                        double ckFeeWeight = 0;
                        ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
                        double returnprice = 0;
                        double pricePerWeight = 0;
                        double finalPriceOfPackage = 0;
                        double totalWeight = 0;
                        var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
                        if (smallpackage.Count > 0)
                        {
                            foreach (var item in smallpackage)
                            {
                                double compareSize = 0;
                                double weight = Convert.ToDouble(item.Weight);

                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    compareSize = (pDai * pRong * pCao) / 6000;
                                    Math.Round(compareSize, 2);
                                }

                                if (weight >= compareSize)
                                {
                                    totalWeight += weight;
                                }
                                else
                                {
                                    totalWeight += compareSize;
                                }
                            }
                            totalweight = totalWeight;

                            if (!string.IsNullOrEmpty(usercreate.FeeTQVNPerWeight))
                            {
                                double feetqvn = 0;
                                if (usercreate.FeeTQVNPerWeight.ToFloat(0) > 0)
                                {
                                    feetqvn = Convert.ToDouble(usercreate.FeeTQVNPerWeight);
                                }
                                pricePerWeight = feetqvn;
                                returnprice = totalweight * feetqvn;
                            }
                            else
                            {
                                var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(warehouseFrom, warehouse, shipping, false);
                                if (fee.Count > 0)
                                {
                                    foreach (var f in fee)
                                    {
                                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                        {
                                            pricePerWeight = Convert.ToDouble(f.Price);
                                            returnprice = totalWeight * Convert.ToDouble(f.Price);
                                        }
                                    }
                                }
                            }


                            foreach (var item in smallpackage)
                            {

                                double compareSize = 0;
                                double weight = Convert.ToDouble(item.Weight);

                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    compareSize = (pDai * pRong * pCao) / 6000;
                                    Math.Round(compareSize, 2);
                                }
                                if (weight >= compareSize)
                                {
                                    double TotalPriceCN = weight * pricePerWeight;
                                    SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceCN);
                                }
                                else
                                {
                                    double TotalPriceTT = compareSize * pricePerWeight;
                                    SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceTT);
                                }
                            }


                        }
                        double currency = Convert.ToDouble(mainorder.CurrentCNYVN);

                        //FeeWeight = returnprice * currency;
                        //returnprice = finalPriceOfPackage;
                        FeeWeight = returnprice;
                        //discount %
                        //FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
                        //discount price
                        if (Convert.ToInt32(mainorder.ReceivePlace) != 11)
                        {
                            FeeWeightDiscount = totalWeight * ckFeeWeight;
                        }
                        FeeWeight = FeeWeight - FeeWeightDiscount;

                        double FeeShipCN = Math.Floor(Convert.ToDouble(mainorder.FeeShipCN));
                        double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                        double IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);
                        double IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);
                        double IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);
                        double isfastprice = 0;
                        if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
                            isfastprice = Convert.ToDouble(mainorder.IsFastPrice);
                        double pricenvd = 0;
                        if (!string.IsNullOrEmpty(mainorder.PriceVND))
                            pricenvd = Convert.ToDouble(mainorder.PriceVND);
                        double Deposit = Convert.ToDouble(mainorder.Deposit);

                        double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                     + IsFastDeliveryPrice + isfastprice + pricenvd;

                        MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
                            IsCheckProductPrice.ToString(),
                            IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
                        MainOrderController.UpdateFeeWeightCK(mainorder.ID, FeeWeightDiscount.ToString());
                        MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
                        var accChangeData = AccountController.GetByUsername(username_current);
                        if (accChangeData != null)
                        {
                            if (status == 2)
                            {
                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                   " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                   + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho VN", 8, currentDate);
                            }
                            if (checkIsChinaCome == true)
                            {
                                int MainorderID = mainorder.ID;
                                //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                                var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
                                if (smallpackages.Count > 0)
                                {
                                    bool isChuaVekhoTQ = true;
                                    var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                    var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status >= 3).ToList();
                                    var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status >= 3).ToList();
                                    double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                    if (che >= sp_main.Count)
                                    {
                                        isChuaVekhoTQ = false;
                                    }
                                    if (isChuaVekhoTQ == false)
                                    {
                                        MainOrderController.UpdateStatus(MainorderID,
                                            Convert.ToInt32(mainorder.UID), 7);
                                        if (mainorder.VNWarehouseDate == null)
                                            MainOrderController.UpdateVNWarehouseDate(MainorderID,
                                               Convert.ToInt32(mainorder.UID), currentDate);
                                        MainOrderRequestShipController.UpdateMainOrderStatusByMainOrderID(MainorderID, 7);

                                        if (mainorder.Status != 7)
                                        {
                                            var setNoti = SendNotiEmailController.GetByID(9);
                                            if (setNoti != null)
                                            {
                                                var acc = AccountController.GetByID(mainorder.UID.Value);
                                                if (acc != null)
                                                {
                                                    if (setNoti.IsSentNotiUser == true)
                                                    {
                                                        NotificationsController.Inser(acc.ID,
                                                              acc.Username, MainorderID,
                                                              "Hàng của đơn hàng " + MainorderID + " đã về kho VN.", 1,
                                                              currentDate, username_current, false);
                                                    }

                                                    if (setNoti.IsSendEmailUser == true)
                                                    {
                                                        try
                                                        {
                                                            PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", acc.Email,
                                                                "Thông báo tại Nam Trung.", "Hàng của đơn hàng " + MainorderID + " đã về kho VN.", "");
                                                        }
                                                        catch { }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                                   " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);
                            }
                        }
                        return "1";
                    }
                    else
                    {
                        var transportation = TransportationOrderController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                        if (transportation != null)
                        {
                            int tID = transportation.ID;
                            int warehouseFrom = Convert.ToInt32(transportation.WarehouseFromID);
                            int warehouse = Convert.ToInt32(transportation.WarehouseID);
                            int shipping = Convert.ToInt32(transportation.ShippingTypeID);

                            bool checkIsChinaCome = true;
                            double totalweight = 0;

                            var packages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (packages.Count > 0)
                            {
                                foreach (var p in packages)
                                {
                                    if (p.Status < 2)
                                        checkIsChinaCome = false;
                                }
                            }
                            var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));
                            double returnprice = 0;
                            double pricePerWeight = 0;
                            double finalPriceOfPackage = 0;

                            foreach (var item in packages)
                            {
                                double compareSize = 0;
                                double weight = Convert.ToDouble(item.Weight);

                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    compareSize = (pDai * pRong * pCao) / 6000;
                                }

                                if (weight >= compareSize)
                                {
                                    totalweight += weight;
                                }
                                else
                                {
                                    totalweight += compareSize;
                                }
                            }


                            if (!string.IsNullOrEmpty(usercreate.FeeTQVNPerWeight))
                            {
                                double feetqvn = 0;
                                if (usercreate.FeeTQVNPerWeight.ToFloat(0) > 0)
                                {
                                    feetqvn = Convert.ToDouble(usercreate.FeeTQVNPerWeight);
                                }
                                pricePerWeight = feetqvn;
                                returnprice = totalweight * feetqvn;
                            }
                            else
                            {
                                var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                                warehouseFrom, warehouse, shipping, true);
                                if (fee.Count > 0)
                                {
                                    foreach (var f in fee)
                                    {
                                        if (totalweight > f.WeightFrom && totalweight <= f.WeightTo)
                                        {
                                            pricePerWeight = Convert.ToDouble(f.Price);
                                            returnprice = totalweight * Convert.ToDouble(f.Price);
                                            break;
                                        }
                                    }
                                }
                            }

                            foreach (var item in packages)
                            {

                                double compareSize = 0;
                                double weight = Convert.ToDouble(item.Weight);


                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    compareSize = (pDai * pRong * pCao) / 6000;
                                }
                                if (weight >= compareSize)
                                {
                                    double TotalPriceCN = weight * pricePerWeight;
                                    SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceCN);
                                }
                                else
                                {
                                    double TotalPriceTT = compareSize * pricePerWeight;
                                    SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceTT);
                                }
                            }




                            finalPriceOfPackage = returnprice;

                            double currency = Convert.ToDouble(transportation.Currency);
                            double totalPriceVND = finalPriceOfPackage;
                            double totalPriceCYN = 0;
                            totalPriceCYN = returnprice / currency;


                            var accChangeData = AccountController.GetByUsername(username_current);
                            if (accChangeData != null)
                            {

                                if (checkIsChinaCome == true)
                                {
                                    //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                                    var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                                    if (smallpackages.Count > 0)
                                    {
                                        bool isChuaVekhoTQ = true;
                                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status >= 3).ToList();
                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status >= 3).ToList();
                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                        if (che >= sp_main.Count)
                                        {
                                            isChuaVekhoTQ = false;
                                        }
                                        if (isChuaVekhoTQ == false)
                                        {
                                            TransportationOrderController.UpdateStatus(tID, 5, currentDate, username_current);

                                            if (transportation.Status != 5)
                                            {
                                                var setNoti = SendNotiEmailController.GetByID(17);
                                                if (setNoti != null)
                                                {
                                                    var acc = AccountController.GetByID(transportation.UID.Value);
                                                    if (acc != null)
                                                    {
                                                        if (setNoti.IsSentNotiUser == true)
                                                        {
                                                            NotificationsController.Inser(acc.ID,
                                                                  acc.Username, transportation.ID,
                                                                  "<a href=\"/chi-tiet-don-hang-van-chuyen-ho/" + transportation.ID + "\" target=\"_blank\">Đơn hàng vận chuyển hộ " + transportation.ID + " đã về kho VN.</a>", 1,
                                                                  currentDate, username_current, false);
                                                        }

                                                        if (setNoti.IsSendEmailUser == true)
                                                        {
                                                            try
                                                            {
                                                                PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", acc.Email,
                                                                    "Thông báo tại Nam Trung.", "Đơn hàng vận chuyển hộ " + transportation.ID + " đã về kho VN.", "");
                                                            }
                                                            catch { }
                                                        }
                                                    }

                                                }
                                            }




                                        }
                                    }
                                }
                            }
                            TransportationOrderController.UpdateTotalWeightTotalPrice(tID, totalweight, totalPriceVND, currentDate, username_current);
                            return "1";
                        }
                        else
                        {
                            return "1";
                        }
                    }
                    return "none";
                }

            }

            return "none";
        }
        [WebMethod]
        public static string GetListPackage1(string barcode)
        {
            DateTime currentDate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 5)
                    {
                        var bigpackage = BigPackageController.GetByPackageCode(barcode);
                        if (bigpackage != null)
                        {
                            int bID = bigpackage.ID;
                            BigPackageItem bi = new BigPackageItem();
                            bi.BigpackageID = bID;
                            bi.BigpackageCode = bigpackage.PackageCode;
                            bi.BigpackageType = 1;
                            List<smallpackageitem> sis = new List<smallpackageitem>();
                            var smallpackages = SmallPackageController.GetBuyBigPackageID(bID, "");
                            if (smallpackages.Count > 0)
                            {
                                foreach (var item in smallpackages)
                                {
                                    smallpackageitem si = new smallpackageitem();
                                    int mID = Convert.ToInt32(item.MainOrderID);
                                    int tID = Convert.ToInt32(item.TransportationOrderID);
                                    si.ID = item.ID;
                                    if (mID > 0)
                                    {
                                        si.OrderType = "Đơn hàng mua hộ";
                                        si.MainorderID = mID;
                                        si.TransportationID = 0;
                                        var mainorder = MainOrderController.GetAllByID(mID);
                                        if (mainorder != null)
                                        {
                                            int UID = Convert.ToInt32(mainorder.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                si.OrderShopCode = mainorder.MainOrderCode;
                                                if (mainorder.IsCheckProduct == true)
                                                    si.Kiemdem = "Có";
                                                else
                                                    si.Kiemdem = "Không";

                                                if (mainorder.IsPacked == true)
                                                    si.Donggo = "Có";
                                                else
                                                    si.Donggo = "Không";

                                                var orders = OrderController.GetByMainOrderID(mID);
                                                si.Soloaisanpham = orders.Count.ToString();
                                                double totalProductQuantity = 0;
                                                if (orders.Count > 0)
                                                {
                                                    foreach (var p in orders)
                                                    {
                                                        totalProductQuantity += Convert.ToDouble(p.quantity);
                                                    }
                                                }
                                                si.Soluongsanpham = totalProductQuantity.ToString();
                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    si.Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                            }
                                        }
                                    }
                                    else if (tID > 0)
                                    {
                                        si.OrderType = "Đơn hàng VC hộ";
                                        si.MainorderID = 0;
                                        si.TransportationID = tID;
                                    }
                                    else
                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                    }
                                    si.Weight = Convert.ToDouble(item.Weight);
                                    si.BarCode = item.OrderTransactionCode;
                                    si.Status = Convert.ToInt32(item.Status);
                                    si.Description = item.Description;
                                    si.Description = item.Description;
                                    si.BigPackageID = bigpackage.ID;
                                    si.IsTemp = Convert.ToBoolean(item.IsTemp);
                                    if (item.IsLost != null)
                                        si.IsThatlac = Convert.ToBoolean(item.IsLost);
                                    else
                                        si.IsThatlac = false;
                                    if (item.IsHelpMoving != null)
                                        si.IsVCH = Convert.ToBoolean(item.IsHelpMoving);
                                    else
                                        si.IsVCH = false;
                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (item.Length != null)
                                    {
                                        dai = Convert.ToDouble(item.Length);
                                    }
                                    if (item.Width != null)
                                    {
                                        rong = Convert.ToDouble(item.Width);
                                    }
                                    if (item.Height != null)
                                    {
                                        cao = Convert.ToDouble(item.Height);
                                    }
                                    si.dai = dai;
                                    si.rong = rong;
                                    si.cao = cao;

                                    sis.Add(si);
                                }
                            }
                            bi.BigpackageSmallPackageCount = smallpackages.Count;
                            bi.Smallpackages = sis;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bi);
                        }
                        else
                        {
                            BigPackageItem bi = new BigPackageItem();
                            bi.BigpackageID = 0;
                            bi.BigpackageCode = "";
                            bi.BigpackageType = 2;
                            List<smallpackageitem> sis = new List<smallpackageitem>();
                            var smallpackages = SmallPackageController.GetListByOrderTransactionCode(barcode);
                            if (smallpackages.Count > 0)
                            {
                                foreach (var item in smallpackages)
                                {
                                    SmallPackageController.UpdateStatusAndIsLostAndDateInKhoDich(item.ID, 3, false, currentDate, currentDate, username);
                                    int bID = Convert.ToInt32(item.BigPackageID);
                                    if (bID > 0)
                                    {
                                        var big = BigPackageController.GetByID(bID);
                                        if (big != null)
                                        {
                                            bool checkIschua = false;
                                            var smalls = SmallPackageController.GetBuyBigPackageID(bID, "");
                                            if (smalls.Count > 0)
                                            {
                                                foreach (var s in smalls)
                                                {
                                                    if (s.Status < 3)
                                                        checkIschua = true;
                                                }
                                                if (checkIschua == false)
                                                {
                                                    BigPackageController.UpdateStatus(bID, 2, currentDate, username);
                                                }
                                            }
                                        }
                                    }

                                    smallpackageitem si = new smallpackageitem();
                                    int mID = Convert.ToInt32(item.MainOrderID);
                                    int tID = Convert.ToInt32(item.TransportationOrderID);
                                    si.ID = item.ID;
                                    if (mID > 0)
                                    {
                                        si.OrderType = "Đơn hàng mua hộ";
                                        si.MainorderID = mID;
                                        si.TransportationID = 0;
                                        var mainorder = MainOrderController.GetAllByID(mID);
                                        if (mainorder != null)
                                        {
                                            int UID = Convert.ToInt32(mainorder.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                si.OrderShopCode = mainorder.MainOrderCode;
                                                if (mainorder.IsCheckProduct == true)
                                                    si.Kiemdem = "Có";
                                                else
                                                    si.Kiemdem = "Không";

                                                if (mainorder.IsPacked == true)
                                                    si.Donggo = "Có";
                                                else
                                                    si.Donggo = "Không";

                                                var orders = OrderController.GetByMainOrderID(mID);
                                                si.Soloaisanpham = orders.Count.ToString();
                                                double totalProductQuantity = 0;
                                                if (orders.Count > 0)
                                                {
                                                    foreach (var p in orders)
                                                    {
                                                        totalProductQuantity += Convert.ToDouble(p.quantity);
                                                    }
                                                }
                                                si.Soluongsanpham = totalProductQuantity.ToString();
                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    si.Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                            }
                                        }
                                    }
                                    else if (tID > 0)
                                    {
                                        si.OrderType = "Đơn hàng VC hộ";
                                        si.MainorderID = 0;
                                        si.TransportationID = tID;
                                    }
                                    else
                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                    }
                                    si.Weight = Convert.ToDouble(item.Weight);
                                    si.BarCode = item.OrderTransactionCode;
                                    si.Status = 3;
                                    si.Description = item.Description;
                                    si.BigPackageID = 0;
                                    si.IsTemp = Convert.ToBoolean(item.IsTemp);
                                    if (item.IsLost != null)
                                        si.IsThatlac = Convert.ToBoolean(item.IsLost);
                                    else
                                        si.IsThatlac = false;
                                    if (item.IsHelpMoving != null)
                                        si.IsVCH = Convert.ToBoolean(item.IsHelpMoving);
                                    else
                                        si.IsVCH = false;
                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (item.Length != null)
                                    {
                                        dai = Convert.ToDouble(item.Length);
                                    }
                                    if (item.Width != null)
                                    {
                                        rong = Convert.ToDouble(item.Width);
                                    }
                                    if (item.Height != null)
                                    {
                                        cao = Convert.ToDouble(item.Height);
                                    }
                                    si.dai = dai;
                                    si.rong = rong;
                                    si.cao = cao;
                                    sis.Add(si);
                                }
                            }
                            bi.BigpackageSmallPackageCount = smallpackages.Count;
                            bi.Smallpackages = sis;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bi);
                        }
                    }
                }
            }


            return "none";
        }
        [WebMethod]
        public static string UpdateQuantityNew1(string barcode, string quantity, int status, int BigPackageID, int packageID, double dai, double rong, double cao)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            //var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {

                if (status == 0)
                {
                    SmallPackageController.UpdateWeightStatus(package.ID, 0, status, BigPackageID, 0, 0, 0);
                    return "1";
                }
                else
                {
                    double quanIn = 0;
                    if (!string.IsNullOrEmpty(quantity))
                    {
                        quanIn = Convert.ToDouble(quantity);
                    }
                    SmallPackageController.UpdateWeightStatusAndDateInLasteWareHouseIsLost(package.ID, quanIn, status,
                        currentDate, false, dai, rong, cao);
                    int bID = Convert.ToInt32(package.BigPackageID);
                    if (bID > 0)
                    {
                        var big = BigPackageController.GetByID(bID);
                        if (big != null)
                        {
                            bool checkIschua = false;
                            var smalls = SmallPackageController.GetBuyBigPackageID(bID, "");
                            if (smalls.Count > 0)
                            {
                                foreach (var s in smalls)
                                {
                                    if (s.Status < 3)
                                        checkIschua = true;
                                }
                                if (checkIschua == false)
                                {
                                    BigPackageController.UpdateStatus(bID, 2, currentDate, username_current);
                                }
                            }
                        }
                    }
                    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                    if (mainorder != null)
                    {
                        int orderID = mainorder.ID;
                        int warehouse = mainorder.ReceivePlace.ToInt(1);
                        int shipping = Convert.ToInt32(mainorder.ShippingType);
                        int warehouseFrom = Convert.ToInt32(mainorder.FromPlace);

                        bool checkIsChinaCome = true;
                        double totalweight = quanIn;
                        var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                        if (packages.Count > 0)
                        {
                            foreach (var p in packages)
                            {
                                if (p.Status < 3)
                                    checkIsChinaCome = false;
                                if (p.OrderTransactionCode != barcode)
                                {
                                    totalweight += Convert.ToDouble(p.Weight);
                                }
                            }
                        }
                        var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));

                        double FeeWeight = 0;
                        double FeeWeightDiscount = 0;
                        double ckFeeWeight = 0;
                        ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
                        double returnprice = 0;
                        double pricePerWeight = 0;
                        double finalPriceOfPackage = 0;
                        var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
                        if (smallpackage.Count > 0)
                        {
                            double totalWeight = 0;
                            foreach (var item in smallpackage)
                            {
                                double totalWeightCN = Convert.ToDouble(item.Weight);
                                double totalWeightTT = 0;
                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    totalWeightTT = (pDai * pRong * pCao) / 6000;
                                    Math.Round(totalWeightTT, 2);
                                }
                                if (totalWeightCN > totalWeightTT)
                                {
                                    totalWeight += totalWeightCN;
                                }
                                else
                                {
                                    totalWeight += totalWeightTT;
                                }
                            }
                            var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(warehouseFrom, warehouse, shipping, false);
                            if (fee.Count > 0)
                            {
                                foreach (var f in fee)
                                {
                                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    {
                                        pricePerWeight = Convert.ToDouble(f.Price);
                                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    }
                                }
                            }
                            MainOrderController.UpdateTotalWeight(mainorder.ID, totalWeight.ToString(), totalWeight.ToString());
                            foreach (var item in smallpackage)
                            {
                                double compareweight = 0;
                                double compareSize = 0;

                                double weight = Convert.ToDouble(item.Weight);
                                compareweight = weight * pricePerWeight;

                                double weigthTT = 0;
                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    weigthTT = (pDai * pRong * pCao) / 6000;
                                    Math.Round(weigthTT, 2);
                                }
                                compareSize = weigthTT * pricePerWeight;

                                if (compareweight >= compareSize)
                                {
                                    finalPriceOfPackage += compareweight;
                                    SmallPackageController.UpdateTotalPrice(item.ID, compareweight);
                                }
                                else
                                {
                                    finalPriceOfPackage += compareSize;
                                    SmallPackageController.UpdateTotalPrice(item.ID, compareSize);
                                }
                            }
                        }
                        double currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                        //FeeWeight = returnprice * currency;
                        returnprice = finalPriceOfPackage;
                        FeeWeight = returnprice;
                        FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
                        FeeWeight = FeeWeight - FeeWeightDiscount;

                        double FeeShipCN = Math.Floor(Convert.ToDouble(mainorder.FeeShipCN));
                        double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                        double IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);
                        double IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);
                        double IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);
                        double isfastprice = 0;
                        if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
                            isfastprice = Convert.ToDouble(mainorder.IsFastPrice);
                        double pricenvd = 0;
                        if (!string.IsNullOrEmpty(mainorder.PriceVND))
                            pricenvd = Convert.ToDouble(mainorder.PriceVND);
                        double Deposit = Convert.ToDouble(mainorder.Deposit);

                        double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                     + IsFastDeliveryPrice + isfastprice + pricenvd;

                        MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
                            IsCheckProductPrice.ToString(),
                            IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());

                        var accChangeData = AccountController.GetByUsername(username_current);
                        if (accChangeData != null)
                        {
                            if (status == 2)
                            {

                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                               + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);


                            }
                            if (checkIsChinaCome == true)
                            {
                                int MainorderID = mainorder.ID;
                                //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                                var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
                                if (smallpackages.Count > 0)
                                {
                                    bool isChuaVekhoTQ = true;
                                    var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                    var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status >= 3).ToList();
                                    var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status >= 3).ToList();
                                    double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                    if (che >= sp_main.Count)
                                    {
                                        isChuaVekhoTQ = false;
                                    }
                                    if (isChuaVekhoTQ == false)
                                    {
                                        MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(mainorder.UID), 7);
                                    }
                                }
                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                                   " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);
                            }
                        }

                        return "1";
                    }
                    else
                    {
                        var transportation = TransportationOrderController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                        if (transportation != null)
                        {
                            int tID = transportation.ID;
                            int warehouseFrom = Convert.ToInt32(transportation.WarehouseFromID);
                            int warehouse = Convert.ToInt32(transportation.WarehouseID);
                            int shipping = Convert.ToInt32(transportation.ShippingTypeID);

                            bool checkIsChinaCome = true;
                            double totalweight = 0;
                            var packages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (packages.Count > 0)
                            {
                                foreach (var p in packages)
                                {
                                    if (p.Status < 2)
                                        checkIsChinaCome = false;

                                    double totalWeightCN = Convert.ToDouble(p.Weight);
                                    double totalWeightTT = 0;
                                    double pDai = Convert.ToDouble(p.Length);
                                    double pRong = Convert.ToDouble(p.Width);
                                    double pCao = Convert.ToDouble(p.Height);
                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                    {
                                        totalWeightTT = (pDai * pRong * pCao) / 6000;
                                    }
                                    if (totalWeightCN > totalWeightTT)
                                    {
                                        totalweight += totalWeightCN;
                                    }
                                    else
                                    {
                                        totalweight += totalWeightTT;
                                    }
                                }
                            }
                            var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));
                            double returnprice = 0;
                            double pricePerWeight = 0;
                            double finalPriceOfPackage = 0;
                            var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                                warehouseFrom, warehouse, shipping, true);
                            if (fee.Count > 0)
                            {
                                foreach (var f in fee)
                                {
                                    if (totalweight > f.WeightFrom && totalweight <= f.WeightTo)
                                    {
                                        pricePerWeight = Convert.ToDouble(f.Price);
                                        returnprice = totalweight * Convert.ToDouble(f.Price);
                                        break;
                                    }
                                }
                            }
                            foreach (var item in packages)
                            {
                                double compareweight = 0;
                                double compareSize = 0;

                                double weight = Convert.ToDouble(item.Weight);
                                compareweight = weight * pricePerWeight;

                                double weigthTT = 0;
                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    weigthTT = (pDai * pRong * pCao) / 6000;
                                }
                                compareSize = weigthTT * pricePerWeight;

                                if (compareweight >= compareSize)
                                {
                                    finalPriceOfPackage += compareweight;
                                    SmallPackageController.UpdateTotalPrice(item.ID, compareweight);
                                }
                                else
                                {
                                    finalPriceOfPackage += compareSize;
                                    SmallPackageController.UpdateTotalPrice(item.ID, compareSize);
                                }
                            }
                            double currency = Convert.ToDouble(transportation.Currency);
                            double totalPriceVND = finalPriceOfPackage;
                            double totalPriceCYN = 0;
                            totalPriceCYN = returnprice / currency;


                            var accChangeData = AccountController.GetByUsername(username_current);
                            if (accChangeData != null)
                            {

                                if (checkIsChinaCome == true)
                                {
                                    //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                                    var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                                    if (smallpackages.Count > 0)
                                    {
                                        bool isChuaVekhoTQ = true;
                                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status >= 3).ToList();
                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status >= 3).ToList();
                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                        if (che >= sp_main.Count)
                                        {
                                            isChuaVekhoTQ = false;
                                        }
                                        if (isChuaVekhoTQ == false)
                                        {
                                            TransportationOrderController.UpdateStatus(tID, 5, currentDate, username_current);
                                        }
                                    }
                                }
                            }
                            TransportationOrderController.UpdateTotalWeightTotalPrice(tID, totalweight, totalPriceVND, currentDate, username_current);
                            return "1";
                        }
                        else
                        {
                            return "1";
                        }
                    }
                    return "none";
                }
            }
            return "none";
        }
        [WebMethod]
        public static string UpdateLost(int packageID)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {
                int bID = Convert.ToInt32(package.BigPackageID);
                SmallPackageController.UpdateIsLost(packageID, true, 0);
                if (bID > 0)
                {
                    var big = BigPackageController.GetByID(bID);
                    if (big != null)
                    {
                        bool checkIschua = false;
                        var smalls = SmallPackageController.GetBuyBigPackageID(bID, "");
                        if (smalls.Count > 0)
                        {
                            foreach (var s in smalls)
                            {
                                if (s.Status < 3)
                                    checkIschua = true;
                            }
                            if (checkIschua == false)
                            {
                                BigPackageController.UpdateStatus(bID, 2, currentDate, username_current);
                            }
                        }
                    }
                }
                return "1";
            }
            return "none";
        }
        #endregion

        #region Class      
        //public class OrderGet
        //{
        //    public int ID { get; set; }
        //    public int MainorderID { get; set; }
        //    public int UID { get; set; }
        //    public string Username { get; set; }
        //    public double Wallet { get; set; }
        //    public string OrderShopCode { get; set; }
        //    public string BarCode { get; set; }
        //    public string TotalWeight { get; set; }
        //    public string TotalPriceVND { get; set; }
        //    public double TotalPriceVNDNum { get; set; }
        //    public int Status { get; set; }
        //    public string Fullname { get; set; }
        //    public string Email { get; set; }
        //    public string Phone { get; set; }
        //    public string Address { get; set; }
        //    public string Kiemdem { get; set; }
        //    public string Donggo { get; set; }
        //} 
        public class PackageAll
        {
            public int PackageAllType { get; set; }
            public string OrderCode { get; set; }
            public int PackageGetCount { get; set; }
            public List<OrderGet> listPackageGet { get; set; }
        }
        public class OrderGet
        {
            public int ID { get; set; }
            public int MainorderID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public string OrderShopCode { get; set; }
            public string BarCode { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPriceVND { get; set; }
            public double TotalPriceVNDNum { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public string Soloaisanpham { get; set; }
            public string Soluongsanpham { get; set; }
            public int Status { get; set; }
            public int BigPackageID { get; set; }
            public List<tbl_BigPackage> ListBig { get; set; }
            public int IsTemp { get; set; }
            public int IsThatlac { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }
        }
        public class BigPackageItem
        {
            public int BigpackageID { get; set; }
            public string BigpackageCode { get; set; }
            public int BigpackageSmallPackageCount { get; set; }
            public int BigpackageType { get; set; }
            public List<smallpackageitem> Smallpackages { get; set; }
        }
        public class smallpackageitem
        {
            public int ID { get; set; }
            public string OrderType { get; set; }
            public int MainorderID { get; set; }
            public int TransportationID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public string OrderShopCode { get; set; }
            public string BarCode { get; set; }
            public double Weight { get; set; }

            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public string Soloaisanpham { get; set; }
            public string Soluongsanpham { get; set; }
            public int Status { get; set; }
            public int BigPackageID { get; set; }
            public bool IsTemp { get; set; }
            public bool IsThatlac { get; set; }
            public bool IsVCH { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }
            public double dai { get; set; }
            public double rong { get; set; }
            public double cao { get; set; }
            public string IMG { get; set; }
            public string Note { get; set; }
        }
        #endregion
    }
}