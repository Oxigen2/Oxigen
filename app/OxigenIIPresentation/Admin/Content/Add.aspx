﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="OxigenIIPresentation.Admin.Content.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
  addToLocalNav('Users', '../Users/List')
  addToLocalNav('Subscriptions', '../Subscriptions/List')
  addToLocalNav('Folders', '../Folders/List')
  addToLocalNav('Slides', '../Slides/List')
  addToLocalNav('Streams', '../Streams/List')
  addToLocalNav('Stream Scheduling', '../StreamSchedule/List')
</script>
<h1>Add Raw Content</h1>
   <div class="OxiValidation"><asp:label id="lblValidationMessage" runat="server" /></div>
   <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlType" runat="server">Content Folder:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList TabIndex="1" id="ddlType" cssclass="DropDownW1 WithFocusHighlight" runat="server">
          <asp:ListItem>Content Folder 1</asp:ListItem>
          <asp:ListItem>Content Folder 2</asp:ListItem>
          <asp:ListItem>Content Folder 3</asp:ListItem>
        </asp:DropDownList>
      </div>
      <asp:requiredfieldvalidator id="rfvType" controltovalidate="ddlType" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtName" runat="server">Content Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvName" controltovalidate="txtName" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtAuthor" runat="server">Author:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtAuthor" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvAuthor" controltovalidate="txtAuthor" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtCaption" runat="server">Caption:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtCaption" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvCaption" controltovalidate="txtCaption" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    
    <div class="FormBox">
      <div class="FormLabel">Suppressed:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2"><label><asp:RadioButton ID="rbSuppressedYes" GroupName="SuppressedGroup" CssClass="Radio WithFocusHighlight" runat="server"/>Yes</label><label><asp:RadioButton ID="rbSuppressedNo" GroupName="SuppressedGroup" Checked="true" CssClass="Radio2 WithFocusHighlight" runat="server"/>No</label></div>
    </div>
  </div>
  <div class="AdminRight">
    <!-- CALENDAR CONTROL NEEDED -->
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtDate" runat="server">Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtDate" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvDate" controltovalidate="txtDate" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtURL" runat="server">URL:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtURL" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/>
        <asp:Panel ID="rfvURL" CssClass="ValidationInfo" EnableViewState="false" Visible="false" runat="server"><asp:Literal runat="server">Please enter a valid URL</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtDuration" runat="server">Display Duration:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtDuration" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvDuration" controltovalidate="txtDuration" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="fuUpload" runat="server">File Upload:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:FileUpload TabIndex="1" id="fuUpload" cssclass="FileUploadW1 WithFocusHighlight" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvUpload" controltovalidate="fuUpload" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
  </div>
  <div class="FormButtons">
    <div class="Info"><span class="Asterisk">*</span> Indicates a required field.</div>
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><a href="List.aspx">Next</a></span>
      <span class="RightEnd"></span>
    </div>
  </div>

</asp:Content>
