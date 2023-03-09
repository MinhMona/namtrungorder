using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;


namespace NHST.Controllers
{
    public class PayhelpController
    {
        #region CRUD
        public static string Insert(int UID, string Username, string Note, string TotalPrice, string TotalPriceVND, string Currency,
            string CurrencyGiagoc, string TotalPriceVNDGiagoc, int Status, string Phone, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PayHelp o = new tbl_PayHelp();
                o.UID = UID;
                o.Username = Username;
                o.Note = Note;
                o.TotalPrice = TotalPrice;
                o.TotalPriceVND = TotalPriceVND;
                o.Currency = Currency;
                o.CurrencyGiagoc = CurrencyGiagoc;
                o.TotalPriceVNDGiagoc = TotalPriceVNDGiagoc;
                o.Status = Status;
                o.Phone = Phone;
                o.IsNotComplete = false;
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_PayHelp.Add(o);
                dbe.SaveChanges();
                int kq = o.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string Note, string TotalPrice, string TotalPriceVND,
            int Status, string Phone,bool IsNotComplete, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Note = Note;
                    o.TotalPrice = TotalPrice;
                    o.TotalPriceVND = TotalPriceVND;                    
                    o.Status = Status;
                    o.Phone = Phone;
                    o.IsNotComplete = IsNotComplete;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Status = Status;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_PayHelp GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static tbl_PayHelp GetByIDAndUID(int ID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID && od.UID == UID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static List<tbl_PayHelp> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> ps = new List<tbl_PayHelp>();
                ps = dbe.tbl_PayHelp.OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_PayHelp> GetAllUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> ps = new List<tbl_PayHelp>();
                ps = dbe.tbl_PayHelp.Where(p => p.UID == UID).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_PayHelp> GetAllByFromStatusFromdateToDate(int status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> os = new List<tbl_PayHelp>();
                os = dbe.tbl_PayHelp.Where(od => od.Status >= status && od.CreatedDate >= fromdate && od.CreatedDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        public static List<tbl_PayHelp> GetAllByStatusFromdateToDate(int status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> os = new List<tbl_PayHelp>();
                os = dbe.tbl_PayHelp.Where(od => od.Status == status && od.CreatedDate >= fromdate && od.CreatedDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        #endregion
    }
}