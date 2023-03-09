<%@ Page Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="report-income-buyer.aspx.cs" Inherits="NHST.manager.report_income_buyer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">

        <h1 class="page-title">Báo cáo doanh thu mua hàng</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">

                    <div class="grid-col-50">
                        <div class="lb">Từ ngày</div>
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rdatefrom" ShowPopupOnFocus="true" Width="100%" runat="server"
                            DateInput-CssClass="radPreventDecorate">
                            <TimeView TimeFormat="HH:mm" runat="server">
                            </TimeView>
                            <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                            </DateInput>
                        </telerik:RadDateTimePicker>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Đến ngày</div>
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rdateto" ShowPopupOnFocus="true" Width="100%" runat="server"
                            DateInput-CssClass="radPreventDecorate">
                            <TimeView TimeFormat="HH:mm" runat="server">
                            </TimeView>
                            <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                            </DateInput>
                        </telerik:RadDateTimePicker>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Username</div>
                        <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" OnClick="btnFilter_Click" Style="margin-top: 24px;"></asp:Button>
                        <%--<asp:Button ID="btnExportExcel" CssClass="btn btn-success" runat="server" Text="Xuất file Excel" OnClick="btnExportExcel_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pninfo" runat="server" Visible="true">
            <div class="cont900" data-css='{"margin-bottom": "20px"}' style="display: none">
                <div class="pane-shadow filter-form">
                    <div class="row border-bottom">
                        <div class="col-md-9" style="margin-top: 20px;">
                            <span class="label-title">Giá trị đơn hàng
                            </span>
                            <span class="label-infor">
                                <asp:Label ID="lblTotalAccount" runat="server"></asp:Label></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-rps table-responsive">
                <div class="col-md-12" style="margin-top: 50px;">
                    <%--<div class="row">
                        <h1>Thống kê doanh thu Sale</h1>
                    </div>--%>
                    <%-- <div class="row">
                        <asp:Button ID="btnExportExcel" CssClass="btn btn-success" runat="server" Text="Xuất file Excel" OnClick="btnExportExcel_Click" />
                    </div>--%>
                    <div class="row">
                        <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                            AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                            AllowSorting="True" OnPageSizeChanged="gr_PageSizeChanged">
                            <MasterTableView CssClass="normal-table" DataKeyNames="Staff">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Staff" HeaderText="Nhân viên" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTAL" HeaderText="Tổng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TAOBAO" HeaderText="TAOBAO" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TMALL" HeaderText="TMALL" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="T1688" HeaderText="1688" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                    PrevPageText="← Previous" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </main>

    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function OnDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= rdateto.ClientID %>");
                datepicker.set_maxDate(date1);


        </script>
    </telerik:RadScriptBlock>
</asp:Content>
