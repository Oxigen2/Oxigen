<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.TemplateFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
 
     <script type="text/javascript" language="javascript">
         $(function () {
             $("#PublisherDisplayName").autocomplete({
                 source: function (request, response) {
                     $.get(
                         '/Publishers/GetPublishersByPartialName',
                         { q: request.term },
                         function (data) {
                             response($.map(data, function (publisher) {
                                 var name = publisher.DisplayName + ' (' + publisher.EmailAddress + ')';
                                 return { label: name, value: name, id: publisher.Id }
                             }))
                         }
                     );
                 },
                 select: function (event, ui) {
                     $("#PublisherDisplayName").val(ui.item.label);
                     $("#Template_Publisher_Id").val(ui.item.id);
                 }
             });
         });
</script>

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm(ViewContext.RouteData.Values["action"] as string, "Templates", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("Template.Id", (ViewData.Model.Template != null) ? ViewData.Model.Template.Id : 0)%>

    <ul>
        <li>
            <label for="File">
                Template File:
            </label>
            <div>
                <input type="file" id="file" name="file" size="23" />
            </div>
            <%= Html.ValidationMessage("File")%>
        </li>
        <li>
            <label for="Template_MetaData">
                MetaData:</label>
            <div>
                <%= Html.TextBox("Template.MetaData",
                    (ViewData.Model.Template != null && ViewData.Model.Template.MetaData != null) ? ViewData.Model.Template.MetaData.ToString() : "")%>
            </div>
            <%= Html.ValidationMessage("Template.MetaData")%>
        </li>
        <li>
			<label for="Template_Publisher">Publisher:</label>
			<div>
				<%=Html.Hidden("Template.Publisher.Id",
                                  (ViewData.Model.Template != null)
                                      ? ViewData.Model.Template.Publisher.Id.ToString()
                                      : "")%>

                    <%=Html.TextBox("PublisherDisplayName", (ViewData.Model.Template != null) ? ViewData.Model.Template.Publisher.DisplayName : "")
                    %>
                   
			</div>
			<%= Html.ValidationMessage("Template.Publisher.Id")%>
		</li>
        <li>
			<label for="Template_DisplayDuration">Display Duration (secs):</label>
			<div>
                    <%=Html.TextBox("Template.DisplayDuration", (ViewData.Model.Template != null) ? ViewData.Model.Template.DisplayDuration.ToString() : "10")
                    %>
                   
			</div>
			<%= Html.ValidationMessage("Template.DisplayDuration")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save Template") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<TemplatesController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
