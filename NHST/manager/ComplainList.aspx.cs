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
using System.Data;
using System.Text;
using System.Web.Services;

namespace NHST.manager
{
    public partial class ComplainList : System.Web.UI.Page
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
                    {
                        btnExcel.Visible = false;
                    }
                    if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 3 && ac.RoleID != 6)
                        Response.Redirect("/trang-chu");
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int status = Convert.ToInt32(ddlStatus.SelectedValue);
            int type = Convert.ToInt32(ddlType.SelectedValue);
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            var la = ComplainController.GetAllWithStatus_SqlHelper(tSearchName.Text.Trim().ToLower(), type, status, ac.RoleID.Value, ac.ID);
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
                }
            }
            //if (ac.RoleID == 0 || ac.RoleID == 2)
            //{
            //    if (status >= 0)
            //    {
            //        var la = ComplainController.GetAllWithStatus_SqlHelper(tSearchName.Text.Trim().ToLower(), type, status);
            //        if (la != null)
            //        {
            //            if (la.Count > 0)
            //            {
            //                gr.DataSource = la;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var la = ComplainController.GetAll_SqlHelper(tSearchName.Text.Trim().ToLower(), type);
            //        if (la != null)
            //        {
            //            if (la.Count > 0)
            //            {
            //                gr.DataSource = la;
            //            }
            //        }
            //    }

            //}
            //else
            //{
            //    List<tbl_Complain> lc = new List<tbl_Complain>();
            //    if (status >= 0)
            //    {
            //        var la = ComplainController.GetAllWithStatus_SqlHelper(tSearchName.Text.Trim().ToLower(), type, status);
            //        if (la != null)
            //        {
            //            foreach (var item in la)
            //            {
            //                int OrderID = Convert.ToInt32(item.OrderID);
            //                var o = MainOrderController.GetAllByID(OrderID);
            //                if (o != null)
            //                {
            //                    if (o.DathangID == ac.ID)
            //                    {
            //                        lc.Add(item);
            //                    }
            //                    else if (o.SalerID == ac.ID)
            //                    {
            //                        lc.Add(item);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var la = ComplainController.GetAll_SqlHelper(tSearchName.Text.Trim().ToLower(), type);
            //        if (la != null)
            //        {
            //            foreach (var item in la)
            //            {
            //                int OrderID = Convert.ToInt32(item.OrderID);
            //                var o = MainOrderController.GetAllByID(OrderID);
            //                if (o != null)
            //                {
            //                    if (o.DathangID == ac.ID)
            //                    {
            //                        lc.Add(item);
            //                    }
            //                    else if (o.SalerID == ac.ID)
            //                    {
            //                        lc.Add(item);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    if (lc.Count > 0)
            //    {
            //        gr.DataSource = lc;
            //    }
            //}

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

        [WebMethod]
        public static string UpdateEmployeeNote(int ID, string EmployeeNote)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                var p = ComplainController.GetByID(ID);
                if (p != null)
                {
                    ComplainController.UpdateEmployeeNote(p.ID, EmployeeNote);
                    return "ok";
                }
            }
            return null;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            int status = Convert.ToInt32(ddlStatus.SelectedValue);
            int type = Convert.ToInt32(ddlType.SelectedValue);
            string username_current = Session["userLoginSystem"].ToString();
            var ac = AccountController.GetByUsername(username_current);

            if (ac.RoleID == 0)
            {
                var la = new List<tbl_Complain>();
                if (status >= 0)
                {
                    la = ComplainController.GetAllWithStatus_SqlHelper(tSearchName.Text.Trim().ToLower(), type, status, ac.RoleID.Value, ac.ID);

                }
                else
                {
                    la = ComplainController.GetAll_SqlHelper(tSearchName.Text.Trim().ToLower(), type);

                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ID</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số tiền</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Nội dung</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ghi chú khách hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ghi chú nội bộ</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Người tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in la)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.ID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.CreatedBy + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", item.Amount) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.ComplainText + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + PJUtils.ReturnStatusComplainRequest(Convert.ToInt32(item.Status)) + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.UserNote + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.EmployeeNote + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "khieu-nai.xls";
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
    }
}