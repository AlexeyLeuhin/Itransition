var ratingPanels = document.getElementsByClassName("rating");
for (var i = 0; i < ratingPanels.length; i++) {
    ratingPanels[i].value = parseFloat(ratingPanels[i].value.replace(",", "."));
    $("#" + ratingPanels[i].id).rating({
        readonly: true,
    });
}

    