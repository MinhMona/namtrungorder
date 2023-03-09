using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using Telerik.Web.UI;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;
using System.Web.Script.Serialization;
using static NHST.WebService1;
using Newtonsoft.Json;

namespace NHST.manager
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    hdfID.Value = ac.ID.ToString();
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                    else
                    {
                        //if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                        //{
                        //    Response.Redirect("/manager/home.aspx");
                        //}
                        if (RoleID == 8)
                        {
                            Response.Redirect("/manager/home.aspx");
                        }
                    }
                    if (ac.RoleID == 3)
                        chkIsPaying.Visible = false;
                }
                checkOrderStaff();
                LoadDDL();
                loaddata();
            }
        }
        public void checkOrderStaff()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int RoleID = obj_user.RoleID.ToString().ToInt();
                int UID = obj_user.ID;
                var id = Convert.ToInt32(Request.QueryString["id"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {

                        int status_order = Convert.ToInt32(o.Status);
                        if (RoleID == 0 || RoleID == 2 || RoleID == 4 || RoleID == 5)
                        {

                        }
                        else if (RoleID == 3)
                        {
                            //if (status_order >= 2)
                            //{
                            //    Role đặt hàng
                            //    if (o.DathangID == UID)
                            //    {
                            //    }
                            //    else
                            //    {
                            //        Response.Redirect("/manager/OrderList.aspx");
                            //    }
                            //}
                            //else
                            //{
                            //    Response.Redirect("/manager/OrderList.aspx");
                            //}

                        }
                        //else if (RoleID == 4)
                        //{
                        //    if (status_order >= 5 && status_order <= 10)
                        //    {
                        //        //Role kho TQ
                        //        //if (o.KhoTQID == UID || o.KhoTQID == 0)
                        //        //{

                        //        //}
                        //        //else
                        //        //{
                        //        //    Response.Redirect("/manager/OrderList.aspx");
                        //        //}
                        //    }
                        //    else
                        //    {
                        //        Response.Redirect("/manager/OrderList.aspx");
                        //    }

                        //}
                        //else if (RoleID == 5)
                        //{
                        //    if (status_order >= 5 && status_order <= 10)
                        //    {
                        //        //Role Kho VN
                        //        //if (o.KhoVNID == UID || o.KhoVNID == 0)
                        //        //{

                        //        //}
                        //        //else
                        //        //{
                        //        //    Response.Redirect("/manager/OrderList.aspx");
                        //        //}
                        //    }
                        //    else
                        //    {
                        //        Response.Redirect("/manager/OrderList.aspx");
                        //    }

                        //}
                        else if (RoleID == 6)
                        {
                            rTotalPriceRealCYN.Visible = false;
                            rTotalPriceReal.Visible = false;
                            pHHCYN.Visible = false;
                            pHHVND.Visible = false;
                            if (status_order != 1)
                            {
                                //Role sale
                                if (o.SalerID == UID)
                                {

                                }
                                else
                                {
                                    Response.Redirect("/manager/OrderList.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("/manager/OrderList.aspx");
                            }
                        }
                        else if (RoleID == 7)
                        {
                            if (status_order >= 2)
                            {

                            }
                            else
                            {
                                Response.Redirect("/manager/OrderList.aspx");
                            }
                        }
                        else if (RoleID == 8)
                        {
                            if (status_order >= 9 && status_order < 10)
                            {

                            }
                            else
                            {
                                Response.Redirect("/manager/OrderList.aspx");
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("/manager/OrderList.aspx");
                }
            }
        }
        public void LoadDDL()
        {
            ddlSaler.Items.Clear();
            ddlSaler.Items.Insert(0, "Chọn Saler");

            ddlDatHang.Items.Clear();
            ddlDatHang.Items.Insert(0, "Chọn nhân viên đặt hàng");

            ddlKhoTQ.Items.Clear();
            ddlKhoTQ.Items.Insert(0, "Chọn nhân viên kho TQ");

            ddlKhoVN.Items.Clear();
            ddlKhoVN.Items.Insert(0, "Chọn nhân viên kho đích");

            var salers = AccountController.GetAllByRoleID(6);
            if (salers.Count > 0)
            {
                ddlSaler.DataSource = salers;
                ddlSaler.DataBind();
            }

            var dathangs = AccountController.GetAllByRoleID(3);
            if (dathangs.Count > 0)
            {
                ddlDatHang.DataSource = dathangs;
                ddlDatHang.DataBind();
            }

            var khotqs = AccountController.GetAllByRoleID(4);
            if (khotqs.Count > 0)
            {
                ddlKhoTQ.DataSource = khotqs;
                ddlKhoTQ.DataBind();
            }

            var khovns = AccountController.GetAllByRoleID(5);
            if (khovns.Count > 0)
            {
                ddlKhoVN.DataSource = khovns;
                ddlKhoVN.DataBind();
            }
            var warehousefrom = WarehouseFromController.GetAllWithIsHidden(false);
            if (warehousefrom.Count > 0)
            {
                ddlWarehouseFrom.DataSource = warehousefrom;
                ddlWarehouseFrom.DataBind();
            }
            var warehouse = WarehouseController.GetAllWithIsHidden(false);
            if (warehouse.Count > 0)
            {
                ddlReceivePlace.DataSource = warehouse;
                ddlReceivePlace.DataBind();
            }

            var shippingtype = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            if (shippingtype.Count > 0)
            {
                ddlShippingType.DataSource = shippingtype;
                ddlShippingType.DataBind();
            }
        }

        public void loaddata()
        {
            var config = ConfigurationController.GetByTop1();
            double currency = 0;
            double currency1 = 0;

            if (config != null)
            {
                double currencyconfig = 0;
                if (!string.IsNullOrEmpty(config.Currency))
                    currencyconfig = Convert.ToDouble(config.Currency);

                hdfcurrent.Value = Math.Floor(currencyconfig).ToString();
                currency = Math.Floor(currencyconfig);
                currency1 = Math.Floor(currencyconfig);
            }
            //var hnfee = FeeWeightTQVNController.GetByReceivePlace("Kho Hà Nội");
            //if (hnfee.Count > 0)
            //{
            //    string htmlhnfee = "";
            //    foreach (var item in hnfee)
            //    {
            //        htmlhnfee += item.ReceivePlace + "," + item.WeightFrom + "," + item.WeightTo + "," + item.Amount + "|";
            //    }
            //    hdfFeeTQVNHN.Value = htmlhnfee;
            //}
            //var hcmfee = FeeWeightTQVNController.GetByReceivePlace("Kho Việt Trì");
            //if (hcmfee.Count > 0)
            //{
            //    string htmlhcmfee = "";
            //    foreach (var item in hcmfee)
            //    {
            //        htmlhcmfee += item.ReceivePlace + "," + item.WeightFrom + "," + item.WeightTo + "," + item.Amount + "|";
            //    }
            //    hdfFeeTQVNHCM.Value = htmlhcmfee;
            //}

            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);

            int uid = obj_user.ID;
            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    string thongtindonhang = "";
                    thongtindonhang += o.ID.ToString();

                    double khachhangCurrency = 0;
                    int khachhangID = Convert.ToInt32(o.UID);
                    var khachhang = AccountController.GetByID(khachhangID);
                    if (khachhang != null)
                    {
                        if (!string.IsNullOrEmpty(khachhang.Currency))
                        {
                            if (khachhang.Currency.ToFloat(0) > 0)
                            {
                                khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                            }
                        }
                        //lblUsername.Text = khachhang.Username;
                        thongtindonhang += "-" + khachhang.ID;
                    }
                    hdfcopytext.Value = thongtindonhang;
                    lblThongtindonhang.Text = thongtindonhang;
                    if (o.CurrentCNYVN.ToFloat(0) > 0)
                    {
                        currency1 = Convert.ToDouble(o.CurrentCNYVN);
                        double currencyconfig = currency1;
                        hdfcurrent.Value = currencyconfig.ToString();
                        currency = currencyconfig;
                    }
                    if (khachhangCurrency > 0)
                    {
                        currency1 = khachhangCurrency;
                        double currencyconfig = khachhangCurrency;
                        hdfcurrent.Value = khachhangCurrency.ToString();
                        currency = khachhangCurrency;
                    }


                    hdfOrderID.Value = o.ID.ToString();
                    if (o.OrderType == 3)
                    {
                        pnbaogia.Visible = true;
                    }
                    else
                    {
                        pnbaogia.Visible = false;
                    }
                    chkIsCheckPrice.Checked = Convert.ToBoolean(o.IsCheckNotiPrice);

                    ViewState["ID"] = id;
                    ltrPrint.Text += "<a class=\"btn btn border-btn\" target=\"_blank\" href='/manager/PrintStamp.aspx?id=" + id + "'>In Tem</a>";

                    ViewState["MOID"] = id;
                    #region Lịch sử thanh toán
                    var PayorderHistory = PayOrderHistoryController.GetAllByMainOrderID(o.ID);
                    if (PayorderHistory.Count > 0)
                    {
                        rptPayment.DataSource = PayorderHistory;
                        rptPayment.DataBind();
                    }
                    else
                    {
                        ltrpa.Text = "<tr>Chưa có lịch sử thanh toán nào.</tr>";
                    }
                    #endregion


                    if (obj_user != null)
                    {
                        int RoleID = Convert.ToInt32(obj_user.RoleID);
                        //if (RoleID == 0)
                        //    ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" />";
                        //else if (RoleID == 1)
                        //    ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/user-icon.png\" width=\"100%\" />";
                        //else
                        //    ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" />";
                        #region CheckRole
                        if (RoleID == 7)
                        {
                            ltrBtnUpdate.Text = "<a href=\"javascript:;\" class=\"btn primary-btn\" onclick=\"UpdateOrder()\">CẬP NHẬT</a>";
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            ddlStatus.Items.Add(new ListItem("Chờ mua hàng", "2"));
                            ddlStatus.Items.Add(new ListItem("Chờ shop TQ phát hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = false;
                            pCNShipFeeNDT.Visible = false;
                            //pBuyNDT.Visible = false;
                            pWeightNDT.Visible = false;
                            pCheckNDT.Visible = false;
                            pPackedNDT.Visible = false;
                            pDeposit.Enabled = false;
                            pCNShipFee.Enabled = false;
                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pCheck.Enabled = false;
                            pWeight.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = true;
                            ltr_OrderFee_UserInfo.Visible = false;
                            ltr_AddressReceive.Visible = false;
                            btnUpdate.Visible = true;
                            btnThanhtoan.Visible = true;
                            if (o.Status >= 5)
                            {
                                rTotalPriceRealCYN.Enabled = false;
                                rTotalPriceReal.Enabled = false;
                            }
                            //pShipHomeNDT.Visible = true;
                            //pnadminmanager.Visible = true;
                            ddlWarehouseFrom.Enabled = true;
                            ddlReceivePlace.Enabled = true;
                            ddlShippingType.Enabled = true;
                            pAmountDeposit.Enabled = false;
                        }
                        else if (RoleID == 3)
                        {
                            ddlStatus.Items.Add(new ListItem("Chờ mua hàng", "2"));
                            //ddlStatus.Items.Add(new ListItem("Chờ shop TQ phát hàng", "5"));
                            pCNShipFeeNDT.Visible = true;
                            pCNShipFee.Enabled = true;
                            if (o.Status > 2)
                            {
                                ddlStatus.Enabled = false;
                                ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                                ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                                //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                                ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                                ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                                pCNShipFeeNDT.Visible = false;
                                pCNShipFee.Enabled = false;
                            }
                            //ltraddordercode.Text = "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addordercode()\">Thêm mã vận đơn</a></div>";
                            //pDepositNDT.Visible = false;

                            //pBuyNDT.Visible = false;
                            pWeightNDT.Visible = false;
                            pCheckNDT.Visible = false;
                            pPackedNDT.Visible = false;
                            pDeposit.Enabled = false;
                            if (o.Status >= 5)
                            {
                                rTotalPriceRealCYN.Enabled = false;
                                rTotalPriceReal.Enabled = false;
                            }

                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pCheck.Enabled = false;
                            pWeight.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = false;
                            ltr_OrderFee_UserInfo.Visible = true;
                            ltr_AddressReceive.Visible = true;
                            ddlWarehouseFrom.Enabled = true;
                            ltrBtnUpdate.Text = "<a href=\"javascript:;\" class=\"btn primary-btn\" onclick=\"UpdateOrder()\">CẬP NHẬT</a>";
                        }
                        else if (RoleID == 4)
                        {


                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;

                            pCheck.Enabled = false;
                            pCheckNDT.Enabled = false;

                            pDeposit.Enabled = false;
                            pBuyNDT.Enabled = false;

                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;

                            pShipHome.Enabled = false;
                            ltr_OrderFee_UserInfo.Visible = false;
                            ltr_AddressReceive.Visible = false;

                            txtOrderWeight.Enabled = true;
                            rTotalPriceRealCYN.Visible = false;
                            rTotalPriceReal.Visible = false;
                            pHHCYN.Visible = false;
                            pHHVND.Visible = false;
                            //pShipHomeNDT.Visible = false;
                        }
                        else if (RoleID == 5)
                        {

                            pCNShipFeeNDT.Enabled = false;

                            rTotalPriceRealCYN.Visible = false;
                            rTotalPriceReal.Visible = false;
                            pHHCYN.Visible = false;
                            pHHVND.Visible = false;

                            pCheckNDT.Enabled = false;
                            pCheck.Enabled = false;

                            pDeposit.Enabled = false;

                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;

                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;

                            pShipHome.Enabled = false;

                            ltr_OrderFee_UserInfo.Visible = true;
                            ltr_AddressReceive.Visible = false;
                            txtOrderWeight.Enabled = true;

                            //ltraddordercode.Text = "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addordercode()\" >Thêm mã vận đơn</a></div>";
                            //pShipHomeNDT.Visible = false;
                        }
                        else if (RoleID == 0)
                        {
                            pnadminmanager.Visible = true;
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
                            ddlStatus.Items.Add(new ListItem("Chờ mua hàng", "2"));
                            ddlStatus.Items.Add(new ListItem("Chờ shop TQ phát hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));

                            //ltraddordercode.Text = "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addordercode()\" >Thêm mã vận đơn</a></div>";
                            txtOrderWeight.Enabled = true;
                            btnThanhtoan.Visible = true;
                            pAmountDeposit.Enabled = true;
                            pDeposit.Enabled = true;
                            chkCheck.Enabled = true;
                            chkPackage.Enabled = true;
                            chkShiphome.Enabled = true;
                            ddlWarehouseFrom.Enabled = true;
                            ddlReceivePlace.Enabled = true;
                            ddlShippingType.Enabled = true;
                            ltrBtnUpdate.Text = "<a href=\"javascript:;\" class=\"btn primary-btn\" onclick=\"UpdateOrder()\">CẬP NHẬT</a>";
                        }
                        else if (RoleID == 2)
                        {
                            pnadminmanager.Visible = true;
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
                            ddlStatus.Items.Add(new ListItem("Chờ mua hàng", "2"));
                            ddlStatus.Items.Add(new ListItem("Chờ shop TQ phát hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = true;
                            pDeposit.Enabled = false;
                            pCNShipFeeNDT.Enabled = true;
                            pCNShipFee.Enabled = true;
                            if (o.Status >= 5)
                            {
                                rTotalPriceRealCYN.Enabled = false;
                                rTotalPriceReal.Enabled = false;
                            }
                            pBuy.Enabled = false;
                            pWeightNDT.Enabled = false;
                            pWeight.Enabled = false;
                            pCheckNDT.Enabled = false;
                            pCheck.Enabled = false;
                            pPackedNDT.Enabled = true;
                            pPacked.Enabled = true;
                            pShipHome.Enabled = true;
                            btnUpdate.Visible = true;
                            txtComment.Visible = true;
                            //ddlTypeComment.Visible = true;
                            btnSend.Visible = true;
                            ltrBtnUpdate.Text = "<a href=\"javascript:;\" class=\"btn primary-btn\" onclick=\"UpdateOrder()\">CẬP NHẬT</a>";
                        }
                        else if (RoleID == 6)
                        {
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            //ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
                            ddlStatus.Items.Add(new ListItem("Chờ mua hàng", "2"));
                            ddlStatus.Items.Add(new ListItem("Chờ shop TQ phát hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = false;
                            pDeposit.Enabled = false;
                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;
                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pWeightNDT.Enabled = false;
                            pWeight.Enabled = false;

                            pCheckNDT.Enabled = true;

                            chkCheck.Enabled = true;
                            pCheck.Enabled = true;

                            pPackedNDT.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = false;

                            txtComment.Visible = true;
                            //ddlTypeComment.Visible = true;
                            btnSend.Visible = true;
                            btnUpdate.Visible = false;

                            rTotalPriceRealCYN.Visible = false;
                            rTotalPriceReal.Visible = false;
                            pHHCYN.Visible = false;
                            pHHVND.Visible = false;
                        }
                        else if (RoleID == 8)
                        {

                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = true;
                            pDeposit.Enabled = false;
                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;
                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pWeightNDT.Enabled = false;
                            pWeight.Enabled = false;
                            pCheckNDT.Enabled = false;
                            pCheck.Enabled = false;
                            pPackedNDT.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = false;
                            btnUpdate.Visible = true;
                            if (o.Status >= 5)
                            {
                                rTotalPriceRealCYN.Enabled = false;
                                rTotalPriceReal.Enabled = false;
                            }

                            txtComment.Visible = true;
                            //ddlTypeComment.Visible = true;
                            btnSend.Visible = true;
                            txtOrderWeight.Enabled = false;
                        }
                        int countOc = 1;
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode2) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight2))
                        {
                            hdfoc2.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc2.Value = "0";
                        }
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode3) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight3))
                        {
                            hdfoc3.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc3.Value = "0";
                        }
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode4) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight4))
                        {
                            hdfoc4.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc4.Value = "0";
                        }
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode5) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight5))
                        {
                            hdfoc5.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc5.Value = "0";
                        }
                        hdforderamount.Value = countOc.ToString();
                        #endregion
                        #region Lấy thông tin nhân viên
                        ddlSaler.SelectedValue = o.SalerID.ToString();
                        ddlDatHang.SelectedValue = o.DathangID.ToString();
                        ddlKhoTQ.SelectedValue = o.KhoTQID.ToString();
                        ddlKhoVN.SelectedValue = o.KhoVNID.ToString();
                        #endregion
                        #region Lấy thông tin người đặt
                        var usercreate = AccountController.GetByID(Convert.ToInt32(o.UID));
                        double ckFeeBuyPro = 0;
                        double ckFeeWeight = 0;
                        if (usercreate != null)
                        {
                            ckFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeBuyPro.ToString());
                            ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());

                            lblCKFeebuypro.Text = ckFeeBuyPro.ToString();
                            lblCKFeeWeight.Text = ckFeeWeight.ToString();

                            hdfFeeBuyProDiscount.Value = ckFeeBuyPro.ToString();
                            hdfFeeWeightDiscount.Value = ckFeeWeight.ToString();
                        }
                        else
                        {
                            lblCKFeebuypro.Text = "0";
                            lblCKFeeWeight.Text = "0";
                            hdfFeeBuyProDiscount.Value = "0";
                            hdfFeeWeightDiscount.Value = "0";
                        }

                        if (RoleID != 8)
                        {
                            if (RoleID == 3)
                            {
                                ltr_OrderFee_UserInfo.Text += "<dt>Mã KH:</dt>";
                                ltr_OrderFee_UserInfo.Text += "<dd>" + o.UID + "</dd>";
                                string username = "";
                                var u = AccountController.GetByID(Convert.ToInt32(o.UID));
                                if (u != null)
                                    username = u.Username;
                                ltr_OrderFee_UserInfo.Text += "<dt>Username:</dt>";
                                ltr_OrderFee_UserInfo.Text += "<dd>" + username + "</dd>";
                                ltr_OrderFee_UserInfo.Text += "<dt>Ghi chú:</dt>";
                                ltr_OrderFee_UserInfo.Text += "<dd class=\"bgnote\">" + o.Note + "</dd>";
                                ltr_OrderFee_UserInfo.Text += "<dt></dt>";
                                ltr_OrderFee_UserInfo.Text += "<dd>Tài khoản không đủ quyền xem thông tin này</dd>";
                            }
                            else
                            {
                                var ui = AccountInfoController.GetByUserID(Convert.ToInt32(o.UID));
                                if (ui != null)
                                {
                                    string phone = ui.MobilePhonePrefix + ui.MobilePhone;

                                    ltr_OrderFee_UserInfo.Text += "<dt>Mã KH:</dt>";
                                    ltr_OrderFee_UserInfo.Text += "<dd>" + o.UID + "</dd>";
                                    string username = "";
                                    var u = AccountController.GetByID(Convert.ToInt32(o.UID));
                                    if (u != null)
                                        username = u.Username;
                                    ltr_OrderFee_UserInfo.Text += "<dt>Username:</dt>";
                                    ltr_OrderFee_UserInfo.Text += "<dd>" + username + "</dd>";
                                    ltr_OrderFee_UserInfo.Text += "<dt>Ghi chú:</dt>";
                                    ltr_OrderFee_UserInfo.Text += "<dd class=\"bgnote\">" + o.Note + "</dd>";
                                }
                            }
                        }

                        ltr_OrderCode.Text += "<div class=\"order-panel\">";
                        ltr_OrderCode.Text += " <div class=\"title\">Mã đơn hàng</div>";
                        ltr_OrderCode.Text += "     <div class=\"cont\">";
                        ltr_OrderCode.Text += "         <p><strong>" + o.ID + "</strong></p>";
                        ltr_OrderCode.Text += "     </div>";
                        ltr_OrderCode.Text += "</div>";

                        ltr_OrderID.Text += "<strong>" + o.ID + "</strong>";



                        var kd = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        var dathang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        var khotq = AccountController.GetByID(Convert.ToInt32(o.KhoTQID));
                        var khovn = AccountController.GetByID(Convert.ToInt32(o.KhoVNID));
                        if (kd != null)
                        {
                            ltr_OrderFee_UserInfo2.Text += "    <dt style=\"width: 200px;\">Nhân viên kinh doanh:</dt>";
                            ltr_OrderFee_UserInfo2.Text += "    <dd><strong>" + kd.Username + "</strong></dd>";

                        }
                        if (dathang != null)
                        {
                            ltr_OrderFee_UserInfo2.Text += "    <dt style=\"width: 200px;\">Nhân viên đặt hàng:</dt>";
                            ltr_OrderFee_UserInfo2.Text += "    <dd><strong>" + dathang.Username + "</strong></dd>";
                        }
                        if (khotq != null)
                        {
                            ltr_OrderFee_UserInfo2.Text += "    <dt style=\"width: 200px;\">Nhân viên kho TQ:</dt>";
                            ltr_OrderFee_UserInfo2.Text += "    <dd><strong>" + khotq.Username + "</strong></dd>";
                        }
                        if (khovn != null)
                        {
                            ltr_OrderFee_UserInfo2.Text += "    <dt style=\"width: 200px;\">Nhân viên kho đích:</dt>";
                            ltr_OrderFee_UserInfo2.Text += "    <dd><strong>" + khovn.Username + "</strong></dd>";
                        }
                        #endregion
                        #region Lấy thông tin đơn hàng
                        string mainordercode = o.MainOrderCode;
                        string site = o.Site;
                        if (!string.IsNullOrEmpty(mainordercode))
                        {
                            if (site == "1688")
                            {
                                ltrXemMa.Text = "<a target=\"_blank\" href=\"https://trade.1688.com/order/new_step_order_detail.htm?orderId=" + mainordercode + "\" class=\"btn primary-btn\" style=\"float:left;\">Xem</a>";
                            }
                            else
                            {
                                ltrXemMa.Text = "<a target=\"_blank\" href=\"https://trade.taobao.com/trade/detail/trade_order_detail.htm?biz_order_id=" + mainordercode + "\" class=\"btn primary-btn\" style=\"float:left;\">Xem</a>";
                            }
                        }
                        else
                        {
                            ltrXemMa.Text = "<a href=\"javascript:;\" class=\"btn primary-btn\" style=\"float:left;\">Xem</a>";
                        }
                        txtMainOrderCode.Text = o.MainOrderCode;
                        chkCheck.Checked = o.IsCheckProduct.ToString().ToBool();
                        chkPackage.Checked = o.IsPacked.ToString().ToBool();
                        chkShiphome.Checked = o.IsFastDelivery.ToString().ToBool();
                        //chkIsFast.Checked = o.IsFast.ToString().ToBool();
                        if (o.IsShopSendGoods != null)
                        {
                            chkIsShopSendGood.Checked = Convert.ToBoolean(o.IsShopSendGoods);
                        }
                        if (o.IsBuying != null)
                        {
                            chkIsBuying.Checked = Convert.ToBoolean(o.IsBuying);
                        }
                        if (o.IsPaying != null)
                        {
                            chkIsPaying.Checked = Convert.ToBoolean(o.IsPaying);
                        }

                        double feeeinwarehouse = 0;
                        if (o.FeeInWareHouse != null)
                            feeeinwarehouse = Convert.ToDouble(o.FeeInWareHouse);
                        rFeeWarehouse.Value = feeeinwarehouse;

                        if (o.IsGiaohang != null)
                            chkIsGiaohang.Checked = o.IsGiaohang.ToString().ToBool();
                        if (!string.IsNullOrEmpty(o.AmountDeposit))
                        {
                            double amountdeposit = Math.Floor(Convert.ToDouble(o.AmountDeposit.ToString()));
                            pAmountDeposit.Value = amountdeposit;
                            //lblAmountDeposit.Text = string.Format("{0:N0}", amountdeposit) + " ";
                        }
                        else
                        {
                            pAmountDeposit.Value = 0;
                            //lblAmountDeposit.Text = "0 ";
                        }
                        if (!string.IsNullOrEmpty(o.TotalPriceReal))
                            rTotalPriceReal.Value = Convert.ToDouble(o.TotalPriceReal);
                        else
                            rTotalPriceReal.Value = 0;

                        if (!string.IsNullOrEmpty(o.TotalPriceRealCYN))
                            rTotalPriceRealCYN.Value = Convert.ToDouble(o.TotalPriceRealCYN);
                        else
                            rTotalPriceRealCYN.Value = 0;

                        ddlStatus.SelectedValue = o.Status.ToString();
                        if (!string.IsNullOrEmpty(o.Deposit))
                            pDeposit.Value = Math.Floor(Convert.ToDouble(o.Deposit));

                        double fscn = 0;
                        if (!string.IsNullOrEmpty(o.FeeShipCN))
                        {
                            fscn = Math.Floor(Convert.ToDouble(o.FeeShipCN));
                            pCNShipFeeNDT.Value = fscn / currency1;
                            pCNShipFee.Value = fscn;
                            lblShipTQ.Text = string.Format("{0:N0}", Convert.ToDouble(o.FeeShipCN));
                        }
                        double realprice = 0;
                        if (!string.IsNullOrEmpty(o.TotalPriceReal))
                            realprice = Convert.ToDouble(o.TotalPriceReal);

                        double tot = Convert.ToDouble(o.PriceVND) + fscn - realprice;
                        double totCYN = tot / currency1;
                        pHHCYN.Value = totCYN;
                        pHHVND.Value = tot;

                        if (!string.IsNullOrEmpty(o.FeeBuyPro))
                        {
                            pBuy.Value = Convert.ToDouble(o.FeeBuyPro);
                            lblFeeBuyProduct.Text = string.Format("{0:N0}", Convert.ToDouble(o.FeeBuyPro));
                        }
                        if (!string.IsNullOrEmpty(o.FeeWeight))
                        {
                            pWeight.Value = Convert.ToDouble(o.FeeWeight);
                        }
                        else
                        {
                            pWeight.Value = 0;
                        }
                        if (!string.IsNullOrEmpty(o.TQVNWeight))
                        {
                            pWeightNDT.Value = Convert.ToDouble(o.TQVNWeight);
                        }
                        else
                        {
                            pWeightNDT.Value = 0;
                        }


                        double checkproductprice = Convert.ToDouble(o.IsCheckProductPrice);
                        pCheck.Value = checkproductprice;
                        pCheckNDT.Value = checkproductprice / currency;


                        double packagedprice = Convert.ToDouble(o.IsPackedPrice);
                        pPacked.Value = packagedprice;
                        pPackedNDT.Value = packagedprice / currency;
                        pShipHome.Value = Convert.ToDouble(o.IsFastDeliveryPrice);
                        lblMoneyNotFee.Text = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND));
                        lblTotalMoney.Text = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + "đ (¥" + string.Format("{0:#.##}", Convert.ToDouble(o.PriceVND) / currency) + ")";
                        double totalFee = Convert.ToDouble(o.IsCheckProductPrice) + Convert.ToDouble(o.IsPackedPrice) +
                           Convert.ToDouble(o.IsFastDeliveryPrice) + Convert.ToDouble(o.IsFastPrice);
                        lblAllFee.Text = string.Format("{0:N0}", totalFee);
                        lblFeeTQVN.Text = string.Format("{0:N0}", Convert.ToDouble(o.FeeWeight));
                        double odweight = 0;
                        if (!string.IsNullOrEmpty(o.OrderWeight))
                            odweight = Convert.ToDouble(o.OrderWeight);
                        txtOrderWeight.Value = odweight;
                        string orderweightfeedc = o.FeeWeightCK;

                        ddlWarehouseFrom.SelectedValue = o.FromPlace.ToString();
                        hdfFromPlace.Value = o.FromPlace.ToString();

                        ddlReceivePlace.SelectedValue = o.ReceivePlace;
                        hdfReceivePlace.Value = o.ReceivePlace;

                        ddlShippingType.SelectedValue = o.ShippingType.ToString();
                        hdfShippingType.Value = o.ShippingType.ToString();

                        if (string.IsNullOrEmpty(orderweightfeedc))
                        {
                            lblCKFeeweightPrice.Text = "0";
                            hdfFeeweightPriceDiscount.Value = "0";
                        }
                        else
                        {
                            lblCKFeeweightPrice.Text = orderweightfeedc;
                            hdfFeeweightPriceDiscount.Value = orderweightfeedc;
                        }
                        //lblCKFeeweightPrice.Text = string.Format("{0:N0}", Convert.ToDouble(orderweightfeedc));

                        double alltotal = totalFee + Convert.ToDouble(o.PriceVND) + Convert.ToDouble(o.FeeShipCN) + Convert.ToDouble(o.FeeBuyPro) + Convert.ToDouble(o.FeeShipCNToVN)
                            + Convert.ToDouble(o.FeeWeight) + feeeinwarehouse;

                        lblAllTotal.Text = string.Format("{0:N0}", alltotal);
                        lblDeposit.Text = string.Format("{0:N0}", Convert.ToDouble(o.Deposit));
                        lblLeftPay.Text = string.Format("{0:N0}", alltotal - Convert.ToDouble(o.Deposit));
                        ltrlblAllTotal1.Text = string.Format("{0:N0}", alltotal);
                        lblDeposit1.Text = string.Format("{0:N0}", Convert.ToDouble(o.Deposit));
                        lblLeftPay1.Text = string.Format("{0:N0}", alltotal - Convert.ToDouble(o.Deposit));
                        string statreturn = PJUtils.IntToRequestAdminReturnBG(Convert.ToInt32(o.Status));
                        ltrStatus1.Text += "<div class=\"inner inline-lb-info " + statreturn + "\">";
                        ltrStatus1.Text += "<div class=\"lb\">Trạng thái</div>";
                        if (o.Status == 5)
                        {
                            if (o.IsShopSendGoods != null)
                            {
                                if (o.IsShopSendGoods == true)
                                {
                                    ltrStatus1.Text += "<div class=\"info\">Shop đã phát hàng</div>";
                                }
                                else
                                {
                                    ltrStatus1.Text += "<div class=\"info\">" + PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status)) + "</div>";
                                }
                            }
                            else
                            {
                                ltrStatus1.Text += "<div class=\"info\">" + PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status)) + "</div>";
                            }
                        }
                        else if (o.Status == 2)
                        {
                            if (o.IsBuying != null)
                            {
                                if (o.IsBuying == true && o.IsPaying == false)
                                {
                                    ltrStatus1.Text += "<div class=\"info\">Đang mua hàng</div>";
                                }
                                else
                                {
                                    ltrStatus1.Text += "<div class=\"info\">" + PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status)) + "</div>";
                                }
                            }
                            else if (o.IsPaying != null)
                            {
                                if (o.IsPaying == true && o.IsBuying == false)
                                {
                                    ltrStatus1.Text += "<div class=\"info\">Shop đã phát hàng</div>";
                                }
                                else
                                {
                                    ltrStatus1.Text += "<div class=\"info\">" + PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status)) + "</div>";
                                }
                            }
                            else if (o.IsPaying != null)
                            {
                                if (o.IsPaying == true && o.IsBuying == true)
                                {
                                    ltrStatus1.Text += "<div class=\"info\">Shop đã phát hàng</div>";
                                }
                                else
                                {
                                    ltrStatus1.Text += "<div class=\"info\">" + PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status)) + "</div>";
                                }
                            }
                            else
                            {
                                ltrStatus1.Text += "<div class=\"info\">" + PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status)) + "</div>";
                            }
                        }

                        else
                        {
                            ltrStatus1.Text += "<div class=\"info\">" + PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status)) + "</div>";
                        }

                        ltrStatus1.Text += "</div>";
                        #endregion
                        #region Lấy thông tin nhận hàng
                        if (RoleID == 3)
                        {
                            ltr_AddressReceive.Text = "Tài khoản không đủ quyền xem thông tin này";
                        }
                        else
                        {
                            ltr_AddressReceive.Text += "<dt>Tên:</dt>";
                            ltr_AddressReceive.Text += "<dd>" + o.FullName + "</dd>";
                            ltr_AddressReceive.Text += "<dt>Địa chỉ:</dt>";
                            ltr_AddressReceive.Text += "<dd>" + o.Address + "</dd>";
                            ltr_AddressReceive.Text += "<dt>Email:</dt>";
                            ltr_AddressReceive.Text += "<dd><a href=\"" + o.Email + "\">" + o.Email + "</a></dd>";
                            ltr_AddressReceive.Text += "<dt>Số dt:</dt>";
                            ltr_AddressReceive.Text += "<dd><a href=\"tel:+" + o.Phone + "\">" + o.Phone + "</a></dd>";

                        }
                        #endregion

                        #region Lấy sản phẩm
                        double totalQuantityProduct = 0;
                        List<tbl_Order> lo = new List<tbl_Order>();
                        //Lấy link chiết khấu

                        var orderck = OrderController.GetByMainOrderID(o.ID);
                        if (orderck.Count > 0)
                        {
                            foreach (var item in orderck)
                            {
                                if (item.isGetLink != true)
                                {
                                    //update link chỗ này
                                    string link = item.link_origin;
                                    if (item.site.ToLower() == "1688")
                                    {
                                        var url = "http://chietkhauali.com/agency/search?token=qeYomnCcLiKSQPw&web=2&num_iid=" + item.item_id;
                                        var datacrawl = PJUtils.ConnectApi(url);
                                        if (datacrawl != null)
                                        {
                                            Root obj = JsonConvert.DeserializeObject<Root>(datacrawl);
                                            if (obj.error == 0)
                                            {
                                                if (obj.data != null)
                                                {
                                                    if (obj.data.coupon_long_url != null)
                                                        link = obj.data.coupon_long_url;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        var url = "http://chietkhauali.com/agency/search?token=qeYomnCcLiKSQPw&web=1&num_iid=" + item.item_id;
                                        var datacrawl = PJUtils.ConnectApi(url);
                                        if (datacrawl != null)
                                        {
                                            Root obj = JsonConvert.DeserializeObject<Root>(datacrawl);
                                            if (obj.error == 0)
                                            {
                                                if (obj.data != null)
                                                {
                                                    if (obj.data.coupon_long_url != null)
                                                        link = obj.data.coupon_long_url;
                                                }
                                            }
                                        }
                                    }
                                    OrderController.UpdateLink(item.ID, link, true);
                                }

                            }
                        }


                        lo = OrderController.GetByMainOrderID(o.ID);
                        if (lo.Count > 0)
                        {
                            //rpt.DataSource = lo;
                            //rpt.DataBind();
                            int stt = 1;
                            foreach (var item in lo)
                            {
                                totalQuantityProduct += item.quantity.ToInt(0);
                                double currentcyt = Convert.ToDouble(item.CurrentCNYVN);
                                double price = 0;
                                double pricepromotion = Convert.ToDouble(item.price_promotion);
                                double priceorigin = Convert.ToDouble(item.price_origin);
                                if (pricepromotion > 0)
                                {
                                    if (priceorigin > pricepromotion)
                                    {
                                        price = pricepromotion;
                                    }
                                    else
                                    {
                                        price = priceorigin;
                                    }
                                }
                                else
                                {
                                    price = priceorigin;
                                }
                                double vndprice = price * currentcyt;
                                ltrProducts.Text += "<tr class=\"info-in\">";
                                //ltrProducts.Text += "<td rowspan=\"2\">" + item.ID + "</td>";
                                ltrProducts.Text += "<td rowspan=\"2\">" + stt + "</td>";
                                ltrProducts.Text += "<td>";
                                ltrProducts.Text += "<div class=\"product-infobox\">";
                                ltrProducts.Text += "<a href=\"" + item.link_origin + "\" target=\"_blank\" class=\"img\">";
                                ltrProducts.Text += "<img src=\"" + item.image_origin + "\" alt=\"\"></a>";
                                ltrProducts.Text += "<div class=\"info\">";


                                //ltrProducts.Text += "<a href=\"" + item.link_origin + "\" target=\"_blank\">" + item.title_origin + "</a>";
                                if (obj_user.RoleID != 0)
                                {
                                    ltrProducts.Text += "<a href=\"" + item.link + "\" target=\"_blank\">" + item.title_origin + "</a>";
                                }
                                else
                                {
                                    ltrProducts.Text += "<a href=\"" + item.link_origin + "\" target=\"_blank\">" + item.title_origin + "</a>";
                                }
                                ltrProducts.Text += "</div>";
                                ltrProducts.Text += "</div>";
                                ltrProducts.Text += "</td>";
                                ltrProducts.Text += "<td>" + item.quantity + "</td>";
                                ltrProducts.Text += "<td>";
                                ltrProducts.Text += "<p>" + string.Format("{0:N0}", vndprice) + " vnđ</p>";
                                ltrProducts.Text += "<p>¥ " + string.Format("{0:0.##}", price) + "</p>";
                                ltrProducts.Text += "</td>";
                                if (string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                {
                                    ltrProducts.Text += "<td>Còn hàng</td>";
                                }
                                else
                                {
                                    if (item.ProductStatus == 2)
                                        ltrProducts.Text += "<td style=\"background:red;color:#fff;padding:5px 10px;\">Hết hàng</td>";
                                    else
                                        ltrProducts.Text += "<td>Còn hàng</td>";
                                }
                                ltrProducts.Text += "<td rowspan=\"2\" class=\"hl-txt\">";
                                ltrProducts.Text += "<a href=\"/manager/ProductEdit.aspx?id=" + item.ID + "\" class=\"left edit-btn\">Sửa</a>";
                                ltrProducts.Text += "<a href=\"javascript:;\" class=\"toggle-detail-row right\"><i class=\"fa fa-caret-down\"></i></a>";
                                ltrProducts.Text += "</td>";
                                ltrProducts.Text += "</tr>";
                                ltrProducts.Text += "<tr class=\"detail-row\" style=\"display:block\">";
                                ltrProducts.Text += "<td colspan=\"4\">";
                                ltrProducts.Text += "<dl class=\"dl\">";
                                ltrProducts.Text += "<dt>Thuộc tính</dt>";
                                ltrProducts.Text += "<dd>" + item.property + "</dd>";
                                ltrProducts.Text += "<dt>Ghi chú:</dt>";
                                ltrProducts.Text += "<dd class=\"bgnote\">" + item.brand + "</dd>";
                                ltrProducts.Text += "</dl>";
                                ltrProducts.Text += "</td>";
                                ltrProducts.Text += "</tr>";

                                //Print
                                ltrProductPrint.Text += "<tr>";
                                //ltrProductPrint.Text += "<td class=\"pro\">" + item.ID + "</td>";
                                ltrProductPrint.Text += "<td class=\"pro\">" + stt + "</td>";
                                ltrProductPrint.Text += "<td class=\"pro\">";
                                ltrProductPrint.Text += "   <div class=\"thumb-product\">";
                                ltrProductPrint.Text += "       <div class=\"pd-img\"><img src=\"" + item.image_origin + "\" alt=\"\"></div>";
                                ltrProductPrint.Text += "       <div class=\"info\">" + item.title_origin + "</div>";
                                ltrProductPrint.Text += "   </div>";
                                ltrProductPrint.Text += "</td>";
                                ltrProductPrint.Text += "<td class=\"pro\">" + item.property + "</td>";
                                ltrProductPrint.Text += "<td class=\"qty\">" + item.quantity + "</td>";

                                ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", vndprice) + " vnđ</p></td>";
                                ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">¥" + string.Format("{0:0.##}", price) + "</p></td>";

                                ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">" + item.brand + "</p></td>";
                                if (string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                {
                                    ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                }
                                else
                                {
                                    if (item.ProductStatus == 2)
                                        ltrProductPrint.Text += "<td class=\"price\"><p class=\"bg-red\">Hết hàng</p></td>";
                                    else
                                        ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                }
                                ltrProducts.Text += "</tr>";
                                stt++;
                            }
                        }
                        lblTotalQuantity.Text = string.Format("{0:N0}", totalQuantityProduct);
                        #endregion
                        #region Lấy bình luận nội bộ
                        var cs = OrderCommentController.GetByOrderIDAndType(o.ID, 2, 1);
                        if (cs != null)
                        {
                            if (cs.Count > 0)
                            {
                                foreach (var item in cs)
                                {
                                    string fullname = "";
                                    string imguser = "";
                                    int role = 0;
                                    int user_postID = 0;
                                    var user = AccountController.GetByID(Convert.ToInt32(item.CreatedBy));
                                    if (user != null)
                                    {
                                        user_postID = user.ID;
                                        role = Convert.ToInt32(user.RoleID);
                                        var userinfo = AccountInfoController.GetByUserID(user.ID);
                                        if (userinfo != null)
                                        {
                                            fullname = userinfo.FirstName + " " + userinfo.LastName;
                                            imguser = userinfo.IMGUser;
                                        }
                                    }
                                    if (uid == user_postID)
                                        ltrInComment.Text += "<div class=\"mess-item mymess\">";
                                    else
                                        ltrInComment.Text += "<div class=\"mess-item \">";
                                    ltrInComment.Text += "<div class=\"img\"><img src=\"" + imguser + "\"/></div>";
                                    ltrInComment.Text += "<div class=\"cont\">";
                                    ltrInComment.Text += "<p class=\"\"><strong class=\"username\">" + fullname + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</p>";
                                    ltrInComment.Text += "<p>" + item.Comment + "</p>";
                                    if (!string.IsNullOrEmpty(item.Link))
                                        ltrInComment.Text += "<p><a href=\"" + item.Link + "\" target=\"_blank\"><img src=\"" + item.Link + "\" /></a></p>";
                                    ltrInComment.Text += "</div>";
                                    ltrInComment.Text += "</div>";

                                }
                            }
                            else
                            {
                                ltrInComment.Text += "<span class=\"no-comment-staff\">Hiện chưa có đánh giá nào.</span>";
                            }
                        }
                        else
                        {
                            ltrInComment.Text += "<span class=\"no-comment-staff\">Hiện chưa có đánh giá nào.</span>";
                        }
                        #endregion
                        #region Lấy bình luận ngoài
                        var cs1 = OrderCommentController.GetByOrderIDAndType(o.ID, 1, 1);
                        if (cs1 != null)
                        {
                            if (cs1.Count > 0)
                            {
                                foreach (var item in cs1)
                                {
                                    string fullname = "";
                                    string imguser = "";
                                    int role = 0;
                                    int user_postID = 0;
                                    var user = AccountController.GetByID(Convert.ToInt32(item.CreatedBy));
                                    if (user != null)
                                    {
                                        user_postID = user.ID;
                                        role = Convert.ToInt32(user.RoleID);
                                        var userinfo = AccountInfoController.GetByUserID(user.ID);
                                        if (userinfo != null)
                                        {
                                            fullname = userinfo.FirstName + " " + userinfo.LastName;
                                            imguser = userinfo.IMGUser;
                                        }
                                    }
                                    if (uid == user_postID)
                                        ltrOutComment.Text += "<div class=\"mess-item mymess\">";
                                    else
                                        ltrOutComment.Text += "<div class=\"mess-item \">";
                                    ltrOutComment.Text += "<div class=\"img\"><img src=\"" + imguser + "\"/></div>";
                                    ltrOutComment.Text += "<div class=\"cont\">";
                                    ltrOutComment.Text += "<p class=\"\"><strong class=\"username\">" + fullname + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</p>";
                                    ltrOutComment.Text += "<p>" + item.Comment + "</p>";
                                    if (!string.IsNullOrEmpty(item.Link))
                                        ltrOutComment.Text += "<p><a href=\"" + item.Link + "\" target=\"_blank\"><img src=\"" + item.Link + "\" /></a></p>";
                                    ltrOutComment.Text += "</div>";
                                    ltrOutComment.Text += "</div>";

                                }
                            }
                            else
                            {
                                ltrOutComment.Text += "<span class=\"no-comment-cust\">Hiện chưa có đánh giá nào.</span>";
                            }
                        }
                        else
                        {
                            ltrOutComment.Text += "<span class=\"no-comment-cust\">Hiện chưa có đánh giá nào.</span>";
                        }
                        #endregion
                        #region Lấy danh sách bao nhỏ
                        StringBuilder spsList = new StringBuilder();
                        var smallpackages = SmallPackageController.GetByMainOrderID(id);
                        if (smallpackages.Count > 0)
                        {
                            foreach (var s in smallpackages)
                            {
                                var mo = MainOrderController.GetAllByID(Convert.ToInt32(s.MainOrderID));
                                int status = Convert.ToInt32(s.Status);
                                double weigthQD = 0;

                                double pDai = Convert.ToDouble(s.Length);
                                double pRong = Convert.ToDouble(s.Width);
                                double pCao = Convert.ToDouble(s.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    weigthQD = (pDai * pRong * pCao) / 6000;
                                }
                                ltrMavandon.Text += "<tr>";
                                ltrMavandon.Text += "   <td>" + s.OrderTransactionCode + "</td>";
                                ltrMavandon.Text += "   <td>" + s.Weight + "</td>";
                                double cantinhtien = weigthQD;
                                if (Convert.ToDouble(s.Weight) > weigthQD)
                                {
                                    cantinhtien = Convert.ToDouble(s.Weight);
                                }
                                if (status == 1)
                                    ltrMavandon.Text += "<td>Mới đặt - chưa về kho TQ</td>";
                                else if (status == 2)
                                    ltrMavandon.Text += "<td>Đã về kho TQ</td>";
                                else if (status == 3)
                                    ltrMavandon.Text += "<td>Đã về kho đích</td>";
                                else if (status == 4)
                                    ltrMavandon.Text += "<td>Đã giao khách hàng</td>";
                                else if (status == 0)
                                    ltrMavandon.Text += "<td>Đã hủy</td>";
                                ltrMavandon.Text += "</tr>";


                                spsList.Append("<div class=\"ordercode order-versionnew\" data-packageID=\"" + s.ID + "\">");
                                spsList.Append("    <div class=\"item-element\" style=\"width:15%;\">");
                                spsList.Append("        <span>Mã Vận đơn:</span>");
                                spsList.Append("        <input class=\"transactionCode form-control\" value=\"" + s.OrderTransactionCode + "\" type=\"text\" placeholder=\"Mã vận đơn\" />");
                                spsList.Append("    </div>");
                                spsList.Append("    <div class=\"item-element\" style=\"width:10%;\" >");
                                spsList.Append("        <span>Cân thực:</span>");
                                spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + s.Weight + "\" type=\"text\" placeholder=\"Cân nặng\" onkeyup=\"returnWeightFee()\" />");
                                spsList.Append("    </div>");
                                spsList.Append("    <div class=\"item-element\" style=\"width:12%;\">");
                                spsList.Append("        <span>Kích thước:</span>");
                                spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + pDai + " x " + pRong + " x " + pCao + "\"  onkeyup=\"returnWeightFee()\" />");
                                spsList.Append("    </div>");

                                spsList.Append("    <div class=\"item-element\" style=\"width:12%;\">");
                                spsList.Append("        <span>Cân quy đổi:</span>");
                                spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + Math.Round(weigthQD, 2) + "\"  onkeyup=\"returnWeightFee()\" />");
                                spsList.Append("    </div>");

                                spsList.Append("    <div class=\"item-element\" style=\"width:12%;\">");
                                spsList.Append("        <span>Cân tính tiền:</span>");
                                spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + Math.Round(cantinhtien, 2) + "\"  onkeyup=\"returnWeightFee()\" />");
                                spsList.Append("    </div>");


                                spsList.Append("    <div class=\"item-element\" style=\"width:16%;\">");
                                spsList.Append("        <span>Trạng thái:</span>");
                                spsList.Append("        <select class=\"transactionCodeStatus form-control\">");

                                if (status == 1)
                                    spsList.Append("            <option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>");
                                else
                                    spsList.Append("            <option value=\"1\">Mới đặt - chưa về kho TQ</option>");
                                if (status == 2)
                                    spsList.Append("            <option value=\"2\" selected>Đã về kho TQ</option>");
                                else
                                    spsList.Append("            <option value=\"2\">Đã về kho TQ</option>");
                                if (status == 3)
                                    spsList.Append("            <option value=\"3\" selected>Đã về kho đích</option>");
                                else
                                    spsList.Append("            <option value=\"3\">Đã về kho đích</option>");
                                if (status == 4)
                                    spsList.Append("            <option value=\"4\" selected>Đã giao khách hàng</option>");
                                else
                                    spsList.Append("            <option value=\"4\">Đã giao khách hàng</option>");
                                if (status == 0)
                                    spsList.Append("            <option value=\"0\" selected>Đã hủy</option>");
                                else
                                    spsList.Append("            <option value=\"0\">Đã hủy</option>");
                                spsList.Append("        </select>");
                                spsList.Append("    </div>");
                                spsList.Append("    <div class=\"item-element\" style=\"width:12%;\">");
                                spsList.Append("        <span>Ghi chú:</span>");
                                spsList.Append("        <input class=\"transactionDescription form-control\" value=\"" + s.Description + "\" type=\"text\" placeholder=\"Ghi chú\"/>");
                                spsList.Append("    </div>");





                                spsList.Append("    <div class=\"item-element\" style=\"width:11%;\"><a href=\"javascript:;\" class=\"btn primary-btn\" style=\"margin-top:19px;\" onclick=\"deleteOrderCode($(this))\">Xóa</a></div>");
                                spsList.Append("</div>");
                            }
                            ltrCodeList.Text = spsList.ToString();
                        }
                        #endregion
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            #region History 

            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    var historyorder = HistoryOrderChangeController.GetByMainOrderID(o.ID);
                    if (historyorder.Count > 0)
                    {
                        List<historyCustom> hc = new List<historyCustom>();
                        foreach (var item in historyorder)
                        {


                            string username = item.Username;
                            string rolename = "admin";
                            var acc = AccountController.GetByUsername(username);
                            if (acc != null)
                            {
                                int role = Convert.ToInt32(acc.RoleID);

                                var r = RoleController.GetByID(role);
                                if (r != null)
                                {
                                    rolename = r.RoleDescription;
                                }
                            }
                            historyCustom h = new historyCustom();
                            h.ID = item.ID;
                            h.Username = username;
                            h.RoleName = rolename;
                            h.Date = string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate);
                            h.Content = item.HistoryContent;
                            hc.Add(h);
                        }
                        gr.DataSource = hc;
                    }
                }
            }

            #endregion
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        #region Button
        protected void btnSend1_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                //var id = Convert.ToInt32(Request.QueryString["id"]);
                var id = Convert.ToInt32(ViewState["ID"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {
                        int type = 1;
                        if (type > 0)
                        {
                            string kq = OrderCommentController.Insert(id, txtComment1.Text, true, type, DateTime.Now, uid, 1);
                            if (type == 1)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                    1, currentDate, obj_user.Username, false);
                                try
                                {
                                    PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                        "Thông báo tại Nam Trung.",
                                        "Đã có đánh giá mới cho đơn hàng #" + id
                                        + " của bạn. CLick vào để xem", "");
                                }
                                catch { }
                            }
                            if (Convert.ToInt32(kq) > 0)
                            {
                                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                hubContext.Clients.All.addNewMessageToPage("", "");
                                PJUtils.ShowMsg("Gửi đánh giá thành công.", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Vui lòng chọn khu vực", "e", false, Page);
                        }
                    }
                }
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                //var id = Convert.ToInt32(Request.QueryString["id"]);
                var id = Convert.ToInt32(ViewState["ID"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {
                        int type = 2;
                        if (type > 0)
                        {
                            string kq = OrderCommentController.Insert(id, txtComment.Text, true, type, DateTime.Now, uid, 1);
                            if (type == 1)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                    1, currentDate, obj_user.Username, false);
                                try
                                {
                                    PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                        "Thông báo tại NamTrung.",
                                        "Đã có đánh giá mới cho đơn hàng #" + id
                                        + " của bạn. CLick vào để xem", "");
                                }
                                catch { }
                            }
                            if (Convert.ToInt32(kq) > 0)
                            {
                                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                hubContext.Clients.All.addNewMessageToPage("", "");
                                PJUtils.ShowMsg("Gửi đánh giá thành công.", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Vui lòng chọn khu vực", "e", false, Page);
                        }
                    }
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            string dateDK = ExpectedDate.SelectedDate.ToString();
            if (obj_user != null)
            {
                int uid = obj_user.ID;

                int RoleID = obj_user.RoleID.ToString().ToInt();
                //var id = Convert.ToInt32(Request.QueryString["id"]);
                var id = Convert.ToInt32(ViewState["ID"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {


                        if (rTotalPriceReal.Value.ToString() == "" || rTotalPriceReal.Value.ToString() == "NaN")
                        {
                            PJUtils.ShowMessageBoxSwAlert("Vui lòng kiểm tra lại tổng tiền mua thật", "e", false, Page);
                        }
                        else if (rTotalPriceRealCYN.Value.ToString() == "" || rTotalPriceRealCYN.Value.ToString() == "NaN")
                        {
                            PJUtils.ShowMessageBoxSwAlert("Vui lòng kiểm tra lại tổng tiền mua thật", "e", false, Page);
                        }
                        else
                        {
                            int uidmuahang = Convert.ToInt32(o.UID);
                            string usermuahang = "";

                            var accmuahan = AccountController.GetByID(uidmuahang);
                            if (accmuahan != null)
                            {
                                usermuahang = accmuahan.Username;
                            }
                            string CurrentOrderTransactionCode = o.OrderTransactionCode;
                            string CurrentOrderTransactionCode2 = o.OrderTransactionCode2;
                            string CurrentOrderTransactionCode3 = o.OrderTransactionCode3;
                            string CurrentOrderTransactionCode4 = o.OrderTransactionCode4;
                            string CurrentOrderTransactionCode5 = o.OrderTransactionCode5;

                            string CurrentOrderTransactionCodeWeight = o.OrderTransactionCodeWeight;
                            string CurrentOrderTransactionCodeWeight2 = o.OrderTransactionCodeWeight2;
                            string CurrentOrderTransactionCodeWeight3 = o.OrderTransactionCodeWeight3;
                            string CurrentOrderTransactionCodeWeight4 = o.OrderTransactionCodeWeight4;
                            string CurrentOrderTransactionCodeWeight5 = o.OrderTransactionCodeWeight5;

                            string CurrentOrderWeight = o.OrderWeight;

                            #region cập nhật và tạo mới smallpackage
                            string tcl = hdfCodeTransactionList.Value;
                            if (!string.IsNullOrEmpty(tcl))
                            {
                                string[] list = tcl.Split('|');
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    string[] item = list[i].Split(',');
                                    int ID = item[0].ToInt(0);
                                    string code = item[1];
                                    string weight = item[2];
                                    int smallpackage_status = item[3].ToInt(1);
                                    string description = item[4];
                                    if (ID > 0)
                                    {
                                        var smp = SmallPackageController.GetByID(ID);
                                        if (smp != null)
                                        {
                                            int bigpackageID = Convert.ToInt32(smp.BigPackageID);
                                            SmallPackageController.Update(ID, accmuahan.ID, usermuahang, bigpackageID, code, smp.ProductType, Convert.ToDouble(smp.FeeShip),
                                                Convert.ToDouble(weight), Convert.ToDouble(smp.Volume), smallpackage_status,
                                                description, currentDate, username);
                                            var bigpack = BigPackageController.GetByID(bigpackageID);
                                            if (bigpack != null)
                                            {
                                                int TotalPackageWaiting = SmallPackageController.GetCountByBigPackageIDStatus(bigpackageID, 1, 2);
                                                if (TotalPackageWaiting == 0)
                                                {
                                                    BigPackageController.UpdateStatus(bigpackageID, 2, currentDate, username);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SmallPackageController.InsertWithMainOrderIDUIDUsername(id, accmuahan.ID, usermuahang, 0, code, "", 0, Convert.ToDouble(weight), 0,
                                                smallpackage_status, description, currentDate, username);
                                        }
                                    }
                                    else
                                    {
                                        SmallPackageController.InsertWithMainOrderIDUIDUsername(id, accmuahan.ID, usermuahang, 0, code, "", 0, Convert.ToDouble(weight), 0,
                                            smallpackage_status, description, currentDate, username);
                                    }
                                }
                            }


                            #endregion
                            #region Lấy ra text của trạng thái đơn hàng
                            string orderstatus = "";
                            int currentOrderStatus = Convert.ToInt32(o.Status);
                            switch (currentOrderStatus)
                            {
                                case 0:
                                    orderstatus = "Chờ đặt cọc";
                                    break;
                                case 1:
                                    orderstatus = "Hủy đơn hàng";
                                    break;
                                case 2:
                                    orderstatus = "Chờ mua hàng";
                                    break;
                                case 5:
                                    orderstatus = "Chờ shop TQ phát hàng";
                                    break;
                                case 6:
                                    orderstatus = "Đang về kho đích";
                                    break;
                                case 7:
                                    orderstatus = "Đã nhận hàng tại kho đích";
                                    break;
                                case 8:
                                    orderstatus = "Chờ thanh toán";
                                    break;
                                case 9:
                                    orderstatus = "Khách đã thanh toán";
                                    break;
                                case 10:
                                    orderstatus = "Đã hoàn thành";
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            #region Cập nhật nhân viên KhoTQ và nhân viên KhoVN
                            if (RoleID == 4)
                            {
                                if (o.KhoTQID == uid || o.KhoTQID == 0)
                                {
                                    MainOrderController.UpdateStaff(o.ID, o.SalerID.ToString().ToInt(0), o.DathangID.ToString().ToInt(0), uid, o.KhoVNID.ToString().ToInt(0));
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý", "e", true, Page);
                                }
                            }
                            else if (RoleID == 5)
                            {
                                if (o.KhoVNID == uid || o.KhoTQID == 0)
                                {
                                    MainOrderController.UpdateStaff(o.ID, o.SalerID.ToString().ToInt(0), o.DathangID.ToString().ToInt(0), o.KhoTQID.ToString().ToInt(0), uid);
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý", "e", true, Page);
                                }
                            }
                            #endregion
                            #region cập nhật thông tin của đơn hàng
                            double feeeinwarehouse = 0;
                            int status = ddlStatus.SelectedValue.ToString().ToInt();
                            if (status == 1)
                            {
                                double Deposit = 0;
                                if (!string.IsNullOrEmpty(o.Deposit))
                                    Deposit = Convert.ToDouble(o.Deposit);
                                if (Deposit > 0)
                                {
                                    var user_order = AccountController.GetByID(o.UID.ToString().ToInt());
                                    if (user_order != null)
                                    {
                                        double wallet = 0;
                                        if (user_order.Wallet != null)
                                            wallet = Convert.ToDouble(user_order.Wallet.ToString());
                                        wallet = wallet + Deposit;
                                        HistoryPayWalletController.Insert(user_order.ID, user_order.Username, o.ID, Deposit,
                                            "Đơn hàng: " + o.ID + " bị hủy và hoàn tiền cọc cho khách.", wallet, 2, 2, currentDate, obj_user.Username);
                                        //HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        //    " đã đổi trạng thái của đơn hàng: " + o.ID + " từ " + orderstatus + " sang " + ddlStatus.SelectedItem + "", 0, currentDate);
                                        AccountController.updateWallet(user_order.ID, wallet, currentDate, obj_user.Username);
                                    }
                                }
                            }
                            else
                            {
                                double OCurrent_deposit = 0;
                                if (!string.IsNullOrEmpty(o.Deposit))
                                    OCurrent_deposit = Convert.ToDouble(o.Deposit);

                                double OCurrent_FeeShipCN = 0;
                                if (!string.IsNullOrEmpty(o.FeeShipCN))
                                    OCurrent_FeeShipCN = Convert.ToDouble(o.FeeShipCN);

                                double OCurrent_FeeBuyPro = 0;
                                if (!string.IsNullOrEmpty(o.FeeBuyPro))
                                    OCurrent_FeeBuyPro = Convert.ToDouble(o.FeeBuyPro);

                                double OCurrent_FeeWeight = 0;
                                if (!string.IsNullOrEmpty(o.FeeWeight))
                                    OCurrent_FeeWeight = Convert.ToDouble(o.FeeWeight);

                                double OCurrent_IsCheckProductPrice = 0;
                                if (!string.IsNullOrEmpty(o.IsCheckProductPrice))
                                    OCurrent_IsCheckProductPrice = Convert.ToDouble(o.IsCheckProductPrice);

                                double OCurrent_IsPackedPrice = 0;
                                if (!string.IsNullOrEmpty(o.IsPackedPrice))
                                    OCurrent_IsPackedPrice = Convert.ToDouble(o.IsPackedPrice);

                                double OCurrent_IsFastDeliveryPrice = 0;
                                if (!string.IsNullOrEmpty(o.IsFastDeliveryPrice))
                                    OCurrent_IsFastDeliveryPrice = Convert.ToDouble(o.IsFastDeliveryPrice);
                                double OCurrent_TotalPriceReal = 0;
                                if (!string.IsNullOrEmpty(o.TotalPriceReal))
                                    OCurrent_TotalPriceReal = Convert.ToDouble(o.TotalPriceReal);

                                double OCurrent_TotalPriceRealCYN = 0;
                                if (!string.IsNullOrEmpty(o.TotalPriceRealCYN))
                                    OCurrent_TotalPriceRealCYN = Convert.ToDouble(o.TotalPriceRealCYN);

                                //double OCurrent_FeeShipCNToVN = Convert.ToDouble(o.FeeShipCNToVN);



                                double Deposit = Convert.ToDouble(pDeposit.Value);
                                double FeeShipCN = Math.Floor(Convert.ToDouble(pCNShipFee.Value));
                                double FeeBuyPro = Convert.ToDouble(pBuy.Value);
                                double FeeWeight = Convert.ToDouble(pWeight.Value);
                                double TotalPriceReal = Convert.ToDouble(rTotalPriceReal.Value);
                                double TotalPriceRealCYN = Convert.ToDouble(rTotalPriceRealCYN.Value);

                                if (o.FeeInWareHouse != null)
                                    feeeinwarehouse = Convert.ToDouble(o.FeeInWareHouse);
                                //double FeeShipCNToVN = Convert.ToDouble(pWeight.Value);

                                double IsCheckProductPrice = 0;
                                if (!string.IsNullOrEmpty(pCheck.Value.ToString()))
                                    IsCheckProductPrice = Convert.ToDouble(pCheck.Value.ToString());

                                double IsPackedPrice = 0;
                                if (!string.IsNullOrEmpty(pPacked.Value.ToString()))
                                    IsPackedPrice = Convert.ToDouble(pPacked.Value.ToString());

                                double IsFastDeliveryPrice = 0;
                                if (!string.IsNullOrEmpty(pShipHome.Value.ToString()))
                                    IsFastDeliveryPrice = Convert.ToDouble(pShipHome.Value.ToString());

                                #region Ghi lịch sử chỉnh sửa các loại giá
                                if (OCurrent_deposit != Deposit)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền đặt cọc của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_deposit) + ", sang: "
                                            + string.Format("{0:N0}", Deposit) + "", 1, currentDate);
                                }
                                if (OCurrent_FeeShipCN != FeeShipCN)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền phí ship Trung Quốc của đơn hàng ID là: " + o.ID + ", từ " + string.Format("{0:N0}", OCurrent_FeeShipCN) + " sang "
                                            + string.Format("{0:N0}", FeeShipCN) + "", 2, currentDate);
                                }
                                if (OCurrent_FeeBuyPro < FeeBuyPro || OCurrent_FeeBuyPro > FeeBuyPro)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền phí mua sản phẩm của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_FeeBuyPro) + ", sang: "
                                            + string.Format("{0:N0}", FeeBuyPro) + "", 3, currentDate);
                                }
                                if (OCurrent_TotalPriceReal < TotalPriceReal || OCurrent_TotalPriceReal > TotalPriceReal)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền phí mua thật của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_TotalPriceReal) + ", sang: "
                                            + string.Format("{0:N0}", TotalPriceReal) + "", 3, currentDate);
                                }
                                if (OCurrent_FeeWeight != FeeWeight)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền phí TQ-VN của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_FeeWeight) + ", sang: "
                                            + string.Format("{0:N0}", FeeWeight) + "", 4, currentDate);
                                }
                                if (OCurrent_IsCheckProductPrice != IsCheckProductPrice)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền phí kiểm tra sản phẩm của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_IsCheckProductPrice) + ", sang: "
                                            + string.Format("{0:N0}", IsCheckProductPrice) + "", 5, currentDate);
                                }
                                if (OCurrent_IsPackedPrice != IsPackedPrice)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền phí đóng gỗ của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_IsPackedPrice) + ", sang: "
                                            + string.Format("{0:N0}", IsPackedPrice) + "", 6, currentDate);
                                }
                                if (OCurrent_IsFastDeliveryPrice != IsFastDeliveryPrice)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi tiền phí ship giao hàng tận nhà của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_IsFastDeliveryPrice) + ", sang: "
                                            + string.Format("{0:N0}", IsFastDeliveryPrice) + "", 7, currentDate);
                                }

                                #endregion
                                double isfastprice = 0;
                                if (!string.IsNullOrEmpty(o.IsFastPrice))
                                    isfastprice = Convert.ToDouble(o.IsFastPrice);

                                double pricenvd = 0;
                                if (!string.IsNullOrEmpty(o.PriceVND))
                                    pricenvd = Convert.ToDouble(o.PriceVND);

                                FeeShipCN = Math.Round(FeeShipCN, 0);
                                FeeBuyPro = Math.Round(FeeBuyPro, 0);
                                if (FeeBuyPro < 10000)
                                {
                                    FeeBuyPro = 10000;
                                }
                                FeeWeight = Math.Round(FeeWeight, 0);
                                IsCheckProductPrice = Math.Round(IsCheckProductPrice, 0);
                                IsPackedPrice = Math.Round(IsPackedPrice, 0);
                                IsFastDeliveryPrice = Math.Round(IsFastDeliveryPrice, 0);
                                isfastprice = Math.Round(isfastprice, 0);
                                pricenvd = Math.Round(pricenvd, 0);
                                Deposit = Math.Round(Deposit, 0);

                                double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                     + IsFastDeliveryPrice + isfastprice + pricenvd;

                                TotalPriceVND = Math.Round(TotalPriceVND, 0);



                                MainOrderController.UpdateFee(o.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(), IsCheckProductPrice.ToString(),
                                    IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());

                                if (status == 2)
                                {
                                    if (o.DepostiDate == null)
                                    {
                                        MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                    }
                                }
                                else if (status == 3)
                                {
                                    if (o.BuyingDate == null)
                                    {
                                        MainOrderController.UpdateBuyingDate(o.ID, currentDate);
                                    }
                                }
                                else if (status == 5)
                                {
                                    if (o.BuyProductDate == null)
                                    {
                                        MainOrderController.UpdateBuyProductDate(o.ID, Convert.ToInt32(o.UID), currentDate);
                                    }
                                }
                                else if (status == 6)
                                {
                                    if (o.TQWarehouseDate == null)
                                    {
                                        MainOrderController.UpdateTQWarehouseDate(o.ID, Convert.ToInt32(o.UID), currentDate);
                                    }
                                }
                                else if (status == 9)
                                {
                                    if (o.PayAllDate == null)
                                    {
                                        MainOrderController.UpdatePayAllDate(o.ID, Convert.ToInt32(o.UID), currentDate);
                                    }
                                }
                                else if (status == 10)
                                {
                                    if (o.CompleteDate == null)
                                    {
                                        MainOrderController.UpdateCompleteDate(o.ID, Convert.ToInt32(o.UID), currentDate);
                                    }
                                }

                            }
                            int currentstt = Convert.ToInt32(o.Status);
                            if (currentstt != 5)
                            {
                                if (status == 5)
                                {
                                    MainOrderController.UpdateBuyProductDate(o.ID, Convert.ToInt32(o.UID), currentDate);
                                }
                            }

                            if (currentstt < 3 || currentstt > 7)
                            {
                                if (status != currentstt)
                                {


                                    OrderCommentController.Insert(id, "Đã có cập nhật mới cho đơn hàng #" + id + " của bạn.", true, 1, DateTime.Now, uid, 1);
                                    try
                                    {

                                        PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                                    "Thông báo tại Nam Trung", "Đã có cập nhật trạng thái cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                    }
                                    catch { }
                                }
                            }
                            else if (currentstt > 2 && currentstt < 8)
                            {
                                if (status < 3 || status > 7)
                                {


                                    OrderCommentController.Insert(id, "Đã có cập nhật mới cho đơn hàng #" + id + " của bạn.", true, 1, DateTime.Now, uid, 1);

                                    try
                                    {

                                        PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                                    "Thông báo tại Nam Trung",
                                                    "Đã có cập nhật trạng thái cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                    }
                                    catch { }
                                }
                            }
                            #region Ghi lịch sử update status của đơn hàng
                            if (status != currentstt)
                            {
                                if (chkIsBuying.Checked == true && status == 2)
                                {
                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + "Đang mua hàng" + "", 0, currentDate);
                                }
                                else if (chkIsPaying.Checked == true && status == 2)
                                {
                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + "Đã thanh toán cho shop" + "", 0, currentDate);

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + "Chờ shop TQ phát hàng" + "", 0, currentDate);
                                }
                                else
                                {
                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + ddlStatus.SelectedItem + "", 0, currentDate);
                                }

                            }
                            else
                            {
                                if (chkIsBuying.Checked == true && status == 2)
                                {
                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + "Đang mua hàng" + "", 0, currentDate);
                                }
                                else if (chkIsPaying.Checked == true && status == 2)
                                {
                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + "Đã thanh toán cho shop" + "", 0, currentDate);
                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + "Chờ shop TQ phát hàng" + "", 0, currentDate);
                                }
                            }
                            #endregion
                            //}
                            string OrderWeight = txtOrderWeight.Value.ToString();
                            if (string.IsNullOrEmpty(CurrentOrderWeight))
                            {
                                if (CurrentOrderWeight != OrderWeight)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                               " đã đổi cân nặng của đơn hàng ID là: " + o.ID + ", là: " + OrderWeight + "", 8, currentDate);
                                }
                            }
                            else
                            {
                                if (CurrentOrderWeight != OrderWeight)
                                {

                                    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                               " đã đổi cân nặng của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderWeight + ", sang: " + OrderWeight + "",
                                               9, currentDate);
                                }
                            }
                            if (status == 5)
                            {
                                var setNoti = SendNotiEmailController.GetByID(7);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiUser == true)
                                    {
                                        NotificationsController.Inser(accmuahan.ID,
                                              accmuahan.Username, o.ID,
                                              "Đơn hàng " + o.ID + " đã được mua hàng.", 1,
                                              currentDate, obj_user.Username, false);
                                    }

                                    if (setNoti.IsSendEmailUser == true)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", accmuahan.Email,
                                                "Thông báo tại Nam Trung.", "Đơn hàng " + o.ID + " đã được mua hàng.", "");
                                        }
                                        catch { }
                                    }
                                }
                            }


                            string CurrentReceivePlace = o.ReceivePlace;
                            string ReceivePlace = ddlReceivePlace.SelectedValue;

                            if (CurrentReceivePlace != ReceivePlace)
                            {
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi nơi nhận hàng của đơn hàng ID là: " + o.ID + ", từ: " + CurrentReceivePlace + ", sang: " + ReceivePlace + "",
                                           8, currentDate);
                            }

                            string CurrentAmountDeposit = o.AmountDeposit.Trim();
                            string AmountDeposit = pAmountDeposit.Value.ToString().Trim();

                            if (CurrentAmountDeposit != AmountDeposit)
                            {
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi số tiền phải cọc của đơn hàng ID là: " + o.ID + ", từ: " + CurrentAmountDeposit + ", sang: " + AmountDeposit + "",
                                           8, currentDate);
                            }

                            bool Currentcheckpro = o.IsCheckProduct.ToString().ToBool();
                            bool checkpro = chkCheck.Checked;
                            if (Currentcheckpro != checkpro)
                            {
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi dịch vụ kiểm tra đơn hàng của đơn hàng ID là: " + o.ID + ", từ: " + Currentcheckpro + ", sang: " + checkpro + "",
                                           8, currentDate);
                            }
                            bool CurrentPackage = o.IsPacked.ToString().ToBool();
                            bool Package = chkPackage.Checked;
                            if (CurrentPackage != Package)
                            {
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi dịch vụ đóng gỗ của đơn hàng ID là: " + o.ID + ", từ: " + CurrentPackage + ", sang: " + Package + "",
                                           8, currentDate);
                            }
                            bool CurrentIsFastDelivery = o.IsFastDelivery.ToString().ToBool();
                            bool MoveIsFastDelivery = chkShiphome.Checked;
                            string TotalPriceReal1 = rTotalPriceReal.Value.ToString();
                            string TotalPriceRealCYN1 = rTotalPriceRealCYN.Value.ToString();

                            if (CurrentIsFastDelivery != MoveIsFastDelivery)
                            {
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi dịch vụ giao hàng tận nhà của đơn hàng ID là: " + o.ID + ", từ: " + CurrentIsFastDelivery + ", sang: " + MoveIsFastDelivery + "",
                                           8, currentDate);
                            }

                            MainOrderController.UpdateOrderWeight(o.ID, OrderWeight);
                            MainOrderController.UpdateCheckPro(o.ID, checkpro);
                            MainOrderController.UpdateIsPacked(o.ID, Package);
                            MainOrderController.UpdateIsFastDelivery(o.ID, MoveIsFastDelivery, chkIsShopSendGood.Checked, chkIsBuying.Checked, chkIsPaying.Checked);
                            MainOrderController.UpdateAmountDeposit(o.ID, AmountDeposit);
                            MainOrderController.UpdateFeeWarehouse(o.ID, feeeinwarehouse);
                            //MainOrderController.UpdateReceivePlace(o.ID, Convert.ToInt32(o.UID), ddlReceivePlace.SelectedValue.ToString());
                            MainOrderController.UpdateFeeWeightDC(o.ID, hdfFeeweightPriceDiscount.Value);
                            MainOrderController.UpdateStatusByID(o.ID, Convert.ToInt32(ddlStatus.SelectedValue));
                            MainOrderController.UpdateOrderWeightCK(o.ID, hdfFeeweightPriceDiscount.Value);
                            MainOrderController.UpdateTQVNWeight(o.ID, o.UID.ToString().ToInt(), pWeightNDT.Value.ToString());
                            MainOrderController.UpdateTotalPriceReal(o.ID, TotalPriceReal1.ToString(), TotalPriceRealCYN1.ToString());
                            MainOrderController.UpdateMainOrderCode(o.ID, o.UID.ToString().ToInt(), txtMainOrderCode.Text);
                            MainOrderController.UpdateFTS(o.ID, o.UID.ToString().ToInt(),
                                ddlWarehouseFrom.SelectedValue.ToInt(),
                                ddlReceivePlace.SelectedValue, ddlShippingType.SelectedValue.ToInt());
                            if (!string.IsNullOrEmpty(dateDK))
                            {
                                MainOrderController.UpdateExpectedDate(o.ID, Convert.ToDateTime(dateDK));
                            }

                            if (status == 5)
                            {
                                if (chkIsShopSendGood.Checked == true)
                                {
                                    if (o.ShopSendGoodsDate == null)
                                    {
                                        MainOrderController.UpdateShopSendGoodsDate(o.ID, currentDate);
                                    }
                                }
                            }
                            if (status == 2)
                            {
                                if (chkIsBuying.Checked == true)
                                {
                                    if (o.BuyingDate == null)
                                    {
                                        MainOrderController.UpdateBuyingDate(o.ID, currentDate);
                                    }
                                }
                            }
                            if (status != 1)
                            {
                                if (chkIsPaying.Checked == true)
                                {
                                    if (o.BuyProductDate == null)
                                    {
                                        MainOrderController.UpdateBuyProductDate(o.ID, currentDate);
                                    }
                                }
                            }
                            if (status == 2)
                            {
                                if (chkIsPaying.Checked == true)
                                {
                                    if (o.PayingDate == null)
                                    {
                                        MainOrderController.UpdatePayingDate(o.ID, currentDate);
                                    }
                                }
                            }
                            if (status != 1)
                            {
                                if (chkIsPaying.Checked == true)
                                {
                                    if (status < 6)
                                    {
                                        MainOrderController.UpdateStatus(o.ID, o.UID.ToString().ToInt(), 5);
                                    }
                                }
                            }
                            #endregion
                            #region Update User Level
                            if (status >= 9)
                            {
                                int cusID = o.UID.ToString().ToInt(0);
                                var cust = AccountController.GetByID(cusID);
                                if (cust != null)
                                {
                                    var cus_orders = MainOrderController.GetSuccessByCustomer(cust.ID);

                                    double totalpay = 0;
                                    if (cus_orders.Count > 0)
                                    {
                                        foreach (var item in cus_orders)
                                        {
                                            double ttpricenvd = 0;
                                            if (!string.IsNullOrEmpty(item.TotalPriceVND))
                                                ttpricenvd = Convert.ToDouble(item.TotalPriceVND);
                                            totalpay += ttpricenvd;
                                        }

                                        if (totalpay >= 100000000 && totalpay < 400000000)
                                        {
                                            if (cust.LevelID == 1)
                                            {
                                                AccountController.updateLevelID(cusID, 2, currentDate, cust.Username);
                                            }
                                        }
                                        else if (totalpay >= 400000000 && totalpay < 900000000)
                                        {
                                            if (cust.LevelID == 2)
                                            {
                                                AccountController.updateLevelID(cusID, 3, currentDate, cust.Username);
                                            }
                                        }
                                        else if (totalpay >= 900000000 && totalpay < 1600000000)
                                        {
                                            if (cust.LevelID == 3)
                                            {
                                                AccountController.updateLevelID(cusID, 4, currentDate, cust.Username);
                                            }
                                        }
                                        else if (totalpay >= 1600000000 && totalpay < 2500000000)
                                        {
                                            if (cust.LevelID == 4)
                                            {
                                                AccountController.updateLevelID(cusID, 5, currentDate, cust.Username);
                                            }
                                        }

                                    }
                                }
                            }
                            #endregion
                            #region Cập nhật thông tin nhân viên sale và đặt hàng
                            int SalerID = ddlSaler.SelectedValue.ToString().ToInt(0);
                            int DathangID = ddlDatHang.SelectedValue.ToString().ToInt(0);
                            int KhoTQID = ddlKhoTQ.SelectedValue.ToString().ToInt(0);
                            int khoVNID = ddlKhoVN.SelectedValue.ToString().ToInt(0);
                            var mo = MainOrderController.GetAllByID(id);
                            if (mo != null)
                            {
                                double feebp = Convert.ToDouble(mo.FeeBuyPro);
                                if (feebp < 10000)
                                {
                                    feebp = 10000;
                                }
                                DateTime CreatedDate = Convert.ToDateTime(mo.CreatedDate);
                                double salepercent = 0;
                                double salepercentaf3m = 0;
                                double dathangpercent = 0;
                                var config = ConfigurationController.GetByTop1();
                                if (config != null)
                                {
                                    salepercent = Convert.ToDouble(config.SalePercent);
                                    salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                                    dathangpercent = Convert.ToDouble(config.DathangPercent);
                                }
                                string salerName = "";
                                string dathangName = "";

                                int salerID_old = Convert.ToInt32(mo.SalerID);
                                int dathangID_old = Convert.ToInt32(mo.DathangID);

                                #region Saler
                                if (SalerID > 0)
                                {
                                    if (SalerID == salerID_old)
                                    {
                                        var staff = StaffIncomeController.GetByMainOrderIDUID(id, salerID_old);
                                        if (staff != null)
                                        {
                                            int rStaffID = staff.ID;
                                            int staffstatus = Convert.ToInt32(staff.Status);
                                            if (staffstatus == 1)
                                            {
                                                var sale = AccountController.GetByID(salerID_old);
                                                if (sale != null)
                                                {
                                                    salerName = sale.Username;
                                                    var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                                    int d = CreatedDate.Subtract(createdDate).Days;
                                                    if (d > 90)
                                                    {
                                                        salepercentaf3m = Convert.ToDouble(staff.PercentReceive);
                                                        double per = feebp * salepercentaf3m / 100;
                                                        StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercentaf3m.ToString(), 1,
                                                            per.ToString(), false, currentDate, username);
                                                    }
                                                    else
                                                    {
                                                        salepercent = Convert.ToDouble(staff.PercentReceive);
                                                        double per = feebp * salepercent / 100;
                                                        StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercent.ToString(), 1,
                                                            per.ToString(), false, currentDate, username);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var staff = StaffIncomeController.GetByMainOrderIDUID(id, salerID_old);
                                        if (staff != null)
                                        {
                                            StaffIncomeController.Delete(staff.ID);
                                        }
                                        var sale = AccountController.GetByID(SalerID);
                                        if (sale != null)
                                        {
                                            salerName = sale.Username;
                                            var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                            int d = CreatedDate.Subtract(createdDate).Days;
                                            if (d > 90)
                                            {
                                                double per = feebp * salepercentaf3m / 100;
                                                StaffIncomeController.Insert(id, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                                CreatedDate, currentDate, username);
                                            }
                                            else
                                            {
                                                double per = feebp * salepercent / 100;
                                                StaffIncomeController.Insert(id, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                                CreatedDate, currentDate, username);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region Đặt hàng
                                if (DathangID > 0)
                                {
                                    if (DathangID == dathangID_old)
                                    {
                                        var staff = StaffIncomeController.GetByMainOrderIDUID(id, dathangID_old);
                                        if (staff != null)
                                        {
                                            if (staff.Status == 1)
                                            {
                                                //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                                double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                                double totalRealPrice = 0;
                                                if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                                    totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                                if (totalRealPrice > 0)
                                                {
                                                    double totalpriceloi = totalPrice - totalRealPrice;

                                                    dathangpercent = Convert.ToDouble(staff.PercentReceive);
                                                    double income = totalpriceloi * dathangpercent / 100;
                                                    //double income = totalpriceloi;
                                                    StaffIncomeController.Update(staff.ID, totalRealPrice.ToString(), dathangpercent.ToString(), 1,
                                                                income.ToString(), false, currentDate, username);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var staff = StaffIncomeController.GetByMainOrderIDUID(id, dathangID_old);
                                        if (staff != null)
                                        {
                                            StaffIncomeController.Delete(staff.ID);
                                        }
                                        var dathang = AccountController.GetByID(DathangID);
                                        if (dathang != null)
                                        {
                                            dathangName = dathang.Username;
                                            //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                            double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                            double totalRealPrice = 0;
                                            if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                                totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                            if (totalRealPrice > 0)
                                            {
                                                double totalpriceloi = totalPrice - totalRealPrice;
                                                double income = totalpriceloi * dathangpercent / 100;
                                                //double income = totalpriceloi;

                                                StaffIncomeController.Insert(id, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                                    income.ToString(), false, CreatedDate, currentDate, username);
                                            }
                                            else
                                            {
                                                StaffIncomeController.Insert(id, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                                CreatedDate, currentDate, username);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }

                            MainOrderController.UpdateStaff(id, SalerID, DathangID, KhoTQID, khoVNID);
                            #endregion
                            MainOrderRequestShipController.UpdateMainOrderStatusByMainOrderID(id, status);
                            PJUtils.ShowMsg("Cập nhật thông tin thành công.", true, Page);
                        }
                    }
                }
            }
        }
        protected void btnStaffUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int SalerID = ddlSaler.SelectedValue.ToString().ToInt(0);
            int DathangID = ddlDatHang.SelectedValue.ToString().ToInt(0);
            int KhoTQID = ddlKhoTQ.SelectedValue.ToString().ToInt(0);
            int khoVNID = ddlKhoVN.SelectedValue.ToString().ToInt(0);
            int ID = ViewState["MOID"].ToString().ToInt();
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            var mo = MainOrderController.GetAllByID(ID);
            if (mo != null)
            {
                double feebp = Convert.ToDouble(mo.FeeBuyPro);
                if (feebp < 10000)
                {
                    feebp = 10000;
                }
                DateTime CreatedDate = Convert.ToDateTime(mo.CreatedDate);
                double salepercent = 0;
                double salepercentaf3m = 0;
                double dathangpercent = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    salepercent = Convert.ToDouble(config.SalePercent);
                    salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                    dathangpercent = Convert.ToDouble(config.DathangPercent);
                }
                string salerName = "";
                string dathangName = "";

                int salerID_old = Convert.ToInt32(mo.SalerID);
                int dathangID_old = Convert.ToInt32(mo.DathangID);

                #region Saler
                if (SalerID > 0)
                {
                    if (SalerID == salerID_old)
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, salerID_old);
                        if (staff != null)
                        {
                            int rStaffID = staff.ID;
                            int status = Convert.ToInt32(staff.Status);
                            if (status == 1)
                            {
                                var sale = AccountController.GetByID(salerID_old);
                                if (sale != null)
                                {
                                    salerName = sale.Username;
                                    var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                    int d = CreatedDate.Subtract(createdDate).Days;
                                    if (d > 90)
                                    {
                                        salepercentaf3m = Convert.ToDouble(staff.PercentReceive);
                                        double per = feebp * salepercentaf3m / 100;
                                        StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercentaf3m.ToString(), 1,
                                            per.ToString(), false, currentDate, username);
                                    }
                                    else
                                    {
                                        salepercent = Convert.ToDouble(staff.PercentReceive);
                                        double per = feebp * salepercent / 100;
                                        StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercent.ToString(), 1,
                                            per.ToString(), false, currentDate, username);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var sale = AccountController.GetByID(SalerID);
                            if (sale != null)
                            {
                                salerName = sale.Username;
                                var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                int d = CreatedDate.Subtract(createdDate).Days;
                                if (d > 90)
                                {
                                    double per = feebp * salepercentaf3m / 100;
                                    StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                    CreatedDate, currentDate, username);
                                }
                                else
                                {
                                    double per = feebp * salepercent / 100;
                                    StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                    CreatedDate, currentDate, username);
                                }
                            }
                        }
                    }
                    else
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, salerID_old);
                        if (staff != null)
                        {
                            StaffIncomeController.Delete(staff.ID);
                        }
                        var sale = AccountController.GetByID(SalerID);
                        if (sale != null)
                        {
                            salerName = sale.Username;
                            var createdDate = Convert.ToDateTime(sale.CreatedDate);
                            int d = CreatedDate.Subtract(createdDate).Days;
                            if (d > 90)
                            {
                                double per = feebp * salepercentaf3m / 100;
                                StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                CreatedDate, currentDate, username);
                            }
                            else
                            {
                                double per = feebp * salepercent / 100;
                                StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                CreatedDate, currentDate, username);
                            }
                        }
                    }
                }
                #endregion
                #region Đặt hàng
                if (DathangID > 0)
                {
                    if (DathangID == dathangID_old)
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, dathangID_old);
                        if (staff != null)
                        {
                            if (staff.Status == 1)
                            {
                                //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                double totalRealPrice = 0;
                                if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                    totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                if (totalRealPrice > 0)
                                {
                                    double totalpriceloi = totalPrice - totalRealPrice;

                                    dathangpercent = Convert.ToDouble(staff.PercentReceive);
                                    double income = totalpriceloi * dathangpercent / 100;
                                    //double income = totalpriceloi;
                                    StaffIncomeController.Update(staff.ID, totalRealPrice.ToString(), dathangpercent.ToString(), 1,
                                                income.ToString(), false, currentDate, username);
                                }

                            }
                        }
                        else
                        {
                            var dathang = AccountController.GetByID(DathangID);
                            if (dathang != null)
                            {
                                dathangName = dathang.Username;
                                //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                double totalRealPrice = 0;
                                if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                    totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                if (totalRealPrice > 0)
                                {
                                    double totalpriceloi = totalPrice - totalRealPrice;
                                    double income = totalpriceloi * dathangpercent / 100;
                                    //double income = totalpriceloi;
                                    StaffIncomeController.Insert(ID, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                        income.ToString(), false, CreatedDate, currentDate, username);
                                }
                                else
                                {
                                    StaffIncomeController.Insert(ID, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                    CreatedDate, currentDate, username);
                                }
                            }
                        }
                    }
                    else
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, dathangID_old);
                        if (staff != null)
                        {
                            StaffIncomeController.Delete(staff.ID);
                        }
                        var dathang = AccountController.GetByID(DathangID);
                        if (dathang != null)
                        {
                            dathangName = dathang.Username;
                            //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                            double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                            double totalRealPrice = 0;
                            if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                            if (totalRealPrice > 0)
                            {
                                double totalpriceloi = totalPrice - totalRealPrice;
                                double income = totalpriceloi * dathangpercent / 100;
                                //double income = totalpriceloi;

                                StaffIncomeController.Insert(ID, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                    income.ToString(), false, CreatedDate, currentDate, username);
                            }
                            else
                            {
                                StaffIncomeController.Insert(ID, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                CreatedDate, currentDate, username);
                            }
                        }
                    }
                }
                #endregion
            }

            MainOrderController.UpdateStaff(ID, SalerID, DathangID, KhoTQID, khoVNID);
            PJUtils.ShowMsg("Cập nhật nhân viên thành công.", true, Page);
        }
        protected void btnThanhtoan_Click(object sender, EventArgs e)
        {
            int id = ViewState["MOID"].ToString().ToInt(0);
            //var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    Response.Redirect("/manager/Pay-Order.aspx?id=" + id);
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);

            int id = ViewState["MOID"].ToString().ToInt(0);

            //lấy main order
            var o = MainOrderController.GetAllByID(id);
            if(o == null)
            {
                PJUtils.ShowMessageBoxSwAlert("Bạn không có quyền xuất file excel!", "e", false, Page);
            }

           var lo = OrderController.GetByMainOrderID(o.ID);

            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");

            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Mã đơn hàng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tiền hàng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ship nội địa</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí mua hàng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí tùy chọn</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Cân nặng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí TQ-VN</strong></th>");
            StrExport.Append(" <th style =\"mso-number-format:'\\@'\" ><strong>Tổng chi phí</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Đã thanh toán</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Cần thanh toán</strong></th>");

            StrExport.Append("  </tr>");

            var tongPhuphi = o.IsCheckProductPrice.ToDecimal(0) + o.IsPackedPrice.ToDecimal(0) + o.IsFastDeliveryPrice.ToDecimal(0);
            StrExport.Append("  <tr>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + o.ID + "</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", o.PriceVND.ToDecimal(0)) + " vnđ</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", o.FeeShipCN.ToDecimal(0)) + " vnđ</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", o.FeeBuyPro.ToDecimal(0)) + " vnđ</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", tongPhuphi)  + " vnđ</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + Math.Round( o.OrderWeight.ToFloat(0),2)  + " kg</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", o.FeeWeight.ToDecimal(0))  + " vnđ</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", o.TotalPriceVND.ToDecimal(0))  + " vnđ</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", o.Deposit.ToDecimal(0))  + " vnđ</td>");
            StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", o.TotalPriceVND.ToDecimal(0) -  o.Deposit.ToDecimal(0)) + " vnđ</td>");

            StrExport.Append("  </tr>");

            StrExport.Append("</table>");

            StrExport.Append("<span>DANH SÁCH SẢN PHẨM</span>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ID</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Link</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên SP</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Link ảnh</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Thuộc tính</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Số lượng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Đơn giá (¥)</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Đơn giá (VNĐ)</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Ghi chú</strong></th>");

            StrExport.Append("  </tr>");
            foreach (var item in lo)
            {
                double currentcyt = Convert.ToDouble(item.CurrentCNYVN);
                double price = 0;
                double pricepromotion = Convert.ToDouble(item.price_promotion);
                double priceorigin = Convert.ToDouble(item.price_origin);
                if (pricepromotion > 0)
                {
                    if (priceorigin > pricepromotion)
                    {
                        price = pricepromotion;
                    }
                    else
                    {
                        price = priceorigin;
                    }
                }
                else
                {
                    price = priceorigin;
                }
                double vndprice = price * currentcyt;

                StrExport.Append("  <tr>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.ID + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.link + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.title_origin + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.image_origin + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.property + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.quantity + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:0.##}", price)  + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", vndprice) + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.brand + "</td>");
               
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "chi-tiet-don-hang.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            //Response.Close();

        }

        #endregion
        #region Ajax
        [WebMethod]
        public static string DeleteSmallPackage(string IDPackage)
        {
            int ID = IDPackage.ToInt(0);
            var smallpackage = SmallPackageController.GetByID(ID);
            if (smallpackage != null)
            {
                string kq = SmallPackageController.Delete(ID);
                return "ok";
            }
            else
            {
                return "null";
            }
        }

        [WebMethod]
        public static string CountFeeWeight(int orderID, int receivePlace, int shippingTypeValue, double weight, int WarehouseFrom)
        {
            var order = MainOrderController.GetAllByID(orderID);
            if (order != null)
            {
                double pricePerKg = 0;
                int fromPlace = WarehouseFrom;
                var warehousefee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                    fromPlace, receivePlace, shippingTypeValue, false);
                if (warehousefee.Count > 0)
                {
                    foreach (var w in warehousefee)
                    {
                        if (w.WeightFrom < weight && weight <= w.WeightTo)
                        {
                            pricePerKg = Convert.ToDouble(w.Price);
                        }
                    }
                }
                int UID = Convert.ToInt32(order.UID);
                var usercreate = AccountController.GetByID(UID);
                double ckFeeWeight = 0;
                if (usercreate != null)
                {
                    ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
                }
                double currency = Convert.ToDouble(order.CurrentCNYVN);
                double totalPriceFeeweightVN = pricePerKg * weight;

                double discountVN = totalPriceFeeweightVN * ckFeeWeight / 100;
                double discountCYN = discountVN / currency;

                double feeoutVN = totalPriceFeeweightVN - discountVN;
                double feeoutCYN = feeoutVN / currency;

                FeeWeightObj f = new FeeWeightObj();
                f.FeeWeightCYN = Math.Floor(feeoutCYN);
                f.FeeWeightVND = Math.Floor(feeoutVN);
                f.DiscountFeeWeightCYN = Math.Floor(discountCYN);
                f.DiscountFeeWeightVN = Math.Floor(discountVN);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(f);
            }
            return "none";
        }


        [WebMethod]
        public static string sendcustomercomment(string comment, int id, string urlIMG, string real)
        {
            var listLink = urlIMG.Split('|').ToList();
            if (listLink.Count > 0)
            {
                listLink.RemoveAt(listLink.Count - 1);
            }
            var listComment = real.Split('|').ToList();
            if (listComment.Count > 0)
            {
                listComment.RemoveAt(listComment.Count - 1);
            }
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (obj_user != null)
            {
                string ret = "";
                var ai = AccountInfoController.GetByUserID(obj_user.ID);
                if (ai != null)
                {
                    ret += ai.FirstName + " " + ai.LastName + "," + ai.IMGUser + "," + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate);
                }
                int uid = obj_user.ID;
                //var id = Convert.ToInt32(Request.QueryString["id"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {
                        var setNoti = SendNotiEmailController.GetByID(12);
                        int type = 1;
                        if (type > 0)
                        {
                            for (int i = 0; i < listLink.Count; i++)
                            {
                                string kqq = OrderCommentController.InsertNew(id, listLink[i], listComment[i], true, type, DateTime.Now, uid, 1);
                            }
                            if (!string.IsNullOrEmpty(comment))
                            {
                                string kq = OrderCommentController.Insert(id, comment, true, type, DateTime.Now, uid, 1);
                                if (type == 1)
                                {
                                    if (setNoti != null)
                                    {
                                        if (setNoti.IsSentNotiUser == true)
                                        {
                                            if (o.OrderType == 1)
                                            {
                                                NotificationsController.Inser(Convert.ToInt32(o.UID),
                                       AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem",
                                       12, currentDate, obj_user.Username, true);
                                            }
                                            else
                                            {
                                                NotificationsController.Inser(Convert.ToInt32(o.UID),
                                       AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem",
                                       13, currentDate, obj_user.Username, true);
                                            }

                                        }

                                        if (setNoti.IsSendEmailUser == true)
                                        {
                                            try
                                            {

                                                PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz",
                                                    AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                                    "Thông báo tại Nam Trung.",
                                                    "Đã có đánh giá mới cho đơn hàng #" + id
                                                    + " của bạn. CLick vào để xem", "");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                ChatHub ch = new ChatHub();
                                ch.SendMessengerToCustomer(uid, id, comment, listLink, listComment);

                                CustomerComment dataout = new CustomerComment();
                                dataout.UID = uid;
                                dataout.OrderID = id;
                                StringBuilder showIMG = new StringBuilder();
                                if (!string.IsNullOrEmpty(comment))
                                {
                                    showIMG.Append("<div class=\"mess-item mymess\">");
                                    showIMG.Append("<div class=\"img\"><img src=\"" + ai.IMGUser + "\"/></div>");
                                    showIMG.Append("<div class=\"cont\">");
                                    showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + "</p>");
                                    if (string.IsNullOrEmpty(comment))
                                    {
                                        showIMG.Append("<p>" + comment + "</p>");
                                    }
                                    showIMG.Append("</div>");
                                    showIMG.Append("</div>");
                                }

                                for (int i = 0; i < listLink.Count; i++)
                                {
                                    showIMG.Append("<div class=\"mess-item mymess\">");
                                    showIMG.Append("<div class=\"img\"><img src=\"" + ai.IMGUser + "\"/></div>");
                                    showIMG.Append("<div class=\"cont\">");
                                    showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + "</p>");
                                    if (!string.IsNullOrEmpty(listLink[i]))
                                        showIMG.Append("<p><a href=\"" + listLink[i] + "\" target=\"_blank\"><img src=\"" + listLink[i] + "\" /></a></p>");
                                    showIMG.Append("</div>");
                                    showIMG.Append("</div>");
                                }

                                dataout.Comment = showIMG.ToString();
                                return serializer.Serialize(dataout);

                            }
                            else
                            {

                                if (listComment.Count > 0)
                                {
                                    ChatHub ch = new ChatHub();
                                    ch.SendMessengerToCustomer(uid, id, comment, listLink, listComment);
                                    CustomerComment dataout = new CustomerComment();
                                    StringBuilder showIMG = new StringBuilder();
                                    for (int i = 0; i < listLink.Count; i++)
                                    {

                                        showIMG.Append("<div class=\"mess-item mymess\">");
                                        showIMG.Append("<div class=\"img\"><img src=\"" + ai.IMGUser + "\"/></div>");
                                        showIMG.Append("<div class=\"cont\">");
                                        showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + "</p>");
                                        if (!string.IsNullOrEmpty(listLink[i]))
                                            showIMG.Append("<p><a href=\"" + listLink[i] + "\" target=\"_blank\"><img src=\"" + listLink[i] + "\" /></a></p>");
                                        showIMG.Append("</div>");
                                        showIMG.Append("</div>");
                                    }
                                    dataout.UID = uid;
                                    dataout.OrderID = id;
                                    dataout.Comment = showIMG.ToString();
                                    return serializer.Serialize(dataout);
                                }

                            }

                        }
                    }
                }
            }
            return serializer.Serialize(null);
        }
        public partial class CustomerComment
        {
            public int UID { get; set; }
            public int OrderID { get; set; }
            public string Comment { get; set; }
            public List<string> Link { get; set; }
            public List<string> CommentName { get; set; }
        }
        [WebMethod]
        public static string sendstaffcomment(string comment, int id, string urlIMG, string real)
        {
            var listLink = urlIMG.Split('|').ToList();
            if (listLink.Count > 0)
            {
                listLink.RemoveAt(listLink.Count - 1);
            }
            var listComment = real.Split('|').ToList();
            if (listComment.Count > 0)
            {
                listComment.RemoveAt(listComment.Count - 1);
            }
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (obj_user != null)
            {
                string ret = "";
                var ai = AccountInfoController.GetByUserID(obj_user.ID);
                if (ai != null)
                {
                    ret += ai.FirstName + " " + ai.LastName + "," + ai.IMGUser + "," + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate);
                }
                int uid = obj_user.ID;
                //var id = Convert.ToInt32(Request.QueryString["id"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {

                        int type = 2;
                        if (type > 0)
                        {
                            for (int i = 0; i < listLink.Count; i++)
                            {
                                string kqq = OrderCommentController.InsertNew(id, listLink[i], listComment[i], true, type, DateTime.Now, uid, 1);
                            }
                            if (!string.IsNullOrEmpty(comment))
                            {
                                string kq = OrderCommentController.Insert(id, comment, true, type, DateTime.Now, uid, 1);
                                var sale = AccountController.GetByID(o.SalerID.Value);
                                if (sale != null)
                                {
                                    if (obj_user.ID != sale.ID)
                                    {
                                        NotificationsController.Inser(sale.ID,
                                                                         sale.Username, id,
                                                                         "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 1,
                                                                          currentDate, username, false);
                                    }
                                }

                                var dathang = AccountController.GetByID(o.DathangID.Value);
                                if (dathang != null)
                                {
                                    if (obj_user.ID != dathang.ID)
                                    {
                                        NotificationsController.Inser(dathang.ID,
                                                                           dathang.Username, id,
                                                                           "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 1,
                                                                            currentDate, username, false);
                                    }
                                }

                                var admins = AccountController.GetAllByRoleID(0);
                                if (admins.Count > 0)
                                {
                                    foreach (var admin in admins)
                                    {
                                        if (obj_user.ID != admin.ID)
                                        {
                                            NotificationsController.Inser(admin.ID,
                                                                          admin.Username, id,
                                                                          "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 1,
                                                                           currentDate, username, false);
                                        }
                                    }
                                }
                                var managers = AccountController.GetAllByRoleID(2);
                                if (managers.Count > 0)
                                {
                                    foreach (var manager in managers)
                                    {
                                        if (obj_user.ID != manager.ID)
                                        {
                                            NotificationsController.Inser(manager.ID,
                                                                           manager.Username, id,
                                                                           "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 1,
                                                                          currentDate, username, false);
                                        }
                                    }
                                }
                                ChatHub ch = new ChatHub();
                                ch.SendMessengerToStaff(uid, id, comment, listLink, listComment);

                                CustomerComment dataout = new CustomerComment();
                                dataout.UID = uid;
                                dataout.OrderID = id;
                                StringBuilder showIMG = new StringBuilder();

                                if (!string.IsNullOrEmpty(comment))
                                {
                                    showIMG.Append("<div class=\"mess-item mymess\">");
                                    showIMG.Append("<div class=\"img\"><img src=\"" + ai.IMGUser + "\"/></div>");
                                    showIMG.Append("<div class=\"cont\">");
                                    showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + "</p>");
                                    if (!string.IsNullOrEmpty(comment))
                                    {
                                        showIMG.Append("<p>" + comment + "</p>");
                                    }

                                    showIMG.Append("</div>");
                                    showIMG.Append("</div>");
                                }

                                for (int i = 0; i < listLink.Count; i++)
                                {
                                    showIMG.Append("<div class=\"mess-item mymess\">");
                                    showIMG.Append("<div class=\"img\"><img src=\"" + ai.IMGUser + "\"/></div>");
                                    showIMG.Append("<div class=\"cont\">");
                                    showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + "</p>");
                                    if (!string.IsNullOrEmpty(listLink[i]))
                                        showIMG.Append("<p><a href=\"" + listLink[i] + "\" target=\"_blank\"><img src=\"" + listLink[i] + "\" /></a></p>");
                                    showIMG.Append("</div>");
                                    showIMG.Append("</div>");
                                }


                                dataout.Comment = showIMG.ToString();
                                return serializer.Serialize(dataout);


                            }
                            else
                            {
                                if (listComment.Count > 0)
                                {
                                    ChatHub ch = new ChatHub();
                                    ch.SendMessengerToStaff(uid, id, comment, listLink, listComment);
                                    CustomerComment dataout = new CustomerComment();
                                    StringBuilder showIMG = new StringBuilder();
                                    for (int i = 0; i < listLink.Count; i++)
                                    {
                                        showIMG.Append("<div class=\"mess-item mymess\">");
                                        showIMG.Append("<div class=\"img\"><img src=\"" + ai.IMGUser + "\"/></div>");
                                        showIMG.Append("<div class=\"cont\">");
                                        showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + "</p>");
                                        if (!string.IsNullOrEmpty(listLink[i]))
                                            showIMG.Append("<p><a href=\"" + listLink[i] + "\" target=\"_blank\"><img src=\"" + listLink[i] + "\" /></a></p>");
                                        showIMG.Append("</div>");
                                        showIMG.Append("</div>");
                                    }
                                    dataout.UID = uid;
                                    dataout.OrderID = id;
                                    dataout.Comment = showIMG.ToString();
                                    return serializer.Serialize(dataout);
                                }
                            }


                        }
                    }
                }
            }
            return serializer.Serialize(null);
        }

        [WebMethod]
        public static string CheckSmallPackageCode(int id, string listOrderCode)
        {
            string returncode = "";
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;

                int RoleID = obj_user.RoleID.ToString().ToInt();
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {
                        int uidmuahang = Convert.ToInt32(o.UID);
                        string usermuahang = "";

                        var accmuahan = AccountController.GetByID(uidmuahang);
                        if (accmuahan != null)
                        {
                            usermuahang = accmuahan.Username;
                        }
                        #region cập nhật và tạo mới smallpackage

                        string tcl = listOrderCode;
                        if (!string.IsNullOrEmpty(tcl))
                        {
                            string[] list = tcl.Split('|');
                            for (int i = 0; i < list.Length - 1; i++)
                            {

                                string[] item = list[i].Split(',');
                                int ID = item[0].ToInt(0);
                                string code = item[1];
                                string weight = item[2];
                                int smallpackage_status = item[3].ToInt(1);
                                string description = item[4];
                                if (ID > 0)
                                {
                                    var smp = SmallPackageController.GetByID(ID);
                                    if (smp != null)
                                    {
                                        bool check = false;
                                        int bigpackageID = Convert.ToInt32(smp.BigPackageID);
                                        var getsmallcheck = SmallPackageController.GetByOrderCode(code);
                                        if (getsmallcheck.Count > 0)
                                        {
                                            foreach (var sp in getsmallcheck)
                                            {
                                                if (sp.ID == ID)
                                                {
                                                    check = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            check = true;
                                        }

                                        if (check == false)
                                        {
                                            returncode += code + "; ";
                                        }
                                    }
                                    else
                                    {
                                        var getsmallcheck = SmallPackageController.GetByOrderCode(code);
                                        if (getsmallcheck.Count > 0)
                                        {
                                            returncode += code + "; ";
                                        }
                                    }
                                }
                                else
                                {
                                    var getsmallcheck = SmallPackageController.GetByOrderCode(code);
                                    if (getsmallcheck.Count > 0)
                                    {
                                        returncode += code + "; ";
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
                if (string.IsNullOrEmpty(returncode))
                {
                    returncode = "ok";
                }
            }
            return returncode;
        }
        #endregion
        #region Class
        public class historyCustom
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string RoleName { get; set; }
            public string Date { get; set; }
            public string Content { get; set; }
        }
        public class FeeWeightObj
        {
            public double FeeWeightVND { get; set; }
            public double FeeWeightCYN { get; set; }
            public double DiscountFeeWeightCYN { get; set; }
            public double DiscountFeeWeightVN { get; set; }
        }
        #endregion
    }
}