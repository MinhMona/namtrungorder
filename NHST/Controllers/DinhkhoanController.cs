using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using System.Data;
using WebUI.Business;
using System.Text;
namespace NHST.Controllers
{
    public class DinhkhoanController
    {
        public static List<tbl_DinhKhoan> GetAll(string s)
        {
            using (var db = new NHSTEntities())
            {
                var lb = db.tbl_DinhKhoan.Where(a => a.MaDinhKhoan.Contains(s)).OrderByDescending(x => x.ID).ToList();
                return lb;
            }
        }
        public static List<tbl_DinhKhoan> GetAllTDK(string s)
        {
            using (var db = new NHSTEntities())
            {
                var lb = db.tbl_DinhKhoan.Where(a => a.TenDinhKhoan.Contains(s)).OrderByDescending(x => x.ID).ToList();
                return lb;
            }
        }
        public static tbl_DinhKhoan GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_DinhKhoan acc = dbe.tbl_DinhKhoan.Where(a => a.ID == ID).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;

            }
            
        }
    }
}