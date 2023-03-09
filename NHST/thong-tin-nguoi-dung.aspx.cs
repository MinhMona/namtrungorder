using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using Telerik.Web.UI;

namespace NHST
{
    public partial class thong_tin_nguoi_dung1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                loadPrefix();
                loaddata();
            }
        }
        public void loadPrefix()
        {
            //var listpre = PJUtils.loadprefix();
            //ddlPrefix.Items.Clear();
            //foreach (var item in listpre)
            //{
            //    ListItem listitem = new ListItem(item.dial_code, item.dial_code);
            //    ddlPrefix.Items.Add(listitem);
            //}
            //ddlPrefix.DataBind();
            ddlWarehouseTo.Items.Insert(0, new ListItem("Chọn kho nhận", "0"));
            var wt = WarehouseController.GetAllWithIsHidden(false);
            if (wt.Count > 0)
            {
                foreach (var item in wt)
                {
                    ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlWarehouseTo.Items.Add(listitem);
                }

            }
            ddlWarehouseTo.DataBind();
        }
        public void loaddata()
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int id = obj_user.ID;
                ViewState["UID"] = id;
                lblUsername.Text = username;
                lblEmail.Text = obj_user.Email;
                var ai = AccountInfoController.GetByUserID(id);
                if (ai != null)
                {
                    txtFirstName.Text = ai.FirstName;
                    txtLastName.Text = ai.LastName;
                    //ddlPrefix.SelectedValue = ai.MobilePhonePrefix;
                    txtPhone.Text = ai.Phone;
                    txtAddress.Text = ai.Address;
                    if (ai.BirthDay != null)
                        rBirthday.SelectedDate = ai.BirthDay;
                    if (ai.Gender != null)
                        ddlGender.SelectedValue = ai.Gender.ToString();
                    //txtEmail.Text = ai.Email;
                    if (!string.IsNullOrEmpty(ai.IMGUser))
                    {
                        imgDaiDien.ImageUrl = ai.IMGUser;
                    }
                }
                if (obj_user.WarehouseTo != null)
                {
                    ddlWarehouseTo.SelectedValue = obj_user.WarehouseTo.ToString();
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int UID = ViewState["UID"].ToString().ToInt(0);
            string pass = txtpass.Text.Trim();
            DateTime birthday = DateTime.Now;
            if (!string.IsNullOrEmpty(rBirthday.SelectedDate.ToString()))
            {
                birthday = Convert.ToDateTime(rBirthday.SelectedDate);
            }
            if (!string.IsNullOrEmpty(pass))
            {
                string confirmpass = txtconfirmpass.Text;
                if (!string.IsNullOrEmpty(confirmpass))
                {
                    if (confirmpass == pass)
                    {
                        string IMG = "";
                        string KhieuNaiIMG = "/Uploads/";
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
                        AccountInfoController.UpdateIMGUser(UID, IMG);
                        string rp = AccountController.UpdatePassword(UID, pass);
                        AccountController.UpdateWarehouseTo(UID, Convert.ToInt32(ddlWarehouseTo.SelectedValue));
                        string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), lblEmail.Text, txtPhone.Text,
                            txtAddress.Text.Trim(), birthday, ddlGender.SelectedValue.ToInt(), "", "", DateTime.Now, lblUsername.Text);

                        if (r == "1" && rp == "1")
                        {
                            PJUtils.ShowMessageBoxSwAlert("Cập nhật thông tin thành công", "s", true, Page);
                            //lblError.Text = "Cập nhật thành công.";
                            //lblError.ForeColor = System.Drawing.Color.Blue;
                            //lblError.Visible = true;
                            //PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                        }
                        else if (r == "1" && rp == "0")
                        {
                            lblConfirmpass.Text = "Mật khẩu mới trùng với mật khẩu cũ.";
                            lblConfirmpass.Visible = true;
                        }
                        else
                        {
                            lblError.Text = "Có lỗi trong quá trình cập nhật.";
                            lblError.Visible = true;
                            //PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                        }
                    }
                    else
                    {
                        lblConfirmpass.Text = "Xác nhận mật khẩu không trùng với mật khẩu.";
                        lblConfirmpass.Visible = true;
                    }
                }
                else
                {
                    lblConfirmpass.Text = "Không để trống xác nhận mật khẩu";
                    lblConfirmpass.Visible = true;
                }
            }
            else
            {
                string IMG = "";
                string KhieuNaiIMG = "/Uploads/";
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
                AccountController.UpdateWarehouseTo(UID, Convert.ToInt32(ddlWarehouseTo.SelectedValue));
                AccountInfoController.UpdateIMGUser(UID, IMG);
                string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), lblEmail.Text, txtPhone.Text,
                    txtAddress.Text.Trim(), birthday, ddlGender.SelectedValue.ToInt(), "", "", DateTime.Now, lblUsername.Text);
                if (r == "1")
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thông tin thành công", "s", true, Page);
                    //lblError.Text = "Cập nhật thành công.";
                    //lblError.ForeColor = System.Drawing.Color.Blue;
                    //lblError.Visible = true;
                    //PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                }
                else
                {
                    lblError.Text = "Có lỗi trong quá trình cập nhật.";
                    lblError.Visible = true;
                    //PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                }
            }
        }
    }
}