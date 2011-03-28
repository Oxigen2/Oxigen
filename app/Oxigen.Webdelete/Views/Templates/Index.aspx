<%@ Page Title="Templates" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<IEnumerable<Oxigen.Core.QueryDtos.TemplateDto>>" %>
<%@ Import Namespace="Oxigen.Core.QueryDtos" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Templates</h1>

    <% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
        <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
    <% } %>

    <table>
        <thead>
            <tr>
			    <th>MetaData</th>
			    <th colspan="3">Action</th>
            </tr>
        </thead>

		<%
		foreach (TemplateDto templateDto in ViewData.Model) { %>
			<tr>
				<td><%= templateDto.MetaData %></td>
				<td><%=Html.ActionLink<TemplatesController>( c => c.Show( templateDto.Id ), "Details ") %></td>
				<td><%=Html.ActionLink<TemplatesController>( c => c.Edit( templateDto.Id ), "Edit") %></td>
				<td>
    				<% using (Html.BeginForm<TemplatesController>(c => c.Delete(templateDto.Id))) { %>
                        <%= Html.AntiForgeryToken() %>
    				    <input type="submit" value="Delete" onclick="return confirm('Are you sure?');" />
                    <% } %>
				</td>
			</tr>
		<%} %>
    </table>

    <p><%= Html.ActionLink<TemplatesController>(c => c.Create(), "Create New Template") %></p>

</asp:Content>
