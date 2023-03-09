using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;
namespace NHST.manager
{
    public partial class Giaodich : System.Web.UI.Page
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
                    if (ac.RoleID == 0 || ac.RoleID == 7 || ac.RoleID == 2)
                    {

                    }
                    else
                        Response.Redirect("/trang-chu");
                }
                loaddata();
                loadPrefix();
            }
        }


        public void loadPrefix()
        {

            
            var dinhkhoan = DinhkhoanController.GetAllTDK("");
            ddlLoaidinhkhoan.Items.Clear();
            ddlLoaidinhkhoan.Items.Insert(0, "Chọn định khoản");
            if (dinhkhoan.Count > 0)
            {
                ddlLoaidinhkhoan.DataSource = dinhkhoan;
                ddlLoaidinhkhoan.DataBind();
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int role = ac.RoleID.ToString().ToInt();

                if (role == 0 || role == 2 || ac.RoleID == 7)
                    pbadmin.Visible = true;
                else
                    pbadmin.Visible = false;
               
                //ViewState["UID"] = id;
                //var a = AccountController.GetByID(id);
                //if (a != null)
                //{
                //    lblUsername.Text = a.Username;
                //    pContent.Content = a.Username + " đã được nạp tiền vào tài khoản.";
                //}
                //else
                //{
                //    Response.Redirect("/manager/Home.aspx");
                //}
            }
        }


        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currentdate = DateTime.Now;
            string BackLink = "/manager/orderlist.aspx";
           
              
            double Amount = Convert.ToDouble(pAmount.Value);
            int DinhKhoanID = ddlLoaidinhkhoan.SelectedValue.ToString().ToInt(0);
            string content = pContent.Content;
            var u_loginin = AccountController.GetByUsername(username_current);
            var dk = DinhkhoanController.GetByID(DinhKhoanID);
            if (u_loginin != null)
            {
                if (dk != null  )
                {
                    if (Amount > 0 )
                    {
                        KyquyController.Insert(dk.ID, Amount,content,currentdate, username_current);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo ký quỹ thành công.", "s", true, BackLink, Page);
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập giá trị định khoản.", "e", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Vui lòng chọn định khoản.", "e", true, Page);
                }
            }
            
         
        }
    }
}