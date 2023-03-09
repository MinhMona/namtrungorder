using MB.Extensions;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Business;

namespace NHST.Controllers
{
    public class TransportationOrderNewController
    {
        #region CRUD
        public static string Insert(int UID, string Username, string Weight, string Currency, string AdditionFeeCYN,
            string AdditionFeeVND, string FeeWarehouseOutCYN, string FeeWarehouseOutVND, string FeeWarehouseWeightCYN,
            string FeeWarehouseWeightVND, string SensorFeeCYN, string SensorFeeeVND, int SmallPackageID, string BarCode, int Status, string Note, string StaffNote,
            string TotalPriceCYN, string TotalPriceVND, DateTime CreatedDate, string CreatedBy, int WareHouseFromID, int WareHouseID, int ShippingTypeID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrderNew p = new tbl_TransportationOrderNew();
                p.UID = UID;
                p.Username = Username;
                p.Weight = Weight;
                p.Currency = Currency;
                p.AdditionFeeCYN = AdditionFeeCYN;
                p.AdditionFeeVND = AdditionFeeVND;
                p.FeeWarehouseOutCYN = FeeWarehouseOutCYN;
                p.FeeWarehouseOutVND = FeeWarehouseOutVND;
                p.FeeWarehouseWeightCYN = FeeWarehouseWeightCYN;
                p.FeeWarehouseWeightVND = FeeWarehouseWeightVND;
                p.SensorFeeCYN = SensorFeeCYN;
                p.SensorFeeeVND = SensorFeeeVND;
                p.SmallPackageID = SmallPackageID;
                p.BarCode = BarCode;
                p.Status = Status;
                p.Note = Note;
                p.StaffNote = StaffNote;
                p.TotalPriceCYN = TotalPriceCYN;
                p.TotalPriceVND = TotalPriceVND;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                p.WareHouseFromID = WareHouseFromID;
                p.WareHouseID = WareHouseID;
                p.ShippingTypeID = ShippingTypeID;
                dbe.tbl_TransportationOrderNew.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int UID, string Username, string Weight, string Currency, string AdditionFeeCYN,
            string AdditionFeeVND, string FeeWarehouseOutCYN, string FeeWarehouseOutVND, string FeeWarehouseWeightCYN,
            string FeeWarehouseWeightVND, string SensorFeeCYN, string SensorFeeeVND, int SmallPackageID, string BarCode, int Status, string Note, string StaffNote,
            string TotalPriceCYN, string TotalPriceVND, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.UID = UID;
                    p.Username = Username;
                    p.Weight = Weight;
                    p.Currency = Currency;
                    p.AdditionFeeCYN = AdditionFeeCYN;
                    p.AdditionFeeVND = AdditionFeeVND;
                    p.FeeWarehouseOutCYN = FeeWarehouseOutCYN;
                    p.FeeWarehouseOutVND = FeeWarehouseOutVND;
                    p.FeeWarehouseWeightCYN = FeeWarehouseWeightCYN;
                    p.FeeWarehouseWeightVND = FeeWarehouseWeightVND;
                    p.SensorFeeCYN = SensorFeeCYN;
                    p.SensorFeeeVND = SensorFeeeVND;
                    p.SmallPackageID = SmallPackageID;
                    p.BarCode = BarCode;
                    p.Status = Status;
                    p.Note = Note;
                    p.StaffNote = StaffNote;
                    p.TotalPriceCYN = TotalPriceCYN;
                    p.TotalPriceVND = TotalPriceVND;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateDateInVNWareHouse(int ID, DateTime DateInVNWareHouse)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.DateInVNWareHouse = DateInVNWareHouse;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateSmallPackageID(int ID, int SmallPackageID)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.SmallPackageID = SmallPackageID;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }

        public static string UpdateIsExportRequest(int ID, bool IsExportRequest)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.IsExportRequest = IsExportRequest;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }

        public static string UpdateMainOrderCode(int ID, string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.MainOrderCode = MainOrderCode;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }

        public static List<tbl_TransportationOrderNew> GetByMainOrderCode(string MainOrderCode)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_TransportationOrderNew.Where(x => x.MainOrderCode == MainOrderCode).ToList();
                return p;
            }
        }

        public static string UpdateDateExport(int ID, DateTime DateExport, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.DateExport = DateExport;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateWeight(int ID, string Weight, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Weight = Weight;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateFee(int ID, string AdditionFeeCYN, string AdditionFeeVND,
            string SensorFeeCYN, string SensorFeeeVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.AdditionFeeCYN = AdditionFeeCYN;
                    p.AdditionFeeVND = AdditionFeeVND;
                    p.SensorFeeCYN = SensorFeeCYN;
                    p.SensorFeeeVND = SensorFeeeVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateCancelOrder(int ID, int Status, string CancelReason, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.CancelReason = CancelReason;
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateRequestOutStock(int ID, int Status, string ExportRequestNote, DateTime DateExportRequest,
            int shippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrderNew.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.ExportRequestNote = ExportRequestNote;
                    p.DateExportRequest = DateExportRequest;
                    p.ShippingTypeVN = shippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_TransportationOrderNew GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrderNew page = dbe.tbl_TransportationOrderNew.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }

        public static List<tbl_TransportationOrderNew> GetAllByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var page = dbe.tbl_TransportationOrderNew.Where(p => p.ID == ID).OrderByDescending(x=>x.ID).ToList();
                return page;
            }
        }

        public static tbl_TransportationOrderNew GetByIDAndUID(int ID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrderNew page = dbe.tbl_TransportationOrderNew.Where(p => p.ID == ID && p.UID == UID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static List<tbl_TransportationOrderNew> GetByUIDAndStatus(int UID, int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrderNew> ts = new List<tbl_TransportationOrderNew>();
                ts = dbe.tbl_TransportationOrderNew.Where(p => p.Status == status && p.UID == UID).ToList();
                return ts;
            }
        }
        public static List<tbl_TransportationOrderNew> GetAllByUIDWithFilter_SqlHelper(int UID,
            string textSearch, int type, int status, string fd, string td)
        {
            var list = new List<tbl_TransportationOrderNew>();
            var sql = @"SELECT * FROM dbo.tbl_TransportationOrderNew ";
            sql += " where UID = " + UID + " ";
            if (!string.IsNullOrEmpty(textSearch))
            {
                if (type == 3)
                {
                    sql += " AND ID = " + textSearch;
                }
                else
                {
                    sql += " AND BarCode like N'%" + textSearch + "%'";
                }
            }
            if (status > -1)
                sql += " AND Status = " + status;

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
            sql += " Order By ID asc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_TransportationOrderNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Weight"] != DBNull.Value)
                    entity.Weight = reader["Weight"].ToString();
                if (reader["Currency"] != DBNull.Value)
                    entity.Currency = reader["Currency"].ToString();
                if (reader["AdditionFeeCYN"] != DBNull.Value)
                    entity.AdditionFeeCYN = reader["AdditionFeeCYN"].ToString();
                if (reader["AdditionFeeVND"] != DBNull.Value)
                    entity.AdditionFeeVND = reader["AdditionFeeVND"].ToString();
                if (reader["FeeWarehouseOutCYN"] != DBNull.Value)
                    entity.FeeWarehouseOutCYN = reader["FeeWarehouseOutCYN"].ToString();
                if (reader["FeeWarehouseOutVND"] != DBNull.Value)
                    entity.FeeWarehouseOutVND = reader["FeeWarehouseOutVND"].ToString();
                if (reader["FeeWarehouseWeightCYN"] != DBNull.Value)
                    entity.FeeWarehouseWeightCYN = reader["FeeWarehouseWeightCYN"].ToString();
                if (reader["FeeWarehouseWeightVND"] != DBNull.Value)
                    entity.FeeWarehouseWeightVND = reader["FeeWarehouseWeightVND"].ToString();
                if (reader["SensorFeeCYN"] != DBNull.Value)
                    entity.SensorFeeCYN = reader["SensorFeeCYN"].ToString();
                if (reader["SensorFeeeVND"] != DBNull.Value)
                    entity.SensorFeeeVND = reader["SensorFeeeVND"].ToString();
                if (reader["SmallPackageID"] != DBNull.Value)
                    entity.SmallPackageID = reader["SmallPackageID"].ToString().ToInt(0);
                if (reader["BarCode"] != DBNull.Value)
                    entity.BarCode = reader["BarCode"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                if (reader["StaffNote"] != DBNull.Value)
                    entity.StaffNote = reader["StaffNote"].ToString();
                if (reader["TotalPriceCYN"] != DBNull.Value)
                    entity.TotalPriceCYN = reader["TotalPriceCYN"].ToString();
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["ExportRequestNote"] != DBNull.Value)
                    entity.ExportRequestNote = reader["ExportRequestNote"].ToString();
                if (reader["DateExportRequest"] != DBNull.Value)
                    entity.DateExportRequest = Convert.ToDateTime(reader["DateExportRequest"].ToString());
                if (reader["DateExport"] != DBNull.Value)
                    entity.DateExport = Convert.ToDateTime(reader["DateExport"].ToString());
                if (reader["ShippingTypeVN"] != DBNull.Value)
                    entity.ShippingTypeVN = reader["ShippingTypeVN"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["ModifiedDate"] != DBNull.Value)
                    entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                if (reader["ModifiedBy"] != DBNull.Value)
                    entity.ModifiedBy = reader["ModifiedBy"].ToString();
                if (reader["WareHouseFromID"] != DBNull.Value)
                    entity.WareHouseFromID = reader["WareHouseFromID"].ToString().ToInt(0);
                if (reader["WareHouseID"] != DBNull.Value)
                    entity.WareHouseID = reader["WareHouseID"].ToString().ToInt(0);
                if (reader["ShippingTypeID"] != DBNull.Value)
                    entity.ShippingTypeID = reader["ShippingTypeID"].ToString().ToInt(0);
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<tbl_TransportationOrderNew> GetAllWithFilter_SqlHelper(int status, string fd,
            string td)
        {
            var list = new List<tbl_TransportationOrderNew>();
            var sql = @"SELECT * FROM dbo.tbl_TransportationOrderNew ";
            sql += " where 1 = 1 ";

            if (status > -1)
                sql += " AND Status = " + status;

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
            sql += " Order By ID asc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_TransportationOrderNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Weight"] != DBNull.Value)
                    entity.Weight = reader["Weight"].ToString();
                if (reader["Currency"] != DBNull.Value)
                    entity.Currency = reader["Currency"].ToString();
                if (reader["AdditionFeeCYN"] != DBNull.Value)
                    entity.AdditionFeeCYN = reader["AdditionFeeCYN"].ToString();
                if (reader["AdditionFeeVND"] != DBNull.Value)
                    entity.AdditionFeeVND = reader["AdditionFeeVND"].ToString();
                if (reader["FeeWarehouseOutCYN"] != DBNull.Value)
                    entity.FeeWarehouseOutCYN = reader["FeeWarehouseOutCYN"].ToString();
                if (reader["FeeWarehouseOutVND"] != DBNull.Value)
                    entity.FeeWarehouseOutVND = reader["FeeWarehouseOutVND"].ToString();
                if (reader["FeeWarehouseWeightCYN"] != DBNull.Value)
                    entity.FeeWarehouseWeightCYN = reader["FeeWarehouseWeightCYN"].ToString();
                if (reader["FeeWarehouseWeightVND"] != DBNull.Value)
                    entity.FeeWarehouseWeightVND = reader["FeeWarehouseWeightVND"].ToString();
                if (reader["SensorFeeCYN"] != DBNull.Value)
                    entity.SensorFeeCYN = reader["SensorFeeCYN"].ToString();
                if (reader["SensorFeeeVND"] != DBNull.Value)
                    entity.SensorFeeeVND = reader["SensorFeeeVND"].ToString();
                if (reader["SmallPackageID"] != DBNull.Value)
                    entity.SmallPackageID = reader["SmallPackageID"].ToString().ToInt(0);
                if (reader["BarCode"] != DBNull.Value)
                    entity.BarCode = reader["BarCode"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                if (reader["StaffNote"] != DBNull.Value)
                    entity.StaffNote = reader["StaffNote"].ToString();
                if (reader["TotalPriceCYN"] != DBNull.Value)
                    entity.TotalPriceCYN = reader["TotalPriceCYN"].ToString();
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["ExportRequestNote"] != DBNull.Value)
                    entity.ExportRequestNote = reader["ExportRequestNote"].ToString();
                if (reader["DateExportRequest"] != DBNull.Value)
                    entity.DateExportRequest = Convert.ToDateTime(reader["DateExportRequest"].ToString());
                if (reader["DateExport"] != DBNull.Value)
                    entity.DateExport = Convert.ToDateTime(reader["DateExport"].ToString());
                if (reader["ShippingTypeVN"] != DBNull.Value)
                    entity.ShippingTypeVN = reader["ShippingTypeVN"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["DateInVNWareHouse"] != DBNull.Value)
                    entity.DateInVNWareHouse = Convert.ToDateTime(reader["DateInVNWareHouse"].ToString());
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["ModifiedDate"] != DBNull.Value)
                    entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                if (reader["ModifiedBy"] != DBNull.Value)
                    entity.ModifiedBy = reader["ModifiedBy"].ToString();

                if (reader["WareHouseFromID"] != DBNull.Value)
                    entity.WareHouseFromID = reader["WareHouseFromID"].ToString().ToInt(0);
                if (reader["WareHouseID"] != DBNull.Value)
                    entity.WareHouseID = reader["WareHouseID"].ToString().ToInt(0);
                if (reader["ShippingTypeID"] != DBNull.Value)
                    entity.ShippingTypeID = reader["ShippingTypeID"].ToString().ToInt(0);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        #endregion
        #region Properties
        public class TOrderNew
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public string Weight { get; set; }
            public string AdditionFeeCYN { get; set; }
            public string AdditionFeeVND { get; set; }
            public string FeeWarehouseOutCYN { get; set; }
            public string FeeWarehouseOutVND { get; set; }
            public string FeeWarehouseWeightCYN { get; set; }
            public string FeeWarehouseWeightVND { get; set; }
            public string SensorFeeCYN { get; set; }
            public string SensorFeeeVND { get; set; }
            public string SmallPackageID { get; set; }
            public string BarCode { get; set; }
        }
        #endregion


        public static List<tbl_TransportationOrderNew> GetAllByUIDWithFilter_SqlHelperWithPage(int UID,
          string textSearch, int type, int status, string fd, string td, int pageIndex, int pageSize)
        {
            var list = new List<tbl_TransportationOrderNew>();
            var sql = @"SELECT * FROM dbo.tbl_TransportationOrderNew ";
            sql += " where UID = " + UID + " ";
            if (!string.IsNullOrEmpty(textSearch))
            {
                if (type == 3)
                {
                    sql += " AND ID = " + textSearch;
                }
                else
                {
                    sql += " AND BarCode like N'%" + textSearch + "%'";
                }
            }
            if (status > -1)
                sql += " AND Status = " + status;

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
            sql += " Order By ID desc OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_TransportationOrderNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Weight"] != DBNull.Value)
                    entity.Weight = reader["Weight"].ToString();
                if (reader["Currency"] != DBNull.Value)
                    entity.Currency = reader["Currency"].ToString();
                if (reader["AdditionFeeCYN"] != DBNull.Value)
                    entity.AdditionFeeCYN = reader["AdditionFeeCYN"].ToString();
                if (reader["AdditionFeeVND"] != DBNull.Value)
                    entity.AdditionFeeVND = reader["AdditionFeeVND"].ToString();
                if (reader["FeeWarehouseOutCYN"] != DBNull.Value)
                    entity.FeeWarehouseOutCYN = reader["FeeWarehouseOutCYN"].ToString();
                if (reader["FeeWarehouseOutVND"] != DBNull.Value)
                    entity.FeeWarehouseOutVND = reader["FeeWarehouseOutVND"].ToString();
                if (reader["FeeWarehouseWeightCYN"] != DBNull.Value)
                    entity.FeeWarehouseWeightCYN = reader["FeeWarehouseWeightCYN"].ToString();
                if (reader["FeeWarehouseWeightVND"] != DBNull.Value)
                    entity.FeeWarehouseWeightVND = reader["FeeWarehouseWeightVND"].ToString();
                if (reader["SensorFeeCYN"] != DBNull.Value)
                    entity.SensorFeeCYN = reader["SensorFeeCYN"].ToString();
                if (reader["SensorFeeeVND"] != DBNull.Value)
                    entity.SensorFeeeVND = reader["SensorFeeeVND"].ToString();
                if (reader["SmallPackageID"] != DBNull.Value)
                    entity.SmallPackageID = reader["SmallPackageID"].ToString().ToInt(0);
                if (reader["BarCode"] != DBNull.Value)
                    entity.BarCode = reader["BarCode"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                if (reader["StaffNote"] != DBNull.Value)
                    entity.StaffNote = reader["StaffNote"].ToString();
                if (reader["TotalPriceCYN"] != DBNull.Value)
                    entity.TotalPriceCYN = reader["TotalPriceCYN"].ToString();
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["ExportRequestNote"] != DBNull.Value)
                    entity.ExportRequestNote = reader["ExportRequestNote"].ToString();
                if (reader["DateExportRequest"] != DBNull.Value)
                    entity.DateExportRequest = Convert.ToDateTime(reader["DateExportRequest"].ToString());
                if (reader["DateExport"] != DBNull.Value)
                    entity.DateExport = Convert.ToDateTime(reader["DateExport"].ToString());
                if (reader["ShippingTypeVN"] != DBNull.Value)
                    entity.ShippingTypeVN = reader["ShippingTypeVN"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["ModifiedDate"] != DBNull.Value)
                    entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                if (reader["ModifiedBy"] != DBNull.Value)
                    entity.ModifiedBy = reader["ModifiedBy"].ToString();
                if (reader["WareHouseFromID"] != DBNull.Value)
                    entity.WareHouseFromID = reader["WareHouseFromID"].ToString().ToInt(0);
                if (reader["WareHouseID"] != DBNull.Value)
                    entity.WareHouseID = reader["WareHouseID"].ToString().ToInt(0);
                if (reader["ShippingTypeID"] != DBNull.Value)
                    entity.ShippingTypeID = reader["ShippingTypeID"].ToString().ToInt(0);
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static int GetTotalUser(int UID,
        string textSearch, int type, int status, string fd, string td)
        {
            var sql = @"SELECT count(*) as Total FROM dbo.tbl_TransportationOrderNew ";
            sql += " where UID = " + UID + " ";
            if (!string.IsNullOrEmpty(textSearch))
            {
                if (type == 3)
                {
                    sql += " AND ID = " + textSearch;
                }
                else
                {
                    sql += " AND BarCode like N'%" + textSearch + "%'";
                }
            }
            if (status > -1)
                sql += " AND Status = " + status;

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
            int i = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    i = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return i;
        }

    }
}