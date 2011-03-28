<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="OxigenIIPresentation.Admin.Streams.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
  addToLocalNav('Users', '../Users/List')
  addToLocalNav('Subscriptions', '../Subscriptions/List')
  addToLocalNav('Folders', '../Folders/List')
  addToLocalNav('Content', '../Content/List')
  addToLocalNav('Slides', '../Slides/List')
  addToLocalNav('Stream Scheduling', '../StreamSchedule/List')
</script>
<h1>Stream List</h1>

</asp:Content>
