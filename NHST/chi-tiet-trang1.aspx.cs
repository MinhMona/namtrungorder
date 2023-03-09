using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class chi_tiet_trang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenu();
                loadData();

            }
        }
        public void LoadMenu()
        {
            var pagetypeid = RouteData.Values["pagetypeid"].ToString().ToInt(0);
            var pageid = RouteData.Values["pageid"].ToString().ToInt(0);
            var pt = PageTypeController.GetAll();
            if (pt != null)
            {
                if (pt.Count > 0)
                {
                    foreach (var item in pt)
                    {
                        if (pagetypeid == item.ID)
                        {
                            ltrMenu.Text += "<div class=\"panel on\">";
                            ltrMenu.Text += "   <div class=\"panel-heading\">" + item.PageTypeName + "<div class=\"indicator right\"></div></div>";
                            ltrMenu.Text += "   <div class=\"panel-body\" style=\"display:block\">";
                        }
                        else
                        {
                            ltrMenu.Text += "<div class=\"panel\">";
                            ltrMenu.Text += "   <div class=\"panel-heading\">" + item.PageTypeName + "<div class=\"indicator right\"></div></div>";
                            ltrMenu.Text += "   <div class=\"panel-body\">";
                        }


                        ltrMenu.Text += "       <ul class=\"side-nav-ul\">";
                        var ps = PageController.GetByPagetTypeID(item.ID);
                        if (ps != null)
                        {
                            if (ps.Count > 0)
                            {
                                foreach (var p in ps)
                                {
                                    if (p.ID == pageid)
                                        ltrMenu.Text += "<li class=\"active\"><a href=\"/" + LeoUtils.ConvertToUnSign(item.PageTypeName) + "-" + item.ID + "/" + LeoUtils.ConvertToUnSign(p.Title) + "-" + p.ID + "\">" + p.Title + "</a></li>";
                                    else
                                        ltrMenu.Text += "<li><a href=\"/" + LeoUtils.ConvertToUnSign(item.PageTypeName) + "-" + item.ID + "/" + LeoUtils.ConvertToUnSign(p.Title) + "-" + p.ID + "\">" + p.Title + "</a></li>";
                                }
                            }
                        }
                        ltrMenu.Text += "       </ul>";
                        ltrMenu.Text += "   </div>";
                        ltrMenu.Text += "</div>";
                    }
                }
            }
        }
        public void loadData()
        {
            try
            {

                var pagetypeid = RouteData.Values["pagetypeid"].ToString().ToInt(0);
                var pageid = RouteData.Values["pageid"].ToString().ToInt(0);

                string pagetypename = "";
                string link = "";
                string pagename = "";
                if (pagetypeid > 0)
                {
                    var pt = PageTypeController.GetByID(pagetypeid);
                    if (pt != null)
                    {
                        pagetypename = pt.PageTypeName;
                        link += "/" + LeoUtils.ConvertToUnSign(pagetypename) + "-" + pt.ID;
                    }
                }

                ltrBrea.Text += "<ul>";
                ltrBrea.Text += "   <li><a href=\"/trang-chu\">Trang chủ</a></li>";
                ltrBrea.Text += "   <li>-</li>";
                ltrBrea.Text += "   <li><a href=\"javascript:;\">" + pagetypename + "</a></li>";
                ltrBrea.Text += "   <li>-</li>";
                if (pageid > 0)
                {
                    var p = PageController.GetByID(pageid);
                    if (p != null)
                    {
                        link += "/" + LeoUtils.ConvertToUnSign(p.Title) + "-" + p.ID;
                        lblTitle.Text = p.Title;
                        ltrContent.Text = p.PageContent;
                        pagename = p.Title;

                        string path = HttpContext.Current.Request.Url.AbsolutePath;

                        string weblink = "https://vanchuyendaquocgia.com";

                        HtmlHead objHeader = (HtmlHead)Page.Header;

                        //we add meta description
                        HtmlMeta objMetaFacebook = new HtmlMeta();

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "fb:app_id");
                        objMetaFacebook.Content = "676758839172144";
                        objHeader.Controls.Add(objMetaFacebook);

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "og:url");
                        objMetaFacebook.Content = weblink + path;
                        objHeader.Controls.Add(objMetaFacebook);

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "og:type");
                        objMetaFacebook.Content = "website";
                        objHeader.Controls.Add(objMetaFacebook);

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "og:title");
                        objMetaFacebook.Content = p.Title;
                        objHeader.Controls.Add(objMetaFacebook);

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "og:description");
                        objMetaFacebook.Content = PJUtils.SubString(PJUtils.RemoveHTMLTags(p.PageContent), 150);
                        objHeader.Controls.Add(objMetaFacebook);

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "og:image");
                        objMetaFacebook.Content = weblink + "/App_Themes/vcdqg/images/main-logo.png";
                        objHeader.Controls.Add(objMetaFacebook);

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "og:image:width");
                        objMetaFacebook.Content = "200";
                        objHeader.Controls.Add(objMetaFacebook);

                        objMetaFacebook = new HtmlMeta();
                        objMetaFacebook.Attributes.Add("property", "og:image:height");
                        objMetaFacebook.Content = "500";
                        objHeader.Controls.Add(objMetaFacebook);
                    }
                }
                ltrBrea.Text += "   <li class=\"current\"><a href=\"" + link + "\">" + pagename + "</a></li>";
                ltrBrea.Text += "</ul>";
            }
            catch
            {
                Response.Redirect("/trang-chu");
            }
        }
    }
}