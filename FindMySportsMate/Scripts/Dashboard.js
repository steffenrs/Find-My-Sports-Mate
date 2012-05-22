$(function () {
    $("#suggestions-tabs").tabs();
    $("#suggestions-join").button();
});

function showMap(Lat,Lon) {
    var myLatLong = new google.maps.LatLng(Lat,Lon);
    var myOptions = {
        center: myLatLong,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("suggestions-right-div"), myOptions);
    var marker = new google.maps.Marker({
        position: myLatLong,
        map: map
    });  
};

function getSuggestion(id) {
    var suggestionDetails = $("#suggestions-content");
    $.ajax({
        url: suggestionDetails.data('update-url')+"/"+id,
        type: "POST",
        success: updateSuggestionDetails
    });
};

function updateSuggestionDetails(data) {
    $("#suggestions-content").html(data);
}

function joinSuggestion(id) {
    openDialog();

    var suggestionDetails = $("#suggestions-content");
}

function closeSuggestion(id) {
    var suggestionDetails = $("#tabs-3");
    $.ajax({
        url: suggestionDetails.data('close-suggestion-url') + "/" + id,
        type: "POST",
        success: closedSuccess(id)
    });
}



function openSuggestion(id) {
    var suggestionDetails = $("#tabs-3");
    $.ajax({
        url: suggestionDetails.data('open-suggestion-url') + "/" + id,
        type: "POST",
        success: openSuccess(id)
    });
}

function openSuccess(id) {
    var status = $("#suggestion-status-" + id);

    var html = "Open <a id='suggestion-lock-"+ id +"' href='#' onclick='closeSuggestion("+ id +")'>";

    var suggestionDetails = $("#tabs-3");
    var src = suggestionDetails.data("close-suggestion-img");
    html += "<img class='suggestions-table-icon' alt='Close suggestion' src='" + src + "' />";
    status.html(html);
}

function closedSuccess(id) {
    var status = $("#suggestion-status-" + id);

    var html = "Closed <a id='suggestion-lock-" + id + "' href='#' onclick='openSuggestion(" + id + ")'>";

    var suggestionDetails = $("#tabs-3");
    var src = suggestionDetails.data("open-suggestion-img");
    html += "<img class='suggestions-table-icon' alt='Open suggestion' src='" + src + "' />";
    status.html(html);
}


function createJoinDialog() {
    var elem = $("#join-suggestion").dialog({
        autoOpen: false,
        height: 300,
        width: 300,
        modal: true,
        resizable: false,
        buttons: {
            "Select": function () { $(this).dialog("close"); selectFunction(); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });

    return elem;
}

function openDialog() {
    $("#join-suggestion").dialog("open");
    return false;
}

function selectFunction() {
    var fields = $("#weekdays");
    fields.clone().appendTo("#suggestions-join-form");

    $("#suggestions-join-form").submit();
}