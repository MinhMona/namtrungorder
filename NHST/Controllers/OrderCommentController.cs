using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;


namespace NHST.Controllers
{
    public class OrderCommentController
    {
        #region CRUD
        public static string Insert(int OrderID, string Comment, bool Status, int Type, DateTime CreatedDate, int CreatedBy, int typeOrder)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OrderComment c = new tbl_OrderComment();
                c.OrderID = OrderID;
                c.Comment = Comment;
                c.Status = Status;
                c.Type = Type;
                c.TypeOrder = typeOrder;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_OrderComment.Add(c);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = c.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select
        public static List<tbl_OrderComment> GetByOrderID(int OrderID, int typeOrder)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderComment> lo = new List<tbl_OrderComment>();
                lo = dbe.tbl_OrderComment.Where(or => or.OrderID == OrderID && or.TypeOrder == typeOrder).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_OrderComment> GetByOrderIDAndType(int OrderID, int Type, int typeOrder)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderComment> lo = new List<tbl_OrderComment>();
                lo = dbe.tbl_OrderComment.Where(or => or.OrderID == OrderID && or.Type == Type && or.TypeOrder == typeOrder).ToList();
                return lo;
            }
        }
        #endregion

        public static string InsertNew(int OrderID, string link, string realName, bool Status, int Type, DateTime CreatedDate, int CreatedBy, int typeOrder)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OrderComment c = new tbl_OrderComment();

                c.OrderID = OrderID;
                c.Status = Status;
                c.Link = link;
                c.Comment = realName;
                c.Type = Type;
                c.TypeContent = 2;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                c.TypeOrder = typeOrder;
                dbe.tbl_OrderComment.Add(c);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = c.ID.ToString();
                return k;
            }
        }
    }
}