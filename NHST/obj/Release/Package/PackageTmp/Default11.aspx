<%@ Page Title="" Language="C#" MasterPageFile="~/PGS1688Master.Master" AutoEventWireup="true" CodeBehind="Default11.aspx.cs" Inherits="NHST.Default10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home" style="margin-top:0">

        <div class="banner">
            <div class="all">
                <div class="banner-content">
                    <div class="bn__header" style="color:#ff0;">
                        Order Hàng Trung quốc
            <h1 class="hd">Giá rẻ - Nhanh chóng - Uy tín</h1>
                    </div>
                    <div class="line-br line-white">
                        <span class="line__icon"></span>
                    </div>
                    <div class="bn__sub">
                        <p>Giúp khách hàng nhập hàng tận gốc</p>
                        <p>Phí dịch vụ chỉ <span class="color2">từ 1%</span> - Vận chuyển từ <span class="color2">8K/KG</span></p>
                    </div>
                    <div class="addon-button">
                        <a href="" class="mn-btn gg-btn"><i class="fab fa-chrome"></i>Chrome</a>
                        <a href="" class="mn-btn cc-btn"><i class="fab fa-chrome"></i>Cốc cốc</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="search-prod">
            <div class="all">
                <div class="sec-title">
                    <h2 class="hd">Tìm kiếm sản phẩm</h2>
                    <div class="line-br">
                        <span class="line__icon"></span>
                    </div>
                </div>
                <div class="form-search">
                    <div class="select-source">
                        <div class="slsource__child">
                            <label class="radio-check">
                                <input type="radio" name="source" checked value="taobao">
                                <span class="label-box">taobao</span>
                            </label>
                        </div>
                        <div class="slsource__child">
                            <label class="radio-check">
                                <input type="radio" name="source" value="1688">
                                <span class="label-box">1688</span>
                            </label>
                        </div>
                        <div class="slsource__child">
                            <label class="radio-check">
                                <input type="radio" name="source" value="tmall">
                                <span class="label-box">tmall</span>
                            </label>
                        </div>
                    </div>
                    <div class="input-form">
                        <asp:TextBox ID="txtSearchText" runat="server" placeholder="Tìm kiếm sản phẩm" CssClass="f-control"></asp:TextBox>                        
                        <span class="submit">
                            <a href="javascript:;" onclick="searchProduct()" class="mn-btn btn-1">Tìm kiếm</a>
                            <asp:Button ID="btnsearchpro" runat="server" CssClass="mn-btn" Text="Tìm kiếm" OnClick="btnsearchpro_Click"
                                OnClientClick="document.forms[0].target = '_blank';" Style="display: none" />
                        </span>
                    </div>
                </div>
            </div>
        </div>

        <div class="services">
            <div class="all">
                <div class="sec-title">
                    <h2 class="hd">Dịch vụ 1688PGS</h2>
                    <div class="line-br">
                        <span class="line__icon"></span>
                    </div>
                </div>
                <ul class="list-services">
                    <li class="serv__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-1.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-hv1.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Đặt hàng,
                                    <br>
                                Ship hàng quốc tế</a></h4>
                            <p>
                                Đặt hàng từ tất cả các website TMĐT Trung Quốc như taobao, 1688, tmall.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                    <li class="serv__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-2.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-hv2.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Chuyển hàng 2 chiều
                                    <br>
                                Trung - Việt</a></h4>
                            <p>
                               Vận chuyển hàng hóa từ khắp các tỉnh thành của Trung Quốc về Việt Nam.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                    <li class="serv__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-3.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-hv3.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Chuyển tiền, nạp tiền
                                    <br>
                                Alipay, Wechat</a></h4>
                            <p>
                                Thanh toán đơn hàng, nạp tiền tài khoản ngân hàng hoặc Alipay, Wechat, QQ pay.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                    <li class="serv__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-4.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-hv4.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Hổ trợ và tư vấn
                                    <br>
                                chuyên nghiệp</a></h4>
                            <p>
                                Tư vấn miễn phí. Hỗ trợ tìm kiếm nguồn hàng, hỗ trợ kiểm đếm hàng hóa.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                    <li class="serv__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-5.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-hv5.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Dễ dàng quản lý
                                    <br>
                                đơn hàng</a></h4>
                            <p>
                                Hệ thống ví điện tử giúp giao dịch nhanh chóng, tích điểm nhận nhiều ưu đãi.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                    <li class="serv__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-6.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/service-icon-hv6.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Giao hàng
                                    <br>
                                tận nơi</a></h4>
                            <p>
                                Ngồi nhà nhập hàng kinh doanh, tiết kiệm thời gian và chi phí hơn trước.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <div class="steps-present-wrap">

            <div class="steps-order">
                <div class="all">
                    <div class="sec-title">
                        <h2 class="hd">Quy trình đặt hàng</h2>
                        <div class="line-br line-white">
                            <span class="line__icon"></span>
                        </div>
                    </div>
                    <ul class="list-steps">
                        <li class="step__item active" step-nav="#step-dangky">
                            <div class="step-block">
                                <div class="icon">
                                    <i class="fas fa-file-alt"></i>
                                </div>
                                <div class="hd">Đăng ký tài khoản</div>
                            </div>
                        </li>
                        <li class="step__item" step-nav="#step-caidat">
                            <div class="step-block">
                                <div class="icon">
                                    <i class="fas fa-cog"></i>
                                </div>
                                <div class="hd">Cài đặt công cụ mua hàng</div>
                            </div>
                        </li>
                        <li class="step__item" step-nav="#step-chonhang">
                            <div class="step-block">
                                <div class="icon">
                                    <i class="fas fa-shopping-bag"></i>
                                </div>
                                <div class="hd">Chọn hàng và thêm vào giỏ hàng</div>
                            </div>
                        </li>
                        <li class="step__item" step-nav="#step-guidon">
                            <div class="step-block">
                                <div class="icon">
                                    <i class="fas fa-share-square"></i>
                                </div>
                                <div class="hd">Gửi đơn đặng hàng</div>
                            </div>
                        </li>
                        <li class="step__item" step-nav="#step-datcoc">
                            <div class="step-block">
                                <div class="icon">
                                    <i class="fas fa-hand-holding-usd"></i>
                                </div>
                                <div class="hd">Đặt cọc tiền hàng</div>
                            </div>
                        </li>
                        <li class="step__item" step-nav="#step-nhanhang">
                            <div class="step-block">
                                <div class="icon">
                                    <i class="fas fa-box"></i>
                                </div>
                                <div class="hd">Nhận hàng và thanh toán</div>
                            </div>
                        </li>
                    </ul>

                    <div class="content-wrap">
                        <div id="step-dangky">
                            <div class="stepct-block">
                                <div class="detail">
                                    <h3 class="hd">Đăng ký tài khoản</h3>
                                    <p>
                                        Nhập các thông tin cá nhân bắt buộc vào ô, lưu ý nhập chính xác các thông tin để đảm bảo cho việc quản lí đơn hàng và nhận hàng của bạn.
                                    </p>
                                    <div class="button">
                                        <a href="/dang-ky" class="mn-btn btn-2">Đăng ký</a>
                                    </div>
                                </div>
                                <div class="img">
                                    <img src="/App_Themes/PGS1688/images/step-img.jpg" alt="">
                                </div>
                            </div>
                        </div>

                        <div id="step-caidat">
                            <div class="stepct-block">
                                <div class="detail">
                                    <h3 class="hd">Cài đặt công cụ mua hàng</h3>
                                    <p>
                                        Click vào cài đặt công cụ đặt hàng của PEGASUS Viet Nam. Công cụ hỗ trợ đặt hàng các website taobao, tmall, 1688.
                                    </p>
                                    <%--<div class="button">
                                        <a href="/dang-ky" class="mn-btn btn-2">Đăng ký</a>
                                    </div>--%>
                                </div>
                                <div class="img">
                                    <img src="/App_Themes/PGS1688/images/step-img.jpg" alt="">
                                </div>
                            </div>
                        </div>
                        <div id="step-chonhang">
                            <div class="stepct-block">
                                <div class="detail">
                                    <h3 class="hd">Chọn hàng và thêm hàng vào giỏ</h3>
                                    <p>
                                        Truy cập vào các trang mua sắm Taobao.com, Tmall.com, 1688.com … chọn hàng và thêm hàng vào giỏ.
                                    </p>
                                    <%--<div class="button">
                                        <a href="/dang-ky" class="mn-btn btn-2">Đăng ký</a>
                                    </div>--%>
                                </div>
                                <div class="img">
                                    <img src="/App_Themes/PGS1688/images/step-img.jpg" alt="">
                                </div>
                            </div>
                        </div>
                        <div id="step-guidon">
                            <div class="stepct-block">
                                <div class="detail">
                                    <h3 class="hd">Gửi đơn đặt hàng</h3>
                                    <p>
                                        Quay lại website PEGASUS Viet Nam và kiểm tra giỏ hàng Click vào “Gửi đơn hàng” để tạo đơn hàng,chờ xác nhận đặt hàng thành công.
                                    </p>
                                    <%--<div class="button">
                                        <a href="/dang-ky" class="mn-btn btn-2">Đăng ký</a>
                                    </div>--%>
                                </div>
                                <div class="img">
                                    <img src="/App_Themes/PGS1688/images/step-img.jpg" alt="">
                                </div>
                            </div>
                        </div>
                        <div id="step-datcoc">
                            <div class="stepct-block">
                                <div class="detail">
                                    <h3 class="hd">Đặt cọc tiền hàng</h3>
                                    <p>
                                        Kiểm tra đơn hàng và đặt cọc tiền hàng qua hình thức chuyển khoản hoặc trực tiếp tại các văn phòng giao dịch gần nhất của PEGASUS Viet Nam.
                                    </p>
                                    <%--<div class="button">
                                        <a href="/dang-ky" class="mn-btn btn-2">Đăng ký</a>
                                    </div>--%>
                                </div>
                                <div class="img">
                                    <img src="/App_Themes/PGS1688/images/step-img.jpg" alt="">
                                </div>
                            </div>
                        </div>
                        <div id="step-nhanhang">
                            <div class="stepct-block">
                                <div class="detail">
                                    <h3 class="hd">Nhận hàng và thanh toán</h3>
                                    <p>
                                        Quý khách nhận được thông báo hàng về Việt Nam. Quý khách thanh toán số tiền còn thiếu qua hình 
                                        thức chuyển khoản hoặc trực tiếp. Sau khi thanh toán quý khách hàng có thể yêu cầu ship hàng 
                                        hoặc trực tiếp nhận hàng tại kho của PEGASUS Viet Nam.
                                    </p>
                                    <%--<div class="button">
                                        <a href="/dang-ky" class="mn-btn btn-2">Đăng ký</a>
                                    </div>--%>
                                </div>
                                <div class="img">
                                    <img src="/App_Themes/PGS1688/images/step-img.jpg" alt="">
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="present">
                <div class="all">

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

        </div>

        <div class="benefits">
            <div class="all">
                <div class="sec-title">
                    <h2 class="hd">Quyền lợi khách hàng</h2>
                    <div class="line-br">
                        <span class="line__icon"></span>
                    </div>
                </div>
                <ul class="list-benefits">
                    <li class="bene__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/benefit-icon-1.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/benefit-icon-hv1.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Khách hàng thân thiết</a></h4>
                            <p>
                                Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                    <li class="bene__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/benefit-icon-2.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/benefit-icon-hv2.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Marketing &amp; bán hàng</a></h4>
                            <p>
                                Hỗ trợ Quý Khách kết nối tới các giải pháp Marketing & bán hàng, cung cấp hệ sinh thái đầy đủ từ nhập hàng tới bán hàng và phân phối.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                    <li class="bene__item">
                        <div class="info-ct-card">
                            <div class="img">
                                <span class="current-icon">
                                    <img src="/App_Themes/PGS1688/images/benefit-icon-3.png" alt="">
                                </span>
                                <span class="hover-icon">
                                    <img src="/App_Themes/PGS1688/images/benefit-icon-hv3.png" alt="">
                                </span>
                            </div>
                            <h4 class="hd"><a href="">Ưu đãi theo sản lượng tháng</a></h4>
                            <p>
                                Hoàn phí vận chuyển theo sản lượng đặt hàng theo từng tháng.
                            </p>
                            <%--<a href="" class="mn-btn btn-right">Xem thêm</a>--%>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <div class="contact">
            <div class="ct-box">
                <div class="ct-title">
                    <h2 class="hd">Thông tin liên hệ</h2>
                </div>
                <div class="ct-content-wrap">
                    <div class="contact-card">
                        <div class="ct-icon">
                            <i class="fas fa-map-marker-alt"></i>
                        </div>
                        <div class="ct-detail">
                            <p class="main">
                                <asp:Literal ID="ltrAddress" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                    <div class="contact-card">
                        <div class="ct-icon">
                            <i class="fas fa-map-marker-alt"></i>
                        </div>
                        <div class="ct-detail">
                            <p class="sub">Hotline:</p>
                            <p class="main">
                                <asp:Literal ID="ltrhotline" runat="server"></asp:Literal>
                            </p>
                        </div>
                    </div>
                    <div class="contact-card">
                        <div class="ct-icon">
                            <i class="fas fa-phone"></i>
                        </div>
                        <div class="ct-detail">
                            <p class="sub">Giờ hoạt động</p>
                            <p class="main">
                                <asp:Literal ID="ltrTimework" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                    <div class="contact-card">
                        <div class="ct-icon">
                            <i class="fas fa-envelope"></i>
                        </div>
                        <div class="ct-detail">
                            <p class="sub">Email:</p>
                            <p class="main">
                                <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="google-map">
                <div class="map" id="map-canvas"></div>
            </div>
        </div>
    </main>
    <asp:HiddenField ID="hdfWebSearch" runat="server" />
    <script type="text/javascript">
        function searchProduct()
        {
            var web = $('input[name=source]:checked').val();
            var text = $("#<%=txtSearchText.ClientID%>").val();
            if(!isEmpty(text))
            {
                $("#<%=hdfWebSearch.ClientID%>").val(web);
                $("#<%=btnsearchpro.ClientID%>").click();
            }
            else
            {
                alert('Vui lòng nhập từ khóa tìm kiếm.');
            }
        }
    </script>
</asp:Content>
