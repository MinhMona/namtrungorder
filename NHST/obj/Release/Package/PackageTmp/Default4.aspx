<%@ Page Title="" Language="C#" MasterPageFile="~/dqgMaster.Master" AutoEventWireup="true" CodeBehind="Default4.aspx.cs" Inherits="NHST.Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div id="primary" class="index">
            <section id="home-banner" class="use-bg" data-bg="/App_Themes/vcdqg/images/banner.jpg">
                <div class="container inner">
                    <aside class="cont text-center white">
                        <h1 class="main-tit">
                            <span>KẾT NỐI MUA HÀNG TẬN GỐC VỚI ĐỐI TÁC TRUNG QUỐC</span>
                            <br />
                            <span>TAOBAO</span>
                            <span class="m-color">TMALL</span>
                            <span>1688</span>
                        </h1>
                        <div class="desc b">
                            <p>Vận chuyển hàng về Việt Nam chỉ 2 - 4 ngày</p>
                            <p>Vận chuyển đa quốc gia cam kết một dịch vụ vượt trội và không ngừng tối ưu liên tục</p>
                        </div>
                        <div class="actions">
                            <a class="btn main-btn hover" href="/gio-hang">TẠO ĐƠN HÀNG</a>
                            <a class="btn white-btn" href="/tao-ky-gui-hang">KÝ GỬI HÀNG</a>
                        </div>
                    </aside>
                </div>
            </section>
            <section id="firm-services" class="sec">
                <div class="container text-center">
                    <h3 class="sec-tit text-center">
                        <span class="sub">DỊCH VỤ CỦA</span> VITI EXPRESS
					        </h3>
                    <div class="sec-desc">
                        Chúng tôi cung cấp cho bạn các giải pháp giao thương, kết nối nguồn hàng với các đối tác Trung Quốc
                    </div>
                    <ul class="list use-cols">
                        <li class="it col">
                            <div class="inner">
                                <div class="ico">
                                    <span class="hover-img">
                                        <img src="/App_Themes/vcdqg/images/ico-ser1.png" alt="" /></span>
                                </div>
                                <h4 class="tit">Order hàng</h4>
                                <div class="desc">Chúng tôi nhận nhập hàng từ các website của Trung Quốc như Taobao, Tmall, 1688, Alibaba</div>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <div class="ico">
                                    <span class="hover-img">
                                        <img src="/App_Themes/vcdqg/images/ico-ser2.png" alt="" /></span>
                                </div>
                                <h4 class="tit">Ký gửi hàng hóa</h4>
                                <div class="desc">Bạn có hàng bên đầu Trung Quốc và cần vận chuyển về Việt Nam, hãy tạo yêu cầu ký gửi</div>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <div class="ico">
                                    <span class="hover-img">
                                        <img src="/App_Themes/vcdqg/images/ico-ser3.png" alt="" /></span>
                                </div>
                                <h4 class="tit">Trung gian kết nối</h4>
                                <div class="desc">Tư vấn, hỗ trợ bạn giao thương với các đối tác Trung Quốc, chuyển đổi tệ</div>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <div class="ico">
                                    <span class="hover-img">
                                        <img src="/App_Themes/vcdqg/images/ico-ser4.png" alt="" /></span>
                                </div>
                                <h4 class="tit">Tìm nguồn hàng</h4>
                                <div class="desc">Tư vấn, hỗ trợ bạn tìm kiếm nguồn hàng từ hàng vạn xưởng sản xuất bên Trung Quốc</div>
                            </div>
                        </li>
                    </ul>
                </div>
            </section>
            <section id="search-product" class="sec">
                <div class="container text-center">
                    <%-- <h3 class="sec-tit text-center">
                        <span class="sub">TÌM KIẾM SẢN PHẨM</span>
					        </h3>--%>
                    <div class="sec-desc-1">
                        <div class="search-form">
                            <div class="search-form-detail">
                                <h3 class="sec-tit text-center"><span class="sub">TÌM KIẾM </span>SẢN PHẨM</h3>
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
                                <%--<div class="new-product">
                                    <a class="new-active" href="/dat-hang-nhanh">Tạo đơn hàng</a>
                                </div>--%>
                            </div>
                        </div>
                        <div class="setting clearfix">
                            <div class="setting-detail">
                                <h3 class="sec-tit text-center"><span class="sub">CÀI ĐẶT </span>CÔNG CỤ</h3>
                                <ul class="list-setting">
                                    <%--<asp:Literal ID="ltrExtension" runat="server"></asp:Literal>--%>
                                    <li>
                                        <a href="javascript:;" onclick="chrome.webstore.install()">
                                            <img src="/App_Themes/pdv/assets/images/setting1.png" alt="#"></a>
                                        <a href="javascript:;" onclick="chrome.webstore.install()" 
                                            style="float: left; clear: both; width: 100%; text-align: center">Chrome</a>

                                        <%--<a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-v%E1%BA%ADn-chuy/fcgjhboahpkilgglmfnikgpdmjjpbohg?authuser=2"
                                            target="_blank">
                                            <img src="/App_Themes/pdv/assets/images/setting1.png" alt="#"></a>
                                        <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-v%E1%BA%ADn-chuy/fcgjhboahpkilgglmfnikgpdmjjpbohg?authuser=2"
                                            target="_blank" style="float: left; clear: both; width: 100%; text-align: center">Chrome</a>--%>
                                    </li>
                                    <li>
                                        <a href="javascript:;" onclick="chrome.webstore.install()">
                                            <img src="/App_Themes/pdv/assets/images/setting2.png" alt="#"></a>
                                        <a href="javascript:;" onclick="chrome.webstore.install()"
                                            style="float: left; clear: both; width: 100%; text-align: center">cococ</a>
                                        <%--<a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-v%E1%BA%ADn-chuy/fcgjhboahpkilgglmfnikgpdmjjpbohg?authuser=2"
                                            target="_blank">
                                            <img src="/App_Themes/pdv/assets/images/setting2.png" alt="#"></a>
                                        <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-v%E1%BA%ADn-chuy/fcgjhboahpkilgglmfnikgpdmjjpbohg?authuser=2"
                                            target="_blank" style="float: left; clear: both; width: 100%; text-align: center">cococ</a>--%>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section id="home-experience" class="sec gray-bg">
                <div class="container clear">
                    <aside class="side main-cont">
                        <h3 class="sec-tit2">KINH NGHIỆM PHONG PHÚ</h3>

                        <div class="desc">
                            <p>Chúng tôi với nhiều năm kinh nghiệm trong lĩnh vực giao thương, trao đổi hàng hóa với các đối tác Trung Quốc sẽ là địa chỉ tin cậy cho bạn nhập hàng để kinh doanh</p>
                            <p>Hàng hóa vận chuyển 2 - 4 ngày từ Trung Quốc về Việt Nam, cam kết giao dịch trong 5h kể từ khi đặt cọc</p>
                            <p>Mức phí luôn cạnh tranh dịch vụ không ngừng cải tiến liên tục, chắc chắn chúng tôi sẽ không để bạn thất vọng</p>
                        </div>

                    </aside>

                    <aside class="side stats">
                        <h3 class="sec-tit2">KHÁCH HÀNG HÀI LÒNG</h3>
                        <ul class="list black">
                            <li class="it">
                                <div class="process-bar" data-percent="98%"><span></span></div>
                                <div class="cont">
                                    <span class="txt">Order hàng</span>
                                    <span class="num">+<span class="animate-num">98</span>%</span>
                                </div>
                            </li>
                            <li class="it">
                                <div class="process-bar" data-percent="85%"><span></span></div>
                                <div class="cont">
                                    <span class="txt">Ký gửi hàng hóa</span>
                                    <span class="num">+<span class="animate-num">85</span>%</span>
                                </div>
                            </li>
                            <li class="it">
                                <div class="process-bar" data-percent="99%"><span></span></div>
                                <div class="cont">
                                    <span class="txt">Tư vấn hỗ trợ</span>
                                    <span class="num">+<span class="animate-num">99</span>%</span>
                                </div>
                            </li>
                            <li class="it">
                                <div class="process-bar" data-percent="98%"><span></span></div>
                                <div class="cont">
                                    <span class="txt">Tìm nguồn hàng</span>
                                    <span class="num">+<span class="animate-num">98</span>%</span>
                                </div>
                            </li>
                        </ul>
                    </aside>
                </div>
            </section>
            <section id="sec-products" class="sec">
                <div class="container text-center">
                    <h3 class="sec-tit text-center">Sản phẩm <span class="sub">HOT</span>
                    </h3>
                    <div class="sec-desc">
                        Các sản phẩm được mua nhiều nhất
                    </div>
                    <ul class="its use-cols">
                        <asp:Literal ID="ltrProductHot" runat="server" EnableViewState="false"></asp:Literal>
                        <%--<li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product1.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product2.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product3.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product4.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product5.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product6.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>

                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product1.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product2.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product3.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product4.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product5.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>
                        <li class="it col">
                            <div class="inner">
                                <aside class="tmb">
                                    <img src="/App_Themes/vcdqg/images/product6.jpg" alt="" />
                                </aside>
                                <aside class="cont">
                                    <h4 class="web">Taobao.com</h4>
                                    <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                </aside>
                            </div>
                        </li>--%>
                    </ul>
                </div>
            </section>


            <section id="testimonials" class="sec use-bg" data-bg="/App_Themes/vcdqg/images/banner3.jpg">
                <div class="container">
                    <ul class="slider text-center white">
                        <li class="it">
                            <aside class="inner">
                                <div class="avatar">
                                    <img src="/App_Themes/vcdqg/images/cl2.png" alt="" />
                                </div>
                                <h4 class="name">Nguyễn Thu Thủy</h4>
                                <div class="cont">
                                    Tôi rất thích dịch vụ của VITIexpress, tư vấn chăm sóc khách hàng nhiệt tình, đơn hàng được đặt nhanh chóng, hàng về đều và ổn định
                                </div>
                            </aside>
                        </li>
                        <li class="it">
                            <aside class="inner">
                                <div class="avatar">
                                    <img src="/App_Themes/vcdqg/images/cl3.png" alt="" />
                                </div>
                                <h4 class="name">Trần Trung Đức</h4>
                                <div class="cont">
                                    Dịch vụ chất lượng, phí dịch vụ và cân nặng có thể nói là khá rẻ so với các bên khác, tôi sẽ còn hợp tác nhiều với VITIexpress
                                </div>
                            </aside>
                        </li>
                        <li class="it">
                            <aside class="inner">
                                <div class="avatar">
                                    <img src="/App_Themes/vcdqg/images/cl1.png" alt="" />
                                </div>
                                <h4 class="name">Võ Ngọc Thiện</h4>
                                <div class="cont">
                                    VITIexpress với hệ thống quản lý đơn hàng online, thuận tiện, giúp tôi theo dõi hàng hóa dễ dàng, còn phải nói hệ thống ví điện tử online không thể chê vào đâu được. Cám ơn VITIexpress, chúc công ty ngày càng phát triển mạnh mẽ
							
                                </div>
                            </aside>
                        </li>
                    </ul>
                </div>
            </section>

            <section id="featured-funcs" class="sec gray-bg">

                <div class="container">
                    <aside class="inner-cont text-center">
                        <h3 class="sec-tit">
                            <span class="sub">TÍNH NĂNG</span> NỔI BẬT
						</h3>
                        <div class="sec-desc">Các tính năng nổi bật của chúng tôi.</div>

                        <ul class="its use-cols">
                            <li class="it col">
                                <div class="inner">
                                    <div class="ico">
                                        <img src="/App_Themes/vcdqg/images/ico-feat1.png" alt="" />
                                    </div>
                                    <h4 class="tit">Đơn hàng online</h4>
                                    <div class="desc">
                                        Hệ thống quản lý đơn hàng online, giúp bạn theo dõi hành trình hàng hóa dễ dàng
								
                                    </div>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <div class="ico">
                                        <img src="/App_Themes/vcdqg/images/ico-feat2.png" alt="" />
                                    </div>
                                    <h4 class="tit">Tư vấn chuyên nghiêp</h4>
                                    <div class="desc">
                                        Đội chăm sóc khách hàng luôn sẵn sàng hỗ trợ, nhiệt tình giải đáp mọi thắc mắc
								
                                    </div>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <div class="ico">
                                        <img src="/App_Themes/vcdqg/images/ico-feat3.png" alt="" />
                                    </div>
                                    <h4 class="tit">Khách hàng thân thiết</h4>
                                    <div class="desc">
                                        Tích lũy điểm khi giao dịch, nhận các ưu đãi chiết khấu về sau
								
                                    </div>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <div class="ico">
                                        <img src="/App_Themes/vcdqg/images/ico-feat4.png" alt="" />
                                    </div>
                                    <h4 class="tit">Ví điện tử</h4>
                                    <div class="desc">
                                        Giao dịch thuận lợi, nhanh chóng thông qua hệ thống ví điện tử
								
                                    </div>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <div class="ico">
                                        <img src="/App_Themes/vcdqg/images/ico-feat5.png" alt="" />
                                    </div>
                                    <h4 class="tit">Chiết khấu cao</h4>
                                    <div class="desc">
                                        Mức chiết khấu theo các mốc, bạn đặt càng nhiều, hưởng lợi càng lớn
								
                                    </div>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <div class="ico">
                                        <img src="/App_Themes/vcdqg/images/ico-feat6.png" alt="" />
                                    </div>
                                    <h4 class="tit">Cam kết hàng hóa</h4>
                                    <div class="desc">
                                        Kiểm đếm hàng hóa chính xác, đóng gỗ hàng không bị móp méo khi vận chuyển
								
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </aside>
                    <!--inner-cont-->
                </div>
            </section>

            <section id="achievements" class="sec use-bg" data-bg="/App_Themes/vcdqg/images/banner4.jpg">
                <div class="container">
                    <ul class="use-cols list text-center white">
                        <li class="it col">
                            <div class="num m-color"><span class="animate-num">5</span>h+</div>
                            <div class="txt">Đặt hàng trong</div>
                        </li>
                        <li class="it col">
                            <div class="num m-color"><span class="animate-num">975</span></div>
                            <div class="txt">Khách hàng hài lòng</div>
                        </li>
                        <li class="it col">
                            <div class="num m-color"><span class="animate-num">8997</span>+</div>
                            <div class="txt">Đơn hàng hoàn thành</div>
                        </li>
                        <li class="it col">
                            <div class="num m-color"><span class="animate-num">91</span>%</div>
                            <div class="txt">Đơn hàng hoàn thành</div>
                        </li>
                    </ul>
                </div>
            </section>

            <section id="sec-contact" class="sec">
                <div class="container">
                    <h3 class="sec-tit"><span class="sub">LIÊN HỆ</span> VỚI CHÚNG TÔI</h3>
                    <div class="sec-desc text-center">
                        Hãy gửi cho chúng tôi 1 email nếu bạn có bất cứ thắc mắc nào
				
                    </div>
                    <div class="cont-wp clear black">
                        <aside class="side cont b500">
                            <h4 class="">VITI EXPRESS Xin Chân Thành Cám Ơn Bạn!</h4>
                            <p>Xin chân thành cám ơn bạn đã ghé thăm website của chúng tôi. Chúng tôi xin chúc bạn một ngày may mắn, tốt lành</p>
                            <ul class="contact">
                                <asp:Literal ID="ltrContact" runat="server"></asp:Literal>

                            </ul>

                        </aside>
                        <aside class="side form">
                            <h4>Liên hệ</h4>

                            <div class="main-form">
                                <div class="form-row">
                                    <div class="field">
                                        <asp:TextBox ID="txtFullname" CssClass="f-control required" placeholder="Họ và tên" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtFullname" ErrorMessage="Vui lòng điền họ và tên"
                                            ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="field">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="f-control required" placeholder="Email">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Vui lòng điền email"
                                            ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ForeColor="Red"
                                            ErrorMessage="Sai định dạng Email" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                            ValidationGroup="a" Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="field">
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="f-control required" placeholder="Số điện thoại">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Vui lòng điền số điện thoại"
                                            ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="field">
                                        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="f-control required" placeholder="Lời nhắn"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="submit">
                                    <asp:Button ID="btnRegister" runat="server" CssClass="main-btn hover submit-btn" Text="Gửi liên hệ"
                                        OnClick="btnRegister_Click" ValidationGroup="a" />
                                </div>

                            </div>
                            <!--main-form-->

                        </aside>
                    </div>
                    <!--cont-wp-->
                </div>
                <!--container-->
            </section>

        </div>
        <!--#primary-->

        <!--Start footer-->
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
            $(document).ready(function () {
                $(".search-form-detail ul li a").click(function () {
                    $('.search-active').removeClass('search-active');
                    $(this).addClass('search-active');
                });
            });
        </script>
    </main>
</asp:Content>
