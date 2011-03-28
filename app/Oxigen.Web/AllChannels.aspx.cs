using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.BLClients;
using System.Data;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIPresentation.CommandHandlers;

namespace OxigenIIPresentation
{
  public partial class AllChannels : System.Web.UI.Page
  {
    const int _pageSize = 64;
    int _mostPopularCounter = 1;
    int _channelCounter = 0;
    string _activeLetter = null;
    SortChannelsBy _sort;

    protected void Page_Load(object sender, EventArgs e)
    {
      string sortBy = null;

      if (Request.QueryString["active"] == null)
        _activeLetter = "A";
      else
        _activeLetter = Request.QueryString["active"];

      if (Request.QueryString["sortBy"] == null)
        sortBy = "a";
      else
        sortBy = Request.QueryString["sortBy"];

      _sort = TryParseSortChannelsBy(sortBy);

      GetSortingLinks();

      BLClient client = null;

      // access the DB for most popular channels and bind "most popular" once
      if (!Page.IsPostBack)
      {
        try
        {
          client = new BLClient();

          MostPopular.DataSource = client.GetChannelMostPopular();
          MostPopular.DataBind();
        }
        finally
        {
          client.Dispose();
        }

        GetChannels();
      }

      Panel activeLetterPanel = GetActivePanel(_activeLetter);

      AddSortingInfoToAlphabet(sortBy);

      activeLetterPanel.CssClass = "ActiveLetter";
    }

    private void GetSortingLinks()
    {
      switch (_sort)
      {
        case SortChannelsBy.Alphabetical:
          AlphabeticalSort.CssClass = "Active";
          AlphabeticalSort.NavigateUrl = null;
          PopularitySort.CssClass = "Inactive";
          PopularitySort.NavigateUrl = "~/AllChannels.aspx?sortBy=p&active=" + _activeLetter;
          break;
        case SortChannelsBy.Popularity:
          PopularitySort.CssClass = "Active";
          PopularitySort.NavigateUrl = null;
          AlphabeticalSort.CssClass = "Inactive";
          AlphabeticalSort.NavigateUrl = "~/AllChannels.aspx?sortBy=a&active=" + _activeLetter;
          break;
      }
    }

    private int CurrentPage
    {
      get
      {
        // look for current page in ViewState
        object o = this.ViewState["_CurrentPage"];
        if (o == null)
          return 0;	// default to showing the first page
        else
          return (int)o;
      }

      set
      {
        this.ViewState["_CurrentPage"] = value;
      }
    }

    public void Pages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      int pageNumberInt = (int)e.Item.DataItem;
      string pageNumberString = pageNumberInt.ToString();

      if (CurrentPage == pageNumberInt - 1)
      {
        Literal pageNoLink = (Literal)e.Item.FindControl("pageNoLink");

        pageNoLink.Text = pageNumberString;
      }
      else
      {
        LinkButton pageLink = (LinkButton)e.Item.FindControl("pageLink");

        pageLink.CommandArgument = pageNumberString;

        pageLink.Text = pageNumberString;
      }
    }

    public void Page_Command(object sender, CommandEventArgs e)
    {
      CurrentPage = int.Parse((string)e.CommandArgument) - 1;
      GetChannels();
    }

    protected List<int> CreatePageLinks(PagedDataSource pds)
    {
      List<int> listPageNumbers = new List<int>();

      // calc low and high limits for numeric links
      int low = pds.CurrentPageIndex - 1;
      int high = pds.CurrentPageIndex + 3;

      if (low < 1) low = 1;
      if (high > pds.PageCount) high = pds.PageCount;
      if (high - low < 5) while ((high < low + 4) && high < pds.PageCount) high++;
      if (high - low < 5) while ((low > high - 4) && low > 1) low--;
      
      for (int x = low; x < high + 1; x++) 
        listPageNumbers.Add(x);
      
      return listPageNumbers;
    }

    private void GetChannels()
    {
      PagedDataSource pds = new PagedDataSource();
      BLClient client = null;

      try
      {
        client = new BLClient();

        pds.DataSource = client.GetChannelListByLetter(_activeLetter, _sort);
      }
      finally
      {
        client.Dispose();
      }

      pds.AllowPaging = true;
      pds.PageSize = _pageSize;

      pds.CurrentPageIndex = CurrentPage;

      Prev.Visible = !pds.IsFirstPage;
      Next.Visible = !pds.IsLastPage;

      channelList.DataSource = pds;
      channelList.DataBind();

      List<int> pageNumbers = CreatePageLinks(pds);

      pages.DataSource = pageNumbers;
      pages.DataBind();
    }

    public void Channels_Databound(object sender, RepeaterItemEventArgs e)
    {
      ChannelSimple channel = (ChannelSimple)e.Item.DataItem;

      if (channel == null)
        return;

      HyperLink ChannelLink = (HyperLink)e.Item.FindControl("ChannelLink");
      Literal SiteMapListRight = (Literal)e.Item.FindControl("SiteMapListRight");

      ChannelLink.Text = channel.ChannelName;
      ChannelLink.NavigateUrl = "~/ChannelDetails.aspx?channelID=" + channel.ChannelID;

      if (_channelCounter == 31)
        SiteMapListRight.Text = "</div><div class=\"SiteMapListRight\">";

      _channelCounter++;
    }

    public void MostPopular_ItemDatabound(object sender, RepeaterItemEventArgs e)
    {
      ChannelSimple channel = (ChannelSimple)e.Item.DataItem;

      if (channel == null)
        return;

      Label ChannelNumber = (Label)e.Item.FindControl("ChannelNumber");
      HyperLink ChannelName = (HyperLink)e.Item.FindControl("ChannelName");

      ChannelNumber.Text = _mostPopularCounter.ToString();
      ChannelName.Text = channel.ChannelName;
      ChannelName.ToolTip = channel.ChannelName;
      ChannelName.NavigateUrl = "~/ChannelDetails.aspx?channelID=" + channel.ChannelID;

      _mostPopularCounter++;
    }

    private void AddSortingInfoToAlphabet(string sortBy)
    {
      toA.NavigateUrl = "~/AllChannels.aspx?active=A&sortBy=" + sortBy;
      toB.NavigateUrl = "~/AllChannels.aspx?active=B&sortBy=" + sortBy;
      toC.NavigateUrl = "~/AllChannels.aspx?active=C&sortBy=" + sortBy;
      toD.NavigateUrl = "~/AllChannels.aspx?active=D&sortBy=" + sortBy;
      toE.NavigateUrl = "~/AllChannels.aspx?active=E&sortBy=" + sortBy;
      toF.NavigateUrl = "~/AllChannels.aspx?active=F&sortBy=" + sortBy;
      toG.NavigateUrl = "~/AllChannels.aspx?active=G&sortBy=" + sortBy;
      toH.NavigateUrl = "~/AllChannels.aspx?active=H&sortBy=" + sortBy;
      toI.NavigateUrl = "~/AllChannels.aspx?active=I&sortBy=" + sortBy;
      toJ.NavigateUrl = "~/AllChannels.aspx?active=J&sortBy=" + sortBy;
      toK.NavigateUrl = "~/AllChannels.aspx?active=K&sortBy=" + sortBy;
      toL.NavigateUrl = "~/AllChannels.aspx?active=L&sortBy=" + sortBy;
      toM.NavigateUrl = "~/AllChannels.aspx?active=M&sortBy=" + sortBy;
      toN.NavigateUrl = "~/AllChannels.aspx?active=N&sortBy=" + sortBy;
      toO.NavigateUrl = "~/AllChannels.aspx?active=O&sortBy=" + sortBy;
      toP.NavigateUrl = "~/AllChannels.aspx?active=P&sortBy=" + sortBy;
      toQ.NavigateUrl = "~/AllChannels.aspx?active=Q&sortBy=" + sortBy;
      toR.NavigateUrl = "~/AllChannels.aspx?active=R&sortBy=" + sortBy;
      toS.NavigateUrl = "~/AllChannels.aspx?active=S&sortBy=" + sortBy;
      toT.NavigateUrl = "~/AllChannels.aspx?active=T&sortBy=" + sortBy;
      toU.NavigateUrl = "~/AllChannels.aspx?active=U&sortBy=" + sortBy;
      toV.NavigateUrl = "~/AllChannels.aspx?active=V&sortBy=" + sortBy;
      toW.NavigateUrl = "~/AllChannels.aspx?active=W&sortBy=" + sortBy;
      toX.NavigateUrl = "~/AllChannels.aspx?active=X&sortBy=" + sortBy;
      toY.NavigateUrl = "~/AllChannels.aspx?active=Y&sortBy=" + sortBy;
      toZ.NavigateUrl = "~/AllChannels.aspx?active=Z&sortBy=" + sortBy;
    }

    private Panel GetActivePanel(string activeLetter)
    {
      switch (activeLetter)
      {
        case "A":
          toA.Visible = false;
          LabelA.Visible = true;
          return panelA;

        case "B":
          toB.Visible = false;
          LabelB.Visible = true;
          return panelB;

        case "C":
          toC.Visible = false;
          LabelC.Visible = true;
          return panelC;

        case "D":
          toD.Visible = false;
          LabelD.Visible = true;
          return panelD;

        case "E":
          toE.Visible = false;
          LabelE.Visible = true;
          return panelE;

        case "F":
          toF.Visible = false;
          LabelF.Visible = true;
          return panelF;

        case "G":
          toG.Visible = false;
          LabelG.Visible = true;
          return panelG;

        case "H":
          toH.Visible = false;
          LabelH.Visible = true;
          return panelH;

        case "I":
          toI.Visible = false;
          LabelI.Visible = true;
          return panelI;

        case "J":
          toJ.Visible = false;
          LabelJ.Visible = true;
          return panelJ;

        case "K":
          toK.Visible = false;
          LabelK.Visible = true;
          return panelK;

        case "L":
          toL.Visible = false;
          LabelL.Visible = true;
          return panelL;

        case "M":
          toM.Visible = false;
          LabelM.Visible = true;
          return panelM;

        case "N":
          toN.Visible = false;
          LabelN.Visible = true;
          return panelN;

        case "O":
          toO.Visible = false;
          LabelO.Visible = true;
          return panelO;

        case "P":
          toP.Visible = false;
          LabelP.Visible = true;
          return panelP;

        case "Q":
          toQ.Visible = false;
          LabelQ.Visible = true;
          return panelQ;

        case "R":
          toR.Visible = false;
          LabelR.Visible = true;
          return panelR;

        case "S":
          toS.Visible = false;
          LabelS.Visible = true;
          return panelS;

        case "T":
          toT.Visible = false;
          LabelT.Visible = true;
          return panelT;

        case "U":
          toU.Visible = false;
          LabelU.Visible = true;
          return panelU;

        case "V":
          toV.Visible = false;
          LabelV.Visible = true;
          return panelV;

        case "W":
          toW.Visible = false;
          LabelW.Visible = true;
          return panelW;

        case "X":
          toX.Visible = false;
          LabelX.Visible = true;
          return panelX;

        case "Y":
          toY.Visible = false;
          LabelY.Visible = true;
          return panelY;

        case "Z":
          toZ.Visible = false;
          LabelZ.Visible = true;
          return panelZ;

        default:
          toA.Visible = false;
          LabelA.Visible = true;
          return panelA;
      }
    }

    private SortChannelsBy TryParseSortChannelsBy(string sortByValue)
    {
      QueryStringParameterValueConfiguration queryStringParameterValueConfiguration = (QueryStringParameterValueConfiguration)System.Configuration.ConfigurationManager.GetSection("queryStringParameterGroup/enumQueryStringParameterSet");

      if (queryStringParameterValueConfiguration.QueryStringParameters[sortByValue] == null)
        throw new ArgumentException("Enum parameter " + sortByValue + " does not exist.");

      string sortBy =  queryStringParameterValueConfiguration.QueryStringParameters[sortByValue].Value;

      return (SortChannelsBy)Enum.Parse(typeof(SortChannelsBy), sortBy);
    }

    public void Prev_Click(object sender, System.EventArgs e)
    {
      CurrentPage--;
      GetChannels();
    }

    public void Next_Click(object sender, System.EventArgs e)
    {
      CurrentPage++;
      GetChannels();
    }
  }
}
