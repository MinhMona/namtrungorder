using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;

namespace NHST.manager
{
    public partial class Add_smallpackage : System.Web.UI.Page
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
                        if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 2)
                            Response.Redirect("/trang-chu");
                        else
                            LoadData();
                    }
                }
            }
        }
        public void LoadData()
        {
            //int BPID = Request.QueryString["ID"].ToInt(0);
            //if (BPID > 0)
            //{
            //    ViewState["BPID"] = BPID;
            //}
            var bp = BigPackageController.GetAll("");
            if (bp.Count > 0)
            {
                ddlPrefix.Items.Clear();
                ddlPrefix.Items.Insert(0, "Chọn bao hàng");
                foreach (var item in bp)
                {
                    ListItem listitem = new ListItem(item.PackageCode, item.ID.ToString());
                    ddlPrefix.Items.Add(listitem);
                }

                ddlPrefix.DataBind();
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            //int BPID = ViewState["BPID"].ToString().ToInt(0);
            int BPID = ddlPrefix.SelectedValue.ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            string kq = SmallPackageController.Insert(BPID, txtOrderTransactionCode.Text.Trim(), txtProductType.Text.Trim(), pShip.Value.ToString().ToFloat(0),
                pWeight.Value.ToString().ToFloat(0), pVolume.Value.ToString().ToFloat(0), 1, currentDate, username_current);

            if (kq.ToInt(0) > 0)
            {
                var allsmall = SmallPackageController.GetBuyBigPackageID(BPID, "");
                if (allsmall.Count > 0)
                {
                    double totalweight = 0;
                    foreach (var item in allsmall)
                    {
                        totalweight += Convert.ToDouble(item.Weight);
                    }
                    BigPackageController.UpdateWeight(BPID, totalweight);
                }
                PJUtils.ShowMessageBoxSwAlert("Tạo mới thành công.", "s", true, Page);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/manager/Order-Transaction-Code.aspx");
        }
    }
}