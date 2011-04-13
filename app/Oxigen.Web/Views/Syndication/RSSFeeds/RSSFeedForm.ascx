<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.Syndication.RSSFeedFormViewModel>" %>
<%@ Import Namespace="Oxigen.Core.Syndication" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>
<%@ Import Namespace="Oxigen.Web.Controllers.Syndication" %> 

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
                     $("#RSSFeed_Publisher_Id").val(ui.item.id);
                     $.getJSON("/Publisher/" + ui.item.id + "/GetSlideFolders", function (result) {
                         var options = $("#slide_folder_options");
                         //don't forget error handling!
                         $.each(result, function (item) {
                             options.append($("<option />").val(item.Id).text(item.Name));
                         });
                     });

                 }
             });
         });
</script>


<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("RSSFeed.Id", (ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.Id : 0)%>

    <ul>
        <li>
			<label for="RSSFeed_Publisher">Publisher:</label>
			<div>
				<%=Html.Hidden("RSSFeed.Publisher.Id",
                                  (ViewData.Model.RSSFeed != null)
                                             ? ViewData.Model.RSSFeed.Publisher.Id.ToString()
                                      : "")%>

                    <%=Html.TextBox("PublisherDisplayName", (ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.Publisher.DisplayName : "")
                    %>
                   
			</div>
			<%= Html.ValidationMessage("RSSFeed.Publisher.Id")%>
		</li>
		<li>
			<label for="RSSFeed_URL">URL:</label>
			<div>
				<%= Html.TextBox("RSSFeed.URL", 
					(ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.URL.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("RSSFeed.URL")%>
		</li>
        <li>
			<label for="RSSFeed_Name">Name:</label>
			<div>
				<%= Html.TextBox("RSSFeed.Name", 
					(ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.Name.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("RSSFeed.Name")%>
		</li>
		<li>
			<label for="RSSFeed_LastChecked">LastChecked:</label>
			<div>
				<%= Html.TextBox("RSSFeed.LastChecked", 
					(ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.LastChecked.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("RSSFeed.LastChecked")%>
		</li>
		<li>
			<label for="RSSFeed_LastItem">LastItem:</label>
			<div>
				<%= Html.TextBox("RSSFeed.LastItem", 
					(ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.LastItem.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("RSSFeed.LastItem")%>
		</li>
		<li>
			<label for="RSSFeed_Template_Name">Template:</label>
			<div>
				<%= Html.TextBox("RSSFeed.Template.Name", 
					(ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.Template.Name : "")%>
			</div>
			<%= Html.ValidationMessage("RSSFeed.Template.Name")%>
		</li>
		<li>
			<label for="RSSFeed_XSLT">XSLT:</label>
			<div>
				<%= Html.TextBox("RSSFeed.XSLT", 
					(ViewData.Model.RSSFeed != null) ? ViewData.Model.RSSFeed.XSLT.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("RSSFeed.XSLT")%>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save RSSFeed") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<RSSFeedsController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
