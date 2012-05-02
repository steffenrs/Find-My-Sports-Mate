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
    suggestionsDetails.find("#creator").html("Creator:" + data.Creator);
    suggestionsDetails.find("#time-period").html("Time period:" + data.Description);
    suggestionsDetails.find("#users").html("Users: " + data.JoinedUsers.length + " / " + data.MaximumUsers);
    suggestionsDetails.find("#status").html("Status: " + data.Open);
    suggestionsDetails.find("#location").html("Location: " + data.Location);
    console.log(data);
}