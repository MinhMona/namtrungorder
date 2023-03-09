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

namespace NHST
{
    public partial class danh_sach_don_van_chuyen_ho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "vu221092";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                string oc = "";
                if (Request.QueryString["oc"] != null)
                {
                    oc = Request.QueryString["oc"];
                }
                int stt = Request.QueryString["stt"].ToInt(-1);
                string fd = Request.QueryString["fd"];
                string td = Request.QueryString["td"];

                txtOrderCode.Text = oc;

                if (Request.QueryString["stt"] != null)
                    ddlStatus.SelectedValue = stt.ToString();
                if (!string.IsNullOrEmpty(Request.QueryString["fd"]))
                {
                    rFD.SelectedDate = Convert.ToDateTime(fd);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["td"]))
                    rTD.SelectedDate = Convert.ToDateTime(td);
                List<tbl_TransportationOrder> ts = new List<tbl_TransportationOrder>();

                if (!string.IsNullOrEmpty(oc))
                {
                    ts = TransportationOrderController.GetByUIDAndPackageCode(UID, oc);
                }
                else
                {
                    ts = TransportationOrderController.GetByUID(UID);
                }

                if (stt > -1)
                {
                    if (!string.IsNullOrEmpty(fd))
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            DateTime fromdate = DateTime.Parse(fd);
                            DateTime todate = DateTime.Parse(td);
                            ts = ts.Where(o => o.Status == stt && o.CreatedDate >= fromdate && o.CreatedDate <= todate).ToList();
                        }
                        else
                        {
                            DateTime fromdate = DateTime.Parse(fd);
                            ts = ts.Where(o => o.Status == stt && o.CreatedDate >= fromdate).ToList();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            DateTime todate = DateTime.Parse(td);
                            ts = ts.Where(o => o.Status == stt && o.CreatedDate <= todate).ToList();
                        }
                        else
                        {
                            ts = ts.Where(o => o.Status == stt).ToList();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(fd))
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            DateTime fromdate = DateTime.Parse(fd);
                            DateTime todate = DateTime.Parse(td);
                            ts = ts.Where(o => o.CreatedDate >= fromdate && o.CreatedDate <= todate).ToList();
                        }
                        else
                        {
                            DateTime fromdate = DateTime.Parse(fd);
                            ts = ts.Where(o => o.CreatedDate >= fromdate).ToList();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td))
                        {
                            DateTime todate = DateTime.Parse(td);
                            ts = ts.Where(o => o.CreatedDate <= todate).ToList();
                        }
                    }
                }

                if (ts.Count > 0)
                {
                    List<Danhsachorder> os = new List<Danhsachorder>();
                    foreach (var t in ts)
                    {
                        double totalPackages = 0;
                        double totalWeight = 0;
                        int status = Convert.ToInt32(t.Status);
                        if (status < 2)
                        {
                            var od = TransportationOrderDetailController.GetByTransportationOrderID(t.ID);
                            if (od.Count > 0)
                            {
                                foreach (var o in od)
                                {
                                    totalWeight += Convert.ToDouble(o.Weight);
                                }
                            }
                            totalPackages = od.Count;
                        }
                        else
                        {
                            var smallpackages = SmallPackageController.GetByTransportationOrderID(t.ID);
                            if (smallpackages.Count > 0)
                            {
                                foreach (var s in smallpackages)
                                {
                                    totalWeight += Convert.ToDouble(s.Weight);
                                }
                            }
                            totalPackages = smallpackages.Count;
                        }
                        Danhsachorder d = new Danhsachorder();
                        d.ID = t.ID;
                        d.UID = UID;
                        d.Username = username_current;
                        d.TotalPackage = string.Format("{0:N0}", totalPackages);
                        d.TotalWeight = string.Format("{0:N0}", totalWeight);
                        d.Status = PJUtils.generateTransportationStatus(status);
                        d.CreatedDate = string.Format("{0:dd/MM/yyyy}", t.CreatedDate);
                        d.TotalPriceVND = string.Format("{0:N0}", t.TotalPrice);
                        os.Add(d);
                    }
                    pagingall(os);
                }
            }
        }
        #region Paging
        public void pagingall(List<Danhsachorder> acs)
        {

            int PageSize = 15;
            if (acs.Count > 0)
            {
                string oc = "";
                if (Request.QueryString["oc"] != null)
                {
                    oc = Request.QueryString["oc"];
                }
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
                    //int status = Convert.ToInt32(item.Status);
                    ltr.Text += "<tr>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.ID + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.TotalPackage + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.TotalPriceVND + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.TotalWeight + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.Status + "</td>";
                    var smallpackages = SmallPackageController.GetByTransportationOrderID(item.ID);
                    if(smallpackages.Count>0)
                    {
                        ltr.Text += "<td style=\"text-align: center\">";
                        ltr.Text += "   <table>";
                        //ltr.Text += "       <tr>";
                        //ltr.Text += "           <th>Mã vận đơn</th>";
                        //ltr.Text += "           <th>Trạng thái</th>";
                        //ltr.Text += "       </tr>";
                        foreach (var s in smallpackages)
                        {                            
                            ltr.Text += "       <tr>";
                            if (!string.IsNullOrEmpty(oc))
                            {
                                if(oc == s.OrderTransactionCode)
                                    ltr.Text += "           <td><span class=\"bg-yellow\">" + s.OrderTransactionCode + "</span></td>";
                                else
                                    ltr.Text += "           <td>" + s.OrderTransactionCode + "</td>";
                            }
                            else
                            {
                                ltr.Text += "           <td>" + s.OrderTransactionCode + "</td>";
                            }
                            
                            ltr.Text += "           <td>" + PJUtils.IntToStringStatusSmallPackage(Convert.ToInt32(s.Status)) + "</td>";
                            ltr.Text += "       </tr>";
                        }
                        ltr.Text += "   </table>";
                        ltr.Text += "</td>";
                    }
                    else
                    {
                        ltr.Text += "<td style=\"text-align: center\">";
                        ltr.Text += "</td>";
                    }
                    
                    ltr.Text += "<td style=\"text-align: center\">";
                    ltr.Text += "     <a href=\"/chi-tiet-don-hang-van-chuyen-ho/" + item.ID + "\" class=\"viewmore-orderlist\">Chi tiết</a>";
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
        //public void LoadData()
        //{
        //    string username_current = Session["userLoginSystem"].ToString();
        //    var obj_user = AccountController.GetByUsername(username_current);
        //    if (obj_user != null)
        //    {
        //        int UID = obj_user.ID;

        //        //Khai báo biến
        //        double tongsodonhang = 0;
        //        double tongtrigiadonhang = 0;
        //        double tongtienlayhang = 0;

        //        double tongtienhangchuagiao = 0;
        //        double Tongtienhangcandatcoc = 0;
        //        double Tongtienhangchovekhotq = 0;
        //        double Tongtienhangdavekhotq = 0;
        //        double Tongtienhangdangokhovn = 0;

        //        double order_stt0 = 0;
        //        double order_stt2 = 0;
        //        double order_stt5 = 0;
        //        double order_stt6 = 0;
        //        double order_stt7 = 0;
        //        double order_stt10 = 0;

        //        //ddlStatus.Items.Add(new ListItem("Chưa đặt cọc", "0"));
        //        //ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
        //        //ddlStatus.Items.Add(new ListItem("Chờ mua hàng", "2"));
        //        //ddlStatus.Items.Add(new ListItem("Chờ shop TQ phát hàng", "5"));
        //        //ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại TQ", "6"));
        //        //ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
        //        //ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
        //        //ddlStatus.Items.Add(new ListItem("Khách đã nhận hàng", "10"));

        //        var os = MainOrderController.GetAllByUIDNotHidden(UID);
        //        if (os != null)
        //        {
        //            if (os.Count > 0)
        //            {


        //                var orderstt0 = os.Where(od => od.Status == 0).ToList();
        //                var orderstt2 = os.Where(od => od.Status == 2).ToList();
        //                var orderstt5 = os.Where(od => od.Status == 5).ToList();
        //                var orderstt6 = os.Where(od => od.Status == 6).ToList();
        //                var orderstt7 = os.Where(od => od.Status == 7).ToList();
        //                var orderstt10 = os.Where(od => od.Status == 10).ToList();

        //                var totalorderchuagiao = os.Where(od => od.Status == 2 && od.Status == 5 && od.Status == 6 && od.Status == 7).ToList();
        //                if (totalorderchuagiao.Count > 0)
        //                {
        //                    foreach (var item in totalorderchuagiao)
        //                    {
        //                        tongtienhangchuagiao += Convert.ToDouble(item.TotalPriceVND);
        //                    }
        //                }
        //                if (orderstt0.Count > 0)
        //                {
        //                    foreach (var item in orderstt0)
        //                    {
        //                        Tongtienhangcandatcoc += Convert.ToDouble(item.TotalPriceVND);
        //                    }
        //                }
        //                if (orderstt5.Count > 0)
        //                {
        //                    foreach (var item in orderstt5)
        //                    {
        //                        Tongtienhangchovekhotq += Convert.ToDouble(item.TotalPriceVND);
        //                    }
        //                }
        //                if (orderstt6.Count > 0)
        //                {
        //                    foreach (var item in orderstt6)
        //                    {
        //                        Tongtienhangdavekhotq += Convert.ToDouble(item.TotalPriceVND);
        //                    }
        //                }
        //                if (orderstt7.Count > 0)
        //                {
        //                    foreach (var item in orderstt7)
        //                    {
        //                        Tongtienhangdangokhovn += Convert.ToDouble(item.TotalPriceVND);
        //                    }
        //                }


        //                order_stt0 = orderstt0.Count;
        //                order_stt2 = orderstt2.Count;
        //                order_stt5 = orderstt5.Count;
        //                order_stt6 = orderstt6.Count;
        //                order_stt7 = orderstt7.Count;
        //                order_stt10 = orderstt10.Count;

        //                tongsodonhang = os.Count;
        //                foreach (var o in orderstt2)
        //                {
        //                    tongtrigiadonhang += Convert.ToDouble(o.TotalPriceVND);
        //                }
        //                if (order_stt7 > 0)
        //                {
        //                    double totalall7 = 0;
        //                    double totalall7_deposit = 0;
        //                    foreach (var item in orderstt7)
        //                    {
        //                        totalall7 += Convert.ToDouble(item.TotalPriceVND);
        //                        totalall7_deposit += Convert.ToDouble(item.Deposit);
        //                    }
        //                    tongtienlayhang = totalall7 - totalall7_deposit;
        //                }

        //                DateTime checkdate = DateTime.Now;
        //                foreach (var item in os)
        //                {
        //                    if (item.Status == 0)
        //                    {
        //                        DateTime CreatedDate = Convert.ToDateTime(item.CreatedDate);
        //                        TimeSpan span = checkdate.Subtract(CreatedDate);
        //                        if (span.Days > 7)
        //                        {
        //                            MainOrderController.UpdateIsHiddenTrue(item.ID);
        //                        }
        //                    }
        //                }
        //                //Ghi ra 
        //                ltrAllOrderCount.Text = string.Format("{0:N0}", tongsodonhang).Replace(",", ".");
        //                ltrAllOrderPrice.Text = string.Format("{0:N0}", tongtrigiadonhang).Replace(",", ".");
        //                ltrTotalGetAllProduct.Text = string.Format("{0:N0}", tongtienlayhang).Replace(",", ".");

        //                ltrTongtienhangchuagiao.Text = string.Format("{0:N0}", tongtienhangchuagiao).Replace(",", ".");
        //                ltrTongtienhangcandatcoc.Text = string.Format("{0:N0}", Tongtienhangcandatcoc).Replace(",", ".");
        //                ltrTongtienhangchovekhotq.Text = string.Format("{0:N0}", Tongtienhangchovekhotq).Replace(",", ".");
        //                ltrTongtienhangdavekhotq.Text = string.Format("{0:N0}", Tongtienhangdavekhotq).Replace(",", ".");
        //                ltrTongtienhangdangokhovn.Text = string.Format("{0:N0}", Tongtienhangdangokhovn).Replace(",", ".");
        //                ltrTongtienhangcanthanhtoandelayhang.Text = string.Format("{0:N0}", tongtienlayhang).Replace(",", ".");

        //                ltrOrderStatus0.Text = string.Format("{0:N0}", order_stt0).Replace(",", ".");
        //                ltrOrderStatus2.Text = string.Format("{0:N0}", order_stt2).Replace(",", ".");
        //                ltrOrderStatus5.Text = string.Format("{0:N0}", order_stt5).Replace(",", ".");
        //                ltrOrderStatus6.Text = string.Format("{0:N0}", order_stt6).Replace(",", ".");
        //                ltrOrderStatus7.Text = string.Format("{0:N0}", order_stt7).Replace(",", ".");
        //                ltrOrderStatus10.Text = string.Format("{0:N0}", order_stt10).Replace(",", ".");

        //                //rpt.DataSource = showoff.OrderBy(o=>o.Status).ToList();
        //                //rpt.DataBind();

        //                #region filter Order
        //                List<tbl_MainOder> m = new List<tbl_MainOder>();
        //                if (Request.QueryString["trangthai"] != null && Request.QueryString["fd"] != null && Request.QueryString["td"] != null)
        //                {
        //                    int status = Request.QueryString["trangthai"].ToInt(-1);
        //                    string fd = Request.QueryString["fd"];
        //                    string td = Request.QueryString["td"];
        //                    if (status >= 0)
        //                    {
        //                        if (!string.IsNullOrEmpty(fd))
        //                        {
        //                            if (!string.IsNullOrEmpty(td))
        //                            {
        //                                m = os.Where(o => o.Status == status && o.CreatedDate >= Convert.ToDateTime(fd) && o.CreatedDate < Convert.ToDateTime(td)).ToList();
        //                            }
        //                            else
        //                            {
        //                                m = os.Where(o => o.Status == status && o.CreatedDate >= Convert.ToDateTime(fd)).ToList();
        //                                //pagingall(ordersearch.OrderByDescending(o => o.ID).ToList());
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (!string.IsNullOrEmpty(td))
        //                            {
        //                                m = os.Where(o => o.Status == status && o.CreatedDate < Convert.ToDateTime(td)).ToList();
        //                            }
        //                            else
        //                            {
        //                                m = os.Where(o => o.Status == status).ToList();
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (!string.IsNullOrEmpty(fd))
        //                        {
        //                            if (!string.IsNullOrEmpty(td))
        //                            {
        //                                m = os.Where(o => o.CreatedDate >= Convert.ToDateTime(fd) && o.CreatedDate < Convert.ToDateTime(td)).ToList();
        //                            }
        //                            else
        //                            {
        //                                m = os.Where(o => o.CreatedDate >= Convert.ToDateTime(fd)).ToList();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (!string.IsNullOrEmpty(td))
        //                            {
        //                                m = os.Where(o => o.CreatedDate < Convert.ToDateTime(td)).ToList();
        //                            }
        //                            else
        //                            {
        //                                m = os.OrderByDescending(o => o.ID).ToList();
        //                                //pagingall(listOrder.OrderByDescending(o => o.ID).ToList());
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (Request.QueryString["status"] != null)
        //                {
        //                    int status = Request.QueryString["status"].ToInt(0);
        //                    if (status != -1)
        //                    {
        //                        //var ordersearch = listOrder.Where(o => o.Status == status).ToList();
        //                        //pagingall(ordersearch.OrderByDescending(o => o.ID).ToList());
        //                    }
        //                    else
        //                    {
        //                        //pagingall(listOrder.OrderByDescending(o => o.ID).ToList());
        //                    }
        //                }
        //                else
        //                {
        //                    //pagingall(listOrder.OrderByDescending(o => o.ID).ToList());
        //                }
        //                if (m.Count > 0)
        //                {
        //                    List<Danhsachorder> showoff = new List<Danhsachorder>();
        //                    foreach (var item in m)
        //                    {
        //                        string image = "";
        //                        var pros = OrderController.GetByMainOrderID(item.ID);
        //                        if (pros.Count > 0)
        //                        {
        //                            image = pros[0].image_origin;
        //                        }
        //                        Danhsachorder d = new Danhsachorder();
        //                        d.ID = item.ID;
        //                        d.ProductImage = image;
        //                        d.ShopID = item.ShopID;
        //                        d.ShopName = item.ShopName;
        //                        d.Site = item.Site;
        //                        d.TotalPriceVND = item.TotalPriceVND;
        //                        d.AmountDeposit = item.AmountDeposit;
        //                        d.Deposit = item.Deposit;
        //                        d.UID = item.UID.ToString().ToInt();
        //                        d.CreatedDate = item.CreatedDate.ToString();
        //                        d.username = AccountController.GetByID(Convert.ToInt32(item.UID)).Username;
        //                        d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                        showoff.Add(d);
        //                    }
        //                }

        //                gr.DataSource = m;
        //                gr.DataBind();
        //                #endregion
        //            }
        //        }
        //    }
        //}

        public class Danhsachorder
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string TotalPackage { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPriceVND { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
            public string Username { get; set; }
        }

        protected void btnSear_Click(object sender, EventArgs e)
        {
            string ordercode = txtOrderCode.Text;
            string status = ddlStatus.SelectedValue;
            string fd = "";
            string td = "";

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
                    Response.Redirect("/danh-sach-don-van-chuyen-ho?oc=" + ordercode + "&stt=" + status + "&fd=" + fd + "&td=" + td + "");
                }
            }
            else
            {
                Response.Redirect("/danh-sach-don-van-chuyen-ho?oc=" + ordercode + "&stt=" + status + "&fd=" + fd + "&td=" + td + "");
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
                                orderdeposited = Convert.ToDouble(o.Deposit);

                            if (!string.IsNullOrEmpty(o.AmountDeposit))
                                amountdeposit = Convert.ToDouble(o.AmountDeposit);

                            double userwallet = Convert.ToDouble(obj_user.Wallet);
                            if (userwallet > 0)
                            {
                                if (orderdeposited > 0)
                                {
                                    if (amountdeposit >= orderdeposited)
                                    {
                                        double depleft = amountdeposit - orderdeposited;
                                        if (userwallet >= depleft)
                                        {
                                            double wallet = userwallet - depleft;
                                            //Cập nhật lại MainOrder                                
                                            MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                            MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                            HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, depleft,
                                                obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                            AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                            PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, depleft, 2, currentDate, obj_user.Username);

                                        }
                                        else
                                        {
                                            double walletleft = depleft - userwallet;
                                            double newpay = orderdeposited + userwallet;
                                            MainOrderController.UpdateDeposit(o.ID, obj_user.ID, newpay.ToString());
                                            HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                                            AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                                            PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                                        }
                                    }
                                }
                                else
                                {
                                    if (userwallet >= amountdeposit)
                                    {
                                        //Cập nhật lại Wallet User
                                        double wallet = userwallet - amountdeposit;
                                        //Cập nhật lại MainOrder                                
                                        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);
                                    }
                                    else
                                    {
                                        double paid = amountdeposit - userwallet;
                                        //Cập nhật lại MainOrder                            
                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, userwallet.ToString());
                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                                        AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                                    }
                                }
                                PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                    "e", true, Page);
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

        #region Button status
        protected void btn0_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 0;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 1;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 2;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn5_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 5;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn6_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 6;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn7_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 7;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn9_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 9;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void btn10_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = 10;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }

        protected void bttnAll_Click(object sender, EventArgs e)
        {
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            int status = -1;
            Response.Redirect("/danh-sach-don-hang?fd=" + fd + "&td=" + td + "&stt=" + status + "");
        }
        #endregion
    }
}