using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;

namespace NHST.manager
{
    public partial class SupportDetail : System.Web.UI.Page
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
                    if (ac != null)
                    {
                        if (ac.RoleID != 0 && ac.RoleID != 2)
                            Response.Redirect("/trang-chu");
                        else
                            LoadData();
                    }
                }
            }
        }
        public void LoadData()
        {
            if (Request.QueryString["ID"] != null)
            {
                int ID = Request.QueryString["ID"].ToInt(0);
                if (ID > 0)
                {
                    var com = SupportController.GetByID(ID);
                    if (com != null)
                    {
                        txtUsername.Text = com.CreatedBy;
                        txtFullname.Text = com.FullName;
                        txtPhone.Text = com.Phone;
                        txtEmail.Text = com.Email;
                        txtComplainText.Text = com.HContent;

                        if (!string.IsNullOrEmpty(com.FileIMG))
                        {
                            imgDaiDien.ImageUrl = com.FileIMG;
                        }
                    }
                }
            }
        }
    }
}