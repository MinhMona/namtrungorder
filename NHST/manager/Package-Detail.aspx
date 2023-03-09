<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Package-Detail.aspx.cs" Inherits="NHST.manager.Package_Detail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-33 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Cập nhật bao hàng</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Mã bao hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtPackageCode" CssClass="form-control has-validate" placeholder="Mã bao hàng" Enabled="false">
                                            </asp:TextBox>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPackageCode"
                                                    Display="Dynamic" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Cân (kg)
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)" Value="0">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Khối (m3)
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)" Value="0">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Mã vận đơn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox ID="txtMavandon" runat="server" CssClass="form-control" onchange="getcode($(this))"></asp:TextBox>
                                            <%--<telerik:RadComboBox ID="RadComboBox1" runat="server"
                                        CheckBoxes="true" EnableCheckAllItemsCheckBox="true"  Skin="MetroTouch">
                                    </telerik:RadComboBox>--%>
                                        </div>
                                        <div class="form-row marbot1">
                                            Trạng thái
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <%--<asp:ListItem Value="0" Text="Mới tạo"></asp:ListItem>--%>
                                                <asp:ListItem Value="1" Text="Đã về kho TQ"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Đã về kho VN"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Hủy"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn"
                                                OnClick="btncreateuser_Click" UseSubmitBehavior="false" />
                                            <asp:Literal ID="ltrCreateSmallpackage" runat="server" Visible="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                        <div class="grid-col-66 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Danh sách mã vận đơn</h3>
                                </div>
                                <div class="cont900" data-css='{"margin-bottom": "20px"}'>
                                    <div class="pane-shadow filter-form" id="filter-form">
                                        <div class="grid-row">
                                            <div class="grid-col-100">
                                                <div class="lb">Tìm kiếm</div>
                                                <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập mã vận đơn để tìm"></asp:TextBox>
                                            </div>
                                            <div class="grid-col-100 center-txt">
                                                <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <article class="pane-primary">
                                    <div class="table-rps table-responsive">
                                        <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                            AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                            AllowSorting="True">
                                            <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="STT" HeaderText="STT" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PackageCode" HeaderText="Bao hàng" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OrderTransactionCode" HeaderText="Mã vận đơn" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ProductType" HeaderText="Loại hàng" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FeeShip" HeaderText="Phí ship(tệ)" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Weight" HeaderText="Cân (kg)" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Volume" HeaderText="Khối (m3)" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Statusname" HeaderText="Trạng thái" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-Width="5%">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                                        <ItemTemplate>
                                                            <a class="btn btn-info btn-sm" href='/manager/SmallPackage-Detail.aspx?ID=<%#Eval("ID") %>'>Sửa</a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                                    PrevPageText="← Previous" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </article>
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
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
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
        function getcode(obj) {
            var val = obj.val();
            //alert(val);
            val += ";";
            obj.val(val);
        }
    </script>

</asp:Content>
