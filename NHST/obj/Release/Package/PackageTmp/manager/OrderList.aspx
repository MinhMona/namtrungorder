<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="NHST.manager.OrderList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html body .RadInput_MetroTouch .riTextBox, html body .RadInputMgr_MetroTouch {
            background: #f8f8f8 !important;
            border: solid 1px #e1e1e1;
        }

        .RadPicker {
            width: 175% !important;
        }

        html body .riSingle .riTextBox[type="text"] {
            line-height: 40px;
            height: 40px !important;
            border-radius: 3px;
            border: solid 1px #ccc !important;
            background: #f8f8f8 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <%-- <asp:Button runat="server" ID="btnExcel" Text="Xuất file Excel báo cáo" UseSubmitBehavior="false" CssClass="btn primary-btn"  OnClick="btnExcel_Click" Visible="false" />--%>

        <asp:Button runat="server" ID="btnTakeOrder" Text="Nhận đơn" UseSubmitBehavior="false" CssClass="btn primary-btn" OnClick="btnTakeOrder_Click" Visible="false" />
        <a href="javascript:;" class="btn right primary-btn" id="filter-form-toggle"><i class="fa fa-filter"></i>Bộ lọc</a>
        <h1 class="page-title">Danh sách đơn hàng</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form" style="display: none">
                <div class="grid-row">
                    <div class="grid-col-50">
                        <div class="lb">Nhập tên sản phẩm hoặc mã vận đơn</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập tên sản phẩm hoặc mã vận đơn"></asp:TextBox>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Tìm theo tên sản phẩm</div>
                        <div class="control-with-suffix">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1" Text="Tìm theo Tên sản phẩm"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tìm theo Mã vận đơn"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Tìm theo Mã đơn hàng"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="suffix hl-txt"><i class="fa fa-sort"></i></span>
                        </div>
                    </div>
                    <p class="grid-col-100 right-txt hl-txt"><a href="javascript:;" id="toggleAdvance" class="btn primary-btn">Nâng cao</a></p>
                    <div id="advance-search" data-css='{"display": "none"}'>
                        <div class="grid-col-50">
                            <div class="lb">Giá từ:</div>
                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                ID="rPriceFrom" MinValue="0" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Giá từ" Value="0">
                            </telerik:RadNumericTextBox>
                        </div>
                        <div class="grid-col-50">
                            <div class="lb">Giá đến:</div>
                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                ID="rPriceTo" MinValue="0" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Giá đến" Value="0">
                            </telerik:RadNumericTextBox>
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

                        <div class="grid-col-50">
                            <div class="inline-date">
                                <div class="lb">Dk từ ngày:</div>
                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="fromDK" ShowPopupOnFocus="true" Width="100%" runat="server"
                                    DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                    </DateInput>
                                </telerik:RadDateTimePicker>
                            </div>
                        </div>
                        <div class="grid-col-50">
                            <div class="inline-date">
                                <div class="lb">DK đến ngày:</div>
                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="toDK" ShowPopupOnFocus="true" Width="100%" runat="server"
                                    DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                    </DateInput>
                                </telerik:RadDateTimePicker>
                            </div>
                        </div>
                        <div class="grid-col-100">
                            <div class="lb">Trạng thái</div>

                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" multiple>
                                <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Chưa đặt cọc"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Hủy đơn hàng"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Chờ mua hàng"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Chờ shop TQ phát hàng"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Shop đã phát hàng"></asp:ListItem>
                               <%-- <asp:ListItem Value="12" Text="Đang mua hàng"></asp:ListItem>--%>
                                <%--<asp:ListItem Value="13" Text="Đã thanh toán cho shop"></asp:ListItem>--%>
                                <asp:ListItem Value="6" Text="Đang về kho đích"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Đã nhận hàng tại kho đích"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Khách đã thanh toán"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Đã hoàn thành"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="grid-col-100">
                            <div class="lb">
                                <asp:CheckBox ID="chkIsnotcode" runat="server" />
                                <span style="margin-left: 5px;">Đơn chưa có mã vận đơn</span>
                            </div>
                        </div>
                    </div>

                    <div class="grid-col-100 center-txt">
                        <a href="javascript:;" class="btn primary-btn" onclick="fulterGet()">Tìm kiếm</a>
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" Style="display: none" />
                    </div>
                </div>
            </div>
            <div class="pane-shadow filter-form" id="filter-form1" style="display: block">
                <asp:Button ID="bttnAll" runat="server" CssClass="order-status bg-yellow" OnClick="bttnAll_Click" />
                <asp:Button ID="btn0" runat="server" CssClass="order-status bg-red" OnClick="btn0_Click" />
                <asp:Button ID="btn1" runat="server" CssClass="order-status bg-black" OnClick="btn1_Click" />
                <asp:Button ID="btn2" runat="server" CssClass="order-status bg-bronze" OnClick="btn2_Click" />
                <asp:Button ID="btn5" runat="server" CssClass="order-status bg-green" OnClick="btn5_Click" />
                <asp:Button ID="btn11" runat="server" CssClass="order-status bg-green" OnClick="btn11_Click" />
                <asp:Button ID="btn12" runat="server" CssClass="order-status bg-red" Visible="false" OnClick="btn12_Click" />
                <%-- <asp:Button ID="btn13" runat="server" CssClass="order-status bg-orange" OnClick="btn13_Click" />--%>
                <asp:Button ID="btn6" runat="server" CssClass="order-status bg-green" OnClick="btn6_Click" />
                <asp:Button ID="btn7" runat="server" CssClass="order-status bg-orange" OnClick="btn7_Click" />
                <asp:Button ID="btn9" runat="server" CssClass="order-status bg-blue" OnClick="btn9_Click" />
                <asp:Button ID="btn10" runat="server" CssClass="order-status bg-blue" OnClick="btn10_Click" />

            </div>
        </div>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="true" AllowFilteringByColumn="True">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="anhsanpham" HeaderText="Ảnh sản phẩm" HeaderStyle-Width="10%" FilterControlWidth="50px"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Site" HeaderText="Website" HeaderStyle-Width="10%" FilterControlWidth="50px"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="orderlinks" HeaderText="Tổng link" HeaderStyle-Width="10%" FilterControlWidth="50px"
                            AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Tổng tiền" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceVND"))) %> vnđ</p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Tiền đã cọc" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposit"))) %> vnđ</p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Uname" HeaderText="User đặt hàng" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="dathang" HeaderText="Nhân viên đặt hàng" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="saler" HeaderText="Nhân viên kinh doanh" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn HeaderText="Ngày đặt hàng" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <%--ngày đặt cọc--%>
                        <telerik:GridTemplateColumn HeaderText="Ngày đặt cọc"  HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="DepostiDate"  FilterControlWidth="100px" AutoPostBackOnFilter="true"

                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("DepostiDate","{0:dd/MM/yyyy HH:mm}")  %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                      <%--  <telerik:GridTemplateColumn HeaderText="Ngày phát hàng dự kiến" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="ExpectedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("ExpectedDate","{0:dd/MM/yyyy HH:mm}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="statusstring" HeaderText="Trạng thái đơn hàng" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/OrderDetail.aspx?id=<%#Eval("ID") %>' target="_blank">Xem</a>
                                <%# Convert.ToInt32(Eval("Status")) == 0? "<a class=\"btn primary-btn\" onclick=\"deposit('"+ Eval("ID") +"')\">Đặt cọc</a>":"" %>
                                <%# Convert.ToInt32(Eval("Status")) == 7? "<a class=\"btn primary-btn\" onclick=\"payall('"+ Eval("ID") +"')\">Thanh toán</a>":"" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </main>
    <asp:HiddenField ID="hdfStatus" runat="server" Value="-1" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:Button ID="btnDespi" runat="server" Style="display: none" OnClick="btnDespi_Click" />
    <asp:Button ID="btnPayall" runat="server" Style="display: none" OnClick="btnPayall_Click" />
    <telerik:RadAjaxLoadingPanel ID="rxLoading" runat="server" Skin="">
        <div class="loading">
            <asp:Image ID="Image1" runat="server" ImageUrl="/App_Themes/NewUI/images/loading.gif" AlternateText="loading" />
        </div>
    </telerik:RadAjaxLoadingPanel>
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
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="bttnAll">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn0">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn7">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn9">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn10">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn11">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function fulterGet() {
                var st = $("#<%=ddlStatus.ClientID%>").val();
                $("#<%=hdfStatus.ClientID%>").val(st);
                $("#<%=btnSearch.ClientID%>").click();
            }
            $(document).ready(function () {
                $("#tag").select2({
                    tags: true,
                    maximumInputLength: 10,

                    templateSelection: function (selection) {
                        if (!selection.id) {
                            return selection.text;
                        }
                        return $('<span class="' + selection.id + '">' + selection.text + '</span>');
                    }
                });
            });
            function deposit(id) {
                var c = confirm('Bạn muốn đặt cọc cho đơn hàng: ' + id + '?');
                if (c == true) {
                    $("#<%=hdfOrderID.ClientID%>").val(id);
                    $("#<%=btnDespi.ClientID%>").click();
                }
            }
            function payall(id) {
                var c = confirm('Bạn muốn thanh toán cho đơn hàng: ' + id + '?');
                if (c == true) {
                    $("#<%=hdfOrderID.ClientID%>").val(id);
                    $("#<%=btnPayall.ClientID%>").click();
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <style>
        .order-status {
            width: auto;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
