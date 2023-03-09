<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="OutStock.aspx.cs" Inherits="NHST.manager.OutStock" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .package-detail {
            float: left;
            border: dashed 1px #000;
            padding: 10px;
            margin-bottom: 30px;
            min-height: 240px;
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
            width: 45% !important;
        }

        .hidden-btn {
            display: none;
        }

        #outall-package {
            margin: 20px 0;
            float: left;
            width: auto;
        }

        .hl-txt, .btn, a:hover {
            color: #fff;
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
                                    <h3 class="lb">Xuất kho</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            <label>Username</label>
                                            <input id="username" class="form-control" type="text" placeholder="Nhập username" value="" />
                                        </div>
                                        <div class="form-row marbot1">
                                            <div class="col-3">
                                                <label>Mã đơn hàng</label>
                                                <input id="txtOrderID" class="form-control" type="text" placeholder="Nhập mã đơn hàng" value="" />
                                            </div>
                                            <div class="col-3">
                                                <label>Loại đơn hàng</label>
                                                <select class="form-control ordertypeget">
                                                    <option value="0">Tất cả</option>
                                                    <option value="1">Đơn hàng mua hộ</option>
                                                    <option value="2">Đơn hàng VC hộ</option>
                                                </select>
                                            </div>
                                            <div class="col-3">
                                                <a href="javascript:;" class="btn btn-success btn-block small-btn" onclick="getpackagebyoID()">Lấy kiện</a>
                                            </div>
                                        </div>
                                        <div class="form-row marbot1">
                                            <label>Barcode</label>
                                            <%--<input id="barcode-input" class="form-control barcode-area width-50-per" type="text" oninput="getCodeNew($(this))" />--%>
                                            <input id="barcode-input" class="form-control barcode-area width-50-per" type="text" />
                                        </div>
                                        <div class="form-row marbot1" id="xuatkhotatca" style="display: none">
                                            <%--<a href="javascript:;" class="btn btn-success btn-block small-btn" style="width: auto;" onclick="outstockall();">Xuất kho tất cả kiện</a>--%>
                                            <a href="javascript:;" class="btn btn-success btn-block small-btn" style="width: auto;" onclick="xuatkhotatcakien();">Xuất kho tất cả kiện</a>
                                        </div>
                                        <div class="form-row marbot1" style="display: none;">
                                            <label style="font-size: 18px;">Số lượng đơn: <span id="countorder">0</span></label>
                                        </div>
                                        <div class="form-row marbot1 error-outstock" style="margin-bottom: 30px;">
                                        </div>
                                        <div class="form-row marbot1 listpack">
                                        </div>
                                        <asp:HiddenField ID="hdfTotalPrice" runat="server" />
                                        <div class="userlist-outstock" style="display: none">
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                    <%--<div>
                        <div class="print-bill">
                            <div class="top">
                                <div class="left">
                                    <span class="company-info">MONA MEDIA</span>
                                    <span class="company-info">Địa chỉ: 319-C16 Lý Thường Kiệt, P.15, Q.11, Tp.HCM</span>
                                </div>
                                <div class="right">
                                    <span class="bill-num">Mẫu số 01 - TT</span>
                                    <span class="bill-promulgate-date">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>
                                </div>
                            </div>
                            <div class="bill-title">
                                <h1>PHIẾU XUẤT KHO</h1>
                                <span class="bill-date">20/05/2018</span>
                            </div>
                            <div class="bill-content">
                                <div class="bill-row">
                                    <label class="row-name">Họ và tên người nhận: </label>
                                    <label class="row-info"></label>
                                </div>
                                <div class="bill-row">
                                    <label class="row-name">Số điện thoại: </label>
                                    <label class="row-info"></label>
                                </div>
                                <div class="bill-row" style="border: none;">
                                    <label class="row-name">Danh sách kiện: </label>
                                    <label class="row-info"></label>
                                </div>
                                <div class="bill-row" style="border: none;">
                                    <table id="customers" class="rgMasterTable normal-table" style="width: 100%; text-align: center;">
                                        <tr>
                                            <td>Mã vận đơn
                                            </td>
                                            <td>Cân nặng
                                            </td>
                                            <td>Ký nhận
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>12342342314
                                            </td>
                                            <td>3 kg
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="bill-footer">
                                <div class="bill-row-two">
                                    <strong>Người xuất hàng</strong>
                                    <span class="note">(Ký, họ tên)</span>
                                </div>
                                <div class="bill-row-two">
                                    <strong>Người nộp tiền</strong>
                                    <span class="note">(Ký, họ tên)</span>
                                </div>
                            </div>
                        </div>
                    </div>--%>
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
    <asp:HiddenField ID="hdfListBarcode" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#barcode-input').focus();
            $('#barcode-input').keydown(function (e) {
                if (e.key === 'Enter') {
                    getCodeNew($(this));
                    e.preventDefault();
                    return false;
                }
            });
        });
        function handleKeyPress(evt) {
            var nbr = (window.event) ? event.keyCode : evt.which;
            if (nbr == 13) {
                return false;
            }
        }
        document.onkeydown = handleKeyPress
        function getCode(obj) {

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
                    url: "/manager/OutStock.aspx/GetCode",
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
                                $(".userlist-outstock").prepend(uhtml);
                            }

                            var html = '';
                            html += "<div class=\"col-md-4 package-item\" >";
                            html += "   <div id=\"bc-" + data.BarCode + "\" class=\"package-detail\" data-weight=\"" + data.TotalWeight + "\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                            //html += "       <div class=\"row-package\">";
                            //html += "           <span class=\"package-label\"><strong>Username:</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Username + "</strong></span>";
                            //html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
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
                            //html += "       <div class=\"row-package\">";
                            //html += "           <span class=\"package-label\">Mã Package:</span>";
                            //html += "           <span class=\"package-info packageCode\">" + data.TotalWeight + "</span>";
                            //html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Trọng lượng:</span>";
                            html += "           <span class=\"package-info packageWeight\">" + data.TotalWeight + " kg</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Kích thước:</span>";
                            html += "           <span class=\"package-info packageSize\">d: " + data.dai + " x ";
                            html += "           r: " + data.rong + " x ";
                            html += "           c: " + data.cao + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Tổng ngày lưu kho:</span>";
                            html += "           <span class=\"package-info packageWeight\">10 ngày</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Tổng tiền lưu kho:</span>";
                            html += "           <span class=\"package-info packageWeight\">100.000 vnđ</span>";
                            html += "       </div>";
                            //html += "       <div class=\"row-package\">";
                            //html += "           <span class=\"package-label\">Tổng tiền:</span>";
                            //html += "           <span class=\"package-info packageWeight\">" + data.TotalPriceVND + " vnđ</span>";
                            //html += "       </div>";
                            html += "       <div class=\"row-package status-pack\">";
                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                            if (data.Status < 3)
                                html += "       <span class=\"package-info packageStatus bg-red\">Chưa về VN</span>";
                            else if (data.Status == 3)
                                html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về VN</span>";
                            else if (data.Status == 4)
                                html += "       <span class=\"package-info packageStatus bg-blue\">Đã giao khách</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package \">";
                            if (data.Status == 3) {
                                if (data.MainOrderStatus == 9) {
                                    html += "       <a href=\"javascript:;\" onclick=\"xuatkho('" + data.BarCode + "','bc-" + data.BarCode + "')\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Xuất kho</a>";
                                    html += "       <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\" style=\"margin-top:0;\">Hủy</a>";
                                }
                                else
                                    html += "       <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Hủy</a>";
                            }
                            else
                                html += "       <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Hủy</a>";
                            html += "       </div>";
                            if (data.MainOrderStatus < 9) {
                                html += "       <div class=\"row-package \" style='color:red;font-weight:bold;'>Đơn hàng chưa thanh toán. Xin vui lòng thanh toán đơn " + data.MainorderID + " trước khi xuất hàng.";
                                html += "       </div>";
                            }
                            html += "       <div class=\"row-package-status \" style='color:red'>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";

                            //if (data.Status >= 5 && data.Status < 7)
                            listbarcode += data.BarCode + ",bc-" + data.BarCode + "|";

                            $(".listpack").prepend(html);
                            $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                            obj.val("");
                            $("#outall-package").show();
                            countOrder();
                            $("#xuatkhotatca").show();
                            //if (data.Status >= 3) {

                            //}
                            //else {
                            //    obj.val("");
                            //}
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
        function add_loading() {
            $(".page-inner").prepend("<div class='loading_bg'></div>");
            var height = $(".page-inner").height();
            $(".loading_bg").css("height", height + "px");
        }
        function xuatkho(Barcode, id) {
            add_loading();
            //alert(Barcode);
            $.ajax({
                type: "POST",
                url: "/manager/OutStock.aspx/SetFinish",
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
            var r = confirm("Bạn muốn hủy xuất kho đơn hàng này?");
            if (r == true) {
                var id = barcode + "|";
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                listbarcode = listbarcode.replace(id, "");
                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                obj.parent().parent().parent().remove();
                if ($(".package-item").length == 0) {
                    $("#outall-package").hide();
                    $("#xuatkhotatca").show();
                }
                countOrder();
            } else {

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
                            url: "/manager/OutStock.aspx/GetWallet",
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
                                    $(".error-outstock").prepend(error);
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
        function VoucherSourcetoPrint(source) {
            var r = "<html><head><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style-p.css\" type=\"text/css\"/><script>function step1(){\n" +
                    "setTimeout('step2()', 10);}\n" +
                    "function step2(){window.print();window.close()}\n" +
                    "</scri" + "pt></head><body onload='step1()'>\n" +
                    "" + source + "</body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }
        function outstockall() {
            var c = confirm("Bạn muốn xuất kho tất cả kiện?");
            if (c) {
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();

                if (dd < 10) {
                    dd = '0' + dd
                }

                if (mm < 10) {
                    mm = '0' + mm
                }

                today = dd + '/' + mm + '/' + yyyy;
                var html = "";
                html += "<div class=\"print-bill\">";
                html += "   <div class=\"top\">";
                html += "       <div class=\"left\">";
                html += "           <span class=\"company-info\">MONA MEDIA</span>";
                html += "           <span class=\"company-info\">Địa chỉ: 319-C16 Lý Thường Kiệt, P.15, Q.11, Tp.HCM</span>";
                html += "       </div>";
                html += "       <div class=\"right\">";
                html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-title\">";
                html += "       <h1>PHIẾU XUẤT KHO</h1>";
                html += "       <span class=\"bill-date\">" + today + " </span>";
                html += "   </div>";
                html += "   <div class=\"bill-content\">";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Họ và tên người nhận: </label>";
                html += "           <label class=\"row-info\"></label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Số ĐT: </label>";
                html += "           <label class=\"row-info\"></label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += "           <label class=\"row-name\">Danh sách kiện: </label>";
                html += "           <label class=\"row-info\"></label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += "			<table id=\"customers\" class=\"rgMasterTable normal-table\" style=\"width:100%;text-align:center;\">";
                html += "					<tr>";
                html += "						<td>Mã vận đơn</td>";
                html += "						<td>Cân nặng</td>";
                html += "						<td>Tiền lưu kho</td>";
                html += "						<td>Ký nhận</td>";
                html += "					</tr>";
                $(".package-detail").each(function () {
                    var barcode = $(this).attr("data-barcode");
                    var weight = $(this).attr("data-weight");
                    html += "					<tr>";
                    html += "						<td>" + barcode + "</td>";
                    html += "						<td>" + weight + " kg</td>";
                    html += "						<td>100.000 vnđ</td>";
                    html += "						<td></td>";
                    html += "					</tr>";
                });

                html += "			</table>";
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-footer\">";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người xuất hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "       </div>";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người nhận hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "       </div>";
                html += "   </div>";
                html += "</div>";
                VoucherPrint(html);
            }
        }
    </script>
    <script type="text/javascript">
        function addLoading() {
            $("#main-col-wrap").prepend("<div class=\"addloading\"></div>");
        }
        function removeLoading() {
            $(".addloading").remove();
        }
        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
        function getbycode(barco) {
            var bc = barco;
            var username = $("#username").val();
            if (isEmpty(bc)) {
                alert('Vui lòng không để trống barcode');
            }
            else if (isEmpty(username)) {
                alert('Vui lòng không để trống username');
            }
            else {
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                $.ajax({
                    type: "POST",
                    url: "/manager/outstock.aspx/getpackages",
                    data: "{barcode:'" + bc + "',username:'" + username + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var p = JSON.parse(msg.d);
                        if (p != "none") {
                            if (p == "notexistuser") {
                                alert('Không tồn tại user trong hệ thống');
                            }
                            else {
                                var pID = p.pID;
                                var UID = p.uID;
                                var uname = p.username;
                                var mID = p.mID;
                                var tID = p.tID;
                                var weight = p.weight;
                                var status = p.status;
                                var getbarcode = p.barcode;
                                var dIWH = p.dateInWarehouse;
                                var kiemdem = p.kiemdem;
                                var donggo = p.donggo;
                                var ordertype = parseFloat(p.OrderType);
                                var ordertypeString = p.OrderTypeString;
                                var totalDaysInWare = p.TotalDayInWarehouse

                                var isExist = false;
                                if ($(".package-detail").length > 0) {
                                    $(".package-detail").each(function () {
                                        var dt_packageID = $(this).attr("data-packageID");
                                        if (pID == dt_packageID) {
                                            isExist = true;
                                        }
                                    });
                                }
                                if (isExist == false) {
                                    var idpack = "bc-" + getbarcode + "-" + pID;
                                    var html = '';
                                    html += "<div class=\"col-md-3 package-item\" >";
                                    if (ordertype == 3) {
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhcxd\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    }
                                    else if (ordertype == 1) {
                                        if (status > 2)
                                            html += "   <div id=\"" + idpack + "\" class=\"package-detail dhmh isvekho\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                        else
                                            html += "   <div id=\"" + idpack + "\" class=\"package-detail dhmh\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                        html += "       <img src=\"/App_Themes/AdminNew/images/dhmh-flag.png\" alt=\"\" class=\"dh-flag\">";
                                    }
                                    else if (ordertype == 2) {
                                        if (status > 2)
                                            html += "   <div id=\"" + idpack + "\" class=\"package-detail dhvch isvekho\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                        else
                                            html += "   <div id=\"" + idpack + "\" class=\"package-detail dhvch \" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                        html += "       <img src=\"/App_Themes/AdminNew/images/dhvch-flag.png\" alt=\"\" class=\"dh-flag\">";
                                    }
                                    if (ordertype == 1) {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + ordertypeString + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + mID + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Kiểm đếm:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + kiemdem + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + donggo + "</strong></span>";
                                        html += "       </div>";
                                    }
                                    else if (ordertype == 2) {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + ordertypeString + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + tID + "</strong></span>";
                                        html += "       </div>";
                                    }
                                    else {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageOrderTypeselect\">";
                                        html += "               <select class=\"form-control packageOrderType\">";
                                        html += "                   <option value=\"1\">Đơn hàng mua hộ</option>";
                                        html += "                   <option value=\"2\">Đơn hàng VC hộ</option>";
                                        html += "               </select>";
                                        html += "           </span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageOrderCode\"><input class=\"packageorderID\"/></span>";
                                        html += "       </div>";
                                    }
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                    html += "           <span class=\"package-info packageCode\">" + getbarcode + "</span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Trọng lượng:</span>";
                                    html += "           <span class=\"package-info packageWeight\">" + weight + " kg</span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Tổng ngày lưu kho:</span>";
                                    html += "           <span class=\"package-info packageWeight\">" + totalDaysInWare + "</span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package status-pack\">";
                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                    if (status == 1) {
                                        html += "       <span class=\"package-info packageStatus bg-red\">Chưa về kho TQ</span>";
                                    }
                                    else if (status == 2) {
                                        html += "       <span class=\"package-info packageStatus bg-orange\">Đã về TQ</span>";
                                    }
                                    else if (status == 3) {
                                        html += "       <span class=\"package-info packageStatus bg-green\">Đã về VN</span>";
                                    }
                                    else if (status == 4) {
                                        html += "       <span class=\"package-info packageStatus bg-blue\">Đã giao khách</span>";
                                    }
                                    html += "       </div>";
                                    var strItem = getbarcode + ",bc-" + getbarcode + "," + pID + "|";
                                    html += "       <div class=\"row-package \">";
                                    if (status == 3) {
                                        if (ordertype == 3) {
                                            html += "       <a href=\"javascript:;\" onclick=\"updateOrderType('" + getbarcode + "','" + strItem + "',$(this),'" + pID + "')\" class=\"capnhatkien btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                            html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block custom-small-button\" style=\"margin-top:0;\">Ẩn</a>";
                                        }
                                        else {
                                            html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Ẩn</a>";
                                        }
                                    }
                                    else {
                                        html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Ẩn</a>";
                                    }
                                    html += "       <div class=\"row-package-status \" style='color:red'>";
                                    html += "       </div>";
                                    html += "   </div>";
                                    html += "</div>";
                                    listbarcode += strItem;
                                    $(".listpack").prepend(html);
                                    $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);

                                    $("#outall-package").show();
                                    countOrder();
                                    $("#xuatkhotatca").show();
                                }
                                else {
                                    if ($(".package-detail").length > 0) {
                                        $(".package-detail").each(function () {
                                            var dt_packageID = $(this).attr("data-packageID");
                                            if (pID == dt_packageID) {
                                                var status = $(this).attr("data-status");
                                                if (status > 2)
                                                    $(this).addClass("isvekho");
                                            }
                                        });
                                    }
                                }
                            }
                            obj.val("");
                        }
                        else {
                            alert('Không tìm thấy kiện');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }
    }
    function getCodeNew(obj) {
        var bc = obj.val();
        var username = $("#username").val();
        if (isEmpty(bc)) {
            alert('Vui lòng không để trống barcode');
        }
        else if (isEmpty(username)) {
            alert('Vui lòng không để trống username');
        }
        else {
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            $.ajax({
                type: "POST",
                url: "/manager/outstock.aspx/getpackages",
                data: "{barcode:'" + bc + "',username:'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    //alert(bc);
                    if (ret != "none") {
                        if (ret == "notexistuser") {
                            alert('Không tồn tại user trong hệ thống');
                        }
                        else {
                            var p = JSON.parse(msg.d);
                            var pID = p.pID;
                            var UID = p.uID;
                            var uname = p.username;
                            var mID = p.mID;
                            var tID = p.tID;
                            var weight = p.weight;
                            var status = p.status;
                            var getbarcode = p.barcode;
                            var dIWH = p.dateInWarehouse;
                            var kiemdem = p.kiemdem;
                            var donggo = p.donggo;
                            var ordertype = parseFloat(p.OrderType);
                            var ordertypeString = p.OrderTypeString;
                            var totalDaysInWare = p.TotalDayInWarehouse

                            var isExist = false;
                            if ($(".package-detail").length > 0) {
                                $(".package-detail").each(function () {
                                    var dt_packageID = $(this).attr("data-packageID");
                                    if (pID == dt_packageID) {
                                        isExist = true;
                                    }
                                });
                            }
                            if (isExist == false) {
                                var idpack = "bc-" + getbarcode + "-" + pID;
                                var html = '';
                                html += "<div class=\"col-md-3 package-item\" >";
                                if (ordertype == 3) {
                                    //if (status > 2)
                                    //    html += "   <div id=\"" + idpack + "\" class=\"package-detail dhcxd isvekho\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    //else
                                    html += "   <div id=\"" + idpack + "\" class=\"package-detail dhcxd\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                }
                                else if (ordertype == 1) {
                                    if (status > 2)
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhmh isvekho\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    else
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhmh\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    html += "       <img src=\"/App_Themes/AdminNew/images/dhmh-flag.png\" alt=\"\" class=\"dh-flag\">";
                                }
                                else if (ordertype == 2) {
                                    if (status > 2)
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhvch isvekho\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    else
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhvch \" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    html += "       <img src=\"/App_Themes/AdminNew/images/dhvch-flag.png\" alt=\"\" class=\"dh-flag\">";
                                }
                                if (ordertype == 1) {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + ordertypeString + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + mID + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Kiểm đếm:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + kiemdem + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + donggo + "</strong></span>";
                                    html += "       </div>";
                                }
                                else if (ordertype == 2) {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + ordertypeString + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + tID + "</strong></span>";
                                    html += "       </div>";
                                }
                                else {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageOrderTypeselect\">";
                                    html += "               <select class=\"form-control packageOrderType\">";
                                    html += "                   <option value=\"1\">Đơn hàng mua hộ</option>";
                                    html += "                   <option value=\"2\">Đơn hàng VC hộ</option>";
                                    html += "               </select>";
                                    html += "           </span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageOrderCode\"><input class=\"packageorderID\"/></span>";
                                    html += "       </div>";
                                }
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                html += "           <span class=\"package-info packageCode\">" + getbarcode + "</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Trọng lượng:</span>";
                                html += "           <span class=\"package-info packageWeight\">" + weight + " kg</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Kích thước:</span>";
                                html += "           <span class=\"package-info packageSize\">d: " + p.dai + " x ";
                                html += "           r: " + p.rong + " x ";
                                html += "           c: " + p.cao + "</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Tổng ngày lưu kho:</span>";
                                html += "           <span class=\"package-info packageWeight\">" + totalDaysInWare + "</span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                if (status == 1) {
                                    html += "       <span class=\"package-info packageStatus bg-red\">Chưa về kho TQ</span>";
                                }
                                else if (status == 2) {
                                    html += "       <span class=\"package-info packageStatus bg-orange\">Đã về TQ</span>";
                                }
                                else if (status == 3) {
                                    html += "       <span class=\"package-info packageStatus bg-green\">Đã về VN</span>";
                                }
                                else if (status == 4) {
                                    html += "       <span class=\"package-info packageStatus bg-blue\">Đã giao khách</span>";
                                }
                                html += "       </div>";
                                var strItem = getbarcode + ",bc-" + getbarcode + "," + pID + "|";
                                html += "       <div class=\"row-package \">";
                                if (status == 3) {
                                    if (ordertype == 3) {
                                        html += "       <a href=\"javascript:;\" onclick=\"updateOrderType('" + getbarcode + "','" + strItem + "',$(this),'" + pID + "')\" class=\"capnhatkien btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                        html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block custom-small-button\" style=\"margin-top:0;\">Ẩn</a>";
                                    }
                                    else {
                                        html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Ẩn</a>";
                                    }
                                }
                                else {
                                    html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Ẩn</a>";
                                }
                                html += "       <div class=\"row-package-status \" style='color:red'>";
                                html += "       </div>";
                                html += "   </div>";
                                html += "</div>";
                                listbarcode += strItem;
                                $(".listpack").prepend(html);
                                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);

                                $("#outall-package").show();
                                countOrder();
                                $("#xuatkhotatca").show();
                            }
                            else {
                                if ($(".package-detail").length > 0) {
                                    $(".package-detail").each(function () {
                                        var dt_packageID = $(this).attr("data-packageID");
                                        if (pID == dt_packageID) {
                                            var status = $(this).attr("data-status");
                                            if (status > 2)
                                                $(this).addClass("isvekho");
                                        }
                                    });
                                }
                            }
                        }
                        obj.val("");
                    }
                    else {
                        alert('Không tìm thấy kiện này');
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
        }
}
function getpackagebyoID() {
    var orderid = 0;
    if (!isEmpty($("#txtOrderID").val()))
        orderid = $("#txtOrderID").val();
    //var orderid = $("#txtOrderID").val();
    var ordertype = $(".ordertypeget option:selected").val();
    var username = $("#username").val();
    if (isEmpty(username)) {
        alert('Vui lòng nhập username.');
    }
    else {
        var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            $.ajax({
                type: "POST",
                url: "/manager/outstock.aspx/getpackagesbyo",
                data: "{orderID:'" + orderid + "',username:'" + username + "',type:'" + ordertype + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret != "none") {
                        var listp = JSON.parse(msg.d);
                        if (listp.length > 0) {
                            for (var i = 0; i < listp.length; i++) {
                                var p = listp[i];
                                var pID = p.pID;
                                var UID = p.uID;
                                var uname = p.username;
                                var mID = p.mID;
                                var tID = p.tID;
                                var weight = p.weight;
                                var status = p.status;
                                var getbarcode = p.barcode;
                                var dIWH = p.dateInWarehouse;
                                var kiemdem = p.kiemdem;
                                var donggo = p.donggo;
                                var ordertype = parseFloat(p.OrderType);
                                var ordertypeString = p.OrderTypeString;
                                var totalDaysInWare = p.TotalDayInWarehouse

                                var isExist = false;
                                if ($(".package-detail").length > 0) {
                                    $(".package-detail").each(function () {
                                        var dt_packageID = $(this).attr("data-packageID");
                                        if (pID == dt_packageID) {
                                            isExist = true;
                                        }
                                    });
                                }
                                if (isExist == false) {
                                    var idpack = "bc-" + getbarcode + "-" + pID;
                                    var html = '';
                                    html += "<div class=\"col-md-3 package-item\" >";
                                    if (ordertype == 3) {
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhcxd\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    }
                                    else if (ordertype == 1) {
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhmh\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                        html += "       <img src=\"/App_Themes/AdminNew/images/dhmh-flag.png\" alt=\"\" class=\"dh-flag\">";
                                    }
                                    else if (ordertype == 2) {
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail dhvch\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                        html += "       <img src=\"/App_Themes/AdminNew/images/dhvch-flag.png\" alt=\"\" class=\"dh-flag\">";
                                    }
                                    else {
                                        html += "   <div id=\"" + idpack + "\" class=\"package-detail\" data-ordertype=\"" + ordertype + "\" data-packageID=\"" + pID + "\" data-weight=\"" + weight + "\" data-barcode=\"" + getbarcode + "\" data-uid=\"" + UID + "\" data-status=\"" + status + "\">";
                                    }
                                    if (ordertype == 1) {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + ordertypeString + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + mID + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Kiểm đếm:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + kiemdem + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + donggo + "</strong></span>";
                                        html += "       </div>";
                                    }
                                    else if (ordertype == 2) {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + ordertypeString + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + tID + "</strong></span>";
                                        html += "       </div>";
                                    }
                                    else {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageOrderTypeselect\">";
                                        html += "               <select class=\"form-control packageOrderType\">";
                                        html += "                   <option value=\"1\">Đơn hàng mua hộ</option>";
                                        html += "                   <option value=\"2\">Đơn hàng VC hộ</option>";
                                        html += "               </select>";
                                        html += "           </span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageOrderCode\"><input class=\"packageorderID\"/></span>";
                                        html += "       </div>";
                                    }
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                    html += "           <span class=\"package-info packageCode\">" + getbarcode + "</span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Trọng lượng:</span>";
                                    html += "           <span class=\"package-info packageWeight\">" + weight + " kg</span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Kích thước:</span>";
                                    html += "           <span class=\"package-info packageSize\">d: " + p.dai + " x ";
                                    html += "           r: " + p.rong + " x ";
                                    html += "           c: " + p.cao + "</span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Tổng ngày lưu kho:</span>";
                                    html += "           <span class=\"package-info packageWeight\">" + totalDaysInWare + "</span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package status-pack\">";
                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                    if (status == 1) {
                                        html += "       <span class=\"package-info packageStatus bg-red\">Chưa về kho TQ</span>";
                                    }
                                    else if (status == 2) {
                                        html += "       <span class=\"package-info packageStatus bg-orange\">Đã về TQ</span>";
                                    }
                                    else if (status == 3) {
                                        html += "       <span class=\"package-info packageStatus bg-green\">Đã về VN</span>";
                                    }
                                    else if (status == 4) {
                                        html += "       <span class=\"package-info packageStatus bg-blue\">Đã giao khách</span>";
                                    }
                                    html += "       </div>";
                                    var strItem = getbarcode + ",bc-" + getbarcode + "," + pID + "|";
                                    html += "       <div class=\"row-package \">";
                                    if (status == 3) {
                                        if (ordertype == 3) {
                                            html += "       <a href=\"javascript:;\" onclick=\"updateOrderType('" + getbarcode + "','" + strItem + "',$(this),'" + pID + "')\" class=\"capnhatkien btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                            html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block custom-small-button\" style=\"margin-top:0;\">Ẩn</a>";
                                        }
                                        else {
                                            html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Ẩn</a>";
                                        }
                                    }
                                    else {
                                        html += "       <a href=\"javascript:;\" onclick=\"huyxuatkhoNew('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\" style=\"margin-top:0;\">Ẩn</a>";
                                    }
                                    html += "       <div class=\"row-package-status \" style='color:red'>";
                                    html += "       </div>";
                                    html += "   </div>";
                                    html += "</div>";
                                    listbarcode += strItem;
                                    $(".listpack").prepend(html);
                                    $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);

                                    $("#outall-package").show();
                                    countOrder();
                                    $("#xuatkhotatca").show();
                                }
                            }
                        }
                        obj.val("");
                    }
                    else {
                        alert('Không tìm thấy kiện');
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
        }
    }
    function huyxuatkhoNew(barcode, obj) {
        var r = confirm("Bạn muốn tắt kiện này?");
        if (r == true) {
            var id = barcode + "|";
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            listbarcode = listbarcode.replace(id, "");
            $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
            obj.parent().parent().parent().remove();
            if ($(".package-item").length == 0) {
                $("#outall-package").hide();
                $("#xuatkhotatca").hide();
            }
            countOrder();
        } else {

        }
    }
    function updateOrderType(bc, id, obj, packageID) {
        var root = obj.parent().parent();
        var mordertype = root.find(".packageOrderType option:selected").val();
        var morderID = root.find(".packageorderID").val();
        var musername = $("#username").val();
        $.ajax({
            type: "POST",
            url: "/manager/OutStock.aspx/addpackagetoprder",
            data: "{ordertype:'" + mordertype + "',username:'" + musername + "',orderid:'" + morderID + "',pID:'" + packageID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ret = JSON.parse(msg.d);
                if (ret != "none") {
                    var p = ret;
                    var pID = p.pID;
                    var code = p.barcode;
                    if ($(".package-detail").length > 0) {
                        $(".package-detail").each(function () {
                            var dt_packageID = $(this).attr("data-packageID");
                            if (pID == dt_packageID) {
                                $(this).parent().remove();
                            }
                        });
                    }

                    getbycode(code);
                }
                else {
                    alert('Có lỗi trong quá trình cập nhật, vui lòng thử lại sau');
                }
                obj.val("");
            }, error: function (xmlhttprequest, textstatus, errorthrow) {
                //alert('lỗi checkend');
            }
        });
    }
    function xuatkhotatcakien() {
        var checkout = true;
        var username = $("#username").val();
        $(".package-detail").each(function () {
            if (!$(this).hasClass("isvekho")) {
                checkout = false;
            }
        });
        if (checkout == false) {
            alert('Số kiện hiện tại chưa đủ để xuất.');
        }
        else {
            var listpackid = "";
            $(".package-detail").each(function () {
                listpackid += $(this).attr("data-packageid") + "|";
            });
            $("#<%=hdfListPID.ClientID%>").val(listpackid);
            $("#<%=hdfUsername.ClientID%>").val(username);
            $("#<%=btnAllOutstock.ClientID%>").click();
        }
    }
    </script>
    <asp:Button ID="btnAllOutstock" runat="server" OnClick="btnAllOutstock_Click" Style="display: none" OnClientClick="document.forms[0].target = '_blank';" />
    <asp:HiddenField ID="hdfListPID" runat="server" />
    <asp:HiddenField ID="hdfUsername" runat="server" />
    <style>
        .huyxuatkhobutton {
            margin-top: 0px;
        }

        .isvekho {
            background: blue;
            border: dashed #fff 1px;
        }

            .isvekho span {
                color: #fff;
            }
        /*.dhmh {
            background: blue;
            border: dashed #fff 1px;
        }

            .dhmh span {
                color: #fff;
            }

        .dhvch {
            background: yellow;
            border: dashed #fff 1px;
        }

            .dhvch span {
                color: #000;
            }*/

        .col-3 {
            float: left;
            width: 32%;
            margin-right: 5px;
        }

            .col-3 .btn {
                margin-top: 18px;
                width: auto !important;
            }

        .package-detail {
            position: relative;
            min-height: 310px;
        }

            .package-detail .dh-flag {
                position: absolute;
                top: 0;
                right: 5px;
                width: 15px;
            }
    </style>
</asp:Content>
