using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using WebUI.Business;
using System.Data;
using MB.Extensions;

namespace NHST.Controllers
{
    public class AdminSendUserWalletController
    {

        #region CRUD
        public static string Insert(int UID, string Username, double Amount, int Status, string Content, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = new tbl_AdminSendUserWallet();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.TradeContent = Content;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_AdminSendUserWallet.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string UpdateStatus(int ID, int Status, string Content, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.TradeContent = Content;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_AdminSendUserWallet GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.Username.Contains(s)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }

        public static List<View_SaleAdminSendUserWallet_List> GetAllBySaleID_View(string s, int saleID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_SaleAdminSendUserWallet_List> aus = new List<View_SaleAdminSendUserWallet_List>();
                aus = dbe.View_SaleAdminSendUserWallet_List.Where(a => a.Username.Contains(s) && a.RoleID != 0 && a.SaleID == saleID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }

        public static List<tbl_AdminSendUserWallet> GetFromDateToDate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> lo = new List<tbl_AdminSendUserWallet>();

                var alllist = dbe.tbl_AdminSendUserWallet.OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();

                if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from && t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else
                {
                    lo = alllist;
                }
                if (lo.Count > 0)
                    return lo.Where(l => l.Status == 2).ToList();
                else return lo;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetByCreatedBy(string s, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.Username.Contains(s) && a.CreatedBy == CreatedBy).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }


        public static List<tbl_AdminSendUserWallet> GetAllBySQL(string Username, string CreatedBy, string fd, string td)
        {
            var list = new List<tbl_AdminSendUserWallet>();
            var sql = @"select * from tbl_AdminSendUserWallet where ID > 0";


            if (!string.IsNullOrEmpty(Username))
            {
                sql += " AND Username Like N'%" + Username + "%' ";
            }

            if (!string.IsNullOrEmpty(CreatedBy))
            {
                sql += " AND CreatedBy Like N'%" + CreatedBy + "%' ";
            }

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
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_AdminSendUserWallet();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["TradeContent"] != DBNull.Value)
                    entity.TradeContent = reader["TradeContent"].ToString();
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["BankID"] != DBNull.Value)
                    entity.BankID = reader["BankID"].ToString().ToInt();

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }


        public static double GetTotalPrice(string Username, string CreatedBy, string fd, string td)
        {
            var list = new List<tbl_AdminSendUserWallet>();
            var sql = @"select Sum(Amount) as total from tbl_AdminSendUserWallet where ID > 0 And Status = 2 ";


            if (!string.IsNullOrEmpty(Username))
            {
                sql += " AND Username Like N'%" + Username + "%' ";
            }

            if (!string.IsNullOrEmpty(CreatedBy))
            {
                sql += " AND CreatedBy Like N'%" + CreatedBy + "%' ";
            }

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
            double total = 0;
            while (reader.Read())
            {
                total = Convert.ToDouble(reader["total"].ToString());
            }
            reader.Close();
            return total;
        }


        #endregion
        public static string InsertNew(int UID, string Username, double Amount, int Status, string Content, DateTime CreatedDate, string CreatedBy, int SmsForwardID)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = new tbl_AdminSendUserWallet();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.SmsForwardID = SmsForwardID;
                a.TradeContent = Content;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_AdminSendUserWallet.Add(a);

                var s = dbe.tbl_SmsForward.Where(x => x.ID == SmsForwardID).FirstOrDefault();
                if (s != null)
                {
                    s.Status = 2;//1: Chưa xử lý || 2: Đã xử lý
                }

                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
    }
}