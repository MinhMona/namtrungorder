<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="NHST.manager.Configuration" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Cấu hình hệ thống</h1>
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Cấu hình hệ thống</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot1">
                                        Tên website
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtWebsitename" CssClass="form-control has-validate" placeholder="Tên website"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWebsitename" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Logo
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="rLogo" ChunkSize="0"
                                            Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                            MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                        </telerik:RadAsyncUpload>
                                        <asp:Image runat="server" ID="imgLogo" Width="200" />
                                        <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Ảnh Banner
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="rBannerIMG" ChunkSize="0"
                                            Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                            MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                        </telerik:RadAsyncUpload>
                                        <asp:Image runat="server" ID="imgBannerIMG" Width="200" />
                                        <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Chrome Extension Link
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <asp:TextBox runat="server" ID="txtChromeExtensionLink" CssClass="form-control has-validate" placeholder="Chrome Extension Link"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Cốc cốc Extension Link
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <asp:TextBox runat="server" ID="txtCocCocExtensionLink" CssClass="form-control has-validate" placeholder="Cốc cốc Extension Link"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Giới thiệu
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadEditor runat="server" ID="rAbount" Width="100%"
                                            Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>
                                    <div class="form-row marbot1">
                                        Địa chỉ VP HN
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadEditor runat="server" ID="rAddress" Width="100%"
                                            Height="200px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>
                                    <div class="form-row marbot1">
                                        Địa chỉ VP HCM
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadEditor runat="server" ID="rAddress2" Width="100%"
                                            Height="200px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>
                                    <div class="form-row marbot1" style="display: none;">
                                        Địa chỉ 3
                                    </div>
                                    <div class="form-row marbot2" style="display: none;">
                                        <telerik:RadEditor runat="server" ID="rAddress3" Width="100%"
                                            Height="200px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>
                                    <div class="form-row marbot1">
                                        Email hỗ trợ
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtEmailSupport" CssClass="form-control has-validate" placeholder="Email hỗ trợ"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmailSupport" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailSupport" ForeColor="Red" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ErrorMessage="(Sai định dạng Email)" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Email liên hệ
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtEmailContact" CssClass="form-control has-validate" placeholder="Email liên hệ"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailContact" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailContact" ForeColor="Red" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ErrorMessage="(Sai định dạng Email)" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Hotline
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtHotline" CssClass="form-control has-validate" placeholder="Hotline"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHotline" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Hotline Kho Hà Nội
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtHotlineKhoHN" CssClass="form-control has-validate" placeholder="Hotline"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                ControlToValidate="txtHotlineKhoHN" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Hotline Kho HCM
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtHotlineKhoHCM" CssClass="form-control has-validate" placeholder="Hotline"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                ControlToValidate="txtHotlineKhoHCM" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Hỗ trợ khách hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtHotlineSupport" CssClass="form-control has-validate" placeholder="Hỗ trợ khách hàng"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Phản hồi
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtHotlineFeedback" CssClass="form-control has-validate" placeholder="Phản hồi"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Facebook
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtFacebook" CssClass="form-control has-validate" placeholder="Facebook"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Twitter
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtTwitter" CssClass="form-control has-validate" placeholder="Twitter"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Google Plus
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtGooglePlus" CssClass="form-control has-validate" placeholder="Google Plus"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Pinterest
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtPinterest" CssClass="form-control has-validate" placeholder="Pinterest"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Instagram
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtInstagram" CssClass="form-control has-validate" placeholder="Instagram"></asp:TextBox>
                                    </div>

                                    <div class="form-row marbot1">
                                        Skype
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtSkype" CssClass="form-control has-validate" placeholder="Skype"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSkype" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none;">
                                        Footer Trái
                                    </div>
                                    <div class="form-row marbot2" style="display: none;">
                                        <telerik:RadEditor runat="server" ID="rFooterTrai" Width="100%"
                                            Height="100px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
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
                                    <div class="form-row marbot1">
                                        Giờ hoạt động
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtTimeWork" CssClass="form-control has-validate" placeholder="Giờ hoạt động"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTimeWork" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Tỷ giá
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pCurrency" MinValue="1000" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="pCurrency" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Tỷ giá thu vô
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rCurrencyIncome" MinValue="1000" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="pCurrency" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none;">
                                        Giá tiền 1 kg hàng
                                    </div>
                                    <div class="form-row marbot2" style="display: none;">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rWeightPrice" MinValue="1000" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="rWeightPrice" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Giá tiền mặc định ký gửi Hà Nội
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pPriceSendDefaultHN" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="pPriceSendDefaultHN" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Giá tiền mặc định ký gửi Việt Trì
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pPriceSendDefaultSG" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="pPriceSendDefaultSG" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none">
                                        Giá tiền mặc định thanh toán hộ
                                    </div>
                                    <div class="form-row marbot2" style="display: none">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pPricePayHelpDefault" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="pPricePayHelpDefault" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1" style="display: none;">
                                        Phần trăm được đặt hàng
                                    </div>
                                    <div class="form-row marbot2" style="display: none;">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rPercent" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rPercent" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Phần trăm nhân viên Sale trong 3 tháng đầu
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rSalePercent" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="pCurrency" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Phần trăm nhân viên Sale sau 3 tháng
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rSalePercentAfter3Month" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="pCurrency" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Phần trăm nhân viên đặt hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rDathangPercent" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="pCurrency" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Thông báo
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                            Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>
                                    <div class="form-row marbot1">
                                        Tiêu đề thông báo
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtNotiPopupTitle" CssClass="form-control has-validate"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Email liên hệ popup
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtEmailNoti" CssClass="form-control has-validate"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Thông báo popup
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadEditor runat="server" ID="rNotiPopup" Width="100%"
                                            Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
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
    </main>
</asp:Content>
