$(function () {
    var diag = createJoinDialog();

    
});

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
    fields.appendTo("#create-suggestion");
    fields.css("display", "none");

    $("#create-suggestion").submit();
}