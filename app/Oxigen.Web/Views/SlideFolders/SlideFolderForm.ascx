<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.SlideFolderFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("SlideFolder.Id", (ViewData.Model.SlideFolder != null) ? ViewData.Model.SlideFolder.Id : 0)%>

    <ul>
		<li>
			<label for="SlideFolder_SlideFolderName">SlideFolderName:</label>
			<div>
				<%= Html.TextBox("SlideFolder.SlideFolderName", 
					(ViewData.Model.SlideFolder != null) ? ViewData.Model.SlideFolder.SlideFolderName.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("SlideFolder.SlideFolderName")%>
		</li>
		<li>
			<label for="SlideFolder_Publisher">Publisher:</label>
			<div>
				<%= Html.TextBox("SlideFolder.Publisher", 
					(ViewData.Model.SlideFolder != null) ? ViewData.Model.SlideFolder.Publisher.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("SlideFolder.Publisher")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save SlideFolder") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<SlideFoldersController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
