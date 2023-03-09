using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Bussiness
{
    public class TableSql
    {
        protected readonly NHSTEntities _context;

        public TableSql(NHSTEntities context)
        {
            _context = context;
        }

        public getTotalCartByUID_Result getTotalCartByUID(int UID)
        {
            var model = _context.getTotalCartByUID(UID).FirstOrDefault();
            return model;
        }

        public List<GetAllByOrderShopTempIDAndUID_Result> GetAllByOrderShopTempIDAndUID(int uid, int shopid)
        {
            var model = _context.GetAllByOrderShopTempIDAndUID(uid, shopid).ToList();
            return model;
        }


        public List<LoadOrderList_Result> LoadOrderList(int? orderType, string txtSearch, int? typeSearch, double? giatu, double? giaden, string startDate, string endDate,
            string ngayPhatTu, string ngayPhatDen, int? status, bool? coMVD, int? roleID, int? UID, int? MainOrderID, int? SalerID, int? DatHang, int? PageSize, int? PageIndex
            )
        {
            var model = _context.LoadOrderList(orderType, txtSearch, typeSearch, giatu, giaden, startDate, endDate,
             ngayPhatTu, ngayPhatDen, status, coMVD, roleID, UID, MainOrderID, SalerID, DatHang, PageSize, PageIndex).ToList();


            return model;
        }

        public List<LoadlistRevenueByDate_Result> LoadlistRevenueByDate(int? minDate, int? maxDate, string txtSearch, string startDate, string endDate,
            int? SalerID, int? DatHang, int? PageSize, int? PageIndex
            )
        {
            var model = _context.LoadlistRevenueByDate(minDate, maxDate, txtSearch, startDate, endDate,
            SalerID, DatHang, PageSize, PageIndex).ToList();


            return model;
        }


    }
}