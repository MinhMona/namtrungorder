using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;
using WebUI.Business;
using System.Data;
namespace NHST.Controllers
{
    public class KyquyController
    {
        public static tbl_KyQuy Insert(int DinhKhoanID, double Amount, string KyQuyContent, DateTime CreatedDate, string CreatedBy)
        {
            using (var db = new NHSTEntities())
            {
                tbl_KyQuy b = new tbl_KyQuy();
                b.DinhKhoanID = DinhKhoanID;
                b.Amount = Amount;
                b.KyQuyContent = KyQuyContent;
                b.CreatedDate = CreatedDate;
                b.CreatedBy = CreatedBy;
                db.tbl_KyQuy.Add(b);
                db.SaveChanges();
                return b;
            }
        }
        public static List<tbl_KyQuy> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                var lb = db.tbl_KyQuy.OrderByDescending(x => x.ID).ToList();
                return lb;
            }
        }

        public static List<ListKyquy> GetAllBySQL(string CreatedBy, string fd, string td)
        {
            var list = new List<ListKyquy>();
            var sql = @"select * from tbl_KyQuy k ";
            sql += "left outer join (select ID as idkq,MaDinhKhoan,TenDinhKhoan from tbl_DinhKhoan)d  on d.idkq = k.DinhKhoanID ";
            sql += "where k.ID > 0 ";



            if (!string.IsNullOrEmpty(CreatedBy))
            {
                sql += " AND k.CreatedBy Like N'%" + CreatedBy + "%' ";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND k.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND k.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By k.ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new ListKyquy();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["DinhKhoanID"] != DBNull.Value)
                    entity.DinhKhoanID = reader["DinhKhoanID"].ToString().ToInt(0);
                if (reader["Amount"] != DBNull.Value)
                    entity.Amount =Convert.ToDouble( reader["Amount"].ToString());
                if (reader["KyQuyContent"] != DBNull.Value)
                    entity.KyQuyContent = reader["KyQuyContent"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = reader["CreatedDate"].ToString();
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["MaDinhKhoan"] != DBNull.Value)
                    entity.MaDinhKhoan = reader["MaDinhKhoan"].ToString();
                if (reader["TenDinhKhoan"] != DBNull.Value)
                    entity.TenDinhKhoan = reader["TenDinhKhoan"].ToString();
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public class ListKyquy
        {
            public int ID { get; set; }
            public int DinhKhoanID { get; set; }
            public double Amount { get; set; }
            public string KyQuyContent { get; set; }
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public string MaDinhKhoan { get; set; }
            public string TenDinhKhoan { get; set; }
        }



    }
}