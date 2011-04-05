<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="CreateWizard.aspx.cs" Inherits="OxigenIIPresentation.Create" %>
<%@ Register assembly="Aurigma.ImageUploader" namespace="Aurigma.ImageUploader" tagprefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="scm" />
<div id="ProgressBackHolder"></div>
<div class="DownloadCreatePage">
  <div class="CreateArrowLeft" id="CreateArrowLeft" onclick="createArrowLeft();return false"></div>
  <div class="CreateArrowRight" id="CreateArrowRight" onclick="createArrowRight();return false"></div>
  <div class="ButtonGreen">
    <img id="ForJSButtonGreen2" src="Images/Default/button-green.png" alt="" />
    <a id="CreateNextButton" href="#">Next <span>&raquo;</span></a>
  </div>
  
  <div id="CreateHiddenFields">
    <asp:HiddenField runat="server" />
    <asp:HiddenField runat="server" />
    <asp:HiddenField runat="server" />
    <asp:HiddenField runat="server" />
    <asp:HiddenField runat="server" />
    <asp:HiddenField runat="server" />
  </div>  
  
  <div class="NavigationArrows" id="NavigationArrows">
    <div><a onclick="updateArrowNav(0); return false;" href="#">1. Upload</a><img src="Images/Default/search-tabs-arrow.png" alt="" /><img src="Images/Default/search-tabs-arrow-inactive.png" alt="" /></div>
    <div><a onclick="updateArrowNav(1); return false;" href="#">2. Convert</a><img src="Images/Default/search-tabs-arrow-2.png" alt="" /><img src="Images/Default/search-tabs-arrow-2-inactive.png" alt="" /></div>
    <div><a onclick="updateArrowNav(2); return false;" href="#">3. Post</a><img src="Images/Default/search-tabs-arrow-2.png" alt="" /><img src="Images/Default/search-tabs-arrow-2-inactive.png" alt="" /></div>
  </div>
  
  <div class="MainPanel EmptyMainPanel">
    <p class="IntroText" id="IntroText">Upload your content (photos, videos and Flash files), edit their properties and organise your content into folders.</p>
    <div class="SlidingPanels">
      <div class="SlidingPanelsInner" id="SlidingPanelsInner">
        <div class="Panel">
          <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>This is where you upload the raw materials into the Oxigen system that can then be converted into Slides, which will make up your content Stream(s).</p><p>You have a <asp:literal ID="StorageCapacity" runat="server" /> MB storage capacity within the Oxigen system which includes your uploaded content and Slides.</p><p>You can upload the following file types:<br />Photos: .jpg, .gif, .bmp, .png<br />Videos: .mov, avi, .mp4, .wmv<br />Flash: .swf</p></div>
          <h2>Upload Content</h2>
          <div>
            <p style="width:330px; padding-left:5px;">Upload content from your PC or retrieve it from one of the services listed below. You can upload photos, videos and Flash animations. Your content will be displayed full-screen so you should ensure it is sufficient resolution to look good.</p>
            <div class="UploadContentHolder">
              <div class="UploadContentCover"></div>
              <a href="#" class="UploadContentPC" onclick="popUploaderDropdown('<%=ImageUploader1.ClientID%>');UploaderBlackOut('DivPopUpUploader'); return false;"></a>
              <div class="UploadContentFacebook2"></div>
              <div class="UploadContentFlickr2"></div>
<%--              <a href="#" class="UploadContentFacebook2" onclick="BlackOut('DivPopUpFacebookLogin'); return false;"></a>
              <a href="#" class="UploadContentFlickr2" onclick="BlackOut('DivPopUpFlickrLogin'); return false;"></a>
--%>            </div>
          </div>
        </div>
        <script type="text/javascript">
          /*<![CDATA[*/
          initArrowNav();
          miniNavShow(0);
          /*]]>*/
        </script>
        <div class="PanelCenter" id="Center0">
          <div class="PanelCenterContent">
            
            <div class="CentreMeta">
          <div class="CentreButtonHolder">
            <div class="AlreadyAdded">
            </div>
<%--            <div class="ButtonRed">
              <img id="ForJSButtonRedCentre" src="Images/Default/centre-button-red.png" alt="" />
              <a onclick="searchRemove2(this,'Panel2'); removeFromPanel2('Panel2'); return false;" href="#">Remove</a>
            </div>
--%>          </div>
        </div>
          
          </div>
        </div>
        <div class="Panel Panel2" id="Panel2">
          <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>This section shows the content (photos, videos, Flash files) you have uploaded into the Oxigen system in order to create Slides which go into your Stream(s).</p><p>You have a <asp:literal ID="StorageCapacity2" runat="server" /> storage capacity within the Oxigen system which includes your uploaded content and Slides.</p><p><b>Deleting Content</b>: you can delete your uploaded content from the Oxigen system by clicking on the cross on the thumbnail image or by deleting an entire Folder in the ”Manage My Content Folders” section.</p><p><b>Content Properties</b>: click on the “P” in the top left of the thumbnail image to edit properties of the content.</p></div>
          <h2>My Content</h2>     
          <div class="PanelInputFields" id="DropDownFolders">
            <asp:DropDownList CssClass="PanelDropDown PanelDropDown1" runat="server">
              <asp:ListItem>My Content Folder 1</asp:ListItem>
            </asp:DropDownList>
            <div class="PanelLinks">
              <a href="#" onclick="manageScreenTransfer(this);BlackOut('DivPopUpProperties');return false;">Manage My Content Folders</a>
            </div>
          </div>
          
          
          <div class="PagerNav">
            <span class="PagerStart"></span>
            <span class="PagerPrevious"></span>
            <div class="PagerPagesOuter">
            <div class="PagerPages">
            </div>
            </div>
            <span class="PagerNext"></span>
            <span class="PagerEnd"></span>
          </div>
        </div>
        <div class="PanelCenter" id="Center1">
          <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p><b>Templates</b>: these are optional styles that make your content more visually appealing when displayed on your PC screen. You do not have to select a template; simply “Do Nothing” and your content will display in its native format.</p></div>

          <div class="PanelCenterContent">
          <div class="CentreMeta">
          
            <div class="CentreButtonHolder">
              <div class="ButtonGreen" id="TemplateButton">
                <img id="ForJSButtonGreenCentre" src="Images/Default/centre-button-green.png" alt="" />
                <a onclick="convertContent();cloneStream('Panel2','Panel3');addCrosses();return false;" href="#">Convert<span>&raquo;</span></a>
              </div>
<%--              <div class="ButtonRed">
                <img id="ForJSButtonRedCentre" src="Images/Default/centre-button-red.png" alt="" />
                <a onclick="searchRemove2(this,'Panel3'); removeFromPanel2('Panel3'); return false;" href="#">Remove</a>
              </div>
--%>            </div>

              <div class="TemplateChooser">
              <div class="FormHolder">
                <span>Template:</span>
                <select id="TemplateChooser">
                  <option value="0">Do nothing</option>
                  <% foreach (Oxigen.Core.Template template in Templates)
                    {
                    %>
                        <option value="<%= template.Id %>"><%= template.Name %></option>
                    <%
                    } 
                  %>
                  <asp:Literal ID="TemplateList" runat="server" />
                </select>
              </div>
              <div class="HiddenSelection">
<%--                <div class="FormHolder">
                  <span>URL</span>
                  <input id="Template1Input1" value="<URL>" type="text" />
                </div>
                <div class="FormHolder">
                  <span>Display Duration</span>
                  <input id="Template1Input2" type="text"/>
                </div>
                  <asp:Label ID="Label3" AssociatedControlID="txtDate" runat="server">Date (dd/mm/yyyy):&nbsp;<span class="Asterisk">*</span></asp:Label>
                  <div class="FormField CalendarField" id="Template1Date1">
                  <asp:TextBox ID="txtDate" CssClass="EditBoxW3 WithFocusHighlight" runat="server"></asp:TextBox>
                  <asp:ImageButton runat="Server" CssClass="CalendarButton" ID="ibtnDate" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
                  <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender1" format="dd/MM/yyyy" runat="server" TargetControlID="txtDate" PopupButtonID="ibtnDate" PopupPosition="BottomLeft" />
                  <asp:Panel ID="pnlDate" CssClass="ValidationInfo" ValidationGroup="vgProceed" EnableViewState="false" Visible="false" runat="server"><asp:Literal ID="litDate" runat="server">Please enter a valid date</asp:Literal></asp:Panel>
                
              </div>--%>
              </div>
              <div class="HiddenSelection">
                <div class="FormHolder">
                  <span>Caption:</span>
                  <input id="ConvertTemplateCaption" type="text" />
                </div>
                <div class="FormHolder">
                  <span>Credit:</span>
                  <input id="ConvertTemplateCredit" type="text" />
                </div>
                <%--
                <div class="FormHolder">
                  <span>Date:</span>
                  <asp:textbox id="rawContentDate" runat="server" />
                    <asp:ImageButton runat="Server" CssClass="CalendarButton" ID="ImageButton2" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
                    <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender3" format="dd/MM/yyyy" runat="server" TargetControlID="rawContentDate" PopupButtonID="ImageButton2" PopupPosition="BottomLeft" />
                </div>
                <div class="FormHolder">
                  <span>Click thru URL:</span>
                  <input type="text" />
                </div>
                <div class="FormHolder">
                  <span>Display Duration</span>
                  <select class="DropDown DropDown2">
                    <option>10 seconds</option>
                    <option>20 seconds</option>
                    <option>30 seconds</option>
                    <option>40 seconds</option>
                    <option>50 seconds</option>
                    <option>1 minute</option>
                    <option>5 minutes</option>
                  </select>
                </div>
                --%>
              </div>
              <div class="HiddenSelection">
                <span id="StupeflixValidation">You must choose 5-15 slides.<br />You have chosen <span>1</span>.</span>
                <div class="FormHolder">
                  <span>Title:</span>
                  <input type="text" />
                </div>
                <div class="FormHolder">
                  <span>Caption:</span>
                  <input type="text" />
                </div>
                <div class="FormHolder">
                  <span>Click thru URL:</span>
                  <input type="text" />
                </div>
                <div class="FormHolder">
                  <span>Display Duration</span>
                  <select class="DropDown DropDown2">
                    <option>10 seconds</option>
                    <option>20 seconds</option>
                    <option>30 seconds</option>
                    <option>40 seconds</option>
                    <option>50 seconds</option>
                    <option>1 minute</option>
                    <option>5 minutes</option>
                  </select>
                </div>
              </div>
              <div class="HiddenSelection">
                <div class="FormHolder">
                  <span>Caption:</span>
                  <input id="TemplateCaption" type="text" />
                </div>
                <div class="FormHolder">
                  <span>Credit:</span>
                  <input id="TemplateCredit" type="text" />
                </div>
              </div>

            </div>
            <script type="text/javascript">
              /*<![CDATA[*/
              templateChooser();
              /*]]>*/
              </script>
          
        </div>
          
          </div>
        </div>
        <div class="Panel Panel3" id="Panel3">
          <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>This section shows your transformed content - photos, videos and/or Flash files that are now packaged up in different style templates - that is ready to be Scheduled to your Stream or Streams.</p><p><b>Deleting Slides</b>: you can only delete a Slide if it is not Scheduled to display in any of your Streams. To delete a Slide, click on the cross on the thumbnail image or you can delete an entire Slide Folder in the “Manage My Slide Folders” section.  Deleting a Slide does not delete your original uploaded Content.</p></div>
          <h2>My Slides</h2>
          <div class="PanelInputFields" id="ForJQSlideDropDown">
            <asp:DropDownList CssClass="PanelDropDown PanelDropDown2" runat="server">
              <asp:ListItem>My Slide Folder 1</asp:ListItem>
            </asp:DropDownList>
            <div class="PanelLinks">
            <a href="#" onclick="manageScreenTransfer(this);BlackOut('DivPopUpProperties')" >Manage My Slide Folders</a>
           </div>
           <div class="OxiValidation"></div>
          </div>
          
            <div class="StreamView">
          </div>

          <div class="PagerNav">
            <span class="PagerStart"></span>
            <span class="PagerPrevious"></span>
            <div class="PagerPagesOuter">
            <div class="PagerPages">
            </div>
            </div>
            <span class="PagerNext"></span>
            <span class="PagerEnd"></span>
          </div>
        </div>
        <div class="PanelCenter" id="Center2">
          <div class="PanelCenterContent">
            
            <div class="CentreMeta">
            
            <div class="CentreButtonHolder">
            <div class="ButtonGreen">
              <img id="ForJSButtonGreenCentre" src="Images/Default/centre-button-green.png" alt="" />
              <a id="PostButton" onclick="multiSchedule(); cloneStream('Panel3','Panel4'); return false;" href="#">Post<span>&raquo;</span></a>
            </div>
<%--            <div class="ButtonRed">
              <img id="ForJSButtonRedCentre" src="Images/Default/centre-button-red.png" alt="" />
              <a onclick="searchRemove(this); removeFromPanel('Panel4'); return false;" href="#">Remove</a>
            </div>
--%>          </div>
          
          <div class="SchedulingDiv">
            
            <a href="#" onclick="schedAdvOptions();return false;" class="SchedAdvOptionLink">Advanced options</a>
            <div id="SchedAdvOptions">
              <div class="FormHolder CentreMetaDiv">
              <span>URL:</span>
              <input onkeyup="findGoButton2('PostButton')" id="MultiSchedURL" class="TextBox" type="text" />
            </div>
            <div class="FormHolder CentreMetaDiv">
              <span>Display Duration:</span>
              <input onkeyup="findGoButton2('PostButton')" id="MultiSchedDur" onkeyup="checkDuration(this)" class="TextBox" type="text" />
            </div>
              <div class="FormHolder CentreMetaDiv" id="MultiSchedSD2">
                <span>Start Date:</span>
                <asp:textbox onkeyup="findGoButton2('PostButton')" id="MultiSchedSD" class="TextBox CentreMetaDate" runat="server" />
                  <asp:ImageButton runat="Server" CssClass="CalendarButton" ID="ImageButton3" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
                  <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender4" format="dd/MM/yyyy" runat="server" TargetControlID="MultiSchedSD" PopupButtonID="ImageButton3" PopupPosition="BottomLeft" />
              </div>
               <div class="FormHolder CentreMetaDiv" id="MultiSchedED2">
                <span>End Date:</span>
                <asp:textbox onkeyup="findGoButton2('PostButton')" id="MultiSchedED" class="TextBox CentreMetaDate" runat="server" />
                  <asp:ImageButton runat="Server" CssClass="CalendarButton" ID="ImageButton4" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
                  <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender5" format="dd/MM/yyyy" runat="server" TargetControlID="MultiSchedED" PopupButtonID="ImageButton4" PopupPosition="BottomLeft" />
              </div>
               <div class="FormHolder CentreMetaDiv TimeField">
                <span>Start Time (HH:MM):</span>
                <input onkeyup="findGoButton2('PostButton')" id="MultiSchedST" class="TextBox" type="text" />
                
              </div>
               <div class="FormHolder CentreMetaDiv TimeField">
                <span>End Time (HH:MM):</span>
                <input onkeyup="findGoButton2('PostButton')" id="MultiSchedET" class="TextBox" type="text" />
              </div>
              <script type="text/javascript">
                //$('#MultiSchedST,#MultiSchedET').timeEntry({ show24Hours: true, spinnerImage: '/images/default/spinnerDefault.png' })
                </script>
              <div class="FormHolder CentreMetaDiv" id="MultiSchedDays">
                <span>Days:</span>
                <div class="DateCheckBox">
                  <input class="CheckBox" type="checkbox"/>
                  <label>M</label>
                </div>
                <div class="DateCheckBox">
                  <input class="CheckBox" type="checkbox"/>
                  <label>T</label>
                </div>
                <div class="DateCheckBox">
                  <input class="CheckBox" type="checkbox"/>
                  <label>W</label>
                </div>
                <div class="DateCheckBox">
                  <input class="CheckBox" type="checkbox"/>
                  <label>T</label>
                </div>
                <div class="DateCheckBox">
                  <input class="CheckBox" type="checkbox"/>
                  <label>F</label>
                </div>
                <div class="DateCheckBox">
                  <input class="CheckBox" type="checkbox"/>
                  <label>S</label>
                </div>
                <div class="DateCheckBox">
                  <input class="CheckBox" type="checkbox"/>
                  <label>S</label>
                </div> 
                <div class="BottomFix"></div>
              </div>
            </div>
          </div>
          
          
            </div>
          
          </div>
        </div>
        <div class="Panel Panel4" id="Panel4">
          <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>A Stream contains some of your packaged up content (Slides), scheduled when you want them to display on a PC screen. You need to give your Stream a name and add properties so that it can be found by your followers.  You can also control the Stream Privacy to ensure it is only seen by the people you want to see it.</p><p><b>Editing Streams</b>: You can delete Slides from your Streams by clicking on the cross on the thumbnail image. Alternatively, you can keep adding Slides to your Stream by scheduling the Slides and confirming the update by clicking “Make It Live”.</p><p><b>Deleting Streams</b>: You must first remove all of the Slides from the Stream so there is no content to display.</p></div>
          <h2>My Streams</h2>  
          <div class="PanelInputFields" id="ForJQStreamDropDown">
            <asp:DropDownList CssClass="PanelDropDown PanelDropDown3" runat="server">
              <asp:ListItem>My Stream 1</asp:ListItem>
            </asp:DropDownList>
          <div class="PanelLinks">
            <a href="#" onclick="popStreamProperties();BlackOut2('DivPopUpStreamProperties'); return false">Manage Stream Properties</a>
            <a href="#" onclick="popnewStreamProperties();BlackOut2('DivPopUpStreamProperties'); return false">Add New Stream</a>
          </div>
          </div>
          

          <%--<div class="StreamView">
          </div>--%>
          <div class="PagerNav">
            <span class="PagerStart"></span>
            <span class="PagerPrevious"></span>
            <div class="PagerPagesOuter">
            <div class="PagerPages">
            </div>
            </div>
            <span class="PagerNext"></span>
            <span class="PagerEnd"></span>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>


    <div class="DivPopUp DivPopUpNoDrag" id="DivPopUpProperties">
      <div class="DivPopUpClose" onclick="windowsClose(this);return false;" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <h2 id="PropertiesTitle">Manage your Content Folders.</h2>
        <div class="RFormBox NoFocusEffect">
          <div class="WindowsView">
            <div class="WindowsTree">
              <h2 id="PropertiesFolders">My Content Folders</h2>
              <div class="WindowsTreeContent"></div>
              <div class="ButtonGreenXSmallLong">
                <img src="Images/Default/button-green-xsmall.png" alt="" />
                <a href="#" onclick="addFolderAjax();addWindowsFolder();return false;">Add Folder</a>
              </div>
              <div class="ButtonGreenXSmallLong SecondWindowsButton">
                <img src="Images/Default/button-red-xsmall.png" alt="" />
                <a href="#" onclick="divAlertPopUp2();return false;">Delete Folder</a>
              </div>
            </div>
            <div class="WindowsFileDepo">
              <h2 id="PropertiesContent">My Content <span id="ContentFolderName"></span></h2>
              <div class="OxiValidation"></div>
              <div class="WindowsLoader"></div>
            </div>
          </div>
        </div>
        <div class="InBetweenRForms"></div>
<%--        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-red-large.png" alt="" />
          <a href="#" onclick="unBlackOut(this);return false">Discard Changes <span>&raquo;</span></a>
        </div>--%>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>


    <div class="DivPopUp DivPopUpSmall" id="DivPopUpNewStreamProperties">
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <p>Configure your stream settings here.</p>
        <div class="RFormBox">
          <div id="BrowseNav2" class="BrowseNav">
            <div class="NavHolder"><a class="BrowseMain" onclick="killNav(this)" id="-1" href="#"><img src="Images/Default/browse-nav-open.png" alt="" /><span>All&nbsp;Categories</span></a><div class="BrowseDrop"><div class="DropBottom"></div></div><div class="BrowseParent"></div></div>
          </div>
          <div class="BrowseTree" id="BrowseTree2">
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <input type="text" class="TextBox" tabindex="1"/>
            <span>Name</span>
            <div class="BottomFix"></div>
          </div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <textarea rows="3" class="TextArea" tabindex="1"></textarea>
            <span>Description</span>
            <div class="BottomFix"></div>
          </div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <textarea rows="3" class="TextArea" tabindex="1"></textarea>
            <span>Long Description</span>
            <div class="BottomFix"></div>
          </div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <input type="text" class="TextBox" tabindex="1"/>
            <input type="text" class="TextBox" tabindex="1"/>
            <input type="text" class="TextBox" tabindex="1"/>
            <input type="text" class="TextBox" tabindex="1"/>
            <input type="text" class="TextBox" tabindex="1"/>
            <input type="text" class="TextBox" tabindex="1"/>
            <span>Keywords</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox WeekDaysList">
          <span>Classification</span>
          <asp:RadioButtonList class="RbHorizontalList PublicPrivateShow" RepeatDirection="Horizontal" runat="server">
            <asp:ListItem Selected="True">Public</asp:ListItem>
            <asp:ListItem>Private</asp:ListItem>
          </asp:RadioButtonList>
          <div class="BottomFix"></div>
        </div>
        <div class="PrivateHidden">
        <div class="InBetweenRForms"></div>       
        <div class="RFormBox">
          <div class="PropertyBox">
            <input type="password" class="TextBox" tabindex="1"/>
            <span>Password</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <input type="password" class="TextBox" tabindex="1"/>
            <span>Confirm Password</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
          <div class="RFormBox WeekDaysList">
            <span>Accept password requests</span>
            <asp:RadioButtonList class="RbHorizontalList PublicPrivateShow" RepeatDirection="Horizontal" runat="server">
              <asp:ListItem Selected="True">Yes</asp:ListItem>
              <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList>
          <div class="BottomFix"></div>
        </div>
        </div>
                <asp:HiddenField runat="server" />

        <div class="InBetweenRForms"></div>       

        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-green-large.png" alt="" />
          <a href="#" onclick="newStreamProperties(); return false">Save Changes <span>&raquo;</span></a>
        </div>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>

      
    <div class="DivPopUp DivPopUpSmall" id="DivPopUpFacebookLogin">
      <span class="IsChanged"></span>
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <div class="IFrameTop"></div>
        <iframe class="IFrameLogIn" src="HTMLPage1.htm">
        
        </iframe>
        <div class="IFrameBottom"></div>        
        <div style="z-index:5000; position:relative; top:-180px; text-align:Center"><a href="#" style="color:#c00; font-size:2em" onclick="unBlackOut2(this)">TEMP CLICK TO MOVE TO NEXT STEP</a></div>

        <%--<p>Please log in to your facebook account to access your content.</p>
        <div class="RFormBox">
          <div class="PropertyBox">
            <input type="text" class="TextBox" tabindex="1" value="someonerandom@googlemail.com"/>
            <span>Username</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <input type="password" class="TextBox" tabindex="1" value="Password"/>
            <span>Password</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="ButtonGreen ButtonRight">
          <img src="Images/Default/button-green.png" alt="" />
          <a href="#" onclick="unBlackOut(this); return false">Log in <span>&raquo;</span></a>
        </div>--%>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>
    
    <div class="DivPopUp DivPopUpSmall" id="DivPopUpFlickrLogin">
      <span class="IsChanged"></span>
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <p>Please log in to your flickr account to access your content.</p>
        <div class="RFormBox">
          <div class="PropertyBox">
            <input type="text" class="TextBox" tabindex="1" value="someonerandom@googlemail.com"/>
            <span>Username</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <input type="password" class="TextBox" tabindex="1" value="Password"/>
            <span>Password</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="ButtonGreen ButtonRight">
          <img src="Images/Default/button-green.png" alt="" />
          <a href="#" onclick="unBlackOut(this); return false">Log in <span>&raquo;</span></a>
        </div>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>
    <div class="DivPopUp DivPopUpSmall" id="DivPopUpCTT">
      <span class="IsChanged"></span>
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <h2>Choose Category</h2>
        
        <div class="BigButtonGreen ButtonRight" style="display:none">
          <img src="Images/Default/button-green-large.png" alt="" />
          <a href="#" onclick="saveCategory();unBlackOut(this);return false">Save Changes <span>&raquo;</span></a>
        </div>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>






<script type="text/javascript">
  /*<![CDATA[*/
  listAjax(-1)
  initBrowseNav();
  createDropDownAjax();
  checkCentreColumn2();
  streamBrowseView2();
  addCrosses2();
  //addProperties('Panel2');
  initPager('Panel2');
  initPager('Panel3');
  initPager('Panel4');
  updatePager();
  //createWindows();
  //initDocumentView();
  $('document').ready(function() {

  });

  /*]]>*/
</script>

</asp:Content>

 <asp:Content ID="Content3" ContentPlaceHolderID="uploader" runat="server">
  <script type="text/javascript">
    /*<![CDATA[*/
    
    var imageUploader1 = null;

    function formatFileSize(value) {
      if (value < 1024) {
        return (value + " b");
      }
      else if (value < 1048576) {
        return (Math.round(value / 1024) + " kb");
      }
      else {
        return (Math.round(value / 10486) / 100 + " mb");
      }
    }

    function unformatFileSize(curr_value) {
      var new_value;
      if (curr_value.indexOf('gb') != -1) {
        new_value = parseFloat(curr_value) * 1073741824
      }
      else if (curr_value.indexOf('mb') != -1) {
        new_value = parseFloat(curr_value) * 1048576
      }
      else if (curr_value.indexOf('kb') != -1) {
        new_value = parseFloat(curr_value) * 1024
      }
      else {
        new_value = parseInt(curr_value);
      }
      return new_value
    }

    function ImageUploader1_FullPageLoad() {
      imageUploader1 = getImageUploader("<%=ImageUploader1.ClientID%>");
      //alert(imageUploader1);
      document.getElementById("spanMaxTotalFileSize").innerHTML = formatFileSize(parseInt(imageUploader1.getMaxTotalFileSize() / 1.1));
      imageUploader1.style.width = "100%";
      imageUploader1.parentNode.style.width = "100%";

      //Installation Progress for Java version of IU add 2 parent controls around ImageUploader object
      if (window['<%= ImageUploader1.JavaScriptWriterVariableName %>'].getControlType() == "Java")
        imageUploader1.parentNode.parentNode.style.width = "100%";
    }

    function ImageUploader1_InitComplete() {
      $('.UploadContentCover').css('display', 'none')
      uploader_ready = 1;
      if (button_clicked == 1) {
        UploaderBlackOut('DivPopUpUploader')
      }
      $('#DivPopUpUploaderProgress').css('display', 'none')
      imgWidth2 = (parseInt($('#DivPopUpUploader input:eq(1)').val()) / (parseInt($('#BytesTotal input').val()))) * 216;
      document.getElementById("spanTotalFileSize").innerHTML = formatFileSize($('#DivPopUpUploader input:eq(1)').val());
      document.getElementById("imgTotalFileSizeUsed").style.width = Math.round(imgWidth2) + "px";
      $('#spanTotalAvailableBytes').html(formatFileSize($('#DivPopUpUploader input:eq(2)').val()))
      $('.Ab').css({ visibility: 'visible' })
    }

    function ImageUploader1_UploadFileCountChange() {
      if (imageUploader1) {
        var unformatted_curr_size = unformatFileSize(document.getElementById("spanTotalFileSize").innerHTML)
        document.getElementById("spanTotalSizeSelected").innerHTML = formatFileSize(parseInt(imageUploader1.getTotalFileSize()));
        imgWidth = ((parseInt(imageUploader1.getTotalFileSize()) + parseInt($('#DivPopUpUploader input:eq(0)').val())) / parseInt($('#BytesTotal input').val())) * 216;
        imgWidth2 = (parseInt($('#DivPopUpUploader input:eq(0)').val()) / (parseInt($('#BytesTotal input').val()))) * 216;
        document.getElementById("imgTotalFileSize").style.width = Math.round(imgWidth) + "px";
        document.getElementById("imgTotalFileSizeUsed").style.width = Math.round(imgWidth2) + "px";
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
      var assetContentFolderID = document.getElementById('UploadUserFolder').value; 
      var titleOvr = document.getElementById('ForJSUploadTitle').getElementsByTagName('input')[0].value;
      var creatorOvr = document.getElementById('ForJSUploadCreator').getElementsByTagName('input')[0].value;
      var descriptionOvr = document.getElementById('ForJSUploadDescription').getElementsByTagName('input')[0].value;
      var dateOvr = document.getElementById('ForJSUploadDate').getElementsByTagName('input')[0].value;
      var urlOvr = document.getElementById('ForJSUploadURL').getElementsByTagName('input')[0].value;
      var displayDurationOvr = document.getElementById('ForJSUploadDisplayDuration').getElementsByTagName('input')[0].value;

      new_total_max = parseInt(imageUploader1.getTotalFileSize()) + parseInt($('#DivPopUpUploader input:eq(0)').val())
      
      getImageUploader("<%=ImageUploader1.ClientID%>").AddField("AssetContentFolderID", assetContentFolderID);
      getImageUploader("<%=ImageUploader1.ClientID%>").AddField("TitleOvr", titleOvr);
      getImageUploader("<%=ImageUploader1.ClientID%>").AddField("CreatorOvr", creatorOvr);
      getImageUploader("<%=ImageUploader1.ClientID%>").AddField("DescriptionOvr", descriptionOvr);
      getImageUploader("<%=ImageUploader1.ClientID%>").AddField("DateOvr", dateOvr);
      getImageUploader("<%=ImageUploader1.ClientID%>").AddField("URLOvr", urlOvr);
      getImageUploader("<%=ImageUploader1.ClientID%>").AddField("DisplayDurationOvr", displayDurationOvr);
      
      needToHideButtonStop = false;      
    }

    var new_total_max = 0;
    
    function ImageUploader1_Progress(Status, Progress, ValueMax, Value, StatusText) {
      //Stop button should be displayed only during the upload process. If the upload 
      //completed (either successfully or not), the button should be hidden.
      needToHideButtonStop = (Status == "COMPLETE" || Status == "ERROR" || Status == "CANCEL");

      if (Status == "COMPLETE") {
        content_pager_pos_last = 3
        var timestamp = new Date().getTime()
        $.get(ajax_path_get + '?command=getUploadStatus' + '&' + timestamp, function(data) {
          if (data == 1) {
            $('#DivPopUpUploader').css({ width: '1px', height: '1px' })
            $('#UploadPopUp').css('display','block')
          }
          else {
            createDropDownAjax()
          }
        });

        

      }
    }

    function ImageUploader1_SelectAll() {
      getImageUploader("<%=ImageUploader1.ClientID%>").SelectAll();
    }

    function ImageUploader1_DeselectAll() {
      getImageUploader("<%=ImageUploader1.ClientID%>").DeselectAll();
    }

    function imageUploaderValidation() {
      if (parseInt(imageUploader1.getTotalFileSize()) == 0) {
        $('.Ab-b .Info .OxiValidation').css('visibility','visible')
      }
    }
    
    /*]]>*/
  </script>
      <div class="DivPopUp DivPopUpLarge" id="DivPopUpUploader">
      <div id="DivPopUpUploaderProgress"></div>
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
      <div id="UsedBytes">
        <asp:hiddenfield Value="0" id="UsedBytes" runat="server" />
      </div>
      <div id="BytesBegin">
        <asp:hiddenfield Value="0" id="BytesBegin" runat="server" />
      </div>
      <div id="BytesTotal">
        <asp:hiddenfield Value="1" id="BytesTotal" runat="server" />
      </div>
		<div class="Ab">
			<div class="Ab-b">
				<div class="Info">
				  Select the folder to which you want to upload your content:
				  <select onchange="uploaderFix(this);" id="UploadUserFolder" name="UploadUserFolder" class="DropDown">
				  </select>
					<br />
					<br />
					The remaining amount of space you have available is <strong><span id="spanMaxTotalFileSize">x</span></strong>.
					<br />
					<br />
					You have used 
					<strong><span id="spanTotalFileSize">0</span></strong> 
					out of your allocation of 
					<strong><span id="spanTotalAvailableBytes"><asp:literal id="TotalAvailableBytes" runat="server" /> <asp:literal id="Unit" runat="server" /></span></strong>
					You have selected to upload 
					<strong><span id="spanTotalSizeSelected">0 b</span></strong>
					.
					<br />
					
					<br />
					<table cellspacing="0" cellpadding="0" border="0" class="Progress">
						<tbody>
							<tr>
								<td class="Left">
								</td>
								<td class="Panel">
								  <div class="ProgressBarGrey"><div id="imgTotalFileSize" class="ProgressBarGreen Value"><div id="imgTotalFileSizeUsed" class="ProgressBarUsed Value"></div></div></div>
								</td>
								<td class="Right">
								</td>
							</tr>
						</tbody>
					</table>
					<div class="DefaultSettings">
					  <div id="DefaultSettingsExpandLink"><a class="Dropped" onclick="expandDefaultSettings();return false;" href="#">Advanced Settings</a></div>
					  <div class="DefaultSettingsInner" id="DefaultSettingsInner">
					    <div class="DefaultSettingsDiv">Title</div>
					    <div id="ForJSUploadTitle" style="display:inline"><asp:textbox class="DefaultSettingsInput" id="UploadTitle" runat="server" name="DefaultTitle" /></div>
					    <div class="BottomFix"></div>
					    <div class="DefaultSettingsDiv">Author</div>
					    <div id="ForJSUploadCreator" style="display:inline"><asp:textbox class="DefaultSettingsInput" id="UploadCreator" runat="server" name="DefaultCreator" /></div>
					    <div class="BottomFix"></div>
					    <div class="DefaultSettingsDiv">Caption</div>
					    <div id="ForJSUploadDescription" style="display:inline"><asp:textbox class="DefaultSettingsInput" id="UploadDescription" runat="server" name="DefaultDescription" /></div>
					    <div class="BottomFix"></div>
					    <div class="DefaultSettingsDiv">Date</div>
					    <div id="ForJSUploadDate" style="display:inline"><asp:textbox class="UploaderDate DefaultSettingsInput" id="UploadDate" runat="server" name="DefaultDate" /></div> 
					      <asp:ImageButton runat="Server" CssClass="CalendarButton UploaderDateButton" ID="ibtnUploadDate" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
                <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender1" format="dd/MM/yyyy" runat="server" TargetControlID="UploadDate" PopupButtonID="ibtnUploadDate" PopupPosition="BottomLeft" />
					    <div class="BottomFix"></div>
              <div class="DefaultSettingsDiv">URL</div>
					    <div id="ForJSUploadURL" style="display:inline"><asp:textbox class="DefaultSettingsInput" id="UploadURL" runat="server" name="DefaultURL" /></div>
					    <div class="BottomFix"></div>
					    <div class="DefaultSettingsDiv">Display Duration</div>
					    <div id="ForJSUploadDisplayDuration" style="display:inline"><asp:textbox class="UploaderDuration DefaultSettingsInput" id="UploadDisplayDuration" runat="server" name="DefaultDisplayDuration" /><span style="float:left;">seconds.</span></div>
					  </div>
					</div>
					
					<div class="OxiValidation">Please select some content</div>
					
					<div class="ButtonGreen">
            <img id="ForJSButtonGreen2" src="Images/Default/button-green.png" alt="" />
            <a class="send" href="#" onclick="imageUploader1.Send();imageUploaderValidation();return false;">Upload</a>
          </div>
					
				</div>
			</div>
			<select class="Toolbar DropDown" id="selectView" onchange="selectView_change();">
				<option value="0" selected="selected">Thumbnails</option>
				<option value="1">Icons</option>
				<option value="2">List</option>
				<option value="3">Details</option>
			</select>
			<div class="Ab-A">									
				<cc1:ImageUploader ID="ImageUploader1" runat="server" 
							Width="680"
							Height="601"  
							OnClientBeforeUpload="ImageUploader1_BeforeUpload"
        			OnClientProgress="ImageUploader1_Progress"
        		  OnClientInitComplete="ImageUploader1_InitComplete"
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
      
      </div>
      <div class="DivPopBottom"></div>
    </div>

      <div class="DivPopUp DivPopUpSmall" id="DivPopUpContentProperties">
      <span class="IsChanged"></span>
      <span style="display:none;" id="HiddenContentID"></span>
      <div class="DivPopUpClose DivPopUpClose2" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <p id="ContentPropertiesTitle">Edit <span>Content</span> Properties.</p>
        <div id="ContentPropertyValidation" class="OxiValidation"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <input id="RCTitle" onkeyup="findGoButton('updateProperties','this')" type="text" class="TextBox" tabindex="1" value="Animated Penguin March"/>
            <span>Title</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div><div class="RFormBox">
          <div class="PropertyBox">
            <input id="RCCreator" onkeyup="findGoButton('updateProperties','this')" type="text" class="TextBox" tabindex="1" value="Ryan Salton"/>
            <span>Author</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <input id="RCCaption" onkeyup="findGoButton('updateProperties','this')" type="text" class="TextBox" tabindex="1" value="Here are my penguins..."/>
            <span>Caption</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div> 
        <div class="RFormBox">
          <div class="PropertyBox" id="RCDate2" >            
            <div class="RFormCalendar">
              <asp:ImageButton runat="Server" CssClass="CalendarButton" ID="ImageButton1" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
              <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender2" format="dd/MM/yyyy" runat="server" TargetControlID="RCDate" PopupButtonID="ImageButton1" PopupPosition="BottomLeft" />
            </div>
            <asp:textbox onkeyup="findGoButton('updateProperties','this')" id="RCDate" runat="server" class="RFormCalendarText TextBox" tabindex="1" value="12/12/2009" /> 
            <span>Date</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div> 
        <div class="RFormBox">
          <div class="PropertyBox">
            <input onkeyup="findGoButton('updateProperties','this')" id="RCURL" type="text" class="TextBox" tabindex="1" value="http://www.alunjohns.com" />
<%--            <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>Website address to which the content links.</p></div>
--%>            <span>URL</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>       
        <div class="RFormBox">
          <div class="PropertyBox">
            <input onkeyup="findGoButton('updateProperties','this')" id="RCDuration" type="text" class="TextBox" tabindex="1" value="" />
            <span>Display Duration</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div> 
        
        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-green-large.png" alt="" />
          <a href="#" id="UpdateProperties" onclick="updateProperties(this); return false">Save Changes <span>&raquo;</span></a>
        </div>
        <div id="HiddenContentPanel">
          <asp:HiddenField runat="server" />
        </div>

        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>

      <div class="DivPopUp DivPopUpSmall DivPopUpNoDrag" id="DivPopUpTimings">
      <span class="IsChanged"></span>
      <div class="DivPopUpClose DivPopUpClose2" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <p>Manage scheduling settings for this item.</p>
        <p>Please enter a date and time in the format of DD/MM/YYYY HH:MM</p>
        <div class="OxiValidation"></div>
        <div class="RFormBox">
          <div class="PropertyBox">
            <div class="LeftDateBox">
              <span>URL:</span>
              <input id="TimingsURL" type="text" class="TextBox" tabindex="1"/>
            </div>
            <div class="RightDateBox">
              <span>Display Duration</span>
              <input id="TimingsDur" type="text" class="TextBox" tabindex="1"/>
            </div>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="BottomFix"></div>
        <div class="InBetweenRForms"></div>
        <div id="clonedField1" class="RepeatedField">
        <asp:repeater id="ScheduleRepeater" runat="server">
        <itemtemplate>
          <div class="RFormBox">
            <div class="PropertyBox">
              <div class="LeftDateBox">
                <span>Start Date</span>                
                <div class="LeftDateCalendar">
                  <asp:ImageButton runat="Server" CssClass="CalendarButton" ID="ibtnPropStartDate" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
                  <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender1" format="dd/MM/yyyy" runat="server" TargetControlID="propStartDate" PopupButtonID="ibtnPropStartDate" PopupPosition="BottomLeft" />
                </div>
                <asp:textbox runat="server" id="propStartDate" class="TextBox TextBox2" tabindex="1" value="10/02/2010"/>
              </div>
              <div class="RightDateBox">
                <span>End Date</span>                
                <div class="RightDateCalendar">
                  <asp:ImageButton runat="Server" CssClass="CalendarButton" ID="ibtnPropEndDate" ImageUrl="~/Images/Default/calendarlogo.gif" AlternateText="Click to show calendar" />
                  <ajaxToolkit:CalendarExtender CssClass="CalendarPopUp" Enabled="true" ID="CalendarExtender6" format="dd/MM/yyyy" runat="server" TargetControlID="propEndDate" PopupButtonID="ibtnPropEndDate" PopupPosition="BottomLeft" />
                </div>
                <asp:textbox runat="server" id="propEndDate" class="TextBox TextBox2" tabindex="1" value="10/02/2011"/>
              </div>
              <div class="BottomFix"></div>
              <div class="LeftDateBox TimeField">
                <span>Start Time</span>
                <input type="text" class="TextBox" tabindex="1" value="10:00"/>
              </div>
              <div class="RightDateBox TimeField">
                <span>End Time</span>
                <input type="text" class="TextBox" tabindex="1" value="20:00"/>
              </div>
              <script type="text/javascript">
                $('#DivPopUpTimings .TimeField input').timeEntry()
                //$('#DivPopUpTimings .TimeField:eq(1) input:eq(0)').timeEntry()
              </script>
            </div>
            <div class="BottomFix"></div>
          </div> 
          <div class="InBetweenRForms"></div>         
          </itemtemplate>            
        </asp:repeater>        
        </div>
        <div style="height:40px;margin-top:-30px">
          <div class="ButtonGreenXSmall DateButton">
              <img src="Images/Default/button-green-xsmall.png" alt="" />
              <a onclick="showFields('RepeatedField')" tabindex="2" href="#">Add</a>
            </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox WeekDaysList">
          <span>Day of week</span>
          <div>
            <input class="CheckBox" type="checkbox"/>
            <label>M</label><br />
          </div>
          <div>
            <input class="CheckBox" type="checkbox"/>
            <label>T</label><br />
          </div>
          <div>
            <input class="CheckBox" type="checkbox"/>
            <label>W</label><br />
          </div>
          <div>
            <input class="CheckBox" type="checkbox"/>
            <label>T</label><br />
          </div>
          <div>
            <input class="CheckBox" type="checkbox"/>
            <label>F</label><br />
          </div>
          <div>
            <input class="CheckBox" type="checkbox"/>
            <label>S</label><br />
          </div>
          <div>
            <input class="CheckBox" type="checkbox"/>
            <label>S</label><br />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-green-large.png" alt="" />
          <a href="#" onclick="sendScheduleInfo(this); return false">Save Changes <span>&raquo;</span></a>
        </div>
        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-red-large.png" alt="" />
          <a href="#" onclick="unBlackOut(this);return false">Discard Changes <span>&raquo;</span></a>
        </div>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>
    
    <div class="DivPopUp DivPopUpSmall" id="UploadPopUp">
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <p>Some of your uploaded content's set duration was out of bounds.</p>
        <p>Allowed durations are as follows:<br />Images: <asp:Literal ID="displayDurationImageMin" runat="server" /> - <asp:Literal ID="displayDurationImageMax" runat="server" /> seconds<br />Video: <asp:Literal ID="displayDurationVideoMin" runat="server" /> - <asp:Literal ID="displayDurationVideoMax" runat="server" /> seconds.<br />Flash: <asp:Literal ID="displayDurationFlashMin" runat="server" /> - <asp:Literal ID="displayDurationFlashMax" runat="server" /> seconds.</p>
        <p>We have adjusted your uploaded content as appropriate.</p>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>
    
    <div class="DivPopUp DivPopUpSmall" id="DivPopUpStreamProperties" style="top:50px">
      <div class="DivPopUpClose DivPopUpClose2" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <span class="IsChanged"></span>
        <p>Configure your stream settings here.</p>
        <div class="OxiValidation"></div>
        
        <div class="RFormBox">
          <div class="PropertyBox">
            <input onkeyup="multiFindGo()" type="text" class="TextBox" tabindex="1"/>
            <span>Name</span>
            <div class="BottomFix"></div>
          </div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <textarea rows="3" class="TextArea" tabindex="1"></textarea>
            <span>Description</span>
            <div class="BottomFix"></div>
          </div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <textarea rows="3" class="TextArea" tabindex="1"></textarea>
            <span>Long Description</span>
            <div class="BottomFix"></div>
          </div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <div id="PropertiesKeywords">
              <input onkeyup="multiFindGo()" type="text" class="TextBox" tabindex="1" value=""/>
              <input onkeyup="multiFindGo()" type="text" class="TextBox" tabindex="1" value=""/>
              <input onkeyup="multiFindGo()" type="text" class="TextBox" tabindex="1" value=""/>
              <input onkeyup="multiFindGo()" type="text" class="TextBox" tabindex="1" value=""/>
              <input onkeyup="multiFindGo()" type="text" class="TextBox" tabindex="1" value=""/>
              <input onkeyup="multiFindGo()" type="text" class="TextBox" tabindex="1" value=""/>
            </div>
            <span>Keywords</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <h3>Select Category</h3>
          <div id="BrowseNav" class="BrowseNav">
            <div class="NavHolder"><a class="BrowseMain" onclick="killNav(this)" id="-1" href="#"><img src="Images/Default/browse-nav-open.png" alt="" /><span>All&nbsp;Categories</span></a><div class="BrowseDrop"><div class="DropBottom"></div></div><div class="BrowseParent"></div></div>
          </div>
          <div class="BrowseTree" id="BrowseTree">
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="BottomFix"></div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox WeekDaysList">
            <span>Classification</span>
            <asp:RadioButtonList class="RbHorizontalList PublicPrivateShow" RepeatDirection="Horizontal" runat="server">
              <asp:ListItem Selected="True">Public</asp:ListItem>
              <asp:ListItem>Private</asp:ListItem>
            </asp:RadioButtonList>
          <div class="BottomFix"></div>
        </div>
        <div class="PrivateHidden">
        <span id="PrivateHiddenVar"></span>
        <div class="InBetweenRForms"></div>       
        <div class="RFormBox">
          <div class="PropertyBox">
            <input onkeyup="multiFindGo();checkPrivateHidden2()" type="password" class="TextBox" tabindex="1" value="Password"/>
            <span>Password</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        <div class="InBetweenRForms"></div>
          <div class="PropertyBox">
            <input onkeyup="multiFindGo()" type="password" class="TextBox" tabindex="1" value="Password"/>
            <span>Confirm Password</span>
            <div class="BottomFix"></div>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="PrivateHiddenExtra">
          <div class="InBetweenRForms"></div>       
          <div class="RFormBox WeekDaysList">
              <span>Locking Action</span>
              <asp:RadioButtonList class="RbVerticalList" RepeatDirection="Vertical" runat="server">
                <asp:ListItem>Allow all current users access to stream</asp:ListItem>
                <asp:ListItem>Remove all access for current users</asp:ListItem>
                <asp:ListItem>Allow users to access stream if they are authorized or have provided password previously</asp:ListItem>
              </asp:RadioButtonList>
            <div class="BottomFix"></div>
          </div>
        </div>
        <div class="InBetweenRForms"></div>
          <div class="RFormBox WeekDaysList">
            <span>Accept password requests</span>
            <asp:RadioButtonList class="RbHorizontalList PublicPrivateShow" RepeatDirection="Horizontal" runat="server">
              <asp:ListItem Selected="True">Yes</asp:ListItem>
              <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList>
          <div class="BottomFix"></div>
        </div>
        </div>
        <div id="HiddenEnterString">
          <asp:HiddenField Value="sendStreamProperties" runat="server" />
        </div>
                <asp:HiddenField runat="server" />

        <div class="InBetweenRForms"></div>       
        
        
        <div class="BigButtonGreen ButtonRight" id="StreamPropertiesButton">
          <img src="Images/Default/button-green-large.png" alt="" />
          <a id="StreamPropertyGo" href="#" onclick="sendStreamProperties(); return false;">Save Changes <span>&raquo;</span></a>
          <a id="StreamPropertyGo2" href="#" onclick="newStreamProperties(); return false">Save Changes <span>&raquo;</span></a>
        </div>
        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-red-large.png" alt="" />
          <a href="#" onclick="popUpRemoveStream(); return false">Delete Stream<span>&raquo;</span></a>
        </div>
        
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>

    <div class="DivPopUpDeleteAlert">
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">

        <p>Are you sure you wish to remove this stream?</p>
        <div class="ButtonGreenSmall ButtonRight">
          <img src="Images/Default/button-green-small.png" alt="" />
          <a onclick="removeStreamAjax();return false" href="#">Yes</a>
        </div>
        <div class="ButtonGreenSmall ButtonRight">
          <img src="Images/Default/button-red-small.png" alt="" />
          <a onclick="unPopUpRemoveStream();return false" href="#">No</a>
        </div>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>


    <div class="StreamPreviewDiv" id="divStreamPreview">
      <div class="StreamPreviewClose"><a onclick="unStreamPreview()" href="#">Close Preview</a></div>
      <div id="divStreamPreviewContent">
        
      </div>
      <div id="StreamPreviewTitle"></div>
    </div>

</asp:Content> 
