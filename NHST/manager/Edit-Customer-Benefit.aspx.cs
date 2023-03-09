using MB.Extensions;
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
    public partial class Edit_Customer_Benefit : System.Web.UI.Page
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
                LoadData();

            }
        }

        public void LoadData()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var cusb = CustomerBenefitsController.GetByID(id);
                if (cusb != null)
                {
                    ViewState["NID"] = id;
                    txtTitle.Text = cusb.CustomerBenefitName;
                    txtSummary.Text = cusb.CustomerBenefitDescription;
                    txtCustomerBenefitLink.Text = cusb.CustomerBenefitLink;
                    isHidden.Checked = Convert.ToBoolean(cusb.IsHidden);

                    if (!string.IsNullOrEmpty(cusb.Icon))
                    {
                        imgDaiDien.ImageUrl = cusb.Icon;
                    }
                    pPosition.Value = cusb.Position;

                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int CBID = ViewState["NID"].ToString().ToInt(0);
            string BackLink = "/manager/Home-Config.aspx";

            string CustomerBenefitName = txtTitle.Text;
            string CustomerBenefitDescription = txtSummary.Text;
            string CustomerBenefitLink = txtCustomerBenefitLink.Text;
            int Position = Convert.ToInt32(pPosition.Value);

            bool IsHidden = isHidden.Checked;
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";

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



            var kq = CustomerBenefitsController.Update(CBID, IMG, CustomerBenefitName, CustomerBenefitDescription, CustomerBenefitLink, IsHidden, Position, Email);

            if (kq != null)
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