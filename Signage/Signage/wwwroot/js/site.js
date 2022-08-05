//reload page every 10 minute
$(document).ready(function () {
    StartScroll();

    setInterval(function () { reload_page(); }, 5 * 60000);

});

function reload_page() {
    window.location.reload(true);
}

function startTime() {
    var today = new Date();
    var hr = today.getHours();
    var min = today.getMinutes();
    var sec = today.getSeconds();
    ap = (hr < 12) ? "AM" : "PM";
    hr = (hr == 0) ? 12 : hr;
    hr = (hr > 12) ? hr - 12 : hr;
    //Add a zero in front of numbers<10
    hr = checkTime(hr);
    min = checkTime(min);
    sec = checkTime(sec);
    document.getElementById("clock").innerHTML = hr + ":" + min + ":" + sec + " " + ap;

    var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    var days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    var curWeekDay = days[today.getDay()];
    var curDay = today.getDate();
    var curMonth = months[today.getMonth()];
    var curYear = today.getFullYear();
    var date = curWeekDay + ", " + curMonth + " " + curDay + ", " + curYear;
    document.getElementById("date").innerHTML = date;

    var time = setTimeout(function () { startTime() }, 500);
}
function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}


function StartScroll() {
    var $el = $(".table-responsive");
    $el.each(function anim() {
        var st = $(this).scrollTop();
        var sb = $(this).prop("scrollHeight") - $(this).innerHeight();
        $(this).animate({ scrollTop: st < sb / 2 ? sb : 0 }, $(this).prop("scrollHeight")*20, anim);

    });
   
}

//async function pageScroll(objDiv) {
//    while (true) {
//        objDiv.scrollTop = objDiv.scrollTop + 1;
//        if ((objDiv.scrollTop + objDiv.clientHeight) == objDiv.scrollHeight) {
//            objDiv.scrollTop = 0;
//        }

//        await sleep(30);
//    }
//}

//function sleep(ms) {
//    return new Promise(resolve => setTimeout(resolve, ms));
//}
//StartScroll();


$("#slideshow > div:gt(0)").hide();

setInterval(function () {
    $('#slideshow > div:first')
        .fadeOut(1000)
        .next()
        .fadeIn(1000)
        .end()
        .appendTo('#slideshow');
}, 20000);