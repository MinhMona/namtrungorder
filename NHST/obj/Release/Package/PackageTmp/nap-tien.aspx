<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="nap-tien.aspx.cs" Inherits="NHST.nap_tien1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Nạp tiền</h4>
                <div class="primary-form">
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
                                <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>
                                <%--<h1 class="font-size-16">Cách 1: Chuyển khoản qua ngân hàng</h1>
                        <table class="customer-table mar-top1">
                            <tr>
                                <th width="10%">Ngân hàng</th>
                                <th width="60%">Thông tin tài khoản</th>
                            </tr>
                            <tr>
                                <td>
                                    <ul class="cardList clearfix">
                                        <li><i title="Ngân hàng TMCP Ngoại Thương Việt Nam" class="VCB"></i></li>
                                    </ul>
                                </td>
                                <td>
                                    <strong class="font-size-16">Ngân hàng TMCP Ngoại Thương Việt Nam</strong><br />
                                    <span class="label-NH">Số tài khoản: </span><i>0012345789456</i>
                                    <br />
                                    <span class="label-NH">Chủ tài khoản: </span><i>Nguyễn Demo</i><br />
                                    <span class="label-NH">Chi nhánh: </span><i>Chi nhánh Sài Gòn</i><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ul class="cardList clearfix">
                                        <li><i class="TCB" title="Ngân hàng thương mại cổ phần Kỹ Thương Việt Nam"></i></li>
                                    </ul>
                                </td>
                                <td>
                                    <strong class="font-size-16">Ngân hàng thương mại cổ phần Kỹ Thương Việt Nam</strong><br />
                                    <span class="label-NH">Số tài khoản: </span><i>01234567897454</i><br />
                                    <span class="label-NH">Chủ tài khoản: </span><i>Nguyễn Demo</i><br />
                                    <span class="label-NH">Chi nhánh: </span><i>Chi nhánh Sài Gòn</i><br />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <ul class="cardList clearfix">
                                        <li><i title="Ngân hàng TMCP Công Thương Việt Nam" class="ICB"></i></li>
                                    </ul>
                                </td>
                                <td>
                                    <strong class="font-size-16">Ngân hàng TMCP Công Thương Việt Nam</strong><br />
                                    <span class="label-NH">Số tài khoản: </span><i>100011121578</i><br />
                                    <span class="label-NH">Chủ tài khoản: </span><i>Nguyễn Demo</i><br />
                                    <span class="label-NH">Chi nhánh: </span><i>Chi nhánh Sài Gòn</i><br />
                                </td>
                            </tr>
                        </table>
                        <div class="row-1col mar-top2">
                            <label class="width-full font-size-18">
                                <strong>Nội dung chuyển khoản: NAP Username "Số điện thoại của bạn (không bắt buộc, dùng để PĐV liên hệ khi gặp sự cố)"</strong>
                                <br />
                                <br />
                                <span class="example">Ví dụ: NAP
                                    <asp:Label ID="lblIDUser" runat="server"></asp:Label>
                                    0912834567</span>
                            </label>
                        </div>--%>
                            </div>
                            <%--<div class="step-income">
                        <h1 class="font-size-16 mar-top3">Cách 2: Chuyển khoản qua ATM hoặc các dịch vụ khác</h1>
                        <div class="row-1col mar-top2">
                            <label class="width-full font-size-18">
                                Quý khách hàng thực hiện chuyển khoản qua ATM hoặc các dịch vụ không có ghi chú, vui lòng liên hệ thông báo cho bộ phận DVKH
                            </label>
                        </div>
                    </div>
                    <div class="step-income">
                        <h1 class="font-size-16 mar-top3">Cách 3: Chuyển khoản qua ATM hoặc các dịch vụ khác</h1>
                        <div class="row-1col mar-top2">
                            <label class="width-full font-size-18">
                                Quý khách có thể nạp tiền trực tiếp tại văn phòng giao dịch của alivietnam. Để đảm bảo an toàn về mặt tài chính cho Quý khách, 
                                chúng tôi khuyến cáo với các lần nạp có số tiền hơn 10 triệu đồng, Quý khách vui lòng sử dụng hình thức chuyển khoản.<br />
                                Xin cảm ơn!

                            </label>
                        </div>
                    </div>--%>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Nạp tiền</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
</asp:Content>
