var draggableList = document.getElementById('items');
var taleId = Number(document.getElementById('taleId').value);

var hubConnectionTaleRedact = new signalR.HubConnectionBuilder()
    .withUrl("/taleRedactor")
    .build();
hubConnectionTaleRedact.start();

hubConnectionTaleRedact.on("AddChapter", function (newChapterId) {
    draggableList.appendChild(constructNewChapterListItem(newChapterId));
});

function constructNewChapterListItem(newChapterId) {
    let li = document.createElement('li');
    li.id = newChapterId;
    let input = document.createElement('input');
    input.type = "text";
    input.value = "New chapter";
    input.readOnly = true;
    input.ondblclick = this.readOnly = false;
    input.onblur = this.readOnly = true;
    li.appendChild(input);
    li.classList.add("list-group-item");
    return li;
}

if (draggableList) {
    var sortable = new Sortable(draggableList, {
        animation: 150,
        
        onEnd: function (evt) {
            evt.oldIndex;  // element's old index within old parent
            evt.newIndex;  // element's new index within new parent     
            
        },
    });
}

var selectedItem = null;
var items = document.getElementsByClassName("list-group-item");
for (i = 0; i < items.length; i+=1) {
    
    items[i].onclick = function (e) {
        if (selectedItem) {
            selectedItem.classList.remove("active");
        }
        this.classList.add("active");
        selectedItem = this;
    }  
}

function addChapter() {
    hubConnectionTaleRedact.invoke("AddChapter", taleId);
}
