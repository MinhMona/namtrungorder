<%@ Page Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Report-General.aspx.cs" Inherits="NHST.manager.Report_General" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <h1 class="page-title">Báo cáo tổng quát</h1>
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
                        <div class="grid-col-100 center-txt">
                            <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" OnClick="btnFilter_Click" Style="margin-top: 24px;"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pninfo" runat="server" Visible="true">
                <div class="cont900" data-css='{"margin-bottom": "20px"}'>
                    <div class="pane-shadow filter-form">
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng Doanh thu (VNĐ)</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltongdoanhthu" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng số đơn hàng</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltongdon" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Giá trị trung bình đơn hàng (VNĐ)</span>
                                <span class="label-infor">
                                    <asp:Label ID="lblgiatriTB" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng tiền tra cho shop (Tệ)</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltongtientrashopCNY" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng tiền tra cho shop (VNĐ)</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltongtientrashopVND" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tỷ giá trung bình (VNĐ)</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltygiaTB" runat="server"></asp:Label></span>
                            </div>
                        </div>

                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng doanh thu dịch vụ mua hàng</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbldoanhthudichvumuahnag" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng phí mua hàng</span>
                                <span class="label-infor">
                                    <asp:Label ID="lblphimuahang" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng tiền mặc cả</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltienmacca" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng cân nặng</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltongcannang" runat="server"></asp:Label></span>
                            </div>
                        </div>
                        <div class="row border-bottom">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <span class="label-title">Tổng tiền giao tận nhà</span>
                                <span class="label-infor">
                                    <asp:Label ID="lbltongphigiaotannha" runat="server"></asp:Label></span>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="table-rps table-responsive" style="display:none">
                    <div class="col-md-12" style="margin-top: 50px;">
                        <div class="row">
                            <asp:Button ID="btnExport" CssClass="btn primary-btn" runat="server" Text="Xuất file Excel" OnClick="btnExport_Click" />
                        </div>
                        <div class="row">
                            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                AllowSorting="True" OnPageSizeChanged="gr_PageSizeChanged">
                                <MasterTableView CssClass="normal-table" DataKeyNames="tongdoanhthu">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="tongdoanhthu" HeaderText="Mã đơn hàng" HeaderStyle-Width="5%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="tongdoanhthu" HeaderText="ShopID" HeaderStyle-Width="5%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="tongdoanhthu" HeaderText="Tổng tiền" HeaderStyle-Width="5%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="tongdoanhthu" HeaderText="Đặt cọc" HeaderStyle-Width="5%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="tongdoanhthu" HeaderText="Còn lại" HeaderStyle-Width="5%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="tongdoanhthu" HeaderText="Trạng thái" HeaderStyle-Width="5%">
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
    </asp:Panel>
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function OnDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= rdateto.ClientID %>");
                datepicker.set_maxDate(date1);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
