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
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class Package_Detail : System.Web.UI.Page
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
                        //if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 8)
                        //    Response.Redirect("/trang-chu");
                        //else
                        //    LoadData();                        
                        LoadData();
                    }
                }
            }
        }
        public void LoadData()
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                int roleID = ac.RoleID.ToString().ToInt();
                int i = Request.QueryString["ID"].ToInt(0);
                if (i > 0)
                {
                    ViewState["ID"] = i;
                    var p = BigPackageController.GetByID(i);
                    if (p != null)
                    {
                        int status = p.Status.ToString().ToInt();
                        if (roleID == 0)
                        {
                            //txtPackageCode.Enabled = true;
                            pWeight.Enabled = true;
                            pVolume.Enabled = true;
                        }

                        txtPackageCode.Text = p.PackageCode;
                        pWeight.Value = p.Weight;
                        pVolume.Value = p.Volume;
                        ddlStatus.SelectedValue = p.Status.ToString();
                        if (status < 1)
                        {
                            ltrCreateSmallpackage.Text = "<a href=\"/manager/Add-smallpackage.aspx?ID=" + p.ID + "\"  class=\"btn primary-btn\">Tạo mã vận đơn</a>";
                        }
                        //var listot = SmallPackageController.GetBuyBigPackageID(i,"");
                        //var listnot = SmallPackageController.GetAllWithoutAddtoBigpacage();
                        //List<tbl_SmallPackage> ss = new List<tbl_SmallPackage>();
                        //foreach (var item in listnot)
                        //{
                        //    ss.Add(item);
                        //}

                        //ss = ss.OrderByDescending(sm => sm.ID).ToList();
                        //RadComboBox1.DataValueField = "ID";
                        //RadComboBox1.DataTextField = "OrderTransactionCode";
                        //RadComboBox1.DataSource = ss;
                        //RadComboBox1.DataBind();
                        if (roleID != 0 && roleID != 4 && roleID != 5)
                        {
                            btncreateuser.Enabled = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["ID"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/Add-Package.aspx";
            if (ID > 0)
            {
                var p = BigPackageController.GetByID(ID);
                if (p != null)
                {
                    string current_code = p.PackageCode;
                    double current_weight = p.Weight.ToString().ToFloat(0);
                    double current_volume = p.Volume.ToString().ToFloat(0);
                    int current_status = p.Status.ToString().ToInt(1);

                    string new_code = txtPackageCode.Text.Trim();
                    double new_weight = pWeight.Value.ToString().ToFloat(0);
                    double new_volume = pVolume.Value.ToString().ToFloat(0);
                    int new_status = ddlStatus.SelectedValue.ToString().ToInt(1);

                    string mavando = txtMavandon.Text.Trim();
                    if (!string.IsNullOrEmpty(mavando))
                    {
                        string[] listmavan = mavando.Split(';');
                        for (int i = 0; i < listmavan.Length - 1; i++)
                        {
                            string ma = listmavan[i];
                            var code = SmallPackageController.CheckCodeExist(ma);
                            if (code.Count > 0)
                            {
                                foreach (var c in code)
                                {
                                    if (c.Status > 1)
                                        SmallPackageController.UpdateBigPackageID(c.ID, p.ID);
                                }
                            }
                        }
                    }

                    //var cs = RadComboBox1.CheckedItems;
                    //if (cs.Count > 0)
                    //{
                    //    foreach (var item in cs)
                    //    {
                    //        SmallPackageController.UpdateBigPackageID(item.Value.ToInt(), p.ID);
                    //    }
                    //}

                    //Update bao hàng 
                    BigPackageController.Update(ID, new_code, new_weight, new_volume, new_status, currentDate, username_current);
                    if (new_status == 2)
                    {
                        var smlpac = SmallPackageController.GetBuyBigPackageID(ID, "");
                        if (smlpac.Count > 0)
                        {
                            foreach (var item in smlpac)
                            {
                                if (item.Status == 2)
                                    SmallPackageController.UpdateStatus(Convert.ToInt32(item.ID), 3, currentDate, username_current);
                            }
                        }
                        var smlpacupdate = SmallPackageController.GetBuyBigPackageID(ID, "");
                        if (smlpacupdate.Count > 0)
                        {
                            foreach (var sm in smlpacupdate)
                            {
                                int MainorderID = 0;
                                if (sm.MainOrderID != null)
                                {
                                    MainorderID = Convert.ToInt32(sm.MainOrderID);
                                }
                                if (MainorderID > 0)
                                {
                                    bool isVN = true;
                                    var m = MainOrderController.GetAllByID(MainorderID);
                                    if (m != null)
                                    {
                                        if (m.Status < 7)
                                        {
                                            var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
                                            if (smallpackages.Count > 0)
                                            {
                                                foreach (var s in smallpackages)
                                                {
                                                    if (s.Status != 3)
                                                    {
                                                        isVN = false;
                                                    }
                                                }
                                            }
                                            if (isVN == true)
                                            {
                                                MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(m.UID), 7);
                                                MainOrderController.UpdateVNWarehouseDate(MainorderID, Convert.ToInt32(m.UID), currentDate);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Kiểm tra update History
                    if (current_code != new_code)
                    {
                        BigPackageHistoryController.Insert(ID, "PackageCode", current_code, new_code, 1, currentDate, username_current);
                    }
                    if (current_weight != new_weight)
                    {
                        BigPackageHistoryController.Insert(ID, "Weight", current_weight.ToString(), new_weight.ToString(), 1, currentDate, username_current);
                    }
                    if (current_volume != new_volume)
                    {
                        BigPackageHistoryController.Insert(ID, "Volume", current_volume.ToString(), new_volume.ToString(), 1, currentDate, username_current);
                    }
                    if (current_status != new_status)
                    {
                        BigPackageHistoryController.Insert(ID, "Status", current_status.ToString(), new_status.ToString(), 1, currentDate, username_current);
                    }
                    //PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật bao hàng thành công.", "s", true, BackLink, Page);
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật bao hàng thành công.", "s", true, Page);
                }
            }
        }

        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int i = Request.QueryString["ID"].ToInt(0);
            var la = SmallPackageController.GetBuyBigPackageID(i, tSearchName.Text);
            if (la != null)
            {
                if (la.Count > 0)
                {
                    List<smpacka> sps = new List<smpacka>();
                    int stt = 1;
                    foreach (var item in la)
                    {
                        smpacka sp = new smpacka();
                        sp.ID = item.ID;
                        sp.STT = stt;
                        var big = BigPackageController.GetByID(Convert.ToInt32(item.BigPackageID));
                        if (big != null)
                        {
                            sp.PackageCode = big.PackageCode;
                        }
                        sp.OrderTransactionCode = item.OrderTransactionCode;
                        sp.ProductType = item.ProductType;
                        sp.FeeShip = item.FeeShip.ToString();
                        sp.Weight = item.Weight.ToString();
                        sp.Volume = item.Volume.ToString();
                        sp.Status = Convert.ToInt32(item.Status);
                        sp.Statusname = PJUtils.IntToStringStatusSmallPackage(Convert.ToInt32(item.Status));
                        sp.CreatedDate = string.Format("{0:dd/MM/yyyy}", item.CreatedDate);
                        sps.Add(sp);
                        stt++;
                    }
                    gr.DataSource = sps;
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
        public class smpacka
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public int BigPackageID { get; set; }
            public string PackageCode { get; set; }
            public string OrderTransactionCode { get; set; }
            public string ProductType { get; set; }
            public string FeeShip { get; set; }
            public string Weight { get; set; }
            public string Volume { get; set; }
            public int Status { get; set; }
            public string Statusname { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}