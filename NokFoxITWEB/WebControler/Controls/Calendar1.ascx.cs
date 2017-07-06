using System.Text; 

// Integrates the JavaScript Zapatec Calendar into ASP.NET web application.
public partial class Calendar1 : System.Web.UI.UserControl 
{ 

 //[System.Diagnostics.DebuggerStepThrough()] 

    //private void InitializeComponent()
    //{
    //}
 // protected System.Web.UI.HtmlControls.HtmlInputButton DateCommand; 
 // protected System.Web.UI.HtmlControls.HtmlImage DateImageCommand;

 //NOTE: The following placeholder declaration is required by the Web Form Designer.
 //Do not delete or move it.
    //private object designerPlaceholderDeclaration; 

    //override protected void OnInit(System.EventArgs e)
    //{
    //    //
    //    // CODEGEN: This call is required by the ASP.NET Web Form Designer.
    //    //
    //    InitializeComponent();
    //    base.OnInit(e);
    //}



 private string _rootPath = "../WebControler/";
 private string _textFormat = "%Y/%m/%d %H:%M"; 
 private bool _showsTime = true; 
 private bool _singleClick = false; 
 private string _dateStatusFunc = ""; 
 private string _commandImageUrl = ""; 
 private string _align = ""; 
 private int _firstDay = 0; 
 private bool _weekNumbers = true; 
 private bool _showOthers = false; 
 private string _timeFormat = ""; 
 private int _step = -1; 
 private string _range = ""; 
 private bool _electric = false; 
 private string _daFormat = ""; 
 private string _extraDateText = ""; 
 private string _flatCallback = ""; 
 private System.DateTime[] _initialDates; 
 private string _disableFunc = ""; 
 private string _onUpdate = ""; 
 private string _scriptStartupBlock = "src/calendar-setup.js"; 
 private Languages _language = Languages.English; 
 private Themes _theme = Themes.Wood; 
 private Layouts _calendarSize = Layouts.Normal; 
 private DisplayModes _displayMode = DisplayModes.Popup;

	// Update: October 15,2005
	private int _numberMonths = 1; // Show one month by default.
	private int _monthsInRow = 1;
	private int _controlMonth = 1; // The default date, all the standard controls for next month, next year, etc are based on the Control Month. 
	private int _timeInterval = 5; // 5 minutes by default

 // Appearance modes.
 public enum DisplayModes 
 { 
   Popup, 
   PopupMultiple, 
   PopupButtonOnly, 
   Flat 
 }

 // List all available language.
 public enum Languages 
 { 
   English, 
   Romanian, 
   Italian, 
   French, 
   German, 
   Danish, 
   Norwegian, 
   Russian, 
   Spanish, 
   Greek, 
   Helene, 
   Czech, 
   Turkish, 
   Croatian,
   TraditionalChinese, 
   Japanese, 
   Korean,
   Simplified_Chinese
 }

 // List all available theme.
 public enum Themes 
 { 
   Winter, 
   Green, 
   Blue, 
   FancyBlue, 
   Yellow, 
   GreenGrass, 
   WindowsXP, 
   Windows2000, 
   System, 
   BlueXP, 
   Maroon, 
   Wood, 
   Forest 
 }

 // Lists possible sizes for the calendar.
 public enum Layouts 
 { 
   Tiny, 
   Small, 
   Normal, 
   Big, 
   Huge 
 } 

 // Page Load Handler.
protected void Page_Load(object sender, System.EventArgs e)
{
    if ((_displayMode == DisplayModes.Flat) | (_displayMode == DisplayModes.PopupButtonOnly))
    {
        DateText.Text = HiddenText.Value; // If DateText is not visible get its value from hidden field.
    }
    CommandPlace.Visible = true;
}

 // The calendar has heavy use of JavaScript script. We use this method to register those scripts for rendering.
 protected override void OnPreRender(System.EventArgs e) 
 { 
   base.OnPreRender(e); 
   this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"Zaptec_" + "src/utils.js", GetScriptDeclaration(_rootPath + "src/utils.js"));
   this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Zaptec_" + "src/calendar.js", GetScriptDeclaration(_rootPath + "src/calendar.js")); 
   string langPath = GetLanguagePath(_language);
   this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Zaptec_Setup_" + _scriptStartupBlock, GetScriptDeclaration(_rootPath + _scriptStartupBlock));
   this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Zaptec_Setup_" + langPath, GetScriptDeclaration(_rootPath + langPath)); 
   if (_displayMode == DisplayModes.Flat) {
       this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID + "_Callback", CreateFlatCallback(this.ClientID)); 
   } else if (_displayMode == DisplayModes.PopupMultiple) { 
     this.Page.ClientScript.RegisterArrayDeclaration(GetMultipleArrayName(this.ClientID), GetInitForMA());
     this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID + "_Close", CreateCloseScript(this.ClientID)); 
   }
   this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID + "_Setup", GetInlineBlock()); 
 } 

 // Creates client side script where path parameter is set as src attribute.
 private string GetScriptDeclaration(string path) 
 { 
   char newLine = System.Convert.ToChar(10); 
   char quote = System.Convert.ToChar(34); 
   StringBuilder declaration = new StringBuilder(); 
   declaration.Append("<script type="); 
   declaration.Append(quote); 
   declaration.Append("text/javascript"); 
   declaration.Append(quote); 
   declaration.Append(" src="); 
   declaration.Append(quote); 
   declaration.Append(path); 
   declaration.Append(quote); 
   declaration.Append("></script>"); 
   declaration.Append(newLine); 
   return declaration.ToString(); 
 } 

 // Creates inline JavaScript which is used to set up the calendar.
 private string GetInlineBlock() 
 { 
   char newLine = System.Convert.ToChar(10); 
   char quote = System.Convert.ToChar(34); 
   StringBuilder block = new StringBuilder(); 
   block.Append("<script type="); 
   block.Append(quote); 
   block.Append("text/javascript"); 
   block.Append(quote); 
   block.Append(">"); 
   block.Append(newLine); 
   block.Append(" <!--"); 
   block.Append(newLine); 
   block.Append(" Zapatec.Calendar.setup({"); 
   block.Append(newLine); 
   if ((_displayMode == DisplayModes.Popup) | (_displayMode == DisplayModes.PopupMultiple)) { 
     if (DisplayMode == DisplayModes.Popup) { 
       block.Append(" inputField : "); 
       block.Append(quote); 
       block.Append(DateText.ClientID); 
       block.Append(quote); 
       block.Append(","); 
       block.Append(newLine); 
     } else { 
       DateText.TextMode = System.Web.UI.WebControls.TextBoxMode.MultiLine; 
       DateText.Rows = 10; 
       block.Append(" multiple : "); 
       block.Append(GetMultipleArrayName(this.ClientID)); 
       block.Append(","); 
       block.Append(newLine); 
       block.Append(" onClose : "); 
       block.Append(GetCloseScriptName(this.ClientID)); 
       block.Append(","); 
       block.Append(newLine); 
     } 
     block.Append(" button : "); 
     block.Append(quote); 
     if (_commandImageUrl.Trim() != "") { 
       System.Web.UI.HtmlControls.HtmlInputImage imageCommand = new System.Web.UI.HtmlControls.HtmlInputImage(); 
       imageCommand.ID = "DateImageCommand"; 
       imageCommand.Src = "~/" + _rootPath + _commandImageUrl; 
       imageCommand.Alt = " Zapatec Calendar"; 
       CommandPlace.Controls.Add(imageCommand); 
       block.Append(imageCommand.ClientID); 
     } else { 
       System.Web.UI.HtmlControls.HtmlInputButton buttonCommand = new System.Web.UI.HtmlControls.HtmlInputButton("reset"); 
       buttonCommand.ID = "DateCommand"; 
       buttonCommand.Value = " ... "; 
       CommandPlace.Controls.Add(buttonCommand); 
       block.Append(buttonCommand.ClientID); 
     } 
     block.Append(quote); 
     block.Append(","); 
     block.Append(newLine); 
   } else if (_displayMode == DisplayModes.Flat) { 
     DateText.Visible = false; 
     CommandPlace.Visible = false; 
     FlatPlace.Visible = true; 
     block.Append(" flat : "); 
     block.Append(quote); 
     block.Append(this.ClientID + "_calendar_container"); 
     block.Append(quote); 
     block.Append(","); 
     block.Append(newLine); 
     block.Append(" flatCallback : "); 
     block.Append(CreateCallbackName(this.ClientID)); 
     block.Append(","); 
     block.Append(newLine); 
   } else if (_displayMode == DisplayModes.PopupButtonOnly) { 
     block.Append(" inputField : "); 
     block.Append(quote); 
     block.Append(HiddenText.ClientID); 
     block.Append(quote); 
     block.Append(","); 
     block.Append(newLine); 
     DateText.Visible = false; 
     block.Append(" button : "); 
     block.Append(quote); 
     if (_commandImageUrl.Trim() != "") { 
       System.Web.UI.HtmlControls.HtmlInputImage imageCommand = new System.Web.UI.HtmlControls.HtmlInputImage(); 
       imageCommand.ID = "DateImageCommand"; 
       imageCommand.Src = "~/" + _rootPath + _commandImageUrl; 
       imageCommand.Alt = " Zapatec Calendar"; 
       CommandPlace.Controls.Add(imageCommand); 
       block.Append(imageCommand.ClientID); 
     } else { 
       System.Web.UI.HtmlControls.HtmlInputButton buttonCommand = new System.Web.UI.HtmlControls.HtmlInputButton("reset"); 
       buttonCommand.ID = "DateCommand"; 
       buttonCommand.Value = " ... "; 
       CommandPlace.Controls.Add(buttonCommand); 
       block.Append(buttonCommand.ClientID); 
     } 
     block.Append(quote); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   //if (_firstDay > 0) { 
     block.Append(" firstDay : "); 
     block.Append(_firstDay.ToString()); 
     block.Append(","); 
     block.Append(newLine); 
   //} 
   block.Append(" weekNumbers : "); 
   block.Append(_weekNumbers.ToString().ToLower()); 
   block.Append(","); 
   block.Append(newLine); 
   block.Append(" showOthers : "); 
   block.Append(_showOthers.ToString().ToLower()); 
   block.Append(","); 
   block.Append(newLine); 
   if (_timeFormat.Trim() != "") { 
     block.Append(" timeFormat : "); 
     block.Append(quote); 
     block.Append(_timeFormat); 
     block.Append(quote); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   if (_step > 0) { 
     block.Append(" step : "); 
     block.Append(_step.ToString()); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   if (_range.Trim() != "") { 
     block.Append(" range : "); 
     block.Append(_range); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   block.Append(" electric : "); 
   block.Append(_electric.ToString().ToLower()); 
   block.Append(","); 
   block.Append(newLine); 
   if (_daFormat.Trim() != "") { 
     block.Append(" daFormat : "); 
     block.Append(quote); 
     block.Append(_daFormat); 
     block.Append(quote); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   if (_extraDateText.Trim() != "") { 
     block.Append(" dateText : "); 
     block.Append(_extraDateText); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   if (_disableFunc.Trim() != "") { 
     block.Append(" disableFunc : "); 
     block.Append(_disableFunc); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   if (_onUpdate.Trim() != "") { 
     block.Append(" onUpdate : "); 
     block.Append(OnUpdate); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   block.Append(" singleClick : "); 
   block.Append(_singleClick.ToString().ToLower()); 
   block.Append(","); 
   block.Append(newLine); 
   block.Append(" ifFormat : "); 
   block.Append(quote); 
   block.Append(_textFormat); 
   block.Append(quote); 
   block.Append(","); 
   block.Append(newLine); 
   if (_dateStatusFunc.Trim() != "") { 
     block.Append(" dateStatusFunc : "); 
     block.Append(_dateStatusFunc); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   if (_align.Trim() != "") { 
     block.Append(" align : "); 
     block.Append(quote); 
     block.Append(_align); 
     block.Append(quote); 
     block.Append(","); 
     block.Append(newLine); 
   } 
   // Update. October 15, 2005
	 if (_numberMonths > 1) 
	 { 
		 block.Append(" numberMonths : "); 
		 block.Append(_numberMonths); 
		 block.Append(","); 
		 block.Append(newLine); 
	 }
	 if (_controlMonth > 1) 
	 {
		 block.Append(" controlMonth : "); 
		 block.Append(_controlMonth); 
		 block.Append(","); 
		 block.Append(newLine); 
	 }
	 if (_monthsInRow > 1) 
	 {
		 block.Append(" monthsInRow : "); 
		 block.Append(_monthsInRow); 
		 block.Append(","); 
		 block.Append(newLine); 
	 }
	 if (_timeInterval > 0) 
	 {
		 block.Append(" timeInterval : "); 
		 block.Append(_timeInterval); 
		 block.Append(","); 
		 block.Append(newLine); 
	 }
   // End of update. October 15, 2005

   block.Append(" showsTime : "); 
   block.Append(_showsTime.ToString().ToLower()); 
   block.Append(newLine); 
   block.Append(" });"); 
   block.Append(newLine); 
   block.Append(" // -->"); 
   block.Append(newLine); 
   block.Append("</script>"); 
   block.Append(newLine); 
   return block.ToString(); 
 } 

 // Creates callback inline script. This script is used to populate the hidden field with the selected date.
 // Hidden field is used where DateTextBox is not visible.
 private string CreateFlatCallback(string id) 
 { 
   char newLine = System.Convert.ToChar(10); 
   char quote = System.Convert.ToChar(34); 
   StringBuilder func = new StringBuilder(); 
   func.Append("<script type="); 
   func.Append(quote); 
   func.Append("text/javascript"); 
   func.Append(quote); 
   func.Append(">"); 
   func.Append(newLine); 
   func.Append("function dateChanged_"); 
   func.Append(id); 
   func.Append("(calendar) {"); 
   func.Append(newLine); 
   func.Append(" var hidden = document.getElementById("); 
   func.Append(quote); 
   func.Append(id); 
   func.Append("_HiddenText"); 
   func.Append(quote); 
   func.Append(");"); 
   func.Append(newLine); 
   func.Append(" hidden.value = calendar.date.print(this.ifFormat)"); 
   func.Append(newLine); 
   if (_flatCallback.Trim() != "") { 
     func.Append(_flatCallback); 
     func.Append("(calendar);"); 
     func.Append(newLine); 
   } 
   func.Append("}"); 
   func.Append(newLine); 
   func.Append("</script>"); 
   func.Append(newLine); 
   return func.ToString(); 
 } 

 // Creates the name of callback function. The name is formed with the name of Calendar1 instance. In this way 
 // if there are more than one instance on the page each instance will have its own version of the function.
 private string CreateCallbackName(string id) 
 { 
   return "dateChanged_" + id; 
 } 

 // Creates close inline script.
 private string CreateCloseScript(string id) 
 { 
   char newLine = System.Convert.ToChar(10); 
   char quote = System.Convert.ToChar(34); 
   StringBuilder func = new StringBuilder(); 
   string ma = GetMultipleArrayName(id); 
   func.Append("<script type="); 
   func.Append(quote); 
   func.Append("text/javascript"); 
   func.Append(quote); 
   func.Append(">"); 
   func.Append(newLine); 
   func.Append("function "); 
   func.Append(GetCloseScriptName(id)); 
   func.Append("(cal) {"); 
   func.Append(newLine); 
   func.Append(" var el = document.getElementById("); 
   func.Append(quote); 
   func.Append(id); 
   func.Append("_DateText"); 
   func.Append(quote); 
   func.Append(");"); 
   func.Append(newLine); 
   func.Append(" el.value = "); 
   func.Append(quote); 
   func.Append(quote); 
   func.Append(";"); 
   func.Append(newLine); 
   func.Append(" "); 
   func.Append(ma); 
   func.Append(".length = 0;"); 
   func.Append(newLine); 
   func.Append(" for (var i in cal.multiple) {"); 
   func.Append(newLine); 
   func.Append(" var currentDate = cal.multiple[i];"); 
   func.Append(newLine); 
   func.Append(" if (currentDate) {"); 
   func.Append(newLine); 
   func.Append(" el.value += currentDate.print("); 
   func.Append(quote); 
   func.Append("%B %d %Y\\n"); 
   func.Append(quote); 
   func.Append(");"); 
   func.Append(newLine); 
   func.Append(" "); 
   func.Append(ma); 
   func.Append("["); 
   func.Append(ma); 
   func.Append(".length] = currentDate;"); 
   func.Append(newLine); 
   func.Append(" }"); 
   func.Append(" }"); 
   func.Append(" cal.hide();"); 
   func.Append(newLine); 
   func.Append(" return true;"); 
   func.Append(newLine); 
   func.Append("};"); 
   func.Append(newLine); 
   func.Append("</script>"); 
   func.Append(newLine); 
   return func.ToString(); 
 } 

 // Creates script which performs initialization of multiple array.
 private string GetInitForMA() 
 { 
   if (_initialDates == null) { 
     return ""; 
   } else { 
     StringBuilder script = new StringBuilder(); 
     char newLine = System.Convert.ToChar(10); 
     bool first = true; 
     foreach (System.DateTime d in _initialDates) { 
       if (first) { 
         first = false; 
       } else { 
         script.Append(", "); 
       } 
       script.Append("new Date("); 
       script.Append(d.Year.ToString()); 
       script.Append(", "); 
       script.Append(d.Month.ToString()); 
       script.Append(", "); 
       script.Append(d.Day.ToString()); 
       script.Append(")"); 
     } 
     return script.ToString(); 
   } 
 } 

 // Gets the name of close function.
 private string GetCloseScriptName(string id) 
 { 
   return id + "_Close"; 
 } 

 // Gets the name of multiple array.
 private string GetMultipleArrayName(string id) 
 { 
   return id + "_MA"; 
 } 

 // Gets the path of selected language.
 private string GetLanguagePath(Languages lang) 
 { 
   System.Collections.Hashtable languagePaths = new System.Collections.Hashtable();
   languagePaths.Add(Languages.English, "lang/calendar-en.js");
   languagePaths.Add(Languages.Romanian, "lang/calendar-ro.js"); 
   languagePaths.Add(Languages.Italian, "lang/calendar-it.js"); 
   languagePaths.Add(Languages.French, "lang/calendar-fr.js"); 
   languagePaths.Add(Languages.German, "lang/calendar-de.js"); 
   languagePaths.Add(Languages.Danish, "lang/calendar-da.js"); 
   languagePaths.Add(Languages.Norwegian, "lang/calendar-no.js"); 
   languagePaths.Add(Languages.Russian, "lang/calendar-ru.js"); 
   languagePaths.Add(Languages.Spanish, "lang/calendar-sp.js"); 
   languagePaths.Add(Languages.Greek, "lang/calendar-el.js"); 
   languagePaths.Add(Languages.Helene, "lang/calendar-he.js"); 
   languagePaths.Add(Languages.Czech, "lang/calendar-cs.js"); 
   languagePaths.Add(Languages.Turkish, "lang/calendar-tr.js"); 
   languagePaths.Add(Languages.Croatian, "lang/calendar-hr.js");
   languagePaths.Add(Languages.TraditionalChinese, "lang/calendar-big5.js"); 
   languagePaths.Add(Languages.Japanese, "lang/calendar-jp.js"); 
   languagePaths.Add(Languages.Korean, "lang/calendar-ko.js");
   languagePaths.Add(Languages.Simplified_Chinese, "lang/calendar-zh.js"); 
   return ((string)(languagePaths[lang])); 
 } 

 // Gets the path of selected theme.
 private string GetThemePath(Themes theme) 
 { 
   System.Collections.Hashtable themePaths = new System.Collections.Hashtable();
   themePaths.Add(Themes.Wood, "../WebControler/themes/wood.css");
   themePaths.Add(Themes.FancyBlue, "../WebControler/themes/fancyblue.css");
   themePaths.Add(Themes.Forest, "../WebControler/themes/forest.css");
   themePaths.Add(Themes.WindowsXP, "../WebControler/themes/winxp.css");
   themePaths.Add(Themes.Green, "../WebControler/themes/green.css");
   themePaths.Add(Themes.Maroon, "../WebControler/themes/maroon.css");
   themePaths.Add(Themes.Yellow, "../WebControler/themes/yellow.css");
   themePaths.Add(Themes.GreenGrass, "../WebControler/themes/greengrass.css");
   themePaths.Add(Themes.Windows2000, "../WebControler/themes/win2k.css");
   themePaths.Add(Themes.BlueXP, "../WebControler/themes/bluexp.css");
   themePaths.Add(Themes.Winter, "../WebControler/themes/winter.css");
   themePaths.Add(Themes.System, "../WebControler/themes/system.css"); 
   return ((string)(themePaths[theme])); 
 } 

 // Gets the title of selected theme.
 private string GetThemeTitle(Themes theme) 
 { 
   System.Collections.Hashtable themeTitles = new System.Collections.Hashtable(); 
   themeTitles.Add(Themes.Wood, "wood"); 
   themeTitles.Add(Themes.FancyBlue, "fancyblue"); 
   themeTitles.Add(Themes.Forest, "forest"); 
   themeTitles.Add(Themes.WindowsXP, "winxp"); 
   themeTitles.Add(Themes.Green, "green"); 
   themeTitles.Add(Themes.Maroon, "maroon"); 
   themeTitles.Add(Themes.Yellow, "yellow"); 
   themeTitles.Add(Themes.GreenGrass, "greengrass"); 
   themeTitles.Add(Themes.Windows2000, "win2k"); 
   themeTitles.Add(Themes.BlueXP, "bluexp"); 
   themeTitles.Add(Themes.Winter, "winter"); 
   themeTitles.Add(Themes.System, "system"); 
   return ((string)(themeTitles[theme])); 
 } 

 // Gets the path of selected layout.
 private string GetLayoutPath(Layouts size) 
 { 
   System.Collections.Hashtable sizePaths = new System.Collections.Hashtable();
   sizePaths.Add(Layouts.Big, "../WebControler/themes/layouts/big.css");
   sizePaths.Add(Layouts.Huge, "../WebControler/themes/layouts/huge.css"); 
   sizePaths.Add(Layouts.Normal, "");
   sizePaths.Add(Layouts.Small, "../WebControler/themes/layouts/small.css");
   sizePaths.Add(Layouts.Tiny, "../WebControler/themes/layouts/tiny.css"); 
   return ((string)(sizePaths[size])); 
 } 

 // Public Properties.

 public string RootPath { 
   get { 
     return _rootPath; 
   } 
   set { 
     _rootPath = value; 
   } 
 } 

 public string TextFormat { 
   get { 
     return _textFormat; 
   } 
   set { 
     _textFormat = value; 
   } 
 } 

 public bool ShowsTime { 
   get { 
     return _showsTime; 
   } 
   set { 
     _showsTime = value; 
   } 
 } 

 public string ThemePath { 
   get { 
     return GetThemePath(_theme); 
   } 
 } 

 public string ThemeTitle { 
   get { 
     return GetThemeTitle(_theme); 
   } 
 } 

 public System.Web.UI.WebControls.TextBox DateTextBox { 
   get { 
     return DateText; 
   } 
 } 

 public bool SingleClick { 
   get { 
     return _singleClick; 
   } 
   set { 
     _singleClick = value; 
   } 
 } 

 public string DateStatusFunc { 
   get { 
     return _dateStatusFunc; 
   } 
   set { 
     _dateStatusFunc = value; 
   } 
 } 

 public string CommandImageUrl { 
   get { 
     return _commandImageUrl; 
   } 
   set { 
     _commandImageUrl = value; 
   } 
 } 

 public string Align { 
   get { 
     return _align; 
   } 
   set { 
     _align = value; 
   } 
 } 

 public int FirstDay { 
   get { 
     return _firstDay; 
   } 
   set { 
     _firstDay = value; 
   } 
 } 

 public Languages Language { 
   get { 
     return _language; 
   } 
   set { 
     _language = value; 
   } 
 } 

 public Themes Theme { 
   get { 
     return _theme; 
   } 
   set { 
     _theme = value; 
   } 
 } 

 public Layouts CalendarSize { 
   get { 
     return _calendarSize; 
   } 
   set { 
     _calendarSize = value; 
   } 
 } 

 public bool WeekNumbers { 
   get { 
     return _weekNumbers; 
   } 
   set { 
     _weekNumbers = value; 
   } 
 } 

 public string LayoutPath { 
   get { 
     return GetLayoutPath(_calendarSize); 
   } 
 } 

 public bool ShowOthers { 
   get { 
     return _showOthers; 
   } 
   set { 
     _showOthers = value; 
   } 
 } 

 public string TimeFormat { 
   get { 
     return _timeFormat; 
   } 
   set { 
     _timeFormat = value; 
   } 
 } 

 public int DateStep { 
   get { 
     return _step; 
   } 
   set { 
     _step = value; 
   } 
 } 

 public string Range { 
   get { 
     return _range; 
   } 
   set { 
     _range = value; 
   } 
 } 

 public bool Electric { 
   get { 
     return _electric; 
   } 
   set { 
     _electric = value; 
   } 
 } 

 public string DaFormat { 
   get { 
     return _daFormat; 
   } 
   set { 
     _daFormat = value; 
   } 
 } 

 public DisplayModes DisplayMode { 
   get { 
     return _displayMode; 
   } 
   set { 
     _displayMode = value; 
   } 
 } 

 public string ExtraDateText { 
   get { 
     return _extraDateText; 
   } 
   set { 
     _extraDateText = value; 
   } 
 } 

 public string FlatCallback { 
   get { 
     return _flatCallback; 
   } 
   set { 
     _flatCallback = value; 
   } 
 } 

 public System.DateTime[] InitialDates { 
   get { 
     return _initialDates; 
   } 
   set { 
     _initialDates = value; 
   } 
 } 

 public string DisableFunc { 
   get { 
     return _disableFunc; 
   } 
   set { 
     _disableFunc = value; 
   } 
 } 

 public string OnUpdate { 
   get { 
     return _onUpdate; 
   } 
   set { 
     _onUpdate = value; 
   } 
 } 

	// Update. October 15, 2005
	public int numberMonths 
	{ 
		get 
		{ 
			return _numberMonths; 
		} 
		set 
		{ 
			_numberMonths = value; 
		} 
	} 
	public int monthsInRow 
	{ 
		get 
		{ 
			return _monthsInRow; 
		} 
		set 
		{ 
			_monthsInRow = value; 
		} 
	} 
	public int controlMonth 
	{ 
		get 
		{ 
			return _controlMonth; 
		} 
		set 
		{ 
			_controlMonth = value; 
		} 
	} 
	public int timeInterval 
	{ 
		get 
		{ 
			return _timeInterval; 
		} 
		set 
		{ 
			_timeInterval = value; 
		} 
	} 
	// End of update from October 15, 2005
}