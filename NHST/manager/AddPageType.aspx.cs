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
    public partial class AddPageType : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            string PageTypeName = txtPageTypeName.Text;
            string PageTypeDescription = pPageTypeDescription.Text;
            DateTime currentDate = DateTime.Now;
            string NodeAliasPath = "/chuyen-muc/" + LeoUtils.ConvertToUnSign(PageTypeName);
            var checkNode = NodeController.GetByNodeAliasPath(NodeAliasPath);
            if (checkNode.Count > 0)
            {
                int next = checkNode.Count + 1;
                NodeAliasPath += "-" + next;
            }
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";
            string BackLink = "/manager/Page-Type-List.aspx";
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


            string nodeID = NodeController.Insert(PageTypeName, NodeAliasPath, 1, "tbl_PageType", currentDate, Email);

            string kq = PageTypeController.Insert(PageTypeName, PageTypeDescription, 1, nodeID.ToInt(0), NodeAliasPath,
                "", txtOGTitle.Text, txtOGDescription.Text, IMG, txtMetaTitle.Text, txtMetaDescription.Text, txtMetakeyword.Text,
                currentDate, Email);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo mới danh mục thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới danh mục. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}