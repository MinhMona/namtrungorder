using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class BigPackageController
    {

        #region CRUD
        public static string Insert(string PackageCode, double Weight, double Volume, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = new tbl_BigPackage();
                a.PackageCode = PackageCode;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_BigPackage.Add(a);
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string PackageCode, double Weight, double Volume, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.PackageCode = PackageCode;
                    a.Weight = Weight;
                    a.Volume = Volume;
                    a.Status = Status;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateWeight(int ID, double Weight)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {

                    a.Weight = Weight;
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
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_BigPackage> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage> ps = new List<tbl_BigPackage>();
                ps = dbe.tbl_BigPackage.Where(p => p.PackageCode.Contains(s)).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_BigPackage> GetAllWithStatus(int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage> ps = new List<tbl_BigPackage>();
                ps = dbe.tbl_BigPackage.Where(p => p.Status == Status).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_BigPackage> GetAllNotHuy()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage> ps = new List<tbl_BigPackage>();
                ps = dbe.tbl_BigPackage.Where(p => p.Status < 3).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static tbl_BigPackage GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_BigPackage GetByPackageCode(string PackageCode)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.PackageCode == PackageCode).FirstOrDefault();
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