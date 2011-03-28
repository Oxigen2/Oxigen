<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="OxigenIIPresentation.Gallery" %>
<%@ OutputCache Duration="1" VaryByParam="None" %>
<%@ Import Namespace="System.Xml" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<title>Aurigma Image Uploader Demos — Optimized Upload Demo — Uploaded Files</title>
	<link href="../common.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<form id="Form1" runat="server">
		<h1>Uploaded Files</h1>
		<asp:DataList ID="DataList1" Width="100%" RepeatLayout="Table" RepeatDirection="Vertical"
			RepeatColumns="4" runat="server">
			<HeaderTemplate>
			</HeaderTemplate>
			<ItemTemplate>
				<a href="<%# galleryPath + EncodeFileName(((XmlElement)(Container.DataItem)).GetAttribute("name")) %>"
					target="_blank">
					<img src="<%# galleryPath + "Thumbnails/" + EncodeFileName(((XmlElement)(Container.DataItem)).GetAttribute("thumbName")) %>"
						alt="<%# Server.HtmlEncode(((XmlElement)(Container.DataItem)).GetAttribute("name"))%>" title="<%# Server.HtmlEncode(((XmlElement)(Container.DataItem)).GetAttribute("name"))%>" />
				</a>
				<br />
				<br />
				<b>Name:</b>
				<%# Server.HtmlEncode(((XmlElement)(Container.DataItem)).GetAttribute("name")) %>
				<br />
				<b>Dimensions:</b>
				<%# Server.HtmlEncode(((XmlElement)(Container.DataItem)).GetAttribute("width")) %>
				x
				<%# Server.HtmlEncode(((XmlElement)(Container.DataItem)).GetAttribute("height")) %>
				<br />
				<b>Description:</b>
				<%# Server.HtmlEncode(((XmlElement)(Container.DataItem)).GetAttribute("description")) %>
			</ItemTemplate>
			<FooterTemplate>
			</FooterTemplate>
		</asp:DataList>
	</form>
	
</body>
</html>
