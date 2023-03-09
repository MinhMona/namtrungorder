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
    public partial class Present : System.Web.UI.Page
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
                loaddata();
            }
        }
        public void loaddata()
        {
            var c = PresentController.GetByTop1();
            if (c != null)
            {
                pYear.Value = c.Experience;
                pQuantityCustomer.Value = c.QuantityCustomer;
                pQuantityOrder.Value = c.QuantityOrder;
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var c = PresentController.GetByTop1();
            if (c != null)
            {
                var kq = PresentController.Update(c.ID,Convert.ToInt32(pYear.Value), Convert.ToInt32(pQuantityCustomer.Value), Convert.ToInt32(pQuantityOrder.Value));
                if (kq != null)
                    PJUtils.ShowMsg("Cập nhật thiết lập thành công.", true, Page);
            }
        }
    }
}