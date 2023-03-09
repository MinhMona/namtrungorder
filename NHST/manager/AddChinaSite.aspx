﻿<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="AddChinaSite.aspx.cs" Inherits="NHST.manager.AddChinaSite" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Thông tin cơ bản</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <asp:Panel runat="server" ID="TTCB">
                                            <div class="form-row marbot1">
                                                Tên trang <span class="require">(*)</span>
                                                <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtSitename"
                                                    ValidationGroup="n" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row marbot2">
                                                <asp:TextBox runat="server" ID="txtSitename" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-row marbot1">
                                                Logo
                                            </div>
                                            <div class="form-row marbot2">
                                                <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="rSiteLogo" ChunkSize="0"
                                                    Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                                    MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                                </telerik:RadAsyncUpload>
                                                <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                                <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                                            </div>
                                            <div class="form-row marbot1">
                                                Ẩn
                                            </div>
                                            <div class="form-row marbot2">
                                                <asp:CheckBox runat="server" ID="chkIshidden" Checked="false"/>
                                            </div>
                                            <div class="form-row no-margin center-txt">
                                                <asp:Button runat="server" ID="btnSave" Text="Tạo mới" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                            </div>
                                        </asp:Panel>
                                        
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>

    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlDVT">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlDVT" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblUnit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery-1.11.0.min.js"></script>
        <script>

            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Admin/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
