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
    public class SmsForwardController
    {
        public static string Insert(string so_tien, string ten_bank, string ma_gd, string noi_dung, string soDu_bank, string thoi_gian, string trans_id, string ma_baoMat, int Type)
        {
            using (var db = new NHSTEntities())
            {
                tbl_SmsForward s = new tbl_SmsForward();
                s.so_tien = so_tien;
                s.soDu_bank = soDu_bank;
                s.ten_bank = ten_bank;
                s.noi_dung = noi_dung;
                s.ma_gd = ma_gd;
                s.thoi_gian = thoi_gian;
                s.trans_id = trans_id;
                s.ma_baoMat = ma_baoMat;
                s.Type = Type;
                s.Status = 1;
                s.CreatedDate = DateTime.Now;
                db.tbl_SmsForward.Add(s);
                db.SaveChanges();
                return s.ID.ToString();
            }
        }
        public static string updateStatus(int ID, int Status,int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_SmsForward.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.Type = Type;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static tbl_SmsForward GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmsForward a = dbe.tbl_SmsForward.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_SmsForward> GetAll(string s, string content)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmsForward> aus = new List<tbl_SmsForward>();
                aus = dbe.tbl_SmsForward.Where(a => a.ten_bank.Contains(s) || a.noi_dung.Contains(content)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }

        public static tbl_SmsForward Check(string ten_bank, string trans_id, string ma_gd, string so_tien, string so_du)
        {
            using (var db = new NHSTEntities())
            {
                var c = db.tbl_SmsForward.Where(x => x.ten_bank == ten_bank && x.trans_id == trans_id && x.ma_gd == ma_gd && x.so_tien == so_tien && x.soDu_bank == so_du).FirstOrDefault();
                return c;
            }
        }

        public static List<tbl_SmsForward> GetAllBySQL(string Content, string Bank, string fd, string td)
        {
            var list = new List<tbl_SmsForward>();
            var sql = @"select ac.Username, sms.* from tbl_SmsForward as sms
                        left join tbl_AdminSendUserWallet as adm on adm.SmsForwardID = sms.ID
                            left join tbl_Account as ac on ac.ID = adm.UID
                            where sms.ID > 0";



            if (!string.IsNullOrEmpty(Content))
            {
                sql += " AND sms.noi_dung Like N'%" + Content + "%' ";
            }

            if (!string.IsNullOrEmpty(Bank))
            {
                sql += " AND sms.ten_bank Like N'%" + Bank + "%' ";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND sms.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND sms.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By sms.ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_SmsForward();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
              
                if (reader["so_tien"] != DBNull.Value)
                    entity.so_tien = reader["so_tien"].ToString();

                if (reader["ten_bank"] != DBNull.Value)
                    entity.ten_bank = reader["ten_bank"].ToString();

                if (reader["ma_gd"] != DBNull.Value)
                    entity.ma_gd = reader["ma_gd"].ToString();

                if (reader["noi_dung"] != DBNull.Value)
                    entity.noi_dung = reader["noi_dung"].ToString();

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["soDu_bank"] != DBNull.Value)
                    entity.soDu_bank = reader["soDu_bank"].ToString();

                if (reader["thoi_gian"] != DBNull.Value)
                    entity.thoi_gian = reader["thoi_gian"].ToString();

                if (reader["trans_id"] != DBNull.Value)
                    entity.trans_id = reader["trans_id"].ToString();

                if (reader["ma_baoMat"] != DBNull.Value)
                    entity.ma_baoMat = reader["ma_baoMat"].ToString();

                if (reader["so_tien"] != DBNull.Value)
                    entity.so_tien = reader["so_tien"].ToString();

                if (reader["Type"] != DBNull.Value)
                    entity.Type = reader["Type"].ToString().ToInt();
               
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
            
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }


        public static double GetTotalPrice(string Content, string Bank, string fd, string td)
        {
            var list = new List<tbl_SmsForward>();
            var sql = @"select Sum(Convert(float, so_tien)) as total from tbl_SmsForward where ID > 0 And Status = 2 ";


            if (!string.IsNullOrEmpty(Content))
            {
                sql += " AND noi_dung Like N'%" + Content + "%' ";
            }

            if (!string.IsNullOrEmpty(Bank))
            {
                sql += " AND ten_bank Like N'%" + Bank + "%' ";
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

    }
}