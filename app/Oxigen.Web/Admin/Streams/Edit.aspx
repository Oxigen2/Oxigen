<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="OxigenIIPresentation.Admin.Streams.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
  addToLocalNav('Users', '../Users/List')
  addToLocalNav('Subscriptions', '../Subscriptions/List')
  addToLocalNav('Folders', '../Folders/List')
  addToLocalNav('Content', '../Content/List')
  addToLocalNav('Slides', '../Slides/List')
  addToLocalNav('Stream Scheduling', '../StreamSchedule/List')
</script>
<h1>Edit Stream</h1>
  <div class="OxiValidation"><asp:label id="lblValidationMessage" runat="server" /></div>
  <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtName" runat="server">Stream Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvName" controltovalidate="txtName" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtDescription" runat="server">Description:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtDescription" TextMode="MultiLine" Rows="5" cssclass="TextAreaW1 WithFocusHighlight" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvDescription" controltovalidate="txtDescription" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="lbStreams" runat="server">Available Slides:</asp:Label></div>
      <div class="FormField"><input type="text" class="EditBoxW1 WithFocusHighlight" maxlength="100" onkeyup="filterFunction('ForJSList1', this)"/></div>
      <div class="FormSpacer"></div>
      <div class="FormField" id="ForJSList1">
        <asp:ListBox onchange="selectFunction(this);" SelectionMode="Multiple" TabIndex="1" id="lbStreams" cssclass="ListBoxW1 WithFocusHighlight" Rows="5" runat="server">
          <asp:ListItem>Hello</asp:ListItem>
          <asp:ListItem>Goodbye</asp:ListItem>
        </asp:ListBox>
      </div>
      <div onclick="adminAddStreams('ForJSList1','ForJSList2')" class="ArrowAdd"></div>
      <div onclick="adminRemoveStreams('ForJSList2')" class="ArrowRemove"></div>
    </div>
    <div class="FormBox">
      <div class="FormLabel">Stream Type:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2" id="ForJSRequest"><label><asp:RadioButton onclick="adminCheckPasswordRequest()" ID="rbTypePublic" GroupName="TypeGroup" Checked="true" CssClass="Radio WithFocusHighlight" runat="server"/>Public</label><label><asp:RadioButton ID="rbTypePrivate" onclick="adminCheckPasswordRequest()" GroupName="TypeGroup" CssClass="Radio2 WithFocusHighlight" runat="server"/>Private</label></div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtPassword" runat="server">Password:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField" id="ForJSRequest1"><asp:textbox TabIndex="1" id="txtPassword" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvPassword" controltovalidate="txtPassword" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel">Accept Password Requests:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2" id="ForJSRequest2"><label><asp:RadioButton ID="rbRequestYes" GroupName="RequestGroup" Checked="true" CssClass="Radio WithFocusHighlight" runat="server"/>Yes</label><label><asp:RadioButton ID="rbRequestNo" GroupName="RequestGroup" CssClass="Radio2 WithFocusHighlight" runat="server"/>No</label></div>
    </div>
  </div>
  <script type="text/javascript">
    adminCheckPasswordRequest();
  </script>
  <div class="AdminRight">
    <div class="FormBox">
      <div class="FormLabel">Suppressed:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2"><label><asp:RadioButton ID="rbSuppressedYes" GroupName="SuppressedGroup" CssClass="Radio WithFocusHighlight" runat="server"/>Yes</label><label><asp:RadioButton ID="rbSuppressedNo" GroupName="SuppressedGroup" Checked="true" CssClass="Radio2 WithFocusHighlight" runat="server"/>No</label></div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtLongDescription" runat="server">Long Description:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtLongDescription" TextMode="MultiLine" Rows="5" cssclass="TextAreaW1 WithFocusHighlight" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvLongDescription" controltovalidate="txtLongDescription" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="lbStreams2" runat="server">Scheduled Slides:</asp:Label></div>
      <div class="FormField"><input type="text" class="EditBoxW1 WithFocusHighlight" maxlength="100" onkeyup="filterFunction('ForJSList2', this)"/></div>
      <div class="FormSpacer"></div>
      <div class="FormField" id="ForJSList2"><asp:ListBox onchange="selectFunction(this);" SelectionMode="Multiple" TabIndex="1" id="lbStreams2" cssclass="ListBoxW1 WithFocusHighlight" Rows="5" runat="server"/></div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="lbCate" runat="server">Select Category:</asp:Label></div>
      <div class="FormField"><input type="text" class="EditBoxW1 WithFocusHighlight" maxlength="100" onkeyup="filterFunction('ForJSList3', this)"/></div>
      <div class="FormSpacer"></div>
      <div class="FormField" id="ForJSList3">
        <asp:ListBox onchange="selectFunction(this);" SelectionMode="Single" TabIndex="1" id="lbCate" cssclass="ListBoxW1 WithFocusHighlight" Rows="5" runat="server">
          <asp:ListItem>Hello</asp:ListItem>
          <asp:ListItem>Goodbye</asp:ListItem>
        </asp:ListBox>
      </div>
    </div>
  </div>
  <div id="ForJSHidden1">
    <asp:HiddenField runat="server" />
  </div>
  <div class="FormButtons">
    <div class="Info"><span class="Asterisk">*</span> Indicates a required field.</div>
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><a onclick="adminPopStreamHidden('ForJSHidden1','ForJSList2')" href="List.aspx">Next</a></span>
      <span class="RightEnd"></span>
    </div>
  </div>
<script type="text/javascript">
  /*<![CDATA[*/
  initArrays();
  /*]]>*/
</script>
</asp:Content>
