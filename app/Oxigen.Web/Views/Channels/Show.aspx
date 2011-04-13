<%@ Page Title="Channels Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.Channels>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Channels Details</h1>

    <ul>
		<li>
			<label for="Channels_CategoryID">CategoryID:</label>
            <span id="Channels_CategoryID"><%= Server.HtmlEncode(ViewData.Model.CategoryID.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_Publisher">Publisher:</label>
            <span id="Channels_Publisher"><%= Server.HtmlEncode(ViewData.Model.Publisher.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ChannelName">ChannelName:</label>
            <span id="Channels_ChannelName"><%= Server.HtmlEncode(ViewData.Model.ChannelName.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ChannelGUID">ChannelGUID:</label>
            <span id="Channels_ChannelGUID"><%= Server.HtmlEncode(ViewData.Model.ChannelGUID.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ChannelDescription">ChannelDescription:</label>
            <span id="Channels_ChannelDescription"><%= Server.HtmlEncode(ViewData.Model.ChannelDescription.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ChannelLongDescription">ChannelLongDescription:</label>
            <span id="Channels_ChannelLongDescription"><%= Server.HtmlEncode(ViewData.Model.ChannelLongDescription.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_Keywords">Keywords:</label>
            <span id="Channels_Keywords"><%= Server.HtmlEncode(ViewData.Model.Keywords.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ImagePath">ImagePath:</label>
            <span id="Channels_ImagePath"><%= Server.HtmlEncode(ViewData.Model.ImagePath.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_bHasDefaultThumbnail">bHasDefaultThumbnail:</label>
            <span id="Channels_bHasDefaultThumbnail"><%= Server.HtmlEncode(ViewData.Model.bHasDefaultThumbnail.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_bLocked">bLocked:</label>
            <span id="Channels_bLocked"><%= Server.HtmlEncode(ViewData.Model.bLocked.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_bAcceptPasswordRequests">bAcceptPasswordRequests:</label>
            <span id="Channels_bAcceptPasswordRequests"><%= Server.HtmlEncode(ViewData.Model.bAcceptPasswordRequests.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ChannelPassword">ChannelPassword:</label>
            <span id="Channels_ChannelPassword"><%= Server.HtmlEncode(ViewData.Model.ChannelPassword.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ChannelGUIDSuffix">ChannelGUIDSuffix:</label>
            <span id="Channels_ChannelGUIDSuffix"><%= Server.HtmlEncode(ViewData.Model.ChannelGUIDSuffix.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_NoContent">NoContent:</label>
            <span id="Channels_NoContent"><%= Server.HtmlEncode(ViewData.Model.NoContent.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_NoFollowers">NoFollowers:</label>
            <span id="Channels_NoFollowers"><%= Server.HtmlEncode(ViewData.Model.NoFollowers.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_AddDate">AddDate:</label>
            <span id="Channels_AddDate"><%= Server.HtmlEncode(ViewData.Model.AddDate.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_EditDate">EditDate:</label>
            <span id="Channels_EditDate"><%= Server.HtmlEncode(ViewData.Model.EditDate.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_MadeDirtyLastDate">MadeDirtyLastDate:</label>
            <span id="Channels_MadeDirtyLastDate"><%= Server.HtmlEncode(ViewData.Model.MadeDirtyLastDate.ToString()) %></span>
		</li>
		<li>
			<label for="Channels_ContentLastAddedDate">ContentLastAddedDate:</label>
            <span id="Channels_ContentLastAddedDate"><%= Server.HtmlEncode(ViewData.Model.ContentLastAddedDate.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<ChannelsController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
