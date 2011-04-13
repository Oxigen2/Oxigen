<%@ Page Title="Edit ChannelsSlide" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.ChannelsSlideFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Edit ChannelsSlide</h1>

	<% Html.RenderPartial("ChannelsSlideForm", ViewData); %>

</asp:Content>
