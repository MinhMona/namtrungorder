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
    public partial class editshippingtype : System.Web.UI.Page
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
        public void LoadData()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = ShippingTypeToWareHouseController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtShippingTypeName.Text = news.ShippingTypeName;
                    isHidden.Checked = Convert.ToBoolean(news.IsHidden);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(Username);
            if (ac.RoleID == 0)
            {
                int id = ViewState["NID"].ToString().ToInt(0);
                var w = ShippingTypeToWareHouseController.GetByID(id);
                if (w != null)
                {
                    ShippingTypeToWareHouseController.Update(id, txtShippingTypeName.Text, "", isHidden.Checked, 
                        DateTime.Now, Username);
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật loại vận chuyển thành công.", "s", true, Page);
                }
            }
        }
    }
}