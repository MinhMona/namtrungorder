<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dang-nhap.aspx.cs" Inherits="NHST.dang_nhap2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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
    <title>Đăng nhập</title>
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
        <div class="side-full-bg"></div>
        <div class="side-full-cont" id="">
            <div class="logo">
                <a href="/">
                    <img src="/App_Themes/UserNew/images/logo.png" alt="" /></a>
            </div>
            <div class="form form-login">
                <div class="form-row" style="text-align:center;color:#000;text-transform:uppercase">
                    <h2>Đăng nhập</h2>
                </div>
                <div class="form-row">
                    <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                </div>
                <div class="form-row">
                    <div class="lb">Username:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Tên đăng nhập"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtUsername" ErrorMessage="Không để trống"
                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="form-row">
                    <div class="lb">Mật khẩu:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Mật khẩu" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage="Không để trống"
                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <%--<input type="password" class="form-control" placeholder="Your Password">--%>
                </div>
                <div class="form-row clear">
                    <a href="/quen-mat-khau" style="float:left">Quên mật khẩu?</a>
                    <a href="/dang-ky" style="float: right">Đăng ký</a>
                    <%--<label class="checklb left">
                        <input type="checkbox"><span class="ip-avata"></span> Remember me</label>--%>
                    <%--<a href="/quen-mat-khau" class="right">Quên mật khẩu ?</a>--%>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn primary-btn fw-btn" Text="Đăng nhập" OnClick="btnLogin_Click" />
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
</html>
