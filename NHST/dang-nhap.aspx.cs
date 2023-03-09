using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class dang_nhap2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {

                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            var ac = AccountController.Login(username.Trim().ToLower(), password.Trim());
            if (ac != null)
            {
                if (ac.Status == 2)
                {
                    int role = Convert.ToInt32(ac.RoleID);
                    if (role != 1)
                    {
                        //Session["userLoginSystem"] = username;
                        //if(role == 4)
                        //    Response.Redirect("/manager/TQWareHouse.aspx");
                        //else if (role == 5)
                        //    Response.Redirect("/manager/VNWarehouse");
                        //else
                        //    Response.Redirect("/manager/orderlist?t=1");
                        lblError.Text = "Sai Username hoặc Password, vui lòng kiểm tra lại.";
                        lblError.Visible = true;
                    }
                    else
                    {
                        Session["userLoginSystem"] = username;
                        Response.Redirect("/danh-sach-don-hang?t=1");
                        
                    }

                }
                else
                {
                    lblError.Text = "Tài khoản của bạn đã bị khóa.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Sai Username hoặc Password, vui lòng kiểm tra lại.";
                lblError.Visible = true;
            }
        }
    }
}