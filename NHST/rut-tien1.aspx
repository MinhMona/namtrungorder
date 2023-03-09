<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="rut-tien1.aspx.cs" Inherits="NHST.rut_tien" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Rút tiền</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <h2 class="content-title">Rút tiền</h2>
            <div class="order-tool clearfix">
                <div class="primary-form custom-width">
                    <table class="customer-table mar-bot3 full-width font-size-16">
                        <tr style="font-weight:bold">
                            <td>Số dư tài khoản
                            </td>
                            <td>
                                <asp:Label ID="lblAccount" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="step-income">
                        <asp:Panel ID="pn" runat="server">
                            <h2 class="content-title">Tạo lệnh rút tiền</h2>
                            <div class="form-row mar-top2">
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-row mar-top2">
                                <div class="lb width-not-full">Số tiền cần rút: </div>
                                <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                    ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="100000" MaxValue="5000000"
                                    NumberFormat-GroupSizes="3" Width="70%" placeholder="Số tiền muốn rút" Value="100000">
                                </telerik:RadNumericTextBox>
                                <a href="javascript:;" onclick="confirmrutien()" class="btn btn-success btn-block pill-btn primary-btn">Tạo lệnh rút tiền</a>
                            </div>
                            <div class="form-row mar-top2">
                                <div class="lb width-not-full">Nội dung: </div>
                                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                <a href="javascript:;" onclick="confirmrutien()" class="btn btn-success btn-block pill-btn primary-btn">Tạo lệnh rút tiền</a>
                            </div>
                            <div class="form-row btn-row">
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="step-income">
                        <h2 class="content-title">Danh sách rút tiền</h2>
                        <div class="step-income">
                            <table class="customer-table mar-top1 full-width">
                                <tr>
                                    <th width="20%" style="text-align: center">Ngày giờ</th>
                                    <th width="20%" style="text-align: center">Số tiền</th>
                                    <th width="20%" style="text-align: center">Trạng thái</th>
                                    <th width="20%" style="text-align: center"></th>
                                </tr>
                                <asp:Literal ID="ltr" runat="server"></asp:Literal>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%--<main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Rút tiền</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    
                </div>
            </div>
        </div>
       
    </main>--%>
    <asp:Button ID="btnCreate" runat="server" Text="Tạo lệnh rút tiền" CssClass="btn btn-success btn-block pill-btn primary-btn"
        OnClick="btnCreate_Click" Style="display: none" />
    <style>
        .width-not-full {
            float: left;
            width: auto;
            margin: 10px 20px 0 0;
        }
        .customer-table th, .customer-table td
        {
            text-align:center;
        }
    </style>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCreate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pn" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSaleGroup" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="s" runat="server">
        <script type="text/javascript">
            function confirmrutien() {
                var r = confirm("Bạn muốn tạo lệnh rút tiền?");
                if (r == true) {
                    $("#<%=btnCreate.ClientID%>").click();
                }
                else {
                }
            }
            function cancelwithdraw(id) {
                var r = confirm("Bạn muốn hủy lệnh rút tiền?");
                if (r == true) {
                    $.ajax({
                        type: "POST",
                        url: "rut-tien.aspx/cancelwithdraw",
                        data: "{ID:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == "1") {
                                location.reload();
                            }
                            else {
                                alert('Có lỗi trong quá trình xử lý, vui lòng thử lại sau');
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi');
                        }
                    });
                }
                else {
                }

            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
