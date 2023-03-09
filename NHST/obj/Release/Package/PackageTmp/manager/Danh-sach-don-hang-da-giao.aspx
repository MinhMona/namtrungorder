<%@ Page Title="Danh sách hàng đã giao" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Danh-sach-don-hang-da-giao.aspx.cs" Inherits="NHST.manager.Danh_sach_don_hang_da_giao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">chi tiết hàng đã giao</h1>
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

                    <div class="grid-col-50" >
                        <div class="lb">Username</div>
                        <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control"></asp:TextBox>
                    </div>


                    <div class="grid-col-100 center-txt">
                        <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" OnClick="btnFilter_Click" Style="margin-top: 24px;"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pninfo" runat="server" Visible="true">
          <%--  <div class="col-md-12" style="margin-top: 20px;">
                <div class="row">
                    <div class="col-md-12" style="margin-top: 20px;">
                        <span class="label-title">Chi tiết xuất kho</span>
                      
                    </div>
                </div>
            </div>--%>

            <div class="col-md-12" style="margin-top: 10px;">
                  <%--  <div class="row">
                        <asp:Button ID="btnExportStaff" CssClass="btn btn-success" runat="server" Text="Xuất file Excel" OnClick="btnExportStaff_Click" />
                    </div>--%>
                <div class="row">
                    <div class="table-rps table-responsive">
                       <telerik:RadGrid runat="server" ID="RadGrid2" OnNeedDataSource="RadGrid2_NeedDataSource" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="10" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                            AllowAutomaticUpdates="True" OnItemCommand="RadGrid2_ItemCommand" OnPageIndexChanged="RadGrid2_PageIndexChanged"
                            AllowSorting="True" OnPageSizeChanged="RadGrid2_PageSizeChanged">
                            <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Thời gian" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TranOrder" HeaderText="Mã vận đơn" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="TotalWeight" HeaderText="Cân nặng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="TotalPay" HeaderText="Tổng tiền" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="Status" HeaderText="Trạng thái" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="Phone" HeaderText="Điện thoại" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="Address" HeaderText="Địa chỉ" HeaderStyle-Width="5%">
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
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>