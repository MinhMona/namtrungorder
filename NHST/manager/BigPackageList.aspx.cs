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
using MB.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace NHST.manager
{
    public partial class BigPackageList : System.Web.UI.Page
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
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");

                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            if (Request.QueryString["code"] != null)
            {
                string code = Request.QueryString["code"];
                var b1 = BigPackageController1.GetByPackageCode(code);
                if (b1 != null)
                {
                    double totalweight = 0;
                    tSearchName.Text = code;
                    List<Danhsachpackagelon> bs = new List<Danhsachpackagelon>();
                    string dhtranghthainhanhcham = "<span class=\"bg-red\">Đi bay</span>";
                    Danhsachpackagelon l = new Danhsachpackagelon();
                    l.ID = b1.ID;
                    l.packagecode = b1.PackageCode;
                    l.SendDate = string.Format("{0:dd/MM/yyyy}", b1.SendDate);
                    l.Status = PJUtils.ReturnStatusBigpackage(Convert.ToInt32(b1.Status));
                    if (b1.IsSlow == true)
                        dhtranghthainhanhcham = "<span class=\"bg-blue\">Đi tàu</span>";
                    List<Danhsachpackagenho> ns = new List<Danhsachpackagenho>();
                    var sps = SmallPackageController1.GetAllByBigPackageID(b1.ID);
                    if (sps.Count > 0)
                    {
                        double weighthn = 0;
                        double weightsg = 0;
                        double pricehn = 0;
                        double pricesg = 0;
                        double total = 0;
                        foreach (var s in sps)
                        {
                            Danhsachpackagenho n = new Danhsachpackagenho();
                            n.packagecode = s.PackageCode;
                            n.userphone = s.UserPhone;
                            n.weight = s.Weight.ToString();
                            n.place = PJUtils.ReturnPlace(Convert.ToInt32(s.Place));
                            n.note = s.Note.Replace("\n", "<br/>");
                            n.barcodeIMG = s.BarcodeURL;
                            n.trangthaithanhtoan = PJUtils.ReturnStatusPayment(Convert.ToInt32(s.StatusPayment));
                            n.trangthainhanhang = PJUtils.ReturnStatusReceivePackage(Convert.ToInt32(s.StatusReceivePackage));
                            n.dhTrangthainhanhcham = dhtranghthainhanhcham;
                            ns.Add(n);
                            if (s.Place == 1)
                            {
                                weighthn += Convert.ToDouble(s.Weight);
                            }
                            else
                            {
                                weightsg += Convert.ToDouble(s.Weight);
                            }
                            totalweight += Convert.ToDouble(s.Weight);
                        }
                        //var weightInforHN = WeightController.GetByPlaceWeightFT(weighthn, 1);
                        //var weightInforSG = WeightController.GetByPlaceWeightFT(weightsg, 2);

                        //if (weightInforHN != null)
                        //    pricehn = Convert.ToDouble(weightInforHN.PricePerWeight) * weighthn;
                        //if (weightInforSG != null)
                        //    pricesg = Convert.ToDouble(weightInforSG.PricePerWeight) * weightsg;
                        //total = pricehn + pricesg;
                        //l.TotalPrice = string.Format("{0:N0}", total).Replace(".", ",");
                    }
                    l.pcknho = ns;
                    l.TotalWeight = totalweight;
                    bs.Add(l);
                    pagingall(bs);
                }
                else
                {
                    var bps = BigPackageController1.GetAll();
                    if (bps.Count > 0)
                    {
                        List<Danhsachpackagelon> bs = new List<Danhsachpackagelon>();
                        foreach (var b in bps)
                        {
                            double totalweight = 0;
                            string dhtranghthainhanhcham = "<span class=\"bg-red\">Đi bay</span>";
                            Danhsachpackagelon l = new Danhsachpackagelon();
                            l.ID = b.ID;
                            l.packagecode = b.PackageCode;
                            l.SendDate = string.Format("{0:dd/MM/yyyy}", b.SendDate);
                            l.Status = PJUtils.ReturnStatusBigpackage(Convert.ToInt32(b.Status));
                            if (b.IsSlow == true)
                                dhtranghthainhanhcham = "<span class=\"bg-blue\">Đi tàu</span>";
                            List<Danhsachpackagenho> ns = new List<Danhsachpackagenho>();
                            var sps = SmallPackageController1.GetAllByBigPackageID(b.ID);
                            if (sps.Count > 0)
                            {
                                double weighthn = 0;
                                double weightsg = 0;
                                double pricehn = 0;
                                double pricesg = 0;
                                double total = 0;
                                foreach (var s in sps)
                                {
                                    Danhsachpackagenho n = new Danhsachpackagenho();
                                    n.packagecode = s.PackageCode;
                                    n.userphone = s.UserPhone;
                                    n.weight = s.Weight.ToString();
                                    n.place = PJUtils.ReturnPlace(Convert.ToInt32(s.Place));
                                    n.note = s.Note.Replace("\n", "<br/>");
                                    n.barcodeIMG = s.BarcodeURL;
                                    n.trangthaithanhtoan = PJUtils.ReturnStatusPayment(Convert.ToInt32(s.StatusPayment));
                                    n.trangthainhanhang = PJUtils.ReturnStatusReceivePackage(Convert.ToInt32(s.StatusReceivePackage));
                                    n.dhTrangthainhanhcham = dhtranghthainhanhcham;
                                    ns.Add(n);
                                    if (s.Place == 1)
                                    {
                                        weighthn += Convert.ToDouble(s.Weight);
                                    }
                                    else
                                    {
                                        weightsg += Convert.ToDouble(s.Weight);
                                    }
                                    totalweight += Convert.ToDouble(s.Weight);
                                }
                                //var weightInforHN = WeightController.GetByPlaceWeightFT(weighthn, 1);
                                //var weightInforSG = WeightController.GetByPlaceWeightFT(weightsg, 2);

                                //if (weightInforHN != null)
                                //    pricehn = Convert.ToDouble(weightInforHN.PricePerWeight) * weighthn;
                                //if (weightInforSG != null)
                                //    pricesg = Convert.ToDouble(weightInforSG.PricePerWeight) * weightsg;
                                //total = pricehn + pricesg;
                                //l.TotalPrice = string.Format("{0:N0}", total).Replace(".", ",");
                            }
                            l.pcknho = ns;
                            l.TotalWeight = totalweight;
                            bs.Add(l);
                        }
                        pagingall(bs);
                    }
                }
            }
            else
            {
                var bps = BigPackageController1.GetAll();
                if (bps.Count > 0)
                {
                    List<Danhsachpackagelon> bs = new List<Danhsachpackagelon>();
                    foreach (var b in bps)
                    {
                        double totalweight = 0;
                        string dhtranghthainhanhcham = "<span class=\"bg-red\">Đi bay</span>";
                        Danhsachpackagelon l = new Danhsachpackagelon();
                        l.ID = b.ID;
                        l.packagecode = b.PackageCode;
                        l.SendDate = string.Format("{0:dd/MM/yyyy}", b.SendDate);
                        l.Status = PJUtils.ReturnStatusBigpackage(Convert.ToInt32(b.Status));
                        if (b.IsSlow == true)
                            dhtranghthainhanhcham = "<span class=\"bg-blue\">Đi tàu</span>";
                        List<Danhsachpackagenho> ns = new List<Danhsachpackagenho>();
                        var sps = SmallPackageController1.GetAllByBigPackageID(b.ID);
                        if (sps.Count > 0)
                        {
                            double weighthn = 0;
                            double weightsg = 0;
                            double pricehn = 0;
                            double pricesg = 0;
                            double total = 0;
                            foreach (var s in sps)
                            {
                                Danhsachpackagenho n = new Danhsachpackagenho();
                                n.packagecode = s.PackageCode;
                                n.userphone = s.UserPhone;
                                n.weight = s.Weight.ToString();
                                n.place = PJUtils.ReturnPlace(Convert.ToInt32(s.Place));
                                n.note = s.Note.Replace("\n", "<br/>");
                                n.barcodeIMG = s.BarcodeURL;
                                n.trangthaithanhtoan = PJUtils.ReturnStatusPayment(Convert.ToInt32(s.StatusPayment));
                                n.trangthainhanhang = PJUtils.ReturnStatusReceivePackage(Convert.ToInt32(s.StatusReceivePackage));
                                n.dhTrangthainhanhcham = dhtranghthainhanhcham;
                                ns.Add(n);
                                if (s.Place == 1)
                                {
                                    weighthn += Convert.ToDouble(s.Weight);
                                }
                                else
                                {
                                    weightsg += Convert.ToDouble(s.Weight);
                                }
                                totalweight += Convert.ToDouble(s.Weight);
                            }
                            //var weightInforHN = WeightController.GetByPlaceWeightFT(weighthn, 1);
                            //var weightInforSG = WeightController.GetByPlaceWeightFT(weightsg, 2);

                            //if (weightInforHN != null)
                            //    pricehn = Convert.ToDouble(weightInforHN.PricePerWeight) * weighthn;
                            //if (weightInforSG != null)
                            //    pricesg = Convert.ToDouble(weightInforSG.PricePerWeight) * weightsg;
                            //total = pricehn + pricesg;
                            //l.TotalPrice = string.Format("{0:N0}", total).Replace(".", ",");
                        }
                        l.pcknho = ns;
                        l.TotalWeight = totalweight;
                        bs.Add(l);
                    }
                    pagingall(bs);
                }
            }

        }

        #region Paging
        public void pagingall(List<Danhsachpackagelon> acs)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                var ui = AccountInfoController.GetByUserID(u.ID);
                if (ui != null)
                {
                    //string IMG = ui.IMG;
                    int PageSize = 10;
                    if (acs.Count > 0)
                    {
                        int TotalItems = acs.Count;
                        if (TotalItems % PageSize == 0)
                            PageCount = TotalItems / PageSize;
                        else
                            PageCount = TotalItems / PageSize + 1;

                        Int32 Page = GetIntFromQueryString("Page");

                        if (Page == -1) Page = 1;
                        int FromRow = (Page - 1) * PageSize;
                        int ToRow = Page * PageSize - 1;
                        if (ToRow >= TotalItems)
                            ToRow = TotalItems - 1;

                        for (int i = FromRow; i < ToRow + 1; i++)
                        {
                            var item = acs[i];
                            ltrorderlist.Text += "<tr style=\"background:#ff0\">";
                            ltrorderlist.Text += "  <td>" + item.ID + "</td>";
                            ltrorderlist.Text += "  <td>" + item.packagecode + "</td>";
                            ltrorderlist.Text += "  <td colspan=\"2\">Ngày gửi: " + item.SendDate + "</td>";
                            //ltrorderlist.Text += "  <td>Tổng tiền: " + item.SendDate + "</td>";
                            ltrorderlist.Text += "  <td colspan=\"2\">Kiện đang ở: " + item.Status + "</td>";
                            ltrorderlist.Text += "  <td colspan=\"2\">Tổng trọng lượng: " + item.TotalWeight + " kg</td>";
                            ltrorderlist.Text += "  <td colspan=\"2\"><a class=\"btn primary-btn\" target=\"_blank\" href=\"/Admin/BigPackageDetail.aspx?ID=" + item.ID + "\">Xem</a></td>";
                            ltrorderlist.Text += "</tr>";
                            var sps = item.pcknho;
                            if (sps.Count > 0)
                            {
                                foreach (var s in sps)
                                {
                                    ltrorderlist.Text += "<tr>";
                                    ltrorderlist.Text += "  <td>" + item.packagecode + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.packagecode + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.userphone + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.weight + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.place + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.note + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.dhTrangthainhanhcham + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.trangthainhanhang + "</td>";
                                    ltrorderlist.Text += "  <td>" + s.trangthaithanhtoan + "</td>";
                                    //ltrorderlist.Text += "  <td><a class=\"btn btn-info btn-sm\" target=\"_blank\" href=\""+s.barcodeIMG+"\">Lấy barcode</a></td>";
                                    ltrorderlist.Text += "  <td><a class=\"btn primary-btn\" target=\"_blank\" href=\"/admin/getprintbarcode.aspx?code=" + s.packagecode + "\">Lấy barcode</a></td>";
                                    ltrorderlist.Text += "</tr>";
                                }
                            }
                        }
                    }
                }
            }

        }
        public static Int32 GetIntFromQueryString(String key)
        {
            Int32 returnValue = -1;
            String queryStringValue = HttpContext.Current.Request.QueryString[key];
            try
            {
                if (queryStringValue == null)
                    return returnValue;
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                returnValue = Convert.ToInt32(queryStringValue);
            }
            catch
            { }
            return returnValue;
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {

            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));

        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                pageUrl += "&Page={0}";
            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.Append("<div class=\"paging\">");
            //output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                output.Append("<a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a>");
                output.Append("<a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><</a>");
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Previous</a></li>");
                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                //output.Append("<li class=\"pagerange\"><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {

                    output.Append("<span class=\"current\">" + i.ToString() + "</span>");
                    //output.Append("<li class=\"current-page-item\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
                    //output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a>");
                output.Append("<a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">></a>");
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                output.Append("<a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var sp = SmallPackageController1.GetAll();
            if (sp.Count > 0)
            {
                //gr.DataSource = sp;

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
            Response.Redirect("/manager/bigpackagelist.aspx?code=" + tSearchName.Text + "");
            //gr.Rebind();
        }
        #endregion
        public class Danhsachpackagelon
        {
            public int ID { get; set; }
            public string packagecode { get; set; }
            public string SendDate { get; set; }
            public string Status { get; set; }
            public string TotalPrice { get; set; }
            public double TotalWeight { get; set; }
            public List<Danhsachpackagenho> pcknho { get; set; }
        }
        public class Danhsachpackagenho
        {
            public string packagecode { get; set; }
            public string userphone { get; set; }
            public string weight { get; set; }
            public string place { get; set; }
            public int placenum { get; set; }
            public string note { get; set; }
            public string barcodeIMG { get; set; }
            public string dhTrangthainhanhcham { get; set; }
            public string trangthaithanhtoan { get; set; }
            public string trangthainhanhang { get; set; }
        }

    }
}