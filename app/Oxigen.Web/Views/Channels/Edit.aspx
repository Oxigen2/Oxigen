<%@ Page Title="Edit Channels" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.ChannelsFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Edit Channels</h1>

	<% Html.RenderPartial("ChannelsForm", ViewData); %>

</asp:Content>
