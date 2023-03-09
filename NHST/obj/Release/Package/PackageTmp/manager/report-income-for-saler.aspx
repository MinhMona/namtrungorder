<%@ Page Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="report-income-for-saler.aspx.cs" Inherits="NHST.manager.report_income_for_saler" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">

        <h1 class="page-title">Báo cáo doanh thu saler</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Chọn khách hàng</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập Username khách để tìm kiếm"></asp:TextBox>
                    </div>
                    <div class="grid-col-50" style="display:none">
                        <div class="lb">Chọn Saler</div>
                        <asp:TextBox runat="server" ID="txtseachSale" CssClass="form-control" placeholder="Nhập Username saler để tìm kiếm"></asp:TextBox>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Mốc thời gian</div>
                        <asp:DropDownList ID="ddlTime" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Tất cả"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Chờ shop phát hàng"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Hoàn thành"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="grid-col-50">
                        <div class="lb">Chọn phòng ban</div>
                        <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Kinh doanh 1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Kinh doanh 2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Kinh doanh 3"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Kinh doanh 4"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

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
                        <%--<asp:Button ID="btnExportExcel" CssClass="btn btn-success" runat="server" Text="Xuất file Excel" OnClick="btnExportExcel_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
       
        <asp:Panel ID="pninfo" runat="server" Visible="true" >
            <div class="cont900" data-css='{"margin-bottom": "20px"}' >
                <div class="pane-shadow filter-form">
                    
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng giá trị đơn hàng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltonggiatridonhang" runat="server"></asp:Label> (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng tiền hàng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongtienhang" runat="server"></asp:Label> (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng phí đơn hàng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongphidonhang" runat="server"></asp:Label> (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng phí mua hàng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongphimuahang" runat="server"></asp:Label> (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng vận chuyển QT</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongvanchuyenqt" runat="server"></asp:Label> (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng vận chuyển nội địa</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongvanchuyennoidia" runat="server"></asp:Label> (VNĐ)</span>
                        </div>
                    </div>
                    <div class="row border-bottom">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng mặc cả</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongmacca" runat="server"></asp:Label> (VNĐ)</span>
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
                            <span class="label-title">Tổng số đơn hàng</span>
                            <span class="label-infor">
                                <asp:Label ID="lbltongsodonhang" runat="server"></asp:Label></span>
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
                            <MasterTableView CssClass="normal-table" DataKeyNames="Saler">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Username" HeaderText="Khách hàng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Saler" HeaderText="Saler" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Giá trị đơn hàng" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("giatridonhang"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Tiền hàng" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("PriceVND"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Phí đơn hàng" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("phidonhang"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Phí mua hàng" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("FeeBuyPro"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Vận chuyển QT" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("FeeWeight"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Vận chuyển nội địa" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("FeeShipCN"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Mặc cả" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("tienmacca"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="TQVNWeight" HeaderText="Cân nặng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalOrder" HeaderText="Số đơn hàng" HeaderStyle-Width="5%">
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

