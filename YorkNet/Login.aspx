<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YorkNet.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to YorkNet</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <div align = "center"><br /><br /><br /><br /><br />
        <table id = "Login" cellpadding = "4" cellspacing = "0" border = "0" width = "30%">
        <tr>
        <td width = "50%">Username:</td><td align = "right"><asp:TextBox ID="txtUsername" runat="server" 
                width = "200px"></asp:TextBox></td><td valign = "top"><asp:RequiredFieldValidator
                        ID="fvUserName" runat="server" 
                        ErrorMessage="<img src = './images/Vali1.gif' />" 
                        ControlToValidate="txtUserName" ToolTip="Please provide a username"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
        <td>Password:</td><td align = "right"><asp:TextBox ID="txtPassword" runat="server" TextMode = "Password" width = "200px"></asp:TextBox>
                        </td><td valign = "top"><asp:RequiredFieldValidator
                        ID="fvPassword" runat="server" 
                        ErrorMessage="<img src = './images/Vali1.gif' />" 
                        ControlToValidate="txtPassword" ToolTip="Please provide a password"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td colspan = "2" align = "right">
            <asp:Button ID="btnLogin" runat="server" 
                Text="Log On" onclick="btnLogin_Click" /></td>
        </tr>
        <tr>
        <td colspan = "3" align = "center">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        </table>
        </div>
    </form>
</body>
</html>
