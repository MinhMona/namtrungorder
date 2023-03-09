using MB.Extensions;
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
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class report_ordering_staff : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
                //LoadGrid1();
            }
        }
        public void LoadData()
        {
            rdatefrom.SelectedDate = DateTime.Now;
            rdateto.SelectedDate = DateTime.Now.AddDays(30);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<ObjOrder> objs = new List<ObjOrder>();
            var userdathang = AccountController.GetAllByRoleID(3);
            if (userdathang.Count > 0)
            {
                foreach (var u in userdathang)
                {
                    ObjOrder oj = new ObjOrder();
                    double totalink = 0;
                    double totalOrderCancel = 0;
                    var orders = MainOrderController.GetReportByMainOrderIDFT(rdatefrom.SelectedDate.ToString(), rdateto.SelectedDate.ToString(), u.ID);
                    if (orders.Count > 0)
                    {
                        foreach (var o in orders)
                        {
                            totalink += o.orderlinks;
                            if (o.Status == 1)
                            {
                                if (o.paylinks > 0)
                                    totalOrderCancel += 1;
                            }
                        }
                    }
                    oj.UserDatHang = u.Username;
                    oj.totalOrder = string.Format("{0:N0}", orders.Count());
                    oj.totalOrderLink = string.Format("{0:N0}", totalink);
                    oj.totalOrderCancel = string.Format("{0:N0}", totalOrderCancel);
                    objs.Add(oj);
                }
            }
            gr.DataSource = objs;
            gr.DataBind();
        }
        public void LoadGrid()
        {
            List<ObjOrder> objs = new List<ObjOrder>();
            var userdathang = AccountController.GetAllByRoleID(3);
            if (userdathang.Count > 0)
            {
                foreach (var u in userdathang)
                {
                    ObjOrder oj = new ObjOrder();
                    double totalink = 0;
                    double totalOrderCancel = 0;
                    //var orders = MainOrderController.GetFromDateToDateAndDathangID(Convert.ToDateTime(rdatefrom.SelectedDate).ToString(), Convert.ToDateTime(rdateto.SelectedDate).ToString(), u.ID);
                    var orders = MainOrderController.GetReportByMainOrderID(u.ID);
                    if (orders.Count > 0)
                    {
                        foreach (var o in orders)
                        {
                            totalink += o.orderlinks;
                            if (o.Status == 1)
                            {
                                if (o.paylinks > 0)
                                    totalOrderCancel += 1;
                            }
                        }
                    }
                    oj.UserDatHang = u.Username;
                    oj.totalOrder = string.Format("{0:N0}", orders.Count());
                    oj.totalOrderLink = string.Format("{0:N0}", totalink);
                    oj.totalOrderCancel = string.Format("{0:N0}", totalOrderCancel);
                    objs.Add(oj);
                }
            }

            gr.DataSource = objs;
            //gr.DataBind();
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadGrid();
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid();
            //gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            //gr.Rebind();
        }

        public class ObjOrder
        {
            public string UserDatHang { get; set; }
            public string totalOrder { get; set; }
            public string totalOrderLink { get; set; }
            public string totalOrderCancel { get; set; }
        }
        #endregion
    }
}