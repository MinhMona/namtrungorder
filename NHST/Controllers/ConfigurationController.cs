using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class ConfigurationController
    {
        #region CRUD
        public static string Update(int ID, string Websitename, string EmailSupport, string EmailContact, string Hotline, string Address, string Facebook, string Twitter, string GooglePlus, string Instagram,
            string Skype, string TimeWork, string Currency, string CurrencyIncome, string PercentOrder, string InfoContent, string LogoIMG, string BannerIMG, string ChromeExtensionLink, string CocCocExtensionLink,
            string AboutText, string Address2, string Address3, string FooterLeft, string FooterRight, string WeightPrice, string PricePayHelpDefault,
            string PriceSendDefaultHN, string PriceSendDefaultSG, string SalePercentAfter3Month, string SalePercent, string DathangPercent,
            string HotlineSupport,string HotlineFeedback, string Pinterest,string popupNoti, string NotiPopupTitle, string NotiPopupEmail,
            string HotlineKhoHCM, string HotlineKhoHN)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                var conf = dbe.tbl_Configuration.Where(c => c.ID == ID).FirstOrDefault();
                if (conf != null)
                {
                    conf.Websitename = Websitename;
                    conf.EmailContact = EmailContact;
                    conf.EmailSupport = EmailSupport;
                    conf.Hotline = Hotline;
                    conf.Address = Address;
                    conf.Facebook = Facebook;
                    conf.Twitter = Twitter;
                    conf.GooglePlus = GooglePlus;
                    conf.Instagram = Instagram;
                    conf.Skype = Skype;
                    conf.TimeWork = TimeWork;
                    conf.Currency = Currency;
                    conf.CurrencyIncome = CurrencyIncome;
                    conf.PercentOrder = PercentOrder;
                    conf.InfoContent = InfoContent;
                    conf.LogoIMG = LogoIMG;
                    conf.BannerIMG = BannerIMG;
                    conf.ChromeExtensionLink = ChromeExtensionLink;
                    conf.CocCocExtensionLink = CocCocExtensionLink;
                    conf.AboutText = AboutText;
                    conf.Address2 = Address2;
                    conf.Address3 = Address3;
                    conf.FooterLeft = FooterLeft;
                    conf.FooterRight = FooterRight;
                    conf.WeightPrice = WeightPrice;
                    conf.PricePayHelpDefault = PricePayHelpDefault;
                    conf.PriceSendDefaultHN = PriceSendDefaultHN;
                    conf.PriceSendDefaultSG = PriceSendDefaultSG;
                    conf.SalePercentAfter3Month = SalePercentAfter3Month;
                    conf.SalePercent = SalePercent;
                    conf.HotlineSupport = HotlineSupport;
                    conf.DathangPercent = DathangPercent;
                    conf.HotlineFeedback = HotlineFeedback;
                    conf.Pinterest = Pinterest;
                    conf.NotiPopup = popupNoti;
                    conf.NotiPopupTitle = NotiPopupTitle;
                    conf.NotiPopupEmail = NotiPopupEmail;
                    conf.HotlineKhoHCM = HotlineKhoHCM;
                    conf.HotlineKhoHN = HotlineKhoHN;
                    dbe.SaveChanges();
                    return "ok";
                }
                else return null;
            }
        }
        #endregion
        #region Select
        public static tbl_Configuration GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var conf = dbe.tbl_Configuration.Where(c => c.ID == ID).FirstOrDefault();
                if (conf != null)
                {
                    return conf;
                }
                else
                    return null;
            }
        }
        public static tbl_Configuration GetByTop1()
        {
            using (var dbe = new NHSTEntities())
            {
                var conf = dbe.tbl_Configuration.Where(c => c.ID == 1).FirstOrDefault();
                if (conf != null)
                {
                    return conf;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}