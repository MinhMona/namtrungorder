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
    public partial class AddPage : System.Web.UI.Page
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
                LoadDDLPageType();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            string NewsTitle = txtTitle.Text;
            string NewsSummary = txtSummary.Text;
            string NewsDescription = pContent.Content;
            bool IsHidden = isHidden.Checked;
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/NewsIMG/";
            string categ = ddlPageType.SelectedItem.ToString();
            string NodeAliasPath = "/chuyen-muc/" + LeoUtils.ConvertToUnSign(categ) + "/" + LeoUtils.ConvertToUnSign(NewsTitle);
            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/PageList.aspx";
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

            var checkNode = NodeController.GetByNodeAliasPath(NodeAliasPath);
            if (checkNode.Count > 0)
            {
                int next = checkNode.Count + 1;
                NodeAliasPath += "-" + next;
            }
            string nodeID = NodeController.Insert(NewsTitle, NodeAliasPath, 2, "tbl_Page", currentDate, Email);
            if (nodeID.ToInt(0) > 0)
            {
                string kq = PageController.Insert(NewsTitle, NewsSummary, IMG, NewsDescription, IsHidden, Convert.ToInt32(ddlPageType.SelectedValue),
                    nodeID.ToInt(), NodeAliasPath, "", txtOGTitle.Text, txtOGDescription.Text, IMG1, txtMetaTitle.Text, txtMetaDescription.Text,
                    txtMetakeyword.Text, currentDate, Email);
                if (Convert.ToInt32(kq) > 0)
                {
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo mới trang thành công.", "s", true, BackLink, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới trang. Vui lòng thử lại.", "e", true, Page);
                }
            }
        }
    }
}