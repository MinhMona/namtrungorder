using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace NHST.Controllers
{
    public class WithdrawController
    {        
        #region CRUD
        public static void Insert1(int UID, string Username, double Amount, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                try
                {
                    tbl_Withdraw a = new tbl_Withdraw();
                    a.UID = UID;
                    a.Username = Username;
                    a.Amount = Amount;
                    a.Status = Status;
                    a.CreatedDate = CreatedDate;
                    a.CreatedBy = CreatedBy;
                    dbe.tbl_Withdraw.Add(a);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    int kq = dbe.SaveChanges();
                    string k = a.ID.ToString();
                }
                catch (DbEntityValidationException dbEx)
                {
                    string html = "";
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
        public static string Insert(int UID, string Username, double Amount, int Status, string Note, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Withdraw a = new tbl_Withdraw();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.Note = Note;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_Withdraw.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertNote(int UID, string Username, double Amount, int Status,string Note, DateTime CreatedDate, string CreatedBy, string BankNumber, string BankAddress, string Beneficiary)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Withdraw a = new tbl_Withdraw();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.Note = Note;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                a.Beneficiary = Beneficiary;
                a.BankAddress = BankAddress;
                a.BankNumber = BankNumber;
                dbe.tbl_Withdraw.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string InsertRechargeCYN(int UID, string Username, double Amount, 
            string Note, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Withdraw a = new tbl_Withdraw();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Note = Note;
                a.Status = Status;
                a.Type = 3;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_Withdraw.Add(a);                
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    dbe.tbl_Withdraw.Remove(a);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Withdraw> GetAllByType(string s, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.Username.Contains(s) && w.Type == Type).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Withdraw> GetBuyUIDAndType(int UID, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.UID == UID && w.Type == Type).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Withdraw GetByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID && f.UID == UID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Withdraw> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.Username.Contains(s)).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<View_SaleWithdraw_List> GetAllBySaleID_View(string s, int saleID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_SaleWithdraw_List> aus = new List<View_SaleWithdraw_List>();
                aus = dbe.View_SaleWithdraw_List.Where(a => a.Username.Contains(s) && a.RoleID != 0 && a.SaleID == saleID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }

        public static tbl_Withdraw GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Withdraw> GetBuyUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.UID == UID).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}