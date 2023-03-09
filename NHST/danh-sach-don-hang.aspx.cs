using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Net;
using Supremes;
using System.IO;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;
namespace NHST
{
    public partial class danh_sach_don_hang1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "fionalee01";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            int t = Request.QueryString["t"].ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                string info = "";
                var accInfo = AccountInfoController.GetByUserID(obj_user.ID);
                if (accInfo != null)
                {
                    info = accInfo.FirstName + " " + accInfo.LastName + "|";
                    info += accInfo.Email + "|" + accInfo.Phone + "|" + accInfo.Address + "|";
                }
                hdfInfouser.Value = info;
                int UID = obj_user.ID;

                //Khai báo biến
                double tongsodonhang = 0;
                double tongtrigiadonhang = 0;
                double tongtienlayhang = 0;

                double tongtienhangchuagiao = 0;
                double Tongtienhangcandatcoc = 0;
                double Tongtienhangchovekhotq = 0;
                double Tongtienhangdavekhotq = 0;
                double Tongtienhangdangokhovn = 0;

                double order_stt0 = 0;
                double order_stt2 = 0;
                double order_stt5 = 0;
                double order_stt6 = 0;
                double order_stt7 = 0;
                double order_stt10 = 0;

                string se = Request.QueryString["s"];
                int typesearch = Request.QueryString["l"].ToInt(0);
                int status = Request.QueryString["stt"].ToInt(-1);
                string fd = Request.QueryString["fd"];
                string td = Request.QueryString["td"];
                txtSearhc.Text = se;
                ddlType.SelectedValue = typesearch.ToString();
                if (Request.QueryString["stt"] != null)
                    ddlStatus.SelectedValue = status.ToString();
                if (!string.IsNullOrEmpty(Request.QueryString["fd"]))
                {
                    rFD.SelectedDate = Convert.ToDateTime(fd);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["td"]))
                    rTD.SelectedDate = Convert.ToDateTime(td);
                //var os = MainOrderController.GetAllByUIDNotHidden_SqlHelper(UID, status, fd, td);

                List<MainOrderController.mainorder> os = new List<MainOrderController.mainorder>();
                os = MainOrderController.GetAllByUIDOrderCodeNotHidden_SqlHelper(UID, t);
                var stt0 = os.Where(o => o.Status == 0).ToList();
                var stt1 = os.Where(o => o.Status == 1).ToList();
                var stt2 = os.Where(o => o.Status == 2).ToList();
                var stt5 = os.Where(o => o.Status == 5).ToList();
                var stt11 = os.Where(o => o.Status == 5 && o.IsShopSendGoods == true).ToList();
                var stt12 = os.Where(o => o.Status == 2 && o.IsBuying == true).ToList();
                var stt13 = os.Where(o => o.Status == 2 && o.IsPaying == true).ToList();
                var stt6 = os.Where(o => o.Status == 6).ToList();
                var stt7 = os.Where(o => o.Status == 7).ToList();
                var stt9 = os.Where(o => o.Status == 9).ToList();
                var stt10 = os.Where(o => o.Status == 10).ToList();
                double userwallet = 0;
                double userwalletleft = 0;
                double totalleftpay = 0;

                if (stt0.Count > 0)
                {
                    ltrbtndepay.Text += "<a href=\"javascript:;\" id=\"btnDepositAll\" onclick=\"depositAll()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover\">Đặt cọc tất cả đơn</a>";
                }
                if (stt7.Count > 0)
                {
                    ltrbtndepay.Text += "<a href=\"javascript:;\" id=\"btnPayAll\" onclick=\"payAll()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover\">Thanh toán tất cả đơn</a>";
                }

                string accountamount = "0 VNĐ";
                string accountleftmoney = "Tài khoản không đủ để thanh toán";

                hdfList7Count.Value = stt7.Count.ToString();

                bttnAll.Text = "Tất cả (" + os.Count + ")";
                btn0.Text = "Chờ đặt cọc (" + stt0.Count + ")";
                btn1.Text = "Hủy đơn hàng (" + stt1.Count + ")";
                btn2.Text = "Chờ mua hàng (" + stt2.Count + ")";
                btn5.Text = "Chờ shop TQ phát hàng (" + stt5.Count + ")";
                btn11.Text = "Shop đã phát hàng (" + stt11.Count + ")";
                btn12.Text = "Đang mua hàng (" + stt12.Count + ")";
                //btn13.Text = "Đã thanh toán cho shop (" + stt13.Count + ")";
                btn6.Text = "Đã về kho TQ (" + stt6.Count + ")";
                btn7.Text = "Đã về kho VN (" + stt7.Count + ")";
                btn9.Text = "Khách đã thanh toán (" + stt9.Count + ")";
                btn10.Text = "Đã hoàn thành (" + stt10.Count + ")";
                if (!string.IsNullOrEmpty(Request.QueryString["fd"]))
                {
                    rFD.SelectedDate = Convert.ToDateTime(fd);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["td"]))
                    rTD.SelectedDate = Convert.ToDateTime(td);

                os = MainOrderController.GetAllByUIDNotHidden_SqlHelperNew(UID, se, typesearch, status, fd, td, t);

                if (os.Count > 0)
                {

                    string listID7 = "";
                    DateTime checkdate = DateTime.Now;
                    tongsodonhang = os.Count;
                    double totalall7 = 0;
                    double totalall7_deposit = 0;
                    foreach (var o in os)
                    {
                        int oStatus = o.Status;
                        if (oStatus > 0)
                        {
                            //Tongtienhangcandatcoc += Convert.ToDouble(o.AmountDeposit);
                            if (oStatus >= 2)
                            {
                                tongtrigiadonhang += Convert.ToDouble(o.TotalPriceVND);
                                if (oStatus == 7)
                                {
                                    double totalpriceVND = 0;
                                    double deposited = 0;
                                    if (o.TotalPriceVND.ToFloat(0) > 0)
                                    {
                                        totalpriceVND = Convert.ToDouble(o.TotalPriceVND);
                                    }
                                    if (o.Deposit.ToFloat(0) > 0)
                                    {
                                        deposited = Convert.ToDouble(o.Deposit);
                                    }
                                    totalall7 += totalpriceVND;
                                    totalall7_deposit += deposited;


                                    //totalleftpay += (totalpriceVND - deposited);
                                    listID7 += o.ID + "|";
                                }
                            }
                        }
                        else if (oStatus == 0)
                        {
                            Tongtienhangcandatcoc += Convert.ToDouble(o.AmountDeposit);
                            DateTime CreatedDate = Convert.ToDateTime(o.CreatedDate);
                            TimeSpan span = checkdate.Subtract(CreatedDate);
                            if (span.Days > 7)
                            {
                                MainOrderController.UpdateIsHiddenTrue(o.ID);
                            }
                        }
                    }
                    tongtienlayhang = totalall7 - totalall7_deposit;
                    if (!string.IsNullOrEmpty(listID7))
                    {
                        //ltrBtnYCG.Text += "<a class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover\" onclick=\"requestall();\" href=\"javascript:;\">Yêu cầu giao hàng</a>";
                        if (obj_user.Wallet > 0)
                        {
                            userwallet = Convert.ToDouble(obj_user.Wallet);
                            accountamount = string.Format("{0:N0}", userwallet).Replace(",", ".") + " VNĐ";

                        }
                        if (userwallet >= tongtienlayhang)
                        {
                            userwalletleft = userwallet - tongtienlayhang;
                            accountleftmoney = string.Format("{0:N0}", userwalletleft).Replace(",", ".") + " VNĐ";
                        }
                        hdfAccountAmount.Value = accountamount;
                        hdfTotalLeft.Value = accountleftmoney;
                        hdfListID7.Value = listID7;
                    }



                    //Ghi ra 
                    ltrAllOrderCount.Text = string.Format("{0:N0}", tongsodonhang).Replace(",", ".");
                    ltrAllOrderPrice.Text = string.Format("{0:N0}", tongtrigiadonhang).Replace(",", ".");
                    //ltrTotalGetAllProduct.Text = string.Format("{0:N0}", tongtienlayhang).Replace(",", ".");

                    ltrTongtienhangchuagiao.Text = string.Format("{0:N0}", tongtienhangchuagiao).Replace(",", ".");
                    ltrTongtienhangcandatcoc.Text = string.Format("{0:N0}", Tongtienhangcandatcoc).Replace(",", ".");
                    ltrTongtienhangchovekhotq.Text = string.Format("{0:N0}", Tongtienhangchovekhotq).Replace(",", ".");
                    ltrTongtienhangdavekhotq.Text = string.Format("{0:N0}", Tongtienhangdavekhotq).Replace(",", ".");
                    ltrTongtienhangdangokhovn.Text = string.Format("{0:N0}", Tongtienhangdangokhovn).Replace(",", ".");
                    ltrTongtienhangcanthanhtoandelayhang.Text = string.Format("{0:N0}", tongtienlayhang).Replace(",", ".");
                    hdfTotalLeftPay.Value = string.Format("{0:N0}", tongtienlayhang).Replace(",", ".") + " VNĐ";

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    var listDep = HttpContext.Current.Session["ListDep"] as List<DepAll>;
                    if (listDep != null)
                    {
                        if (listDep.Count > 0)
                        {
                            hdfShowDep.Value = serializer.Serialize(listDep);
                        }
                    }
                    pagingall(os);
                }
            }
        }
        #region Paging
        public void pagingall(List<MainOrderController.mainorder> acs)
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

                StringBuilder html = new StringBuilder();
                List<int> ListIDs = (List<int>)Session["listIDs"];
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    int status = Convert.ToInt32(item.Status);

                    //html.Append("<tr>");

                    if (status == 0)
                    {
                        double deposited = 0;
                        double TotalPriceVND = 0;
                        if (item.TotalPriceVND.ToFloat(0) > 0)
                        {
                            TotalPriceVND = Convert.ToDouble(item.TotalPriceVND);
                        }
                        if (item.Deposit.ToFloat(0) > 0)
                        {
                            deposited = Convert.ToDouble(item.Deposit);
                        }
                        double must_Deposit = Convert.ToDouble(item.AmountDeposit);
                        double must_Deposit_left = must_Deposit - deposited;

                        if (item.OrderType == 1)
                        {
                            html.Append("<tr data-action=\"deposit\">");
                            html.Append("<td>");
                            var list = HttpContext.Current.Session["ListDep"] as List<DepAll>;
                            if (list != null)
                            {
                                var check = list.Where(x => x.MainOrderID == item.ID).FirstOrDefault();
                                if (check != null)
                                {
                                    html.Append(" <label><input type=\"checkbox\" checked onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-id=\"" + item.ID + "\"><span></span></label>");
                                }
                                else
                                {
                                    html.Append(" <label><input type=\"checkbox\" onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-id=\"" + item.ID + "\"><span></span></label>");
                                }
                            }
                            else
                            {
                                html.Append(" <label><input type=\"checkbox\" onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-id=\"" + item.ID + "\"><span></span></label>");
                            }
                            html.Append("</td>");
                        }
                        else
                        {
                            if (item.IsCheckNotiPrice == false)
                            {
                                html.Append("<tr data-action=\"deposit\">");
                                html.Append("<td>");
                                var list = HttpContext.Current.Session["ListDep"] as List<DepAll>;
                                if (list != null)
                                {
                                    var check = list.Where(x => x.MainOrderID == item.ID).FirstOrDefault();
                                    if (check != null)
                                    {
                                        html.Append(" <label><input type=\"checkbox\" checked onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-id=\"" + item.ID + "\"><span></span></label>");
                                    }
                                    else
                                    {
                                        html.Append(" <label><input type=\"checkbox\" onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-id=\"" + item.ID + "\"><span></span></label>");
                                    }
                                }
                                else
                                {
                                    html.Append(" <label><input type=\"checkbox\" onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-id=\"" + item.ID + "\"><span></span></label>");
                                }
                                html.Append("</td>");
                            }
                            else
                            {
                                html.Append("<tr>");
                                html.Append("<td>");
                                html.Append("</td>");
                            }
                        }
                    }

                    else
                    {
                        html.Append("<tr>");
                        html.Append("<td>");
                        html.Append("</td>");
                    }

                    html.Append("<td style=\"text-align: center\">" + item.ID + "</td>");
                    html.Append("<td style=\"text-align: center\"><img src=\"" + item.anhsanpham + "\" alt=\"\" /></td>");
                    html.Append("<td style=\"text-align: center\">" + item.ShopName + "</td>");
                    html.Append("<td style=\"text-align: center\">" + item.Site + "</td>");
                    html.Append("<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>");
                    html.Append("<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.AmountDeposit)) + "</td>");
                    html.Append("<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + "</td>");
                    html.Append("<td style=\"text-align: center\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                    html.Append("<td style=\"text-align: center\">" + string.Format("{0:dd/MM/yyyy}", item.ExpectedDate) + "</td>");

                   html.Append("<td style=\"text-align: center\"><p class=\"s-txt\">Lên đơn: " + string.Format("{0:HH:mm:ss}", item.CreatedDate) + " - " + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + " </p>");
                    //if (item.CreatedDate != null)
                    //{
                    //    if (!string.IsNullOrEmpty(item.CreatedDate.ToString()))
                    //       html.Append("<p class=\"black s-txt\">Mới tạo: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.CreatedDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.CreatedDate)) + " </p>");
                    //}
                    if (item.DepostiDate != null)
                    {
                        if (!string.IsNullOrEmpty(item.DepostiDate.ToString()))
                           html.Append("<p class=\"black s-txt\">Chờ mua hàng: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.DepostiDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.DepostiDate)) + " </p>");
                    }
                    if (item.BuyingDate != null)
                    {
                        if (!string.IsNullOrEmpty(item.BuyingDate.ToString()))
                           html.Append("<p class=\"black s-txt\">Đang mua hàng: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.BuyingDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.BuyingDate)) + " </p>");
                    }
                    if (item.ShopSendGoodsDate != null)
                    {
                        if (!string.IsNullOrEmpty(item.ShopSendGoodsDate.ToString()))
                           html.Append("<p class=\"black s-txt\">Shop phát hàng: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.ShopSendGoodsDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.BuyProductDate)) + " </p>");
                    }
                    if (item.TQWarehouseDate != null)
                    {
                        if (!string.IsNullOrEmpty(item.TQWarehouseDate.ToString()))
                           html.Append("<p class=\"black s-txt\">Đang về kho đích: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.TQWarehouseDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.TQWarehouseDate)) + " </p>");
                    }
                    if (item.VNWarehouseDate != null)
                    {
                        if (!string.IsNullOrEmpty(item.VNWarehouseDate.ToString()))
                           html.Append("<p class=\"black s-txt\">Đã đến kho đích: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.VNWarehouseDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.VNWarehouseDate)) + " </p>");
                    }
                    if (item.PayAllDate != null)
                    {
                        if (!string.IsNullOrEmpty(item.PayAllDate.ToString()))
                           html.Append("<p class=\"black s-txt\">Đã thanh toán: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.PayAllDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.PayAllDate)) + " </p>");
                    }
                    if (item.CompleteDate != null)
                    {
                        if (!string.IsNullOrEmpty(item.CompleteDate.ToString()))
                           html.Append("<p class=\"black s-txt\">Hoàn thành: " + string.Format("{0:HH:mm:ss}", Convert.ToDateTime(item.CompleteDate)) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.CompleteDate)) + " </p>");
                    }

                   html.Append("  </td>");

                    if (item.OrderType == 3)
                    {
                        if (item.IsCheckNotiPrice == true)
                        {
                            html.Append("<td style=\"text-align: center;white-space: nowrap;\"><span class=\"bg-yellow-gold\">Chờ báo giá</span></td>");
                        }
                        else
                        {
                            html.Append("<td style=\"text-align: center;white-space: nowrap;\">" + PJUtils.IntToRequestAdmin(status) + "</td>");
                        }
                    }
                    else
                    {
                        if (status == 5)
                        {
                            if (item.IsShopSendGoods == true)
                            {
                                html.Append("<td style=\"text-align: center;white-space: nowrap;\"><span class=\"bg-green\">Shop đã phát hàng</span></td>");
                            }
                            else
                            {
                                html.Append("<td style=\"text-align: center;white-space: nowrap;\">" + PJUtils.IntToRequestAdmin(status) + "</td>");
                            }
                        }
                        else if (status == 2)
                        {
                            if (item.IsBuying == true && item.IsPaying == false)
                            {
                                html.Append("<td style=\"text-align: center;white-space: nowrap;\"><span class=\"bg-red\">Đang mua hàng</span></td>");
                            }
                            else if (item.IsPaying == true && item.IsBuying == false)
                            {
                                html.Append("<td style=\"text-align: center;white-space: nowrap;\"><span class=\"bg-green\">Chờ shop TQ phát hàng</span></td>");
                            }
                            else if (item.IsPaying == true && item.IsBuying == true)
                            {
                                html.Append("<td style=\"text-align: center;white-space: nowrap;\"><span class=\"bg-green\">Chờ shop TQ phát hàng</span></td>");
                            }
                            else
                            {
                                html.Append("<td style=\"text-align: center;white-space: nowrap;\">" + PJUtils.IntToRequestAdmin(status) + "</td>");
                            }
                        }

                        else
                            html.Append("<td style=\"text-align: center;white-space: nowrap;\">" + PJUtils.IntToRequestAdmin(status) + "</td>");
                    }

                    html.Append("<td style=\"text-align: center\">");
                    if (item.OrderType == 3)
                    {
                        if (item.IsCheckNotiPrice == true)
                        {

                        }
                        else
                        {
                            if (item.Status == 0)
                            {
                                html.Append("    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "')\" class=\"bg-green\" style=\"float:left;width:100%;margin-bottom:5px;\">Đặt cọc</a><br/>");
                            }
                        }
                    }
                    else
                    {
                        if (item.Status == 0)
                        {
                            html.Append("    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "')\" class=\"bg-green\" style=\"float:left;width:100%;margin-bottom:5px;\">Đặt cọc</a><br/>");
                        }
                    }

                    html.Append("     <a href=\"/chi-tiet-don-hang/" + item.ID + "\" class=\"viewmore-orderlist\">Chi tiết</a>");
                    html.Append("    <a href=\"javascript:;\" onclick=\"OrderSame('" + item.ID + "')\" class=\"bg-green\" style=\"float:left;width:100%;margin-bottom:5px;\">Tạo đơn tương tự</a><br/>");
                    if (item.Status == 0)
                    {
                        html.Append("     <a href=\"javascript:;\" onclick=\"cancelOrder('" + item.ID + "')\" class=\"complain-orderlist bg-black\">Hủy đơn hàng</a>");
                    }
                    else
                    {
                        html.Append("     <a href=\"/them-khieu-nai/" + item.ID + "\" class=\"complain-orderlist\">Khiếu nại</a>");
                    }

                    html.Append("  </td>");
                    html.Append("</tr>");
                }
                ltr.Text = html.ToString();
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
            output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Previous</a></li>");
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
                output.Append("<li class=\"pagerange\"><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<li class=\"current-page-item\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<li class=\"pagerange\" ><a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a></li>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion


        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string Site { get; set; }
            public string TotalPriceVND { get; set; }
            public string AmountDeposit { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
        }

        protected void btnSear_Click(object sender, EventArgs e)
        {
            int t = Request.QueryString["t"].ToInt(1);
            string status = ddlStatus.SelectedValue;
            string fd = "";
            string td = "";
            string text = txtSearhc.Text;
            string typesear = ddlType.SelectedValue;
            if (rFD.SelectedDate != null)
            {
                fd = rFD.SelectedDate.ToString();
            }
            if (rTD.SelectedDate != null)
            {
                td = rTD.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(fd) && !string.IsNullOrEmpty(td))
            {
                if (Convert.ToDateTime(fd) > Convert.ToDateTime(td))
                {
                    PJUtils.ShowMessageBoxSwAlert("Từ ngày phải trước đến ngày", "e", false, Page);
                }
                else
                {
                    Response.Redirect("/danh-sach-don-hang?t=" + t + "&stt=" + status + "&s=" + text + "&l=" + typesear + "&fd=" + fd + "&td=" + td + "");
                }
            }
            else
            {
                Response.Redirect("/danh-sach-don-hang?t=" + t + "&stt=" + status + "&s=" + text + "&l=" + typesear + "&fd=" + fd + "&td=" + td + "");
            }
            //LoadData();
        }

        protected void btnAllrequest_Click(object sender, EventArgs e)
        {
            string lo = hdfListOrder.Value;
            DateTime currentDate = DateTime.Now;
            if (!string.IsNullOrEmpty(lo))
            {
                string username = Session["userLoginSystem"].ToString();
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    int UID = u.ID;
                    string[] listorders = lo.Split('|');
                    for (int i = 0; i < listorders.Length - 1; i++)
                    {
                        int OrderID = listorders[i].ToInt(0);
                        MainOrderController.UpdateIsGiaohang(OrderID, true);
                    }
                    PJUtils.ShowMessageBoxSwAlert("Yêu cầu thành công", "s", true, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Vui lòng chọn đơn hàng để xác nhận", "e", true, Page);
            }
        }

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                if (obj_user.Wallet > 0)
                {
                    int OID = hdfOrderID.Value.ToInt();
                    if (OID > 0)
                    {
                        int uid = obj_user.ID;
                        var o = MainOrderController.GetAllByUIDAndID(uid, OID);
                        if (o != null)
                        {
                            double orderdeposited = 0;
                            double amountdeposit = 0;

                            if (!string.IsNullOrEmpty(o.Deposit))
                                orderdeposited = Math.Round(Convert.ToDouble(o.Deposit), 0);

                            if (!string.IsNullOrEmpty(o.AmountDeposit))
                                amountdeposit = Math.Round(Convert.ToDouble(o.AmountDeposit), 0);
                            double leftDeposit = amountdeposit - orderdeposited;
                            if (leftDeposit > 0)
                            {
                                double userwallet = Convert.ToDouble(obj_user.Wallet);
                                if (userwallet > 0)
                                {
                                    if (leftDeposit > 0)
                                    {
                                        if (userwallet >= leftDeposit)
                                        {
                                            double wallet = userwallet - leftDeposit;
                                            //Cập nhật lại MainOrder                                
                                            MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                            MainOrderController.UpdateDeposit_Datetime(o.ID, obj_user.ID, amountdeposit.ToString(), currentDate);
                                            if (o.DepostiDate != null)
                                                MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                            HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, leftDeposit,
                                                obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                            AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                            PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, leftDeposit, 2, currentDate, obj_user.Username);
                                            PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                                        }
                                        else
                                        {
                                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                                "e", true, Page);
                                        }
                                    }
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                                "e", true, Page);
                                }
                            }

                        }
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                    "e", true, Page);
                }
            }
        }

        protected void btnOrderSame_Click(object sender, EventArgs e)
        {

            double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            //string rq = Session["orderitem"].ToString();
            if (obj_user != null)
            {
                int OID = hdfOrderID.Value.ToInt();
                var mosame = MainOrderController.GetAllByID(OID);
                
                var acsame = AccountController.GetByID(Convert.ToInt32( mosame.UID));
                //int salerID = obj_user.SaleID.ToString().ToInt(0);
                int salerID = mosame.SalerID.ToString().ToInt(0);
                int dathangID = mosame.DathangID.ToString().ToInt(0);
                //int dathangID = obj_user.DathangID.ToString().ToInt(0);
                //int UID = mosame.UID;
                int receivePlace = Convert.ToInt32(mosame.ReceivePlace);
                //int receivePlace = Convert.ToInt32(ddlPlace.SelectedValue);
                double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);
                //string wareship = hdfTeamWare.Value;
                double total = 0;
                double fastprice = 0;
                double pricepro = 0;
                double priceproCYN = 0;
                var odersame = OrderController.GetByMainOrderID(mosame.ID);
                if (odersame.Count > 0 )
                {
                    foreach (var item in odersame)
                    {
                        int quantity = Convert.ToInt32(item.quantity);
                        double originprice = Convert.ToDouble(item.price_origin);
                        double promotionprice = Convert.ToDouble(item.price_promotion);
                        double u_pricecbuy = 0;
                        double u_pricevn = 0;
                        double e_pricebuy = 0;
                        double e_pricevn = 0;

                        if (promotionprice > 0)
                        {
                            if (promotionprice < originprice)
                            {
                                u_pricecbuy = promotionprice;
                                u_pricevn = promotionprice * current;
                            }
                            else
                            {
                                u_pricecbuy = originprice;
                                u_pricevn = originprice * current;
                            }
                        }
                        else
                        {
                            u_pricecbuy = originprice;
                            u_pricevn = originprice * current;
                        }

                        e_pricebuy = u_pricecbuy * quantity;
                        e_pricevn = u_pricevn * quantity;

                        pricepro += e_pricevn;
                        priceproCYN += e_pricebuy;

                    }
                }
                double feecnship = 0;

                if (mosame.IsFast == true)
                {
                    fastprice = (pricepro * 5 / 100);
                }
                string ShopID = mosame.ShopID;
                string ShopName = mosame.ShopName;
                string Site = mosame.Site;
                bool IsForward = Convert.ToBoolean(mosame.IsForward);
                string IsForwardPrice = mosame.IsForwardPrice;
                bool IsFastDelivery = Convert.ToBoolean(mosame.IsFastDelivery);
                string IsFastDeliveryPrice = mosame.IsFastDeliveryPrice;
                bool IsCheckProduct = Convert.ToBoolean(mosame.IsCheckProduct);
                string IsCheckProductPrice = mosame.IsCheckProductPrice;
                bool IsPacked = Convert.ToBoolean(mosame.IsPacked);
                string IsPackedPrice = mosame.IsPackedPrice;
                bool IsFast = Convert.ToBoolean(mosame.IsFast);
                string IsFastPrice = fastprice.ToString();

                double pricecynallproduct = 0;

                double totalFee_CountFee = fastprice + pricepro + feecnship + mosame.IsCheckProductPrice.ToFloat(0);
                double servicefee = 0;
                double servicefeeMoney = 0;

                bool getFeeFromUser = false;
                if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                {
                    if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                    {
                        servicefee = Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                        getFeeFromUser = true;
                    }
                    else
                    {
                        var adminfeebuypro = FeeBuyProController.GetAll();
                        if (adminfeebuypro.Count > 0)
                        {
                            foreach (var item in adminfeebuypro)
                            {
                                if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                {
                                    servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                    //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    var adminfeebuypro = FeeBuyProController.GetAll();
                    if (adminfeebuypro.Count > 0)
                    {
                        foreach (var item in adminfeebuypro)
                        {
                            if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                            {
                                servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                break;
                            }
                        }
                    }
                }

                double feebpnotdc = pricepro * servicefee;
                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                double feebp = 0;
                feebp = feebpnotdc - subfeebp;
                feebp = Math.Round(feebp, 0);

                if (feebp < 10000)
                {
                    feebp = 10000;
                }

                total = fastprice + pricepro + feebp + feecnship + mosame.IsCheckProductPrice.ToFloat(0);

                string PriceVND = pricepro.ToString();
                string PriceCNY = priceproCYN.ToString();
                //string FeeShipCN = (10 * current).ToString();
                string FeeShipCN = feecnship.ToString();
                string FeeBuyPro = feebp.ToString();
                string FeeWeight = "0";
                string Note = mosame.Note;
                int Status = 0;
                string Deposit = "0";
                string CurrentCNYVN = current.ToString();
                string TotalPriceVND = total.ToString();
                string AmountDeposit = (total * LessDeposit / 100).ToString();

                string kq = MainOrderController.Insert(Convert.ToInt32( mosame.UID), ShopID ,ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, "0", IsCheckProduct, IsCheckProductPrice,
                                    IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro, FeeWeight, mosame.Note, mosame.FullName, mosame.Address
                                    , mosame.Email, mosame.Phone, Status, Deposit,CurrentCNYVN, TotalPriceVND, salerID, dathangID, currentDate, Convert.ToInt32(mosame.UID),AmountDeposit, 1);

                int idkq = Convert.ToInt32(kq);
                if (idkq > 0)
                {
                    foreach (var item in odersame)
                    {
                        int quantity = Convert.ToInt32(item.quantity);
                        double originprice = Convert.ToDouble(item.price_origin);
                        double promotionprice = Convert.ToDouble(item.price_promotion);
                        double u_pricecbuy = 0;
                        double u_pricevn = 0;
                        double e_pricebuy = 0;
                        double e_pricevn = 0;
                        if (promotionprice > 0)
                        {
                            if (promotionprice < originprice)
                            {
                                u_pricecbuy = promotionprice;
                                u_pricevn = promotionprice * current;
                            }
                            else
                            {
                                u_pricecbuy = originprice;
                                u_pricevn = originprice * current;
                            }
                        }
                        else
                        {
                            u_pricecbuy = originprice;
                            u_pricevn = originprice * current;
                        }


                        e_pricebuy = u_pricecbuy * quantity;
                        e_pricevn = u_pricevn * quantity;

                        pricecynallproduct += e_pricebuy;

                        string image = item.image_origin;
                        if (image.Contains("%2F"))
                        {
                            image = image.Replace("%2F", "/");
                        }
                        if (image.Contains("%3A"))
                        {
                            image = image.Replace("%3A", ":");
                        }
                        string ret = OrderController.Insert(Convert.ToInt32(mosame.UID), item.title_origin, item.title_translated, item.price_origin, item.price_promotion, item.property_translated,
                        item.property, item.data_value, image, image, item.shop_id, item.shop_name, item.seller_id, item.wangwang, item.quantity,
                        item.stock, item.location_sale, item.site, item.comment, item.item_id, item.link_origin, item.outer_id, item.error, item.weight, item.step, item.stepprice, item.brand,
                        item.category_name, item.category_id, item.tool, item.version, Convert.ToBoolean(item.is_translate), Convert.ToBoolean(item.IsForward), "0",
                        Convert.ToBoolean(item.IsFastDelivery), "0", Convert.ToBoolean(item.IsCheckProduct), "0", Convert.ToBoolean(item.IsPacked), "0", Convert.ToBoolean(item.IsFast),
                        fastprice.ToString(), pricepro.ToString(), PriceCNY, item.Note, mosame.FullName, mosame.Address, mosame.Email,
                        mosame.Phone, 0, "0", current.ToString(), total.ToString(), idkq, DateTime.Now, Convert.ToInt32(mosame.UID), item.link);

                        if (item.price_promotion.ToFloat(0) > 0)
                            OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_promotion);
                        else
                            OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_origin);
                    }
                    MainOrderController.UpdateReceivePlace(idkq, Convert.ToInt32(mosame.UID), mosame.ReceivePlace, mosame.ShippingType.ToString().ToInt());
                    MainOrderController.UpdateFromPlace(idkq, Convert.ToInt32(mosame.UID), mosame.FromPlace.ToString().ToInt(), mosame.ShippingType.ToString().ToInt());
                    var admins = AccountController.GetAllByRoleID(0);
                    if (admins.Count > 0)
                    {
                        foreach (var admin in admins)
                        {
                            NotificationController.Inser(Convert.ToInt32(acsame.ID), username, admin.ID,
                                                               admin.Username, idkq,
                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                                                               1, currentDate, username, false);
                        }
                    }

                    var managers = AccountController.GetAllByRoleID(2);
                    if (managers.Count > 0)
                    {
                        foreach (var manager in managers)
                        {
                            NotificationController.Inser(Convert.ToInt32(acsame.ID), username, manager.ID,
                                                               manager.Username, 0,
                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                                                               1, currentDate, username, false);
                        }
                    }
                    //var sale = AccountController.GetAllBySaleID(salerID);
                    //if (sale.Count > 0)
                    //{
                    //    foreach (var manager in sale)
                    //    {
                    //        NotificationController.Inser(Convert.ToInt32(acsame.ID), username, manager.ID,
                    //                                           manager.Username, 0,
                    //                                           "Có đơn hàng mới ID là: " + idkq, 0,
                    //                                           1, currentDate, username, false);
                    //    }
                    //}
                }
                double salepercent = 0;
                double salepercentaf3m = 0;
                double dathangpercent = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    salepercent = Convert.ToDouble(config.SalePercent);
                    salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                    dathangpercent = Convert.ToDouble(config.DathangPercent);
                }
                string salerName = "";
                string dathangName = "";

                if (salerID > 0)
                {
                    var sale = AccountController.GetByID(salerID);
                    if (sale != null)
                    {
                        salerName = sale.Username;
                        var createdDate = Convert.ToDateTime(sale.CreatedDate);
                        int d = currentDate.Subtract(createdDate).Days;
                        if (d > 90)
                        {
                            double per = feebp * salepercentaf3m / 100;
                            StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                            currentDate, currentDate, username);
                        }
                        else
                        {
                            double per = feebp * salepercent / 100;
                            StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                            currentDate, currentDate, username);
                        }
                    }
                }
                if (dathangID > 0)
                {
                    var dathang = AccountController.GetByID(dathangID);
                    if (dathang != null)
                    {
                        dathangName = dathang.Username;
                        StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                            currentDate, currentDate, username);
                        NotificationController.Inser(dathangID, username, dathang.ID,
                                                       dathang.Username, idkq,
                                                       "Có đơn hàng mới ID là: " + idkq, 0,
                                                       1, currentDate, username, false);
                    }
                }

                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                hubContext.Clients.All.addNewMessageToPage("", "");
                Response.Redirect("/danh-sach-don-hang?t=1");





               

            }
            else
            {
                Response.Redirect("/trang-chu");
            }




        }










        #region Button status
        protected void btn0_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 0;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 1;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 2;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn5_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 5;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }
        protected void btn11_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 11;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn12_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 12;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn13_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 13;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn6_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 6;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn7_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 7;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn9_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 9;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn10_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 10;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void bttnAll_Click(object sender, EventArgs e)
        {
            string ty = "1";
            if (Request.QueryString["t"] != null)
                ty = Request.QueryString["t"];
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = -1;
            Response.Redirect("/danh-sach-don-hang?t=" + ty + "&fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }
        #endregion

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = hdfOrderID.Value.ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double wallet = obj_user.Wallet.ToString().ToFloat();
                        wallet = wallet + Convert.ToDouble(o.Deposit);
                        AccountController.updateWallet(obj_user.ID, wallet, DateTime.Now, obj_user.Username);
                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, "0");

                        string kq = MainOrderController.UpdateStatus(id, uid, 1);
                        if (kq == "ok")
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    }
                }
            }
        }

        [WebMethod]
        public static string sendrequest(int PTTT, int PTNH, string FullName, string Email,
            string Phone, string Note, string Address, string ListID)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                int statusM = 0;
                if (!string.IsNullOrEmpty(ListID))
                {
                    string retu = "";
                    string[] IDs = ListID.Split('|');
                    for (int i = 0; i < IDs.Length - 1; i++)
                    {
                        int ID = Convert.ToInt32(IDs[i]);
                        var mainorder = MainOrderController.GetAllByUIDAndID(UID, ID);
                        if (mainorder != null)
                        {
                            statusM = Convert.ToInt32(mainorder.Status);
                            MainOrderRequestShipController.Insert(ID, UID, FullName, Email, Phone, Note,
                                    Address, 1, statusM, PTTT, PTNH, currentDate, username);
                            retu = "ok";
                        }
                        else
                        {
                            retu = "khongtimthaydonhang";
                        }
                    }
                    return retu;
                }
                else
                    return "khongtimthaydonhang";
            }
            else
                return "notuser";
        }

        protected void btnDepositAll_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                double wallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);

                if (wallet > 0)
                {
                    double totalMustPay = 0;
                    var mainorder2 = MainOrderController.GetAllByUIDAndStatus(UID, 0);
                    if (mainorder2.Count > 0)
                    {
                        foreach (var mainOrder in mainorder2)
                        {
                            double Deposited = 0;
                            if (mainOrder.Deposit.ToFloat(0) > 0)
                            {
                                Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                            }
                            double ADeposited = 0;
                            if (mainOrder.AmountDeposit.ToFloat(0) > 0)
                            {
                                ADeposited = Math.Round(Convert.ToDouble(mainOrder.AmountDeposit), 0);
                            }

                            double moneyleft = Math.Round((ADeposited - Deposited), 0);

                            if (moneyleft > 0)
                            {
                                totalMustPay += moneyleft;
                            }
                        }
                    }

                    if (wallet >= totalMustPay)
                    {
                        foreach (var mainOrder in mainorder2)
                        {
                            double Deposited = 0;
                            if (mainOrder.Deposit.ToFloat(0) > 0)
                            {
                                Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                            }
                            double ADeposited = 0;
                            if (mainOrder.AmountDeposit.ToFloat(0) > 0)
                            {
                                ADeposited = Math.Round(Convert.ToDouble(mainOrder.AmountDeposit), 0);
                            }

                            double moneyleft = Math.Round((ADeposited - Deposited), 0);

                            int UIDOrder = Convert.ToInt32(mainOrder.UID);
                            var accPay = AccountController.GetByID(UIDOrder);
                            if (accPay != null)
                            {
                                double accWallet = Math.Round(Convert.ToDouble(accPay.Wallet), 0);
                                if (accWallet >= moneyleft)
                                {
                                    double walletLeft = Math.Round(accWallet - moneyleft, 0);
                                    double payalll = Math.Round(Deposited + moneyleft, 0);
                                    walletLeft = Math.Round(walletLeft, 0);
                                    MainOrderController.UpdateStatus(mainOrder.ID, UIDOrder, 2);
                                    MainOrderController.UpdateDeposit_Datetime(mainOrder.ID, UIDOrder, payalll.ToString(), currentDate);
                                    MainOrderController.UpdateDepositDate(mainOrder.ID, currentDate);
                                    AccountController.updateWallet(UIDOrder, walletLeft, currentDate, accPay.Username);
                                    HistoryOrderChangeController.Insert(mainOrder.ID, UIDOrder, accPay.Username, accPay.Username +
                                                " đã đổi trạng thái của đơn hàng ID là: " + mainOrder.ID + ", từ: Chưa đặt cọc, sang: Chờ mua hàng.", 1, currentDate);
                                    HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, mainOrder.ID,
                                                    moneyleft, obj_user.Username + " đã đặt cọc đơn hàng: " + mainOrder.ID + ".",
                                                    walletLeft, 1, 1, currentDate, obj_user.Username);
                                    PayOrderHistoryController.Insert(mainOrder.ID, obj_user.ID, 2, moneyleft, 2, currentDate, obj_user.Username);
                                }
                            }
                        }

                        PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.",
                              "e", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.",
                              "e", true, Page);
                }
            }
        }

        protected void btnPayAll_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                double wallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);

                if (wallet > 0)
                {
                    double totalMustPay = 0;
                    var mainorder7 = MainOrderController.GetAllByUIDAndStatus(UID, 7);
                    if (mainorder7.Count > 0)
                    {
                        foreach (var mainOrder in mainorder7)
                        {
                            double Deposited = 0;
                            if (mainOrder.Deposit.ToFloat(0) > 0)
                            {
                                Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                            }

                            double feewarehouse = 0;
                            if (mainOrder.FeeInWareHouse.ToString().ToFloat(0) > 0)
                                feewarehouse = Math.Round(Convert.ToDouble(mainOrder.FeeInWareHouse), 0);

                            double totalPriceVND = 0;
                            if (mainOrder.TotalPriceVND.ToFloat(0) > 0)
                                totalPriceVND = Math.Round(Convert.ToDouble(mainOrder.TotalPriceVND), 0);
                            double moneyleft = Math.Round((totalPriceVND + feewarehouse) - Deposited, 0);

                            if (moneyleft > 0)
                            {
                                totalMustPay += moneyleft;
                            }
                        }
                    }

                    if (wallet >= totalMustPay)
                    {
                        foreach (var mainOrder in mainorder7)
                        {
                            double Deposited = 0;
                            if (mainOrder.Deposit.ToFloat(0) > 0)
                            {
                                Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                            }

                            double feewarehouse = 0;
                            if (mainOrder.FeeInWareHouse.ToString().ToFloat(0) > 0)
                                feewarehouse = Math.Round(Convert.ToDouble(mainOrder.FeeInWareHouse), 0);

                            double totalPriceVND = 0;
                            if (mainOrder.TotalPriceVND.ToFloat(0) > 0)
                                totalPriceVND = Math.Round(Convert.ToDouble(mainOrder.TotalPriceVND), 0);
                            double moneyleft = Math.Round((totalPriceVND + feewarehouse) - Deposited, 0);

                            int UIDOrder = Convert.ToInt32(mainOrder.UID);
                            var accPay = AccountController.GetByID(UIDOrder);
                            if (accPay != null)
                            {
                                double accWallet = Math.Round(Convert.ToDouble(accPay.Wallet), 0);
                                if (accWallet >= moneyleft)
                                {
                                    double walletLeft = Math.Round(accWallet - moneyleft, 0);
                                    double payalll = Math.Round(Deposited + moneyleft, 0);
                                    walletLeft = Math.Round(walletLeft, 0);
                                    MainOrderController.UpdateStatus(mainOrder.ID, UIDOrder, 9);
                                    AccountController.updateWallet(UIDOrder, walletLeft, currentDate, accPay.Username);

                                    HistoryOrderChangeController.Insert(mainOrder.ID, UIDOrder, accPay.Username, accPay.Username +
                                                " đã đổi trạng thái của đơn hàng ID là: " + mainOrder.ID + ", từ: Đã về kho VN, sang: Khách đã thanh toán.", 1, currentDate);

                                    HistoryPayWalletController.Insert(UIDOrder, accPay.Username, mainOrder.ID, moneyleft,
                                        accPay.Username + " đã thanh toán đơn hàng: " + mainOrder.ID + ".",
                                        walletLeft, 1, 3, currentDate, accPay.Username);
                                    MainOrderController.UpdateDeposit_Datetime(mainOrder.ID, UIDOrder, payalll.ToString(), currentDate);
                                    MainOrderController.UpdatePayAllDate(mainOrder.ID, UIDOrder, currentDate);
                                    PayOrderHistoryController.Insert(mainOrder.ID, UIDOrder, 9, moneyleft, 2, currentDate, accPay.Username);
                                }
                            }
                        }

                        PJUtils.ShowMessageBoxSwAlert("Thanh toán đơn hàng thành công.", "s", true, Page);
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để thanh toán đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.",
                                  "e", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để thanh toán đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.",
                                  "e", true, Page);
                }
            }
        }

        public class DepAll
        {
            public int MainOrderID { get; set; }
            public double TotalDeposit { get; set; }
        }

        [WebMethod]
        public static string CheckDepAll(int MainOrderID, string TotalPrice)
        {
            List<DepAll> ldep = new List<DepAll>();
            var list = HttpContext.Current.Session["ListDep"] as List<DepAll>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    var check = list.Where(x => x.MainOrderID == MainOrderID).FirstOrDefault();
                    if (check != null)
                    {
                        list.Remove(check);
                    }
                    else
                    {
                        DepAll d = new DepAll();
                        d.MainOrderID = MainOrderID;
                        d.TotalDeposit = Convert.ToDouble(TotalPrice);
                        list.Add(d);
                    }
                }
                else
                {
                    DepAll d = new DepAll();
                    d.MainOrderID = MainOrderID;
                    d.TotalDeposit = Convert.ToDouble(TotalPrice);
                    list.Add(d);
                    //HttpContext.Current.Session["ListDep"] = ldep;
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(list);
            }
            else
            {
                DepAll d = new DepAll();
                d.MainOrderID = MainOrderID;
                d.TotalDeposit = Convert.ToDouble(TotalPrice);
                ldep.Add(d);
                HttpContext.Current.Session["ListDep"] = ldep;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(ldep);
            }
        }

        protected void btnDepositSelected1_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.UtcNow.AddHours(7);
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                double wallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);

                if (wallet > 0)
                {
                    var list = HttpContext.Current.Session["ListDep"] as List<DepAll>;
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            double totalMustPay = 0;
                            foreach (var item in list)
                            {
                                int orderID = item.MainOrderID;
                                var mainOrder = MainOrderController.GetAllByUIDAndID(UID, orderID);
                                if (mainOrder != null)
                                {
                                    if (mainOrder.Status == 0)
                                    {
                                        double Deposited = 0;
                                        double AmountDeposited = 0;
                                        if (mainOrder.Deposit.ToFloat(0) > 0)
                                        {
                                            Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                                        }
                                        if (mainOrder.AmountDeposit.ToFloat(0) > 0)
                                        {
                                            AmountDeposited = Math.Round(Convert.ToDouble(mainOrder.AmountDeposit), 0);
                                        }
                                        double mustDeposit = AmountDeposited - Deposited;
                                        if (mustDeposit > 0)
                                        {
                                            totalMustPay += mustDeposit;
                                        }
                                    }
                                }
                            }

                            if (wallet >= totalMustPay)
                            {
                                foreach (var item in list)
                                {
                                    int orderID = item.MainOrderID;
                                    var mainOrder = MainOrderController.GetAllByUIDAndID(UID, orderID);
                                    if (mainOrder != null)
                                    {
                                        if (mainOrder.Status == 0)
                                        {
                                            double Deposited = 0;
                                            double AmountDeposited = 0;
                                            if (mainOrder.Deposit.ToFloat(0) > 0)
                                            {
                                                Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                                            }
                                            if (mainOrder.AmountDeposit.ToFloat(0) > 0)
                                            {
                                                AmountDeposited = Math.Round(Convert.ToDouble(mainOrder.AmountDeposit), 0);
                                            }
                                            double mustDeposit = AmountDeposited - Deposited;
                                            int UIDOrder = Convert.ToInt32(mainOrder.UID);
                                            var accPay = AccountController.GetByID(UIDOrder);
                                            if (accPay != null)
                                            {
                                                double accWallet = Math.Round(Convert.ToDouble(accPay.Wallet), 0);
                                                if (accWallet >= mustDeposit)
                                                {
                                                    double walletleft = accWallet - mustDeposit;
                                                    walletleft = Math.Round(walletleft, 0);
                                                    AccountController.updateWallet(obj_user.ID, walletleft, currentDate,
                                                        obj_user.Username);
                                                    //Cập nhật lại MainOrder                                
                                                    MainOrderController.UpdateStatus(mainOrder.ID, obj_user.ID, 2);
                                                    int statusOOld = Convert.ToInt32(mainOrder.Status);
                                                    int statusONew = 2;
                                                    //if (statusONew != statusOOld)
                                                    //{
                                                    //    StatusChangeHistoryController.Insert(mainOrder.ID, statusOOld, statusONew, currentDate, username_current);
                                                    //}
                                                    MainOrderController.UpdateDeposit_Datetime(mainOrder.ID, obj_user.ID, AmountDeposited.ToString(), currentDate);
                                                    MainOrderController.UpdateDepositDate(mainOrder.ID, currentDate);
                                                    HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, mainOrder.ID,
                                                        mustDeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + mainOrder.ID + ".",
                                                        walletleft, 1, 1, currentDate, obj_user.Username);
                                                    PayOrderHistoryController.Insert(mainOrder.ID, obj_user.ID, 2, mustDeposit, 2, currentDate, obj_user.Username);
                                                }
                                            }
                                        }
                                    }
                                }
                                Session["ListDep"] = null;
                                PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                      "e", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng được chọn để đặt cọc.", "e", true, Page);
                        }
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng được chọn để đặt cọc.", "e", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                   "e", true, Page);
                }
            }
        }


    }
}