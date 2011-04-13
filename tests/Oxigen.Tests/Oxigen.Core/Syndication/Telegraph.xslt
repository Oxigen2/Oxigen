<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />

  <xsl:template match="/rss/channel">
    { items: [
    <xsl:for-each select="item">
      {
      "date": "<xsl:value-of select="pubDate"
           disable-output-escaping="yes"  />",
      "guid": "<xsl:value-of select="guid"
               disable-output-escaping="yes"  />",
      "url": "<xsl:value-of select="link"
                disable-output-escaping="yes"  />",
      "title": "<xsl:value-of select="title"
           disable-output-escaping="yes"  />",
      "image": "<xsl:value-of select="enclosure/@url"/>"
      }<xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each>
    ] }
  </xsl:template>
</xsl:stylesheet>