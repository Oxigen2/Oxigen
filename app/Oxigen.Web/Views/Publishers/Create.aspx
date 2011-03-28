<%@ Page Title="Create Publisher" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.PublisherFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Create Publisher</h1>

	<% Html.RenderPartial("PublisherForm", ViewData); %>

</asp:Content>
