<%@ Page Title="RSSFeeds" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.RSSFeedDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos.Syndication" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
<%@ Import Namespace="Oxigen.Web.Controllers.Syndication" %> 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>RSSFeeds</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>URL</th>
			    <th>Name</th>
			    <th colspan="4">Action</th>
            </tr>
        </thead>

		<%
		foreach (RSSFeedDto rSSFeedDto in ViewData.Model) { %>
			<tr>
				<td><%= rSSFeedDto.URL %></td>
				<td><%= rSSFeedDto.Name %></td>
                <td><%=Html.ActionLink<RSSFeedsController>( c => c.Run( rSSFeedDto.Id ), "Run ") %></td>
				<td><%=Html.ActionLink<RSSFeedsController>( c => c.Show( rSSFeedDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<RSSFeedsController>( c => c.Edit( rSSFeedDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<RSSFeedsController>(c => c.Delete(rSSFeedDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<RSSFeedsController>(c => c.Create(), "Create New RSSFeed") %></p>

</asp:Content>
