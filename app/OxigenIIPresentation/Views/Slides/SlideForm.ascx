<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.SlideFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("Slide.Id", (ViewData.Model.Slide != null) ? ViewData.Model.Slide.Id : 0)%>

    <ul>
		<li>
			<label for="Slide_Filename">Filename:</label>
			<div>
				<%= Html.TextBox("Slide.Filename", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.Filename.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.Filename")%>
		</li>
		<li>
			<label for="Slide_FilenameExtension">FilenameExtension:</label>
			<div>
				<%= Html.TextBox("Slide.FilenameExtension", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.FilenameExtension.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.FilenameExtension")%>
		</li>
		<li>
			<label for="Slide_FilenameNoPath">FilenameNoPath:</label>
			<div>
				<%= Html.TextBox("Slide.FilenameNoPath", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.FilenameNoPath.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.FilenameNoPath")%>
		</li>
		<li>
			<label for="Slide_GUID">GUID:</label>
			<div>
				<%= Html.TextBox("Slide.GUID", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.GUID.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.GUID")%>
		</li>
		<li>
			<label for="Slide_GUID">GUID:</label>
			<div>
				<%= Html.TextBox("Slide.GUID", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.GUID.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.GUID")%>
		</li>
		<li>
			<label for="Slide_SubDir">SubDir:</label>
			<div>
				<%= Html.TextBox("Slide.SubDir", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.SubDir.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.SubDir")%>
		</li>
		<li>
			<label for="Slide_Name">Name:</label>
			<div>
				<%= Html.TextBox("Slide.Name", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.Name.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.Name")%>
		</li>
		<li>
			<label for="Slide_Creator">Creator:</label>
			<div>
				<%= Html.TextBox("Slide.Creator", 
    (ViewData.Model.Slide != null) ? ViewData.Model.Slide.Creator : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.Creator")%>
		</li>
		<li>
			<label for="Slide_Caption">Caption:</label>
			<div>
				<%= Html.TextBox("Slide.Caption", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.Caption : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.Caption")%>
		</li>
		<li>
			<label for="Slide_ClickThroughURL">ClickThroughURL:</label>
			<div>
				<%= Html.TextBox("Slide.ClickThroughURL", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.ClickThroughURL.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.ClickThroughURL")%>
		</li>
		<li>
			<label for="Slide_WebsiteURL">WebsiteURL:</label>
			<div>
				<%= Html.TextBox("Slide.WebsiteURL", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.WebsiteURL : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.WebsiteURL")%>
		</li>
		<li>
			<label for="Slide_DisplayDuration">DisplayDuration:</label>
			<div>
				<%= Html.TextBox("Slide.DisplayDuration", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.DisplayDuration.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.DisplayDuration")%>
		</li>
		<li>
			<label for="Slide_Length">Length:</label>
			<div>
				<%= Html.TextBox("Slide.Length", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.Length.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.Length")%>
		</li>
		<li>
			<label for="Slide_ImagePath">ImagePath:</label>
			<div>
				<%= Html.TextBox("Slide.ImagePath", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.ImagePath.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.ImagePath")%>
		</li>
		<li>
			<label for="Slide_ImagePathWinFS">ImagePathWinFS:</label>
			<div>
				<%= Html.TextBox("Slide.ImagePathWinFS", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.ImagePathWinFS.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.ImagePathWinFS")%>
		</li>
		<li>
			<label for="Slide_ImageName">ImageName:</label>
			<div>
				<%= Html.TextBox("Slide.ImageName", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.ImageName : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.ImageName")%>
		</li>
		<li>
			<label for="Slide_PlayerType">PlayerType:</label>
			<div>
				<%= Html.TextBox("Slide.PlayerType", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.PlayerType : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.PlayerType")%>
		</li>
		<li>
			<label for="Slide_PreviewType">PreviewType:</label>
			<div>
				<%= Html.TextBox("Slide.PreviewType", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.PreviewType : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.PreviewType")%>
		</li>
		<li>
			<label for="Slide_bLocked">bLocked:</label>
			<div>
				<%= Html.TextBox("Slide.bLocked", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.bLocked.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.bLocked")%>
		</li>
		<li>
			<label for="Slide_UserGivenDate">UserGivenDate:</label>
			<div>
				<%= Html.TextBox("Slide.UserGivenDate", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.UserGivenDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.UserGivenDate")%>
		</li>
		<li>
			<label for="Slide_AddDate">AddDate:</label>
			<div>
				<%= Html.TextBox("Slide.AddDate", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.AddDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.AddDate")%>
		</li>
		<li>
			<label for="Slide_EditDate">EditDate:</label>
			<div>
				<%= Html.TextBox("Slide.EditDate", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.EditDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.EditDate")%>
		</li>
		<li>
			<label for="Slide_MadeDirtyLastDate">MadeDirtyLastDate:</label>
			<div>
				<%= Html.TextBox("Slide.MadeDirtyLastDate", 
					(ViewData.Model.Slide != null) ? ViewData.Model.Slide.MadeDirtyLastDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Slide.MadeDirtyLastDate")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save Slide") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<SlidesController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
