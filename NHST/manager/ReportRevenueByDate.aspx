<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manager/adminMaster.Master" CodeBehind="ReportRevenueByDate.aspx.cs" Inherits="NHST.manager.ReportRevenueByDate" %>


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

        .float-right {
            float: right !important;
        }



        .pagi-table .pagi-button {
            padding: 5px 10px;
            cursor: pointer;
            transition: all .2s ease;
            -webkit-transition: all .2s ease;
            -moz-transition: all .2s ease;
            border-radius: 3px;
        }

            .pagi-table .pagi-button.current-active {
                background: #F64302;
                color: #fff;
            }

        thead {
            display: table-header-group;
            vertical-align: middle;
            border-color: inherit;
        }

        .table > thead > tr > th {
            border-bottom: 1px solid #e2e2e4;
            color: white !important;
            border-top: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <%-- <asp:Button runat="server" ID="btnExcel" Text="Xuất file Excel báo cáo" UseSubmitBehavior="false" CssClass="btn primary-btn"  OnClick="btnExcel_Click" Visible="false" />--%>

       <a href="javascript:;" class="btn right primary-btn" id="filter-form-toggle"><i class="fa fa-filter"></i>Bộ lọc</a>
        <h1 class="page-title">Danh sách đơn hàng</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form" style="display: block">
                <div class="grid-row">
                   

                    <div class="grid-col-100">
                        <div class="lb">Tìm kiếm</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập từ khóa..."></asp:TextBox>
                    </div>


                    <div class="grid-col-50">
                        <div class="lb">NV đặt hàng</div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="searchNVDH" AppendDataBoundItems="true"
                            DataValueField="ID" DataTextField="Username">
                        </asp:DropDownList>
                        
                    </div>

                    <div class="grid-col-50">
                        <div class="lb">NV kinh doanh</div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="searchNVKD" AppendDataBoundItems="true"
                            DataValueField="ID" DataTextField="Username">
                        </asp:DropDownList>
                        
                    </div>


                    <p class="grid-col-100 right-txt hl-txt"><a href="javascript:;" id="toggleAdvance" class="btn primary-btn">Nâng cao</a></p>
                    <div id="advance-search" data-css='{"display": "none"}'>
                        

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

                        
                        <div class="grid-col-100">
                            <div class="lb">Số ngày</div>

                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                               
                                <asp:ListItem Value="0" Text="Dưới 60 ngày"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Từ 60 ngày đến 250 ngày"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Trên 250 ngày"></asp:ListItem>
                                
                            </asp:DropDownList>
                        </div>
                        
                    </div>

                    <div class="grid-col-100 center-txt">
                        <a href="javascript:;" class="btn primary-btn" onclick="fulterGet()">Tìm kiếm</a>
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" Style="display: none" />
                    </div>
                </div>
            </div>
            
        </div>
        <div class="table-rps table-responsive">
    
            <table class="table bordered highlight striped normal-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        
                       <th>User đặt hàng
                        </th>
                        
                        <th>Tổng tiền</th>
                        <th>Tiền đã cọc</th>   
                        <th>Nhân viên kinh doanh
                        </th>       
                        <th>Ngày đặt cọc
                        </th>
                        <th>Trạng thái 
                        </th>

                        
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                </tbody>
            </table>



        </div>
        <div class="pagi-table float-right mt-2">
            <%this.DisplayHtmlStringPaging1();%>
        </div>
        <div class="clearfix"></div>
    </main>
    <asp:HiddenField ID="hdfStatus" runat="server" Value="-1" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
    
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
           
        </script>







    </telerik:RadScriptBlock>
    <style>
        .order-status {
            width: auto;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
