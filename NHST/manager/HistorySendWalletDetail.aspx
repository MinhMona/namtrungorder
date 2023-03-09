<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="HistorySendWalletDetail.aspx.cs" Inherits="NHST.manager.HistorySendWalletDetail" %>
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
                                    <h3 class="lb">Nạp tiền vào wallet User</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Tên đăng nhập / Nickname
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số tiền
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pWallet" MinValue="0" NumberFormat-DecimalDigits="0" Enabled="false"
                                                NumberFormat-GroupSizes="3" Width="100%">
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
                                        <asp:Panel ID="pbadmin" runat="server">
                                            <div class="form-row marbot1">
                                                Trạng thái
                                            </div>
                                            <div class="form-row marbot2">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="2" Text="Duyệt chuyển"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Đang chờ"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Hủy"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn"
                                                OnClick="btncreateuser_Click" />
                                        </div>
                                    </div>
                                </div>
                            </article>
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
    </script>
</asp:Content>
