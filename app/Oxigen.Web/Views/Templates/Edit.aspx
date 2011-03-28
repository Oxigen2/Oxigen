<%@ Page Title="Edit Template" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.TemplateFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Edit Template</h1>

	<% Html.RenderPartial("TemplateForm", ViewData); %>

</asp:Content>
