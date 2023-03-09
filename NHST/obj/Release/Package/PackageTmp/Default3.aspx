<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterNotLogin.Master" AutoEventWireup="true" CodeBehind="Default3.aspx.cs" Inherits="NHST.Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="banner clearfix">
        <asp:Literal ID="ltrBanner" runat="server"></asp:Literal>
        <asp:Literal ID="ltrAddon" runat="server"></asp:Literal>
        <%--<img src="App_Themes/pdv/assets/images/banner.png" alt="" />
        
        <div class="banner-add-on">
            <label>ADD ON</label>
            <a href="javascript:;" class="addon-bg-chrome"></a>
            <a href="javascript:;" class="addon-bg-cococ"></a>
        </div>--%>
        <%--<div class="container">            
            <div class="banner-left">
                <div class="banner-img">
                    <div class="banner-width">
                        <img src="/App_Themes/pdv/assets/images/banner.jpg" alt="#">
                    </div>
                </div>
            </div>
            <div class="banner-right">
                <div class="banner-info">
                    <h1 class="banner-title">nhập hàng tận gốc<br>
                        <span>tại alivietnam</span></h1>
                    <p class="banner-text">dịch vụ nhập hàng trung quốc trực tuyến</p>
                    <p class="banner-contact"><a href="https://alivietnam.com/">www.alivietnam.com </a><span>|</span> hostline: <a href="tel:+04671888">04.6671.888</a></p>
                </div>
            </div>
        </div>--%>
    </section>
    <section class="intro">
        <div class="container">
            <h2 class="main-title">Giới thiệu</h2>
            <img src="/App_Themes/pdv/assets/images/bordet-bottom.png" alt="#">
            <p class="main-text">
                <asp:Literal ID="ltrAbout" runat="server"></asp:Literal>
            </p>
            <%--<p class="main-text"><span>Alivietnam</span> kết nối khách hàng – những người làm kinh doanh – với hàng chục triệu nhà cung cấp trực tuyến Trung Quốc. Với kinh nghiệm nhiều năm, đội ngũ tư vấn và tìm kiếm nguồn hàng của Alivietnam luôn sẵn sàng kết nối Quý khách hàng với những địa chỉ sản xuất, bán buôn, bán lẻ bằng những công cụ và cơ sở dữ liệu phong phú.</p>--%>
            <ul class="list-intro clearfix">
                <asp:Literal ID="ltrWebChina" runat="server"></asp:Literal>
                <%--<li>
                    <a href="https://world.taobao.com/" target="_blank">
                        <img src="/App_Themes/pdv/assets/images/intro1.png" alt=""></a>
                    <h3><a href="https://world.taobao.com/" target="_blank">taobao.com</a></h3>
                    <p>Website bán lẻ lớn nhất Trung Quốc</p>
                </li>
                <li>
                    <a href="https://www.tmall.com/" target="_blank">
                        <img src="/App_Themes/pdv/assets/images/intro2.png" alt=""></a>
                    <h3><a href="https://www.tmall.com/" target="_blank">TMALL.COM</a></h3>
                    <p>Website bán hàng hiệu uy tín nhất Trung Quốc</p>
                </li>
                <li>
                    <a href="https://www.1688.com/" target="_blank">
                        <img src="/App_Themes/pdv/assets/images/intro3.png" alt=""></a>
                    <h3><a href="https://www.1688.com/" target="_blank">1688.COM</a></h3>
                    <p>Website bán buôn số 1 Trung Quốc</p>
                </li>--%>
            </ul>
        </div>
    </section>
    <section class="link-product clearfix">
        <div class="container">
            <div class="link-search">
                <asp:TextBox ID="txtLink" runat="server" placeholder="Nhập link sản phẩm"></asp:TextBox>
                <div class="submit">
                    <a href="javascript:;" onclick="createOrder()">Tạo đơn hàng nhanh</a>
                    <asp:Button ID="btnCart" runat="server" CssClass="submit-btn" Text="Tạo đơn hàng nhanh" OnClick="btnCart_Click"
                        OnClientClick="document.forms[0].target = '_blank';" Style="display: none;" />
                </div>
            </div>
            <p class="link-product-right">Sử dụng công cụ để đặt hàng thuận tiện hơn</p>
        </div>
    </section>
    <section class="search-product clearfix">
        <div class="container">
            <div class="search-form">
                <div class="search-form-detail">
                    <h3 class="title-home-product">tìm kiếm sản phẩm</h3>
                    <ul>
                        <li><a class="pages-search search-active" data-value="taobao" href="javascript:;">taobao.com</a></li>
                        <li><a class="pages-search" data-value="tmall" href="javascript:;">tmail.com</a></li>
                        <li><a class="pages-search" data-value="1688" href="javascript:;">1688.com</a></li>
                    </ul>
                    <div class="form-action">
                        <asp:TextBox ID="txtTextSearch" runat="server" placeholder="Sản phẩm tìm kiếm"></asp:TextBox>
                        <div class="submit1">
                            <a href="javascript:;" onclick="searchProduct()">tìm kiếm</a>
                            <asp:Button ID="btnsearchpro" runat="server" CssClass="submit-btn" Text="tìm kiếm" OnClick="btnsearchpro_Click"
                                OnClientClick="document.forms[0].target = '_blank';" Style="display: none;" />
                        </div>
                    </div>
                    <div class="new-product">
                        <a class="new-active" href="/dat-hang-nhanh">Tạo đơn hàng</a>
                    </div>
                </div>
            </div>
            <div class="setting clearfix">
                <div class="setting-detail">
                    <h3 class="title-home-product">Cài đặt công cụ trên trình duyệt</h3>
                    <ul class="list-setting">
                        <asp:Literal ID="ltrExtension" runat="server"></asp:Literal>
                        <%--<li style="width: 50%">
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-ali-vi%E1%BB%87t/ogebhicbnpbhpkkkklahoopokihcjloa?authuser=2"
                                target="_blank">
                                <img src="/App_Themes/pdv/assets/images/setting1.png" alt="#"></a>
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-ali-vi%E1%BB%87t/ogebhicbnpbhpkkkklahoopokihcjloa?authuser=2"
                                target="_blank" style="float: left; clear: both; width: 100%; text-align: center">Chrome</a>
                        </li>
                        <li style="width: 50%">
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-ali-vi%E1%BB%87t/ogebhicbnpbhpkkkklahoopokihcjloa?authuser=2"
                                target="_blank">
                                <img src="/App_Themes/pdv/assets/images/setting2.png" alt="#"></a>
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-ali-vi%E1%BB%87t/ogebhicbnpbhpkkkklahoopokihcjloa?authuser=2"
                                target="_blank" style="float: left; clear: both; width: 100%; text-align: center">cococ</a>
                        </li>--%>
                        <%-- <li>
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-ali-vi%E1%BB%87t/ogebhicbnpbhpkkkklahoopokihcjloa?authuser=2"
                                target="_blank">
                                <img src="/App_Themes/pdv/assets/images/setting3.png" alt="#"></a>
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-ali-vi%E1%BB%87t/ogebhicbnpbhpkkkklahoopokihcjloa?authuser=2"
                                target="_blank">firefox</a>
                        </li>--%>
                    </ul>
                </div>
            </div>
        </div>
    </section>
    <section class="process clearfix">
        <div class="container">
            <h2 class="main-title">quy trình đặt hàng</h2>
            <img src="/App_Themes/pdv/assets/images/border-bt2.png" alt="#">
            <ul class="list-process">
                <asp:Literal ID="ltrQuytrinh" runat="server"></asp:Literal>
                <%--<li>
                    <a href="/dang-ky">
                        <img src="/App_Themes/pdv/assets/images/set1.png" alt="#"></a>
                    <p><a href="/dang-ky">đăng ký tài khoản</a></p>
                </li>
                <li>
                    <a href="javascript:;" onclick="alert('Hiện tại công cụ đang được cập nhật vui lòng thử lại sau')">
                        <img src="/App_Themes/pdv/assets/images/set2.png" alt="#"></a>
                    <p><a href="javascript:;" onclick="alert('Hiện tại công cụ đang được cập nhật vui lòng thử lại sau')">cài đặt công cụ mua hàng</a></p>
                </li>
                <li>
                    <a href="/gio-hang">
                        <img src="/App_Themes/pdv/assets/images/set3.png" alt="#"></a>
                    <p><a href="/gio-hang">chọn hàng vào giở</a></p>
                </li>
                <li>
                    <a href="/3/28/huong-dan/mot-so-thac-mac-khi-moi-dat-hang">
                        <img src="/App_Themes/pdv/assets/images/set4.png" alt="#"></a>
                    <p><a href="/3/28/huong-dan/mot-so-thac-mac-khi-moi-dat-hang">dướng dẫn đặt hàng</a></p>
                </li>
                <li>
                    <a href="/danh-sach-don-hang">
                        <img src="/App_Themes/pdv/assets/images/set5.png" alt="#"></a>
                    <p><a href="/danh-sach-don-hang">đặt cọc tiền hàng</a></p>
                </li>
                <li>
                    <a href="/danh-sach-don-hang">
                        <img src="/App_Themes/pdv/assets/images/set6.png" alt="#"></a>
                    <p><a href="/danh-sach-don-hang">nhận hàng thanh toán</a></p>
                </li>--%>
            </ul>
        </div>
    </section>
    <section class="commit clearfix">
        <div class="container clearfix">
            <h2 class="main-title">chúng tôi cam kết</h2>
            <img src="/App_Themes/pdv/assets/images/border-bt3.png" alt="#">
            <div class="commit-detail clearfix">
                <div class="commit-left">
                    <img src="/App_Themes/pdv/assets/images/bg-camket.jpg" alt="">
                </div>
                <div class="commit-right">
                    <img class="img-hidden" src="/App_Themes/pdv/assets/images/bg-camket.jpg" alt="#">
                    <asp:Literal ID="ltrCamket" runat="server"></asp:Literal>
                    <%-- <div class="commit-list">
                        <h3><a href="javascript:;">không có thời gian trễ khi đặt hàng</a></h3>
                        <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                    </div>
                    <div class="commit-list">
                        <h3><a href="javascript:;">cam kết mua hàng trong 24h</a></h3>
                        <p>Miễn phí mua hàng nếu mua quá thời gian cam kết</p>
                    </div>
                    <div class="commit-list">
                        <h3><a href="javascript:;">Tích kiệm thời gian quản lý</a></h3>
                        <p>Hệ thống quản lý thông minh, giúp quý khách chủ động the dõi thông tin đơn hàng mọi lúc mọi nơi</p>
                    </div>
                    <div class="commit-list">
                        <h3><a href="javascript:;">hỗ trợ trực tuyến 24/7</a></h3>
                        <p>luôn sẵn sàng giải quyết mọi thắc mắc kể cả ngoài khung giờ làm việc</p>
                    </div>--%>
                </div>
            </div>
        </div>
    </section>
    <section class="services clearfix">
        <div class="container clearfix">
            <h2 class="main-title">dịch vụ của chúng tôi</h2>
            <img src="/App_Themes/pdv/assets/images/border-bt4.png" alt="#">
            <div class="services-list clearfix">
                <asp:Literal ID="ltrService" runat="server" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </section>
    <section class="benefit clearfix">
        <div class="container">
            <h2 class="main-title">quyền lợi khách hàng</h2>
            <img src="/App_Themes/pdv/assets/images/border-bt2.png" alt="#">
            <div class="benefit-box clearfix">
                <img class="benefit-img-hiden" src="/App_Themes/pdv/assets/images/bg-benefit.png" alt="#">
                <div class="benefit-left clearfix">
                    <asp:Literal ID="ltrLeftBenefit" runat="server"></asp:Literal>
                    <%-- <div class="benefit-list">
                        <div class="benefit-auto">
                            <div class="benefit-img">
                                <img src="/App_Themes/pdv/assets/images/check.png" alt="#">
                            </div>
                            <div class="benefit-info">
                                <h3>khách hàng thân thiết</h3>
                                <p>Tích điểm thành viên từ mỗi đơn hàng và nhận nhiều ưu đãi cho đơn hàng sau</p>
                            </div>
                        </div>
                    </div>
                    <div class="benefit-list">
                        <div class="benefit-auto">
                            <div class="benefit-img">
                                <img src="/App_Themes/pdv/assets/images/check.png" alt="#">
                            </div>
                            <div class="benefit-info">
                                <h3>ưu đãi theo sản lượng tháng</h3>
                                <p>Dịch vụ của chúng tôi mang đến nhiều gói dịch vụ khác nhau, đó là những giải pháp vận chuyển hàng hóa hoàn chỉnh và toàn diện nhất cho nhu cầu của khách hàng.</p>
                            </div>
                        </div>
                    </div>
                    <div class="benefit-list">
                        <div class="benefit-auto">
                            <div class="benefit-img">
                                <img src="/App_Themes/pdv/assets/images/check.png" alt="#">
                            </div>
                            <div class="benefit-info">
                                <h3>MARKETING & BÁN HÀNG</h3>
                                <p>Đội ngũ nhân viên năng động, tràn đầy nhiệt huyết sẽ mang đến cho bạn những dịch vụ hoàn thiện nhất bởi thái độ và trách nhiệm chuyên nghiệp nhất.</p>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="benefit-right">
                    <asp:Literal ID="ltrRightBenefit" runat="server"></asp:Literal>
                    <%--<div class="benefit-list">
                        <div class="benefit-auto">
                            <div class="benefit-img">
                                <img src="/App_Themes/pdv/assets/images/check.png" alt="#">
                            </div>
                            <div class="benefit-info">
                                <h3>hoàn 100% tiền khi mất hàng</h3>
                                <p>Chúng tôi hoàn tiền 100% đối với đơn hàng bị mất của khách hàng.</p>
                            </div>
                        </div>
                    </div>
                    <div class="benefit-list">
                        <div class="benefit-auto">
                            <div class="benefit-img">
                                <img src="/App_Themes/pdv/assets/images/check.png" alt="#">
                            </div>
                            <div class="benefit-info">
                                <h3>không lo tác biên</h3>
                                <p>Qui trình giao nhận của chúng tôi không thể hoàn thiện hơn được nữa bởi sự chính xác của qui trình đóng gói, bảo quản và sự cẩn thận, chuyên nghiệp của đội ngũ nhân viên.</p>
                            </div>
                        </div>
                    </div>
                    <div class="benefit-list">
                        <div class="benefit-auto">
                            <div class="benefit-img">
                                <img src="/App_Themes/pdv/assets/images/check.png" alt="#">
                            </div>
                            <div class="benefit-info">
                                <h3>tối ưu chi phí</h3>
                                <p>10%-20% Đó là chi phí tiết kiệm được khi bạn sử dụng dịch vụ của chúng tôi</p>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </section>
    <section class="slider">
        <div class="container">
            <h3>Danh sách <span>website uy tín trung quốc</span></h3>
            <img src="/App_Themes/pdv/assets/images/Layer 1.png" alt="#">
            <div id="slider">
                <asp:Literal ID="ltrPartner" runat="server"></asp:Literal>
                <%--<div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider1.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider2.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider3.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider4.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider5.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider1.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider2.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider3.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider4.png" alt="">
                </div>
                <div class="item">
                    <img src="/App_Themes/pdv/assets/images/slider5.png" alt="">
                </div>--%>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdfType" runat="server" />
    <asp:HiddenField ID="hdfheha" runat="server" />
    <script type="text/javascript">
        function searchProduct() {
            var valuea = 'taobao';
            $(".pages-search ").each(function () {
                if ($(this).hasClass("search-active")) {
                    valuea = $(this).attr("data-value");
                }
            });
            $("#<%= hdfType.ClientID%>").val(valuea);
            $("#<%=btnsearchpro.ClientID%>").click();
        }
        function createOrder() {
            var c = $("#<%=hdfheha.ClientID%>").val();
            if (c == 0) {
                alert('Vui lòng đăng nhập trước khi đặt hàng');
            }
            else {
                $("#<%=btnCart.ClientID%>").click();
            }
        }
    </script>
</asp:Content>
