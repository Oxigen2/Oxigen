<%@ Page Title="AssetContents" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.AssetContentDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>AssetContents</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>Name</th>
			    <th>Caption</th>
			    <th>GUID</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (AssetContentDto assetContentDto in ViewData.Model) { %>
			<tr>
				<td><%= assetContentDto.Name %></td>
				<td><%= assetContentDto.Caption %></td>
				<td><%= assetContentDto.GUID %></td>
				<td><%=Html.ActionLink<AssetContentsController>( c => c.Show( assetContentDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<AssetContentsController>( c => c.Edit( assetContentDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<AssetContentsController>(c => c.Delete(assetContentDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<AssetContentsController>(c => c.Create(), "Create New AssetContent") %></p>

</asp:Content>
