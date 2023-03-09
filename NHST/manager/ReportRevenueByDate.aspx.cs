using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class ReportRevenueByDate : System.Web.UI.Page
    {
        private readonly NHSTEntities _context = new NHSTEntities();
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
                  

                    loadFilter();
                    LoadData();
                }
            }
        }


        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public int STT { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
            public string dathang { get; set; }
            public string kinhdoanh { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
        }

        

        #region Button status
        protected void btn0_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "0";
            hdfStatus.Value = "0";

            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=0");


            //gr.Rebind();
        }

        protected void btn1_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "1";
            hdfStatus.Value = "1";
            // gr.Rebind();

            var orderType = 1;


            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=1");
        }

        protected void btn2_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "2";
            hdfStatus.Value = "2";
            //gr.Rebind();
            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=2");
        }

        protected void btn5_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "5";
            hdfStatus.Value = "5";
            //gr.Rebind();

            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=5");
        }
        protected void btn11_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "11";
            hdfStatus.Value = "11";
            // gr.Rebind();
            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=11");
        }
        protected void btn12_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "12";
            hdfStatus.Value = "12";
            //gr.Rebind();

            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=12");
        }

        protected void btn13_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "13";
            hdfStatus.Value = "13";
            // gr.Rebind();

            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=13");
        }
        protected void btn6_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "6";
            hdfStatus.Value = "6";
            // gr.Rebind();

            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=6");
        }

        protected void btn7_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "7";
            //gr.Rebind();
            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=7");
        }

        protected void btn9_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "9";
            hdfStatus.Value = "9";
            //gr.Rebind();
            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=9");
        }

        protected void btn10_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "10";
            hdfStatus.Value = "10";
            // gr.Rebind();
            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=10");
        }

        protected void bttnAll_Click(object sender, EventArgs e)
        {

            ddlStatus.SelectedValue = "-1";
            hdfStatus.Value = "-1";
            //gr.Rebind();

            var orderType = 1;

            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }


            Response.Redirect("orderlist2?ot=" + orderType + "&st=-1");
        }
        #endregion

        protected void btnDespi_Click(object sender, EventArgs e)
        {
            int OrderID = hdfOrderID.Value.ToInt(0);
            if (OrderID > 0)
            {
                var mainorder = MainOrderController.GetAllByID(OrderID);
                if (mainorder != null)
                {
                    int UID = Convert.ToInt32(mainorder.UID);
                    var obj_user = AccountController.GetByID(UID);
                    DateTime currentDate = DateTime.Now;
                    if (obj_user != null)
                    {
                        if (obj_user.Wallet > 0)
                        {
                            int OID = OrderID;
                            if (OID > 0)
                            {
                                var o = MainOrderController.GetAllByID(OID);
                                if (o != null)
                                {
                                    double orderdeposited = 0;
                                    if (!string.IsNullOrEmpty(o.Deposit))
                                        orderdeposited = Convert.ToDouble(o.Deposit);
                                    double amountdeposit = Convert.ToDouble(o.AmountDeposit);
                                    double userwallet = Convert.ToDouble(obj_user.Wallet);
                                    if (userwallet > 0)
                                    {
                                        if (userwallet >= amountdeposit)
                                        {
                                            using (NHSTEntities productDbContext = new NHSTEntities())
                                            {
                                                using (var transaction = productDbContext.Database.BeginTransaction())
                                                {
                                                    try
                                                    {
                                                        double wallet = userwallet - amountdeposit;
                                                        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                                        //Cập nhật lại MainOrder
                                                        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                                        MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);
                                                        transaction.Commit();
                                                        PJUtils.ShowMessageBoxSwAlert("Đặt cọc thành công", "s", true, Page);
                                                    }
                                                    catch (Exception exception)
                                                    {
                                                        transaction.Rollback();
                                                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                                    }
                                                }
                                            }
                                            //Cập nhật lại Wallet User

                                        }
                                        else
                                        {
                                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, Page);
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, Page);
                        }
                    }
                }
            }
        }


        public void loadFilter()
        {

            ddlStatus.SelectedValue = "-1";


            var salers = AccountController.GetAllByRoleID(6);


            searchNVKD.Items.Clear();
            searchNVKD.Items.Insert(0, "Chọn nhân viên");

            if (salers.Count > 0)
            {


                searchNVKD.DataSource = salers;
                searchNVKD.DataBind();
            }
            var dathangs = AccountController.GetAllByRoleID(3);


            searchNVDH.Items.Clear();
            searchNVDH.Items.Insert(0, "Chọn nhân viên");


            if (dathangs.Count > 0)
            {


                searchNVDH.DataSource = dathangs;
                searchNVDH.DataBind();
            }
        }


        #region LoadData
        private void LoadData()
        {
            TableSql tb = new TableSql(_context);

            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                string s = tSearchName.Text.Trim();
                if (Request.QueryString["s"] != null)
                {
                    s = Request.QueryString["s"];

                    tSearchName.Text = s;
                }


               


                string fromdate = rFD.SelectedDate.ToString();
                if (!String.IsNullOrEmpty(Request.QueryString["fd"]))
                {
                    fromdate = Request.QueryString["fd"].ToString();

                    rFD.SelectedDate = Convert.ToDateTime(fromdate);
                }


                string todate = rTD.SelectedDate.ToString();
                if (!String.IsNullOrEmpty(Request.QueryString["td"]))
                {
                    todate = Request.QueryString["td"].ToString();

                    rTD.SelectedDate = Convert.ToDateTime(todate);
                }

                

                int page = 0;
                Int32 Page = GetIntFromQueryString("Page");
                if (Page > 0)
                {
                    page = Page - 1;
                }
                

               

                int SalerID = 0;
                if (Request.QueryString["saler"] != null)
                {

                    SalerID = Request.QueryString["saler"].ToInt(1);
                    searchNVKD.SelectedValue = Request.QueryString["saler"];
                }


                int DatHang = 0;
                if (Request.QueryString["dathang"] != null)
                {
                    searchNVDH.SelectedValue = Request.QueryString["dathang"];
                    DatHang = Request.QueryString["dathang"].ToInt(1);
                }

                int status = 0;
                if (Request.QueryString["status"] != null)
                {

                    status = Request.QueryString["status"].ToInt(0);
                    ddlStatus.SelectedValue = Request.QueryString["status"];
                }
                
                int? minDate = 0;
                int? maxDate = 60;

                if(status == 1)
                {
                    minDate = 60;
                    maxDate = 250;
                }

                if (status == 2)
                {
                    minDate = 250;
                    maxDate = null;
                }


                var totalrow = 0;

                var la = tb.LoadlistRevenueByDate(minDate,maxDate, s, fromdate, todate, SalerID, DatHang, 20, page * 20);
                if (la.Count > 0)
                {
                    totalrow = la[0].TotalRow.Value;
                }

                pagingall(la, totalrow);

              
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string s = tSearchName.Text.Trim();
            
           
            string fromdate = rFD.SelectedDate.ToString();
            string todate = rTD.SelectedDate.ToString();
            
            int status = ddlStatus.SelectedValue.ToInt(0);
            int orderType = 1;
            int SalerID = searchNVKD.SelectedValue.ToInt(0);

            int DatHang = searchNVDH.SelectedValue.ToInt(0);


           
            if (Request.QueryString["ot"] != null)
            {
                orderType = Request.QueryString["ot"].ToInt(1);
            }

            if (string.IsNullOrEmpty(s) == true && fromdate == "" && todate == ""  && SalerID == 0 && DatHang == 0 && status == 0)
            {
                Response.Redirect("ReportRevenueByDate?ot=" + orderType);
            }
            else
            {
                Response.Redirect("ReportRevenueByDate?ot=" + orderType + "&s=" + s + "&fd=" + fromdate + "&td=" + todate  + "&saler=" + SalerID + "&dathang=" + DatHang + "&status=" + status +"");
            }
        }

        #endregion

        #region Pagging
        public void pagingall(List<LoadlistRevenueByDate_Result> acs, int total)
        {
            int PageSize = 20;
            if (total > 0)
            {
                int TotalItems = total;
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
                StringBuilder hcm = new StringBuilder();

                double pricepro = 0;

                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];

                    hcm.Append("<tr>");

                  

                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.Username + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VNĐ</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + " VNĐ</td>");


                    hcm.Append("<td>" + item.SalerName + "</td>");

                    hcm.Append("<td>" + item.DepostiDate.Value.ToString("dd/MM/yyyy HH:mm") + "</td>");



                    hcm.Append("<td>" + item.Status + "</td>");

                    hcm.Append("</tr>");
                }
                ltr.Text = hcm.ToString();
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
                if (pageUrl.IndexOf("Page=") > 0)
                {
                    int a = pageUrl.IndexOf("Page=");
                    int b = pageUrl.Length;
                    pageUrl.Remove(a, b - a);
                }
                else
                {
                    pageUrl += "&Page={0}";
                }

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
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<a class=\"prev-page pagi-button\" title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Prev</a>");
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
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<a class=\"pagi-button current-active\">" + i.ToString() + "</a>");
                }
                else
                {
                    output.Append("<a class=\"pagi-button\" href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
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
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<a class=\"next-page pagi-button\" title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion


    }
}