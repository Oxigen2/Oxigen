<%@ Page Title="Create RSSFeed" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.Syndication.RSSFeedFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Create RSSFeed</h1>

	<% Html.RenderPartial("RSSFeedForm", ViewData); %>

</asp:Content>
