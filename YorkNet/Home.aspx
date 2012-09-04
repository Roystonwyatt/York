<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="YorkNet.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <a href="#" class="cupid-blue SelectedCupidBlue">Home</a>
    <a href="TelephoneDirectory.aspx" class="cupid-blue small-button">Telephone Directory</a>
    <a href="PoliciesandProcedures.aspx" class="cupid-blue small-button">Policies & procedures</a>
    <br />
    <br />
    </td>
    </tr>
    </table>
                 <table id = "Content" border = "0" cellpadding = "5" cellspacing = "0" width = "90%">
                <tr height = "450">
                  <td width = "33%" valign = "top" style = "border-right-width: medium; border-right-color: Black;border-right-style:solid;">
                      <span class="style1"><strong>News</strong></span><br />
                      <br />
                      <table id = "News" cellpadding = "7" cellspacing = "0" border = "0" width = "100%">
                      <asp:PlaceHolder ID="plcNews" runat="server"></asp:PlaceHolder>
                      </table>
                    </td>
                  <td width = "33%" valign = "top" style = "border-right-width: medium; border-right-color: Black;border-right-style:solid;">
                      <span class="style1"><strong>Policies & Procedures</strong></span><br />
                      <br />
                      <table id = "Policies" cellpadding = "4" cellspacing = "0" border = "0" width = "90%">
                      <asp:PlaceHolder ID="plcPolicies" runat="server"></asp:PlaceHolder>
                      </table>
                    </td>
                  <td width = "34%" valign = "top"><span class="style1"><strong>Events</strong></span><br />
                      <br />
                          <div align = "center"><p align = "center"><asp:Calendar ID="EventsCalendar" 
                                  runat="server" BackColor="White" 
                          BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" 
                          Font-Size="9pt" ForeColor="Black" Height="260px" NextPrevFormat="ShortMonth" 
                          Width="374px" onselectionchanged="EventsCalendar_SelectionChanged">
                              <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
                                  Height="8pt" />
                              <DayStyle BackColor="#CCCCCC" />
                              <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                              <OtherMonthDayStyle ForeColor="#999999" />
                              <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                              <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" 
                                  Font-Size="12pt" ForeColor="White" Height="12pt" />
                              <TodayDayStyle BackColor="#999999" ForeColor="White" />
                      </asp:Calendar>
                      <br />
                              <asp:Panel ID="pnlEventDetails" runat="server" BorderStyle="Solid"  BorderWidth="2px"
                                  Height="180px" ScrollBars="Vertical" Width = "365px">
                              <Table id = "Events" cellpadding = "5" cellspacing = "0" border = "0" width = "90%">
                                  <asp:PlaceHolder ID="plcEvents" runat="server"></asp:PlaceHolder>
                              </Table>  
                              </asp:Panel>
                      </p></div>
                    </td>
                </tr>
                </table>

    </div>
        <script language="javascript" type="text/javascript">
            //Center greybox window
            function OpenPolicyWindow(id) {
                var caption = "Policy Document";
                var url = "../ProcedureDetails.aspx?id="+id;
                return GB_showCenter(caption, url, 600, 800)
            }

            function OpenNewsWindow(id) {
                var caption = "News Detail";
                var url = "../NewsDetail.aspx?id="+id;
                return GB_showCenter(caption, url, 600, 800)
            }
    </script>
    </form>
</body>
</html>
