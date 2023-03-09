using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class report_income_for_saler : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0  && obj_user.RoleID != 6)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                // LoadData();
                //LoadGrid1();
            }

        }
        //public void LoadData()
        //{
        //    rdatefrom.SelectedDate = DateTime.Now;
        //    rdateto.SelectedDate = DateTime.Now.AddDays(30);
        //}
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            //var acc = Session["userLoginSystem"].ToString();
            LoadGrid();
            gr.Rebind();

        }

        public void LoadGrid()
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            int Stt = Convert.ToInt32(ddlTime.SelectedValue);
            int department = Convert.ToInt32(ddlStaff.SelectedValue);
            var IncomSaler = MainOrderController.GetFromDateToDate_IncomSaler(Convert.ToString(rdatefrom.SelectedDate), Convert.ToString(rdateto.SelectedDate), tSearchName.Text.Trim().ToLower(), ac.Username, Stt, department);

            if (IncomSaler.Count > 0)
            {
                double tonggiatridonhang = 0;
                double tongtienhang = 0;
                double tongphidonhang = 0;
                double tongphimuahang = 0;
                double tongvanchuyentq = 0;
                double trongvanchuyennoidia = 0;
                double tongmacca = 0;
                double tongtongcannang = 0;
                double tongsodonhhang = 0;
                foreach (var item in IncomSaler)
                {
                    tonggiatridonhang += Convert.ToDouble(item.giatridonhang);
                    tongtienhang += Convert.ToDouble(item.PriceVND);
                    tongphidonhang += Convert.ToDouble(item.phidonhang);
                    tongphimuahang += Convert.ToDouble(item.FeeBuyPro);
                    tongvanchuyentq += Convert.ToDouble(item.FeeWeight);
                    trongvanchuyennoidia += Convert.ToDouble(item.FeeShipCN);
                    tongmacca += Convert.ToDouble(item.tienmacca);
                    tongtongcannang += Convert.ToDouble(item.TQVNWeight);
                    tongsodonhhang += Convert.ToDouble(item.TotalOrder);
                }
                lbltonggiatridonhang.Text = string.Format("{0:N0}", tonggiatridonhang);
                lbltongtienhang.Text = string.Format("{0:N0}", tongtienhang);
                lbltongphidonhang.Text = string.Format("{0:N0}", tongphidonhang);
                lbltongphimuahang.Text = string.Format("{0:N0}", tongphimuahang);
                lbltongvanchuyenqt.Text = string.Format("{0:N0}", tongvanchuyentq);
                lbltongvanchuyennoidia.Text = string.Format("{0:N0}", trongvanchuyennoidia);
                lbltongmacca.Text = string.Format("{0:N0}", tongmacca);
                lbltongcannang.Text = string.Format("{0:N0}", tongtongcannang);
                lbltongsodonhang.Text = string.Format("{0:N0}", tongsodonhhang);
                gr.DataSource = IncomSaler;

            }
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
            gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            gr.Rebind();
        }
        #endregion

        public class Report_IncomSaler
        {
            public string Username { get; set; }
            public string Saler { get; set; }
            public int SalerID { get; set; }
            public string TotalPriceVND { get; set; }
            public string PriceVND { get; set; }
            public string FeeBuyPro { get; set; }
            public string FeeShipCN { get; set; }
            public string giatridonhang { get; set; }
            public string phidonhang { get; set; }
            public string TotalPriceReal { get; set; }
            public string TQVNWeight { get; set; }
            public string FeeWeight { get; set; }
            public string TotalOrder { get; set; }
        }
    }
}