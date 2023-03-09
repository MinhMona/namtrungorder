using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;

namespace NHST.manager
{
    public partial class add_another_order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                loadPrefix();
                loaddata();
            }
        }
        public void loadPrefix()
        {
            //var listpre = PJUtils.loadprefix();
            //ddlPrefix.Items.Clear();
            //foreach (var item in listpre)
            //{
            //    ListItem listitem = new ListItem(item.dial_code, item.dial_code);
            //    ddlPrefix.Items.Add(listitem);
            //}
            //ddlPrefix.DataBind();
        }
        public void loaddata()
        {
            var uid = Request.QueryString["i"].ToInt(0);
            if (uid > 0)
            {
                var acc = AccountController.GetByID(uid);
                if (acc != null)
                {
                    int id = acc.ID;
                    ViewState["UID"] = id;
                    lblUsername.Text = acc.Username;
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            //try
            //{
            int uid_Khach = ViewState["UID"].ToString().ToInt(0);

            double currency = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
                currency = Convert.ToDouble(config.Currency);
            DateTime currentDate = DateTime.Now;

            string product = hdfProductList.Value;
            int UIDCreate = 0;
            string usaler = Session["userLoginSystem"].ToString();
            var accSaller = AccountController.GetByUsername(usaler);
            var obj_user = AccountController.GetByID(uid_Khach);
            if (obj_user != null)
            {
                string fullname = "";
                string address = "";
                string email = "";
                string phone = "";
                if(accSaller!=null)
                    UIDCreate = accSaller.ID;
                var ai = AccountInfoController.GetByUserID(UIDCreate);
                if (ai != null)
                {
                    fullname = ai.FirstName + " " + ai.LastName;
                    address = ai.Address;
                    email = ai.Email;
                    phone = ai.Phone;
                }
                int salerID = Convert.ToInt32(obj_user.SaleID);
                int dathangID = Convert.ToInt32(obj_user.DathangID);
                int UID = obj_user.ID;
                double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);

                double priceCYN = 0;
                double priceVND = 0;
                string[] products = product.Split('|');
                if (products.Length - 1 > 0)
                {
                    for (int i = 0; i < products.Length - 1; i++)
                    {
                        string[] item = products[i].Split(']');
                        string productlink = item[0];
                        string productname = item[1];
                        string productvariable = item[2];
                        double productquantity = item[3].ToFloat(0);
                        var productnote = item[4];

                        string productimage = "";
                        double productprice = 0;
                        double productpromotionprice = 0;

                        double pricetoPay = 0;

                        if (productpromotionprice <= productprice)
                        {
                            pricetoPay = productpromotionprice;
                        }
                        else
                        {
                            pricetoPay = productprice;
                        }
                        priceCYN += (pricetoPay * productquantity);
                    }
                }
                priceVND = priceCYN * currency;
                double feebpnotdc = 0;
                feebpnotdc = (priceCYN * 3 / 100) * currency;
                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                double feebp = feebpnotdc - subfeebp;
                double TotalPriceVND = (priceCYN * currency) + feebp;
                string AmountDeposit = (TotalPriceVND * LessDeposit / 100).ToString();

                string Deposit = "0";

                string kq = MainOrderController.Insert(UID, "", "", "", false, "0", false, "0", false, "0",
                            false, "0", false, "0", priceVND.ToString(), priceCYN.ToString(), "0", feebp.ToString(),
                            "0", "", fullname, address, email, phone, 0, Deposit,
                            currency.ToString(), TotalPriceVND.ToString(), salerID, dathangID, currentDate,
                            UIDCreate, AmountDeposit, 3);
                int idkq = Convert.ToInt32(kq);
                if (idkq > 0)
                {
                    for (int i = 0; i < products.Length - 1; i++)
                    {
                        string[] item = products[i].Split(']');
                        string productlink = item[0];
                        string productname = item[1];
                        string productvariable = item[2];
                        double productquantity = item[3].ToFloat(0);
                        var productnote = item[4];

                        string productimage = "";
                        double productprice = 0;
                        double productpromotionprice = 0;
                        double pricetoPay = 0;

                        if (productpromotionprice <= productprice)
                        {
                            pricetoPay = productpromotionprice;
                        }
                        else
                        {
                            pricetoPay = productprice;
                        }
                        priceCYN += (pricetoPay * productquantity);

                        int quantity = item[3].ToInt(0);
                        double originprice = 0;
                        double promotionprice = 0;

                        double u_pricecbuy = 0;
                        double u_pricevn = 0;
                        double e_pricebuy = 0;
                        double e_pricevn = 0;
                        if (promotionprice < originprice)
                        {
                            u_pricecbuy = promotionprice;
                            u_pricevn = promotionprice * currency;
                        }
                        else
                        {
                            u_pricecbuy = originprice;
                            u_pricevn = originprice * currency;
                        }

                        e_pricebuy = u_pricecbuy * quantity;
                        e_pricevn = u_pricevn * quantity;



                        string image = productimage;
                        if (image.Contains("%2F"))
                        {
                            image = image.Replace("%2F", "/");
                        }
                        if (image.Contains("%3A"))
                        {
                            image = image.Replace("%3A", ":");
                        }
                        string ret = OrderController.Insert(UID, productname, productname, originprice.ToString(), promotionprice.ToString(), productvariable,
                        productvariable, productvariable, image, image, "", "", "", "", quantity.ToString(),
                        "", "", "", "", "", productlink, "", "", "", "", "", "",
                        "", "0", "Web", "", false, false, "0",
                        false, "0", false, "0", false, "0", false,
                        "0", e_pricevn.ToString(), e_pricebuy.ToString(), productnote, fullname, address, email,
                        phone, 0, "0", currency.ToString(), TotalPriceVND.ToString(), idkq, DateTime.Now, UIDCreate, productlink);

                        if (promotionprice > 0)
                            OrderController.UpdatePricePriceReal(ret.ToInt(0), originprice.ToString(), promotionprice.ToString());
                        else
                            OrderController.UpdatePricePriceReal(ret.ToInt(0), originprice.ToString(), originprice.ToString());
                    }
                    MainOrderController.UpdateReceivePlace(idkq, UID, "4", 1);
                    MainOrderController.UpdateIsCheckNotiPrice(idkq, UID, false);

                    //var admins = AccountController.GetAllByRoleID(0);
                    //if (admins.Count > 0)
                    //{
                    //    foreach (var admin in admins)
                    //    {
                    //        NotificationController.Inser(UID, userBuy, admin.ID,
                    //                                           admin.Username, idkq,
                    //                                           "Có đơn hàng mới ID là: " + idkq, 0,
                    //                                           1, currentDate, userBuy);
                    //    }
                    //}

                    //var managers = AccountController.GetAllByRoleID(2);
                    //if (managers.Count > 0)
                    //{
                    //    foreach (var manager in managers)
                    //    {
                    //        NotificationController.Inser(UID, userBuy, manager.ID,
                    //                                           manager.Username, 0,
                    //                                           "Có đơn hàng mới ID là: " + idkq, 0,
                    //                                           1, currentDate, userBuy);
                    //    }
                    //}
                }
                PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
            }
            //}
            //catch
            //{
            //    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình tạo mới, vui lòng thử lại sau", "e", true, Page);
            //}
        }
    }
}