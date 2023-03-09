using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class dang_xuat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                ViewState["UID"] = UID;

                #region Load Lịch sử nạp tiền
                //var notis = NotificationController.GetAllByReceivedID(UID);
                //if (notis.Count > 0)
                //{
                //    foreach (var item in notis)
                //    {
                //        NotificationController.UpdateStatus(item.ID, 1, DateTime.Now, username);
                //    }
                //}
                NotificationController.UpdateStatus_SQL(username, 1);
                #endregion
            }
            Session.Abandon();
            Response.Redirect("/dang-nhap");
        }
    }
}