<%@ Page Title="Channels" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.ChannelsDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Channels</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>CategoryID</th>
			    <th>Publisher</th>
			    <th>ChannelName</th>
			    <th>ChannelGUID</th>
			    <th>ChannelDescription</th>
			    <th>ChannelLongDescription</th>
			    <th>Keywords</th>
			    <th>ImagePath</th>
			    <th>bHasDefaultThumbnail</th>
			    <th>bLocked</th>
			    <th>bAcceptPasswordRequests</th>
			    <th>ChannelPassword</th>
			    <th>ChannelGUIDSuffix</th>
			    <th>NoContent</th>
			    <th>NoFollowers</th>
			    <th>AddDate</th>
			    <th>EditDate</th>
			    <th>MadeDirtyLastDate</th>
			    <th>ContentLastAddedDate</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (ChannelsDto channelsDto in ViewData.Model) { %>
			<tr>
				<td><%= channelsDto.CategoryID %></td>
				<td><%= channelsDto.Publisher %></td>
				<td><%= channelsDto.ChannelName %></td>
				<td><%= channelsDto.ChannelGUID %></td>
				<td><%= channelsDto.ChannelDescription %></td>
				<td><%= channelsDto.ChannelLongDescription %></td>
				<td><%= channelsDto.Keywords %></td>
				<td><%= channelsDto.ImagePath %></td>
				<td><%= channelsDto.bHasDefaultThumbnail %></td>
				<td><%= channelsDto.bLocked %></td>
				<td><%= channelsDto.bAcceptPasswordRequests %></td>
				<td><%= channelsDto.ChannelPassword %></td>
				<td><%= channelsDto.ChannelGUIDSuffix %></td>
				<td><%= channelsDto.NoContent %></td>
				<td><%= channelsDto.NoFollowers %></td>
				<td><%= channelsDto.AddDate %></td>
				<td><%= channelsDto.EditDate %></td>
				<td><%= channelsDto.MadeDirtyLastDate %></td>
				<td><%= channelsDto.ContentLastAddedDate %></td>
				<td><%=Html.ActionLink<ChannelsController>( c => c.Show( channelsDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<ChannelsController>( c => c.Edit( channelsDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<ChannelsController>(c => c.Delete(channelsDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<ChannelsController>(c => c.Create(), "Create New Channels") %></p>

</asp:Content>
