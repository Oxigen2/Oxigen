<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Error404.aspx.cs" Inherits="OxigenIIPresentation.Error404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Sorry, the page you are looging for cannot be found</h1>
        <p>
            The error is logged and we will investigate it</p>

            <p>You can either:</p>
        <ul>
            <li><a href="mailto:admin@videojug.com?subject=Error 404: <%=Request.QueryString["aspxerrorpath"] %>">contact us</a></li>
            <li>go to the <a href='/'>home page</a></li>
        </ul>
</asp:Content>
