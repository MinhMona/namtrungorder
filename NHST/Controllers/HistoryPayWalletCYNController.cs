using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;


namespace NHST.Controllers
{
    public class HistoryPayWalletCYNController
    {
        #region CRUD
        public static string Insert(int UID, string UserName,  double Amount, double MoneyLeft, int Type,
            int TradeType, string Note, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWalletCYN a = new tbl_HistoryPayWalletCYN();
                a.UID = UID;
                a.UserName = UserName;
                a.Amount = Amount;
                a.MoneyLeft = MoneyLeft;
                a.Type = Type;
                a.TradeType = TradeType;
                a.Note = Note;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWalletCYN.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }        
        #endregion
        #region Select
        public static List<tbl_HistoryPayWalletCYN> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWalletCYN> aus = new List<tbl_HistoryPayWalletCYN>();
                aus = dbe.tbl_HistoryPayWalletCYN.Where(a => a.UserName.Contains(s)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWalletCYN> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWalletCYN> aus = new List<tbl_HistoryPayWalletCYN>();
                aus = dbe.tbl_HistoryPayWalletCYN.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWalletCYN> GetByUIDTradeTypeDateSend(int UID, int TradeType, DateTime DateSend)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWalletCYN> aus = new List<tbl_HistoryPayWalletCYN>();
                aus = dbe.tbl_HistoryPayWalletCYN.Where(a => a.UID == UID && a.TradeType == TradeType && a.DateSend == DateSend).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        #endregion
    }
}