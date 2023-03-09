<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="ShippingRequestDetail.aspx.cs" Inherits="NHST.manager.ShippingRequestDetail" %>
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
                                            Họ tên
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtFullName"
                                                 CssClass="form-control" Enabled="true"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Email
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" Enabled="true"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Phone
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" Enabled="true"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Địa chỉ
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" Enabled="true"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Nội dung
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Trạng thái đơn hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Literal ID="ltrMainOrderStatus" runat="server"></asp:Literal>
                                        </div>
                                        <div class="form-row marbot1">
                                            Hình thức thanh toán
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlPTTT" runat="server" CssClass="form-control">                                                
                                                <asp:ListItem Value="1" Text="Chuyển khoản"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Thanh toán bằng tiền mặt khi nhận hàng"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row marbot1">
                                            Phương thức nhận hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlPTNH" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="Nhận hàng trực tiếp tại kho (kho xếp hàng ra trước)"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Giao hàng tận nhà (Áp dụng với khách Hà Nội và Hồ Chí Minh)"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Phương thức khác (Khách ghi chú cụ thể bên dưới)"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row marbot1">
                                            Trạng thái yêu cầu
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="Đã hủy"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Đang giao"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Đã giao"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                            <a href="/manager/request-homeshipping.aspx" class="btn primary-btn">Trở về</a>
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
    
</asp:Content>
