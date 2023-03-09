<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Present.aspx.cs" Inherits="NHST.manager.Present" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Cấu hình hệ thống</h1>
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Cấu hình hệ thống</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">

                                    <div class="form-row marbot1">
                                        Năm kinh nghiệm
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pYear" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="pYear" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Số lượng khách hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pQuantityCustomer" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="pQuantityCustomer" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Số lượng đơn hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pQuantityOrder"  NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="pQuantityOrder" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn" OnClick="btncreateuser_Click" />
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
