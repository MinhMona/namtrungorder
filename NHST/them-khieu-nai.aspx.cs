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
using Telerik.Web.UI;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;

namespace NHST
{
    public partial class them_khieu_nai1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "admin";
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
            if (RouteData.Values["id"] != null)
            {
                int MainOrderID = RouteData.Values["id"].ToString().ToInt(0);
                if (MainOrderID > 0)
                {
                    string username = Session["userLoginSystem"].ToString();
                    var u = AccountController.GetByUsername(username);
                    if (u != null)
                    {
                        int UID = u.ID;

                        var mainorder = MainOrderController.GetAllByUIDAndID(UID, MainOrderID);
                        if (mainorder != null)
                        {
                            txtOrderID.Text = MainOrderID.ToString();
                        }
                    }
                }
            }
            else
                Response.Redirect("/danh-sach-don-hang");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int orderid = txtOrderID.Text.ToInt(0);
            DateTime currentDate = DateTime.Now;
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                var shops = MainOrderController.GetAllByUIDAndID(UID, orderid);
                if (shops != null)
                {
                    string IMG = "";
                    string KhieuNaiIMG = "/Uploads/KhieuNaiIMG/";
                    if (hinhDaiDien.UploadedFiles.Count > 0)
                    {
                        foreach (UploadedFile f in hinhDaiDien.UploadedFiles)
                        {
                            if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                            {
                                if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                                {
                                    var o = KhieuNaiIMG + Guid.NewGuid() + f.GetExtension();
                                    try
                                    {
                                        f.SaveAs(Server.MapPath(o));
                                        IMG += o + "|";
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    string kq = ComplainController.InsertNew(UID, orderid, pAmount.Value.ToString(), IMG,
                        txtNote.Text, UserNote.Text, 1, ddlComplainType.SelectedValue, DateTime.Now, username);
                    if (kq.ToInt(0) > 0)
                    {
                        OrderCommentController.Insert(orderid, "Bạn vừa tạo 1 khiếu nại", true, 1, DateTime.Now, u.ID,3);

                        var o = MainOrderController.GetAllByUIDAndID(UID, orderid);
                        if (o != null)
                        {
                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    NotificationController.Inser(u.ID, u.Username, admin.ID,
                                                                       admin.Username, orderid,
                                                                       "Đã có khiếu nại mới cho đơn hàng #" + orderid + ". CLick vào để xem", 0, 5,
                                                                       currentDate, u.Username, false);
                                }
                            }

                            var managers = AccountController.GetAllByRoleID(2);
                            if (managers.Count > 0)
                            {
                                foreach (var manager in managers)
                                {
                                    NotificationController.Inser(u.ID, u.Username, manager.ID,
                                                                       manager.Username, orderid,
                                                                       "Đã có khiếu nại mới cho đơn hàng #" + orderid + ". CLick vào để xem", 0, 5,
                                                                       currentDate, u.Username, false);
                                }
                            }

                        }

                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "signalRNow()", true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>signalRNow();</script>", false);

                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                        hubContext.Clients.All.addNewMessageToPage("", "");

                        PJUtils.ShowMessageBoxSwAlert("Tạo khiếu nại thành công", "s", true, Page);
                    }
                }
                else
                {
                    lblError.Text = "Không tìm thấy đơn hàng";
                    lblError.Visible = true;

                    //PJUtils.ShowMessageBoxSwAlert("Không tìm thấy đơn hàng", "e", true, Page);
                }
            }
        }
    }
}