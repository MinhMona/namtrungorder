<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="nap-tien-te.aspx.cs" Inherits="NHST.nap_tien_te" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-nap-tien {
            float: left;
            width: 100%;
        }

            .page-nap-tien tr {
                float: left;
                width: 100%;
                margin-bottom: 10px;
            }

                .page-nap-tien tr th {
                    float: left;
                    width: 20%;
                    text-align: left;
                    vertical-align: middle;
                    min-height: 1px;
                    font-weight: bold;
                }

                .page-nap-tien tr td {
                    float: left;
                    width: 80%;
                    text-align: left;
                    margin-bottom: 10px;
                }

                    .page-nap-tien tr td textarea {
                        min-height: 150px;
                        width: 100%;
                        border: solid 1px #e1e1e1;
                        padding: 10px;
                    }
                    .table-panel-main table td
                    {
                        text-align:center;
                    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Gửi yêu cầu nạp tiền tệ</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot2">
                                        <asp:Label ID="lblTradeHistory" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-row marbot1">
                                        Tên đăng nhập
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:Literal ID="ltrIfn" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-row marbot1">
                                        Số tiền
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="pAmount" NumberFormat-DecimalDigits="3" Value="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Ghi chú
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="btnSend" runat="server" Text="GỬI YÊU CẦU" CssClass="pill-btn btn order-btn main-btn submit-btn" OnClick="btnSend_Click" />
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Lịch sử nạp tiền tệ</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="subcription">
                                        <asp:Literal ID="ltrPageNamtien" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-confirm-send">                                       
                                        <div class="table-rps table-responsive">
                                            <table class="customer-table mar-top1 full-width normal-table">
                                                <thead>
                                                    <tr>
                                                        <th>Ngày</th>
                                                        <th>Số tiền</th>
                                                        <th>Trạng thái</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="tbl-footer clear">
                                            <div class="subtotal fr">
                                                <asp:Literal ID="ltrTotal" runat="server"></asp:Literal>
                                            </div>
                                            <div class="pagenavi fl">
                                                <%this.DisplayHtmlStringPaging1();%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>

        <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" Style="display: none;" />
    </main>
    <asp:HiddenField ID="hdfTradeID" runat="server" />


    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function deleteTrade(ID) {
                var r = confirm("Bạn muốn hủy yêu cầu?");
                if (r == true) {
                    $("#<%= hdfTradeID.ClientID %>").val(ID);
                    $("#<%= btnclear.ClientID %>").click();
                } else {
                }
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
