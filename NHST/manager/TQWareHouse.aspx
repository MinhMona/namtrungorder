<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="TQWareHouse.aspx.cs" Inherits="NHST.manager.TQWareHouse" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .package-detail {
            float: left;
            border: dashed 1px #000;
            padding: 10px;
            margin-bottom: 30px;
            min-height: 385px;
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
            font-size: 12px;
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

        .custom-small-button {
            margin-top: 5px;
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
                                    <h3 class="lb">Kiểm kho TQ</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row" style="line-height: 40px; margin-bottom: 0">
                                            <span style="float: left; margin-right: 10px;">Chọn bao lớn</span>
                                            <asp:DropDownList ID="ddlBigpackage" runat="server" CssClass="form-control"
                                                DataTextField="PackageCode" DataValueField="ID" Style="float: left; width: 20%;">
                                            </asp:DropDownList>
                                            <a href="/manager/Add-Package.aspx" target="_blank" class="btn primary-btn" style="float: left; margin-left: 10px;">Tạo mới bao lớn
                                            </a>
                                        </div>
                                        <div class="form-row marbot1">
                                            Barcode
                                        </div>
                                        <div class="form-row marbot2">
                                            <input id="barcode-input" class="form-control barcode-area width-50-per" type="text" />
                                            <%--<input id="barcode-input" class="form-control barcode-area width-50-per" type="text"
                                                oninput="getCode($(this))" />--%>
                                        </div>
                                        <div class="form-row no-margin">
                                            <div class="instu">
                                                <div class="instu-left">
                                                    <span class="instu-left-package">
                                                        <i class="package-color white"></i>
                                                        Hàng chưa về kho TQ
                                                    </span>
                                                    <span class="instu-left-package">
                                                        <i class="package-color blue"></i>
                                                        Hàng đã về kho TQ</span>
                                                    <span class="instu-left-package">
                                                        <i class="package-color black"></i>Hàng đã hủy</span>
                                                </div>
                                                <div class="instu-right">
                                                    <a href="http://websitenhaphang.com/nghiep-vu/kho-tq" target="_blank">Hướng dẫn khi không tìm thấy mã đơn hàng
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-row no-margin">
                                            <a href="javascript:;" onclick="addCodeTemp()" class="btn primary-btn">Thêm mã kiện mới</a>
                                        </div>
                                        <div class="form-row no-margin center-txt" style="display: none;">
                                            <a id="capnhatall" style="display: none;" href="javascript:;" onclick="updateAll()" id="outall-package"
                                                class="btn primary-btn hidden-btn">Cập nhật tất cả</a>
                                        </div>
                                        <div id="tbl-list-ordercode" class="table-responsive form-row marbot2" style="display: none;">
                                            <table class="rgMasterTable normal-table" style="width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th>Mã đơn hệ thống</th>
                                                        <th>Mã đơn hàng</th>
                                                        <th>Số kiện</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody class="list-ordercodetransaction">
                                                    <%--<tr data-code="123412341234" data-orderid="1">
                                                        <td>123412341234</td>
                                                        <td>3</td>
                                                        <td>
                                                            <a href="javascript:;" onclick="addPackageTemp($(this))" class="btn primary-btn">Thêm kiện tạm</a>
                                                        </td>
                                                    </tr>
                                                    <tr data-code="123412341234" data-orderid="1">
                                                        <td>123412341234</td>
                                                        <td>3</td>
                                                        <td>
                                                            <a href="javascript:;" onclick="addPackageTemp($(this))" class="btn primary-btn">Thêm kiện tạm</a>
                                                        </td>
                                                    </tr>--%>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="form-row marbot2">
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
        <style>
            .addloading {
                width: 100%;
                height: 100%;
                position: fixed;
                overflow: hidden;
                background: #fff url('/App_Themes/NewUI/images/loading.gif') center center no-repeat;
                z-index: 999999;
                top: 0;
                left: 0;
                bottom: 0;
                right: 0;
                opacity: 0.8;
            }

            #bg_popup_home {
                position: fixed;
                width: 100%;
                height: 100%;
                background-color: #333;
                opacity: 0.7;
                filter: alpha(opacity=70);
                left: 0px;
                top: 0px;
                z-index: 999999999;
                opacity: 0;
                filter: alpha(opacity=0);
            }

            #popup_ms_home {
                background: #fff;
                border-radius: 0px;
                box-shadow: 0px 2px 10px #fff;
                float: left;
                position: fixed;
                width: 735px;
                z-index: 10000;
                left: 50%;
                margin-left: -370px;
                top: 200px;
                opacity: 0;
                filter: alpha(opacity=0);
                height: 360px;
            }

                #popup_ms_home .popup_body {
                    border-radius: 0px;
                    float: left;
                    position: relative;
                    width: 735px;
                }

                #popup_ms_home .content {
                    /*background-color: #487175;     border-radius: 10px;*/
                    margin: 12px;
                    padding: 15px;
                    float: left;
                }

                #popup_ms_home .title_popup {
                    /*background: url("../images/img_giaoduc1.png") no-repeat scroll -200px 0 rgba(0, 0, 0, 0);*/
                    color: #ffffff;
                    font-family: Arial;
                    font-size: 24px;
                    font-weight: bold;
                    height: 35px;
                    margin-left: 0;
                    margin-top: -5px;
                    padding-left: 40px;
                    padding-top: 0;
                    text-align: center;
                }

                #popup_ms_home .text_popup {
                    color: #fff;
                    font-size: 14px;
                    margin-top: 20px;
                    margin-bottom: 20px;
                    line-height: 20px;
                }

                    #popup_ms_home .text_popup a.quen_mk, #popup_ms_home .text_popup a.dangky {
                        color: #FFFFFF;
                        display: block;
                        float: left;
                        font-style: italic;
                        list-style: -moz-hangul outside none;
                        margin-bottom: 5px;
                        margin-left: 110px;
                        -webkit-transition-duration: 0.3s;
                        -moz-transition-duration: 0.3s;
                        transition-duration: 0.3s;
                    }

                        #popup_ms_home .text_popup a.quen_mk:hover, #popup_ms_home .text_popup a.dangky:hover {
                            color: #8cd8fd;
                        }

                #popup_ms_home .close_popup {
                    background: url("/App_Themes/Camthach/images/close_button.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
                    display: block;
                    height: 28px;
                    position: absolute;
                    right: 0px;
                    top: 5px;
                    width: 26px;
                    cursor: pointer;
                    z-index: 10;
                }

            #popup_content_home {
                height: auto;
                position: fixed;
                background-color: #fff;
                top: 15%;
                z-index: 999999999;
                left: 25%;
                border-radius: 10px;
                -moz-border-radius: 10px;
                -webkit-border-radius: 10px;
                width: 50%;
                padding: 20px;
            }

            #popup_content_home {
                padding: 0;
            }

            .popup_header, .popup_footer {
                float: left;
                width: 100%;
                background: #2154b0;
                padding: 10px;
                position: relative;
                color: #fff;
            }

            .popup_header {
                font-weight: bold;
                font-size: 16px;
                text-transform: uppercase;
            }

            .close_message {
                top: 10px;
            }

            .changeavatar {
                padding: 10px;
                margin: 5px 0;
                float: left;
                width: 100%;
            }

            .float-right {
                float: right;
            }

            .content1 {
                float: left;
                width: 100%;
            }

            .content2 {
                float: left;
                width: 100%;
                border-top: 1px solid #eee;
                clear: both;
                margin-top: 10px;
            }

            .btn.btn-close {
                float: right;
                background: #2154b0;
                color: #fff;
                margin: 10px 5px;
                text-transform: none;
            }

                .btn.btn-close:hover {
                    background: #1f85b1;
                }

            .btn.btn-close-full {
                float: right;
                background: #7bb1c7;
                color: #fff;
                margin: 10px 5px;
                text-transform: none;
            }

                .btn.btn-close-full:hover {
                    background: #6692a5;
                }
        </style>
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
        function VoucherSourcetoPrint(source) {
            var r = "<html><head><script>function step1(){\n" +
                    "setTimeout('step2()', 10);}\n" +
                    "function step2(){window.print();window.close()}\n" +
                    "</scri" + "pt></head><body onload='step1()'>\n" +
                    "<img src='" + source + "' /></body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }

        function printBarcode(barcode) {
            //var barcode = "12341234-4123412342134";
            $.ajax({
                type: "POST",
                url: "/manager/TQWareHouse.aspx/PriceBarcode",
                data: "{barcode:'" + barcode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    VoucherPrint(data);
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });
        }

        //function handleKeyPress(evt) {
        //    var nbr = (window.event) ? event.keyCode : evt.which;
        //    if (nbr == 13) {
        //        return false;
        //    }
        //}
        //document.onkeydown = handleKeyPress
        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }
        function close_popup_ms() {
            $("#pupip_home").animate({ "opacity": 0 }, 400);
            $("#bg_popup_home").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip_home").remove();
                $(".zoomContainer").remove();
                $("#bg_popup_home").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
            }, 500);
        }
        function addCodeTemp() {
            var obj = $('form');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup_home'></div>";
            var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                     "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
            fr += "<div class=\"popup_header\">";
            fr += "Thêm mã kiện mới";
            fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "</div>";
            fr += "     <div class=\"changeavatar\">";
            fr += "         <div class=\"content1\" style=\"display:none\">";
            fr += "             <span class=\"package-label\"> Mã đơn hàng: </span>";
            fr += "             <span class=\"package-info\"><input id=\"ordercode-temp\" class=\"form-control\" /></span>";
            fr += "         </div>";
            fr += "         <div class=\"content1\" style=\"margin-top:20px;\">";
            fr += "             <span class=\"package-label\"> Mã kiện: </span>";
            fr += "             <span class=\"package-info\"><input id=\"ordertransactioncode-temp\" class=\"form-control\" /></span>";
            fr += "         </div>";
            fr += "         <div class=\"content1\" style=\"margin-top:20px;\">";
            fr += "             <span class=\"package-label\"> Ghi chú: </span>";
            fr += "             <span class=\"package-info\"><input id=\"note-temp\" class=\"form-control\" /></span>";

            fr += "         </div>";

            fr += "         <div class=\"content1 fixheight\" style=\"margin-top:20px;\">";
            fr += "             <span class=\"package-label\">Hình ảnh: </span>";
            fr += "             <span class=\"package-info\"><label><input id=\"images\" accept=\".png,.jpg,.jpeg\"  type=\"file\"/ multiple onchange=\"readFile(this);\"><span>Thêm hình</span></label></span>";
          
            fr += "             <ul id=\"gallery\"></ul>";
            fr += "         </div>";
            fr += "         <div class=\"content2\">";
            fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";
            fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick='CheckAddTempCode()'>Thêm</a>";
            fr += "         </div>";
            fr += "     </div>";
            fr += "<div class=\"popup_footer\">";
            //fr += "<span class=\"float-right\">" + email + "</span>";
            fr += "</div>";
            fr += "   </div>";
            fr += "</div>";
            $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
            $(fr).appendTo($(obj));
            setTimeout(function () {
                $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                $("#bg_popup").attr("onclick", "close_popup_ms()");
            }, 1000);
        }

        function Remove(obj) {
            obj.parent().parent().remove();
        }




        function readFile(input) {
            counter = input.files.length;
            for (x = 0; x < counter; x++) {
                if (input.files && input.files[x]) {

                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $("#gallery").append('<li><a class=\"right\"><i class=\"dlt\" onclick=\"Remove($(this))\">x</i></a><img src="' + e.target.result + '" class="img-thumbnail"></li>');
                    };

                    reader.readAsDataURL(input.files[x]);
                }
            }
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
        function addPackageTemp(obj) {
            var barcode = obj.parent().parent().attr("data-code");
            var mainorderid = obj.parent().parent().attr("data-mainorderid");
            var c = confirm("Bạn có muốn thêm kiện tạm cho mã đơn hàng: " + barcode + " không?");
            if (c) {
                CheckAddTempCodeWithBarcode(barcode, mainorderid);
            }
        }
        jQuery(document).ready(function () {
            var imagesPreview = function (input, placeToInsertImagePreview) {
                if (input.files) {
                    var filesAmount = input.files.length;

                    for (i = 0; i < filesAmount; i++) {
                        var reader = new FileReader();

                        reader.onload = function (event) {
                            $(`<li><img src="${event.target.result}" alt=""></li>`).appendTo(placeToInsertImagePreview);
                        }

                        reader.readAsDataURL(input.files[i]);
                    }
                }

            };

            $('#gallery-photo-add').on('change', function () {
                imagesPreview(this, '.list-photo');
            });
        });


        //tạo mã mới với barcode
        function CheckAddTempCodeWithBarcode(barcode, mainorderid) {
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            var code = barcode;
            add_loading();
            $.ajax({
                type: "POST",
                url: "/manager/TQWareHouse.aspx/CheckOrderShopCode",
                data: "{ordershopcode:'" + code + "',mainorderid:'" + mainorderid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret != "none") {
                        var PackageAll = JSON.parse(ret);
                        var data = PackageAll.listPackageGet[0];
                        var UID = data.UID;
                        var Wallet = data.Wallet;
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
                            var idpack = "bc-" + data.BarCode + "-" + packageID;
                            var html = '';
                            html += "<div class=\"col-md-3 package-item \" >";
                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
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
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                            html += "       </div>";
                            var status = data.Status;
                            if (status < 3) {
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                html += "           <select class=\"package-status-select\">";
                                html += "               <option value=\"2\">Đã về kho TQ</option>";
                                html += "           </select>";
                                html += "       </div>";
                            }
                            else {
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                if (status == 0)
                                    html += "       <span class=\"package-info packageStatus bg-black\">Đã hủy</span>";
                                else if (status == 1)
                                    html += "       <span class=\"package-info packageStatus bg-red\">Mới đặt - chưa về kho TQ</span>";
                                else if (status == 2)
                                    html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về kho TQ</span>";
                                else if (status == 3)
                                    html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                else
                                    html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                html += "       </div>";
                            }
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                            html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\" style=\"color:#fff\">";
                            html += "           <span class=\"package-label\">Kích thước:</span>";
                            html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                            html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                            html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                            html += "       </div>";
                            var selectedbigpack = parseFloat($("#<%=ddlBigpackage.ClientID%> option:selected").val());
                            html += "       <div class=\"row-package\" style=\"color:#fff\">";
                            html += "           <span class=\"package-label\">Bao lớn:</span>";
                            html += "           <select class=\"package-info bigpackageID form-control\" style=\"color:#000;width:40%\">";
                            html += "               <option value=\"0\">Chọn bao lớn</option>";
                            var getBs = data.ListBig;
                            for (var k = 0; k < getBs.length; k++) {
                                var b = getBs[k];
                                var idbig = parseFloat(b.ID);
                                if (selectedbigpack == idbig) {
                                    html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                }
                                else {
                                    html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                }
                            }
                            html += "           </select>";
                            html += "       </div>";

                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Ghi chú:</span>";
                            html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú\" style=\"color:#000;\" value=\"" + data.Note + "\"/>";
                            html += "       </div>";



                            var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                            html += "       <div class=\"row-package \">";
                            if (status == 0) {
                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                            }
                            else {
                                if (status < 3) {
                                    html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                }
                                else
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                            }
                            //html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">In tem</a>";

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
                            $("#capnhatall").show();
                            close_popup_ms();
                        }

                    }
                    remove_loading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });
        }

        //tạo mã kiện mới
        function CheckAddTempCode() {
            var c = confirm("Bạn muốn tạo mã kiện mới?");
            if (c) {
                debugger;
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                var code = $("#ordercode-temp").val();
                var ordercode = $("#ordertransactioncode-temp").val();
                var note = $("#note-temp").val();
                var base64 = "";
                $("#gallery li img").each(function () {
                    base64 += $(this).attr('src') + "|";
                })
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/manager/TQWareHouse.aspx/CheckOrderShopCodeNew",
                    data: "{ordershopcode:'" + code + "',ordertransaction:'" + ordercode + "',base64:'" + base64 + "',note:'" + note + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret != "none") {
                            if (ret == "noteexistordercode") {
                                alert('Không tồn tại mã đơn hàng trong hệ thống, vui lòng kiểm tra lại.');
                            }
                            else if (ret == "existsmallpackage") {
                                alert('Mã kiện đã tồn tại trong hệ thống, vui lòng chọn mã khác');
                            }
                            else {
                                var PackageAll = JSON.parse(ret);
                                var data = PackageAll.listPackageGet[0];
                                var UID = data.UID;
                                var Wallet = data.Wallet;
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
                                    var idpack = "bc-" + data.BarCode + "-" + packageID;
                                    var html = '';
                                    html += "<div class=\"col-md-3 package-item \" >";
                                    html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                    if (data.OrderType == "Đơn hàng mua hộ") {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                        //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
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
                                    else if (data.OrderType == "Đơn hàng VC hộ") {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                        //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong></span>";
                                        html += "       </div>";
                                    }
                                    else {
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                        html += "       </div>";
                                    }
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                    html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                    html += "       </div>";
                                    var status = data.Status;
                                    if (status < 3) {
                                        html += "       <div class=\"row-package status-pack\">";
                                        html += "           <span class=\"package-label\">Trạng thái:</span>";
                                        html += "           <select class=\"package-status-select\">";
                                        html += "               <option value=\"2\">Đã về kho TQ</option>";
                                        html += "           </select>";
                                        html += "       </div>";
                                    }
                                    else {
                                        html += "       <div class=\"row-package status-pack\">";
                                        html += "           <span class=\"package-label\">Trạng thái:</span>";
                                        if (status == 0)
                                            html += "       <span class=\"package-info packageStatus bg-black\">Đã hủy</span>";
                                        else if (status == 1)
                                            html += "       <span class=\"package-info packageStatus bg-red\">Mới đặt - chưa về kho TQ</span>";
                                        else if (status == 2)
                                            html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về kho TQ</span>";
                                        else if (status == 3)
                                            html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                        else
                                            html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                        html += "       </div>";
                                    }
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                    html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                    html += "           <span class=\"package-label\">Kích thước:</span>";
                                    html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                    html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                    html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                    html += "       </div>";
                                    var selectedbigpack = parseFloat($("#<%=ddlBigpackage.ClientID%> option:selected").val());
                                    html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                    html += "           <span class=\"package-label\">Bao lớn:</span>";
                                    html += "           <select class=\"package-info bigpackageID form-control\" style=\"color:#000;width:40%\">";
                                    html += "               <option value=\"0\">Chọn bao lớn</option>";
                                    var getBs = data.ListBig;
                                    for (var k = 0; k < getBs.length; k++) {
                                        var b = getBs[k];
                                        var idbig = parseFloat(b.ID);
                                        if (selectedbigpack == idbig) {
                                            html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                        }
                                        else {
                                            html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                        }
                                    }
                                    html += "           </select>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\">Ghi chú:</span>";
                                    html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú\" style=\"color:#000;\" value=\"" + data.Note + "\"/>";
                                    html += "       </div>";



                                    var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                    html += "       <div class=\"row-package \">";
                                    if (status == 0) {
                                        html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";
                                    }
                                    else {
                                        if (status < 3) {
                                            html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                            //html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">In tem</a>";
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                        }
                                        else
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                                    }


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
                                    $("#capnhatall").show();
                                    close_popup_ms();
                                }
                            }

                    }
                        remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert(errorthrow);
                    }
                });
        }
    }
   <%-- function CheckAddTempCode11() {
        var c = confirm("Bạn muốn tạo mã kiện mới?");
        if (c) {
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            var code = $("#ordercode-temp").val();
            var ordercode = $("#ordertransactioncode-temp").val();
            var note = $("#note-temp").val();
            var base64 = "";
            $("#gallery li img").each(function () {
                base64 += $(this).attr('src') + "|";
            })
            add_loading();
            $.ajax({
                type: "POST",
                url: "/manager/TQWareHouse.aspx/CheckOrderShopCodeNew",
                //data: "{ordershopcode:'" + code + "',ordertransaction:'" + ordercode + "'}",
                data: "{ordershopcode:'" + code + "',ordertransaction:'" + ordercode + "',base64:'" + base64 + "',note:'" + note + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret != "none") {
                        if (ret == "noteexistordercode") {
                            alert('Không tồn tại mã đơn hàng trong hệ thống, vui lòng kiểm tra lại.');
                        }
                        else {
                            var PackageAll = JSON.parse(ret);
                            var data = PackageAll.listPackageGet[0];
                            var UID = data.UID;
                            var Wallet = data.Wallet;
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
                                var idpack = "bc-" + data.BarCode + "-" + packageID;
                                var html = '';
                                html += "<div class=\"col-md-3 package-item \" >";
                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                if (data.OrderType == "Đơn hàng mua hộ") {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
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
                                else if (data.OrderType == "Đơn hàng VC hộ") {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong></span>";
                                    html += "       </div>";
                                }
                                else {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                    html += "       </div>";
                                }
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                html += "       </div>";
                                var status = data.Status;
                                if (status < 3) {
                                    html += "       <div class=\"row-package status-pack\">";
                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                    html += "           <select class=\"package-status-select\">";
                                    html += "               <option value=\"2\">Đã về kho TQ</option>";
                                    html += "           </select>";
                                    html += "       </div>";
                                }
                                else {
                                    html += "       <div class=\"row-package status-pack\">";
                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                    if (status == 0)
                                        html += "       <span class=\"package-info packageStatus bg-black\">Đã hủy</span>";
                                    else if (status == 1)
                                        html += "       <span class=\"package-info packageStatus bg-red\">Mới đặt - chưa về kho TQ</span>";
                                    else if (status == 2)
                                        html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về kho TQ</span>";
                                    else if (status == 3)
                                        html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                    else
                                        html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                    html += "       </div>";
                                }
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                html += "           <span class=\"package-label\">Kích thước:</span>";
                                html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                html += "       </div>";
                                var selectedbigpack = parseFloat($("#<%=ddlBigpackage.ClientID%> option:selected").val());
                                html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                html += "           <span class=\"package-label\">Bao lớn:</span>";
                                html += "           <select class=\"package-info bigpackageID form-control\" style=\"color:#000;width:40%\">";
                                html += "               <option value=\"0\">Chọn bao lớn</option>";
                                var getBs = data.ListBig;
                                for (var k = 0; k < getBs.length; k++) {
                                    var b = getBs[k];
                                    var idbig = parseFloat(b.ID);
                                    if (selectedbigpack == idbig) {
                                        html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                    }
                                    else {
                                        html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                    }
                                }
                                html += "           </select>";
                                html += "       </div>";

                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Ghi chú:</span>";
                                html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú:\" style=\"color:#000;\" value=\"" + data.Note + "\"/>";
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
                                        html += "           <li>";
                                        html += "<div class=\"close1\"><i class=\"close_message\" onclick=\"Remove($(this))\"></i></div>";
                                        html += "<a href=\"" + IMG[i] + "\" data-lightbox=\"image-1\"><img src=\"" + IMG[i] + "\"></a>";
                                        html += "</li> ";
                                    }
                                }
                                html += "</ul>";
                                html += "       </div>";

                                var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                html += "       <div class=\"row-package \">";
                                if (status == 0) {
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";
                                }
                                else {
                                    if (status < 3) {
                                        html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                        //html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">In tem</a>";
                                        html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                    }
                                    else
                                        html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                                }


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
                                $("#capnhatall").show();
                                close_popup_ms();
                            }
                        }

                    }
                    remove_loading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });
        }
    }--%>
    function addLoading() {
        $("#main-col-wrap").append("<div class=\"addloading\"></div>");
    }
    function removeLoading() {
        $(".addloading").remove();
    }
    function isEmpty(str) {
        return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
    }
    //Lấy từ mã đơn
    function getCode(obj) {
        var bc = obj.val();
        var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            if (isEmpty(bc)) {
                alert('Vui lòng không để trống barcode.');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/manager/TQWareHouse.aspx/GetCode",
                    data: "{barcode:'" + bc + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var listdata = JSON.parse(msg.d);
                        if (listdata != "none") {

                            var bo = listdata;
                            var type = parseFloat(bo.BigPackOutType);
                            if (type == 1) {
                                var listPackageAlls = bo.Pall;
                                //var listSmallpackage = PackageAll.listPackageGet[0].listPackageGet;
                                for (var k = 0; k < listPackageAlls.length; k++) {
                                    var listPackageAll = listPackageAlls[k];
                                    var listSmallpackage = listPackageAll.listPackageGet;
                                    var MainorderID = listPackageAll.MainOrderID;
                                    var mb = MainorderID + "-" + bc;
                                    var checkOrderCode = false;
                                    $(".item-ordercode").each(function () {
                                        if ($(this).attr("data-mb") == mb) {
                                            checkOrderCode = true;
                                        }
                                    });
                                    if (checkOrderCode == false) {
                                        var htmlListOrderCode = "";
                                        htmlListOrderCode += "<tr class=\"item-ordercode\" data-mb=\"" + mb + "\" data-mainorderid=\"" + listPackageAll.MainOrderID + "\" data-code=\"" + bc + "\" data-orderid=\"1\">";
                                        htmlListOrderCode += "  <td>" + listPackageAll.MainOrderID + "</td>";
                                        //htmlListOrderCode += "  <td><span style=\"float:left\">" + bc + "</span><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></td>";
                                        htmlListOrderCode += "  <td><span style=\"float:left\">" + bc + "</span></td>";
                                        htmlListOrderCode += "  <td>" + listSmallpackage.length + "</td>";
                                        htmlListOrderCode += "  <td>";
                                        htmlListOrderCode += "      <a href=\"javascript:;\" onclick=\"addPackageTemp($(this))\" class=\"btn primary-btn\">Thêm kiện tạm</a>";
                                        htmlListOrderCode += "  </td>";
                                        htmlListOrderCode += "</tr>";
                                        $(".list-ordercodetransaction").append(htmlListOrderCode);
                                    }
                                    for (var i = 0; i < listSmallpackage.length; i++) {
                                        var check1 = false;
                                        var data = listSmallpackage[i];
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

                                        var itembarcode = data.BarCode + ",bc-" + data.BarCode;
                                        var listbarcode1 = $("#<%=hdfListBarcode.ClientID%>").val();

                                        var bs1 = listbarcode1.split('|');
                                        for (var j = 0; j < bs1.length - 1; j++) {
                                            if (itembarcode == bs1[j]) {
                                                check1 = true;
                                            }
                                        }

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
                                            var idpack = "bc-" + data.BarCode + "-" + packageID;
                                            var html = '';
                                            html += "<div class=\"col-md-3 package-item\" >";
                                            if (data.Status == 0) {
                                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail ishuy\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                            }
                                            else if (data.Status > 0 && data.Status < 2) {
                                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail \" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                            }
                                            else {
                                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                            }
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Kho:</strong></span>";
                                            html += "           <span class=\"package-info packageReceivePlace\"><strong style=\"background:orange;color:#fff;padding:5px 10px;\">" + data.Noinhan + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                            //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
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
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                            html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                            html += "       </div>";
                                            var status = data.Status;
                                            if (status < 3) {
                                                if (status == 0) {
                                                    html += "       <div class=\"row-package status-pack\" style=\"display:none\">";
                                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                    html += "           <select class=\"package-status-select\">";
                                                    html += "               <option value=\"0\">Hủy kiện</option>";
                                                    html += "           </select>";
                                                    html += "       </div>";
                                                }
                                                else if (status == 1) {
                                                    html += "       <div class=\"row-package status-pack\">";
                                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                    html += "           <select class=\"package-status-select\">";
                                                    html += "               <option value=\"2\">Đã về kho TQ</option>";
                                                    html += "               <option value=\"0\">Hủy kiện</option>";
                                                    html += "           </select>";
                                                    html += "       </div>";
                                                }
                                                else if (status == 2) {
                                                    html += "       <div class=\"row-package status-pack\">";
                                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                    html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                                    html += "               <option value=\"2\">Đã về kho TQ</option>";
                                                    html += "           </select>";
                                                    html += "           <span class=\"package-info packageStatus bg-orange\" style=\"color:#000;\">Đã về kho TQ</span>";
                                                    html += "       </div>";
                                                }
                                            }
                                            else {
                                                html += "       <div class=\"row-package status-pack\">";
                                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                if (status == 0)
                                                    html += "       <span class=\"package-info packageStatus bg-black\">Đã hủy</span>";
                                                else if (status == 1)
                                                    html += "       <span class=\"package-info packageStatus bg-red\">Mới đặt - chưa về kho TQ</span>";
                                                else if (status == 2)
                                                    html += "       <span class=\"package-info packageStatus bg-orange\">Đã về kho TQ</span>";
                                                else if (status == 3)
                                                    html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                                else
                                                    html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                                html += "       </div>";
                                            }
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                            html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                            html += "           <span class=\"package-label\">Kích thước:</span>";
                                            html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                            html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                            html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                            html += "       </div>";
                                            var selectedbigpack = parseFloat($("#<%=ddlBigpackage.ClientID%> option:selected").val());
                                            html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                            html += "           <span class=\"package-label\">Bao lớn:</span>";
                                            html += "           <select class=\"package-info bigpackageID form-control\" style=\"color:#000;width:40%\">";
                                            html += "               <option value=\"0\">Chọn bao lớn</option>";
                                            var getBs = data.ListBig;
                                            for (var k = 0; k < getBs.length; k++) {
                                                var b = getBs[k];
                                                var idbig = parseFloat(b.ID);
                                                if (selectedbigpack == idbig) {
                                                    html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                                }
                                                else {
                                                    html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                                }
                                            }
                                            html += "           </select>";
                                            html += "       </div>";

                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\">Ghi chú:</span>";
                                            html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú\" style=\"color:#000;\" value=\"" + data.Note + "\"/>";
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

                                            html += "       <div class=\"row-package \">";

                                            var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                            if (status == 0) {
                                                html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" style=\"display:none\">Cập nhật</a>";
                                                //html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                                            }
                                            else {
                                                if (status < 3) {
                                                    html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                                    html += "           <a href=\"/manager/PrintStamp.aspx?id=" + data.MainorderID + "\"  class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" target=\"_blank\">In Tem</a>";
                                                    
                                                    //html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                                }
                                                else {
                                                    html += "           <a href=\"/manager/PrintStamp.aspx?id=" + data.MainorderID + "\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" target=\"_blank\">In Tem</a>";
                                                }
                                            }


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
                                            $("#capnhatall").show();
                                        }
                                    }
                                }
                                obj.val("");
                            }
                            else {
                                var PackageAll = bo.Pall;
                                var listSmallpackage = PackageAll[0].listPackageGet;
                                for (var i = 0; i < listSmallpackage.length; i++) {
                                    var data = listSmallpackage[i];
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
                                        var html = '';
                                        var idpack = "bc-" + data.BarCode + "-" + packageID;
                                        html += "<div class=\"col-md-3 package-item \" >";
                                        if (data.Status == 0) {
                                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail ishuy\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        else {
                                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        if (data.OrderType == "Đơn hàng mua hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                            //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
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
                                        else if (data.OrderType == "Đơn hàng VC hộ") {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            html += "       </div>";
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                            //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong></span>";
                                            html += "       </div>";
                                        }
                                        else {
                                            html += "       <div class=\"row-package\">";
                                            html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderType + "</strong></span>";
                                            html += "       </div>";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                        html += "       </div>";
                                        var status = data.Status;
                                        if (status < 3) {
                                            if (status == 0) {
                                                html += "       <div class=\"row-package status-pack\" style=\"display:none\">";
                                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                html += "           <select class=\"package-status-select\">";
                                                html += "               <option value=\"0\">Hủy kiện</option>";
                                                html += "           </select>";
                                                html += "       </div>";
                                            }
                                            else if (status == 1) {
                                                html += "       <div class=\"row-package status-pack\">";
                                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                html += "           <select class=\"package-status-select\">";
                                                html += "               <option value=\"2\">Đã về kho TQ</option>";
                                                html += "               <option value=\"0\">Hủy kiện</option>";
                                                html += "           </select>";
                                                html += "       </div>";
                                            }
                                            else if (status == 2) {
                                                html += "       <div class=\"row-package status-pack\">";
                                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                                html += "               <option value=\"2\">Đã về kho TQ</option>";
                                                html += "           </select>";
                                                html += "           <span class=\"package-info packageStatus bg-orange\" style=\"color:#000;\">Đã về kho TQ</span>";
                                                html += "       </div>";
                                            }
                                        }
                                        else {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            if (status == 0)
                                                html += "       <span class=\"package-info packageStatus bg-black\">Đã hủy</span>";
                                            else if (status == 1)
                                                html += "       <span class=\"package-info packageStatus bg-red\">Mới đặt - chưa về kho TQ</span>";
                                            else if (status == 2)
                                                html += "       <span class=\"package-info packageStatus bg-orange\">Đã về kho TQ</span>";
                                            else if (status == 3)
                                                html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                            else
                                                html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                            html += "       </div>";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                        html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                        html += "           <span class=\"package-label\">Kích thước:</span>";
                                        html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                        html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                        html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                        html += "       </div>";
                                        var selectedbigpack = parseFloat($("#<%=ddlBigpackage.ClientID%> option:selected").val());
                                        html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                        html += "           <span class=\"package-label\">Bao lớn:</span>";
                                        html += "           <select class=\"package-info bigpackageID form-control\" style=\"color:#000;width:40%\">";
                                        html += "               <option value=\"0\">Chọn bao lớn</option>";
                                        var getBs = data.ListBig;
                                        for (var k = 0; k < getBs.length; k++) {
                                            var b = getBs[k];
                                            var idbig = parseFloat(b.ID);
                                            if (selectedbigpack == idbig) {
                                                html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                            }
                                            else {
                                                html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                            }
                                        }
                                        html += "           </select>";
                                        html += "       </div>";

                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Ghi chú:</span>";
                                        html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú:\" style=\"color:#000;\" value=\"" + data.Note + "\"/>";
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
                                        if (status == 0) {
                                            html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" style=\"display:none\">Cập nhật</a>";
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                                        }
                                        else {
                                            if (status < 3) {
                                                html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                                html += "           <a href=\"/manager/PrintStamp.aspx?id=" + data.MainorderID + "\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" target=\"_blank\">In Tem</a>";
                                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                            }
                                            else {
                                                html += "           <a href=\"/manager/PrintStamp.aspx?id=" + data.MainorderID + "\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" target=\"_blank\">In Tem</a>";
                                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                            }
                                        }
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
                                        $("#capnhatall").show();
                                    }
                                    else {
                                        obj.val("");
                                        add_loading();
                                        $(".package-detail").each(function () {
                                            var ordercodetrans = $(this).attr("data-barcode");
                                            if (ordercodetrans == bc) {
                                                var packageID = $(this).attr("data-packageID");
                                                var idpack = "bc-" + bc + "-" + packageID;
                                                updateWeight(bc, idpack, $(this).find(".updatebutton"), packageID);
                                            }
                                        })

                                    }
                                }
                                obj.val("");
                            }
                        }
                        else {
                            alert('Không tìm thấy');
                        }
                        showtlblist();
                        removeLoading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        removeLoading();
                    }
                });

            }

        }
        function getCode_old(obj) {
            addLoading();
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
                    url: "/manager/TQWareHouse.aspx/GetCode",
                    data: "{barcode:'" + bc + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var listdata = JSON.parse(msg.d);
                        if (listdata != "none") {
                            var PackageAll = listdata;
                            var type = parseFloat(PackageAll.PackageAllType);
                            var listSmallpackage = PackageAll.listPackageGet;
                            if (type == 1) {
                                var checkOrderCode = false;
                                $(".item-ordercode").each(function () {
                                    if ($(this).attr("data-code") == bc) {
                                        checkOrderCode = true;
                                    }
                                })
                                if (checkOrderCode == false) {
                                    var htmlListOrderCode = "";
                                    htmlListOrderCode += "<tr class=\"item-ordercode\" data-code=\"" + bc + "\" data-orderid=\"1\">";
                                    htmlListOrderCode += "  <td>" + bc + "</td>";
                                    htmlListOrderCode += "  <td>" + listSmallpackage.length + "</td>";
                                    htmlListOrderCode += "  <td>";
                                    htmlListOrderCode += "      <a href=\"javascript:;\" onclick=\"addPackageTemp($(this))\" class=\"btn primary-btn\">Thêm kiện tạm</a>";
                                    htmlListOrderCode += "  </td>";
                                    htmlListOrderCode += "</tr>";
                                    $(".list-ordercodetransaction").append(htmlListOrderCode);
                                }

                                for (var i = 0; i < listSmallpackage.length; i++) {
                                    var check1 = false;
                                    var data = listSmallpackage[i];
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

                                    var itembarcode = data.BarCode + ",bc-" + data.BarCode;
                                    var listbarcode1 = $("#<%=hdfListBarcode.ClientID%>").val();

                                    var bs1 = listbarcode1.split('|');
                                    for (var j = 0; j < bs1.length - 1; j++) {
                                        if (itembarcode == bs1[j]) {
                                            check1 = true;
                                        }
                                    }
                                    if (check1 == false) {
                                        var html = '';
                                        html += "<div class=\"col-md-3 package-item\" >";
                                        if (data.Status == 0) {
                                            html += "   <div id=\"bc-" + data.BarCode + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail ishuy\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        else if (data.Status > 0 && data.Status < 2) {
                                            html += "   <div id=\"bc-" + data.BarCode + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail \" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        else {
                                            html += "   <div id=\"bc-" + data.BarCode + "\" data-mainorderid=\"" + data.MainorderID + "\"  class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng mua hộ</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                        html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                        //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
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
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                        html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                        html += "       </div>";
                                        var status = data.Status;
                                        if (status < 3) {
                                            if (status == 0) {
                                                html += "       <div class=\"row-package status-pack\" style=\"display:none\">";
                                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                html += "           <select class=\"package-status-select\">";
                                                html += "               <option value=\"0\">Hủy kiện</option>";
                                                html += "           </select>";
                                                html += "       </div>";
                                            }
                                            else if (status == 1) {
                                                html += "       <div class=\"row-package status-pack\">";
                                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                html += "           <select class=\"package-status-select\">";
                                                html += "               <option value=\"2\">Đã về kho TQ</option>";
                                                html += "               <option value=\"0\">Hủy kiện</option>";
                                                html += "           </select>";
                                                html += "       </div>";
                                            }
                                            else if (status == 2) {
                                                html += "       <div class=\"row-package status-pack\">";
                                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                                html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                                html += "               <option value=\"2\">Đã về kho TQ</option>";
                                                html += "           </select>";
                                                html += "           <span class=\"package-info packageStatus bg-orange\" style=\"color:#000;\">Đã về kho TQ</span>";
                                                html += "       </div>";
                                            }
                                        }
                                        else {
                                            html += "       <div class=\"row-package status-pack\">";
                                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                                            if (status == 0)
                                                html += "       <span class=\"package-info packageStatus bg-black\">Đã hủy</span>";
                                            else if (status == 1)
                                                html += "       <span class=\"package-info packageStatus bg-red\">Mới đặt - chưa về kho TQ</span>";
                                            else if (status == 2)
                                                html += "       <span class=\"package-info packageStatus bg-orange\">Đã về kho TQ</span>";
                                            else if (status == 3)
                                                html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                            else
                                                html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                            html += "       </div>";
                                        }
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                        html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                        html += "           <span class=\"package-label\">Kích thước:</span>";
                                        html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                        html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                        html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                        html += "       </div>";
                                        var selectedbigpack = parseFloat($("#<%=ddlBigpackage.ClientID%> option:selected").val());
                                        html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                        html += "           <span class=\"package-label\">Bao lớn:</span>";
                                        html += "           <select class=\"package-info bigpackageID form-control\" style=\"color:#000;width:40%\">";
                                        html += "               <option value=\"0\">Chọn bao lớn</option>";
                                        var getBs = data.ListBig;
                                        for (var k = 0; k < getBs.length; k++) {
                                            var b = getBs[k];
                                            var idbig = parseFloat(b.ID);
                                            if (selectedbigpack == idbig) {
                                                html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                            }
                                            else {
                                                html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                            }
                                        }
                                        html += "           </select>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package\">";
                                        html += "           <span class=\"package-label\">Ghi chú:</span>";
                                        html += "           <input class=\"package-info packagedescription\" placeholder=\"Ghi chú\" style=\"color:#000;\" value=\"" + data.Note + "\"/>";
                                        html += "       </div>";
                                        html += "       <div class=\"row-package \">";

                                        if (status == 0) {
                                            html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" style=\"display:none\">Cập nhật</a>";
                                            html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                                        }
                                        else {
                                            if (status < 3) {
                                                html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                                //html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">In tem</a>";
                                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                            }
                                            else {
                                                //html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">In tem</a>";
                                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                            }
                                        }


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
                                        $("#capnhatall").show();
                                    }
                                }
                                obj.val("");
                            }
                            else {
                                var data = listSmallpackage[0];
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
                                html += "<div class=\"col-md-3 package-item \" >";
                                if (data.Status == 0) {
                                    html += "   <div id=\"bc-" + data.BarCode + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail ishuy\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                }
                                else {
                                    html += "   <div id=\"bc-" + data.BarCode + "\" data-mainorderid=\"" + data.MainorderID + "\" class=\"package-detail isvekho\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                }
                                if (listSmallpackage.OrderType == "Đơn hàng mua hộ") {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng mua hộ</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã đơn hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + data.OrderShopCode + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
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
                                else {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại đơn hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng VC hộ</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Order ID:</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong><a style=\"float:right\" href=\"javascript:;\" onclick=\"sendmess()\"><img src=\"/App_Themes/AdminNew/images/message-icon.png\"></a></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/manager/transportationdetail.aspx?id=" + data.TransportationID + "\" target=\"_blank\">" + data.TransportationID + "</a></strong></span>";
                                    html += "       </div>";
                                }


                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                html += "       </div>";
                                var status = data.Status;
                                if (status < 3) {
                                    if (status == 0) {
                                        html += "       <div class=\"row-package status-pack\" style=\"display:none\">";
                                        html += "           <span class=\"package-label\">Trạng thái:</span>";
                                        html += "           <select class=\"package-status-select\">";
                                        html += "               <option value=\"0\">Hủy kiện</option>";
                                        html += "           </select>";
                                        html += "       </div>";
                                    }
                                    else if (status == 1) {
                                        html += "       <div class=\"row-package status-pack\">";
                                        html += "           <span class=\"package-label\">Trạng thái:</span>";
                                        html += "           <select class=\"package-status-select\">";
                                        html += "               <option value=\"2\">Đã về kho TQ</option>";
                                        html += "               <option value=\"0\">Hủy kiện</option>";
                                        html += "           </select>";
                                        html += "       </div>";
                                    }
                                    else if (status == 2) {
                                        html += "       <div class=\"row-package status-pack\">";
                                        html += "           <span class=\"package-label\">Trạng thái:</span>";
                                        html += "           <select class=\"package-status-select\" style=\"display:none\">";
                                        html += "               <option value=\"2\">Đã về kho TQ</option>";
                                        html += "           </select>";
                                        html += "           <span class=\"package-info packageStatus bg-orange\" style=\"color:#000;\">Đã về kho TQ</span>";
                                        html += "       </div>";
                                    }
                                }
                                else {
                                    html += "       <div class=\"row-package status-pack\">";
                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                    if (status == 0)
                                        html += "       <span class=\"package-info packageStatus bg-black\">Đã hủy</span>";
                                    else if (status == 1)
                                        html += "       <span class=\"package-info packageStatus bg-red\">Mới đặt - chưa về kho TQ</span>";
                                    else if (status == 2)
                                        html += "       <span class=\"package-info packageStatus bg-orange\">Đã về kho TQ</span>";
                                    else if (status == 3)
                                        html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                    else
                                        html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                    html += "       </div>";
                                }
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                html += "           <span class=\"package-label\">Kích thước:</span>";
                                html += "           d: <input type=\"Number\" min=\"0\" class=\"package-info lengthsize\" placeholder=\"dài\" style=\"color:#000;width:50px\" value=\"" + data.dai + "\"/> x ";
                                html += "           r: <input type=\"Number\" min=\"0\" class=\"package-info widthsize\" placeholder=\"rộng\" style=\"color:#000;width:50px\" value=\"" + data.rong + "\"/> x ";
                                html += "           c: <input type=\"Number\" min=\"0\" class=\"package-info heightsize\" placeholder=\"cao\" style=\"color:#000;width:50px\" value=\"" + data.cao + "\"/>";
                                html += "       </div>";
                                var selectedbigpack = parseFloat($("#<%=ddlBigpackage.ClientID%> option:selected").val());
                                html += "       <div class=\"row-package\" style=\"color:#fff\">";
                                html += "           <span class=\"package-label\">Bao lớn:</span>";
                                html += "           <select class=\"package-info bigpackageID form-control\" style=\"color:#000;width:40%\">";
                                html += "               <option value=\"0\">Chọn bao lớn</option>";
                                var getBs = data.ListBig;
                                for (var k = 0; k < getBs.length; k++) {
                                    var b = getBs[k];
                                    var idbig = parseFloat(b.ID);
                                    if (selectedbigpack == idbig) {
                                        html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                    }
                                    else {
                                        html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                    }
                                }
                                html += "           </select>";
                                html += "       </div>";
                                html += "       <div class=\"row-package \">";
                                if (status == 0) {
                                    html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\" style=\"display:none\">Cập nhật</a>";
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Ẩn</a>";
                                }
                                else {
                                    if (status < 3) {
                                        html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"updatebutton xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                        //html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">In tem</a>";
                                        html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                    }
                                    else {
                                        //html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">In tem</a>";
                                        html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Ẩn</a>";
                                    }
                                }



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
                                $("#capnhatall").show();
                            }
                        }
                        else {
                            alert('Không tìm thấy');
                        }
                        showtlblist();
                        removeLoading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        removeLoading();
                    }
                });

            } else {
                obj.val("");
                add_loading();
                $(".package-detail").each(function () {
                    var ordercodetrans = $(this).attr("data-barcode");
                    if (ordercodetrans == bc) {
                        var packageID = $(this).attr("data-packageID");
                        var idpack = "bc-" + bc + "-" + packageID;
                        updateWeight(bc, idpack, $(this).find(".updatebutton"), packageID);
                      
                    }
                })

            }
            removeLoading();
        }
        function showtlblist() {
            if ($(".item-ordercode").length > 0) {
                $("#tbl-list-ordercode").show();
            }
            else {
                $("#tbl-list-ordercode").hide();
            }
        }
        function countOrder() {
            //countorder
            var count = $(".package-detail").length;
            $("#countorder").html(count);
        }
        function add_loading() {
            $(".page-inner").append("<div class='loading_bg'></div>");
            var height = $(".page-inner").height();
            $(".loading_bg").css("height", height + "px");
        }
        function remove_loading() {
            $(".loading_bg").remove();
        }
        function updateWeight(barcode, id, obj, packageID) {
            debugger;
            var dai = obj.parent().parent().find(".lengthsize").val();
            var rong = obj.parent().parent().find(".widthsize").val();
            var cao = obj.parent().parent().find(".heightsize").val();
            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            var bigpackage = obj.parent().parent().find(".bigpackageID").val();
            var note = obj.parent().parent().find(".packagedescription").val();
            
            add_loading();
            $.ajax({
                type: "POST",
                url: "/manager/TQWareHouse.aspx/UpdateQuantity_old",
                data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'"
                    + bigpackage + "',packageID:'" + packageID + "',dai:'" + dai + "',rong:'" + rong + "',cao:'"
                    + cao + "',ghichu:'" + note + "'}",
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
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:white");
                        }

                    }
                    remove_loading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(textstatus);
                }
            });

        }
        function updateWeightTemp(barcode, mainorderID, obj) {
            add_loading();
            $.ajax({
                type: "POST",
                url: "/manager/TQWareHouse.aspx/UpdateTemp",
                data: "{barcode:'" + barcode + "',mainorderID:'" + mainorderID + "'}",
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
        function printNewBarCode(barcode) {

        }
        function updateWeight_Each(barcode, id, quantity, status, bigpackage, obj) {
            //var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            //var status = obj.parent().parent().find(".package-status-select").val();
            //var bigpackage = obj.parent().parent().find(".package-bigpackage-select").val();
            if (quantity > 0) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/manager/TQWareHouse.aspx/UpdateQuantity",
                    data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'" + bigpackage + "'}",
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
                //alert("Vui lòng nhập số Kg");
                obj.find(".row-package-status").html("<span style=\"color:red\">Chưa nhập số kg</span>");
                obj.attr("style", "border:solid 2px red;");
            }

        }
        function updateAll() {
            $(".package-detail").each(function () {
                var barcode = $(this).attr("data-barcode");
                var id = $(this).attr("id");
                var quantity = $(this).find(".packageWeightUpdate").val();
                var status = $(this).find(".package-status-select").val();
                var bigpackage = $(this).find(".package-bigpackage-select").val();
                //alert(barcode + " - " + id + " - " + quantity + " - " + status + " - " + bigpackage);
                //alert(barcode + " - " + id + " - " + quantity + " - " + status + " - " + bigpackage);
                updateWeight_Each(barcode, id, quantity, status, bigpackage, $(this));
            });
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
                    $("#capnhatall").hide();
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
                url: "/manager/OutStock.aspx/SetFinish",
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
        function sendmess() {
            swal
            (
                {
                    title: 'Gửi yêu cầu hỗ trợ đơn hàng',
                    text: "Nội dung:",
                    type: "input",
                    showCancelButton: false,
                    confirmButtonClass: "btn btn-success btn-block small-btn",
                    confirmButtonText: "Gửi",
                    html: true,
                    closeOnConfirm: false
                }
                    //function () { window.location.replace(window.location.href); }
	         );
        }
    </script>
    <style>
        #capnhatall {
            display: inline;
            margin-top: 20px;
            float: left;
            width: auto;
        }

        .package-item {
            width: 470px !important;
        }
    </style>
</asp:Content>
