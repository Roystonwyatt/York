<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="YorkNet.AdminTelephoneDirectory" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to YorkNet</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align = "center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         <script type="text/javascript">
             // Add click handlers for buttons to show and hide modal popup on pageLoad
             function pageLoad() {
                 $addHandler($get("Button5"), 'click', showModalPopupViaClient);
                 $addHandler($get("Button6"), 'click', showModalPopupLocations);
                 $addHandler($get("Button1"), 'click', showModalPopupAddLocations);
             }

             function showModalPopupViaClient(ev) {
                 ev.preventDefault();
                 var modalPopupBehavior = $find('programmaticModalException');
                 modalPopupBehavior.show();
             }

             function showModalPopupLocations(ev) {
                 ev.preventDefault();
                 var modalPopupBehavior = $find('programmaticModalLocations');
                 modalPopupBehavior.show();
             }

             function showModalPopupAddLocations(ev) {
                 ev.preventDefault();
                 var modalPopupBehavior = $find('programmaticModalAddLocations');
                 modalPopupBehavior.show();
             }
    </script>
    <span style = "font-family: Verdana;font-size:medium;">
        <br />
        <strong>Admin<br />
        </strong></span>  
          
        <table id = "tCtr" cellpadding = "0" cellspacing = "0" border = "0" width = "80%">
        <tr><td align = "left">
            <table id = "ctr" cellpadding = "0" cellspacing = "0" border = "0" width = "100%">
            <tr height = 50>
              <td align = "left"><asp:Label ID="lblFullName" runat="server" Font-Bold="True" ForeColor="Black" Font-Size="X-Small"></asp:Label></td>
              <td align = "right">
                 &nbsp;&nbsp;&nbsp;
                  <a href = "Home.aspx">Log Off</a></td>
             </tr>
            </table>
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" 
                Width = "100%">
                 <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel2" Height = "500px">
                    <HeaderTemplate>
                        News
                    </HeaderTemplate>
                </asp:TabPanel>
               <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="TabPanel6" Height = "500px">
                <HeaderTemplate>
                    Locations
                </HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <asp:Button ID="btnAddLocation" runat="server" Text="✚ Add Location"  
                        CssClass = "cupid-blue small-button" onclick="btnAddLocation_Click" />
                    <br />
                    <br />
                    <asp:Panel ID="pnlTabLocations" runat="server" Width = "100%" 
                        BorderStyle="Solid" BorderWidth="2px" Height = "420px" >
                        <br />
                        <div align = "center"><table id = "ctr2" cellspacing = "0" cellpadding = "7" border = "0" width = "95%">
                        <tr><td>
                        <asp:TreeView ID="tvTabLocations" runat="server" ImageSet="Contacts" 
                            NodeIndent="10" Width="407px"
                                onselectednodechanged="tvTabLocations_SelectedNodeChanged">
                            <HoverNodeStyle Font-Underline="False" />
                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                            <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" BackColor = "Silver"
                                VerticalPadding="0px" />
                        </asp:TreeView>
                        </td></tr>
                        </table>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Height = "500px">
            <HeaderTemplate>
                Telephone Directory
            </HeaderTemplate>
             <ContentTemplate>
               <br />
                    <asp:Button ID="Button2" runat="server" Text="✚ Add User"  
                        CssClass = "cupid-blue small-button"  />
                    <br />
                    <br />
              <asp:Panel ID="Panel2" runat="server" Width = "100%" 
                        BorderStyle="Solid" BorderWidth="2px" Height = "420px" >
                        <br />
             <table id = "container" cellpadding = "5" cellspacing = "0" border = "0" width = "100%">
                <tr height = "40">
                <td width = "20%" align = "right">Name:</td><td width = "30%" align = "left" 
                        valign="middle">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>&nbsp;</td>
                <td width = "20%" align = "right" valign = "top">File to upload:</td><td width = "30%" align = "left" rowspan = "2" valign = "top">
                   <asp:FileUpload ID="fluPhoto" runat="server" CssClass="myEditableTexts" 
                        Width="80%" />&nbsp;<asp:LinkButton ID="btnPreview" runat="server" 
                        onclick="btnPreview_Click">Preview</asp:LinkButton>
                    </td>
                </tr>
                <tr height = "40">
                <td width = "20%" align = "right">Surname:</td><td width = "30%" align = "left" 
                        valign="middle">
                    <asp:TextBox ID="txtSurname" runat="server" Width="200px"></asp:TextBox>&nbsp;</td>
                </tr>
                <tr height = "40">
                <td width = "20%" align = "right">Division:</td><td width = "30%" align = "left">
                    <asp:DropDownList ID="ddDivision" runat="server" onload="ddDivision_Load">
                    </asp:DropDownList>
                    </td>
                <td width = "20%" align = "right">&nbsp;</td>
                    <td width = "30%" align = "left" 
                        rowspan = "7">
                    <asp:Image ID="imgPhoto" runat="server" Height="250px" Width="250px" />
                    </td>
                </tr>
                <tr height = "40">
                <td width = "20%" align = "right" valign="top">Mobile No.:</td><td width = "30%" 
                        align = "left" valign="middle">
                    <asp:TextBox ID="txtMobile" runat="server" Width="200px" Height="74px" 
                        TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr height = "40">
                <td width = "20%" align = "right" valign="top">Email Address:</td><td width = "30%" align = "left">
                    <asp:TextBox ID="txtEmail" runat="server" Width="258px" Height="74px" 
                        TextMode="MultiLine"></asp:TextBox>&nbsp;</td>
                </tr>
    
                 <tr>
                     <td align="right" width="20%">
                         &nbsp;</td>
                     <td align="left" width="30%">
                         <asp:Button ID="btnTelephoneNumbers" runat="server" OnClick="btnTelephoneNumbers_Click" 
                             Text="Add Telephone No." Enabled = "false"/>
                         &nbsp;<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" 
                             Text="Submit" />
                     </td>
                 </tr>
    
                 <tr>
                     <td align="right" width="20%">
                         &nbsp;</td>
                     <td align="left" width="30%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td align="right" width="20%">
                         &nbsp;</td>
                     <td align="left" width="30%">
                         &nbsp;</td>
                 </tr>
    
                </table>
                </asp:Panel>
                </ContentTemplate>
        </asp:TabPanel>
         <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" Height = "500px">
            <HeaderTemplate>
                Policies &amp; Procedures
            </HeaderTemplate>
        </asp:TabPanel>
        
         <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel2" Height = "500px">
            <HeaderTemplate>
                Meeting Rooms
            </HeaderTemplate>
        </asp:TabPanel>
         <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel2" Height = "500px">
            <HeaderTemplate>
                Links
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    </td></tr>
    </table>
    </div>
      <!--Modal Popup below to stop user-->
    <!--Button used below to hide the warning dialog modal popup on startup-->
        <asp:Button ID="Button5" runat="server" Text="Button" style ="display:none;"/>
        <asp:ModalPopupExtender id="mdlExceptions" runat="server" DropShadow="False" 
    BackgroundCssClass="modalBackground" BehaviorID="programmaticModalException" 
    PopupControlID="pnlExceptions" TargetControlID = "Button5" CancelControlID = "btnExceptOK">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlExceptions" runat="server" CssClass="modalPopup" style="width:500px;padding:10px;display:none;" >
                <table align="center" class="contenttable" style="width: 500px">
                    <tr>
                        <td align = "center">
                            <table id = "ProjectInfo" cellpadding = "4" cellspacing = "0" width = "100%" border = "0">
                                <tr>
                                    <td width = "100%" align = "center">
                                        <asp:Label ID="lblError" runat="server" Font-Bold="False" Font-Size="Small" 
                                            ForeColor="Black"></asp:Label>
                                     </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align = "center">
                            <br />
                            <asp:Button ID="btnExceptOK" runat="server" Text="OK" CssClass = "myButtons"  />
                        </td>
                    </tr>
                </table>
        </asp:Panel>
          <!--Modal Popup to load the locations-->
          <asp:Button ID="Button6" runat="server" Text="Button" style ="display:none;"/>
        <asp:ModalPopupExtender id="mdlLocations" runat="server" DropShadow="False" 
    BackgroundCssClass="modalBackground" BehaviorID="programmaticModalLocations" 
    PopupControlID="pnlLocations" TargetControlID = "Button6" CancelControlID = "btnClose">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlLocations" runat="server" CssClass="modalPopup" style="width:500px;padding:10px;display:none;" >
                <table align="center" class="contenttable" style="width: 500px">
                    <tr>
                        <td align = "left">
                            <asp:Panel ID="Panel1" runat="server" Width = "100%" ScrollBars = "Vertical">
                            <asp:TreeView ID="tvLocations" runat="server" BorderStyle = "Solid" 
                                    BorderWidth = "2px" Height = "300px" Width = "95%" imageSet="Contacts" 
                            NodeIndent="10" onselectednodechanged="tvLocations_SelectedNodeChanged">
                            <HoverNodeStyle Font-Underline="False" />
                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                            <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" BackColor = "Silver"
                                VerticalPadding="0px" />
                        </asp:TreeView>
                        </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align = "Left">
                            <br />
                            <asp:TextBox ID="txtExtensions" runat="server" Width = "300px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClose" runat="server" Text="Cancel"  />&nbsp;&nbsp;
                            <asp:Button ID="btnSubmitExtension" runat="server" Text="Submit" 
                                onclick="btnSubmitExtension_Click"   />
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Label ID="lblUserError" runat="server" Text="" Font-Size = "X-Small" Font-Bold = "true" ForeColor = "Red"></asp:Label>
                    </td>
                    </tr>
                </table>
        </asp:Panel>
        <!--Modal Popup to load the creation of locations-->
       <asp:Button ID="Button1" runat="server" Text="Button" style ="display:none;"/>
        <asp:ModalPopupExtender id="mdlAddLocations" runat="server" DropShadow="False" 
    BackgroundCssClass="modalBackground" BehaviorID="programmaticModalAddLocations" 
    PopupControlID="pnlAddLocations" TargetControlID = "Button1" CancelControlID = "btnCancelAddLocations">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAddLocations" runat="server" CssClass="modalPopup" style="width:500px;padding:10px;display:none;" >
                <table align="center" class="contenttable" style="width: 500px">
                    <tr>
                        <td align = "right">Assigned Location:
                        </td>
                        <td align = "left">
                            <asp:Label ID="lblLocation" runat="server" Text="" Font-Bold = "true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align = "right">New Location:
                        </td>
                        <td align = "left"><asp:TextBox ID="txtNewLocationName" runat="server" Width = "300px"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator
                        ID="fvNewLocation" runat="server" 
                        ErrorMessage="<img src = './images/Vali1.gif' />" 
                        ControlToValidate="txtNewLocationName" ToolTip="Please provide a location name" Enabled = "false"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td align = "right" valign = "top">Description:
                        </td>
                        <td align = "left" valign = "top">
                            <asp:TextBox ID="txtLocationDescription" runat="server" Width = "300px" TextMode = "MultiLine" Height = "100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align = "right">Telephone Number:
                        </td>
                        <td align = "left">
                            <asp:TextBox ID="txtLocationTelNumber" runat="server" Width = "300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align = "center" colspan = "2">
                            <br />
                            <asp:Button ID="btnCancelAddLocations" runat="server" Text="Cancel"  />&nbsp;&nbsp;
                            <asp:Button ID="btnSubmitNewLocation" runat="server" Text="Submit" 
                                onclick="btnSubmitNewLocation_Click"   />
                        </td>
                    </tr>
                    <tr Height = "30">
                    <td colspan = "2" align = "center">
                        <asp:Label ID="lblLocationError" runat="server" Text="" Font-Size = "X-Small" Font-Bold = "true" ForeColor = "Red"></asp:Label>
                    </td>
                    </tr>
                </table>
        </asp:Panel>
        <!--AJAX Extenders to limit certain textboxes to numeric entries only-->
        <asp:FilteredTextBoxExtender
           ID="FilteredTextBoxExtender5"
           runat="server"
           TargetControlID="txtLocationTelNumber"
           FilterType="Numbers" />
    </form>
</body>
</html>
