<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.TemplateFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm("Create", "Templates", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("Template.Id", (ViewData.Model.Template != null) ? ViewData.Model.Template.Id : 0)%>

    <ul>
        <li>
            <label for="File">
                MetaDate:</label>
            <div>
                <input type="file" id="fileUpload" name="fileUpload" size="23" />
            </div>
            <%= Html.ValidationMessage("File")%>
        </li>
        <li>
            <label for="Template_MetaDate">
                MetaDate:</label>
            <div>
                <%= Html.TextBox("Template.MetaData", 
					(ViewData.Model.Template != null) ? ViewData.Model.Template.MetaData.ToString() : "")%>
            </div>
            <%= Html.ValidationMessage("Template.MetaData")%>
        </li>
        <li>
			<label for="Template_OwnedBy">OwnedBy:</label>
			<div>
				<%= Html.TextBox("Template.OwnedBy.Id", 
					(ViewData.Model.Template != null) ? ViewData.Model.Template.OwnedBy.Id.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Template.OwnedBy.Id")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save Template") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<TemplatesController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
