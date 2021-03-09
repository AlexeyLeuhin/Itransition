var draggableList = document.getElementById('items');
var taleId = Number(document.getElementById('taleId').value);
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
