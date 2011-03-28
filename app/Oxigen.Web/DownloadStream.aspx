<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="DownloadStream.aspx.cs" Inherits="OxigenIIPresentation.DownloadStream" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="uploader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="MainContent">
  <h1>Stream Subscriptions / Installers</h1>
  <p>Thanks for updating your stream subscriptions.  Any changes you have made to your stream subscriptions have been registered.  If you have already installed the Oxigen Player on your PC(s), these changes will be reflected within the next hour.  If you would like the changes to take effect sooner, please right click the Oxigen icon in your systray (bottom right of screen) and select Update Content.</p>
  <p>If you have not yet installed the Oxigen Player on your PC, please click the download link below for an installer with your stream subscriptions pre-configured:</p>

</div>
<br />

<table cellspacing="0" class="PCTable">
    <tr>
    <td>
        <asp:label id="StreamName" runat="server" />
    </td>
    <td>
        <asp:linkbutton ID="DownloadButton" runat="server" Text="Download Now" OnClick="DownloadButton_Click" />
    </td>
    <td>
        <asp:Label ID="URL" runat="server" />
    </td>
    </tr>
    <tr>
      <td colspan="3" class="TableSpacer">&nbsp;</td>
    </tr>
</table>

</asp:Content>
