
    function calendar(t) {
        var sPath = "../Controls/calendar.aspx";
        strFeatures = "dialogWidth=206px;dialogHeight=235px;center=no;help=no;dialogLeft="+(window.event.screenX-20)+";dialogTop="+(window.event.screenY+10);
        st = t.value;
        sDate = showModalDialog(sPath,st,strFeatures);
        
        if(sDate!=null && sDate!="")
        {
            t.value = formatDate(sDate,0);
        }
    }

    function formatDate(sDate) {
        var sScrap = "";
        var dScrap = new Date(sDate);
        var sMon="";
        var sDay="";
        if (dScrap == "NaN") return sScrap;

        iDay = dScrap.getDate();
        iMon = dScrap.getMonth();
        iYea = dScrap.getFullYear();
        if(iMon<9)
        sMon="/0"+(iMon+1);
        else
        sMon="/"+(iMon+1);
        if(iDay<10)
        sDay="/0"+iDay;
        else
        sDay="/"+iDay;
        sScrap = iYea+sMon+sDay ;
        return sScrap;
    }
    //clear field content
    function clearContent(obj){
    obj.value="";
    } 