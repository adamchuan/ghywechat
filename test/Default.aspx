<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView runat="server" ID="gv1">
    </asp:GridView>

    <asp:TextBox runat="server" ID="msg"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="tijiao" Text="tijiao" />
    </div>
    </form>
</body>
</html>
