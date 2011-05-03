<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Error413.aspx.cs" Inherits="OxigenIIPresentation.Error413" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Sorry, the file you are uploading is too large</h1>
            <p>You can either:</p>
        <ul>
        <li><a href="javascript:history.go(-1)">go back</a> and try again</li>
            <li><a href="mailto:admin@videojug.com?subject=Error 500: <%=Request.QueryString["aspxerrorpath"] %>">contact us</a></li>
            <li>go to the <a href='/'>home page</a></li>
        </ul>
</asp:Content>

