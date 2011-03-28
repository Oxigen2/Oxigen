<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="ForgottenPassword.aspx.cs" Inherits="OxigenIIPresentation.ForgottenPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="uploader" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1 class="PageTitle">Forgotten Password</h1>

<div class="OxiValidation"><asp:label id="lblValidationMessage" runat="server" /></div>
<div class="BottomFix" style="height:10px;"></div>
<p>If you have forgotten your password please give us your email address and we will send a new password to you which you can reset once you are on the site.</p>
  <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtEmail" runat="server">Email Address:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtEmail" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvName" controltovalidate="txtEmail" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
  </div>
  <div class="FormButtons">
    <div class="Info"><span class="Asterisk">*</span> Indicates a required field.</div>
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><asp:linkbutton ID="btnNext" Text="Next" OnClick="Next_Click" runat="server"/></span>
      <span class="RightEnd"></span>
    </div>
  </div>


</asp:Content>
