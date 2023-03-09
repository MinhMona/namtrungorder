<%@ Page Title="" Language="C#" MasterPageFile="~/daiphongMaster.Master" AutoEventWireup="true" CodeBehind="Default5.aspx.cs" Inherits="NHST.Default4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section class="banner">
            <div class="bg-banner" style="background-image: url('/App_Themes/daiphong/images/banner-background.jpg')">
                <div class="all">
                    <h1 class="bg-title">Bạn cần gì từ <span class="hl-txt">chúng tôi ?</span></h1>
                </div>
            </div>

            <div class="banner-info">
                <div class="all">
                    <div class="banner-info__left">
                        <div class="img">
                            <img src="/App_Themes/daiphong/images/mobile-icon.png" alt="">
                        </div>
                        <div class="title">
                            <a href="">Liên hệ
                                <br>
                                đặt hàng</a>
                        </div>
                    </div>
                    <div class="banner__action">
                        <p class="title">Công cụ đặt hàng</p>
                        <div class="button">
                            <a href="" class="btn btn-3">
                                <i class="fa fa-chrome"></i>
                                CHROME
                            </a>
                            <a href="" class="btn btn-4">
                                <i class="fa fa-chrome"></i>
                                CỐC CỐC
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="services wow bounceInRight">
            <div class="all">
                <h4 class="sec__title center-txt">Dịch vụ</h4>
                <ul class="ser-list">
                    <li class="item hvr-rectangle-out">
                        <div class="mona-airplane"></div>
                        <p class="item__title">
                            Đặt hàng,<br>
                            Ship hàng quốc tế
                        </p>
                        <p class="item__sub">Đặt hàng từ tất cả các website TMĐT Trung Quốc như taobao, 1688, tmall.</p>
                    </li>
                    <li class="item hvr-rectangle-out">
                        <div class="mona-international"></div>
                        <p class="item__title">
                            Chuyển hàng<br>
                            2 chiều Trung - Việt
                        </p>
                        <p class="item__sub">Vận chuyển hàng hóa từ khắp các tỉnh thành của Trung  Quốc về Việt Nam </p>
                    </li>
                    <li class="item hvr-rectangle-out">
                        <div class="mona-bankcard"></div>
                        <p class="item__title">
                            Chuyển tiền, nạp tiền<br>
                            Alipay, Wechat
                        </p>
                        <p class="item__sub">Thanh toán đơn hàng, nạp tiền tài khoản ngân hàng hoặc Alipay, Wechat, QQ pay.</p>
                    </li>
                    <li class="item hvr-rectangle-out">
                        <div class="mona-chat"></div>
                        <p class="item__title">
                            Hổ trợ và tư vấn<br>
                            chuyển nghiệp
                        </p>
                        <p class="item__sub">Tư vấn miễn phí. Hỗ trợ tìm kiếm nguồn hàng, hỗ trợ kiểm đếm hàng hóa.</p>
                    </li>
                    <li class="item hvr-rectangle-out">
                        <div class="mona-comp-link"></div>
                        <p class="item__title">
                            Dễ dàng quản lý<br>
                            đơn hàng
                        </p>
                        <p class="item__sub">Hệ thống ví điện tử giúp giao dịch nhanh chóng, tích điểm nhận nhiều ưu đãi.</p>
                    </li>
                    <li class="item hvr-rectangle-out">
                        <div class="mona-truck"></div>
                        <p class="item__title">
                            Giao hàng<br>
                            tận nơi
                        </p>
                        <p class="item__sub">Ngồi nhà nhập hàng kinh doanh, tiết kiệm thời gian và chi phí hơn trước.</p>
                    </li>
                </ul>
            </div>
        </section>

        <section class="guide">
            <div class="all">
                <h4 class="sec__title center-txt">Hướng dẫn đăng kí</h4>
            </div>
            <div class="guide-content">
                <div class="guide-step">
                    <div class="guide-step__tab home-guide__current" tab="dangki">
                        <p>Đăng kí tài khoản</p>
                    </div>
                    <div class="guide-step__tab" tab="caidat">
                        <p>Cài đặt công cụ mua hàng</p>
                    </div>
                    <div class="guide-step__tab" tab="chonhang">
                        <p>Chọn hàng và thêm vào giỏ hàng</p>
                    </div>
                    <div class="guide-step__tab" tab="guihang">
                        <p>Gửi đơn hàng</p>
                    </div>
                    <div class="guide-step__tab" tab="datcoc">
                        <p>Đặt cọc tiền hàng</p>
                    </div>
                    <div class="guide-step__tab" tab="nhanhang">
                        <p>Nhận hàng và thanh toán</p>
                    </div>
                </div>
                <div id="guide-swap-tab">
                    <div class="guide-info" tab="dangki" style="background-image: url('/App_Themes/daiphong/images/huongdan-background.jpg')">
                        <div class="img">
                            <img src="/App_Themes/daiphong/images/huongdan-dangki-icon.png" alt="">
                        </div>
                        <p class="guide-info__title">Đăng kí tài khoản</p>
                        <p class="guide-info__sub">
                            Color is so powerful that it can persuade, motivate, inspire and touch people’s soft spot – the heart. This is the reason
              why understanding colors is pretty crucial in relating and communicating with other people. Not only that, it is also
              important to businesses in order for their business to sell.
                        </p>
                        <a href="/dang-ky" class="btn btn-1">Đăng kí</a>
                    </div>

                    <div class="guide-info" tab="caidat" style="background-image: url('/App_Themes/daiphong/images/huongdan-background.jpg')">
                        <div class="img">
                            <img src="/App_Themes/daiphong/images/huongdan-dangki-icon.png" alt="">
                        </div>
                        <p class="guide-info__title">Cài đặt công cụ mua hàng</p>
                        <p class="guide-info__sub">
                            Color is so powerful that it can persuade, motivate, inspire and touch people’s soft spot – the heart. This is the reason
              why understanding colors is pretty crucial in relating and communicating with other people. Not only that, it is also
              important to businesses in order for their business to sell.
                        </p>
                        <a href="javascript:;" class="btn btn-1">Cài đặt</a>
                    </div>

                    <div class="guide-info" tab="chonhang" style="background-image: url('/App_Themes/daiphong/images/huongdan-background.jpg')">
                        <div class="img">
                            <img src="/App_Themes/daiphong/images/huongdan-dangki-icon.png" alt="">
                        </div>
                        <p class="guide-info__title">Chọn hàng và thêm vào giỏ hàng</p>
                        <p class="guide-info__sub">
                            Color is so powerful that it can persuade, motivate, inspire and touch people’s soft spot – the heart. This is the reason
              why understanding colors is pretty crucial in relating and communicating with other people. Not only that, it is also
              important to businesses in order for their business to sell.
                        </p>
                        <a href="javascript:;" class="btn btn-1">Chọn hàng</a>
                    </div>

                    <div class="guide-info" tab="guihang" style="background-image: url('/App_Themes/daiphong/images/huongdan-background.jpg')">
                        <div class="img">
                            <img src="/App_Themes/daiphong/images/huongdan-dangki-icon.png" alt="">
                        </div>
                        <p class="guide-info__title">Gửi đơn hàng</p>
                        <p class="guide-info__sub">
                            Color is so powerful that it can persuade, motivate, inspire and touch people’s soft spot – the heart. This is the reason
              why understanding colors is pretty crucial in relating and communicating with other people. Not only that, it is also
              important to businesses in order for their business to sell.
                        </p>
                        <a href="javascript:;" class="btn btn-1">Gửi hàng</a>
                    </div>

                    <div class="guide-info" tab="datcoc" style="background-image: url('/App_Themes/daiphong/images/huongdan-background.jpg')">
                        <div class="img">
                            <img src="/App_Themes/daiphong/images/huongdan-dangki-icon.png" alt="">
                        </div>
                        <p class="guide-info__title">Đặt cọc tiền hàng và thanh toán</p>
                        <p class="guide-info__sub">
                            Color is so powerful that it can persuade, motivate, inspire and touch people’s soft spot – the heart. This is the reason
              why understanding colors is pretty crucial in relating and communicating with other people. Not only that, it is also
              important to businesses in order for their business to sell.
                        </p>
                        <a href="javascript:;" class="btn btn-1">Đặt cọc</a>
                    </div>

                    <div class="guide-info" tab="nhanhang" style="background-image: url('/App_Themes/daiphong/images/huongdan-background.jpg')">
                        <div class="img">
                            <img src="/App_Themes/daiphong/images/huongdan-dangki-icon.png" alt="">
                        </div>
                        <p class="guide-info__title">Nhận hàng và thanh toán</p>
                        <p class="guide-info__sub">
                            Color is so powerful that it can persuade, motivate, inspire and touch people’s soft spot – the heart. This is the reason
              why understanding colors is pretty crucial in relating and communicating with other people. Not only that, it is also
              important to businesses in order for their business to sell.
                        </p>
                        <a href="javascript:;" class="btn btn-1">Nhận hàng</a>
                    </div>

                </div>
            </div>
        </section>

        <section class="about">
            <div class="all">

                <div class="features wow fadeInLeft">
                    <h4 class="sec__title">Quyền lợi khách hàng</h4>

                    <ul class="features-content__list">
                        <li class="features-content__item hvr-sweep-to-right">
                            <div class="mona-numb1"></div>
                            <div class="info">
                                <p class="item__title">Khách hàng thân thiết</p>
                                <p class="item__sub">Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.</p>
                            </div>
                        </li>
                        <li class="features-content__item hvr-sweep-to-right">
                            <div class="mona-monitor-cart"></div>
                            <div class="info">
                                <p class="item__title">Khách hàng thân thiết</p>
                                <p class="item__sub">Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.</p>
                            </div>
                        </li>
                        <li class="features-content__item hvr-sweep-to-right">
                            <div class="mona-down-percent"></div>
                            <div class="info">
                                <p class="item__title">Khách hàng thân thiết</p>
                                <p class="item__sub">Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.</p>
                            </div>
                        </li>
                    </ul>

                </div>

                <div class="information wow fadeInRight">
                    <h4 class="sec__title">Thông tin công ty</h4>
                    <div class="information-content">
                        <p class="item__sub">
                            Developed by the Intel Corporation, HDCP stands for high-bandwidth digital content protection. As the descriptive name implies,
            HDCP is all about protecting the integrity... of various audio and video content as it travels over a multiplicity of different
            types of
                        </p>
                        <div class="information-content__item">
                            <h4 class="sec__title-2">10</h4>
                            <p class="sec__sub-2">Năm kinh nghiệm</p>
                        </div>
                        <div class="information-content__item">
                            <h4 class="sec__title-2">12,573</h4>
                            <p class="sec__sub-2">Khách hàng</p>
                        </div>
                        <div class="information-content__item">
                            <h4 class="sec__title-2">4,590</h4>
                            <p class="sec__sub-2">Đơn hàng</p>
                        </div>
                        <div class="information-content__item">
                            <h4 class="sec__title-2">3,480</h4>
                            <p class="sec__sub-2">Đơn hàng thành công</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="contact">
            <div class="contact-method">
                <div class="all">
                    <div class="contact-info">
                        <h4 class="sec__title">Liên hệ</h4>
                        <asp:Literal ID="ltrContactFooter" runat="server"></asp:Literal>



                        <div class="contact-form">
                            <asp:UpdatePanel ID="upd" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
                                    <div class="clear"></div>
                                    <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" placeholder="Họ và tên khách hàng"></asp:TextBox>
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Số điện thoại"></asp:TextBox>
                                    <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" placeholder="Lời nhắn" TextMode="MultiLine"></asp:TextBox>
                                    <asp:Button ID="btnSend" runat="server" CssClass="btn btn-1" Text="Gửi" OnClick="btnSend_Click"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<input type="text" class="form-control" name="" placeholder="Họ và tên khách hàng">
                            <input type="text" class="form-control" name="" placeholder="Số điện thoại">
                            <textarea class="form-control" placeholder="Lời nhắn" name="" cols="30" rows="10"></textarea>
                            <a href="javascript:;" class="btn btn-1">Gửi</a>--%>
                        </div>
                    </div>
                </div>
                <div class="google-map">
                    <div class="map" id="map-canvas"></div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
