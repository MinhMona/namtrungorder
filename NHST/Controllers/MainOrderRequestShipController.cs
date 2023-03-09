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

namespace NHST.Controllers
{
    public class MainOrderRequestShipController
    {
        #region CRUD
        public static string Insert(int MainOrderID, int UID, string FullName, string Email, string Phone, string Note,
            string Address, int RequestStatus, int MainOrderStatus, int ShippingMethod, int PaymentMethod,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_MainOrderRequestShip o = new tbl_MainOrderRequestShip();
                o.MainOrderID = MainOrderID;
                o.UID = UID;
                o.FullName = FullName;
                o.Email = Email;
                o.Phone = Phone;
                o.Note = Note;
                o.Address = Address;
                o.RequestStatus = RequestStatus;
                o.MainOrderStatus = MainOrderStatus;
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_MainOrderRequestShip.Add(o);
                dbe.SaveChanges();
                string k = o.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string FullName, string Email, string Phone, 
            string Note, string Address, int RequestStatus, int ShippingMethod, int PaymentMethod,
            DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOrderRequestShip.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FullName = FullName;
                    or.Email = Email;
                    or.Phone = Phone;
                    or.Note = Note;
                    or.Address = Address;
                    or.RequestStatus = RequestStatus;
                    or.ShippingMethod = ShippingMethod;
                    or.PaymentMethod = PaymentMethod;
                    or.ModifiedDate = ModifiedDate;
                    or.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateMainOrderStatus(int ID, int MainOrderStatus)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOrderRequestShip.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.MainOrderStatus = MainOrderStatus;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateMainOrderStatusByMainOrderID(int MainOrderID, int MainOrderStatus)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOrderRequestShip.Where(o => o.MainOrderID == MainOrderID).FirstOrDefault();
                if (or != null)
                {
                    or.MainOrderStatus = MainOrderStatus;
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
        public static List<tbl_MainOrderRequestShip> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOrderRequestShip> cs = new List<tbl_MainOrderRequestShip>();
                cs = dbe.tbl_MainOrderRequestShip.Where(c => c.FullName.Contains(s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_MainOrderRequestShip> GetAllAndStatus(string s, int RequestStatus, int MainOrderStatus)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOrderRequestShip> cs = new List<tbl_MainOrderRequestShip>();
                if(RequestStatus != -1)
                {
                    if(MainOrderStatus!=-1)
                    {
                        cs = dbe.tbl_MainOrderRequestShip.Where(c => c.FullName.Contains(s) && c.RequestStatus == RequestStatus && c.MainOrderStatus == MainOrderStatus).OrderByDescending(c => c.ID).ToList();
                    }
                    else
                    {
                        cs = dbe.tbl_MainOrderRequestShip.Where(c => c.FullName.Contains(s) && c.RequestStatus == RequestStatus).OrderByDescending(c => c.ID).ToList();
                    }
                }
                else
                {
                    if (MainOrderStatus != -1)
                    {
                        cs = dbe.tbl_MainOrderRequestShip.Where(c => c.FullName.Contains(s) && c.MainOrderStatus == MainOrderStatus).OrderByDescending(c => c.ID).ToList();
                    }
                    else
                    {
                        cs = dbe.tbl_MainOrderRequestShip.Where(c => c.FullName.Contains(s)).OrderByDescending(c => c.ID).ToList();
                    }
                }
                
                return cs;
            }
        }
        public static tbl_MainOrderRequestShip GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_MainOrderRequestShip.Where(p => p.MainOrderID == MainOrderID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        public static tbl_MainOrderRequestShip GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_MainOrderRequestShip.Where(p => p.ID == ID).FirstOrDefault();
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