<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="NHST.manager.OrderDetail" %>

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
                     <asp:Button ID="btnExcel" runat="server" CssClass="btn primary-btn" Text="Xuất Excel" OnClick="btnExcel_Click" />

                                                

                    <a href="javascript:;" onclick="printDiv()" class="btn primary-btn">In đơn hàng</a>
                    <asp:Literal ID="ltrPrint" runat="server"></asp:Literal>
                </div>
                <h1 class="page-title">Chi tiết đơn hàng</h1>
                <div class="feat-row grid-row ">
                    <div class="grid-col-100">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Thông tin đơn</h3>
                            </div>
                            <div class="cont">
                                <div class="collapse-item show">
                                    <div class="cl-body">
                                        <ul class="chi-phi-ul">
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Thông tin</div>
                                                    <div class="chiphi-it">
                                                        <asp:Label ID="lblThongtindonhang" runat="server"></asp:Label>
                                                        <a href="javascript:;" class="btn primary-btn" onclick="copyToClipboard()">Copy</a>
                                                    </div>
                                                </div>
                                            </li>
                                            <%--<li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Mã đơn hàng</div>
                                                    <div class="chiphi-it">
                                                        <asp:Label ID="lblOrderID" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Username</div>
                                                    <div class="chiphi-it">
                                                        <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>--%>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
                <div class="feat-row grid-row">
                    <div class="grid-col-100">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Mã đơn hàng</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row">
                                        <p class="lb">Mã đơn hàng</p>
                                        <asp:TextBox ID="txtMainOrderCode" runat="server" CssClass="form-control" placeholder="Mã đơn hàng"
                                            Style="width: 95%; float: left"></asp:TextBox>
                                        <asp:Literal ID="ltrXemMa" runat="server"></asp:Literal>
                                        <%--<a href="" class="btn primary-btn" style="float:left;">Xem</a>--%>
                                    </div>
                                </div>

                                <div class="grid-col-50">
                                    <div class="inline-date">
                                        <div class="lb">Ngày phát hàng dự kiến:</div>
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="ExpectedDate" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Ngày phát hàng dự kiến" CssClass="date" DateInput-EmptyMessage="Ngày phát hàng dự kiến">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </div>
                                </div>

                            </div>
                        </article>
                    </div>
                </div>
                <div class="feat-row grid-row">
                    <div class="grid-col-50">
                        <asp:Panel ID="pnadminmanager" runat="server" Visible="false">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Nhân viên xử lý</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner" style="height: 295px;">
                                        <div class="grid-row">
                                            <div class="grid-col-100 form-row">
                                                <p class="lb">Nhân viên saler</p>
                                                <div class="control-with-suffix fw">
                                                    <asp:DropDownList ID="ddlSaler" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        DataTextField="Username" DataValueField="ID">
                                                    </asp:DropDownList>
                                                    <span class="suffix hl-txt"><i class="fa fa-sort"></i></span>
                                                </div>
                                            </div>
                                            <div class="grid-col-100 form-row">
                                                <p class="lb">Nhân viên đặt hàng</p>
                                                <div class="control-with-suffix fw">
                                                    <asp:DropDownList ID="ddlDatHang" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        DataTextField="Username" DataValueField="ID">
                                                    </asp:DropDownList>
                                                    <span class="suffix hl-txt"><i class="fa fa-sort"></i></span>
                                                </div>
                                            </div>
                                            <div class="grid-col-100 form-row" style="display: none;">
                                                <asp:DropDownList ID="ddlKhoTQ" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                    DataTextField="Username" DataValueField="ID">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlKhoVN" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                    DataTextField="Username" DataValueField="ID">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="grid-col-100 center-txt">
                                                <asp:Button ID="btnStaffUpdate" runat="server" CssClass="btn primary-btn" Text="CẬP NHẬT" OnClick="btnStaffUpdate_Click" />
                                                <%--<a href="#" class="btn primary-btn">Cập nhật</a>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </asp:Panel>
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Nhân viên xử lý</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <dl class="dl">
                                        <asp:Literal ID="ltr_OrderFee_UserInfo2" runat="server"></asp:Literal>
                                    </dl>
                                </div>
                            </div>
                        </article>
                    </div>
                    <div class="grid-col-50">
                        <article class="pane-primary">
                            <div class="heading" style="background-color: #6639a6">
                                <h3 class="lb center-txt">Liên lạc nội bộ</h3>
                            </div>
                            <div class="chatmess-box messboxtim">
                                <div class="mess-list staff-list-comment" id="ContactLocal">
                                    <asp:Literal ID="ltrInComment" runat="server"></asp:Literal>
                                </div>
                                <div class="mess-write">
                                    <div class="writebox">
                                        <label>
                                            <span>Thêm ảnh</span>
                                            <asp:FileUpload runat="server" ID="IMGUpLoadToLocal" class="upload-img" type="file" AllowMultiple="true" title=""></asp:FileUpload>
                                        </label>
                                        <ul class="row-package staffcomment">
                                        </ul>
                                    </div>
                                    <div class="writebox" style="clear: both; margin-top: 10px;">
                                        <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" placeholder="Nội dung"></asp:TextBox>
                                        <a id="btnsendcommentstaff" href="javascript:;" class="btn" onclick="SentMessageToLocal()">Gửi</a>
                                        <asp:Button ID="btnSend" runat="server" Text="Gửi" ValidationGroup="n" Style="display: none"
                                            CssClass="btn" OnClick="btnSend_Click" />
                                    </div>
                                    <div class="writebox">
                                        <span class="info-show-staff"></span>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
                <%--<div class="feat-row grid-row">
                    <div class="grid-col-50">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Khách hàng</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <dl class="dl">
                                        <asp:Literal ID="ltr_OrderFee_UserInfo1" runat="server"></asp:Literal>                                       
                                    </dl>
                                </div>
                            </div>
                        </article>
                    </div>
                    <div class="grid-col-50">
                       
                    </div>
                </div>--%>
                <div class="feat-row grid-row">
                    <div class="grid-col-50">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Thông tin người đặt hàng</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <dl class="dl">
                                        <asp:Literal ID="ltr_OrderFee_UserInfo" runat="server"></asp:Literal>
                                    </dl>
                                </div>
                            </div>
                        </article>
                    </div>
                    <div class="grid-col-50 print0">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Thông tin người nhận hàng</h3>
                            </div>
                            <div class="cont" style="height: 235px;">
                                <div class="inner">
                                    <dl class="dl">
                                        <asp:Literal ID="ltr_AddressReceive" runat="server"></asp:Literal>
                                    </dl>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
                <div class="feat-row grid-row">
                    <div class="grid-col-100 table-rps">
                        <table class="normal-table panel-table">
                            <thead>
                                <tr>
                                    <th style="">ID</th>
                                    <th style="width: 300px;">Sản phẩm</th>
                                    <th>S.lg</th>
                                    <th>Đơn giá</th>
                                    <th>Trạng thái</th>
                                    <th style="width: 100px"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrProducts" runat="server"></asp:Literal>

                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="feat-row grid-row ">
                    <div class="grid-col-100">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Danh sách mã vận đơn</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div id="transactioncodeList" class="cont">
                                        <asp:Literal ID="ltrCodeList" runat="server"></asp:Literal>
                                    </div>
                                    <div class="ordercode addordercode"><a href="javascript:;" class="hl-txt" onclick="addCodeTransaction()"><u>+ Thêm mã vận đơn</u></a></div>
                                    <%--<a href="#" class="hl-txt"><u>+ Thêm mã vận đơn</u></a>--%>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
                <div class="feat-row grid-row ">
                    <div class="grid-col-100">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Chi phí của đơn hàng</h3>
                            </div>
                            <div class="cont">
                                <div class="collapse-item show">
                                    <div class="cl-heading">Phí cố định</div>
                                    <div class="cl-body">

                                        <ul class="chi-phi-ul">

                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Shop đã phát hàng</div>
                                                    <div class="control-with-suffix sttchiphi-it" style="width: 384px">
                                                        <asp:CheckBox ID="chkIsShopSendGood" runat="server" />
                                                    </div>
                                                </div>
                                            </li>

                                            <li>
                                                <div class="cont630" >
                                                    <div class="chiphi-it lb">chờ thanh toán</div>
                                                    <div class="control-with-suffix sttchiphi-it" style="width: 384px">
                                                        <asp:CheckBox ID="chkIsBuying" runat="server" />
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Đã thanh toán(chờ shop phát hàng)</div>
                                                    <div class="control-with-suffix sttchiphi-it" style="width: 384px">
                                                        <asp:CheckBox ID="chkIsPaying" runat="server" Visible="true" />
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tổng số sản phẩm</div>
                                                    <div class="chiphi-it">
                                                        <asp:Label ID="lblTotalQuantity" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tiền hàng trên web</div>
                                                    <div class="chiphi-it">
                                                        <asp:Label ID="lblTotalMoney" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                            </li>

                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tổng số tiền mua thật</div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="rTotalPriceRealCYN" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountRealPrice()"
                                                        NumberFormat-GroupSizes="3" placeholder="Tiền mua thật CNY">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="rTotalPriceReal" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountRealPrice1()"
                                                        NumberFormat-GroupSizes="3" Enabled="true">
                                                    </telerik:RadNumericTextBox>
                                                    <%--<input type="text" class="form-control chiphi-it">
                                                    <input type="text" class="form-control chiphi-it">--%>
                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Phí ship Trung Quốc</div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pCNShipFeeNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountShipFee('ndt')"
                                                        NumberFormat-GroupSizes="3" placeholder="Tiền ship Trung Quốc CNY" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pCNShipFee" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountShipFee('vnd')"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <%--<input type="text" class="form-control chiphi-it">
                                                    <input type="text" class="form-control chiphi-it">--%>
                                                    <div class="chiphi-it suffix">vnd</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tiền hoa hồng</div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it"
                                                        Skin="MetroTouch"
                                                        ID="pHHCYN" NumberFormat-DecimalDigits="2" Enabled="false"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-controlchiphi-it" Skin="MetroTouch"
                                                        ID="pHHVND" NumberFormat-DecimalDigits="0" Enabled="false"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <%--<input type="text" class="form-control chiphi-it">
                                                    <input type="text" class="form-control chiphi-it">--%>
                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">
                                                        Phí mua hàng (CK
                                                        <asp:Label ID="lblCKFeebuypro" runat="server"></asp:Label>%)
                                                    </div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pBuyNDT" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountFeeBuyPro()"
                                                        NumberFormat-GroupSizes="3" placeholder="Phí mua hàng CNY">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pBuy" MinValue="0" NumberFormat-DecimalDigits="0"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>

                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>
                                            </li>
                                            <li style="display: none">
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Tổng cân nặng</div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="txtOrderWeight" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="gettotalweight2()"
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <%--<input type="text" class="form-control chiphi-it">--%>
                                                    <div class="chiphi-it suffix">Kg</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">
                                                        Phí vận chuyển TQ-VN (CK
                                                        <asp:Label ID="lblCKFeeWeight" runat="server"></asp:Label>VNĐ/KG: 
                                                        <asp:Label ID="lblCKFeeweightPrice" runat="server"></asp:Label>)
                                                    </div>

                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pWeightNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="returnWeightFee()"
                                                        NumberFormat-GroupSizes="3" placeholder="Phí cân nặng CNY" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup=""
                                                        NumberFormat-GroupSizes="3" Value="0">
                                                    </telerik:RadNumericTextBox>
                                                    <div class="chiphi-it suffix">vnđ</div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">
                                                        Phí lưu kho
                                                    </div>
                                                    <div class="control-with-suffix chiphi-it" style="width: 384px">
                                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                            ID="rFeeWarehouse" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="returnWeightFee()"
                                                            NumberFormat-GroupSizes="3" placeholder="Phí cân nặng CNY" Value="0" Width="100%" Enabled="false">
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <div class="chiphi-it lb">Trạng thái đơn hàng</div>
                                                    <div class="control-with-suffix chiphi-it" style="width: 384px">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
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

                                                        <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control" onchange="returnWeightFee()" Enabled="false"
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

                                                        <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control" onchange="returnWeightFee()" Enabled="false"
                                                            DataValueField="ID" DataTextField="ShippingTypeName">
                                                        </asp:DropDownList>
                                                        <span class="hl-txt suffix"><i class="fa fa-sort"></i></span>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="cont630">
                                                    <asp:Panel ID="pnbaogia" runat="server">
                                                        <div class="chiphi-it lb">Báo giá</div>
                                                        <div class="control-with-suffix chiphi-it" style="width: 384px">
                                                            <asp:CheckBox ID="chkIsCheckPrice" runat="server" />
                                                        </div>
                                                    </asp:Panel>

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
                                                        <asp:CheckBox ID="chkCheck" runat="server" Enabled="false" />
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
                                                        <asp:CheckBox ID="chkPackage" runat="server" Enabled="false" /><span class="ip-avata"></span> <span class="txt">Phí đóng gỗ</span></label>
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
                                                        <asp:CheckBox ID="chkShiphome" runat="server" Enabled="false" /><span class="ip-avata"></span> <span class="txt">Phí ship giao hàng tận nhà</span>
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
                                                    <div class="chiphi-it lb">Số tiền phải cọc</div>
                                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                                        ID="pAmountDeposit" MinValue="0" NumberFormat-DecimalDigits="0"
                                                        NumberFormat-GroupSizes="3" Enabled="false">
                                                    </telerik:RadNumericTextBox>
                                                    <div class="chiphi-it suffix">vnd</div>
                                                </div>
                                            </li>
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
                                            <li></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
                <div class="feat-row grid-row print1">
                    <div class="grid-col-100 print1content">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Tổng tiền hàng khách cần thanh toán</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <dl class="dl dt-170">
                                        <dt>Tiền hàng</dt>
                                        <dd>
                                            <asp:Label ID="lblMoneyNotFee" runat="server"></asp:Label>
                                            vnđ</dd>
                                        <dt>Ship nội địa</dt>
                                        <dd>
                                            <asp:Label ID="lblShipTQ" runat="server" Text="0"></asp:Label>
                                            vnđ</dd>
                                        <dt>Phí mua hàng</dt>
                                        <dd>
                                            <asp:Label ID="lblFeeBuyProduct" runat="server" Text="0"></asp:Label>
                                            vnđ</dd>
                                        <dt>Phí tùy chọn</dt>
                                        <dd>
                                            <asp:Label ID="lblAllFee" runat="server" Text="0"></asp:Label>
                                            vnđ</dd>
                                        <dt>Phí vận chuyển TQ-VN</dt>
                                        <dd>
                                            <asp:Label ID="lblFeeTQVN" runat="server" Text="0"></asp:Label>
                                            vnđ</dd>
                                        <dt>Tổng chi phí</dt>
                                        <dd>
                                            <asp:Label ID="lblAllTotal" runat="server" Text="0"></asp:Label>
                                            vnđ</dd>
                                        <dt>Đã thanh toán</dt>
                                        <dd>
                                            <asp:Label ID="lblDeposit" runat="server" Text="0"></asp:Label>
                                            vnđ</dd>
                                        <dt>Cần thanh toán</dt>
                                        <dd>
                                            <asp:Label ID="lblLeftPay" runat="server" Text="0"></asp:Label>
                                            vnđ</dd>
                                    </dl>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
                <div class="feat-row grid-row">
                    <div class="grid-col-100 table-rps">
                        <table class="normal-table panel-table">
                            <thead>
                                <tr>
                                    <th>Ngày thanh toán</th>
                                    <th>Loại thanh toán</th>
                                    <th>Hình thức thanh toán</th>
                                    <th style="width: 170px">Số tiền</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptPayment" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td>
                                                <%# PJUtils.ShowStatusPayHistory( Eval("Status").ToString().ToInt()) %>
                                            </td>
                                            <td>
                                                <%#Eval("Type").ToString() == "1"?"Trực tiếp":"Ví điện tử" %>
                                            </td>
                                            <td class="qty"><%#Eval("Amount","{0:N0}") %> VNĐ
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                                <%--<tr>
                                    <td>01/03/2018</td>
                                    <td><span class="bg-red">Đặt cọc</span></td>
                                    <td>Ví điện tử</td>
                                    <td>239,289 vnđ</td>
                                </tr>--%>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="feat-row grid-row">
                    <div class="grid-col-100 table-rps">
                        <asp:Panel ID="pn" runat="server">
                            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                AllowSorting="true" AllowFilteringByColumn="True">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView CssClass="normal-table panel-table" DataKeyNames="ID">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Date" HeaderText="Ngày thay đổi" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Username" HeaderText="Người thay đổi" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RoleName" HeaderText="Quyền hạn" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Content" HeaderText="Nội dung" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                        PrevPageText="← Previous" />
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="order-panels print4" style="display: none">
                <div class="order-panel">
                    <div class="title">Danh sách mã vận đơn</div>
                    <div class="cont" style="overflow-x: scroll">
                        <table class="tb-product">
                            <thead>
                                <tr>
                                    <th>Mã vận đơn</th>
                                    <th>Cân nặng</th>
                                    <th>Trạng thái</th>
                                    <%--<th>Ghi chú</th>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrMavandon" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="order-panel print3" style="display: none;">
                <div class="title">Danh sách sản phẩm</div>
                <div class="cont" style="overflow-x: scroll">
                    <table class="tb-product">
                        <tr>
                            <th class="pro">ID</th>
                            <th class="pro">Sản phẩm</th>
                            <th class="pro">Thuộc tính</th>
                            <th class="qty">Số lượng</th>
                            <th class="price">Đơn giá</th>
                            <th class="price">Giá sản phẩm CNY</th>
                            <th class="price">Ghi chú riêng sản phẩm</th>
                            <th class="price">Trạng thái</th>
                        </tr>
                        <asp:Literal ID="ltrProductPrint" runat="server"></asp:Literal>
                    </table>
                </div>
            </div>
            <div class="order-panels print6" style="display: none;">
                <span style="font-size: 30px; text-align: center; text-transform: uppercase; float: left; width: 100%; margin-bottom: 40px;">Phiếu xuất kho</span>
            </div>
            <div class="order-panels print6" style="display: none;">
                <asp:Literal ID="ltr_OrderCode" runat="server"></asp:Literal>
            </div>
            <div class="grid-col-33" id="right-col-wrap">
                <div class="waypoint-trigger">
                    <article class="pane-primary">
                        <div class="cont">
                            <div class="inner inline-lb-info">
                                <div class="lb">Mã đơn hàng</div>
                                <div class="info">
                                    <asp:Literal ID="ltr_OrderID" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <asp:Literal ID="ltrStatus1" runat="server"></asp:Literal>

                            <div class="inner inline-lb-info">
                                <div class="lb">Tổng tiền</div>
                                <div class="info">
                                    <asp:Literal ID="ltrlblAllTotal1" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="inner inline-lb-info">
                                <div class="lb">Đã trả</div>
                                <div class="info">
                                    <asp:Literal ID="lblDeposit1" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="inner inline-lb-info">
                                <div class="lb">Còn lại</div>
                                <div class="info">
                                    <asp:Literal ID="lblLeftPay1" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="inner inline-lb-info">
                                <div class="cont630 center-txt">
                                    <asp:Literal ID="ltrBtnUpdate" runat="server"></asp:Literal>
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn pill-btn primary-btn admin-btn full-width" Style="display: none" Text="CẬP NHẬT" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnThanhtoan" runat="server" CssClass="btn primary-btn" Text="THANH TOÁN" OnClick="btnThanhtoan_Click" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </article>
                    <article class="pane-primary">
                        <div class="heading" style="background-color: #00adb5">
                            <h3 class="lb center-txt">Liên lạc với khách hàng</h3>
                        </div>
                        <div class="chatmess-box messboxxanh">
                            <div class="mess-list customer-comment" id="ContactCustomer">
                                <asp:Literal ID="ltrOutComment" runat="server"></asp:Literal>

                            </div>
                            <div class="mess-write">

                                <div class="writebox">
                                    <asp:TextBox ID="txtComment1" runat="server" CssClass="form-control" placeholder="Nội dung"></asp:TextBox>
                                    <a id="btnsendcomment" href="javascript:;" class="btn" onclick="SentMessageToCustomer()">Gửi</a>
                                    <asp:Button ID="btnSend1" runat="server" Text="Gửi" ValidationGroup="1n" Style="display: none" CssClass="btn" OnClick="btnSend1_Click" UseSubmitBehavior="false" />
                                </div>
                                <div class="writebox" style="clear: both; margin-top: 10px;">
                                    <label>
                                        <span>Thêm ảnh</span>
                                        <asp:FileUpload runat="server" ID="ImgUpLoadToCustomer" class="upload-img" type="file" AllowMultiple="true" title=""></asp:FileUpload>
                                    </label>
                                    <ul class="row-package customercomment">
                                    </ul>
                                </div>
                                <div class="writebox">
                                    <span class="info-show"></span>
                                </div>
                            </div>
                        </div>
                    </article>

                </div>
            </div>
            <div class="grid-col-50 print7" style="display: none">
                <div style="float: left; width: 100%; margin-bottom: 50px;">
                    Khi nhận hàng quý khách lưu ý:<br />
                    - Quý khách vui lòng kiểm tra lại cân nặng và kích thước, tình trạng kiện hàng cùng với nhân viên giao hàng.<br />
                    - Quý khách vui lòng gửi khiếu nại trong vòng 48h kể từ khi nhận hàng.<br />
                    - Quý khách tạo khiếu nại trên hệ thống, vui lòng ghi rõ thông tin cần khiếu nại và gửi kèm hình ảnh
                    chụp bao bì hàng trước khi mở bao bì, ảnh chụp sản phẩm/hàng hóa khiếu nại, thông tin mã đơn hàng, 
                    mã shop, mã link, danh sách liệt kê hàng hóa mua của nhà cung cấp gửi kèm.<br />
                    - Khi cần hỗ trợ Quý khách vui lòng liên hệ theo hotline: 0962.111.688.<br />
                    - Khi cần hỗ trợ giao hàng Quý khách vui lòng liên hệ hotline: Kho HN 097.415.1160.
                    <br />
                    <br />
                    <br />
                    Khách hàng xác nhận tình trạng nhận hàng:
                    <div style="width: 100%; height: 45px; border-bottom: dotted 1px #000;"></div>
                    <div style="width: 100%; height: 45px; border-bottom: dotted 1px #000;"></div>
                </div>
                <div style="float: left; width: 100%; clear: both;">
                    <div style="float: left; width: 20%; text-align: center">
                        KHÁCH HÀNG
                    </div>
                    <div style="float: right; width: 20%; text-align: center">
                        Ngày ............... / .............. / 20............<br />
                        NHÂN VIÊN XUẤT KHO
                    </div>
                </div>
            </div>
        </div>
    </main>

    <div id="printcontent" class="printdetail" style="display: none;">
    </div>
    <asp:HiddenField ID="hdfcurrent" runat="server" />
    <asp:HiddenField ID="hdfFeeBuyProDiscount" runat="server" />
    <asp:HiddenField ID="hdfFeeWeightDiscount" runat="server" />
    <asp:HiddenField ID="hdfFeeweightPriceDiscount" runat="server" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
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
            margin-bottom: 50px;
        }

            .ordercode .item-element {
                float: left;
                /* width: 14%;*/
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

        .row-package {
            list-style: none;
        }

            .row-package li {
                position: relative;
                width: 40%;
                margin-top: 10px;
            }

        }
    </style>
    <asp:HiddenField ID="hdforderamount" runat="server" />
    <asp:HiddenField ID="hdfoc2" runat="server" />
    <asp:HiddenField ID="hdfoc3" runat="server" />
    <asp:HiddenField ID="hdfoc4" runat="server" />
    <asp:HiddenField ID="hdfoc5" runat="server" />
    <asp:HiddenField ID="hdfFromPlace" runat="server" />
    <asp:HiddenField ID="hdfReceivePlace" runat="server" />
    <asp:HiddenField ID="hdfShippingType" runat="server" />
    <asp:HiddenField ID="hdfFeeTQVNHN" runat="server" />
    <asp:HiddenField ID="hdfFeeTQVNHCM" runat="server" />
    <asp:HiddenField ID="hdfCodeTransactionList" runat="server" />
    <asp:HiddenField ID="hdfMOrderID" runat="server" />
    <asp:HiddenField ID="hdfID" runat="server" />

    <telerik:RadScriptBlock ID="rsc" runat="server">

       

        <script type="text/javascript">
            function Remove(obj, ido) {
                obj.parent().parent().remove();
                $("#" + ido).val(null);
            }
            function readFiles(input, id, inputid) {
                counter = input.files.length;
                for (x = 0; x < counter; x++) {
                    if (input.files && input.files[x]) {

                        var reader = new FileReader();

                        reader.onload = function (e) {
                            $("ul.row-package." + id + "").html("<li><div class=\"close1\"><i class=\"close_message\" style=\"right:-25px;\" onclick=\"Remove($(this),'" + inputid + "')\"></i></div><a href=\"' + e.target.result + '\" data-lightbox=\"image-1\"><img src=\"" + e.target.result + "\"></a></li>");
                        };

                        reader.readAsDataURL(input.files[x]);
                    }
                }
            }
            function UpdateOrder() {
                //btnUpdate
                var mid = $("#<%=hdfOrderID.ClientID%>").val();
                var list = "";
                $(".order-versionnew").each(function () {
                    var id = $(this).attr("data-packageID");
                    var code = $(this).find(".transactionCode").val();
                    var weight = $(this).find(".transactionWeight").val();
                    var status = $(this).find(".transactionCodeStatus").val();
                    var note = $(this).find(".transactionDescription").val();
                    list += id + "," + code + "," + weight + "," + status + "," + note + "|";
                });
                $.ajax({
                    type: "POST",
                    url: "/manager/OrderDetail.aspx/CheckSmallPackageCode",
                    data: "{id:'" + mid + "',listOrderCode:'" + list + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret == "ok") {
                            $("#<%=hdfCodeTransactionList.ClientID%>").val(list);
                            $("#<%=btnUpdate.ClientID%>").click();
                        }
                        else {
                            alert('Các mã bị trùng: ' + ret);
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert('Lỗi');
                    }
                });
               <%-- $("#<%=hdfCodeTransactionList.ClientID%>").val(list);
                $("#<%=btnUpdate.ClientID%>").click();--%>
            }
            function addCodeTransaction() {
                var html = "";
                html += "<div class=\"ordercode order-versionnew\" data-packageID=\"0\">";
                html += " <div class=\"item-element\" style=\"width:15%;\">";
                html += "     <span>Mã Vận đơn:</span>";
                html += "     <input class=\"transactionCode form-control\" type=\"text\" placeholder=\"Mã vận đơn\" />";
                html += " </div>";

                html += " <div class=\"item-element\"style=\"width:10%;\">";
                html += "     <span>Cân thực:</span>";
                html += "     <input class=\"transactionWeight form-control\" type=\"text\" placeholder=\"Cân thực\" value=\"0\" onkeyup=\"returnWeightFee()\" />";
                html += " </div>";

                html += " <div class=\"item-element\"style=\"width:12%;\">";
                html += "     <span>Kích thước :</span>";
                html += "     <input class=\"transactionWeight form-control\"  value=\"0 x 0 x 0\" Enabled=\"false\"  onkeyup=\"returnWeightFee()\" />";
                html += " </div>";

                html += " <div class=\"item-element\"style=\"width:12%;\">";
                html += "     <span>Cân quy đổi:</span>";
                html += "     <input class=\"transactionWeight form-control\"  value=\"0\" Enabled=\"false\"  onkeyup=\"returnWeightFee()\" />";
                html += " </div>";
                html += " <div class=\"item-element\"style=\"width:12%;\">";
                html += "     <span>Cân tính tiền:</span>";
                html += "     <input class=\"transactionWeight form-control\"  value=\"0\" Enabled=\"false\"  onkeyup=\"returnWeightFee()\" />";
                html += " </div>";

                html += " <div class=\"item-element\"style=\"width:16%;\">";
                html += "     <span>Trạng thái:</span>";
                html += "     <select class=\"transactionCodeStatus form-control\">";
                html += "         <option value=\"1\">Mới đặt - chưa về kho TQ</option>";
                html += "         <option value=\"2\">Đã về kho TQ</option>";
                html += "         <option value=\"3\">Đã về kho đích</option>";
                html += "         <option value=\"4\">Đã giao khách hàng</option>";
                html += "         <option value=\"0\">Đã hủy</option>";
                html += "     </select>";
                html += " </div>";
                html += " <div class=\"item-element\"style=\"width:12%;\">";
                html += "     <span>Ghi chú:</span>";
                html += "     <input class=\"transactionDescription form-control\" type=\"text\" placeholder=\"Ghi chú\" />";
                html += " </div>";
               
               
                html += "<div class=\"item-element\"style=\"width:11%;\"><a href=\"javascript:;\" class=\"btn primary-btn\" style=\"margin-top:19px;\" onclick=\"deleteOrderCode($(this))\">Xóa</a></div>";
                html += "</div>";
                $("#transactioncodeList").append(html);
            }
            function deleteOrderCode(obj) {
                var r = confirm("Bạn muốn xóa mã vận đơn này?");
                if (r == true) {
                    var id = obj.parent().parent().attr("data-packageID");
                    $.ajax({
                        type: "POST",
                        url: "/manager/OrderDetail.aspx/DeleteSmallPackage",
                        data: "{IDPackage:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            obj.parent().parent().remove();
                            gettotalweight();
                            UpdateOrder();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            alert('Lỗi');
                        }
                    });
                }
                else {

                }
            }
            function printDiv() {
                $(".print0").clone().prependTo(".print1").find(".lb").attr("style", "color:#000");
                $(".print1").find(".print1content").removeClass("grid-col-100").addClass("grid-col-50").find(".lb").attr("style", "color:#000");
                $(".print1").clone().appendTo(".printdetail").show();
                $(".print3").clone().appendTo(".printdetail").show();
                $(".print4").clone().appendTo(".printdetail").show();
                $(".print7").clone().appendTo(".printdetail").show();
                var html = "";
                html += '<html>';
                html += '<head>';
                html += '<link rel="stylesheet" href="/App_Themes/NHST/css/style.css" />';
                html += '<link rel="stylesheet" href="/App_Themes/NHST/css/responsive.css" />';
                html += '<link rel="stylesheet" href="/App_Themes/NHST/css/style-custom.css" />';
                html += '<link rel="stylesheet" href="/App_Themes/AdminNew/css/style.css" />';
                html += '<link rel="stylesheet" href="/App_Themes/AdminNew/css/style-p.css" />';
                //$('link').each(function () { // find all <link tags that have
                //    if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                //        html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';                        
                //    }
                //});
                html += '</head>';
                html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
                html += '</html>';
                var w = window.open("", "print");
                if (w) { w.document.write(html); w.document.close() }
                $(".print1").find(".print0").remove();
                $(".print1").find(".print1content").removeClass("grid-col-50").addClass("grid-col-100").find(".lb").attr("style", "color:#fff");
            }

          

            $(document).ready(function () {
                $(".print6").clone().appendTo(".printdetail").show();
                //$(".print2").clone().appendTo(".printdetail");
                //$(".print0").clone().appendTo(".printdetail").show();                
                //$(".print0").prependTo(".print1").find(".lb").attr("style","color:#000");
                //$(".print1").find(".print1content").removeClass("grid-col-100").addClass("grid-col-50").find(".lb").attr("style", "color:#000");
                //$(".print1").clone().appendTo(".printdetail").show();
                //$(".print3").clone().appendTo(".printdetail").show();
                //$(".print4").clone().appendTo(".printdetail").show();
                //$(".print7").clone().appendTo(".printdetail").show();

                //$(".print5").clone().appendTo(".printdetail");


                var oc2 = $("#<%= hdfoc2.ClientID%>").val();
                var oc3 = $("#<%= hdfoc3.ClientID%>").val();
                var oc4 = $("#<%= hdfoc4.ClientID%>").val();
                var oc5 = $("#<%= hdfoc5.ClientID%>").val();

                if (oc2 == "1") {
                    $("#oc2").show();
                }
                if (oc3 == "1") {
                    $("#oc3").show();
                }
                if (oc4 == "1") {
                    $("#oc4").show();
                }
                if (oc5 == "1") {
                    $("#oc5").show();
                }
            });
            function isEmpty(str) {
                debugger
                return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            }

            var currency = $("#<%=hdfcurrent.ClientID%>").val();
            function CountShipFee(type) {
                var shipfeendt = $("#<%= pCNShipFeeNDT.ClientID%>").val();
                var shipfeevnd = $("#<%= pCNShipFee.ClientID%>").val();
                if (type == "vnd") {
                    if (isEmpty(shipfeevnd) != true) {
                        var ndt = shipfeevnd / currency;
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pCNShipFee.ClientID%>").val(0);
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(shipfeendt)) {
                        var vnd = shipfeendt * currency;
                        $("#<%= pCNShipFee.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pCNShipFee.ClientID%>").val(0);
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(0);
                    }
                }
            }

            function CountCheckPro(type) {
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
            function CountRealPrice() {
                var rTotalPriceRealCYN = $("#<%= rTotalPriceRealCYN.ClientID%>").val();
                var rTotalPriceReal = $("#<%= rTotalPriceReal.ClientID%>").val();
                var newpriuce = rTotalPriceRealCYN * currency;
                $("#<%= rTotalPriceReal.ClientID%>").val(newpriuce);

            }
            function CountRealPrice1() {
                var rTotalPriceRealCYN = $("#<%= rTotalPriceRealCYN.ClientID%>").val();
                var rTotalPriceReal = $("#<%= rTotalPriceReal.ClientID%>").val();
                var newpriuce = rTotalPriceReal / currency;
                $("#<%= rTotalPriceRealCYN.ClientID%>").val(newpriuce);

            }
            function CountFeeBuyPro() {
                var pBuyNotDC = $("#<%= pBuyNDT.ClientID%>").val();
                var pBuyDC = $("#<%= pBuy.ClientID%>").val();

                var discountper = $("#<%= hdfFeeBuyProDiscount.ClientID%>").val();
                var subfee = pBuyNotDC * discountper / 100;
                var vnd = (pBuyNotDC - subfee) * currency;
                $("#<%= pBuy.ClientID%>").val(vnd);
            }
            function addordercode() {
                var count = 0;
                count = parseInt($("#<%=hdforderamount.ClientID %>").val());

                if (count == 5) {
                    return;
                }
                else {
                    count = count + 1;
                    $("#<%=hdforderamount.ClientID %>").val(count);
                    var occ = "oc" + count;
                    $("#" + occ + "").show();
                }
            }

            function CountFeeWeight(type) {
                var pWeightNDT = $("#<%= pWeightNDT.ClientID%>").val();
                var pWeightVND = $("#<%= pWeight.ClientID%>").val();
                var discountper = $("#<%= hdfFeeWeightDiscount.ClientID%>").val();

                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                var totalweight = parseFloat(pWeightNDT);

                if (totalweight > 0) {

                    //var leftweight = totalweight - 1;
                    var leftweight = totalweight;
                    var totalfeeweight = 0;
                    //var firstfeeweight = 100000;
                    var firstfeeweight = 0;
                    if (type == "kg") {
                        var steps = countfeearea.split('|');
                        if (steps.length > 0) {
                            for (var i = 0; i < steps.length - 1; i++) {
                                var step = steps[i];
                                var itemstep = step.split(',');
                                var wf = parseFloat(itemstep[1]);
                                var wt = parseFloat(itemstep[2]);
                                var amount = parseFloat(itemstep[3]);

                                if (totalweight >= wf && totalweight <= wt) {
                                    //totalfeeweight = firstfeeweight + (leftweight * amount);
                                    totalfeeweight = totalweight * amount;
                                }
                            }
                        }
                        var vnd = totalfeeweight;
                        var subfee = vnd * discountper / 100;
                        vnd = vnd - subfee;
                        $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(subfee));
                        $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(subfee));
                        $("#<%= pWeight.ClientID%>").val(vnd);
                    }
                }
                else {
                    $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(0));
                    $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(0));
                    $("#<%= pWeight.ClientID%>").val(0);
                }
            }
            function gettotalweight_old() {

                <%--var ocw = $("#<%= txtOrdertransactionCodeWeight.ClientID%>").val();
                var ocw2 = $("#<%= txtOrdertransactionCodeWeight2.ClientID%>").val();
                var ocw3 = $("#<%= txtOrdertransactionCodeWeight3.ClientID%>").val();
                var ocw4 = $("#<%= txtOrdertransactionCodeWeight4.ClientID%>").val();
                var ocw5 = $("#<%= txtOrdertransactionCodeWeight5.ClientID%>").val();--%>

                var ocw = "";
                var ocw2 = "";
                var ocw3 = "";
                var ocw4 = "";
                var ocw5 = "";

                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                //alert(countfeearea);

                if (isEmpty(ocw)) {
                    ocw = 0;
                }
                if (isEmpty(ocw2)) {
                    ocw2 = 0;
                }
                if (isEmpty(ocw3)) {
                    ocw3 = 0;
                }
                if (isEmpty(ocw4)) {
                    ocw4 = 0;
                }
                if (isEmpty(ocw5)) {
                    ocw5 = 0;
                }
                var totalweight = parseFloat(ocw) + parseFloat(ocw2) + parseFloat(ocw3) + parseFloat(ocw4) + parseFloat(ocw5);
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                //var firstfeeweight = 100000;
                var firstfeeweight = 0;
                var firstfeepacked = 20;

                var leftweight = totalweight;
                //var leftweight = totalweight - 1;


                var totalfeeweight = 0;

                var steps = countfeearea.split('|');
                if (steps.length > 0) {
                    for (var i = 0; i < steps.length - 1; i++) {
                        var step = steps[i];
                        var itemstep = step.split(',');
                        var wf = parseFloat(itemstep[1]);
                        var wt = parseFloat(itemstep[2]);
                        var amount = parseFloat(itemstep[3]);
                        if (totalweight >= wf && totalweight <= wt) {
                            totalfeeweight = firstfeeweight + (leftweight * amount);
                        }
                    }
                }

                var feepackedndt = leftweight * 1 + 20;
                var feepackedvnd = feepackedndt * currency;

                var pweightndt = totalfeeweight / currency;

                //$("#<%= pPackedNDT.ClientID %>").val(feepackedndt);
                //$("#<%= pPacked.ClientID %>").val(feepackedvnd);
                //$("#<%= pWeight.ClientID %>").val(totalfeeweight);
                $("#<%= pWeightNDT.ClientID %>").val(totalweight);

                $("#<%= txtOrderWeight.ClientID %>").val(totalweight);
                CountFeeWeight("kg");
            }
            function gettotalweight() {
                //txtOrderWeight, txtOrdertransactionCodeWeight
                var totalweight = 0;
                $(".transactionWeight").each(function () {
                    totalweight += parseFloat($(this).val());
                });
                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var shippingtype = $("#<%= hdfShippingType.ClientID%>").val();
                <%--var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }--%>
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                var firstfeeweight = 0;
                var firstfeepacked = 20;

                var leftweight = totalweight;
                var totalfeeweight = 0;

                var steps = countfeearea.split('|');
                if (steps.length > 0) {
                    for (var i = 0; i < steps.length - 1; i++) {
                        var step = steps[i];
                        var itemstep = step.split(',');
                        var wf = parseFloat(itemstep[1]);
                        var wt = parseFloat(itemstep[2]);
                        var amount = parseFloat(itemstep[3]);
                        if (totalweight >= wf && totalweight <= wt) {
                            totalfeeweight = firstfeeweight + (leftweight * amount);
                        }
                    }
                }

                var feepackedndt = leftweight * 1 + 20;
                var feepackedvnd = feepackedndt * currency;

                var pweightndt = totalfeeweight / currency;
                $("#<%= pWeightNDT.ClientID %>").val(totalweight);

                $("#<%= txtOrderWeight.ClientID %>").val(totalweight);
                CountFeeWeight("kg");
            }
            function gettotalweight2() {
                //txtOrderWeight, txtOrdertransactionCodeWeight
                var totalweight = $("#<%=txtOrderWeight.ClientID%>").val();

                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                var firstfeeweight = 0;
                var firstfeepacked = 20;

                var leftweight = totalweight;
                var totalfeeweight = 0;

                var steps = countfeearea.split('|');
                if (steps.length > 0) {
                    for (var i = 0; i < steps.length - 1; i++) {
                        var step = steps[i];
                        var itemstep = step.split(',');
                        var wf = parseFloat(itemstep[1]);
                        var wt = parseFloat(itemstep[2]);
                        var amount = parseFloat(itemstep[3]);
                        if (totalweight >= wf && totalweight <= wt) {
                            totalfeeweight = firstfeeweight + (leftweight * amount);
                        }
                    }
                }

                var feepackedndt = leftweight * 1 + 20;
                var feepackedvnd = feepackedndt * currency;

                var pweightndt = totalfeeweight / currency;
                $("#<%= pWeightNDT.ClientID %>").val(totalweight);

                $("#<%= txtOrderWeight.ClientID %>").val(totalweight);
                CountFeeWeight("kg");
            }
            function getplace() {
                var receiveValue = $("#<%= ddlReceivePlace.ClientID%>").val();
                var shippingTypeValue = $("#<%= ddlShippingType.ClientID%>").val();
                $("#<%= hdfReceivePlace.ClientID%>").val(receiveValue);
                $("#<%= hdfShippingType.ClientID%>").val(shippingTypeValue);
                gettotalweight();
                CountFeeWeight('kg');
            }
            function returnWeightFee() {
                var orderID = $("#<%= hdfOrderID.ClientID%>").val();
                var WarehouseFrom = $("#<%= ddlWarehouseFrom.ClientID%>").val();
                var receiveValue = $("#<%= ddlReceivePlace.ClientID%>").val();
                var shippingTypeValue = $("#<%= ddlShippingType.ClientID%>").val();
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                var totalweight = 0;
                $(".transactionWeight").each(function () {
                    totalweight += parseFloat($(this).val());
                });
                $.ajax({
                    type: "POST",
                    url: "/manager/orderdetail.aspx/CountFeeWeight",
                    data: "{orderID:'" + orderID + "',receivePlace:'" + receiveValue + "',shippingTypeValue:'" + shippingTypeValue + "',weight:'" + totalweight + "',WarehouseFrom:'" + WarehouseFrom + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        if (data != "none") {
                            var FeeWeightVND = data.FeeWeightVND;
                            var FeeWeightCYN = data.FeeWeightCYN;
                            var DiscountFeeWeightCYN = data.DiscountFeeWeightCYN;
                            var DiscountFeeWeightVN = data.DiscountFeeWeightVN;

                            //alert(FeeWeightVND + " - " + FeeWeightCYN + " - " + DiscountFeeWeightCYN + " - " + DiscountFeeWeightVN);
                            $("#<%=pWeightNDT.ClientID%>").val(totalweight);
                            $("#<%=pWeight.ClientID%>").val(FeeWeightVND);
                            $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(DiscountFeeWeightVN));
                            $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(DiscountFeeWeightVN));
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
                //alert(receiveValue + " - " + shippingTypeValue + " - " + currency + " - " + totalweight);
            }
            function copyToClipboard() {
                // Create new element
                var el = document.createElement('textarea');
                // Set value (string to be copied)
                el.value = $("#<%=hdfcopytext.ClientID%>").val();
                // Set non-editable to avoid focus and move outside of view
                el.setAttribute('readonly', '');
                el.style = { position: 'absolute', left: '-9999px' };
                document.body.appendChild(el);
                // Select text inside element
                el.select();
                // Copy text to clipboard
                document.execCommand('copy');
                // Remove temporary element
                document.body.removeChild(el);
            }
            //chat realtime
            $(document).ready(function () {
                $('#<%=txtComment.ClientID%>').on("keypress", function (e) {
                    if (e.keyCode == 13) {
                        SentMessageToLocal();
                    }
                });
                $('#<%=txtComment1.ClientID%>').on("keypress", function (e) {
                    if (e.keyCode == 13) {
                        SentMessageToCustomer();
                    }
                });
            });


            $(function () {
                var chat = $.connection.chatHub;
                chat.client.broadcastMessageFromUser = function (uid, id, message) {
                    var u = $("#<%= hdfID.ClientID%>").val();
                    console.log(message);
                    if (uid != u) {
                        var OrderID = $("#<%= hdfOrderID.ClientID%>").val();
                        if (id == OrderID) {
                            $("#ContactCustomer").append(message);

                            if ($("#client-chat").hasClass("hidden")) {
                                let $noti = $('<div class="toast-noti-fixed teal darken-1" ><p><span>Bạn có 1 tin nhắn mới từ <span>Khách hàng</span></span><a href="javascript:;" class="view-message" data-mess-id="#client-chat">Xem</a></p></div>');
                                $('body').append($noti);
                                setTimeout(function () {
                                    $noti.fadeOut('slow', function () {
                                        $(this).remove();
                                    })
                                }, 2000);
                                setTimeout(function () {
                                    $('.toast-noti-fixed').addClass('show');
                                }, 100);
                            }

                        }
                    }
                };
                chat.client.broadcastMessageForLocal = function (uid, id, message) {
                    var u = $("#<%= hdfID.ClientID%>").val();
                    if (uid != u) {
                        var OrderID = $("#<%= hdfOrderID.ClientID%>").val();
                        if (id == OrderID) {
                            $("#ContactLocal").append(message);
                            loadchat();
                            if ($("#local-chat").hasClass("hidden")) {
                                let $noti = $('<div class="toast-noti-fixed teal darken-1" ><p><span>Bạn có 1 tin nhắn mới từ <span>Nội bộ</span></span><a href="javascript:;" class="view-message" data-mess-id="#local-chat">Xem</a></p></div>');
                                $('body').append($noti);
                                setTimeout(function () {
                                    $noti.fadeOut('slow', function () {
                                        $(this).remove();
                                    })
                                }, 2000);
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

            ///unction isEmpty(str) {
            ///   return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            ///
            function SentMessageToCustomer() {

                var data = new FormData();
                var orderID = $("#<%=hdfOrderID.ClientID%>").val();
                var comment = $("#<%=txtComment1.ClientID%>").val();
                var files = $("#<%=ImgUpLoadToCustomer.ClientID%>").get(0).files;
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
                                $("#<%=ImgUpLoadToCustomer.ClientID%>").replaceWith($("#<%=ImgUpLoadToCustomer.ClientID%>").val('').clone(true));
                                sendCustomerComment(orderID, comment, url, real);

                            }
                        },
                        error: function (e) {
                            console.log(e)
                        }
                    });

                }
                else {
                    sendCustomerComment(orderID, comment, url, real);
                }

            }
            function sendCustomerComment(orderID, comment, url, real) {
                if (isEmpty(comment) && url == "") {
                    $(".info-show").html("Vui lòng điền nội dung.").attr("style", "color:red");
                }
                else {
                    $(".info-show").html("Đang cập nhật...").attr("style", "color:blue");
                    $.ajax({
                        type: "POST",
                        url: "/manager/OrderDetail.aspx/sendcustomercomment",
                        data: "{comment:'" + comment + "',id:'" + orderID + "',urlIMG:'" + url + "',real:'" + real + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var data = JSON.parse(msg.d);
                            if (data != null) {
                                var dataComment = data.Comment;
                                console.log(dataComment);
                                $("#ContactCustomer").append(dataComment);
                                //$(".materialboxed").materialbox({
                                //    inDuration: 200,
                                //    onOpenStart: function (element) {
                                //        $(element).parents('.chat-area.ps').attr('style', 'overflow:visible !important;');
                                //        $('.inside-chat').hide();
                                //    },
                                //    onCloseStart: function (element) {
                                //        $(element).parents('.chat-area.ps').attr('style', '');
                                //        $('.inside-chat').show();
                                //    }
                                //});
                                $("#imgUpToCustomer").html("");

                                $(".info-show").html("");
                                $("#<%=txtComment1.ClientID%>").val('');
                                //("#txtComment1").val("");

                            }
                            else {
                                $("#imgUpToCustomer").html("");
                                $(".info-show").html("Có lỗi trong quá trình gửi, vui lòng thử lại sau.").attr("style", "color:red");
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            console.log('lỗi checkend');
                        }
                    });
                }

            }

            function SentMessageToLocal() {
                var data = new FormData();
                var orderID = $("#<%=hdfOrderID.ClientID%>").val();
                var comment = $("#<%=txtComment.ClientID%>").val();
                var files = $("#<%=IMGUpLoadToLocal.ClientID%>").get(0).files;
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
                                $("#<%=IMGUpLoadToLocal.ClientID%>").replaceWith($("#<%=IMGUpLoadToLocal.ClientID%>").val('').clone(true));
                                sendStaffComment(orderID, comment, url, real);

                            }
                        },
                        error: function (e) {
                            console.log(e)
                        }
                    });

                }
                else {
                    sendStaffComment(orderID, comment, url, real);
                }


            }
            function sendStaffComment(orderID, comment, url, real) {

                if (isEmpty(comment) && url == "") {
                    $(".info-show-staff").html("Vui lòng điền nội dung.").attr("style", "color:red");
                }
                else {
                    $(".info-show-staff").html("Đang cập nhật...").attr("style", "color:blue");
                    $.ajax({
                        type: "POST",
                        url: "/manager/OrderDetail.aspx/sendstaffcomment",
                        data: "{comment:'" + comment + "',id:'" + orderID + "',urlIMG:'" + url + "',real:'" + real + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var data = JSON.parse(msg.d);
                            if (data != null) {
                                var dataComment = data.Comment;
                                $("#ContactLocal").append(dataComment);
                                //$(".materialboxed").materialbox({
                                //    inDuration: 200,
                                //    onOpenStart: function (element) {
                                //        $(element).parents('.chat-area.ps').attr('style', 'overflow:visible !important;');
                                //    },
                                //    onCloseStart: function (element) {
                                //        $(element).parents('.chat-area.ps').attr('style', '');
                                //    }
                                //});
                                $("#imgUpToLocal").html("");

                                $(".info-show-staff").html("");
                                //$("#txtComment").val("");
                                $("#<<%=txtComment.ClientID%>").val('');

                                //$('select').formSelect();

                            }
                            else {
                                $("#imgUpToLocal").html("");
                                $(".info-show-staff").html("Có lỗi trong quá trình gửi, vui lòng thử lại sau.").attr("style", "color:red");
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            console.log('lỗi checkend');
                        }
                    });
                }

            }

        </script>
    </telerik:RadScriptBlock>
    <asp:HiddenField ID="hdfcopytext" runat="server" />
    <style>
        .checklb input:checked + .ip-avata, .radiolb input:checked + .ip-avata {
            background: #fff;
            border-color: #484848;
        }

        .checklb .ip-avata {
            width: 17px;
            height: 17px;
        }

            .checklb .ip-avata:before {
                width: 6px;
                height: 6px;
            }
    </style>
</asp:Content>
