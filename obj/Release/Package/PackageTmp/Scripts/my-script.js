function Init() {
    var inputs = document.querySelectorAll("textarea");
    for (var i = 0; i < inputs.length; i++) {
        var elem = inputs[i];
        elem.onkeydown = function (e) {
            this.style.height = ((this.value.match(/\n/g) || ["test"]).length * 24 + 30) + "px";
        }
    }
}
function GetArgument(typeA,callback) {
    $.ajax({
        type: "GET",
        url: "/argument",
        data: { type: typeA },
        success: function (data) {
            if (callback && callback instanceof Function)
                callback(data)
            
        },
        error: function (err) {
            console.error(err);
        }
    })
}
function Validate(argList,obj) {
    var resp = [];
    var l = argList.filter(function (elem) {
        return elem.type == "Lit";
    })
    if (l.length < 5) {
        resp.push(" Brakujące argumenty literackie - brakuje " + (5 - l.length));
    }
    var f = argList.filter(function (elem) {
        return elem.type == "Fil";
    })
    if (f.length < 2) {
        resp.push(" Brakujące argumenty filmowe - brakuje " + (2 - l.length));
    }
    var o = argList.filter(function (elem) {
        return elem.type == "Oth";
    })
    if (o.length <= 0) {
        resp.push(" Brakujące argumenty pozostałe - brakuje jakiegokolwiek");
    }
    for (var i = 0; i < argList.length; i++) {
        var elem = argList[i];
        if (elem.content == "") {
            resp.push("Argument " + (i + 1) + " - Brakuje treści");
        }
        if (elem.source == "") {
            resp.push("Argument " + (i + 1) + " - Brakuje źródła");
        }
    }
    /*
    Question:
    Side: 
    Autor_Id:
    LibraryResource*/
    if (obj.Question == "") {
        resp.push(" Brakuje tematu rozprawki");
    }
    if (obj.Side == "") {
        resp.push(" Brakuje tezy rozprawki");
    }
    if (obj.Autor_Id == "") {
        resp.push("Błąd - nie wybrany autor");
    }
    if (obj.LibraryResource == "") {
        resp.push(" Brakuje tytułu książki lub materiału wypożyczonego z biblioteki szkolnej");
    }
    if (resp.length > 0) {
        return { comp: false, res: resp };
    }
    else {
        return { comp: true }
    }
}
function StringArgument(listOfA) {
    var retObj = {
        ConString: "",
        SouString: "",
        TypeString: ""
    }
    for (var i = 0; i < listOfA.length; i++)
    {
        var elem = listOfA[i];
        elem.content.replace("|", "");
        retObj.ConString += elem.content + (i != listOfA.length-1 ? "|" : "");
        retObj.SouString += elem.source + (i != listOfA.length - 1 ? "|" : "");
        retObj.TypeString += elem.type + (i != listOfA.length - 1 ? "|" : "");
    }
    return retObj;
}