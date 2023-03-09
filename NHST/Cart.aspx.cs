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


namespace NHST
{
    public partial class Cart1 : System.Web.UI.Page
    {
        private readonly NHSTEntities _context = new NHSTEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    loaddata();
                    LoadQuery();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadQuery()
        {
            if (Session["linksearch"] != null)
            {
                string link = Session["linksearch"].ToString();
                txt_link.Text = link;
                Session.Remove("linksearch");
                loadProduct(link);
            }
        }

        public void loaddata()
        {
            string username_current = Session["userLoginSystem"].ToString();
            TableSql tb = new TableSql(_context);

            var obj_user = AccountController.GetByUsername(username_current);
            int UID = 0;
            double currency = 0;
            double feeBuyPro = 0;

            if (obj_user != null)
            {
                UID = obj_user.ID;
                if (!string.IsNullOrEmpty(obj_user.Currency))
                {
                    if (obj_user.Currency.ToFloat(0) > 0)
                    {
                        currency = Convert.ToDouble(obj_user.Currency);
                    }
                }
            }
            //List<tbl_OrderTemp> os = OrderTempController.GetAllByUID(UID);
            string html = "";

            double pricecurrency = 0;
            if (currency > 0)
                pricecurrency = currency;
            else
                pricecurrency = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);

            double pricefullcart = 0;
            int totalproduct = 0;
            //int countshop = 0;

            var totalall = tb.getTotalCartByUID(UID);
            //int page = 0;
            int PageSize = 20;

            int TotalItems = totalall != null ? totalall.TotalShop.Value : 0;
            if (TotalItems % PageSize == 0)
                PageCount = TotalItems / PageSize;
            else
                PageCount = TotalItems / PageSize + 1;

            Int32 Page = GetIntFromQueryString("Page");

            if (Page == -1) Page = 1;
            int FromRow = (Page - 1) * PageSize;
            //int ToRow = Page * PageSize - 1;
            //if (ToRow >= TotalItems)
            //    ToRow = TotalItems - 1;


            var oshop = OrderShopTempController.GetByUID(UID).Skip(FromRow).Take(PageSize).ToList();
            if (oshop != null)
            {
                if (oshop.Count > 0)
                {

                    //countshop = oshop.Count();
                    string listorderid = "";
                    foreach (var shop in oshop)
                    {
                        double TotalPriceProductInShop = 0;
                        double TotalPriceShop = 0;
                       // listorderid += shop.ID + "|";

                        html += "<div class=\"table-panel\">";
                        html += "   <div class=\"table-panel-header\">";
                        html += "       <h4 class=\"title\">" + shop.ShopName + "</h4>";
                        html += "       <div class=\"delivery-opt\">";
                        if (shop.IsFastDelivery == true)
                        {
                            html += "           <label><input type=\"checkbox\" name=\"delivery_opt_1\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsFastDelivery')\" checked><span class=\"ip-avata\"></span> <span class=\"ip-label\">Giao tận nhà</span></label>";
                        }
                        else
                        {
                            html += "           <label><input type=\"checkbox\" name=\"delivery_opt_1\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsFastDelivery')\"><span class=\"ip-avata\"></span> <span class=\"ip-label\">Giao tận nhà</span></label>";
                        }
                        if (shop.IsCheckProduct == true)
                        {
                            html += "           <label><input type=\"checkbox\" name=\"delivery_opt_1\" id=\"" + shop.ID + "_checkproductselect\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsCheckProduct')\" checked><span class=\"ip-avata\"></span> <span class=\"ip-label\">Kiểm hàng</span></label>";
                        }
                        else
                        {
                            html += "           <label><input type=\"checkbox\" name=\"delivery_opt_1\" id=\"" + shop.ID + "_checkproductselect\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsCheckProduct')\"><span class=\"ip-avata\"></span> <span class=\"ip-label\">Kiểm hàng</span></label>";
                        }
                        if (shop.IsPacked == true)
                        {
                            html += "           <label><input type=\"checkbox\" name=\"delivery_opt_1\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsPacked')\" checked><span class=\"ip-avata\"></span> <span class=\"ip-label\">Đóng gỗ</span></label>";
                        }
                        else
                        {
                            html += "           <label><input type=\"checkbox\" name=\"delivery_opt_1\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsPacked')\"><span class=\"ip-avata\"></span> <span class=\"ip-label\">Đóng gỗ</span></label>";
                        }
                        html += "       </div>";
                        html += "   </div>";
                        html += "   <div class=\"table-panel-main custom-flex-1\">";
                        html += "       <div class=\"delivery-opt custom-flex-2\" style=\"float:left;width: 10%;\" data-item-id=\"" + shop.ID + "\">";
                        html += "           <label><input type=\"checkbox\" name=\"delivery_opt_1\" onclick=\"getCartToSuccess($(this))\"><span class=\"ip-avata\"></span></label>";
                        html += "       </div>";
                        html += "       <table style=\"float:left;width:90%;\">";
                        html += "           <tr>";
                        //html += "               <th class=\"check\"><label class=\"checklb\"><input type=\"checkbox\"><span class=\"ip-avata\"></span></label></th>";
                        html += "               <th class=\"img\">Sản phẩm</th>";
                        html += "               <th class=\"qty\">Thuộc tính</th>";
                        html += "               <th class=\"qty\">Số lượng</th>";
                        html += "               <th class=\"price\">Đơn giá</th>";
                        html += "               <th class=\"total\">Tiền hàng</th>";
                        html += "               <th class=\"total\">Xóa</th>";
                        html += "           </tr>";
                        var ors = tb.GetAllByOrderShopTempIDAndUID(UID, shop.ID);//OrderTempController.GetAllByOrderShopTempIDAndUID(UID, shop.ID);
                        if (ors != null)
                        {
                            if (ors.Count > 0)
                            {
                                foreach (var item in ors)
                                {
                                    totalproduct += 1;
                                    int ID = item.ID;
                                    string linkproduct = item.link_origin;
                                    string productname = item.title_origin;
                                    string brand = item.brand;
                                    string image = item.image_origin;
                                    int quantity = Convert.ToInt32(item.quantity);
                                    double originprice = Convert.ToDouble(item.price_origin);
                                    double promotionprice = Convert.ToDouble(item.price_promotion);
                                    double u_pricecbuy = 0;
                                    double u_pricevn = 0;
                                    double e_pricebuy = 0;
                                    double e_pricevn = 0;
                                    double e_pricetemp = 0;
                                    double e_totalproduct = 0;
                                    if (promotionprice > 0)
                                    {
                                        if (promotionprice < originprice)
                                        {
                                            u_pricecbuy = promotionprice;
                                            u_pricevn = promotionprice * pricecurrency;
                                        }
                                        else
                                        {
                                            u_pricecbuy = originprice;
                                            u_pricevn = originprice * pricecurrency;
                                        }
                                    }
                                    else
                                    {
                                        u_pricecbuy = originprice;
                                        u_pricevn = originprice * pricecurrency;
                                    }
                                    e_pricebuy = u_pricecbuy * quantity;
                                    e_pricevn = u_pricevn * quantity;

                                    e_totalproduct = e_pricevn + e_pricetemp;

                                    TotalPriceProductInShop += e_pricevn;
                                    TotalPriceShop += e_totalproduct;

                                    //pricefullcart += e_totalproduct;

                                    if (image.Contains("%2F"))
                                    {
                                        image = image.Replace("%2F", "/");
                                    }
                                    if (image.Contains("%3A"))
                                    {
                                        image = image.Replace("%3A", ":");
                                    }

                                    html += "           <tr>";
                                    html += "               <td class=\"img\">";
                                    html += "                   <div class=\"thumb-product\">";
                                    html += "                       <div class=\"pd-img\" style=\"display:block; width:100%\" ><img style=\"width:50%\" src=\"" + image + "\" alt=\"\"></div>";
                                    html += "                       <div class=\"info\" style=\"display:block; width:100%;clear:both\" ><a href=\"" + linkproduct + "\" target=\"_blank\">" + productname + "</a></div>";
                                    html += "                   </div>";
                                    html += "                   <div class=\"clearfix\"></div>";
                                    html += "                   <div class=\"brand-name-product\" data-parent=\"" + shop.ID + "\">";
                                    html += "                       <input type=\"text\" id=\"note_" + ID + "\" class=\"form-control notebrand\" value=\"" + brand + "\" placeholder=\"Ghi chú riêng sản phẩm\" data-item-id=\"" + ID + "\">";
                                    html += "                   </div>";
                                    html += "               </td>";
                                    html += "               <td class=\"qty\">" + item.property + "</td>";
                                    //html += "               <td class=\"qty\"><input type=\"number\" value=\"" + quantity + "\" class=\"form-control\" min=\"1\"  onkeyup=\"updatequantity('" + ID + "',$(this),'" + shop.ID + "')\" onmouseup=\"updatequantity('" + item.ID + "',$(this))\" ></td>";
                                    html += "               <td class=\"qty\"><input type=\"number\" value=\"" + quantity + "\" class=\"quantitiofpro form-control\" min=\"1\" >"
                                         + "                    <br/><a href=\"javascript:;\" style=\"width:100%;margin-top:5px;\" class=\"pill-btn btn btn-search primary-btn custom-color\" onclick=\"updatequantity('" + ID + "',$(this),'" + shop.ID + "')\"><i class=\"fa fa-refresh\"></i</a>"
                                         + "                </td>";
                                    html += "               <td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", u_pricevn) + "đ</p><p class=\"\">¥" + u_pricecbuy + "</p></td>";
                                    html += "               <td class=\"total\"><p class=\"\">" + string.Format("{0:N0}", e_pricevn) + "đ</p><p class=\"\">¥" + e_pricebuy + "</p></td>";
                                    html += "               <td class=\"total\"><p class=\"\"><a href=\"javascript:;\" onclick=\"deleteordertemp('" + ID + "','" + shop.ID + "')\"><i class=\"fa fa-trash\"></i></a></td>";
                                    html += "           </tr>";
                                    //html += "           <tr><td colspan=\"5\" class=\"note-td\"><textarea class=\"form-control note\" placeholder=\"Chú thích đơn hàng\"></textarea></td></tr>";
                                    html += "           <tr class=\"hover-tr\"><td colspan=\"5\" class=\"hover-td\"><div class=\"hover-block\"><a href=\"javascript:;\" onclick=\"deleteordershoptemp('" + shop.ID + "')\"><i class=\"fa fa-trash\"></i></a></div></td></tr>";
                                }
                            }
                        }
                        hdfallorderid.Value = listorderid;
                        html += "       </table>";
                        html += "   </div>";
                        html += "   <div class=\"table-panel-total\">";
                        html += "       <table>";
                        html += "           <tr><td>Tiền hàng</td><td><strong>" + string.Format("{0:N0}", TotalPriceProductInShop) + "</strong></td></tr>";
                        //html += "           <tr><td>Phí tạm tính</td><td><strong>0</strong></td></tr>";
                        html += "           <tr><td><strong>Tổng tính</strong></td><td><strong class=\"hl-txt\" id=\"priceVND_" + shop.ID + "\" data-price=\"" + TotalPriceShop + "\">" + string.Format("{0:N0}", TotalPriceShop) + " đ</strong></td></tr>";
                        html += "       </table>";
                        if (!string.IsNullOrEmpty(shop.Note))
                        {
                            html += "       <div class=\"note-block\"><textarea id=\"order_temp_" + shop.ID + "\" class=\"form-control note\" placeholder=\"Chú thích đơn hàng\">" + shop.Note + "</textarea></div>";
                        }
                        else
                        {
                            html += "       <div class=\"note-block\"><textarea id=\"order_temp_" + shop.ID + "\" class=\"form-control note\" placeholder=\"Chú thích đơn hàng\"></textarea></div>";
                        }
                        html += "       <div class=\"clearfix\"></div>";
                        //if (shop.IsFast == true)
                        //    html += "       <div class=\"check chk_isfast mar-top1 mar-bot1\"><label class=\"checklb\"><input id=\"chk_fast_" + shop.ID + "\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsFast')\" type=\"checkbox\" checked><span class=\"ip-avata border-dark\"></span>Đơn hàng hỏa tốc</label></div>";
                        //else
                        //    html += "       <div class=\"check chk_isfast mar-top1 mar-bot1\"><label class=\"checklb\"><input id=\"chk_fast_" + shop.ID + "\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsFast')\" type=\"checkbox\"><span class=\"ip-avata border-dark\"></span>Đơn hàng hỏa tốc</label></div>";
                        html += "       <div class=\"btn-wrap\"><a href=\"javascript:;\" class=\"pill-btn btn order-btn main-btn hover\" onclick=\"checkout('" + shop.ID + "')\">ĐẶT HÀNG</a></div>";
                        html += "   </div>";
                        html += "</div>";
                        pricefullcart += TotalPriceShop;
                    }
                    pricefullcart = (double)totalall.PriceNNT * pricecurrency;
                    //Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
                    //if (CurrentPage == -1) CurrentPage = 1;
                    string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };

                    html += GetHtmlPagingAdvanced(6, Page, PageCount, Context.Request.RawUrl, strText);
                    ltr_cart.Text = html;
                    ltr_sub.Text = " <div class=\"left\"><span class=\"hl-txt\">" + totalall.TotalShop + "</span> Shop  /  <span class=\"hl-txt\">" + totalall.TotalProduct + "</span> Sản phẩm  /  <span class=\"hl-txt\">" + string.Format("{0:N0}", pricefullcart) + " vnđ</span> Tiền Hàng</div>";
                    ltr_total_pay.Text += "<div class=\"table-price-total\">";
                    ltr_total_pay.Text += " <div class=\"right\">";
                    ltr_total_pay.Text += "     <p class=\"final-total\">Tổng tính <strong class=\"hl-txt\">" + string.Format("{0:N0}", pricefullcart) + "</strong><span class=\"hl-txt\">vnđ</span></p>";
                    //ltr_total_pay.Text += "     <a href=\"javascript:;\" class=\"pill-btn btn order-btn main-btn hover\" onclick=\"checkoutAll('" + listorderid + "')\">ĐẶT HÀNG " + totalproduct + " SẢN PHẨM ĐÃ CHỌN</a>";
                    ltr_total_pay.Text += "     <a href=\"javascript:;\" class=\"pill-btn btn order-btn main-btn hover\" onclick=\"checkoutAll('" + totalall.listorderid + "')\">ĐẶT HÀNG TẤT CẢ ĐƠN HÀNG</a>";
                    ltr_total_pay.Text += "     <a href=\"javascript:;\" class=\"pill-btn btn order-btn getallOrder\" onclick=\"checkoutAllSelect()\">ĐẶT HÀNG <span class=\"numofOrder\">" + totalproduct + "</span> ĐƠN HÀNG ĐÃ CHỌN</a>";
                    ltr_total_pay.Text += " </div>";
                    ltr_total_pay.Text += "</div>";
                    pn_search.Visible = true;
                }
                else
                {
                    pn_search.Visible = false;
                    ltr_cart.Text = "Hiện tại không có sản phẩm nào trong giỏ hàng của bạn.";
                }
            }
            else
            {
                pn_search.Visible = false;
                ltr_cart.Text = "Hiện tại không có sản phẩm nào trong giỏ hàng của bạn.";
            }
        }

        public void GetData(string url)
        {
            WebClient w = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string pageSource = w.DownloadString(url);
            ltr_content.Text = pageSource;
        }

        public void loadProduct(string link)
        {
            if (link.Contains("m.intl"))
            {
                Uri linkpro = new Uri(link);
                string idpro = HttpUtility.ParseQueryString(linkpro.Query).Get("id");
                string spm = HttpUtility.ParseQueryString(linkpro.Query).Get("spm");
                string orderlink = "https://world.taobao.com/item/" + idpro + ".htm?spm=" + spm + "";
                loadProduct1(orderlink);
            }
            else
            {
                string httplink = "";
                if (link.Contains("https"))
                    httplink = "https";
                else
                    httplink = "http";

                pn_productview.Visible = false;
                if (!string.IsNullOrEmpty(link))
                {

                    var request = (HttpWebRequest)WebRequest.Create(link);
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
                    request.Method = "GET";


                    var content = String.Empty;
                    HttpStatusCode statusCode;
                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        var contentType = response.ContentType;
                        Encoding encoding = null;
                        if (contentType != null)
                        {
                            var match = Regex.Match(contentType, @"(?<=charset\=).*");
                            if (match.Success)
                                encoding = Encoding.GetEncoding(match.ToString());
                        }

                        encoding = encoding ?? Encoding.UTF8;

                        statusCode = ((HttpWebResponse)response).StatusCode;
                        using (var reader = new StreamReader(stream, encoding))
                            content = reader.ReadToEnd();
                    }
                    var doc = Dcsoup.Parse(content);


                    var scoreDiv = doc.Select("html");
                    #region Taobao
                    if (link.Contains(".taobao.com/"))
                    {
                        if (link.Contains("item.taobao.com"))
                        {

                            //ltr_content.Text = scoreDiv.Html;                        
                            var anofollow = scoreDiv.Select("a[rel=nofollow]");
                            //if(anofollow.HasClass("tb-main-pic"))
                            //{
                            //    anofollow = anofollow.Select("a[rel=no-follow]");
                            //}
                            var href = anofollow.Attr("href");

                            var img = scoreDiv.Select("img[id=J_ImgBooth]");
                            var imgsrc = img.Attr("src");

                            var title = scoreDiv.Select("h3[class=tb-main-title]").Text;

                            //var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                            string origin_price = scoreDiv.Select("input[name=current_price]").Val;
                            //if (span_origin_price.Attr("itemprop") == "lowPrice")
                            //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                            //else if (span_origin_price.Attr("itemprop") == "price")
                            //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                            var promotion_price_div = scoreDiv.Select("input[name=current_price]").Val;
                            string promotion_price = "";
                            promotion_price = origin_price;

                            //var seller_link = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");
                            string itemID = scoreDiv.Select("input[name=item_id]").Val;
                            string shopID = "";
                            string seller_id = "0";
                            if (!string.IsNullOrEmpty(href))
                            {
                                Uri myUri = new Uri(httplink + ":" + href.ToString());
                                //itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                                shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                            }
                            seller_id = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");

                            var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");

                            var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                                .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                            var listmaterial = scoreDiv.Select("div[class=tb-skin]").Select("dl");

                            var attribute = scoreDiv.Select("div[id=attributes]").Html;
                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tb-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            ltr_material.Text += liitem.Html;
                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        //ltr_material.Text += item.Select("dd").Html;

                                        ltr_material.Text += "</div>";
                                    }
                                }
                            }

                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = origin_price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "taobao_" + shopID;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = seller_id;
                            ltr_wangwang.Value = wangwang;
                            ltr_stock.Value = "";
                            ltr_location_sale.Value = "";
                            ltr_site.Value = "TAOBAO";
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;

                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            hdf_product_ok.Value = "ok";
                            pn_productview.Visible = true;
                        }
                        else
                        {
                            //ltr_content.Text = scoreDiv.Html;
                            //var scoreDiv = doc.Select("div[class=sea-detail-bd]");
                            var anofollow = scoreDiv.Select("a[rel=no-follow]");
                            var href = anofollow.Attr("href");

                            var img = anofollow.Select("img[id=J_ThumbView]");
                            var imgsrc = img.Attr("src");

                            var title = img.Attr("alt");

                            var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                            string origin_price = "0";
                            if (span_origin_price.Attr("itemprop") == "lowPrice")
                                origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                            else if (span_origin_price.Attr("itemprop") == "price")
                                origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                            var promotion_price_div = scoreDiv.Select("div[id=J_PromoPrice]");
                            string promotion_price = "";
                            promotion_price = promotion_price_div.Select("strong[class=tb-rmb-num]").Text;

                            var seller_link = scoreDiv.Select("button[id=J_listBuyerOnView]").Attr("data-api");
                            string itemID = "0";
                            string shopID = "0";
                            string seller_id = "0";
                            if (!string.IsNullOrEmpty(href))
                            {
                                Uri myUri = new Uri(httplink + ":" + href.ToString());
                                itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                                shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                            }
                            if (!string.IsNullOrEmpty(seller_link))
                            {
                                Uri seller = new Uri(httplink + ":" + seller_link);
                                seller_id = HttpUtility.ParseQueryString(seller.Query).Get("seller_num_id");
                            }



                            var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");



                            var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                                .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                            var listmaterial = scoreDiv.Select("div[class=item-sku]").Select("dl");

                            var attribute = scoreDiv.Select("div[id=attributes]").Html;
                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-pv") + "\">";
                                        ltr_material.Text += liitem.Html;
                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    //ltr_material.Text += item.Select("dd").Html;

                                    ltr_material.Text += "</div>";
                                }
                            }

                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = origin_price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "taobao_" + shopID;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = seller_id;
                            ltr_wangwang.Value = wangwang;
                            ltr_stock.Value = "";
                            ltr_location_sale.Value = "";
                            ltr_site.Value = "TAOBAO";
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;

                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            hdf_product_ok.Value = "ok";
                            pn_productview.Visible = true;
                        }

                    }
                    #endregion
                    #region 1688
                    else if (link.Contains(".1688.com/"))
                    {
                        //ltr_content.Text = scoreDiv.Html.ToString();
                        if (link.Contains("detail.1688.com"))
                        {

                            string fullcontent = scoreDiv.Html.ToString();
                            string price_ref = PJUtils.getBetween(fullcontent, "convertPrice", ",");
                            string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");
                            string shopID = PJUtils.getBetween(fullcontent, "userId", ",");
                            string itemID = PJUtils.getBetween(fullcontent, "'offerid'", ",");

                            //Title
                            var title = scoreDiv.Select("h1[class=d-title]").Text;

                            //Price
                            string price = scoreDiv.Select("td[class=price]")[0].Select("em[class=value]").Text;

                            //Image
                            var imgsrc = scoreDiv.Select("div[class=tab-pane]").Select("img").Attr("src");

                            //ShopID
                            string shopid = shopID.Replace(":", "").Replace("\"", "").Replace("'", "");

                            //ShopName
                            string shopname = scoreDiv.Select("a[class=company-name]").Text;

                            //Wangwang
                            string wangwang = shopname;

                            //Site
                            string site = "1688";

                            //ItemID
                            itemID = itemID.Replace("'", "").Replace(":", "");

                            //attribute
                            var attribute = scoreDiv.Select("div[id=mod-detail-attributes]").Html;

                            //Material
                            var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tm-sale-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            var atag = liitem.Select("a");
                                            if (atag.HasAttr("style"))
                                            {
                                                ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                            }
                                            else
                                            {
                                                ltr_material.Text += "<a>" + atag.Html + "</a>";
                                            }

                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        ltr_material.Text += "</div>";
                                    }

                                }
                            }



                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title + " - " + itemID;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = shopid;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = shopid;
                            ltr_wangwang.Value = wangwang;
                            //ltr_stock.Value = "";
                            //ltr_location_sale.Value = "";
                            ltr_site.Value = site;
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;
                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            pn_productview.Visible = true;
                        }
                    }
                    #endregion
                    #region Tmall
                    else if (link.Contains(".tmall.com/") || link.Contains(".tmall.hk/"))
                    {
                        //ltr_content.Text = scoreDiv.Html;
                        if (link.Contains("world.tmall.com"))
                        {
                            string fullcontent = scoreDiv.Html.ToString();
                            string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");
                            string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");


                            //Title
                            var title = scoreDiv.Select("input[name=title]").Val;

                            //Price
                            string origin_price = returntext.Replace(":", "");
                            origin_price = origin_price.Replace("\"", "");
                            string finlaprice = "";
                            string[] oarray = new string[] { };

                            if (origin_price.Contains("-"))
                            {
                                oarray = origin_price.Split('-');
                                finlaprice = oarray[0];
                            }
                            else
                            {
                                finlaprice = origin_price;
                            }
                            //Image
                            var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                            //ShopID
                            string shopid = scoreDiv.Select("div[id=LineZing]").Attr("shopid");

                            //ShopName
                            string shopname = scoreDiv.Select("input[name=seller_nickname]").Val;

                            //Wangwang
                            string wangwang = shopname;

                            //Site
                            string site = "TMALL";

                            //ItemID
                            string itemID = scoreDiv.Select("div[id=LineZing]").Attr("itemid");

                            //attribute
                            var attribute = scoreDiv.Select("div[id=attributes]").Html;

                            //Material
                            var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tm-sale-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            var atag = liitem.Select("a");
                                            if (atag.HasAttr("style"))
                                            {
                                                ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                            }
                                            else
                                            {
                                                ltr_material.Text += "<a>" + atag.Html + "</a>";
                                            }

                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        ltr_material.Text += "</div>";
                                    }

                                }
                            }



                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = finlaprice;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + finlaprice + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(finlaprice) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "tmall_" + shopid;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = sellerID;
                            ltr_wangwang.Value = wangwang;
                            //ltr_stock.Value = "";
                            //ltr_location_sale.Value = "";
                            ltr_site.Value = site;
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;
                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            pn_productview.Visible = true;
                        }
                        else
                        {
                            string fullcontent = scoreDiv.Html.ToString();
                            string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");

                            //Title
                            var title = scoreDiv.Select("div[class=tb-detail-hd]").Select("h1").Text;

                            //Price
                            string origin_price = returntext.Replace(":", "");
                            origin_price = origin_price.Replace("\"", "");

                            //Property

                            //Value

                            //Image
                            var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                            //ShopID
                            string shopidLink = scoreDiv.Select("a[id=xshop_collection_href]").Attr("href");
                            string shopid = "";
                            if (!string.IsNullOrEmpty(shopidLink))
                            {
                                Uri shopidLinkget = new Uri(httplink + ":" + shopidLink.ToString());
                                shopid = HttpUtility.ParseQueryString(shopidLinkget.Query).Get("id");
                            }

                            //ShopName
                            var shopname = scoreDiv.Select("input[name=seller_nickname]").Attr("value");

                            //Seller ID
                            string selleridlink = scoreDiv.Select("div[id=J_SellerInfo]").Attr("data-url");
                            Uri selleriddetach = new Uri(httplink + ":" + selleridlink.ToString());
                            string sellerid = HttpUtility.ParseQueryString(selleriddetach.Query).Get("user_num_id");

                            //Wangwang
                            string wangwang = shopname;

                            //Site
                            string site = "TMALL";

                            //ItemID
                            Uri itemIDLink = new Uri(link.ToString());
                            string itemID = HttpUtility.ParseQueryString(itemIDLink.Query).Get("id");

                            //Origin Link
                            string originlink = link;

                            //Outer ID
                            string outerid = HttpUtility.ParseQueryString(itemIDLink.Query).Get("skuID");

                            //attribute
                            var attribute = scoreDiv.Select("div[id=attributes]").Html;

                            //Material
                            var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tm-sale-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            var atag = liitem.Select("a");
                                            if (atag.HasAttr("style"))
                                            {
                                                ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                            }
                                            else
                                            {
                                                ltr_material.Text += "<a>" + atag.Html + "</a>";
                                            }

                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        ltr_material.Text += "</div>";
                                    }

                                }
                            }

                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = origin_price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "tmall_" + shopid;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = sellerid;
                            ltr_wangwang.Value = wangwang;
                            ltr_stock.Value = "";
                            ltr_location_sale.Value = "";
                            ltr_site.Value = site;
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;
                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            pn_productview.Visible = true;
                        }
                    }
                    #endregion
                }
                else
                {
                    hdf_product_ok.Value = "fail";
                }
            }
        }

        public void loadProduct1(string link)
        {
            //txt_link.Text = link;           
            string httplink = "";
            if (link.Contains("https"))
                httplink = "https";
            else
                httplink = "http";

            pn_productview.Visible = false;
            if (!string.IsNullOrEmpty(link))
            {

                var request = (HttpWebRequest)WebRequest.Create(link);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
                request.Method = "GET";


                var content = String.Empty;
                HttpStatusCode statusCode;
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {

                    var contentType = response.ContentType;
                    Encoding encoding = null;
                    if (contentType != null)
                    {
                        var match = Regex.Match(contentType, @"(?<=charset\=).*");
                        if (match.Success)
                            encoding = Encoding.GetEncoding(match.ToString());
                    }

                    encoding = encoding ?? Encoding.UTF8;

                    statusCode = ((HttpWebResponse)response).StatusCode;
                    using (var reader = new StreamReader(stream, encoding))
                        content = reader.ReadToEnd();
                }
                var doc = Dcsoup.Parse(content);


                var scoreDiv = doc.Select("body");
                if (link.Contains(".taobao.com/"))
                {
                    if (link.Contains("item.taobao.com"))
                    {

                        //ltr_content.Text = scoreDiv.Html;                        
                        var anofollow = scoreDiv.Select("a[rel=nofollow]");
                        //if(anofollow.HasClass("tb-main-pic"))
                        //{
                        //    anofollow = anofollow.Select("a[rel=no-follow]");
                        //}
                        var href = anofollow.Attr("href");

                        var img = scoreDiv.Select("img[id=J_ImgBooth]");
                        var imgsrc = img.Attr("src");

                        var title = scoreDiv.Select("h3[class=tb-main-title]").Text;

                        //var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                        string origin_price = scoreDiv.Select("input[name=current_price]").Val;
                        //if (span_origin_price.Attr("itemprop") == "lowPrice")
                        //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                        //else if (span_origin_price.Attr("itemprop") == "price")
                        //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                        var promotion_price_div = scoreDiv.Select("input[name=current_price]").Val;
                        string promotion_price = "";
                        promotion_price = origin_price;

                        //var seller_link = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");
                        string itemID = scoreDiv.Select("input[name=item_id]").Val;
                        string shopID = "";
                        string seller_id = "0";
                        if (!string.IsNullOrEmpty(href))
                        {
                            Uri myUri = new Uri(httplink + ":" + href.ToString());
                            //itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                            shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                        }
                        seller_id = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");

                        var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");



                        var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                            .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                        var listmaterial = scoreDiv.Select("div[class=tb-skin]").Select("dl");

                        var attribute = scoreDiv.Select("div[id=attributes]").Html;
                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tb-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        ltr_material.Text += liitem.Html;
                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    //ltr_material.Text += item.Select("dd").Html;

                                    ltr_material.Text += "</div>";
                                }

                            }
                        }

                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = origin_price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "taobao_" + shopID;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = seller_id;
                        ltr_wangwang.Value = wangwang;
                        ltr_stock.Value = "";
                        ltr_location_sale.Value = "";
                        ltr_site.Value = "TAOBAO";
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;

                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        hdf_product_ok.Value = "ok";
                        pn_productview.Visible = true;
                    }
                    else
                    {
                        //ltr_content.Text = scoreDiv.Html;
                        //var scoreDiv = doc.Select("div[class=sea-detail-bd]");
                        var anofollow = scoreDiv.Select("a[rel=no-follow]");
                        var href = anofollow.Attr("href");

                        var img = anofollow.Select("img[id=J_ThumbView]");
                        var imgsrc = img.Attr("src");

                        var title = img.Attr("alt");

                        var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                        string origin_price = "0";
                        if (span_origin_price.Attr("itemprop") == "lowPrice")
                            origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                        else if (span_origin_price.Attr("itemprop") == "price")
                            origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                        var promotion_price_div = scoreDiv.Select("div[id=J_PromoPrice]");
                        string promotion_price = "";
                        promotion_price = promotion_price_div.Select("strong[class=tb-rmb-num]").Text;

                        var seller_link = scoreDiv.Select("button[id=J_listBuyerOnView]").Attr("data-api");
                        string itemID = "0";
                        string shopID = "0";
                        string seller_id = "0";
                        if (!string.IsNullOrEmpty(href))
                        {
                            Uri myUri = new Uri(httplink + ":" + href.ToString());
                            itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                            shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                        }
                        if (!string.IsNullOrEmpty(seller_link))
                        {
                            Uri seller = new Uri(httplink + ":" + seller_link);
                            seller_id = HttpUtility.ParseQueryString(seller.Query).Get("seller_num_id");
                        }



                        var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");



                        var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                            .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                        var listmaterial = scoreDiv.Select("div[class=item-sku]").Select("dl");

                        var attribute = scoreDiv.Select("div[id=attributes]").Html;
                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                var listmate = item.Select("dd").Select("ul").Select("li");
                                ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                foreach (var liitem in listmate)
                                {
                                    ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-pv") + "\">";
                                    ltr_material.Text += liitem.Html;
                                    ltr_material.Text += "</li>";
                                }
                                ltr_material.Text += "</ul>";
                                //ltr_material.Text += item.Select("dd").Html;

                                ltr_material.Text += "</div>";
                            }
                        }

                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = origin_price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "taobao_" + shopID;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = seller_id;
                        ltr_wangwang.Value = wangwang;
                        ltr_stock.Value = "";
                        ltr_location_sale.Value = "";
                        ltr_site.Value = "TAOBAO";
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;

                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        hdf_product_ok.Value = "ok";
                        pn_productview.Visible = true;
                    }

                }
                else if (link.Contains(".1688.com/"))
                {
                    //ltr_content.Text = scoreDiv.Html.ToString();
                    if (link.Contains("detail.1688.com"))
                    {

                        string fullcontent = scoreDiv.Html.ToString();
                        string price_ref = PJUtils.getBetween(fullcontent, "convertPrice", ",");
                        string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");
                        string shopID = PJUtils.getBetween(fullcontent, "userId", ",");
                        string itemID = PJUtils.getBetween(fullcontent, "'offerid'", ",");

                        //Title
                        var title = scoreDiv.Select("h1[class=d-title]").Text;

                        //Price
                        string price = scoreDiv.Select("td[class=price]")[0].Select("em[class=value]").Text;

                        //Image
                        var imgsrc = scoreDiv.Select("div[class=tab-pane]").Select("img").Attr("src");

                        //ShopID
                        string shopid = shopID.Replace(":", "").Replace("\"", "").Replace("'", "");

                        //ShopName
                        string shopname = scoreDiv.Select("a[class=company-name]").Text;

                        //Wangwang
                        string wangwang = shopname;

                        //Site
                        string site = "1688";

                        //ItemID
                        itemID = itemID.Replace("'", "").Replace(":", "");

                        //attribute
                        var attribute = scoreDiv.Select("div[id=mod-detail-attributes]").Html;

                        //Material
                        var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tm-sale-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        var atag = liitem.Select("a");
                                        if (atag.HasAttr("style"))
                                        {
                                            ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                        }
                                        else
                                        {
                                            ltr_material.Text += "<a>" + atag.Html + "</a>";
                                        }

                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    ltr_material.Text += "</div>";
                                }

                            }
                        }



                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title + " - " + itemID;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = shopid;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = shopid;
                        ltr_wangwang.Value = wangwang;
                        //ltr_stock.Value = "";
                        //ltr_location_sale.Value = "";
                        ltr_site.Value = site;
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;
                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        pn_productview.Visible = true;
                    }
                }
                else if (link.Contains(".tmall.com/") || link.Contains(".tmall.hk/"))
                {
                    //ltr_content.Text = scoreDiv.Html;
                    if (link.Contains("world.tmall.com"))
                    {
                        string fullcontent = scoreDiv.Html.ToString();
                        string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");
                        string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");


                        //Title
                        var title = scoreDiv.Select("input[name=title]").Val;

                        //Price
                        string origin_price = returntext.Replace(":", "");
                        origin_price = origin_price.Replace("\"", "");
                        string finlaprice = "";
                        string[] oarray = new string[] { };

                        if (origin_price.Contains("-"))
                        {
                            oarray = origin_price.Split('-');
                            finlaprice = oarray[0];
                        }
                        else
                        {
                            finlaprice = origin_price;
                        }
                        //Image
                        var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                        //ShopID
                        string shopid = scoreDiv.Select("div[id=LineZing]").Attr("shopid");

                        //ShopName
                        string shopname = scoreDiv.Select("input[name=seller_nickname]").Val;

                        //Wangwang
                        string wangwang = shopname;

                        //Site
                        string site = "TMALL";

                        //ItemID
                        string itemID = scoreDiv.Select("div[id=LineZing]").Attr("itemid");

                        //attribute
                        var attribute = scoreDiv.Select("div[id=attributes]").Html;

                        //Material
                        var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tm-sale-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        var atag = liitem.Select("a");
                                        if (atag.HasAttr("style"))
                                        {
                                            ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                        }
                                        else
                                        {
                                            ltr_material.Text += "<a>" + atag.Html + "</a>";
                                        }

                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    ltr_material.Text += "</div>";
                                }

                            }
                        }



                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = finlaprice;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + finlaprice + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(finlaprice) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "tmall_" + shopid;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = sellerID;
                        ltr_wangwang.Value = wangwang;
                        //ltr_stock.Value = "";
                        //ltr_location_sale.Value = "";
                        ltr_site.Value = site;
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;
                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        pn_productview.Visible = true;
                    }
                    else
                    {
                        string fullcontent = scoreDiv.Html.ToString();
                        string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");

                        //Title
                        var title = scoreDiv.Select("div[class=tb-detail-hd]").Select("h1").Text;

                        //Price
                        string origin_price = returntext.Replace(":", "");
                        origin_price = origin_price.Replace("\"", "");

                        //Property

                        //Value

                        //Image
                        var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                        //ShopID
                        string shopidLink = scoreDiv.Select("a[id=xshop_collection_href]").Attr("href");
                        Uri shopidLinkget = new Uri(httplink + ":" + shopidLink.ToString());
                        string shopid = HttpUtility.ParseQueryString(shopidLinkget.Query).Get("id");

                        //ShopName
                        var shopname = scoreDiv.Select("input[name=seller_nickname]").Attr("value");

                        //Seller ID
                        string selleridlink = scoreDiv.Select("div[id=J_SellerInfo]").Attr("data-url");
                        Uri selleriddetach = new Uri(httplink + ":" + selleridlink.ToString());
                        string sellerid = HttpUtility.ParseQueryString(selleriddetach.Query).Get("user_num_id");

                        //Wangwang
                        string wangwang = shopname;

                        //Site
                        string site = "TMALL";

                        //ItemID
                        Uri itemIDLink = new Uri(link.ToString());
                        string itemID = HttpUtility.ParseQueryString(itemIDLink.Query).Get("id");

                        //Origin Link
                        string originlink = link;

                        //Outer ID
                        string outerid = HttpUtility.ParseQueryString(itemIDLink.Query).Get("skuID");

                        //attribute
                        var attribute = scoreDiv.Select("div[id=attributes]").Html;

                        //Material
                        var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tm-sale-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        var atag = liitem.Select("a");
                                        if (atag.HasAttr("style"))
                                        {
                                            ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                        }
                                        else
                                        {
                                            ltr_material.Text += "<a>" + atag.Html + "</a>";
                                        }

                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    ltr_material.Text += "</div>";
                                }

                            }
                        }

                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = origin_price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "tmall_" + shopid;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = sellerid;
                        ltr_wangwang.Value = wangwang;
                        ltr_stock.Value = "";
                        ltr_location_sale.Value = "";
                        ltr_site.Value = site;
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;
                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        pn_productview.Visible = true;
                    }
                }
            }
            else
            {
                hdf_product_ok.Value = "fail";
            }
        }


        protected void btn_search_Click(object sender, EventArgs e)
        {
            string link = txt_link.Text.Trim();
            loadProduct(link);
            //GetData(link);
        }

        #region Webservice
        [WebMethod]
        public static string deleteOrderShopTemp(string ID)
        {
            string kq = OrderShopTempController.Delete(Convert.ToInt32(ID));
            return kq;
        }
        [WebMethod]
        public static string deleteOrderTemp(string ID, string shopID)
        {
            string kq = "0";
            var ordertemp = OrderTempController.GetByID(Convert.ToInt32(ID));
            if (ordertemp != null)
            {
                string pricestep = ordertemp.stepprice;
                int UID = Convert.ToInt32(ordertemp.UID);
                string itemid = ordertemp.item_id;
                kq = OrderTempController.Delete(Convert.ToInt32(ID));


                OrderTempController.UpdatePriceInsert(UID, pricestep, itemid);

                int IDS = Convert.ToInt32(shopID);
                var ordert = OrderTempController.GetAllByOrderShopTempID(IDS);
                if (ordert.Count == 0)
                {
                    OrderShopTempController.Delete(IDS);
                }

            }
            return kq;
        }
        [WebMethod]
        public static string UpdateQuantityOrderTemp(string ID, int quantity, string brand)
        {
            OrderTempController.UpdateBrand(Convert.ToInt32(ID), brand);
            string kq = OrderTempController.UpdateQuantity(Convert.ToInt32(ID), quantity);
            return kq;
        }
        [WebMethod]
        public static string UpdateNoteFastPriceVND(int ID, string note, string priceVND)
        {
            string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, priceVND);
            if (kq == "1")
                return kq;
            else
                return "fail";
        }
        [WebMethod]
        public static string UpdateField(int ID, bool chk, string field)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            string UID = OrderShopTempController.SelectUIDByIDOrder(ID);
            if (UID != null)
            {
                if (UID == obj_user.ID.ToString())
                {
                    if (field == "IsCheckProduct")
                    {
                        if (chk == true)
                        {
                            //Lấy ra danh sách sản phẩm để cộng tiền rồi update lại phí kiểm tra hàng hóa
                            var os = OrderShopTempController.GetByUIDAndID(UID.ToInt(), ID);
                            if (os != null)
                            {
                                double total = 0;
                                var listpro = OrderTempController.GetAllByOrderShopTempID(os.ID);
                                double counpros = 0;
                                if (listpro.Count > 0)
                                {
                                    foreach (var item in listpro)
                                    {
                                        counpros += item.quantity.ToInt(1);
                                    }
                                }
                                if (counpros >= 1 && counpros <= 2)
                                {
                                    total = total + (5000 * counpros);
                                }
                                else if (counpros > 2 && counpros <= 10)
                                {
                                    total = total + (3500 * counpros);
                                }
                                else if (counpros > 10 && counpros <= 100)
                                {
                                    total = total + (2000 * counpros);
                                }
                                else if (counpros > 100 && counpros <= 500)
                                {
                                    total = total + (1500 * counpros);
                                }
                                else if (counpros > 500)
                                {
                                    total = total + (1000 * counpros);
                                }
                                total = Math.Round(total, 0);
                                OrderShopTempController.UpdateCheckProductPrice(ID, total.ToString());
                            }
                        }
                        else
                        {
                            //Update lại phí kiểm tra hàng hóa là 0
                            OrderShopTempController.UpdateCheckProductPrice(ID, "0");
                        }
                    }
                    string kq = OrderShopTempController.Update4Field(ID, chk, field);
                    return kq;
                }
                else
                {
                    return "wronguser";
                }
            }
            return "null";
        }
        #endregion

        protected void checkoutallorder_Click(object sender, EventArgs e)
        {
            string lo = hdforderlistall.Value;
            string brandtext = hdfProductBrand.Value;
            if (!string.IsNullOrEmpty(brandtext))
            {
                string[] bps = brandtext.Split('|');
                for (int i = 0; i < bps.Length - 1; i++)
                {
                    string bt = bps[i];
                    string[] item = bt.Split(']');
                    int IDpro = item[0].ToInt(0);
                    string bra = item[1];
                    OrderTempController.UpdateBrand(IDpro, bra);
                }
            }

            Guid temp = Guid.NewGuid();
            List<string> orderId = hdfListOrderTempID.Value.Split('|').Where(n => !string.IsNullOrEmpty(n)).ToList();

            if (!string.IsNullOrEmpty(lo))
            {
                string[] orders = lo.Split('|');
                for (int i = 0; i < orders.Length - 1; i++)
                {
                    string order = orders[i];
                    string[] items = order.Split(']');
                    int ID = items[0].ToInt(0);
                    string note = items[1];
                    string pricevnd = items[2];
                    string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, pricevnd);
                }
                Session["PayAllTempOrder"] = orderId;
                Response.Redirect("/thanh-toan");
            }
        }

        protected void checkout1order_Click(object sender, EventArgs e)
        {
            string lo = hdforderlistall.Value;
            string brandtext = hdfProductBrand.Value;
            if (!string.IsNullOrEmpty(brandtext))
            {
                string[] bps = brandtext.Split('|');
                for (int i = 0; i < bps.Length - 1; i++)
                {
                    string bt = bps[i];
                    string[] item = bt.Split(']');
                    int IDpro = item[0].ToInt(0);
                    string bra = item[1];
                    OrderTempController.UpdateBrand(IDpro, bra);
                }
            }
            if (!string.IsNullOrEmpty(lo))
            {
                int ID = 0;
                string[] orders = lo.Split('|');

                for (int i = 0; i < orders.Length - 1; i++)
                {
                    string order = orders[i];
                    string[] items = order.Split(']');
                    ID = items[0].ToInt(0);
                    string note = items[1];
                    string pricevnd = items[2];
                    string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, pricevnd);
                }
                List<string> orderId = new List<string>();
                orderId.Add(ID.ToString());
                Session["PayAllTempOrder"] = orderId;
                Response.Redirect("/thanh-toan/" + ID);
            }
        }


        #region phân trang

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
    }
}