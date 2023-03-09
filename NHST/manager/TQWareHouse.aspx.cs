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
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;

namespace NHST.manager
{
    public partial class TQWareHouse : System.Web.UI.Page
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
                    if (ac.RoleID != 4 && ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
                LoadDDL();
            }
        }
        public void LoadDDL()
        {
            var bs = BigPackageController.GetAllWithStatus(1);
            ddlBigpackage.Items.Clear();
            ddlBigpackage.Items.Insert(0, new ListItem("Chọn bao lớn", "0"));
            if (bs.Count > 0)
            {
                foreach (var b in bs)
                {
                    ListItem listitem = new ListItem(b.PackageCode, b.ID.ToString());
                    ddlBigpackage.Items.Add(listitem);
                }
            }
            ddlBigpackage.DataBind();
        }
        [WebMethod]
        public static string GetCode1(string barcode)
        {
            DateTime currentDate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 4)
                    {
                        var packages = SmallPackageController.GetListByOrderTransactionCode(barcode.Trim());
                        if (packages.Count > 0)
                        {
                            BigPackOut bo = new BigPackOut();
                            bo.BigPackOutType = 0;
                            List<PackageAll> palls = new List<PackageAll>();
                            PackageAll pa = new PackageAll();
                            pa.PackageAllType = 0;
                            pa.PackageGetCount = packages.Count;
                            List<OrderGet> og = new List<OrderGet>();
                            foreach (var package in packages)
                            {
                                OrderGet o = new OrderGet();
                                if (package.Status == 0)
                                {
                                    SmallPackageController.UpdateStatus(package.ID, 0, currentDate, username);
                                }
                                else
                                {
                                    SmallPackageController.UpdateStatus(package.ID, 2, currentDate, username);
                                }

                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                                if (mainorder != null)
                                {
                                    int MainorderID = mainorder.ID;
                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
                                    if (smallpackages.Count > 0)
                                    {
                                        bool isChuaVekhoTQ = true;
                                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                        if (che >= sp_main.Count)
                                        {
                                            isChuaVekhoTQ = false;
                                        }
                                        if (isChuaVekhoTQ == false)
                                        {
                                            MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(mainorder.UID), 6);
                                        }
                                    }


                                    o.ID = package.ID;
                                    o.OrderType = "Đơn hàng mua hộ";
                                    o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                    o.BarCode = package.OrderTransactionCode;
                                    o.TotalWeight = package.Weight.ToString();
                                    o.Status = Convert.ToInt32(package.Status);
                                    int mainOrderID = Convert.ToInt32(package.MainOrderID);
                                    o.MainorderID = mainOrderID;
                                    o.TransportationID = 0;
                                    o.OrderShopCode = mainorder.MainOrderCode;
                                    var orders = OrderController.GetByMainOrderID(mainOrderID);
                                    o.Soloaisanpham = orders.Count.ToString();
                                    double totalProductQuantity = 0;
                                    if (orders.Count > 0)
                                    {
                                        foreach (var p in orders)
                                        {
                                            totalProductQuantity += Convert.ToDouble(p.quantity);
                                        }
                                    }
                                    o.Soluongsanpham = totalProductQuantity.ToString();
                                    if (mainorder.IsCheckProduct == true)
                                        o.Kiemdem = "Có";
                                    else
                                        o.Kiemdem = "Không";
                                    if (mainorder.IsPacked == true)
                                        o.Donggo = "Có";
                                    else
                                        o.Donggo = "Không";

                                    var listb = BigPackageController.GetAllWithStatus(1);
                                    if (listb.Count > 0)
                                    {
                                        o.ListBig = listb;
                                    }
                                    o.IsTemp = 0;

                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (package.Length != null)
                                    {
                                        dai = Convert.ToDouble(package.Length);
                                    }
                                    if (package.Width != null)
                                    {
                                        rong = Convert.ToDouble(package.Width);
                                    }
                                    if (package.Height != null)
                                    {
                                        cao = Convert.ToDouble(package.Height);
                                    }
                                    o.dai = dai;
                                    o.rong = rong;
                                    o.cao = cao;


                                    og.Add(o);
                                }
                                else
                                {
                                    var orderTransportation = TransportationOrderController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                                    if (orderTransportation != null)
                                    {
                                        int tID = orderTransportation.ID;
                                        var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                                        if (smallpackages.Count > 0)
                                        {
                                            bool isChuaVekhoTQ = true;
                                            var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                            var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                            var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                            double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                            if (che >= sp_main.Count)
                                            {
                                                isChuaVekhoTQ = false;
                                            }
                                            if (isChuaVekhoTQ == false)
                                            {
                                                TransportationOrderController.UpdateStatus(tID, 4, currentDate, username);
                                            }
                                        }
                                        o.ID = package.ID;
                                        o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                        o.BarCode = package.OrderTransactionCode;
                                        o.TotalWeight = package.Weight.ToString();
                                        o.Status = Convert.ToInt32(package.Status);
                                        o.MainorderID = 0;
                                        o.TransportationID = tID;
                                        o.OrderType = "Đơn hàng VC hộ";
                                        o.OrderShopCode = "";
                                        o.Soloaisanpham = "";
                                        o.Soluongsanpham = "";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        og.Add(o);
                                    }
                                    else
                                    {
                                        o.ID = package.ID;
                                        o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                        o.BarCode = package.OrderTransactionCode;
                                        o.TotalWeight = package.Weight.ToString();
                                        o.Status = Convert.ToInt32(package.Status);
                                        o.MainorderID = 0;
                                        o.TransportationID = 0;
                                        o.OrderType = "Chưa xác định";
                                        o.OrderShopCode = "";
                                        o.Soloaisanpham = "";
                                        o.Soluongsanpham = "";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        double dai = 0;
                                        double rong = 0;
                                        double cao = 0;
                                        if (package.Length != null)
                                        {
                                            dai = Convert.ToDouble(package.Length);
                                        }
                                        if (package.Width != null)
                                        {
                                            rong = Convert.ToDouble(package.Width);
                                        }
                                        if (package.Height != null)
                                        {
                                            cao = Convert.ToDouble(package.Height);
                                        }
                                        o.dai = dai;
                                        o.rong = rong;
                                        o.cao = cao;
                                        og.Add(o);
                                    }
                                }
                            }
                            pa.listPackageGet = og;
                            palls.Add(pa);
                            bo.Pall = palls;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bo);
                        }
                        else
                        {
                            var listorders = MainOrderController.GetListByMainOrderCode(barcode);
                            BigPackOut bo = new BigPackOut();
                            bo.BigPackOutType = 1;
                            if (listorders.Count > 0)
                            {
                                List<PackageAll> palls = new List<PackageAll>();
                                foreach (var order in listorders)
                                {
                                    #region Lấy tất cả các cục hiện có trong đơn
                                    int MainOrderID = order.ID;
                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                    PackageAll pa = new PackageAll();
                                    pa.MainOrderID = MainOrderID;
                                    pa.PackageAllType = 1;
                                    pa.PackageGetCount = smallpackages.Count;
                                    List<OrderGet> og = new List<OrderGet>();
                                    if (smallpackages.Count > 0)
                                    {
                                        foreach (var s in smallpackages)
                                        {
                                            OrderGet o = new OrderGet();
                                            o.OrderType = "Đơn hàng mua hộ";
                                            o.ID = s.ID;
                                            o.BigPackageID = Convert.ToInt32(s.BigPackageID);
                                            o.BarCode = s.OrderTransactionCode;
                                            o.TotalWeight = s.Weight.ToString();
                                            o.Status = Convert.ToInt32(s.Status);
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.TransportationID = 0;
                                            o.OrderShopCode = order.MainOrderCode;

                                            var orders = OrderController.GetByMainOrderID(MainOrderID);
                                            o.Soloaisanpham = orders.Count.ToString();
                                            double totalProductQuantity = 0;
                                            if (orders.Count > 0)
                                            {
                                                foreach (var p in orders)
                                                {
                                                    totalProductQuantity += Convert.ToDouble(p.quantity);
                                                }
                                            }
                                            o.Soluongsanpham = totalProductQuantity.ToString();
                                            if (order.IsCheckProduct == true)
                                                o.Kiemdem = "Có";
                                            else
                                                o.Kiemdem = "Không";
                                            if (order.IsPacked == true)
                                                o.Donggo = "Có";
                                            else
                                                o.Donggo = "Không";

                                            var listb = BigPackageController.GetAllWithStatus(1);
                                            if (listb.Count > 0)
                                            {
                                                o.ListBig = listb;
                                            }
                                            o.IsTemp = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;
                                            if (s.Length != null)
                                            {
                                                dai = Convert.ToDouble(s.Length);
                                            }
                                            if (s.Width != null)
                                            {
                                                rong = Convert.ToDouble(s.Width);
                                            }
                                            if (s.Height != null)
                                            {
                                                cao = Convert.ToDouble(s.Height);
                                            }
                                            o.dai = dai;
                                            o.rong = rong;
                                            o.cao = cao;
                                            og.Add(o);
                                        }

                                    }
                                    #endregion
                                    #region tạo cục tạm
                                    //string temp = "temp_" + PJUtils.GetRandomStringByDateTime();
                                    //OrderGet o = new OrderGet();
                                    //o.ID = 0;
                                    //o.BigPackageID = 0;
                                    //o.BarCode = temp;
                                    //o.TotalWeight = "0";
                                    //o.Status = 1;
                                    //int mainOrderID = Convert.ToInt32(order.ID);
                                    //o.MainorderID = mainOrderID;
                                    //var orders = OrderController.GetByMainOrderID(mainOrderID);
                                    //o.Soloaisanpham = orders.Count.ToString();
                                    //double totalProductQuantity = 0;
                                    //if (orders.Count > 0)
                                    //{
                                    //    foreach (var p in orders)
                                    //    {
                                    //        totalProductQuantity += Convert.ToDouble(p.quantity);
                                    //    }
                                    //}
                                    //o.Soluongsanpham = totalProductQuantity.ToString();
                                    //if (order.IsCheckProduct == true)
                                    //    o.Kiemdem = "Có";
                                    //else
                                    //    o.Kiemdem = "Không";
                                    //if (order.IsPacked == true)
                                    //    o.Donggo = "Có";
                                    //else
                                    //    o.Donggo = "Không";

                                    //var listb = BigPackageController.GetAllNotHuy();
                                    //if (listb.Count > 0)
                                    //{
                                    //    o.ListBig = listb;
                                    //}
                                    //o.IsTemp = 1;
                                    #endregion
                                    pa.listPackageGet = og;
                                    palls.Add(pa);

                                }
                                bo.Pall = palls;
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bo);
                            //var order = MainOrderController.GetByMainOrderCode(barcode);
                            //if (order != null)
                            //{

                            //}

                        }
                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string GetCode(string barcode)
        {
            DateTime currentDate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 4)
                    {
                        var packages = SmallPackageController.GetListByOrderTransactionCode(barcode.Trim());
                        if (packages.Count > 0)
                        {
                            BigPackOut bo = new BigPackOut();
                            bo.BigPackOutType = 0;
                            List<PackageAll> palls = new List<PackageAll>();
                            PackageAll pa = new PackageAll();
                            pa.PackageAllType = 0;
                            pa.PackageGetCount = packages.Count;
                            List<OrderGet> og = new List<OrderGet>();
                            foreach (var package in packages)
                            {
                                OrderGet o = new OrderGet();
                                if (package.Status == 0)
                                {
                                    SmallPackageController.UpdateStatus(package.ID, 0, currentDate, username);
                                }
                                else
                                {
                                    SmallPackageController.UpdateStatus(package.ID, 2, currentDate, username);
                                }

                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                                if (mainorder != null)
                                {
                                    string noinhan = "";
                                    int receivePlace = Convert.ToInt32(mainorder.ReceivePlace);
                                    var warehouse = WarehouseController.GetByID(receivePlace);
                                    if (warehouse != null)
                                    {
                                        noinhan = warehouse.WareHouseName;
                                    }
                                    int MainorderID = mainorder.ID;
                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
                                    if (smallpackages.Count > 0)
                                    {
                                        bool isChuaVekhoTQ = true;
                                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                        if (che >= sp_main.Count)
                                        {
                                            isChuaVekhoTQ = false;
                                        }
                                        if (isChuaVekhoTQ == false)
                                        {
                                            MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(mainorder.UID), 6);

                                            var setNoti = SendNotiEmailController.GetByID(8);
                                            if (setNoti != null)
                                            {
                                                var acc = AccountController.GetByID(mainorder.UID.Value);
                                                if (acc != null)
                                                {
                                                    if (setNoti.IsSentNotiUser == true)
                                                    {
                                                        NotificationsController.Inser(acc.ID,
                                                              acc.Username, MainorderID,
                                                              "Hàng của đơn hàng " + MainorderID + " đã về kho TQ.", 1,
                                                              currentDate, username, false);
                                                    }

                                                    if (setNoti.IsSendEmailUser == true)
                                                    {
                                                        try
                                                        {
                                                            PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", acc.Email,
                                                                "Thông báo tại Nam Trung.", "Hàng của đơn hàng " + MainorderID + " đã về kho TQ.", "");
                                                        }
                                                        catch { }
                                                    }
                                                }

                                            }
                                        }
                                    }


                                    o.ID = package.ID;
                                    o.OrderType = "Đơn hàng mua hộ";
                                    o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                    o.BarCode = package.OrderTransactionCode;
                                    o.TotalWeight = package.Weight.ToString();
                                    o.Status = Convert.ToInt32(package.Status);
                                    int mainOrderID = Convert.ToInt32(package.MainOrderID);
                                    o.MainorderID = mainOrderID;
                                    o.TransportationID = 0;
                                    o.IMG = package.ListIMG;
                                    o.Note = package.Description;
                                    o.Noinhan = noinhan;
                                    //if (!string.IsNullOrEmpty(package.Description))
                                    //    o.Note = package.Description;
                                    //else
                                    //    o.Note = string.Empty;
                                    o.OrderShopCode = mainorder.MainOrderCode;
                                    var orders = OrderController.GetByMainOrderID(mainOrderID);
                                    o.Soloaisanpham = orders.Count.ToString();
                                    double totalProductQuantity = 0;
                                    if (orders.Count > 0)
                                    {
                                        foreach (var p in orders)
                                        {
                                            totalProductQuantity += Convert.ToDouble(p.quantity);
                                        }
                                    }
                                    o.Soluongsanpham = totalProductQuantity.ToString();
                                    if (mainorder.IsCheckProduct == true)
                                        o.Kiemdem = "Có";
                                    else
                                        o.Kiemdem = "Không";
                                    if (mainorder.IsPacked == true)
                                        o.Donggo = "Có";
                                    else
                                        o.Donggo = "Không";

                                    var listb = BigPackageController.GetAllWithStatus(1);
                                    if (listb.Count > 0)
                                    {
                                        o.ListBig = listb;
                                    }
                                    o.IsTemp = 0;

                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (package.Length != null)
                                    {
                                        dai = Convert.ToDouble(package.Length);
                                    }
                                    if (package.Width != null)
                                    {
                                        rong = Convert.ToDouble(package.Width);
                                    }
                                    if (package.Height != null)
                                    {
                                        cao = Convert.ToDouble(package.Height);
                                    }
                                    o.dai = dai;
                                    o.rong = rong;
                                    o.cao = cao;


                                    og.Add(o);
                                }
                                else
                                {
                                    var orderTransportation = TransportationOrderController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                                    if (orderTransportation != null)
                                    {
                                        int tID = orderTransportation.ID;
                                        var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                                        if (smallpackages.Count > 0)
                                        {
                                            bool isChuaVekhoTQ = true;
                                            var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                            var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                            var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                            double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                            if (che >= sp_main.Count)
                                            {
                                                isChuaVekhoTQ = false;
                                            }
                                            if (isChuaVekhoTQ == false)
                                            {
                                                TransportationOrderController.UpdateStatus(tID, 4, currentDate, username);

                                                var setNoti = SendNotiEmailController.GetByID(16);
                                                if (setNoti != null)
                                                {
                                                    var acc = AccountController.GetByID(orderTransportation.UID.Value);
                                                    if (acc != null)
                                                    {
                                                        if (setNoti.IsSentNotiUser == true)
                                                        {
                                                            NotificationsController.Inser(acc.ID,
                                                                  acc.Username, tID,
                                                                  "<a href=\"/chi-tiet-don-hang-van-chuyen-ho/" + tID + "\" target=\"_blank\">Đơn hàng vận chuyển hộ " + tID + " đã về kho TQ.</a>", 1,
                                                                  currentDate, username, false);
                                                        }

                                                        if (setNoti.IsSendEmailUser == true)
                                                        {
                                                            try
                                                            {
                                                                PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", acc.Email,
                                                                    "Thông báo tại Nam Trung.", "Đơn hàng vận chuyển hộ " + tID + " đã về kho TQ.", "");
                                                            }
                                                            catch { }
                                                        }
                                                    }

                                                }

                                            }
                                        }
                                        o.ID = package.ID;
                                        o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                        o.BarCode = package.OrderTransactionCode;
                                        o.TotalWeight = package.Weight.ToString();
                                        o.Status = Convert.ToInt32(package.Status);
                                        o.MainorderID = 0;
                                        o.TransportationID = tID;
                                        o.OrderType = "Đơn hàng VC hộ";
                                        o.OrderShopCode = "";
                                        o.Soloaisanpham = "";
                                        o.Soluongsanpham = "";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        if (!string.IsNullOrEmpty(package.ListIMG))
                                            o.IMG = package.ListIMG;
                                        else
                                            o.IMG = string.Empty;
                                        o.Note = package.Description;
                                        //if (!string.IsNullOrEmpty(package.Description))
                                        //    o.Note = package.Description;
                                        //else
                                        //    o.Note = string.Empty;
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        og.Add(o);
                                    }
                                    else
                                    {
                                        o.ID = package.ID;
                                        o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                        o.BarCode = package.OrderTransactionCode;
                                        o.TotalWeight = package.Weight.ToString();
                                        o.Status = Convert.ToInt32(package.Status);
                                        o.MainorderID = 0;
                                        o.TransportationID = 0;
                                        o.OrderType = "Chưa xác định";
                                        o.OrderShopCode = "";
                                        o.Soloaisanpham = "";
                                        o.IMG = package.ListIMG;
                                        o.Note = package.Description;
                                        //if (!string.IsNullOrEmpty(package.Description))
                                        //    o.Note = package.Description;
                                        //else
                                        //    o.Note = string.Empty;
                                        o.Soluongsanpham = "";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        double dai = 0;
                                        double rong = 0;
                                        double cao = 0;
                                        if (package.Length != null)
                                        {
                                            dai = Convert.ToDouble(package.Length);
                                        }
                                        if (package.Width != null)
                                        {
                                            rong = Convert.ToDouble(package.Width);
                                        }
                                        if (package.Height != null)
                                        {
                                            cao = Convert.ToDouble(package.Height);
                                        }
                                        o.dai = dai;
                                        o.rong = rong;
                                        o.cao = cao;
                                        og.Add(o);
                                    }
                                }
                            }
                            pa.listPackageGet = og;
                            palls.Add(pa);
                            bo.Pall = palls;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bo);
                        }
                        else
                        {
                            var listorders = MainOrderController.GetListByMainOrderCode(barcode);
                            BigPackOut bo = new BigPackOut();
                            bo.BigPackOutType = 1;
                            if (listorders.Count > 0)
                            {
                                List<PackageAll> palls = new List<PackageAll>();
                                foreach (var order in listorders)
                                {
                                    #region Lấy tất cả các cục hiện có trong đơn
                                    int MainOrderID = order.ID;
                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                    PackageAll pa = new PackageAll();
                                    pa.MainOrderID = MainOrderID;
                                    pa.PackageAllType = 1;
                                    pa.PackageGetCount = smallpackages.Count;
                                    List<OrderGet> og = new List<OrderGet>();
                                    if (smallpackages.Count > 0)
                                    {
                                        foreach (var s in smallpackages)
                                        {
                                            OrderGet o = new OrderGet();
                                            o.OrderType = "Đơn hàng mua hộ";
                                            o.ID = s.ID;
                                            o.BigPackageID = Convert.ToInt32(s.BigPackageID);
                                            o.BarCode = s.OrderTransactionCode;
                                            o.TotalWeight = s.Weight.ToString();
                                            o.Status = Convert.ToInt32(s.Status);
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.TransportationID = 0;
                                            o.OrderShopCode = order.MainOrderCode;
                                            if (!string.IsNullOrEmpty(s.ListIMG))
                                                o.IMG = s.ListIMG;
                                            else
                                                o.IMG = string.Empty;
                                            o.Note = s.Description;
                                            //if (!string.IsNullOrEmpty(s.Description))
                                            //    o.Note = s.Description;
                                            //else
                                            //    o.Note = string.Empty;
                                            var orders = OrderController.GetByMainOrderID(MainOrderID);
                                            o.Soloaisanpham = orders.Count.ToString();
                                            double totalProductQuantity = 0;
                                            if (orders.Count > 0)
                                            {
                                                foreach (var p in orders)
                                                {
                                                    totalProductQuantity += Convert.ToDouble(p.quantity);
                                                }
                                            }
                                            o.Soluongsanpham = totalProductQuantity.ToString();
                                            if (order.IsCheckProduct == true)
                                                o.Kiemdem = "Có";
                                            else
                                                o.Kiemdem = "Không";
                                            if (order.IsPacked == true)
                                                o.Donggo = "Có";
                                            else
                                                o.Donggo = "Không";

                                            var listb = BigPackageController.GetAllWithStatus(1);
                                            if (listb.Count > 0)
                                            {
                                                o.ListBig = listb;
                                            }
                                            o.IsTemp = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;
                                            if (s.Length != null)
                                            {
                                                dai = Convert.ToDouble(s.Length);
                                            }
                                            if (s.Width != null)
                                            {
                                                rong = Convert.ToDouble(s.Width);
                                            }
                                            if (s.Height != null)
                                            {
                                                cao = Convert.ToDouble(s.Height);
                                            }
                                            o.dai = dai;
                                            o.rong = rong;
                                            o.cao = cao;
                                            og.Add(o);
                                        }

                                    }
                                    #endregion
                                    #region tạo cục tạm
                                    //string temp = "temp_" + PJUtils.GetRandomStringByDateTime();
                                    //OrderGet o = new OrderGet();
                                    //o.ID = 0;
                                    //o.BigPackageID = 0;
                                    //o.BarCode = temp;
                                    //o.TotalWeight = "0";
                                    //o.Status = 1;
                                    //int mainOrderID = Convert.ToInt32(order.ID);
                                    //o.MainorderID = mainOrderID;
                                    //var orders = OrderController.GetByMainOrderID(mainOrderID);
                                    //o.Soloaisanpham = orders.Count.ToString();
                                    //double totalProductQuantity = 0;
                                    //if (orders.Count > 0)
                                    //{
                                    //    foreach (var p in orders)
                                    //    {
                                    //        totalProductQuantity += Convert.ToDouble(p.quantity);
                                    //    }
                                    //}
                                    //o.Soluongsanpham = totalProductQuantity.ToString();
                                    //if (order.IsCheckProduct == true)
                                    //    o.Kiemdem = "Có";
                                    //else
                                    //    o.Kiemdem = "Không";
                                    //if (order.IsPacked == true)
                                    //    o.Donggo = "Có";
                                    //else
                                    //    o.Donggo = "Không";

                                    //var listb = BigPackageController.GetAllNotHuy();
                                    //if (listb.Count > 0)
                                    //{
                                    //    o.ListBig = listb;
                                    //}
                                    //o.IsTemp = 1;
                                    #endregion
                                    pa.listPackageGet = og;
                                    palls.Add(pa);

                                }
                                bo.Pall = palls;
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bo);
                            //var order = MainOrderController.GetByMainOrderCode(barcode);
                            //if (order != null)
                            //{

                            //}

                        }
                    }
                }
            }
            return "none";
        }

        [WebMethod]
        public static string CheckOrderShopCode(string ordershopcode, int mainorderid)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);
                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 4)
                    {

                        if (HttpContext.Current.Session["userLoginSystem"] != null)
                        {
                            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                            var user = AccountController.GetByUsername(username);
                            if (user != null)
                            {
                                int userRole = Convert.ToInt32(user.RoleID);

                                if (userRole == 0 || userRole == 2 || userRole == 4)
                                {
                                    if (!string.IsNullOrEmpty(ordershopcode))
                                    {
                                        var order = MainOrderController.GetByMainOrderCodeAndID(mainorderid, ordershopcode);
                                        if (order != null)
                                        {
                                            int MainOrderID = order.ID;
                                            string temp = "";
                                            temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                            string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);

                                            #region Lấy tất cả các cục hiện có trong đơn

                                            var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                            PackageAll pa = new PackageAll();
                                            pa.PackageAllType = 0;
                                            pa.PackageGetCount = smallpackages.Count;
                                            List<OrderGet> og = new List<OrderGet>();

                                            OrderGet o = new OrderGet();
                                            o.ID = packageID.ToInt(0);
                                            o.OrderType = "Đơn hàng mua hộ";
                                            o.BigPackageID = 0;
                                            o.BarCode = temp;
                                            o.TotalWeight = "0";
                                            o.Status = 1;
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.TransportationID = 0;
                                            o.OrderShopCode = order.MainOrderCode;
                                            var orders = OrderController.GetByMainOrderID(MainOrderID);
                                            o.Soloaisanpham = orders.Count.ToString();
                                            double totalProductQuantity = 0;
                                            if (orders.Count > 0)
                                            {
                                                foreach (var p in orders)
                                                {
                                                    totalProductQuantity += Convert.ToDouble(p.quantity);
                                                }
                                            }
                                            o.Soluongsanpham = totalProductQuantity.ToString();
                                            if (order.IsCheckProduct == true)
                                                o.Kiemdem = "Có";
                                            else
                                                o.Kiemdem = "Không";
                                            if (order.IsPacked == true)
                                                o.Donggo = "Có";
                                            else
                                                o.Donggo = "Không";

                                            var listb = BigPackageController.GetAllWithStatus(1);
                                            if (listb.Count > 0)
                                            {
                                                o.ListBig = listb;
                                            }
                                            o.IsTemp = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;
                                            o.dai = dai;
                                            o.rong = rong;
                                            o.cao = cao;
                                            og.Add(o);
                                            #endregion
                                            pa.listPackageGet = og;

                                            if (smallpackages.Count > 0)
                                            {
                                                bool isChuaVekhoTQ = true;
                                                var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                                var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                                var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                                double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                                if (che >= sp_main.Count)
                                                {
                                                    isChuaVekhoTQ = false;
                                                }
                                                if (isChuaVekhoTQ == false)
                                                {
                                                    MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                                }
                                            }
                                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                                            return serializer.Serialize(pa);
                                        }
                                        else
                                            return "none";
                                    }
                                    else
                                    {
                                        #region Lấy tất cả các cục hiện có trong đơn
                                        int MainOrderID = 0;
                                        string temp = "";
                                        temp = "00-" + PJUtils.GetRandomStringByDateTime();
                                        string packageID = SmallPackageController.InsertAll(MainOrderID, 0, temp, "", 0, 0, 0, 1, true, false, 0,
                                            DateTime.Now, username);
                                        PackageAll pa = new PackageAll();
                                        pa.PackageAllType = 0;
                                        pa.PackageGetCount = 0;
                                        List<OrderGet> og = new List<OrderGet>();
                                        //string temp = "temp-" + PJUtils.GetRandomStringByDateTime();


                                        OrderGet o = new OrderGet();
                                        o.ID = packageID.ToInt(0);
                                        o.OrderType = "Chưa xác định";
                                        o.BigPackageID = 0;
                                        o.BarCode = temp;
                                        o.TotalWeight = "0";
                                        o.Status = 1;
                                        int mainOrderID = Convert.ToInt32(MainOrderID);
                                        o.MainorderID = mainOrderID;
                                        o.TransportationID = 0;
                                        o.OrderShopCode = "";
                                        var orders = OrderController.GetByMainOrderID(MainOrderID);
                                        o.Soloaisanpham = orders.Count.ToString();
                                        double totalProductQuantity = 0;
                                        if (orders.Count > 0)
                                        {
                                            foreach (var p in orders)
                                            {
                                                totalProductQuantity += Convert.ToDouble(p.quantity);
                                            }
                                        }
                                        o.Soluongsanpham = totalProductQuantity.ToString();
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        double dai = 0;
                                        double rong = 0;
                                        double cao = 0;
                                        o.dai = dai;
                                        o.rong = rong;
                                        o.cao = cao;
                                        og.Add(o);
                                        #endregion
                                        pa.listPackageGet = og;


                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        return serializer.Serialize(pa);
                                    }
                                }
                                else
                                    return "none";

                            }
                            else
                            {
                                return "none";
                            }

                        }
                        else
                        {
                            return "none";
                        }
                    }
                    else
                        return "none";

                }
                else
                {
                    return "none";
                }

            }
            else
            {
                return "none";
            }

        }
        [WebMethod]
        public static string CheckOrderShopCodeNew1(string ordershopcode, string ordertransaction)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);
                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 4)
                    {

                        if (HttpContext.Current.Session["userLoginSystem"] != null)
                        {
                            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                            var user = AccountController.GetByUsername(username);
                            if (user != null)
                            {
                                int userRole = Convert.ToInt32(user.RoleID);

                                if (userRole == 0 || userRole == 2 || userRole == 4)
                                {
                                    if (!string.IsNullOrEmpty(ordershopcode))
                                    {
                                        var order = MainOrderController.GetByMainOrderCode(ordershopcode);
                                        if (order != null)
                                        {
                                            int MainOrderID = order.ID;
                                            string temp = "";
                                            if (!string.IsNullOrEmpty(ordertransaction))
                                                temp = ordertransaction;
                                            else
                                                temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                            string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);

                                            #region Lấy tất cả các cục hiện có trong đơn

                                            var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                            PackageAll pa = new PackageAll();
                                            pa.PackageAllType = 0;
                                            pa.PackageGetCount = smallpackages.Count;
                                            List<OrderGet> og = new List<OrderGet>();

                                            OrderGet o = new OrderGet();
                                            o.ID = packageID.ToInt(0);
                                            o.OrderType = "Đơn hàng mua hộ";
                                            o.BigPackageID = 0;
                                            o.BarCode = temp;
                                            o.TotalWeight = "0";
                                            o.Status = 1;
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.OrderShopCode = order.MainOrderCode;
                                            var orders = OrderController.GetByMainOrderID(MainOrderID);
                                            o.Soloaisanpham = orders.Count.ToString();
                                            double totalProductQuantity = 0;
                                            if (orders.Count > 0)
                                            {
                                                foreach (var p in orders)
                                                {
                                                    totalProductQuantity += Convert.ToDouble(p.quantity);
                                                }
                                            }
                                            o.Soluongsanpham = totalProductQuantity.ToString();
                                            if (order.IsCheckProduct == true)
                                                o.Kiemdem = "Có";
                                            else
                                                o.Kiemdem = "Không";
                                            if (order.IsPacked == true)
                                                o.Donggo = "Có";
                                            else
                                                o.Donggo = "Không";

                                            var listb = BigPackageController.GetAllWithStatus(1);
                                            if (listb.Count > 0)
                                            {
                                                o.ListBig = listb;
                                            }
                                            o.IsTemp = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;

                                            o.dai = dai;
                                            o.rong = rong;
                                            o.cao = cao;
                                            og.Add(o);
                                            #endregion
                                            pa.listPackageGet = og;

                                            if (smallpackages.Count > 0)
                                            {
                                                bool isChuaVekhoTQ = true;
                                                var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                                var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                                var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                                double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                                if (che >= sp_main.Count)
                                                {
                                                    isChuaVekhoTQ = false;
                                                }
                                                if (isChuaVekhoTQ == false)
                                                {
                                                    MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                                }
                                            }
                                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                                            return serializer.Serialize(pa);
                                        }
                                        else
                                            return "noteexistordercode";
                                    }
                                    else
                                    {
                                        #region Lấy tất cả các cục hiện có trong đơn
                                        int MainOrderID = 0;
                                        string temp = "";
                                        if (!string.IsNullOrEmpty(ordertransaction))
                                            temp = ordertransaction;
                                        else
                                            temp = "00-" + PJUtils.GetRandomStringByDateTime();
                                        string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);


                                        PackageAll pa = new PackageAll();
                                        pa.PackageAllType = 0;
                                        pa.PackageGetCount = 0;
                                        List<OrderGet> og = new List<OrderGet>();
                                        //string temp = "temp-" + PJUtils.GetRandomStringByDateTime();
                                        OrderGet o = new OrderGet();
                                        o.ID = packageID.ToInt(0);
                                        o.OrderType = "Chưa xác định";
                                        o.BigPackageID = 0;
                                        o.BarCode = temp;
                                        o.TotalWeight = "0";
                                        o.Status = 1;
                                        int mainOrderID = Convert.ToInt32(MainOrderID);
                                        o.MainorderID = mainOrderID;
                                        o.TransportationID = 0;
                                        o.OrderShopCode = "";
                                        //var orders = OrderController.GetByMainOrderID(MainOrderID);

                                        //double totalProductQuantity = 0;
                                        //if (orders.Count > 0)
                                        //{
                                        //    foreach (var p in orders)
                                        //    {
                                        //        totalProductQuantity += Convert.ToDouble(p.quantity);
                                        //    }
                                        //}
                                        o.Soloaisanpham = "0";
                                        o.Soluongsanpham = "0";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        double dai = 0;
                                        double rong = 0;
                                        double cao = 0;

                                        o.dai = dai;
                                        o.rong = rong;
                                        o.cao = cao;
                                        og.Add(o);
                                        #endregion
                                        pa.listPackageGet = og;
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        return serializer.Serialize(pa);
                                    }
                                }
                                else
                                    return "none";

                            }
                            else
                            {
                                return "none";
                            }

                        }
                        else
                        {
                            return "none";
                        }
                    }
                    else
                        return "none";

                }
                else
                {
                    return "none";
                }

            }
            else
            {
                return "none";
            }

        }
        [WebMethod]
        public static string CheckOrderShopCodeNew(string ordershopcode, string ordertransaction, string base64, string note)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);

                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 4)
                    {

                        if (HttpContext.Current.Session["userLoginSystem"] != null)
                        {
                            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                            var user = AccountController.GetByUsername(username);
                            if (user != null)
                            {
                                int userRole = Convert.ToInt32(user.RoleID);

                                if (userRole == 0 || userRole == 2 || userRole == 4)
                                {
                                    if (!string.IsNullOrEmpty(ordershopcode))
                                    {
                                        var order = MainOrderController.GetByMainOrderCode(ordershopcode);
                                        if (order != null)
                                        {
                                            int MainOrderID = order.ID;
                                            string temp = "";
                                            if (!string.IsNullOrEmpty(ordertransaction))
                                                temp = ordertransaction;
                                            else
                                                temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                            var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                            if (getsmallcheck.Count > 0)
                                            {
                                                return "existsmallpackage";
                                            }
                                            else
                                            {
                                                string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);
                                                SmallPackageController.UpdateNote(Convert.ToInt32(packageID), note);


                                                var package = SmallPackageController.GetByID(Convert.ToInt32(packageID));
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

                                                SmallPackageController.UpdateIMG(package.ID, link, DateTime.Now, username_check);
                                                #region Lấy tất cả các cục hiện có trong đơn

                                                var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                                PackageAll pa = new PackageAll();
                                                pa.PackageAllType = 0;
                                                pa.PackageGetCount = smallpackages.Count;
                                                List<OrderGet> og = new List<OrderGet>();

                                                OrderGet o = new OrderGet();
                                                o.ID = packageID.ToInt(0);
                                                o.OrderType = "Đơn hàng mua hộ";
                                                o.BigPackageID = 0;
                                                o.BarCode = temp;
                                                o.TotalWeight = "0";
                                                o.Status = 1;
                                                int mainOrderID = Convert.ToInt32(MainOrderID);
                                                o.MainorderID = mainOrderID;
                                                o.OrderShopCode = order.MainOrderCode;
                                                var orders = OrderController.GetByMainOrderID(MainOrderID);
                                                o.Soloaisanpham = orders.Count.ToString();
                                                double totalProductQuantity = 0;
                                                if (orders.Count > 0)
                                                {
                                                    foreach (var p in orders)
                                                    {
                                                        totalProductQuantity += Convert.ToDouble(p.quantity);
                                                    }
                                                }
                                                o.Soluongsanpham = totalProductQuantity.ToString();
                                                if (order.IsCheckProduct == true)
                                                    o.Kiemdem = "Có";
                                                else
                                                    o.Kiemdem = "Không";
                                                if (order.IsPacked == true)
                                                    o.Donggo = "Có";
                                                else
                                                    o.Donggo = "Không";

                                                var listb = BigPackageController.GetAllWithStatus(1);
                                                if (listb.Count > 0)
                                                {
                                                    o.ListBig = listb;
                                                }
                                                o.IsTemp = 0;
                                                double dai = 0;
                                                double rong = 0;
                                                double cao = 0;

                                                o.dai = dai;
                                                o.rong = rong;
                                                o.cao = cao;
                                                og.Add(o);
                                                #endregion
                                                pa.listPackageGet = og;

                                                if (smallpackages.Count > 0)
                                                {
                                                    bool isChuaVekhoTQ = true;
                                                    var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                                    var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                                    var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                                    double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                                    if (che >= sp_main.Count)
                                                    {
                                                        isChuaVekhoTQ = false;
                                                    }
                                                    if (isChuaVekhoTQ == false)
                                                    {
                                                        MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                                    }
                                                }
                                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                                return serializer.Serialize(pa);
                                            }

                                        }
                                        else
                                            return "noteexistordercode";
                                    }
                                    else
                                    {
                                        #region Lấy tất cả các cục hiện có trong đơn
                                        int MainOrderID = 0;
                                        string temp = "";
                                        if (!string.IsNullOrEmpty(ordertransaction))
                                            temp = ordertransaction;
                                        else
                                            temp = "00-" + PJUtils.GetRandomStringByDateTime();

                                        var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                        if (getsmallcheck.Count > 0)
                                        {
                                            return "existsmallpackage";
                                        }
                                        else
                                        {
                                            string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                            0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);
                                            SmallPackageController.UpdateNote(Convert.ToInt32(packageID), note);
                                            var package = SmallPackageController.GetByID(Convert.ToInt32(packageID));
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

                                            SmallPackageController.UpdateIMG(package.ID, link, DateTime.Now, username_check);
                                            PackageAll pa = new PackageAll();
                                            pa.PackageAllType = 0;
                                            pa.PackageGetCount = 0;
                                            List<OrderGet> og = new List<OrderGet>();
                                            //string temp = "temp-" + PJUtils.GetRandomStringByDateTime();
                                            OrderGet o = new OrderGet();
                                            o.ID = packageID.ToInt(0);
                                            o.OrderType = "Chưa xác định";
                                            o.BigPackageID = 0;
                                            o.BarCode = temp;
                                            o.TotalWeight = "0";
                                            o.Status = 1;
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.TransportationID = 0;
                                            o.OrderShopCode = "";
                                            //var orders = OrderController.GetByMainOrderID(MainOrderID);

                                            //double totalProductQuantity = 0;
                                            //if (orders.Count > 0)
                                            //{
                                            //    foreach (var p in orders)
                                            //    {
                                            //        totalProductQuantity += Convert.ToDouble(p.quantity);
                                            //    }
                                            //}
                                            o.Soloaisanpham = "0";
                                            o.Soluongsanpham = "0";
                                            o.Kiemdem = "Không";
                                            o.Donggo = "Không";
                                            var listb = BigPackageController.GetAllWithStatus(1);
                                            if (listb.Count > 0)
                                            {
                                                o.ListBig = listb;
                                            }
                                            o.IsTemp = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;

                                            o.dai = dai;
                                            o.rong = rong;
                                            o.cao = cao;
                                            o.Note = note;
                                            og.Add(o);
                                            #endregion
                                            pa.listPackageGet = og;
                                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                                            return serializer.Serialize(pa);
                                        }
                                    }
                                }
                                else
                                    return "none";
                            }
                            else
                            {
                                return "none";
                            }
                        }
                        else
                        {
                            return "none";
                        }
                    }
                    else
                        return "none";
                }
                else
                {
                    return "none";
                }
            }
            else
            {
                return "none";
            }
        }
        public static string CheckOrderShopCodeNew_old(string ordershopcode, string ordertransaction, string base64, string note)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);
                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 4)
                    {

                        if (HttpContext.Current.Session["userLoginSystem"] != null)
                        {
                            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                            var user = AccountController.GetByUsername(username);
                            if (user != null)
                            {
                                int userRole = Convert.ToInt32(user.RoleID);

                                if (userRole == 0 || userRole == 2 || userRole == 4)
                                {
                                    string link = "";
                                    if (!string.IsNullOrEmpty(base64))
                                    {
                                        string[] ba64 = base64.Split('|');
                                        for (int i = 0; i < ba64.Length - 1; i++)
                                        {
                                            string imageData = ba64[i];
                                            string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/smallpackageIMG/");
                                            string date = DateTime.Now.ToString("dd-MM-yyyy");
                                            string time = DateTime.Now.ToString("hh:mm tt");
                                            Page page = (Page)HttpContext.Current.Handler;
                                            //  TextBox txtCampaign = (TextBox)page.FindControl("txtCampaign");
                                            string s = i.ToString();
                                            string fileNameWitPath = path + s + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                                            string linkIMG = "/Uploads/smallpackageIMG/" + s + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
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
                                    if (!string.IsNullOrEmpty(ordershopcode))
                                    {
                                        var order = MainOrderController.GetByMainOrderCode(ordershopcode);
                                        if (order != null)
                                        {
                                            int MainOrderID = order.ID;
                                            string temp = "";
                                            if (!string.IsNullOrEmpty(ordertransaction))
                                                temp = ordertransaction;
                                            else
                                                temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                            string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);

                                            #region Lấy tất cả các cục hiện có trong đơn

                                            var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                            PackageAll pa = new PackageAll();
                                            pa.PackageAllType = 0;
                                            pa.PackageGetCount = smallpackages.Count;
                                            List<OrderGet> og = new List<OrderGet>();

                                            OrderGet o = new OrderGet();
                                            o.ID = packageID.ToInt(0);
                                            o.OrderType = "Đơn hàng mua hộ";
                                            o.BigPackageID = 0;
                                            o.BarCode = temp;
                                            o.TotalWeight = "0";
                                            o.Status = 1;
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.OrderShopCode = order.MainOrderCode;
                                            var orders = OrderController.GetByMainOrderID(MainOrderID);
                                            o.Soloaisanpham = orders.Count.ToString();
                                            double totalProductQuantity = 0;
                                            if (orders.Count > 0)
                                            {
                                                foreach (var p in orders)
                                                {
                                                    totalProductQuantity += Convert.ToDouble(p.quantity);
                                                }
                                            }
                                            o.Soluongsanpham = totalProductQuantity.ToString();
                                            if (order.IsCheckProduct == true)
                                                o.Kiemdem = "Có";
                                            else
                                                o.Kiemdem = "Không";
                                            if (order.IsPacked == true)
                                                o.Donggo = "Có";
                                            else
                                                o.Donggo = "Không";

                                            var listb = BigPackageController.GetAllWithStatus(1);
                                            if (listb.Count > 0)
                                            {
                                                o.ListBig = listb;
                                            }
                                            o.IsTemp = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;

                                            o.dai = dai;
                                            o.rong = rong;
                                            o.cao = cao;
                                            og.Add(o);
                                            #endregion
                                            pa.listPackageGet = og;

                                            if (smallpackages.Count > 0)
                                            {
                                                bool isChuaVekhoTQ = true;
                                                var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                                var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                                var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                                double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                                if (che >= sp_main.Count)
                                                {
                                                    isChuaVekhoTQ = false;
                                                }
                                                if (isChuaVekhoTQ == false)
                                                {
                                                    MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                                }
                                            }
                                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                                            return serializer.Serialize(pa);
                                        }
                                        else
                                            return "noteexistordercode";
                                    }
                                    else
                                    {
                                        #region Lấy tất cả các cục hiện có trong đơn
                                        int MainOrderID = 0;
                                        string temp = "";
                                        if (!string.IsNullOrEmpty(ordertransaction))
                                            temp = ordertransaction;
                                        else
                                            temp = "00-" + PJUtils.GetRandomStringByDateTime();
                                        string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTempAndIMG(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username, link, note);


                                        PackageAll pa = new PackageAll();
                                        pa.PackageAllType = 0;
                                        pa.PackageGetCount = 0;
                                        List<OrderGet> og = new List<OrderGet>();
                                        //string temp = "temp-" + PJUtils.GetRandomStringByDateTime();
                                        OrderGet o = new OrderGet();
                                        o.ID = packageID.ToInt(0);
                                        o.OrderType = "Chưa xác định";
                                        o.BigPackageID = 0;
                                        o.BarCode = temp;
                                        o.TotalWeight = "0";
                                        o.Status = 1;
                                        int mainOrderID = Convert.ToInt32(MainOrderID);
                                        o.MainorderID = mainOrderID;
                                        o.TransportationID = 0;
                                        o.OrderShopCode = "";
                                        o.IMG = link;
                                        o.Note = note;
                                        //var orders = OrderController.GetByMainOrderID(MainOrderID);

                                        //double totalProductQuantity = 0;
                                        //if (orders.Count > 0)
                                        //{
                                        //    foreach (var p in orders)
                                        //    {
                                        //        totalProductQuantity += Convert.ToDouble(p.quantity);
                                        //    }
                                        //}
                                        o.Soloaisanpham = "0";
                                        o.Soluongsanpham = "0";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        double dai = 0;
                                        double rong = 0;
                                        double cao = 0;

                                        o.dai = dai;
                                        o.rong = rong;
                                        o.cao = cao;
                                        og.Add(o);
                                        #endregion
                                        pa.listPackageGet = og;
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        return serializer.Serialize(pa);
                                    }
                                }
                                else
                                    return "none";

                            }
                            else
                            {
                                return "none";
                            }

                        }
                        else
                        {
                            return "none";
                        }
                    }
                    else
                        return "none";

                }
                else
                {
                    return "none";
                }

            }
            else
            {
                return "none";
            }

        }

        [WebMethod]
        public static string UpdateVekho(string barcode)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 4)
                    {

                        var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
                        if (package != null)
                        {
                            SmallPackageController.UpdateStatus(package.ID, 2, DateTime.Now, username);
                            var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                            if (mainorder != null)
                            {
                                int MainorderID = mainorder.ID;
                                var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
                                if (smallpackages.Count > 0)
                                {
                                    bool isChuaVekhoTQ = true;
                                    var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                    var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                    var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                    double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                    if (che >= sp_main.Count)
                                    {
                                        isChuaVekhoTQ = false;
                                    }
                                    if (isChuaVekhoTQ == false)
                                    {
                                        MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(mainorder.UID), 6);
                                    }
                                    return "ok";
                                }
                                else
                                {
                                    return "none";
                                }
                            }
                            else return "none";
                        }
                        else
                        {
                            return "none";
                        }
                    }
                    else
                        return "none";

                }
                else
                {
                    return "none";
                }

            }
            else
            {
                return "none";
            }
        }
        [WebMethod]
        public static string UpdateTemp(string barcode, string mainorderID)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            //SmallPackageController.InsertWithMainOrderIDAndIsTemp(mainorderID.ToInt(0), 0, barcode, "", 0, 0, 0, 2, true, currentDate, username);
            return "1";

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
        public static string UpdateQuantity_old(string barcode, string quantity, int status, int BigPackageID,
            int packageID, double dai, double rong, double cao, string ghichu)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            //var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {
                SmallPackageController.UpdateDescription(package.ID, ghichu);
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

                    SmallPackageController.UpdateWeightStatus(package.ID, quantityD, status, BigPackageID, dai, rong, cao);
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
                                if (p.Status < 2)
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

                            if (!string.IsNullOrEmpty(usercreate.FeeTQVNPerWeight))
                            {
                                double feetqvn = 0;
                                if (usercreate.FeeTQVNPerWeight.ToFloat(0) > 0)
                                {
                                    feetqvn = Convert.ToDouble(usercreate.FeeTQVNPerWeight);
                                }
                                pricePerWeight = feetqvn;
                                returnprice = totalWeight * feetqvn;
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
                                            break;
                                        }
                                    }
                                }
                            }

                            MainOrderController.UpdateTotalWeight(mainorder.ID, totalWeight.ToString(),
                           totalWeight.ToString());
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
                        //int khachhangID = Convert.ToInt32(mainorder.UID);
                        //var khachhang = AccountController.GetByID(khachhangID);
                        //double khachhangCurrency = 0;
                        //if (khachhang != null)
                        //{
                        //    if (!string.IsNullOrEmpty(khachhang.Currency))
                        //    {
                        //        if (khachhang.Currency.ToFloat(0) > 0)
                        //        {
                        //            khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                        //        }
                        //    }
                        //}
                        //if (khachhangCurrency > 0)
                        //{
                        //    currency = khachhangCurrency;
                        //}
                        //FeeWeight = returnprice * currency;
                        returnprice = finalPriceOfPackage;
                        FeeWeight = returnprice;
                        // FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
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
                                    var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                    var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                    double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                    if (che >= sp_main.Count)
                                    {
                                        isChuaVekhoTQ = false;
                                    }
                                    if (isChuaVekhoTQ == false)
                                    {
                                        MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(mainorder.UID), 6);
                                        if (mainorder.TQWarehouseDate == null)
                                            MainOrderController.UpdateTQWarehouseDate(MainorderID, Convert.ToInt32(mainorder.UID), currentDate);
                                        MainOrderRequestShipController.UpdateMainOrderStatusByMainOrderID(MainorderID, 6);
                                    }
                                }
                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                                   " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
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
                            //int khachhangID = Convert.ToInt32(transportation.UID);
                            //var khachhang = AccountController.GetByID(khachhangID);
                            //double khachhangCurrency = 0;
                            //if (khachhang != null)
                            //{
                            //    if (!string.IsNullOrEmpty(khachhang.Currency))
                            //    {
                            //        if (khachhang.Currency.ToFloat(0) > 0)
                            //        {
                            //            khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                            //        }
                            //    }
                            //}
                            //if (khachhangCurrency > 0)
                            //{
                            //    currency = khachhangCurrency;
                            //}
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
                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                        if (che >= sp_main.Count)
                                        {
                                            isChuaVekhoTQ = false;
                                        }
                                        if (isChuaVekhoTQ == false)
                                        {
                                            TransportationOrderController.UpdateStatus(tID, 4, currentDate, username_current);
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
        public static string UpdateQuantity(string barcode, int status, double dai, double rong, double cao)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                SmallPackageController.UpdateWeightStatus(package.ID, 0, status, 0, dai, rong, cao);
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    int orderID = mainorder.ID;
                    int warehouse = mainorder.ReceivePlace.ToInt(1);
                    int shipping = Convert.ToInt32(mainorder.ShippingType);

                    bool checkIsChinaCome = true;
                    double totalweight = 0;
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 2)
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
                            if (checkoutweight == true)
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
                        if (status == 2)
                        {

                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                           " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                           + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);


                        }
                        if (checkIsChinaCome == true)
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
                        }
                    }

                    return "1";
                }
                else return "none";
            }

            return "none";
        }
        [WebMethod]
        public static string PriceBarcode(string barcode)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 4)
                    {
                        if (!string.IsNullOrEmpty(barcode))
                        {
                            string barcodeIMG = "/Uploads/smallpackagebarcode/" + barcode + ".gif";
                            Bitmap barCode = PJUtils.CreateBarcode1(barcode);
                            barCode.Save(HttpContext.Current.Server.MapPath("~" + barcodeIMG + ""), ImageFormat.Gif);
                            return barcodeIMG;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                        return "none";
                }
                else
                {
                    return "none";
                }
            }
            else
            {
                return "none";
            }

        }
        #region Class   
        public class BigPackOut
        {
            public int BigPackOutType { get; set; }
            public List<PackageAll> Pall { get; set; }
        }
        public class PackageAll
        {
            public int MainOrderID { get; set; }
            public int PackageAllType { get; set; }
            public string OrderCode { get; set; }
            public int PackageGetCount { get; set; }
            public List<OrderGet> listPackageGet { get; set; }
        }
        public class OrderGet
        {
            public int ID { get; set; }
            public int MainorderID { get; set; }
            public int TransportationID { get; set; }
            public string OrderType { get; set; }
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
            public double dai { get; set; }
            public double rong { get; set; }
            public double cao { get; set; }
            public string IMG { get; set; }
            public string Note { get; set; }
            public string Noinhan { get; set; }
        }
        #endregion
    }
}