using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class Add_Service : System.Web.UI.Page
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
            string CustomerBenefitName = txtTitle.Text;
            string link = txtCustomerBenefitLink.Text;

            string CustomerBenefitDescription = txtSummary.Text;
            int Position = Convert.ToInt32(pPosition.Value);
            bool IsHidden = isHidden.Checked;
            string IMG = "";
            string QLIMG = "/Uploads/Images/";

            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/Home-Config.aspx";
            if (hinhDaiDien.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in hinhDaiDien.UploadedFiles)
                {
                    var o = QLIMG + Guid.NewGuid() + f.GetExtension();
                    try
                    {
                        f.SaveAs(Server.MapPath(o));
                        IMG = o;
                    }
                    catch { }
                }
            }

            var kq = ServiceController.Insert(CustomerBenefitName, CustomerBenefitDescription, link, IMG, IsHidden, Position, Email);
            if (kq != null)
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