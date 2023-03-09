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
    public partial class ShippingRequestDetail : System.Web.UI.Page
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
                        if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 5)
                            Response.Redirect("/trang-chu");
                        else
                        {
                            LoadData();
                        }
                    }
                }
            }
        }
        public void LoadData()
        {
            if (Request.QueryString["id"] != null)
            {
                int ID = Request.QueryString["id"].ToInt(0);
                if (ID > 0)
                {
                    var com = MainOrderRequestShipController.GetByID(ID);
                    if (com != null)
                    {
                        ViewState["ID"] = ID;
                        int com_Status = Convert.ToInt32(com.RequestStatus);
                        string username_current = Session["userLoginSystem"].ToString();
                        txtFullName.Text = com.FullName;
                        txtEmail.Text = com.Email;
                        txtPhone.Text = com.Phone;
                        txtAddress.Text = com.Address;
                        txtNote.Text = com.Note;
                        ltrMainOrderStatus.Text = PJUtils.IntToRequestAdmin(Convert.ToInt32(com.MainOrderStatus));
                        ddlStatus.SelectedValue = com.RequestStatus.ToString();
                        ddlPTTT.SelectedValue = com.PaymentMethod.ToString();
                        ddlPTNH.SelectedValue = com.ShippingMethod.ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ID = ViewState["ID"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var ac = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (ID > 0)
            {
                var com = MainOrderRequestShipController.GetByID(ID);
                if (com != null)
                {
                    var setNoti = SendNotiEmailController.GetByID(10);
                    int status = ddlStatus.SelectedValue.ToInt();
                    MainOrderRequestShipController.Update(ID, txtFullName.Text, txtEmail.Text, txtPhone.Text,
                        txtNote.Text, txtAddress.Text, ddlStatus.SelectedValue.ToInt(0),
                        ddlPTNH.SelectedValue.ToInt(1), ddlPTTT.SelectedValue.ToInt(1),
                        currentDate, username_current);
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                }
            }
        }
    }
}