<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="thong-tin-nguoi-dung1.aspx.cs" Inherits="NHST.thong_tin_nguoi_dung" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        p{
            text-align: initial;
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
                            <a href="/trang-chu" class="color-black">Trang chủ</a> - <span>
                                Thông tin người dùng</span>
                        </p>
                        <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
                    </div>
                </div>
                <div class="intro-page">
                    <h2 class="content-title">Thông tin người dùng</h2>
                    <div class="primary-form">

                        <div class="form-row">
                            <div class="lb">
                                <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="lb">Tên đăng nhập / Nickname</div>
                            <strong><asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                        </div>
                        <div class="form-row">
                            <div class="lb">Họ của bạn</div>
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate full-width" placeholder="Họ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Tên của bạn</div>
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate full-width" placeholder="Tên"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Số điện thoại (dùng để nhận mã kích hoạt tài khoản)</div>
                            <div class="form-group-left">
                                <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2 full-width"></asp:DropDownList>
                                <%--<asp:TextBox runat="server" ID="txt_mavung" CssClass="form-control " Text="+84" ReadOnly ></asp:TextBox>--%>
                            </div>
                            <div class="form-group-right">
                                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control full-width" placeholder="Số điện thoại" Enabled="false"
                                    onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                    MaxLength="11"></asp:TextBox>
                            </div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ForeColor="Red" ErrorMessage="Không được để trống số điện thoại."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Địa chỉ</div>
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control has-validate full-width" placeholder="Địa chỉ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Email</div>
                            <strong><asp:Label ID="lblEmail" runat="server"></asp:Label></strong>
                            <%--<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>--%>
                        </div>
                        <div class="form-row">
                            <div class="lb">Mật khẩu</div>
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate full-width" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <div class="lb">Xác nhận mật khẩu</div>
                            <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate full-width" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:Label ID="lblConfirmpass" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </span>
                        </div>
                        <div class="form-row btn-row">
                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn"
                                OnClick="btncreateuser_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <uc:sidebar id="SideBar1" runat="server" />

        </div>
    </section>
   
    <%--<telerik:radajaxmanager id="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>--%>
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
