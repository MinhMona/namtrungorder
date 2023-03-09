<%@ Page Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Report-PayOrder-Internal.aspx.cs" Inherits="NHST.manager.Report_PayOrder_Internal" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Thống kê danh sách thanh toán đơn nội bộ</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-50">
                        <div class="lb">Username khách</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="username khách"></asp:TextBox>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Người tạo</div>
                        <asp:TextBox runat="server" ID="txtCreatedBy" CssClass="form-control" placeholder="Người tạo"></asp:TextBox>
                    </div>
                    <div class="grid-col-100">
                        <div class="lb">Người thực hiện</div>
                        <asp:TextBox runat="server" ID="txtPerformStaff" CssClass="form-control" placeholder="Người thực hiện"></asp:TextBox>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Trạng thái</div>
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Đang mua hàng"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Đã thanh toán cho shop"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Website</div>
                        <asp:DropDownList runat="server" ID="ddlSite" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                            <asp:ListItem Value="0" Text="TAOBAO"></asp:ListItem>
                            <asp:ListItem Value="1" Text="1688"></asp:ListItem>
                            <asp:ListItem Value="2" Text="TMALL"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="grid-col-50">
                        <div class="inline-date">
                            <div class="lb">Từ ngày:</div>
                            <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
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
                    <div class="grid-col-50" style="display: none">
                        <div class="lb">
                            <asp:CheckBox ID="chkIsnotcode" runat="server" />
                            <span style="margin-left: 5px;">Chưa thanh toán</span>
                        </div>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExcel" runat="server" CssClass="btn primary-btn" Text="Xuất Excel" OnClick="btnExcel_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form">
                <div class="row border-bottom">
                    <asp:Panel ID="pnReport" runat="server" Style="width: 100%">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <span class="label-title">Tổng tiền tệ</span>
                            <span class="label-infor">
                                <asp:Label ID="lblTongTien" runat="server"></asp:Label></span>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>


        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Site" HeaderText="Website" HeaderStyle-Width="10%" FilterControlWidth="50px"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Uname" HeaderText="Username" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="MainOrderCode" HeaderText="Mã đơn hàng" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="dathang" HeaderText="NV đặt hàng" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="TotalPriceCYN" HeaderText="Tổng tiền (tệ)" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="statusstring" HeaderText="Trạng thái đơn hàng" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="PerformStaff" HeaderText="Người thực hiện" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn HeaderText="Ngày thực hiện" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="PayingDate">
                            <ItemTemplate>
                                <%#Eval("PayingDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>





                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </main>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="pnReport" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

</asp:Content>
