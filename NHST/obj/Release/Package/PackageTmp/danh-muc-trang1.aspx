<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterNotLogin.Master" AutoEventWireup="true" CodeBehind="danh-muc-trang1.aspx.cs" Inherits="NHST.danh_muc_trang" %>

<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="services-page clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p>
                    <a href="/trang-chu" class="color-black">Trang chủ</a> - <span>
                        <asp:Label ID="lblTitle" runat="server" EnableViewState="false"></asp:Label></span>
                </p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <uc:SideBar ID="SideBar1" runat="server" />
            <div class="services-content">
                <asp:Literal ID="ltrSummary" runat="server" EnableViewState="false"></asp:Literal>
                <%--<h3 class="order-title">dich vu cua alivietnam</h3>
                <p>
                    Hiện các công ty về dịch vụ vận chuyển Trung Việt ra đời ngày càng nhiều giúp các tiểu thương và những tín đồ kinh doanh online hàng hóa có xuất xứ từ Trung Quốc thuận lợi hơn. Để chọn được một công ty uy tín chuyên về dịch vụ vận chuyển từ Trung Quốc về Việt Nam đòi hỏi mỗi người phải thực sự thông thái.
					Davitrans là một địa chỉ uy tín luôn nhận được sự tí n nhiệm cao từ cộng đồng người dùng bởi chất lượng dịch vụ tận tâm hết mình và luôn có mức phí thấp hơn nhiều so với giá trên thị trường. Bởi các nhân viên của Davitrans đều tự mình đảm nhiệm toàn bộ quá trình vận chuyển hàng hóa, không phải qua bất cứ bộ phận trung gian nào. Tại Davitrans có đầy đủ các dịch vụ vận chuyển Trung Việt gồm
                </p>--%>
                <div class="services-list clearfix">
                    <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                    <div class="pagination">
                        <%this.DisplayHtmlStringPaging1();%>
                    </div>
                </div>
                <div class="cmt">
                    <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                    <%--<img src="/App_Themes/pdv/assets/images/cmt.jpg" alt="#">--%>
                </div>
            </div>

        </div>
    </section>
</asp:Content>
