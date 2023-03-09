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
    public class AccountController
    {
        #region CRUD
        public static string Insert(string Username, string Email, string Password, int RoleID, int LevelID, int VIPLevel, int Status, int SaleID, int DathangID, DateTime CreatedDate, string CreatedBy, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {

                tbl_Account user = new tbl_Account();
                user.Username = Username;
                user.Email = Email;
                user.Password = PJUtils.Encrypt("userpass", Password);
                user.RoleID = RoleID;
                user.LevelID = LevelID;
                user.VIPLevel = VIPLevel;
                user.Status = Status;
                user.Wallet = 0;
                user.SaleID = SaleID;
                user.DathangID = DathangID;
                user.CreatedDate = CreatedDate;
                user.CreatedBy = CreatedBy;
                user.ModifiedBy = ModifiedBy;
                user.ModifiedDate = ModifiedDate;
                dbe.tbl_Account.Add(user);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = user.ID.ToString();
                return k;
            }

        }
        public static string updateVipLevel(int ID, int VIPLevel, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.VIPLevel = VIPLevel;
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
        public static string updateLevelID(int ID, int LevelID, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.LevelID = LevelID;
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
        public static string updateWallet(int ID, double Wallet, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Wallet = Wallet;
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
        public static string updatewarehouseFromwarehouseTo(int ID, int WarehouseFrom, int WarehouseTo)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.WarehouseFrom = WarehouseFrom;
                    a.WarehouseTo = WarehouseTo;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updatestatus(int ID, int status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = status;
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
        public static string UpdateRole(int ID, int roleid, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.RoleID = roleid;
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
        public static string UpdateSiteType(int ID, int sitetype, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.SiteType = sitetype;
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
        public static string UpdateDepartment(int ID, int Department, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Department = Department;
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

        public static string UpdateMaxOrderPrice(int ID, double MaxOrderPrice, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.MaxOrderPrice = MaxOrderPrice;
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

        public static string UpdateNumberOrder(int ID, int NumberOrder, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.NumberOrder = NumberOrder;
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
        public static string UpdateNumberTake(int ID, int NumberTake, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.NumberTake = NumberTake;
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
        public static string UpdateSaleID(int ID, int saleID, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.SaleID = saleID;
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
        public static string UpdateDathangID(int ID, int DathangID, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {


                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.DathangID = DathangID;
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
        public static string UpdatePassword(int ID, string Password)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Password = PJUtils.Encrypt("userpass", Password);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return null;
            }
        }

        public static string UpdateWarehouseTo(int UID, int WarehouseTo)
        {
            using (var dbe = new NHSTEntities())
            {
                var ui = dbe.tbl_Account.Where(a => a.ID == UID).FirstOrDefault();
                if (ui != null)
                {
                    ui.WarehouseTo = WarehouseTo;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string updateWalletCYN(int ID, double WalletCYN)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.WalletCYN = WalletCYN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateFee(int ID, string Currency, string FeeBuyPro, string FeeTQVNPerWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Currency = Currency;
                    a.FeeBuyPro = FeeBuyPro;
                    a.FeeTQVNPerWeight = FeeTQVNPerWeight;
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
        public static List<tbl_Account> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<View_UserList> GetAll_View(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserList> las = new List<View_UserList>();
                las = dbe.View_UserList.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_Account> GetAllBySaleID(int SaleID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.SaleID == SaleID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<View_StaffCustomer> GetAllBySaleID_View(string s, int saleID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_StaffCustomer> las = new List<View_StaffCustomer>();
                las = dbe.View_StaffCustomer.Where(a => a.Username.Contains(s) && a.RoleID != 0 && a.SaleID == saleID).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<View_UserListWithWallet> GetAllWithWallet_View(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserListWithWallet> las = new List<View_UserListWithWallet>();
                las = dbe.View_UserListWithWallet.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }

        public static List<View_UserListExcel> GetAll_ViewUserListExcel(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserListExcel> las = new List<View_UserListExcel>();
                las = dbe.View_UserListExcel.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.ID).ToList();
                return las;
            }
        }
        public static List<tbl_Account> GetUserAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID == 1).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return null;
            }
        }
        public static List<tbl_Account> GetAllOrderDesc(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.ID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return null;
            }
        }
        public static List<tbl_Account> GetAllNotSearch()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<tbl_Account> GetAllByRoleID(int RoleID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.RoleID == RoleID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<tbl_Account> GetAllByRoleIDAndRoleID(int RoleID1, int RoleID2)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.RoleID == RoleID1 || a.RoleID == RoleID2).OrderBy(a => a.RoleID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static tbl_Account GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.ID == ID).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }

        public static tbl_Account GetByUsername(string Username)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Username == Username).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account GetByEmail(string Email)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Email == Email).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account Login(string Username, string Password)
        {
            using (var dbe = new NHSTEntities())
            {
                Password = PJUtils.Encrypt("userpass", Password);
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Username == Username && a.Password == Password).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account LoginEmail(string Email, string Password)
        {
            using (var dbe = new NHSTEntities())
            {
                Password = PJUtils.Encrypt("userpass", Password);
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Email == Email && a.Password == Password).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account GetByPhone(string Phone)
        {
            using (var dbe = new NHSTEntities())
            {
                var ai = dbe.tbl_AccountInfo.Where(a => a.Phone == Phone).FirstOrDefault();
                if (ai != null)
                {
                    tbl_Account acc = dbe.tbl_Account.Where(a => a.ID == ai.UID).FirstOrDefault();
                    if (acc != null)
                        return acc;
                    else
                        return null;
                }
                else
                    return null;


            }
        }
        #endregion
        public static tbl_Account UpdateScanWareHouse(int ID, int WareHouseTQ, int WareHouseVN)
        {
            using (var db = new NHSTEntities())
            {
                tbl_Account acc = db.tbl_Account.Where(n => n.ID == ID).FirstOrDefault();
                if (acc != null)
                {
                    acc.WareHouseTQ = WareHouseTQ;
                    acc.WareHouseVN = WareHouseVN;
                    db.SaveChanges();
                    return acc;
                }
                else
                    return null;
            }
        }
        public static tbl_Account InsertNew(string Username, string Email, string Password, int RoleID, int LevelID, int VIPLevel, int Status, int SaleID, int DathangID, DateTime CreatedDate, string CreatedBy, DateTime ModifiedDate, string ModifiedBy, string Token)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {

                tbl_Account user = new tbl_Account();
                user.Username = Username;
                user.Email = Email;
                user.Password = PJUtils.Encrypt("userpass", Password);
                user.Token = Token;
                user.RoleID = RoleID;
                user.LevelID = LevelID;
                user.VIPLevel = VIPLevel;
                user.Status = Status;
                user.Wallet = 0;
                user.SaleID = SaleID;
                user.DathangID = DathangID;
                user.Currency = "0";
                user.FeeBuyPro = "";
                user.FeeTQVNPerWeight = "";
                user.Deposit = 0;
                user.CreatedDate = CreatedDate;
                user.CreatedBy = CreatedBy;
                user.ModifiedBy = ModifiedBy;
                user.ModifiedDate = ModifiedDate;
                dbe.tbl_Account.Add(user);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                return user;
            }

        }

        public static int UserWallet_Auto(int UID, double Amount, int HpID, string Note)
        {
            using (NHSTEntities dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                using (var transaction = dbe.Database.BeginTransaction())
                {
                    try
                    {
                        var ac = AccountController.GetByID(UID);
                        if (ac != null)
                        {
                            double walletleft = Convert.ToDouble(ac.Wallet) + Amount;

                            var a = dbe.tbl_Account.Where(x => x.ID == ac.ID).FirstOrDefault();
                            if (a != null)
                            {
                                a.Wallet = walletleft;
                            }
                            //ac.Wallet = walletleft;

                            tbl_AdminSendUserWallet acc = new tbl_AdminSendUserWallet();
                            acc.UID = UID;
                            acc.Username = ac.Username;
                            acc.Amount = Amount;
                            acc.Status = 2;
                            acc.BankID = 100;
                            acc.TradeContent = "Nạp tiền tự động - " + Note + "";
                            acc.CreatedDate = DateTime.Now;
                            acc.CreatedBy = ac.Username;
                            dbe.tbl_AdminSendUserWallet.Add(acc);

                            tbl_HistoryPayWallet payWallet = new tbl_HistoryPayWallet();
                            payWallet.UID = UID;
                            payWallet.UserName = acc.Username;
                            payWallet.MainOrderID = 0;
                            payWallet.Amount = Amount;
                            payWallet.HContent = "Nạp tiền tự động - " + Note + "";
                            payWallet.MoneyLeft = walletleft;
                            payWallet.Type = 2;
                            payWallet.TradeType = 4;
                            payWallet.CreatedDate = DateTime.Now;
                            payWallet.CreatedBy = ac.Username;
                            dbe.tbl_HistoryPayWallet.Add(payWallet);

                            var checkhis = dbe.tbl_HistoryAutoBanking.Where(x => x.UID == UID && x.Note == Note).FirstOrDefault();
                            if (checkhis != null)
                            {
                                checkhis.Status = 2;
                            }
                        }

                        string url = "http://tt.mona.media/paymentservice.asmx/UpdateStatus?UID=4&Key=monasms-autobanking&PayID=" + HpID + "";
                        var kq = PJUtils.ConnectApi(url);

                        dbe.SaveChanges();
                        transaction.Commit();
                        return 1;

                    }
                    catch
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
        public static string UpdatePasswordSystem(int ID, string Password, string NewPassword)
        {
            using (var db = new NHSTEntities())
            {
                string pass = PJUtils.Encrypt("userpass", Password);
                var ac = db.tbl_Account.Where(x => x.ID == ID).FirstOrDefault();
                if (ac != null)
                {
                    if (ac.Password == pass)
                    {
                        ac.Password = PJUtils.Encrypt("userpass", NewPassword);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        int kq = db.SaveChanges();
                        return kq.ToString();
                    }
                    else
                        return "fail";
                }
                else
                    return "none";
            }
        }

     











    }
}