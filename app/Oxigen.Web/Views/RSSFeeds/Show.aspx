<%@ Page Title="RSSFeed Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.RSSFeed>" %>
<%@ Import Namespace="Oxigen.Web.Controllers.Syndication" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>RSSFeed Details</h1>

    <ul>
		<li>
			<label for="RSSFeed_URL">URL:</label>
            <span id="RSSFeed_URL"><%= Server.HtmlEncode(ViewData.Model.URL.ToString()) %></span>
		</li>
		<li>
			<label for="RSSFeed_LastChecked">LastChecked:</label>
            <span id="RSSFeed_LastChecked"><%= Server.HtmlEncode(ViewData.Model.LastChecked.ToString()) %></span>
		</li>
		<li>
			<label for="RSSFeed_LastItem">LastItem:</label>
            <span id="RSSFeed_LastItem"><%= Server.HtmlEncode(ViewData.Model.LastItem.ToString()) %></span>
		</li>
		<li>
			<label for="RSSFeed_Template">Template:</label>
            <span id="RSSFeed_Template"><%= Server.HtmlEncode(ViewData.Model.Template.ToString()) %></span>
		</li>
		<li>
			<label for="RSSFeed_XSLT">XSLT:</label>
            <span id="RSSFeed_XSLT"><%= Server.HtmlEncode(ViewData.Model.XSLT.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<RSSFeedsController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
