<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:template match="/">

    <html>
      <head>
        <title>Rapidtrade Order</title>
      </head>
      <body leftmargin="0" marginwidth="0" topmargin="0" marginheight="0" offset="0" bgcolor="#EEE" style="background:#EEE;">
        <table width="100%" cellpadding="10" cellspacing="0" bgcolor="#EEE" style="background:#EEE;">
          <tr>
            <td valign="top" align="center">
              <table border="0" cellpadding="0" cellspacing="0" width="800" bgcolor="#FFF" style="padding:10px;background:#FFF;">
                <tr>
                  <td width="360" valign="bottom">
                    <img src="http://www.rapidtrade.biz/Portals/0/rapidtrade.png" border="0" style="margin:30px 0 10px 0;" />
                  </td>
                  <td width="440" valign="bottom">
                    <p style="text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:20pt;color:#AAA;margin:0;">Incoming Order</p>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <p style="border-top:solid 2px #AAA;"></p>
                  </td>
                </tr>
                <tr>
                  <td style="text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:black;vertical-align:top;">
                    <b>Ordered By:</b>
                    <br>
                      Account:<xsl:value-of select="OrderInfo/AccountID"/>
                    </br>
                    
                  <br></br>
                  <br>Name: <xsl:value-of select="OrderInfo/DeliveryName"/>                </br>
                  <br>Address: </br><br><xsl:value-of select="OrderInfo/DeliveryAddress1"/></br>
                  <br><xsl:value-of select="OrderInfo/DeliveryAddress2"/></br>
                    <br>
                     Email:   <xsl:value-of select="OrderInfo/DeliveryAddress3"/>
                    </br>
                    <br>
                      Telephone: <xsl:value-of select="OrderInfo/DeliveryPostCode"/>
                    </br>
                  <br></br>
                  </td>
                  <td>
                    <p style="text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;color:black;vertical-align:top;font-size:8pt;margin:0 0 20px 0;">
                      Order Number: <xsl:value-of select="OrderInfo/OrderID"/>
                      <br>
                        Order Date: <xsl:value-of select="OrderInfo/CreateDate"/>
                      </br>
                      <br>
                        Reference: <xsl:value-of select="OrderInfo/Reference"/>
                      </br>
                    </p>
                    <p style="text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;color:black;vertical-align:top;font-size:10pt;">
                      <b>Required Date: </b>
                      <xsl:value-of select="OrderInfo/RequiredByDate"/>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p style="text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;color:black;vertical-align:top;font-size:10pt;margin:20px 0 20px 0;">
                      <b>Comment:</b>
                      <br>
                        <xsl:value-of select="OrderInfo/Comments"/>
                      </br>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                      <tr valign="bottom">
                        <td style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;border-bottom:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Item</td>
                        <td style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;border-bottom:solid 1px #AAA;text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Product</td>
                        <td style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;border-bottom:solid 1px #AAA;text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Description</td>
                        <td style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;border-bottom:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Qty Ordered</td>
                        <td style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;border-bottom:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Price</td>
                        <td style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;border-bottom:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Sub Total</td>
                      </tr>
                      <xsl:for-each select="OrderInfo/OrderItems/OrderItemInfo">
                        <tr>
                          <td style="padding:5px;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">
                            <xsl:value-of select="ItemID"/>
                          </td>
                          <td style="padding:5px;text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">
                            <xsl:value-of select="ProductID"/>
                          </td>
                          <td style="padding:5px;text-align:left;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">
                            <xsl:value-of select="Description"/>
                          </td>
                          <td style="padding:5px;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">
                            <xsl:value-of select="Quantity"/>
                          </td>
                          <td style="padding:5px;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">
                            <xsl:value-of select="Nett"/>
                          </td>
                          <td style="padding:5px;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">
                            <xsl:value-of select="Value"/>
                          </td>
                        </tr>
                      </xsl:for-each>
                      <tr valign="bottom">
                        <td colspan="5" style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Order Total Excl:</td>
                        <td style="font-weight:bold;padding:10px 5px 5px 5px;border-top:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">

                        </td>
                      </tr>
                      <tr valign="bottom">
                        <td colspan="5" style="font-weight:bold;padding:2px 5px;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">GST:</td>
                        <td style="font-weight:bold;padding:2px 5px;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">

                        </td>
                      </tr>
                      <tr valign="bottom">
                        <td colspan="5" style="font-weight:bold;padding:2px 5px;border-bottom:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">Order Total Incl:</td>
                        <td style="font-weight:bold;padding:2px 5px;border-bottom:solid 1px #AAA;text-align:right;font-family:Geneva,Verdana,Arial,Helvetica,sans-serif;font-size:10pt;color:#000;">

                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </body>
    </html>

  </xsl:template>
</xsl:stylesheet>
