using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class WarehouseFeeController
    {
        #region CRUD
        public static string Insert(int WarehouseFromID, int WarehouseID, double WeightFrom, double WeightTo, double Price,
            int ShippingTypeToWareHouseID, int ShippingType, bool IsHidden, bool IsHelpMoving, DateTime CreatedDate,
            string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_WarehouseFee c = new tbl_WarehouseFee();
                c.WarehouseFromID = WarehouseFromID;
                c.WarehouseID = WarehouseID;
                c.WeightFrom = WeightFrom;
                c.WeightTo = WeightTo;
                c.Price = Price;
                c.ShippingTypeToWareHouseID = ShippingTypeToWareHouseID;
                c.ShippingType = ShippingType;
                c.IsHidden = IsHidden;
                c.IsHelpMoving = IsHelpMoving;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_WarehouseFee.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, int WarehouseFromID, int WarehouseID, double WeightFrom, double WeightTo, double Price,
            int ShippingTypeToWareHouseID, int ShippingType, bool IsHidden, bool IsHelpMoving, DateTime ModifiedDate,
            string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_WarehouseFee.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.WarehouseFromID = WarehouseFromID;
                    c.WarehouseID = WarehouseID;
                    c.WeightFrom = WeightFrom;
                    c.WeightTo = WeightTo;
                    c.Price = Price;
                    c.ShippingTypeToWareHouseID = ShippingTypeToWareHouseID;
                    c.ShippingType = ShippingType;
                    c.IsHidden = IsHidden;
                    c.IsHelpMoving = IsHelpMoving;
                    c.ModifiedDate = ModifiedDate;
                    c.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_WarehouseFee> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.IsHidden == IsHidden).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithWarehouseID(int WarehouseID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithWarehouseIDAndIsHidden(int WarehouseID, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID && c.IsHidden == IsHidden).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithWarehouseIDAndTypeAndIsHidden(int WarehouseID, int ShippingType, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID && c.ShippingType == ShippingType && c.IsHidden == IsHidden).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static tbl_WarehouseFee GetAllWithWarehouseIDAndTypeAndWeightAndIsHidden(int WarehouseID, int ShippingType, double weight, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                var cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID && c.ShippingType == ShippingType && c.IsHidden == IsHidden && c.WeightFrom < weight && c.WeightTo >= weight).FirstOrDefault();
                if (cs != null)
                    return cs;
                else return null;
            }
        }
        public static tbl_WarehouseFee CheckBeforeInsert(int WarehouseFromID, int WarehouseID, int ShippingTypeToWareHouseID, bool IsHelpMoving)
        {
            using (var dbe = new NHSTEntities())
            {
                var cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseFromID == WarehouseFromID &&
                                                         c.WarehouseID == WarehouseID &&
                                                         c.ShippingTypeToWareHouseID == ShippingTypeToWareHouseID &&
                                                         c.IsHelpMoving == IsHelpMoving).FirstOrDefault();
                if (cs != null)
                    return cs;
                else return null;
            }
        }
        public static List<tbl_WarehouseFee> GetByHelpMoving(bool IsHelpMoving)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.IsHelpMoving == IsHelpMoving).ToList();
                return cs;

            }
        }
        public static List<tbl_WarehouseFee> GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(int WarehouseFromID, int WarehouseID,int ShippingTypeToWareHouseID, bool IsHelpMoving)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c =>c.WarehouseFromID == WarehouseFromID && c.WarehouseID == WarehouseID && c.ShippingTypeToWareHouseID == ShippingTypeToWareHouseID && c.IsHelpMoving == IsHelpMoving).ToList();
                return cs;

            }
        }        
        public static tbl_WarehouseFee GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_WarehouseFee.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}