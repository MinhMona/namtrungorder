<%@ Page Title="" Language="C#" MasterPageFile="~/123nhaphangMaster.Master" AutoEventWireup="true" CodeBehind="dang-ky2.aspx.cs" Inherits="NHST.dang_ky1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Đăng ký</h4>
                <div class="primary-form">
                    <div class="form-row">
                        <div class="lb">
                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Họ của bạn</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate" placeholder="Họ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFirstName" ForeColor="Red"
                                    Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Tên của bạn</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="txtLastName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Địa chỉ</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" placeholder="Địa chỉ"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtAddress" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Số điện thoại</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                MaxLength="11"></asp:TextBox>
                            <%--<div class="form-group-left">
                                    <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>                                    
                                </div>
                                <div class="form-group-right">
                                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                        MaxLength="11"></asp:TextBox>

                                </div>--%>
                            <div class="clearfix"></div>
                            <%--<span class="mar-top1" style="float: left; width: 100%; margin-top: 10px;text-align:left;">Quý khách vui lòng bỏ số 0 ở đầu dãy số di động. VD: +84-91XXX</span>--%>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtPhone" ForeColor="Red"
                                    ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Email</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                                    ForeColor="Red" Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <div class="clearfix"></div>
                            <asp:Label ID="lblcheckemail" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmail"
                                    ErrorMessage="Sai định dạng Email" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
                            </span>
                        </div>
                       
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Ngày sinh:</div>
                        </div>
                        <div class="form-row-right">
                            <div class="ip">
                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rBirthday" ShowPopupOnFocus="true" Width="100%" runat="server"
                                    DateInput-CssClass="radPreventDecorate" placeholder="Ngày sinh" CssClass="date" DateInput-EmptyMessage="Ngày sinh">
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                    </DateInput>
                                </telerik:RadDateTimePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rBirthday" ErrorMessage="Không để trống"
                                    Display="Dynamic" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Giới tính</div>
                        </div>
                        <div class="form-row-right">
                            <div class="ip">
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Tên đăng nhập:</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control has-validate" placeholder="Tên đăng nhập"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtUsername"
                                    ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <div class="clearfix"></div>
                            <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Mật khẩu</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate" placeholder="Mật khẩu đăng nhập" TextMode="Password"
                                onkeyup="return passwordChanged();"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtpass" Display="Dynamic" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <span id="strength"></span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Xác nhận mật khẩu</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtconfirmpass" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Không trùng với mật khẩu." ControlToCompare="txtpass" ControlToValidate="txtconfirmpass"></asp:CompareValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row btn-row">
                        <asp:Button ID="btncreateuser" runat="server" Text="Đăng ký" CssClass="mn-btn btn-1"
                            OnClick="btncreateuser_Click" />
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
