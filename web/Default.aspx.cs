using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // List<Auto.Picture.Compress.Config.UpLoadArgs> list = Auto.Picture.Compress.Config.UpLoadArgsFactory.CreateUpLoadArgs(typeof(CollectingImage));
        //string str = Server.MapPath("~/UploadArgsConfig");
        //int a = 6;
        
        Response.Write(Che168.Utils.Tools.GetCookie("clubUserShow"));
        Response.Write(Che168.Utils.Tools.GetCookie("pcpopclub"));
    }

    
}