<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="add-another-order.aspx.cs" Inherits="NHST.manager.add_another_order" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap" class="custom-padding-80-200">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Tạo đơn hàng TỪ CÁC TRANG THƯƠNG MẠI ĐIỆN TỬ KHÁC</h4>
                <div class="primary-form">
                    <div class="form-row">
                        <div class="lb">
                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Tên đăng nhập</div>
                        </div>
                        <div class="form-row-right">
                            <strong>
                                <asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Danh sách sản phẩm</div>
                        </div>
                        <div class="form-row-right btn-row" style="">
                            <a href="javascript:;" onclick="deleteAllProduct()" id="deleteAllProduct" class="delete-all" style="float: right; margin-right: 5px; display: none; width: auto;">
                                <i class="fa fa-close"></i>Xóa hết sản phẩm</a>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="table-rps table-responsive">
                            <table class="customer-table mar-top1 full-width normal-table">
                                <thead>
                                    <tr>
                                        <th>Link sản phẩm
                                        </th>
                                        <th>Tên sản phẩm
                                        </th>
                                        <th>Màu sắc, kích thước
                                        </th>
                                        <th>Số lượng
                                        </th>
                                        <th>Yêu cầu thêm
                                        </th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody class="product-list">
                                    <tr class="product-item">
                                        <td>
                                            <input class="product-link form-control margin-custom-5" placeholder="Nhập link sản phẩm" /></td>
                                        <td>
                                            <input class="product-name form-control margin-custom-5" placeholder="Nhập tên sản phẩm" /></td>
                                        <td>
                                            <input class="product-colorsize form-control margin-custom-5" placeholder="Nhập màu sắc, kích thước" /></td>
                                        <td>
                                            <input class="product-quantity form-control margin-custom-5" placeholder="Số lượng" type="number" value="1" min="0" /></td>
                                        <td>
                                            <input class="product-request form-control margin-custom-5" placeholder="Nhập yêu cầu" /></td>
                                        <td><a href="javascript:;" onclick="deleteProduct($(this))" class="btn btn-bg-close"><i class="fa fa-close"></i></a></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-row">
                        <a href="javascript:;" onclick="addProduct()" class="btn btn-bg-submit" style="float: right; margin-right: 5px; width: auto;">
                            <i class="fa fa-plus"></i>Thêm sản phẩm</a>
                    </div>
                    <div class="form-row btn-row">
                        <a href="javascript:;" onclick="CreateOrder()" class="btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display">Tạo đơn hàng</a>

                    </div>
                </div>
            </div>
        </section>
    </main>
    <asp:HiddenField ID="hdfProductList" runat="server" />
    <script type="text/javascript">
        function addProduct() {
            var html = "";
            html += "<tr class=\"product-item\">";
            html += "<td><input class=\"product-link form-control margin-custom-5\" placeholder=\"Nhập link sản phẩm\"/></td>";
            html += "<td><input class=\"product-name form-control margin-custom-5\" placeholder=\"Nhập tên sản phẩm\"/></td>";
            html += "<td><input class=\"product-colorsize form-control margin-custom-5\" placeholder=\"Nhập màu sắc, kích thước\"/></td>";
            html += "<td><input class=\"product-quantity form-control margin-custom-5\" placeholder=\"Số lượng\" type=\"number\" value=\"1\" min=\"0\"/></td>";
            html += "<td><input class=\"product-request form-control margin-custom-5\" placeholder=\"Nhập yêu cầu\"/></td>";
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
            var c = confirm('Bạn muốn xóa sản phẩm này?');
            if (c == true) {
                obj.parent().parent().remove();
            }
            checkShowButton();
        }
        function deleteAllProduct() {
            var c = confirm('Bạn muốn xóa tất cả sản phẩm?');
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
            if ($(".product-item").length > 0) {
                var html = "";
                var check = false;
                $(".product-item").each(function () {
                    var item = $(this);
                    var productlink_obj = item.find(".product-link");
                    var productlink = item.find(".product-link").val();

                    var productname_obj = item.find(".product-name");
                    var productname = item.find(".product-name").val();

                    var productsizecolor_obj = item.find(".product-colorsize");
                    var productsizecolor = item.find(".product-colorsize").val();

                    var productquantity_obj = item.find(".product-quantity");
                    var productquantity = item.find(".product-quantity").val();
                    var productquantityfloat = parseFloat(item.find(".product-quantity").val());

                    var productrequest = item.find(".product-request").val();
                    if (isBlank(productlink)) {
                        //alert('Vui lòng nhập link sản phẩm');
                        check = true;
                    }
                    if (isBlank(productname)) {
                        //alert('Vui lòng nhập tên sản phẩm');
                        
                        check = true;
                    }
                    if (isBlank(productsizecolor)) {
                        //alert('Vui lòng nhập màu sắc, kích thước sản phẩm');
                        
                        check = true;
                    }
                    if (isBlank(productquantity)) {
                        //alert('Vui lòng số lượng cần mua, và số lượng phải lớn hơn 0');
                        
                        check = true;
                    }
                    else if (productquantityfloat <= 0) {
                        
                        check = true;
                    }
                  
                    validateText(productlink_obj);
                    validateText(productname_obj);
                    validateText(productsizecolor_obj);
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
                        var productname = item.find(".product-name").val();
                        var productsizecolor = item.find(".product-colorsize").val();
                        var productquantity = item.find(".product-quantity").val();
                        var productrequest = item.find(".product-request").val();
                        html += productlink + "]" + productname + "]" + productsizecolor + "]" + productquantity + "]" + productrequest + "|";
                    });

                }
                $("#<%=hdfProductList.ClientID%>").val(html);
                $("#<%=btncreateuser.ClientID%>").click();
            }
            else {
                alert('Vui lòng nhập sản phẩm');
            }
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
