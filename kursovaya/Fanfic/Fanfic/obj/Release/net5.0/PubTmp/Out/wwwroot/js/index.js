var ratingPanels = document.getElementsByClassName("rating");
for (var i = 0; i < ratingPanels.length; i++) {
    ratingPanels[i].value = parseFloat(ratingPanels[i].value.replace(",", "."));
    $("#" + ratingPanels[i].id).rating({
        readonly: true,
    });
}

var tagsDictionary = [];
function getTags() {
    return $.ajax({
        url: "/?handler=Tags",
        type: 'GET'
    });
}

(function () {
    $.when(getTags()).done(function (response) {
        var tagsData = response.tags;
        for (var i = 0; i < tagsData.length; i++) {
            tagsDictionary.push({ "word": tagsData[i].name, "weight": tagsData[i].weight });
        }
        $("#wordCloud").jQWCloud({
            cloud_color: 'yellow',
            verticalEnabled: true,
            word_common_classes: "tagWords",
            words: tagsDictionary,
            word_click: function () {
                let d = { "tagName": $(this).text()};
                window.location.href = "/SearchResult?handler=SearchByTag&tagName=" + $(this).text();
            },
            word_mouseEnter: function () {
                $(this).css("text-decoration", "underline");
            },
            word_mouseOut: function () {
                $(this).css("text-decoration", "none");
            },
        });
    });
})(jQuery);





    