<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html>
	<head>
		<title>DoneInTime Report</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
	</head>
	<body>
		<h1>Liste des tâches en cours</h1>
		<ul>
			<xsl:for-each select="Tasks/Task">
				<li>
					<span>
						<xsl:if test="IsRunning='True'"><xsl:attribute name="style">background-color: #00FF00;</xsl:attribute></xsl:if>
						<xsl:value-of select="Name" /> : 
						<xsl:value-of select="TimeCount" />
					</span>
				</li>
			</xsl:for-each>
		</ul>
    </body>
  </html>
</xsl:template>
</xsl:stylesheet>