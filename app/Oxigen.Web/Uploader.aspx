<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Uploader.aspx.cs" Inherits="OxigenIIPresentation.Uploader" %>
<%@ Register assembly="Aurigma.ImageUploader" namespace="Aurigma.ImageUploader" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<title>Aurigma Image Uploader Demos — User Quota Demo</title>
	<meta http-equiv="MSThemeCompatible" content="yes" />
	<link href="common.css" type="text/css" rel="stylesheet" />
	<link href="style.css" type="text/css" rel="stylesheet" />
	<style type="text/css">
		.ScreenStyle {background-color:#ffffff;font-family:verdana;font-size:11px;padding:10px;text-align:center}
    </style>
	<script type="text/javascript">
	  //<![CDATA[
	  var imageUploader1 = null;

	  function formatFileSize(value) {
	    if (value < 1024) {
	      return value + " b";
	    }
	    else if (value < 1048576) {
	      return Math.round(value / 1024) + " kb";
	    }
	    else {
	      return Math.round(value / 10485.76) / 100 + " mb";
	    }
	  }

	  function ImageUploader1_FullPageLoad() {
	    imageUploader1 = getImageUploader("<%=ImageUploader1.ClientID%>");
	    document.getElementById("spanMaxFileCount").innerHTML = imageUploader1.getMaxFileCount();
	    document.getElementById("spanMaxTotalFileSize").innerHTML = formatFileSize(parseInt(imageUploader1.getMaxTotalFileSize()));

	    imageUploader1.style.width = "100%";
	    imageUploader1.parentNode.style.width = "100%";
	    //Installation Progress for Java version of IU add 2 parent controls around ImageUploader object
	    if (window['<%= ImageUploader1.JavaScriptWriterVariableName %>'].getControlType() == "Java")
	      imageUploader1.parentNode.parentNode.style.width = "100%";
	  }

	  function ImageUploader1_UploadFileCountChange() {
	    if (imageUploader1) {
	      document.getElementById("spanUploadFileCount").innerHTML = parseInt(imageUploader1.getUploadFileCount());
	      var imgWidth = parseInt(imageUploader1.getUploadFileCount()) / parseInt(imageUploader1.getMaxFileCount()) * 132;
	      document.getElementById("imgUploadFileCount").style.width = Math.round(imgWidth) + "px";

	      document.getElementById("spanTotalFileSize").innerHTML = formatFileSize(parseInt(imageUploader1.getTotalFileSize()));
	      imgWidth = parseInt(imageUploader1.getTotalFileSize()) / parseInt(imageUploader1.getMaxTotalFileSize()) * 132;
	      document.getElementById("imgTotalFileSize").style.width = Math.round(imgWidth) + "px";
	    }
	  }

	  function ImageUploader1_ViewChange(Pane) {
	    if (imageUploader1) {
	      document.getElementById("selectView").selectedIndex = parseInt(imageUploader1.getFolderView());
	    }
	  }

	  function selectView_change() {
	    if (imageUploader1) {
	      var selectView = document.getElementById("selectView");
	      imageUploader1.setFolderView(parseInt(selectView.options[selectView.selectedIndex].value));
	    }
	  }
	  
    function ImageUploader1_BeforeUpload() {
	    getImageUploader("<%=ImageUploader1.ClientID%>").setButtonStopText("Stop");
	    needToHideButtonStop = false;
    }

    function ImageUploader1_Progress(Status, Progress, ValueMax, Value, StatusText) {
      //Stop button should be displayed only during the upload process. If the upload 
      //completed (either successfully or not), the button should be hidden.
      needToHideButtonStop = (Status == "COMPLETE" || Status == "ERROR" || Status == "CANCEL");
    }
    	  
	//]]>
	</script>

</head>
<body>
	<form id="form1" runat="server">
		<div class="Ab">
			<div class="Ab-b">
				<h2>User&nbsp;Quota</h2>
							
				<div class="Info">
					Select up to <strong><span id="spanMaxFileCount">x</span></strong> files for upload.
					<br />
					<br />
					<strong><span id="spanUploadFileCount">0</span></strong>&nbsp;files are selected.
					<br />
					<table cellspacing="0" cellpadding="0" border="0" class="Progress">
						<tbody>
							<tr>
								<td class="Left">
								</td>
								<td class="Panel">
									<img id="imgUploadFileCount" src="../Images/ProgressGreen.gif" alt="" class="Value" /></td>
								<td class="Right">
								</td>
							</tr>
						</tbody>
					</table>
					<br />
					<br />
					The total file size should be smaller then <strong><span id="spanMaxTotalFileSize">x</span></strong>.
					<br />
					<br />
					Total size os selected files is <strong><span id="spanTotalFileSize">0</span></strong>.
					<br />
					<table cellspacing="0" cellpadding="0" border="0" class="Progress">
						<tbody>
							<tr>
								<td class="Left">
								</td>
								<td class="Panel">
									<img id="imgTotalFileSize" src="../Images/ProgressGreen.gif" alt="" class="Value" /></td>
								<td class="Right">
								</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
			<div class="Ab-A">
				<ul class="Toolbar">
					<li><a class="selectAll" href="#" onclick="imageUploader1.SelectAll();return false;">Select All</a></li>
					<li><a class="selectNone" href="#" onclick="imageUploader1.DeselectAll();return false;">Deselect All</a></li>
					<li><a class="send" href="#" onclick="imageUploader1.Send();return false;">Upload</a></li>
					<li><a class="refresh" href="#" onclick="imageUploader1.Refresh();return false;">Refresh</a></li>
					<li>
						<select id="selectView" onchange="selectView_change();">
							<option value="0" selected="selected">Thumbnails</option>
							<option value="1">Icons</option>
							<option value="2">List</option>
							<option value="3">Details</option>
						</select>
					</li>
				</ul>
									
				<cc1:ImageUploader ID="ImageUploader1" runat="server" 
							Width="610"
							Height="490" 
							OnClientBeforeUpload="ImageUploader1_BeforeUpload"
        			OnClientProgress="ImageUploader1_Progress"
							OnClientUploadFileCountChange="ImageUploader1_UploadFileCountChange"
							OnClientViewChange="ImageUploader1_ViewChange"												
							OnClientFullPageLoad="ImageUploader1_FullPageLoad">
							<InstallationProgress 
								Visible="True"
								ProgressCssClass="ScreenStyle"
								InstructionsCssClass="ScreenStyle" />
						</cc1:ImageUploader>
			</div>
		</div>
	</form>
</body>
</html>
