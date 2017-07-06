var is_ie = document.all ? true : false;
var is_ff = window.addEventListener ? true : false;

//得到控件的绝对位置
function getposition(id) {
	e = document.getElementById(id);
	var t = e.offsetTop;
	var l = e.offsetLeft;
	while (e = e.offsetParent) {
		t += e.offsetTop;
		l += e.offsetLeft;
	}
	var r = new Array();
	r['x'] = l;
	r['y'] = t;
	return r;
}

//debug
document.write('<div id="jsdebug"></div>');
function d(e) {
	s = '';
	for(k in e) {
		t = typeof e[k];
		s += t + ' : <b>' + k + ' :</b> ' + e[k] + '<br />';
	}
	document.getElementById('jsdebug').innerHTML = s;
}

/***********************************************************************************************************************/
var controlid;				//控件 日历数值显示， 绝对位置定位
var currdate 	= null;			//当前初始化时间	默认为本地时间
var startdate 	= null; 		//日期范围 - 开始日期
var enddate 	= null; 		//日期范围 - 截止日期
var yy 			= null; 	//年
var mm 			= null;		//月
var i;					//列
var j;					//行
var currday		= null; 	//今天
var today 		= new Date(); 	//当前时间
today.setHours(0);
today.setMinutes(0);
today.setSeconds(0);
today.setMilliseconds(0);

//	pasedate('2005-1-2') 返回date对象
function parsedate(s){
	if(s == ''){ return false;};
	var reg = new RegExp("[^0-9-]","")
	if(s.search(reg)>=0)
		return today;
	var ss = s.split("-");
	if(ss.length != 3)
		return today;
	if(isNaN(ss[0])||isNaN(ss[1])||isNaN(ss[2]))
		return today;
	return new Date(parseFloat(ss[0]),parseFloat(ss[1])-1,parseFloat(ss[2]));
}

function setdate(d){
	document.getElementById('calendardiv').style.display = 'none';
	controlid.value = yy + "-" + (mm+1) + "-" + d;
}

function myCancelBubble(event) {
	e = event ? event : window.event ;
	if(is_ff) {
		e.stopPropagation();
	} else if(is_ie) {
		e.cancelBubble = true;
	}
}

function initcalendar(){
	//当前时间
	s = '<style>';
	s += '#calendardiv{background-color:#FFFFFF;cursor:default}';
	s += '#calendardiv a{color:#333333;text-decoration:none;}';
	s += '#calendardiv table{border:1px solid #333333}';
	s += '.expire, .expire a{color:#ccc;}';
	s += '.default, .default a{color:#333333}';
	s += '.checked, .checked a{font-weight:bold;}';
	s += '.today{color:#ffcc00}';
	s += '</style>';
	s += '<div id="calendardiv" style="display:none;position:absolute;" onclick="myCancelBubble(event)">';
	s += '<iframe id="iframediv" scrolling="no" frameborder="0" style="position:absolute;z-index:-1;"></iframe>';
	s += '<table cellpadding="2" cellspacing="4">';
	s += '<tr><td colspan="7"><table width="100%" style="border:0px" align="center"><tr><td id="prev" align="center"><a href="javascript:drawcalendar(yy-1,mm);" title="上一年"><img src="../Jscript/images/first.gif" border="0" width="9" height="8" /></a>&nbsp; &nbsp<a href="javascript:drawcalendar(yy,mm-1);" title="上个月"><img src="../Jscript/images/prev.gif" border="0" width="8" height="8" /></a></td><td colspan="5" id="yyyymm" align="center"></td><td id="next" align="center"><a href="javascript:drawcalendar(yy,mm+1);" title="下个月"><img src="../Jscript/images/next.gif" border="0" width="8" height="8" /></a>&nbsp &nbsp;<a href="javascript:drawcalendar(yy+1,mm);" title="下一年"><img src="../Jscript/images/last.gif" border="0" width="9" height="8" /></a></td></tr></table></td></tr>';
	//s += '<tr><td id="prev"> </td><td colspan="5" id="yyyymm" align="center"></td></tr>';
	s += '<tr><td>日</td><td>一</td><td>二</td><td>三</td><td>四</td><td>五</td><td>六</td></tr>';
	for(i=0; i <6; i++){
		s += "<tr>";
		for(j=1; j<=7; j++)
			s += "<td id=d"+(i*7+j)+" height=\"19\" onmouseover=this.oldcolor=this.style.backgroundColor;this.style.backgroundColor='#eef3f7' onmouseout=this.style.backgroundColor=this.oldcolor>0</td>";
		s += "</tr>";
	}
	s += '</table>';
	s += '</div>';
	document.write(s);
	currday 	= currdate ? currdate : today;// 默认为本地时间
	//点击隐藏
	document.onclick = function() {
		document.getElementById('calendardiv').style.display = 'none';
	}
}

function showcalendar(event, controlid1){
	// 判断controlid position
	
	controlid   = document.getElementById(controlid1);
	startdate   = parsedate('2000-01-01');
	enddate     = currday;
	defday		= parsedate(controlid.value);
	if(!defday)
		defday=currday;
	var p   = getposition(controlid1);
	document.getElementById('calendardiv').style.display = 'block';
	document.getElementById('calendardiv').style.left = p['x'];
	document.getElementById('calendardiv').style.top  = p['y'] + 20;
	
	myCancelBubble(event);

	drawcalendar(defday.getFullYear(),defday.getMonth());
}

// 刷新日历
function drawcalendar(y, m){
	var x  = new Date(y, m, 1);
	var mv = x.getDay();
	var d = x.getDate();
	var de = null;					// 单元格对象
	yy 	   = x.getFullYear();
	mm 	   = x.getMonth();
	document.getElementById("yyyymm").innerHTML = yy + "." + (mm+1 > 9  ? mm+1 : "0" + (mm+1));
	//将1号以前的单元设置为空
	for(var i=1; i<=mv; i++){
		de = document.getElementById("d"+i);
		de.innerHTML= "";
		de.className= "";
	}

	//开始画当月日历
	while(x.getMonth() == mm){
		de = document.getElementById("d"+(d+mv));
		if((enddate && x.getTime() > enddate.getTime()) || (startdate && x.getTime() < startdate.getTime())) {
			de.className = 'expire';
			de.innerHTML = d;
		}else{
			de.className = 'default';
			de.innerHTML = "<a href=javascript:setdate("+d+");>"+d+"</a>";
		}
		if(x.getTime() == currday.getTime()) {
			de.className = 'checked';
		}
		if(x.getTime() == today.getTime()) {
			de.className = 'today';
		}
		x.setDate(++d);
	}
	// 尾部空格
	while(d + mv <= 42){
		de = document.getElementById("d"+(d+mv));
		de.innerHTML = "";
		de.bgColor = "";
		de.className = "";
		d++;
	}
}

initcalendar();

Date.prototype.Format = function(fmt){ 
	var o = { //new Date().Format('yyyy-MM-dd')
		"M+" : this.getMonth()+1,                 
		"d+" : this.getDate(),                    
		"h+" : this.getHours(),                   
		"m+" : this.getMinutes(),                 
		"s+" : this.getSeconds(),                
		"q+" : Math.floor((this.getMonth()+3)/3), 
		"S" : this.getMilliseconds()             
	}; 
	if(/(y+)/.test(fmt)) 
		fmt=fmt.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length)); 
	for(var k in o) 
		if(new RegExp("("+ k +")").test(fmt)) 
	fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length))); 
	return fmt; 
}

/*  
 * 根據傳入日期，要處理的天數，進行加/減計算。  
 * 日期格式：XXXX年XX月XX日  
 */  
function addByTransDate(dateParameter, num) {   
  
    var translateDate = "", dateString = "", monthString = "", dayString = "";   
    translateDate = dateParameter.replace("-", "/").replace("-", "/");;   
  
    var newDate = new Date(translateDate);   
    newDate = newDate.valueOf();   
    newDate = newDate + num * 24 * 60 * 60 * 1000;   
    newDate = new Date(newDate);   
  
    //如果月份長度少於2，則前加 0 補位   
    if ((newDate.getMonth() + 1).toString().length == 1) {   
  
        monthString = 0 + "" + (newDate.getMonth() + 1).toString();   
        alert(translateDate);   
    } else {   
  
        monthString = (newDate.getMonth() + 1).toString();   
        alert(translateDate);   
    }   
  
    //如果天數長度少於2，則前加 0 補位   
    if (newDate.getDate().toString().length == 1) {   
  
        dayString = 0 + "" + newDate.getDate().toString();   
    } else {   
  
        dayString = newDate.getDate().toString();   
    }   
  
    dateString = newDate.getFullYear() + "-" + monthString + "-" + dayString;   
    return dateString;   
}   
  
function reduceByTransDate(dateParameter, num) {   
  
    var translateDate = "", dateString = "", monthString = "", dayString = "";   
    translateDate = dateParameter.replace("-", "/").replace("-", "/");;   
  
    var newDate = new Date(translateDate);   
    newDate = newDate.valueOf();   
    newDate = newDate - num * 24 * 60 * 60 * 1000;   
    newDate = new Date(newDate);   
  
    //如果月份長度少於2，則前加 0 補位   
    if ((newDate.getMonth() + 1).toString().length == 1) {   
  
        monthString = 0 + "" + (newDate.getMonth() + 1).toString();   
        alert(translateDate);   
    } else {   
  
        monthString = (newDate.getMonth() + 1).toString();   
        alert(translateDate);   
    }   
  
    //如果天數長度少於2，則前加 0 補位   
    if (newDate.getDate().toString().length == 1) {   
  
        dayString = 0 + "" + newDate.getDate().toString();   
    } else {   
  
        dayString = newDate.getDate().toString();   
    }   
  
    dateString = newDate.getFullYear() + "-" + monthString + "-" + dayString;   
    return dateString;   
}  

