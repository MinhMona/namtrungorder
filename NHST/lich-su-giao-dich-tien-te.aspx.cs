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
using NHST.Controllers;
using NHST.Models;

namespace NHST
{
    public partial class lich_su_giao_dich_tien_te : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "admin";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                ViewState["UID"] = UID;
                lblAccount.Text = "¥ " + u.WalletCYN + "";
                var ts = HistoryPayWalletCYNController.GetByUID(UID);
                string requy = "0";
                string renam = "0";
                if (ts.Count > 0)
                {
                    List<RYear> ys = new List<RYear>();
                    foreach (var item in ts)
                    {
                        RYear y = new RYear();
                        y.dyear = Convert.ToDateTime(item.CreatedDate);
                        ys.Add(y);

                    }
                    var t = ys.Select(s => s.dyear.Year).Distinct().ToList();
                    if (t.Count > 0)
                    {
                        foreach (var item in t)
                        {
                            ListItem listitem = new ListItem(item.ToString(), item.ToString());
                            ddlyear.Items.Add(listitem);
                            ddlyear.DataBind();
                        }
                    }
                    else
                    {
                        ListItem listitem = new ListItem("2017", "2017");
                        ddlyear.Items.Add(listitem);
                        ddlyear.DataBind();
                    }
                    if (Request.QueryString["thang"] != null && Request.QueryString["nam"] != null)
                    {
                        int thang = Request.QueryString["thang"].ToInt(1);
                        int nam = Request.QueryString["nam"].ToInt(2017);

                        requy = thang.ToString();
                        renam = nam.ToString();

                        DateTime fd;
                        DateTime td;
                        if (nam != 0)
                        {
                            if (thang > 0)
                            {
                                fd = new DateTime(nam, thang, 1);
                                td = new DateTime(nam, thang + 1, 1);
                            }
                            else
                            {
                                fd = new DateTime(nam, 1, 1);
                                td = new DateTime(nam + 1, 1, 1);
                            }
                            var filter = ts.Where(tr => tr.CreatedDate >= fd && tr.CreatedDate < td).ToList();
                            pagingall(filter);
                        }
                        else
                        {
                            int fnam = 2017;
                            int tnam = DateTime.Now.Year;
                            int leftnam = tnam - fnam;
                            if (leftnam > 0)
                            {
                                List<tbl_HistoryPayWalletCYN> all = new List<tbl_HistoryPayWalletCYN>();
                                for (int i = 0; i < leftnam; i++)
                                {
                                    if (thang > 0)
                                    {
                                        fd = new DateTime(fnam + 1, thang, 1);
                                        td = new DateTime(fnam + 1, thang + 1, 1);
                                    }
                                    else
                                    {
                                        fd = new DateTime(fnam + 1, 1, 1);
                                        td = new DateTime(fnam + 1 + 1, 1, 1);
                                    }
                                    var filter = ts.Where(tr => tr.CreatedDate >= fd && tr.CreatedDate < td).ToList();
                                    foreach (var item in filter)
                                    {
                                        all.Add(item);
                                    }
                                    fnam++;
                                }
                                pagingall(all);
                            }
                            else
                            {
                                if (thang > 0)
                                {
                                    fd = new DateTime(fnam, thang, 1);
                                    td = new DateTime(fnam, thang + 1, 1);
                                }
                                else
                                {
                                    fd = new DateTime(fnam, 1, 1);
                                    td = new DateTime(fnam + 1, 1, 1);
                                }
                                var filter = ts.Where(tr => tr.CreatedDate >= fd && tr.CreatedDate < td).ToList();
                                pagingall(filter);
                            }
                        }
                    }
                    else
                    {
                        pagingall(ts);
                    }
                }
                else
                {
                    ListItem listitem = new ListItem("2017", "2017");
                    ddlyear.Items.Add(listitem);
                    ddlyear.DataBind();
                    if (Request.QueryString["thang"] != null && Request.QueryString["nam"] != null)
                    {
                        int thang = Request.QueryString["thang"].ToInt(1);
                        int nam = Request.QueryString["nam"].ToInt(2017);
                        requy = thang.ToString();
                        renam = nam.ToString();
                    }
                }
                ddlQuy.SelectedValue = requy.ToString();
                ddlyear.SelectedValue = renam.ToString();
            }
        }
        #region Paging
        public void pagingall(List<tbl_HistoryPayWalletCYN> acs)
        {
            int PageSize = 15;
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

                int UID = Convert.ToInt32(ViewState["UID"]);
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    ltr.Text += "<tr>";
                    ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.Note + "</td>";
                    //ltr.Text += "<td style=\"text-align: center\">" + item.Note + "</td>";
                    ltr.Text += "<td style=\"text-align: right\">";
                    ltr.Text += "<strong class=\"font-size-16\">";
                    string symbol = "+";
                    if (item.Type == 1)
                        symbol = "-";

                    double amount = 0;

                    if (!string.IsNullOrEmpty(item.Amount.ToString()))
                        amount = Convert.ToDouble(item.Amount);

                    ltr.Text += symbol + " ¥ " + amount + "</strong><br />";
                    ltr.Text += "</td>";
                    ltr.Text += "<td style=\"text-align: center\">";
                    ltr.Text += "<strong class=\"font-size-16\">" + PJUtils.GetTradeTypeCYN(Convert.ToInt32(item.TradeType)) + "</strong><br />";
                    ltr.Text += "</td>";
                    ltr.Text += "<td style=\"text-align: right\">";
                    ltr.Text += "<strong class=\"font-size-16\">";
                    double moneyleft = 0;
                    if (item.MoneyLeft == null)
                        moneyleft = 0;
                    else
                    {
                        moneyleft = Convert.ToDouble(item.MoneyLeft);
                    }
                    ltr.Text += "¥ " + moneyleft + "</strong><br />";
                    ltr.Text += "</td>";
                    ltr.Text += "</tr>";
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
                //output.Append("<li class=\"pagerange\" ><a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a></li>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string quy = ddlQuy.SelectedValue;
            string nam = ddlyear.SelectedValue;
            Response.Redirect("/lich-su-giao-dich-tien-te?thang=" + quy + "&nam=" + nam + "");
        }
        public class RYear
        {
            public int ID { get; set; }
            public string Code { get; set; }
            public DateTime dyear { get; set; }
            public int year { get; set; }
        }
    }
}