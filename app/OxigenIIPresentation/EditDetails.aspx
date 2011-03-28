<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="EditDetails.aspx.cs" Inherits="OxigenIIPresentation.EditDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="uploader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="scm" OnAsyncPostBackError="AsyncPostbackError" runat="server" />
<div class="PageTitle">
  <div><span class="NormalTabLeft"></span><span class="NormalTabMiddle">Edit My Details</span><span class="NormalTabRight"></span></div>
</div>
<div class="PanelNormalTop">
  <p><asp:literal id="ShortDescription" runat="server" /></p>
</div>
<div class="PanelNormal">
  <div class="OxiValidation"><asp:literal id="ValidationMessage" runat="server"/></div>
    <div class="AdminLeft">
    <div class="FormBox">
      <div class="FormLabel"><asp:Label ID="Label1" AssociatedControlID="txtEmail" runat="server">Email Address:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtEmail" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/>
        <asp:Panel ID="rfvEmail" CssClass="ValidationInfo" Visible="false" runat="server"><asp:Literal ID="Literal1" runat="server">Please enter a valid email address</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label ID="Label2" AssociatedControlID="txtPassword1" runat="server">Set New Password</asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtPassword1" Text="fgikxfdkg" TextMode="Password" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/>
        <asp:Panel ID="rfvPassword1" CssClass="ValidationInfo" Visible="false" runat="server"><asp:Literal ID="Literal2" runat="server">Please enter a valid password</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label ID="Label3" AssociatedControlID="txtPassword2" runat="server">Confirm New Password:</asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtPassword2" TextMode="Password" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/>
        <asp:Panel ID="rfvPassword2" CssClass="ValidationInfo"  Visible="false" runat="server"><asp:Literal ID="Literal3" runat="server">Please comfirm new password</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtFirstName" runat="server">First Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtFirstName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvFirstName" controltovalidate="txtFirstName" display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="txtLastName" runat="server">Last Name:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField"><asp:textbox TabIndex="1" id="txtLastName" cssclass="EditBoxW1 WithFocusHighlight" maxlength="150" runat="server"/></div>
      <asp:requiredfieldvalidator id="rfvLastName" controltovalidate="txtLastName"  display="dynamic" enableclientscript="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:requiredfieldvalidator>
    </div>
    <div class="FormBox">
      <div class="FormLabel">Gender:&nbsp;<span class="Asterisk">*</span></div>
      <div class="FormField2"><label><asp:RadioButton ID="rbGenderMale" GroupName="GenderGroup" CssClass="Radio WithFocusHighlight" runat="server"/>Male</label><label><asp:RadioButton ID="rbGenderFemale" GroupName="GenderGroup" Checked="true" CssClass="Radio2 WithFocusHighlight" runat="server"/>Female</label></div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label runat="server">Date of Birth:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField DateFields"><asp:DropDownList ID="dobDay" runat="server" /><asp:DropDownList ID="dobMonth" runat="server" /><asp:DropDownList ID="dobYear" runat="server" /></div>
      <asp:panel id="rfvDOB" Visible="false" runat="server"><span class="ValidationInfo">This is a required field</span></asp:panel>
   </div>
  </div>
  <div class="AdminRight">
    <div class="FormBox" id="ForJSCountry">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlCountry" runat="server">Country:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList CssClass="DropDownW1 WithFocusHighlight" ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="Countries_SelectedIndexChanged" runat="server"/>
        <asp:Panel ID="rfvCountry" CssClass="ValidationInfo" Visible="false" runat="server"><asp:Literal runat="server">Please select a country</asp:Literal></asp:Panel>
      </div>
    </div>
    <asp:UpdatePanel ID="updGeo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Panel ID="StatePanel" runat="server">
        <div class="FormBox" id="ForJSState">
          <div class="FormLabel"><asp:Label AssociatedControlID="ddlState" runat="server">State:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
          <div class="FormField">
            <asp:DropDownList CssClass="DropDownW1 WithFocusHighlight" ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="States_SelectedIndexChanged" runat="server" />
            <asp:Panel ID="rfvState" CssClass="ValidationInfo" Visible="false" runat="server"><asp:Literal runat="server">Please select a state</asp:Literal></asp:Panel>
          </div>
        </div>
        </asp:Panel>
        <div class="FormBox" id="ForJSCity">
          <div class="FormLabel"><asp:Label AssociatedControlID="ddlTownCities" runat="server">Town/City:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
          <div class="FormField">
            <asp:DropDownList CssClass="DropDownW1 WithFocusHighlight" ID="ddlTownCities" runat="server"/>
          </div>
          <asp:Panel ID="rfvTown" CssClass="ValidationInfo" Visible="false" runat="server"><asp:Literal runat="server">Please select a town/city</asp:Literal></asp:Panel>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="ddlState" EventName="SelectedIndexChanged" />
    </Triggers>
    </asp:UpdatePanel>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlSector" runat="server">Occupation Sector:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList CssClass="DropDownW1 WithFocusHighlight" ID="ddlSector" runat="server"></asp:DropDownList>
        <asp:Panel ID="rfvSector" CssClass="ValidationInfo" Visible="false" runat="server"><asp:Literal runat="server">Please select a sector</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlLevel" runat="server">Employment Level:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList CssClass="DropDownW1 WithFocusHighlight" ID="ddlLevel" runat="server"></asp:DropDownList>
        <asp:Panel ID="rfvLevel" CssClass="ValidationInfo"  Visible="false" runat="server"><asp:Literal runat="server">Please select an employment level</asp:Literal></asp:Panel>
      </div>
    </div>
    <div class="FormBox">
      <div class="FormLabel"><asp:Label AssociatedControlID="ddlIncome" runat="server">Annual Household Income:&nbsp;<span class="Asterisk">*</span></asp:Label></div>
      <div class="FormField">
        <asp:DropDownList CssClass="DropDownW1 WithFocusHighlight" ID="ddlIncome" runat="server"></asp:DropDownList>
        <asp:Panel ID="rfvIncome" CssClass="ValidationInfo" Visible="false" runat="server"><asp:Literal runat="server">Please select an income</asp:Literal></asp:Panel>
      </div>
    </div>
  </div>
  <div class="FormButtons">
    <div class="Info"><span class="Asterisk">*</span> Indicates a required field.</div>
    <div class="ButtonStd">
      <span class="LeftEnd"></span>
      <span class="Centre"><asp:linkbutton ID="btnNext" OnClick="Next_Click" runat="server">Save Changes</asp:linkButton></span>
      <span class="RightEnd"></span>
    </div>
    <div class="ButtonStdSpacer"></div>
    <div class="ButtonStd ButtonStdRed">
      <span class="LeftEnd"></span>
        <span class="Centre"><a href="#" onclick="BlackOut('DivPopUpRemove');return false">Delete Account</a></span>
      <span class="RightEnd"></span>
    </div>
  </div>
  <div class="BottomFix"></div>
</div>
<div class="PanelNormalBottom"></div>


      <div class="DivPopUp DivPopUpSmall" id="DivPopUpRemove">
      <div class="DivPopUpClose DivPopUpClose2" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <p>This will delete all streams and content associated with this account and will be unrecoverable.</p>
        <p>Please be aware that to delete your account you will need to uninstall your screensaver first</p>
        <p>Are you sure you wish to delete your account?</p>
        <div class="FormButtons" style="width:470px;">
          <div class="ButtonStd">
            <span class="LeftEnd"></span>
            <span class="Centre"><asp:LinkButton ID="btnDeleteAccount" OnClick="DeleteAccount_Click" runat="server">Delete Account</asp:LinkButton></span>
            <span class="RightEnd"></span>
          </div>
          <div class="ButtonStdSpacer"></div>
          <div class="ButtonStd ButtonStdRed">
            <span class="LeftEnd"></span>
            <span class="Centre"><a href="#" onclick="globalClosePopUp();return false">Go Back</a></span>
            <span class="RightEnd"></span>
          </div>
        </div>
      </div>
      <div class="DivPopBottom"></div>
    </div>


</asp:Content>
