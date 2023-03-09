<%@ Page Title="" Language="C#" MasterPageFile="~/VinhAnhMaster.Master" AutoEventWireup="true" CodeBehind="Default6.aspx.cs" Inherits="NHST.Default5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">

        <section class="banner">
            <div class="all">
                <section class="banner__main">

                    <div class="title">
                        <h1 class="fz-60">Ground, Air or Sea</h1>
                        <h4 class="fz-18">We deliver your package in on-time</h4>
                    </div>
                    <div class="form">
                        <div class="search-input">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Nhập link sản phẩm"></asp:TextBox>
                            <%--<input type="text" name="" id="" class="form-control" placeholder="Nhập link sản phẩm">--%>
                            <%--<span class="select">
                                <select name="carlist" form="carform" class="form-control">
                                    <option value="taobao">Taobao</option>
                                    <option value="tmall">Tmall</option>
                                </select>
                                <span class="icon"><i class="fas fa-sort"></i></span>
                            </span>--%>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-1 form-control" Text="Đặt hàng nhanh" OnClick="btnSubmit_Click" />
                            <%--<a href="" class="btn btn-1" class="form-control">Đặt hàng nhanh</a>--%>
                        </div>
                    </div>

                </section>

                <div class="banner__bot">

                    <div class="banner__bot-child">
                        <div class="img">
                            <img src="/App_Themes/vinhanh/images/banner-bot__icon-1.png" alt="icon banner">
                        </div>
                        <div class="content">
                            <p>Hotline:</p>
                            <h1 class="fz-36">
                                <asp:Label ID="lblHotline" runat="server"></asp:Label>
                            </h1>
                        </div>
                    </div>
                    <div class="banner__bot-child">
                        <div class="img">
                            <img src="/App_Themes/vinhanh/images/banner-bot__icon-2.png" alt="icon banner">
                        </div>
                        <div class="content">
                            <p>Hỗ trợ khách hàng</p>
                            <h1 class="fz-36">
                                <asp:Label ID="lblHotlineSupport" runat="server"></asp:Label>
                            </h1>
                        </div>
                    </div>
                    <div class="banner__bot-child">
                        <div class="img">
                            <img src="/App_Themes/vinhanh/images/banner-bot__icon-2.png" alt="icon banner">
                        </div>
                        <div class="content">
                            <p>Phản hồi</p>
                            <h1 class="fz-36">
                                <asp:Label ID="lblHotlineFeedback" runat="server"></asp:Label>
                            </h1>
                        </div>
                    </div>


                </div>
            </div>
        </section>

        <section class="process">
            <div class="col-6">

                <section class="col__child">
                    <div class="img">
                        <img src="/App_Themes/vinhanh/images/process-icon-1.png" alt="process icon">
                    </div>
                    <div class="title">
                        <h4 class="fz-18">Tạo đơn hàng</h4>
                    </div>
                </section>
                <section class="col__child">
                    <div class="img">
                        <img src="/App_Themes/vinhanh/images/process-icon-2.png" alt="process icon">
                    </div>
                    <div class="title">
                        <h4 class="fz-18">Gửi đơn hàng</h4>
                    </div>
                </section>
                <section class="col__child">
                    <div class="img">
                        <img src="/App_Themes/vinhanh/images/process-icon-3.png" alt="process icon">
                    </div>
                    <div class="title">
                        <h4 class="fz-18">Vinh Anh báo giá</h4>
                    </div>
                </section>
                <section class="col__child">
                    <div class="img">
                        <img src="/App_Themes/vinhanh/images/process-icon-4.png" alt="process icon">
                    </div>
                    <div class="title">
                        <h4 class="fz-18">Chốt đơn hàng</h4>
                    </div>
                </section>
                <section class="col__child">
                    <div class="img">
                        <img src="/App_Themes/vinhanh/images/process-icon-5.png" alt="process icon">
                    </div>
                    <div class="title">
                        <h4 class="fz-18">Thanh toán</h4>
                    </div>
                </section>
                <section class="col__child">
                    <div class="img">
                        <img src="/App_Themes/vinhanh/images/process-icon-6.png" alt="process icon">
                    </div>
                    <div class="title">
                        <h4 class="fz-18">Nhận hàng tại nhà</h4>
                    </div>
                </section>

            </div>
        </section>
        <asp:Literal ID="ltrOrderProduct" runat="server" Visible="true"></asp:Literal>
        <%--<section class="taobao order">
            <div class="all">
                <section class="order-hd">
                    <div class="hd__title">
                        <h2 class="fz-30">Đặt hàng từ <a href="">
                            <img src="/App_Themes/vinhanh/images/taobao-hd-title.png" alt="taobao">
                        </a>
                        </h2>
                    </div>
                </section>
                <div class="order-col-4">

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/taobao-img-1.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Thời trang nữ</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Áo len</a></li>
                            <li class="item"><a href="">Váy liền</a></li>
                            <li class="item"><a href="">Quần</a></li>
                            <li class="item"><a href="">Áo lót</a></li>
                            <li class="item"><a href="">Áo phông</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/taobao-img-2.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Thời trang nam</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Áo</a></li>
                            <li class="item"><a href="">Quần ống con</a></li>
                            <li class="item"><a href="">Áo phông</a></li>
                            <li class="item"><a href="">Áo sơ mi</a></li>
                            <li class="item"><a href="">Quần lót</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/taobao-img-3.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Mẹ và bé</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Đồ dùng phòng ngủ của bé</a></li>
                            <li class="item"><a href="">Xe đẩy</a></li>
                            <li class="item"><a href="">Đồ chơi</a></li>
                            <li class="item"><a href="">Giày dép trẻ em</a></li>
                            <li class="item"><a href="">Quần áo trẻ em</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/taobao-img-4.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Văn phòng phẩm</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Thẻ nhớ</a></li>
                            <li class="item"><a href="">Thiết bị văn phòng</a></li>
                            <li class="item"><a href="">Cổng truyền mạng</a></li>
                            <li class="item"><a href="">Văn phòng phẩm</a></li>
                            <li class="item"><a href="">Giấy gói quà</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                </div>
            </div>
        </section>        
        <section class="tmall order">
            <div class="all">
                <section class="order-hd">
                    <div class="hd__title">
                        <h2 class="fz-30">Đặt hàng từ <a href="">
                            <img src="/App_Themes/vinhanh/images/tmall-hd-title.png" alt="taobao">
                        </a>
                        </h2>
                    </div>
                </section>
                <div class="order-col-4">

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/tmall-img-1.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Thời trang nữ</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Áo len</a></li>
                            <li class="item"><a href="">Váy liền</a></li>
                            <li class="item"><a href="">Quần</a></li>
                            <li class="item"><a href="">Áo lót</a></li>
                            <li class="item"><a href="">Áo phông</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/tmall-img-2.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Thời trang nam</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Áo</a></li>
                            <li class="item"><a href="">Quần ống con</a></li>
                            <li class="item"><a href="">Áo phông</a></li>
                            <li class="item"><a href="">Áo sơ mi</a></li>
                            <li class="item"><a href="">Quần lót</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/tmall-img-3.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Mẹ và bé</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Đồ dùng phòng ngủ của bé</a></li>
                            <li class="item"><a href="">Xe đẩy</a></li>
                            <li class="item"><a href="">Đồ chơi</a></li>
                            <li class="item"><a href="">Giày dép trẻ em</a></li>
                            <li class="item"><a href="">Quần áo trẻ em</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/tmall-img-4.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Văn phòng phẩm</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Thẻ nhớ</a></li>
                            <li class="item"><a href="">Thiết bị văn phòng</a></li>
                            <li class="item"><a href="">Cổng truyền mạng</a></li>
                            <li class="item"><a href="">Văn phòng phẩm</a></li>
                            <li class="item"><a href="">Giấy gói quà</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                </div>
            </div>
        </section>
        <section class="1688 order">
            <div class="all">
                <section class="order-hd">
                    <div class="hd__title">
                        <h2 class="fz-30">Đặt hàng từ <a href="">
                            <img src="/App_Themes/vinhanh/images/1688-hd-title.png" alt="taobao">
                        </a>
                        </h2>
                    </div>
                </section>
                <div class="order-col-4">

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/1688-img-1.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Thời trang nữ</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Áo len</a></li>
                            <li class="item"><a href="">Váy liền</a></li>
                            <li class="item"><a href="">Quần</a></li>
                            <li class="item"><a href="">Áo lót</a></li>
                            <li class="item"><a href="">Áo phông</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/1688-img-2.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Thời trang nam</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Áo</a></li>
                            <li class="item"><a href="">Quần ống con</a></li>
                            <li class="item"><a href="">Áo phông</a></li>
                            <li class="item"><a href="">Áo sơ mi</a></li>
                            <li class="item"><a href="">Quần lót</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/1688-img-3.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Mẹ và bé</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Đồ dùng phòng ngủ của bé</a></li>
                            <li class="item"><a href="">Xe đẩy</a></li>
                            <li class="item"><a href="">Đồ chơi</a></li>
                            <li class="item"><a href="">Giày dép trẻ em</a></li>
                            <li class="item"><a href="">Quần áo trẻ em</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                    <article class="col__child">
                        <div class="title">
                            <div class="img">
                                <a href="#">
                                    <img src="/App_Themes/vinhanh/images/1688-img-4.jpg" alt="category img"></a>
                            </div>
                            <h4 class="fz-18">Văn phòng phẩm</h4>
                        </div>
                        <ul class="list">
                            <li class="item"><a href="">Thẻ nhớ</a></li>
                            <li class="item"><a href="">Thiết bị văn phòng</a></li>
                            <li class="item"><a href="">Cổng truyền mạng</a></li>
                            <li class="item"><a href="">Văn phòng phẩm</a></li>
                            <li class="item"><a href="">Giấy gói quà</a></li>
                            <li class="item"><a href="">Xem tất cả</a></li>
                        </ul>
                    </article>

                </div>
            </div>
        </section>--%>

        <section class="features">
            <div class="all">
                <div class="feat-col-3">

                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/vinhanh/images/features-icon-1.png" alt="feat icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Đền 100% tiền hàng</h4>
                            <p>Cám kết hoàn tiền nếu mất hàng</p>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/vinhanh/images/features-icon-2.png" alt="feat icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Không lo tắt biên</h4>
                            <p>Vận chuyển hàng thông suốt</p>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/vinhanh/images/features-icon-3.png" alt="feat icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Miễn phí dịch vụ</h4>
                            <p>Cho đợn hàng đầu tiên</p>
                        </article>
                    </div>

                </div>
            </div>
        </section>
    </main>
</asp:Content>
