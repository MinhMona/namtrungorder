<%@ Page Title="" Language="C#" MasterPageFile="~/123nhaphangMaster.Master" AutoEventWireup="true" CodeBehind="Default9.aspx.cs" Inherits="NHST.Default8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <div class="banner">
            <div class="all">
                <div class="banner__title">
                    <div class="title">
                        <h1>
                            <span class="sub-hd">Chuyên order</span><br>
                            Hàng <span class="color">Trung Quốc</span>
                        </h1>
                    </div>
                    <div class="button">
                        <a href="" class="mn-btn gg-btn"><i class="fab fa-chrome"></i>Chrome</a>
                        <a href="" class="mn-btn cc-btn"><i class="fab fa-chrome"></i>Cốc Cốc</a>
                    </div>
                </div>
            </div>
            <div class="float-img">
                <img src="/App_Themes/123nhaphang/images/banner-xe.png" alt="">
            </div>
        </div>

        <div class="present">
            <div class="all">
                <div class="search">
                    <div class="s_select">
                        <asp:DropDownList ID="ddlWebsearch" runat="server" CssClass="f-control">
                            <asp:ListItem Value="taobao" Text="Taobao"></asp:ListItem>
                            <asp:ListItem Value="1688" Text="1688"></asp:ListItem>
                            <asp:ListItem Value="tmall" Text="Tmall"></asp:ListItem>
                        </asp:DropDownList>
                        <span class="icon">
                            <i class="fas fa-chevron-down"></i>
                        </span>
                    </div>
                    <div class="s_input">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="f-control" placeholder="Tìm kiếm sản phẩm"></asp:TextBox>
                    </div>
                    <div class="s-button">
                        <asp:Button ID="btnsearchpro" runat="server" CssClass="mn-btn" Text="Tìm kiếm" OnClick="btnsearchpro_Click"
                            OnClientClick="document.forms[0].target = '_blank';" />
                    </div>
                </div>
                <div class="ps-wrap">
                    <div class="ps__child">
                        <h4 class="hd">10</h4>
                        <p class="sub-hd">Năm kinh nghiệm</p>
                    </div>
                    <div class="ps__child">
                        <h4 class="hd">12,345</h4>
                        <p class="sub-hd">Khách hàng</p>
                    </div>
                    <div class="ps__child">
                        <h4 class="hd">67,890</h4>
                        <p class="sub-hd">Đơn hàng</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="steps-order">
            <div class="all">
                <ul class="list-steps">
                    <li class="step__item active" step-nav="#step-dangky">
                        <div class="it-wrap">
                            <div class="icon">
                                <i class="fas fa-file-alt"></i>
                            </div>
                            <div class="hd">Đăng ký tài khoản</div>
                        </div>
                    </li>
                    <li class="step__item" step-nav="#step-caidat">
                        <div class="it-wrap">
                            <div class="icon">
                                <i class="fas fa-cog"></i>
                            </div>
                            <div class="hd">Cài đặt công cụ mua hàng</div>
                        </div>
                    </li>
                    <li class="step__item" step-nav="#step-chonhang">
                        <div class="it-wrap">
                            <div class="icon">
                                <i class="fas fa-shopping-bag"></i>
                            </div>
                            <div class="hd">Chọn hàng và thêm vào giỏ hàng</div>
                        </div>
                    </li>
                    <li class="step__item" step-nav="#step-guidon">
                        <div class="it-wrap">
                            <div class="icon">
                                <i class="fas fa-share-square"></i>
                            </div>
                            <div class="hd">Gửi đơn đặng hàng</div>
                        </div>
                    </li>
                    <li class="step__item" step-nav="#step-datcoc">
                        <div class="it-wrap">
                            <div class="icon">
                                <i class="fas fa-hand-holding-usd"></i>
                            </div>
                            <div class="hd">Đặt cọc tiền hàng</div>
                        </div>
                    </li>
                    <li class="step__item" step-nav="#step-nhanhang">
                        <div class="it-wrap">
                            <div class="icon">
                                <i class="fas fa-box"></i>
                            </div>
                            <div class="hd">Nhận hàng và thanh toán</div>
                        </div>
                    </li>
                </ul>

                <div class="content-wrap">
                    <div class="step-content" id="step-dangky">
                        <div class="detail">
                            <h2 class="hd">Đăng ký tài khoản</h2>
                            <p>
                                Nhập các thông tin cá nhân bắt buộc vào ô, lưu ý nhập chính xác các thông tin để đảm bảo cho việc quản lí đơn hàng và nhận hàng của bạn.
                            </p>
                            <%--<div class="button">
                                <a href="" class="mn-btn btn-1">Đăng ký</a>
                            </div>--%>
                        </div>
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/step-img-1.jpg" alt="">
                        </div>
                    </div>

                    <div class="step-content" id="step-caidat">
                        <div class="detail">
                            <h2 class="hd">Cài đặt công cụ mua hàng</h2>
                            <p>
                                Click vào cài đặt công cụ đặt hàng của 123Nhaphang. Công cụ hỗ trợ đặt hàng các website taobao, tmall, 1688.
                            </p>
                            <%--<div class="button">
                                <a href="" class="mn-btn btn-1">Đăng ký</a>
                            </div>--%>
                        </div>
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/step-img-1.jpg" alt="">
                        </div>
                    </div>
                    <div class="step-content" id="step-chonhang">
                        <div class="detail">
                            <h2 class="hd">Chọn hàng và thêm hàng vào giỏ</h2>
                            <p>
                                Truy cập vào các trang mua sắm Taobao.com, Tmall.com, 1688.com … chọn hàng và thêm hàng vào giỏ.
                            </p>
                            <%--<div class="button">
                                <a href="" class="mn-btn btn-1">Đăng ký</a>
                            </div>--%>
                        </div>
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/step-img-1.jpg" alt="">
                        </div>
                    </div>
                    <div class="step-content" id="step-guidon">
                        <div class="detail">
                            <h2 class="hd">Gửi đơn hàng</h2>
                            <p>
                                Quay lại website 123nhaphang và kiểm tra giỏ hàng Click vào “Gửi đơn hàng” để tạo đơn hàng,chờ xác nhận đặt hàng thành công.
                            </p>
                            <%--<div class="button">
                                <a href="" class="mn-btn btn-1">Đăng ký</a>
                            </div>--%>
                        </div>
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/step-img-1.jpg" alt="">
                        </div>
                    </div>
                    <div class="step-content" id="step-datcoc">
                        <div class="detail">
                            <h2 class="hd">Đặt cọc tiền hàng</h2>
                            <p>
                                Kiểm tra đơn hàng và đặt cọc tiền hàng qua hình thức chuyển khoản hoặc trực tiếp tại các văn phòng giao dịch gần nhất của 123nhaphang.
                            </p>
                            <%--<div class="button">
                                <a href="" class="mn-btn btn-1">Đăng ký</a>
                            </div>--%>
                        </div>
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/step-img-1.jpg" alt="">
                        </div>
                    </div>
                    <div class="step-content" id="step-nhanhang">
                        <div class="detail">
                            <h2 class="hd">Nhận hàng và thanh toán</h2>
                            <p>
                                Quý khách nhận được thông báo hàng về Việt Nam. Quý khách thanh toán số tiền còn  thiếu qua hình thức chuyển khoản hoặc trực tiếp. Sau khi thanh toán quý khách hàng có thể yêu cầu ship hàng hoặc trực tiếp nhận hàng tại kho của 123nhaphang.
                            </p>
                            <%--<div class="button">
                                <a href="" class="mn-btn btn-1">Đăng ký</a>
                            </div>--%>
                        </div>
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/step-img-1.jpg" alt="">
                        </div>
                    </div>
                </div>
            </div>

        </div>


        <div class="features">
            <ul class="list-feats">
                <li class="feat__item">
                    <div class="img">
                        <a href="">
                            <img src="/App_Themes/123nhaphang/images/features-img-1.jpg" alt=""></a>
                    </div>
                    <div class="content">
                        <div class="ct-wrap">
                            <p class="hd">
                                Đặt hàng, ship hàng<br>
                                quốc tế
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </div>
                </li>
                <li class="feat__item">
                    <div class="img">
                        <a href="">
                            <img src="/App_Themes/123nhaphang/images/features-img-2.jpg" alt=""></a>
                    </div>
                    <div class="content">
                        <div class="ct-wrap">
                            <p class="hd">
                                Chuyển hàng 2 chiều<br>
                                Trung - Việt
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </div>
                </li>
                <li class="feat__item">
                    <div class="img">
                        <a href="">
                            <img src="/App_Themes/123nhaphang/images/features-img-3.jpg" alt=""></a>
                    </div>
                    <div class="content">
                        <div class="ct-wrap">
                            <p class="hd">
                                Chuyển tiền, nạp tiền<br>
                                Alipay, Wechat
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </div>
                </li>
                <li class="feat__item revert">
                    <div class="img">
                        <a href="">
                            <img src="/App_Themes/123nhaphang/images/features-img-4.jpg" alt=""></a>
                    </div>
                    <div class="content">
                        <div class="ct-wrap">
                            <p class="hd">
                                Hỗ trợ và tư vấn<br>
                                chuyên nghiệp
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </div>
                </li>
                <li class="feat__item revert">
                    <div class="img">
                        <a href="">
                            <img src="/App_Themes/123nhaphang/images/features-img-5.jpg" alt=""></a>
                    </div>
                    <div class="content">
                        <div class="ct-wrap">
                            <p class="hd">
                                Dễ dàng quản lý<br>
                                đơn hàng
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </div>
                </li>
                <li class="feat__item revert">
                    <div class="img">
                        <a href="">
                            <img src="/App_Themes/123nhaphang/images/features-img-6.jpg" alt=""></a>
                    </div>
                    <div class="content">
                        <div class="ct-wrap">
                            <p class="hd">
                                Giao hàng<br>
                                tận nơi
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </div>
                </li>
            </ul>
        </div>

        <div class="benefits">
            <div class="all">
                <div class="title">
                    <h2>Quyền lợi khách hàng</h2>
                </div>
                <ul class="list-benefits">
                    <li class="benefit__item">
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/benefit-img-1.png" alt="">
                        </div>
                        <div class="content">
                            <p class="hd"><a href="">Khách hàng thân thiết</a></p>
                            <p>
                                Cooking in the heart of Cajun country is an art form. There really is very little science to this particular form of cooking
              that includes a lot more than mere lagniappe from the pantry or the spice cabinet.
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </li>
                    <li class="benefit__item">
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/benefit-img-2.png" alt="">
                        </div>
                        <div class="content">
                            <p class="hd"><a href="">Marketing &amp; Bán hàng</a></p>
                            <p>
                                Cooking in the heart of Cajun country is an art form. There really is very little science to this particular form of cooking
              that includes a lot more than mere lagniappe from the pantry or the spice cabinet.
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </li>
                    <li class="benefit__item">
                        <div class="img">
                            <img src="/App_Themes/123nhaphang/images/benefit-img-3.png" alt="">
                        </div>
                        <div class="content">
                            <p class="hd"><a href="">Ưu đãi theo sản lượng tháng</a></p>
                            <p>
                                Cooking in the heart of Cajun country is an art form. There really is very little science to this particular form of cooking
              that includes a lot more than mere lagniappe from the pantry or the spice cabinet.
                            </p>
                            <a href="" class="mn-btn btn-right">Xem thêm</a>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <div class="contact">
            <div class="all">
                <div class="ct-box">
                    <div class="img">
                        <img src="/App_Themes/123nhaphang/images/logo-icon.png" alt="">
                    </div>
                    <div class="detail">
                        <p class="main">
                            319-C16 Chung cư Thuận Việt
            Lý Thường Kiệt, Phường 15, Quận 11
                        </p>
                    </div>
                    <div class="detail">
                        <p class="sub">Hotline:</p>
                        <p class="main"><a href="tel:+">0126.922.1062</a></p>
                    </div>
                    <div class="detail">
                        <p class="sub">Giờ hoạt động</p>
                        <p class="main">08:30 am - 06:30 pm</p>
                    </div>
                    <div class="detail">
                        <p class="sub">Hotline:</p>
                        <p class="main"><a href="mailto:">admin@hoangbachorder.com</a></p>
                    </div>
                </div>
                <div class="google-map">
                    <div class="map" id="map-canvas"></div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
