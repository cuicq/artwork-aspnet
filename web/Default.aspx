<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form method="post" action="UploadPassport.ashx?free_infoid=236541" id="form1" enctype="multipart/form-data" target="_blank">

    <input type="file" name="FileUpload1" id="FileUpload1" /><br />
    <input type="file" name="FileUpload1" id="FileUpload2" />
    <input type="submit" value="上传" />
    </form>
</body>
</body>
</html>
