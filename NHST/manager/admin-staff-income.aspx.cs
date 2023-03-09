using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class admin_staff_income : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
            }
        }
        public void LoadData()
        {
            //DateTime currentDate = DateTime.Now;
            //rdateto.SelectedDate = currentDate;
            //rdatefrom.SelectedDate = currentDate;
            var listStaff = AccountController.GetAllByRoleIDAndRoleID(3, 6);
            ddlUsername.Items.Clear();
            ddlUsername.Items.Insert(0, "Chọn nhân viên");
            if (listStaff.Count > 0)
            {
                ddlUsername.DataSource = listStaff;
                ddlUsername.DataBind();
            }
            int UID = Request.QueryString["uid"].ToInt(0);
            int status = Request.QueryString["stt"].ToInt(0);
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];

            ddlUsername.SelectedValue = UID.ToString();
            ddlStatus.SelectedValue = status.ToString();
            if (!string.IsNullOrEmpty(fd))
                rdatefrom.SelectedDate = Convert.ToDateTime(fd);
            if (!string.IsNullOrEmpty(td))
                rdateto.SelectedDate = Convert.ToDateTime(td);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string uid = ddlUsername.SelectedValue;
            string fd = rdatefrom.SelectedDate.ToString();
            string td = rdateto.SelectedDate.ToString();
            string stt = ddlStatus.SelectedValue;
            Response.Redirect("/manager/admin-staff-income.aspx?uid=" + uid + "&stt=" + stt + "&fd=" + fd + "&td=" + td + "");
        }

        //public void LoadGrid()
        //{
        //    var acc = Session["userLoginSystem"].ToString();
        //    #region Thống kê thanh toán
        //    int UID = ddlUsername.SelectedValue.ToInt(0);
        //    int status = ddlStatus.SelectedValue.ToInt(0);
        //    string fd = rdatefrom.SelectedDate.ToString();
        //    string td = rdateto.SelectedDate.ToString();
        //    List<tbl_StaffIncome> history = new List<tbl_StaffIncome>();
        //    if (UID > 0)
        //    {
        //        if (status > 0)
        //        {
        //            if (!string.IsNullOrEmpty(fd))
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByUIDStatusFromDateToDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetByUIDStatusFromDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate));
        //                }
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByUIDStatusToDate(UID, status, Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetByUIDStatus(UID, status);
        //                }
        //            }

        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(fd))
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByUIDFromDateToDate(UID, Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetByUIDFromDate(UID, Convert.ToDateTime(rdatefrom.SelectedDate));
        //                }
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByUIDToDate(UID, Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetByUID(UID);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (status > 0)
        //        {
        //            if (!string.IsNullOrEmpty(fd))
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByUIDStatusFromDateToDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetByUIDStatusFromDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate));
        //                }
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByUIDStatusToDate(UID, status, Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetByUIDStatus(UID, status);
        //                }
        //            }

        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(fd))
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetByFromDate(Convert.ToDateTime(rdatefrom.SelectedDate));
        //                }
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(td))
        //                {
        //                    history = StaffIncomeController.GetByToDate(Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
        //                }
        //                else
        //                {
        //                    history = StaffIncomeController.GetAll("");
        //                }
        //            }
        //        }
        //    }

        //    if (history.Count > 0)
        //    {
        //        double totalpricePay = 0;
        //        double totalpriceNotPay = 0;
        //        foreach (var item in history)
        //        {
        //            double pricereceive = 0;
        //            if (!string.IsNullOrEmpty(item.TotalPriceReceive))
        //                pricereceive = Convert.ToDouble(item.TotalPriceReceive);
        //            if (item.Status == 1)
        //            {
        //                totalpriceNotPay += pricereceive;
        //            }
        //            else
        //            {
        //                totalpricePay += pricereceive;
        //            }
        //        }
        //        gr.DataSource = history;
        //        //gr.DataBind();
        //    }
        //    #endregion
        //}

        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var acc = Session["userLoginSystem"].ToString();
            #region Thống kê thanh toán
            //int UID = ddlUsername.SelectedValue.ToInt(0);
            //int status = ddlStatus.SelectedValue.ToInt(0);
            //string fd = rdatefrom.SelectedDate.ToString();
            //string td = rdateto.SelectedDate.ToString();

            int UID = Request.QueryString["uid"].ToInt(0);
            int status = Request.QueryString["stt"].ToInt(0);
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];

            //ddlUsername.SelectedValue = UID.ToString();
            //ddlStatus.SelectedValue = status.ToString();
            //if (!string.IsNullOrEmpty(fd))
            //    rdatefrom.SelectedDate = Convert.ToDateTime(fd);
            //if (!string.IsNullOrEmpty(td))
            //    rdateto.SelectedDate = Convert.ToDateTime(td);

            List<tbl_StaffIncome> history = new List<tbl_StaffIncome>();
            if (UID > 0)
            {
                if (status > 0)
                {
                    if (!string.IsNullOrEmpty(fd))
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByUIDStatusFromDateToDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetByUIDStatusFromDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByUIDStatusToDate(UID, status, Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetByUIDStatus(UID, status);
                        }
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(fd))
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByUIDFromDateToDate(UID, Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetByUIDFromDate(UID, Convert.ToDateTime(rdatefrom.SelectedDate));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByUIDToDate(UID, Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetByUID(UID);
                        }
                    }
                }
            }
            else
            {
                if (status > 0)
                {
                    if (!string.IsNullOrEmpty(fd))
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByUIDStatusFromDateToDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetByUIDStatusFromDate(UID, status, Convert.ToDateTime(rdatefrom.SelectedDate));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByUIDStatusToDate(UID, status, Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetByUIDStatus(UID, status);
                        }
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(fd))
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetByFromDate(Convert.ToDateTime(rdatefrom.SelectedDate));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            history = StaffIncomeController.GetByToDate(Convert.ToDateTime(rdateto.SelectedDate).AddDays(1));
                        }
                        else
                        {
                            history = StaffIncomeController.GetAll("");
                        }
                    }
                }
            }

            if (history.Count > 0)
            {
                double totalpricePay = 0;
                double totalpriceNotPay = 0;
                foreach (var item in history)
                {
                    double pricereceive = 0;
                    if (!string.IsNullOrEmpty(item.TotalPriceReceive))
                        pricereceive = Convert.ToDouble(item.TotalPriceReceive);
                    if (item.Status == 1)
                    {
                        totalpriceNotPay += pricereceive;
                    }
                    else
                    {
                        totalpricePay += pricereceive;
                    }
                }
                lblNotPay.Text = string.Format("{0:N0}", totalpriceNotPay);
                lblPay.Text = string.Format("{0:N0}", totalpricePay);
                gr.DataSource = history.Where(h=> h.TotalPriceReceive.ToFloat() > 0).ToList();
                //gr.DataBind();
            }
            #endregion
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            //LoadGrid();
            gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            //LoadGrid();
            gr.Rebind();
        }
        #endregion

        [WebMethod]
        public static string UpdateStatus(int ID)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var s = StaffIncomeController.GetByID(ID);
            if (s != null)
            {
                string kq = StaffIncomeController.UpdateStatus(s.ID, 2, DateTime.Now, username);
                return kq;
            }
            return "0";
        }
    }
}