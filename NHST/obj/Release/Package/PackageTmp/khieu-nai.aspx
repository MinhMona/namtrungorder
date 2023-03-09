<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="khieu-nai.aspx.cs" Inherits="NHST.khieu_nai1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                <h4 class="sec__title center-txt">Khiếu nại</h4>
                <div class="container">
                    <aside class="filters">
                        <ul style="display:flex">
                            <li class="input-field col s10 l2" style="width:80%">
                                <asp:TextBox ID="search_id_new" placeholder="Mã Shop" runat="server" CssClass="search_name"></asp:TextBox>
                            </li>
                            <li class="input-field col s12 l2" style="width:20%; margin-left:5px" >
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover" OnClick="btnSearch_Click" Text="LỌC TÌM KIẾM" />
                            </li>
                        </ul>
                    </aside>
                </div>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <div class="step-income">
                                <table class="customer-table mar-top1 full-width normal-table">
                                    <tr>
                                        <th>Ngày</th>
                                        <th>Mã Shop</th>
                                        <th>Tiền bồi thường</th>
                                        <th>Nội dung</th>
                                        <th>Trạng thái</th>
                                        <th>Chi tiết KN</th>
                                    </tr>
                                    <tbody>
                                        <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                    </tbody>
                                </table>
                                <div class="pagination">
                                    <%this.DisplayHtmlStringPaging1();%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <script>
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_id_new.ClientID%>').val());
                $('#<%=btnSearch.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {
            console.log('dkm ngon');
            console.log($('#<%=search_id_new.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })

    </script>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Nạp tiền</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
</asp:Content>
