<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Add-Package.aspx.cs" Inherits="NHST.manager.Add_Package" %>

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
                                <h3 class="lb">Tạo bao hàng</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot1">
                                        Mã bao hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtPackageCode" CssClass="form-control has-validate" placeholder="Mã bao hàng"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPackageCode" Display="Dynamic"
                                                 ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Cân (kg)
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Khối (m3)
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Khối (m3)" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </div>                                    
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="Button1" runat="server" Text="Tạo mới" CssClass="btn primary-btn"
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
</asp:Content>
