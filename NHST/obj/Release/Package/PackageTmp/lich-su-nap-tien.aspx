<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="lich-su-nap-tien.aspx.cs" Inherits="NHST.lich_su_nap_tien1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Lịch sử giao dịch</h4>
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




                            <%--<a href="javascript:;" class="btn" id="filter-btn">Bộ lọc</a>
                            <div class="filter-wrap mb-2">
                                <div class="row">
                                    <div class="input-field col s6 l4">
                                        <asp:TextBox runat="server" ID="FD" placeholder="" CssClass="datetimepicker from-date"></asp:TextBox>
                                        <label>Từ ngày</label>
                                    </div>
                                    <div class="input-field col s6 l4">
                                        <asp:TextBox runat="server" placeholder="" ID="TD" CssClass="datetimepicker to-date"></asp:TextBox>
                                        <label>Đến ngày</label>
                                        <span class="helper-text"
                                            data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                    </div>
                                    <div class="input-field col s12 l4">
                                        <asp:DropDownList runat="server" ID="ddlStatus">
                                            <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Đặt cọc"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Thanh toán"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Cộng tiền"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Trừ tiền"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="status">Trạng thái</label>
                                    </div>

                                    <div class="col s12 right-align">
                                        <asp:Button ID="btnSear" runat="server"
                                            CssClass="btn" OnClick="btnSear_Click" Text="Lọc kết quả" />
                                    </div>
                                </div>
                            </div>--%>

                            <%--    <a href="javascript:;" class="btn right primary-btn" id="filter-form-toggle"><i class="fa fa-filter"></i>Bộ lọc</a>--%>
                            <a href="javascript:;" class="btn primary-btn" id="filter-form-toggle">Bộ lọc</a>
                            <asp:Button ID="btnExcel" runat="server" class="btn primary-btn" Text="Xuất Excel" OnClick="btnExcel_Click"></asp:Button>
                            <div class="pane-shadow filter-form" id="filter-form" style="display: none">
                                <div class="grid-row">

                                    <%--<li class="width-20-per">
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </li>
                                    <li class="width-20-per">
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rTD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </li>--%>
                                    <div class="grid-col-50">
                                        <div class="inline-date">
                                            <div class="lb">Từ ngày:</div>
                                            <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                                <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%" >
                                                </DateInput>
                                            </telerik:RadDateTimePicker>
                                        </div>
                                    </div>

                                    <div class="grid-col-50">
                                        <div class="inline-date">
                                            <div class="lb">Đến ngày:</div>
                                            <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rTD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                                                <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                                </DateInput>
                                            </telerik:RadDateTimePicker>
                                        </div>
                                    </div>


                                    <div class="grid-col-50">
                                        <div class="lb">trạng thái</div>
                                        <div class="control-with-suffix">
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Đặt cọc"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Thanh toán hóa đơn"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Tiền đã cộng"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Tiền đã trừ"></asp:ListItem>
                                            </asp:DropDownList>
                                            <span class="suffix hl-txt"><i class="fa fa-sort"></i></span>
                                        </div>
                                    </div>


                                    <div class="grid-col-100 center-txt">
                                        <asp:Button ID="btnSear" runat="server" class="btn primary-btn" OnClick="btnSear_Click" Text="Lọc kết quả" />
                                        
                                        <%--  <a href="javascript:;" class="btn primary-btn" onclick="fulterGet()">Tìm kiếm</a>
                                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSear_Click" Style="display: none" />--%>
                                    </div>
                                </div>
                            </div>


                            <div class="table-rps table-responsive">
                                <table class="customer-table mar-top1 full-width normal-table">
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
            </div>

        </section>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container text-center container-800">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Lịch sử giao dịch</span></h3>
                   
                </div>
            </section>
        </div>
    </main>--%>
</asp:Content>
