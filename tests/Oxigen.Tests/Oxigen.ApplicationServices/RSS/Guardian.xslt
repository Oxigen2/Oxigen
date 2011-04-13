<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:media="http://search.yahoo.com/mrss/"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />

  <xsl:template match="/rss/channel">
    <xsl:for-each select="item[media:content/@width=460]">
      <li>
        pubDate = <xsl:value-of select="pubDate"
                     disable-output-escaping="yes"  />
        guid= <xsl:value-of select="guid"
                         disable-output-escaping="yes"  />
        link = <xsl:value-of select="link"
                        disable-output-escaping="yes"  />
        title = <xsl:value-of select="title"
                       disable-output-escaping="yes"  />
        pubDate = <xsl:value-of select="pubDate" />
        image = <xsl:value-of select="media:content[@width=460]/@url" />
        credit =  <xsl:value-of select="media:content[@width=460]/media:credit" />

      </li>

    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>