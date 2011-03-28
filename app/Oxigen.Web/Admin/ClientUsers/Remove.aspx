<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Remove.aspx.cs" Inherits="OxigenIIPresentation.Admin.ClientUsers.Remove" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
  addToLocalNav('Manage Client', '../Clients/List')
</script>
  <h1>Remove Client User</h1>
  <div class="OxiValidation"><asp:label id="lblRemoveStatus" text="You have selected 1 Client User(s) for removal." runat="server" /></div>
  <div class="OxiValidation"><asp:label id="lblWarning" runat="server" /></div>
  <p><asp:label id="lblRemoveInstructions" text="If you wish to proceed and remove the selected Client User(s) click Next." runat="server" /></p>


  <div class="FormButtons2">
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><a href="List.aspx">Next</a></span>
      <span class="RightEnd"></span>
    </div>
  </div>
  
</asp:Content>
