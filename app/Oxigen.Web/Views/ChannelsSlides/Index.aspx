<%@ Page Title="ChannelsSlides" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.ChannelsSlideDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>ChannelsSlides</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>Channel</th>
			    <th>Slide</th>
			    <th>ClickThroughURL</th>
			    <th>DisplayDuration</th>
			    <th>Schedule</th>
			    <th>PresentationConvertedSchedule</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (ChannelsSlideDto channelsSlideDto in ViewData.Model) { %>
			<tr>
				<td><%= channelsSlideDto.Channel %></td>
				<td><%= channelsSlideDto.Slide %></td>
				<td><%= channelsSlideDto.ClickThroughURL %></td>
				<td><%= channelsSlideDto.DisplayDuration %></td>
				<td><%= channelsSlideDto.Schedule %></td>
				<td><%= channelsSlideDto.PresentationConvertedSchedule %></td>
				<td><%=Html.ActionLink<ChannelsSlidesController>( c => c.Show( channelsSlideDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<ChannelsSlidesController>( c => c.Edit( channelsSlideDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<ChannelsSlidesController>(c => c.Delete(channelsSlideDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<ChannelsSlidesController>(c => c.Create(), "Create New ChannelsSlide") %></p>

</asp:Content>
