using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class Configuration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                loaddata();
            }
        }
        public void loaddata()
        {
            var c = ConfigurationController.GetByTop1();
            if (c != null)
            {
                txtWebsitename.Text = c.Websitename;
                if (c.LogoIMG != null)
                    imgLogo.ImageUrl = c.LogoIMG;
                if (c.BannerIMG != null)
                    imgBannerIMG.ImageUrl = c.BannerIMG;
                txtChromeExtensionLink.Text = c.ChromeExtensionLink;
                txtCocCocExtensionLink.Text = c.CocCocExtensionLink;
                rAbount.Content = c.AboutText;
                rAddress.Content = c.Address;
                rAddress2.Content = c.Address2;
                rAddress3.Content = c.Address3;
                txtEmailSupport.Text = c.EmailSupport;
                txtEmailContact.Text = c.EmailContact;
                txtHotline.Text = c.Hotline;
                txtHotlineSupport.Text = c.HotlineSupport;
                txtHotlineFeedback.Text = c.HotlineFeedback;
                txtHotlineKhoHCM.Text = c.HotlineKhoHCM;
                txtHotlineKhoHN.Text = c.HotlineKhoHN;
                txtFacebook.Text = c.Facebook;
                txtTwitter.Text = c.Twitter;
                txtGooglePlus.Text = c.GooglePlus;
                txtPinterest.Text = c.Pinterest;
                txtInstagram.Text = c.Instagram;
                txtSkype.Text = c.Skype;
                txtTimeWork.Text = c.TimeWork;
                pCurrency.Value = Convert.ToDouble(c.Currency);
                rFooterTrai.Content = c.FooterLeft;
                rFooterPhai.Content = c.FooterRight;
                pContent.Content = c.InfoContent;

                rPercent.Value = Convert.ToDouble(c.PercentOrder);
                rWeightPrice.Value = Convert.ToDouble(c.WeightPrice);
                rCurrencyIncome.Value = Convert.ToDouble(c.CurrencyIncome);
                pPricePayHelpDefault.Value = Convert.ToDouble(c.PricePayHelpDefault);
                pPriceSendDefaultHN.Value = Convert.ToDouble(c.PriceSendDefaultHN);
                pPriceSendDefaultSG.Value = Convert.ToDouble(c.PriceSendDefaultSG);
                rSalePercentAfter3Month.Value = Convert.ToDouble(c.SalePercentAfter3Month);
                rSalePercent.Value = Convert.ToDouble(c.SalePercent);
                rDathangPercent.Value = Convert.ToDouble(c.DathangPercent);
                rNotiPopup.Content = c.NotiPopup;
                txtNotiPopupTitle.Text = c.NotiPopupTitle;
                txtEmailNoti.Text = c.NotiPopupEmail;
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var c = ConfigurationController.GetByTop1();
            if (c != null)
            {
                string PathIMG = "/Uploads/images/";
                string LogoIMG = "";
                string BannerIMG = "";
                if (rLogo.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in rLogo.UploadedFiles)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                var o = PathIMG + Guid.NewGuid() + f.GetExtension();
                                try
                                {
                                    f.SaveAs(Server.MapPath(o));
                                    LogoIMG = o;
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                    LogoIMG = imgLogo.ImageUrl;

                if (rBannerIMG.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in rBannerIMG.UploadedFiles)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                var o = PathIMG + Guid.NewGuid() + f.GetExtension();
                                try
                                {
                                    f.SaveAs(Server.MapPath(o));
                                    BannerIMG = o;
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                    BannerIMG = imgBannerIMG.ImageUrl;


                var kq = ConfigurationController.Update(c.ID, txtWebsitename.Text, txtEmailSupport.Text, txtEmailContact.Text, txtHotline.Text,
                    rAddress.Content, txtFacebook.Text, txtTwitter.Text, txtGooglePlus.Text,
                    txtInstagram.Text, txtSkype.Text, txtTimeWork.Text, pCurrency.Value.ToString(), rCurrencyIncome.Value.ToString(), rPercent.Value.ToString(),
                    pContent.Content, LogoIMG, BannerIMG, txtChromeExtensionLink.Text, txtCocCocExtensionLink.Text, rAbount.Content, rAddress2.Content, rAddress3.Content,
                    rFooterTrai.Content, rFooterPhai.Content, rWeightPrice.Value.ToString(),
                    pPricePayHelpDefault.Value.ToString(), pPriceSendDefaultHN.Value.ToString(), pPriceSendDefaultSG.Value.ToString(),
                    rSalePercentAfter3Month.Value.ToString(), rSalePercent.Value.ToString(), rDathangPercent.Value.ToString(), txtHotlineSupport.Text,
                    txtHotlineFeedback.Text, txtPinterest.Text, rNotiPopup.Content, txtNotiPopupTitle.Text, txtEmailNoti.Text,
                    txtHotlineKhoHCM.Text, txtHotlineKhoHN.Text);
                if (kq == "ok")
                    PJUtils.ShowMsg("Cập nhật thiết lập thành công.", true, Page);
            }
        }
    }
}