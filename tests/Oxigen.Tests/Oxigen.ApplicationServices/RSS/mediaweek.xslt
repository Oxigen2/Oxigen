<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />

  <xsl:template match="/rss/channel">
    <slidefeed>
      <script>
        //css_ref HtmlAgilityPack.dll;
        using HtmlAgilityPack;
        using System.Text.RegularExpressions;
        public class Script {
        static public string ScrapeImage(string url) {
        var hw = new HtmlWeb();
        var doc = hw.Load(url);
        var node = doc.DocumentNode.SelectSingleNode("//div[@id=\"articleImageHolder\"]//img");
        if (node != null)
        {
        var src = node.Attributes["src"].Value;
        src = Regex.Replace(src, "/scaleToFit/\\d+/\\d+/", "/scaleToFit/854/570/");
        return src;
        }
        else
        {
        node = doc.DocumentNode.SelectSingleNode("//div[@class=\"articleAssetsImages\"]//img");
        if (node == null) return null;
        var src = node.Attributes["src"].Value;
        return src;
        }
        }
        }
      </script>
      <xsl:for-each select="item">
        <item>
          <xsl:attribute name="guid">
            <xsl:value-of select="link" />
          </xsl:attribute>
          <parameter name="PublishedDate" type="date" format="d MMMM yyyy HH:mm">
            <xsl:value-of select="pubDate"/>
          </parameter>
          <parameter name="TitleText" type="text">
            <xsl:value-of select="title"/>
          </parameter>
          <parameter name="ClickThroughUrl" type="text">
            <xsl:value-of select="link"/>
          </parameter>
          <parameter name="MasterImage" type="image">
            <call-script name="ScrapeImage">
              <wih-param name="url">
                <xsl:value-of select="link"/>
              </wih-param>
            </call-script>
          </parameter>
        </item>
      </xsl:for-each>
    </slidefeed>
  </xsl:template>
</xsl:stylesheet>



