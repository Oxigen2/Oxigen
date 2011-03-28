<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="OxigenIIPresentation.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="uploader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="PageTitle">
  <div><span class="NormalTabLeft"></span><span class="NormalTabMiddle">Contact Us</span><span class="NormalTabRight"></span></div>
</div>
<div class="PanelNormalTop">
  <p></p>
</div>
<div class="PanelNormal">
  <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtName" runat="server">Your Name:</asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="Requiredfieldvalidator1" controltovalidate="txtName" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtEmail" runat="server">Reply Email Address:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtEmail" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/>
        <asp:Panel ID="rfvEmail" CssClass="ValidationInfo" EnableViewState="false" Visible="false" runat="server"><asp:Literal runat="server">Please enter a valid email address</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlSubject" runat="server">Subject:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList TabIndex="1" id="ddlSubject" cssclass="DropDownW1 WithFocusHighlight" runat="server">
          <asp:ListItem>Please select ¬</asp:ListItem>
          <asp:ListItem>Technical Support</asp:ListItem>
          <asp:ListItem>Product Suggestion</asp:ListItem>
          <asp:ListItem>Copyright Notification</asp:ListItem>
          <asp:ListItem>Community Guidelines Notification</asp:ListItem>
          <asp:ListItem>Privacy</asp:ListItem>
          <asp:ListItem>Other</asp:ListItem>
        </asp:DropDownList>
        <asp:Panel ID="rfvSubject" CssClass="ValidationInfo" EnableViewState="false" Visible="false" runat="server"><asp:Literal runat="server">Please select a subject</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtMessage" runat="server">Message:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtMessage" cssclass="TextAreaW1 WithFocusHighlight" Rows="8" TextMode="MultiLine" maxlength="1000" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvMessage" controltovalidate="txtMessage" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
  </div>
  <div class="AdminRight">
    <div class="FormSpacer" style="height:15px;"></div>
    <p>Oxigen II Limited</p>
    <p>21 Arlington Street</p>
    <p>London</p>
    <p>SW1A 1RN</p>
    <div class="FormSpacer"></div>
    <p>Company number - 4359578</p>
    <p>VAT registration number - 788723373</p>
  </div>
  
  <div class="FormButtons">
    <div class="Info"><span class="Asterisk">*</span> Indicates a required field.</div>
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><asp:linkbutton ID="Submit" runat="server" OnClick="OnClick_Next">Next</asp:linkButton></span>
      <span class="RightEnd"></span>
    </div>
  </div>
  
  <div class="BottomFix"></div>
</div>
<div class="PanelNormalBottom"></div>

</asp:Content>
