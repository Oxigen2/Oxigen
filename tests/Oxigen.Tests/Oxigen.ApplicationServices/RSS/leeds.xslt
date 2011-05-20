<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:media="http://search.yahoo.com/mrss"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />

  <xsl:template match="/rss/channel">
    <slidefeed>
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
          <parameter name="DescriptionText" type="text">
            <xsl:value-of select="description"/>
          </parameter>
          <parameter name="ClickThroughUrl" type="text">
            <xsl:value-of select="link"/>
          </parameter>
          <parameter name="MasterImage" type="image">
            <xsl:value-of select="media:thumbnail[@width=360]/@url"/>
          </parameter>
        </item>
      </xsl:for-each>
    </slidefeed>
  </xsl:template>
</xsl:stylesheet>