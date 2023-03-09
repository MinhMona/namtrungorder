<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="BigPackageList.aspx.cs" Inherits="NHST.manager.BigPackageList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/NewUI/css/fonticons.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
    <%@ Import Namespace="NHST.Controllers" %>
    <%@ Import Namespace="NHST.Models" %>
    <%@ Import Namespace="NHST.Bussiness" %>
    <main id="main-wrap">
        <a type="button" class="btn right primary-btn" href="/manager/AddBigPackage.aspx">Thêm kiện lớn</a>
        <h1 class="page-title">Danh sách kiện lớn</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                        <div class="grid-col-100">
                            <div class="lb">Tìm kiếm</div>
                            <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập mã bao lớn để tìm"></asp:TextBox>
                        </div>
                        <div class="grid-col-100 center-txt">
                            <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="table-rps table-responsive">
            <table class="normal-table">
                <tr>
                    <th>ID
                    </th>
                    <th>Mã vận đơn
                    </th>
                    <th>User Phone
                    </th>
                    <th>Trọng lượng
                    </th>
                    <th>Kho
                    </th>
                    <th>Ghi chú
                    </th>
                    <th>Trạng thái
                    </th>
                    <th>Trạng thái nhận hàng
                    </th>
                    <th>Trạng thái thanh toán
                    </th>
                    <th></th>
                </tr>
                <asp:Literal ID="ltrorderlist" runat="server" EnableViewState="false"></asp:Literal>
            </table>
            <div class="pagenavi fl">
                <%this.DisplayHtmlStringPaging1();%>
            </div>
        </div>
    </main>
    <style>
        .pagenavi {
            display: flex;
            float: right;
        }

            .pagenavi a,
            .pagenavi span {
                width: 40px;
                height: 40px;
                line-height: 40px;
                text-align: center;
                color: #fff;
                font-weight: bold;
                background: #22BAA0;
                display: inline-block;
                font-weight: bold;
                margin-right: 1px;
                text-decoration: none;
            }

                .pagenavi .current,
                .pagenavi a:hover {
                    background: #12AFCB;
                    color: #fff;
                }

        .table.table-bordered > thead > tr > th, .table.table-bordered > tbody > tr > th, .table.table-bordered > tfoot > tr > th, .table.table-bordered > thead > tr > td, .table.table-bordered > tbody > tr > td, .table.table-bordered > tfoot > tr > td {
            color: #000 !important;
        }
    </style>
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
</asp:Content>
