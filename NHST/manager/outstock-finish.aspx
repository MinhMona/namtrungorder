<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="outstock-finish.aspx.cs" Inherits="NHST.manager.outstock_finish" %>

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
                        <div class="grid-col-30 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Chi tiết phiên xuất kho</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Họ tên người đến nhận
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control has-validate" 
                                                placeholder="Họ tên người nhận">
                                            </asp:TextBox>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtFullname"
                                                    Display="Dynamic" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số điện thoại người đến nhận
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control has-validate" 
                                                placeholder="Số điện thoại">
                                            </asp:TextBox>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtPhone"
                                                    Display="Dynamic" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button ID="btnPrint" runat="server" Text="In phiếu Xuất kho" CssClass="btn primary-btn"
                                                OnClick="btnPrint_Click" UseSubmitBehavior="false" />
                                            <asp:Panel ID="pnButton" runat="server" Visible="false">
                                                <asp:Button ID="btncreateuser" runat="server" Text="Xuất kho" CssClass="btn primary-btn"
                                                    OnClick="btncreateuser_Click" UseSubmitBehavior="false" />
                                            </asp:Panel>
                                            <asp:Panel ID="pnrefresh" runat="server" Visible="false">
                                                <asp:Button ID="btnRefresh" runat="server" Text="Reload" CssClass="btn primary-btn"
                                                    OnClick="btnRefresh_Click" UseSubmitBehavior="false" />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                        <div class="grid-col-70 grid-row-center">
                            <asp:Literal ID="ltrList" runat="server"></asp:Literal>
                           <%-- <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Đơn hàng mua hộ: #12</h3>
                                </div>
                                <article class="pane-primary">
                                    <table class="normal-table full-width">
                                        <tr>
                                            <th>Mã kiện
                                            </th>
                                            <th>Cân nặng (kg)
                                            </th>
                                            <th>Ngày lưu kho (ngày)
                                            </th>
                                            <th>Trạng thái
                                            </th>
                                            <th>Tiền lưu kho
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>123123123213
                                            </td>
                                            <td>5
                                            </td>
                                            <td>10</td>
                                            <td><span class="bg-green">Đã về kho đích</span></td>
                                            <td>100.000</td>
                                        </tr>
                                        <tr>
                                            <td>123123123213
                                            </td>
                                            <td>5
                                            </td>
                                            <td>10</td>
                                            <td><span class="bg-green">Đã về kho đích</span></td>
                                            <td>100.000</td>
                                        </tr>
                                        <tr style="font-size: 18px; text-transform: uppercase">
                                            <td colspan="4">Tổng tiền
                                            </td>
                                            <td>100.000 vnđ</td>
                                        </tr>
                                    </table>
                                </article>
                            </article>
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Đơn hàng VC hộ: #12</h3>
                                </div>
                                <article class="pane-primary">
                                    <table class="normal-table full-width">
                                        <tr>
                                            <th>Mã kiện
                                            </th>
                                            <th>Cân nặng (kg)
                                            </th>
                                            <th>Ngày lưu kho (ngày)
                                            </th>
                                            <th>Trạng thái
                                            </th>
                                            <th>Tiền lưu kho
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>123123123213
                                            </td>
                                            <td>5
                                            </td>
                                            <td>10</td>
                                            <td><span class="bg-green">Đã về kho đích</span></td>
                                            <td>100.000</td>
                                        </tr>
                                        <tr>
                                            <td>123123123213
                                            </td>
                                            <td>5
                                            </td>
                                            <td>10</td>
                                            <td><span class="bg-green">Đã về kho đích</span></td>
                                            <td>100.000</td>
                                        </tr>
                                        <tr style="font-size: 18px; text-transform: uppercase">
                                            <td colspan="4">Tổng tiền
                                            </td>
                                            <td>100.000 vnđ</td>
                                        </tr>
                                    </table>
                                </article>
                            </article>--%>
                        </div>

                    </div>
                </div>
            </div>
        </main>
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
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
        function getcode(obj) {
            var val = obj.val();
            //alert(val);
            val += ";";
            obj.val(val);
        }
        function VoucherSourcetoPrint(source) {
            var r = "<html><head><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style.css\" type=\"text/css\"/><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style-p.css\" type=\"text/css\"/><script>function step1(){\n" +
                    "setTimeout('step2()', 10);}\n" +
                    "function step2(){window.print();window.close()}\n" +
                    "</scri" + "pt></head><body onload='step1()'>\n" +
                    "" + source + "</body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }       
    </script>
    <style>
        .text-align-right
        {
            text-align:right;
        }
        .form-control
        {
            background:#fff;
        }
    </style>
</asp:Content>
