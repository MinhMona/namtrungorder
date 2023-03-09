<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="lich-su-giao-dich-tien-te.aspx.cs" Inherits="NHST.lich_su_giao_dich_tien_te" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Lịch sử giao dịch (tệ)</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div id="primary" class="page trans-history">
                            <div class="container">
                                <div class="main-content policy clear">
                                    <div class="search-table">
                                        <aside class="filters">
                                            <ul>
                                                <li class="lbl"><b class="black">Lọc tìm kiếm</b></li>
                                                <li>
                                                    <asp:DropDownList ID="ddlQuy" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Tháng 1"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tháng 2"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Tháng 3"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Tháng 4"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Tháng 5"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Tháng 6"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="Tháng 7"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="Tháng 8"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="Tháng 9"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Tháng 10"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="Tháng 11"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="Tháng 12"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<select>
                                        <option value="Tất cả">Tất cả</option>
                                    </select>--%>
                                                </li>
                                                <li class="submit">
                                                    <asp:Button ID="btnFilter" runat="server" CssClass="pill-btn btn order-btn main-btn hover submit-btn" Text="LỌC TÌM KIẾM" OnClick="btnFilter_Click" />
                                                </li>
                                            </ul>
                                        </aside>
                                        <div class="res-table">
                                            <table class="customer-table mar-bot3 full-width font-size-16">
                                                <tr style="font-weight: bold">
                                                    <td>Số dư tài khoản
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="table-rps table-responsive">
                                            <table class="customer-table mar-top1 full-width normal-table">
                                                <thead>
                                                    <tr>
                                                        <th>Ngày</th>
                                                        <th>Nội dung</th>
                                                        <%--<th>Ghi chú</th>--%>
                                                        <th>Số tiền</th>
                                                        <th>Loại giao dịch</th>
                                                        <th>Số dư</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="tbl-footer clear">
                                            <div class="subtotal fr">
                                                <asp:Literal ID="ltrTotal" runat="server"></asp:Literal>
                                            </div>
                                            <div class="pagenavi fl">
                                                <%this.DisplayHtmlStringPaging1();%>
                                            </div>
                                        </div>
                                        <!--tbl-footer-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <style>
        .page .filters {
            background: #ebebeb;
            border: 1px solid #e1e1e1;
            font-weight: bold;
            padding: 20px;
            margin-bottom: 20px;
        }

            .page .filters ul li {
                display: inline-block;
                text-align: center;
                padding-right: 2px;
            }

                .page .filters ul li.lbl {
                    padding-right: 25px;
                    color: #2a363b;
                }

        select.form-control {
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            -ms-appearance: none;
            -o-appearance: none;
            background: url(../images/icon-select.png) no-repeat right 15px center;
            padding: 0;
            padding-right: 25px;
            padding-left: 15px;
            line-height: 40px;
        }

        .page .filters select {
            background: url(../images/red-dropdown-bg.png) #fff no-repeat right 30px center;
            width: 270px;
            padding-left: 30px;
        }
    </style>
</asp:Content>
