using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;
using System.Text;
using static NHST.manager.report_income_buyer;

namespace NHST.Controllers
{
    public class MainOrderController
    {
        #region CRUD
        public static string Insert(int UID, string ShopID, string ShopName, string Site, bool IsForward, string IsForwardPrice, bool IsFastDelivery, string IsFastDeliveryPrice, bool IsCheckProduct,
            string IsCheckProductPrice, bool IsPacked, string IsPackedPrice, bool IsFast, string IsFastPrice, string PriceVND, string PriceCNY, string FeeShipCN, string FeeBuyPro, string FeeWeight,
            string Note, string FullName, string Address, string Email, string Phone, int Status, string Deposit, string CurrentCNYVN, string TotalPriceVND,
            int SalerID, int DathangID, DateTime CreatedDate, int CreatedBy, string AmountDeposit, int OrderType)
        {
            using (var dbe = new NHSTEntities())
            {

                tbl_MainOder o = new tbl_MainOder();
                o.UID = UID;
                o.ShopID = ShopID;
                o.ShopName = ShopName;
                o.Site = Site;
                o.IsForward = IsForward;
                o.IsForwardPrice = IsForwardPrice;
                o.IsFastDelivery = IsFastDelivery;
                o.IsFastDeliveryPrice = IsFastDeliveryPrice;
                o.IsCheckProduct = IsCheckProduct;
                o.IsCheckProductPrice = IsCheckProductPrice;
                o.IsPacked = IsPacked;
                o.IsPackedPrice = IsPackedPrice;
                o.IsFast = IsFast;
                o.IsFastPrice = IsFastPrice;
                o.PriceVND = PriceVND;
                o.PriceCNY = PriceCNY;
                o.FeeShipCN = FeeShipCN;
                o.FeeBuyPro = FeeBuyPro;
                o.FeeWeight = FeeWeight;
                o.Note = Note;
                o.FullName = FullName;
                o.Address = Address;
                o.Email = Email;
                o.Phone = Phone;
                o.Status = Status;
                o.Deposit = Deposit;
                o.CurrentCNYVN = CurrentCNYVN;
                o.TotalPriceVND = TotalPriceVND;
                o.SalerID = SalerID;
                o.DathangID = DathangID;
                o.KhoTQID = 0;
                o.KhoVNID = 0;
                o.FeeShipCNToVN = "0";
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                o.IsHidden = false;
                o.AmountDeposit = AmountDeposit;
                o.OrderType = OrderType;
                dbe.tbl_MainOder.Add(o);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                string k = o.ID.ToString();
                return k;
            }
        }
        public static string UpdateStaff(int ID, int SalerID, int DathangID, int KhoTQID, int KhoVNID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.SalerID = SalerID;
                    or.DathangID = DathangID;
                    or.KhoTQID = KhoTQID;
                    or.KhoVNID = KhoVNID;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdatePayAllDate(int ID, int UID, DateTime PayAllDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PayAllDate = PayAllDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateNumberTakeDate(int ID, DateTime NumberTakeDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.NumberTakeDate = NumberTakeDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }


        public static string UpdateDathang(int ID, int DathangID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.DathangID = DathangID;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateSaler(int ID, int SalerID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.SalerID = SalerID;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }



        public static string UpdateStatusByID(int ID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderWeight(int ID, string OrderWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderWeight = OrderWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateAmountDeposit(int ID, string AmountDeposit)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.AmountDeposit = AmountDeposit;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateNote(int ID, string Note)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Note = Note;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateCheckPro(int ID, bool IsCheckProduct)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsCheckProduct = IsCheckProduct;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsGiaohang(int ID, bool IsGiaohang)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsGiaohang = IsGiaohang;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsPacked(int ID, bool IsPacked)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsPacked = IsPacked;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeWeightDC(int ID, string Feeweightdc)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = Feeweightdc;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsFastDelivery(int ID, bool IsFastDelivery, bool isShopSendGood, bool IsBuying, bool IsPaying)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsFastDelivery = IsFastDelivery;
                    or.IsShopSendGoods = isShopSendGood;
                    or.IsBuying = IsBuying;
                    or.IsPaying = IsPaying;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateShopSendGoodsDate(int ID, DateTime ShopSendGoodsDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.ShopSendGoodsDate = ShopSendGoodsDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdatePayingDate(int ID, DateTime PayingDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PayingDate = PayingDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateBuyProductDate(int ID, DateTime BuyProductDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.BuyProductDate = BuyProductDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsShopSendGoods(int ID, bool IsShopSendGoods)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsShopSendGoods = IsShopSendGoods;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderWeightCK(int ID, string OrderWeightCKS)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = OrderWeightCKS;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode(int ID, string OrderTransactionCode, string OrderTransactionCodeweight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode = OrderTransactionCode;
                    or.OrderTransactionCodeWeight = OrderTransactionCodeweight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode2(int ID, string OrderTransactionCode2, string OrderTransactionCodeweight2)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode2 = OrderTransactionCode2;
                    or.OrderTransactionCodeWeight2 = OrderTransactionCodeweight2;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode3(int ID, string OrderTransactionCode3, string OrderTransactionCodeweight3)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode3 = OrderTransactionCode3;
                    or.OrderTransactionCodeWeight3 = OrderTransactionCodeweight3;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode4(int ID, string OrderTransactionCode4, string OrderTransactionCodeweight4)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode4 = OrderTransactionCode4;
                    or.OrderTransactionCodeWeight4 = OrderTransactionCodeweight4;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode5(int ID, string OrderTransactionCode5, string OrderTransactionCodeweight5)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode5 = OrderTransactionCode5;
                    or.OrderTransactionCodeWeight5 = OrderTransactionCodeweight5;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateDeposit(int ID, int UID, string Deposit)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateDeposit_Datetime(int ID, int UID, string Deposit, DateTime DepostiDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    or.DepostiDate = DepostiDate;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateReceivePlace(int ID, int UID, string ReceivePlace, int ShippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.ReceivePlace = ReceivePlace;
                    or.ShippingType = ShippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFromPlace(int ID, int UID, int FromPlace, int ShippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FromPlace = FromPlace;
                    or.ShippingType = ShippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFTS(int ID, int UID, int FromPlace, string ReceivePlace, int ShippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FromPlace = FromPlace;
                    or.ReceivePlace = ReceivePlace;
                    or.ShippingType = ShippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsCheckNotiPrice(int ID, int UID, bool IsCheckNotiPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsCheckNotiPrice = IsCheckNotiPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderType(int ID, int UID, int OrderType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderType = OrderType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateTQVNWeight(int ID, int UID, string TQVNWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TQVNWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateMainOrderCode(int ID, int UID, string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.MainOrderCode = MainOrderCode;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalPriceReal(int ID, string TotalPriceReal, string TotalPriceRealCYN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TotalPriceReal = TotalPriceReal;
                    or.TotalPriceRealCYN = TotalPriceRealCYN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateDepositDate(int ID, DateTime DepositDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.DepostiDate = DepositDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateBuyingDate(int ID, DateTime BuyingDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.BuyingDate = BuyingDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateBuyProductDate(int ID, int UID, DateTime BuyProductDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.BuyProductDate = BuyProductDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }


        public static string UpdateTQWarehouseDate(int ID, int UID, DateTime TQWarehouseDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TQWarehouseDate = TQWarehouseDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateVNWarehouseDate(int ID, int UID, DateTime VNWarehouseDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.VNWarehouseDate = VNWarehouseDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdatePayDate(int ID, int UID, DateTime PayDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PayDate = PayDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateCompleteDate(int ID, int UID, DateTime CompleteDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.CompleteDate = CompleteDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }


        public static string UpdateFee(int ID, string Deposit, string FeeShipCN, string FeeBuyPro, string FeeWeight,
           string IsCheckProductPrice, string IsPackedPrice, string IsFastDeliveryPrice, string TotalPriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    or.FeeShipCN = FeeShipCN;
                    or.FeeBuyPro = FeeBuyPro;
                    or.FeeWeight = FeeWeight;
                    or.IsCheckProductPrice = IsCheckProductPrice;
                    or.IsPackedPrice = IsPackedPrice;
                    or.IsFastDeliveryPrice = IsFastDeliveryPrice;
                    or.TotalPriceVND = TotalPriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeWeightCK(int ID, string FeeWeightCK)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(x => x.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = FeeWeightCK;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalWeight(int ID, string TotalWeight, string OrderWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TotalWeight;
                    or.OrderWeight = OrderWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeShipTQVN(int ID, string FeeShipTQVN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeShipCNToVN = FeeShipTQVN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeWarehouse(int ID, double FeeInWareHouse)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeInWareHouse = FeeInWareHouse;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePriceNotFee(int ID, string PriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PriceVND = PriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePriceCYN(int ID, string PriceCNY)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PriceCNY = PriceCNY;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsHiddenTrue(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsHidden = true;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion

        #region GetAll
        public static List<View_OrderListFilterWithStatusString> GetByUserInViewFilterWithStatusString1(int RoleID, int OrderType, int StaffID,
            string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                if (RoleID != 1)
                {
                    List<View_OrderListFilterWithStatusString> lo = new List<View_OrderListFilterWithStatusString>();
                    List<View_OrderListFilterWithStatusString> losearch = new List<View_OrderListFilterWithStatusString>();
                    //if (RoleID == 0)
                    //{
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    //else if (RoleID == 2)
                    //{
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    //else if (RoleID == 3)
                    //{
                    //    //lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.DathangID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status != 1 && l.DathangID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    //else if (RoleID == 4)
                    //{
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status < 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    //else if (RoleID == 5)
                    //{
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status <= 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    //else if (RoleID == 6)
                    //{
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status != 1 && l.SalerID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    //else if (RoleID == 8)
                    //{
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 9 && l.Status < 10 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    //else
                    //{
                    //    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    //}
                    lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status == 2 && l.IsBuying == false && l.IsPaying == false && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            if (Type == 1)
                            {
                                var listos = GetMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else if (Type == 2)
                            {
                                var listos = GetSmallPackageMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                losearch = lo.Where(o => o.MainOrderCode == searchtext).ToList();
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                    return losearch;
                }
                return null;
            }
        }
        public static List<View_OrderListFilterWithStatusString> GetByUserInViewFilterWithStatusString(int RoleID, int OrderType, int StaffID,
            string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                if (RoleID != 1)
                {
                    List<View_OrderListFilterWithStatusString> lo = new List<View_OrderListFilterWithStatusString>();
                    List<View_OrderListFilterWithStatusString> losearch = new List<View_OrderListFilterWithStatusString>();
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.DathangID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                        //lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status != 1 && l.DathangID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status < 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status <= 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status != 1 && l.SalerID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 9 && l.Status < 10 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            if (Type == 1)
                            {
                                var listos = GetMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else if (Type == 2)
                            {
                                var listos = GetSmallPackageMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                losearch = lo.Where(o => o.MainOrderCode == searchtext).ToList();
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                    return losearch;
                }
                return null;
            }
        }

        public static string SelectUIDByIDOrder(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    return ot.UID.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public static tbl_MainOder GetByMainOrderCode(string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_MainOder.Where(o => o.MainOrderCode == MainOrderCode).FirstOrDefault();
                if (ot != null)
                {
                    return ot;
                }
                else
                {
                    return null;
                }
            }
        }
        public static tbl_MainOder GetByMainOrderCodeAndID(int MainOrderID, string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_MainOder.Where(o => o.ID == MainOrderID && o.MainOrderCode == MainOrderCode).FirstOrDefault();
                if (ot != null)
                {
                    return ot;
                }
                else
                {
                    return null;
                }
            }
        }
        public static List<tbl_MainOder> GetListByMainOrderCode(string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> ot = new List<tbl_MainOder>();
                ot = dbe.tbl_MainOder.Where(o => o.MainOrderCode == MainOrderCode).ToList();
                return ot;
            }
        }
        public static List<tbl_MainOder> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.OrderByDescending(o => o.ID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOder> GetByRoleID(int RoleID, int StaffID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                if (RoleID != 1)
                {
                    if (RoleID == 3)
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2 && l.Status < 5 && l.DathangID == StaffID).ToList();
                    else if (RoleID == 4)
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 5 && (l.KhoTQID == StaffID || l.KhoTQID == 0)).ToList();
                    else if (RoleID == 5)
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 6 && (l.KhoVNID == StaffID || l.KhoVNID == 0)).ToList();
                    else if (RoleID == 6)
                        lo = dbe.tbl_MainOder.Where(l => l.SalerID == StaffID).ToList();
                    else if (RoleID == 7)
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 7).ToList();
                    else
                    {
                        lo = dbe.tbl_MainOder.ToList();
                    }
                }
                return lo;
            }
        }
        public static List<View_OrderList> GetByUserInView(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderList> lo = new List<View_OrderList>();
                List<View_OrderList> losearch = new List<View_OrderList>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderList.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderList.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 6 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<View_OrderListFilter> GetByUserInViewFilter(int RoleID, int StaffID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilter> lo = new List<View_OrderListFilter>();
                List<View_OrderListFilter> losearch = new List<View_OrderListFilter>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }

                }
                return lo;
            }
        }
        public static List<View_OrderListFilter> GetByUserInViewFilter2(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilter> lo = new List<View_OrderListFilter>();
                List<View_OrderListFilter> losearch = new List<View_OrderListFilter>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    var findpackage = SmallPackageController.GetByMainOrderIDAndCode(item.ID, searchtext);
                                    if (findpackage.Count > 0)
                                    {
                                        losearch.Add(item);
                                    }
                                    //if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    //{
                                    //    if (item.OrderTransactionCode.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    //{
                                    //    if (item.OrderTransactionCode2.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    //{
                                    //    if (item.OrderTransactionCode3.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    //{
                                    //    if (item.OrderTransactionCode4.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    //{
                                    //    if (item.OrderTransactionCode5.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                }
                            }
                        }
                        else if (Type == 3)
                        {
                            foreach (var item in lo)
                            {
                                var findpackage = SmallPackageController.GetByMainOrderID(item.ID);
                                if (findpackage.Count == 0)
                                {
                                    losearch.Add(item);
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<View_OrderListDamuahang> GetByUserInViewFilterStatus5()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListDamuahang> lo = new List<View_OrderListDamuahang>();
                lo = dbe.View_OrderListDamuahang.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListKhoTQ> GetByUserInViewFilterStatus6()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListKhoTQ> lo = new List<View_OrderListKhoTQ>();
                lo = dbe.View_OrderListKhoTQ.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListKhoVN> GetByUserInViewFilterStatus7()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListKhoVN> lo = new List<View_OrderListKhoVN>();
                lo = dbe.View_OrderListKhoVN.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_Orderlistwithstatus> GetByUserInViewFilterStatus(int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_Orderlistwithstatus> lo = new List<View_Orderlistwithstatus>();
                lo = dbe.View_Orderlistwithstatus.Where(l => l.Status == status).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListFilterYCGiao> GetByUserInViewFilterYCG(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilterYCGiao> lo = new List<View_OrderListFilterYCGiao>();
                List<View_OrderListFilterYCGiao> losearch = new List<View_OrderListFilterYCGiao>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<tbl_MainOder> GetByUser(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                List<tbl_MainOder> losearch = new List<tbl_MainOder>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.tbl_MainOder.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.tbl_MainOder.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 6 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<tbl_MainOder> GetSuccessByCustomer(int customerID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(l => l.Status == 10 && l.UID == customerID).ToList();
                return lo;
            }
        }
        public static List<tbl_MainOder> GetFromDateToDate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();

                var alllist = dbe.tbl_MainOder.OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();

                if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from && t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else
                {
                    lo = alllist;
                }
                if (lo.Count > 0)
                    return lo;
                else return lo;
            }
        }
        public static List<OrderReportGetSQLIncome> GetFromDateToDateSQL(string fd, string td)
        {
            var list = new List<OrderReportGetSQLIncome>();
            var sql = @"SELECT mo.*, countRowLink as orderlinks, countRowPay as paylinks ,wh.WareHouseName, whf.WareHouseName as WareHouseFromName ";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += " (SELECT count(*) AS countRowLink, MainOrderID FROM tbl_Order AS a GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID LEFT OUTER JOIN";
            sql += " (SELECT count(*) AS countRowPay, MainOrderID FROM tbl_PayOrderHistory AS p GROUP BY p.MainOrderID) AS p ON p.MainOrderID = mo.ID   Left outer join tbl_Warehouse as wh on wh.ID = mo.ReceivePlace   Left outer join tbl_WarehouseFrom as whf on whf.ID = mo.FromPlace";
            sql += " where 1 = 1";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new OrderReportGetSQLIncome();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt();
                if (reader["SalerID"] != DBNull.Value)
                    entity.SalerID = reader["SalerID"].ToString().ToInt();
                if (reader["DathangID"] != DBNull.Value)
                    entity.DathangID = reader["DathangID"].ToString().ToInt();
                if (reader["orderlinks"] != DBNull.Value)
                    entity.orderlinks = reader["orderlinks"].ToString().ToInt();
                if (reader["paylinks"] != DBNull.Value)
                    entity.paylinks = reader["paylinks"].ToString().ToInt();
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["IsFastPrice"] != DBNull.Value)
                    entity.IsFastPrice = reader["IsFastPrice"].ToString();
                if (reader["FeeShipCN"] != DBNull.Value)
                    entity.FeeShipCN = reader["FeeShipCN"].ToString();
                if (reader["FeeBuyPro"] != DBNull.Value)
                    entity.FeeBuyPro = reader["FeeBuyPro"].ToString();
                if (reader["FeeWeight"] != DBNull.Value)
                    entity.FeeWeight = reader["FeeWeight"].ToString();
                if (reader["IsCheckProductPrice"] != DBNull.Value)
                    entity.IsCheckProductPrice = reader["IsCheckProductPrice"].ToString();
                if (reader["IsPackedPrice"] != DBNull.Value)
                    entity.IsPackedPrice = reader["IsPackedPrice"].ToString();
                if (reader["IsFastDeliveryPrice"] != DBNull.Value)
                    entity.IsFastDeliveryPrice = reader["IsFastDeliveryPrice"].ToString();
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    entity.CurrentCNYVN = reader["CurrentCNYVN"].ToString();
                if (reader["ShopID"] != DBNull.Value)
                    entity.ShopID = reader["ShopID"].ToString();
                if (reader["ShopName"] != DBNull.Value)
                    entity.ShopName = reader["ShopName"].ToString();
                if (reader["FullName"] != DBNull.Value)
                    entity.FullName = reader["FullName"].ToString();
                if (reader["Email"] != DBNull.Value)
                    entity.Email = reader["Email"].ToString();
                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["TotalPriceReal"] != DBNull.Value)
                    entity.TotalPriceReal = reader["TotalPriceReal"].ToString();

                if (reader["DepostiDate"] != DBNull.Value)
                    entity.DepostiDate = Convert.ToDateTime(reader["DepostiDate"]);
                if (reader["BuyProductDate"] != DBNull.Value)
                    entity.BuyProductDate = Convert.ToDateTime(reader["BuyProductDate"]);
                if (reader["TQWarehouseDate"] != DBNull.Value)
                    entity.TQWarehouseDate = Convert.ToDateTime(reader["TQWarehouseDate"]);
                if (reader["VNWarehouseDate"] != DBNull.Value)
                    entity.VNWarehouseDate = Convert.ToDateTime(reader["VNWarehouseDate"]);
                if (reader["PayAllDate"] != DBNull.Value)
                    entity.PayAllDate = Convert.ToDateTime(reader["PayAllDate"]);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

                if (reader["WareHouseName"] != DBNull.Value)
                    entity.WareHouseName = reader["WareHouseName"].ToString();
                if (reader["WareHouseFromName"] != DBNull.Value)
                    entity.WareHouseFromName = reader["WareHouseFromName"].ToString();
                if (reader["OrderWeight"] != DBNull.Value)
                    entity.OrderWeight = reader["OrderWeight"].ToString();
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<tbl_MainOder> GetFromDateToDateAndFromStatus(string from, string to, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();

                var alllist = dbe.tbl_MainOder.Where(t => t.Status >= Status).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();

                if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= Convert.ToDateTime(from) && t.CreatedDate <= Convert.ToDateTime(to)).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= Convert.ToDateTime(from)).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate <= Convert.ToDateTime(to)).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else
                {
                    lo = alllist;
                }
                if (lo.Count > 0)
                    return lo;
                else return lo;
            }
        }
        public static List<mainorder> GetDathangID(int dathangID)
        {
            using (var dbe = new NHSTEntities())
            {
                var list = new List<mainorder>();
                var sql = @"SELECT mo.ID, mo.Status ";
                sql += " FROM dbo.tbl_MainOder AS mo";
                sql += " where mo.DathangID = " + dathangID + "";
                sql += " Order By ID desc";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                int i = 1;
                while (reader.Read())
                {
                    var entity = new mainorder();
                    entity.STT = i;
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = reader["ID"].ToString().ToInt(0);
                    if (reader["Status"] != DBNull.Value)
                        entity.Status = reader["Status"].ToString().ToInt();
                    i++;
                    list.Add(entity);
                }
                reader.Close();
                return list;
            }
        }
        public static List<OrderReportGetSQL> GetReportByMainOrderID(int dathangID)
        {
            using (var dbe = new NHSTEntities())
            {
                var list = new List<OrderReportGetSQL>();
                var sql = @"SELECT mo.ID, mo.status, mo.DathangID, countRowLink as orderlinks, countRowPay as paylinks ";
                sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " (SELECT count(*) AS countRowLink, MainOrderID FROM tbl_Order AS a GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID LEFT OUTER JOIN";
                sql += " (SELECT count(*) AS countRowPay, MainOrderID FROM tbl_PayOrderHistory AS p GROUP BY p.MainOrderID) AS p ON p.MainOrderID = mo.ID";
                sql += " where mo.DathangID = " + dathangID + "";
                sql += " Order By ID desc";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                int i = 1;
                while (reader.Read())
                {
                    var entity = new OrderReportGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = reader["ID"].ToString().ToInt(0);
                    if (reader["Status"] != DBNull.Value)
                        entity.Status = reader["Status"].ToString().ToInt();
                    if (reader["orderlinks"] != DBNull.Value)
                        entity.orderlinks = reader["orderlinks"].ToString().ToInt();
                    if (reader["paylinks"] != DBNull.Value)
                        entity.paylinks = reader["paylinks"].ToString().ToInt();
                    i++;
                    list.Add(entity);
                }
                reader.Close();
                return list;
            }
        }
        public static List<OrderReportGetSQL> GetReportByMainOrderIDFT(string fd, string td, int dathangID)
        {
            using (var dbe = new NHSTEntities())
            {
                var list = new List<OrderReportGetSQL>();
                var sql = @"SELECT mo.ID, mo.status, mo.DathangID, countRowLink as orderlinks, countRowPay as paylinks ";
                sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " (SELECT count(*) AS countRowLink, MainOrderID FROM tbl_Order AS a GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID LEFT OUTER JOIN";
                sql += " (SELECT count(*) AS countRowPay, MainOrderID FROM tbl_PayOrderHistory AS p GROUP BY p.MainOrderID) AS p ON p.MainOrderID = mo.ID";
                sql += " where mo.DathangID = " + dathangID + "";
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND BuyProductDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND BuyProductDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
                sql += " Order By ID desc";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                int i = 1;
                while (reader.Read())
                {
                    var entity = new OrderReportGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = reader["ID"].ToString().ToInt(0);
                    if (reader["Status"] != DBNull.Value)
                        entity.Status = reader["Status"].ToString().ToInt();
                    if (reader["orderlinks"] != DBNull.Value)
                        entity.orderlinks = reader["orderlinks"].ToString().ToInt();
                    if (reader["paylinks"] != DBNull.Value)
                        entity.paylinks = reader["paylinks"].ToString().ToInt();
                    i++;
                    list.Add(entity);
                }
                reader.Close();
                return list;
            }
        }
        public static List<mainorder> GetFromDateToDateAndDathangID(string fd, string td, int dathangID)
        {
            using (var dbe = new NHSTEntities())
            {
                var list = new List<mainorder>();
                var sql = @"SELECT mo.ID, mo.Status ";
                sql += " FROM dbo.tbl_MainOder AS mo";
                sql += " where mo.DathangID = " + dathangID + "";
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
                sql += " Order By ID desc";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                int i = 1;
                while (reader.Read())
                {
                    var entity = new mainorder();
                    entity.STT = i;
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = reader["ID"].ToString().ToInt(0);
                    if (reader["Status"] != DBNull.Value)
                        entity.Status = reader["Status"].ToString().ToInt();
                    i++;
                    list.Add(entity);
                }
                reader.Close();
                return list;
            }
        }
        public static List<tbl_MainOder> GetFromStatus(int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var alllist = dbe.tbl_MainOder.Where(t => t.Status >= Status).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                if (alllist.Count > 0)
                    return alllist;
                else return alllist;
            }
        }
        public static List<tbl_MainOder> GetAllByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOder> GetAllByUIDAndStatus(int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.Status == Status).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOder> GetAllByUIDNotHidden(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.IsHidden == false).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<mainorder> GetAllByUIDNotHidden_SqlHelper(int UID, int status, string fd, string td, int OrderType)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND,mo.ExpectedDate, mo.Deposit,mo.AmountDeposit, mo.CreatedDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, mo.OrderType, mo.IsCheckNotiPrice, o.anhsanpham, mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " where UID = " + UID + " AND mo.OrderType = " + OrderType + " ";

            if (status >= 0)
            {
                if (status == 11)
                {
                    sql += " AND Status = 5 AND mo.IsShopSendGoods = 1";
                }
                else if (status == 12)
                {
                    sql += " AND Status = 2 AND mo.IsBuying = 1";
                }
                else if (status == 13)
                {
                    sql += " AND Status = 2 AND mo.IsPaying = 1";
                }
                else
                {
                    sql += " AND Status = " + status;
                }
            }


            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["ExpectedDate"] != DBNull.Value)
                    entity.ExpectedDate = Convert.ToString(reader["ExpectedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                else
                    entity.IsCheckNotiPrice = false;
                if (reader["IsShopSendGoods"] != DBNull.Value)
                    entity.IsShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"].ToString());
                else
                    entity.IsShopSendGoods = false;

                if (reader["IsBuying"] != DBNull.Value)
                    entity.IsBuying = Convert.ToBoolean(reader["IsBuying"].ToString());
                else
                    entity.IsBuying = false;

                if (reader["IsPaying"] != DBNull.Value)
                    entity.IsPaying = Convert.ToBoolean(reader["IsPaying"].ToString());
                else
                    entity.IsPaying = false;

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<mainorder> GetAllByUIDOrderCodeNotHidden_SqlHelper(int UID, int type)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.OrderType, mo.Status, mo.TotalPriceVND,mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying, mo.Deposit  ";
            sql += " FROM dbo.tbl_MainOder AS mo ";
            sql += " where UID = " + UID + " AND mo.OrderType = " + type + " ";
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();


                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                int status = -1;
                bool isShopSendGoods = false;
                bool isBuying = false;
                bool isPaying = false;
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["IsShopSendGoods"] != DBNull.Value)
                    isShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"]);
                entity.IsShopSendGoods = isShopSendGoods;

                if (reader["IsBuying"] != DBNull.Value)
                    isBuying = Convert.ToBoolean(reader["IsBuying"]);
                entity.IsBuying = isBuying;

                if (reader["IsPaying"] != DBNull.Value)
                    isPaying = Convert.ToBoolean(reader["IsPaying"]);
                entity.IsPaying = isPaying;

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserInSQLHelper_nottextnottypeWithstatus(int RoleID, int OrderType, int StaffID, int page, int maxrows)
        {
            var list = new List<OrderGetSQL>();
            if (RoleID != 1)
            {
                var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.DepostiDate,mo.ExpectedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying, countRowLink as orderlinks, mo.Site, ";
                sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
                sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
                sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Chờ mua hàng</span>' ";
                sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
                sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
                sql += "WHEN 5 THEN N'<span class=\"bg-green\">Chờ shop TQ phát hàng</span>' ";
                sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
                sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
                sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
                sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
                sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
                sql += "        END AS statusstring, mo.DathangID, ";
                sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
                sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
                sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";

                sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
                //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM      dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
                sql += " (SELECT count(*) AS countRowLink, MainOrderID FROM tbl_Order AS aa GROUP BY aa.MainOrderID) AS aa ON aa.MainOrderID = mo.ID LEFT OUTER JOIN";
                sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";

                sql += "        Where UID > 0 ";
                sql += "    AND mo.OrderType  = " + OrderType + "";
                if (RoleID == 3)
                {
                    sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
                    //sql += "    AND mo.Status != 1 and mo.DathangID = " + StaffID + "";
                }
                else if (RoleID == 4)
                {
                    //sql += "    AND mo.Status >= 5 and mo.Status < 7";
                    sql += "    AND mo.Status >= 2";
                }
                else if (RoleID == 5)
                {
                    //sql += "    AND mo.Status >= 5 and mo.Status <= 7";
                    sql += "    AND mo.Status >= 2";
                }
                else if (RoleID == 6)
                {
                    sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
                }
                else if (RoleID == 8)
                {
                    sql += "    AND mo.Status >= 9 and mo.Status < 10";
                }
                else if (RoleID == 7)
                {
                    sql += "    AND mo.Status >= 2";
                }
                sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    int MainOrderID = reader["ID"].ToString().ToInt(0);
                    var entity = new OrderGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = MainOrderID;
                    if (reader["TotalPriceVND"] != DBNull.Value)
                        entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                    if (reader["orderlinks"] != DBNull.Value)
                        entity.orderlinks = reader["orderlinks"].ToString();
                    if (reader["Site"] != DBNull.Value)
                        entity.Site = reader["Site"].ToString();
                    if (reader["Deposit"] != DBNull.Value)
                        entity.Deposit = reader["Deposit"].ToString();
                    if (reader["DepostiDate"] != DBNull.Value)
                        entity.DepostiDate = reader["DepostiDate"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = reader["CreatedDate"].ToString();
                    if (reader["ExpectedDate"] != DBNull.Value)
                        entity.ExpectedDate = reader["ExpectedDate"].ToString();
                    int status = -1;
                    bool isShopSendGoods = false;
                    bool isBuying = false;
                    bool isPaying = false;
                    if (reader["Status"] != DBNull.Value)
                    {
                        entity.Status = Convert.ToInt32(reader["Status"].ToString());
                        status = Convert.ToInt32(reader["Status"].ToString());
                    }
                    if (reader["IsShopSendGoods"] != DBNull.Value)
                        isShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"]);
                    entity.IsShopSendGoods = isShopSendGoods;

                    if (reader["IsBuying"] != DBNull.Value)
                        isBuying = Convert.ToBoolean(reader["IsBuying"]);
                    entity.IsBuying = isBuying;

                    if (reader["IsPaying"] != DBNull.Value)
                        isPaying = Convert.ToBoolean(reader["IsPaying"]);
                    entity.IsPaying = isPaying;

                    if (status == 5)
                    {
                        if (isShopSendGoods == true)
                        {
                            entity.statusstring = "<span class=\"bg-green\">Shop đã phát hàng</span>";
                        }
                        else
                        {
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                        }
                    }
                    else if (status == 2)
                    {
                        if (isBuying == true && isPaying == false)
                        {
                            entity.statusstring = "<span class=\"bg-red\">Đang mua hàng</span>";
                        }
                        else if (isPaying == true && isBuying == false)
                        {
                            entity.statusstring = "<span class=\"bg-green\">Chờ shop TQ phát hàng</span>";
                        }
                        else if (isPaying == true && isBuying == true)
                        {
                            entity.statusstring = "<span class=\"bg-green\">Chờ shop TQ phát hàng</span>";
                        }
                        else
                        {
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                        }
                    }

                    else
                    {
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                    }







                    if (reader["OrderTransactionCode"] != DBNull.Value)
                        entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                    if (reader["OrderTransactionCode2"] != DBNull.Value)
                        entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                    if (reader["OrderTransactionCode3"] != DBNull.Value)
                        entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                    if (reader["OrderTransactionCode4"] != DBNull.Value)
                        entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                    if (reader["OrderTransactionCode5"] != DBNull.Value)
                        entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                    if (reader["Uname"] != DBNull.Value)
                        entity.Uname = reader["Uname"].ToString();
                    if (reader["saler"] != DBNull.Value)
                        entity.saler = reader["saler"].ToString();
                    if (reader["dathang"] != DBNull.Value)
                        entity.dathang = reader["dathang"].ToString();
                    if (reader["anhsanpham"] != DBNull.Value)
                        entity.anhsanpham = reader["anhsanpham"].ToString();
                    if (reader["OrderType"] != DBNull.Value)
                        entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                    if (reader["IsCheckNotiPrice"] != DBNull.Value)
                        entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                    else
                        entity.IsCheckNotiPrice = false;
                    list.Add(entity);
                }
                reader.Close();
            }
            return list;
        }
        public static List<OrderGetSQL> GetByUserInSQLHelper_nottextnottypeWithstatusSet(int RoleID, int OrderType, int StaffID, int page, int maxrows)
        {
            var list = new List<OrderGetSQL>();
            if (RoleID != 1)
            {
                var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying, countRowLink as orderlinks, mo.Site,  ";
                sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
                sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
                sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Chờ mua hàng</span>' ";
                sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
                sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
                sql += "WHEN 5 THEN N'<span class=\"bg-green\">Chờ shop TQ phát hàng</span>' ";
                sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
                sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
                sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
                sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
                sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
                sql += "        END AS statusstring, mo.DathangID, ";
                sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
                sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
                sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";

                sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
                sql += " (SELECT count(*) AS countRowLink, MainOrderID FROM tbl_Order AS aa GROUP BY aa.MainOrderID) AS aa ON aa.MainOrderID = mo.ID LEFT OUTER JOIN";
                //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM      dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
                sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";

                sql += "        Where UID > 0 ";
                sql += "    AND mo.OrderType  = " + OrderType + " AND mo.Status = 2 AND mo.IsPaying = 0 AND mo.IsBuying = 0 ";

                sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    int MainOrderID = reader["ID"].ToString().ToInt(0);
                    var entity = new OrderGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = MainOrderID;

                    if (reader["orderlinks"] != DBNull.Value)
                        entity.orderlinks = reader["orderlinks"].ToString();
                    if (reader["Site"] != DBNull.Value)
                        entity.Site = reader["Site"].ToString();

                    if (reader["TotalPriceVND"] != DBNull.Value)
                        entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                    if (reader["Deposit"] != DBNull.Value)
                        entity.Deposit = reader["Deposit"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = reader["CreatedDate"].ToString();
                    int status = -1;
                    bool isShopSendGoods = false;
                    bool isBuying = false;
                    bool isPaying = false;
                    if (reader["Status"] != DBNull.Value)
                    {
                        entity.Status = Convert.ToInt32(reader["Status"].ToString());
                        status = Convert.ToInt32(reader["Status"].ToString());
                    }
                    if (reader["IsShopSendGoods"] != DBNull.Value)
                        isShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"]);

                    entity.IsShopSendGoods = isShopSendGoods;

                    if (reader["IsBuying"] != DBNull.Value)
                        isBuying = Convert.ToBoolean(reader["IsBuying"]);
                    entity.IsBuying = isBuying;

                    if (reader["IsPaying"] != DBNull.Value)
                        isPaying = Convert.ToBoolean(reader["IsPaying"]);
                    entity.IsPaying = isPaying;

                    if (status == 5)
                    {
                        if (isShopSendGoods == true)
                        {
                            entity.statusstring = "<span class=\"bg-green\">Shop đã phát hàng</span>";
                        }
                        else
                        {
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                        }
                    }
                    else if (status == 2)
                    {
                        if (isBuying == true && isPaying == false)
                        {
                            entity.statusstring = "<span class=\"bg-red\">Đang mua hàng</span>";
                        }
                        else if (isPaying == true && isBuying == false)
                        {
                            entity.statusstring = "<span class=\"bg-green\">Chờ shop TQ phát hàng</span>";
                        }
                        else if (isPaying == true && isBuying == true)
                        {
                            entity.statusstring = "<span class=\"bg-green\">Chờ shop TQ phát hàng</span>";
                        }
                        else
                        {
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                        }

                    }

                    else
                    {
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                    }


                    if (reader["OrderTransactionCode"] != DBNull.Value)
                        entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                    if (reader["OrderTransactionCode2"] != DBNull.Value)
                        entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                    if (reader["OrderTransactionCode3"] != DBNull.Value)
                        entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                    if (reader["OrderTransactionCode4"] != DBNull.Value)
                        entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                    if (reader["OrderTransactionCode5"] != DBNull.Value)
                        entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                    if (reader["Uname"] != DBNull.Value)
                        entity.Uname = reader["Uname"].ToString();
                    //if (reader["saler"] != DBNull.Value)
                    //    entity.saler = reader["saler"].ToString();
                    //if (reader["dathang"] != DBNull.Value)
                    //    entity.dathang = reader["dathang"].ToString();
                    if (reader["anhsanpham"] != DBNull.Value)
                        entity.anhsanpham = reader["anhsanpham"].ToString();
                    if (reader["OrderType"] != DBNull.Value)
                        entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                    if (reader["IsCheckNotiPrice"] != DBNull.Value)
                        entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                    else
                        entity.IsCheckNotiPrice = false;


                    int dathangID = 0;
                    int salerID = 0;

                    if (reader["DathangID"] != DBNull.Value)
                        dathangID = reader["DathangID"].ToString().ToInt(0);
                    if (reader["SalerID"] != DBNull.Value)
                        salerID = reader["SalerID"].ToString().ToInt(0);
                    StringBuilder dathangstring = new StringBuilder();
                    var listdathang = AccountController.GetAllByRoleID(3);
                    if (listdathang.Count > 0)
                    {

                        dathangstring.Append("<select class=\"form-control\" onchange=\"updateDathang($(this))\" data-mainorderid=\"" + MainOrderID + "\">");
                        dathangstring.Append("<option value=\"0\" selected>-----------</option>");
                        foreach (var d in listdathang)
                        {
                            if (d.ID == dathangID)
                            {
                                dathangstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                            }
                            else
                            {
                                dathangstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                            }
                        }
                        dathangstring.Append("</select>");
                    }
                    entity.dathang = dathangstring.ToString();

                    StringBuilder salerstring = new StringBuilder();
                    var listsaler = AccountController.GetAllByRoleID(6);
                    if (listsaler.Count > 0)
                    {
                        salerstring.Append("<select class=\"form-control\" onchange=\"updateSaler($(this))\" data-mainorderid=\"" + MainOrderID + "\"\">");
                        salerstring.Append("<option value=\"0\" selected>-----------</option>");
                        foreach (var d in listsaler)
                        {
                            if (d.ID == salerID)
                            {
                                salerstring.Append("<option value=\"" + d.ID + "\" selected>" + d.Username + "</option>");
                            }
                            else
                            {
                                salerstring.Append("<option value=\"" + d.ID + "\">" + d.Username + "</option>");
                            }
                        }
                        salerstring.Append("</select>");
                    }
                    entity.saler = salerstring.ToString();
                    list.Add(entity);
                }
                reader.Close();
            }
            return list;
        }
        public static List<mainorder> GetAllByUIDNotHidden_SqlHelper1(int UID, int status, string fd, string td)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit,mo.AmountDeposit, mo.CreatedDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, o.anhsanpham";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " where UID = " + UID + " and IsHidden = 0 ";

            if (status >= 0)
                sql += " AND Status = " + status;

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static tbl_MainOder GetAllByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static tbl_MainOder GetAllByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static List<OrderGetSQL> getOrderByRoleIDStaffID1_SQL(int RoleID, int StaffID)
        {
            var list = new List<OrderGetSQL>();
            int Count = 0;
            var sql = @"SELECT ID, status, mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying from tbl_MainOder as mo";
            sql += "        Where UID > 0";
            if (RoleID == 3)
            {
                sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
            }
            else if (RoleID == 4)
            {
                sql += "    AND mo.Status >= 5 and mo.Status < 7";
            }
            else if (RoleID == 5)
            {
                sql += "    AND mo.Status >= 5 and mo.Status <= 7";
            }
            else if (RoleID == 6)
            {
                sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
            }
            else if (RoleID == 8)
            {
                sql += "    AND mo.Status >= 9 and mo.Status < 10";
            }
            else if (RoleID == 7)
            {
                sql += "    AND mo.Status >= 2";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }

                if (reader["IsShopSendGoods"] != DBNull.Value)
                    entity.IsShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"].ToString());
                else
                    entity.IsShopSendGoods = false;

                int status = -1;
                bool isBuying = false;
                bool isPaying = false;
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    status = Convert.ToInt32(reader["Status"].ToString());
                }

                if (reader["IsBuying"] != DBNull.Value)
                    isBuying = Convert.ToBoolean(reader["IsBuying"]);
                entity.IsBuying = isBuying;

                if (reader["IsPaying"] != DBNull.Value)
                    isPaying = Convert.ToBoolean(reader["IsPaying"]);
                entity.IsPaying = isPaying;

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static int getOrderByRoleIDStaffID_SQL(int RoleID, int StaffID)
        {
            int Count = 0;
            var sql = @"SELECT COUNT(*) as Total from tbl_MainOder as mo";
            sql += "        Where UID > 0";
            if (RoleID == 3)
            {
                sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
            }
            else if (RoleID == 4)
            {
                sql += "    AND mo.Status >= 5 and mo.Status < 7";
            }
            else if (RoleID == 5)
            {
                sql += "    AND mo.Status >= 5 and mo.Status <= 7";
            }
            else if (RoleID == 6)
            {
                sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
            }
            else if (RoleID == 8)
            {
                sql += "    AND mo.Status >= 9 and mo.Status < 10";
            }
            else if (RoleID == 7)
            {
                sql += "    AND mo.Status >= 2";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
        public static List<OrderGetSQL> GetByUserIDInSQLHelperWithFilter(int UID,
            string searchtext, int Type, string fd, string td, double priceFrom, double priceTo,
            bool isNotCode)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Chờ mua hàng</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Chờ shop TQ phát hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID, ";
            sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
            sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages, sm1.totalSmallPackagesWithSearchText, ofi.totalOrderSearch, ";
            sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";

            sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
            sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackagesWithSearchText FROM tbl_smallpackage where OrderTransactionCode like N'%" + searchtext + "%') sm1 ON sm1.MainOrderID = mo.ID and totalSmallPackagesWithSearchText = 1 LEFT OUTER JOIN";
            sql += " (SELECT MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalOrderSearch FROM tbl_Order where title_origin like N'%" + searchtext + "%') ofi ON ofi.MainOrderID = mo.ID and totalOrderSearch = 1 LEFT OUTER JOIN";
            sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages=1 LEFT OUTER JOIN";
            sql += " (SELECT image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";

            sql += "        Where UID = " + UID + " ";
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)
                {
                    sql += "  AND mo.Mainordercode like N'%" + searchtext + "%'";
                }
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            if (priceFrom > 0)
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (priceTo > 0)
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND totalSmallPackages is null";
            }
            sql += " ORDER BY mo.ID DESC";
            //sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(searchtext))
                {
                    int totalOrderSearch = 0;
                    if (reader["totalOrderSearch"] != DBNull.Value)
                        totalOrderSearch = reader["totalOrderSearch"].ToString().ToInt(0);

                    int totalSmallPackagesWithSearchText = 0;
                    if (reader["totalSmallPackagesWithSearchText"] != DBNull.Value)
                        totalSmallPackagesWithSearchText = reader["totalSmallPackagesWithSearchText"].ToString().ToInt(0);

                    if (Type == 1)
                    {
                        if (totalOrderSearch > 0)
                        {
                            int MainOrderID = reader["ID"].ToString().ToInt(0);
                            var entity = new OrderGetSQL();
                            if (reader["ID"] != DBNull.Value)
                                entity.ID = MainOrderID;
                            if (reader["TotalPriceVND"] != DBNull.Value)
                                entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                            if (reader["Deposit"] != DBNull.Value)
                                entity.Deposit = reader["Deposit"].ToString();
                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.CreatedDate = reader["CreatedDate"].ToString();
                            if (reader["Status"] != DBNull.Value)
                            {
                                entity.Status = Convert.ToInt32(reader["Status"].ToString());
                            }
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                            if (reader["OrderTransactionCode"] != DBNull.Value)
                                entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                            if (reader["OrderTransactionCode2"] != DBNull.Value)
                                entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                            if (reader["OrderTransactionCode3"] != DBNull.Value)
                                entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                            if (reader["OrderTransactionCode4"] != DBNull.Value)
                                entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                            if (reader["OrderTransactionCode5"] != DBNull.Value)
                                entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                            if (reader["Uname"] != DBNull.Value)
                                entity.Uname = reader["Uname"].ToString();
                            if (reader["saler"] != DBNull.Value)
                                entity.saler = reader["saler"].ToString();
                            if (reader["dathang"] != DBNull.Value)
                                entity.dathang = reader["dathang"].ToString();
                            if (reader["anhsanpham"] != DBNull.Value)
                                entity.anhsanpham = reader["anhsanpham"].ToString();
                            if (reader["OrderType"] != DBNull.Value)
                                entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                            if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                            else
                                entity.IsCheckNotiPrice = false;
                            list.Add(entity);
                        }
                    }
                    else if (Type == 2)
                    {
                        if (totalSmallPackagesWithSearchText > 0)
                        {
                            int MainOrderID = reader["ID"].ToString().ToInt(0);
                            var entity = new OrderGetSQL();
                            if (reader["ID"] != DBNull.Value)
                                entity.ID = MainOrderID;
                            if (reader["TotalPriceVND"] != DBNull.Value)
                                entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                            if (reader["Deposit"] != DBNull.Value)
                                entity.Deposit = reader["Deposit"].ToString();
                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.CreatedDate = reader["CreatedDate"].ToString();
                            if (reader["Status"] != DBNull.Value)
                            {
                                entity.Status = Convert.ToInt32(reader["Status"].ToString());
                            }
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                            if (reader["OrderTransactionCode"] != DBNull.Value)
                                entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                            if (reader["OrderTransactionCode2"] != DBNull.Value)
                                entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                            if (reader["OrderTransactionCode3"] != DBNull.Value)
                                entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                            if (reader["OrderTransactionCode4"] != DBNull.Value)
                                entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                            if (reader["OrderTransactionCode5"] != DBNull.Value)
                                entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                            if (reader["Uname"] != DBNull.Value)
                                entity.Uname = reader["Uname"].ToString();
                            if (reader["saler"] != DBNull.Value)
                                entity.saler = reader["saler"].ToString();
                            if (reader["dathang"] != DBNull.Value)
                                entity.dathang = reader["dathang"].ToString();
                            if (reader["anhsanpham"] != DBNull.Value)
                                entity.anhsanpham = reader["anhsanpham"].ToString();
                            if (reader["OrderType"] != DBNull.Value)
                                entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                            if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                            else
                                entity.IsCheckNotiPrice = false;
                            list.Add(entity);
                        }
                    }
                    else
                    {
                        int MainOrderID = reader["ID"].ToString().ToInt(0);
                        var entity = new OrderGetSQL();
                        if (reader["ID"] != DBNull.Value)
                            entity.ID = MainOrderID;
                        if (reader["TotalPriceVND"] != DBNull.Value)
                            entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                        if (reader["Deposit"] != DBNull.Value)
                            entity.Deposit = reader["Deposit"].ToString();
                        if (reader["CreatedDate"] != DBNull.Value)
                            entity.CreatedDate = reader["CreatedDate"].ToString();
                        if (reader["Status"] != DBNull.Value)
                        {
                            entity.Status = Convert.ToInt32(reader["Status"].ToString());
                        }
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                        if (reader["OrderTransactionCode"] != DBNull.Value)
                            entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                        if (reader["OrderTransactionCode2"] != DBNull.Value)
                            entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                        if (reader["OrderTransactionCode3"] != DBNull.Value)
                            entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                        if (reader["OrderTransactionCode4"] != DBNull.Value)
                            entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                        if (reader["OrderTransactionCode5"] != DBNull.Value)
                            entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                        if (reader["Uname"] != DBNull.Value)
                            entity.Uname = reader["Uname"].ToString();
                        if (reader["saler"] != DBNull.Value)
                            entity.saler = reader["saler"].ToString();
                        if (reader["dathang"] != DBNull.Value)
                            entity.dathang = reader["dathang"].ToString();
                        if (reader["anhsanpham"] != DBNull.Value)
                            entity.anhsanpham = reader["anhsanpham"].ToString();
                        if (reader["OrderType"] != DBNull.Value)
                            entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                        if (reader["IsCheckNotiPrice"] != DBNull.Value)
                            entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                        else
                            entity.IsCheckNotiPrice = false;
                        list.Add(entity);
                    }
                }
                else
                {
                    int MainOrderID = reader["ID"].ToString().ToInt(0);
                    var entity = new OrderGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = MainOrderID;
                    if (reader["TotalPriceVND"] != DBNull.Value)
                        entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                    if (reader["Deposit"] != DBNull.Value)
                        entity.Deposit = reader["Deposit"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = reader["CreatedDate"].ToString();
                    if (reader["Status"] != DBNull.Value)
                    {
                        entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    }
                    if (reader["statusstring"] != DBNull.Value)
                    {
                        entity.statusstring = reader["statusstring"].ToString();
                    }
                    if (reader["OrderTransactionCode"] != DBNull.Value)
                        entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                    if (reader["OrderTransactionCode2"] != DBNull.Value)
                        entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                    if (reader["OrderTransactionCode3"] != DBNull.Value)
                        entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                    if (reader["OrderTransactionCode4"] != DBNull.Value)
                        entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                    if (reader["OrderTransactionCode5"] != DBNull.Value)
                        entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                    if (reader["Uname"] != DBNull.Value)
                        entity.Uname = reader["Uname"].ToString();
                    if (reader["saler"] != DBNull.Value)
                        entity.saler = reader["saler"].ToString();
                    if (reader["dathang"] != DBNull.Value)
                        entity.dathang = reader["dathang"].ToString();
                    if (reader["anhsanpham"] != DBNull.Value)
                        entity.anhsanpham = reader["anhsanpham"].ToString();
                    if (reader["OrderType"] != DBNull.Value)
                        entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                    if (reader["IsCheckNotiPrice"] != DBNull.Value)
                        entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                    else
                        entity.IsCheckNotiPrice = false;
                    list.Add(entity);
                }
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserIDInSQLHelper_WithPaging(int userID, int page, int maxrows)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Chờ mua hàng</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Chờ shop TQ phát hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID, ";
            sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
            sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
            sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham ";
            //sql += "  CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"bg-blue\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"bg-yellow\">Đã nhập</span>' ELSE N'<span class=\"bg-red\">Chưa nhập</span>' END AS hasSmallpackage";
            sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " LEFT OUTER JOIN  (SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "        Where UID = " + userID + " ";
            sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = reader["CreatedDate"].ToString();
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;

                //if (reader["hasSmallpackage"] != DBNull.Value)
                //    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserIDInSQLHelper_WithNoPaging(int userID)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Chờ mua hàng</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Chờ shop TQ phát hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID, ";
            sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
            sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
            sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham ";
            //sql += "  CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"bg-blue\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"bg-yellow\">Đã nhập</span>' ELSE N'<span class=\"bg-red\">Chưa nhập</span>' END AS hasSmallpackage";
            sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " LEFT OUTER JOIN  (SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "        Where UID = " + userID + " ";
            sql += " ORDER BY mo.ID DESC";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = reader["CreatedDate"].ToString();
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;

                //if (reader["hasSmallpackage"] != DBNull.Value)
                //    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<MainOrderID> GetMainOrderIDBySearch(string search)
        {
            List<MainOrderID> ods = new List<MainOrderID>();
            var sql = @"Select MainOrderID from tbl_order where title_origin like N'%" + search + "%' GROUP BY MainorderID";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                MainOrderID os = new MainOrderID();
                if (reader["MainOrderID"] != DBNull.Value)
                    os.ID = reader["MainOrderID"].ToString().ToInt(0);
                ods.Add(os);
            }
            reader.Close();
            return ods;
        }
        public static List<MainOrderID> GetSmallPackageMainOrderIDBySearch(string search)
        {
            List<MainOrderID> ods = new List<MainOrderID>();
            var sql = @"Select MainOrderID from tbl_SmallPackage where OrderTransactionCode like N'%" + search + "%' GROUP BY MainorderID";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                MainOrderID os = new MainOrderID();
                if (reader["MainOrderID"] != DBNull.Value)
                    os.ID = reader["MainOrderID"].ToString().ToInt(0);
                ods.Add(os);
            }
            reader.Close();
            return ods;
        }
        public static double GetTotalAmountDeposit(int UID, int status, int OrderType)
        {

            var sql = @"SELECT SUM(CONVERT(decimal(16,0),mo.AmountDeposit)) as TotalAmountDeposit";
            sql += " FROM dbo.tbl_MainOder as mo ";
            sql += " where UID = " + UID + " And OrderType=" + OrderType + " ";

            if (status >= 0)
                sql += " AND Status = " + status;

            double TotalAmountDeposit = 0;
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["TotalAmountDeposit"] != DBNull.Value)
                    TotalAmountDeposit = Convert.ToDouble(reader["TotalAmountDeposit"].ToString());
            }

            reader.Close();
            return TotalAmountDeposit;
        }


        public static string GetTotalPrice(int UID, int status, int OrderType)
        {

            var sql = @"SELECT SUM(CONVERT(decimal(16,0),mo.TotalPriceVND)) as TotalPrice";
            sql += " FROM dbo.tbl_MainOder as mo ";
            sql += " where UID = " + UID + " And OrderType=" + OrderType + " ";

            if (status >= 0)
                sql += " AND Status = " + status;

            string TotalPrice = "0";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["TotalPrice"] != DBNull.Value)
                    TotalPrice = reader["TotalPrice"].ToString();
            }

            reader.Close();
            return TotalPrice;
        }


        public static string TongGiaTriDH(int UID, int OrderType)
        {
            var sql = @"SELECT SUM(CONVERT(decimal(16,0),mo.TotalPriceVND)) as TotalPrice";
            sql += " FROM dbo.tbl_MainOder as mo ";
            sql += " where UID = " + UID + " And OrderType=" + OrderType + " ";

            sql += " AND Status > 2 ";

            string Total = "0";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["TotalPrice"] != DBNull.Value)
                    Total = reader["TotalPrice"].ToString();
            }

            reader.Close();
            return Total;
        }
        public class MainOrderID
        {
            public int ID { get; set; }
        }
        public class mainorder
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public string AmountDeposit { get; set; }
            public string ExpectedDate { get; set; }
            public int Status { get; set; }
            public string ShopName { get; set; }
            public string Site { get; set; }
            public string anhsanpham { get; set; }
            public bool IsGiaohang { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public bool IsShopSendGoods { get; set; }
            public bool IsBuying { get; set; }
            public bool IsPaying { get; set; }
            public int OrderType { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }
            public string quantityPro { get; set; }
            public string hasSmallpackage { get; set; }

            public DateTime CreatedDate { get; set; }
            public string DepostiDate { get; set; }
            public string BuyingDate { get; set; }
            public string ShopSendGoodsDate { get; set; }
            public string BuyProductDate { get; set; }
            public string TQWarehouseDate { get; set; }
            public string VNWarehouseDate { get; set; }
            public string PayAllDate { get; set; }
            public string CompleteDate { get; set; }
        }
        public class OrderGetSQL
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string anhsanpham { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string orderlinks { get; set; }
            public string Site { get; set; }
            public string TotalPriceVND { get; set; }
            public string TotalPriceRealCYN { get; set; }
            public string PriceVND { get; set; }
            public string CurrentCNYVN { get; set; }
            public string FeeShipCN { get; set; }
            public string Deposit { get; set; }
            public string MainOrderCode { get; set; }
            public int UID { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
            public string DepostiDate { get; set; }
            public string PayingDate { get; set; }
            public string ExpectedDate { get; set; }
            public string statusstring { get; set; }
            public string PerformStaff { get; set; }
            public int OrderType { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public bool IsShopSendGoods { get; set; }
            public bool IsBuying { get; set; }
            public bool IsPaying { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }

            public string Uname { get; set; }
            public string dathang { get; set; }
            public string saler { get; set; }
            public string dathangstr { get; set; }
            public string salerstr { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
            public string hasSmallpackage { get; set; }
        }
        public class OrderReportGetSQL
        {
            public int ID { get; set; }
            public int Status { get; set; }
            public int orderlinks { get; set; }
            public int paylinks { get; set; }
        }
        public class OrderReportGetSQLIncome
        {
            public int ID { get; set; }
            public int Status { get; set; }
            public int UID { get; set; }
            public int SalerID { get; set; }
            public int DathangID { get; set; }
            public int orderlinks { get; set; }
            public int paylinks { get; set; }

            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public string IsFastPrice { get; set; }
            public string FeeShipCN { get; set; }
            public string FeeBuyPro { get; set; }
            public string FeeWeight { get; set; }
            public string IsCheckProductPrice { get; set; }
            public string IsPackedPrice { get; set; }
            public string IsFastDeliveryPrice { get; set; }
            public string CurrentCNYVN { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string PriceVND { get; set; }
            public string TotalPriceReal { get; set; }

            public DateTime DepostiDate { get; set; }
            public DateTime BuyProductDate { get; set; }
            public DateTime TQWarehouseDate { get; set; }
            public DateTime VNWarehouseDate { get; set; }
            public DateTime PayAllDate { get; set; }
            public DateTime CreatedDate { get; set; }

            public string OrderWeight { get; set; }

            public string WareHouseName { get; set; }
            public string WareHouseFromName { get; set; }
        }
        #endregion
        public static List<tbl_MainOder> GetByCustomerAndStatus(int customerID, int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(l => l.Status == status && l.UID == customerID).ToList();
                return lo;
            }
        }
        public static string UpdateStatus_DateTime(int ID, int UID, int Status, DateTime BuyProductDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    or.BuyProductDate = BuyProductDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateExpectedDate(int ID, DateTime ExpectedDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.ExpectedDate = ExpectedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static List<tbl_MainOder> GetAllBySQL_IsPaying(string s, int Status, bool IsPaying, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var list = new List<tbl_MainOder>();
                var sql = @"select ac.Username  ,* from tbl_MainOder mo ";
                sql += "left outer join tbl_Account ac on mo.UID = ac.ID ";
                sql += " where ac.ID > 0 AND ac.Username LIKE N'%" + s + "%' ";
                if (Status > -1)
                    sql += " AND Status = " + Status + " ";
                if (IsPaying)
                    sql += " AND IsPaying = 1 ";
                if (!string.IsNullOrEmpty(CreatedBy))
                    sql += " AND CreatedBy LIKE N'%" + CreatedBy + "%' ";
                sql += " Order By ID desc";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                int i = 1;
                while (reader.Read())
                {
                    var entity = new tbl_MainOder();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = reader["ID"].ToString().ToInt(0);
                    if (reader["UID"] != DBNull.Value)
                        entity.UID = reader["UID"].ToString().ToInt();
                    //if (reader["Username"] != DBNull.Value)
                    //    entity.Username = reader["Username"].ToString();
                    if (reader["FullName"] != DBNull.Value)
                        entity.FullName = reader["FullName"].ToString();
                    if (reader["Phone"] != DBNull.Value)
                        entity.Phone = reader["Phone"].ToString();
                    if (reader["Status"] != DBNull.Value)
                        entity.Status = reader["Status"].ToString().ToInt();
                    if (reader["IsPaying"] != DBNull.Value)
                        entity.IsPaying = Convert.ToBoolean(reader["IsPaying"]);
                    if (reader["PriceVND"] != DBNull.Value)
                        entity.PriceVND = reader["PriceVND"].ToString();
                    if (reader["Note"] != DBNull.Value)
                        entity.Note = reader["Note"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    if (reader["CreatedBy"] != DBNull.Value)
                        entity.CreatedBy = reader["CreatedBy"].ToString().ToInt(0);
                    if (reader["ModifiedDate"] != DBNull.Value)
                        entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    if (reader["ModifiedBy"] != DBNull.Value)
                        entity.ModifiedBy = reader["ModifiedBy"].ToString().ToInt(0);

                    i++;
                    list.Add(entity);
                }
                reader.Close();
                return list;
            }
        }
        public static string UpdatePaying(int ID, bool IsPaying, DateTime currentDate,string PerformStaff)
        {
            using (var db = new NHSTEntities())
            {
                var l = db.tbl_MainOder.Where(x => x.ID == ID).FirstOrDefault();
                if (l != null)
                {
                    l.IsPaying = IsPaying;
                    l.PayingDate = currentDate;
                    l.PerformStaff = PerformStaff;
                    db.SaveChanges();
                    return l.ID.ToString();
                }
                else return null;
            }
        }





        public static List<OrderGetSQL> GetOrderbyPaying(string s, int Status,int site, bool IsPaying, string CreatedBy)
        {
            var list = new List<OrderGetSQL>();

            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.MainOrderCode,mo.DepostiDate,mo.CurrentCNYVN,mo.FeeShipCN ,mo.TotalPriceRealCYN ,mo.PriceVND , mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying as IsPaying, mo.CreatedBy, countRowLink as orderlinks, mo.Site, ";
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Chờ mua hàng</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Chờ shop TQ phát hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID ";
            sql += ", mo.SalerID, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
            sql += "  CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";
            sql += "  FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += "  dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += "  dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
            sql += "  (SELECT count(*) AS countRowLink, MainOrderID FROM tbl_Order AS aa GROUP BY aa.MainOrderID) AS aa ON aa.MainOrderID = mo.ID LEFT OUTER JOIN";
            sql += "  (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " where mo.OrderType  = 1";
            //sql += " where u.Username LIKE N'%" + s + "%'";
            //sql += "    AND mo.OrderType  = 1 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND u.Username LIKE N'%" + s + "%' ";
            if (Status == -1)
            {
                sql += " AND mo.Status = 2 AND IsBuying = 1 AND IsPaying = 0 ";
            }
            else if (Status == 0) // chờ thanh toán
            {
                sql += " AND mo.Status = 2 AND IsBuying = 1 AND IsPaying = 0";
            }
            else if (Status == 2) //Đã thanh toán(chờ shop phát hàng)
            {
                sql += " AND mo.Status = 5 AND IsPaying = 1 ";
            }
           
            if (site == -1)
            {
                sql += " And ( mo.Site like N'%1688%' or mo.Site like N'%taobao%' or mo.Site like N'%tmall%' ) ";
            }
            else if (site ==0)
            {
                sql += " And mo.Site like N'%taobao%'";
            }
            else if (site ==1)
            {
                sql += " And mo.Site like N'%1688%'";
            }
            else if (site==2)
            {
                sql += " And mo.Site like N'%tmall%'";
            }
            if (IsPaying)
                sql += " AND IsPaying = 1 ";
            if (!string.IsNullOrEmpty(CreatedBy))
                sql += " AND d.Username LIKE N'%" + CreatedBy + "%' ";

            sql += " ORDER BY mo.DepostiDate DESC ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["TotalPriceRealCYN"] != DBNull.Value)
                    entity.TotalPriceRealCYN = reader["TotalPriceRealCYN"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["FeeShipCN"] != DBNull.Value)
                    entity.FeeShipCN = reader["FeeShipCN"].ToString();
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    entity.CurrentCNYVN = reader["CurrentCNYVN"].ToString();
                if (reader["orderlinks"] != DBNull.Value)
                    entity.orderlinks = reader["orderlinks"].ToString();
                if (reader["Site"] != DBNull.Value)
                    entity.Site = reader["Site"].ToString();
                if (reader["MainOrderCode"] != DBNull.Value)
                    entity.MainOrderCode = reader["MainOrderCode"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = reader["CreatedDate"].ToString();
                if (reader["DepostiDate"] != DBNull.Value)
                    entity.DepostiDate = reader["DepostiDate"].ToString();
                int status = -1;
                bool isShopSendGoods = false;
                bool isBuying = false;
                bool isPaying = false;
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["IsShopSendGoods"] != DBNull.Value)
                    isShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"]);
                entity.IsShopSendGoods = isShopSendGoods;

                if (reader["IsBuying"] != DBNull.Value)
                    isBuying = Convert.ToBoolean(reader["IsBuying"]);
                entity.IsBuying = isBuying;

                if (reader["IsPaying"] != DBNull.Value)
                    isPaying = Convert.ToBoolean(reader["IsPaying"]);
                entity.IsPaying = isPaying;

                if (status == 5)
                {
                    if (isShopSendGoods == true)
                    {
                        entity.statusstring = "<span class=\"bg-green\">Shop đã phát hàng</span>";
                    }
                    else
                    {
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                    }
                }
                else if (status == 2)
                {
                    if (isBuying == true && isPaying == false)
                    {
                        entity.statusstring = "<span class=\"bg-red\">Đang mua hàng</span>";
                    }
                    else if (isPaying == true && isBuying == false)
                    {
                        entity.statusstring = "<span class=\"bg-green\">Đã thanh toán cho shop</span>";
                    }
                    else if (isPaying == true && isBuying == true)
                    {
                        entity.statusstring = "<span class=\"bg-green\">Đã thanh toán cho shop</span>";
                    }
                    else
                    {
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                    }
                }

                else
                {
                    if (reader["statusstring"] != DBNull.Value)
                    {
                        entity.statusstring = reader["statusstring"].ToString();
                    }
                }

                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();


                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;
                list.Add(entity);
            }
            reader.Close();

            return list;
        }
        public static List<OrderGetSQL> Report_GetOrderbyPaying(string s,string PerformStaff, int Status,int site, bool IsPaying, string CreatedBy, string fd, string td)
        {
            var list = new List<OrderGetSQL>();

            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.MainOrderCode , mo.PerformStaff, mo.PayingDate,mo.CurrentCNYVN,mo.FeeShipCN ,mo.TotalPriceRealCYN ,mo.PriceVND , mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying as IsPaying, mo.CreatedBy,  mo.Site, "; //countRowLink as orderlinks,
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Chờ mua hàng</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Chờ shop TQ phát hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID ";
            sql += ", mo.SalerID, u.Username AS Uname, s.Username AS saler, d.Username AS dathang ";
            //sql += "  CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";
            sql += "  FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += "  dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += "  dbo.tbl_Account AS d ON mo.DathangID = d.ID "; //LEFT OUTER JOIN
            //sql += "  (SELECT count(*) AS countRowLink, MainOrderID FROM tbl_Order AS aa GROUP BY aa.MainOrderID) AS aa ON aa.MainOrderID = mo.ID LEFT OUTER JOIN";
            //sql += "  (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " where mo.OrderType  = 1";
            //sql += " where u.Username LIKE N'%" + s + "%'";
            //sql += "    AND mo.OrderType  = 1 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND u.Username LIKE N'%" + s + "%' ";
            if (Status == -1)
            {
                sql += " AND mo.Status = 2 ";
            }
            else if (Status == 0)
            {
                sql += " AND mo.Status = 2 AND IsBuying = 1 AND IsPaying = 0";
            }
            else if (Status == 2)
            {
                sql += " AND mo.Status = 5 AND IsPaying = 1 ";
            }
            else if (Status == 3)
            {
                sql += " AND mo.Status = 2 AND IsBuying = 1 AND IsPaying = 1 ";
            }
            if (site == -1)
            {
                sql += " And ( mo.Site like N'%1688%' or mo.Site like N'%taobao%' or mo.Site like N'%tmall%' ) ";
            }
            else if (site ==0)
            {
                sql += " And mo.Site like N'%taobao%'";
            }
            else if (site ==1)
            {
                sql += " And mo.Site like N'%1688%'";
            }
            else if (site==2)
            {
                sql += " And mo.Site like N'%tmall%'";
            }
            if (IsPaying)
                sql += " AND IsPaying = 1 ";
            if (!string.IsNullOrEmpty(CreatedBy))
                sql += " AND d.Username LIKE N'%" + CreatedBy + "%' ";

            if (!string.IsNullOrEmpty(PerformStaff))
                sql += " AND mo.PerformStaff like N'%" + PerformStaff + "%'";

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.PayingDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.PayingDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }

            sql += " ORDER BY mo.ID DESC ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["TotalPriceRealCYN"] != DBNull.Value)
                    entity.TotalPriceRealCYN = reader["TotalPriceRealCYN"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["FeeShipCN"] != DBNull.Value)
                    entity.FeeShipCN = reader["FeeShipCN"].ToString();
                if (reader["MainOrderCode"] != DBNull.Value)
                    entity.MainOrderCode = reader["MainOrderCode"].ToString();
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    entity.CurrentCNYVN = reader["CurrentCNYVN"].ToString();
                //if (reader["orderlinks"] != DBNull.Value)
                //    entity.orderlinks = reader["orderlinks"].ToString();
                if (reader["Site"] != DBNull.Value)
                    entity.Site = reader["Site"].ToString();
                if (reader["PerformStaff"] != DBNull.Value)
                    entity.PerformStaff = reader["PerformStaff"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = reader["CreatedDate"].ToString();
                if (reader["PayingDate"] != DBNull.Value)
                    entity.PayingDate = reader["PayingDate"].ToString();
                int status = -1;
                bool isShopSendGoods = false;
                bool isBuying = false;
                bool isPaying = false;
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["IsShopSendGoods"] != DBNull.Value)
                    isShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"]);
                entity.IsShopSendGoods = isShopSendGoods;

                if (reader["IsBuying"] != DBNull.Value)
                    isBuying = Convert.ToBoolean(reader["IsBuying"]);
                entity.IsBuying = isBuying;

                if (reader["IsPaying"] != DBNull.Value)
                    isPaying = Convert.ToBoolean(reader["IsPaying"]);
                entity.IsPaying = isPaying;

                if (status == 5)
                {
                    if (isShopSendGoods == true)
                    {
                        entity.statusstring = "<span class=\"bg-green\">Shop đã phát hàng</span>";
                    }
                    else
                    {
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                    }
                }
                else if (status == 2)
                {
                    if (isBuying == true && isPaying == false)
                    {
                        entity.statusstring = "<span class=\"bg-red\">Đang mua hàng</span>";
                    }
                    else if (isPaying == true && isBuying == false)
                    {
                        entity.statusstring = "<span class=\"bg-green\">Đã thanh toán cho shop</span>";
                    }
                    else if (isPaying == true && isBuying == true)
                    {
                        entity.statusstring = "<span class=\"bg-green\">Đã thanh toán cho shop</span>";
                    }
                    else
                    {
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                    }
                }

                else
                {
                    if (reader["statusstring"] != DBNull.Value)
                    {
                        entity.statusstring = reader["statusstring"].ToString();
                    }
                }

                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();


                //if (reader["anhsanpham"] != DBNull.Value)
                //    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;
                list.Add(entity);
            }
            reader.Close();

            return list;
        }


        public static List<mainorder> GetAllByUIDNotHidden_SqlHelperNew(int UID, string search, int typesearch, int status, string fd, string td, int OrderType)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND,mo.ExpectedDate, mo.Deposit,mo.AmountDeposit, mo.CreatedDate , mo.DepostiDate, mo.BuyingDate, mo.BuyProductDate,mo.ShopSendGoodsDate, mo.TQWarehouseDate, mo.VNWarehouseDate, mo.PayAllDate, mo.CompleteDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, mo.OrderType, mo.IsCheckNotiPrice, o.anhsanpham, mo.IsShopSendGoods, mo.IsBuying, mo.IsPaying";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " where UID = " + UID + " AND mo.OrderType = " + OrderType + " ";

            if (status >= 0)
            {
                if (status == 11)
                {
                    sql += " AND Status = 5 AND mo.IsShopSendGoods = 1";
                }
                else if (status == 12)
                {
                    sql += " AND Status = 2 AND mo.IsBuying = 1";
                }
                else if (status == 13)
                {
                    sql += " AND Status = 2 AND mo.IsPaying = 1";
                }
                else
                {
                    sql += " AND Status = " + status;
                }
            }
            if (typesearch != 0)
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (typesearch == 1)
                    {
                        sql += " AND ID=" + search + "";
                    }
                    else if (typesearch == 3)
                    {
                        sql += " AND site like N'%" + search + "%'";
                    }
                }

            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["ExpectedDate"] != DBNull.Value)
                    entity.ExpectedDate = Convert.ToString(reader["ExpectedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                else
                    entity.IsCheckNotiPrice = false;
                if (reader["IsShopSendGoods"] != DBNull.Value)
                    entity.IsShopSendGoods = Convert.ToBoolean(reader["IsShopSendGoods"].ToString());
                else
                    entity.IsShopSendGoods = false;

                if (reader["IsBuying"] != DBNull.Value)
                    entity.IsBuying = Convert.ToBoolean(reader["IsBuying"].ToString());
                else
                    entity.IsBuying = false;

                if (reader["IsPaying"] != DBNull.Value)
                    entity.IsPaying = Convert.ToBoolean(reader["IsPaying"].ToString());
                else
                    entity.IsPaying = false;

                if (reader["DepostiDate"] != DBNull.Value)
                    entity.DepostiDate = reader["DepostiDate"].ToString();
                if (reader["BuyingDate"] != DBNull.Value)
                    entity.BuyingDate = reader["BuyingDate"].ToString();
                if (reader["BuyProductDate"] != DBNull.Value)
                    entity.BuyProductDate = reader["BuyProductDate"].ToString();
                if (reader["ShopSendGoodsDate"] != DBNull.Value)
                    entity.ShopSendGoodsDate = reader["ShopSendGoodsDate"].ToString();
                if (reader["TQWarehouseDate"] != DBNull.Value)
                    entity.TQWarehouseDate = reader["TQWarehouseDate"].ToString();
                if (reader["VNWarehouseDate"] != DBNull.Value)
                    entity.VNWarehouseDate = reader["VNWarehouseDate"].ToString();
                if (reader["PayAllDate"] != DBNull.Value)
                    entity.PayAllDate = reader["PayAllDate"].ToString();
                if (reader["CompleteDate"] != DBNull.Value)
                    entity.CompleteDate = reader["CompleteDate"].ToString();

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<View_OrderDetailExcel> GetAllBySaleID_View(int UID, int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderDetailExcel> aus = new List<View_OrderDetailExcel>();
                aus = dbe.View_OrderDetailExcel.Where(a => a.UID == UID && a.ID == MainOrderID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }

        public static int GetTotalLink(int UID, string fd, string td)
        {
            int Count = 0;
            var sql = @"SELECT COUNT(*) as Total from tbl_MainOder as mo";
            sql += "        Where Status >= 5 And DathangID=" + UID;
            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
        public static int GetTotal(string fd, string td, string Username, int Status)
        {
            int Count = 0;
            var sql = @"select  COUNT(*) as Total from tbl_MainOder mo ";
            sql += "left outer join (select ID,Username from tbl_Account )a on mo.UID = a.ID ";
            sql += "left join (select DISTINCT MainOrderID, Status from tbl_SmallPackage) as sm on mo.ID = sm.MainOrderID ";
            sql += "where mo.ID > 0 and sm.Status = 3 ";

            if (Status == -1)
            {
                sql += " AND mo.Status >= 7 and mo.Status <10 ";
            }
            else if (Status == 0)
            {
                sql += " AND mo.Status = 7 ";
            }
            else if (Status == 2)
            {
                sql += " AND mo.Status = 9 ";
            }


            if (!string.IsNullOrEmpty(Username))
                sql += " AND Username like N'%" + Username + "%'";

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.BuyProductDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.BuyProductDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
        public static List<tbl_MainOder> GetAllOrderByWarehouseVN(string fd, string td, string Username, int Status, int page, int maxrows)
        {
            var list = new List<tbl_MainOder>();

            var sql = @"select *,a.Username from tbl_MainOder mo  ";
            sql += "left outer join (select ID ,Username from tbl_Account )a on mo.UID = a.ID ";
            sql += "left join (select DISTINCT MainOrderID, Status from tbl_SmallPackage) as sm on mo.ID = sm.MainOrderID ";
            sql += "where mo.ID > 0 and sm.Status = 3 ";
            if (Status == -1)
            {
                sql += " AND mo.Status >= 7 and mo.Status <10 ";
            }
            else if (Status == 0)
            {
                sql += " AND mo.Status = 7 ";
            }
            else if (Status == 2)
            {
                sql += " AND mo.Status = 9 ";
            }


            if (!string.IsNullOrEmpty(Username))
                sql += " AND a.Username like N'%" + Username + "%'";

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.BuyProductDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.BuyProductDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }

            sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_MainOder();
                #region code mới

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["Address"] != DBNull.Value)
                    entity.Address = reader["Address"].ToString();
                if (reader["Email"] != DBNull.Value)
                    entity.Email = reader["Email"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                i++;
                list.Add(entity);

                #endregion
            }
            reader.Close();
            return list;
        }

        public static List<tbl_MainOder> GetFromDateToDateNew(string fd, string td, string Username, int Status)
        {
            var list = new List<tbl_MainOder>();

            var sql = @"select *,a.Username from tbl_MainOder mo  ";
            sql += "left outer join (select ID ,Username from tbl_Account )a on mo.UID = a.ID ";
            sql += "where mo.ID > 0";
            if (Status == -1)
            {
                sql += " AND mo.Status >= 7 and mo.Status <10 ";
            }
            else if (Status == 0)
            {
                sql += " AND mo.Status = 7 ";
            }
            else if (Status == 2)
            {
                sql += " AND mo.Status = 9 ";
            }


            if (!string.IsNullOrEmpty(Username))
                sql += " AND a.Username like N'%" + Username + "%'";

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }

            sql += " ORDER BY a.ID DESC ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_MainOder();
                #region code mới

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["Address"] != DBNull.Value)
                    entity.Address = reader["Address"].ToString();
                if (reader["Email"] != DBNull.Value)
                    entity.Email = reader["Email"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                i++;
                list.Add(entity);

                #endregion
            }
            reader.Close();
            return list;
        }


        //public static List<IncomSaler> GetFromDateToDate_IncomSaler(string fd, string td, string Username, string Saler, int Status, int department)
        //{
        //    var list = new List<IncomSaler>();

        //    var sql = @"select  kh.Username, ac.Username as Saler, ac.ID as SalerID";
        //    sql += ", Sum(convert(float,mo.TotalPriceVND)) as TotalPriceVND ";
        //    sql += ", Sum(convert(float,mo.PriceVND)) as PriceVND ";
        //    sql += ", Sum(convert(float,mo.FeeBuyPro)) as FeeBuyPro ";
        //    sql += ", Sum(convert(float,mo.FeeShipCN)) as FeeShipCN ";
        //    sql += ", Sum(convert(float,ISNULL(mo.TotalPriceReal,0))) as TotalPriceReal ";
        //    sql += ", Sum(convert(float,mo.PriceVND)+convert(float,mo.FeeBuyPro)+(convert(float,mo.FeeShipCN))+(convert(float,mo.FeeWeight))) as giatridonhang ";
        //    sql += ", Sum(convert(float,mo.FeeBuyPro)+(convert(float,mo.FeeShipCN))+(convert(float,mo.FeeWeight))) as phidonhang ";
        //    sql += ", Sum((convert(float,mo.PriceVND) +(convert(float,mo.FeeShipCN)))-(convert(float,ISNULL(mo.TotalPriceReal,0)))) as tienmacca ";
        //    //sql += ", Sum(convert(float,mo.TotalPriceVND)-((convert(float,mo.FeeShipCN))+(convert(float,mo.TotalPriceReal)))) as tienmacca ";
        //    sql += ", Sum(convert(float,ISNULL(mo.TotalPriceReal,0))) as TotalPriceReal ";
        //    sql += ", Sum(convert(float,mo.TQVNWeight)) as TQVNWeight ";
        //    sql += ", Sum(convert(float,mo.FeeWeight)) as FeeWeight, Count(mo.ID) as TotalOrder from tbl_MainOder as mo, ";
        //    sql += "tbl_Account as ac, ";
        //    sql += "tbl_Account as kh ";
        //    sql += "where mo.SalerID = ac.ID and mo.UID = kh.ID ";



        //    if (Status == 1)
        //    {
        //        if (!string.IsNullOrEmpty(Username))
        //            sql += " AND kh.Username like N'%" + Username + "%'";
        //        if (!string.IsNullOrEmpty(Saler))
        //            sql += " AND ac.Username like N'%" + Saler + "%'";
        //        sql += " AND mo.Status > 2 ";
        //        if (department == 0)
        //        {
        //            sql += " AND ac.Department > 0  ";
        //        }
        //        if (department == 1)
        //        {
        //            sql += " AND ac.Department = 1 ";
        //        }
        //        if (department == 2)
        //        {
        //            sql += " AND ac.Department = 2 ";
        //        }
        //        if (department == 3)
        //        {
        //            sql += " AND ac.Department = 3 ";
        //        }
        //        if (department == 4)
        //        {
        //            sql += " AND ac.Department = 4 ";
        //        }
        //        if (!string.IsNullOrEmpty(fd))
        //        {
        //            var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //            sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
        //        }
        //        if (!string.IsNullOrEmpty(td))
        //        {
        //            var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //            sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
        //        }
        //    }
        //    else if (Status == 2)
        //    {
        //        sql += " AND ( mo.Status >= 5) ";
        //        if (!string.IsNullOrEmpty(Username))
        //            sql += " AND kh.Username like N'%" + Username + "%'";
        //        if (!string.IsNullOrEmpty(Saler))
        //            sql += " AND ac.Username like N'%" + Saler + "%'";
        //        if (department == 0)
        //        {
        //            sql += " AND ac.Department > 0  ";
        //        }
        //        if (department == 1)
        //        {
        //            sql += " AND ac.Department = 1 ";
        //        }
        //        if (department == 2)
        //        {
        //            sql += " AND ac.Department = 2 ";
        //        }
        //        if (department == 3)
        //        {
        //            sql += " AND ac.Department = 3 ";
        //        }
        //        if (department == 4)
        //        {
        //            sql += " AND ac.Department = 4 ";
        //        }
        //        if (!string.IsNullOrEmpty(fd))
        //        {
        //            var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //            sql += " AND mo.BuyProductDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
        //        }
        //        if (!string.IsNullOrEmpty(td))
        //        {
        //            var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //            sql += " AND mo.BuyProductDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
        //        }
        //    }
        //    else /*if (Status == 3)*/
        //    {
        //        sql += " AND mo.Status = 10 ";
        //        if (!string.IsNullOrEmpty(Username))
        //            sql += " AND kh.Username like N'%" + Username + "%'";

        //        if (!string.IsNullOrEmpty(Saler))
        //            sql += " AND ac.Username like N'%" + Saler + "%'";

        //        if (department == 0)
        //        {
        //            sql += " AND ac.Department > 0  ";
        //        }
        //        if (department == 1)
        //        {
        //            sql += " AND ac.Department = 1 ";
        //        }
        //        if (department == 2)
        //        {
        //            sql += " AND ac.Department = 2 ";
        //        }
        //        if (department == 3)
        //        {
        //            sql += " AND ac.Department = 3 ";
        //        }
        //        if (department == 4)
        //        {
        //            sql += " AND ac.Department = 4 ";
        //        }
        //        if (!string.IsNullOrEmpty(fd))
        //        {
        //            var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //            sql += " AND mo.CompleteDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
        //        }
        //        if (!string.IsNullOrEmpty(td))
        //        {
        //            var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //            sql += " AND mo.CompleteDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
        //        }
        //    }

        //    sql += " group by  ac.Username , ac.ID , kh.Username ";
        //    var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
        //    int i = 1;
        //    while (reader.Read())
        //    {
        //        var entity = new IncomSaler();
        //        #region code mới

        //        if (reader["SalerID"] != DBNull.Value)
        //            entity.SalerID = reader["SalerID"].ToString().ToInt(0);
        //        if (reader["Username"] != DBNull.Value)
        //            entity.Username = reader["Username"].ToString();
        //        if (reader["Saler"] != DBNull.Value)
        //            entity.Saler = reader["Saler"].ToString();
        //        if (reader["TotalPriceVND"] != DBNull.Value)
        //            entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
        //        if (reader["PriceVND"] != DBNull.Value)
        //            entity.PriceVND = reader["PriceVND"].ToString();
        //        if (reader["FeeBuyPro"] != DBNull.Value)
        //            entity.FeeBuyPro = reader["FeeBuyPro"].ToString();
        //        if (reader["FeeShipCN"] != DBNull.Value)
        //            entity.FeeShipCN = reader["FeeShipCN"].ToString();
        //        if (reader["giatridonhang"] != DBNull.Value)
        //            entity.giatridonhang = reader["giatridonhang"].ToString();
        //        if (reader["phidonhang"] != DBNull.Value)
        //            entity.phidonhang = reader["phidonhang"].ToString();
        //        if (reader["tienmacca"] != DBNull.Value)
        //            entity.tienmacca = reader["tienmacca"].ToString();
        //        if (reader["TotalPriceReal"] != DBNull.Value)
        //            entity.TotalPriceReal = reader["TotalPriceReal"].ToString();
        //        if (reader["TQVNWeight"] != DBNull.Value)
        //            entity.TQVNWeight = reader["TQVNWeight"].ToString();
        //        if (reader["FeeWeight"] != DBNull.Value)
        //            entity.FeeWeight = reader["FeeWeight"].ToString();
        //        if (reader["TotalOrder"] != DBNull.Value)
        //            entity.TotalOrder = reader["TotalOrder"].ToString();


        //        i++;
        //        list.Add(entity);

        //        #endregion
        //    }
        //    reader.Close();
        //    return list;
        //}


        public static List<IncomSaler> GetFromDateToDate_IncomSaler(string fd, string td, string Username, string Saler, int Status, int department)
        {
            var list = new List<IncomSaler>();

            var sql = @"select  kh.Username, ac.Username as Saler, ac.ID as SalerID";
            sql += ", Sum(convert(float,mo.TotalPriceVND)) as TotalPriceVND ";
            sql += ", Sum(convert(float,mo.PriceVND)) as PriceVND ";
            sql += ", Sum(convert(float,mo.FeeBuyPro)) as FeeBuyPro ";
            sql += ", Sum(convert(float,mo.FeeShipCN)) as FeeShipCN ";
            sql += ", Sum(convert(float,mo.Deposit)) as Deposit , (Sum(convert(float,mo.TotalPriceVND)) - Sum(convert(float,mo.Deposit)))ConLai ";
            sql += ", Sum(convert(float,ISNULL(mo.TotalPriceReal,0))) as TotalPriceReal ";
            sql += ", Sum(convert(float,mo.PriceVND)+convert(float,mo.FeeBuyPro)+(convert(float,mo.FeeShipCN))+(convert(float,mo.FeeWeight))) as giatridonhang ";
            sql += ", Sum(convert(float,mo.FeeBuyPro)+(convert(float,mo.FeeShipCN))+(convert(float,mo.FeeWeight))) as phidonhang ";
            sql += ", Sum((convert(float,mo.PriceVND) +(convert(float,mo.FeeShipCN)))-(convert(float,ISNULL(mo.TotalPriceReal,0)))) as tienmacca ";
            //sql += ", Sum(convert(float,mo.TotalPriceVND)-((convert(float,mo.FeeShipCN))+(convert(float,mo.TotalPriceReal)))) as tienmacca ";
            sql += ", Sum(convert(float,ISNULL(mo.TotalPriceReal,0))) as TotalPriceReal ";
            sql += ", Sum(convert(float,mo.TQVNWeight)) as TQVNWeight ";
            sql += ", Sum(convert(float,mo.FeeWeight)) as FeeWeight, Count(mo.ID) as TotalOrder from tbl_MainOder as mo, ";
            sql += "tbl_Account as ac, ";
            sql += "tbl_Account as kh ";
            sql += "where mo.SalerID = ac.ID and mo.UID = kh.ID ";



            if (Status == 1)
            {
                if (!string.IsNullOrEmpty(Username))
                    sql += " AND kh.Username like N'%" + Username + "%'";
                if (!string.IsNullOrEmpty(Saler))
                    sql += " AND ac.Username like N'%" + Saler + "%'";
                sql += " AND mo.Status > 2 ";
                //if (department == 0)
                //{
                //    sql += " AND ac.Department > 0  ";
                //}
                if (department == 1)
                {
                    sql += " AND ac.Department = 1 ";
                }
                if (department == 2)
                {
                    sql += " AND ac.Department = 2 ";
                }
                if (department == 3)
                {
                    sql += " AND ac.Department = 3 ";
                }
                if (department == 4)
                {
                    sql += " AND ac.Department = 4 ";
                }
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
            }
            else if (Status == 2)
            {
                sql += " AND ( mo.Status >= 5) ";
                if (!string.IsNullOrEmpty(Username))
                    sql += " AND kh.Username like N'%" + Username + "%'";
                if (!string.IsNullOrEmpty(Saler))
                    sql += " AND ac.Username like N'%" + Saler + "%'";
                //if (department == 0)
                //{
                //    sql += " AND ac.Department > 0  ";
                //}
                if (department == 1)
                {
                    sql += " AND ac.Department = 1 ";
                }
                if (department == 2)
                {
                    sql += " AND ac.Department = 2 ";
                }
                if (department == 3)
                {
                    sql += " AND ac.Department = 3 ";
                }
                if (department == 4)
                {
                    sql += " AND ac.Department = 4 ";
                }
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.BuyProductDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.BuyProductDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
            }
            else /*if (Status == 3)*/
            {
                sql += " AND mo.Status = 10 ";
                if (!string.IsNullOrEmpty(Username))
                    sql += " AND kh.Username like N'%" + Username + "%'";

                if (!string.IsNullOrEmpty(Saler))
                    sql += " AND ac.Username like N'%" + Saler + "%'";

                //if (department == 0)
                //{
                //    sql += " AND ac.Department > 0  ";
                //}
                if (department == 1)
                {
                    sql += " AND ac.Department = 1 ";
                }
                if (department == 2)
                {
                    sql += " AND ac.Department = 2 ";
                }
                if (department == 3)
                {
                    sql += " AND ac.Department = 3 ";
                }
                if (department == 4)
                {
                    sql += " AND ac.Department = 4 ";
                }
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CompleteDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CompleteDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
            }

            sql += " group by  ac.Username , ac.ID , kh.Username ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new IncomSaler();
                #region code mới

                if (reader["SalerID"] != DBNull.Value)
                    entity.SalerID = reader["SalerID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Saler"] != DBNull.Value)
                    entity.Saler = reader["Saler"].ToString();
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["FeeBuyPro"] != DBNull.Value)
                    entity.FeeBuyPro = reader["FeeBuyPro"].ToString();
                if (reader["FeeShipCN"] != DBNull.Value)
                    entity.FeeShipCN = reader["FeeShipCN"].ToString();
                if (reader["giatridonhang"] != DBNull.Value)
                    entity.giatridonhang = reader["giatridonhang"].ToString();
                if (reader["phidonhang"] != DBNull.Value)
                    entity.phidonhang = reader["phidonhang"].ToString();
                if (reader["tienmacca"] != DBNull.Value)
                    entity.tienmacca = reader["tienmacca"].ToString();
                if (reader["TotalPriceReal"] != DBNull.Value)
                    entity.TotalPriceReal = reader["TotalPriceReal"].ToString();
                if (reader["TQVNWeight"] != DBNull.Value)
                    entity.TQVNWeight = reader["TQVNWeight"].ToString();
                if (reader["FeeWeight"] != DBNull.Value)
                    entity.FeeWeight = reader["FeeWeight"].ToString();
                if (reader["TotalOrder"] != DBNull.Value)
                    entity.TotalOrder = reader["TotalOrder"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["ConLai"] != DBNull.Value)
                    entity.ConLai = reader["ConLai"].ToString();

                i++;
                list.Add(entity);

                #endregion
            }
            reader.Close();
            return list;
        }

        public class IncomSaler
        {
            public string Username { get; set; }
            public string Saler { get; set; }
            public int SalerID { get; set; }
            public string TotalPriceVND { get; set; }
            public string PriceVND { get; set; }
            public string FeeBuyPro { get; set; }
            public string FeeShipCN { get; set; }
            public string giatridonhang { get; set; }
            public string phidonhang { get; set; }
            public string tienmacca { get; set; }
            public string TotalPriceReal { get; set; }
            public string TQVNWeight { get; set; }
            public string FeeWeight { get; set; }
            public string TotalOrder { get; set; }

            public string Deposit { get; set; }
            public string ConLai { get; set; }
        }

        public static List<tbl_MainOder> GetListTakeOrder(double TotalPrice, int MaxOrder, int Site)
        {
            var list = new List<tbl_MainOder>();

            var sql = @"select * from tbl_MainOder where Status = 2 and DathangID < 1 and Convert(float, TotalPriceVND) <= " + TotalPrice + " ";
            //if (Site == 1)
            //{
            //    sql += " And Site Not like N'%1688%'";
            //}
            //else
            //{
            //    sql += " And Site like N'%1688%'";
            //}
            if (Site == 1)
            {
                sql += " And Site Not like N'%1688%'";
            }
            else if (Site == 2)
            {
                sql += " And Site like N'%1688%'";
            }
            else
            {
                sql += " And ( Site like N'%1688%' or Site like N'%taobao%' or Site like N'%tmall%' )";
            }

            sql += " ORDER BY ID ASC OFFSET 0 ROWS FETCH NEXT " + MaxOrder + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_MainOder();
                #region code mới

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                i++;
                list.Add(entity);

                #endregion
            }
            reader.Close();
            return list;
        }

        public static int GetNumberTakeDate(int UID, DateTime CurrentDate)
        {
            int Count = 0;
            var sql = @"select Count(*) as Total from tbl_MainOder where DathangID=" + UID + " and MONTH(NumberTakeDate) = MONTH('" + CurrentDate + "')  ";
            sql += " and DAY(NumberTakeDate) = DAY('" + CurrentDate + "') and year(NumberTakeDate) = year('" + CurrentDate + "') ";


            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }

        public static List<SiteObject> GetFromDateToDate_IncomeBuyer(string fd, string td, string Username)
        {
            var list = new List<SiteObject>();

            //var sql = @"select ac.Username as dathang, ac.ID as dathangID, mo.Site ,  Sum(convert(float,mo.TotalPriceVND)) as TotalPriceVND, Sum(convert(float,mo.PriceVND)) as PriceVND";
            //sql += ", Sum(convert(float,mo.FeeBuyPro)) as FeeBuyPro, Sum(convert(float,mo.FeeShipCN)) as FeeShipCN, Sum(convert(float,mo.TotalPriceReal)) as TotalPriceReal ";
            //sql += ", Sum(convert(float,mo.TQVNWeight)) as TQVNWeight, Sum(convert(float,mo.FeeWeight)) as FeeWeight, Count(mo.ID) as TotalOrder ";
            //sql += ", Sum((convert(float,mo.PriceVND) +(convert(float,mo.FeeShipCN)))-(convert(float,mo.TotalPriceReal))) as tienmacca ";
            //sql += "from tbl_MainOder as mo, ";
            //sql += "tbl_Account as ac ";
            //sql += "where mo.dathangid = ac.ID and mo.Status > 2 ";


            var sql = @" select ac.Username as dathang, ac.ID as dathangID, mo.Site ";
            sql += ", Sum(convert(float,mo.TotalPriceVND)) as TotalPriceVND ";
            sql += ", Sum(convert(float,mo.PriceVND)) as PriceVND ";
            sql += ", Sum(convert(float,mo.FeeBuyPro)) as FeeBuyPro ";
            sql += ", Sum(convert(float,mo.FeeShipCN)) as FeeShipCN ";
            sql += ", Sum(convert(float,ISNULL(mo.TotalPriceReal,0))) as TotalPriceReal ";
            sql += ", Sum(convert(float,mo.TQVNWeight)) as TQVNWeight ";
            sql += ", Sum(convert(float,mo.FeeWeight)) as FeeWeight ";
            sql += ", Count(mo.ID) as TotalOrder ";
            sql += ", dbo.CountDonDamPhan(ac.ID, mo.Site) dondamphan ";
            sql += ", ((convert(float,dbo.CountDonDamPhan(ac.ID, mo.Site)) / Count(convert(float,mo.ID))) * 100 ) as tyledamphanTC ";
            sql += ", Sum((convert(float,mo.PriceVND) + (convert(float,mo.FeeShipCN))) - (convert(float,ISNULL(mo.TotalPriceReal,0)))) / (Sum(convert(float,mo.PriceVND) + (convert(float,mo.FeeShipCN)))) * 100  as tyledamphan ";
            sql += ", Sum((convert(float,mo.PriceVND) +(convert(float,mo.FeeShipCN)))-(convert(float,ISNULL(mo.TotalPriceReal,0)))) as tienmacca ";
            sql += " from tbl_MainOder as mo ";
            sql += ", tbl_Account as ac ";
            sql += " where mo.dathangid = ac.ID and mo.Status > 2 ";
            //sql += " GROUP BY ac.Username, ac.ID, mo.Site ";



            if (!string.IsNullOrEmpty(Username))
                sql += " AND ac.Username like N'%" + Username + "%'";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += "group by  ac.Username , ac.ID,mo.Site";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                #region code mới
                int UID = reader["dathangID"].ToString().ToInt(0);
                var check = list.Where(x => x.DathangID == UID).FirstOrDefault();
                if (check != null)
                {
                    if (reader["Site"] != DBNull.Value)
                    {
                        if (reader["Site"].ToString().ToLower() == "taobao")
                        {
                            if (reader["TotalOrder"] != DBNull.Value)
                                check.TotalOrderTaobao = reader["TotalOrder"].ToString().ToInt(0);

                            if (reader["PriceVND"] != DBNull.Value)
                                check.PriceVNDTaobao = Convert.ToDouble(reader["PriceVND"].ToString());

                            if (reader["tienmacca"] != DBNull.Value)
                                check.MacCaTaobao = Convert.ToDouble(reader["tienmacca"].ToString());

                            if (reader["FeeShipCN"] != DBNull.Value)
                                check.FeeShipCNTaobao = Convert.ToDouble(reader["FeeShipCN"].ToString());

                            if (reader["dondamphan"] != DBNull.Value)
                                check.dondamphanTaobao = Convert.ToDouble(reader["dondamphan"].ToString());

                            if (reader["tyledamphanTC"] != DBNull.Value)
                                check.tyledamphanTCTaobao = Convert.ToDouble(reader["tyledamphanTC"].ToString());

                            if (reader["tyledamphan"] != DBNull.Value)
                                check.tyledamphanTaobao = Convert.ToDouble(reader["tyledamphan"].ToString());
                        }
                        else if (reader["Site"].ToString().ToLower() == "tmall")
                        {
                            if (reader["TotalOrder"] != DBNull.Value)
                                check.TotalOrderTmall = reader["TotalOrder"].ToString().ToInt(0);

                            if (reader["PriceVND"] != DBNull.Value)
                                check.PriceVNDTmall = Convert.ToDouble(reader["PriceVND"].ToString());

                            if (reader["tienmacca"] != DBNull.Value)
                                check.MacCaTmall = Convert.ToDouble(reader["tienmacca"].ToString());

                            if (reader["FeeShipCN"] != DBNull.Value)
                                check.FeeShipCNTmall = Convert.ToDouble(reader["FeeShipCN"].ToString());

                            if (reader["dondamphan"] != DBNull.Value)
                                check.dondamphanTmall = Convert.ToDouble(reader["dondamphan"].ToString());

                            if (reader["tyledamphanTC"] != DBNull.Value)
                                check.tyledamphanTCTmall = Convert.ToDouble(reader["tyledamphanTC"].ToString());

                            if (reader["tyledamphan"] != DBNull.Value)
                                check.tyledamphanTmall = Convert.ToDouble(reader["tyledamphan"].ToString());
                        }
                        else
                        {
                            if (reader["TotalOrder"] != DBNull.Value)
                                check.TotalOrder1688 = reader["TotalOrder"].ToString().ToInt(0);

                            if (reader["PriceVND"] != DBNull.Value)
                                check.PriceVND1688 = Convert.ToDouble(reader["PriceVND"].ToString());

                            if (reader["tienmacca"] != DBNull.Value)
                                check.MacCa1688 = Convert.ToDouble(reader["tienmacca"].ToString());

                            if (reader["FeeShipCN"] != DBNull.Value)
                                check.FeeShipCN1688 = Convert.ToDouble(reader["FeeShipCN"].ToString());

                            if (reader["dondamphan"] != DBNull.Value)
                                check.dondamphan1688 = Convert.ToDouble(reader["dondamphan"].ToString());

                            if (reader["tyledamphanTC"] != DBNull.Value)
                                check.tyledamphanTC1688 = Convert.ToDouble(reader["tyledamphanTC"].ToString());

                            if (reader["tyledamphan"] != DBNull.Value)
                                check.tyledamphan1688 = Convert.ToDouble(reader["tyledamphan"].ToString());
                        }
                    }
                }
                else
                {
                    var entity = new SiteObject();
                    if (reader["Site"] != DBNull.Value)
                    {
                        if (reader["DathangID"] != DBNull.Value)
                            entity.DathangID = reader["DathangID"].ToString().ToInt(0);
                        if (reader["Dathang"] != DBNull.Value)
                            entity.Dathang = reader["Dathang"].ToString();

                        if (reader["Site"].ToString().ToLower() == "taobao")
                        {
                            if (reader["TotalOrder"] != DBNull.Value)
                                entity.TotalOrderTaobao = reader["TotalOrder"].ToString().ToInt(0);

                            if (reader["PriceVND"] != DBNull.Value)
                                entity.PriceVNDTaobao = Convert.ToDouble(reader["PriceVND"].ToString());

                            if (reader["tienmacca"] != DBNull.Value)
                                entity.MacCaTaobao = Convert.ToDouble(reader["tienmacca"].ToString());

                            if (reader["FeeShipCN"] != DBNull.Value)
                                entity.FeeShipCNTaobao = Convert.ToDouble(reader["FeeShipCN"].ToString());

                            if (reader["dondamphan"] != DBNull.Value)
                                entity.dondamphanTaobao = Convert.ToDouble(reader["dondamphan"].ToString());

                            if (reader["tyledamphanTC"] != DBNull.Value)
                                entity.tyledamphanTCTaobao = Convert.ToDouble(reader["tyledamphanTC"].ToString());

                            if (reader["tyledamphan"] != DBNull.Value)
                                entity.tyledamphanTaobao = Convert.ToDouble(reader["tyledamphan"].ToString());
                        }
                        else if (reader["Site"].ToString().ToLower() == "tmall")
                        {
                            if (reader["TotalOrder"] != DBNull.Value)
                                entity.TotalOrderTmall = reader["TotalOrder"].ToString().ToInt(0);

                            if (reader["PriceVND"] != DBNull.Value)
                                entity.PriceVNDTmall = Convert.ToDouble(reader["PriceVND"].ToString());

                            if (reader["tienmacca"] != DBNull.Value)
                                entity.MacCaTmall = Convert.ToDouble(reader["tienmacca"].ToString());

                            if (reader["FeeShipCN"] != DBNull.Value)
                                entity.FeeShipCNTmall = Convert.ToDouble(reader["FeeShipCN"].ToString());

                            if (reader["dondamphan"] != DBNull.Value)
                                entity.dondamphanTmall = Convert.ToDouble(reader["dondamphan"].ToString());


                            if (reader["tyledamphanTC"] != DBNull.Value)
                                entity.tyledamphanTCTmall = Convert.ToDouble(reader["tyledamphanTC"].ToString());

                            if (reader["tyledamphan"] != DBNull.Value)
                                entity.tyledamphanTmall = Convert.ToDouble(reader["tyledamphan"].ToString());
                        }
                        else
                        {
                            if (reader["TotalOrder"] != DBNull.Value)
                                entity.TotalOrder1688 = reader["TotalOrder"].ToString().ToInt(0);

                            if (reader["PriceVND"] != DBNull.Value)
                                entity.PriceVND1688 = Convert.ToDouble(reader["PriceVND"].ToString());

                            if (reader["tienmacca"] != DBNull.Value)
                                entity.MacCa1688 = Convert.ToDouble(reader["tienmacca"].ToString());

                            if (reader["FeeShipCN"] != DBNull.Value)
                                entity.FeeShipCN1688 = Convert.ToDouble(reader["FeeShipCN"].ToString());

                            if (reader["dondamphan"] != DBNull.Value)
                                entity.dondamphan1688 = Convert.ToDouble(reader["dondamphan"].ToString());

                            if (reader["tyledamphanTC"] != DBNull.Value)
                                entity.tyledamphanTC1688 = Convert.ToDouble(reader["tyledamphanTC"].ToString());

                            if (reader["tyledamphan"] != DBNull.Value)
                                entity.tyledamphan1688 = Convert.ToDouble(reader["tyledamphan"].ToString());
                        }
                    }
                    list.Add(entity);
                }
                #endregion
            }
            reader.Close();
            return list;
        }

        public static List<ReportGeneral> GetFromDateToDate_RepostGeneral(string fd, string td)
        {
            var list = new List<ReportGeneral>();

            var sql = @"select Sum(convert(float,mo.TotalPriceVND)) as tongdoanhthu";
            sql += ", Count(mo.ID) as tongdon ";
            sql += ", Sum(convert(float,mo.TotalPriceRealCYN)) as tongtientrashopCNY ";
            sql += ", Sum(convert(float,mo.TotalPriceReal)) as tongtientrashopVND ";
            sql += ", Sum(convert(float,mo.FeeBuyPro) + convert(float,mo.PriceVND) +(convert(float,mo.FeeShipCN))-convert(float,mo.TotalPriceReal)) as doanhthudichvumuahnag  ";
            sql += ", Sum(convert(float,mo.FeeBuyPro)) as phimuahang ";
            sql += ", Sum(convert(float,mo.PriceVND) + convert(float,mo.FeeShipCN) - convert(float,mo.TotalPriceReal)) as tienmacca ";
            sql += ", Sum(convert(float,mo.PriceVND)) as PriceVND ";
            sql += ", Sum(convert(float,mo.CurrentCNYVN)) as CurrentCNYVN ";
            sql += ", Sum(convert(float,mo.FeeShipCN)) as FeeShipCN ";
            sql += ", Sum(convert(float,mo.TotalPriceReal)) as TotalPriceReal ";
            sql += ", Sum(convert(float,mo.TQVNWeight)) as tongcannang ";
            sql += ", Sum(convert(float,mo.FeeWeight)) as phicannang ";
            sql += ", Sum(convert(float,mo.IsFastDeliveryPrice)) as tongphigiaotannha ";
            sql += "from tbl_MainOder as mo  ";
            sql += "where mo.Status > 2 ";

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }


            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new ReportGeneral();
                #region code mới

                if (reader["tongdon"] != DBNull.Value)
                    entity.tongdon = reader["tongdon"].ToString().ToInt(0);
                if (reader["tongdoanhthu"] != DBNull.Value)
                    entity.tongdoanhthu = reader["tongdoanhthu"].ToString();
                if (reader["tongtientrashopCNY"] != DBNull.Value)
                    entity.tongtientrashopCNY = reader["tongtientrashopCNY"].ToString();
                if (reader["tongtientrashopVND"] != DBNull.Value)
                    entity.tongtientrashopVND = reader["tongtientrashopVND"].ToString();
                if (reader["doanhthudichvumuahnag"] != DBNull.Value)
                    entity.doanhthudichvumuahnag = reader["doanhthudichvumuahnag"].ToString();
                if (reader["phimuahang"] != DBNull.Value)
                    entity.phimuahang = reader["phimuahang"].ToString();
                if (reader["tienmacca"] != DBNull.Value)
                    entity.tienmacca = reader["tienmacca"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    entity.CurrentCNYVN = reader["CurrentCNYVN"].ToString();
                if (reader["FeeShipCN"] != DBNull.Value)
                    entity.FeeShipCN = reader["FeeShipCN"].ToString();
                if (reader["TotalPriceReal"] != DBNull.Value)
                    entity.TotalPriceReal = reader["TotalPriceReal"].ToString();
                if (reader["tongcannang"] != DBNull.Value)
                    entity.tongcannang = reader["tongcannang"].ToString();
                if (reader["phicannang"] != DBNull.Value)
                    entity.phicannang = reader["phicannang"].ToString();
                if (reader["tongphigiaotannha"] != DBNull.Value)
                    entity.tongphigiaotannha = reader["tongphigiaotannha"].ToString();


                i++;
                list.Add(entity);

                #endregion
            }
            reader.Close();
            return list;
        }

        public class ReportGeneral
        {
            public string tongdoanhthu { get; set; }
            public string tongtientrashopCNY { get; set; }
            public int tongdon { get; set; }
            public string tongtientrashopVND { get; set; }
            public string doanhthudichvumuahnag { get; set; }
            public string phimuahang { get; set; }
            public string tienmacca { get; set; }
            public string PriceVND { get; set; }
            public string CurrentCNYVN { get; set; }
            public string FeeShipCN { get; set; }
            public string TotalPriceReal { get; set; }
            public string tongcannang { get; set; }
            public string phicannang { get; set; }
            public string tongphigiaotannha { get; set; }
        }











    }

}