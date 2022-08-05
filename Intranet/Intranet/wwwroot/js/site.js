// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AllDayChecked() {
    var box = document.getElementById("checkBox");
    var div = document.getElementById("time");
    if (box.checked) {
        div.hidden = true;
    } else {
        div.hidden = false;
    }
}

function validateForm() {
    var box = document.getElementById("checkBox");
    var timeStart = document.forms["Event"]["startTime"].value
    var timeEnd = document.forms["Event"]["endTime"].value
    console.log(timeStart + " " + timeEnd)

    var dayStart = document.forms["Event"]["startDate"].value
    var dayEnd = document.forms["Event"]["endDate"].value
    if (box.checked) {
        return checkDate(dayStart, dayEnd);


    } else {
        if (checkDate(dayStart, dayEnd)) {

            if (timeEnd == "" || timeStart == "") {
                alert("Please Fill in Approriate Time!");
                return false;
            } else {
                if (dayStart == dayEnd) {
                    return checkTime(dayStart, timeStart, timeEnd);
                }
            }
        }
        else {
            return false;
        }

    }
}

function checkDate(start, end) {
    var startDate = Date.parse(start);
    var endDate = Date.parse(end);
    if (endDate.valueOf() < startDate.valueOf()) {

        alert("End Date must be after Start Date");
        return false;
    }
    else return true;
}

function checkTime(day, start, end) {

    var startTime = new Date(day + " " + start);
    var endTime = new Date(day + " " + end);
    if (startTime.valueOf() > endTime.valueOf()) {
        alert("End Time must be after Start Time");
        return false;
    } else return true;


}
