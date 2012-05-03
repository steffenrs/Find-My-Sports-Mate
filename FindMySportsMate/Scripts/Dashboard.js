$(function () {
    $("#suggestions-tabs").tabs();
    loadScript();
   // initializeMap();
});

function loadScript() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyACZEsTgnDXNAuphz0jeO_jT-bZsQLa5ho&sensor=false&callback=initializeMap";
    document.body.appendChild(script);
}

function initializeMap() {
    var myOptions = {
        center: new google.maps.LatLng(-34.397, 150.644),
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("suggestions-right-div"),
            myOptions);
}

function getSuggestion(id) {
    var suggestionDetails = $("#suggestions-details");
 
    $.ajax({
        url: suggestionDetails.data('update-url')+"/"+id,
        type: "POST",
        success: updateSuggestionDetails 
    });
};

function updateSuggestionDetails(data) {
    var suggestionsDetails = $("#suggestions-details");
    suggestionsDetails.find("#description").html(data.Description);
    suggestionsDetails.find("#creator").html("Creator:" + data.Creator.FirstName);
    suggestionsDetails.find("#time-period").html("Time period:" + data.Description);
    suggestionsDetails.find("#users").html("Users: " + data.JoinedUsers.length + " / " + data.MaximumUsers);
    suggestionsDetails.find("#status").html("Status: " + ((data.IsClosed) ? "Closed" : "Open"));
    suggestionsDetails.find("#location").html("Location: " + data.Location.Name);

    var listHtml = "";

    $.each(data.JoinedUsers, function () {
        listHtml += "<li><a href='" + suggestionsDetails.data("view-user") + "/" + this.User.Id + "'>" + this.User.FirstName + "</a></li>";
    });

    suggestionsDetails.find("ul").html(listHtml);
}