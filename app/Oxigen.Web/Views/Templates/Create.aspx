<%@ Page Title="Create Template" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.TemplateFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<script type="text/javascript" src="../../scripts/jquery-ui.min.js"></script>

	<h1>Create Template</h1>

	<% Html.RenderPartial("TemplateForm", ViewData); %>


</asp:Content>
