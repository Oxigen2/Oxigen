<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="OxigenIIPresentation.Admin.StreamSchedule.Schedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
  removeFromLocalNav('1,2,3')
  addToLocalNav('Schedule', '../StreamSchedule/Schedule')
  addToLocalNav('Users', '../Users/List')
  addToLocalNav('Subscriptions', '../Subscriptions/List')
  addToLocalNav('Folders', '../Folders/List')
  addToLocalNav('Content', '../Content/List')
  addToLocalNav('Slides', '../Slides/List')
  addToLocalNav('Streams', '../Streams/List')
</script>
<h1>Stream Content Scheduling</h1>
  <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label runat="server">Days of Week:</asp:Label></div>
      <div class="FormField">
        <asp:checkboxlist TabIndex="1" RepeatDirection="Horizontal" cssclass="CbHorizontalList WithFocusHighlight" runat="server">
          <asp:ListItem>M</asp:ListItem>
          <asp:ListItem>T</asp:ListItem>
          <asp:ListItem>W</asp:ListItem>
          <asp:ListItem>T</asp:ListItem>
          <asp:ListItem>F</asp:ListItem>
          <asp:ListItem>S</asp:ListItem>
          <asp:ListItem>S</asp:ListItem>
        </asp:checkboxlist>
      </div>
    </div>
  </div>
  <div class="AdminRight">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtAdvSch" runat="server">Advanced Scheduling:</asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtAdvSch" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
    </div>
  </div>
  <div class="BottomFix"></div>
<div id="ForJSSchedStarts">
    <div class="AdminLeft">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label AssociatedControlID="txtSD" runat="server">Start Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" id="txtSD" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
        <asp:requiredfieldvalidator id="rfvSD" controltovalidate="txtSD" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label AssociatedControlID="txtST" runat="server">Start Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" id="txtST" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
        <asp:requiredfieldvalidator id="rfvST" controltovalidate="txtST" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
      </div>
    </div>
    <div class="AdminLeft">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">Start Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">Start Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
    </div>
    <div class="AdminLeft">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">Start Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">Start Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
    </div>
    <div class="AdminLeft">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">Start Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">Start Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
    </div>
  </div>
  <div id="ForJSSchedEnds">
    <div class="AdminRight">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label AssociatedControlID="txtED" runat="server">End Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" id="txtED" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
        <asp:requiredfieldvalidator id="rfvED" controltovalidate="txtED" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label AssociatedControlID="txtET" runat="server">End Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" id="txtET" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
        <asp:requiredfieldvalidator id="rfvET" controltovalidate="txtET" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
      </div>
    </div>
    <div class="AdminRight">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">End Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">End Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
    </div>
    <div class="AdminRight">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">End Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">End Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
    </div>
    <div class="AdminRight">
      <!-- CALENDAR CONTROL TO GO HERE -->
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">End Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
      <div class="FormBox">
        <div class="FormLabel"><asp:Label runat="server">End Time:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
        <div class="FormField"><asp:textbox TabIndex="1" cssclass="EditBoxW1 WithFocusHighlight" maxlength="10" runat="server"/></div>
      </div>
    </div>
    
  </div>
  <div class="FormButtons">
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><a onclick="adminAddSchedule();return false" href="#">Add Schedule</a></span>
      <span class="RightEnd"></span>
    </div>
  </div>
  <script type="text/javascript">
    initAdminSchedule()
  </script>


</asp:Content>
