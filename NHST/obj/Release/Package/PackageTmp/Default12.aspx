<%@ Page Title="" Language="C#" MasterPageFile="~/TruongThanhMaster.Master" AutoEventWireup="true" CodeBehind="Default12.aspx.cs" Inherits="NHST.Default11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <div class="banner">
            <div class="stick-bn-top">
                <div class="img">
                    <img src="/App_Themes/TruongThanh/images/stick-bntop.png" alt="">
                </div>
            </div>
            <div class="all">
                <div class="banner-content">
                    <div class="plane-img">
                        <div class="img">
                            <img src="/App_Themes/TruongThanh/images/banner-plane.png" alt="">
                        </div>
                    </div>
                    <div class="addon-button">
                        <p class="button-title">Cài đặt công cụ</p>
                        <a href="" class="mn-btn gg-btn"><i class="fab fa-chrome"></i>Chrome</a>
                        <a href="" class="mn-btn cc-btn"><i class="fab fa-chrome"></i>Cốc cốc</a>
                    </div>
                </div>
            </div>
            <div class="stick-bn-bot">
                <div class="img">
                    <img src="/App_Themes/TruongThanh/images/stick-bnbot.png" alt="">
                </div>
            </div>

        </div>

        <div class="resource">
            <div class="all">
                <div class="search">

                    <div class="search-title">
                        <h4 class="hd">Tìm kiếm sản phẩm</h4>
                    </div>
                    <div class="search-form">
                        <div class="select-form">
                            <asp:DropDownList ID="ddlWebsearch" runat="server" CssClass="f-control">
                                <asp:ListItem Value="taobao" Text="Taobao"></asp:ListItem>
                                <asp:ListItem Value="1688" Text="1688"></asp:ListItem>
                                <asp:ListItem Value="tmall" Text="Tmall"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="icon"><i class="fas fa-angle-down"></i></span>
                        </div>
                        <div class="input-form">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="f-control" placeholder="Tìm kiếm sản phẩm"></asp:TextBox>
                            <asp:Button ID="btnsearchpro" runat="server" CssClass="submit" Text="Tìm kiếm" OnClick="btnsearchpro_Click"
                                OnClientClick="document.forms[0].target = '_blank';" />
                        </div>
                    </div>

                </div>

                <ul class="list-resource">
                    <li class="src__item">
                        <div class="src-card">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/TruongThanh/images/taobao-brand.png" alt=""></a>
                            </div>
                            <h4 class="hd">Khách hàng với Khách hàng</h4>
                            <p>Là hình thức thương mại điện tử giữa những người tiêu dùng với nhau</p>
                        </div>
                    </li>
                    <li class="src__item">
                        <div class="src-card">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/TruongThanh/images/tmall-brand.png" alt=""></a>
                            </div>
                            <h4 class="hd">Doanh nghiệp với khách hàng</h4>
                            <p>Là hình thức thương mại điện tử giao dịch giữa công ty và người tiêu dùng</p>
                        </div>
                    </li>
                    <li class="src__item">
                        <div class="src-card">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/TruongThanh/images/1688-brand.png" alt=""></a>
                            </div>
                            <h4 class="hd">Doanh nghiệp với doanh nghiệp</h4>
                            <p>Là hình thức thương mại điẹn tử giữa những doanh nghiệp với nhau</p>
                        </div>
                    </li>
                </ul>

            </div>
        </div>

        <div class="services">
            <div class="all">
                <div class="services-wrap">
                    <div class="sec-title">
                        <h2 class="hd">Dịch vụ logistics</h2>
                        <p>Trường Thành Express</p>
                    </div>
                    <div class="serv-content">
                        <ul class="list-services">
                            <asp:Literal ID="ltrService" runat="server"></asp:Literal>
                            <%--<li class="serv__item">
                                <div class="serv-card">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/services-icon-1.png" alt="">
                                    </div>
                                    <h4 class="hd">Đặt hàng, Ship hàng
                                            <br>
                                        quốc tế</h4>
                                    <p>
                                        Đặt hàng từ tất cả các website TMĐT Trung Quốc như taobao, 1688, tmall.
                                    </p>
                                </div>
                            </li>
                            <li class="serv__item">
                                <div class="serv-card">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/services-icon-2.png" alt="">
                                    </div>
                                    <h4 class="hd">Chuyển hàng 2 chiều
                                            <br>
                                        Trung - VIệt</h4>
                                    <p>
                                        Vận chuyển hàng hóa từ khắp các tỉnh thành của Trung Quốc về Việt Nam
                                    </p>
                                </div>
                            </li>
                            <li class="serv__item">
                                <div class="serv-card">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/services-icon-3.png" alt="">
                                    </div>
                                    <h4 class="hd">Chuyển tiền, nạp tiền Alipay, Wechat</h4>
                                    <p>
                                        Thanh toán đơn hàng, nạp tiền tài khoản ngân hàng hoặc Alipay, Wechat, QQ pay.
                                    </p>
                                </div>
                            </li>
                            <li class="serv__item">
                                <div class="serv-card">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/services-icon-4.png" alt="">
                                    </div>
                                    <h4 class="hd">Hổ trợ &amp; tư vấn
                                            <br>
                                        chuyên nghiệp</h4>
                                    <p>
                                        Tư vấn miễn phí. Hỗ trợ tìm kiếm nguồn hàng, hỗ trợ kiểm đếm hàng hóa.
                                    </p>
                                </div>
                            </li>
                            <li class="serv__item">
                                <div class="serv-card">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/services-icon-5.png" alt="">
                                    </div>
                                    <h4 class="hd">Dễ dàng quản lý
                                            <br>
                                        đơn hàng</h4>
                                    <p>
                                        Hệ thống ví điện tử giúp giao dịch nhanh chóng, tích điểm nhận nhiều ưu đãi.
                                    </p>
                                </div>
                            </li>
                            <li class="serv__item">
                                <div class="serv-card">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/services-icon-6.png" alt="">
                                    </div>
                                    <h4 class="hd">Vận chuyển, giao hàng
                                            <br>
                                        tận nơi</h4>
                                    <p>
                                        Ngồi nhà nhập hàng kinh doanh, tiết kiệm thời gian và chi phí hơn trước.
                                    </p>
                                </div>
                            </li>--%>

                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <div class="process">
            <div class="all">
                <div class="process-wrap">
                    <div class="sec-title">
                        <h2 class="hd">Quy trình</h2>
                        <p>Trường Thành Express</p>
                    </div>
                    <div class="process-content">
                        <ul class="list-steps">
                            <asp:Literal ID="ltrStep1" runat="server"></asp:Literal>
                            <%--<li class="step__item active" step-nav="#step-dangky">
                                <div class="step-block">
                                    <div class="icon">
                                        <img src="/App_Themes/TruongThanh/images/step-icon-1.png" alt="">
                                    </div>
                                    <span class="check"><span class="check-box"></span></span>
                                    <div class="hd">Đăng ký tài khoản</div>
                                </div>
                            </li>
                            <li class="step__item" step-nav="#step-caidat">
                                <div class="step-block">
                                    <div class="icon">
                                        <img src="/App_Themes/TruongThanh/images/step-icon-2.png" alt="">
                                    </div>
                                    <span class="check"><span class="check-box"></span></span>
                                    <div class="hd">
                                        Cài đặt công
                                            <br>
                                        cụ mua hàng
                                    </div>
                                </div>
                            </li>
                            <li class="step__item" step-nav="#step-chonhang">
                                <div class="step-block">
                                    <div class="icon">
                                        <img src="/App_Themes/TruongThanh/images/step-icon-3.png" alt="">
                                    </div>
                                    <span class="check"><span class="check-box"></span></span>
                                    <div class="hd">
                                        Chọn hàng &amp;
                                            <br>
                                        thêm vào giỏ hàng
                                    </div>
                                </div>
                            </li>
                            <li class="step__item" step-nav="#step-guidon">
                                <div class="step-block">
                                    <div class="icon">
                                        <img src="/App_Themes/TruongThanh/images/step-icon-4.png" alt="">
                                    </div>
                                    <span class="check"><span class="check-box"></span></span>
                                    <div class="hd">Gửi đơn hàng</div>
                                </div>
                            </li>
                            <li class="step__item" step-nav="#step-datcoc">
                                <div class="step-block">
                                    <div class="icon">
                                        <img src="/App_Themes/TruongThanh/images/step-icon-5.png" alt="">
                                    </div>
                                    <span class="check"><span class="check-box"></span></span>
                                    <div class="hd">Đặt cọc đơn hàng</div>
                                </div>
                            </li>
                            <li class="step__item" step-nav="#step-nhanhang">
                                <div class="step-block">
                                    <div class="icon">
                                        <img src="/App_Themes/TruongThanh/images/step-icon-6.png" alt="">
                                    </div>
                                    <span class="check"><span class="check-box"></span></span>
                                    <div class="hd">
                                        Nhận hàng &amp;
                                            <br>
                                        thanh toán
                                    </div>
                                </div>
                            </li>--%>
                        </ul>
                        <div class="steps-content">
                            <asp:Literal ID="ltrStep2" runat="server"></asp:Literal>
                           <%-- <div id="step-dangky">
                                <div class="stepct-block">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/step-img.png" alt="">
                                    </div>
                                    <div class="detail">
                                        <h3 class="hd">Đăng ký tài khoản</h3>
                                        <p>
                                            Nhập các thông tin cá nhân bắt buộc vào ô, lưu ý nhập chính xác các thông tin để đảm bảo cho việc quản lí đơn hàng và nhận hàng của bạn.
                                        </p>
                                        <div class="button">
                                            <a href="/dang-ky" class="mn-btn btn-2">Đăng ký</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="step-caidat">
                                <div class="stepct-block">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/step-img.png" alt="">
                                    </div>
                                    <div class="detail">
                                        <h3 class="hd">Cài đặt công cụ mua hàng</h3>
                                        <p>
                                            Click vào cài đặt công cụ đặt hàng của Trường Thành Express. Công cụ hỗ trợ đặt hàng các website taobao, tmall, 1688.
                                        </p>

                                    </div>
                                </div>
                            </div>
                            <div id="step-chonhang">
                                <div class="stepct-block">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/step-img.png" alt="">
                                    </div>
                                    <div class="detail">
                                        <h3 class="hd">Chọn hàng & thêm vào giỏ hàng</h3>
                                        <p>
                                            Truy cập vào các trang mua sắm Taobao.com, Tmall.com, 1688.com … chọn hàng và thêm hàng vào giỏ.
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div id="step-guidon">
                                <div class="stepct-block">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/step-img.png" alt="">
                                    </div>
                                    <div class="detail">
                                        <h3 class="hd">Gửi đơn hàng</h3>
                                        <p>
                                            Quay lại website Trường Thành Express và kiểm tra giỏ hàng Click vào “Gửi đơn hàng” để tạo đơn hàng,chờ xác nhận đặt hàng thành công.
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div id="step-datcoc">
                                <div class="stepct-block">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/step-img.png" alt="">
                                    </div>
                                    <div class="detail">
                                        <h3 class="hd">Đặt cọc đơn hàng</h3>
                                        <p>
                                            Kiểm tra đơn hàng và đặt cọc tiền hàng qua hình thức chuyển khoản hoặc trực tiếp tại các văn phòng giao dịch gần nhất của Trường Thành Express.
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div id="step-nhanhang">
                                <div class="stepct-block">
                                    <div class="img">
                                        <img src="/App_Themes/TruongThanh/images/step-img.png" alt="">
                                    </div>
                                    <div class="detail">
                                        <h3 class="hd">Nhận hàng & thanh toán
                                        </h3>
                                        <p>
                                            Quý khách nhận được thông báo hàng về Việt Nam. Quý khách thanh toán số tiền còn thiếu qua hình 
                                        thức chuyển khoản hoặc trực tiếp. Sau khi thanh toán quý khách hàng có thể yêu cầu ship hàng 
                                        hoặc trực tiếp nhận hàng tại kho của Trường Thành Express.
                                        </p>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="rights-ensure">
            <div class="all">
                <div class="rights">
                    <div class="sec-title cen-title">
                        <h2 class="hd">Quyền lợi khách hàng</h2>
                    </div>
                    <ul class="list-righ-ensu">
                        <asp:Literal ID="ltrQL1" runat="server"></asp:Literal>
                        <%--<li class="righ-ensu__item">
                            <div class="left-icon-card">
                                <div class="img">
                                    <img src="/App_Themes/TruongThanh/images/benefit-icon-1.png" alt="">
                                </div>
                                <div class="ct">
                                    <h4 class="hd">Khách hàng thân thiết</h4>
                                    <p>Tích điểm thành viên từ mỗi đơn hàng và nhận được nhiều ưu đãi </p>
                                </div>
                            </div>
                        </li>
                        <li class="righ-ensu__item">
                            <div class="left-icon-card">
                                <div class="img">
                                    <img src="/App_Themes/TruongThanh/images/benefit-icon-2.png" alt="">
                                </div>
                                <div class="ct">
                                    <h4 class="hd">Hệ thống thông minh</h4>
                                    <p>Quản lý bằng hệ thống hiện đại quy trình chặt chẽ tránh nhầm hàng, mất hàng</p>
                                </div>
                            </div>
                        </li>
                        <li class="righ-ensu__item">
                            <div class="left-icon-card">
                                <div class="img">
                                    <img src="/App_Themes/TruongThanh/images/benefit-icon-3.png" alt="">
                                </div>
                                <div class="ct">
                                    <h4 class="hd">Hổ trợ và tư vấn</h4>
                                    <p>Hỗ trợ và tư vấn: Luôn sẵn sàng giải đáp mọi thắc mắc của bạn.</p>
                                </div>
                            </div>
                        </li>--%>
                    </ul>
                </div>
                <div class="ensure">
                    <div class="sec-title cen-title">
                        <h2 class="hd">Cam kết dịch vụ</h2>
                    </div>
                    <ul class="list-righ-ensu">
                        <asp:Literal ID="ltrQL2" runat="server"></asp:Literal>
                        <%--<li class="righ-ensu__item">
                            <div class="left-icon-card">
                                <div class="img">
                                    <img src="/App_Themes/TruongThanh/images/benefit-icon-4.png" alt="">
                                </div>
                                <div class="ct">
                                    <h4 class="hd">Tỷ giá</h4>
                                    <p>Tỷ giá ổn định chuẩn xác 100% theo ngân hàng Công Thương Việt Nam</p>
                                </div>
                            </div>
                        </li>
                        <li class="righ-ensu__item">
                            <div class="left-icon-card">
                                <div class="img">
                                    <img src="/App_Themes/TruongThanh/images/benefit-icon-5.png" alt="">
                                </div>
                                <div class="ct">
                                    <h4 class="hd">Thời gian vận chuyển</h4>
                                    <p>Thời gian vận chuyển ổn định chuẩn xác</p>
                                </div>
                            </div>
                        </li>
                        <li class="righ-ensu__item">
                            <div class="left-icon-card">
                                <div class="img">
                                    <img src="/App_Themes/TruongThanh/images/benefit-icon-6.png" alt="">
                                </div>
                                <div class="ct">
                                    <h4 class="hd">Cam kết hổ trợ</h4>
                                    <p>Chúng tôi phát triển công nghệ để hỗ trợ Quý khách Quản lý mọi giao dịch và lộ trình hàng hoá 24/24</p>
                                </div>
                            </div>
                        </li>--%>
                    </ul>
                </div>
            </div>
        </div>
        <div class="contact">
            <div class="map-frame">
                <iframe src="https://www.google.com/maps/embed?pb=!1m26!1m12!1m3!1d3919.447947751103!2d106.65261921480077!3d10.776962992321197!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!4m11!3e6!4m3!3m2!1d10.777098299999999!2d106.6549514!4m5!1s0x31752ed189fa855d%3A0xf63e15bfce46baef!2zQ8O0bmcgdHkgVE5ISCAtIE1PTkEgTUVESUEsIEzhuqd1IDIgMzE5IGMxNiBMw70gVGjGsOG7nW5nIEtp4buHdCwgcGjGsOG7nW5nIDE1LCBxdeG6rW4gMTEsIFBoxrDhu51uZyAxNSwgSG8gQ2hpIE1pbmgsIEjhu5MgQ2jDrSBNaW5oIDc2MDAwMA!3m2!1d10.776963!2d106.6548079!5e0!3m2!1svi!2s!4v1533801886638"
                    width="" height="" frameborder="0" style="border: 0" allowfullscreen></iframe>
            </div>
            <div class="contact-box">
                <div class="all">
                    <div class="box-wrap">
                        <div class="ctb__child">
                            <span class="icon"><i class="fas fa-map-marker-alt"></i></span>
                            <div class="ct">
                                <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                <%--319-C16 Lý Thường Kiệt
                                    <br>
                                Phường 15, Quận 11--%>
                            </div>
                        </div>
                        <div class="ctb__child">
                            <span class="icon"><i class="fas fa-phone"></i></span>
                            <div class="ct">
                                <p>Hotline:</p>
                                <p>
                                    <asp:Literal ID="ltrhotline" runat="server"></asp:Literal>
                                    <%--<a href="tel:+">0126.922.0162</a>--%></p>
                            </div>
                        </div>
                        <div class="ctb__child">
                            <span class="icon"><i class="far fa-clock"></i></span>
                            <div class="ct">
                                <p>Giờ hoạt động:</p>
                                <p>
                                    <asp:Literal ID="ltrTimework" runat="server"></asp:Literal></p>
                            </div>
                        </div>
                        <div class="ctb__child">
                            <span class="icon"><i class="fas fa-envelope"></i></span>
                            <div class="ct">
                                <p>Email:</p>
                                <p>
                                    <asp:Literal ID="ltrEmail" runat="server"></asp:Literal></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <script type="text/javascript">
        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }
        function close_popup_ms() {
            $("#pupip_home").animate({ "opacity": 0 }, 400);
            $("#bg_popup_home").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip_home").remove();
                $(".zoomContainer").remove();
                $("#bg_popup_home").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
            }, 500);
        }
        function closeandnotshow() {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/setNotshow",
                //data: "{ID:'" + id + "',UserName:'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    close_popup_ms();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });

        }
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/getPopup",
                //data: "{ID:'" + id + "',UserName:'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "null") {
                        var data = JSON.parse(msg.d);
                        var title = data.NotiTitle;
                        var content = data.NotiContent;
                        var email = data.NotiEmail;
                        var obj = $('form');
                        $(obj).css('overflow', 'hidden');
                        $(obj).attr('onkeydown', 'keyclose_ms(event)');
                        var bg = "<div id='bg_popup_home'></div>";
                        var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                                 "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
                        fr += "<div class=\"popup_header\">";
                        fr += title;
                        fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
                        fr += "</div>";
                        fr += "     <div class=\"changeavatar\">";
                        fr += "         <div class=\"content1\">";
                        fr += content;
                        fr += "         </div>";
                        fr += "         <div class=\"content2\">";
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close-full\" onclick='closeandnotshow()'>Đóng & không hiện thông báo</a>";
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";

                        fr += "         </div>";
                        fr += "     </div>";
                        fr += "<div class=\"popup_footer\">";
                        fr += "<span class=\"float-right\">" + email + "</span>";
                        fr += "</div>";
                        fr += "   </div>";
                        fr += "</div>";
                        $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                        $(fr).appendTo($(obj));
                        setTimeout(function () {
                            $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                            $("#bg_popup").attr("onclick", "close_popup_ms()");
                        }, 1000);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
        });
    </script>
    <style>
        #bg_popup_home {
            position: fixed;
            width: 100%;
            height: 100%;
            background-color: #333;
            opacity: 0.7;
            filter: alpha(opacity=70);
            left: 0px;
            top: 0px;
            z-index: 999999999;
            opacity: 0;
            filter: alpha(opacity=0);
        }

        #popup_ms_home {
            background: #fff;
            border-radius: 0px;
            box-shadow: 0px 2px 10px #fff;
            float: left;
            position: fixed;
            width: 735px;
            z-index: 10000;
            left: 50%;
            margin-left: -370px;
            top: 200px;
            opacity: 0;
            filter: alpha(opacity=0);
            height: 360px;
        }

            #popup_ms_home .popup_body {
                border-radius: 0px;
                float: left;
                position: relative;
                width: 735px;
            }

            #popup_ms_home .content {
                /*background-color: #487175;     border-radius: 10px;*/
                margin: 12px;
                padding: 15px;
                float: left;
            }

            #popup_ms_home .title_popup {
                /*background: url("../images/img_giaoduc1.png") no-repeat scroll -200px 0 rgba(0, 0, 0, 0);*/
                color: #ffffff;
                font-family: Arial;
                font-size: 24px;
                font-weight: bold;
                height: 35px;
                margin-left: 0;
                margin-top: -5px;
                padding-left: 40px;
                padding-top: 0;
                text-align: center;
            }

            #popup_ms_home .text_popup {
                color: #fff;
                font-size: 14px;
                margin-top: 20px;
                margin-bottom: 20px;
                line-height: 20px;
            }

                #popup_ms_home .text_popup a.quen_mk, #popup_ms_home .text_popup a.dangky {
                    color: #FFFFFF;
                    display: block;
                    float: left;
                    font-style: italic;
                    list-style: -moz-hangul outside none;
                    margin-bottom: 5px;
                    margin-left: 110px;
                    -webkit-transition-duration: 0.3s;
                    -moz-transition-duration: 0.3s;
                    transition-duration: 0.3s;
                }

                    #popup_ms_home .text_popup a.quen_mk:hover, #popup_ms_home .text_popup a.dangky:hover {
                        color: #8cd8fd;
                    }

            #popup_ms_home .close_popup {
                background: url("/App_Themes/Camthach/images/close_button.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
                display: block;
                height: 28px;
                position: absolute;
                right: 0px;
                top: 5px;
                width: 26px;
                cursor: pointer;
                z-index: 10;
            }

        #popup_content_home {
            height: auto;
            position: fixed;
            background-color: #fff;
            top: 15%;
            z-index: 999999999;
            left: 25%;
            border-radius: 10px;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 50%;
            padding: 20px;
        }

        #popup_content_home {
            padding: 0;
        }

        .popup_header, .popup_footer {
            float: left;
            width: 100%;
            background: #29aae1;
            padding: 10px;
            position: relative;
            color: #fff;
        }

        .popup_header {
            font-weight: bold;
            font-size: 16px;
            text-transform: uppercase;
        }

        .close_message {
            top: 10px;
        }

        .changeavatar {
            padding: 10px;
            margin: 5px 0;
            float: left;
            width: 100%;
        }

        .float-right {
            float: right;
        }

        .content1 {
            float: left;
            width: 100%;
        }

        .content2 {
            float: left;
            width: 100%;
            border-top: 1px solid #eee;
            clear: both;
            margin-top: 10px;
        }

        .btn.btn-close {
            float: right;
            background: #29aae1;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding:10px 20px;
        }

            .btn.btn-close:hover {
                background: #1f85b1;
            }

        .btn.btn-close-full {
            float: right;
            background: #7bb1c7;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding:10px 20px;
        }

            .btn.btn-close-full:hover {
                background: #6692a5;
            }
    </style>
</asp:Content>
