<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="OxigenIIPresentation.Download" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="ForJSDownloadHidden">
  <asp:HiddenField id="streamInfo" Value="" runat="server" />
</div>
<div class="DownloadCreatePage">
  <div class="ButtonGreen">
    <img id="ForJSButtonGreen2" src="Images/Default/button-green.png" alt="" />
    <a onclick="downloadAjax();" href="DownloadSuccess.aspx">I'm done <span>&raquo;</span></a>
  </div>
  <div class="Navigation">
    <div><span>Download</span><img src="Images/Default/search-tabs.png" alt="" /></div>
  </div>
  <div class="MainPanel">
    <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p><b>Add Streams</b>: Within Find Streams, click on a content Stream you like the look of and more information will display in the middle section.  Simply click on "Add to PC" and this stream will be selected to appear on your PC. Once you are happy with your Stream selections, click "I'm Done" to confirm the changes and your PC display will automatically be updated.</p><p><b>Remove Streams</b>: All selected content Streams appear within the "My PCs" section on the right hand side.  To remove, simply click on the cross in the top right corner of the Stream. The Stream can be re-selected and added again from within the "Find Streams" Section. Click "I'm Done" to confirm the changes and your PC display will automatically be updated.</p><p><b>I'm Done</b>:  If you are not logged-in, after you have made your Stream selections, clicking on "I'm Done" will create an installer for the Oxigen Player that you need to run on your PC in order for the Streams to appear.  If you are logged-in, clicking on "I'm Done" will save your Stream settings and the changes made will automatically display on your PC shortly. </p></div>
    <p class="IntroText">Select the content streams in the left panel and add them to your PCs on the right. When you're finished, click "I'm Done"</p>
    <div class="LeftColumn JQColumn" id="LeftColumn">
      <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>Look for the content Streams you want to display on your PC. You can add multiple content Streams.</p><p><b>Browse</b>: Find the content Streams that interest you by looking through the different categories and sub-categories.</p><p><b>Search</b>: Simply enter the search term in the box for what you would like to see and we will check our existing Streams and return relevant Streams for you.</p><p>To find friends, use the Search box and enter your friend's name or their Stream name.</p><p><b>Add a Stream</b>: Click on the Stream and more information will appear in the middle section. You can then add the Stream to your PCs.</p><p><b>Locked Streams</b> (padlock): If a Stream is private it will be locked so you need to enter a  password to gain access to the Stream.</p></div>
      <h2>Find Streams</h2>
      <div class="MiniNavigation LongVersion">
        <div class="Active"><a onclick="miniNavShow(0);return false;" href="#">Browse</a></div>
        <div class="Inactive"><a onclick="miniNavShow(1);return false;" href="#">Search</a></div>
<%--        <div class="Inactive"><a onclick="miniNavShow(2);return false;" href="#">Make me one</a></div>
--%>      </div>
      
      <div class="MiniNavDivs">
        <div id="BrowseNav" class="BrowseNav">
          <div class="NavHolder"><a class="BrowseMain" onclick="killNav(this)" id="-1" href="#"><img src="Images/Default/browse-nav-open.png" alt="" /><span>All&nbsp;Categories</span></a><div class="BrowseDrop"><div class="DropBottom"></div></div><div class="BrowseParent"></div></div>
        </div>  
        <div class="SortBy" id="SortBy">
          <span>Sort by:</span><a onclick="ajaxSortBy(this);return false;" class="Active" href="#">Popularity</a><span>|</span><a onclick="ajaxSortBy(this);return false;" class="Inactive" href="#">Most Recent</a><span>|</span><a onclick="ajaxSortBy(this);return false;" class="Inactive" href="#">Alphabetical</a>
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
      <div class="MiniNavDivs">
        <div class="SearchPanel">
        <div class="ButtonBlue ButtonRight">
          <img src="Images/Default/button-blue.png" alt="" />
          <a onclick="searchAjax(this)" href="#">Search <span>&raquo;</span></a>
        </div>
        <asp:TextBox CssClass="TextBox" runat="server" />
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
      <div class="MiniNavDivs">
        <div class="SearchPanel">
        <div class="ButtonBlue ButtonRight">
          <img src="Images/Default/button-blue.png" alt="" />
          <a href="#">Search <span>&raquo;</span></a>
        </div>
        <asp:TextBox CssClass="TextBox" runat="server" />
        </div>
      </div>
      
    </div>
      <script type="text/javascript">
        /*<![CDATA[*/
        listAjax(-1)
        initBrowseNav();
        miniNavShow(0);
        //initPrivacy('LeftColumn')
        //initPager('LeftColumn')
        /*]]>*/
      </script>
    <div class="CentreColumn" id="CentreColumn">
      <div class="CentreContent">
        <div class="CentreMeta">
          <div class="CentreButtonHolder">
            <div class="AlreadyAdded">
              You have already added this stream to this PC
            </div>
            <div class="ButtonGreen">
              <img id="ForJSButtonGreenCentre" src="Images/Default/centre-button-green.png" alt="" />
              <a onclick="addContentAjax('DropDownPCs');cloneStream('LeftColumn','RightColumn');addCrosses();addWeight();return false;" href="#">Add to PC<span>&raquo;</span></a>
            </div>
          </div>

          <div class="MetaDiv">About:<br /><span id="MetaAbout"></span></div>
          <div class="MetaDiv">Added: <span id="MetaDateAdded"></span></div>
          <div class="MetaDiv">By: <span id="MetaAddedBy"></span></div>
          <div class="MetaDiv">Content Items: <span id="MetaContent"></span></div>
          <div class="MetaDiv">Followers: <span id="MetaFollowers"></span></div>
          <div class="MetaDiv MetaDivPadded"><a id="MetaLink">View more details</a></div>
          <div class="StreamPrivateMessage">This is a private stream and you will not be able to view it until you unlock it with the password.</div>
          <div class="WeightingSliderLabel">Weighting: <span id="MetaWeighting"></span></div>
          <div class="WeightingSlider"><div class="WeightingSliderInner"></div></div>
        </div>
        <script type="text/javascript">
          /*<![CDATA[*/
          initWeightSlider();
          /*]]>*/
        </script>
      </div>
    </div>
    <div class="RightColumn JQColumn" id="RightColumn">
      <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>Enables you to control the content Streams that will show on your different PCs.</p><p>The first time you use the system you will only have settings for one PC that is named as "My PC".  If you would like your Stream settings to appear on multiple PCs, click on "Manage My PCs" and you will be able to add or remove PCs.  For example, you can have different Streams (and Stream weightings) display on a work PC and different Streams on your home PC.</p></div>
      <h2>My PCs</h2>
      <div class="PanelInputFields" id="DropDownPCs">
        <asp:DropDownList onchange="streamlistAjax(this.value,'RightColumn', 1, 4)" CssClass="PanelDropDown" runat="server">
          <asp:ListItem>My PC</asp:ListItem>
          <asp:ListItem>My Other PC</asp:ListItem>
        </asp:DropDownList>
      <div class="PanelLinks">
<%--        <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>Enables you to add, rename or remove a PC and change the associated content Streams to be shown on that PC.</p></div>
        <a onclick="manageScreenTransfer(this);BlackOut('DivPopUpProperties')" href="#">Manage my PCs</a>
--%>        <a id="AddPCLink" onclick="addPCAjax();return false" href="#">Add PC</a>
      </div>
      <div class="PanelLinks2">
        <div onclick="helpActivate(this)" class="HelpMark"><img src="Images/Default/help-mark.png" alt="" /><p>This controls how often a specific content Stream will display on your PC.  The default is a weighting of 10 and all numbers are relative to each other.</p><p>To adjust Stream weighting, click on the "P" (Stream Properties) and then alter the slider within the pop-up box.</p></div>
        <span class="WeightingLink">Weighting: <a href="#" onclick="showWeighting(); return false">Show</a>&nbsp;|&nbsp;<a href="#" onclick="hideWeighting(); return false" class="WeightingSpanSelected">Hide</a></span>
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
    <script type="text/javascript">
      /*<![CDATA[*/
      dropDownAjax('DropDownPCs')
      //initPrivacy('RightColumn')
      /*]]>*/
    </script>
  </div>
</div>

    <div class="DivPopUp DivPopUpSmall DivPopUpNoDrag" id="DivPopUpPersonal">
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <span class="IsChanged"></span>
        <p>Configure your personal stream for "My PC".</p>
        <div class="RFormBox">
          <div class="DOBText">
            <span>Date of birth</span>
            <asp:DropDownList CssClass="DropDown DropDownYear" TabIndex="3" runat="server">
              <asp:ListItem>YYYY</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList CssClass="DropDown DropDownMonth" TabIndex="2" runat="server">
              <asp:ListItem>MM</asp:ListItem>
              <asp:ListItem>Jan</asp:ListItem>
              <asp:ListItem>Feb</asp:ListItem>
              <asp:ListItem>Mar</asp:ListItem>
              <asp:ListItem>Apr</asp:ListItem>
              <asp:ListItem>May</asp:ListItem>
              <asp:ListItem>Jun</asp:ListItem>
              <asp:ListItem>Jul</asp:ListItem>
              <asp:ListItem>Aug</asp:ListItem>
              <asp:ListItem>Sep</asp:ListItem>
              <asp:ListItem>Oct</asp:ListItem>
              <asp:ListItem>Nov</asp:ListItem>
              <asp:ListItem>Dec</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList CssClass="DropDown DropDownDay" TabIndex="1" runat="server">
              <asp:ListItem>DD</asp:ListItem>
              <asp:ListItem>1</asp:ListItem>
              <asp:ListItem>2</asp:ListItem>
              <asp:ListItem>3</asp:ListItem>
              <asp:ListItem>4</asp:ListItem>
              <asp:ListItem>5</asp:ListItem>
              <asp:ListItem>6</asp:ListItem>
              <asp:ListItem>7</asp:ListItem>
              <asp:ListItem>8</asp:ListItem>
              <asp:ListItem>9</asp:ListItem>
              <asp:ListItem>10</asp:ListItem>
              <asp:ListItem>11</asp:ListItem>
              <asp:ListItem>12</asp:ListItem>
              <asp:ListItem>13</asp:ListItem>
              <asp:ListItem>14</asp:ListItem>
              <asp:ListItem>15</asp:ListItem>
              <asp:ListItem>16</asp:ListItem>
              <asp:ListItem>17</asp:ListItem>
              <asp:ListItem>18</asp:ListItem>
              <asp:ListItem>19</asp:ListItem>
              <asp:ListItem>20</asp:ListItem>
              <asp:ListItem>21</asp:ListItem>
              <asp:ListItem>22</asp:ListItem>
              <asp:ListItem>23</asp:ListItem>
              <asp:ListItem>24</asp:ListItem>
              <asp:ListItem>25</asp:ListItem>
              <asp:ListItem>26</asp:ListItem>
              <asp:ListItem>27</asp:ListItem>
              <asp:ListItem>28</asp:ListItem>
              <asp:ListItem>29</asp:ListItem>
              <asp:ListItem>30</asp:ListItem>
              <asp:ListItem>31</asp:ListItem>
            </asp:DropDownList>
            <div class="BottomFix"></div>
          </div>
          <div class="NormCheckBox">
            <asp:CheckBox Text="Horoscope" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="CityText">
            <span>City</span>
            <asp:TextBox CssClass="TextBox" TabIndex="4" runat="server"></asp:TextBox>
          </div>
          <div class="NormCheckBox">
            <asp:CheckBox Text="Weather" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="CityText">
            <span>Your Twitter Username</span>
            <asp:TextBox CssClass="TextBox" TabIndex="4" runat="server"></asp:TextBox>
          </div>
          <div class="NormCheckBox">
            <asp:CheckBox Text="Twitter" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="StocksText">
            <span>Stocks</span>
            <asp:Listbox CssClass="ListBox" Rows="4" SelectionMode="Multiple" TabIndex="5" runat="server">
              <asp:ListItem>AAPL</asp:ListItem>
              <asp:ListItem>APS.V</asp:ListItem>
              <asp:ListItem>BAY.L</asp:ListItem>
              <asp:ListItem>CHW</asp:ListItem>
              <asp:ListItem>CSCO</asp:ListItem>
              <asp:ListItem>DELL</asp:ListItem>
              <asp:ListItem>ERM</asp:ListItem>
              <asp:ListItem>ERTS</asp:ListItem>
              <asp:ListItem>FUTR</asp:ListItem>
              <asp:ListItem>GOOG</asp:ListItem>
              <asp:ListItem>HNT</asp:ListItem>
              <asp:ListItem>MSQ</asp:ListItem>
              <asp:ListItem>PSON</asp:ListItem>
              <asp:ListItem>WPP</asp:ListItem>
            </asp:Listbox>
            <span class="AdditionalListInfo">(Hold CTRL to select multiple stocks)</span>
          </div>
          <div class="NormCheckBox">
            <asp:CheckBox Text="Stocks" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="NormCheckBox">
            <asp:CheckBox Text="Word of the day" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="NormCheckBox">
            <asp:CheckBox Text="Joke of the day" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="NormCheckBox">
            <asp:CheckBox Text="Today's birthdays" runat="server" />
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="CityText">
            <asp:TextBox CssClass="TextBox" runat="server"></asp:TextBox>
          </div>
          <div class="NormCheckBox">
            <label>Weighting</label>
          </div>
          <div class="BottomFix"></div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-green-large.png" alt="" />
          <a href="#" onclick="unBlackOut(this);return false">Save Changes <span>&raquo;</span></a>
        </div>
        <div class="BigButtonGreen ButtonRight">
          <img src="Images/Default/button-red-large.png" alt="" />
          <a href="#" onclick="unBlackOut(this);return false">Discard Changes <span>&raquo;</span></a>
        </div>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>
  
<%--      <div class="DivPopUp DivPopUpNoDrag" id="DivPopUpProperties">
      <div onclick="dropDownAjax('DropDownPCs')" class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle DivPopUpMiddlePCs">
        <h2>Manage your PCs.</h2>
        <div class="RFormBox NoFocusEffect">
          <div class="WindowsView">
            <div class="WindowsTree">
              <h2>My PCs</h2>
              <div class="WindowsTreeContent">
              </div>
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
              <h2>My Content <span id="ContentFolderName"></span></h2>
              <div class="WindowsLoader"></div>
            </div>
          </div>
        </div>
        <div class="InBetweenRForms"></div>
        <div class="BottomFix"></div>
      </div>
      <div class="DivPopBottom"></div>
    </div>
--%>    
    <div class="DivPopUp DivPopUpMedium" id="DivPopUpPrivate">
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <div id="UnlockHiddenID">
          <asp:HiddenField runat="server"/>
        </div>
        <p>This is a private stream, please enter the password.</p>
        <div class="OxiValidation"></div>
        <div id="PasswordCheck" class="RFormBox">
          <div class="LeftLargeLabel">
            <span>Unlock Stream</span>
          </div>  
          <div class="ExistingUserForm">
            <div class="ButtonGreen ButtonRight">
              <img src="Images/Default/button-green.png" alt="" />
              <a href="#" onclick="unlockStream(this); return false">Submit <span>&raquo;</span></a>
            </div>
            <div class="RightForm">
              <span class="FormLabel">Password:</span>
              <div><asp:TextBox onkeyup="findGoButton('unlockStream','this',event)" TabIndex="2" TextMode="Password" CssClass="TextBox" runat="server"></asp:TextBox></div>
            </div>
            <div class="BottomFix"></div>
          </div>        
        </div>
        <div id="ForJSPasswordRequest">
        <div class="InBetweenRForms"></div>
        <div class="RFormBox">
          <div class="LeftLargeLabel">
            <span>Request Password</span>
          </div> 
          <div class="ExistingUserForm">
            <div class="RightForm">
              <span class="FormLabel">Your name:</span>
              <div ID="JSRequestName"><asp:TextBox TabIndex="2" CssClass="TextBox" runat="server"></asp:TextBox></div>
            </div>
            <div class="BottomFix"></div>
            <div class="RightForm">
              <span class="FormLabel">Your email address:</span>
              <div ID="JSRequestEmail"><asp:TextBox TabIndex="2" CssClass="TextBox" runat="server"></asp:TextBox></div>
            </div>
            <div class="BottomFix"></div> 
            <div class="RightForm">
              <span class="FormLabel">Your message:</span>
              <div ID="JSRequestMessage"><asp:TextBox CssClass="TextArea" TextMode="MultiLine" Rows="7" runat="server"></asp:TextBox></div>
            </div>                 
            <div class="BottomFix"></div>
            <div class="ButtonGreen ButtonRight" style="top:112px;">
              <img src="Images/Default/button-green-large.png" alt="" />
              <a href="#" onclick="sendPasswordRequest(); return false">Send <span>&raquo;</span></a>
            </div>
          <div class="CaptchaBox">
            <div class="CaptchaInner">
              <img src="ImageGen.aspx"/> 
              <span>Please enter value shown</span>             
              <input type="text" />
              <div class="BottomFix"></div>
            </div>
            <div class="BottomFix"></div>
          </div> 
            <div class="BottomFix"></div>
          </div>        
        </div>
        
        <div class="BottomFix"></div>
        </div>
      </div>
      <div class="DivPopBottom"></div>
    </div>

<script type="text/javascript">
  /*<![CDATA[*/
  checkCentreColumn();
  initPersonal('RightColumn');
  streamBrowseView();
  addCrosses();
  addWeight();
  initPrivacy('RightColumn')
  initPager('RightColumn')
  updatePager()
  checkDownloadAuto()
  /*]]>*/
</script>

</asp:Content>
