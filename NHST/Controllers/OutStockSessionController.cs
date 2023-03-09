using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using WebUI.Business;
using System.Data;

namespace NHST.Controllers
{
    public class OutStockSessionController
    {
        #region CRUD
        public static string Insert(int UID, string Username, string FullName, string Phone, int Status,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                tbl_OutStockSession user = new tbl_OutStockSession();
                user.UID = UID;
                user.Username = Username;
                user.FullName = FullName;
                user.Phone = Phone;
                user.Status = Status;
                user.CreatedDate = CreatedDate;
                user.CreatedBy = CreatedBy;
                dbe.tbl_OutStockSession.Add(user);
                int kq = dbe.SaveChanges();
                string k = user.ID.ToString();
                return k;
            }

        }
        public static string update(int ID, string FullName, string Phone, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.FullName = FullName;
                    a.Phone = Phone;
                    a.Status = Status;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateInfo(int ID, string FullName, string Phone)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.FullName = FullName;
                    a.Phone = Phone;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateTotalPay(int ID, double TotalPay)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.TotalPay = TotalPay;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_OutStockSession> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSession> las = new List<tbl_OutStockSession>();
                las = dbe.tbl_OutStockSession.Where(a => a.Username.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }

        public static List<tbl_OutStockSession> GetAllNew(string s, int Status, bool IsPay)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSession> las = new List<tbl_OutStockSession>();
                if (Status > -1 && IsPay == true)
                {
                    las = dbe.tbl_OutStockSession.Where(a => a.Username.Contains(s) && a.Status == Status && a.IsPay == IsPay).OrderByDescending(a => a.CreatedDate).ToList();
                }
                else if (Status > -1 && IsPay == false)
                {
                    las = dbe.tbl_OutStockSession.Where(a => a.Username.Contains(s) && a.Status == Status).OrderByDescending(a => a.CreatedDate).ToList();
                }
                else if (IsPay == true)
                {
                    las = dbe.tbl_OutStockSession.Where(a => a.Username.Contains(s) && a.IsPay == IsPay).OrderByDescending(a => a.CreatedDate).ToList();
                }
                else
                {
                    las = dbe.tbl_OutStockSession.Where(a => a.Username.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                }
                return las;
            }
        }


        public static List<tbl_OutStockSession> GetAllBySQL(string s, int Status, bool IsPay, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var list = new List<tbl_OutStockSession>();
                var sql = @"select * from tbl_OutStockSession ";
                sql += " where Username LIKE N'%" + s + "%' ";
                if (Status > -1)
                    sql += " AND Status = " + Status + " ";
                if (IsPay)
                    sql += " AND IsPay = 1 ";
                if (!string.IsNullOrEmpty(CreatedBy))
                    sql += " AND CreatedBy LIKE N'%" + CreatedBy + "%' ";
                sql += " Order By ID desc";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                int i = 1;
                while (reader.Read())
                {
                    var entity = new tbl_OutStockSession();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = reader["ID"].ToString().ToInt(0);
                    if (reader["UID"] != DBNull.Value)
                        entity.UID = reader["UID"].ToString().ToInt();
                    if (reader["Username"] != DBNull.Value)
                        entity.Username = reader["Username"].ToString();
                    if (reader["FullName"] != DBNull.Value)
                        entity.FullName = reader["FullName"].ToString();
                    if (reader["Phone"] != DBNull.Value)
                        entity.Phone = reader["Phone"].ToString();
                    if (reader["Status"] != DBNull.Value)
                        entity.Status = reader["Status"].ToString().ToInt();
                    if (reader["IsPay"] != DBNull.Value)
                        entity.IsPay = Convert.ToBoolean(reader["IsPay"]);
                    if (reader["TotalPay"] != DBNull.Value)
                        entity.TotalPay = Convert.ToDouble(reader["TotalPay"].ToString());
                    if (reader["Note"] != DBNull.Value)
                        entity.Note = reader["Note"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    if (reader["CreatedBy"] != DBNull.Value)
                        entity.CreatedBy = reader["CreatedBy"].ToString();
                    if (reader["ModifiedDate"] != DBNull.Value)
                        entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    if (reader["ModifiedBy"] != DBNull.Value)
                        entity.ModifiedBy = reader["ModifiedBy"].ToString();
                    i++;
                    list.Add(entity);
                }
                reader.Close();
                return list;
            }
        }












        public static List<tbl_OutStockSession> GetAllByUsername(string username)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSession> las = new List<tbl_OutStockSession>();
                las = dbe.tbl_OutStockSession.Where(a => a.Username == username).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static tbl_OutStockSession GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OutStockSession acc = dbe.tbl_OutStockSession.Where(a => a.ID == ID).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }

        public static string UpdatePay(int ID, bool IsPay)
        {
            using (var db = new NHSTEntities())
            {
                var l = db.tbl_OutStockSession.Where(x => x.ID == ID).FirstOrDefault();
                if (l != null)
                {
                    l.IsPay = IsPay;
                    db.SaveChanges();
                    return l.ID.ToString();
                }
                else return null;
            }
        }

        public static List<OutStockSession> GetFromDateToDateNew(string fd, string td, string Username)
        {
            var list = new List<OutStockSession>();

            var sql = @"select * from tbl_OutStockSession os ";
            sql += "left outer join (select UID as uid,Address from tbl_AccountInfo )ai on os.UID = ai.uid ";
            sql += "where os.ID > 0";



            if (!string.IsNullOrEmpty(Username))
                sql += " AND os.Username like N'%" + Username + "%'";

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
            sql += " Order By os.ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new OutStockSession();
                #region code mới

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString();
                if (reader["TotalPay"] != DBNull.Value)
                    entity.TotalPay = reader["TotalPay"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Address"] != DBNull.Value)
                    entity.Address = reader["Address"].ToString();


                i++;
                list.Add(entity);

                #endregion
            }
            reader.Close();
            return list;
        }
        public class OutStockSession
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public string Phone { get; set; }
            public string Status { get; set; }
            public string TotalPay { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Address { get; set; }

        }

        public static List<tbl_OutStockSession> GetAllOutStock(string fd, string td, string Username, int page, int maxrows)
        {
            var list = new List<tbl_OutStockSession>();

            var sql = @"select * from tbl_OutStockSession where ID > 0 and Status= 2 ";
            //sql += "left outer join (select UID as uid,Address from tbl_AccountInfo )ai on os.UID = ai.uid ";
            //sql += "where ID > 0";



            if (!string.IsNullOrEmpty(Username))
                sql += " AND Username like N'%" + Username + "%'";

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

            sql += " ORDER BY ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_OutStockSession();
                #region code mới

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["TotalPay"] != DBNull.Value)
                    entity.TotalPay = Convert.ToDouble(reader["TotalPay"].ToString());
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                //if (reader["Address"] != DBNull.Value)
                //    entity.Address = reader["Address"].ToString();


                i++;
                list.Add(entity);

                #endregion
            }
            reader.Close();
            return list;
        }


        public static int GetTotal(string fd, string td, string Username)
        {
            int Count = 0;
            var sql = @"select  COUNT(*) as Total from tbl_OutStockSession where ID > 0 and Status= 2 ";
            //sql += "left outer join (select UID as uid,Address from tbl_AccountInfo )ai on os.UID = ai.uid ";
            //sql += "where ID > 0";



            if (!string.IsNullOrEmpty(Username))
                sql += " AND Username like N'%" + Username + "%'";

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



        #endregion
    }
}