<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="khieu-nai1.aspx.cs" Inherits="NHST.khieu_nai" %>

<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Khiếu nại</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <h2 class="content-title">Khiếu nại</h2>
            <div class="order-tool clearfix">
                <div class="primary-form custom-width">
                    <a href="/them-khieu-nai" class="btn pill-btn primary-btn admin-btn">Thêm mới</a>
                    <div class="step-income">
                        <table class="customer-table mar-top1 full-width">
                            <tr>
                                <th>Ngày</th>
                                <th>Mã Shop</th>
                                <th>Tiền bồi thường</th>
                                <th>Nội dung</th>
                                <th>Trạng thái</th>
                            </tr>
                            <tbody>
                                <asp:Literal ID="ltr" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                        <div class="pagination">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
