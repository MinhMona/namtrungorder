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
using NHST.Controllers;
using NHST.Models;

namespace NHST
{
    public partial class chi_tiet_thanh_toan_ho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadData()
        {
            var id = RouteData.Values["id"].ToString().ToInt(0);
            if (id > 0)
            {
                string username = Session["userLoginSystem"].ToString();
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    int UID = u.ID;
                    var p = PayhelpController.GetByIDAndUID(id, UID);
                    if (p != null)
                    {
                        ViewState["ID"] = id;
                        int status = Convert.ToInt32(p.Status);
                        if (status == 0)
                        {
                            btnSend.Visible = true;
                            ltrPay.Text = "<a href=\"javascript:;\" onclick=\"paymoney();\" class=\"submit-btn border-ra-5px\">Thanh toán</a>";
                        }
                        else btnSend.Visible = false;

                        ltrIfn.Text = username;
                        pAmount.Value = Convert.ToDouble(p.TotalPrice);
                        pVND.Value = Convert.ToDouble(p.TotalPriceVND);
                        rTigia.Value = Convert.ToDouble(p.Currency);
                        txtNote.Text = p.Note;
                        string trangthai = "";
                        if (status == 0)
                            trangthai = "Chưa thanh toán";
                        else if (status == 1)
                            trangthai = "Đã thanh toán";
                        else
                            trangthai = "Đã hủy";
                        lblStatus.Text = PJUtils.ReturnStatusPayHelp(status);
                        var pds = PayhelpDetailController.GetByPayhelpID(id);
                        if (pds.Count > 0)
                        {
                            int i = 0;
                            foreach (var item in pds)
                            {
                                if (i == 0)
                                    ltrList.Text += "<div class=\"itemyeuau row-item \">";
                                else
                                    ltrList.Text += "<div class=\"itemyeuau row-item custom-border-padding\" >";

                                ltrList.Text += "<div class=\"rowhalf\" >";
                                ltrList.Text += "<div class=\"label-field\">Giá tiền:</div>";
                                ltrList.Text += "<div class=\"textbox-field\"><input class=\"txtDesc2 form-control border-ra-bg\" disabled value=\"" + item.Desc2 + "\"/></div></div>";
                                ltrList.Text += "<div class=\"rowhalf\">";
                                ltrList.Text += "<div class=\"label-field\">Nội dung:</div>";
                                ltrList.Text += "<div class=\"textbox-field\"><input class=\"txtDesc1 form-control border-ra-bg\" disabled value=\"" + item.Desc1 + "\"/></div>";
                                ltrList.Text += "</div>";
                                ltrList.Text += "</div>";
                                i++;
                            }
                        }

                    }
                    else Response.Redirect("/thanh-toan-ho");
                }
                else Response.Redirect("/thanh-toan-ho");
            }
            else
            {
                Response.Redirect("/thanh-toan-ho");
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            var id = ViewState["ID"].ToString().ToInt(0);
            if (id > 0)
            {
                string username = Session["userLoginSystem"].ToString();
                DateTime currentDate = DateTime.Now;
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    int UID = u.ID;
                    var p = PayhelpController.GetByIDAndUID(id, UID);
                    if (p != null)
                    {
                        double wallet = Convert.ToDouble(u.Wallet);
                        double walletCYN = Convert.ToDouble(u.WalletCYN);

                        double Totalprice_left = 0;

                        double Currency = Convert.ToDouble(p.Currency);
                        double TotalPrice = Convert.ToDouble(p.TotalPrice);
                        double TotalPriceVND = Convert.ToDouble(p.TotalPriceVND);
                        if (walletCYN > 0)
                        {
                            if (walletCYN >= TotalPrice)
                            {
                                double walletCYN_left = walletCYN - TotalPrice;
                                AccountController.updateWalletCYN(UID, walletCYN_left);
                                HistoryPayWalletCYNController.Insert(UID, username, TotalPrice, walletCYN_left, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
                                    currentDate, username);

                                PayhelpController.UpdateStatus(id, 1, currentDate, username);
                                PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                            }
                            else
                            {
                                double walletCYN_left = TotalPrice - walletCYN;
                                double totalpricevndpay = walletCYN_left * Currency;
                                if (wallet >= totalpricevndpay)
                                {
                                    //double walletCYN_left = TotalPrice - walletCYN;
                                    AccountController.updateWalletCYN(UID, 0);
                                    HistoryPayWalletCYNController.Insert(UID, username, walletCYN, 0, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
                                        currentDate, username);

                                    //double totalpricevndpay = walletCYN_left * Currency;
                                    double walletleft = wallet - totalpricevndpay;
                                    AccountController.updateWallet(UID, walletleft, currentDate, username);
                                    HistoryPayWalletController.Insert(UID, username, 0, totalpricevndpay,
                                        username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, currentDate, username);
                                    PayhelpController.UpdateStatus(id, 1, currentDate, username);                                    
                                    PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
                                }
                            }
                        }
                        else
                        {
                            if (wallet >= TotalPriceVND)
                            {
                                double walletleft = wallet - TotalPriceVND;
                                AccountController.updateWallet(UID, walletleft, currentDate, username);
                                HistoryPayWalletController.Insert(UID, username, 0, TotalPriceVND,
                                    username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, currentDate, username);
                                PayhelpController.UpdateStatus(id, 1, currentDate, username);                                
                                PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
                            }
                        }
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}