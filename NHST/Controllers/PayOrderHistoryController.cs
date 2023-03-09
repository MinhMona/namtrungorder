using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;

namespace NHST.Controllers
{
    public class PayOrderHistoryController
    {        
        #region CRUD
        public static string Insert(int MainOrderID, int UID, int Status, double Amount,int Type, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PayOrderHistory a = new tbl_PayOrderHistory();
                a.MainOrderID = MainOrderID;
                a.UID = UID;
                a.Status = Status;
                a.Amount = Amount;
                a.Type = Type;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_PayOrderHistory.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select
        public static List<tbl_PayOrderHistory> GetAllByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayOrderHistory> o = new List<tbl_PayOrderHistory>();
                o = dbe.tbl_PayOrderHistory.Where(h => h.MainOrderID == MainOrderID).OrderByDescending(h => h.CreatedDate).ToList();
                return o;
            }
        }
        public static int GetCountByMainOrderID(int MainOrderID)
        {
            int Count = 0;
            var sql = @"SELECT COUNT(*) as Total from tbl_PayOrderHistory as mo";
            sql += "        Where MainOrderID = " + MainOrderID + "";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
        public static List<tbl_PayOrderHistory> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayOrderHistory> o = new List<tbl_PayOrderHistory>();
                o = dbe.tbl_PayOrderHistory.OrderByDescending(h => h.CreatedDate).ToList();
                return o;
            }
        }
        public static List<tbl_PayOrderHistory> GetFromDateToDate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayOrderHistory> lo = new List<tbl_PayOrderHistory>();

                var alllist = dbe.tbl_PayOrderHistory.OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();

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
                    return lo;
                else return lo;
            }
        }
        #endregion
    }
}