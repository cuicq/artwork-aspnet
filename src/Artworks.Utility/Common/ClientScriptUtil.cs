using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Artworks.Utility.Common
{
    /// <summary>
    /// 客户端脚本。
    /// </summary>
    public class ClientScriptUtil
    {
        public static void Alert(string message)
        {
            Regist("    alert('" + message + "');");
        }

        public static void AlertAndCloseThisOpenWindow(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("    alert('{0}');\n", message);
            builder.AppendLine("    window.opener.reload();");
            builder.AppendLine("    window.close();");
            Regist(builder.ToString());
        }

        public static void AlertAndGoBack(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("    alert('{0}');\n", message);
            builder.AppendLine("    window.history.back();");
            Regist(builder.ToString());
            HttpContext.Current.Response.End();
        }

        public static void AlertAndRedirect(string message, string url)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("    alert('{0}');\n", message);
            builder.AppendFormat("    window.location.href='{0}'\n", url);
            Regist(builder.ToString());
            HttpContext.Current.Response.End();
        }

        public static void AlertAndReload(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("    alert('{0}');\n", message);
            builder.AppendLine("    window.location.reload();");
            Regist(builder.ToString());
            HttpContext.Current.Response.End();
        }

        public static void GoBack()
        {
            Regist("    window.history.back();");
            HttpContext.Current.Response.End();
        }

        public static void ParentPageRedirect(string url)
        {
            Regist("    parent.location.href='" + url + "';");
            HttpContext.Current.Response.End();
        }

        public static void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }

        public static void Regist(string script)
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\" type=\"text/javascript\" defer>\n" + script + "\n</script>\n");
        }
    }


}
