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

namespace NHST.manager
{
    public partial class Home_Config : System.Web.UI.Page
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
                        Response.Redirect("/trang-chu");
                }
                LoadData();
                loaddataPresent();

            }
        }
        public string html = "";
        public string htmlList = "";
        public void LoadData()
        {
            var categories = MenuController.GetByLevel(0).OrderBy(c => c.Position).ToList();
            if (categories != null)
            {

                html += "<ul>";
                foreach (var c in categories)
                {
                    //html += "<li data-jstree='{ \"opened\" : false}'>" + c.CategoryName + "";
                    html += "<li id=\"" + c.ID + "\" data-jstree='{ \"opened\" : false}'>" + c.MenuName + " - <span class=\"register-link\" onclick=\"editMenu('" + c.ID + "')\">Edit</span> | <span class=\"register-link\" onclick=\"AddChildMenu('" + c.ID + "')\">Add Child</span>";
                    Loadtree(c.ID);
                    html += "</li>";
                }
                html += "</ul>";
                ltrTree.Text = html;
            }
        }

        public void Loadtree(int UID)
        {
            var categories = MenuController.GetByLevel(UID);
            if (categories != null)
            {
                html += "<ul>";
                foreach (var c in categories)
                {
                    var ui = MenuController.GetByID(c.ID);
                    html += "<li id=\"" + c.ID + "\" data-jstree='{ \"opened\" : false}'>" + c.MenuName + " - <span class=\"register-link\" onclick=\"editMenu('" + c.ID + "')\">Edit</span>";
                    Loadtree(c.ID);
                    html += "</li>";
                }
                html += "</ul>";
            }
        }

        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = BannerController.GetAllAD();
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string t = hdfTest.Value;
            string[] list = t.Split('*');

            for (int i = 0; i < list.Length - 1; i++)
            {
                string[] value = list[i].Split('-');
                var root = MenuController.UpdateIndex(value[0].ToInt(), i, 0);
                if (root != null)
                {
                    if (!string.IsNullOrEmpty(value[1]))
                    {
                        string[] vl1 = value[1].Split('|');
                        for (int j = 0; j < vl1.Length - 1; j++)
                        {
                            var child = MenuController.UpdateIndex(vl1[j].ToInt(), j, root.ID);
                        }
                    }
                }

            }

            string Backlink = "/manager/CategoryMenuList.aspx";
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, Backlink, Page);
        }

        public void loaddataPresent()
        {
            var c = PresentController.GetByTop1();
            if (c != null)
            {
                pYear.Value = c.Experience;
                pQuantityCustomer.Value = c.QuantityCustomer;
                pQuantityOrder.Value = c.QuantityOrder;
            }

            var ft = FooterController.GetByTop1();
            if (ft != null)
            {
                txtFacebook.Text = ft.LinkFanpage;

                //rFooterTrai.Content = c.FooterLeft;
                rFooterTrai.Content = ft.FooterHTML;
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var ft = FooterController.GetByTop1();
            if (ft != null)
            {
                //var content = hdfContent.Value;
                var kq = FooterController.Update(ft.ID, rFooterTrai.Content, txtFacebook.Text);
            }
            var c = PresentController.GetByTop1();
            if (c != null)
            {
                var kq = PresentController.Update(c.ID, Convert.ToInt32(pYear.Value), Convert.ToInt32(pQuantityCustomer.Value), Convert.ToInt32(pQuantityOrder.Value));
                if (kq != null)
                    PJUtils.ShowMsg("Cập nhật thiết lập thành công.", true, Page);
            }
        }

        protected void S_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = StepController.GetAll("");
            if (la != null)
            {
                if (la.Count > 0)
                {
                    S.DataSource = la;
                }
            }
        }

        protected void S_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;
        }

        protected void S_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = hdfID.Value.ToString().ToInt();
            var b = StepController.GetByID(ID);
            if (b != null)
            {
                StepController.Delete(ID);
                PJUtils.ShowMessageBoxSwAlert("Xóa thành công", "s", true, Page);
            }
        }

        protected void SV_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = ServiceController.GetAllAD();
            if (la != null)
            {
                if (la.Count > 0)
                {
                    SV.DataSource = la;
                }
            }
        }

        protected void SV_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;
        }

        protected void SV_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void C_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = CustomerBenefitsController.GetAllAD("");
            if (la != null)
            {
                if (la.Count > 0)
                {
                    C.DataSource = la;
                }
            }
        }

        protected void C_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;
        }

        protected void C_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
    }
}