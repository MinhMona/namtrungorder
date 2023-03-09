<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="admin-noti.aspx.cs" Inherits="NHST.manager.admin_noti" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Thông báo của bạn</h4>
                <div class="primary-form custom-width">
                    <div class="step-income">
                        <table class="customer-table mar-top1 full-width normal-table">
                            <tr>
                                <th>Ngày</th>
                                <th>Mã đơn hàng</th>
                                <th>Nội dung</th>
                                <th>Trạng thái</th>
                            </tr>
                            <asp:Literal ID="ltr" runat="server"></asp:Literal>
                        </table>
                        <div class="pagination">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>

    <%-- <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Thông báo của bạn</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    
                </div>
            </div>
        </div>
    </main>--%>
</asp:Content>
