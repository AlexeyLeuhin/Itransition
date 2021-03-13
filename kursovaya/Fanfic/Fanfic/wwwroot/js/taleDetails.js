var draggableList = document.getElementById('items');
var taleId = Number(document.getElementById('taleId').value);
var saveButton = document.getElementById("saveButton");
var leftArrow = document.getElementById("leftArrow");
var rightArrow = document.getElementById("rightArrow");
var selectedItem = null;
var listItems = document.getElementsByClassName("list-group-item");


$("#rating-id").rating({
    step: 1,
}).on("rating:change", function (event, value, caption) {
    $.ajax({
        url: "TaleDetails?handler=AddRating",
        data: { "ratingValue": value, "taleId": taleId},
        type: 'Post',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            $('#rating-id').rating('refresh', { readonly: true });
        },
    });
});


leftArrow.onclick = function (e) {
    if (selectedItem) {
        let prevChapter = selectedItem.previousElementSibling;
        onSelect(prevChapter);
        if (prevChapter.previousElementSibling == null) {
            leftArrow.disabled = true;
        }
        rightArrow.disabled = false;
    }
}

rightArrow.onclick = function (e) {
    if (selectedItem) {
        let nextChapter = selectedItem.nextElementSibling;
        onSelect(nextChapter);
        if (nextChapter.nextElementSibling == null) {
            rightArrow.disabled = true;
        }
        leftArrow.disabled = false;
    }
}

function constructNewChapterListItem(newChapterId) {
    let li = document.createElement('li');
    li.id = newChapterId;
    li.onclick = function (e) {
        onSelect(this);
    }; 
    let input = document.createElement('input');
    input.type = "text";
    input.value = "New chapter";
    input.onkeyup = function (e) {
        onChapterRename(this);
    };
    input.readOnly = true;
    input.ondblclick = function() { this.readOnly = false; };
    input.onblur = function () { this.readOnly = true; };
    li.appendChild(input);
    li.classList.add("list-group-item");
    let btn = document.createElement('button');
    btn.value = newChapterId;
    btn.onclick = function () { deleteChapter(this.value); };
    btn.classList.add("transparent-button");
    let icon = document.createElement('i');
    icon.className = 'fa fa-trash-o';
    icon.areaHidden = true;
    btn.appendChild(icon);  
    li.appendChild(btn);
    return li;
}

if (draggableList) {
    var sortable = new Sortable(draggableList, {
        animation: 150,
        
        onEnd: function (evt) {
            let oldIndex = evt.oldIndex;  
            let newIndex = evt.newIndex;
            $.ajax({
                url: "TaleDetails?handler=RenumerateChapters",
                data: { "taleId": taleId, "oldIndex": oldIndex, "newIndex": newIndex},
                type: 'Post',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                success: function (response) {
                    draggableList.appendChild(constructNewChapterListItem(response));
                },
            });
            checkNavigationElements();
        },
    });
}

function checkNavigationElements() {
    if (selectedItem != null) {
        leftArrow.hidden = false;
        rightArrow.hidden = false;
        rightArrow.disabled = false;
        leftArrow.disabled = false;
        if (selectedItem.nextElementSibling == null) {
            rightArrow.disabled = true;
        }
        if (selectedItem.previousElementSibling == null) {
            leftArrow.disabled = true;
        }
        if (saveButton) {
            saveButton.hidden = false;
        }
    } else {
        leftArrow.hidden = true;
        rightArrow.hidden = true;
        if (saveButton) {
            saveButton.hidden = true;
        }
    }
    
}

for (i = 0; i < listItems.length; i += 1) {

    listItems[i].onclick = function (e) {
        checkNavigationElements();
        onSelect(this);
    };
    listItems[i].children[0].onkeyup = function (e) {
        onChapterRename(this);
    };
}

function addChapter() {
    $.ajax({
        url: "TaleDetails?handler=AddChapter",
        data: { "taleId": taleId }, 
        type: 'Post',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            draggableList.appendChild(constructNewChapterListItem(response));
        },
    });
}

function deleteChapter(curChapterId) {
    let id = Number(curChapterId);
    $.ajax({
        url: "TaleDetails?handler=DeleteChapter",
        data: { "chapterId": id, "taleId": taleId },
        type: 'Post',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (id) {
            let li = document.getElementById(id);
            draggableList.removeChild(li);
            document.getElementById("chapterNameLabel").textContent = "";
            document.getElementById("chapterText").value = "";
            selectedItem = null;
            checkNavigationElements();
        },
    });   
}

function onSelect(listElement){
    if (selectedItem) {
        selectedItem.classList.remove("active");
    }
    listElement.classList.add("active");
    selectedItem = listElement;
    document.getElementById("chapterNameLabel").textContent = listElement.children[0].textContent;

    $.ajax({
        url: "TaleDetails?handler=ChapterText",
        data: { "chapterId": selectedItem.id },
        type: 'GET',
        complete: function (text) {
            document.getElementById("chapterText").value = text.responseJSON;
        },
    });
    checkNavigationElements();
}

function onChapterRename(element) {
    let newName = element.value;
    $.ajax({
        url: "TaleDetails?handler=RenameChapter",
        data: { "chapterId": selectedItem.id, "newName": newName },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        type: 'POST'
    });
    document.getElementById("chapterNameLabel").textContent = newName;
}

function saveChapterText() {
    let text = document.getElementById("chapterText").value;
    $.ajax({
        url: "TaleDetails?handler=ChapterText",
        data: { "chapterId": selectedItem.id, "text": text },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        type: 'POST',
        success: function (response) {
            alert(response);
        },
    });
}