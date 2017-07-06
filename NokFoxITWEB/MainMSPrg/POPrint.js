//預覽打印
function POPrint(POID, POCNT) {
    var url = "FPOQ01.aspx?poid=" + POID + "&pocnt=" + POCNT;
    window.open(url,"", 'status=no,resizable=yes,scrollbars=yes,titlebar=no,toolbar=yes,top=0,left=0');
    return false;
}
//直接打印不顯示頁面
  function POPrint2(POID, POCNT,targetName) { 

}