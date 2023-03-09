using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNet.SignalR;
using NHST.Controllers;

namespace NHST.Hubs
{
    public class ChatHub : Hub
    {
        public void Login(string ID, string LoginStatus)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            // Call the broadcastMessage method to update clients.
            context.Clients.All.broadcastLogin(ID, LoginStatus);
        }

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void SendMessengerToStaff(int uid, int id, string comment, List<string> link, List<string> realname)
        {
            var ac = AccountInfoController.GetByUserID(uid);
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            StringBuilder showIMG = new StringBuilder();
            if (!string.IsNullOrEmpty(comment))
            {
                showIMG.Append("<div class=\"mess-item\">");
                showIMG.Append("<div class=\"img\"><img src=\"" + ac.IMGUser + "\"/></div>");
                showIMG.Append("<div class=\"cont\">");
                showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</p>");
                if (!string.IsNullOrEmpty(comment))
                {
                    showIMG.Append("<p>" + comment + "</p>");
                }
                showIMG.Append("</div>");
                showIMG.Append("</div>");
            }
            for (int i = 0; i < link.Count; i++)
            {
                showIMG.Append("<div class=\"mess-item\">");
                showIMG.Append("<div class=\"img\"><img src=\"" + ac.IMGUser + "\"/></div>");
                showIMG.Append("<div class=\"cont\">");
                showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</p>");
                if (!string.IsNullOrEmpty(comment))
                {
                    showIMG.Append("<p>" + comment + "</p>");
                }
                if (!string.IsNullOrEmpty(link[i]))
                    showIMG.Append("<p><a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"" + link[i] + "\" /></a></p>");
                showIMG.Append("</div>");
                showIMG.Append("</div>");
            }
           
            string message = showIMG.ToString();
            context.Clients.All.broadcastMessageForLocal(uid, id, message);

        }
        public void SendMessengerToCustomer(int uid, int id, string comment, List<string> link, List<string> realname)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

            StringBuilder showIMG = new StringBuilder();
            if (!string.IsNullOrEmpty(comment))
            {
                showIMG.Append("   <span class=\"user-comment \">" + AccountInfoController.GetByUserID(uid).FirstName + " " + AccountInfoController.GetByUserID(uid).LastName + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", DateTime.Now) + "]</b> : <span class=\"green\">" + comment + "</span><br>");
            }
            for (int i = 0; i < link.Count; i++)
            {

                if (!string.IsNullOrEmpty(link[i]))
                {
                    showIMG.Append("       <span class=\"user-comment\">" + AccountInfoController.GetByUserID(uid).FirstName + " " + AccountInfoController.GetByUserID(uid).LastName + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", DateTime.Now) + "]</b> :<br/> <a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"" + link[i] + "\"/></a><br>");
                }
            }

            string message = showIMG.ToString();
            context.Clients.All.broadcastMessage(uid, id, message);
        }
        public void SendMessenger(int uid, int id, string comment, List<string> link, List<string> realname)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            var ac = AccountInfoController.GetByUserID(uid);
            StringBuilder showIMG = new StringBuilder();
            if (!string.IsNullOrEmpty(comment))
            {
                showIMG.Append("<div class=\"mess-item\">");
                showIMG.Append("<div class=\"img\"><img src=\"" + ac.IMGUser + "\"/></div>");
                showIMG.Append("<div class=\"cont\">");
                showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</p>");
                if (!string.IsNullOrEmpty(comment))
                {
                    showIMG.Append("<p>" + comment + "</p>");
                }                
                showIMG.Append("</div>");
                showIMG.Append("</div>");
            }
            for (int i = 0; i < link.Count; i++)
            {                
                 showIMG.Append("<div class=\"mess-item\">");
                showIMG.Append("<div class=\"img\"><img src=\"" + ac.IMGUser + "\"/></div>");
                showIMG.Append("<div class=\"cont\">");
                showIMG.Append("<p class=\"\"><strong class=\"username\">" + AccountController.GetByID(uid).Username + "</strong>" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</p>");
                if (!string.IsNullOrEmpty(comment))
                {
                    showIMG.Append("<p>" + comment + "</p>");
                }
                if (!string.IsNullOrEmpty(link[i]))
                    showIMG.Append("<p><a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"" + link[i] + "\" /></a></p>");
                showIMG.Append("</div>");
                showIMG.Append("</div>");
            }
            
            string message = showIMG.ToString();
            context.Clients.All.broadcastMessageFromUser(uid, id, message);
        }
        public void SendMessengerTrans(int uid, int id, string comment, List<string> link, List<string> realname)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            StringBuilder showIMG = new StringBuilder();
            for (int i = 0; i < link.Count; i++)
            {
                showIMG.Append("<div class=\"chat\">");
                showIMG.Append("    <div class=\"chat-avatar\">");
                showIMG.Append("    <p class=\"name\">" + AccountController.GetByID(uid).Username + "</p>");
                showIMG.Append("    </div>");
                showIMG.Append("    <div class=\"chat-body\">");
                showIMG.Append("        <div class=\"chat-text\">");
                showIMG.Append("            <div class=\"date-time center-align\">" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</div>");
                showIMG.Append("            <div class=\"text-content\">");
                showIMG.Append("                <div class=\"content\">");
                showIMG.Append("                    <div class=\"content-img\">");
                showIMG.Append("	                    <div class=\"img-block\">");
                if (link[i].Contains(".doc"))
                {
                    showIMG.Append("<a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"/App_Themes/UserNew45/assets/images/icon/file.png\" title=\"" + realname[i] + "\"  class=\"\" height=\"50\"/></a>");

                }
                else if (link[i].Contains(".xls"))
                {
                    showIMG.Append("<a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"/App_Themes/UserNew45/assets/images/icon/file.png\" title=\"" + realname[i] + "\"  class=\"\" height=\"50\"/></a>");
                }
                else
                {
                    showIMG.Append("<a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"" + link[i] + "\" title=\"" + realname[i] + "\"  class=\"\" height=\"50\"/></a>");
                }
                showIMG.Append("	                    </div>");
                showIMG.Append("                    </div>");
                showIMG.Append("                </div>");
                showIMG.Append("            </div>");
                showIMG.Append("        </div>");
                showIMG.Append("    </div>");
                showIMG.Append("</div>");
            }
            if (!string.IsNullOrEmpty(comment))
            {
                showIMG.Append("<div class=\"chat\">");
                showIMG.Append("    <div class=\"chat-avatar\">");
                showIMG.Append("    <p class=\"name\">" + AccountController.GetByID(uid).Username + "</p>");
                showIMG.Append("    </div>");
                showIMG.Append("    <div class=\"chat-body\">");
                showIMG.Append("        <div class=\"chat-text\">");
                showIMG.Append("            <div class=\"date-time center-align\">" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</div>");
                showIMG.Append("            <div class=\"text-content\">");
                showIMG.Append("                <div class=\"content\">");
                showIMG.Append("                    <p>" + comment + "</p>");
                showIMG.Append("                </div>");
                showIMG.Append("            </div>");
                showIMG.Append("        </div>");
                showIMG.Append("    </div>");
                showIMG.Append("</div>");
            }
            string message = showIMG.ToString();
            context.Clients.All.broadcastMessageTrans(uid, id, message);
        }
        public void SendMessengerPayHelp(int uid, int id, string comment, List<string> link, List<string> realname)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            StringBuilder showIMG = new StringBuilder();
            for (int i = 0; i < link.Count; i++)
            {
                showIMG.Append("<div class=\"chat\">");
                showIMG.Append("    <div class=\"chat-avatar\">");
                showIMG.Append("    <p class=\"name\">" + AccountController.GetByID(uid).Username + "</p>");
                showIMG.Append("    </div>");
                showIMG.Append("    <div class=\"chat-body\">");
                showIMG.Append("        <div class=\"chat-text\">");
                showIMG.Append("            <div class=\"date-time center-align\">" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</div>");
                showIMG.Append("            <div class=\"text-content\">");
                showIMG.Append("                <div class=\"content\">");
                showIMG.Append("                    <div class=\"content-img\">");
                showIMG.Append("	                    <div class=\"img-block\">");
                if (link[i].Contains(".doc"))
                {
                    showIMG.Append("<a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"/App_Themes/UserNew45/assets/images/icon/file.png\" title=\"" + realname[i] + "\"  class=\"\" height=\"50\"/></a>");

                }
                else if (link[i].Contains(".xls"))
                {
                    showIMG.Append("<a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"/App_Themes/UserNew45/assets/images/icon/file.png\" title=\"" + realname[i] + "\"  class=\"\" height=\"50\"/></a>");
                }
                else
                {
                    showIMG.Append("<a href=\"" + link[i] + "\" target=\"_blank\"><img src=\"" + link[i] + "\" title=\"" + realname[i] + "\"  class=\"\" height=\"50\"/></a>");
                }
                showIMG.Append("	                    </div>");
                showIMG.Append("                    </div>");
                showIMG.Append("                </div>");
                showIMG.Append("            </div>");
                showIMG.Append("        </div>");
                showIMG.Append("    </div>");
                showIMG.Append("</div>");
            }
            if (!string.IsNullOrEmpty(comment))
            {
                showIMG.Append("<div class=\"chat\">");
                showIMG.Append("    <div class=\"chat-avatar\">");
                showIMG.Append("    <p class=\"name\">" + AccountController.GetByID(uid).Username + "</p>");
                showIMG.Append("    </div>");
                showIMG.Append("    <div class=\"chat-body\">");
                showIMG.Append("        <div class=\"chat-text\">");
                showIMG.Append("            <div class=\"date-time center-align\">" + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + "</div>");
                showIMG.Append("            <div class=\"text-content\">");
                showIMG.Append("                <div class=\"content\">");
                showIMG.Append("                    <p>" + comment + "</p>");
                showIMG.Append("                </div>");
                showIMG.Append("            </div>");
                showIMG.Append("        </div>");
                showIMG.Append("    </div>");
                showIMG.Append("</div>");
            }
            string message = showIMG.ToString();
            context.Clients.All.broadcastMessagePayHelp(uid, id, message);
        }
    }
}