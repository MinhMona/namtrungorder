<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="thong-tin-nguoi-dung.aspx.cs" Inherits="NHST.thong_tin_nguoi_dung1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        p {
            text-align: initial;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-50 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Thông tin tài khoản</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot2">
                                        <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-row marbot1">
                                        Tên đăng nhập
                                    </div>
                                    <div class="form-row marbot2">
                                        <strong>
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                                    </div>
                                    <div class="form-row marbot1">
                                        Họ của bạn
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate full-width" placeholder="Họ"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                                    <div class="form-row marbot1">
                                        Tên của bạn
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate full-width" placeholder="Tên"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" Display="Dynamic"
                                                ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Ảnh đối chiếu
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="hinhDaiDien" ChunkSize="0"
                                            Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                            MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                        </telerik:RadAsyncUpload>
                                        <asp:Image runat="server" ID="imgDaiDien" Width="200" style="float:left;clear:both"/>
                                        <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                                    </div>
                                    <div class="form-row marbot1">
                                        Số điện thoại
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control full-width" placeholder="Số điện thoại" Enabled="false"
                                            onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                            MaxLength="11"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ForeColor="Red"
                                                Display="Dynamic" ErrorMessage="Không được để trống số điện thoại."></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                                    <div class="form-row marbot1">
                                        Địa chỉ
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control has-validate full-width" placeholder="Địa chỉ"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ForeColor="Red" ErrorMessage="Không được để trống."
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                                    <div class="form-row marbot1">
                                        Email
                                    </div>
                                    <div class="form-row marbot2">
                                        <strong>
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label></strong>
                                        <%--<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
					                        <span class="error-info-show">
						                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
					                        </span>--%>
                                    </div>

                                    <div class="form-row marbot1">
                                        Ngày sinh
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rBirthday" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Ngày sinh" CssClass="date" DateInput-EmptyMessage="Ngày sinh">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rBirthday" ErrorMessage="Không để trống"
                                            Display="Dynamic" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-row marbot1">
                                        Giới tính
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-row marbot1">
                                        Kho nhận
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:DropDownList ID="ddlWarehouseTo" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-row marbot1">
                                        Mật khẩu
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate full-width" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Xác nhận mật khẩu
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate full-width" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:Label ID="lblConfirmpass" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                        </span>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover"
                                            OnClick="btncreateuser_Click" />
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>
        <script>
            
        </script>
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
        .form-row-right {
            line-height: 40px;
        }
    </style>
</asp:Content>
