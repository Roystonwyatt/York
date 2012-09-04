<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcedureDetails.aspx.cs" Inherits="YorkNet.ProcedureDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div align = "center">
    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:Literal ID="PolicyLink" runat="server"></asp:Literal>
        <br />
        <br />
        <asp:Literal ID="HTMLDetails" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
