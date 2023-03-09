<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterNotLogin.Master" AutoEventWireup="true" CodeBehind="quen-mat-khau1.aspx.cs" Inherits="NHST.quen_mat_khau" %>
<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-control{
            width:100%;
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
                            <a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Quên mật khẩu</span>
                        </p>
                        <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
                    </div>
                </div>
                
                <div class="intro-page">
                    <h2 class="content-title">Quên mật khẩu</h2>
                    <div class="form-tt center"></div>
                    <div class="form-row">
                        <div class="lb">
                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="lb">Email</div>
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email để lấy lại Mật khẩu"></asp:TextBox>
                        <span class="error-info-show">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                        </span>
                        <div class="clearfix"></div>
                        <span class="error-info-show">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ForeColor="Red" ErrorMessage="Sai định dạng Email" SetFocusOnError="true">
                            </asp:RegularExpressionValidator>
                        </span>
                    </div>
                    <div class="form-row btn-row">
                        <asp:Button ID="btngetpass" runat="server" Text="Gửi mật khẩu vào mail" CssClass="btn btn-success btn-block pill-btn primary-btn"
                            OnClick="btngetpass_Click" />
                    </div>
                </div>
            </div>
            <uc:sidebar id="SideBar1" runat="server" />
        </div>
    </section>
</asp:Content>
