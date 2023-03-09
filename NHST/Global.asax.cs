using NHSTPJ;
using PhanBonPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace NHST
{
    public class Global : HttpApplication
    {
        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("home", "trang-chu", "~/Default.aspx");
            //routes.MapPageRoute("about", "gioi-thieu", "~/Gioithieu.aspx");
            //routes.MapPageRoute("contact", "lien-he", "~/Lienhe.aspx");

            //routes.MapPageRoute("product", "san-pham", "~/Sanpham.aspx");
            //routes.MapPageRoute("productcate", "san-pham/{catename}-{i}", "~/Sanpham.aspx");

            //routes.MapPageRoute("policy", "chinh-sach-dai-ly", "~/Chinhsach.aspx");

            //routes.MapPageRoute("news", "tin-tuc", "~/Danhsachtin.aspx");
            //routes.MapPageRoute("newsdetail", "tin-tuc/{newstitle}-{i}", "~/Chitiettin.aspx");
            routes.MapPageRoute("login", "manager", "~/dang-nhap.aspx");
            routes.MapPageRoute("cart", "gio-hang", "~/Cart.aspx");
            routes.MapPageRoute("kyguihanghoa", "ky-gui-hang", "~/tao-ky-gui-hang.aspx");
            routes.MapPageRoute("cartlink", "gio-hang/{link}", "~/Cart.aspx");
            routes.MapPageRoute("congcu", "cong-cu", "~/cong-cu.aspx");
            routes.MapPageRoute("dathangnhanh", "dat-hang-nhanh", "~/cong-cu-dat-hang.aspx");
            routes.MapPageRoute("payment", "thanh-toan/{order}", "~/Thanh-toan.aspx");
            routes.MapPageRoute("orderdetail", "chi-tiet-don-hang/{id}", "~/chi-tiet-don-hang.aspx");
            routes.MapPageRoute("histortrade", "lich-su-giao-dich", "~/lich-su-nap-tien.aspx");
            //routes.MapPageRoute("category", "{pagetypeid}/{pagetypename}", "~/danh-muc-trang.aspx");
            //routes.MapPageRoute("article", "{pagetypeid}/{pageid}/{pagetypename}/{pagetitle}", "~/chi-tiet-trang.aspx");
            routes.MapPageRoute("payhelpdetail", "chi-tiet-thanh-toan-ho/{id}", "~/chi-tiet-thanh-toan-ho.aspx");
            routes.MapPageRoute("category", "chuyen-muc/{pagetypename}", "~/danh-muc-trang.aspx");
            routes.MapPageRoute("article", "chuyen-muc/{pagetypename}/{pagetitle}", "~/chi-tiet-trang.aspx");
            routes.MapPageRoute("khieunai", "them-khieu-nai/{id}", "~/them-khieu-nai.aspx");
            routes.MapPageRoute("chitietdonhangvanchuyenho", "chi-tiet-don-hang-van-chuyen-ho/{id}", "~/chi-tiet-don-hang-van-chuyen-ho.aspx");
        }
        void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Ignore("{*dialogs}", new { dialogs = @".*Telerik\.Web\.UI\.DialogHandler\.aspx.*" });
            RouteTable.Routes.Ignore("{*allaxd}", new { allaxd = @".*\.axd(/.*)?" });

            RegisterRoutes(RouteTable.Routes);
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }
}