<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.TemplateFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("Template.Id", (ViewData.Model.Template != null) ? ViewData.Model.Template.Id : 0)%>

    <ul>
		<li>
			<label for="Template_MetaData">MetaData:</label>
			<div>
				<%= Html.TextBox("Template.MetaData", 
					(ViewData.Model.Template != null) ? ViewData.Model.Template.MetaData.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Template.MetaData")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save Template") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<TemplatesController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
