using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class AddTafiffTQVN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0)
                        {
                            Response.Redirect("/manager/Tariff-TQVN.aspx");
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();

            string ReceivePlace = ddlReceivePlace.SelectedValue;
            double WeightFrom = pWeightFrom.Value.ToString().ToFloat(0);
            double WeightTo = pWeightTo.Value.ToString().ToFloat(0);
            double Amount = pAmount.Value.ToString().ToFloat(0);

            var check = FeeWeightTQVNController.GetByWeightAndRecivePlaceAndAmount(WeightFrom, WeightTo, ReceivePlace, Amount);
            if (check != null)
            {
                PJUtils.ShowMessageBoxSwAlert("Chi phí đã tồn tại, vui lòng xem lại", "e", false, Page);
            }
            else
            {
                string kq = FeeWeightTQVNController.Insert(ReceivePlace, WeightFrom, WeightTo, Amount, DateTime.Now, Username);
                if (kq.ToInt(0) > 0)
                    PJUtils.ShowMessageBoxSwAlert("Tạo mới chi phí thành công", "s", true, Page);
            }
        }
    }
}