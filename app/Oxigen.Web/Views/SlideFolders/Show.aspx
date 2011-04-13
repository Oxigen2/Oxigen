<%@ Page Title="SlideFolder Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.SlideFolder>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>SlideFolder Details</h1>

    <ul>
		<li>
			<label for="SlideFolder_SlideFolderName">SlideFolderName:</label>
            <span id="SlideFolder_SlideFolderName"><%= Server.HtmlEncode(ViewData.Model.SlideFolderName.ToString()) %></span>
		</li>
		<li>
			<label for="SlideFolder_Publisher">Publisher:</label>
            <span id="SlideFolder_Publisher"><%= Server.HtmlEncode(ViewData.Model.Publisher.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<SlideFoldersController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
