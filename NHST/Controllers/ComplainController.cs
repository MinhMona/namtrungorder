using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using System.Data;
using WebUI.Business;

namespace NHST.Controllers
{
    public class ComplainController
    {
        #region CRUD
        public static string Insert(int UID, int OrderID, string Amount, string IMG, string ComplainText, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Complain c = new tbl_Complain();
                c.UID = UID;
                c.OrderID = OrderID;
                c.Amount = Amount;
                c.IMG = IMG;
                c.ComplainText = ComplainText;
                c.Status = Status;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_Complain.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string InsertNew(int UID, int OrderID, string Amount, string IMG, string ComplainText, string UserNote,
            int Status, string ComplainType, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Complain c = new tbl_Complain();
                c.UID = UID;
                c.OrderID = OrderID;
                c.Amount = Amount;
                c.IMG = IMG;
                c.ComplainText = ComplainText;
                c.UserNote = UserNote;
                c.Status = Status;
                c.ComplainType = ComplainType;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_Complain.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, string Amount, string ComplainText, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.Amount = Amount;
                    c.ComplainText = ComplainText;
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
        public static string UpdateNew(int ID, string Amount, string ComplainText, string EmployeeNote, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.Amount = Amount;
                    c.ComplainText = ComplainText;
                    c.EmployeeNote = EmployeeNote;
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


        public static string UpdateEmployeeNote(int ID, string EmployeeNote)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.EmployeeNote = EmployeeNote;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Complain> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.UID == UID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_Complain> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.CreatedBy.Contains(s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_Complain> GetAllOrderID(int s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.OrderID == (s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_Complain> GetAllByOrderShopCodeAndUID(int UID, int OrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.UID == UID && c.OrderID == OrderID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static tbl_Complain GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Complain> GetAllWithFromStatus(string s, int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.CreatedBy.Contains(s) && c.Status >= status).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static List<tbl_Complain> GetAllWithStatus_SqlHelper(string search, int type, int status, int roleID, int UID)
        {
            var sql = @"select c.* from tbl_Complain as c ";
            sql += " left outer join (select ID,dathangID,SalerID from tbl_MainOder) as mo ON mo.ID = c.OrderID  ";
            sql += " left outer join(select ID, Username from tbl_Account) as dathang ON dathang.ID = mo.dathangID ";
            sql += " left outer join(select ID, Username from tbl_Account) as saler ON saler.ID = mo.SalerID ";
            sql += " where c.ID > 0 ";
            if (!string.IsNullOrEmpty(search))
            {
                if (type == 1) //username
                {
                    sql += " and c.CreatedBy Like N'%" + search + "%'";
                }
                else if (type == 2)//OrderID
                {
                    sql += " and mo.MainOrderCode Like N'%" + search + "%'";
                }
                else if (type == 3) //Username đặt hàng
                {
                    sql += " and dathang.Username Like N'%" + search + "%' ";
                }
                else if (type == 4)//Username saler
                {
                    sql += " and saler.Username Like N'%" + search + "%' ";
                }
                else if (type == 5)//OrderID
                {
                    sql += " and mo.ID Like N'%" + search + "%'";
                }
            }

            if (status >= 0)
                sql += " AND c.Status = " + status;

            if (roleID == 3)
            {
                sql += " AND mo.dathangID=" + UID;
            }

            if (roleID == 6)
            {
                sql += " AND mo.SalerID=" + UID;
            }

            sql += " Order By c.ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_Complain> a = new List<tbl_Complain>();
            while (reader.Read())
            {
                var entity = new tbl_Complain();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["OrderID"] != DBNull.Value)
                    entity.OrderID = reader["OrderID"].ToString().ToInt(0);

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = reader["Amount"].ToString();

                if (reader["ComplainText"] != DBNull.Value)
                    entity.ComplainText = reader["ComplainText"].ToString();

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["UserNote"] != DBNull.Value)
                    entity.UserNote = reader["UserNote"].ToString();

                if (reader["EmployeeNote"] != DBNull.Value)
                    entity.EmployeeNote = reader["EmployeeNote"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public static List<tbl_Complain> GetAll_SqlHelper(string search, int type)
        {
            var sql = @"select c.* from tbl_Complain as c ";
            sql += " left outer join (select * from tbl_MainOder) as mo ON mo.ID = c.OrderID";
            sql += " left outer join(select * from tbl_Account) as dathang ON dathang.ID = mo.dathangID ";
            sql += " left outer join(select * from tbl_Account) as saler ON saler.ID = mo.SalerID ";
            sql += " where c.ID > 0 ";
            if (!string.IsNullOrEmpty(search))
            {
                if (type == 1) //username
                {
                    sql += " and c.CreatedBy Like N'%" + search + "%'";
                }
                else if (type == 2)//OrderID
                {
                    sql += " and mo.MainOrderCode Like N'%" + search + "%'";
                }
                else if (type == 3) //Username đặt hàng
                {
                    sql += " and dathang.Username Like N'%" + search + "%' ";
                }
                else if (type == 4)//Username saler
                {
                    sql += " and saler.Username Like N'%" + search + "%' ";
                }
            }

            sql += " Order By c.ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_Complain> a = new List<tbl_Complain>();
            while (reader.Read())
            {
                var entity = new tbl_Complain();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["OrderID"] != DBNull.Value)
                    entity.OrderID = reader["OrderID"].ToString().ToInt(0);

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = reader["Amount"].ToString();

                if (reader["ComplainText"] != DBNull.Value)
                    entity.ComplainText = reader["ComplainText"].ToString();

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["UserNote"] != DBNull.Value)
                    entity.UserNote = reader["UserNote"].ToString();

                if (reader["EmployeeNote"] != DBNull.Value)
                    entity.EmployeeNote = reader["EmployeeNote"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        #endregion
        public static List<tbl_Complain> GetListUserBySQL_searchid(int UID,string OrderID, int pageIndex, int pageSize)
        {
            var sql = @"select* ";
            sql += "from tbl_Complain ";
            sql += "where  UID = " + UID + "  ";
            if (!string.IsNullOrEmpty(OrderID))
                sql += "and OrderID Like N'%" + OrderID + "%' ";
            sql += "order by ID desc, CreatedDate desc OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";
            List<tbl_Complain> list = new List<tbl_Complain>();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new tbl_Complain();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["OrderID"] != DBNull.Value)
                    entity.OrderID = reader["OrderID"].ToString().ToInt(0);

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = reader["Amount"].ToString();

                if (reader["ComplainText"] != DBNull.Value)
                    entity.ComplainText = reader["ComplainText"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
              

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static int GetTotalUser_searchid(int UID, string OrderID)
        {
            var sql = @"select Total=COUNT(*) ";
            sql += "from tbl_Complain ";
            sql += "where  UID = " + UID + "  ";
            if (!string.IsNullOrEmpty(OrderID))
                sql += "and OrderID Like N'%" + OrderID + "%' ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return a;
        }
    }
}