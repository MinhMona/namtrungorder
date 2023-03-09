<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quen-mat-khau.aspx.cs" Inherits="NHST.quen_mat_khau2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=yes" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta property="og:title" content="" />
    <meta property="og:type" content="website" />
    <meta property="og:url" content="" />
    <meta property="og:image" content="" />
    <meta property="og:site_name" content="" />
    <meta property="og:description" content="" />
    <title>Quên mật khẩu</title>
    <link rel="stylesheet" href="/App_Themes/LoginThemes/css/style.css" media="all" />
    <link rel="stylesheet" href="/App_Themes/LoginThemes/css/style-P.css" media="all" />
    <script src="/App_Themes/Ann/js/jquery-1.9.1.min.js"></script>
</head>
<body>
    <!--Start of Tawk.to Script-->
    <script type="text/javascript">
            var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
            (function () {
                var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
                s1.async = true;
                s1.src = 'https://embed.tawk.to/5b597eaddf040c3e9e0bfa19/default';
                s1.charset = 'UTF-8';
                s1.setAttribute('crossorigin', '*');
                s0.parentNode.insertBefore(s1, s0);
            })();
        </script>
    <!--End of Tawk.to Script-->

    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsm" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
        </telerik:RadScriptManager>
        <div class="side-full-bg"></div>
        <div class="side-full-cont" id="">
            <div class="logo">
                <a href="/">
                    <img src="/App_Themes/UserNew/images/logo.png" alt="" /></a>
            </div>
            <div class="form form-login">
                <div class="form-row" style="text-align: center; color: #000; text-transform: uppercase">
                    <h2>Quên mật khẩu</h2>
                </div>
                <div class="form-row">
                    <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="form-row">
                    <div class="lb">Email:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email để lấy lại Mật khẩu"></asp:TextBox>
                    
                </div>
                <div class="form-row">
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                    </span>
                    <div class="clearfix"></div>
                    <span class="error-info-show">
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="txtEmail"
                            ErrorMessage="Sai định dạng Email" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" SetFocusOnError="true"
                            ForeColor="Red" />
                    </span>
                </div>                
                <div class="form-row">
                    <asp:Button ID="btngetpass" runat="server" Text="Gửi mật khẩu vào mail" CssClass="btn primary-btn fw-btn"
                        OnClick="btngetpass_Click" />
                </div>
            </div>
        </div>

        <%--<a href="javascript:;" class="scroll-top-link" id="scroll-top"><i class="fa fa-angle-up"></i></a>--%>
        <script src="/App_Themes/Ann/js/bootstrap.min.js"></script>
        <script src="/App_Themes/Ann/js/bootstrap-table/bootstrap-table.js"></script>
        <script src="/App_Themes/Ann/js/chartjs.min.js"></script>
        <script src="/App_Themes/Ann/js/master.js"></script>
        <script>
  
        </script>
    </form>
</body>
