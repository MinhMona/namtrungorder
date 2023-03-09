<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Chi-tiet-thanh-toan-ho.aspx.cs" Inherits="NHST.manager.Chi_tiet_thanh_toan_ho" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                                    <h3 class="lb">Chi tiết yêu cầu</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            <div class="lb" style="float: left; width: 18%">Username: </div>
                                            <div class="info">
                                                <asp:Literal ID="lblUsername" runat="server"></asp:Literal>
                                                <%--<asp:Label ID="lblUsername" runat="server"></asp:Label>--%>
                                            </div>
                                        </div>
                                        <div class="form-row marbot2">
                                        </div>
                                        <asp:Literal ID="ltrList" runat="server"></asp:Literal>
                                        <div class="form-row marbot1">
                                            Tổng tiền (tệ) <span>
                                                <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="pPriceCYN" ForeColor="Red"
                                                    ErrorMessage="(*)" ValidationGroup="n" Display="Dynamic"></asp:RequiredFieldValidator></span>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="pPriceCYN" NumberFormat-DecimalDigits="2" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Tổng tiền (VNĐ) <span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pPriceVND" ForeColor="Red"
                                                    ErrorMessage="(*)" ValidationGroup="n" Display="Dynamic"></asp:RequiredFieldValidator></span>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="pPriceVND" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Giá tệ (VNĐ) <span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pCurrency" ForeColor="Red"
                                                    ErrorMessage="(*)" ValidationGroup="n" Display="Dynamic"></asp:RequiredFieldValidator></span>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="pCurrency" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Giá tệ thật
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="rrealPriceCYN" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Tỉ giá thật
                                        </div>
                                        <div class="form-row marbot2">
                                            <span class="realCurrency form-control">0</span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Tổng tiền thật
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="rRealTotalPrice" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Tổng tiền trả cuối
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="rFinalPayPrice" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số đt
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Ghi chú
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtSummary" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Chưa hoàn thiện
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:CheckBox ID="chkIsNotComplete" runat="server" />
                                        </div>
                                        <div class="form-row marbot1">
                                            Trạng thái
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Đã hủy" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Chưa thanh toán" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Đã xác nhận" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Đã thanh toán" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Hoàn thành" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <a href="javascript:;" class="btn btn-success" onclick="update()">Cập nhật</a>
                                            <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click"
                                                Style="display: none;" />
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                    <div class="feat-row grid-row" style="display: none">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Lịch sử thay đổi nơi đến</h3>
                                </div>
                                <table class="normal-table panel-table">
                                    <tr>
                                        <th class="pro">Ngày thay đổi</th>
                                        <th class="pro">Trạng thái cũ</th>
                                        <th class="pro">Trạng thái mới</th>
                                        <th class="qty">Người đổi</th>
                                    </tr>
                                    <asp:Repeater ID="rptPayment" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="pro">
                                                    <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td class="pro">
                                                    <%# Eval("OldeStatusText").ToString() %>
                                                </td>
                                                <td class="pro">
                                                    <%#Eval("NewStatusText").ToString() %>
                                                </td>
                                                <td class="qty"><%#Eval("Username") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                                </table>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </asp:Panel>
    <asp:HiddenField ID="hdfList" runat="server" />
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlDVT">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlDVT" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblUnit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery-1.11.0.min.js"></script>
        <script>

            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Admin/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
        <script type="text/javascript">
            function update() {
                var list = "";
                $(".itemyeuau").each(function () {
                    var id = $(this).attr("data-id");
                    var des1 = $(this).find(".txtDesc1").val();
                    var des2 = $(this).find(".txtDesc2").val();
                    list += id + "," + des1 + "," + des2 + "|";
                });
                $("#<%=hdfList.ClientID%>").val(list);
                $("#<%=btnSave.ClientID%>").click();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
