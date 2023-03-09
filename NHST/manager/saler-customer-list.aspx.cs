using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using MB.Extensions;

namespace NHST.manager
{
    public partial class saler_customer_list : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 6)
                        Response.Redirect("/trang-chu");
                    else
                    {
                        if (ac.RoleID == 0)
                            btnExcel.Visible = true;
                        else
                            btnExcel.Visible = false;
                    }

                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string search = tSearchName.Text;
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                if (ac.RoleID == 6)
                {
                    var la = AccountController.GetAllBySaleID_View("", ac.ID).Where(u => u.RoleID == 1).ToList();

                    if (la.Count > 0)
                    {
                        List<UserToExcel> us = new List<UserToExcel>();
                        foreach (var item in la)
                        {
                            string username = item.Username;
                            if (PJUtils.RemoveUnicode(username.ToLower()).Contains(PJUtils.RemoveUnicode(search.ToLower())))
                            {
                                int UID = item.ID;
                                UserToExcel u = new UserToExcel();
                                u.ID = item.ID;
                                u.UserName = item.Username;
                                //var ui = AccountInfoController.GetByUserID(UID);
                                //if (ui != null)
                                //{
                                //    u.Ho = ui.FirstName;
                                //    u.Ten = ui.LastName;
                                //    u.Sodt = ui.MobilePhonePrefix + ui.MobilePhone;
                                //}
                                u.Ho = item.FirstName;
                                u.Ten = item.LastName;
                                u.Sodt = item.Phone;
                                u.Status = PJUtils.StatusToRequest(item.Status);
                                u.Role = item.RoleName;
                                u.CreatedDate = string.Format("{0:dd/MM/yyyy hh:mm}", item.CreatedDate);
                                u.RoleID = item.RoleID.ToString().ToInt(1);
                                u.wallet = string.Format("{0:N0}", item.Wallet);
                                u.Saler = item.saler;
                                u.dathang = item.dathang;

                                us.Add(u);
                            }
                        }
                        gr.DataSource = us;
                    }
                }
            }
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion
        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                //var la = AccountController.GetAllOrderDesc("");
                var la = AccountController.GetAll_ViewUserListExcel("").Where(u => u.RoleID == 1).ToList();

                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>ID</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th><strong>Họ</strong></th>");
                StrExport.Append("      <th><strong>Tên</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số ĐT</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Quyền hạn</strong></th>");
                StrExport.Append("      <th><strong>Nhân viên kinh doanh</strong></th>");
                StrExport.Append("      <th><strong>Địa chỉ</strong></th>");
                StrExport.Append("      <th><strong>Email</strong></th>");
                StrExport.Append("      <th><strong>Level</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in la)
                {
                    string firstname = item.FirstName;
                    string lastname = item.LastName;
                    string phone = item.MobilePhonePrefix + item.MobilePhone;
                    string address = item.Address;
                    string email = item.Email;
                    //var ui = AccountInfoController.GetByUserID(item.ID);
                    //if (ui != null)
                    //{
                    //    firstname = ui.FirstName;
                    //    lastname = ui.LastName;
                    //    phone = ui.MobilePhonePrefix + ui.MobilePhone;
                    //    address = ui.Address;
                    //    email = ui.Email;
                    //}
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.ID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + firstname + "</td>");
                    StrExport.Append("      <td>" + lastname + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + phone + "</td>");
                    StrExport.Append("      <td>" + PJUtils.StatusToRequest(item.Status) + "</td>");
                    //StrExport.Append("      <td>" + PJUtils.ReturnRoleName(RoleController.GetByID(Convert.ToInt32(item.RoleID)).RoleName) + "</td>");
                    StrExport.Append("      <td>" + item.RoleName + "</td>");

                    //var acsaler = AccountController.GetByID(Convert.ToInt32(item.SaleID));
                    //if (acsaler != null)
                    //    StrExport.Append("      <td>" + acsaler.Username + "</td>");
                    //else
                    //    StrExport.Append("      <td></td>");

                    StrExport.Append("      <td>" + item.saler + "</td>");

                    StrExport.Append("      <td>" + address + "</td>");
                    StrExport.Append("      <td>" + email + "</td>");
                    //var level = UserLevelController.GetByID(Convert.ToInt32(item.LevelID));
                    //StrExport.Append("      <td>" + level.LevelName + "</td>");
                    StrExport.Append("      <td>" + item.LevelName + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "UserList.xls";
                string strcontentType = "application/vnd.ms-excel";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.ContentType = strcontentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.Write(StrExport.ToString());
                Response.Flush();
                //Response.Close();
                Response.End();
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Bạn không có quyền xuất file excel!", "e", false, Page);
            }

        }
        public class UserToExcel
        {
            public int ID { get; set; }
            public string UserName { get; set; }
            public string Ho { get; set; }
            public string Ten { get; set; }
            public string Sodt { get; set; }
            public string Status { get; set; }
            public string Role { get; set; }
            public int RoleID { get; set; }
            public string Saler { get; set; }
            public string dathang { get; set; }
            public string wallet { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}