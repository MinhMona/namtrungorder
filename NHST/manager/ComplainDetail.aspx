<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="ComplainDetail.aspx.cs" Inherits="NHST.manager.ComplainDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col-50">
                    <div class="feat-row grid-row">
                        <div class="grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Chi tiết khiếu nại</h3>
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
                                            Mã Shop
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtOrderShopCode" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Tỷ giá
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblCurrence" runat="server" CssClass="form-control"></asp:Label>
                                        </div>

                                        <div class="form-row marbot1">
                                            Số tiền (VNĐ)
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch" oninput="countte($(this))"
                                                ID="pBuyNDT" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pBuyNDT" ErrorMessage="Không để rỗng"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup="n"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-row marbot1">
                                            Số tiền (¥)
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblAmountCYN" runat="server" CssClass="form-control"></asp:Label>
                                        </div>

                                        <div class="form-row marbot1">
                                            Ảnh
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Literal ID="ltrIMG" runat="server"></asp:Literal>
                                            <%--<asp:Image runat="server" ID="imgDaiDien" Width="200" />--%>
                                        </div>
                                        <div class="form-row marbot1">
                                            Loại khiếu nại
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblComplainType" runat="server"></asp:Label>
                                            <%--<asp:Image runat="server" ID="imgDaiDien" Width="200" />--%>
                                        </div>
                                        <div class="form-row marbot1">
                                            Nội dung
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtComplainText" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Ghi chú nội bộ
                                        </div>
                                        <div class="form-row marbot2">
                                             <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Trạng thái
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">                                             
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>

                 <div class="grid-col-50">
                        <article class="pane-primary">
                            <div class="heading" style="background-color: #6639a6">
                                <h3 class="lb center-txt">Liên lạc nội bộ</h3>
                            </div>
                            <div class="chatmess-box messboxtim">
                                <div class="mess-list staff-list-comment" id="ContactLocal">
                                    <asp:Literal ID="ltrInComment" runat="server"></asp:Literal>
                                </div>
                                <div class="mess-write">
                                    <div class="writebox">
                                        <label>
                                            <span>Thêm ảnh</span>
                                            <asp:FileUpload runat="server" ID="IMGUpLoadToLocal" class="upload-img" type="file" AllowMultiple="true" title=""></asp:FileUpload>
                                        </label>
                                        <ul class="row-package staffcomment">
                                        </ul>
                                    </div>
                                    <div class="writebox" style="clear: both; margin-top: 10px;">
                                        <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" placeholder="Nội dung"></asp:TextBox>
                                        <a id="btnsendcommentstaff" href="javascript:;" class="btn" onclick="SentMessageToLocal()">Gửi</a>
                                        <asp:Button ID="btnSend" runat="server" Text="Gửi" ValidationGroup="n" Style="display: none"
                                            CssClass="btn" OnClick="btnSend_Click" />
                                    </div>
                                    <div class="writebox">
                                        <span class="info-show-staff"></span>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
            </div>
        </main>
    </asp:Panel>
    <asp:HiddenField ID="hdfCurrency" runat="server" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
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
            function countte(obj) {
                var currency = parseFloat($("#<%=hdfCurrency.ClientID %>").val());
                var value = obj.val();
                var total = value / currency;
                $("#<%= lblAmountCYN.ClientID%>").text(total);
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

            //chat realtime
            $(document).ready(function () {
                $('#<%=txtComment.ClientID%>').on("keypress", function (e) {
                    if (e.keyCode == 13) {
                        SentMessageToLocal();
                    }
                });
               
            });

            function SentMessageToLocal() {
                var data = new FormData();
                var orderID = $("#<%=hdfOrderID.ClientID%>").val();
                var comment = $("#<%=txtComment.ClientID%>").val();
                var files = $("#<%=IMGUpLoadToLocal.ClientID%>").get(0).files;
                var url = "";
                var real = "";
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    data.append("comment", comment);
                    data.append("orderID", orderID);
                    $.ajax({
                        url: '/HandlerCS.ashx',
                        type: 'POST',
                        data: data,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (file) {

                            if (file.length > 0) {
                                file.forEach(function (data, index) {
                                    url += data.name + "|";
                                    real += data.realname + "|";
                                });
                                $("#<%=IMGUpLoadToLocal.ClientID%>").replaceWith($("#<%=IMGUpLoadToLocal.ClientID%>").val('').clone(true));
                                sendStaffComment(orderID, comment, url, real);

                            }
                        },
                        error: function (e) {
                            console.log(e)
                        }
                    });

                }
                else {
                    sendStaffComment(orderID, comment, url, real);
                }


            }


            function sendStaffComment(orderID, comment, url, real) {

                if (isEmpty(comment) && url == "") {
                    $(".info-show-staff").html("Vui lòng điền nội dung.").attr("style", "color:red");
                }
                else {
                    $(".info-show-staff").html("Đang cập nhật...").attr("style", "color:blue");
                    $.ajax({
                        type: "POST",
                        url: "/manager/ComplainDetail.aspx/sendstaffcomment",
                        data: "{comment:'" + comment + "',id:'" + orderID + "',urlIMG:'" + url + "',real:'" + real + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var data = JSON.parse(msg.d);
                            if (data != null) {
                                var dataComment = data.Comment;
                                $("#ContactLocal").append(dataComment);
                                //$(".materialboxed").materialbox({
                                //    inDuration: 200,
                                //    onOpenStart: function (element) {
                                //        $(element).parents('.chat-area.ps').attr('style', 'overflow:visible !important;');
                                //    },
                                //    onCloseStart: function (element) {
                                //        $(element).parents('.chat-area.ps').attr('style', '');
                                //    }
                                //});
                                $("#imgUpToLocal").html("");

                                $(".info-show-staff").html("");
                                //$("#txtComment").val("");
                                $("#<<%=txtComment.ClientID%>").val('');

                                //$('select').formSelect();

                            }
                            else {
                                $("#imgUpToLocal").html("");
                                $(".info-show-staff").html("Có lỗi trong quá trình gửi, vui lòng thử lại sau.").attr("style", "color:red");
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            console.log('lỗi checkend');
                        }
                    });
                }

            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
