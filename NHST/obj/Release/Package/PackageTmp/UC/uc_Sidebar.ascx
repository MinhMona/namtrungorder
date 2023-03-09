<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Sidebar.ascx.cs" Inherits="NHST.UC.uc_Sidebar" %>
<div class="sidebar">
    <%--<div class="sidebar-box">
        <h3 class="sidebar-title">hỗ trợ mua hàng</h3>
        <div class="box-detail">
            <p class="sidebar-call">
                <a href="tel:+0246671888">
                    <img src="/App_Themes/pdv/assets/images/call.png" alt="#">
                    hostline:
                            <br>
                    <span>024.6671.888</span></a>
            </p>
            <div class="address">
                <h3>MIỀN BẮC</h3>
                <p><span class="name">HOÀNG:</span> <span class="tell-sp">0965 888 666</span> <span>| </span>kd1@gmail.com</p>
                <p><span class="name">TUẤN: </span><span class="tell-sp">0989 333 666 </span><span>| </span>kd2@gmail.com</p>
                <p><span class="name">TRANG: </span><span class="tell-sp">0941 321 456 </span><span>| </span>kd3@gmail.com</p>
                <div class="sv-link">
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-face.png" alt="#"></a>
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-zalo.png" alt="#"></a>
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-sky.png" alt="#"></a>
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-viber.png" alt="#"></a>
                </div>
            </div>
            <div class="address">
                <h3>MIỀN nam</h3>
                <p><span class="name">HOÀNG:</span> <span class="tell-sp">0965 888 666</span> <span>| </span>kd1@gmail.com</p>
                <p><span class="name">TUẤN: </span><span class="tell-sp">0989 333 666 </span><span>| </span>kd2@gmail.com</p>
                <p><span class="name">TRANG: </span><span class="tell-sp">0941 321 456 </span><span>| </span>kd3@gmail.com</p>
                <div class="sv-link">
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-face.png" alt="#"></a>
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-zalo.png" alt="#"></a>
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-sky.png" alt="#"></a>
                    <a href="#">
                        <img src="/App_Themes/pdv/assets/images/sv-viber.png" alt="#"></a>
                </div>
            </div>
        </div>
    </div>--%>
    <div class="sidebar-box">
        <h3 class="sidebar-title">Danh mục</h3>
        <asp:Literal ID="ltrCategory" runat="server" EnableViewState="false"></asp:Literal>
    </div>

    <div class="sidebar-box">
        <h3 class="sidebar-title">bài viết mới</h3>
        <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
       
    </div>

    <%--<div class="sidebar-box">
        <h3 class="sidebar-title">FANPAGE FACEBOOK</h3>
        <div class="sidebar-fanpage">
            <iframe src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2Ffacebook&tabs=timeline&width=260px&height=200px&small_header=true&adapt_container_width=true&hide_cover=false&show_facepile=true&appId=163887910846059" width="260px" height="200px" style="border: none; overflow: hidden" scrolling="no" frameborder="0" allowtransparency="true"></iframe>
        </div>
    </div>--%>
</div>
