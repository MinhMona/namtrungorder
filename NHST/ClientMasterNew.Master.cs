using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class ClientMasterNew : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoginSystem"] == null)
            {
                Response.Redirect("/manager/Login.aspx");
            }
            else
            {
                string username_current = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username_current);
                if (obj_user != null)
                {
                }
                LoadNotification();
                LoadMenu();
            }
        }
        public void LoadMenu()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                var ordershoptemp = OrderShopTempController.GetByUID(obj_user.ID);
                ltrCountCart.Text = "(" + ordershoptemp.Count + ")";
            }
        }
        public void LoadNotification()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var acc = AccountController.GetByUsername(username_current);
            if (acc != null)
            {
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    ltrinfo.Text += "<a href=\"/\" class=\"right-it rate\"><i class=\"fa fa-home\"></i></a>";
                    ltrinfo.Text += "<a href=\"javascript:;\" class=\"right-it rate\"> ¥1 = " + string.Format("{0:N0}", config.Currency) + "</a>";
                    var noti = NotificationController.GetByReceivedID(acc.ID);
                    var noti1 = noti.Where(n => n.NotifType == 1).Take(5).ToList();
                    var noti2 = noti.Where(n => n.NotifType == 2).Take(5).ToList();
                    var noti3 = noti.Where(n => n.NotifType == 3).Take(5).ToList();
                    var noti5 = noti.Where(n => n.NotifType == 5).Take(5).ToList();
                    var noti6 = noti.Where(n => n.NotifType == 6).Take(5).ToList();

                    string stt1Name = "Đơn hàng";
                    string stt1Shortname = "dh";

                    string stt2Name = "Nạp tiền";
                    string stt2Shortname = "nt";

                    string stt3Name = "Rút tiền";
                    string stt3Shortname = "rt";

                    string stt5Name = "Khiếu nại";
                    string stt5Shortname = "kn";

                    string stt6Name = "Đăng ký";
                    string stt6Shortname = "dk";

                    if (acc.RoleID != 1)
                    {
                        ltrinfo.Text += "<a href=\"/manager/admin-noti\" class=\"right-it noti\"><i class=\"fa fa-bell-o\"></i><span class=\"badge\">" + noti.Count + "</span></a>";
                        if (noti.Count > 0)
                        {
                            ltrinfo.Text += "<div class=\"notification_wrap\">";
                            ltrinfo.Text += "  <div class=\"tab_nav\">";
                            ltrinfo.Text += "   <a href=\"javascript:;\" class=\"close-noti\">Đóng</a>";
                            ltrinfo.Text += "	<a href=\"#noti-all\" class=\"tab_link active\">Tất cả</a>";
                            if (noti1.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt1Shortname + "\" class=\"tab_link\">" + stt1Name + "</a>";
                            }
                            if (noti2.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt2Shortname + "\" class=\"tab_link\">" + stt2Name + "</a>";
                            }
                            if (noti3.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt3Shortname + "\" class=\"tab_link\">" + stt3Name + "</a>";
                            }
                            if (noti5.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt5Shortname + "\" class=\"tab_link\">" + stt5Name + "</a>";
                            }
                            if (noti6.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt6Shortname + "\" class=\"tab_link\">" + stt6Name + "</a>";
                            }
                            ltrinfo.Text += "  </div>";
                            ltrinfo.Text += "  <div class=\"tab_content_wrap\">";
                            ltrinfo.Text += "	<ul class=\"tab_content show\" id=\"noti-all\">";
                            if (noti.Count > 0)
                            {
                                var notiget5items = noti.Take(15).ToList();
                                foreach (var item in notiget5items)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    if (item.NotifType == 1)
                                    {
                                        //ltrinfo.Text += "		<a href=\"/manager/OrderDetail.aspx?id=" + item.OrderID + "\" onclick=\"checkisRead('" + item.ID + "')\">";
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/OrderDetail.aspx?id=" + item.OrderID + "')\">";
                                    }
                                    else if (item.NotifType == 2)
                                    {
                                        //ltrinfo.Text += "		<a href=\"/manager/HistorySendWallet\" onclick=\"checkisRead('" + item.ID + "')\">";
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/HistorySendWallet')\">";
                                    }
                                    else if (item.NotifType == 3)
                                    {
                                        //ltrinfo.Text += "		<a href=\"/manager/Withdraw-List\" onclick=\"checkisRead('" + item.ID + "')\">";
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/Withdraw-List')\">";
                                    }
                                    else if (item.NotifType == 5)
                                    {
                                        //ltrinfo.Text += "		<a href=\"/manager/ComplainList\" onclick=\"checkisRead('" + item.ID + "')\">";
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/ComplainList')\">";
                                    }
                                    else if (item.NotifType == 6)
                                    {
                                        //ltrinfo.Text += "		<a href=\"/manager/userlist\" onclick=\"checkisRead('" + item.ID + "')\">";
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/userlist')\">";
                                    }
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }
                            }
                            ltrinfo.Text += "	</ul>";
                            if (noti1.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt1Shortname + "\">";
                                foreach (var item in noti1)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/OrderDetail.aspx?id=" + item.OrderID + "')\">";
                                    //ltrinfo.Text += "		<a href=\"/manager/OrderDetail.aspx?id=" + item.OrderID + "\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            if (noti2.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt2Shortname + "\">";
                                foreach (var item in noti2)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/HistorySendWallet')\">";
                                    //ltrinfo.Text += "		<a href=\"/manager/HistorySendWallet\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            if (noti3.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt3Shortname + "\">";
                                foreach (var item in noti3)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/Withdraw-List')\">";
                                    //ltrinfo.Text += "		<a href=\"/manager/Withdraw-List\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            if (noti5.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt5Shortname + "\">";
                                foreach (var item in noti5)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/ComplainList')\">";
                                    //ltrinfo.Text += "		<a href=\"/manager/ComplainList\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            if (noti6.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt6Shortname + "\">";
                                foreach (var item in noti6)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/manager/userlist')\">";
                                    //ltrinfo.Text += "		<a href=\"/manager/userlist\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            ltrinfo.Text += "  </div>";
                            ltrinfo.Text += "  <div class=\"view-all-noti\"><a href=\"/manager/admin-noti.aspx\">Xem tất cả</a></div>";
                            ltrinfo.Text += "</div>";
                        }

                    }
                    else
                    {
                        ltrinfo.Text += "<a href=\"/thong-bao-cua-ban\" class=\"right-it noti\"><i class=\"fa fa-bell-o\"></i><span class=\"badge\">" + noti.Count + "</span></a>";
                        if(noti.Count>0)
                        {
                            ltrinfo.Text += "<div class=\"notification_wrap\">";
                            ltrinfo.Text += "  <div class=\"tab_nav\">";
                            ltrinfo.Text += "   <a href=\"javascript:;\" class=\"close-noti\">Đóng</a>";
                            ltrinfo.Text += "	<a href=\"#noti-all\" class=\"tab_link active\">Tất cả</a>";
                            if (noti1.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt1Shortname + "\" class=\"tab_link\" >" + stt1Name + "</a>";
                            }
                            if (noti2.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt2Shortname + "\" class=\"tab_link\">" + stt2Name + "</a>";
                            }
                            if (noti3.Count > 0)
                            {
                                ltrinfo.Text += "	<a href=\"#noti-" + stt3Shortname + "\" class=\"tab_link\">" + stt3Name + "</a>";
                            }

                            ltrinfo.Text += "  </div>";
                            ltrinfo.Text += "  <div class=\"tab_content_wrap\">";
                            ltrinfo.Text += "	<ul class=\"tab_content show\" id=\"noti-all\">";
                            if (noti.Count > 0)
                            {
                                var notiget5items = noti.Take(15).ToList();
                                foreach (var item in notiget5items)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    if (item.NotifType == 1)
                                    {
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/chi-tiet-don-hang/" + item.OrderID + "')\">";
                                        //ltrinfo.Text += "		<a href=\"/chi-tiet-don-hang/" + item.OrderID + "\" onclick=\"checkisRead('" + item.ID + "')\">";
                                    }
                                    else if (item.NotifType == 2)
                                    {
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/lich-su-giao-dich')\">";
                                        //ltrinfo.Text += "		<a href=\"/lich-su-giao-dich\" onclick=\"checkisRead('" + item.ID + "')\"> ";
                                    }
                                    else if (item.NotifType == 3)
                                    {
                                        ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/lich-su-giao-dich')\">";
                                        //ltrinfo.Text += "		<a href=\"/lich-su-giao-dich\" onclick=\"checkisRead('" + item.ID + "')\"> ";
                                    }
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }
                            }
                            ltrinfo.Text += "	</ul>";
                            if (noti1.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt1Shortname + "\">";
                                foreach (var item in noti1)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/chi-tiet-don-hang/" + item.OrderID + "')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            if (noti2.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt2Shortname + "\">";
                                foreach (var item in noti2)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/lich-su-giao-dich')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            if (noti3.Count > 0)
                            {
                                ltrinfo.Text += "	<ul class=\"tab_content\" id=\"noti-" + stt3Shortname + "\">";
                                foreach (var item in noti3)
                                {
                                    ltrinfo.Text += "	  <li>";
                                    ltrinfo.Text += "		<a href=\"javascript:;\" onclick=\"checkisRead('" + item.ID + "','/lich-su-giao-dich')\">";
                                    ltrinfo.Text += "		  <div class=\"icon-noti\"><span class=\"glyphicon glyphicon-list-alt\"></span></div>";
                                    ltrinfo.Text += "		  <div class=\"noti-content\">";
                                    ltrinfo.Text += "			<p class=\"content\">" + item.Message + "</p>";
                                    ltrinfo.Text += "			<time class=\"media-meta\" datetime=\"" + item.CreatedDate + "\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>";
                                    ltrinfo.Text += "		  </div>";
                                    ltrinfo.Text += "		</a>";
                                    ltrinfo.Text += "	  </li>";
                                }

                                ltrinfo.Text += "	</ul>";
                            }
                            ltrinfo.Text += "  </div>";
                            ltrinfo.Text += "  <div class=\"view-all-noti\"><a href=\"/thong-bao-cua-ban\">Xem tất cả</a></div>";
                            ltrinfo.Text += "</div>";
                        }
                    }
                    string html = "";
                    decimal levelID = Convert.ToDecimal(acc.LevelID);
                    int levelID1 = Convert.ToInt32(acc.LevelID);
                    string level = "Vip 0";
                    var userLevel = UserLevelController.GetByID(levelID1);
                    if (userLevel != null)
                    {
                        level = userLevel.LevelName;
                    }

                    decimal countLevel = UserLevelController.GetAll("").Count();
                    decimal te = levelID / countLevel;
                    te = Math.Round(te, 2, MidpointRounding.AwayFromZero);
                    decimal tile = te * 100;

                    ltrinfo.Text += "  <div class=\"right-it username dropdown\">";
                    ltrinfo.Text += "      <a href=\"#\" class=\"link__item\">" + acc.Username + "</a>";
                    ltrinfo.Text += "<div class=\"status-wrap\">";
                    ltrinfo.Text += "  <div class=\"status\">";
                    ltrinfo.Text += "      <header><h4>" + level + "</h4></header>";
                    ltrinfo.Text += "      <main>";
                    ltrinfo.Text += "          <section class=\"level\">";
                    ltrinfo.Text += "              <div class=\"level__info\">";
                    ltrinfo.Text += "                  <p>Level</p>";
                    ltrinfo.Text += "                  <p class=\"rank\">" + level + "</p>";
                    ltrinfo.Text += "              </div>";
                    ltrinfo.Text += "              <div class=\"process\">";
                    ltrinfo.Text += "                  <span style=\"width: " + tile + "%\"></span>";
                    ltrinfo.Text += "              </div>";
                    ltrinfo.Text += "          </section>";
                    ltrinfo.Text += "          <section class=\"balance\">";
                    ltrinfo.Text += "              <p>Số dư:</p>";
                    ltrinfo.Text += "              <div class=\"balance__number\">";
                    ltrinfo.Text += "                  <p class=\"vnd\">" + string.Format("{0:N0}", acc.Wallet) + " vnđ</p>";
                    //ltrLogin.Text += "                  <p class=\"cny\">2450Y</p>";
                    ltrinfo.Text += "              </div>";
                    ltrinfo.Text += "          </section>";
                    if (acc.RoleID != 1)
                    {
                        ltrinfo.Text += "          <section class=\"links\">";
                        ltrinfo.Text += "              <a href=\"/manager/login\">Quản trị<i class=\"fa fa-caret-right\"></i></a>";
                        ltrinfo.Text += "          </section>";
                    }
                    ltrinfo.Text += "          <section class=\"links\">";
                    ltrinfo.Text += "              <a href=\"/thong-tin-nguoi-dung\">Thông tin tài khoản<i class=\"fa fa-caret-right\"></i></a>";
                    ltrinfo.Text += "          </section>";
                    ltrinfo.Text += "          <section class=\"links\">";
                    ltrinfo.Text += "              <a href=\"/danh-sach-don-hang?t=1\">Đơn hàng của bạn<i class=\"fa fa-caret-right\"></i></a>";
                    ltrinfo.Text += "          </section>";
                    ltrinfo.Text += "          <section class=\"links\"><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch<i class=\"fa fa-caret-right\"></i></a></section>";
                    ltrinfo.Text += "      </main>";
                    ltrinfo.Text += "      <footer><a href=\"/dang-xuat\" class=\"btn btn-3\">ĐĂNG XUẤT</a></footer>";
                    ltrinfo.Text += "  </div>";
                    ltrinfo.Text += "</div>";
                    ltrinfo.Text += "  </div>";
                    //ltrinfo.Text += "<span class=\"right-it username\">" + obj_user.Username + "</span>";

                    ltrinfo.Text += "<a href=\"/trang-chu\" class=\"right-it rate\"><span class=\"right-it username\">Trang ngoài</span></a>";
                    ltrinfo.Text += "<a href=\"/dang-xuat\" class=\"right-it logout\"><i class=\"fa fa-sign-out\"></i>Sign out</a>";
                }
                //if (obj_user.RoleID == 0)
                //{

                //}
                int UID = acc.ID;
                var notiadmin = NotificationController.GetByReceivedID(UID);
                //ltrAmountNoti.Text = notiadmin.Count.ToString();
                //if (notiadmin.Count > 0)
                //{
                //    StringBuilder html = new StringBuilder();
                //    foreach (var item in notiadmin)
                //    {
                //        html.Append("<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + item.ID + "','" + item.OrderID + "','2')\">" + item.Message + "</a></li>");
                //    }
                //    ltrNoti.Text = html.ToString();
                //}
                //else
                //{
                //    ltrNoti.Text = "<li role=\"presentation\"><a href=\"javascript:;\">Không có thông báo mới</a></li>";
                //}
            }
        }
    }
}