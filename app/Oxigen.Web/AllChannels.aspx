<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="AllChannels.aspx.cs" Inherits="OxigenIIPresentation.AllChannels" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="PageTitle">
  <div><span class="NormalTabLeft"></span><span class="NormalTabMiddle">All Streams</span><span class="NormalTabRight"></span></div>
</div>
<div class="PanelNormalTop">
  <p>Browse below either alphabetically or by the most popular to find more information on a screensaver stream.</p>
</div>
<div class="PanelNormal">

  <div class="SiteMap">
    <div class="SiteMapLeft">
      <h2>Most Popular Streams</h2>
      <div class="SiteMapPopChannels">
        <table cellspacing="0">
          <asp:repeater id="MostPopular" runat="server" onitemdatabound="MostPopular_ItemDatabound">
            <itemtemplate>
            <tr>
              <td class="Col1"><asp:label id="ChannelNumber" runat="server" />.</td>
              <td><asp:hyperlink id="ChannelName" runat="server"/></td>
            </tr>
            </itemtemplate>
          </asp:repeater>
        </table>
      </div>
    </div>
    <div class="SiteMapRight">
      <h2>Alphabetical List of All Streams</h2>
      <div class="SiteMapLetters">
        <asp:panel id="panelA" runat="server"><asp:hyperlink text="A" runat="server" navigateurl="~/AllChannels.aspx?active=A" id="toA" visible="true"/><asp:label id="LabelA" text="A" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelB" runat="server"><asp:hyperlink text="B" runat="server" navigateurl="~/AllChannels.aspx?active=B" id="toB" visible="true"/><asp:label id="LabelB" text="B" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelC" runat="server"><asp:hyperlink text="C" runat="server" navigateurl="~/AllChannels.aspx?active=C" id="toC" visible="true"/><asp:label id="LabelC" text="C" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelD" runat="server"><asp:hyperlink text="D" runat="server" navigateurl="~/AllChannels.aspx?active=D" id="toD" visible="true"/><asp:label id="LabelD" text="D" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelE" runat="server"><asp:hyperlink text="E" runat="server" navigateurl="~/AllChannels.aspx?active=E" id="toE" visible="true"/><asp:label id="LabelE" text="E" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelF" runat="server"><asp:hyperlink text="F" runat="server" navigateurl="~/AllChannels.aspx?active=F" id="toF" visible="true"/><asp:label id="LabelF" text="F" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelG" runat="server"><asp:hyperlink text="G" runat="server" navigateurl="~/AllChannels.aspx?active=G" id="toG" visible="true"/><asp:label id="LabelG" text="G" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelH" runat="server"><asp:hyperlink text="H" runat="server" navigateurl="~/AllChannels.aspx?active=H" id="toH" visible="true"/><asp:label id="LabelH" text="H" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelI" runat="server"><asp:hyperlink text="I" runat="server" navigateurl="~/AllChannels.aspx?active=I" id="toI" visible="true"/><asp:label id="LabelI" text="I" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelJ" runat="server"><asp:hyperlink text="J" runat="server" navigateurl="~/AllChannels.aspx?active=J" id="toJ" visible="true"/><asp:label id="LabelJ" text="J" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelK" runat="server"><asp:hyperlink text="K" runat="server" navigateurl="~/AllChannels.aspx?active=K" id="toK" visible="true"/><asp:label id="LabelK" text="K" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelL" runat="server"><asp:hyperlink text="L" runat="server" navigateurl="~/AllChannels.aspx?active=L" id="toL" visible="true"/><asp:label id="LabelL" text="L" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelM" runat="server"><asp:hyperlink text="M" runat="server" navigateurl="~/AllChannels.aspx?active=M" id="toM" visible="true"/><asp:label id="LabelM" text="M" visible="false" runat="server" /></asp:panel>
        <div class="SiteMapLetterSplitter"></div>
        <asp:panel id="panelN" runat="server"><asp:hyperlink text="N" runat="server" navigateurl="~/AllChannels.aspx?active=N" id="toN" visible="true"/><asp:label id="LabelN" text="N" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelO" runat="server"><asp:hyperlink text="O" runat="server" navigateurl="~/AllChannels.aspx?active=O" id="toO" visible="true"/><asp:label id="LabelO" text="O" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelP" runat="server"><asp:hyperlink text="P" runat="server" navigateurl="~/AllChannels.aspx?active=P" id="toP" visible="true"/><asp:label id="LabelP" text="P" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelQ" runat="server"><asp:hyperlink text="Q" runat="server" navigateurl="~/AllChannels.aspx?active=Q" id="toQ" visible="true"/><asp:label id="LabelQ" text="Q" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelR" runat="server"><asp:hyperlink text="R" runat="server" navigateurl="~/AllChannels.aspx?active=R" id="toR" visible="true"/><asp:label id="LabelR" text="R" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelS" runat="server"><asp:hyperlink text="S" runat="server" navigateurl="~/AllChannels.aspx?active=S" id="toS" visible="true"/><asp:label id="LabelS" text="S" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelT" runat="server"><asp:hyperlink text="T" runat="server" navigateurl="~/AllChannels.aspx?active=T" id="toT" visible="true"/><asp:label id="LabelT" text="T" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelU" runat="server"><asp:hyperlink text="U" runat="server" navigateurl="~/AllChannels.aspx?active=U" id="toU" visible="true"/><asp:label id="LabelU" text="U" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelV" runat="server"><asp:hyperlink text="V" runat="server" navigateurl="~/AllChannels.aspx?active=V" id="toV" visible="true"/><asp:label id="LabelV" text="V" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelW" runat="server"><asp:hyperlink text="W" runat="server" navigateurl="~/AllChannels.aspx?active=W" id="toW" visible="true"/><asp:label id="LabelW" text="W" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelX" runat="server"><asp:hyperlink text="X" runat="server" navigateurl="~/AllChannels.aspx?active=X" id="toX" visible="true"/><asp:label id="LabelX" text="X" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelY" runat="server"><asp:hyperlink text="Y" runat="server" navigateurl="~/AllChannels.aspx?active=Y" id="toY" visible="true"/><asp:label id="LabelY" text="Y" visible="false" runat="server" /></asp:panel>
        <asp:panel id="panelZ" runat="server"><asp:hyperlink text="Z" runat="server" navigateurl="~/AllChannels.aspx?active=Z" id="toZ" visible="true"/><asp:label id="LabelZ" text="Z" visible="false" runat="server" /></asp:panel>
      </div>
      <div class="SortBy">
        <span>Sort by:</span><asp:hyperlink id="AlphabeticalSort" runat="server" text="Alphabetical" /><span>|</span><asp:hyperlink id="PopularitySort" runat="server" text="Popularity" />
      </div>
      <div class="SiteMapList">
        <div class="SiteMapListLeft">
          <asp:repeater id="channelList" runat="server" onitemdatabound="Channels_Databound">
            <itemtemplate>
              <div><asp:hyperlink id="ChannelLink" runat="server" /></div>    
              <asp:literal id="SiteMapListRight" runat="server" />        
            </itemtemplate>
          </asp:repeater>   
        </div>     
      </div>
      <div class="SiteMapPager">
        <span><asp:linkbutton id="Prev" runat="server" onclick="Prev_Click" text=" << "/></span>
        <asp:repeater id="pages" runat="server" onitemdatabound="Pages_ItemDataBound">
          <itemtemplate>
            <span><asp:linkButton id="pageLink" oncommand="Page_Command" runat="server" /></span>
            <span class="SiteMapPagerCurrent"><asp:literal id="pageNoLink" runat="server" /></span>
          </itemtemplate>
        </asp:repeater>
        <span><asp:linkbutton id="Next" runat="server" onclick="Next_Click" text=" >> "/></span>
      </div>
    </div>
  </div>
  <div class="BottomFix"></div>
</div>
<div class="PanelNormalBottom"></div>



</asp:Content>
