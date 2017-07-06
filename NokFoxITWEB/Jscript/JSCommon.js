//Delete the space
function trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

function PageBack(pagefile) {
    var sHref = window.location.href.split("?");
    var sBackHref = pagefile + "?" + sHref[1];
    window.location.href = sBackHref;
}

//判斷值是否為數字
function FilterNaNCheck(txtObject, isCheck) {
    var txtValue = txtObject.value;
    if (isNaN(txtValue)) {
        alert("數據不合法,敬請重新輸入!");
        txtObject.value = "";
        txtObject.focus();
    }
    else if (txtValue <= 0 && isCheck) {
        alert("數據必須大於0，敬請重新輸入!");
        txtObject.value = "";
    }
    else if (txtValue < 0) {
        alert("數據不能為負數，敬請重新輸入!");
        txtObject.value = "0";
    }
}

//判斷值是否為數字
function FilterNaNCheckValue(txtObject, isCheck, compareValue) {
    var txtValue = txtObject.value;
    if (isNaN(txtValue)) {
        alert("數據不合法,敬請重新輸入!");
        txtObject.value = "";
        txtObject.focus();
    }
    else if (txtValue <= 0 && isCheck) {
        alert("數據必須大於0，敬請重新輸入!");
        txtObject.value = "";
    }
    else if (txtValue < 0) {
        alert("數據不能為負數，敬請重新輸入!");
        txtObject.value = "0";
    }
    else if (txtValue > compareValue) {
        alert("數據輸入不合法，不能大於" + compareValue + "!!");
        txtObject.value = "0";
    }
}


function checkLength(txtObj, intLength, txtMsg) {
    if (document.getElementById(txtObj).value.length < intLength) {
        alert(txtMsg + "的長度必須大於" + intLength + "位!");
        return false;
    }
    else {
        return true;
    }
}

function checkLengthValue(objTxt, intLength, txtMsg) {
    if (objTxt.value.length > intLength) {
        alert(txtMsg);
        objTxt.value = objTxt.value.substring(0, intLength);
    }
}

function checkData(startDateId, endDateID, IsValid) {
    var startDate = document.getElementById(startDateId).value;
    var endDate = document.getElementById(endDateID).value;
    var now = new Date();
    var currentYear = now.getFullYear() + "-";
    var currentMonth = now.getMonth() + 1;
    var currentDay = now.getDate();
    var currentDate = currentYear;
    currentDate += currentMonth < 10 ? "0" + currentMonth : currentMonth;
    currentDate += currentDay < 10 ? "-0" + currentDay : "-" + currentDay;
    if (endDate < startDate) {
        alert("對不起，起止日期輸入有誤，敬請確認!");
        return false;
    }
    else {
        if (IsValid && endDate < currentDate) {
            alert("對不起，結束日期不能小於當前日期!");
            return false;
        }
        if (IsValid && startDate < currentDate) {
            alert("對不起,開始日期不能小於當前日期!");
            return false;
        }
        return true;
    }

}

///過濾，只輸入數字
function FilterNonNumber() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
    }
}

///過濾，只輸入數字
function FilterNonNumberExlPlus() {
    var obj = event.srcElement;
    var val = obj.value;
    if (val.match(/^-?\d*$/))
        return true;
    else return false;
}

//過濾，只輸入小數
function FilterNonDecimal() {
    if (event.keyCode == 46 || (event.keyCode >= 48 && event.keyCode <= 57)) {
        //event.returnValue = true;
    }
    else {
        event.returnValue = false;
    }
}

//過濾，只輸入小數
function FilterNonDecimalValue(objTxt, compareValue) {
    if (event.keyCode == 46 || (event.keyCode >= 48 && event.keyCode <= 57)) {
        //event.returnValue = true;
    }
    else {
        event.returnValue = false;
    }
    if (objTxt.value > compareValue) {
        alert("數據輸入不合法，不能大於" + compareValue + "!!!");
        objTxt.value = "";
    }
}


//屏蔽中文輸入,使用onkeyup或onkeydown
function RemoveSpecialChar() {
    var e = window.event.srcElement;
    var iptData = document.getElementById(e.id);
    if (iptData.value.match(/[^\x00-\xff]/ig)) {
        iptData.value = iptData.value.replace(/[^\x00-\xff]/ig, '');
    }
}

//過濾，輸入字母和數字
function FilterNonNumberAndLetter() {
    if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) {
        //event.returnValue = true;
    }
    else {
        event.returnValue = false;
    }
}

//屏蔽粘貼時的非數字字符
function FileterNonNumberOnPaste() {
    return !clipboardData.getData('text').match(/\D/);
}


//屏蔽鍵入的任何返回值
function ShieldKeyDown() {
    event.returnValue = false;
}

//驗證GridView是否存在被選中的行
//用法:var flag=GridViewHasChecked('gvDept',0)
function GridViewHasChecked(gridViewId, checkBoxColumn) {
    var GridView2 = document.getElementById(gridViewId);
    if (!GridView2) {
        alert('列表為空，請先檢索！');
        return false;
    }
    for (i = 1; i < GridView2.rows.length; i++) {
        if (GridView2.rows[i].cells[checkBoxColumn].getElementsByTagName("INPUT")[0].checked) {
            return true;
        }
    }
    alert('您未選擇任何數據！');
    return false;
}

//GridView CheckAll,
//用法:被點擊的CheckBox的onclick事件,onclick="CheckAll('gvDept',0,this)"
function CheckAll(gridViewId, checkBoxColumn, oCheckbox) {
    var GridView2 = document.getElementById(gridViewId);
    for (i = 1; i < GridView2.rows.length; i++) {
        GridView2.rows[i].cells[checkBoxColumn].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
    }
}

//驗證若干TextBox是否為空
//用法:var textBoxIds = new Array("txtUserCode", "txtPwd");
//      var flag=TextBoxIsEmpty(textBoxIds);
function TextBoxIsEmpty(textBoxIds) {
    for (var i = 0; i < textBoxIds.length; i++) {
        var textBox = document.getElementById(textBoxIds[i]);
        if (typeof (textBox) == "object" && trim(textBox.value) == "") {
            return true;
        }
    }
    return false;
}

//驗證若干DropDownList是否有選
//用法:var listIds = new Array("ddlYear", "txtMonth");
//      var flag=DropDownExistsEmpty(listIds);
function DropDownExistsEmpty(listIds) {
    for (var i = 0; i < listIds.length; i++) {
        var list = document.getElementById(listIds[i]);
        if (typeof (list) == "object" && (list.options.length == 0 || list.options.value.length == 0)) {
            return true;
        }
    }
    return false;
}

//將鍵入的Enter鍵轉換為Tab鍵輸出
function EnterToTab() {
    if (event.keyCode == 13) {
        event.keyCode = 9;
        event.returnValue = true;
    }
}

function CloseWindow(isConfirm) {
    if (isConfirm) {
        if (confirm("你確定要關閉當前窗口嗎?")) {
            window.opener = null;
            window.open('', '_self');
            window.close();
        }
    }
    else {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
}

function clearBlank(pTxtId) {
    var txts = pTxtId.value;
    var aTxts = txts.split("\r\n");
    for (var i = 0; i < aTxts.length; i++) {
        aTxts[i] = rtrim(aTxts[i]);
    }
    pTxtId.value = aTxts.join("\r\n");
}
