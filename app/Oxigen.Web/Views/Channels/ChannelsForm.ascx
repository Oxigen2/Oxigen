<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.ChannelsFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("Channels.Id", (ViewData.Model.Channels != null) ? ViewData.Model.Channels.Id : 0)%>

    <ul>
		<li>
			<label for="Channels_CategoryID">CategoryID:</label>
			<div>
				<%= Html.TextBox("Channels.CategoryID", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.CategoryID.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.CategoryID")%>
		</li>
		<li>
			<label for="Channels_Publisher">Publisher:</label>
			<div>
				<%= Html.TextBox("Channels.Publisher", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.Publisher.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.Publisher")%>
		</li>
		<li>
			<label for="Channels_ChannelName">ChannelName:</label>
			<div>
				<%= Html.TextBox("Channels.ChannelName", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ChannelName.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ChannelName")%>
		</li>
		<li>
			<label for="Channels_ChannelGUID">ChannelGUID:</label>
			<div>
				<%= Html.TextBox("Channels.ChannelGUID", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ChannelGUID.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ChannelGUID")%>
		</li>
		<li>
			<label for="Channels_ChannelDescription">ChannelDescription:</label>
			<div>
				<%= Html.TextBox("Channels.ChannelDescription", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ChannelDescription.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ChannelDescription")%>
		</li>
		<li>
			<label for="Channels_ChannelLongDescription">ChannelLongDescription:</label>
			<div>
				<%= Html.TextBox("Channels.ChannelLongDescription", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ChannelLongDescription.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ChannelLongDescription")%>
		</li>
		<li>
			<label for="Channels_Keywords">Keywords:</label>
			<div>
				<%= Html.TextBox("Channels.Keywords", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.Keywords.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.Keywords")%>
		</li>
		<li>
			<label for="Channels_ImagePath">ImagePath:</label>
			<div>
				<%= Html.TextBox("Channels.ImagePath", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ImagePath.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ImagePath")%>
		</li>
		<li>
			<label for="Channels_bHasDefaultThumbnail">bHasDefaultThumbnail:</label>
			<div>
				<%= Html.TextBox("Channels.bHasDefaultThumbnail", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.bHasDefaultThumbnail.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.bHasDefaultThumbnail")%>
		</li>
		<li>
			<label for="Channels_bLocked">bLocked:</label>
			<div>
				<%= Html.TextBox("Channels.bLocked", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.bLocked.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.bLocked")%>
		</li>
		<li>
			<label for="Channels_bAcceptPasswordRequests">bAcceptPasswordRequests:</label>
			<div>
				<%= Html.TextBox("Channels.bAcceptPasswordRequests", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.bAcceptPasswordRequests.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.bAcceptPasswordRequests")%>
		</li>
		<li>
			<label for="Channels_ChannelPassword">ChannelPassword:</label>
			<div>
				<%= Html.TextBox("Channels.ChannelPassword", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ChannelPassword.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ChannelPassword")%>
		</li>
		<li>
			<label for="Channels_ChannelGUIDSuffix">ChannelGUIDSuffix:</label>
			<div>
				<%= Html.TextBox("Channels.ChannelGUIDSuffix", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ChannelGUIDSuffix.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ChannelGUIDSuffix")%>
		</li>
		<li>
			<label for="Channels_NoContent">NoContent:</label>
			<div>
				<%= Html.TextBox("Channels.NoContent", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.NoContent.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.NoContent")%>
		</li>
		<li>
			<label for="Channels_NoFollowers">NoFollowers:</label>
			<div>
				<%= Html.TextBox("Channels.NoFollowers", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.NoFollowers.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.NoFollowers")%>
		</li>
		<li>
			<label for="Channels_AddDate">AddDate:</label>
			<div>
				<%= Html.TextBox("Channels.AddDate", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.AddDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.AddDate")%>
		</li>
		<li>
			<label for="Channels_EditDate">EditDate:</label>
			<div>
				<%= Html.TextBox("Channels.EditDate", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.EditDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.EditDate")%>
		</li>
		<li>
			<label for="Channels_MadeDirtyLastDate">MadeDirtyLastDate:</label>
			<div>
				<%= Html.TextBox("Channels.MadeDirtyLastDate", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.MadeDirtyLastDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.MadeDirtyLastDate")%>
		</li>
		<li>
			<label for="Channels_ContentLastAddedDate">ContentLastAddedDate:</label>
			<div>
				<%= Html.TextBox("Channels.ContentLastAddedDate", 
					(ViewData.Model.Channels != null) ? ViewData.Model.Channels.ContentLastAddedDate.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Channels.ContentLastAddedDate")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save Channels") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<ChannelsController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
