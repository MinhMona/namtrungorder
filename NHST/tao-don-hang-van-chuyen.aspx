<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="tao-don-hang-van-chuyen.aspx.cs" Inherits="NHST.tao_don_hang_van_chuyen" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Tạo đơn hàng vận chuyển hộ</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot2">
                                        <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-row marbot1">
                                        Tên đăng nhập
                                    </div>
                                    <div class="form-row marbot2">
                                        <strong>
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                                    </div>

                                    <div class="form-row marbot1">
                                        Chọn kho TQ
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:DropDownList ID="ddlWarehouseFrom" runat="server" CssClass="form-control"
                                            DataValueField="ID" DataTextField="WareHouseName">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-row marbot1">
                                        Chọn kho đích
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control"
                                            DataValueField="ID" DataTextField="WareHouseName">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-row marbot1">
                                        Chọn phương thức vận chuyển
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control"
                                            DataValueField="ID" DataTextField="ShippingTypeName">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-row marbot1">
                                        Danh sách kiện ký gửi
                                        <a href="javascript:;" onclick="deleteAllProduct()" id="deleteAllProduct" class="delete-all" style="display: none"><i class="fa fa-close"></i>Xóa hết kiện</a>
                                    </div>                                    
                                    <div class="form-row marbot2">
                                        <div class="table-rps table-responsive">
                                            <table class="customer-table mar-top1 full-width normal-table">
                                                <thead>
                                                    <tr>
                                                        <th>Mã kiện
                                                        </th>
                                                        <th>Cân nặng
                                                        </th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody class="product-list">
                                                    <tr class="product-item">
                                                        <td>
                                                            <input class="product-link form-control margin-custom-5" placeholder="Nhập mã vận đơn" /></td>
                                                        <td>
                                                            <input class="product-quantity form-control margin-custom-5" placeholder="Số lượng" type="number" value="0" min="0" /></td>
                                                        <td><a href="javascript:;" onclick="deleteProduct($(this))" class="btn btn-bg-close"><i class="fa fa-close"></i></a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="form-row-marbot2">
                                        <a href="javascript:;" onclick="addProduct()" class="btn btn-bg-submit" style="float: right; margin-right: 5px; width: auto;"><i class="fa fa-plus"></i> Thêm kiện</a>
                                    </div>
                                    <div class="form-row marbot1">
                                        Ghi chú
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <a href="javascript:;" onclick="CreateOrder()" class="btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display">Tạo đơn hàng</a>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <asp:HiddenField ID="hdfProductList" runat="server" />
    <script type="text/javascript">
        function addProduct() {
            var html = "";
            html += "<tr class=\"product-item\">";
            html += "<td><input class=\"product-link form-control margin-custom-5\" placeholder=\"Nhập mã vận đơn\"/></td>";
            html += "<td><input class=\"product-quantity form-control margin-custom-5\" placeholder=\"Số lượng\" type=\"number\" value=\"0\" min=\"0\"/></td>";
            html += "<td><a href=\"javascript:;\" onclick=\"deleteProduct($(this))\" class=\"btn btn-bg-close\"><i class=\"fa fa-close\"></i></a></td>";
            html += "</tr>";
            $(".product-list").append(html);
            checkShowButton();
        }
        function checkShowButton() {
            if ($(".product-item").length > 0) {
                $("#deleteAllProduct").show();
            }
            else {
                $("#deleteAllProduct").hide();
            }
        }
        function deleteProduct(obj) {
            var c = confirm('Bạn muốn xóa kiện này?');
            if (c == true) {
                obj.parent().parent().remove();
            }
            checkShowButton();
        }
        function deleteAllProduct() {
            var c = confirm('Bạn muốn xóa tất cả kiện?');
            if (c == true) {
                $(".product-item").remove();
            }
            checkShowButton();
        }
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
        function CreateOrder() {
            //if ($(".product-item").length > 0) {

            //}
            //else {
            //    alert('Vui lòng nhập mã vận đơn');
            //}
            var html = "";
            var check = false;
            $(".product-item").each(function () {
                var item = $(this);
                var productlink_obj = item.find(".product-link");
                var productlink = item.find(".product-link").val();

                var productquantity_obj = item.find(".product-quantity");
                var productquantity = item.find(".product-quantity").val();
                var productquantityfloat = parseFloat(item.find(".product-quantity").val());

                if (isBlank(productlink)) {
                    //alert('Vui lòng nhập link sản phẩm');
                    check = true;
                }

                if (isBlank(productquantity)) {
                    //alert('Vui lòng số lượng cần mua, và số lượng phải lớn hơn 0');

                    check = true;
                }
                else if (productquantityfloat < 0) {

                    check = true;
                }

                validateText(productlink_obj);
                //validateText(productname_obj);
                //validateText(productsizecolor_obj);
                validateText(productquantity_obj);
                validateNumber(productquantity_obj);
            });
            if (check == true) {
                alert('Vui lòng điền đầy đủ thông tin từng sản phẩm');
            }
            else {
                $(".product-item").each(function () {
                    var item = $(this);
                    var productlink = item.find(".product-link").val();
                    var productquantity = item.find(".product-quantity").val();
                    html += productlink + "]" + productquantity + "|";
                });

            }
            $("#<%=hdfProductList.ClientID%>").val(html);
            $("#<%=btncreateuser.ClientID%>").click();
        }
        function validateText(obj) {
            var value = obj.val();
            if (isBlank(value)) {
                obj.addClass("border-select");
            }
            else {
                obj.removeClass("border-select");
            }
        }
        function validateNumber(obj) {
            var value = parseFloat(obj.val());
            if (value <= 0)
                obj.addClass("border-select");
            else
                obj.removeClass("border-select");
        }
        function isBlank(str) {
            return (!str || /^\s*$/.test(str));
        }
    </script>
    <style>
        .table-panel-main table td {
            padding: 20px 10px;
        }

        .form-row-right {
            line-height: 40px;
        }

        .custom-padding-display {
            display: inline-block;
            padding: 10px 40px !important;
        }

        .border-select {
            border: solid 2px red;
        }
    </style>
    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover"
        OnClick="btncreateuser_Click" Style="display: none" />
</asp:Content>
