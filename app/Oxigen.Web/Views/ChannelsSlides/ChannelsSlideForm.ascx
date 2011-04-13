<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.ChannelsSlideFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("ChannelsSlide.Id", (ViewData.Model.ChannelsSlide != null) ? ViewData.Model.ChannelsSlide.Id : 0)%>

    <ul>
		<li>
			<label for="ChannelsSlide_Channel">Channel:</label>
			<div>
				<%= Html.TextBox("ChannelsSlide.Channel", 
					(ViewData.Model.ChannelsSlide != null) ? ViewData.Model.ChannelsSlide.Channel.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("ChannelsSlide.Channel")%>
		</li>
		<li>
			<label for="ChannelsSlide_Slide">Slide:</label>
			<div>
				<%= Html.TextBox("ChannelsSlide.Slide", 
					(ViewData.Model.ChannelsSlide != null) ? ViewData.Model.ChannelsSlide.Slide.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("ChannelsSlide.Slide")%>
		</li>
		<li>
			<label for="ChannelsSlide_ClickThroughURL">ClickThroughURL:</label>
			<div>
				<%= Html.TextBox("ChannelsSlide.ClickThroughURL", 
					(ViewData.Model.ChannelsSlide != null) ? ViewData.Model.ChannelsSlide.ClickThroughURL.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("ChannelsSlide.ClickThroughURL")%>
		</li>
		<li>
			<label for="ChannelsSlide_DisplayDuration">DisplayDuration:</label>
			<div>
				<%= Html.TextBox("ChannelsSlide.DisplayDuration", 
					(ViewData.Model.ChannelsSlide != null) ? ViewData.Model.ChannelsSlide.DisplayDuration.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("ChannelsSlide.DisplayDuration")%>
		</li>
		<li>
			<label for="ChannelsSlide_Schedule">Schedule:</label>
			<div>
				<%= Html.TextBox("ChannelsSlide.Schedule", 
					(ViewData.Model.ChannelsSlide != null) ? ViewData.Model.ChannelsSlide.Schedule.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("ChannelsSlide.Schedule")%>
		</li>
		<li>
			<label for="ChannelsSlide_PresentationConvertedSchedule">PresentationConvertedSchedule:</label>
			<div>
				<%= Html.TextBox("ChannelsSlide.PresentationConvertedSchedule", 
					(ViewData.Model.ChannelsSlide != null) ? ViewData.Model.ChannelsSlide.PresentationConvertedSchedule.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("ChannelsSlide.PresentationConvertedSchedule")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save ChannelsSlide") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<ChannelsSlidesController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
