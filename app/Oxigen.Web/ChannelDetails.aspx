<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="ChannelDetails.aspx.cs" Inherits="OxigenIIPresentation.ChannelDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="uploader" runat="server">
<div class="StreamPreviewDiv" id="divStreamPreview">
  <div class="StreamPreviewClose"><a onclick="unStreamPreview()" href="#">Close Preview</a></div>
  <div id="divStreamPreviewContent">
    
  </div>
  <div id="StreamPreviewTitle"></div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="PageTitle">
  <div><span class="NormalTabLeft"></span><span class="NormalTabMiddle"><asp:literal id="ChannelName1" runat="server" /></span><span class="NormalTabRight"></span></div>
</div>
<div class="PanelNormalTop">
  <p><asp:literal id="ShortDescription" runat="server" /></p>
</div>
<div class="PanelNormal">
  <div class="DetailControls">
    <div class="DetailPrev" onclick="prevChannelSlideShow()"></div>
    <div class="DetailPrev2"></div>
    <div class="DetailPlay" onclick="playChannelSlideShow()"></div>
    <div class="DetailPause" onclick="pauseChannelSlideShow()"></div>
    <div class="DetailNext" onclick="nextChannelSlideShow()"></div>
    <div class="DetailNext2"></div>
  </div>
  <div class="ChannelDetails">
    <div class="DetailAttr">
      <span class="AttrTitle"><asp:literal id="ChannelName2" runat="server" /> Properties:</span>
      <span>By: <asp:literal id="PublisherName" runat="server" /></span>
      <span>Followers: <asp:literal id="NoFollowers" runat="server" /></span>
      <span>Created: <asp:literal id="AddDate" runat="server" /></span>
      <span>Last updated: <asp:literal id="ContentLastAddedDate" runat="server" /></span>
      <span>Content Items: <asp:literal id="NoContents" runat="server" /></span>
    </div>
  </div>
  <div class="BottomFix"></div>
    <div class="ChannelSlideShow">
      <asp:repeater id="ChannelSlides" runat="server" onitemdatabound="ChannelSlides_ItemDataBound">
        <itemtemplate>
          <asp:literal id="ChannelOpeningSlide" runat="server" />
             <div class="StreamHolder"><div class="StreamImages"><asp:image id="Thumbnail" runat="server" /></div><span><asp:literal id="SlideName" runat="server" /></span></div>
          <asp:literal id="ChannelClosingSlide" runat="server" />
        </itemtemplate>
      </asp:repeater>
    <div class="BottomFix"></div>
  </div>
  <h2><asp:label id="ChannelName3" runat="server" /></h2>
  <asp:label id="LongDescription" runat="server" />
  <div class="BottomFix" style="height:20px"></div>
  <div class="BigButtonGreen" style="float:right;">
    <img id="ForJSButtonGreenCentre" src="Images/Default/button-green-large.png" alt="" />
    <asp:linkbutton id="downloadLink"  OnClientClick="_gaq.push(['_trackPageview', '/download/now'])" OnClick="Download_Click" runat="server">Download now<span>&raquo;</span></asp:linkbutton>
  </div>
  <div class="BigButtonGreen" style="float:right">
    <img id="ForJSButtonGreenCentre2" src="Images/Default/button-green-large.png" alt="" />
    <asp:hyperlink id="addStreamLink" runat="server">Add to my PC<span>&raquo;</span></asp:hyperlink>
  </div>
  <div class="BigButtonGreen">
    <img id="ForJSButtonRedCentre" src="Images/Default/button-red-large.png" alt="" />
    <a href="#" onclick="BlackOut('DivReport');return false">Report this stream<span>&nbsp;</span></a>
  </div>
  <div class="BottomFix"></div>
</div>
<div class="PanelNormalBottom"></div>

  <div class="DivPopUp DivPopUpSmall" id="DivReport">
      <span style="display:none;" id="HiddenContentID"></span>
      <div class="DivPopUpClose DivPopUpClose2" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <p>Edit Content Properties.</p>
        <div id="ReportValidation" class="OxiValidation"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <asp:TextBox CssClass="TextBox" tabindex="1" runat="server" />
            <span>Your name</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <asp:TextBox CssClass="TextBox" tabindex="1" runat="server" />
            <span>Your email</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <span>Comment</span>
            <asp:TextBox TextMode="MultiLine" style="width:226px;" tabindex="1" cssClass="TextArea" runat="server"></asp:TextBox>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div> 
        
        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-green-large.png" alt="" />
          <a href="#" id="UpdateProperties">Send Report <span>&raquo;</span></a>
        </div>
        <div id="HiddenContentPanel">
          <asp:HiddenField runat="server" />
        </div>

        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>


<script type="text/javascript">
  /*<![CDATA[*/
  <asp:literal id="PreviewLiteral" text= "addPreview();" runat="server" />;
  showIcons();
  initchannelSlideShow();
  /*]]>*/
</script>


</asp:Content>
