<%@ Page Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Danh-sach-don-hang-cho-giao.aspx.cs" Inherits="NHST.manager.Danh_sach_don_hang_cho_giao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Danh sách hàng chờ giao</h1>
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
                    <div class="grid-col-50">
                        <div class="lb">Trạng thái</div>
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Đã về kho VN"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Khách đã thanh toán"></asp:ListItem>
                        </asp:DropDownList>
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
            <div class="cont900" data-css='{"margin-bottom": "20px"}'>
                <div class="pane-shadow filter-form">
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng số lượng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongsoluong" runat="server"></asp:Label></span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng cân nặng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongcannang" runat="server"></asp:Label> (KG)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng tiền hàng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongtienhang" runat="server"></asp:Label>
                                (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng tiền trả</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongtientra" runat="server"></asp:Label>
                                (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng tiền còn lại</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongtienconlai" runat="server"></asp:Label>
                                (VNĐ)</span>
                        </div>
                    </div>

                     <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Số dư ví</span>
                            <span class="label-infor">
                                <asp:Label ID="lblSoduvi" runat="server"></asp:Label>
                                (VNĐ)</span>
                        </div>
                    </div>

                     <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Số dư sau XK</span>
                            <span class="label-infor">
                                <asp:Label ID="lblSodusauxk" runat="server"></asp:Label>
                                (VNĐ)</span>
                        </div>
                    </div>
                </div>
            </div>
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
                                    <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TranOrder" HeaderText="Mã vận đơn" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalQuantity" HeaderText="Tổng số lượng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalWeight" HeaderText="Tổng cân nặng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="totalPriceOrder" HeaderText="Tổng tiền hàng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalDeposit" HeaderText="Tổng tiền trả" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalPay" HeaderText="Tổng tiền còn lại" HeaderStyle-Width="5%">
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
