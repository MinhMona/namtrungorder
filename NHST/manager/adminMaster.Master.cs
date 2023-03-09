using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class adminMaster : System.Web.UI.MasterPage
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
                    string username_current = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(username_current);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 1)
                        {
                            //lUName.Text = obj_user.Username;

                        }
                        else
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                    LoadNotification();
                    LoadMenu();
                }
            }

        }
        public void LoadMenu()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int Role = Convert.ToInt32(obj_user.RoleID);
                if (Role != 1)
                {
                    StringBuilder html = new StringBuilder();
                    if (Role == 0)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li class=\"active\"><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i home\"></span>Cài đặt <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/configuration.aspx\">Cấu hình hệ thống</a></li>");
                        html.Append("           <li><a href=\"/manager/Tariff-TQVN.aspx\">TL phí TQ-VN</a></li>");
                        html.Append("           <li><a href=\"/manager/Tariff-Buypro\">TL phí dịch vụ mua hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/User-Level.aspx\">TL người dùng</a></li>");
                        html.Append("           <li><a href=\"/manager/thiet-lap-thong-bao.aspx\">Thiết lập thông báo</a></li>");
                        html.Append("           <li><a href=\"/manager/Home-Config.aspx\">Nội dung trang chủ</a></li>");
                        html.Append("           <li><a href=\"/manager/pricechangeList.aspx\">TL Phí thanh toán hộ</a></li>");
                        html.Append("           <li><a href=\"/manager/Noti-app-list.aspx\">Thông báo app</a></li>");
                        //html.Append("           <li><a href=\"#\">TL vận chuyển</a></li>");
                        //html.Append("           <li><a href=\"#\">TL Phí thanh toán hộ</a></li>");
                        //html.Append("           <li><a href=\"#\">TL phí vận chuyển hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i user\"></span>Nhân viên <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/stafflist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/admin-staff-income\">Kiểm tra hoa hồng NV</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i users\"></span>Khách Hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/userlist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/SmsForward\">Truy vấn nạp tiền</a></li>");
                        html.Append("           <li><a href=\"/manager/HistorySendWallet\">Lịch sử nạp</a></li>");
                        //html.Append("           <li><a href=\"/manager/RequestRechargeCYN\">Lịch sử nạp tệ</a></li>");
                        html.Append("           <li><a href=\"/manager/Withdraw-List\">Lịch sử rút</a></li>");
                        //html.Append("           <li><a href=\"/manager/refund-cyn\">Lịch sử rút tệ</a></li>");
                        //html.Append("           <li><a href=\"/manager/accountant-payment\">Thanh toán hóa đơn</a></li>");
                        html.Append("           <li><a href=\"/manager/accountant-outstock-payment\">Thanh toán xuất kho</a></li>");
                        html.Append("           <li><a href=\"/manager/PayOrder-Internal\">Thanh toán đơn nội bộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        //html.Append("   <li><a href=\"/manager/orderlist\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist2?ot=1\">Đơn hàng mua hộ</a></li>");
                        html.Append("           <li><a href=\"/manager/OrderListSetStaff.aspx?ot=1\">Đơn hàng mua hộ gán nhân viên</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        html.Append("           <li><a href=\"/manager/transportation-list\">Đơn hàng VC hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");

                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Công nợ<i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("   <li><a href=\"/manager/Giaodich.aspx\">Ký quỹ</a></li>");
                        html.Append("   <li><a href=\"/manager/HistoryEscrow.aspx\">Lịch sử ký quỹ</a></li>");
                        html.Append("   <li><a href=\"/manager/List-Dinhkhoan.aspx\">Danh sách định khoản</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");

                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Yêu cầu giao hàng<i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("   <li><a href=\"/manager/request-homeshipping.aspx\">Yêu cầu giao hàng</a>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-da-giao.aspx\">Danh sách hàng đã giao</a>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-cho-giao.aspx\">Danh sách hàng chờ giao</a>");
                        html.Append("       </ul>");
                        html.Append("   </li>");


                        html.Append("   <li><a href=\"/manager/danh-sach-thanh-toan-ho\"><span class=\"nav-i panelList\"></span>Thanh toán hộ</a></li>");

                       
                        html.Append("   <li><a href=\"/manager/ComplainList.aspx\"><span class=\"nav-i cube\"></span>Khiếu nại</a>");

                        html.Append("   <li><a href=\"/manager/ContactList.aspx\"><span class=\"nav-i panelList\"></span>Liên hệ</a></li>");
                        html.Append("   <li><a href=\"/manager/PartnerList.aspx\"><span class=\"nav-i panelList\"></span>Logo Đối tác</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i paper\"></span>Bài viết <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Page-Type-List\">Danh mục</a></li>");
                        html.Append("           <li><a href=\"/manager/PageList\">Danh sách trang</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/TQWareHouse\">Kiểm hàng TQ</a></li>");
                        html.Append("           <li><a href=\"/manager/VNWarehouse\">Kiểm hàng VN</a></li>");
                        html.Append("           <li><a href=\"/manager/OutStock\">Xuất kho cho khách</a></li>");
                        html.Append("           <li><a href=\"/manager/Warehouse-Management\">Quản lý bao hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Order-Transaction-Code\">Quản lý mã vận đơn</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i grid\"></span>Thống kê <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Report-order-price.aspx\">TK lợi nhuận</a></li>");
                        html.Append("           <li><a href=\"/manager/report-income-saler.aspx\">TK doanh thu Sale</a></li>");
                        html.Append("           <li><a href=\"/manager/report-income-buyer.aspx\">TK doanh thu Mua hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-General.aspx\">Thống kê tổng quát</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-Income.aspx\">TK doanh thu</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-Orders.aspx\">TK đơn hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-ordering-staff.aspx\">TK đơn hàng NVĐH</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-recharge.aspx\">TK tiền nạp</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-user-wallet.aspx\">TK số dư</a></li>");
                        //html.Append("           <li><a href=\"/manager/Report-order.aspx\">TK đơn hàng mua, kho TQ, kho đích</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-User-Use-Wallet.aspx\">TK giao dịch</a></li>");
                        html.Append("           <li><a href=\"/manager/report-PayOrder-Internal.aspx\">TK thanh toán nội bộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 2)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i user\"></span>Nhân viên <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/stafflist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/admin-staff-income\">Kiểm tra hoa hồng NV</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i users\"></span>Khách Hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/userlist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/SmsForward\">Truy vấn nạp tiền</a></li>");
                        html.Append("           <li><a href=\"/manager/HistorySendWallet\">Lịch sử nạp</a></li>");
                        //html.Append("           <li><a href=\"/manager/RequestRechargeCYN\">Lịch sử nạp tệ</a></li>");
                        html.Append("           <li><a href=\"/manager/Withdraw-List\">Lịch sử rút</a></li>");
                        //html.Append("           <li><a href=\"/manager/refund-cyn\">Lịch sử rút tệ</a></li>");
                        //html.Append("           <li><a href=\"/manager/accountant-payment\">Thanh toán hóa đơn</a></li>");
                        html.Append("           <li><a href=\"/manager/accountant-outstock-payment\">Thanh toán xuất kho</a></li>");
                        html.Append("           <li><a href=\"/manager/PayOrder-Internal\">Thanh toán đơn nội bộ</a></li>");

                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist2?ot=1\">Đơn hàng mua hộ</a></li>");
                        html.Append("           <li><a href=\"/manager/OrderListSetStaff.aspx?ot=1\">Đơn hàng mua hộ gán nhân viên</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        html.Append("           <li><a href=\"/manager/transportation-list\">Đơn hàng VC hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");

                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Công nợ<i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("   <li><a href=\"/manager/Giaodich.aspx\">Ký quỹ</a></li>");
                        html.Append("   <li><a href=\"/manager/HistoryEscrow.aspx\">Lịch sử ký quỹ</a></li>");
                        html.Append("   <li><a href=\"/manager/List-Dinhkhoan.aspx\">Danh sách định khoản</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");

                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Yêu cầu giao hàng<i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("   <li><a href=\"/manager/request-homeshipping.aspx\">Yêu cầu giao hàng</a></li>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-da-giao.aspx\">Danh sách hàng đã giao</a></li>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-cho-giao.aspx\">Danh sách hàng chờ giao</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");

                        html.Append("   <li><a href=\"/manager/danh-sach-thanh-toan-ho\"><span class=\"nav-i panelList\"></span>Thanh toán hộ</a></li>");
                        //html.Append("   <li><a href=\"/manager/request-homeshipping.aspx\"><span class=\"nav-i cube\"></span>Yêu cầu giao hàng</a>");
                        //html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-da-giao.aspx\"><span class=\"nav-i cube\"></span>Danh sách hàng đã giao</a>");
                        //html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-cho-giao.aspx\"><span class=\"nav-i cube\"></span>Danh sách hàng chờ giao</a>");
                        html.Append("   <li><a href=\"/manager/ComplainList.aspx\"><span class=\"nav-i cube\"></span>Khiếu nại</a>");
                        html.Append("   <li><a href=\"/manager/ContactList.aspx\"><span class=\"nav-i panelList\"></span>Liên hệ</a></li>");
                        html.Append("   <li><a href=\"/manager/PartnerList.aspx\"><span class=\"nav-i panelList\"></span>Logo Đối tác</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i paper\"></span>Bài viết <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Page-Type-List\">Danh mục</a></li>");
                        html.Append("           <li><a href=\"/manager/PageList\">Danh sách trang</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/TQWareHouse\">Kiểm hàng TQ</a></li>");
                        html.Append("           <li><a href=\"/manager/VNWarehouse\">Kiểm hàng VN</a></li>");
                        html.Append("           <li><a href=\"/manager/OutStock\">Xuất kho cho khách</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i grid\"></span>Thống kê <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Report-ordering-staff.aspx\">TK đơn hàng NVĐH</a></li>");
                        //html.Append("           <li><a href=\"/manager/report-income-buyer.aspx\">TK doanh thu Mua hàng</a></li>");
                        //html.Append("           <li><a href=\"/manager/report-income-saler.aspx\">TK doanh thu Sale</a></li>");
                        //html.Append("           <li><a href=\"/manager/Report-General.aspx\">Thống kê tổng quát</a></li>");
                        //html.Append("           <li><a href=\"/manager/report-PayOrder-Internal.aspx\">TK thanh toán nội bộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 3)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"/manager/ComplainList.aspx\"><span class=\"nav-i cube\"></span>Khiếu nại</a>");
                        html.Append("   <li><a href=\"/manager/orderlist2\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        //html.Append("   <li><a href=\"/manager/staff-income\"><span class=\"nav-i panelList\"></span>Kiểm tra hoa hồng NV</a></li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 4)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist2?ot=1\">Đơn hàng mua hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        html.Append("           <li><a href=\"/manager/transportation-list\">Đơn hàng VC hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/TQWareHouse\">Kiểm hàng TQ</a></li>");
                        html.Append("           <li><a href=\"/manager/Warehouse-Management\">Quản lý bao hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Order-Transaction-Code\">Quản lý mã vận đơn</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 5)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist2?ot=1\">Đơn hàng mua hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        html.Append("           <li><a href=\"/manager/transportation-list\">Đơn hàng VC hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/VNWarehouse\">Kiểm hàng VN</a></li>");
                        html.Append("           <li><a href=\"/manager/OutStock\">Xuất kho cho khách</a></li>");
                        html.Append("           <li><a href=\"/manager/Warehouse-Management\">Quản lý bao hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Order-Transaction-Code\">Quản lý mã vận đơn</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Yêu cầu giao hàng<i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("   <li><a href=\"/manager/request-homeshipping.aspx\">Yêu cầu giao hàng</a></li>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-da-giao.aspx\">Danh sách hàng đã giao</a></li>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-cho-giao.aspx\">Danh sách hàng chờ giao</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        //html.Append("   <li><a href=\"/manager/request-homeshipping.aspx\"><span class=\"nav-i cube\"></span>Yêu cầu giao hàng</a>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 6)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i users\"></span>Khách Hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/saler-customer-list\">Danh sách</a></li>");
                        html.Append("       </ul>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Saler-HistorySendWallet-List\">Lịch sử nạp</a></li>");
                        html.Append("       </ul>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Saler-Withdraw-List\">Lịch sử rút</a></li>");
                        html.Append("       </ul>");

                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/report-income-for-saler.aspx\"><span class=\"nav-i panelList\"></span>TK doanh thu Sale</a>");
                        html.Append("   <li><a href=\"/manager/ComplainList.aspx\"><span class=\"nav-i cube\"></span>Khiếu nại</a>");
                        html.Append("   <li><a href=\"/manager/orderlist2\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        //html.Append("   <li><a href=\"/manager/staff-income\"><span class=\"nav-i panelList\"></span>Kiểm tra hoa hồng NV</a></li>");
                        //html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i grid\"></span>Thống kê <i class=\"caret\"></i></a>");
                        //html.Append("       <ul class=\"side-sub\">");
                        //html.Append("           <li><a href=\"/manager/report-income-saler.aspx\">TK doanh thu Sale</a></li>");
                        //html.Append("       </ul>");
                        //html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 7)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Yêu cầu giao hàng<i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("   <li><a href=\"/manager/request-homeshipping.aspx\">Yêu cầu giao hàng</a>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-da-giao.aspx\">Danh sách hàng đã giao</a>");
                        html.Append("   <li><a href=\"/manager/Danh-sach-don-hang-cho-giao.aspx\">Danh sách hàng chờ giao</a>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/Withdraw-List\"><span class=\"nav-i panelList\"></span>Lịch sử rút tiền</a></li>");
                        html.Append("   <li><a href=\"/manager/refund-cyn\"><span class=\"nav-i panelList\"></span>Lịch sử rút tiền tệ</a></li>");
                        html.Append("           <li><a href=\"/manager/SmsForward\">Truy vấn nạp tiền</a></li>");
                        html.Append("   <li><a href=\"/manager/historysendwalletaccountant\"><span class=\"nav-i panelList\"></span>Lịch sử nạp tiền</a></li>");
                        html.Append("   <li><a href=\"/manager/RequestRechargeCYN\"><span class=\"nav-i panelList\"></span>Lịch sử nạp tiền tệ</a></li>");
                        html.Append("   <li><a href=\"/manager/RechargeCYN\"><span class=\"nav-i panelList\"></span>Lịch sử nạp tiền tệ</a></li>");
                        html.Append("   <li><a href=\"/manager/List-Dinhkhoan.aspx\"><span class=\"nav-i cube\"></span>Danh sách định khoản</a>");
                        html.Append("   <li><a href=\"/manager/Giaodich.aspx\"><span class=\"nav-i cube\"></span>Ký quỹ</a>");
                        html.Append("   <li><a href=\"/manager/HistoryEscrow.aspx\"><span class=\"nav-i cube\"></span>Lịch sử ký quỹ</a>");
                        html.Append("   <li><a href=\"/manager/Accountant-User-List\"><span class=\"nav-i panelList\"></span>Danh sách người dùng</a></li>");
                        html.Append("   <li><a href=\"/manager/OrderList2\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        html.Append("   <li><a href=\"/manager/accountant-outstock-payment\"><span class=\"nav-i panelList\"></span>Thanh toán xuất kho</a></li>");
                        html.Append("   <li><a href=\"/manager/PayOrder-Internal\"><span class=\"nav-i panelList\"></span>Thanh toán đơn nội bộ</a></li>");
                        html.Append("   <li><a href=\"/manager/Report-General\"><span class=\"nav-i panelList\"></span>Thống kê tổng quát</a></li>");
                        html.Append("           <li><a href=\"/manager/report-PayOrder-Internal.aspx\">TK thanh toán nội bộ</a></li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    ltrMenu.Text = html.ToString();
                }
            }
        }
        public void LoadNotification()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    ltrinfo.Text += "";
                    ltrinfo.Text += "<a href=\"/manager/home\" class=\"right-it rate\"><i class=\"fa fa-home\"></i></a>";
                    ltrinfo.Text += "<a href=\"/manager/configuration\" class=\"right-it rate\"> ¥1 = " + string.Format("{0:N0}", config.Currency) + "</a>";
                    var noti = NotificationController.GetByReceivedID(obj_user.ID);
                    var noti1 = noti.Where(n => n.NotifType == 1).Take(5).ToList();
                    var noti2 = noti.Where(n => n.NotifType == 2).Take(5).ToList();
                    var noti3 = noti.Where(n => n.NotifType == 3).Take(5).ToList();
                    var noti5 = noti.Where(n => n.NotifType == 5).Take(5).ToList();
                    var noti6 = noti.Where(n => n.NotifType == 6).Take(5).ToList();

                    string stt1Name = "Đơn hàng";
                    string stt1Shortname = "dh";

                    string stt2Name = "Nạp tiền";
                    string stt2Shortname = "nt";

                    string stt3Name = "Rút tiền";
                    string stt3Shortname = "rt";

                    string stt5Name = "Khiếu nại";
                    string stt5Shortname = "kn";

                    string stt6Name = "Đăng ký";
                    string stt6Shortname = "dk";


                    ltrinfo.Text += "<a href=\"/manager/admin-noti\" class=\"right-it noti\"><i class=\"fa fa-bell-o\"></i><span class=\"badge\">" + noti.Count + "</span></a>";
                    if (noti.Count > 0)
                    {
                        ltrinfo.Text += "<div class=\"notification_wrap\">";
                        ltrinfo.Text += "  <div class=\"tab_nav\">";
                        ltrinfo.Text += "   <a href=\"javascript:;\" class=\"close-noti\">Đóng</a>";
                        ltrinfo.Text += "	<a href=\"#noti-all\" class=\"tab_link active\">Tất cả</a>";
                        if (noti1.Count > 0)
                        {
                            ltrinfo.Text += "	<a href=\"#noti-" + stt1Shortname + "\" class=\"tab_link\">" + stt1Name + "</a>";
                        }
                        if (noti2.Count > 0)
                        {
                            ltrinfo.Text += "	<a href=\"#noti-" + stt2Shortname + "\" class=\"tab_link\">" + stt2Name + "</a>";
                        }
                        if (noti3.Count > 0)
                        {
                            ltrinfo.Text += "	<a href=\"#noti-" + stt3Shortname + "\" class=\"tab_link\">" + stt3Name + "</a>";
                        }
                        if (noti5.Count > 0)
                        {
                            ltrinfo.Text += "	<a href=\"#noti-" + stt5Shortname + "\" class=\"tab_link\">" + stt5Name + "</a>";
                        }
                        if (noti6.Count > 0)
                        {
                            ltrinfo.Text += "	<a href=\"#noti-" + stt6Shortname + "\" class=\"tab_link\">" + stt6Name + "</a>";
                        }
                        ltrinfo.Text += "  </div>";
                        ltrinfo.Text += "  <div class=\"tab_content_wrap\">";
                        ltrinfo.Text += "	<ul class=\"tab_content show\" id=\"noti-all\">";
                        if (noti.Count > 0)
                        {
                            var notiget5items = noti.Take(15).ToList();
                            foreach (var item in notiget5items)
                            {
                                ltrinfo.Text += "	  <li>";
                                if (item.NotifType == 1)
                                {
                                    //ltrinfo.Text += "		<a href=\"/manager/OrderDetail.aspx?id=" + item.OrderID + "\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/OrderDetail.aspx?id=" + item.OrderID + "')\">";
                                }
                                else if (item.NotifType == 2)
                                {
                                    //ltrinfo.Text += "		<a href=\"/manager/HistorySendWallet\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/HistorySendWallet')\">";
                                }
                                else if (item.NotifType == 3)
                                {
                                    //ltrinfo.Text += "		<a href=\"/manager/Withdraw-List\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/Withdraw-List')\">";
                                }
                                else if (item.NotifType == 5)
                                {
                                    //ltrinfo.Text += "		<a href=\"/manager/ComplainList\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/ComplainList')\">";
                                }
                                else if (item.NotifType == 6)
                                {
                                    //ltrinfo.Text += "		<a href=\"/manager/userlist\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/userlist')\">";
                                }
                                ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                ltrinfo.Text += "		  <div class=\"noti-content\">";
                                ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                ltrinfo.Text += "		  </div>";
                                ltrinfo.Text += "		</a>";
                                ltrinfo.Text += "	  </li>";
                            }
                        }
                        ltrinfo.Text += "	</ul>";
                        if (noti1.Count > 0)
                        {
                            ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt1Shortname + "\">";
                            foreach (var item in noti1)
                            {
                                ltrinfo.Text += "	  <li>";
                                ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/OrderDetail.aspx?id=" + item.OrderID + "')\">";
                                //ltrinfo.Text += "		<a href=\"/manager/OrderDetail.aspx?id=" + item.OrderID + "\" onclick=\"checkisRead('" + item.ID + "')\">";
                                ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                ltrinfo.Text += "		  <div class=\"noti-content\">";
                                ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                ltrinfo.Text += "		  </div>";
                                ltrinfo.Text += "		</a>";
                                ltrinfo.Text += "	  </li>";
                            }

                            ltrinfo.Text += "	</ul>";
                        }
                        if (noti2.Count > 0)
                        {
                            ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt2Shortname + "\">";
                            foreach (var item in noti2)
                            {
                                ltrinfo.Text += "	  <li>";
                                ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/HistorySendWallet')\">";
                                ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                ltrinfo.Text += "		  <div class=\"noti-content\">";
                                ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                ltrinfo.Text += "		  </div>";
                                ltrinfo.Text += "		</a>";
                                ltrinfo.Text += "	  </li>";
                            }

                            ltrinfo.Text += "	</ul>";
                        }
                        if (noti3.Count > 0)
                        {
                            ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt3Shortname + "\">";
                            foreach (var item in noti3)
                            {
                                ltrinfo.Text += "	  <li>";
                                ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/Withdraw-List')\">";
                                ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                ltrinfo.Text += "		  <div class=\"noti-content\">";
                                ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                ltrinfo.Text += "		  </div>";
                                ltrinfo.Text += "		</a>";
                                ltrinfo.Text += "	  </li>";
                            }

                            ltrinfo.Text += "	</ul>";
                        }
                        if (noti5.Count > 0)
                        {
                            ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt5Shortname + "\">";
                            foreach (var item in noti5)
                            {
                                ltrinfo.Text += "	  <li>";
                                ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/ComplainList')\">";
                                ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                ltrinfo.Text += "		  <div class=\"noti-content\">";
                                ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                ltrinfo.Text += "		  </div>";
                                ltrinfo.Text += "		</a>";
                                ltrinfo.Text += "	  </li>";
                            }

                            ltrinfo.Text += "	</ul>";
                        }
                        if (noti6.Count > 0)
                        {
                            ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt6Shortname + "\">";
                            foreach (var item in noti6)
                            {
                                ltrinfo.Text += "	  <li>";
                                ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/userlist')\">";
                                ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                ltrinfo.Text += "		  <div class=\"noti-content\">";
                                ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                ltrinfo.Text += "		  </div>";
                                ltrinfo.Text += "		</a>";
                                ltrinfo.Text += "	  </li>";
                            }

                            ltrinfo.Text += "	</ul>";
                        }
                        ltrinfo.Text += "  </div>";
                        ltrinfo.Text += "  <div class=\"view-all-noti\"><a href=\"/manager/admin-noti.aspx\">Xem tất cả</a></div>";
                        ltrinfo.Text += "</div>";
                    }


                    ltrinfo.Text += "<span class=\"right-it username\"><a href=\"/thong-tin-nguoi-dung\" target=\"_blank\">" + obj_user.Username + "</a></span>";
                    ltrinfo.Text += "Số dư :<span class=\"right-it Wallet\">" + string.Format("{0:N0}", obj_user.Wallet) + "</span>";
                    ltrinfo.Text += "<a href=\"/trang-chu\" class=\"right-it rate\"><span class=\"right-it username\">Trang ngoài</span></a>";
                    ltrinfo.Text += "<a href=\"/dang-xuat\" class=\"right-it logout\"><i class=\"fa fa-sign-out\"></i>Sign out</a>";
                }
                //if (obj_user.RoleID == 0)
                //{

                //}
                //int UID = obj_user.ID;
                //var notiadmin = NotificationController.GetByReceivedID(UID);
                //ltrAmountNoti.Text = notiadmin.Count.ToString();
                //if (notiadmin.Count > 0)
                //{
                //    StringBuilder html = new StringBuilder();
                //    foreach (var item in notiadmin)
                //    {
                //        html.Append("<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + item.ID + "','" + item.OrderID + "','2')\">" + item.Message + "</a></li>");
                //    }
                //    ltrNoti.Text = html.ToString();
                //}
                //else
                //{
                //    ltrNoti.Text = "<li role=\"presentation\"><a href=\"javascript:;\">Không có thông báo mới</a></li>";
                //}
            }
        }
    }
}