<html>
<head>
<title>¤é¾ä Calendar</title>
<script language="JavaScript">
	var sTemp
</script>
</head>

<body style="margin: 0px;" scroll="no" bgcolor="#c0c0c0">

<table border="0" cellpadding="0" cellspacing="0" width="100%">
  <tr>
    <td>
      <OBJECT data="calendar2.htm" height="160" id=cal style="HEIGHT: 160px; LEFT: 0px; TOP: 0px; WIDTH: 200px" 
	type=text/x-scriptlet width="200"></OBJECT>
    </td>
  </tr>
  <tr>
    <td><input type="button" value="Close" name="cmdClose" style="WIDTH: 200px" ONCLICK="window.close();"></td>
  </tr>
</table>

<p>
<script LANGUAGE="JavaScript" FOR="cal" EVENT="onscriptletevent(name,eventdata)">
	dateChange(eventdata);
</script>
<script type="text/javascript" language="JavaScript">
<!--
window.onload = initWindow;
function initWindow() {
	window.returnValue = window.dialogArguments;
	cal.setDate(window.dialogArguments);
}

function dateChange(sValue) {

	window.returnValue = sValue;
	window.close();
}

// -->
</script></p>
</body>
</html>

<html><script type="text/javascript" language="JavaScript"></script></html>
