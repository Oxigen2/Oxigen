<%@ Page Title="Create SlideFolder" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.SlideFolderFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Create SlideFolder</h1>

	<% Html.RenderPartial("SlideFolderForm", ViewData); %>

</asp:Content>
