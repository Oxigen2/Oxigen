<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="OxigenIIPresentation.Admin.Folders.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
  addToLocalNav('Users', '../Users/List')
  addToLocalNav('Subscriptions', '../Subscriptions/List')
  addToLocalNav('Content', '../Content/List')
  addToLocalNav('Slides', '../Slides/List')
  addToLocalNav('Streams', '../Streams/List')
  addToLocalNav('Stream Scheduling', '../StreamSchedule/List')
</script>
<h1>Edit Folder</h1>
   <div class="OxiValidation"><asp:label id="lblValidationMessage" runat="server" /></div>
   <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlType" runat="server">Folder Type:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList TabIndex="1" id="ddlType" cssclass="DropDownW1 WithFocusHighlight" runat="server">
          <asp:ListItem>Please select ¬</asp:ListItem>
          <asp:ListItem>Raw Content</asp:ListItem>
          <asp:ListItem>Slide</asp:ListItem>
        </asp:DropDownList>
      </div>
      <asp:requiredfieldvalidator id="rfvType" controltovalidate="ddlType" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel">Suppressed:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2"><label><asp:RadioButton ID="rbSuppressedYes" GroupName="SuppressedGroup" CssClass="Radio WithFocusHighlight" runat="server"/>Yes</label><label><asp:RadioButton ID="rbSuppressedNo" GroupName="SuppressedGroup" Checked="true" CssClass="Radio2 WithFocusHighlight" runat="server"/>No</label></div>
    </div>
  </div>
  <div class="AdminRight">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtName" runat="server">Folder Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvName" controltovalidate="txtName" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
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
