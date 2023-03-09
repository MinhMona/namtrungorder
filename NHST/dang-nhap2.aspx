<%@ Page Title="" Language="C#" MasterPageFile="~/1688Master.Master" AutoEventWireup="true" CodeBehind="dang-nhap2.aspx.cs" Inherits="NHST.dang_nhap1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Đăng nhập</h4>
                <div class="primary-form">
                    <div class="form-row">
                        <div class="lb">
                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Tên đăng nhập</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Tên đăng nhập"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" Display="Dynamic"
                                    ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Mật khẩu</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtpass" ForeColor="Red"
                                    Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <div class="link-out">
                                <a href="/quen-mat-khau" title="Lấy lại pass bằng email" class="color-black" style="margin-right: 15px;">Lấy lại pass bằng Email</a>
                                |
                                <a href="/dang-ky" class="color-black" style="margin-left: 15px" title="Đăng ký tài khoản mới">Đăng ký tài khoản mới</a>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                        </div>
                        <div class="form-row-right">
                        </div>
                    </div>
                    <div class="form-row btn-row">
                        <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="pill-btn btn btn main-btn hover btn-1"
                            OnClick="btnLogin_Click" UseSubmitBehavior="true" />
                    </div>
                </div>
            </div>
        </section>
    </main>
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
</asp:Content>
