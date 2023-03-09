<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="UserWallet.aspx.cs" Inherits="NHST.manager.UserWallet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-50 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Nạp tiền vào ví</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot1">
                                        Tên đăng nhập / Nickname
                                    </div>
                                    <div class="form-row marbot2">
                                        <strong><asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                                    </div>
                                    <div class="form-row marbot1">
                                        Số tiền
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pWallet" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Nội dung
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                            Height="400px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
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
                                                <asp:ListItem Value="1" Text="Đang chờ"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Duyệt chuyển"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Hủy"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-row center-txt margin-top20">
                                        <asp:Button ID="Button1" runat="server" Text="Nạp tiền" CssClass="btn primary-btn"
                                            OnClick="btncreateuser_Click" />
                                        <a href="/manager/userlist.aspx" class="btn primary-btn">Hủy</a>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
