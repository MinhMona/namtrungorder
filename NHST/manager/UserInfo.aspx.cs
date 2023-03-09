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
    public partial class UserInfo : System.Web.UI.Page
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
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                    else
                    {
                        if (ac.RoleID == 0 || ac.RoleID == 2)
                        {
                            btncreateuser.Visible = true;
                        }
                        else
                        {
                            btncreateuser.Visible = false;
                        }
                    }

                }
                loadPrefix();
                LoadSaler();
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
            var Levels = UserLevelController.GetAll("");
            if (Levels.Count > 0)
            {
                ddlLevelID.DataSource = Levels;
                ddlLevelID.DataBind();
            }
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
        public void LoadSaler()
        {
            var salers = AccountController.GetAllByRoleID(6);
            ddlSale.Items.Clear();
            ddlSale.Items.Insert(0, "Chọn nhân viên kinh doanh");
            if (salers.Count > 0)
            {
                ddlSale.DataSource = salers;
                ddlSale.DataBind();
            }
            var dathangs = AccountController.GetAllByRoleID(3);
            ddlDathang.Items.Clear();
            ddlDathang.Items.Insert(0, "Chọn nhân viên đặt hàng");
            if (dathangs.Count > 0)
            {
                ddlDathang.DataSource = dathangs;
                ddlDathang.DataBind();
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                if (ac.RoleID == 0)
                {
                    pnAdmin.Visible = true;
                }
                else
                {
                    if (ac.RoleID == 6)
                    {
                        ltrBackButtonSaler.Text = "<a href=\"/manager/saler-customer-list.aspx\" class=\"btn primary-btn\">Trở về</a>";
                    }
                    else if (ac.ID != id)
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                ViewState["UID"] = id;
                var a = AccountController.GetByID(id);
                if (a != null)
                {
                    lblTradeHistory.Text = "<a href=\"/manager/trade-history.aspx?ID=" + id + "\" target=\"_blank\">Lịch sử giao dịch</a>";
                    lblUsername.Text = a.Username;
                    lblEmail.Text = a.Email;

                    if (ac.ID != a.SaleID && ac.RoleID == 6)
                    {
                        Response.Redirect("/manager/saler-customer-list.aspx");
                    }

                    var ai = AccountInfoController.GetByUserID(id);
                    if (ai != null)
                    {
                        txtFirstName.Text = ai.FirstName;
                        txtLastName.Text = ai.LastName;
                        //ddlPrefix.SelectedValue = ai.MobilePhonePrefix;
                        txtPhone.Text = ai.Phone;
                        txtAddress.Text = ai.Address;
                        //txtEmail.Text = ai.Email;
                        if (ai.BirthDay != null)
                            rBirthday.SelectedDate = ai.BirthDay;
                        if (ai.Gender != null)
                            ddlGender.SelectedValue = ai.Gender.ToString();
                    }
                    ddlRole.SelectedValue = a.RoleID.ToString();
                    ddlDepartment.SelectedValue = a.Department.ToString();
                    ddlSiteType.SelectedValue = a.SiteType.ToString();
                    ddlStatus.SelectedValue = a.Status.ToString();
                    ddlLevelID.SelectedValue = a.LevelID.ToString();
                    ddlVipLevel.SelectedValue = a.VIPLevel.ToString();
                    ddlSale.SelectedValue = a.SaleID.ToString();
                    ddlDathang.SelectedValue = a.DathangID.ToString();
                    if (a.WarehouseTo != null)
                    {
                        ddlWarehouseTo.SelectedValue = a.WarehouseTo.ToString();
                    }
                    if (!string.IsNullOrEmpty(a.Currency))
                        txtCurrency.Text = a.Currency.ToString();
                    else
                        txtCurrency.Text = "";

                    if (!string.IsNullOrEmpty(a.FeeBuyPro))
                        txtFeebuypro.Text = a.FeeBuyPro;
                    else
                        txtFeebuypro.Text = "";

                    if (!string.IsNullOrEmpty(a.FeeTQVNPerWeight))
                        txtFeeWeight.Text = a.FeeTQVNPerWeight;
                    else
                        txtFeeWeight.Text = "";

                    if (a.MaxOrderPrice != null)
                        txtMaxOrderPrice.Value =Convert.ToDouble( a.MaxOrderPrice);
                    else
                        txtMaxOrderPrice.Value = 0;

                    if (a.NumberOrder != null)
                        txtNumberOrder.Text = a.NumberOrder.ToString();
                    else
                        txtNumberOrder.Text = "0";

                    if (a.NumberTake != null)
                        txtNumberTake.Text = a.NumberTake.ToString();
                    else
                        txtNumberTake.Text = "0";

                    if (a.Deposit != null)
                        rDeposit.Value = Convert.ToDouble(a.Deposit);
                    else
                        rDeposit.Value = 0;
                }
                else
                {
                    Response.Redirect("/Manager/Home.aspx");
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int UID = ViewState["UID"].ToString().ToInt(0);
            string pass = txtpass.Text.Trim();
            int Status = ddlStatus.SelectedValue.ToString().ToInt();
            int RoleID = ddlRole.SelectedValue.ToString().ToInt();
            int Department = ddlDepartment.SelectedValue.ToString().ToInt();
            int SiteType = ddlSiteType.SelectedValue.ToString().ToInt();
            int LevelID = ddlLevelID.SelectedValue.ToString().ToInt();
            int SaleID = ddlSale.SelectedValue.ToString().ToInt(0);
            int VIPLevel = ddlVipLevel.SelectedValue.ToString().ToInt(1);
            int DathangID = ddlDathang.SelectedValue.ToString().ToInt(0);
            double MaxOrderPrice = Convert.ToDouble( txtMaxOrderPrice.Text);
            int NumberOrder =Convert.ToInt32( txtNumberOrder.Text);
            int NumberTake =Convert.ToInt32(txtNumberTake.Text);
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            if (!string.IsNullOrEmpty(pass))
            {
                string confirmpass = txtconfirmpass.Text;
                if (!string.IsNullOrEmpty(confirmpass))
                {
                    if (confirmpass == pass)
                    {
                        AccountController.updateLevelID(UID, LevelID, currentDate, username_current);
                        //AccountController.updateVipLevel(UID, VIPLevel, currentDate, username_current);
                        AccountController.updatestatus(UID, Status, currentDate, username_current);
                        AccountController.UpdateRole(UID, RoleID, currentDate, username_current);
                        AccountController.UpdateDepartment(UID, Department, currentDate, username_current);
                        AccountController.UpdateNumberOrder(UID, NumberOrder, currentDate, username_current);
                        AccountController.UpdateNumberTake(UID, NumberTake, currentDate, username_current);
                        AccountController.UpdateMaxOrderPrice(UID, MaxOrderPrice, currentDate, username_current);
                        AccountController.UpdateSiteType(UID, SiteType, currentDate, username_current);
                        AccountController.UpdateSaleID(UID, SaleID, currentDate, username_current);
                        AccountController.UpdateDathangID(UID, DathangID, currentDate, username_current);
                        AccountController.UpdateWarehouseTo(UID, Convert.ToInt32(ddlWarehouseTo.SelectedValue));
                        AccountController.UpdateFee(UID, txtCurrency.Text, txtFeebuypro.Text, txtFeeWeight.Text);
                        string rp = AccountController.UpdatePassword(UID, pass);

                        string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), lblEmail.Text, txtPhone.Text,
                            txtAddress.Text.Trim(), Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), "", "", currentDate,
                            username_current);
                        if (r == "1" && rp == "1")
                        {
                            PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                        }
                        else if (r == "1" && rp == "0")
                        {
                            lblConfirmpass.Text = "Mật khẩu mới trùng với mật khẩu cũ.";
                            lblConfirmpass.Visible = true;
                        }
                        else
                        {
                            PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
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
                AccountController.updateLevelID(UID, LevelID, currentDate, username_current);
                //AccountController.updateVipLevel(UID, VIPLevel, currentDate, username_current);
                AccountController.updatestatus(UID, Status, currentDate, username_current);
                AccountController.UpdateRole(UID, RoleID, currentDate, username_current);
                AccountController.UpdateDepartment(UID, Department, currentDate, username_current);
                AccountController.UpdateSiteType(UID, SiteType, currentDate, username_current);
                AccountController.UpdateNumberOrder(UID, NumberOrder, currentDate, username_current);
                AccountController.UpdateNumberTake(UID, NumberTake, currentDate, username_current);
                AccountController.UpdateMaxOrderPrice(UID, MaxOrderPrice, currentDate, username_current);
                AccountController.UpdateSaleID(UID, SaleID, currentDate, username_current);
                AccountController.UpdateDathangID(UID, DathangID, currentDate, username_current);
                AccountController.UpdateWarehouseTo(UID, Convert.ToInt32(ddlWarehouseTo.SelectedValue));
                AccountController.UpdateFee(UID, txtCurrency.Text, txtFeebuypro.Text, txtFeeWeight.Text);
                string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), lblEmail.Text, txtPhone.Text,
                    txtAddress.Text.Trim(), Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), "", "", DateTime.Now,
                    username_current);
                if (r == "1")
                {
                    PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                }
                else
                {
                    PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                }
            }
        }
    }
}