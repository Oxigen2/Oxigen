<%@ Page Title="Publisher Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.Publisher>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Publisher Details</h1>

    <ul>
		<li>
			<label for="Publisher_UserID">UserID:</label>
            <span id="Publisher_UserID"><%= Server.HtmlEncode(ViewData.Model.UserID.ToString()) %></span>
		</li>
		<li>
			<label for="Publisher_FirstName">FirstName:</label>
            <span id="Publisher_FirstName"><%= Server.HtmlEncode(ViewData.Model.FirstName.ToString()) %></span>
		</li>
		<li>
			<label for="Publisher_LastName">LastName:</label>
            <span id="Publisher_LastName"><%= Server.HtmlEncode(ViewData.Model.LastName.ToString()) %></span>
		</li>
		<li>
			<label for="Publisher_DisplayName">DisplayName:</label>
            <span id="Publisher_DisplayName"><%= Server.HtmlEncode(ViewData.Model.DisplayName.ToString()) %></span>
		</li>
		<li>
			<label for="Publisher_EmailAddress">EmailAddress:</label>
            <span id="Publisher_EmailAddress"><%= Server.HtmlEncode(ViewData.Model.EmailAddress.ToString()) %></span>
		</li>
		<li>
			<label for="Publisher_UsedBytes">UsedBytes:</label>
            <span id="Publisher_UsedBytes"><%= Server.HtmlEncode(ViewData.Model.UsedBytes.ToString()) %></span>
		</li>
		<li>
			<label for="Publisher_TotalAvailableBytes">TotalAvailableBytes:</label>
            <span id="Publisher_TotalAvailableBytes"><%= Server.HtmlEncode(ViewData.Model.TotalAvailableBytes.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<PublishersController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
