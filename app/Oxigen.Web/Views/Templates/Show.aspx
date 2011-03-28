<%@ Page Title="Template Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.Template>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Template Details</h1>

    <ul>
		<li>
			<label for="Template_MetaDate">MetaDate:</label>
            <span id="Template_MetaDate"><%= Server.HtmlEncode(ViewData.Model.MetaDate.ToString()) %></span>
		</li>
		<li>
			<label for="Template_OwnedBy">OwnedBy:</label>
            <span id="Template_OwnedBy"><%= Server.HtmlEncode(ViewData.Model.OwnedBy.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<TemplatesController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
