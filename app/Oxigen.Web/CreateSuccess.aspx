<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="CreateSuccess.aspx.cs" Inherits="OxigenIIPresentation.CreateSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="uploader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="PageTitle">
  <div><span class="NormalTabLeft"></span><span class="NormalTabMiddle">Streams Updated!</span><span class="NormalTabRight"></span></div>
</div>
<div class="PanelNormalTop">
  <p>Your streams have been successfully updated.</p>
</div>
<div class="PanelNormal">
    <p>Thanks for updating your Oxigen streams.  Any changes you have made to your streams have been registered and will appear on your followers' PCs within an hour.</p>
<br />

<table cellspacing="0" class="PCTable">
    <asp:Repeater id="Streams" runat="server" OnItemDataBound="Streams_DataBound">
    <ItemTemplate>
        <tr>
        <td>
            <asp:label id="StreamName" runat="server" />
        </td>
        <td>
            <asp:linkbutton ID="DownloadButton" runat="server" Text="Download Now" OnCommand="DownloadButton_Command" />
        </td>
        <td>
            <asp:Label ID="URL" runat="server" />
        </td>
        </tr>
        <tr>
          <td colspan="3" class="TableSpacer">&nbsp;</td>
        </tr>
    </ItemTemplate>
    </asp:Repeater>
</table>

  <div class="BottomFix"></div>
</div>
<div class="PanelNormalBottom"></div>


</asp:Content>
