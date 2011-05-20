<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="ErrorGeneric.aspx.cs" Inherits="OxigenIIPresentation.ErrorGeneric" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Sorry, an Error has Occured</h1>
        <p>
            The error is logged and we will investigate it</p>

            <p>You can either:</p>
        <ul>
            <li><a href="javascript:history.go(-1)">go back</a> and try again</li>
            <li><a href="/contactus.aspx?subject=Error 500: <%=Request.QueryString["aspxerrorpath"] %>">contact us</a></li>
            <li>go to the <a href='/'>home page</a></li>
        </ul>

</asp:Content>
