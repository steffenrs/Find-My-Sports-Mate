$(function () {
    $("#suggestions-tabs").tabs();
    $("#suggestions-join").button();
    showMap();
});

function showMap() {
    var myOptions = {
        center: new google.maps.LatLng(-34.397, 150.644),
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
    var suggestionDetails = $("#suggestions-content");
    $.ajax({
        url: suggestionDetails.data('join-url') + "/" + id,
        type: "POST",
        success: updateSuggestionDetails
    });
}