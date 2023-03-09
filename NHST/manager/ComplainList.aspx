<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="ComplainList.aspx.cs" Inherits="NHST.manager.ComplainList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Danh sách khiếu nại</h1>
        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
            <div class="cont900" data-css='{"margin-bottom": "20px"}'>
                <div class="pane-shadow filter-form" id="filter-form">
                    <div class="grid-row">
                        <div class="grid-col-50">
                            <div class="lb">Tìm kiếm Username</div>
                            <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập Username, Mã đơn hàng"></asp:TextBox>
                        </div>
                        <div class="grid-col-50">
                            <div class="lb">Tìm theo tên sản phẩm</div>
                            <div class="control-with-suffix">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Tìm theo Username"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Tìm theo Mã đơn hàng"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Tìm theo Username đặt hàng"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Tìm theo Username kinh doanh"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="ID đơn hàng"></asp:ListItem>
                                </asp:DropDownList>
                                <span class="suffix hl-txt"><i class="fa fa-sort"></i></span>
                            </div>
                        </div>
                        <div class="grid-col-100">
                            <div class="lb">Trạng thái</div>
                            <div class="control-with-suffix">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">

                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Kết thúc khiếu nại"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Khiếu nại mới"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đang xử lý"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Chờ hàng về thêm"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Chờ shop hoàn tiền"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Đổi trả hàng"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="GD xử lý"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Kế toán xử lý"></asp:ListItem>
                                    <%--<asp:ListItem Value="8" Text="Đã hoàn thành"></asp:ListItem>--%>
                                    <asp:ListItem Value="9" Text="Chờ CSKH hoàn thành"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="CSKH xử lý"></asp:ListItem>
                                </asp:DropDownList>
                                <span class="suffix hl-txt"><i class="fa fa-sort"></i></span>
                            </div>
                        </div>
                        <div class="grid-col-100 center-txt">
                            <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnExcel" runat="server" CssClass="btn primary-btn" Text="Xuất Excel" OnClick="btnExcel_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True" AllowFilteringByColumn="false">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="OrderID" HeaderText="Đơn hàng" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" FilterDelay="2000" AutoPostBackOnFilter="false">
                            <ItemTemplate>
                                <p class=""><%# Eval("OrderID") %></p>
                                <p>
                                    <a class="btn primary-btn" href='/manager/Orderdetail.aspx?id=<%#Eval("OrderID") %>' target="_blank">Xem đơn hàng</a>
                                </p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Username" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Số tiền" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("Amount"))).Replace(",",".") %> vnđ</p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Nội dung" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class="" style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 350px"><%# Eval("ComplainText") %></p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class=""><%# PJUtils.ReturnStatusComplainRequest(Convert.ToInt32(Eval("Status"))) %></p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Ghi chú nội bộ" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <textarea data-id="<%# Eval("ID") %>" class="EmployeeNote" style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 350px"><%# Eval("EmployeeNote") %></textarea>
                                <%--<textarea class="txtNote" style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 350px"><%# Eval("EmployeeNote") %></textarea>
                                <a href="javascript:;" class="btn primary-btn"onclick='Update("+Eval("ID")+",$(this))'">Cập nhật</a>--%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/ComplainDetail.aspx?id=<%#Eval("ID") %>'>Xem</a>

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
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function fulterGet() {
                var st = $("#<%=ddlStatus.ClientID%>").val();
                $("#<%=hdfStatus.ClientID%>").val(st);
                $("#<%=btnSearch.ClientID%>").click();
            }

            //function Update(obj, ID) {
            //    debugger;
            //    var note = obj.parent().find(".txtNote").val();
            //    $.ajax({
            //        type: "POST",
            //        url: "/manager/ComplainList.aspx/UpdateEmployeeNote",
            //        data: "{ID:'" + ID + "',EmployeeNote:'" + note + "'}",
            //        contentType: "application/json; charset=utf-8",
            //        dataType: "json",
            //        success: function (msg) {
            //            if (msg.d != null) {
            //                swal('Thành công!');
            //            }
            //            else {
            //                swal('Có lỗi trong quá trình xử lý');
            //            }
            //        },
            //        error: function (xmlhttprequest, textstatus, errorthrow) {
            //            console.log('lỗi checkend');
            //        }
            //    });
            //}

            $(document).ready(function () {
                $('.EmployeeNote').keydown(function (e) {
                    if (e.keyCode == 13) {
                        if (e.shiftKey) {
                            return true;
                        }
                        Update($(this));
                        e.preventDefault();
                        return false;
                    }
                });
            });

            //$(document).ready(function () {
            //    $('.EmployeeNote').keydown(function (e) {
            //        if (e.key == 'Enter') {
            //            Update($(this));
            //            e.preventDefault();
            //            return false;
            //        }
            //    });
            //});

            function Update(obj) {
                var id = obj.attr('data-id');
                var note = obj.val();
                $.ajax({
                    type: "POST",
                    url: "/manager/ComplainList.aspx/UpdateEmployeeNote",
                    data: "{ID:'" + id + "',EmployeeNote:'" + note + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != null) {
                            swal('Thành công!');
                        }
                        else {
                            swal('Có lỗi trong quá trình xử lý');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        console.log('lỗi checkend');
                    }
                });
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
