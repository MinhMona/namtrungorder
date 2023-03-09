<%@ Page Title="" Language="C#" MasterPageFile="~/KT1688Master.Master" AutoEventWireup="true" CodeBehind="Default10.aspx.cs" Inherits="NHST.Default9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home" style="margin-top:0">
        <div class="banner">
            <%--<div class="button-wrap">
                <h2 class="hd">Cài đặt công cụ</h2>
                <div class="button">
                    <a href="javascript:;" class="mn-btn gg-btn">
                        <span class="gg-triangle"></span>
                        <i class="fab fa-chrome"></i>Chrome</a>
                    <a href="javascript:;" class="mn-btn cc-btn">
                        <span class="cc-triangle"></span>
                        <i class="fab fa-chrome"></i>Cốc Cốc</a>
                </div>
            </div>--%>
        </div>

        <div class="search-prd">
            <div class="all">
                <div class="form">
                    <div class="f-select">
                        <asp:DropDownList ID="ddlWebsearch" runat="server" CssClass="f-control">
                            <asp:ListItem Value="taobao" Text="Taobao"></asp:ListItem>
                            <asp:ListItem Value="1688" Text="1688"></asp:ListItem>
                            <asp:ListItem Value="tmall" Text="Tmall"></asp:ListItem>
                        </asp:DropDownList>
                        <span class="icon"><i class="fas fa-chevron-down"></i></span>
                    </div>
                    <div class="f-input">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="f-control" placeholder="Tìm kiếm sản phẩm"></asp:TextBox>
                        <asp:Button ID="btnsearchpro" runat="server" CssClass="mn-btn" Text="Tìm kiếm" OnClick="btnsearchpro_Click"
                            OnClientClick="document.forms[0].target = '_blank';" Style="display: none" />
                        <a href="javascript:;" onclick="searchproduct()" class="submit"><i class="fas fa-search"></i></a>
                    </div>
                </div>
            </div>
        </div>

        <div class="features">
            <div class="all">
                <ul class="list-feat">
                    <li class="feat__item">
                        <div class="feat-block">
                            <div class="img">
                                <a href="javascript:;">
                                    <img src="/App_Themes/kt1688/images/features-img-1.jpg" alt=""></a>
                            </div>
                            <div class="ct">
                                <h4 class="hd"><a href="javascript:;">Đặt hàng, Ship hàng quốc tế</a></h4>
                                <%--<a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>--%>
                            </div>
                        </div>
                    </li>
                    <li class="feat__item">
                        <div class="feat-block">
                            <div class="img">
                                <a href="javascript:;">
                                    <img src="/App_Themes/kt1688/images/features-img-2.jpg" alt=""></a>
                            </div>
                            <div class="ct">
                                <h4 class="hd"><a href="javascript:;">Chuyển hàng 2 chiều Trung - Việt</a></h4>
                                <%--<a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>--%>
                            </div>
                        </div>
                    </li>
                    <li class="feat__item">
                        <div class="feat-block">
                            <div class="img">
                                <a href="javascript:;">
                                    <img src="/App_Themes/kt1688/images/features-img-3.jpg" alt=""></a>
                            </div>
                            <div class="ct">
                                <h4 class="hd"><a href="javascript:;">Chuyển tiền, nạp tiền Alipay, Wechat</a></h4>
                                <%--<a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>--%>
                            </div>
                        </div>
                    </li>
                    <li class="feat__item">
                        <div class="feat-block">
                            <div class="img">
                                <a href="javascript:;">
                                    <img src="/App_Themes/kt1688/images/features-img-4.jpg" alt=""></a>
                            </div>
                            <div class="ct">
                                <h4 class="hd"><a href="javascript:;">Hổ trợ và tư vấn chuyển nghiệp</a></h4>
                                <%--<a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>--%>
                            </div>
                        </div>
                    </li>
                    <li class="feat__item">
                        <div class="feat-block">
                            <div class="img">
                                <a href="javascript:;">
                                    <img src="/App_Themes/kt1688/images/features-img-5.jpg" alt=""></a>
                            </div>
                            <div class="ct">
                                <h4 class="hd"><a href="javascript:;">Dễ dàng quản lý đơn hàng</a></h4>
                                <%--<a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>--%>
                            </div>
                        </div>
                    </li>
                    <li class="feat__item">
                        <div class="feat-block">
                            <div class="img">
                                <a href="javascript:;">
                                    <img src="/App_Themes/kt1688/images/features-img-6.jpg" alt=""></a>
                            </div>
                            <div class="ct">
                                <h4 class="hd"><a href="javascript:;">Giao hàng tận nơi</a></h4>
                                <%--<a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>--%>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <div class="steps-order">
            <div class="all">
                <div class="title">
                    <h3 class="hd">Quy trình đặt hàng</h3>
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
                                    <a href="/dang-ky" class="mn-btn btn-3">Đăng ký</a>
                                </div>
                            </div>
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/step-img-1.jpg" alt="">
                            </div>
                        </div>
                    </div>

                    <div id="step-caidat">
                        <div class="stepct-block">
                            <div class="detail">
                                <h3 class="hd">Cài đặt công cụ mua hàng</h3>
                                <p>
                                    Click vào cài đặt công cụ đặt hàng của KT1688. Công cụ hỗ trợ đặt hàng các website taobao, tmall, 1688.
                                </p>
                            </div>
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/step-img-1.jpg" alt="">
                            </div>
                        </div>
                    </div>
                    <div id="step-chonhang">
                        <div class="stepct-block">
                            <div class="detail">
                                <h3 class="hd">Chọn hàng và thêm hàng vào giỏ</h3>
                                <p>
                                    Truy cập vào các trang mua sắm Taobao.com, Tmall.com, 1688.com … chọn hàng và thêm hàng 
                                    vào giỏ.
                                </p>
                            </div>
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/step-img-1.jpg" alt="">
                            </div>
                        </div>
                    </div>
                    <div id="step-guidon">
                        <div class="stepct-block">
                            <div class="detail">
                                <h3 class="hd">Gửi đơn hàng</h3>
                                <p>
                                    Quay lại website KT1688 và kiểm tra giỏ hàng Click vào “Gửi đơn hàng” để tạo đơn hàng, 
                                    chờ xác nhận đặt hàng thành công.
                                </p>
                            </div>
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/step-img-1.jpg" alt="">
                            </div>
                        </div>
                    </div>
                    <div id="step-datcoc">
                        <div class="stepct-block">
                            <div class="detail">
                                <h3 class="hd">Đặt cọc tiền hàng</h3>
                                <p>
                                    Kiểm tra đơn hàng và đặt cọc tiền hàng qua hình thức chuyển khoản hoặc trực tiếp tại các 
                                    văn phòng giao dịch gần nhất của KT1688.
                                </p>
                            </div>
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/step-img-1.jpg" alt="">
                            </div>
                        </div>
                    </div>
                    <div id="step-nhanhang">
                        <div class="stepct-block">
                            <div class="detail">
                                <h3 class="hd">Nhận hàng và thanh toán</h3>
                                <p>
                                    Quý khách nhận được thông báo hàng về Việt Nam. Quý khách thanh toán số tiền còn thiếu 
                                    qua hình thức chuyển khoản hoặc trực tiếp. Sau khi thanh toán quý khách hàng có thể yêu 
                                    cầu ship hàng hoặc trực tiếp nhận hàng tại kho của KT1688.
                                </p>
                            </div>
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/step-img-1.jpg" alt="">
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

        <div class="benefits">
            <div class="all">
                <div class="title">
                    <h2 class="hd">Quyền lợi khách hàng</h2>
                </div>
                <ul class="list-benefits">
                    <li class="benefit__item">
                        <div class="benefit-block">
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/benefit-img-1.png" alt="">
                            </div>
                            <div class="content">
                                <p class="hd"><a href="javascript:;">Khách hàng thân thiết</a></p>
                                <p>
                                    Cooking in the heart of Cajun country is an art form. There really is very little science to this particular form of cooking
                that includes a lot more than mere lagniappe from the pantry or the spice cabinet.
                                </p>
                                <a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>
                            </div>
                        </div>
                    </li>
                    <li class="benefit__item">
                        <div class="benefit-block">
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/benefit-img-2.png" alt="">
                            </div>
                            <div class="content">
                                <p class="hd"><a href="javascript:;">Marketing &amp; Bán hàng</a></p>
                                <p>
                                    Cooking in the heart of Cajun country is an art form. There really is very little science to this particular form of cooking
                that includes a lot more than mere lagniappe from the pantry or the spice cabinet.
                                </p>
                                <a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>
                            </div>
                        </div>
                    </li>
                    <li class="benefit__item">
                        <div class="benefit-block">
                            <div class="img">
                                <img src="/App_Themes/kt1688/images/benefit-img-3.png" alt="">
                            </div>
                            <div class="content">
                                <p class="hd"><a href="javascript:;">Ưu đãi theo sản lượng tháng</a></p>
                                <p>
                                    Cooking in the heart of Cajun country is an art form. There really is very little science to this particular form of cooking
                that includes a lot more than mere lagniappe from the pantry or the spice cabinet.
                                </p>
                                <a href="javascript:;" class="mn-btn btn-right">Xem thêm</a>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <div class="contact">

            <div class="google-map">
                <div class="map" id="map-canvas"></div>
            </div>
            <div class="ct-box">
                <div class="ctbox-wrap">
                    <div class="detail-block">
                        <p class="main">
                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal></p>
                    </div>
                    <div class="detail-block">
                        <p class="sub">Hotline:</p>
                        <p class="main color4">
                            <asp:Literal ID="ltrhotline" runat="server"></asp:Literal>
                            <%--<a href="tel:+">0126.922.1062</a>--%>
                        </p>
                    </div>
                    <div class="detail-block">
                        <p class="sub">Giờ hoạt động</p>
                        <p class="main">
                            <asp:Literal ID="ltrTimework" runat="server"></asp:Literal></p>
                    </div>
                    <div class="detail-block">
                        <p class="sub">Email:</p>
                        <p class="main color4">
                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>                            
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <script type="text/javascript">
        function searchproduct()
        {
            var text = $("#<%=txtSearch.ClientID%>").val();
            if(isEmpty(text))
            {
                alert('Vui lòng nhập từ khóa tìm kiếm.');
            }
            else
            {
                $("#<%=btnsearchpro.ClientID%>").click();
            }
        }
    </script>
</asp:Content>
