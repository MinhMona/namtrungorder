<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="VNWarehouse.aspx.cs" Inherits="NHST.manager.VNWarehouse" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .package-detail {
            float: left;
            border: dashed 1px #000;
            padding: 10px;
            margin-bottom: 30px;
            min-height: 490px;
        }

        .row-package {
            float: left;
            width: 100%;
            margin-bottom: 10px;
        }

        .package-label {
            float: left;
            width: 45%;
        }

        .width-50-per {
            width: 100%;
        }

        .custom-small-button {
            width: 30% !important;
            margin-top: 5px;
        }

        .hidden-btn {
            display: none;
        }

        #outall-package {
            margin: 20px 0;
            float: left;
            width: auto;
        }

        .isvekho {
            background: #2154b0;
            border: dashed #fff 1px;
        }

            .isvekho span {
                color: #fff;
            }

        .ishuy {
            background: #000;
            border: dashed #fff 1px;
        }

            .ishuy span {
                color: #fff;
            }

        .isthatlac {
            background: #ff7247;
            border: dashed #fff 1px;
        }

            .isthatlac span {
                color: #fff;
            }

        .isdagiao {
            background: #00ff43;
            border: dashed #fff 1px;
        }

            .isdagiao span {
                color: #000;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-100 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Kiểm kho Việt Nam</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            <label>Mã bao hàng / Mã vận đơn</label>
                                        </div>
                                        <div class="form-row marbot1">
                                            <input id="barcode-input" class="form-control barcode-area width-50-per" type="text" />
                                            <%--<input id="barcode-input" class="form-control barcode-area width-50-per" type="text"
                                                oninput="getCode($(this))" />--%>
                                        </div>
                                        <a href="javascript:;" onclick="UpdateAll()" class="btn primary-btn">Cập nhật tất cả đơn</a>
                                        <div class="form-row marbot1">
                                            <div class="row error-outstock" style="margin-bottom: 30px;">
                                            </div>
                                            <div class="row listpack">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        <asp:HiddenField ID="hdfTotalPrice" runat="server" />
        <div class="userlist-outstock" style="display: none">
        </div>
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
    <asp:HiddenField ID="hdfListBarcode" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#barcode-input').focus();
            $('#barcode-input').keydown(function (e) {
                if (e.key === 'Enter') {
                    getCode($(this));
                    e.preventDefault();
                    return false;
                }
            });
        });

        function UpdateAll() {
            $(".package-detail").length > 0
            {
                $(".package-detail").each(function () {
                    debugger;
                    var barcode = $(this).attr("data-barcode");
                    var packageID = $(this).attr("data-packageID");
                    updateWeightNew(barcode, packageID, $(this).find(".updatebutton"), packageID);
                    alert('Cập nhật thành công !');
                  

                })
            }
        }

        function getCode_old(obj) {
            var bc = obj.val();
            var bc_e = obj.val() + ",bc-" + obj.val();
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            var bs = listbarcode.split('|');
            var check = false;
            for (var i = 0; i < bs.length - 1; i++) {
                if (bc_e == bs[i]) {
                    check = true;
                }
            }
            if (check == false) {
                $.ajax({
                    type: "POST",
                    url: "/manager/VNWareHouse.aspx/GetCodeInfo",
                    data: "{barcode:'" + bc + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var listdata = JSON.parse(msg.d);
                        if (listdata != "none") {
                            for (var i = 0; i < listdata.length; i++) {
                                var data = listdata[i];
                                var UID = data.UID;
                                var Wallet = data.Wallet;
                                var check = false;
                                $(".user-out").each(function () {
                                    if ($(this).attr("data-uid") == UID) {
                                        check = true;
                                    }
                                })
                                if (check == false) {
                                    var uhtml = "<div class=\"user-out\" data-uid=\"" + UID + "\" data-username=\"" + data.Username + "\" data-wallet=\"" + Wallet + "\" ></div>";
                                    $(".userlist-outstock").append(uhtml);
                                }

                                var html = '';
                                html += "<div class=\"col-md-4 package-item\" >";
                                html += "   <div id=\"bc-" + data.BarCode + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/Admin/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Kiểm đếm:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Tên khách:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.Fullname + "</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Email:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.Email + "</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Phone:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.Phone + "</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Địa chỉ:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.Address + "</span>";
                                html += "       </div>";
                                var status = data.Status;
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";

                                html += "           <select class=\"package-status-select\">";
                                if (status < 2)
                                    html += "               <option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                else if (status > 1 && status < 4) {
                                    html += "               <option value=\"3\">Đã về kho đích</option>";
                                }
                                else if (status == 4) {
                                    html += "               <option value=\"4\" selected>Đã giao khách</option>";
                                }
                                html += "           </select>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                html += "       </div>";
                                //html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                //html += "           <span class=\"package-label\">Kích thước:</span>";
                                //html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"0\"/> x ";
                                //html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"0\"/> x ";
                                //html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"0\"/>";
                                //html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Ghi chú:</span>";
                                html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú:\" style=\"color:#000;\" value=\"" + data.Description + "\"/>";
                                html += "       </div>";
                                html += "       <div class=\"row-package \">";
                                if (status > 1 && status < 4) {
                                    html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                                }
                                else
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                                html += "       </div>";
                                html += "       <div class=\"row-package-status \">";
                                html += "       </div>";
                                html += "   </div>";
                                html += "</div>";

                                listbarcode += data.BarCode + ",bc-" + data.BarCode + "|";

                                $(".listpack").prepend(html);
                                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                                obj.val("");
                                $("#outall-package").show();
                                countOrder();
                            }
                        }
                        else {
                            alert('Không tìm thấy');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            } else {
                obj.val("");
                add_loading();
                $(".package-detail").each(function () {
                    var ordercodetrans = $(this).attr("data-barcode");
                    if (ordercodetrans == bc) {
                        updateWeight(bc, 'bc-' + bc, $(this).find(".updatebutton"));
                    }
                })
            }

        }
        function updateWeight(barcode, id, obj) {
            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            //alert(quantity + " - " + status);
            var bigpackage = "0";
            if (quantity > 0) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/admin/VNWareHouse.aspx/UpdateQuantity",
                    data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'" + bigpackage + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        //if (data == "1") {
                        //    $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:blue");
                        //}
                        if (status == 0) {
                            obj.parent().parent().addClass("ishuy");
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:white");
                            obj.parent().find(".updatebutton").hide();
                        }
                        else {
                            obj.parent().parent().addClass("isvekho");
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:white");
                        }

                        remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert(errorthrow);
                    }
                });
            }
            else {
                obj.parent().parent().find(".row-package-status").html("<span style=\"color:red\">Chưa nhập số kg</span>");
                obj.parent().parent().attr("style", "border:solid 2px red;");
            }
        }

        function getCode_finish(obj) {
            var bc = obj.val();
            var bc_e = obj.val() + ",bc-" + obj.val();
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();


            var bs = listbarcode.split('|');
            var check = false;
            for (var i = 0; i < bs.length - 1; i++) {
                if (bc_e == bs[i]) {
                    check = true;
                }
            }
            if (check == false) {
                $.ajax({
                    type: "POST",
                    url: "/admin/VNWareHouse.aspx/GetCode",
                    data: "{barcode:'" + bc + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);

                        if (data != "none") {
                            var UID = data.UID;
                            var Wallet = data.Wallet;
                            var check = false;
                            $(".user-out").each(function () {
                                if ($(this).attr("data-uid") == UID) {
                                    check = true;
                                }
                            })
                            if (check == false) {
                                var uhtml = "<div class=\"user-out\" data-uid=\"" + UID + "\" data-username=\"" + data.Username + "\" data-wallet=\"" + Wallet + "\" ></div>";
                                $(".userlist-outstock").append(uhtml);
                            }

                            var html = '';
                            html += "<div class=\"col-md-4 package-item\" >";
                            html += "   <div id=\"bc-" + data.BarCode + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                            //html += "       <div class=\"row-package\">";
                            //html += "           <span class=\"package-label\"><strong>Username:</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Username + "</strong></span>";
                            //html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/Admin/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Kiểm đếm:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Tên khách:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Fullname + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Email:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Email + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Phone:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Phone + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Địa chỉ:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Address + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package status-pack\">";
                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                            html += "           <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                            html += "       </div>";

                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Trọng lượng:</span>";
                            html += "           <span class=\"package-info packageWeight\">" + data.TotalWeight + " kg</span>";
                            html += "       </div>";

                            html += "       <div class=\"row-package \">";

                            html += "       </div>";
                            html += "       <div class=\"row-package-status \">";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";

                            listbarcode += data.BarCode + ",bc-" + data.BarCode + "|";

                            $(".listpack").prepend(html);
                            $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                            obj.val("");
                            $("#outall-package").show();
                            countOrder();
                        }
                        else {
                            alert('Không tìm thấy');

                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            } else {
                obj.val("");
            }

        }
        function countOrder() {
            //countorder
            var count = $(".package-detail").length;
            $("#countorder").html(count);
        }

        function updateWeight_old(barcode, id, obj) {
            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            var bigpackage = "0";
            if (quantity > 0) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/admin/TQWareHouse.aspx/UpdateQuantity",
                    data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'" + bigpackage + "'}",
                    //data: "{barcode:'" + barcode + "',status:'" + status + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        if (data == "1") {
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:blue");
                        }
                        remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert(errorthrow);
                    }
                });
            }
            else {
                obj.parent().parent().find(".row-package-status").html("<span style=\"color:red\">Chưa nhập số kg</span>");
                obj.parent().parent().attr("style", "border:solid 2px red;");
            }
        }

        function xuatkho(Barcode, id) {
            add_loading();
            //alert(Barcode);
            $.ajax({
                type: "POST",
                url: "/admin/OutStock.aspx/SetFinish",
                data: "{barcode:'" + Barcode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data == "ok") {
                        //obj.parent().parent().find(".status-pack").html("<span class=\"package-label\">Trạng thái:</span><span class=\"package-info packageStatus bg-blue\">Đã giao</span>");
                        //obj.parent().parent().find(".xuatkhobutton").remove();
                        //obj.parent().parent().find(".huyxuatkhobutton").remove();

                        //obj.parent().find(".status-pack").html("<span class=\"package-info packageStatus\">Đã giao</span>");

                        $("#" + id).find(".status-pack").html("<span class=\"package-label\">Trạng thái:</span><span class=\"package-info packageStatus bg-blue\">Đã giao</span>");
                        $("#" + id).find(".xuatkhobutton").remove();
                        $("#" + id).find(".huyxuatkhobutton").remove();
                        $("#" + id).find('.row-package-status').html('Xuất kho thành công.').attr("style", "color:blue");
                    }
                    else if (data != "ok" && data != "none") {

                        $("#" + id).find('.row-package-status').html('Đơn hàng barcode: ' + Barcode + '<br/> Còn thiếu: ' + data + ' VNĐ để nhận hàng.');

                        //swal
                        //(
                        //    {
                        //        title: 'Thông báo',
                        //        text: 'Đơn hàng barcode: ' + Barcode + ', còn thiếu: ' + data + ' VNĐ để nhận hàng',
                        //        type: 'error'
                        //    }
                        //    //function () { window.location.replace(window.location.href); }
                        //);
                    }
                    remove_loading();
                    countOrder();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });

        }
        function huyxuatkho(barcode, obj) {
            var r = confirm("Bạn muốn tắt package này?");
            if (r == true) {
                var id = barcode + "|";
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                listbarcode = listbarcode.replace(id, "");
                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                obj.parent().parent().parent().remove();
                if ($(".package-item").length == 0) {
                    $("#outall-package").hide();
                }
                countOrder();
            }
        }
        function xuatkhoall() {
            var r = confirm("Bạn muốn xuất kho tất cả đơn hàng này?");
            if (r == true) {
                $(".error-outstock").html("");
                add_loading();
                if ($(".user-out").length > 0) {
                    $(".user-out").each(function () {
                        var UID = $(this).attr("data-uid");
                        var Username = $(this).attr("data-username");
                        $.ajax({
                            type: "POST",
                            url: "/admin/OutStock.aspx/GetWallet",
                            data: "{UID:'" + UID + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                var data = msg.d;
                                var userWallet = parseFloat(data);
                                var totalpriceorder = 0;
                                $(".package-detail").each(function () {
                                    if ($(this).attr("data-uid") == UID) {
                                        if ($(this).attr("data-status") == 5)
                                            totalpriceorder += parseFloat($(this).attr("data-totalprice"));
                                    }
                                });
                                if (userWallet >= totalpriceorder) {
                                    $(".package-detail").each(function () {
                                        if ($(this).attr("data-uid") == UID) {
                                            var barcode = $(this).attr("data-barcode");
                                            var id = $(this).attr("ID");
                                            xuatkho(barcode, id);
                                        }
                                    });
                                }
                                else {
                                    var moneyneed = numberWithCommas(totalpriceorder - userWallet);
                                    var error = "";
                                    error += "<div class=\"col-md-12\" style=\"color:red\">";
                                    error += "Username: <strong>" + Username + "</strong> còn thiếu: <strong>" + moneyneed + "</strong> vnđ để xuất kho <br/>";
                                    error += "</div>";
                                    //$(".error-outstock").append("Username: " + Username + " còn thiếu: " + moneyneed + " vnđ để xuất kho <br/>");
                                    $(".error-outstock").append(error);
                                    remove_loading();
                                }
                                countOrder();
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                //alert('lỗi checkend');
                            }
                        });
                    });
                }

               <%-- var listbarcode1 = $("#<%=hdfListBarcode.ClientID%>").val();
                var listbarcode = listbarcode1.split('|');
                for (var i = 0; i < listbarcode.length - 1; i++) {
                    var b = listbarcode[i];
                    var bcid = b.split(',');
                    xuatkho(bcid[0], bcid[1]);
                }--%>
            } else {

            }

        }
        function xuatkhoForall(Barcode) {
            $.ajax({
                type: "POST",
                url: "/admin/OutStock.aspx/SetFinish",
                data: "{barcode:'" + Barcode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;

                    //if (data == "ok") {
                    //    obj.parent().parent().find(".status-pack").html("<span class=\"package-label\">Trạng thái:</span><span class=\"package-info packageStatus bg-blue\">Đã giao</span>");
                    //    obj.parent().parent().find(".xuatkhobutton").remove();
                    //    //obj.parent().find(".status-pack").html("<span class=\"package-info packageStatus\">Đã giao</span>");

                    //}
                    countOrder();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });

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
        function numberWithCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ".");
            return parts;
            //return parts.join(".");
        }
    </script>
    <script type="text/javascript">
        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
        function add_loading() {
            $(".page-inner").append("<div class='loading_bg'></div>");
            var height = $(".page-inner").height();
            $(".loading_bg").css("height", height + "px");
        }
        function remove_loading() {
            $(".loading_bg").remove();
        }
        //Phần mới
        function getCode(obj) {
            var bc = obj.val();
            if (isEmpty(bc)) {
                alert('Vui lòng nhập mã bao hàng / mã vận đơn');
            }
            else {
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                $.ajax({
                    type: "POST",
                    url: "/manager/VNWareHouse.aspx/GetListPackage",
                    data: "{barcode:'" + bc + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var bi = JSON.parse(msg.d);
                        if (bi != "none") {
                            var biType = bi.BigpackageType;
                            var listsmallpackage = bi.Smallpackages;

                            if (biType == 1) {
                                for (var i = 0; i < listsmallpackage.length; i++) {
                                    var data = listsmallpackage[i];
                                    var UID = data.UID;
                                    var packageID = data.ID;
                                    var isExist = false;
                                    if ($(".package-detail").length > 0) {
                                        $(".package-detail").each(function () {
                                            var dt_packageID = $(this).attr("data-packageID");
                                            if (packageID == dt_packageID) {
                                                isExist = true;
                                            }
                                        });
                                    }
                                    if (isExist == false) {
                                        var orderType = data.OrderType;
                                        var idpack = "bc-" + data.BarCode + "-" + packageID;
                                        var html = '';
                                        html += "<div class=\"col-md-3 package-item\" >";
                                        if (data.Status == 0) {
                                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail ishuy\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        else if (data.Status == 2) {
                                            if (data.IsThatlac == true)
                                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail isthatlac\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                            else
                                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail \" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        else if (data.Status > 2 && data.Status < 4) {

                                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        else {
                                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail isdagiao\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        //html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-status=\"" + data.Status + "\">";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                        html += "       </div>";
                                        if (orderType == "Đơn hàng mua hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Số loại sản phẩm:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soloaisanpham + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Số lượng sản phẩm:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soluongsanpham + "</strong></span>";
                                            html += "       </div>";
                                            if (data.Kiemdem == "Có") {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Kiểm đếm:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Kiemdem + "</strong></span>";
                                                html += "       </div>";
                                            }
                                            else {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong >Kiểm đếm:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                                                html += "       </div>";
                                            }
                                            if (data.Donggo == "Có") {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Đóng gỗ:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Donggo + "</strong></span>";
                                                html += "       </div>";
                                            } else {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                                                html += "       </div>";
                                            }
                                        }
                                        else if (orderType == "Đơn hàng VC hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong></span>";
                                            html += "       </div>";
                                        }
                                        else {
                                            //html += "       <div class=\"row-package\">";
                                            //html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            //html += "       </div>";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                        html += "       </div>";
                                        if (orderType == "Đơn hàng mua hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\">Username:</span>";
                                            html += "           <span class=\"package-info packageCode\"><strong><a href=\"/manager/PrintStamp.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.Username + "</a></strong></span>";
                                            html += "       </div>";
                                        }
                                        else {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\">Username:</span>";
                                            html += "           <span class=\"package-info packageCode\">" + data.Username + "</span>";
                                            html += "       </div>";
                                        }
                                        //html += "       <div class=\"row-package\">";
                                        //html += "           <span class=\"package-label\">Username:</span>";
                                        //html += "           <span class=\"package-info packageCode\">" + data.Username + "</span>";
                                        //html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Tên khách:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.Fullname + "</span>";
                                        html += "       </div>";
                                        //html += "       <div class=\"row-package\">";
                                        //html += "           <span class=\"package-label\">Email:</span>";
                                        //html += "           <span class=\"package-info packageCode\">" + data.Email + "</span>";
                                        //html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Phone:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.Phone + "</span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Địa chỉ:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.Address + "</span>";
                                        html += "       </div>";
                                        var status = data.Status;
                                        if (status == 1) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\">";
                                            html += "               <option value=\"3\">Đã về kho đích</option>";
                                            html += "           </select>";
                                            html += "       </div>";
                                        }
                                        else if (status == 2) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\">";
                                            html += "               <option value=\"3\">Đã về kho đích</option>";
                                            html += "           </select>";
                                            html += "       </div>";
                                        }
                                        else if (status == 3) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                            html += "               <option value=\"3\">Đã về kho đích</option>";
                                            html += "           </select>";
                                            html += "           <span class=\"package-info packageStatus bg-blue\" style=\"color:#000;\">Đã về kho đích</span>";
                                            html += "       </div>";
                                        }
                                        else if (status == 4) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                            html += "               <option value=\"4\">Đã giao khách</option>";
                                            html += "           </select>";
                                            html += "           <span class=\"package-info packageStatus bg-green\" style=\"color:#000;\">Đã giao khách</span>";
                                            html += "       </div>";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                        html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.Weight + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                        html += "           <span class=\"package-label\">Kích thước:</span>";
                                        html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                        html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                        html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Ghi chú:</span>";
                                        html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú:\" style=\"color:#000;\" value=\"" + data.Description + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Hình ảnh:</strong></span>";
                                        html += " <label>";
                                        html += " <input id=\"images\" type=\"file\" multiple onchange=\"readFiles(this,'" + idpack + "');\"></span>"
                                        html += "    <span>";
                                        html += "     Thêm ảnh";
                                        html += "     </span > ";
                                        html += "  </label>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package " + idpack + "\">";
                                        html += "<ul class=\"row-package " + idpack + "\">";
                                        if (data.IMG != null) {
                                            var IMG = data.IMG.split('|');
                                            for (var i = 0; i < IMG.length - 1; i++) {
                                                html += "           <li><div class=\"close1\"><i class=\"close_message\" onclick=\"Remove($(this))\"></i></div><a  href =\"" + IMG[i] + "\" data-lightbox=\"image-1\"><img src=\"" + IMG[i] + "\"></a></li>";

                                            }
                                        }
                                        html += "</ul>";
                                        html += "       </div>";



                                        var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                        html += "       <div class=\"row-package \">";
                                        if (status > 1 && status < 4) {
                                            html += "           <a href=\"javascript:;\" onclick=\"updateWeightNew('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                            html += "           <a href=\"javascript:;\" onclick=\"updateIsLost('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"thatlacbutton btn btn-success btn-block small-btn custom-small-button\">Thất lạc</a>";
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                                        }
                                        else
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                                        html += "       </div>";
                                        html += "       <div class=\"row-package-status \">";
                                        html += "       </div>";
                                        html += "   </div>";
                                        html += "</div>";

                                        listbarcode += strItem;

                                        $(".listpack").prepend(html);
                                        $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                                        $("#outall-package").show();
                                        countOrder();
                                    }

                                }
                                obj.val("");
                            }
                            else {
                                for (var i = 0; i < listsmallpackage.length; i++) {
                                    var data = listsmallpackage[i];
                                    var UID = data.UID;
                                    var packageID = data.ID;
                                    var isExist = false;
                                    if ($(".package-detail").length > 0) {
                                        $(".package-detail").each(function () {
                                            var dt_packageID = $(this).attr("data-packageID");
                                            if (packageID == dt_packageID) {
                                                isExist = true;
                                            }
                                        });
                                    }
                                    if (isExist == false) {
                                        var orderType = data.OrderType;
                                        var idpack = "bc-" + data.BarCode + "-" + packageID;
                                        var html = '';
                                        html += "<div class=\"col-md-3 package-item\" >";
                                        if (data.Status == 3) {

                                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        else {
                                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail isdagiao\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        //html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-status=\"" + data.Status + "\">";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                        html += "       </div>";
                                        if (orderType == "Đơn hàng mua hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Số loại sản phẩm:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soloaisanpham + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Số lượng sản phẩm:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soluongsanpham + "</strong></span>";
                                            html += "       </div>";
                                            if (data.Kiemdem == "Có") {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Kiểm đếm:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Kiemdem + "</strong></span>";
                                                html += "       </div>";
                                            }
                                            else {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong >Kiểm đếm:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                                                html += "       </div>";
                                            }
                                            if (data.Donggo == "Có") {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Đóng gỗ:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Donggo + "</strong></span>";
                                                html += "       </div>";
                                            } else {
                                                html += "       <div class=\"row-package\">";
                                                html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                                                html += "       </div>";
                                            }
                                        }
                                        else if (orderType == "Đơn hàng VC hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong></span>";
                                            html += "       </div>";
                                        }
                                        else {
                                            //html += "       <div class=\"row-package\">";
                                            //html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            //html += "       </div>";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                        html += "       </div>";
                                        if (orderType == "Đơn hàng mua hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\">Username:</span>";
                                            html += "           <span class=\"package-info packageCode\"><strong><a href=\"/manager/PrintStamp.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.Username + "</a></strong></span>";
                                            html += "       </div>";
                                        }
                                        else {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\">Username:</span>";
                                            html += "           <span class=\"package-info packageCode\">" + data.Username + "</span>";
                                            html += "       </div>";
                                        }
                                        //html += "       <div class=\"row-package\">";
                                        //html += "           <span class=\"package-label\">Username:</span>";
                                        //html += "           <span class=\"package-info packageCode\">" + data.Username + "</span>";
                                        //html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Tên khách:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.Fullname + "</span>";
                                        html += "       </div>";
                                        //html += "       <div class=\"row-package\">";
                                        //html += "           <span class=\"package-label\">Email:</span>";
                                        //html += "           <span class=\"package-info packageCode\">" + data.Email + "</span>";
                                        //html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Phone:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.Phone + "</span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Địa chỉ:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.Address + "</span>";
                                        html += "       </div>";
                                        var status = data.Status;
                                        if (status == 1) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\">";
                                            html += "               <option value=\"3\">Đã về kho đích</option>";
                                            html += "           </select>";
                                            html += "       </div>";
                                        }
                                        else if (status == 2) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\">";
                                            html += "               <option value=\"3\">Đã về kho đích</option>";
                                            html += "           </select>";
                                            html += "       </div>";
                                        }
                                        else if (status == 3) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                            html += "               <option value=\"3\">Đã về kho đích</option>";
                                            html += "           </select>";
                                            html += "           <span class=\"package-info packageStatus bg-blue\" style=\"color:#000;\">Đã về kho đích</span>";
                                            html += "       </div>";
                                        }
                                        else if (status == 4) {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                            html += "               <option value=\"4\">Đã giao khách</option>";
                                            html += "           </select>";
                                            html += "           <span class=\"package-info packageStatus bg-green\" style=\"color:#000;\">Đã giao khách</span>";
                                            html += "       </div>";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                        html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.Weight + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                        html += "           <span class=\"package-label\">Kích thước:</span>";
                                        html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                        html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                        html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Ghi chú:</span>";
                                        html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú:\" style=\"color:#000;\" value=\"" + data.Description + "\"/>";
                                        html += "       </div>";

                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Hình ảnh:</strong></span>";
                                        html += " <label>";
                                        html += " <input id=\"images\" multiple type=\"file\" onchange=\"readFiles(this,'" + idpack + "');\"></span>"
                                        html += "    <span>";
                                        html += "     Thêm ảnh";
                                        html += "     </span > ";
                                        html += "  </label>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package " + idpack + "\">";
                                        html += "<ul class=\"row-package " + idpack + "\">";
                                        if (data.IMG != null) {
                                            var IMG = data.IMG.split('|');
                                            for (var i = 0; i < IMG.length - 1; i++) {

                                                html += "           <li><div class=\"close1\"><i class=\"close_message\" onclick=\"Remove($(this))\"></i></div><a  href =\"" + IMG[i] + "\" data-lightbox=\"image-1\"><img src=\"" + IMG[i] + "\"></a></li>";
                                            }
                                        }
                                        html += "</ul>";
                                        html += "       </div>";

                                        var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                        html += "       <div class=\"row-package \">";
                                        if (status > 1 && status < 4) {
                                            html += "           <a href=\"javascript:;\" onclick=\"updateWeightNew('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                                        }
                                        else
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                                        html += "       </div>";
                                        html += "       <div class=\"row-package-status \">";
                                        html += "       </div>";
                                        html += "   </div>";
                                        html += "</div>";

                                        listbarcode += strItem;

                                        $(".listpack").prepend(html);
                                        $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                                        $("#outall-package").show();
                                        countOrder();
                                    }
                                    else {
                                        obj.val("");
                                        add_loading();
                                        $(".package-detail").each(function () {
                                            var ordercodetrans = $(this).attr("data-barcode");
                                            if (ordercodetrans == bc) {
                                                var packageID = $(this).attr("data-packageID");
                                                var idpack = "bc-" + bc + "-" + packageID;
                                                updateWeightNew(bc, idpack, $(this).find(".updatebutton"), packageID);
                                            }
                                        })
                                    }
                                }
                                obj.val("");
                            }
                        }
                        else {
                            alert('Không tìm thấy thông tin');
                            obj.val("");
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }
        }
        function Remove(obj) {
            obj.parent().parent().remove();
        }


        function readFiles(input, id) {
            counter = input.files.length;

            for (x = 0; x < counter; x++) {
                if (input.files && input.files[x]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $("ul.row-package." + id + "").append('<li><div class=\"close1\"><i class=\"close_message\" onclick=\"Remove($(this))\"></i></div><a href=\"' + e.target.result + '\" data-lightbox=\"image-1\"><img src="' + e.target.result + '"></a></li>');
                    };
                    reader.readAsDataURL(input.files[x]);
                }
            }
        }
        function updateIsLost(barcode, id, obj, packageID) {
            var c = confirm("Bạn muốn báo thất lạc kiện này?");
            if (c) {
                var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
                var status = obj.parent().parent().find(".package-status-select").val();
                var bigpackage = obj.parent().parent().find(".package-bigpackage-select").val();
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/manager/VNWareHouse.aspx/UpdateLost",
                    data: "{packageID:'" + packageID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        if (data == "1") {
                            obj.parent().parent().addClass("isthatlac");
                            $("#" + id).find('.thatlacbutton').remove();
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:white");
                        }
                        remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert(errorthrow);
                    }
                });
            }


        }
        function updateWeightNew(barcode, id, obj, packageID) {
            debugger;
            var dai = obj.parent().parent().find(".lengthsize").val();
            var rong = obj.parent().parent().find(".widthsize").val();
            var cao = obj.parent().parent().find(".heightsize").val();

            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            var bigpackage = obj.parent().parent().find(".package-bigpackage-select").val();
            var note = obj.parent().parent().find(".packagedescription").val();
            debugger;
            var base64 = "";
            $("ul.row-package." + id + " li img").each(function () {
                base64 += $(this).attr('src') + "|";
            })
            add_loading();
            $.ajax({
                type: "POST",
                url: "/manager/VNWareHouse.aspx/UpdateQuantityNew",
                //data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'0',packageID:'" + packageID + "',dai:'" + dai + "',rong:'" + rong + "',cao:'" + cao + "'}",
                data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'0',packageID:'" + packageID + "',dai:'" + dai + "',rong:'" + rong + "',cao:'" + cao + "',base64:'" + base64 + "',note:'" + note + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data == "1") {
                        if (status == 0) {
                            obj.parent().parent().addClass("ishuy");
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:white");
                            obj.parent().find(".updatebutton").hide();
                            obj.parent().find(".printbarcode").hide();
                        }
                        else {
                            obj.parent().parent().addClass("isvekho");
                            $("#" + id).find('.thatlacbutton').remove();
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:white");
                        }
                    }
                    remove_loading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });

        }
        function huyxuatkhoNew(barcode, obj) {
            var r = confirm("Bạn muốn tắt package này?");
            if (r == true) {
                var id = barcode + "|";
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                listbarcode = listbarcode.replace(id, "");
                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                obj.parent().parent().parent().remove();
                if ($(".package-item").length == 0) {
                    $("#outall-package").hide();
                    $("#capnhatall").hide();
                }
                countOrder();
            } else {

            }
        }
        //End phần mới
    </script>
    <style>
        .package-item {
            width: 470px !important;
        }

        .row-package label input {
            display: none;
        }

        .row-package label {
            border: 1px solid #e1e1e1;
            background-color: #fff;
            padding: 5px 20px;
            cursor: pointer;
        }

            .row-package label span {
                color: #2154b0;
                font-size: 14px;
                font-weight: 200;
            }

        .packageShopCode img {
            width: 22.5%;
        }

        .dlt {
            color: red;
            font-weight: bold;
            cursor: pointer;
        }

        .row-package li {
            width: 30%;
            margin: 0 5px;
            min-width: 100px;
        }

        ul.row-package {
            list-style: none;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            -webkit-box-align: center;
            -ms-flex-align: center;
            align-items: center;
        }

        .close1 {
            position: absolute;
            color: red;
            cursor: pointer;
            margin-left: 30%;
            margin-top: -4%;
        }
        /*.package-info a {
            position: absolute;
            margin-left: 75px;
        }*/
    </style>
</asp:Content>
