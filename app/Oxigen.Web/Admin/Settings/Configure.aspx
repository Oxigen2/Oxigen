<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Configure.aspx.cs" Inherits="OxigenIIPresentation.Admin.Settings.Configure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
  removeFromLocalNav('0,1,2,3')
  addToLocalNav('Configure','Configure')
</script>

<h1>Oxigen Configuration</h1>
<h2>Operation Settings</h2>
<div class="AdminLeft">
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtDefDis" runat="server">Default Display Duration:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtDefDis" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvDefDis" controltovalidate="txtDefDis" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtLogInt" runat="server">Log Exchanger Process Interval:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtLogInt" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvLogInt" controltovalidate="txtLogInt" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtConInt" runat="server">Content Exchanger Process Interval:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtConInt" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvConInt" controltovalidate="txtConInt" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtSofInt" runat="server">Software Updater Process Interval:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtSofInt" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvSofInt" controltovalidate="txtSofInt" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtServerT" runat="server">Server Timeout:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtServerT" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvServerT" controltovalidate="txtServerT" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtAdDis" runat="server">Advert Display Threshold:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtAdDis" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvAdDis" controltovalidate="txtAdDis" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtLogTim" runat="server">Log Timer Interval:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtLogTim" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvLogTim" controltovalidate="txtLogTim" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
</div>
<div class="AdminRight">
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtProtCon" runat="server">Protected Content Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtProtCon" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvProtCon" controltovalidate="txtProtCon" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtMaxLines" runat="server">Maximum Lines:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtMaxLines" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvMaxLines" controltovalidate="txtMaxLines" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtNoAsset" runat="server">"No Asset" Screen Display Duration:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtNoAsset" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvNoAsset" controltovalidate="txtNoAsset" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtTimReq" runat="server">Request Timeout:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtTimReq" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvTimReq" controltovalidate="txtTimReq" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtDateTime" runat="server">Date/Time Difference Tolerance for Content Exchanger:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtDateTime" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvDateTime" controltovalidate="txtDateTime" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtAssetFile" runat="server">Days to Keep Asset Files After They Expire:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtAssetFile" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvAssetFile" controltovalidate="txtAssetFile" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
</div>
<div class="BottomFix"></div>
<h2>Server Settings</h2>
<div class="AdminLeft">
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtPrimDom" runat="server">Primary Domain Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtPrimDom" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvPrimDom" controltovalidate="txtPrimDom" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtSecDom" runat="server">Secondary Domain Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtSecDom" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvSecDom" controltovalidate="txtSecDom" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtNRel" runat="server">Number of Relay Servers to Send Logs to:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtNRel" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvNRel" controltovalidate="txtNRel" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtNRel1" runat="server">Number of Relay Servers to get Configuration from:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtNRel1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvNRel1" controltovalidate="txtNRel" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
</div>
<div class="AdminRight">
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtNRel2" runat="server">Number of Relay Servers to get Stream Slides from:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtNRel2" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvNRel2" controltovalidate="txtNRel2" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtNRel3" runat="server">Number of Relay Servers to get Stream Playlists from:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtNRel3" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvNRel3" controltovalidate="txtNRel3" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>
  <div class="FormBox">
    <div class="FormLabel"><asp:Label AssociatedControlID="txtNMaster" runat="server">Number of Master Servers to get Demographic Data and Subscriptions from:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
    <div class="FormField"><asp:textbox TabIndex="1" id="txtNMaster" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
    <asp:requiredfieldvalidator id="rfvNMaster" controltovalidate="txtNMaster" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
  </div>

</div>
<div class="FormButtons">
  <div class="Info"><span class="Asterisk">*</span> Indicates a required field.</div>
  <div class="ButtonStd">
    <span class="LeftEnd"></span>
    <span class="Centre"><a href="#">Save</a></span>
    <span class="RightEnd"></span>
  </div>
</div>
</asp:Content>
