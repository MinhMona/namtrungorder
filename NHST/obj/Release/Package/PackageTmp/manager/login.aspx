<%@ Page Title="Đăng nhập nhân viên" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="NHST.manager.login" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Đăng nhập</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="primary-form">

                        <div class="form-row">
                            <div class="lb">
                                <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="lb">Tên đăng nhập</div>
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Tên đăng nhập"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="txtUsername" ForeColor="Red" ValidationGroup="Group"
                                    ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Mật khẩu đăng nhập</div>
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                    ControlToValidate="txtpass" ForeColor="Red" ValidationGroup="Group"
                                    ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <telerik:RadCaptcha ID="RadCaptcha1" runat="server"
                                ErrorMessage="Sai mã captcha." ForeColor="Red"
                                ValidationGroup="Group" EnableRefreshImage="false"
                                CaptchaTextBoxLabel="" CaptchaTextBoxCssClass="custom-width-1">
                                <CaptchaImage ImageCssClass="imageClass"></CaptchaImage>
                            </telerik:RadCaptcha>
                            <asp:Label ID="lblTxtRadCaptcha" Text="Custom Captcha Textbox" Visible="false" runat="server" />
                            <asp:TextBox ID="txtRadCaptcha" runat="server" Visible="false" MaxLength="5"></asp:TextBox>
                            <%--<p class="recaptcha">
                                <div id="capcha"></div>
                                <span class="error-info" id="error-capcha" style="color: red;"></span>
                            </p>--%>
                        </div>
                        <div class="form-row btn-row">
                            <%--<a id="ddangnap" href="javascript:;" class="btn btn-success btn-block pill-btn primary-btn">Đăng nhập
                            </a>--%>
                            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" ValidationGroup="Group"
                                CssClass="btn btn-success btn-block pill-btn primary-btn"
                                UseSubmitBehavior="false" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"
        async defer></script>
    <asp:Button ID="ssssd" runat="server" Text="Đăng nhập" CssClass="btn btn-success btn-block pill-btn primary-btn" Style="display: none"
        UseSubmitBehavior="false" OnClick="btnLogin_Click" />
    <input type="hidden" id="checkcap" />
    <input type="hidden" id="errorcode" />
    
    <script type="text/javascript">
        var onloadCallback = function () {
            grecaptcha.render('capcha', {
                'sitekey': '6Lcr1nkUAAAAAG9MvxtYlcQ41n9zbpvTY7YdCara',
                'callback': function (response) {
                    $.ajax({
                        type: "POST",
                        url: "/login.aspx/VerifyCaptcha",
                        data: "{response: '" + response + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "JSON",
                        success: function (r) {
                            var captchaResponse = jQuery.parseJSON(r.d);
                            if (captchaResponse.success) {
                                $("#checkcap").val(captchaResponse.success);
                                $("#error-capcha").hide();
                            } else {
                                $("#checkcap").val(captchaResponse.success);
                                var error = captchaResponse["error-codes"][0];
                                $("#error").html("RECaptcha error. " + error);
                            }

                            $("#errorcode").val(captchaResponse.error - codes);
                        }, error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi checkend');
                        }
                    });
                }
            });
        };

        function checkcapcha() {
            var kq = $("#checkcap").val();
            if (kq != 'true') {
                $("#error-capcha").show().html("Vui lòng chọn vào captcha.");
            }
            else {
                <%-- var username = $("#<%=txtUsername.ClientID%>").val();
                var pass = $("#<%=txtpass.ClientID%>").val();
                $.ajax({
                    type: "POST",
                    url: "/dang-nhap.aspx/Login",
                    data: "{username:'" + username + "',password:'" + pass + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if(ret == "ok")
                        {
                            window.location = "/trang-chu";
                        }
                        else if (ret == "0") {
                            alert('Tài khoản của bạn chưa được kích hoạt, vui lòng liên hệ với Admin để biết thêm chi tiết.');
                        }
                        else if (ret == "1") {
                            alert('Tài khoản của bạn đang bị khóa, vui lòng liên hệ với Admin để biết thêm chi tiết.');
                        }
                        else if (ret == "2") {
                            alert('Tài khoản hiện không tồn tại trong hệ thống.');
                        }
                        else if (ret == "3") {
                            alert('Đăng nhập không thành công, vui lòng kiểm tra lại.');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });--%>
                <%= ssssd.ClientID%>.click();
            }
        }
        $("#ddangnap").click(function () {
            checkcapcha();
        });
    </script>
    <script type="text/javascript">
        function pageLoad() {
            //Disable autoComplete
            var customCaptchaTextbox = $get("<%=txtRadCaptcha.ClientID %>");
            if (customCaptchaTextbox)
                customCaptchaTextbox.setAttribute("autoComplete", "off");
        }
    </script>
    <telerik:RadAjaxManager ID="ajaxManager" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cbCustomTextBox" EventName="CheckedChanged">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtRadCaptcha"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblTxtRadCaptcha"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadCaptcha1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <style>
        .rcRefreshImage {
            padding-left: 25px;
            padding-right: 25px;
        }

        .imageClass {
            float: left;
            padding-top: 0px;
            padding-bottom: 10px;
        }

        .module1 {
            width: 850px;
            background-color: #dff3ff;
            border: 1px solid #c6e1f2;
            padding: 15px 0 15px 0;
        }

        .custom-width-1 {
            float: left;
            height: 40px;
            font-family: "SFUIText";
            font-size: 14px;
            border: solid 1px #ebebeb;
            padding: 0 10px;
            outline: 0;
            -webkit-transition: all .3s ease-in-out;
            transition: all .3s ease-in-out;
            width: 120px;
            margin-left:20px;
        }
    </style>
</asp:Content>
