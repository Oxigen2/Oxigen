<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:media="http://search.yahoo.com/mrss/"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />

  <xsl:template match="/rss/channel">
    <items>
    <xsl:for-each select="item[media:content/@width=460]">
      <item>
        <date><xsl:value-of select="pubDate"/></date>
        <guid><xsl:value-of select="guid" /></guid>
        <url><xsl:value-of select="link"/></url>
        <title><xsl:value-of select="title"/></title>
        <image><xsl:value-of select="media:content[@width=460]/@url" /></image>
        <credit><xsl:value-of select="media:content[@width=460]/media:credit" /></credit>
      </item>
    </xsl:for-each>
    </items>
  </xsl:template>
</xsl:stylesheet>