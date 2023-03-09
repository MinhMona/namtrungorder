<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang.aspx.cs" Inherits="NHST.chi_tiet_don_hang1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
            background: #fff;
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

        .shop-info {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #ad0d12;
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
            height: 264px;
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
            line-height: 40px;
        }

        .shop-info {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #ad0d12;
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

            .user-comment.green {
                color: green;
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

        .width-60-per {
            width: 60%;
            float: left;
            margin-right: 50px;
        }

        .width-35-per {
            width: 35%;
            float: left;
        }

        .font-size-25 {
            font-size: 25px;
        }

       .comment_content img {
            max-width: 30%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Chi tiết đơn hàng</h4>
                <div class="primary-form">
                    <div class="waitting">
                        <div class="all">
                            <div class="wait__hd">
                                <asp:Literal ID="ltrstep" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div class="order-tool clearfix">
                        <div class="order-panels mar-bot2 color-white">
                            <asp:Label ID="ltr_info" runat="server" Visible="false" CssClass="inforshow"></asp:Label>
                        </div>
                        <div class="order-panels mar-bot2">
                            <a href="javascript:;" onclick="printDiv()" class="btn pill-btn primary-btn admin-btn main-btn hover btn-1">In đơn hàng</a>
                            <asp:Button runat="server" ID="btnExcel" Text="Xuất Excel" class="btn pill-btn primary-btn admin-btn main-btn hover btn-1" OnClick="btnExcel_Click" />
                            <asp:Literal ID="ltrRequestShip" runat="server" Visible="false"></asp:Literal>


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
                                <div class="comment_content" id="ContactCustomer">
                                    <asp:Literal ID="ltrComment" runat="server"></asp:Literal>
                                </div>
                                <div class="comment_action" style="padding-bottom: 4px; padding-top: 4px;">
                                    <label>
                                        <span>Thêm ảnh</span>
                                        <%-- <input style="float: left; border: none" id="images" multiple="" type="file" onchange="readFiles(this,'customercomment');">--%>
                                        <asp:FileUpload runat="server" ID="IMGUpLoadToUS" class="upload-img" type="file" AllowMultiple="true" title=""></asp:FileUpload>
                                    </label>
                                    <ul class="row-package customercomment"></ul>
                                </div>
                                <div class="comment_action" style="padding-bottom: 4px; padding-top: 4px;">
                                    <div class="preview-upload" id="imgUpload">
                                    </div>
                                    <input id="txtComment" class="comment-text" placeholder="Type message here.." />
                                    <a id="sendnotecomment" class="btn pill-btn primary-btn main-btn hover" href="javascript:;" onclick="SendMessage()" style="min-width: 10px;">Gửi</a>
                                </div>

                            </div>
                        </div>
                        <div class="order-panels">
                            <div class="order-panel">
                                <div class="title">Danh sách mã vận đơn</div>
                                <div class="cont clear">
                                    <div class="tbl-product-wrap">
                                        <table class="tb-product">
                                            <tr>
                                                <th class="pro">Mã vận đơn</th>
                                                <th class="pro">Cân thực</th>
                                                <th class="pro">Kích thước</th>
                                                <th class="pro">Cân quy đổi</th>
                                                <th class="pro">Cân tính tiền</th>
                                                <th class="pro">Trạng thái</th>
                                                 <th class="price">Ghi chú</th>
                                            </tr>
                                            <asp:Literal ID="ltrSmallPackages" runat="server"></asp:Literal>
                                        </table>
                                    </div>
                                </div>
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
                                        <a class="btn pill-btn primary-btn main-btn hover" href="javascript:;" onclick="cancelOrder()">Hủy đơn hàng</a>
                                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn pill-btn primary-btn main-btn hover" UseSubmitBehavior="false"
                                            CausesValidation="false" Text="Hủy đơn hàng" Style="display: none;" OnClick="btn_cancel_Click" />
                                        <asp:Literal ID="ltrbtndeposit" runat="server"></asp:Literal>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnthanhtoan" runat="server" Visible="false">
                                    <div class="order-panels">
                                        <a class="btn pill-btn primary-btn main-btn hover" href="javascript:;" onclick="payallorder()">Thanh toán</a>
                                        <asp:Button ID="btnPayAll" runat="server" CssClass="btn pill-btn primary-btn main-btn hover" UseSubmitBehavior="false"
                                            CausesValidation="false" Text="Thanh toán" Style="display: none;" OnClick="btnPayAll_Click" />
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
            </div>

        </section>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Chi tiết đơn hàng</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
    <asp:HiddenField runat="server" ID="hdfID" />
    <asp:Button ID="btnPostComment" runat="server" Style="display: none;" OnClick="btnPostComment_Click" CausesValidation="false" UseSubmitBehavior="false" />
    <asp:Button ID="btnDeposit" runat="server" CssClass="btn pill-btn primary-btn" Style="display: none" CausesValidation="false" UseSubmitBehavior="false" Text="Đặt cọc" OnClick="btnDeposit_Click" />
    <script src="/App_Themes/UserNew45/assets/js/custom/chat.js"></script>
    <style>
        .row-package {
            list-style: none;
        }

            .row-package li {
                position: relative;
                width: 40%;
                margin-top: 10px;
            }
    </style>
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
        function requestShip(id) {
            var obj = $('form');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup_home'></div>";
            var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
            fr += "<div class=\"popup_header\">Yêu cầu giao hàng";
            fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "</div>";
            fr += "     <div class=\"changeavatar\">";
            fr += "         <div class=\"content1\">";
            fr += "<div class=\"form-request\">";
            fr += "	<div class=\"form-request-row\">";
            fr += "		<label class=\"r-lable\">Họ tên người nhận</label>";
            fr += "		<input class=\"f-control request-txt r-fullname\" >";
            fr += "		<label class=\"r-error r-fullname-error\"></label>";
            fr += "	</div>";
            fr += "	<div class=\"form-request-row\">";
            fr += "		<label class=\"r-lable\">Email</label>";
            fr += "		<input class=\"f-control request-txt r-email\">";
            fr += "		<label class=\"r-error r-email-error\"></label>";
            fr += "	</div>";
            fr += "	<div class=\"form-request-row\">";
            fr += "		<label class=\"r-lable\">Số ĐT người nhận</label>";
            fr += "		<input class=\"f-control request-txt r-phone\">";
            fr += "		<label class=\"r-error r-phone-error\"></label>";
            fr += "	</div>";
            fr += "	<div class=\"form-request-row\">";
            fr += "		<label class=\"r-lable\">Địa chỉ</label>";
            fr += "		<input class=\"f-control request-txt r-address\">";
            fr += "		<label class=\"r-error r-address-error\"></label>";
            fr += "	</div>";
            fr += "	<div class=\"form-request-row\">";
            fr += "		<label class=\"r-lable\">Ghi chú</label>";
            fr += "		<input class=\"f-control request-txt r-note\">";
            fr += "	</div>";
            fr += "</div>";
            fr += "         </div>";
            fr += "         <div class=\"content2\">";
            fr += "<a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";
            fr += "<a href=\"javascript:;\" class=\"btn btn-close-full\" onclick=\"sendrequest('" + id + "')\">Gửi yêu cầu</a>";
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
        }
        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
        function sendrequest(id) {
            var fullname = $(".r-fullname").val();
            var email = $(".r-email").val();
            var phone = $(".r-phone").val();
            var address = $(".r-address").val();
            var note = $(".r-note").val();
            var checkbo = false;
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
                $.ajax({
                    type: "POST",
                    url: "/chi-tiet-don-hang.aspx/sendrequest",
                    data: "{ID:'" + id + "',FullName:'" + fullname + "',Email:'" + email + "',Phone:'" + phone + "',Note:'" + note + "',Address:'" + address + "'}",
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
    </script>
    <style>
        .form-request {
            float: left;
            width: 100%;
        }

        .form-request-row {
            float: left;
            width: 100%;
            margin: 10px 0;
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
    </style>

    <script>

        $(document).ready(function () {
            $('#txtComment').on("keypress", function (e) {
                if (e.keyCode == 13) {
                    SendMessage();
                }
            });
        });
        $(function () {
            var chat = $.connection.chatHub;
            chat.client.broadcastMessage = function (uid, id, message) {
                var u = $("#<%= hdfID.ClientID%>").val();
                if (uid != u) {
                    var OrderID = $("#<%= hdfOrderID.ClientID%>").val();
                    if (id == OrderID) {
                        $("#ContactCustomer").append(message);
                        
                        if ($("#contact-chat").hasClass("hidden")) {
                            let $noti = $('<div class="toast-noti-fixed teal darken-1"><p><span>Bạn có 1 tin nhắn mới từ <span>Hỗ trợ</span></span><a href="javascript:;" class="view-message" data-mess-id="#contact-chat">Xem</a></p></div>');
                            $('body').append($noti);
                            setTimeout(function () {
                                $noti.fadeOut('slow', function () {
                                    $(this).remove();
                                })
                            }, 3000);
                            setTimeout(function () {
                                $('.toast-noti-fixed').addClass('show');
                            }, 100);
                        }
                    }
                }
            };
            $.connection.hub.start().done(function () {

            });
        });
        function SendMessage() {
            var data = new FormData();
            var orderID = $("#<%=hdfOrderID.ClientID%>").val();
            var comment = $("#txtComment").val();
            var files = $("#<%=IMGUpLoadToUS.ClientID%>").get(0).files;
            var url = "";
            var real = "";
            if (files.length > 0) {
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                data.append("comment", comment);
                data.append("orderID", orderID);
                $.ajax({
                    url: '/HandlerCS.ashx',
                    type: 'POST',
                    data: data,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (file) {
                        if (file.length > 0) {
                            file.forEach(function (data, index) {
                                url += data.name + "|";
                                real += data.realname + "|";
                            });
                            $("#<%=IMGUpLoadToUS.ClientID%>").replaceWith($("#<%=IMGUpLoadToUS.ClientID%>").val('').clone(true));
                            sendmessagetous(orderID, comment, url, real);

                        }
                    },
                    error: function (e) {
                        console.log(e)
                    }
                });

            }
            else {
                sendmessagetous(orderID, comment, url, real);
            }
        }


        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
        function sendmessagetous(orderID, comment, url, real) {
            if (isEmpty(comment) && url == "") {
                $(".info-show").html("Vui lòng điền nội dung.").attr("style", "color:red");
            }
            else {
                $(".info-show").html("Đang cập nhật...").attr("style", "color:blue");
                $.ajax({
                    type: "POST",
                    url: "/chi-tiet-don-hang.aspx/PostComment",
                    data: "{commentext:'" + comment + "',shopid:'" + orderID + "',urlIMG:'" + url + "',real:'" + real + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        if (data != null) {
                            var dataComment = data.Comment;
                            console.log(dataComment)

                            $("#ContactCustomer").append(dataComment);
                            $("#imgUpload").html("");
                            $(".info-show").html("");
                            $("#txtComment").val("");
                            //$('select').formSelect();

                        }
                        else {
                            $("#imgUpload").html("");
                            $(".info-show").html("Có lỗi trong quá trình gửi, vui lòng thử lại sau.").attr("style", "color:red");
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        console.log('lỗi checkend');
                    }
                });
            }
        }


        $(document).ready(function () {

            $('.upload-file-chat').on('click', function () {
                $(this).siblings('.upload-img').trigger('click');
            });

        });
        $('.upload-file-chat').on('click', function () {
            $(this).siblings('.upload-img').trigger('click');
        });
        $(window).scroll(function () {
            var id = $('.table-of-contents li a.active').attr('href');
            $('.scrollspy').each(function () {
                var itemId = $(this).attr('id');
                if (('#' + itemId) == id) {
                    $(this).parent().css({
                        'box-shadow': '0 8px 17px 2px rgba(0, 0, 0, 0.14), 0 3px 14px 2px rgba(0, 0, 0, 0.12), 0 5px 5px -3px rgba(0, 0, 0, 0.2)',
                        'border': '1px solid #000'
                    });
                    $('.scrollspy').not(this).parent().css({
                        'box-shadow': 'rgba(0, 0, 0, 0.14) 0px 2px 2px 0px, rgba(0, 0, 0, 0.12) 0px 3px 1px -2px, rgba(0, 0, 0, 0.2) 0px 1px 5px 0px',
                        'border': '0'
                    });
                }

            });
        });

    </script>
</asp:Content>
