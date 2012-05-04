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
    console.log("lol");
    var suggestionDetails = $("#suggestions-content");
    console.log(suggestionDetails.data('update-url'));

    $.ajax({
        url: suggestionDetails.data('update-url')+"/"+id,
        type: "POST",
        success: updateSuggestionDetails 
    });
};

function updateSuggestionDetails(data) {
    var suggestionsDetails = $("#suggestions-left-div");
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