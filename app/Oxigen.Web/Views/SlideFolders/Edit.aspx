<%@ Page Title="Edit SlideFolder" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.SlideFolderFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Edit SlideFolder</h1>

	<% Html.RenderPartial("SlideFolderForm", ViewData); %>

</asp:Content>
