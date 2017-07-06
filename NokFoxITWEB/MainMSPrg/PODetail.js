function onkeyupReg(obj) {
    var total = 0;

    var tv = ChangeEmpty(document.getElementById("Label70").innerText);

    var v1 = ChangeEmpty(document.getElementById("TextBox2").value);
    var v2 = ChangeEmpty(document.getElementById("TextBox4").value);
    var v3 = ChangeEmpty(document.getElementById("TextBox6").value);
    var v4 = ChangeEmpty(document.getElementById("TextBox8").value);
    var v5 = ChangeEmpty(document.getElementById("TextBox10").value);
    var v6 = ChangeEmpty(document.getElementById("TextBox12").value);
    var v7 = ChangeEmpty(document.getElementById("TextBox14").value);
    var v8 = ChangeEmpty(document.getElementById("TextBox16").value);

    total += parseInt(v1);
    total += parseInt(v2);
    total += parseInt(v3);
    total += parseInt(v4);
    total += parseInt(v5);
    total += parseInt(v6);
    total += parseInt(v7);
    total += parseInt(v8);

    if (total != "NaN") {
        if (total > parseInt(tv)) {
            obj.value = "";
        }
    }

    if (obj.value.length == 1) {
        obj.value = obj.value.replace(/[^1-9]/g, '')
    }
    else {
        obj.value = obj.value.replace(/\D/g, '')
    }

    if (obj.value != "") {
        if (obj.id.toString() != "TextBox16") {
            switch (obj.id) {
                case "TextBox2":
                    document.getElementById("TextBox4").disabled = "";
                    break;
                case "TextBox4":
                    document.getElementById("TextBox6").disabled = "";
                    break;
                case "TextBox6":
                    document.getElementById("TextBox8").disabled = "";
                    break;
                case "TextBox8":
                    document.getElementById("TextBox10").disabled = "";
                    break;
                case "TextBox10":
                    document.getElementById("TextBox12").disabled = "";
                    break;
                case "TextBox12":
                    document.getElementById("TextBox14").disabled = "";
                    break;
                case "TextBox14":
                    document.getElementById("TextBox16").disabled = "";
                    break;
            }
        }

    } else {
        //                document.getElementById("TextBox2").disabled = true;
        //                document.getElementById("TextBox4").disabled = true;
        //                document.getElementById("TextBox6").disabled = true;
        //                document.getElementById("TextBox8").disabled = true;
        //                document.getElementById("TextBox10").disabled = true;
        //                document.getElementById("TextBox12").disabled = true;
        //                document.getElementById("TextBox14").disabled = true;
        //                document.getElementById("TextBox16").disabled = true;
        //               
        //                obj.disabled = "";
    }
}
function ChangeEmpty(v) {
    if (v == "") {
        return 0;
    } else {
        return v;
    }
}
function onafterpasteReg(obj) {

    if (obj.value.length == 1) {
        obj.value = obj.value.replace(/[^1-9]/g, '')
    }
    else {
        obj.value = obj.value.replace(/\D/g, '')
    }
}
        