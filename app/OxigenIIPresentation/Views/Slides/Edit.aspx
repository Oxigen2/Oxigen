<%@ Page Title="Edit Slide" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.SlideFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Edit Slide</h1>

	<% Html.RenderPartial("SlideForm", ViewData); %>

</asp:Content>
