using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;
using System.Text;
using System.Web.Script.Serialization;
using static NHST.manager.OrderDetail;
using System.Web.Services;

namespace NHST.manager
{
    public partial class ComplainDetail : System.Web.UI.Page
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
                    {
                        if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 3 && ac.RoleID != 6)
                            Response.Redirect("/trang-chu");
                        else
                        {
                            LoadData();
                        }
                    }
                }
            }
        }
        public void LoadData()
        {
            if (Request.QueryString["ID"] != null)
            {
                int ID = Request.QueryString["ID"].ToInt(0);
                ViewState["ID"] = ID;
                hdfOrderID.Value = ID.ToString();
                if (ID > 0)
                {
                    var com = ComplainController.GetByID(ID);
                    if (com != null)
                    {
                        int com_Status = Convert.ToInt32(com.Status);
                        string username_current = Session["userLoginSystem"].ToString();
                        tbl_Account ac = AccountController.GetByUsername(username_current);
                        if (ac != null)
                        {
                            int role = Convert.ToInt32(ac.RoleID);
                            if (role == 0 || role == 2 || role == 3 || role == 6)
                            {
                                if (role == 0 || role == 2)
                                {
                                    ddlStatus.Items.Add(new ListItem("Kết thúc khiếu nại", "0"));
                                    ddlStatus.Items.Add(new ListItem("Khiếu nại mới", "1"));
                                    ddlStatus.Items.Add(new ListItem("Đang xử lý", "2"));
                                    ddlStatus.Items.Add(new ListItem("Chờ hàng về thêm", "3"));
                                    ddlStatus.Items.Add(new ListItem("Chờ shop hoàn tiền", "4"));
                                    ddlStatus.Items.Add(new ListItem("Đổi trả hàng", "5"));
                                    ddlStatus.Items.Add(new ListItem("GD xử lý", "6"));
                                    ddlStatus.Items.Add(new ListItem("Kế toán xử lý", "7"));
                                    ddlStatus.Items.Add(new ListItem("Chờ CSKH hoàn thành", "9"));
                                    ddlStatus.Items.Add(new ListItem("CSKH xử lý", "10"));
                                    
                                    
                                    

                                    //string manager = "KhieuNaiDV";
                                    //if (role == 0)
                                    //{
                                    //    ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "8"));
                                    //}
                                    //else if (ac.Username.ToLower().Contains(manager.ToLower()))
                                    //{
                                    //    ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "8"));
                                    //}

                                    if (com_Status == 8)
                                    {
                                        ddlStatus.Enabled = false;
                                    }
                                }
                                else
                                {
                                    if (com_Status == 8)
                                    {
                                        ddlStatus.Items.Add(new ListItem("Kết thúc khiếu nại", "0"));
                                        ddlStatus.Items.Add(new ListItem("Khiếu nại mới", "1"));
                                        ddlStatus.Items.Add(new ListItem("Đang xử lý", "2"));
                                        ddlStatus.Items.Add(new ListItem("Chờ hàng về thêm", "3"));
                                        ddlStatus.Items.Add(new ListItem("Chờ shop hoàn tiền", "4"));
                                        ddlStatus.Items.Add(new ListItem("Đổi trả hàng", "5"));
                                        ddlStatus.Items.Add(new ListItem("GD xử lý", "6"));
                                        ddlStatus.Items.Add(new ListItem("Kế toán xử lý", "7"));
                                        ddlStatus.Items.Add(new ListItem("Chờ CSKH hoàn thành", "9"));
                                        ddlStatus.Items.Add(new ListItem("CSKH xử lý", "10"));
                                        ddlStatus.Enabled = false;
                                    }
                                    else
                                    {
                                        ddlStatus.Items.Add(new ListItem("Kết thúc khiếu nại", "0"));
                                        ddlStatus.Items.Add(new ListItem("Khiếu nại mới", "1"));
                                        ddlStatus.Items.Add(new ListItem("Đang xử lý", "2"));
                                        ddlStatus.Items.Add(new ListItem("Chờ hàng về thêm", "3"));
                                        ddlStatus.Items.Add(new ListItem("Chờ shop hoàn tiền", "4"));
                                        ddlStatus.Items.Add(new ListItem("Đổi trả hàng", "5"));
                                        ddlStatus.Items.Add(new ListItem("GD xử lý", "6"));
                                        ddlStatus.Items.Add(new ListItem("Kế toán xử lý", "7"));
                                        ddlStatus.Items.Add(new ListItem("Chờ CSKH hoàn thành", "9"));
                                        ddlStatus.Items.Add(new ListItem("CSKH xử lý", "10"));
                                    }
                                }
                            }
                        }

                        var ordershop = MainOrderController.GetAllByID(Convert.ToInt32(com.OrderID));
                        if (ordershop != null)
                        {
                            hdfCurrency.Value = ordershop.CurrentCNYVN;
                            lblCurrence.Text = string.Format("{0:N0}", Convert.ToDouble(ordershop.CurrentCNYVN)).Replace(",", ".");
                            lblAmountCYN.Text = string.Format("{0:N0}", Convert.ToDouble(com.Amount) / Convert.ToDouble(ordershop.CurrentCNYVN)).Replace(",", ".");
                            ViewState["ID"] = ID;
                            txtUsername.Text = com.CreatedBy;
                            txtOrderShopCode.Text = com.OrderID.ToString();
                            pBuyNDT.Value = Convert.ToDouble(com.Amount);
                            txtComplainText.Text = com.ComplainText;
                            txtNote.Text = com.EmployeeNote;
                            lblComplainType.Text = com.ComplainType;
                            ddlStatus.SelectedValue = com.Status.ToString();
                            if (!string.IsNullOrEmpty(com.IMG))
                            {
                                string imgs = com.IMG;
                                if (imgs.Contains("|"))
                                {
                                    string[] imgslist = imgs.Split('|');
                                    for (int i = 0; i < imgslist.Length - 1; i++)
                                    {
                                        string img = imgslist[i];
                                        ltrIMG.Text += "<a href=\"" + img + "\" target=\"_blank\"><img src=\"" + img + "\" width=\"200px\" style=\"float:left;margin-right:5px;\"/></a>";
                                    }
                                }
                            }
                        }


                        #region Lấy bình luận nội bộ
                        var cs = OrderCommentController.GetByOrderIDAndType(ID, 3, 1);
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
                                    if (ac.ID == user_postID)
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
                        int type = 3;
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
                    var o = ComplainController.GetByID(id);
                    if (o != null)
                    {
                        var mo = MainOrderController.GetAllByID(o.OrderID.Value);

                        int type = 3;
                        if (type > 0)
                        {
                            for (int i = 0; i < listLink.Count; i++)
                            {
                                string kqq = OrderCommentController.InsertNew(id, listLink[i], listComment[i], true, type, DateTime.Now, uid, 1);
                            }
                            if (!string.IsNullOrEmpty(comment))
                            {
                                string kq = OrderCommentController.Insert(id, comment, true, type, DateTime.Now, uid, 1);
                                var sale = AccountController.GetByID(mo.SalerID.Value);
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

                                var dathang = AccountController.GetByID(mo.DathangID.Value);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["ID"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var ac = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/ComplainList.aspx";
            if (ID > 0)
            {
                var com = ComplainController.GetByID(ID);
                if (com != null)
                {
                    var setNoti = SendNotiEmailController.GetByID(10);
                    int status = ddlStatus.SelectedValue.ToInt();

                    ComplainController.UpdateNew(com.ID, pBuyNDT.Value.ToString(), txtComplainText.Text, txtNote.Text, status, DateTime.Now, username_current);
                    if (status == 0) //8-đã hoàn thành
                    {
                        string uReceive = txtUsername.Text.Trim().ToLower();
                        var u = AccountController.GetByUsername(uReceive);
                        if (u != null)
                        {
                            int UID = u.ID;
                            double wallet = Convert.ToDouble(u.Wallet);
                            wallet = wallet + Convert.ToDouble(pBuyNDT.Value);

                            //AccountController.updateWallet(u.ID, wallet, currentDate, username_current);
                            //HistoryPayWalletController.Insert(u.ID, u.Username, Convert.ToInt32(com.OrderID), Convert.ToDouble(pBuyNDT.Value),
                            //    u.Username + " đã được hoàn tiền khiếu nại của đơn hàng: " + com.OrderID + " vào tài khoản.",
                            //    wallet, 2, 7, currentDate, username_current);
                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    NotificationsController.Inser(Convert.ToInt32(u.ID),
                               AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                               "<a href=\"/khieu-nai?ordershopcode=" + com.OrderID + "\" target=\"_blank\">Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.</a>",
                               5, currentDate, ac.Username, false);
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail("Kd.namtrung@gmail.com", "ugkqejxkyhbppdkz", u.Email,
                                            "Thông báo tại Nam Trung.", "Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.", "");
                                    }
                                    catch { }
                                }
                            }

                           
                        }
                    }
                   
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                }
            }
        }
    }
}