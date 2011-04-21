<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Oxigen.ApplicationServices.ViewModels.RSSFeedFormViewModel>" %>
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
                     $.getJSON("/SlideFolders/ListByProducer/" + ui.item.id, function (result) {
                         var options = $("#RSSFeed_SlideFolder_Id");
                         options.empty();
                         $.each(result, function (index, item) {                            
                             options.append($("<option />").val(item.Id).text(item.SlideFolderName));
                         });
                     });
                     $.getJSON("/Templates/ListByProducer/" + ui.item.id, function (result) {
                         var options = $("#RSSFeed_Template_Id");
                         options.empty();
                         $.each(result, function (index, item) {
                             options.append($("<option />").val(item.Id).text(item.Name));
                         });
                     });
                     $.getJSON("/Channels/ListByProducer/" + ui.item.id, function (result) {
                         var options = $("#RSSFeed_Channel_Id");
                         options.empty();
                         $.each(result, function (index, item) {
                             options.append($("<option />").val(item.Id).text(item.ChannelName));
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
			<label for="PublisherDisplayName">Publisher:</label>
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
			<label for="RSSFeed_SlideFolder_Id">Slide Folder:</label>
			<div>
				<select id="RSSFeed_SlideFolder_Id" name="RSSFeed.SlideFolder.Id"></select>
                   
			</div>
			<%= Html.ValidationMessage("RSSFeed.SlideFolder.Id")%>
		</li>
		<li>
			<label for="RSSFeed_Template_Id">Template:</label>
			<div>
				<select id="RSSFeed_Template_Id" name="RSSFeed.Template.Id"></select>
			</div>
			<%= Html.ValidationMessage("RSSFeed.Template.Id")%>
		</li>
        <li>
			<label for="RSSFeed_Channel_Id">Channel:</label>
			<div>
				<select id="RSSFeed_Channel_Id" name="RSSFeed.Channel.Id"></select>
			</div>
			<%= Html.ValidationMessage("RSSFeed.Channel.Id")%>
		</li>
		<li>
			<label for="RSSFeed_XSLT">XSLT:</label>
			<div>
				<%= Html.TextArea("RSSFeed.XSLT", 
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
