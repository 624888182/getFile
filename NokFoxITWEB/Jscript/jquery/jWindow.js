/*
jWindow.open(url, options);

url :required, the page you want to open
options : {height:100, width:100, scrolling:"auto"}
    height : required (Number)
    width : required (Number)
    scrolling : optional (String, default is "auto")
    
    
for example :
    jWindow.open("xx.html", {height:300, width:500, scrolling:"yes"})
    
close the window in child page:
    window.parent.jWindow.close();
*/

window.jWindow = function(){
    var getMasker = function(){
        var mask = document.createElement("div");
        mask.setAttribute("id", "jWin-masker");
        if (window.event) {
            mask.style.filter = 'alpha(opacity=70)';
        } else {
            mask.style.opacity = 0.7;
        }
        return mask;
    };
    var getiframe = function(){
        var iframe = document.createElement("iframe");
        iframe.setAttribute("id", "jWin-Masker-iframe");
        iframe.setAttribute("frameborder", 0);
        if (window.event) {
            iframe.style.filter = 'alpha(opacity=0)';
        } else {
            iframe.style.opacity = 0;
        }
        return iframe;
    };
    var getWindow = function(){
        var jWin = document.createElement("div");
        jWin.setAttribute("id", "jWin-window");
        
        var header = document.createElement("div");
        header.setAttribute("id", "jWin-Window-header");
        
        var closebtn = document.createElement("div");
        closebtn.setAttribute("id", "jWin-Window-close");
        
        var title = document.createElement("span");
        title.setAttribute("id", "jWin-Window-title");
        
        header.appendChild(closebtn);
        header.appendChild(title);
        
        jWin.appendChild(header);
        
        var body = document.createElement("div");
        body.setAttribute("id", "jWin-Window-mainbody");
        
        var mainiframe = document.createElement("iframe");
        mainiframe.setAttribute("id", "jWin-Window-iframe");
        mainiframe.setAttribute("frameborder", 0);
        
        body.appendChild(mainiframe);
        
        jWin.appendChild(body);

        return jWin;
    };
    
    var setWindowStyle = function(){
        var dde = document.documentElement;
        
        var height = window.jWindow.Height;
        var width = window.jWindow.Width;
        var top = (dde.clientHeight - height) / 2 + dde.scrollTop;
        var left = (dde.clientWidth - width) / 2 + dde.scrollLeft;
        
        top = top < 0 ? 0 : top;
        left = left < 0 ? 0 : left;
        
        var win = document.getElementById("jWin-window");
        win.style.top = top + "px";
        win.style.left = left + "px";
        
        var docHeight = dde.scrollHeight;
        var docWidth = dde.scrollWidth;
        if (window.event) {
            var clientHeight = dde.clientHeight;
            if(docHeight < clientHeight) {
                docHeight = clientHeight;
            }
        }
        
        var masker = document.getElementById("jWin-masker");
        var frameMasker = document.getElementById("jWin-Masker-iframe");
        
        masker.style.height = docHeight + "px";
        masker.style.width = docWidth + "px";
        frameMasker.style.height = docHeight + "px";
        frameMasker.style.width = docWidth + "px";
    };
    //拖拽
    var drag = function(evt){
        if (window.jWindow.Draggable) {
            var win = document.getElementById("jWin-window");
            evt = evt || window.event;
            
            win.style.top = window.jWindow.Top + evt.clientY - window.jWindow.OffsetY + "px";
            win.style.left = window.jWindow.Left + evt.clientX - window.jWindow.OffsetX + "px";
        }
    };
    
    return {
        Masker : null,//底部用于遮擋的div
        FrameMasker : null,//底部用于遮擋的iframe，主要是為了擋住IE中的下拉框
        Win : null,//用于顯示的主窗口
        Height : 0,//窗口的高度
        Width : 0,//窗口的寬度
        OffsetX : 0,//以下五個變量用于拖拽功能
        OffsetY : 0,
        Top : 0,
        Left : 0,
        Draggable : false,
        open : function(url, options){
            this.Masker = getMasker();
            this.FrameMasker = getiframe();
            this.Win = getWindow();
            this.Height = options.height;
            this.Width = options.width;
            
            document.body.appendChild(this.Masker);
            document.body.appendChild(this.FrameMasker);
            document.body.appendChild(this.Win);
            
            document.getElementById("jWin-Window-title").innerHTML = options.title || "&nbsp;";
            
            //關閉按鈕事件
            var closeButton = document.getElementById("jWin-Window-close");
            closeButton.onclick = function(){
                window.jWindow.close();
            };
            closeButton.onmouseover = function(){
                this.style.backgroundPosition = "-15px 0px";
            };
            closeButton.onmouseout = function(){
                this.style.backgroundPosition = "0px 0px";
            };
            
            //窗口拖拽
            var win = document.getElementById("jWin-window");
            win.onmousedown = function(evt){
                if (this.addEventListener) {
                    this.addEventListener("mousemove", drag, false)
                } else {
                    this.attachEvent("onmousemove", drag);
                }
                evt = evt || window.event;
                window.jWindow.OffsetX = evt.clientX;
                window.jWindow.OffsetY = evt.clientY;
                window.jWindow.Top = parseFloat(this.style.top);
                window.jWindow.Left = parseFloat(this.style.left);
                window.jWindow.Draggable = true;
            };
            win.onmouseup = function(evt){
                if (this.removeEventListener) {
                    this.removeEventListener("mousemove", drag, false)
                } else {
                    this.detachEvent("onmousemove", drag);
                }
                window.jWindow.Draggable = false;
            };
            win.onmouseout = function(evt){
                window.jWindow.Draggable = false;
            };
            
            //瀏覽器resize事件
            if (window.addEventListener) {
                window.addEventListener("resize", setWindowStyle, false);
            } else {
                window.attachEvent("onresize", setWindowStyle);
            }
            
            //設定彈出窗口的高度和寬度
            document.getElementById("jWin-window").style.width = options.width + "px";
            var mainFrame = document.getElementById("jWin-Window-iframe");
            mainFrame.setAttribute("src", url);
            mainFrame.setAttribute("height", options.height);
            mainFrame.setAttribute("width", options.width);
            mainFrame.setAttribute("scrolling", options.scrolling || "auto");
            
            setWindowStyle();
        },
        close : function(){
            this.Masker.parentNode.removeChild(this.Masker);
            this.FrameMasker.parentNode.removeChild(this.FrameMasker);
            this.Win.parentNode.removeChild(this.Win);
            this.Masker = null;
            this.FrameMasker = null;
            this.Win = null;
            
            if (window.removeEventListener) {
                window.removeEventListener("resize", setWindowStyle, false);
            } else {
                window.detachEvent("onresize", setWindowStyle);
            }
        }
    };
}(); 