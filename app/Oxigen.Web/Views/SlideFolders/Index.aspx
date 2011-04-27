<%@ Page Title="SlideFolders" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.SlideFolderDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>SlideFolders</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>Folder Name</th>
			    <th>Publisher Id</th>
                <th>Slide Count</th>
                <th>Max Slide Count</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (SlideFolderDto slideFolderDto in ViewData.Model) { %>
			<tr>
				<td><%= slideFolderDto.SlideFolderName %></td>
				<td><%= slideFolderDto.PublisherID %></td>
                <td><%= slideFolderDto.SlideCount %></td>
                <td><%= slideFolderDto.MaxSlideCount %></td>
				<td><%=Html.ActionLink<SlideFoldersController>( c => c.Show( slideFolderDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<SlideFoldersController>( c => c.Edit( slideFolderDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<SlideFoldersController>(c => c.Delete(slideFolderDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<SlideFoldersController>(c => c.Create(), "Create New SlideFolder") %></p>

</asp:Content>
