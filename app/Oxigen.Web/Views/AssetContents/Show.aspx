<%@ Page Title="AssetContent Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.AssetContent>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>AssetContent Details</h1>

    <ul>
		<li>
			<label for="AssetContent_Name">Name:</label>
            <span id="AssetContent_Name"><%= Server.HtmlEncode(ViewData.Model.Name.ToString()) %></span>
		</li>
		<li>
			<label for="AssetContent_Caption">Caption:</label>
            <span id="AssetContent_Caption"><%= Server.HtmlEncode(ViewData.Model.Caption.ToString()) %></span>
		</li>
		<li>
			<label for="AssetContent_GUID">GUID:</label>
            <span id="AssetContent_GUID"><%= Server.HtmlEncode(ViewData.Model.GUID.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<AssetContentsController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
