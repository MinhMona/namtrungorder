<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="NHST.manager.UserInfo" %>

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
                                    <h3 class="lb">Thông tin User</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblTradeHistory" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Tên đăng nhập / Nickname
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Họ của bạn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate" placeholder="Họ"></asp:TextBox>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Tên của bạn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate" placeholder="Tên"></asp:TextBox>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số điện thoại (dùng để nhận mã kích hoạt tài khoản)
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                                MaxLength="11"></asp:TextBox>
                                            <%--<div class="form-row-left">
                                        <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>                                        
                                    </div>
                                    <div class="form-row-right">
                                        <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                            MaxLength="11"></asp:TextBox>
                                    </div>--%>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Địa chỉ
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
                                            <%-- <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </span>--%>
                                        </div>
                                        <div class="form-row marbot1">
                                            Email
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                            <%--<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>--%>
                                        </div>
                                        <div class="form-row marbot1">
                                            <div class="lb">Ngày sinh:</div>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rBirthday" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                DateInput-CssClass="radPreventDecorate" placeholder="Ngày sinh" CssClass="date" DateInput-EmptyMessage="Ngày sinh">
                                                <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                </DateInput>
                                            </telerik:RadDateTimePicker>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rBirthday" ErrorMessage="Không để trống"
                                                Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot1">
                                            <div class="lb">Giới tính</div>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row marbot1">
                                            <div class="lb">Kho nhận</div>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlWarehouseTo" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>

                                        <asp:Panel ID="pnAdmin" runat="server" Visible="false">
                                            <div class="form-row marbot1">
                                                Mật khẩu
                                            </div>
                                            <div class="form-row marbot1">
                                                <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="form-row marbot1">
                                                Xác nhận mật khẩu
                                            </div>
                                            <div class="form-row marbot2">
                                                <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                                                <span class="error-info-show">
                                                    <asp:Label ID="lblConfirmpass" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="form-row col-md-12" style="display: block">
                                                <label for="exampleInputName">Tỉ giá riêng</label>
                                                <br />
                                                <asp:TextBox runat="server" ID="txtCurrency" CssClass="form-control has-validate" placeholder="Tỷ giá"></asp:TextBox>
                                            </div>
                                            <div class="form-row col-md-12" style="display: block">
                                                <label for="exampleInputName">Phí mua hàng riêng (tiền VNĐ)</label>
                                                <br />
                                                <asp:TextBox runat="server" ID="txtFeebuypro" CssClass="form-control has-validate" placeholder="Phí mua hàng"></asp:TextBox>
                                                <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="rFeeBuyPro" MinValue="0" NumberFormat-DecimalDigits="0"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>--%>
                                            </div>
                                            <div class="form-row col-md-12" style="display: block">
                                                <label for="exampleInputName">Phí cân nặng riêng (tiền VNĐ/kg)</label>
                                                <br />
                                                <asp:TextBox runat="server" ID="txtFeeWeight" CssClass="form-control has-validate" placeholder="Phí cân nặng"></asp:TextBox>
                                                <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="rFeeTQVNPerWeight" MinValue="0" NumberFormat-DecimalDigits="0"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>--%>
                                            </div>
                                            <div class="form-row col-md-12" style="display: none">
                                                <label for="exampleInputName">Phần trăm đặt cọc</label>
                                                <br />
                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="rDeposit" MinValue="0" NumberFormat-DecimalDigits="0"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>
                                            </div>

                                            <div class="form-row col-md-12" style="display: block">
                                                <label for="exampleInputName">Số đơn tối đa</label>
                                                <br />
                                                <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="txtNumberOrder" MinValue="0" NumberFormat-DecimalDigits="0"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>--%>
                                                <asp:TextBox runat="server" ID="txtNumberOrder" CssClass="form-control has-validate" placeholder="Số lượng đơn tối đa" MinValue="0" ></asp:TextBox>
                                            </div>

                                            <div class="form-row col-md-12" style="display: block">
                                                <label for="exampleInputName">Số lần nhận </label>
                                                <br />
                                                <asp:TextBox runat="server" ID="txtNumberTake" CssClass="form-control has-validate" placeholder="Số lần nhận tối đa trong ngày"></asp:TextBox>
                                            </div>

                                            <div class="form-row col-md-12" style="display: block">
                                                <label for="exampleInputName">Số tiền tối đa</label>
                                                <br />
                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="txtMaxOrderPrice" MinValue="0" NumberFormat-DecimalDigits="0"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>
                                                <%--<asp:TextBox runat="server" ID="" CssClass="form-control has-validate" placeholder="Số tiền tối đa"></asp:TextBox>--%>
                                            </div>

                                            <div class="form-row col-md-12">
                                                <label for="exampleInputName">Level</label>
                                                <br />
                                                <asp:DropDownList ID="ddlLevelID" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataTextField="LevelName"
                                                    DataValueField="ID">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-row col-md-12" style="display: none;">
                                                <label for="exampleInputName">VIP Level</label>
                                                <br />
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
                                            <div class="grid-col-50">
                                                <label for="exampleInputName">Nhân viên kinh doanh</label>
                                                <br />
                                                <asp:DropDownList ID="ddlSale" runat="server" CssClass="form-control select2" AppendDataBoundItems="true"
                                                    DataValueField="ID" DataTextField="Username">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="grid-col-50">
                                                <label for="exampleInputName">Nhân viên đặt hàng</label>
                                                <br />
                                                <asp:DropDownList ID="ddlDathang" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="grid-col-50">
                                                <label for="exampleInputName">Quyền</label>
                                                <br />
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

                                            <div class="grid-col-50">
                                                <label for="exampleInputName">Chọn phòng ban</label>
                                                <br />
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="1">Kinh doanh 1</asp:ListItem>
                                                    <asp:ListItem Value="2">Kinh doanh 2</asp:ListItem>
                                                    <asp:ListItem Value="3">Kinh doanh 3</asp:ListItem>
                                                    <asp:ListItem Value="4">Kinh doanh 4</asp:ListItem>
                                                     <asp:ListItem Value="5">Kinh doanh 5</asp:ListItem>
                                                    <asp:ListItem Value="6">Kinh doanh 6</asp:ListItem>
                                                    <asp:ListItem Value="7">Kinh doanh 7</asp:ListItem>
                                                    <asp:ListItem Value="8">Kinh doanh 8</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="grid-col-50">
                                                <label for="exampleInputName">Site</label>
                                                <br />
                                                <asp:DropDownList ID="ddlSiteType" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="1">Tmall/Taobao</asp:ListItem>
                                                    <asp:ListItem Value="2">1688.com</asp:ListItem>
                                                    <asp:ListItem Value="3">1688/Tmall/Taobao</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="grid-col-50">
                                                <label for="exampleInputName">Trạng thái tài khoản</label>
                                                <br />
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="1">Chưa kích hoạt</asp:ListItem>
                                                    <asp:ListItem Value="2">Đã kích hoạt</asp:ListItem>
                                                    <asp:ListItem Value="3">Đang bị khóa</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn"
                                                OnClick="btncreateuser_Click" />
                                            <asp:Literal ID="ltrBackButtonSaler" runat="server"></asp:Literal>

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
    <style>
        .grid-row .grid-col-50 {
            width: 50%;
            margin-top: 21px;
        }

        .form-row:last-child {
            margin-bottom: 35px;
            margin-top: 35px;
        }

        
    </style>
</asp:Content>
