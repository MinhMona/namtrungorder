<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterNotLogin.Master" AutoEventWireup="true" CodeBehind="chi-tiet-trang2.aspx.cs" Inherits="NHST.chi_tiet_trang1" %>

<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        p {
            text-align: initial;
        }

        .intro-page table {
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="services-page clearfix">
        <div class="container">
            <div class="intro-page">
                <div class="breadcrumb clearfix">
                    <p>
                        <a href="/trang-chu" class="color-black">Trang chủ</a> - <span>
                            <asp:Label ID="lblTitle" runat="server"></asp:Label></span>
                    </p>
                    <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
                </div>
            </div>
            <uc:SideBar ID="SideBar1" runat="server" />
            <div class="services-content intro-align">
                <div class="intro-page">
                    <asp:Literal ID="ltrDetail" runat="server"></asp:Literal>
                </div>
                <div class="cmt">
                    <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                    <%--<img src="/App_Themes/pdv/assets/images/cmt.jpg" alt="#">--%>
                </div>
                <div class="other-page">
                    <div class="line-head black-gray"></div>
                    <h2 class="other-header-title">Bài viết cùng chuyên mục</h2>
                    <ul class="list-other-news">
                        <asp:Literal ID="ltrNewsOther" runat="server"></asp:Literal>
                        <%--<li>
                            <a href="javascript:;">Bài viết cùng chuyên mục</a>
                        </li>
                        <li>
                            <a href="javascript:;">Bài viết cùng chuyên mục</a>
                        </li>
                        <li>
                            <a href="javascript:;">Bài viết cùng chuyên mục</a>
                        </li>--%>
                    </ul>
                </div>
            </div>

        </div>
    </section>
</asp:Content>
