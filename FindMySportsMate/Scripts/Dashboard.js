$(function () {
    $("#suggestions-tabs").tabs();
    $("#suggestions-join").button();
    showMap();
});

function showMap() {
    var myOptions = {
        center: new google.maps.LatLng(),
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("suggestions-right-div"), myOptions);
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
//    $.ajax({
//        url: suggestionDetails.data('join-url') + "/" + id,
//        type: "POST",
//        success: updateSuggestionDetails
//    });
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