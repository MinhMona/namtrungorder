<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="Thanh-toan.aspx.cs" Inherits="NHST.Thanh_toan1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>        
        .checkout-sec .checkout-left {
            float: left;
            width: 50%;
            padding-right: 15px;
        }

        .checkout-sec .checkout-right {
            float: left;
            width: 50%;
            padding-left: 15px;
        }

        .policiy-check {
            float: left;
            text-align: left;
        }

            .policiy-check input[type=checkbox] {
                float: left;
                text-align: left;
                width: auto;
            }

        .feat-tt {
            float: left;
            width: 100%;
            font-size: 14px;
            margin-bottom: 15px;
            text-transform: uppercase;
            font-weight: bold;
        }

        .right {
            float: right;
        }


        .order-detail {
            float: left;
            width: 100%;
            background-color: #fafafa;
            padding: 30px;
            margin-bottom: 20px;
        }

            .order-detail table {
                float: left;
                width: 100%;
                border-collapse: collapse;
            }

                .order-detail table td {
                    vertical-align: middle;
                    padding: 5px 0;
                }

        .hl-txt {
            color: #685142;
        }

        .form-control {
            width: 100%;
        }

        .tool-detail {
            text-align: left;
        }

        .order-detail table td {
            vertical-align: middle;
            padding: 5px 0;
        }

        .order-detail table .borderbtm td {
            padding-bottom: 20px;
        }

        .order-detail table .borderbtm + tr td {
            padding-top: 20px;
        }

        .order-detail table .borderbtm {
            border-bottom: solid 1px #ebebeb;
        }

        .thumb-product .info {
            float: none;
            display: table-cell;
            vertical-align: middle;
            padding: 0 15px 0 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Thanh toán</h4>
                <div class="primary-form custom-width">
                    <div class="order-tool clearfix">
                        <div class="tool-detail">
                            <div class="sec step-sec">
                                <div class="steps">
                                    <div class="step active">
                                        <div class="step-img">
                                            <img src="/App_Themes/NHST/images/order-step-1.png" alt="" class="custom-width-img">
                                        </div>
                                        <h4 class="title">Giỏ hàng</h4>
                                    </div>
                                    <div class="step">
                                        <div class="step-img">
                                            <img src="/App_Themes/NHST/images/order-step-2.png" alt="" class="custom-width-img" width="85px" height="85px">
                                        </div>
                                        <h4 class="title">Chọn địa chỉ nhận hàng</h4>
                                    </div>
                                    <div class="step">
                                        <div class="step-img">
                                            <img src="/App_Themes/NHST/images/order-step-3.png" alt="" class="custom-width-img">
                                        </div>
                                        <h4 class="title">Đặt cọc và kết đơn</h4>
                                    </div>
                                </div>
                            </div>

                            <div class="sec checkout-sec" style="display: inline-block; margin: 50px 0">
                                <div class="checkout-left">
                                    <h4 class="feat-tt">Thông tin tài khoản</h4>
                                    <div class="order-addinfo">
                                        <div>
                                            <div class="form-row">
                                                <div class="lb">Họ tên</div>
                                                <asp:TextBox ID="txt_Fullname" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Địa chỉ</div>
                                                <asp:TextBox ID="txt_Address" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Email</div>
                                                <asp:TextBox ID="txt_Email" runat="server" ReadOnly="true" CssClass="form-control">></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Số điện thoại</div>
                                                <asp:TextBox ID="txt_Phone" runat="server" ReadOnly="true" CssClass="form-control">></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <h4 class="feat-tt" style="margin-top: 50px;">Địa chỉ giao hàng</h4>
                                    <div class="order-addinfo">
                                        <div>
                                            <div class="form-row">
                                                <div class="lb">Họ tên</div>
                                                <asp:TextBox ID="txt_DFullname" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rq1" ControlToValidate="txt_DFullname" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Địa chỉ</div>
                                                <asp:TextBox ID="txt_DAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_DAddress" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Email</div>
                                                <asp:TextBox ID="txt_DEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_DEmail" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Số điện thoại</div>
                                                <asp:TextBox ID="txt_DPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_DPhone" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Chọn kho VN</div>
                                                <asp:DropDownList ID="ddlPlace" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqFavoriteColor" Text="Vui lòng chọn kho đến"
                                                    InitialValue="0" ForeColor="Red"
                                                    ControlToValidate="ddlPlace" runat="server" />

                                            </div>
                                        </div>
                                        <div class="form-row" style="margin-top:20px;">
                                            <asp:CheckBox ID="chk_DK" runat="server" />
                                            Tôi đồng ý với các <a href="/chuyen-muc/chinh-sach" style="color: blue;" target="_blank">điều khoản đặt hàng</a> của Nhập Hàng TQ
                                        </div>
                                        <div class="form-row btn-row">
                                            <asp:Label ID="lblCheckckd" runat="server" Text="Vui lòng xác nhận trước khi hoàn tất đơn hàng." ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                        <div class="form-row btn-row">
                                            <a href="/gio-hang" class="left hl-txt link"><i class="fa fa-long-arrow-alt-left"></i>Quay lại</a>
                                            <a href="javascript:;" id="finishorder" class="right btn pill-btn primary-btn  main-btn hover">HOÀN TẤT</a>
                                            <asp:Button ID="btn_saveOrder" runat="server" OnClick="btn_saveOrder_Click" Text="HOÀN TẤT" Style="display: none;"
                                                CssClass="right btn pill-btn primary-btn  main-btn hover" />
                                        </div>
                                    </div>
                                </div>
                                <div class="checkout-right">
                                    <asp:Literal ID="ltr_pro" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <asp:HiddenField ID="hdfTeamWare" runat="server" />
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                
            </section>
        </div>
    </main>--%>
    <script type="text/javascript">
        function getWareHouse(obj) {
            var wa = obj.val();
            var shippinttype = obj.parent().parent().parent().find(".shippingtype");
            if (wa != "4") {

                shippinttype.show();
                shippinttype.find("select").val("1");
            }
            else {
                shippinttype.hide();
                shippinttype.find("select").val("1");
            }
        }
        $("#finishorder").click(function () {
            var html = "";
            $(".ordershoptem").each(function () {
                var obj = $(this);
                var id = obj.attr("data-id");
                var warehouseID = obj.find(".warehoseselect").val();
                var warehousefromID = obj.find(".warehosefromselect").val();
                //var shippingtype = obj.find(".shippingtypesselect").val();
                var shippingtype = "1";
                html += id + ":" + warehouseID + "-" + shippingtype + "-" + warehousefromID + "|";
            });
            $("#<%= hdfTeamWare.ClientID%>").val(html);
            $("#<%= btn_saveOrder.ClientID%>").click();
        });
    </script>
</asp:Content>
