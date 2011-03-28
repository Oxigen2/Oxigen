<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="OxigenIIPresentation.Admin.Slides.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
  addToLocalNav('Users', '../Users/List')
  addToLocalNav('Subscriptions', '../Subscriptions/List')
  addToLocalNav('Folders', '../Folders/List')
  addToLocalNav('Content', '../Content/List')
  addToLocalNav('Streams', '../Streams/List')
  addToLocalNav('Stream Scheduling', '../StreamSchedule/List')
</script>
<h1>Add Slide</h1>
  <div class="OxiValidation"><asp:label id="lblValidationMessage" runat="server" /></div>
  <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlFolder" runat="server">Slide Folder:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList TabIndex="1" id="ddlFolder" cssclass="DropDownW1 WithFocusHighlight" runat="server">
          <asp:ListItem>Slide Folder 1</asp:ListItem>
          <asp:ListItem>Slide Folder 2</asp:ListItem>
          <asp:ListItem>Slide Folder 3</asp:ListItem>
        </asp:DropDownList>
      </div>
      <asp:requiredfieldvalidator id="rfvFolder" controltovalidate="ddlFolder" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtName" runat="server">Slide Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvName" controltovalidate="txtName" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtAuthor" runat="server">Author:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtAuthor" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvAuthor" controltovalidate="txtAuthor" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtCaption" runat="server">Caption:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtCaption" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvCaption" controltovalidate="txtCaption" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <!-- CALENDAR CONTROL NEEDED HERE -->
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtDate" runat="server">Date:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtDate" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvDate" controltovalidate="txtDate" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="lbStreams" runat="server">Available Content:</asp:Label></div>
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
    
  </div>
  <div class="AdminRight">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlType" runat="server">Slide Type:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField" id="ForJSDropDownCheck">
        <asp:DropDownList onchange="adminCheckTemplate()" TabIndex="1" id="ddlType" cssclass="DropDownW1 WithFocusHighlight" runat="server">
          <asp:ListItem>Basic</asp:ListItem>
          <asp:ListItem>Oxigen Template</asp:ListItem>
          <asp:ListItem>Stupeflix</asp:ListItem>
        </asp:DropDownList>
      </div>
      <asp:requiredfieldvalidator id="rfvType" controltovalidate="ddlType" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlTemplate" runat="server">Oxigen Template:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField" id="ForJSDropDownCheck2">
        <asp:DropDownList TabIndex="1" id="ddlTemplate" cssclass="DropDownW1 WithFocusHighlight" runat="server">
          <asp:ListItem>Please select ¬</asp:ListItem>
          <asp:ListItem>Oxigen Template 1</asp:ListItem>
          <asp:ListItem>Oxigen Template 2</asp:ListItem>
          <asp:ListItem>Oxigen Template 3</asp:ListItem>
        </asp:DropDownList>
      </div>
      <asp:requiredfieldvalidator id="rfvTemplate" controltovalidate="ddlTemplate" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <script type="text/javascript">
      adminCheckTemplate();
    </script>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtUrl" runat="server">URL:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtUrl" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvURL" controltovalidate="txtUrl" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtDisplay" runat="server">Display Duration:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtDisplay" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvDisplay" controltovalidate="txtDisplay" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel">Suppressed:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2"><label><asp:RadioButton ID="rbSuppressedYes" GroupName="SuppressedGroup" CssClass="Radio WithFocusHighlight" runat="server"/>Yes</label><label><asp:RadioButton ID="rbSuppressedNo" GroupName="SuppressedGroup" Checked="true" CssClass="Radio2 WithFocusHighlight" runat="server"/>No</label></div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="lbStreams2" runat="server">Content to Convert:</asp:Label></div>
      <div class="FormField"><input type="text" class="EditBoxW1 WithFocusHighlight" maxlength="100" onkeyup="filterFunction('ForJSList2', this)"/></div>
      <div class="FormSpacer"></div>
      <div class="FormField" id="ForJSList2"><asp:ListBox onchange="selectFunction(this);" SelectionMode="Multiple" TabIndex="1" id="lbStreams2" cssclass="ListBoxW1 WithFocusHighlight" Rows="5" runat="server"/></div>
    </div>
<%--    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtTree" runat="server">Geographic Taxonomy Tree:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtTree" cssclass="TextAreaW1 WithFocusHighlight" TextMode="MultiLine" Rows="5" maxlength="600" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvTree" controltovalidate="txtTree" enableViewState="false" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>--%>
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
