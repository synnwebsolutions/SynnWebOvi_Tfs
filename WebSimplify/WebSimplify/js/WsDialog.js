function hideModalPanel(panelId) {
    document.getElementById(panelId).style.display = "none";
    document.getElementById(panelId + "_modal").style.display = "none";
    enableTabControlsForModal(panelId);
    showSelectBoxForModal(panelId);
}

function showModalPanel(panelId) {
    document.getElementById(panelId).style.display = "";
    document.getElementById(panelId + "_modal").style.display = "";
    disableTabControlsForModal(panelId);
    hideSelectBoxesForModal(panelId);
}

var arModalPanelTabbleElements = new Array("A", "BUTTON", "TEXTAREA", "INPUT", "IFRAME", "SELECT");
var arModalPaenlOldTabIndexValues = new Array();

function enableTabControlsForModal(panelId) {
    return;
    for (var j = 0; j < arModalPanelTabbleElements.length; j++) {
        var tagElements = document.getElementsByTagName(arModalPanelTabbleElements[j]);
        for (var k = 0 ; k < tagElements.length; k++)
            if (!isSonOf(tagElements[k], panelId)) {
                if (parseInt(tagElements[k].tabIndex) == -200) {
                    tagElements[k].tabIndex = arModalPaenlOldTabIndexValues[k];
                }
                else
                    if (parseInt(tagElements[k].tabIndex) < -200)
                        tagElements[k].tabIndex = parseInt(tagElements[k].tabIndex) + 1;
            }
    }
}

function disableTabControlsForModal(panelId) {
    for (var j = 0; j < arModalPanelTabbleElements.length; j++) {
        var tagElements = document.getElementsByTagName(arModalPanelTabbleElements[j]);
        var len = tagElements.length
        var panel = document.getElementById(panelId);
        for (var k = 0 ; k < len; k++)
            if (!isSonOf(tagElements[k], panel)) {
                if (parseInt(tagElements[k].tabIndex) <= -200) {
                    tagElements[k].tabIndex = parseInt(tagElements[k].tabIndex) - 1;
                }
                else {
                    arModalPaenlOldTabIndexValues[k] = tagElements[k].tabIndex;
                    tagElements[k].tabIndex = "-200";
                }
            }
    }
}

function isSonOf(obj, container) {
    if (jQuery) {
        if (jQuery.contains(container, obj))
            return true;
        return false;
    }
    var parent = obj.parentNode;
    while (parent) {
        if (parent == container)
            return true;
        parent = parent.parentNode;
    }
    return false;
}

var ModalSelectBoxList = "";

function showSelectBoxForModal(panelId) {
    var brsVersion = parseInt(window.navigator.appVersion.charAt(0), 10);

    if (brsVersion == 4 && window.navigator.userAgent.indexOf("MSIE 6") > -1) {
        for (var i = 0; i < document.forms.length; i++) {
            for (var e = 0; e < document.forms[i].length; e++) {
                if (document.forms[i].elements[e].tagName == "SELECT") {
                    if (!isSonOf(document.forms[i].elements[e], panelId)) {
                        if (ModalSelectBoxList.indexOf(panelId + "-" + document.forms[i].elements[e].id + ";") > -1 && parseInt(document.forms[i].elements[e].tabIndex) > -200)
                            document.forms[i].elements[e].style.visibility = "";
                    }
                }
            }
        }
    }
}

function hideSelectBoxesForModal(panelId) {
    var brsVersion = parseInt(window.navigator.appVersion.charAt(0), 10);

    if (brsVersion == 4 && window.navigator.userAgent.indexOf("MSIE 6") > -1) {
        for (var i = 0; i < document.forms.length; i++) {
            for (var e = 0; e < document.forms[i].length; e++) {
                if (document.forms[i].elements[e].tagName == "SELECT") {
                    if (!isSonOf(document.forms[i].elements[e], panelId)) {
                        document.forms[i].elements[e].style.visibility = "hidden";
                        ModalSelectBoxList += panelId + "-" + document.forms[i].elements[e].id + ";";
                    }
                }
            }
        }
    }
}

var ieForDrag = document.all
var nsForDrag = document.getElementById && !ieForDrag

function getStyleClass(className) {
    if (document.all) {
        for (var s = 0; s < document.styleSheets.length; s++)
            for (var r = 0; r < document.styleSheets[s].rules.length; r++)
                if (document.styleSheets[s].rules[r].selectorText == '.' + className)
                    return document.styleSheets[s].rules[r];
    }
    else
        if (document.getElementById) {
            for (var s = 0; s < document.styleSheets.length; s++)
                for (var r = 0; r < document.styleSheets[s].cssRules.length; r++)
                    if (document.styleSheets[s].cssRules[r].selectorText.indexOf('.' + className) == 0)
                        return document.styleSheets[s].cssRules[r];
        }
    return null;
}

function canBeDragged(obj) {
    if (obj.style.cursor == "move")
        return true;
    var classname;
    if (obj.className != "")
        classname = obj.className;
    else
        classname = obj.parentNode.className;
    if (classname != "") {
        if (getStyleClass(classname).style.cursor == "move")
            return true;
    }
    return false;
}

function DragDivByClassName(e) {
    var fobj = nsForDrag ? e.target : event.srcElement;
    try {
        if (canBeDragged(fobj)) {
            var containerObject = fobj;
            while (containerObject.style.position != "absolute") {
                if (containerObject.parentNode != null)
                    containerObject = containerObject.parentNode;
                else
                    return;
            }
            var ev = e || event;
            var offsetx = ev.clientX - containerObject.offsetLeft;
            var offsety = ev.clientY - containerObject.offsetTop;
            document.onmousemove = function (mousemovee) {
                var mousemoveevent = mousemovee || event;
                if (containerObject.style.left == "")
                    containerObject.style.right = (document.documentElement.clientWidth - containerObject.clientWidth - (mousemoveevent.clientX - offsetx)) + "px";
                else
                    containerObject.style.left = (mousemoveevent.clientX - offsetx) + "px";
                containerObject.style.top = (mousemoveevent.clientY - offsety) + "px";
                containerObject.style.left + ",top:" + containerObject.style.top + "<br>" +
                mousemoveevent.clientY + "-" + offsety + "<br>target:" + mousemoveevent.target;
                return false;
            }
            fobj.onmouseup = function () {
                document.onmousemove = null;
            }
        }
    }
    catch (err) {

    }
}

document.onmousedown = DragDivByClassName;

