<%@ Page Title="Create Slide" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.SlideFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Create Slide</h1>

	<% Html.RenderPartial("SlideForm", ViewData); %>

</asp:Content>
