<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.AssetContentFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("AssetContent.Id", (ViewData.Model.AssetContent != null) ? ViewData.Model.AssetContent.Id : 0)%>

    <ul>
		<li>
			<label for="AssetContent_Name">Name:</label>
			<div>
				<%= Html.TextBox("AssetContent.Name", 
					(ViewData.Model.AssetContent != null) ? ViewData.Model.AssetContent.Name.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("AssetContent.Name")%>
		</li>
		<li>
			<label for="AssetContent_Caption">Caption:</label>
			<div>
				<%= Html.TextBox("AssetContent.Caption",
                    ((ViewData.Model.AssetContent != null) && (ViewData.Model.AssetContent.Caption != null)) ? ViewData.Model.AssetContent.Caption.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("AssetContent.Caption")%>
		</li>
		<li>
			<label for="AssetContent_GUID">GUID:</label>
			<div>
				<%= Html.TextBox("AssetContent.GUID", 
					((ViewData.Model.AssetContent != null) && (ViewData.Model.AssetContent.GUID != null))? ViewData.Model.AssetContent.GUID.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("AssetContent.GUID")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save AssetContent") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<AssetContentsController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
