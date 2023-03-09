using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace NHST
{
    /// <summary>
    /// Summary description for namtrungservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class namtrungservice : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void Webhook()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var rs = new ResponseClass();

            string header = HttpContext.Current.Request.Headers["secure-token"];
            try
            {
                if (header == "bmhhcGhhbmd0cV8yMDIx")
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "Success";
                    DateTime currentdate = DateTime.Now;
                    string data = "";
                    using (var stream = new MemoryStream())
                    {
                        var request = HttpContext.Current.Request;
                        request.InputStream.Seek(0, SeekOrigin.Begin);
                        request.InputStream.CopyTo(stream);
                        data = Encoding.UTF8.GetString(stream.ToArray());
                    }
                    //Root casso = JsonSerializer.Deserialize<Root>(data);
                    Root casso = JsonConvert.DeserializeObject<Root>(data);
                    if (casso != null)
                    {
                        if (casso.data.Count > 0)
                        {
                            foreach (var item in casso.data)
                            {
                                var checksms = SmsForwardController.Check(item.ten_bank, item.trans_id, item.ma_gd, item.so_tien, item.soDu_bank);
                                if (checksms == null)
                                {
                                    string sID = SmsForwardController.Insert(item.so_tien.ToString(), item.ten_bank, item.ma_gd, item.noi_dung, item.soDu_bank.ToString(), item.thoi_gian, item.trans_id, header, item.Type.ToInt(1));
                                    if (sID != null)
                                    {
                                        if (item.Type.ToInt(0) == 1)
                                        {
                                            if (Convert.ToDouble(item.so_tien) > 0)
                                            {

                                                //string pattern = @"NTO *(\w*) *(\w*)";
                                                 string pattern = @"NT *(\w.*)";
                                                //string input = @"MBVCB.930995248.NAP 0839245952 qthai17.7@gmail.com.CT tu 0371000493042 NGUYEN QUANG THAI toi 0071000877538 VO VIET TIEN";
                                                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

                                                Regex rg = new Regex(pattern);
                                                string noidung = "";
                                                foreach (Match m in Regex.Matches(item.noi_dung, pattern, options))
                                                {
                                                    noidung = m.Value;
                                                }

                                                if (!string.IsNullOrEmpty(noidung))
                                                {
                                                    var temp = noidung.Split(' ');
                                                    string key = temp[0].ToLower();
                                                    if (key.ToLower() == "nt")
                                                    {
                                                        string username = temp[1].ToLower();
                                                       // string phone = temp[2].ToLower();
                                                        var u = AccountController.GetByUsername(username);
                                                        if (u != null)
                                                        {
                                                            //var checkphone = AccountInfoController.GetByPhoneAndUID(phone, u.ID);
                                                            //if (checkphone != null)
                                                            //{
                                                                double money = Convert.ToDouble(item.so_tien.Replace(",", ""));
                                                                double wallet = Math.Round(Convert.ToDouble(u.Wallet), 0);
                                                                money = Math.Round(money, 0);
                                                                wallet = wallet + money;
                                                                wallet = Math.Round(wallet, 0);



                                                                AdminSendUserWalletController.InsertNew(u.ID, u.Username, money, 2, item.noi_dung, currentdate, "Auto", sID.ToInt(0));
                                                                AccountController.updateWallet(u.ID, wallet, currentdate, "Auto");

                                                                HistoryPayWalletController.Insert(u.ID, u.Username, 0, money, u.Username + " đã được nạp tiền vào tài khoản.", wallet, 2, 4, currentdate, "Auto");

                                                                NotificationController.Inser(u.ID, u.Username,
                                                                            Convert.ToInt32(u.ID),
                                                                           u.Username, 0,
                                                                           "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.</a>", 0,
                                                                           2, currentdate, "Auto", false);

                                                                var setNoti = SendNotiEmailController.GetByID(3);
                                                                if (setNoti != null)
                                                                {
                                                                    if (setNoti.IsSentNotiUser == true)
                                                                    {
                                                                        NotificationsController.Inser(Convert.ToInt32(u.ID),
                                                                                                u.Username, 0,
                                                                                                "<a href=\"/lich-su-giao-dich\" target=\"_blank\">Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.</a>",
                                                                                                2, currentdate, "Auto", false);
                                                                    }

                                                                    if (setNoti.IsSendEmailUser == true)
                                                                    {
                                                                        try
                                                                        {
                                                                            PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", u.Email,
                                                                                "Thông báo tại namtrungorder.com.", "Bạn vừa được nạp " + string.Format("{0:N0}", money) + " VNĐ vào tài khoản.", "");
                                                                        }
                                                                        catch { }
                                                                    }
                                                                }

                                                            //}

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }



                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                    rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                    rs.Message = "Unauthorized";
                }
            }
            catch (Exception ex)
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                rs.Message = "Error";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        public class Datum
        {
            public string so_tien { get; set; }
            public string ten_bank { get; set; }
            public string ma_gd { get; set; }
            public string noi_dung { get; set; }
            public string soDu_bank { get; set; }
            public string thoi_gian { get; set; }
            public string trans_id { get; set; }
            public string Type { get; set; }
        }

        public class Root
        {
            public List<Datum> data { get; set; }
        }

        public class ResponseClass
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Code { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Status { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Message { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Key { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public object data { get; set; }
        }



    }
}
