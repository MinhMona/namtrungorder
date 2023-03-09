using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;

namespace NHST.Controllers
{
    public class StaffIncomeController
    {
        #region CRUD
        public static string Insert(int MainOrderID, string OrderTotalPrice, string PercentReceive, int UID, string Username, int RoleID, int Status,
            string TotalPriceReceive, bool IsHidden, DateTime OrderCreatedDate, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_StaffIncome c = new tbl_StaffIncome();
                c.MainOrderID = MainOrderID;
                c.OrderTotalPrice = OrderTotalPrice;
                c.PercentReceive = PercentReceive;
                c.UID = UID;
                c.Username = Username;
                c.RoleID = RoleID;
                c.Status = Status;
                c.TotalPriceReceive = TotalPriceReceive;
                c.OrderCreatedDate = OrderCreatedDate;
                c.IsHidden = IsHidden;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_StaffIncome.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    dbe.tbl_StaffIncome.Remove(c);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string Update(int ID, string OrderTotalPrice, string PercentReceive, int Status,
            string TotalPriceReceive, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.OrderTotalPrice = OrderTotalPrice;
                    c.PercentReceive = PercentReceive;
                    c.Status = Status;
                    c.TotalPriceReceive = TotalPriceReceive;
                    c.IsHidden = IsHidden;
                    c.ModifiedDate = ModifiedDate;
                    c.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.Status = Status;
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
        public static List<tbl_StaffIncome> GetByUIDWithHidden(int UID, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.IsHidden == IsHidden).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByStatus(int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.Status == Status).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByStatusFromDateToDate(int Status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.Status == Status && c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static List<tbl_StaffIncome> GetByUIDFromDateToDate(int UID, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDFromDateToDateIsHidden(int UID, DateTime fromdate, DateTime todate, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate >= fromdate && c.CreatedDate < todate && c.IsHidden == IsHidden)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusFromDateToDate(int UID, int Status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByFromDateToDate(DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusFromDate(int UID, int Status, DateTime fromdate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate >= fromdate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusToDate(int UID, int Status, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
       
        public static List<tbl_StaffIncome> GetByUIDFromDate(int UID, DateTime fromdate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate >= fromdate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDToDate(int UID, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByStatusToDate(int Status, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.Status == Status && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static List<tbl_StaffIncome> GetByFromDate(DateTime fromdate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedDate >= fromdate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByToDate(DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatus(int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusFromDateToDateIsHidden(int UID, int Status, DateTime fromdate, DateTime todate, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate >= fromdate && c.CreatedDate < todate && c.IsHidden == IsHidden)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedBy.Contains(s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static tbl_StaffIncome GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        public static tbl_StaffIncome GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.MainOrderID == MainOrderID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        public static tbl_StaffIncome GetByMainOrderIDUID(int MainOrderID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.MainOrderID == MainOrderID && p.UID == UID).FirstOrDefault();
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