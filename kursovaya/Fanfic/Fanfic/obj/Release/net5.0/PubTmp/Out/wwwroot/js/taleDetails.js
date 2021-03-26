var draggableList = document.getElementById('items');
var taleId = Number(document.getElementById('taleId').value);
var userId = document.getElementById("userId").value;
var saveButton = document.getElementById("saveButton");
var leftArrow = document.getElementById("leftArrow");
var rightArrow = document.getElementById("rightArrow");
var selectedItem = null;
var dropFile = document.getElementById("divUploadFile");
var listItems = document.getElementsByClassName("list-group-item");
var shoulNotBeSelected = false;
var likeButton = document.getElementById("likeButton");

var hubConnectionComments = new signalR.HubConnectionBuilder()
    .withUrl("/comments")
    .build();

hubConnectionComments.on("PostComment", function (comment) {
    createNewCommentVisualization(comment.message, comment.createTime, comment.author.name);
});

hubConnectionComments.start();


function Editor(input, preview) {
    this.update = function () {
        preview.innerHTML = marked(input.value);
    };
    input.editor = this;
    this.update();
}
if (document.getElementById("chapterText") != null) {
    new Editor(document.getElementById("chapterText"), document.getElementById("previewText"));
}

$(function () {
    $("#divUploadFile").filedrop({
        fallback_id: 'update-profile-button',
        fallback_dropzoneClick: false,
        url: '/FileUpload/UploadChapterImage',
        allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif'],
        allowedfileextensions: ['.jpg', '.jpeg', '.png', '.gif'],
        paramname: 'chapterImage',
        maxfiles: 1,
        maxfilesize: 5,
        dragOver: function () {
            $("#divUploadFile").addClass('drop-place-active');
        },
        dragLeave: function () {
            $("#divUploadFile").removeClass('drop-place-active');
        },
        drop: function () {
            $("#imgLoading").show();
        },
        beforeEach: function (file) {
            new Compressor(file, {
                quality: 0.6,
                width: 600,
                height: 600,
                success(result) {
                    const formData = new FormData();
                    formData.append('chapterImage', result, result.name);
                    formData.append("chapterId", selectedItem.id);
                    $.ajax({
                        url: "/FileUpload/UploadChapterImage",
                        data: formData,
                        processData: false,
                        contentType: false,
                        type: 'Post',
                        success: function (response) {
                            $("#divUploadFile").removeClass('drop-place-active');
                            $("#imgLoading").hide();
                            location.reload();
                        }
                    });
                }
            })
            return false;
        },
        uploadFinished: function (i, file, response, time) {
            $("#imgLoading").hide();
            document.getElementById("chapterImage-" + selectedItem.id).src = response;
        },
        afterAll: function () {
            $("#divUploadFile").addClass('');
        }
    });
});




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
            location.reload();
        }
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
    input.readOnly = true;
    input.ondblclick = function() { this.readOnly = false; };
    input.onblur = listItemOnBlur(this);
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
            var img = document.createElement('img');
            img.id = "chapterImage-" + response;
            img.src = "/chapterPlaceholder.png";
            img.classList.add("rounded-circle");
            img.classList.add("chapter-image");
            img.width = 200;
            img.height = 200;
            img.hidden = true;
            document.getElementById("chaptersImages").appendChild(img);
            draggableList.appendChild(constructNewChapterListItem(response));
        },
    });
}

function deleteChapter(curChapterId) {
    let id = Number(curChapterId);
    let li = document.getElementById(id);
    draggableList.removeChild(li);
    shoulNotBeSelected = true;
    if (selectedItem && selectedItem.id == curChapterId) {
        document.getElementById("chapterNameLabel").textContent = "";
        document.getElementById("chapterText").value = "";
        document.getElementById("chapterImage-" + id).hidden = true;
        document.getElementById("likesNumber").hidden = true;
        if (dropFile != null) {
            dropFile.hidden = true;
        }
        likeButton.removeChild(document.getElementById("likeHeart"));
        likeButton.hidden = true;
        selectedItem = null;
    }
    checkNavigationElements();
    $.ajax({
        url: "TaleDetails?handler=DeleteChapter",
        data: { "chapterId": id, "taleId": taleId },
        type: 'POST',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        }
    });   
}

function onSelect(listElement) {
    if (shoulNotBeSelected == false) {
        if (selectedItem) {
            document.getElementById("chapterImage-" + selectedItem.id).hidden = true;
            selectedItem.classList.remove("active");
            if (document.getElementById("likeHeart")) {
                likeButton.removeChild(document.getElementById("likeHeart"));
            }
           
        }
        likeButton.hidden = false;
        document.getElementById("rightColumn").hidden = false;
        selectedItem = listElement;
        document.getElementById("chapterImage-" + listElement.id).hidden = false;
        document.getElementById("likesNumber").hidden = false;
        if (dropFile != null) {
            dropFile.hidden = false;
        }
        listElement.classList.add("active");
        document.getElementById("chapterNameLabel").textContent = listElement.children[0].value;
        $.ajax({
            url: "TaleDetails?handler=ChapterInfo",
            data: { "chapterId": listElement.id },
            type: 'GET',
            complete: function (response) {
                response = response.responseJSON;
                if (document.getElementById("chapterText")) {
                    document.getElementById("chapterText").value = response.text;
                }
                document.getElementById("previewText").innerHTML = marked(response.text);
                document.getElementById("likesNumber").textContent = response.chapterLikes;
                likeButton.appendChild(createLikeHeartIcon(response.userLikedChapter));
            },
        });
        checkNavigationElements();
    } else {
        shoulNotBeSelected = false;
    }
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

function createLikeHeartIcon(liked) {
    let ic = document.createElement("i");
    ic.id = "likeHeart";
    ic.classList.add("fa");
    if (liked) {
        ic.classList.add("fa-heart");
    } else {
        ic.classList.add("fa-heart-o");
    }
    return ic;
}

function onLikePressed() {
    let liked = false;
    if (likeButton.children[0].classList[1] == "fa-heart-o") {
        liked = true;
        document.getElementById("likesNumber").textContent = Number(document.getElementById("likesNumber").textContent) + 1;
    } else {
        document.getElementById("likesNumber").textContent = Number(document.getElementById("likesNumber").textContent) - 1;
    }
    likeButton.removeChild(document.getElementById("likeHeart"));
    likeButton.appendChild(createLikeHeartIcon(liked));
    $.ajax({
        url: "TaleDetails?handler=LikePressed",
        data: { "chapterId": selectedItem.id},
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        type: 'POST',
        success: function (response) {
            
        },
    });
}

function sendComment() {
    commentText = document.getElementById("commentInput").value;
    hubConnectionComments.invoke("PostComment", commentText, taleId, userId);
    document.getElementById("commentInput").value = "";
}

function createNewCommentVisualization(commentText, time, authorName) {
    let comment = document.createElement("div");
    comment.classList.add("container");
    let txtdiv = document.createElement("div");
    txtdiv.classList.add("row");
    let txtarea = document.createElement("textarea");
    txtarea.readOnly = true;
    txtarea.value = commentText;
    txtdiv.appendChild(txtarea);
    comment.appendChild(txtdiv);
    let infodiv = document.createElement("div");
    infodiv.classList.add("row");
    let timediv = document.createElement("div");
    timediv.classList.add("col-6");
    let timelbl = document.createElement("label");
    timelbl.textContent = time;
    timediv.appendChild(timelbl);
    infodiv.appendChild(timediv);
    let namediv = document.createElement("div");
    namediv.classList.add("col-6");
    let namelbl = document.createElement("label");
    namelbl.textContent = authorName;
    namediv.appendChild(namelbl);
    infodiv.appendChild(namediv);
    comment.appendChild(infodiv);
    document.getElementById("comments").appendChild(comment);
}

function listItemOnBlur(el) {
    el.readOnly = 'true';
    onChapterRename(el);
}