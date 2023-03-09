<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="NHST.manager.AddUser" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Thêm mới</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                        </div>

                                        <div class="form-row marbot1">
                                            Họ
                                            <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtFirstName" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" placeholder="Họ"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Tên
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtLastName" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Số điện thoại (dùng để nhận mã kích hoạt tài khoản)
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtPhone" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                                MaxLength="11"></asp:TextBox>
                                            <%-- <div class="form-group-left">
		                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>
	                                            </div>
	                                            <div class="form-group-right">
		                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
			                                            MaxLength="11"></asp:TextBox>
	                                            </div>--%>
                                        </div>

                                        <div class="form-row marbot1">
                                            Ngày sinh
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rBirthday" ErrorMessage="Không để trống"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rBirthday" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                DateInput-CssClass="radPreventDecorate" placeholder="Ngày sinh" CssClass="date" DateInput-EmptyMessage="Ngày sinh">
                                                <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                </DateInput>
                                            </telerik:RadDateTimePicker>
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
                                            Email
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Email đăng nhập"></asp:TextBox>
                                            <div class="clearfix"></div>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ForeColor="Red" ErrorMessage="(Sai định dạng Email)" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        </div>

                                        <div class="form-row marbot1">
                                            Tên đăng nhập / Nickname
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtUsername" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Tên đăng nhập / Nickname"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Mật khẩu
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txt_Password" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txt_Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Xác nhận Mật khẩu
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtConfirmPassword" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" SetFocusOnError="true" ValidationGroup="n" ForeColor="Red" ErrorMessage="(Không trùng khớp với mật khẩu)" ControlToCompare="txt_Password" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtConfirmPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Level
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlLevelID" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataTextField="LevelName"
                                                DataValueField="ID">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1" style="display:none">
                                            VIP Level
                                        </div>
                                        <div class="form-row marbot2" style="display: none">
                                            <asp:DropDownList ID="ddlVipLevel" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="0">VIP 0</asp:ListItem>
                                                <asp:ListItem Value="1">VIP 1</asp:ListItem>
                                                <asp:ListItem Value="2">VIP 2</asp:ListItem>
                                                <asp:ListItem Value="3">VIP 3</asp:ListItem>
                                                <asp:ListItem Value="4">VIP 4</asp:ListItem>
                                                <asp:ListItem Value="5">VIP 5</asp:ListItem>
                                                <asp:ListItem Value="6">VIP 6</asp:ListItem>
                                                <asp:ListItem Value="7">VIP 7</asp:ListItem>
                                                <asp:ListItem Value="8">VIP 8</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1">
                                            Quyền
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="1">User</asp:ListItem>
                                                <asp:ListItem Value="2">Manager</asp:ListItem>
                                                <asp:ListItem Value="3">Nhân viên đặt hàng</asp:ListItem>
                                                <asp:ListItem Value="4">Nhân viên kho TQ</asp:ListItem>
                                                <asp:ListItem Value="5">Nhân viên kho đích</asp:ListItem>
                                                <asp:ListItem Value="6">Nhân viên sale</asp:ListItem>
                                                <asp:ListItem Value="7">Nhân viên kế toán</asp:ListItem>
                                                <asp:ListItem Value="8">Nhân viên thủ kho</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1">
                                            Trạng thái tài khoản
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="1">Chưa kích hoạt</asp:ListItem>
                                                <asp:ListItem Value="2">Đã kích hoạt</asp:ListItem>
                                                <asp:ListItem Value="3">Đang bị khóa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1">
                                            Nhân viên kinh doanh
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlSale" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1">
                                            Nhân viên đặt hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlDathang" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSaleGroup" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
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
