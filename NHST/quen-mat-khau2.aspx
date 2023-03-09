<%@ Page Title="" Language="C#" MasterPageFile="~/1688Master.Master" AutoEventWireup="true" CodeBehind="quen-mat-khau2.aspx.cs" Inherits="NHST.quen_mat_khau1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Quên mật khẩu</h4>
                <div class="primary-form">
                    <div class="form-row">
                        <div class="lb">
                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Email</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email để lấy lại Mật khẩu"></asp:TextBox>
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
                    </div>
                    <div class="form-row btn-row">
                        <asp:Button ID="btngetpass" runat="server" Text="Gửi mật khẩu vào mail" CssClass="pill-btn btn btn main-btn hover btn-1"
                            OnClick="btngetpass_Click" />
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
