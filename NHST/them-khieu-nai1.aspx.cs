using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using NHST.Controllers;
using NHST.Models;
using Telerik.Web.UI;


namespace NHST
{
    public partial class them_khieu_nai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "admin";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }

        }
        public void LoadData()
        {
            if (Request.QueryString["ordershopcode"] != null)
            {
                string ordershopcode = Request.QueryString["ordershopcode"];
                string username = Session["userLoginSystem"].ToString();
                //var u = AccountController.GetByUsername(username);
                //if (u != null)
                //{
                //    int UID = u.ID;
                //    var shops = OrderShopController.GetByOrderShopCodeUID(UID, ordershopcode);
                //    if (shops != null)
                //    {
                //        lblShopOrderCode.Text = ordershopcode;
                //    }
                //}
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int orderid = txtOrderID.Text.ToInt(0);
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                var shops = MainOrderController.GetAllByUIDAndID(UID, orderid);
                if (shops != null)
                {
                    string IMG = "";
                    string KhieuNaiIMG = "/Uploads/KhieuNaiIMG/";
                    if (hinhDaiDien.UploadedFiles.Count > 0)
                    {
                        foreach (UploadedFile f in hinhDaiDien.UploadedFiles)
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
                    else
                        IMG = imgDaiDien.ImageUrl;
                    string kq = ComplainController.Insert(UID, orderid, pAmount.Value.ToString(), IMG, txtNote.Text, 1, DateTime.Now, username);
                    if (kq.ToInt(0) > 0)
                    {
                        OrderCommentController.Insert(UID, "Bạn vừa tạo 1 khiếu nại", true, 1, DateTime.Now, u.ID,3);
                        PJUtils.ShowMessageBoxSwAlert("Tạo khiếu nại thành công", "s", true, Page);
                    }
                }
                else
                {
                    lblError.Text = "Không tìm thấy đơn hàng";
                    lblError.Visible = true;

                    //PJUtils.ShowMessageBoxSwAlert("Không tìm thấy đơn hàng", "e", true, Page);
                }
            }
        }
    }
}