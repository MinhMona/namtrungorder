using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;

namespace NHST
{
    public partial class thong_tin_nguoi_dung : System.Web.UI.Page
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
            var listpre = PJUtils.loadprefix();
            ddlPrefix.Items.Clear();
            foreach (var item in listpre)
            {
                ListItem listitem = new ListItem(item.dial_code, item.dial_code);
                ddlPrefix.Items.Add(listitem);
            }
            ddlPrefix.DataBind();
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
                    ddlPrefix.SelectedValue = ai.MobilePhonePrefix;
                    txtPhone.Text = ai.MobilePhone;
                    txtAddress.Text = ai.Address;
                    //txtEmail.Text = ai.Email;
                }
            }          
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int UID = ViewState["UID"].ToString().ToInt(0);
            string pass = txtpass.Text.Trim();
            if (!string.IsNullOrEmpty(pass))
            {
                string confirmpass = txtconfirmpass.Text;
                if (!string.IsNullOrEmpty(confirmpass))
                {
                    if (confirmpass == pass)
                    {
                        string rp = AccountController.UpdatePassword(UID, pass);
                        //string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), ddlPrefix.SelectedValue, txtPhone.Text,lblEmail.Text,"", 
                        //    txtAddress.Text.Trim(), "", "", DateTime.Now, lblUsername.Text);
                        //if (r == "1" && rp == "1")
                        //{
                        //    lblError.Text = "Cập nhật thành công.";
                        //    lblError.ForeColor = System.Drawing.Color.Blue;
                        //    lblError.Visible = true;
                        //    //PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                        //}
                        //else if (r == "1" && rp == "0")
                        //{
                        //    lblConfirmpass.Text = "Mật khẩu mới trùng với mật khẩu cũ.";
                        //    lblConfirmpass.Visible = true;
                        //}
                        //else
                        //{
                        //    lblError.Text = "Có lỗi trong quá trình cập nhật.";
                        //    lblError.Visible = true;
                        //    //PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                        //}
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
                //string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), ddlPrefix.SelectedValue, txtPhone.Text, 
                //    lblEmail.Text, "", txtAddress.Text.Trim(), "", "", DateTime.Now, lblUsername.Text);
                //if (r == "1")
                //{
                //    lblError.Text = "Cập nhật thành công.";
                //    lblError.ForeColor = System.Drawing.Color.Blue;
                //    lblError.Visible = true;
                //    //PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                //}
                //else
                //{
                //    lblError.Text = "Có lỗi trong quá trình cập nhật.";
                //    lblError.Visible = true;
                //    //PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                //}
            }
        }
    }
}