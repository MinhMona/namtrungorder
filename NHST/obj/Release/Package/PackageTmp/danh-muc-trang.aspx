<%@ Page Title="" Language="C#" MasterPageFile="~/NamTrungMaster.Master" AutoEventWireup="true" CodeBehind="danh-muc-trang.aspx.cs" Inherits="NHST.danh_muc_trang1" %>

<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .services {
            background: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <div class="banner1">
        </div>
        <%--<div class="banner">
            <div class="all">
                <div class="banner-content">
                    <div class="bn__header">
                        Order Hàng Trung quốc
            <h1 class="hd">Giá rẻ - Nhanh chóng - Uy tín</h1>
                    </div>
                    <div class="line-br line-white">
                        <span class="line__icon"></span>
                    </div>
                    <div class="bn__sub">
                        <p>Giúp khách hàng nhập hàng tận gốc</p>
                        <p>Phí dịch vụ chỉ <span class="color2">từ 1%</span> - Vận chuyển từ <span class="color2">8K/KG</span></p>
                    </div>
                    <div class="addon-button">
                        <a href="" class="mn-btn gg-btn"><i class="fab fa-chrome"></i>Chrome</a>
                        <a href="" class="mn-btn cc-btn"><i class="fab fa-chrome"></i>Cốc cốc</a>
                    </div>
                </div>
            </div>
        </div>--%>
        <section id="firm-services" class="services" style="padding: 30px 0;">
            <div class="all">
                <div class="breakcrum">
                    <a href="/" class="brc-home"><i class="fa fa-home"></i>Trang chủ</a>
                    <span class="brc-seperate"><i class="fa fa-arrow-right" style="font-size: 10px;"></i></span>
                    <asp:Literal ID="ltrbre" runat="server"></asp:Literal>

                </div>
                <h4 class="sec__title center-txt">
                    <asp:Label ID="lblTitle" runat="server" EnableViewState="false"></asp:Label></h4>
                <div class="line-br">
                    <span class="line__icon"></span>
                </div>
                <div class="primary-form">
                    <div class="services-page clearfix">
                        <uc:SideBar ID="SideBar1" runat="server" />
                        <div class="services-content">
                            <asp:Literal ID="ltrSummary" runat="server" EnableViewState="false"></asp:Literal>

                            <div class="services-list clearfix">
                                <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                <div class="pagination">
                                    <%this.DisplayHtmlStringPaging1();%>
                                </div>
                            </div>
                            <div class="cmt" style="display: none">
                                <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                                <%--<img src="/App_Themes/pdv/assets/images/cmt.jpg" alt="#">--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </main>
</asp:Content>
