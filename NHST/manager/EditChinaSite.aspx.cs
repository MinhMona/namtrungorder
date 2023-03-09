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
    public partial class EditChinaSite : System.Web.UI.Page
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

                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = ChinaSiteController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtSitename.Text = news.Sitename;
                    if (!string.IsNullOrEmpty(news.SiteLogo))
                    {
                        imgDaiDien.ImageUrl = news.SiteLogo;
                    }
                    chkIshidden.Checked = Convert.ToBoolean(news.IsHidden);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();

            int NewsID = ViewState["NID"].ToString().ToInt(0);
            DateTime currentDate = DateTime.Now;
            var news = ChinaSiteController.GetByID(NewsID);
            if (news != null)
            {


                string IMG = "";
                string KhieuNaiIMG = "/Uploads/";
                if (rSiteLogo.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in rSiteLogo.UploadedFiles)
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


                string kq = ChinaSiteController.Update(NewsID, txtSitename.Text, IMG, chkIshidden.Checked, currentDate, Email);
                if (kq == "ok")
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật danh mục. Vui lòng thử lại.", "e", true, Page);
                }
            }
        }
    }
}