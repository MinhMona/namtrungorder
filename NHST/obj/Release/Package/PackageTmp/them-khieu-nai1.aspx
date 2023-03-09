<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="them-khieu-nai1.aspx.cs" Inherits="NHST.them_khieu_nai" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Tạo khiếu nại</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <h2 class="content-title">Tạo khiếu nại</h2>
            <div class="order-tool clearfix">
                <div class="primary-form custom-width">
                    <div class="step-income">
                        <asp:Panel ID="pn" runat="server">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="lb width-not-full">Mã đơn hàng: </div>
                                <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control full-width"></asp:TextBox>
                            </div>
                            <div class="form-row">
                                <div class="lb width-not-full">Số tiền bồi thường: </div>
                                <telerik:RadNumericTextBox runat="server" CssClass="form-control full-width" Skin="MetroTouch"
                                    ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="0"
                                    NumberFormat-GroupSizes="3" Width="100%">
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="re" runat="server" ControlToValidate="pAmount" Display="Dynamic"
                                    ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-row">
                                <div class="lb width-full">Ảnh đối chiếu: </div>
                                <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="hinhDaiDien" ChunkSize="0"
                                    Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                    MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                </telerik:RadAsyncUpload>
                                <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                            </div>
                            <div class="form-row">
                                <div class="lb width-not-full">Nội dung: </div>
                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNote" Display="Dynamic"
                                    ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-row">
                                <asp:Button ID="btnSend" runat="server" Text="Tạo khiếu nại" CssClass="btn pill-btn primary-btn admin-btn mar-top3" OnClick="btnSend_Click" ValidationGroup="khieunai" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <style>
        .width-not-full {
            float: left;
            width: auto;
            margin: 10px 20px 0 0;
        }

        .btn.pill-btn {
            font-weight: bold;
            text-transform: uppercase;
        }
    </style>
    <telerik:RadAjaxLoadingPanel ID="rxLoading" runat="server" Skin="">
        <div class="loading1">
            <asp:Image ID="Image1" runat="server" ImageUrl="/App_Themes/NHST/loading1.gif" AlternateText="loading" />
        </div>
    </telerik:RadAjaxLoadingPanel>
    <!-- END CONTENT -->
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>
        <script>
            function DelRow(that, link) {

                $(that).parent().parent().remove();
                var myHidden = $("#<%= listImg.ClientID %>");
                var tempF = myHidden.value;
                myHidden.value = tempF.replace(link, '');
            }
            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page
                function OnFileSelected(fileSelected) {
                    if (textBox) {
                        {
                            var myHidden = document.getElementById('<%= listImg.ClientID %>');
                            var tempF = myHidden.value;

                            tempF = tempF + '#' + fileSelected;
                            myHidden.value = tempF;

                            $('.hidImage').append('<tr><td><img height="100px" src="' + fileSelected + '"/></td><td style="text-align:center"><a class="btn btn-success" onclick="DelRow(this,\'' + fileSelected + '\')">Xóa</a></td></li>');
                            //alert(fileSelected);
                            textBox.set_value(fileSelected);
                        }
                    }
                }

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
        <script type="text/javascript">
            function keypress(e) {
                var keypressed = null;
                if (window.event) {
                    keypressed = window.event.keyCode; //IE
                }
                else {
                    keypressed = e.which; //NON-IE, Standard
                }
                if (keypressed < 48 || keypressed > 57) {
                    if (keypressed == 8 || keypressed == 127) {
                        return;
                    }
                    return false;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <style>
        .RadUpload_Metro .ruFakeInput {
            float: left;
            width: 60%;
        }

        .page.account-management .right-content .right-side {
            padding-left: 20px;
        }

        div.RadUploadSubmit, div.RadUpload_Metro .ruButton {
            padding: 0;
        }
    </style>
</asp:Content>
