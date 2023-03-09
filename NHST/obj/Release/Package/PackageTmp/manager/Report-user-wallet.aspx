<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Report-user-wallet.aspx.cs" Inherits="NHST.manager.Report_user_wallet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Thống kê Khách hàng có số dư</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
        </div>
        <div class="col-md-12">
            <div class="panel panel-white">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase"></h3>
                </div>
                <div class="panel-body">
                    <div class="row m-b-lg">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="User có số dư tài khoản"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-success" Text="Xem" OnClick="btnFilter_Click"></asp:Button>
                                    <asp:Button ID="btnExcel" runat="server" CssClass="btn primary-btn" Text="Xuất Excel" OnClick="btnExcel_Click" Style="margin-top: 24px;"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pninfo" runat="server" Visible="true">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <div class="row">
                                    <asp:Literal ID="ltrinf" runat="server"></asp:Literal>
                                </div>
                                <div class="row">
                                    <div class="table-rps table-responsive">
                                        <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                            AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                            AllowSorting="True" AllowFilteringByColumn="True">
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UserName" HeaderText="UserName" HeaderStyle-Width="15%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Ho" HeaderText="Họ" HeaderStyle-Width="10%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Ten" HeaderText="Tên" HeaderStyle-Width="10%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Sodt" HeaderText="Số đt" HeaderStyle-Width="15%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Status" HeaderText="Trạng thái" HeaderStyle-Width="10%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Role" HeaderText="Quyền hạn" HeaderStyle-Width="15%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Saler" HeaderText="Nhân viên kinh doanh" HeaderStyle-Width="15%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="dathang" HeaderText="Nhân viên đặt hàng" HeaderStyle-Width="15%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="wallet" HeaderText="Số dư" HeaderStyle-Width="15%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-Width="15%"
                                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                                        <ItemTemplate>
                                                            <a class="btn btn-info btn-sm" href='/Admin/UserInfo.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                                                            <%#Eval("RoleID").ToString() == "1"?"<a class=\"btn btn-info btn-sm\" href=\"/Admin/UserWallet.aspx?i="+Eval("ID")+"\">Nạp tiền</a>":"" %>
                                                            <%#Eval("RoleID").ToString() == "1"?"<a class=\"btn btn-info btn-sm\" href=\"/Admin/User-Transaction.aspx?i="+Eval("ID")+"\">Lịch sử giao dịch</a>":"" %>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                                    PrevPageText="← Previous" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>
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
    </telerik:RadScriptBlock>
    <style>
        .label-infor {
            float: left;
            width: 60%;
            padding: 0 10px;
            text-align: left;
        }

        .label-title {
            float: left;
            width: 40%;
            font-weight: bold;
        }
    </style>
</asp:Content>
