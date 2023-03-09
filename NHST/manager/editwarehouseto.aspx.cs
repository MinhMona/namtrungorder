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
    public partial class editwarehouseto : System.Web.UI.Page
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
                var news = WarehouseController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtWareHouseName.Text = news.WareHouseName;
                    txtAddress.Text = news.Address;
                    txtEmail.Text = news.Email;
                    txtPhone.Text = news.Phone;
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
                var w = WarehouseController.GetByID(id);
                if (w != null)
                {
                    WarehouseController.Update(id, txtWareHouseName.Text, 0, txtAddress.Text, txtEmail.Text, txtPhone.Text,
                    "", "", isHidden.Checked, DateTime.Now, Username);
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật kho đến thành công.", "s", true, Page);
                }
            }
        }
    }
}