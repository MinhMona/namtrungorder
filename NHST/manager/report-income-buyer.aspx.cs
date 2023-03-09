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
    public partial class report_income_buyer : System.Web.UI.Page
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
               //  LoadData();
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

            var IncomeBuyer = MainOrderController.GetFromDateToDate_IncomeBuyer(Convert.ToString(rdatefrom.SelectedDate), Convert.ToString(rdateto.SelectedDate), txtUsername.Text.Trim().ToLower());

            if (IncomeBuyer.Count > 0)
            {
                
                List<Data> lt = new List<Data>();
                foreach (var item in IncomeBuyer)
                {
                    Data d = new Data();
                    StringBuilder Taobao = new StringBuilder();
                    StringBuilder Tmall = new StringBuilder();
                    StringBuilder t1688 = new StringBuilder();
                    StringBuilder total = new StringBuilder();

                    string tongtienmaccataobao = string.Format("{0:N0}", Convert.ToDouble(item.MacCaTaobao));
                    string tongtientrenwebtaobao = string.Format("{0:N0}", Convert.ToDouble(item.PriceVNDTaobao));
                    string tongphishiptqtaobao = string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCNTaobao));
                    double tongtyledamphantaobao = (Convert.ToDouble(tongtienmaccataobao) / (Convert.ToDouble(tongtientrenwebtaobao) + Convert.ToDouble(tongphishiptqtaobao))) * 100;

                    Taobao.Append("<p class=\"report\" >Số lượng : " + string.Format("{0:N0}", Convert.ToDouble(item.TotalOrderTaobao))  + " ĐƠN </p>");
                    Taobao.Append("<p class=\"report\" >Tiền hàng trên web : " + string.Format("{0:N0}", Convert.ToDouble(item.PriceVNDTaobao)) + " VNĐ</p>");
                    Taobao.Append("<p class=\"report\">Tiền mặc cả : " + string.Format("{0:N0}", Convert.ToDouble(item.MacCaTaobao)) + " VNĐ</p>");
                    Taobao.Append("<p class=\"report\">Phí ship TQ : " + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCNTaobao)) + " VNĐ</p>");
                    Taobao.Append("<p class=\"report\">Đàm phán thành công : " + string.Format("{0:N0}", Convert.ToDouble(item.dondamphanTaobao)) + " ĐƠN</p>");
                    Taobao.Append("<p class=\"report\">Tỷ lệ đàm phán TC : " + string.Format("{0:N0}", Convert.ToDouble(item.tyledamphanTCTaobao)) + " % </p>");
                    if (tongtyledamphantaobao > 0)
                    {
                        Taobao.Append("<p class=\"report\">Tỷ lệ đàm phán : " + Math.Round(tongtyledamphantaobao, 2) + " % </p>");
                    }
                    else
                    {
                        Taobao.Append("<p class=\"report\">Tỷ lệ đàm phán : 0 % </p>");
                    }


                    string tongtienmaccatmall = string.Format("{0:N0}", Convert.ToDouble(item.MacCaTmall));
                    string tongtientrenwebtmall = string.Format("{0:N0}", Convert.ToDouble(item.PriceVNDTmall));
                    string tongphishiptqtmall = string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCNTmall));
                    double tongtyledamphantmall = (Convert.ToDouble(tongtienmaccatmall) / (Convert.ToDouble(tongtientrenwebtmall) + Convert.ToDouble(tongphishiptqtmall))) * 100;


                    Tmall.Append("<p class=\"report\">Số lượng : " + string.Format("{0:N0}", Convert.ToDouble(item.TotalOrderTmall)) + " ĐƠN </p>");
                    Tmall.Append("<p class=\"report\">Tiền hàng trên web : " + string.Format("{0:N0}", Convert.ToDouble(item.PriceVNDTmall)) + " VNĐ</p>");
                    Tmall.Append("<p class=\"report\">Tiền mặc cả : " + string.Format("{0:N0}", Convert.ToDouble(item.MacCaTmall)) + " VNĐ</p>");
                    Tmall.Append("<p class=\"report\">Phí ship TQ: " + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCNTmall)) + " VNĐ</p>");
                    Tmall.Append("<p class=\"report\">Đàm phán thành công : " + string.Format("{0:N0}", Convert.ToDouble(item.dondamphanTmall)) + " ĐƠN</p>");
                    Tmall.Append("<p class=\"report\">Tỷ lệ đàm phán TC : " + string.Format("{0:N0}", Convert.ToDouble(item.tyledamphanTCTmall)) + " % </p>");

                    if (tongtyledamphantmall > 0)
                    {
                        Tmall.Append("<p class=\"report\">Tỷ lệ đàm phán : " + Math.Round(tongtyledamphantmall, 2) + " % </p>");
                    }
                    else
                    {
                        Tmall.Append("<p class=\"report\">Tỷ lệ đàm phán : 0 % </p>");
                    }

                    string tongtienmacca1688 = string.Format("{0:N0}", Convert.ToDouble(item.MacCa1688));
                    string tongtientrenweb1688 = string.Format("{0:N0}", Convert.ToDouble(item.PriceVND1688));
                    string tongphishiptq1688 = string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCN1688));
                    double tongtyledamphan1688 = (Convert.ToDouble(tongtienmacca1688) / (Convert.ToDouble(tongtientrenweb1688) + Convert.ToDouble(tongphishiptq1688))) * 100;

                    t1688.Append("<p class=\"report\">Số lượng : " + string.Format("{0:N0}", Convert.ToDouble(item.TotalOrder1688)) + " ĐƠN </p>");
                    t1688.Append("<p class=\"report\">Tiền hàng trên web : " + string.Format("{0:N0}", Convert.ToDouble(item.PriceVND1688)) + " VNĐ</p>");
                    t1688.Append("<p class=\"report\">Tiền mặc cả : " + string.Format("{0:N0}", Convert.ToDouble(item.MacCa1688)) + " VNĐ</p>");
                    t1688.Append("<p class=\"report\">Phí ship TQ : " + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCN1688)) + " VNĐ</p>");
                    t1688.Append("<p class=\"report\">Đàm phán thành công : " + string.Format("{0:N0}", Convert.ToDouble(item.dondamphan1688)) + " ĐƠN </p>");
                    t1688.Append("<p class=\"report\">Tỷ lệ đàm phán TC : " + string.Format("{0:N0}", Convert.ToDouble(item.tyledamphanTC1688)) + " % </p>");

                    if (tongtyledamphan1688 > 0)
                    {
                        t1688.Append("<p class=\"report\">Tỷ lệ đàm phán : " + Math.Round(tongtyledamphan1688, 2) + " % </p>");
                    }
                    else
                    {
                        t1688.Append("<p class=\"report\">Tỷ lệ đàm phán : 0 % </p>");
                    }

                    string tongtienmacca = string.Format("{0:N0}", Convert.ToDouble(item.MacCaTaobao) + Convert.ToDouble(item.MacCaTmall) + Convert.ToDouble(item.MacCa1688));
                    string tongtientrenweb = string.Format("{0:N0}", Convert.ToDouble(item.PriceVNDTaobao) + Convert.ToDouble(item.PriceVNDTmall) + Convert.ToDouble(item.PriceVND1688));
                    string tongphiship = string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCNTaobao) + Convert.ToDouble(item.FeeShipCNTmall) + Convert.ToDouble(item.FeeShipCN1688));
                    double tongtyledamphan = (Convert.ToDouble(tongtienmacca) / (Convert.ToDouble(tongtientrenweb) + Convert.ToDouble(tongphiship))) * 100;

                    total.Append("<p class=\"report\">Tổng số lượng : " + string.Format("{0:N0}", Convert.ToDouble(item.TotalOrderTaobao +  item.TotalOrderTmall + item.TotalOrder1688)) + " ĐƠN </p>");
                    total.Append("<p class=\"report\">Tổng tiền hàng trên web : " + string.Format("{0:N0}", Convert.ToDouble(item.PriceVNDTaobao) + Convert.ToDouble(item.PriceVNDTmall) + Convert.ToDouble(item.PriceVND1688)) + " VNĐ</p>");
                    total.Append("<p class=\"report\">Tổng tiền mặc cả : " + string.Format("{0:N0}", Convert.ToDouble(item.MacCaTaobao) + Convert.ToDouble(item.MacCaTmall) + Convert.ToDouble(item.MacCa1688)) + " VNĐ</p>");
                    total.Append("<p class=\"report\">Tổng phí ship TQ : " + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCNTaobao) + Convert.ToDouble(item.FeeShipCNTmall) + Convert.ToDouble(item.FeeShipCN1688)) + " VNĐ</p>");
                    total.Append("<p class=\"report\">Tổng đàm phán thành công : " + string.Format("{0:N0}", Convert.ToDouble(item.dondamphanTaobao) + Convert.ToDouble(item.dondamphanTmall) + Convert.ToDouble(item.dondamphan1688)) + " ĐƠN </p>");
                    total.Append("<p class=\"report\">Tổng tỷ lệ đàm phán TC : " + string.Format("{0:N0}", (Convert.ToDouble(item.tyledamphanTCTaobao) + Convert.ToDouble(item.tyledamphanTCTmall) + Convert.ToDouble(item.tyledamphanTC1688)) / 3) + " % </p>");
                    if (tongtyledamphan >0)
                    {
                        total.Append("<p class=\"report\">Tổng tỷ lệ đàm phán : " + Math.Round(tongtyledamphan, 2) + " % </p>");
                    }
                    else
                    {
                        total.Append("<p class=\"report\">Tổng tỷ lệ đàm phán : 0 % </p>");
                    }

                    d.Staff = item.Dathang;
                    d.TAOBAO = Taobao.ToString();
                    d.TMALL = Tmall.ToString();
                    d.T1688 = t1688.ToString();
                    d.TOTAL = total.ToString();
                    lt.Add(d);
                }
                if (lt.Count > 0)
                    gr.DataSource = lt;


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




        public class SiteObject
        {
            public int DathangID { get; set; }
            public string Dathang { get; set; }
            public int TotalOrderTaobao { get; set; }
            public int TotalOrderTmall { get; set; }
            public int TotalOrder1688 { get; set; }

            public double PriceVNDTaobao { get; set; }
            public double PriceVNDTmall { get; set; }
            public double PriceVND1688 { get; set; }

            public double MacCaTaobao { get; set; }
            public double MacCaTmall { get; set; }
            public double MacCa1688 { get; set; }
            
            public double FeeShipCNTaobao { get; set; }
            public double FeeShipCNTmall { get; set; }
            public double FeeShipCN1688 { get; set; }

            public double dondamphanTaobao { get; set; }
            public double dondamphanTmall { get; set; }
            public double dondamphan1688 { get; set; }

            public double tyledamphanTCTaobao { get; set; }
            public double tyledamphanTCTmall { get; set; }
            public double tyledamphanTC1688 { get; set; }

            public double tyledamphanTaobao { get; set; }
            public double tyledamphanTmall { get; set; }
            public double tyledamphan1688 { get; set; }
        }



        public class Data
        {
            public string Staff { get; set; }
            public string TOTAL { get; set; }
            public string TAOBAO { get; set; }
            public string TMALL { get; set; }
            public string T1688 { get; set; }
        }
    }
}