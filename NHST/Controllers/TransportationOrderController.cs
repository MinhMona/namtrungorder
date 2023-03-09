using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class TransportationOrderController
    {
        #region CRUD
        public static string Insert(int UID, string Username, int WarehouseFromID, int WarehouseID, int ShippingTypeID, int Status, double TotalWeight,
           double Currency, double TotalPrice, string Description, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrder p = new tbl_TransportationOrder();
                p.UID = UID;
                p.Username = Username;
                p.WarehouseFromID = WarehouseFromID;
                p.WarehouseID = WarehouseID;
                p.ShippingTypeID = ShippingTypeID;
                p.Status = Status;
                p.TotalWeight = TotalWeight;
                p.Currency = Currency;
                p.TotalPrice = TotalPrice;
                p.Description = Description;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_TransportationOrder.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int UID, string Username, int WarehouseFromID, int WarehouseID, int ShippingTypeID, int Status, double TotalWeight,
            double Currency, double TotalPrice, string Description, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.UID = UID;
                    p.Username = Username;
                    p.WarehouseFromID = WarehouseFromID;
                    p.WarehouseID = WarehouseID;
                    p.ShippingTypeID = ShippingTypeID;
                    p.Status = Status;
                    p.TotalWeight = TotalWeight;
                    p.Currency = Currency;
                    p.TotalPrice = TotalPrice;
                    p.Description = Description;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }

        public static string Update_Fee(int ID, string IsFastDeliveryPrice, string IsCheckProductPrice, string IsPackedPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.IsFastDeliveryPrice = IsFastDeliveryPrice;
                    p.IsCheckProductPrice = IsCheckProductPrice;
                    p.IsPackedPrice = IsPackedPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatusAndDeposited(int ID, double Deposited, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Deposited = Deposited;
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }



        public static string UpdateTotalWeightTotalPriceStatus(int ID, int Status, double TotalWeight,
            double TotalPrice, string Description, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.TotalWeight = TotalWeight;
                    p.TotalPrice = TotalPrice;
                    p.Description = Description;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalWeightTotalPrice(int ID, double TotalWeight,
           double TotalPrice, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.TotalWeight = TotalWeight;
                    p.TotalPrice = TotalPrice;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateWarehouseFee(int ID, double WarehouseFee)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.WarehouseFee = WarehouseFee;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_TransportationOrder> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrder> pages = new List<tbl_TransportationOrder>();
                pages = dbe.tbl_TransportationOrder.Where(p => p.Username.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_TransportationOrder> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrder> pages = new List<tbl_TransportationOrder>();
                pages = dbe.tbl_TransportationOrder.Where(p => p.UID == UID).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static string UpdateIsPacked(int ID, bool IsPacked)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_TransportationOrder.Where(o => o.ID == ID).FirstOrDefault();
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

        public static string UpdateIsFastDelivery(int ID, bool IsFastDelivery)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_TransportationOrder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsFastDelivery = IsFastDelivery;
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
                var or = dbe.tbl_TransportationOrder.Where(o => o.ID == ID).FirstOrDefault();
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

        public static List<tbl_TransportationOrder> GetByUIDAndPackageCode(int UID, string PackageCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrder> returnPages = new List<tbl_TransportationOrder>();
                List<tbl_TransportationOrder> pages = new List<tbl_TransportationOrder>();
                pages = dbe.tbl_TransportationOrder.Where(p => p.UID == UID).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    foreach (var p in pages)
                    {
                        var packages = SmallPackageController.GetByTransportationOrderID(p.ID);
                        if (packages.Count > 0)
                        {
                            foreach (var s in packages)
                            {
                                if(s.OrderTransactionCode == PackageCode)
                                {
                                    returnPages.Add(p);
                                    break;
                                }
                            }
                        }
                    }
                }
                return returnPages;
            }
        }
        public static tbl_TransportationOrder GetByIDAndUID(int ID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var pages = dbe.tbl_TransportationOrder.Where(p => p.UID == UID && p.ID == ID).FirstOrDefault();
                return pages;
            }
        }
        public static tbl_TransportationOrder GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrder page = dbe.tbl_TransportationOrder.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}