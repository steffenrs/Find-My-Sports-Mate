$(function () {
    $("#suggestions-tabs").tabs();
});

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
    console.log(data);
}