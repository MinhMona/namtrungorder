<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-van-chuyen-ho.aspx.cs" Inherits="NHST.danh_sach_don_van_chuyen_ho" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/NewUI/css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        .black {
            color: #2a363b;
        }

        ul {
            margin: 0;
            padding: 0;
            list-style: none;
        }

        .m-color {
            color: #ff5000;
        }

        b, strong, .b {
            font-weight: bold;
        }

        b, strong {
            font-weight: bolder;
        }

        .page.orders-list .statistics li:last-child {
            border: none;
        }

        .page.orders-list .statistics li {
            display: inline-block;
            padding-right: 10px;
            margin-right: 10px;
            border-right: 1px solid #2a363b;
            line-height: 1;
        }

        .page.orders-list .stat-detail {
            width: 100%;
            margin: 20px 0;
            border-top: 1px solid #e1e1e1;
            border-bottom: 1px solid #e1e1e1;
            display: block;
            padding: 5px 0;
        }

        table {
            border-collapse: collapse;
        }

        .page.orders-list .stat-detail th, .page.orders-list .stat-detail td {
            padding: 10px 0;
            vertical-align: top;
            text-align: left;
        }

        .page.orders-list .stat-detail th {
            padding-right: 35px;
        }

        .page.orders-list .stat-detail td {
            display: inline-block;
            width: 395px;
        }

        article, aside, details, figcaption, figure, footer, header, main, menu, nav, section {
            display: block;
        }

        .clear {
            zoom: 1;
        }

        input, select {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        .RadPicker_Default .rcCalPopup, .RadPicker_Default .rcTimePopup {
            display: none;
        }

        html body .riSingle .riTextBox[type="text"] {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        .page .filters {
            background: #ebebeb;
            border: 1px solid #e1e1e1;
            font-weight: bold;
            padding: 20px;
            margin-bottom: 20px;
        }

        .page.orders-list .filters .lbl {
            padding-right: 50px;
        }

        .page .filters ul li {
            display: inline-block;
            text-align: center;
            padding-right: 2px;
        }

        .page .filters ul li {
            padding-right: 4px;
        }

        .page .filters input {
            padding: 2px 10px;
        }

        .page.orders-list .filters input.order-id {
            width: 270px;
        }

        .page .status-list > li {
            display: block;
            float: left;
            margin: 0 1px 10px 0;
        }

        .page .status-list a {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #f8f8f8;
            color: #959595;
            font-weight: bold;
            padding: 0 15px;
        }

            .page .status-list li.current > a, .page .status-list a:hover {
                background: #ff5000;
                color: #fff;
            }

        .width-20-per {
            width: 20%;
        }

        .width-15-per {
            width: 15%;
        }

        .page.orders-list .tbl-subtotal {
            margin-bottom: 20px;
        }

            .page.orders-list .tbl-subtotal th {
                padding-right: 60px;
            }

            .page.orders-list .tbl-subtotal td {
                padding: 8px 30px 8px 0;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Danh sách đơn hàng vận chuyển hộ</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="content-text">
                            <div id="primary" class="page orders-list">
                                <div class="container" style="width:100%;">
                                    <aside class="filters">
                                        <ul>
                                            <li class="lbl">Tìm kiếm</li>
                                            <li>
                                                <asp:TextBox ID="txtOrderCode" runat="server" placeholder="Tìm mã vận đơn" CssClass="form-control"></asp:TextBox>
                                            </li>
                                            <li>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Đơn hủy"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Đang xử lý"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Đã về kho TQ"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Đã về kho VN"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="Khách đã thanh toán"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="Đã hoàn thành"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li class="width-20-per">
                                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                    DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                                    <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                                    </DateInput>
                                                </telerik:RadDateTimePicker>
                                            </li>
                                            <li class="width-20-per">
                                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rTD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                    DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                                                    <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                                    </DateInput>
                                                </telerik:RadDateTimePicker>
                                            </li>
                                            <li class="width-15-per" style="width: 100%; clear: both;margin-top:10px;">
                                                <asp:Button ID="btnSear" runat="server" CssClass="btn pill-btn primary-btn" OnClick="btnSear_Click" Text="LỌC TÌM KIẾM" />
                                                <%--<a class="submit-btn" href="#">LỌC TÌM KIẾM</a>--%>
                                            </li>
                                        </ul>
                                    </aside>
                                </div>
                                <%--<div class="container">
                                    <asp:Button ID="bttnAll" runat="server" CssClass="order-status bg-yellow" OnClick="bttnAll_Click" />
                                    <asp:Button ID="btn0" runat="server" CssClass="order-status bg-red" OnClick="btn0_Click" />
                                    <asp:Button ID="btn1" runat="server" CssClass="order-status bg-black" OnClick="btn1_Click" />
                                    <asp:Button ID="btn2" runat="server" CssClass="order-status bg-bronze" OnClick="btn2_Click" />
                                    <asp:Button ID="btn5" runat="server" CssClass="order-status bg-green" OnClick="btn5_Click" />
                                    <asp:Button ID="btn6" runat="server" CssClass="order-status bg-green" OnClick="btn6_Click" />
                                    <asp:Button ID="btn7" runat="server" CssClass="order-status bg-orange" OnClick="btn7_Click" />
                                    <asp:Button ID="btn9" runat="server" CssClass="order-status bg-blue" OnClick="btn9_Click" />
                                    <asp:Button ID="btn10" runat="server" CssClass="order-status bg-blue" OnClick="btn10_Click" />                                    
                                    <asp:Button ID="btnAllrequest" runat="server" Style="display: none" OnClick="btnAllrequest_Click" UseSubmitBehavior="false" />
                                </div>--%>
                            </div>
                            <div class="table-panel">
                                <div class="table-rps table-responsive">
                                    <table class="normal-table">
                                        <tr>
                                            <th class="id" style="width: 1%;">ID</th>
                                            <th class="pro" style="width: 1%;">Số kiện</th>
                                            <th class="pro" style="width: 5%;">Tổng tiền</th>
                                            <th class="pro" style="width: 1%;">Tổng trọng lượng</th>
                                            <th class="date" style="width: 5%;">Ngày đặt hàng</th>
                                            <th class="status" style="width: 5%;">Trạng thái đơn hàng</th>
                                            <th class="date" style="width: 5%;">Danh sách mã vận đơn</th>
                                            <th class="status" style="width: 5%;">Thao tác</th>
                                        </tr>
                                        <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                    </table>
                                    <div class="pagination">
                                        <%this.DisplayHtmlStringPaging1();%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </main>
    <style>
        .table.table-bordered > thead > tr > th, .table.table-bordered > tbody > tr > th, .table.table-bordered > tfoot > tr > th, .table.table-bordered > thead > tr > td, .table.table-bordered > tbody > tr > td, .table.table-bordered > tfoot > tr > td {
            padding: 10px 0;
        }

        .rgPager table, .rgPager table:hover {
            border: none !important;
        }

            .rgPager table th {
                background: none;
            }


        select.form-control {
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            -ms-appearance: none;
            -o-appearance: none;
            background: #fff url(/App_Themes/NHST/images/icon-select.png) no-repeat right 15px center;
            padding-right: 25px;
            padding-left: 15px;
            line-height: 40px;
        }

        .ycgh-chk {
            width: 13%;
            margin: 0 auto;
        }

        .btn.pill-btn {
            border-radius: 0px;
            -webkit-border-radius: 0px;
            font-size: 15px;
            font-weight: normal;
            text-transform: none;
            padding: 5px;
        }

        .table-panel .table-panel-main table tr:nth-child(odd) {
            background-color: #fafafa;
        }

        .viewmore-orderlist {
            background-color: #ff5000;
            color: #fff;
            border: 1px solid transparent;
            padding: 5px;
            float: left;
            width: 100%;
            margin-bottom: 5px;
            text-align: center;
        }

            .viewmore-orderlist:hover {
                background-color: #ca3f00;
                color: #fff;
            }
    </style>
    <asp:HiddenField ID="hdfListOrder" runat="server" />
    <asp:HiddenField ID="hdfAmount" runat="server" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:Button ID="btnDeposit" runat="server" OnClick="btnDeposit_Click" Style="display: none" />
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function show_messageorder(content) {
                var obj = $('body');
                $(obj).css('overflow', 'hidden');
                $(obj).attr('onkeydown', 'keyclose_ms(event)');
                var bg = "<div id='bg_popup'></div>";
                var fr = "<div id='pupip' class=\"columns-container1\"><div class=\"container\" id=\"columns\"><div class='row'>" +
                         "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content\"><a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
                fr += "     <div class=\"changeavatar\">";
                fr += content;
                fr += "         <label class=\"lbl\">Nhập địa chỉ giao hàng: </label>";
                fr += "         <div class=\"clearfix\"></div>";
                fr += "         <input class=\"addresscustomer\" type=\"text\" class=\"\" style=\"\margin:10px 0;\"/>";
                fr += "         <div class=\"clearfix\"></div>";
                fr += "         <a class=\"submit-btn\" onclick=\"acceptgiao()\" style=\"float:right;\">Xác nhận</a>";
                fr += "     </div>";
                fr += "   </div>";

                fr += "</div></div></div>";
                $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                $(fr).appendTo($(obj));
                setTimeout(function () {
                    $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                    $("#bg_popup").attr("onclick", "close_popup_ms()");
                }, 1000);
            }
            function keyclose_ms(e) {
                if (e.keyCode == 27) {
                    close_popup_ms();
                }
            }
            function close_popup_ms() {
                $("#pupip").animate({ "opacity": 0 }, 400);
                $("#bg_popup").animate({ "opacity": 0 }, 400);
                setTimeout(function () {
                    $("#pupip").remove();
                    $(".zoomContainer").remove();
                    $("#bg_popup").remove();
                    $('body').css('overflow', 'auto').attr('onkeydown', '');
                }, 500);
            }
            function requestall() {
                var listOrder = '';
                var count = 0;
                $(".ycgh-chk").each(function () {
                    var status = $(this).attr("data-status");
                    var status1 = $(this).attr("data-sta");
                    var id = $(this).attr("data-id");
                    if (status == 9) {
                        if (status1 == 0) {
                            if ($(this).is(":checked")) {
                                listOrder += id + "|";
                                count++;
                            }
                        }
                    }
                });
                if (count > 0) {
                    var r = confirm("Bạn muốn yêu cầu giao " + count + " đơn hàng đã chọn!");
                    if (r == true) {
                        $("#<%=hdfListOrder.ClientID%>").val(listOrder);
                        $("#<%=hdfAmount.ClientID%>").val(count);
                        <%--$("#<%=btnAllrequest.ClientID%>").click();--%>
                    } else {
                    }
                }
                else {
                    alert("Vui lòng chọn đơn hàng mà bạn muốn giao.");
                }
            }
            function depositOrder(orderID) {
                var c = confirm('Bạn muốn đặt cọc đơn: ' + orderID);
                if (c == true) {
                    $("#<%=hdfOrderID.ClientID%>").val(orderID);
                    $("#<%=btnDeposit.ClientID%>").click();
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <style>
        .order-status {
            float: left;
            margin-right: 5px;
            width: auto;
            margin-bottom: 10px;
            text-align: center;
            border: solid 1px #ccc;
        }

            .order-status:hover {
                border: solid 1px #ccc;
            }
    </style>
</asp:Content>

