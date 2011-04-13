<%@ Page Title="ChannelsSlide Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.ChannelsSlide>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>ChannelsSlide Details</h1>

    <ul>
		<li>
			<label for="ChannelsSlide_Channel">Channel:</label>
            <span id="ChannelsSlide_Channel"><%= Server.HtmlEncode(ViewData.Model.Channel.ToString()) %></span>
		</li>
		<li>
			<label for="ChannelsSlide_Slide">Slide:</label>
            <span id="ChannelsSlide_Slide"><%= Server.HtmlEncode(ViewData.Model.Slide.ToString()) %></span>
		</li>
		<li>
			<label for="ChannelsSlide_ClickThroughURL">ClickThroughURL:</label>
            <span id="ChannelsSlide_ClickThroughURL"><%= Server.HtmlEncode(ViewData.Model.ClickThroughURL.ToString()) %></span>
		</li>
		<li>
			<label for="ChannelsSlide_DisplayDuration">DisplayDuration:</label>
            <span id="ChannelsSlide_DisplayDuration"><%= Server.HtmlEncode(ViewData.Model.DisplayDuration.ToString()) %></span>
		</li>
		<li>
			<label for="ChannelsSlide_Schedule">Schedule:</label>
            <span id="ChannelsSlide_Schedule"><%= Server.HtmlEncode(ViewData.Model.Schedule.ToString()) %></span>
		</li>
		<li>
			<label for="ChannelsSlide_PresentationConvertedSchedule">PresentationConvertedSchedule:</label>
            <span id="ChannelsSlide_PresentationConvertedSchedule"><%= Server.HtmlEncode(ViewData.Model.PresentationConvertedSchedule.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<ChannelsSlidesController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
