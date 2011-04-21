<%@ Page Title="Edit RSSFeed" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.RSSFeedFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Edit RSSFeed</h1>

	<% Html.RenderPartial("RSSFeedForm", ViewData); %>

</asp:Content>
