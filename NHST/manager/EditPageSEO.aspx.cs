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
    public partial class EditPageSEO : System.Web.UI.Page
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
                LoadNews();

            }
        }
        public void LoadNews()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = PageSEOController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtPageName.Text = news.Pagename;
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
            DateTime currentDate = DateTime.Now;
            int NewsID = ViewState["NID"].ToString().ToInt(0);
            var news = PageSEOController.GetByID(NewsID);
            string BackLink = "/manager/Page-SEO-List.aspx";
            if (news != null)
            {
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

                string kq = PageSEOController.Update(NewsID, txtPageName.Text, "", txtOGTitle.Text, txtOGDescription.Text, IMG, txtMetaTitle.Text,
                    txtMetaDescription.Text, txtMetakeyword.Text, currentDate, Email);
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