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
    public partial class EditPage : System.Web.UI.Page
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
                LoadDDLPageType();
                LoadNews();

            }
        }
        public void LoadDDLPageType()
        {
            var pt = PageTypeController.GetAll();
            if (pt != null)
            {
                if (pt.Count > 0)
                {
                    ddlPageType.DataSource = pt;
                    ddlPageType.DataBind();
                }
            }
        }
        public void LoadNews()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = PageController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtTitle.Text = news.Title;
                    txtSummary.Text = news.Summary;
                    pContent.Content = news.PageContent;
                    ddlPageType.SelectedValue = news.PageTypeID.ToString();
                    isHidden.Checked = Convert.ToBoolean(news.IsHidden);
                    txtLink.Text = news.NodeAliasPath;
                    if (!string.IsNullOrEmpty(news.IMG))
                    {
                        imgDaiDien.ImageUrl = news.IMG;
                    }
                    txtOGTitle.Text = news.ogtitle;
                    txtOGDescription.Text = news.ogdescription;
                    if (!string.IsNullOrEmpty(news.ogimage))
                    {
                        Image1.ImageUrl = news.ogimage;
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
            DateTime currentDate = DateTime.Now;
            int NewsID = ViewState["NID"].ToString().ToInt(0);
            string BackLink = "/manager/PageList.aspx";
            var news = PageController.GetByID(NewsID);
            if (news != null)
            {
                string NewsTitle = txtTitle.Text;
                string NewsSummary = txtSummary.Text;
                string NewsDescription = pContent.Content;
                bool IsHidden = isHidden.Checked;
                string IMG = "";
                string KhieuNaiIMG = "/Uploads/NewsIMG/";
                string categ = ddlPageType.SelectedItem.ToString();
                string NodeAliasPath = "/chuyen-muc/" + LeoUtils.ConvertToUnSign(categ) + "/" + LeoUtils.ConvertToUnSign(NewsTitle);
                if (hinhDaiDien.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in hinhDaiDien.UploadedFiles)
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


                string IMG1 = "";
                string KhieuNaiIMG1 = "/Uploads/Images/";
                if (rOGImage.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in rOGImage.UploadedFiles)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                var o = KhieuNaiIMG1 + Guid.NewGuid() + f.GetExtension();
                                try
                                {
                                    f.SaveAs(Server.MapPath(o));
                                    IMG1 = o;
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                    IMG1 = Image1.ImageUrl;

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
                        NodeController.Update(NodeID, NewsTitle, NodeAliasPath, 2, "tbl_Page", currentDate, Email);
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
                    string kq1 = NodeController.Insert(NewsTitle, NodeAliasPath, 2, "tbl_Page", currentDate, Email);
                    if (kq1.ToInt(0) > 0)
                    {
                        NodeID = kq1.ToInt(0);

                    }

                }

                string kq = PageController.Update(NewsID, NewsTitle, NewsSummary, IMG, NewsDescription, IsHidden, Convert.ToInt32(ddlPageType.SelectedValue),
                    NodeID, NodeAliasPath, "", txtOGTitle.Text, txtOGDescription.Text, IMG1, txtMetaTitle.Text, txtMetaDescription.Text, txtMetakeyword.Text,
                    currentDate, Email);

                if (kq == "ok")
                {
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật trang thành công.", "s", true, BackLink, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật trang. Vui lòng thử lại.", "e", true, Page);
                }
            }

        }
    }
}