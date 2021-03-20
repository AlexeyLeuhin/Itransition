function addTag() {
    let tagText = document.getElementById("inputTag").value;
    let tagsStorage = document.getElementById("hiddenTags");
    if (tagsStorage.value == "") {
        document.getElementById("hiddenTags").value += tagText;
    } else {
        document.getElementById("hiddenTags").value += " " + tagText;
    }
    document.getElementById("inputTag").value = "";
    addTagVizualization(tagText);
}




function addTagVizualization(tagText) {
    let li = document.createElement("li");
    let lbl = document.createElement("label");
    lbl.textContent = tagText;
    li.appendChild(lbl);
    document.getElementById("addedTags").appendChild(li);
}