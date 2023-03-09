<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="SupportDetail.aspx.cs" Inherits="NHST.manager.SupportDetail" %>

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
                                    <h3 class="lb">Chi tiết hỗ trợ</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Username                                            
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Họ tên
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số đt
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Email
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Ảnh
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                        </div>
                                        <div class="form-row marbot1">
                                            Nội dung
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtComplainText" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <a href="/manager/supportlist.aspx" class="btn primary-btn">Trở về</a>
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
    <asp:HiddenField ID="hdfCurrency" runat="server" />
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="sc" runat="server">
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
    </telerik:RadScriptBlock>
</asp:Content>
