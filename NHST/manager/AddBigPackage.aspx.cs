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
using MB.Extensions;
using System.Text;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Imaging;

namespace NHST.manager
{
    public partial class AddBigPackage : System.Web.UI.Page
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
                    if (ac != null)
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string list = hdflistpackage.Value;
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            DateTime SendDate = Convert.ToDateTime(rSendDate.SelectedDate);
            DateTime ArrivedDate = Convert.ToDateTime(rArrivedDate.SelectedDate);
            int Place = ddlPlace.SelectedValue.ToInt();
            int Status = ddlStatus.SelectedValue.ToInt();
            double AdditionFee = Convert.ToDouble(pAdditionFee.Value);
            string kq = BigPackageController1.Insert(SendDate, ArrivedDate, txtPackageCode.Text, pContent.Content,
                Place, Status, chskIsSlow.Checked, AdditionFee, currentDate, username_current);
            int bID = kq.ToInt(0);
            if (bID > 0)
            {
                if (!string.IsNullOrEmpty(list))
                {
                    string[] itemlist = list.Split('|');
                    for (int i = 0; i < itemlist.Length - 1; i++)
                    {
                        string item = itemlist[i];
                        string[] itemdetail = item.Split(',');
                        string pCode = itemdetail[0].Trim();
                        string barcodeIMG = "/Uploads/smallpackagebarcode/" + pCode + ".gif";
                        Bitmap barCode = PJUtils.CreateBarcode1(pCode);
                        barCode.Save(Server.MapPath("~" + barcodeIMG + ""), ImageFormat.Gif);

                        string pUserPhone = itemdetail[1].Trim();
                        string pWeight = itemdetail[2].Trim();
                        string pNote = itemdetail[3].Trim();
                        string pReceived = itemdetail[4].Trim();
                        string pPayment = itemdetail[5].Trim();
                        string pNoteCus = itemdetail[6].Trim();
                        if (!string.IsNullOrEmpty(pCode) && !string.IsNullOrEmpty(pUserPhone) && !string.IsNullOrEmpty(pWeight))
                        {
                            var u = AccountController.GetByPhone(pUserPhone);
                            if (u != null)
                            {
                                //NotificationController.InsertAdmin(UID, username, a.ID, a.Username, ShopID, ordershopcode, comment, 0, true, currentDate, username);

                                double weight = Convert.ToDouble(pWeight);
                                SmallPackageController1.Insert(bID, SendDate, pCode, u.ID, pUserPhone, weight, Place, pReceived.ToInt(), pPayment.ToInt(),
                                    pNote, pNoteCus, barcodeIMG, currentDate, username_current);
                                try
                                {
                                    //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", u.Email, "Thông báo nhận hàng tại vominhthien.com", "Kiện hàng của bạn đã về đến kho vominhthien.com vào ngày: " + string.Format("{0:dd/MM/yyyy}", ArrivedDate), "");
                                    PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", u.Email,
                                                            "Thông báo nhận hàng tại Nam Trung.",
                                                            "Kiện hàng của bạn đã về đến kho 1688express.com vào ngày: " + string.Format("{0:dd/MM/yyyy}", ArrivedDate), "");
                                }
                                catch { }
                            }
                            else
                            {
                                double weight = Convert.ToDouble(pWeight);
                                SmallPackageController1.Insert(bID, SendDate, pCode, 0, pUserPhone, weight, Place, pReceived.ToInt(), pPayment.ToInt(), pNote,
                                     pNoteCus, barcodeIMG, currentDate, username_current);
                            }
                        }
                    }
                }
                PJUtils.ShowMessageBoxSwAlert("Tạo mới thành công", "s", true, Page);
            }
        }

        [WebMethod]
        public static string GetBarCode()
        {
            string code = "";
            code = "VCDQG" + PJUtils.RandomString(9);
            //DateTime d = DateTime.Now;
            //code = d.Year.ToString() + d.Month.ToString() + d.Day.ToString() + d.Hour.ToString() + d.Minute.ToString() + d.Second.ToString() + d.Millisecond.ToString();
            return code;
        }
    }
}