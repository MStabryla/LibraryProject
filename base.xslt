<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <!--<xsl:output method="xml" indent="yes"/>-->

  <xsl:template match="/">
    <html>
      <head>
        <!--meta charset="UTF-8"/>-->
        <title>Projekt biblioteczny - klasa 4id</title>
        <style>

        </style>
      </head>
      <body>
        <h1 style="text-align:center;">Rozprawki klasy 4id</h1>
        <p>
          Jest to zbiór rozprawek sporządzony przez klasę 4id w dniu  29.11.2017.Dane pochodzą ze strony <a href="http://bibliotekazsl.azurewebsites.net/">bibiotekazsl.azurewebsites.net</a>
        </p>
        <div class="list">
          <xsl:for-each select="essays/essay">
            <div class="essay">
              <h3>
                <xsl:value-of select="question"/>
              </h3>
              <span class="lres">
                Zasób biblioteczny : <xsl:value-of select="library_res"/>
              </span>
              <span class="author">
                <xsl:value-of select="author/name"/>
                <xsl:value-of select="author/surname"/>
              </span>
              <div class="arguments">
                <xsl:for-each select="arguments/argument">
                  <div class="arg">
                    <span class="kind">
                      <xsl:value-of select="rodzaj"/>
                    </span>
                    <h5>
                      <xsl:value-of select="Źródło"/>
                    </h5>
                    <p>
                      <xsl:value-of select="treść"/>
                    </p>
                  </div>
                </xsl:for-each>
              </div>
            </div>
          </xsl:for-each>

        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
