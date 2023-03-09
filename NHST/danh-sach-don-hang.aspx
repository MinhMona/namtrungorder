<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-hang.aspx.cs" Inherits="NHST.danh_sach_don_hang1" %>

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
            color: #ad0d12;
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
                background: #ad0d12;
                color: #fff;
            }

        .width-20-per {
            width: 40%;
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

        .table-panel {
            padding: 0 15px;
        }

        .result-select.show {
            -webkit-transform: none;
            transform: none
        }

        .result-select .noti-wrap {
            padding: 10px 24px
        }

            .result-select .noti-wrap .noti-item {
                background: #fff;
                margin: 5px 0;
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -webkit-box-align: center;
                -ms-flex-align: center;
                align-items: center;
                -webkit-box-pack: justify;
                -ms-flex-pack: justify;
                justify-content: space-between
            }

                .result-select .noti-wrap .noti-item p {
                    margin: 0
                }

        @media screen and (max-width: 600px) {
            .result-select .noti-wrap .noti-item {
                -ms-flex-flow: wrap;
                flex-flow: wrap
            }

                .result-select .noti-wrap .noti-item .info {
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    width: 100%;
                    -webkit-box-pack: justify;
                    -ms-flex-pack: justify;
                    justify-content: space-between
                }
        }

        .result-select .noti-wrap .noti-item .col {
            padding: 10px 15px;
            -webkit-transition: all .2s ease-in-out;
            -moz-transition: all .2s ease-in-out;
            -o-transition: all .2s ease-in-out;
            -ms-transition: all .2s ease-in-out;
            transition: all .2s ease-in-out
        }

        @media screen and (max-width: 600px) {
            .result-select .noti-wrap .noti-item .col {
                font-size: 14px
            }
        }

        .result-select .noti-wrap .noti-item .noti-txt {
            font-size: 16px;
            color: #000;
            font-weight: 500;
            width: 300px
        }

            .result-select .noti-wrap .noti-item .noti-txt .count {
                color: red;
                font-size: 20px
            }

        .result-select .noti-wrap .noti-item .noti-total {
            font-size: 16px;
            color: #000;
            font-weight: 500
        }

            .result-select .noti-wrap .noti-item .noti-total .total {
                color: red;
                font-size: 20px
            }

        @media screen and (max-width: 600px) {
            .result-select .noti-wrap .noti-item .noti-total .total {
                font-size: 16px
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Danh sách đơn hàng</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="content-text">
                            <div id="primary" class="page orders-list">
                                <div class="container">
                                    <asp:Literal ID="ltrSumOrder" runat="server"></asp:Literal>
                                    <ul class="statistics black">
                                        <li>Tổng số đơn hàng: <b class="m-color">
                                            <asp:Literal ID="ltrAllOrderCount" runat="server"></asp:Literal></b>
                                        </li>
                                        <li>Tổng trị giá: <b class="m-color">
                                            <asp:Literal ID="ltrAllOrderPrice" runat="server"></asp:Literal></b> VNĐ 
                                        </li>
                                        <li style="display: none">Số tiền để lấy hàng trong kho: <b class="m-color">
                                            <asp:Literal ID="ltrTotalGetAllProduct" runat="server"></asp:Literal></b> VNĐ 
                                        </li>
                                    </ul>
                                    <table class="stat-detail black table-custom" style="display: none">
                                        <tr>
                                            <th rowspan="4">Trong đó:</th>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus0" runat="server"></asp:Literal></b> đơn hàng chưa đặt cọc.</td>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus2" runat="server"></asp:Literal></b> đơn hàng chờ mua hàng.</td>
                                        </tr>
                                        <tr>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus5" runat="server"></asp:Literal></b> đơn hàng Chờ shop TQ phát hàng.</td>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus6" runat="server"></asp:Literal></b> đơn hàng đã có hàng tại TQ.</td>
                                        </tr>
                                        <tr>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus7" runat="server"></asp:Literal></b> đơn hàng đã có hàng tại VN.</td>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus10" runat="server"></asp:Literal></b> đơn hàng đã nhận hàng.</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="container">
                                    <table class="tbl-subtotal">
                                        <tbody>
                                            <tr class="black b">
                                                <th rowspan="7" valign="top">Thông tin tiền hàng:</th>
                                                <td style="display: none">Tổng tiền hàng chưa giao:	</td>
                                                <td style="display: none">
                                                    <asp:Literal ID="ltrTongtienhangchuagiao" runat="server"></asp:Literal>
                                                    VNĐ </td>
                                                <td style="display: none"></td>
                                                <td>Tổng tiền hàng cần đặt cọc: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangcandatcoc" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Tổng tiền hàng cần thanh toán để lấy hàng đã nhận về kho đích: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangcanthanhtoandelayhang" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr style="display: none">
                                                <td>Tổng tiền hàng chờ về kho TQ: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangchovekhotq" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr style="display: none">
                                                <td>Tổng tiền hàng đã về kho TQ: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangdavekhotq" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr style="display: none">
                                                <td>Tổng tiền hàng đang ở kho đích : </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangdangokhovn" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                                <div class="container">
                                    <aside class="filters">
                                        <ul>
                                            <li class="lbl">Tìm kiếm</li>

                                            <li class="input-field col s12 l2">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Chọn loại tìm kiếm"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ID đơn hàng"></asp:ListItem>
                                                    <%--<asp:ListItem Value="2" Text="Ghi chú sản phẩm"></asp:ListItem>--%>
                                                    <asp:ListItem Value="3" Text="Website"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>

                                            <li class="input-field col s12 l4">
                                                <asp:TextBox ID="txtSearhc" placeholder="ID/Ghi chú/Website" runat="server" CssClass="search_name"></asp:TextBox>
                                                <%--  <label for="search_name">
                                                </label>--%>
                                            </li>

                                            <li>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Chưa đặt cọc"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Hủy đơn hàng"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Chờ mua hàng"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Chờ shop TQ phát hàng"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="Shop đã phát hàng"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="Đang mua hàng"></asp:ListItem>
                                                    <%--<asp:ListItem Value="13" Text="Đã thanh toán cho shop"></asp:ListItem>--%>
                                                    <asp:ListItem Value="6" Text="Đã về kho TQ"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="Đã về kho VN"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="Khách đã thanh toán"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="Đã hoàn thành"></asp:ListItem>
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
                                            <li class="width-15-per">
                                                <asp:Button ID="btnSear" runat="server" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover" OnClick="btnSear_Click" Text="LỌC TÌM KIẾM" />
                                                <%--<a class="submit-btn" href="#">LỌC TÌM KIẾM</a>--%>
                                            </li>
                                        </ul>
                                    </aside>
                                </div>
                                <div class="container">
                                    <asp:Button ID="bttnAll" runat="server" CssClass="order-status bg-yellow" OnClick="bttnAll_Click" />
                                    <asp:Button ID="btn0" runat="server" CssClass="order-status bg-red" OnClick="btn0_Click" />
                                    <asp:Button ID="btn1" runat="server" CssClass="order-status bg-black" OnClick="btn1_Click" />
                                    <asp:Button ID="btn2" runat="server" CssClass="order-status bg-bronze" OnClick="btn2_Click" />
                                    <asp:Button ID="btn5" runat="server" CssClass="order-status bg-green" OnClick="btn5_Click" />
                                    <asp:Button ID="btn11" runat="server" CssClass="order-status bg-green" OnClick="btn11_Click" />
                                    <asp:Button ID="btn12" runat="server" CssClass="order-status bg-red" OnClick="btn12_Click" />
                                    <%--<asp:Button ID="btn13" runat="server" CssClass="order-status bg-orange" OnClick="btn13_Click" />--%>
                                    <asp:Button ID="btn6" runat="server" CssClass="order-status bg-green" OnClick="btn6_Click" />
                                    <asp:Button ID="btn7" runat="server" CssClass="order-status bg-orange" OnClick="btn7_Click" />
                                    <asp:Button ID="btn9" runat="server" CssClass="order-status bg-blue" OnClick="btn9_Click" />
                                    <asp:Button ID="btn10" runat="server" CssClass="order-status bg-blue" OnClick="btn10_Click" />

                                </div>
                                <div class="container">
                                    <p style="margin: 10px 0; display: block; line-height: 30px;">
                                        <asp:Literal ID="ltrBtnYCG" runat="server"></asp:Literal>
                                    </p>
                                    <%--<asp:Button ID="btnAllrequest" runat="server" Style="display: none" OnClick="btnAllrequest_Click" UseSubmitBehavior="false" />--%>
                                </div>
                                <div class="container" style="margin-top: 30px;">
                                    <asp:Literal ID="ltrbtndepay" runat="server"></asp:Literal>
                                </div>

                                 <div class="clearfix"></div>
                                    <div class="result-select">
                                        <div class="all">
                                            <div class="fixed-noti">
                                                <div class="hide-noti"></div>
                                                <div class="noti-wrap">


                                                    <div class="noti-item deposit" style="display: none">
                                                        <div class="info">
                                                            <div class="noti-txt col">
                                                                <p>
                                                                    <span class="count">
                                                                        <asp:Label ID="lblDepositOrderCount" runat="server"></asp:Label></span>
                                                                    đơn hàng <span class="type">đặt
                                                                                    cọc</span>.
                                                                </p>
                                                            </div>
                                                            <div class="noti-total col">
                                                                <p>
                                                                    Tổng tiền: <span
                                                                        class="total totaldepositselected">
                                                                        <asp:Literal runat="server" ID="ltrTotalPriceDeposit"></asp:Literal>
                                                                        VNĐ</span>
                                                                </p>
                                                            </div>
                                                        </div>

                                                        <div class="noti-action col">
                                                            <a href="javascript:;" onclick="depositSelected()" class="btn btn-success btn-block pill-btn primary-btn main-btn hover"><span class="type">đặt
                                                                                cọc</span>đơn đã chọn</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                            </div>
                            <div class="table-panel">
                                <div class="table-rps table-responsive">

                                    <table class="normal-table">
                                        <tr>
                                            <th class="center-checkbox" style="width: 1%;"></th>
                                            <th class="id" style="width: 1%;">ID</th>
                                            <th class="pro" style="width: 1%;">Ảnh sản phẩm</th>
                                            <th class="pro" style="width: 5%;">Tên Shop</th>
                                            <th class="qty" style="width: 5%;">Website</th>
                                            <th class="price" style="width: 5%;">Tổng tiền</th>
                                            <th class="price" style="width: 5%;">Số Tiền phải cọc</th>
                                            <th class="price" style="width: 5%;">Tiền đã cọc</th>
                                            <th class="date" style="width: 5%;">Ngày đặt hàng</th>
                                            <th class="date" style="width: 5%;">Ngày phát hàng DK</th>
                                            <th class="status">Lịch sử đơn hàng</th>
                                            <th class="status" style="width: 5%;">Trạng thái đơn hàng</th>
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
            margin-right: 5px;
            line-height: 30px;
            margin-bottom: 10px;
        }

        .table-panel .table-panel-main table tr:nth-child(odd) {
            background-color: #fafafa;
        }

        .viewmore-orderlist {
            background-color: #2178bf;
            color: #fff;
            border: 1px solid transparent;
            padding: 5px;
            float: left;
            width: 100%;
            margin-bottom: 5px;
            text-align: center;
        }

            .viewmore-orderlist:hover {
                background-color: #195f98;
                color: #fff;
            }

        .complain-orderlist {
            background-color: #ff3e2b;
            color: #fff;
            border: 1px solid transparent;
            padding: 5px;
            float: left;
            width: 100%;
            margin-bottom: 5px;
            text-align: center;
        }

            .complain-orderlist:hover {
                background-color: #e63827;
                color: #fff;
            }

        [type="checkbox"] + span:not(.lever) {
            position: relative;
            padding-left: 35px;
            cursor: pointer;
            display: inline-block;
            height: 25px;
            line-height: 25px;
            font-size: 1rem;
            user-select: none;
        }
    </style>
    <asp:HiddenField ID="hdfListOrder" runat="server" />
    <asp:HiddenField runat="server" ID="hdfShowDep" Value="0" />
    <asp:HiddenField ID="hdfAmount" runat="server" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:HiddenField ID="hdfAccountAmount" runat="server" />
    <asp:HiddenField ID="hdfTotalLeft" runat="server" />
    <asp:HiddenField ID="hdfTotalLeftPay" runat="server" />
    <asp:HiddenField ID="hdfListID7" runat="server" />
    <asp:HiddenField ID="hdfList7Count" runat="server" />
    <asp:HiddenField ID="hdfInfouser" runat="server" />
    <asp:Button ID="btnDeposit" runat="server" OnClick="btnDeposit_Click" Style="display: none" />
    <asp:Button ID="btnOrderSame" runat="server" OnClick="btnOrderSame_Click" Style="display: none" />
    <asp:Button ID="btnDepositSelected1" runat="server" OnClick="btnDepositSelected1_Click" Style="display: none" UseSubmitBehavior="false" />
    <asp:Button ID="btnCancelOrder" runat="server" OnClick="btnCancelOrder_Click" Style="display: none" />
    <asp:HiddenField ID="hdfListOrderID" runat="server" />
    <asp:Button ID="btnDepositAll" runat="server" OnClick="btnDepositAll_Click" Style="display: none" UseSubmitBehavior="false" />
    <asp:Button ID="btnPayAll" runat="server" OnClick="btnPayAll_Click" Style="display: none" UseSubmitBehavior="false" />

    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function depositAll() {
                var c = confirm('Bạn muốn đặt cọc tất cả đơn hàng?');
                if (c == true) {
                    $("#<%=btnDepositAll.ClientID%>").click();
                }
            }
            function payAll() {
                var c = confirm('Bạn muốn thanh toán tất cả đơn hàng?');
                if (c == true) {
                    $("#<%=btnPayAll.ClientID%>").click();
                }
            }

            function cancelOrder(id) {
                var c = confirm("Bạn muốn hủy đơn hàng: " + id + "?");
                if (c == true) {
                    $("#<%=hdfOrderID.ClientID%>").val(id);
                    $("#<%=btnCancelOrder.ClientID%>").click();
                }
            }
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
            function keyclose_ms1(e) {
                if (e.keyCode == 27) {
                    close_popup_ms1();
                }
            }
            function close_popup_ms1() {
                $("#pupip_home").animate({ "opacity": 0 }, 400);
                $("#bg_popup_home").animate({ "opacity": 0 }, 400);
                setTimeout(function () {
                    $("#pupip_home").remove();
                    $(".zoomContainer").remove();
                    $("#bg_popup_home").remove();
                    $('body').css('overflow', 'auto').attr('onkeydown', '');
                }, 500);
            }
            function requestall() {
                var count = $("#<%=hdfList7Count.ClientID%>").val();
                var r = confirm("Bạn muốn yêu cầu giao " + count + " đơn hàng đã chọn!");
                if (r == true) {
                    var acw = $("#<%=hdfAccountAmount.ClientID%>").val();
                    var left = $("#<%=hdfTotalLeft.ClientID%>").val();
                    var leftpay = $("#<%=hdfTotalLeftPay.ClientID%>").val();
                    var infolist = $("#<%=hdfInfouser.ClientID%>").val();
                    var infostr = infolist.split('|');

                    var obj = $('form');
                    $(obj).css('overflow', 'hidden');
                    $(obj).attr('onkeydown', 'keyclose_ms(event)');
                    var bg = "<div id='bg_popup_home'></div>";
                    var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                        "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
                    fr += "<div class=\"popup_header\">Yêu cầu giao hàng";
                    fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms1()' class='close_message'></a>";
                    fr += "</div>";
                    fr += "     <div class=\"changeavatar\">";
                    fr += "         <div class=\"content1\">";
                    fr += "             <div class=\"form-request\">";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                Tổng tiền hàng cần thanh toán để lấy hàng đã nhận về kho đích: <span style=\"font-size:15px;font-weight:bold\">" + leftpay + "</span> <br/>";
                    fr += "		                Số dư hiện tại: <span style=\"font-size:15px;font-weight:bold\">" + acw + "</span><br/>";
                    fr += "		                Số dư sau khi xuất kho: <span style=\"font-size:15px;font-weight:bold\">" + left + "</span><br/>";
                    fr += "	                </div>";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                <label class=\"r-lable\">Hình thức thanh toán</label>";
                    fr += "		                <select class=\"r-pttt\">";
                    fr += "		                    <option value=\"0\">--Chọn hình thức thanh toán--</option>";
                    fr += "		                    <option value=\"1\">Chuyển khoản</option>";
                    fr += "		                    <option value=\"2\">Thanh toán bằng tiền mặt khi nhận hàng</option>";
                    fr += "		                </select>";
                    fr += "		                <label class=\"r-error r-pttt-error\"></label>";
                    fr += "	                </div>";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                <label class=\"r-lable\">Phương thức nhận hàng</label>";
                    fr += "		                <select class=\"r-ptnh\">";
                    fr += "		                    <option value=\"0\">--Chọn phương thức nhận hàng--</option>";
                    fr += "		                    <option value=\"1\">Nhận hàng trực tiếp tại kho (kho xếp hàng ra trước)</option>";
                    fr += "		                    <option value=\"2\">Giao hàng tận nhà (Áp dụng với khách Hà Nội và Hồ Chí Minh)</option>";
                    fr += "		                    <option value=\"3\">Phương thức khác (Khách ghi chú cụ thể bên dưới)</option>";
                    fr += "		                </select>";
                    fr += "		                <label class=\"r-error r-ptnh-error\"></label>";
                    fr += "	                </div>";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                <label class=\"r-lable\">Ghi chú</label>";
                    fr += "		                <input class=\"f-control request-txt r-note\">";
                    fr += "	                </div>";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                <label class=\"r-lable\">Họ tên người nhận</label>";
                    fr += "		                <input class=\"f-control request-txt r-fullname\" value=\"" + infostr[0] + "\">";
                    fr += "		                <label class=\"r-error r-fullname-error\"></label>";
                    fr += "	                </div>";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                <label class=\"r-lable\">Email</label>";
                    fr += "		                <input class=\"f-control request-txt r-email\" value=\"" + infostr[1] + "\">";
                    fr += "		                <label class=\"r-error r-email-error\"></label>";
                    fr += "	                </div>";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                <label class=\"r-lable\">Số ĐT người nhận</label>";
                    fr += "		                <input class=\"f-control request-txt r-phone\" value=\"" + infostr[2] + "\">";
                    fr += "		                <label class=\"r-error r-phone-error\"></label>";
                    fr += "	                </div>";
                    fr += "	                <div class=\"form-request-row\">";
                    fr += "		                <label class=\"r-lable\">Địa chỉ</label>";
                    fr += "		                <input class=\"f-control request-txt r-address\" value=\"" + infostr[3] + "\">";
                    fr += "		                <label class=\"r-error r-address-error\"></label>";
                    fr += "	                </div>";
                    fr += "             </div>";
                    fr += "         </div>";
                    fr += "         <div class=\"content2\">";
                    fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms1()'>Đóng</a>";
                    fr += "             <a href=\"javascript:;\" class=\"btn btn-close-full\" onclick=\"sendrequest($(this))\">Gửi yêu cầu</a>";
                    fr += "         </div>";
                    fr += "     </div>";
                    fr += "<div class=\"popup_footer\">";
                    //fr += "<span class=\"float-right\">" + email + "</span>";
                    fr += "</div>";
                    fr += "   </div>";
                    fr += "</div>";
                    $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                    $(fr).appendTo($(obj));
                    setTimeout(function () {
                        $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                        $("#bg_popup").attr("onclick", "close_popup_ms()");
                    }, 1000);
                } else {
                }
            }
            function sendrequest(obj) {
                var pttt = $(".r-pttt").val();
                var ptnh = $(".r-ptnh").val();
                var fullname = $(".r-fullname").val();
                var email = $(".r-email").val();
                var phone = $(".r-phone").val();
                var address = $(".r-address").val();
                var note = $(".r-note").val();
                var listID = $("#<%=hdfListID7.ClientID%>").val();
                var checkbo = false;
                if (pttt == "0") {
                    $(".r-pttt-error").html("Vui lòng chọn Hình thức thanh toán");
                    checkbo = true;
                }
                else {
                    $(".r-pttt-error").html("");
                }
                if (ptnh == "0") {
                    $(".r-ptnh-error").html("Vui lòng chọn Phương thức nhận hàng");
                    checkbo = true;
                }
                else {
                    $(".r-ptnh-error").html("");
                }
                if (isEmpty(fullname)) {
                    $(".r-fullname-error").html("Vui lòng nhập họ tên");
                    checkbo = true;
                }
                else {
                    $(".r-fullname-error").html("");
                }
                if (isEmpty(email)) {
                    $(".r-email-error").html("Vui lòng nhập Email");
                    checkbo = true;
                }
                else {
                    $(".r-email-error").html("");
                }
                if (isEmpty(phone)) {
                    $(".r-phone-error").html("Vui lòng nhập số ĐT");
                    checkbo = true;
                }
                else {
                    $(".r-phone-error").html("");
                }
                if (isEmpty(address)) {
                    $(".r-address-error").html("Vui lòng địa chỉ");
                    checkbo = true;
                }
                else {
                    $(".r-address-error").html("");
                }

                if (!isEmpty(fullname) && !isEmpty(email) && !isEmpty(phone) && !isEmpty(address)) {
                    checkbo == false;
                }

                if (checkbo == true) {
                    return false;
                }
                else {
                    obj.removeAttr("onclick");
                    $.ajax({
                        type: "POST",
                        url: "/danh-sach-don-hang.aspx/sendrequest",
                        data: "{PTTT:'" + pttt + "',PTNH:'" + ptnh + "',FullName:'" + fullname + "',Email:'" + email + "',Phone:'"
                            + phone + "',Note:'" + note + "',Address:'" + address + "',ListID:'" + listID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            close_popup_ms();
                            var d = msg.d;
                            if (d == "ok") {
                                swal
                                    (
                                        {
                                            title: 'Thông báo',
                                            text: 'Tạo lệnh yêu cầu giao thành công',
                                            type: 'success'
                                        },
                                        function () { window.location.replace(window.location.href); }
                                    );
                            }
                            else if (d == "saidonhang") {
                                alert('Đơn hàng không phải của bạn.');
                            }
                            else if (d == "khongtimthaydonhang") {
                                alert('Không tìm thấy đơn hàng.');
                            }
                            else if (d == "notuser") {
                                alert('Không tìm thấy người dùng.');
                            }

                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            alert('lỗi');
                        }
                    });
                }
            }
            function requestall1() {
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
            function OrderSame(orderID) {
                var c = confirm('Bạn muốn đặt đơn hàng tương tự đơn : ' + orderID);
                if (c == true) {
                    $("#<%=hdfOrderID.ClientID%>").val(orderID);
                    $("#<%=btnOrderSame.ClientID%>").click();
                }
            }

            function depositAllOrder() {
                var c = confirm('Bạn muốn đặt cọc đơn tất cả?');
                if (c == true) {
                    $("#<%=btnDepositAll.ClientID%>").click();
                }
            }


            function CheckDepAll(ID, TotalPrice) {
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-don-hang.aspx/CheckDepAll",
                    data: "{MainOrderID:'" + ID + "',TotalPrice:'" + TotalPrice + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        if (data.length > 0) {
                            var totalDep = 0;
                            for (var i = 0; i < data.length; i++) {
                                totalDep += data[i].TotalDeposit;
                            }
                            $('.noti-item.deposit').find('.count').text(data.length);
                            $(".totaldepositselected").html(formatThousands(totalDep) + " VNĐ")
                            $('body').find('.result-select').addClass('show');
                            $('.noti-item.deposit').show();
                        }
                        else {
                            var count = $('.noti-item.checkout').find('.count').text();
                            var count1 = $('.noti-item.GiaoHang').find('.count').text();
                            $('.noti-item.deposit').find('.count').text("0");
                            $(".totaldepositselected").html("0 VNĐ")
                            if (count == 0 && count1 == 0) {
                                $('body').find('.result-select').removeClass('show');
                            }
                            $('.noti-item.deposit').hide();
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert(errorthrow);
                    }
                });
            }

            function depositSelected() {
                var c = confirm('Bạn muốn đặt cọc tất cả đơn hàng đã chọn?');
                if (c == true) {
                    $("#<%=btnDepositSelected1.ClientID%>").click();
                }
            }


            $(document).ready(function () {
                var table = $('.order-list-info .table');
                $('tr[data-action="other"] td label').hide();
                var listCb = table.find('tbody tr td label input[type="checkbox"]');

                var showDep = $("#<%=hdfShowDep.ClientID%>").val();
                if (showDep != 0) {
                    var data = JSON.parse(showDep);
                    var totalDep = 0;
                    for (var i = 0; i < data.length; i++) {
                        totalDep += data[i].TotalDeposit;
                    }
                    $('.noti-item.deposit').find('.count').text(data.length);
                    $(".totaldepositselected").html(formatThousands(totalDep) + " VNĐ")
                    $('body').find('.result-select').addClass('show');
                    $('.noti-item.deposit').show();
                }



                $('.collapase_header .collapse').on('click', function () {
                    $(this).toggleClass('active');
                    $(this).parent().next().slideToggle();

                });

            });

            function isEmpty(str) {
                return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            }

            var formatThousands = function (n, dp) {
                var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
                while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
                return s.substr(0, i + 3) + r + (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
            };

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

        .form-request {
            float: left;
            width: 100%;
        }

        .form-request-row {
            float: left;
            width: 100%;
            margin: 0;
        }

        .r-lable {
            float: left;
            width: 100%;
            font-size: 14px;
            margin: 5px 0;
        }

        .r-error {
            float: left;
            width: 100%;
            font-size: 12px;
            color: red;
            margin-top: 10px;
        }

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
            top: 10%;
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
            height: 570px;
            overflow-y: scroll;
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
