<%@ Page Title="Template Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Oxigen.Core.Template>" %>
<%@ Import Namespace="Oxigen.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Template Details</h1>

    <ul>
		<li>
			<label for="Template_MetaData">MetaData:</label>
            <span id="Template_MetaData"><%= Server.HtmlEncode(ViewData.Model.MetaData.ToString()) %></span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpressionForAreas<TemplatesController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
