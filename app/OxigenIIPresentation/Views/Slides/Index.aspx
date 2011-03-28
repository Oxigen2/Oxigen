<%@ Page Title="Slides" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.SlideDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Slides</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>Filename</th>
			    <th>FilenameExtension</th>
			    <th>FilenameNoPath</th>
			    <th>GUID</th>
			    <th>GUID</th>
			    <th>SubDir</th>
			    <th>SlideName</th>
			    <th>Creator</th>
			    <th>Caption</th>
			    <th>ClickThroughURL</th>
			    <th>WebsiteURL</th>
			    <th>DisplayDuration</th>
			    <th>Length</th>
			    <th>ImagePath</th>
			    <th>ImagePathWinFS</th>
			    <th>ImageFilename</th>
			    <th>PlayerType</th>
			    <th>PreviewType</th>
			    <th>bLocked</th>
			    <th>UserGivenDate</th>
			    <th>AddDate</th>
			    <th>EditDate</th>
			    <th>MadeDirtyLastDate</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (SlideDto slideDto in ViewData.Model) { %>
			<tr>
				<td><%= slideDto.Filename %></td>
				<td><%= slideDto.FilenameExtension %></td>
				<td><%= slideDto.FilenameNoPath %></td>
				<td><%= slideDto.GUID %></td>
				<td><%= slideDto.GUID %></td>
				<td><%= slideDto.SubDir %></td>
				<td><%= slideDto.SlideName %></td>
				<td><%= slideDto.Creator %></td>
				<td><%= slideDto.Caption %></td>
				<td><%= slideDto.ClickThroughURL %></td>
				<td><%= slideDto.WebsiteURL %></td>
				<td><%= slideDto.DisplayDuration %></td>
				<td><%= slideDto.Length %></td>
				<td><%= slideDto.ImagePath %></td>
				<td><%= slideDto.ImagePathWinFS %></td>
				<td><%= slideDto.ImageFilename %></td>
				<td><%= slideDto.PlayerType %></td>
				<td><%= slideDto.PreviewType %></td>
				<td><%= slideDto.bLocked %></td>
				<td><%= slideDto.UserGivenDate %></td>
				<td><%= slideDto.AddDate %></td>
				<td><%= slideDto.EditDate %></td>
				<td><%= slideDto.MadeDirtyLastDate %></td>
				<td><%=Html.ActionLink<SlidesController>( c => c.Show( slideDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<SlidesController>( c => c.Edit( slideDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<SlidesController>(c => c.Delete(slideDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<SlidesController>(c => c.Create(), "Create New Slide") %></p>

</asp:Content>
