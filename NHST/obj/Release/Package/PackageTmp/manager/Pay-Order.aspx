<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Pay-Order.aspx.cs" Inherits="NHST.manager.Pay_Order" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Thanh toán đơn hàng</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Mã đơn hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblMainOrderID" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Tổng hóa đơn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblTotalPriceVND" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Người đặt hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblUsername" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Loại thanh toán
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" onchange="showprice()">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1">
                                            Số tiền phải đặt cọc
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblAmountDeposit" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số tiền phải thanh toán
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblMusPay" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Phương thức thanh toán
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control select2" onchange="showprice()">
                                                <%--<asp:ListItem Value="1" Text="Trực tiếp"></asp:ListItem>--%>
                                                <asp:ListItem Value="2" Text="Ví điện tử"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1">
                                            Số tiền
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="pAmount" MinValue="0" NumberFormat-DecimalDigits="0"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Tiền thanh toán">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Nội dung
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                                Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                                DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                                <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                            </telerik:RadEditor>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button ID="btnPay" runat="server" Text="Thanh toán" CssClass="btn primary-btn"
                                                OnClick="btnPay_Click" />
                                            <asp:Button ID="btnback" runat="server" Text="Chi tiết đơn hàng" CssClass="btn primary-btn"
                                                OnClick="btnback_Click" />
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        <asp:HiddenField ID="hdfshow" runat="server" />
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <script type="text/javascript">
        function keypress(e) {
            var keypressed = null;
            if (window.event) {
                keypressed = window.event.keyCode; //IE
            }
            else {
                keypressed = e.which; //NON-IE, Standard
            }
            if (keypressed < 48 || keypressed > 57) {
                if (keypressed == 8 || keypressed == 127) {
                    return;
                }
                return false;
            }
        }
        $(document).ready(function () {
            var show = $("#<%=hdfshow.ClientID%>").val();
            $("#" + show).show();
        });
            function showprice() {
                var val = $("#<%= ddlStatus.ClientID%>").val();
                if (val == 2) {
                    $("#pndeposit").show();
                    $("#pnPayall").hide();
                }
                else {
                    $("#pndeposit").hide();
                    $("#pnPayall").show();
                }
            }
    </script>
</asp:Content>
