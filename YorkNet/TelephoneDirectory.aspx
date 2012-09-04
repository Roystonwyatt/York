<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelephoneDirectory.aspx.cs" Inherits="YorkNet.TelephoneDirectory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome to Yorknet</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />    
    <link href="~/greybox/gb_styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
    <script language = "javascript" type = "text/jscript">
    <!--
        function Openwindow() {
            window.open('http://www.google.com/', 'open_window', 'menubar, toolbar, location, directories, status, scrollbars, resizable, dependent, width=640, height=480, left=0, top=0');
        }

    -->
    </script>
    <script type="text/javascript">
        var GB_ROOT_DIR = '<%= this.ResolveClientUrl("~/greybox/")%>';
    </script>
    <%--Include grybox javascript files--%>
    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS_fx.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/gb_scripts.js") %>'></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align = "center">
    <table id = "HeaderBanner" border = "0" width = "90%" cellpadding = "5" cellspacing = "0">
    <tr height = "100">
    <td align = "center" width = "70%">Yorknet Banner goes here</td>
    <td>
    <table id = "LoginTable" cellpadding = "0" cellspacing = "0" border = "0" width = "100%">
    <tr><td><b>Sign in:</b></td><td></td></tr>
    <tr><td>Username:</td><td align = "left"><asp:TextBox ID="txtUserName" runat="server" Width = "160px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="fvUserName" runat="server" 
                        ErrorMessage="<img src = './images/Vali1.gif' />" 
                        ControlToValidate="txtUserName" ToolTip="Please provide a username"></asp:RequiredFieldValidator></td></tr>
    <tr><td>Password:</td><td align = "left"><asp:TextBox ID="txtPassword" runat="server" TextMode = "Password" Width = "160px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="fvPassword" runat="server" 
                        ErrorMessage="<img src = './images/Vali1.gif' />" 
                        ControlToValidate="txtPassword" ToolTip="Please provide a password"></asp:RequiredFieldValidator></td></tr>
    <tr><td></td><td align = "left"><asp:Button ID="btnSignIn" runat="server" 
            Text="Sign in" onclick="btnSignIn_Click" /></td></tr>
    </table>
    </td>
    </tr>
    </table>
        <asp:Label ID="lblerror" runat="server" Font-Bold="True" Font-Size="Smaller" 
            ForeColor="Red"></asp:Label>
    <hr width = "90%" />
    <table id = "Menu" cellpadding = "0" cellspacing = "0" border = "0" width = "90%">
    <tr>
    <td width = "100%" align = "left">
    <a href="Home.aspx" class="cupid-blue small-button">Home</a>
    <a href="#" class="cupid-blue SelectedCupidBlue">Telephone Directory</a>
    <a href="PoliciesandProcedures.aspx" class="cupid-blue small-button">Policies & procedures</a>
    <br />
    <br />
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
