<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="admin-staff-income.aspx.cs" Inherits="NHST.manager.admin_staff_income" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">        
        <h1 class="page-title">Thống kê hoa hồng nhân viên</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form" style="display: block">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Username</div>
                        <asp:DropDownList ID="ddlUsername" runat="server" CssClass="form-control select2" AppendDataBoundItems="true"
                            DataValueField="ID" DataTextField="Username">
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
                    <div class="grid-col-100">
                        <div class="lb">Trạng thái</div>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Chưa thanh toán"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Đã thanh toán"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" OnClick="btnFilter_Click"
                            Style="margin-top: 24px;"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form">
                <div class="grid-row">
                    <div class="grid-col-50">
                        <div class="lb">Tổng tiền đã thanh toán</div>
                    </div>
                    <div class="grid-col-50">
                        <asp:Label ID="lblPay" runat="server" Text="0"></asp:Label>
                        vnđ
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Tổng tiền chưa thanh toán</div>
                        
                    </div>
                    <div class="grid-col-50">
                        <asp:Label ID="lblNotPay" runat="server" Text="0"></asp:Label>
                        vnđ
                    </div>
                </div>
            </div>

        </div>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="5" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True" AllowFilteringByColumn="false">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="MainOrderID" HeaderText="Mã đơn hàng" HeaderStyle-Width="5%"
                            AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Phần trăm" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("PercentReceive") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Hoa hồng" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                            FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class="moneyrose" data-value="<%#Eval("TotalPriceReceive") %>">
                                    <%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceReceive"))) %> vnđ
                                </p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="5%"
                            AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Quyền hạn" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Convert.ToInt32(Eval("RoleID")) == 3 ?"NV đặt hàng":"NV Kinh doanh" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                            FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class="statusThanhtoan">
                                    <%#Convert.ToInt32(Eval("Status")) == 1 ?
                                                                    "<span class=\"bg-red\">Chưa thanh toán</span>":
                                                                    "<span class=\"bg-blue\">Đã thanh toán</span>" %>
                                </p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Ngày giờ" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="">
                            <ItemTemplate>
                                <a class="btn primary-btn" target="_blank" href='/manager/OrderDetail.aspx?id=<%#Eval("MainOrderID") %>'>Chi tiết đơn</a>
                                <br />
                                <%# Convert.ToInt32(Eval("Status")) == 1?
                                                        "<a class=\"btn primary-btn\" onclick=\"thanhtoanHoahong('"+Eval("ID")+"',$(this))\" href='javascript:;'>Thanh toán</a>":
                                                        "" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </main>

    <%-- </asp:Panel>--%>
    <%-- <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function OnDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= rdateto.ClientID %>");
                datepicker.set_maxDate(date1);
            }
            function thanhtoanHoahong(ID, obj) {
                var money = parseFloat(obj.parent().parent().find(".moneyrose").attr("data-value"));
                if (money > 0) {
                    $.ajax({
                        type: "POST",
                        url: "/manager/admin-staff-income.aspx/UpdateStatus",
                        data: "{ID:'" + ID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == 1) {
                                var status = obj.parent().parent().find(".statusThanhtoan");
                                status.html("<span class=\"bg-blue\">Đã thanh toán</span>");
                                obj.remove();
                            }
                            else {
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert(errorthrow);
                        }
                    });
                }
                else {
                    alert('Hoa hồng chưa có');
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
