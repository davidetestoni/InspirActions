document.onkeydown = checkKey;

function checkKey(e) {
    e = e || window.event;
    if (e.keyCode == '38') {
        // up arrow
    }
    else if (e.keyCode == '40') {
        // down arrow
    }
    else if (e.keyCode == '37') {
        // left arrow
        sessionRef.invokeMethodAsync('PreviousTask');
    }
    else if (e.keyCode == '39') {
        // right arrow
        sessionRef.invokeMethodAsync('NextTask');
    }
}

function registerSession(sessionRef) {
    this.sessionRef = sessionRef;
}