<%@ Page Title="Slide Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.Slide>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Slide Details</h1>

    <ul>
		<li>
			<label for="Slide_Filename">Filename:</label>
            <span id="Slide_Filename"><%= Server.HtmlEncode(ViewData.Model.Filename.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_FilenameExtension">FilenameExtension:</label>
            <span id="Slide_FilenameExtension"><%= Server.HtmlEncode(ViewData.Model.FilenameExtension.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_FilenameNoPath">FilenameNoPath:</label>
            <span id="Slide_FilenameNoPath"><%= Server.HtmlEncode(ViewData.Model.FilenameNoPath.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_GUID">GUID:</label>
            <span id="Slide_GUID"><%= Server.HtmlEncode(ViewData.Model.GUID.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_GUID">GUID:</label>
            <span id="Slide_GUID"><%= Server.HtmlEncode(ViewData.Model.GUID.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_SubDir">SubDir:</label>
            <span id="Slide_SubDir"><%= Server.HtmlEncode(ViewData.Model.SubDir.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_SlideName">SlideName:</label>
            <span id="Slide_SlideName"><%= Server.HtmlEncode(ViewData.Model.SlideName.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_Creator">Creator:</label>
            <span id="Slide_Creator"><%= Server.HtmlEncode(ViewData.Model.Creator.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_Caption">Caption:</label>
            <span id="Slide_Caption"><%= Server.HtmlEncode(ViewData.Model.Caption.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_ClickThroughURL">ClickThroughURL:</label>
            <span id="Slide_ClickThroughURL"><%= Server.HtmlEncode(ViewData.Model.ClickThroughURL.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_WebsiteURL">WebsiteURL:</label>
            <span id="Slide_WebsiteURL"><%= Server.HtmlEncode(ViewData.Model.WebsiteURL.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_DisplayDuration">DisplayDuration:</label>
            <span id="Slide_DisplayDuration"><%= Server.HtmlEncode(ViewData.Model.DisplayDuration.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_Length">Length:</label>
            <span id="Slide_Length"><%= Server.HtmlEncode(ViewData.Model.Length.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_ImagePath">ImagePath:</label>
            <span id="Slide_ImagePath"><%= Server.HtmlEncode(ViewData.Model.ImagePath.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_ImagePathWinFS">ImagePathWinFS:</label>
            <span id="Slide_ImagePathWinFS"><%= Server.HtmlEncode(ViewData.Model.ImagePathWinFS.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_ImageFilename">ImageFilename:</label>
            <span id="Slide_ImageFilename"><%= Server.HtmlEncode(ViewData.Model.ImageFilename.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_PlayerType">PlayerType:</label>
            <span id="Slide_PlayerType"><%= Server.HtmlEncode(ViewData.Model.PlayerType.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_PreviewType">PreviewType:</label>
            <span id="Slide_PreviewType"><%= Server.HtmlEncode(ViewData.Model.PreviewType.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_bLocked">bLocked:</label>
            <span id="Slide_bLocked"><%= Server.HtmlEncode(ViewData.Model.bLocked.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_UserGivenDate">UserGivenDate:</label>
            <span id="Slide_UserGivenDate"><%= Server.HtmlEncode(ViewData.Model.UserGivenDate.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_AddDate">AddDate:</label>
            <span id="Slide_AddDate"><%= Server.HtmlEncode(ViewData.Model.AddDate.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_EditDate">EditDate:</label>
            <span id="Slide_EditDate"><%= Server.HtmlEncode(ViewData.Model.EditDate.ToString()) %></span>
		</li>
		<li>
			<label for="Slide_MadeDirtyLastDate">MadeDirtyLastDate:</label>
            <span id="Slide_MadeDirtyLastDate"><%= Server.HtmlEncode(ViewData.Model.MadeDirtyLastDate.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<SlidesController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
