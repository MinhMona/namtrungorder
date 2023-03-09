<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="accountant-outstock-payment.aspx.cs" Inherits="NHST.manager.accountant_outstock_payment" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Danh sách phiên xuất kho</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-50">
                        <div class="lb">Tìm kiếm</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập username để tìm"></asp:TextBox>
                    </div>
                     <div class="grid-col-50">
                        <div class="lb">Người tạo</div>
                        <asp:TextBox runat="server" ID="txtCreatedBy" CssClass="form-control" placeholder="Nhập username người tạo"></asp:TextBox>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Thuộc loại</div>
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Đã xử lý"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">
                            <asp:CheckBox ID="chkIsnotcode" runat="server" />
                            <span style="margin-left: 5px;">Chưa thanh toán</span>
                        </div>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                    </div>
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
                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Tổng tiền (VNĐ)" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="PageTypeID">
                            <ItemTemplate>
                                <%# Eval("TotalPay","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Thuộc loại" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="PageTypeID">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Eval("Status")) == 0?
                                         "<span class=\"bg-yellow\">Chưa xử lý</span>":
                                         "<span class=\"bg-blue\">Đã xử lý</span>"%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Chưa thanh toán" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("IsPay")!=null? 
                                        Convert.ToBoolean(Eval("IsPay")) == true? 
                                        "<input onclick=\"CheckPay("+Eval("ID")+",$(this))\" type=\"checkbox\" checked>":"<input onclick=\"CheckPay("+Eval("ID")+",$(this))\" type=\"checkbox\">" 
                                        :"<input onclick=\"CheckPay("+Eval("ID")+",$(this))\"  type=\"checkbox\">" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Người tạo" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/view-outstock-session.aspx?id=<%#Eval("ID") %>'>Sửa</a>
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
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <script type="text/javascript">
        function CheckPay(ID, obj) {
            var c = confirm('Bạn muốn cập nhật lại trạng thái thanh toán?');
            if (c) {
                var ispay = obj.prop('checked');
                $.ajax({
                    type: "POST",
                    url: "/manager/accountant-outstock-payment.aspx/UpdateIsPay",
                    data: "{ID:'" + ID + "', IsPay:'" + ispay + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {

                    }
                })
            }
        }
    </script>
</asp:Content>


