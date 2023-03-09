<%@ Page Title="" Language="C#" MasterPageFile="~/NamTrungMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NHST.Default12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <div class="banner">
            <div class="all">
                <div class="banner-entry">
                    <div class="banner-content">
                        <div class="banner-title">
                            <h1 class="hd">Công cụ đặt hàng chuyên nghiệp</h1>
                        </div>
                        <div class="addon-button">
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-nh%E1%BA%ADp/bjjkjhapbkfmldkecdelhifaalbcepdo" target="_blank" class="mn-btn gg-btn"><i class="fab fa-chrome"></i>Chrome</a>
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-nh%E1%BA%ADp/bjjkjhapbkfmldkecdelhifaalbcepdo" target="_blank" class="mn-btn cc-btn"><i class="fab fa-chrome"></i>Cốc cốc</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="supply">
            <div class="all">
                <div class="sec-content">
                    <div class="sec-title">
                        <h2 class="hd">Dịch vụ cung cấp</h2>
                    </div>
                    <div class="sec-detail">
                        Nam Trung với nhiều năm kinh nghiệm trong ngành giao thương với Trung Quốc sẽ là địa chỉ tin cậy cho bạn nhập hàng để kinh doanh<br />
                        Phí dịch vụ rẻ, cạnh tranh, dịch vụ không ngừng cải tiến liên tục, chắc chắn chúng tôi sẽ không để bạn thất vọng
                    </div>
                </div>
                <div class="supply-desc">
                    <div class="supdesc__child">
                        <h3 class="desc-hd">order hàng</h3>
                        <div class="progess-bar">
                            <span class="progess-fill" data-percent="100"></span>
                        </div>
                    </div>
                    <div class="supdesc__child">
                        <h3 class="desc-hd">Ký gửi hàng</h3>
                        <div class="progess-bar">
                            <span class="progess-fill" data-percent="95"></span>
                        </div>
                    </div>
                    <div class="supdesc__child">
                        <h3 class="desc-hd">Tìm nguồn hàng</h3>
                        <div class="progess-bar">
                            <span class="progess-fill" data-percent="90"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="services">
            <div class="all">
                <div class="sec-title2">
                    <h2 class="hd">Dịch vụ của chúng tôi</h2>
                </div>
                <asp:Repeater ID="rptService" runat="server" EnableViewState="false">
                    <HeaderTemplate>
                        <ul class="list-services">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="serv__item wow rollIn">
                            <div class="serv-card">
                                <div class="img">
                                    <img src="<%#Eval("ServiceIMG") %>" alt="">
                                </div>
                                <h4 class="hd"><%#Eval("ServiceName") %></h4>
                                <p><%#Eval("ServiceContent") %></p>
                            </div>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="commercial">
            <div class="all">
                <div class="commercial-wrap">
                    <div class="comm-info wow flipInX">
                        <span class="comm-bg"></span>
                        <h2 class="hd">Kết nối giao thương, Xoá nhoà khoảng cách</h2>
                        <div class="comm-list">
                            <div class="comm-detail">
                                <p class="tt">5 giờ</p>
                                <p>Đặt hàng ngay sau khi đặt cọc</p>
                            </div>
                            <div class="comm-detail">
                                <p class="tt">3 ngày</p>
                                <p>Từ khi chúng tôi nhận hàng tới Hà Nội</p>
                            </div>
                            <div class="comm-detail">
                                <p class="tt">96%</p>
                                <p>Khách hàng hài lòng</p>
                            </div>
                            <div class="comm-detail">
                                <p class="tt">99%</p>
                                <p>Đơn hàng giao dịch thành công</p>
                            </div>
                        </div>
                    </div>
                    <div class="sec-content">
                        <div class="sec-title">
                            <h2 class="hd">Tiên phong kết nối
                                    <br>
                                giao thương Trung Quốc</h2>
                        </div>
                        <div class="sec-detail">
                            Chúng tôi với các thành viên sáng lập là những người trẻ, nhiều năm kinh nghiệm trong lĩnh vực giao thương với Trung Quốc
                            <br />
                            Chúng tôi thông thạo hiểu biết về Trung Quốc, về nguồn hàng, văn hóa Trung Quốc
                            <br />
                            Chúng tôi có thể chuyển hàng toàn quốc, dù bạn ở bất cứ nơi đâu cũng có thể mua được hàng hóa Trung Quốc
                            <br />
                            Với sứ mệnh kết nối giao thương, xóa nhòa khoảng cách, chắc chắn chúng tôi là địa chỉ tin cậy cho bạn nhập hàng Trung Quốc.
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="present">
            <div class="all">
                <ul class="list-present">
                    <li class="pre__item wow fadeInLeft">
                        <p class="hd">6</p>
                        <span>Năm kinh nghiệm</span>
                    </li>
                    <li class="pre__item wow fadeInLeft">
                        <p class="hd">1689</p>
                        <span>Khách hàng sử dụng</span>
                    </li>
                    <li class="pre__item wow fadeInLeftBig">
                        <p class="hd">9668</p>
                        <span>Đơn được đặt hàng</span>
                    </li>
                    <li class="pre__item wow fadeInLeftBig">
                        <p class="hd">3839</p>
                        <span>Đánh giá khách hàng</span>
                    </li>
                </ul>
            </div>
        </div>

        <div class="news">
            <div class="all">
                <asp:Repeater ID="rptNotiNews" runat="server" EnableViewState="false">
                    <HeaderTemplate>
                        <ul class="list-news wow lightSpeedIn">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="news__item">
                            <div class="news-card">
                                <a href="<%#Eval("NodeAliasPath") %>" class="card-bg">
                                    <span class="bg-img" style="background-image: url('<%# Eval("IMG")%>')"></span>
                                    <span class="date"><%#Eval("CreatedDate","{0:dd}") %> tháng <%#Eval("CreatedDate","{0:MM, yyyy}") %></span>
                                </a>
                                <div class="ct">
                                    <h4 class="hd"><a href="<%#Eval("NodeAliasPath") %>"><%#Eval("Title") %></a></h4>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="register wow zoomIn">
            <div class="all">
                <div class="reg-content">
                    <h2 class="hd">Hãy Để Chúng Tôi Giúp Việc Kinh Doanh Của Bạn Tốt Hơn
                            <br>
                        Tham Gia Với Chúng Tôi Ngay,
                            <br>
                        Để Nhận Nhiều Ưu Đãi</h2>
                    <div class="reg-button">
                        <a href="/dang-ky" class="mn-btn btn-1 auto-w">Đăng ký</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="contact">
            <div class="all">

                <div class="contact-content">
                    <div class="sec-title2">
                        <h2 class="hd">Liên hệ với chúng tôi</h2>
                    </div>
                    <div class="ct-detail">
                        <asp:Literal ID="ltrContactFooter" runat="server"></asp:Literal>

                    </div>
                </div>
                <div class="contact-form wow rotateInDownLeft">
                    <div class="form__child">
                        <asp:TextBox ID="txtFullName" runat="server" placeholder="Họ và tên" CssClass="f-control">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rq" runat="server" ValidationGroup="contact" ControlToValidate="txtFullName"
                            ErrorMessage="Không để trống" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form__child">
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="f-control">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rq1" runat="server" ValidationGroup="contact" ControlToValidate="txtEmail"
                            ErrorMessage="Không để trống" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form__child">
                        <asp:TextBox runat="server" ID="txtPhone" CssClass="f-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                            MaxLength="11"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtPhone" ForeColor="Red"
                            ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                    </div>

                    <div class="form__child">
                        <asp:TextBox ID="txtContent" runat="server" placeholder="Nội dung" TextMode="MultiLine"
                            CssClass="f-control">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="contact" ControlToValidate="txtContent"
                            ErrorMessage="Không để trống" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form__submit">
                        <asp:Button ID="btnSend" runat="server" ValidationGroup="contact" UseSubmitBehavior="false"
                            Text="Gửi" CssClass="mn-btn btn-1" OnClick="btnSend_Click" />
                        <%--<a href="#" class="mn-btn btn-1">Gửi</a>--%>
                    </div>
                </div>

            </div>
        </div>

        <div class="partner">
            <div class="all">
                <asp:Repeater ID="rptPartner" runat="server" EnableViewState="false">
                    <HeaderTemplate>
                        <ul class="list-brand" id="brand-slider">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="brand__item">
                            <div class="img">
                                <a href="<%#Eval("PartnerLink") %>">
                                    <img src="<%#Eval("PartnerIMG") %>" alt=""></a>
                            </div>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
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
            background: #f07b3f;
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
            padding: 10px 20px;
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
            padding: 10px 20px;
        }

            .btn.btn-close-full:hover {
                background: #6692a5;
            }
    </style>
</asp:Content>