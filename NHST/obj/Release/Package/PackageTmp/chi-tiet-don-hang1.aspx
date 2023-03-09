<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang1.aspx.cs" Inherits="NHST.chi_tiet_don_hang" %>

<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        dl dt {
            float: left;
            width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
            color: #3e3e3e;
            margin-right: 5px;
        }

        .full-width {
            width: 100% !important;
        }

        .order-panels {
            float: left;
            width: 100%;
            display: -ms-flexbox;
            display: -webkit-box;
            display: flex;
            -ms-flex-direction: row;
            -webkit-box-orient: horizontal;
            -webkit-box-direction: normal;
            flex-direction: row;
            -ms-flex-wrap: nowrap;
            flex-wrap: nowrap;
            -ms-flex-pack: justify;
            -webkit-box-pack: justify;
            justify-content: space-between;
            -ms-flex-line-pack: stretch;
            align-content: stretch;
            -ms-flex-align: stretch;
            -webkit-box-align: stretch;
            align-items: stretch;
        }

        @media (max-width: 768px) {
            .order-panels {
                -ms-flex-direction: column;
                -webkit-box-orient: vertical;
                -webkit-box-direction: normal;
                flex-direction: column;
            }
        }

        .order-panel {
            float: left;
            width: 100%;
            margin-bottom: 30px;
            padding: 10px;
            line-height: 1.6;
            box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.2);
        }

            .order-panel .title {
                text-transform: uppercase;
                color: #2b2e4a;
                font-weight: bold;
                font-size: 16px;
                padding-bottom: 5px;
                border-bottom: solid 1px #fff;
                margin-bottom: 10px;
            }

            .order-panel .cont {
                display: block;
                width: 100%;
            }

            .order-panel .bottom {
                border-top: solid 1px #fff;
                padding-top: 10px;
                margin-top: 10px;
                text-align: right;
            }

                .order-panel .bottom .btn {
                    text-transform: uppercase;
                    font-weight: bold;
                }

            .order-panel dl {
                float: none;
            }

                .order-panel dl dt {
                    width: 60%;
                }

            .order-panel textarea {
                width: 100%;
                height: 60px;
            }

            .order-panel table.ratting-tb {
                width: 100%;
            }

                .order-panel table.ratting-tb th {
                    height: 40px;
                    font-weight: bold;
                    border-bottom: solid 2px #ebebeb;
                    vertical-align: middle;
                }

                .order-panel table.ratting-tb td {
                    height: 40px;
                    vertical-align: middle;
                }

            .order-panel table.tb-product {
                width: 100%;
            }

                .order-panel table.tb-product caption {
                    color: #ff7e67;
                    text-transform: uppercase;
                    font-size: 16px;
                    text-align: left;
                    line-height: 50px;
                    font-weight: bold;
                }

                .order-panel table.tb-product th {
                    vertical-align: middle;
                    background-color: #f8f8f8;
                }

                .order-panel table.tb-product td {
                    vertical-align: top;
                }

                .order-panel table.tb-product td, .order-panel table.tb-product th {
                    padding: 10px;
                    border: solid 1px #ebebeb;
                }

                .order-panel table.tb-product .qty input {
                    display: block;
                    margin: 0 auto;
                    text-align: center;
                }

            .order-panel .table-wrap + .table-wrap {
                margin-top: 20px;
            }

            .order-panel .table-wrap {
                width: 100%;
                overflow: auto;
            }

                .order-panel .table-wrap table {
                    min-width: 600px;
                }

            .order-panel + .order-panel {
                margin-left: 30px;
            }

            .order-panel.greypn {
                background-color: #eeeeee;
            }

        @media (max-width: 768px) {
            .order-panel + .order-panel {
                margin-left: 0;
            }
        }

        .total-order-block {
            float: left;
            width: 100%;
        }

            .total-order-block .order-panel {
                width: calc(50% - 15px);
                margin-right: 30px;
                padding: 20px 30px;
            }

                .total-order-block .order-panel + .order-panel {
                    margin-right: 0;
                    margin-left: 0;
                }

                .total-order-block .order-panel dl dt {
                    text-align: right;
                    float: right;
                    color: inherit;
                    font-weight: bold;
                }

                .total-order-block .order-panel dl dd {
                    text-align: right;
                }

        @media (max-width: 768px) {
            .total-order-block .order-panel {
                width: 100%;
            }
        }

        dl dd {
            display: block;
            padding-left: 100px;
        }

            .clear:before, dl dd:before, .thumb-blog-ul li:before, .primary-form:before, .panel .panel-heading:before, .panel .panel-body:before {
                display: table;
                content: " ";
            }

            .clear:after, dl dd:after, .thumb-blog-ul li:after, .primary-form:after, .panel .panel-heading:after, .panel .panel-body:after {
                content: "";
                display: table;
                clear: both;
            }

        .title-fee {
            float: left;
            width: 100%;
            border-bottom: solid 1px #ccc;
            font-size: 20px;
            margin: 20px 0;
            color: #000;
        }

        .brand-name-product {
            float: left;
            width: 100%;
            margin: 10px 0 40px 0;
        }

            .brand-name-product input {
                float: left;
                width: 100%;
            }

        .table-price-sec .tbp-top {
            float: left;
            width: 100%;
            margin-bottom: 30px;
        }

        .table-panel .table-panel-header {
            float: left;
            width: 100%;
            color: white;
            background-color: #ff7e67;
            padding: 25px;
        }

            .table-panel .table-panel-header .title {
                text-transform: uppercase;
                float: left;
                line-height: 20px;
                padding: 5px 0;
            }

            .table-panel .table-panel-header .delivery-opt {
                float: right;
            }

                .table-panel .table-panel-header .delivery-opt label {
                    float: left;
                    cursor: pointer;
                    margin-right: 50px;
                }

                    .table-panel .table-panel-header .delivery-opt label:last-child {
                        margin-right: 0;
                    }

                    .table-panel .table-panel-header .delivery-opt label input[type="checkbox"] {
                        display: none;
                    }

                    .table-panel .table-panel-header .delivery-opt label span {
                        display: inline-block;
                        vertical-align: middle;
                    }

                    .table-panel .table-panel-header .delivery-opt label .ip-avata {
                        border-radius: 50%;
                        -webkit-border-radius: 50%;
                        width: 30px;
                        height: 30px;
                        margin-right: 20px;
                        background-color: white;
                        position: relative;
                    }

                        .table-panel .table-panel-header .delivery-opt label .ip-avata:before {
                            content: '';
                            display: block;
                            margin: 0 auto;
                            -webkit-transition: all .3s ease-in-out;
                            transition: all .3s ease-in-out;
                            width: 20px;
                            height: 20px;
                            margin-top: 5px;
                            border-radius: 50%;
                            -webkit-border-radius: 50%;
                            background-color: #ff7e67;
                            transform: scale(0);
                            -webkit-transform: scale(0);
                            -moz-transform: scale(0);
                            -ms-transform: scale(0);
                            -o-transform: scale(0);
                        }

                    .table-panel .table-panel-header .delivery-opt label input[type="checkbox"]:checked + .ip-avata:before {
                        transform: none;
                        -webkit-transform: none;
                        -moz-transform: none;
                        -ms-transform: none;
                        -o-transform: none;
                    }

        .table-panel .table-panel-total {
            float: left;
            width: 33.3333333333333%;
            padding: 0 30px;
        }

            .table-panel .table-panel-total table {
                width: 100%;
            }

                .table-panel .table-panel-total table tr {
                    border-bottom: solid 1px #ebebeb;
                }

                    .table-panel .table-panel-total table tr:last-child {
                        border-bottom: none;
                    }

                .table-panel .table-panel-total table td {
                    height: 80px;
                    vertical-align: middle;
                }

                    .table-panel .table-panel-total table td:last-child {
                        text-align: right;
                        font-size: 18px;
                    }

            .table-panel .table-panel-total .note-block {
                float: left;
                width: 100%;
            }

                .table-panel .table-panel-total .note-block textarea.note {
                    float: left;
                    width: 100%;
                    resize: none;
                    height: 90px;
                    padding: 15px 20px;
                }

            .table-panel .table-panel-total .btn-wrap {
                float: left;
                width: 100%;
                padding-top: 30px;
            }

                .table-panel .table-panel-total .btn-wrap .btn {
                    display: block;
                    font-weight: bold;
                    width: 100%;
                    text-align: center;
                    font-size: 12px;
                }

        @media (max-width: 480px) {
            .table-panel .table-panel-header {
                padding: 15px;
            }

            .table-panel .delivery-opt label {
                margin-bottom: 10px;
            }

            .table-panel .table-panel-main {
                width: 100%;
                overflow: auto;
            }

                .table-panel .table-panel-main table {
                    width: 600px;
                }

            .table-panel .table-panel-total {
                width: 100%;
                padding: 0;
            }
        }

        .table-panel-main {
            float: left;
            width: 66.66666666666666%;
        }

            .table-panel-main table {
                float: left;
                width: 100%;
                position: relative;
                border: solid 1px #ebebeb;
                border-top: none;
            }

                .table-panel-main table th {
                    height: 80px;
                    vertical-align: middle;
                    background-color: #f8f8f8;
                    font-weight: bold;
                    padding: 0 25px;
                }

                .table-panel-main table td {
                    padding: 20px 0;
                    vertical-align: middle;
                }

                    .table-panel-main table td .checklb .ip-avata {
                        border-color: #ececec;
                    }

                    .table-panel-main table td.hover-td {
                        padding: 0;
                    }

                .table-panel-main table .checklb .ip-avata {
                    margin: 0;
                }

                .table-panel-main table .check {
                    padding: 0;
                    padding-left: 25px;
                }

                .table-panel-main table .img {
                    padding: 0 15px;
                }

                .table-panel-main table .qty {
                    padding: 0 15px;
                }

                    .table-panel-main table .qty input {
                        width: 70px;
                    }

                .table-panel-main table .price {
                    padding: 0 15px;
                }

                    .table-panel-main table .price p:last-child {
                        color: #777777;
                    }

                .table-panel-main table .total {
                    padding-right: 25px;
                }

                    .table-panel-main table .total p:last-child {
                        color: #777777;
                    }

                .table-panel-main table textarea.note {
                    width: 100%;
                    resize: none;
                    height: 90px;
                    padding: 15px 20px;
                }

                .table-panel-main table .hover-block {
                    display: none;
                }

                .table-panel-main table .note-td {
                    padding-right: 25px;
                    padding-left: 25px;
                    border-bottom: solid 1px #ebebeb;
                }

                .table-panel-main table .hover-tr {
                    display: none;
                }

                .table-panel-main table:hover {
                    border-color: #ff7e67;
                }

                    .table-panel-main table:hover .hover-tr {
                        display: table-row;
                    }

                    .table-panel-main table:hover .hover-block {
                        position: absolute;
                        top: 50%;
                        right: -15px;
                        display: block;
                        font-size: 18px;
                        line-height: 30px;
                        width: 30px;
                        height: 30px;
                        text-align: center;
                        margin-top: -15px;
                        border: solid 1px #ebebeb;
                        background-color: white;
                    }

                        .table-panel-main table:hover .hover-block a {
                            display: block;
                        }

        .table-price-total {
            float: left;
            width: 100%;
            padding: 30px 25px;
            margin-top: 30px;
            border-top: solid 1px #ebebeb;
        }

            .table-price-total .order-btn {
                padding-right: 50px;
                padding-left: 50px;
                display: inline-block;
                vertical-align: middle;
                font-weight: bold;
                font-size: 12px;
            }

            .table-price-total .final-total {
                display: inline-block;
                vertical-align: middle;
                margin-right: 25px;
            }

                .table-price-total .final-total strong.hl-txt {
                    font-size: 30px;
                }

        .thumb-product {
            display: table;
            width: 100%;
        }

            .thumb-product .pd-img {
                float: none;
                display: table-cell;
                vertical-align: middle;
                width: 70px;
                margin-right: 20px;
                position: relative;
            }

                .thumb-product .pd-img .badge {
                    position: absolute;
                    right: 0;
                    top: 0;
                    z-index: 1;
                    width: 20px;
                    text-align: center;
                    height: 20px;
                    line-height: 20px;
                    background-color: #959595;
                    color: white;
                    font-weight: bold;
                    border-radius: 50%;
                    margin-right: -10px;
                    margin-top: -10px;
                }

            .thumb-product .info {
                float: none;
                display: table-cell;
                vertical-align: middle;
            }

        .checklb {
            cursor: pointer;
            float: left;
        }

            .checklb input {
                display: none;
            }

            .checklb span {
                display: inline-block;
                vertical-align: middle;
            }

            .checklb .ip-avata {
                border-radius: 50%;
                -webkit-border-radius: 50%;
                width: 30px;
                height: 30px;
                border: solid 1px #f8f8f8;
                margin-right: 10px;
                background-color: white;
                position: relative;
            }

                .checklb .ip-avata:before {
                    content: '';
                    display: block;
                    margin: 0 auto;
                    -webkit-transition: all .3s ease-in-out;
                    transition: all .3s ease-in-out;
                    width: 20px;
                    height: 20px;
                    margin-top: 4px;
                    border-radius: 50%;
                    -webkit-border-radius: 50%;
                    background-color: #ff7e67;
                    transform: scale(0);
                    -webkit-transform: scale(0);
                    -moz-transform: scale(0);
                    -ms-transform: scale(0);
                    -o-transform: scale(0);
                }

            .checklb input:checked + .ip-avata:before {
                transform: none;
                -webkit-transform: none;
                -moz-transform: none;
                -ms-transform: none;
                -o-transform: none;
            }

            .checklb + .checklb {
                margin-left: 20px;
            }

        .radiolb {
            cursor: pointer;
            float: left;
        }

            .radiolb input {
                display: none;
            }

            .radiolb span {
                display: inline-block;
                vertical-align: middle;
            }

            .radiolb .ip-avata {
                border-radius: 0;
                -webkit-border-radius: 0;
                width: 20px;
                height: 20px;
                border: solid 1px #ebebeb;
                margin-right: 10px;
                background-color: white;
                position: relative;
            }

                .radiolb .ip-avata:before {
                    content: '';
                    display: block;
                    margin: 0 auto;
                    -webkit-transition: all .3s ease-in-out;
                    transition: all .3s ease-in-out;
                    width: 10px;
                    height: 10px;
                    margin-top: 4px;
                    border-radius: 0;
                    -webkit-border-radius: 0;
                    background-color: #ff7e67;
                    transform: scale(0);
                    -webkit-transform: scale(0);
                    -moz-transform: scale(0);
                    -ms-transform: scale(0);
                    -o-transform: scale(0);
                }

            .radiolb input:checked + .ip-avata:before {
                transform: none;
                -webkit-transform: none;
                -moz-transform: none;
                -ms-transform: none;
                -o-transform: none;
            }

            .radiolb + .radiolb {
                margin-left: 20px;
            }

        .dropdown {
            position: relative;
        }

        .left {
            float: left;
        }

        .right {
            float: right;
        }

        .table-price-sec .tbp-top .left {
            line-height: 20px;
            padding: 10px 0;
        }

        .hl-txt {
            color: #ff7e67;
        }

        input[type=checkbox] {
            height: auto;
            width: auto;
        }

        *, *:after, *:before {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
            padding: 0;
            margin: 0;
        }

        .shop-info {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #1b75b9;
            color: #959595;
            font-weight: bold;
            padding: 0 15px;
            color: #fff;
            float: left;
        }

        .font-size-20 {
            font-size: 16px;
        }

        .tbl-subtotal tr {
            border-bottom: 1px dashed #999;
            width: 100%;
            float: left;
            padding-bottom: 5px;
        }

        .float-right {
            float: right;
        }

        .float-left {
            float: left;
        }

        .color-orange {
            color: orange;
        }

        b, strong, .b {
            font-weight: bold;
        }

        .comment {
            float: left;
            width: 100%;
        }

        .green {
            color: green;
        }

        .comment_content {
            min-height: 0px;
            text-align: left;
            vertical-align: top;
            border: 1px solid #E3E3E3;
            background: #FFFFF0;
            color: #666666;
            padding: 10px;
            max-height: 300px;
            overflow-y: scroll;
        }

        .user-comment {
            color: black;
            font-weight: bold;
        }

        .font-size-10 {
            font-size: 10px;
        }

        .comment-text {
            float: left;
            width: 85%;
            padding: 5px 10px;
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

        #sendnotecomment {
            padding: 0px 25px;
            float: right;
        }
  
        .shop-info {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #1b75b9;
            color: #959595;
            font-weight: bold;
            padding: 0 15px;
            color: #fff;
            float: left;
        }

        .font-size-20 {
            font-size: 16px;
        }

        .tbl-subtotal tr {
            border-bottom: 1px dashed #999;
            width: 100%;
            float: left;
            padding-bottom: 5px;
        }

        .float-right {
            float: right;
        }

        .float-left {
            float: left;
        }

        .color-orange {
            color: orange;
        }

        b, strong, .b {
            font-weight: bold;
        }

        .comment {
            float: left;
            width: 100%;
        }

        .green {
            color: green;
        }

        .comment_content {
            min-height: 0px;
            text-align: left;
            vertical-align: top;
            border: 1px solid #E3E3E3;
            background: #FFFFF0;
            color: #666666;
            padding: 10px;
            max-height: 300px;
            overflow-y: scroll;
        }

        .user-comment {
            color: black;
            font-weight: bold;
        }

        .font-size-10 {
            font-size: 10px;
        }

        .comment-text {
            float: left;
            width: 85%;
            padding: 5px 10px;
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

        #sendnotecomment {
            padding: 0px 25px;
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Chi tiết đơn hàng</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <h2 class="content-title">Chi tiết đơn hàng</h2>
            <div class="order-tool clearfix">               
                <div class="order-panels mar-bot2 color-white">
                    <asp:Label ID="ltr_info" runat="server" Visible="false" CssClass="inforshow"></asp:Label>
                </div>
                <div class="order-panels mar-bot2">
                    <a href="javascript:;" onclick="printDiv()" class="btn pill-btn primary-btn admin-btn">In đơn hàng</a>
                </div>
                <div class="order-panels" style="display: none">
                    <asp:Literal ID="ltr_OrderFee_UserInfo" runat="server"></asp:Literal>
                </div>

                <div class="order-panels">
                    <div class="order-panel">
                        <div class="title" style="text-align: center;">Thông tin người đặt</div>
                        <div class="form-row">
                            <div class="lb">Họ tên</div>
                            <asp:TextBox ID="txt_Fullname" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <div class="lb">Địa chỉ</div>
                            <asp:TextBox ID="txt_Address" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <div class="lb">Email</div>
                            <asp:TextBox ID="txt_Email" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <div class="lb">Số điện thoại</div>
                            <asp:TextBox ID="txt_Phone" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="order-panel">
                        <div class="title" style="text-align: center;">Chat với chúng tôi</div>
                        <asp:Literal ID="ltrComment" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="order-panel print3">
                    <div class="title">Danh sách sản phẩm</div>
                    <div class="cont clear">
                        <div class="tbl-product-wrap">
                            <asp:Literal ID="ltrshopinfo" runat="server" EnableViewState="false"></asp:Literal>
                            <table class="tb-product">
                                <tr>
                                    <th class="pro">STT</th>
                                    <th class="pro">Sản phẩm</th>
                                    <th class="pro">Thuộc tính</th>
                                    <th class="qty">Số lượng</th>
                                    <th class="price">Đơn giá (¥)</th>
                                    <th class="price">Đơn giá (VNĐ)</th>
                                    <th class="price">Ghi chú riêng sản phẩm</th>
                                    <th class="price">Trạng thái</th>
                                </tr>
                                <asp:Literal ID="ltrProducts" runat="server"></asp:Literal>
                            </table>
                        </div>
                        <asp:Panel ID="pn" runat="server" Visible="false">
                            <div class="order-panels">
                                <a class="btn pill-btn primary-btn" href="javascript:;" onclick="cancelOrder()">Hủy đơn hàng</a>
                                <asp:Button ID="btn_cancel" runat="server" CssClass="btn pill-btn primary-btn" CausesValidation="false" Text="Hủy đơn hàng" Style="display: none;" OnClick="btn_cancel_Click" />
                                <asp:Literal ID="ltrbtndeposit" runat="server"></asp:Literal>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnthanhtoan" runat="server" Visible="false">
                            <div class="order-panels">
                                <a class="btn pill-btn primary-btn" href="javascript:;" onclick="payallorder()">Thanh toán</a>
                                <asp:Button ID="btnPayAll" runat="server" CssClass="btn pill-btn primary-btn" CausesValidation="false" Text="Thanh toán" Style="display: none;" OnClick="btnPayAll_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="order-panels" style="display: none">
                    <asp:Literal ID="ltrOrderFee" runat="server"></asp:Literal>
                </div>
                <div class="order-panel print5">
                    <div class="title">Lịch sử thanh toán </div>
                    <div class="cont">
                        <table class="tb-product">
                            <tr>
                                <th class="pro">Ngày thanh toán</th>
                                <th class="pro">Loại thanh toán</th>
                                <th class="pro">Hình thức thanh toán</th>
                                <th class="qty">Số tiền</th>
                            </tr>
                            <asp:Repeater ID="rptPayment" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="pro">
                                            <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                        </td>
                                        <td class="pro">
                                            <%# PJUtils.ShowStatusPayHistory( Eval("Status").ToString().ToInt()) %>
                                        </td>
                                        <td class="pro">
                                            <%#Eval("Type").ToString() == "1"?"Trực tiếp":"Ví điện tử" %>
                                        </td>
                                        <td class="qty"><%#Eval("Amount","{0:N0}") %> VNĐ
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div id="printcontent" class="printdetail" style="display: none;">
        </div>
        <asp:HiddenField ID="hdfCommentText" runat="server" />
        <asp:HiddenField ID="hdfShopID" runat="server" />
        <asp:HiddenField ID="hdfOrderID" runat="server" />

    </section>
    <main id="main-wrap">
        <div class="sec order-detail-sec">
            <div class="all">
                <div class="main">
                    

                </div>
            </div>
        </div>
       
    </main>
    <asp:Button ID="btnPostComment" runat="server" Style="display: none;" OnClick="btnPostComment_Click" CausesValidation="false"/>
    <asp:Button ID="btnDeposit" runat="server" CssClass="btn pill-btn primary-btn" Style="display: none" CausesValidation="false" Text="Đặt cọc" OnClick="btnDeposit_Click" />
    <script type="text/javascript">
        function printDiv() {
            var html = "";

            $('link').each(function () { // find all <link tags that have
                if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                    html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';
                }
            });
            html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
            var w = window.open("", "print");
            if (w) { w.document.write(html); w.document.close() }
        }
        $(document).ready(function () {
            $(".print1").clone().appendTo(".printdetail");
            $(".print2").clone().appendTo(".printdetail");
            $(".print3").clone().appendTo(".printdetail");
            $(".print4").clone().appendTo(".printdetail");
            $(".print5").clone().appendTo(".printdetail");
        });
        function payallorder() {
            var r = confirm("Bạn muốn thanh toán đơn hàng này?");
            if (r == true) {
                $("#<%= btnPayAll.ClientID%>").click();
            }
            else {
            }
        }
        function cancelOrder() {
            var r = confirm("Bạn muốn hủy đơn hàng này?");
            if (r == true) {
                $("#<%= btn_cancel.ClientID%>").click();
            }
            else {
            }
        }
        function depositOrder() {
            var r = confirm("Bạn muốn đặt cọc đơn hàng này?");
            if (r == true) {
                $("#<%= btnDeposit.ClientID%>").click();
            }
            else {
            }
        }
        function PrintDiv() {
            var contents = document.getElementById("dvContents").innerHTML;
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title>DIV Contents</title>');
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
        }
        function postcomment(obj) {
            var parent = obj.parent();
            var commentext = parent.find(".comment-text").val();
            var shopid = obj.attr("order");
            if (commentext == "" || commentext == null) {
                alert("Vui lòng không để trống nội dung");
            }
            else {
                $("#<%= hdfCommentText.ClientID%>").val(commentext);
                $("#<%= hdfShopID.ClientID%>").val(shopid);               
                $("#<%= btnPostComment.ClientID%>").click();

            }
        }
    </script>
</asp:Content>
