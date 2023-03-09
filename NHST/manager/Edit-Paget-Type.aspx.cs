using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;

namespace NHST.manager
{
    public partial class Edit_Paget_Type : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                //LoadDDLPageType();
                LoadNews();
            }
        }
        public void LoadNews()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = PageTypeController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtPageTypeName.Text = news.PageTypeName;
                    pPageTypeDescription.Content = news.PageTypeDescription;
                    txtOGTitle.Text = news.ogtitle;
                    txtOGDescription.Text = news.ogdescription;
                    if (!string.IsNullOrEmpty(news.ogimage))
                    {
                        imgDaiDien.ImageUrl = news.ogimage;
                    }
                    txtMetaTitle.Text = news.metatitle;
                    txtMetaDescription.Text = news.metadescription;
                    txtMetakeyword.Text = news.metakeyword;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();

            int NewsID = ViewState["NID"].ToString().ToInt(0);
            DateTime currentDate = DateTime.Now;
            var news = PageTypeController.GetByID(NewsID);
            string BackLink = "/manager/Page-Type-List.aspx";
            if (news != null)
            {

                string PageTypeName = txtPageTypeName.Text;
                string PageTypeDescription = pPageTypeDescription.Content;

                string NodeAliasPath = "/chuyen-muc/" + LeoUtils.ConvertToUnSign(PageTypeName);

                int NodeID = 0;
                if (news.NodeID != null)
                {
                    NodeID = Convert.ToInt32(news.NodeID);
                    var node = NodeController.GetByID(NodeID);
                    if (node != null)
                    {

                        var checkNode = NodeController.GetByNodeAliasPathAndNotContainsID(NodeAliasPath, NodeID);
                        if (checkNode.Count > 0)
                        {
                            int next = checkNode.Count + 1;
                            NodeAliasPath += "-" + next;
                        }
                        NodeController.Update(NodeID, PageTypeName, NodeAliasPath, 1, "tbl_PageType", currentDate, Email);
                    }
                }
                else
                {
                    var checkNode = NodeController.GetByNodeAliasPath(NodeAliasPath);
                    if (checkNode.Count > 0)
                    {
                        int next = checkNode.Count + 1;
                        NodeAliasPath += "-" + next;
                    }
                    string kq1 = NodeController.Insert(PageTypeName, NodeAliasPath, 2, "tbl_PageType", currentDate, Email);
                    if (kq1.ToInt(0) > 0)
                    {
                        NodeID = kq1.ToInt(0);

                    }
                }
                string IMG = "";
                string KhieuNaiIMG = "/Uploads/Images/";
                if (rOGImage.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in rOGImage.UploadedFiles)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                var o = KhieuNaiIMG + Guid.NewGuid() + f.GetExtension();
                                try
                                {
                                    f.SaveAs(Server.MapPath(o));
                                    IMG = o;
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                    IMG = imgDaiDien.ImageUrl;


                string kq = PageTypeController.Update(NewsID, PageTypeName, PageTypeDescription, 1, NodeID, NodeAliasPath,
                    "", txtOGTitle.Text, txtOGDescription.Text, IMG, txtMetaTitle.Text, txtMetaDescription.Text, txtMetakeyword.Text,
                    DateTime.Now, Email);
                if (kq == "ok")
                {
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật danh mục thành công.", "s", true, BackLink, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật danh mục. Vui lòng thử lại.", "e", true, Page);
                }
            }
        }
    }
}