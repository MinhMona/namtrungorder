<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dang-ky.aspx.cs" Inherits="NHST.dang_ky2" %>
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
   <%-- <script type="text/javascript">
            var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
            (function () {
                var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
                s1.async = true;
                s1.src = 'https://embed.tawk.to/5b597eaddf040c3e9e0bfa19/default';
                s1.charset = 'UTF-8';
                s1.setAttribute('crossorigin', '*');
                s0.parentNode.insertBefore(s1, s0);
            })();
        </script>--%>
    <!--End of Tawk.to Script-->
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scr">
        </asp:ScriptManager>
        <div class="side-full-bg"></div>
        <div class="side-full-cont" id="">
            <div class="logo">
                <a href="/">
                    <img src="/App_Themes/UserNew/images/logo.png" alt="" /></a>
            </div>
            <div class="form form-login">
                <div class="form-row" style="text-align: center; color: #000; text-transform: uppercase">
                    <h2>Đăng ký</h2>
                </div>
                <div class="form-row">
                    <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="form-row">
                    <div class="lb">Họ của bạn:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate" placeholder="Họ"></asp:TextBox>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFirstName" ForeColor="Red"
                            Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                    </span>
                </div>

                <div class="form-row">
                    <div class="lb">Tên của bạn:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                    <div class="clearfix"></div>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="txtLastName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div class="form-row">
                    <div class="lb">Địa chỉ:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" placeholder="Địa chỉ"></asp:TextBox>
                    <div class="clearfix"></div>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtAddress" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div class="form-row">
                    <div class="lb">Số điện thoại:</div>
                </div>
                <div class="form-row">
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
                <div class="form-row">
                    <div class="lb">Email:</div>
                </div>
                <div class="form-row">
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
                <div class="form-row">
                    <div class="lb">Ngày sinh:</div>
                </div>
                <div class="form-row">
                    <div class="ip">
                        <telerik:raddatetimepicker rendermode="Lightweight" id="rBirthday" showpopuponfocus="true" width="100%" runat="server"
                            dateinput-cssclass="radPreventDecorate" placeholder="Ngày sinh" cssclass="date" dateinput-emptymessage="Ngày sinh">
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                    </DateInput>
                                </telerik:raddatetimepicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rBirthday" ErrorMessage="Không để trống"
                            Display="Dynamic" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-row">
                    <div class="lb">Giới tính:</div>
                </div>
                <div class="form-row">
                    <div class="ip">
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="lb">Tên đăng nhập:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control has-validate" placeholder="Tên đăng nhập"></asp:TextBox>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtUsername"
                            ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                    </span>
                    <div class="clearfix"></div>
                    <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                </div>

                <div class="form-row">
                    <div class="lb">Mật khẩu:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate" placeholder="Mật khẩu đăng nhập" TextMode="Password"
                        onkeyup="return passwordChanged();"></asp:TextBox>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtpass" Display="Dynamic" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                    </span>
                    <span class="helper-text">
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator2"
                                            runat="server"
                                            ErrorMessage="Mật khẩu ít nhất 8 ký tự, có ít nhất 1 ký tự viết hoa." Display="Dynamic"
                                            ControlToValidate="txtpass" ForeColor="Red"
                                            ValidationExpression="^(?=.*[A-Z]).{8,15}$" ValidationGroup="Group" />
                                    </span>
                    <span id="strength"></span>
                </div>

                <div class="form-row">
                    <div class="lb">Xác nhận mật khẩu:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtconfirmpass" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                        <span class="helper-text">
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator3"
                                            runat="server"
                                            ErrorMessage="Mật khẩu ít nhất 8 ký tự, có ít nhất 1 ký tự viết hoa." Display="Dynamic"
                                            ControlToValidate="txtconfirmpass" ForeColor="Red"
                                            ValidationExpression="^(?=.*[A-Z]).{8,15}$" ValidationGroup="Group" />
                                    </span>
                        <br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Không trùng với mật khẩu." ControlToCompare="txtpass" ControlToValidate="txtconfirmpass"></asp:CompareValidator>
                    </span>
                </div>
                <div class="form-row">
                    <div class="lb">Tài khoản nhân viên kinh doanh:</div>
                </div>
                <div class="form-row">
                    <asp:TextBox runat="server" ID="txtSalerUsername" CssClass="form-control has-validate" placeholder="Tên đăng nhập NVKD"></asp:TextBox>                    
                    <div class="clearfix"></div>
                    <asp:Label ID="lblSalerError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                </div>
                <div class="form-row">
                    <div class="lb">Kho TQ:</div>
                </div>
                <div class="form-row">
                    <asp:DropDownList ID="ddlWarehouseFrom" runat="server" CssClass="form-control" onchange="returnWeightFee()"
                        DataValueField="ID" DataTextField="WareHouseName">
                    </asp:DropDownList>
                </div>
                <div class="form-row">
                    <div class="lb">Kho đích:</div>
                </div>
                <div class="form-row">
                    <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control" onchange="returnWeightFee()"
                        DataValueField="ID" DataTextField="WareHouseName">
                    </asp:DropDownList>
                </div>
                <div class="form-row">
                    <asp:Button ID="btncreateuser" runat="server" Text="Đăng ký" CssClass="btn primary-btn fw-btn"
                        OnClick="btncreateuser_Click" />
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

