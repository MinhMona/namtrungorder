<%@ Page Title="" Language="C#" MasterPageFile="~/camthachMaster.Master" AutoEventWireup="true" CodeBehind="Default7.aspx.cs" Inherits="NHST.Default6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section class="banner">
            <div class="all">
                <section class="banner__main">
                    <div class="title">
                        <h2 class="fz-30">Chúng tôi</h2>
                        <h4 class="fz-60"><span>Uy Tín</span>
                            là hàng đầu</h4>
                    </div>
                    <style>
                        .search-item-left {
                            float: left;
                            width: 70%;
                        }

                        .search-item-right {
                            float: left;
                            width: 30%;
                        }

                        .search-item .search-item-left .bottom-content .form-search {
                            position: relative;
                            float: left;
                            width: 80%;
                        }

                        .search-item .form-control {
                            height: 45px;
                            border-radius: 4px;
                            background-color: #f8f8f8;
                            font-weight: 500;
                            width: 98%;
                        }

                        .search-item .search-item-left .bottom-content .form-search a {
                            position: absolute;
                            top: 15px;
                            left: 95%;
                            color: #29aae1;
                        }

                        .search-item .btn {
                            height: 45px;
                            line-height: 28px;
                            width: 150px;
                            margin-left: 5px;
                        }


                        .search-item .search-item-right .button .btn.btn-icon {
                            padding-left: 4.3rem;
                        }

                        .search-item .search-item-right .button .btn > i {
                            padding: 10px 0 10px 25px;
                            font-size: 25px;
                            background-color: unset;
                        }

                        .search-item .top-content {
                            float: left;
                            width: 100%;
                            min-height: 75px;
                        }

                        .top-content h2 {
                            padding: 20px 0;
                            width: 100%;
                            font-size: 20px;
                            color: #fff;
                            text-align: center;
                        }

                        .right-content-button {
                            text-align: center;
                            float: left;
                            width: 100%;
                        }

                        .search-item {
                            margin-bottom: 50px;
                        }
                    </style>
                    <div class="search-item">
                        <div class="search-item-left">
                            <div class="top-content">
                                <div class="checkbox">
                                    <label class="">
                                        <input type="checkbox" name="" checked>
                                        <span class="text">taobao.com</span>
                                    </label>
                                    <label class="">
                                        <input type="checkbox" name="">
                                        <span class="text">tmall.com</span>
                                    </label>
                                    <label class="">
                                        <input type="checkbox" name="">
                                        <span class="text">1688.com</span>
                                    </label>
                                </div>
                            </div>
                            <div class="bottom-content">
                                <div class="form-search">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Nhập link sản phẩm"></asp:TextBox>
                                    <a href="javascript:;"><i class="fas fa-search"></i></a>
                                    <%--<span href="javascript:;"><i class="fas fa-search"></i></span>--%>
                                </div>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-1" Text="Tạo đơn hàng" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="search-item-right">
                            <div class="top-content">
                                <h2>Tải công cụ đặt hàng tại đây:</h2>
                            </div>
                            <div class="right-content-button button">
                                <a href="" class="btn btn-2 btn-icon"><i class="fab fa-chrome"></i>Chrome</a>
                                <a href="" class="btn btn-3 btn-icon"><i class="fab fa-chrome"></i>Cốc cốc</a>
                            </div>
                        </div>

                        <div class="content">
                        </div>
                    </div>

                </section>

                <div class="banner__bot">

                    <div class="banner__bot-child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/banner-icon-1.png" alt="icon banner">
                        </div>
                        <div class="content">
                            <p>Tỉ giá 1¥</p>
                            <h2 class="fz-30">
                                <asp:Literal ID="ltrCurrency" runat="server"></asp:Literal></h2>
                        </div>
                    </div>
                    <div class="banner__bot-child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/banner-icon-2.png" alt="icon banner">
                        </div>
                        <div class="content">
                            <p>Email</p>
                            <h2 class="fz-30">
                                <asp:Literal ID="ltrEmail" runat="server"></asp:Literal></h2>
                        </div>
                    </div>
                    <div class="banner__bot-child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/banner-icon-3.png" alt="icon banner">
                        </div>
                        <div class="content">
                            <p>Hotline</p>
                            <h2 class="fz-30">
                                <asp:Literal ID="ltrHotline" runat="server"></asp:Literal></h2>
                        </div>
                    </div>
                    <div class="banner__bot-child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/banner-icon-4.png" alt="icon banner">
                        </div>
                        <div class="content">
                            <p>Giờ hoạt động:</p>
                            <h2 class="fz-30">
                                <asp:Literal ID="ltrTimeWork" runat="server"></asp:Literal></h2>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="business">
            <div class="bus-wrap">
                <div class="col-right">
                    <article class="bus__child-hd">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/bussiness-icon.png" alt="truck icon">
                        </div>
                        <div class="content">
                            <h1 class="fz-36">Về dịch vụ</h1>
                            <p class="fz-18">
                                <asp:Literal ID="ltrService" runat="server"></asp:Literal>
                            </p>
                        </div>
                    </article>
                </div>
                <div class="col-6">
                    <asp:Literal ID="ltrServiceList" runat="server"></asp:Literal>
                </div>
            </div>
        </section>

        <section class="information">
            <div class="all">
                <div class="infor-col-3">

                    <article class="col__child">
                        <h1 class="fz-48">5</h1>
                        <p class="fz-18">Năm kinh nghiệm</p>
                    </article>
                    <article class="col__child">
                        <h1 class="fz-48">12,345</h1>
                        <p class="fz-18">Khách hàng</p>
                    </article>
                    <article class="col__child">
                        <h1 class="fz-48">86,500</h1>
                        <p class="fz-18">Đơn hàng</p>
                    </article>

                </div>
            </div>
        </section>

        <section class="reg-guide">
            <div class="all">
                <section class="reg__title">
                    <h1 class="fz-36">Hướng dẫn đăng ký</h1>
                </section>

                <div class="reg-col-6">

                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/reg-guide-1.png" alt="step guide icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Đăng ký tài khoản</h4>
                            <%--<p class="fz-14">With easy access to Broadband and DSL the number of people using the Internet has skyrocket in recent years. Email.</p>--%>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/reg-guide-2.png" alt="step guide icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Cài đặt công cụ mua hàng</h4>
                            <%--<p class="fz-14">With easy access to Broadband and DSL the number of people using the Internet has skyrocket in recent years. Email.</p>--%>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/reg-guide-3.png" alt="step guide icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Mua hàng và thêm vào giỏ hàng</h4>
                            <%--<p class="fz-14">With easy access to Broadband and DSL the number of people using the Internet has skyrocket in recent years. Email.</p>--%>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/reg-guide-4.png" alt="step guide icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Gửi đơn đặt hàng</h4>
                            <%--<p class="fz-14">With easy access to Broadband and DSL the number of people using the Internet has skyrocket in recent years. Email.</p>--%>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/reg-guide-5.png" alt="step guide icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Đặt cọc tiền hàng</h4>
                            <%--<p class="fz-14">With easy access to Broadband and DSL the number of people using the Internet has skyrocket in recent years. Email.</p>--%>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/reg-guide-6.png" alt="step guide icon">
                        </div>
                        <article class="content">
                            <h4 class="fz-18">Nhận hàng và thanh toán</h4>
                            <%--<p class="fz-14">With easy access to Broadband and DSL the number of people using the Internet has skyrocket in recent years. Email.</p>--%>
                        </article>
                    </div>

                </div>
            </div>
        </section>

        <section class="benefits">
            <div class="all">
                <div class="bene__title">
                    <h1 class="fz-36">Quyền lợi khách hàng</h1>
                </div>
                <div class="bene-col-3">
                    <asp:Literal ID="ltrQLKH" runat="server"></asp:Literal>
                    <%--<article class="col__child">
                        <div class="img">
                            <a href="#">
                                <img src="/App_Themes/Camthach/images/benefits-img-1.jpg" alt="quyen loi img"></a>
                        </div>
                        <section class="content">
                            <h4 class="fz-18"><a href="#">Khách hàng thân thiết</a></h4>
                            <p class="fz-14">Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.</p>
                            <a href="" class="link">Xem thêm <i class="fas fa-arrow-alt-circle-right"></i></a>
                        </section>
                    </article>
                    <article class="col__child">
                        <div class="img">
                            <a href="#">
                                <img src="/App_Themes/Camthach/images/benefits-img-2.jpg" alt="quyen loi img"></a>
                        </div>
                        <section class="content">
                            <h4 class="fz-18"><a href="#">Ưu đãi theo sản lượng tháng</a></h4>
                            <p class="fz-14">Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.</p>
                            <a href="" class="link">Xem thêm <i class="fas fa-arrow-alt-circle-right"></i></a>
                        </section>
                    </article>
                    <article class="col__child">
                        <div class="img">
                            <a href="#">
                                <img src="/App_Themes/Camthach/images/benefits-img-3.jpg" alt="quyen loi img"></a>
                        </div>
                        <section class="content">
                            <h4 class="fz-18"><a href="#">Marketing và bán hàng</a></h4>
                            <p class="fz-14">Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.</p>
                            <a href="" class="link">Xem thêm <i class="fas fa-arrow-alt-circle-right"></i></a>
                        </section>
                    </article>--%>
                </div>
            </div>
        </section>

        <section class="contact">
            <div class="all">
                <div class="contact-col-3">

                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/banner-icon-2.png" alt="contact icon">
                        </div>
                        <article class="content">
                            <p class="fz-14">Email:</p>
                            <h2 class="fz-30">
                                <asp:Literal ID="ltrEmail1" runat="server"></asp:Literal></h2>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/banner-icon-3.png" alt="contact icon">
                        </div>
                        <article class="content">
                            <p class="fz-14">Hotline:</p>
                            <h2 class="fz-30">
                                <asp:Literal ID="ltrHotline1" runat="server"></asp:Literal>
                            </h2>
                        </article>
                    </div>
                    <div class="col__child">
                        <div class="img">
                            <img src="/App_Themes/Camthach/images/banner-icon-4.png" alt="contact icon">
                        </div>
                        <article class="content">
                            <p class="fz-14">Giờ hoạt động:</p>
                            <h2 class="fz-30">
                                <asp:Literal ID="ltrTimework1" runat="server"></asp:Literal>
                            </h2>
                        </article>
                    </div>

                </div>
            </div>
        </section>
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
        }

            .btn.btn-close-full:hover {
                background: #6692a5;
            }
    </style>
</asp:Content>
