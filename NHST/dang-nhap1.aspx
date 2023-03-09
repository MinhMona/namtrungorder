<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterNotLogin.Master" AutoEventWireup="true" CodeBehind="dang-nhap1.aspx.cs" Inherits="NHST.dang_nhap" %>

<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-control {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="services-page clearfix">
        <div class="container">
            <div class="services-content intro-align">
                <div class="intro-page">
                    <div class="breadcrumb clearfix">
                        <p>
                            <a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Đăng nhập</span>
                        </p>
                        <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
                    </div>
                </div>
                <div class="intro-page">
                    <div class="primary-form">
                        <div class="form-row">
                            <div class="lb">
                                <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="lb">Tên đăng nhập / Nickname / Email</div>
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Tên đăng nhập / Nickname / Email"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Mật khẩu đăng nhập</div>
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtpass" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <a href="/quen-mat-khau" title="Lấy lại pass bằng email" class="color-black" style="margin-right: 15px;">Lấy lại pass bằng Email</a>
                            |
                                    <a href="/dang-ky" class="color-black" style="margin-left: 15px" title="Đăng ký tài khoản mới">Đăng ký tài khoản mới</a>
                        </div>
                        <div class="form-row btn-row">
                            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn btn-success btn-block pill-btn primary-btn"
                                OnClick="btnLogin_Click" UseSubmitBehavior="true" />
                        </div>
                    </div>
                </div>
            </div>
            <uc:SideBar ID="SideBar1" runat="server" />
        </div>
    </section>

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
