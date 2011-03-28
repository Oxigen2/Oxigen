<%@ Page Title="Edit AssetContent" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.ApplicationServices.ViewModels.AssetContentFormViewModel>" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h1>Edit AssetContent</h1>

	<% Html.RenderPartial("AssetContentForm", ViewData); %>

</asp:Content>
