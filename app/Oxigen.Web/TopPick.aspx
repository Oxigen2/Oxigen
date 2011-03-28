<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="TopPick.aspx.cs" Inherits="OxigenIIPresentation.TopPick" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="PageTitle">
  <div><span class="NormalTabLeft"></span><span class="NormalTabMiddle">Top 5 <asp:literal id="CategoryName1" runat="server" /></span><span class="NormalTabRight"></span></div>
</div>
<div class="PanelNormalTop">
  <p>Below shows a selection of the most popular free <asp:literal id="CategoryName2" runat="server" /> screensavers for you to download</p>
</div>
<div class="PanelNormal">

  <div class="PanelList">
    <asp:repeater id="Channels" runat="server" onitemdatabound="Channels_ItemDataBound">
      <itemtemplate>
        <div class="PanelListItem">
          <h2><asp:literal id="ChannelNumber" runat="server" />. <asp:literal id="ChannelName1" runat="server" /></h2>
          <div class="StreamHolder"><div class="StreamImages"><asp:image id="Thumbnail" runat="server"/></div><span><asp:literal id="ChannelName2" runat="server" /></span></div>
          <div class="PanelInfo">
            <div class="BigButtonGreen">
              <img id="Img1" src="Images/Default/button-green-large.png" alt="" />
              <a href="Download.aspx">Download now<span>&raquo;</span></a>
            </div>
            <div class="PanelAttr"><span>By: <asp:literal id="PublisherName" runat="server" /></span><span>Followers: <asp:literal id="NoFollowers" runat="server" /></span><span>Added: <asp:literal id="AddDate" runat="server" /></span><span>Content Items: <asp:literal id="NoContent" runat="server" /></span></div>
            <p class="LargePara"><asp:literal id="ShortDescription" runat="server" /></p>
            <asp:literal id="LongDescription" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
      </itemtemplate>
    </asp:repeater>    
    <div class="BottomFix"></div>
  </div>

</div>
<div class="PanelNormalBottom"></div>

</asp:Content>
