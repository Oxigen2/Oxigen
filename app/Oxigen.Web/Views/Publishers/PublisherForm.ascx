<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.PublisherFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("Publisher.Id", (ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.Id : 0)%>

    <ul>
		<li>
			<label for="Publisher_UserID">UserID:</label>
			<div>
				<%= Html.TextBox("Publisher.UserID", 
					(ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.UserID.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Publisher.UserID")%>
		</li>
		<li>
			<label for="Publisher_FirstName">FirstName:</label>
			<div>
				<%= Html.TextBox("Publisher.FirstName", 
					(ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.FirstName.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Publisher.FirstName")%>
		</li>
		<li>
			<label for="Publisher_LastName">LastName:</label>
			<div>
				<%= Html.TextBox("Publisher.LastName", 
					(ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.LastName.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Publisher.LastName")%>
		</li>
		<li>
			<label for="Publisher_DisplayName">DisplayName:</label>
			<div>
				<%= Html.TextBox("Publisher.DisplayName", 
					(ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.DisplayName.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Publisher.DisplayName")%>
		</li>
		<li>
			<label for="Publisher_EmailAddress">EmailAddress:</label>
			<div>
				<%= Html.TextBox("Publisher.EmailAddress", 
					(ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.EmailAddress.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Publisher.EmailAddress")%>
		</li>
		<li>
			<label for="Publisher_UsedBytes">UsedBytes:</label>
			<div>
				<%= Html.TextBox("Publisher.UsedBytes", 
					(ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.UsedBytes.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Publisher.UsedBytes")%>
		</li>
		<li>
			<label for="Publisher_TotalAvailableBytes">TotalAvailableBytes:</label>
			<div>
				<%= Html.TextBox("Publisher.TotalAvailableBytes", 
					(ViewData.Model.Publisher != null) ? ViewData.Model.Publisher.TotalAvailableBytes.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Publisher.TotalAvailableBytes")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save Publisher") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<PublishersController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
