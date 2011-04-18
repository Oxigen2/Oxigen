<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />

  <xsl:template match="/rss/channel">
    <items>
    <xsl:for-each select="item[enclosure]">
      <item>
        <date><xsl:value-of select="pubDate"/></date>
      ``<guid><xsl:value-of select="guid" /></guid>
        <title><xsl:value-of select="title"/></title>
        <url><xsl:value-of select="link"/></url>
        <image><xsl:value-of select="enclosure/@url"/></image>
    </item>
    </xsl:for-each>
  </items>  
  </xsl:template>
</xsl:stylesheet>