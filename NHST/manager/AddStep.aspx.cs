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
    public partial class AddStep : System.Web.UI.Page
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
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";
            string BackLink = "/manager/Home-Config.aspx";
            if (pIcon.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in pIcon.UploadedFiles)
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
            string kq = StepController.Insert(txtStepName.Text, IMG, Convert.ToInt32(pPartnerIndex.Value), 
                txtStepLink.Text, DateTime.Now, Username, txtSummary.Text, txtClassIcon.Text, 
                Convert.ToBoolean(isHidden.Checked));
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}