<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Home-Config.aspx.cs" Inherits="NHST.manager.Home_Config" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/gijgo@1.9.10/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/gijgo@1.9.10/js/gijgo.min.js" type="text/javascript"></script>
	<script src="/App_Themes/NewUI/js/app.js"></script>
        <script src="/App_Themes/NewUI/js/jstree.min.js"></script>
        <script src="/App_Themes/NewUI/js/ui-tree.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <a style="display:none" type="button" class="btn primary-btn right" href="/manager/AddParentMenu.aspx">Thêm menu cha</a>
        <h1 class="page-title" style="display: none">Danh sách Menu</h1>
        <div class="panel-body" style="display: none">
            <div id="tree_1" class="tree-demo">
                <asp:Literal ID="ltrTree" runat="server"></asp:Literal>
            </div>
            <div>
                <a class="btn primary-btn" onclick="Test()">Cập nhật</a>
            </div>
        </div>
        <%--<a type="button" class="btn primary-btn right" href="/manager/Add-banner.aspx">Thêm banner</a>--%>
        <%--<h1 class="page-title">Danh sách banner</h1>--%>
        <%--        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Tìm kiếm</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập tiêu đề để tìm"></asp:TextBox>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="table-rps table-responsive" style="display:none">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="10" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Hình ảnh" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.ShowIMG(Eval("BannerIMG").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="BannerLink" HeaderText="Banner Link" HeaderStyle-Width="10%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.BoolToStatusShow(Eval("IsHidden").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/Edit-Banner.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>


        <%--<h1 class="page-title">Cấu hình trang chủ</h1>--%>
        <div class="grid-row" style="display:none">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Cấu hình trang chủ</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot1">
                                        Năm kinh nghiệm
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pYear" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <%--<span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="pYear" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>--%>
                                    </div>
                                    <div class="form-row marbot1">
                                        Số lượng khách hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pQuantityCustomer" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <%--<span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="pQuantityCustomer" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>--%>
                                    </div>
                                    <div class="form-row marbot1">
                                        Số lượng đơn hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pQuantityOrder" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <%--<span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="pQuantityOrder" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>--%>
                                    </div>

                                    <div class="form-row marbot1">
                                        Footer
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadEditor runat="server" ID="rFooterTrai" Width="100%"
                                            Height="100px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>

                                    <div class="form-row marbot1">
                                        Facebook
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtFacebook" CssClass="form-control has-validate" placeholder="Facebook"></asp:TextBox>
                                    </div>

                                    <div class="form-row marbot1" style="display: none;">
                                        Footer Phải
                                    </div>
                                    <div class="form-row marbot2" style="display: none;">
                                        <telerik:RadEditor runat="server" ID="rFooterPhai" Width="100%"
                                            Height="100px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>

                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn" OnClick="btncreateuser_Click" />
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>


        <%--<a type="button" class="btn primary-btn right" href="/manager/AddStep.aspx">Thêm bước</a>--%>
        <h1 class="page-title" style="display: none">Danh sách các bước</h1>

        <div class="table-rps table-responsive" style="display: none">
            <telerik:RadGrid runat="server" ID="S" OnNeedDataSource="S_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="S_ItemCommand" OnPageIndexChanged="S_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StepName" HeaderText="Tên" HeaderStyle-Width="10%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Hình ảnh" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.ShowIMG(Eval("StepIMG").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="StepLink" HeaderText="Link" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StepIndex" HeaderText="Index" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.BoolToStatusShow(Eval("IsHidden").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/EditStep.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                                <a class="btn primary-btn" href="javascript:;" onclick="DeleteStep('<%#Eval("ID") %>')">Xóa</a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>


        <%--<a type="button" class="btn primary-btn right" href="/manager/Add-Service.aspx">Thêm Dịch Vụ</a>--%>
        <h1 class="page-title">Danh sách dịch vụ</h1>
        <%--        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Tìm kiếm</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập tiêu đề để tìm"></asp:TextBox>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="SV" OnNeedDataSource="SV_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="SV_ItemCommand" OnPageIndexChanged="SV_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceName" HeaderText="Tên dịch vụ" HeaderStyle-Width="10%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Hình ảnh" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.ShowIMG(Eval("ServiceIMG").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.BoolToStatusShow(Eval("IsHidden").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/Edit-Service.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>

        <%--<a type="button" class="btn primary-btn right" href="/manager/Add-Customer-Benefit.aspx">Thêm Quyền Lợi</a>--%>
        <h1 class="page-title" style="display: none">Danh sách quyền lợi khách hàng & Cam kết dịch vụ</h1>
        <div class="table-rps table-responsive" style="display: none">
            <telerik:RadGrid runat="server" ID="C" OnNeedDataSource="C_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="C_ItemCommand" OnPageIndexChanged="C_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CustomerBenefitName" HeaderText="Tên quyền lợi" HeaderStyle-Width="10%">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.BoolToStatusShow(Eval("IsHidden").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Position" HeaderText="Vị trí" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/Edit-Customer-Benefit.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>

        <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" Style="display: none;" />
        <asp:HiddenField ID="hdfID" runat="server" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Style="display: none" />
    </main>

    <style>
        .table-rps {
            padding-bottom: 10px;
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
            <telerik:AjaxSetting AjaxControlID="btnDelete">
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
        <asp:HiddenField runat="server" ID="hdfTest" />
        <style>
            .register-link {
                color: blue;
                text-decoration: underline;
                font-style: italic;
            }

            .panel-body {
                background: #fff;
                margin-bottom:20px;
            }
        </style>
        <script type="text/javascript">
            function editMenu(ID) {
                var win = window.open("/manager/editmenu.aspx?i=" + ID + "", '_blank');
                //win.focus();
                //window.location = "/admin/categoryinfo.aspx?ID=" + ID + "";
            }
            function AddChildMenu(ID) {
                var win = window.open("/manager/AddMenu.aspx?ID=" + ID + "", '_blank');
                //win.focus();
                //window.location = "/admin/addChildCategory.aspx?ID=" + ID + "";
            }


            function Test() {

                var v = $('#tree_1').jstree(true).get_json('#', { flat: false });
                var mytext = JSON.stringify(v);
                console.log(v);
                var listitem = "";

                for (var i in v) {
                    var item = "";
                    var ids = "";
                    if (typeof (v[i].li_attr.id) != "undefined") {
                        ids = v[i].li_attr.id;
                    }
                    var parent = [];
                    if (typeof (v[i].children) != null) {
                        var child = v[i].children;
                        for (var j in child) {
                            if (typeof (child[j].id) != "undefined") {
                                parent += child[j].id + "|";
                            }
                            else {
                                parent += "|";
                            }
                        }
                    }

                    listitem += ids + "-" + parent + "*";
                };


                $("#<%=hdfTest.ClientID%>").val(listitem);
                $("#<%= btnUpdate.ClientID%>").click();
            }

            function DeleteStep(ID) {
                var r = confirm('Bạn muốn xóa bước này?');
                if (r == true) {
                    $("#<%= hdfID.ClientID%>").val(ID);
                    $("#<%= btnDelete.ClientID%>").click();
                }
                else {

                }
            }

            <%-- function DeleteSocialSupport(ID) {
                var r = confirm('Bạn muốn xóa menu này?');
                if (r == true) {
                    $("#<%= hdfID.ClientID%>").val(ID);
                    $("#<%= btnDelete.ClientID%>").click();
                }
                else {

                }
            }--%>
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
