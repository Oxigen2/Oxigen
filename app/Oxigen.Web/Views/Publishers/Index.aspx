<%@ Page Title="Publishers" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.PublisherDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Publishers</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>UserID</th>
			    <th>FirstName</th>
			    <th>LastName</th>
			    <th>DisplayName</th>
			    <th>EmailAddress</th>
			    <th>UsedBytes</th>
			    <th>TotalAvailableBytes</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (PublisherDto publisherDto in ViewData.Model) { %>
			<tr>
				<td><%= publisherDto.UserID %></td>
				<td><%= publisherDto.FirstName %></td>
				<td><%= publisherDto.LastName %></td>
				<td><%= publisherDto.DisplayName %></td>
				<td><%= publisherDto.EmailAddress %></td>
				<td><%= publisherDto.UsedBytes %></td>
				<td><%= publisherDto.TotalAvailableBytes %></td>
				<td><%=Html.ActionLink<PublishersController>( c => c.Show( publisherDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<PublishersController>( c => c.Edit( publisherDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<PublishersController>(c => c.Delete(publisherDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<PublishersController>(c => c.Create(), "Create New Publisher") %></p>

</asp:Content>
