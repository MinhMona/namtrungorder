<%@ Page Language="C#"  MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Giaodich.aspx.cs" Inherits="NHST.manager.Giaodich" %>

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
                                <h3 class="lb">Thông tin giao dịch</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    
                                    <div class="form-row marbot2">
                                        <strong><asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                                    </div>
                                    <div class="form-row marbot1">
                                        Giá trị giao dịch
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pAmount" MinValue="0" NumberFormat-DecimalDigits="0"
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
                                            Loại định khoản
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlLoaidinhkhoan" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataTextField="TenDinhKhoan"
                                                    DataValueField="ID">
                                                </asp:DropDownList>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-row center-txt margin-top20">
                                        <asp:Button ID="Button1" runat="server" Text="Cập nhất" CssClass="btn primary-btn"
                                            OnClick="btncreateuser_Click" />
                                        <a href="/manager/orderlist.aspx" class="btn primary-btn">Hủy</a>
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
