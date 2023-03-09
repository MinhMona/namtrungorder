using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class PageController
    {
        #region CRUD
        public static string Insert(string Title, string Summary, string IMG, string PageContent, bool IsHidden, int PageTypeID,
            int NodeID, string NodeAliasPath, string ogurl, string ogtitle, string ogdescription, string ogimage, string metatitle,
            string metadescription, string metakeyword, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Page p = new tbl_Page();
                p.Title = Title;
                p.Summary = Summary;
                p.IMG = IMG;
                p.PageContent = PageContent;
                p.PageTypeID = PageTypeID;
                p.IsHidden = IsHidden;
                p.NodeID = NodeID;
                p.NodeAliasPath = NodeAliasPath;
                p.ogurl = ogurl;
                p.ogtitle = ogtitle;
                p.ogdescription = ogdescription;
                p.ogimage = ogimage;
                p.metatitle = metatitle;
                p.metadescription = metadescription;
                p.metakeyword = metakeyword;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Page.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string Title, string Summary, string IMG, string PageContent, bool IsHidden, int PageTypeID,
             int NodeID, string NodeAliasPath, string ogurl, string ogtitle, string ogdescription, string ogimage, string metatitle,
            string metadescription, string metakeyword, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Page.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Title = Title;
                    p.Summary = Summary;
                    p.IMG = IMG;
                    p.PageContent = PageContent;
                    p.PageTypeID = PageTypeID;
                    p.IsHidden = IsHidden;
                    p.NodeID = NodeID;
                    p.NodeAliasPath = NodeAliasPath;
                    p.ogurl = ogurl;
                    p.ogtitle = ogtitle;
                    p.ogdescription = ogdescription;
                    p.ogimage = ogimage;
                    p.metatitle = metatitle;
                    p.metadescription = metadescription;
                    p.metakeyword = metakeyword;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        #endregion
        #region Select
        public static List<tbl_Page> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Page> pages = new List<tbl_Page>();
                pages = dbe.tbl_Page.Where(p => p.Title.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static List<tbl_Page> GetByPagetTypeID(int PageTypeID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Page> pages = new List<tbl_Page>();
                pages = dbe.tbl_Page.Where(p => p.PageTypeID == PageTypeID && p.IsHidden == false).ToList();
                return pages;
            }
        }
        public static List<tbl_Page> GetTopByPagetTypeID(int TopN, int PageTypeID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Page> pages = new List<tbl_Page>();
                pages = dbe.tbl_Page.Where(p => p.PageTypeID == PageTypeID && p.IsHidden == false).Take(TopN).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static tbl_Page GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Page page = dbe.tbl_Page.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static tbl_Page GetByNodeAliasPath(string NodeAliasPath)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Page page = dbe.tbl_Page.Where(p => p.NodeAliasPath == NodeAliasPath).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}