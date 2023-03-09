<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="transportationdetail.aspx.cs" Inherits="NHST.manager.transportationdetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #main-col-wrap {
            width: 66.666666%;
        }

        html body .RadInput_MetroTouch .riDisabled, html body .RadInput_Disabled_MetroTouch {
            background: #888 !important;
            color: #fff !important;
        }

        .aspNetDisabled {
            display: initial !important;
        }
         .RadUpload .ruInputs li {
            clear: both;
        }

        .ab {
            height: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col-66" id="main-col-wrap">
                <div class="right">
                    <a href="javascript:;" onclick="printDiv()" class="btn primary-btn" style="display: none">In đơn hàng</a>
                    <asp:Literal ID="ltrPrint" runat="server"></asp:Literal>
                </div>
                <h1 class="page-title">Chi tiết đơn hàng</h1>
                <div class="feat-row grid-row">
                    <div class="grid-col-100 table-rps">
                        <table class="normal-table">
                            <thead>
                                <tr>
                                    <th style="">ID</th>
                                    <th style="width: 300px;">Mã vận đơn</th>
                                    <th>Cân nặng</th>
                                    <th>Trạng thái</th>
                                    <%--<th style="width: 150px"></th>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrPackages" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="feat-row grid-row">
                    <div class="grid-col-100">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Thông tin đơn hàng</h3>
                            </div>
                            <div class="cont">
                                <div class="collapse-item show">
                                    <div class="cl-body">
                                        <ul class="chi-phi-ul">
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tổng số kiện</div>
                                                    <div class="chiphi-it">
                                                        <asp:Label ID="lblTotalPackage" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tổng cân nặng</div>
                                                    <div class="chiphi-it">
                                                        <asp:Label ID="lblTotalWeight" runat="server"></asp:Label>
                                                        kg
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tổng tiền</div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="rTotalPriceCYN" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="countPrice('cyn')"
                                                        NumberFormat-GroupSizes="3">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="rTotalPrice" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="countPrice('vnd')"
                                                        NumberFormat-GroupSizes="3" Enabled="true">
                                                    </telerik:RadNumericTextBox>
                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Trạng thái đơn hàng</div>
                                                    <div class="control-with-suffix chiphi-it" style="width: 384px">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Text="Đã hủy"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Đang xử lý"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Đang vận chuyển về kho đích"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="Đã về kho đích"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Đã thanh toán"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="Đã hoàn thành"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <span class="hl-txt suffix"><i class="fa fa-sort"></i></span>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Kho TQ</div>
                                                    <div class="control-with-suffix chiphi-it" style="width: 384px">
                                                        <asp:DropDownList ID="ddlWarehouseFrom" runat="server" CssClass="form-control" onchange="returnWeightFee()" Enabled="false"
                                                            DataValueField="ID" DataTextField="WareHouseName">
                                                        </asp:DropDownList>
                                                        <span class="hl-txt suffix"><i class="fa fa-sort"></i></span>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Nhận hàng tại</div>
                                                    <div class="control-with-suffix chiphi-it" style="width: 384px">
                                                        <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control" onchange="returnWeightFee()"
                                                            DataValueField="ID" DataTextField="WareHouseName">
                                                        </asp:DropDownList>
                                                        <span class="hl-txt suffix"><i class="fa fa-sort"></i></span>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Phương thức vận chuyển</div>
                                                    <div class="control-with-suffix chiphi-it" style="width: 384px">
                                                        <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control"
                                                            onchange="returnWeightFee()"
                                                            DataValueField="ID" DataTextField="ShippingTypeName">
                                                        </asp:DropDownList>
                                                        <span class="hl-txt suffix"><i class="fa fa-sort"></i></span>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="collapse-item show">
                                    <div class="cl-heading">Phí tùy chọn</div>
                                    <div class="cl-body">
                                        <ul class="chi-phi-ul">
                                            <li>
                                                <div class="cont630">
                                                    <label class="chiphi-it lb checklb">
                                                        <asp:CheckBox ID="chkCheck" runat="server" />
                                                        <span class="ip-avata"></span><span class="txt">Phí kiểm đếm</span></label>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pCheckNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountCheckPro('ndt')"
                                                        NumberFormat-GroupSizes="3" placeholder="Phí kiểm đếm CNY" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pCheck" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountCheckPro('vnd')"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>

                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <label class="chiphi-it lb checklb">
                                                        <asp:CheckBox ID="chkPackage" runat="server" /><span class="ip-avata"></span> <span class="txt">Phí đóng gỗ</span></label>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pPackedNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountFeePackage('ndt')"
                                                        NumberFormat-GroupSizes="3" placeholder="Phí đóng gỗ CNY" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pPacked" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountFeePackage('vnd')"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <label class="chiphi-it lb checklb">
                                                        <asp:CheckBox ID="chkShiphome" runat="server"  /><span class="ip-avata"></span> <span class="txt">Phí ship giao hàng tận nhà</span>
                                                    </label>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pShipHome" MinValue="0" NumberFormat-DecimalDigits="0"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>
                                            </li>
                                            <li style="display:none">
                                                <div class="cont630">
                                                    <label class="chiphi-it lb checklb">
                                                        <asp:CheckBox ID="chkIsGiaohang" runat="server" Enabled="false" />
                                                        <span class="ip-avata"></span><span class="txt">Yêu cầu giao hàng</span>

                                                    </label>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="collapse-item show">
                                    <div class="cl-heading">Thanh toán</div>
                                    <div class="cl-body">
                                        <ul class="chi-phi-ul">
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Số tiền đã trả</div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pDeposit" MinValue="0" NumberFormat-DecimalDigits="0" Enabled="false"
                                                        NumberFormat-GroupSizes="3">
                                                    </telerik:RadNumericTextBox>
                                                    <div class="chiphi-it suffix">vnd</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630 center-txt">
                                                    <asp:Literal ID="ltrBtnUpdate" runat="server"></asp:Literal>
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary-btn" Text="CẬP NHẬT" OnClick="btnUpdate_Click" />
                                                    <a href="/manager/transportation-list" class="btn primary-btn">Trở về</a>
                                                    <%--<asp:Button ID="btnUpdate" runat="server" CssClass="btn pill-btn primary-btn admin-btn full-width" Style="display: none" Text="CẬP NHẬT" OnClick="" />
                                                    <asp:Button ID="btnThanhtoan" runat="server" CssClass="btn primary-btn" Text="THANH TOÁN" OnClick="btnThanhtoan_Click" Visible="false" />--%>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
    </main>

    <div id="printcontent" class="printdetail" style="display: none;">
    </div>
    <asp:HiddenField ID="hdfStatus" runat="server" />
    <asp:HiddenField ID="hdfCurrency" runat="server" />

    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <style>
        .sec {
            padding: 20px 0;
        }

        .main {
            width: 91%;
        }

        dl dd {
            display: block;
            padding-left: 0px;
            float: left;
            margin-bottom: 15px;
        }

        select.form-control {
            line-height: 25px;
        }

        /*.riSingle {
            width: 40% !important;
        }*/

        .admin-btn {
            float: right;
            clear: both;
            margin-top: 10px;
            line-height: 30px;
        }

        .order-panel dl dt {
            width: 40%;
            clear: both;
        }

        .ordercodes {
            width: 100%;
        }

        .ordercode {
            float: left;
            width: 100%;
            clear: both;
            margin-bottom: 10px;
        }

            .ordercode .item-element {
                float: left;
                width: 25%;
                padding: 0 10px;
            }

        .addordercode {
            /*padding: 0 10px;
            margin: 20px 0;
            background: url('/App_Themes/NewUI/images/icon-plus.png') center left no-repeat;*/
        }

            .addordercode a {
                padding-left: 10px;
            }

        .title-fee {
            float: left;
            width: 100%;
            border-bottom: solid 1px #ccc;
            font-size: 20px;
            margin: 20px 0;
            color: #000;
        }

        .bg-red-nhst {
            background: #ea2028;
            color: #fff;
        }

            .bg-red-nhst dt, .bg-red-nhst .title {
                color: #fff;
            }

        .order-panel .title {
            border-bottom: solid 1px #fff;
        }
    </style>

    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function add_loading() {
                $(".page-inner").append("<div class='loading_bg'></div>");
                var height = $(".page-inner").height();
                $(".loading_bg").css("height", height + "px");
            }
            function remove_loading() {
                $(".loading_bg").remove();
            }
            function returnWeightFee() {
                var status = parseFloat($("#<%=ddlStatus.ClientID%>").val());
                if (status > 1) {

                    var totalweight = 0;
                    var currency = parseFloat($("#<%= hdfCurrency.ClientID%>").val());
                    var warehouseFrom = $("#<%=ddlWarehouseFrom.ClientID%>").val();
                    var warehouseTo = $("#<%= ddlReceivePlace.ClientID%>").val();
                    var shippingType = $("#<%=ddlShippingType.ClientID%>").val();
                    if ($(".package-item").length > 0) {
                        $(".package-item").each(function () {
                            totalweight += parseFloat($(this).attr("data-weight"));
                        });
                    }
                    add_loading();
                    $.ajax({
                        type: "POST",
                        url: "/manager/transportationdetail.aspx/getPrice",
                        data: "{weight:'" + totalweight + "',warehouseFrom:'" + warehouseFrom + "',warehouseTo:'" + warehouseTo + "',shippingType:'" + shippingType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var data = parseFloat(msg.d);
                            var totalpriceCYN = data / currency;
                            var totalpriceVND = data;
                            $("#<%= rTotalPriceCYN.ClientID%>").val(totalpriceCYN);
                            $("#<%= rTotalPrice.ClientID%>").val(totalpriceVND);
                            remove_loading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi checkend');
                        }
                    });
                }
            }

            function CountCheckPro(type) {
                var currency = parseFloat($("#<%= hdfCurrency.ClientID%>").val());
                var pCheckNDT = $("#<%= pCheckNDT.ClientID%>").val();
                var pCheckVND = $("#<%= pCheck.ClientID%>").val();
                if (type == "vnd") {
                    if (!isEmpty(pCheckVND)) {
                        var ndt = pCheckVND / currency;
                        $("#<%= pCheckNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pCheckNDT.ClientID%>").val(0);
                        $("#<%= pCheck.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(pCheckNDT)) {
                        var vnd = pCheckNDT * currency;
                        $("#<%= pCheck.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pCheckNDT.ClientID%>").val(0);
                        $("#<%= pCheck.ClientID%>").val(0);
                    }
                }
            }

            function CountFeePackage(type) {
                var currency = parseFloat($("#<%= hdfCurrency.ClientID%>").val());
                var pPackedNDT = $("#<%= pPackedNDT.ClientID%>").val();
                var pPackedVND = $("#<%= pPacked.ClientID%>").val();
                if (type == "vnd") {
                    if (!isEmpty(pPackedVND)) {
                        var ndt = pPackedVND / currency;
                        $("#<%= pPackedNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pPackedNDT.ClientID%>").val(0);
                        $("#<%= pPacked.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(pPackedNDT)) {
                        var vnd = pPackedNDT * currency;
                        $("#<%= pPacked.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pPackedNDT.ClientID%>").val(0);
                        $("#<%= pPacked.ClientID%>").val(0);
                    }
                }
            }


           

            function countPrice(type)
            {
                var currency = parseFloat($("#<%= hdfCurrency.ClientID%>").val());
                if(type == "cyn")
                {
                    var totalprice = parseFloat($("#<%= rTotalPriceCYN.ClientID%>").val());
                    var totalpriceVND = totalprice * currency;
                    $("#<%= rTotalPrice.ClientID%>").val(totalpriceVND);
                }
                else
                {
                    var totalpriceVND = parseFloat($("#<%= rTotalPrice.ClientID%>").val());
                    var totalpriceCYN = totalpriceVND / currency;
                    $("#<%= rTotalPriceCYN.ClientID%>").val(totalpriceCYN);
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
