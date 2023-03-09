<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="lich-su-nap-tien1.aspx.cs" Inherits="NHST.lich_su_nap_tien" %>

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
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Lịch sử giao dịch</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <h2 class="content-title">Lịch sử giao dịch</h2>
            <div class="order-tool clearfix">
                <div class="primary-form custom-width">
                    <table class="customer-table mar-bot3 full-width font-size-16">
                        <tr style="font-weight: bold">
                            <td>Số dư tài khoản
                            </td>
                            <td>
                                <asp:Label ID="lblAccount" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="step-income">
                        <table class="customer-table mar-top1 full-width">
                            <tr>
                                <th width="20%" style="text-align: center">Ngày giờ</th>
                                <th width="20%" style="text-align: center">Nội dung</th>
                                <th width="20%" style="text-align: center">Số tiền</th>
                                <th width="20%" style="text-align: center">Loại giao dịch</th>
                                <th width="20%" style="text-align: center">Số dư</th>
                            </tr>
                            <asp:Literal ID="ltr" runat="server"></asp:Literal>                            
                        </table>
                        <div class="pagination">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%--<main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Lịch sử giao dịch</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    
                </div>
            </div>
        </div>
    </main>--%>
</asp:Content>
