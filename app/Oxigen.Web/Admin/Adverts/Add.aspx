<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="OxigenIIPresentation.Admin.Adverts.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Add Advertisement</h1>
   <div class="OxiValidation"><asp:label id="lblValidationMessage" runat="server" /></div>
   <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtName" runat="server">Advertisement Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvName" controltovalidate="txtName" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtCode" runat="server">Client Code:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtCode" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvCode" controltovalidate="txtCode" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtTreeDef" runat="server">Taxonomy Tree Definitions:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtTreeDef" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvTreeDef" controltovalidate="txtTreeDef" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtLocInc" runat="server">Location inclusion:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtLocInc" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvLocInc" controltovalidate="txtLocInc" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtRulesInc" runat="server">Channel Rules Inclusion:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtRulesInc" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvRulesInc" controltovalidate="txtRulesInc" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtDemo" runat="server">Demographic Requirements for Playback:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtDemo" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvDemo" controltovalidate="txtDemo" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
  </div>
  <div class="AdminRight">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtFreq" runat="server">Frequency Cap:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtFreq" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvFreq" controltovalidate="txtFreq" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtWeight" runat="server">Advert Weighting:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtWeight" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvWeight" controltovalidate="txtWeight" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtTree" runat="server">Geographic Taxonomy Tree:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtTree" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvTree" controltovalidate="txtTree" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtLocExc" runat="server">Location Exclusion:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtLocExc" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvLocExc" controltovalidate="txtLocExc" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtRulesExc" runat="server">Channel Rules Exclusion:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtRulesExc" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvRulesExc" controltovalidate="txtRulesExc" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel">Suppressed:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2"><label><asp:RadioButton ID="rbSuppressedYes" GroupName="SuppressedGroup" CssClass="Radio WithFocusHighlight" runat="server"/>Yes</label><label><asp:RadioButton ID="rbSuppressedNo" GroupName="SuppressedGroup" Checked="true" CssClass="Radio2 WithFocusHighlight" runat="server"/>No</label></div>
    </div>
  </div>
  <div class="BottomFix"></div>
  <h2>Schedule</h2>
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
  <div class="FormButtons">
    <div class="Info"><span class="Asterisk">*</span> Indicates a required field.</div>
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><a href="List.aspx">Next</a></span>
      <span class="RightEnd"></span>
    </div>
  </div>
</asp:Content>
