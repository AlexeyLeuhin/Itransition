let canvas = document.getElementById('canvas');
paper.install(window);
paper.setup(canvas);
var tool1 = new Tool();
var activeItem;
var hit;
var oldDragPosition;
var oldJsonText;
var oldJsonLine;

var hubConnectionDrawing = new signalR.HubConnectionBuilder()
.withUrl("/drawing")
.build();
var currentPath = new Path();


hubConnectionText.on("DeleteText", function (json) {
    var y = project.activeLayer.getItems({ className: 'PointText' })
    for(var i = 0; i<y.length; i+=1){
        if(y[i].exportJSON() == json){
            y[i].remove();
        }
    }
    view.draw();
});

hubConnectionDrawing.on("DeleteLine", function (json) {
    var y = project.activeLayer.getItems({ className: 'Path' })
    for(var i = 0; i<y.length; i+=1){
        if(y[i].exportJSON() == json){
            y[i].remove();
        }
    }
    view.draw();
});

hubConnectionDrawing.on("AddLine", function (json) {
    var line = new Path();
    line.importJSON(json);
});

hubConnectionDrawing.on("UpdateLine", function (oldJson, newJson) {
    var y = project.activeLayer.getItems({ className: 'Path' })
    for(var i = 0; i<y.length; i+=1){
        if(y[i].exportJSON() == oldJson){
            y[i].remove();
        }
    }   
    var text = new Path();
    text.importJSON(newJson);
    view.draw();
});

hubConnectionText.on("AddText", function (json) {
    var text = new PointText();
    text.importJSON(json);
});

hubConnectionText.on("UpdateText", function (oldJson, newJson) {
    var y = project.activeLayer.getItems({ className: 'PointText' })
    for(var i = 0; i<y.length; i+=1){
        if(y[i].exportJSON() == oldJson){
            y[i].remove();
        }
    }   
    var text = new PointText();
    text.importJSON(newJson);
    view.draw();
});

hubConnectionDrawing.start();

tool1.onMouseDown = function (event){
    if(activeItem){
        activeItem.selected = false;
    }
    if(activeTool == ActiveButton.DRAW){
        currentPath = createConfiguredPath('black', 3);
    } else if(activeTool == null){
        hit = project.hitTest(event.point);         
        activeItem = hit && hit.item;          
        if(activeItem && activeItem.className == 'Path'){
            oldDragPosition = event.point;   
            oldJsonLine = activeItem.exportJSON(); 
            activeItem.selected = true; 
        }else if(activeItem && activeItem.className == 'PointText') {
            oldJsonText = activeItem.exportJSON();
            activeItem.selected = true;
        }       
    } else if(activeTool == ActiveButton.ERASER){
        hit = project.hitTest(event.point);     
        activeItem = hit && hit.item;          
        if(activeItem.className == 'PointText'){
            hubConnectionText.invoke("DeleteText", activeItem.exportJSON()); 
        } else{
            hubConnectionDrawing.invoke("DeleteLine", activeItem.exportJSON());
        }  
        activeItem.remove();
        view.draw();
    }
}

tool1.onMouseDrag = function(event){
    if(activeTool == ActiveButton.DRAW){
        currentPath.add(new Point(event.point.x, event.point.y+17));
        view.draw();
    }    
    if(activeItem && activeItem.className == 'Path'){
        activeItem.position.x += (event.point.x-oldDragPosition.x);
        activeItem.position.y += (event.point.y-oldDragPosition.y);
        oldDragPosition = event.point;
    }else if(activeItem && activeItem.className == 'PointText'){
        activeItem.position = event.point;
    }
}

tool1.onMouseUp = function(event){
    if(activeTool == ActiveButton.DRAW){
        hubConnectionDrawing.invoke("AddLine", currentPath.exportJSON());
    } else if(activeItem){
        activeItem.selected = false; 
        if(activeItem.className == 'Path'){    
            updateLine(activeItem);   
        }  else if(activeItem.className == 'PointText'){
            updateText(activeItem);     
        }
        activeItem.selected = true;  
    }      
}

tool1.onKeyDown = function(event) {
	if(activeItem && activeItem.className == 'PointText'){
        if(event.key == 'backspace'){
            if(!activeItem.isEmpty()){
                activeItem.content = activeItem.content.slice(0, -1);
            }
        }else if(event.key == 'space'){
            activeItem.content += ' ';
        }
        else{
            activeItem.content += event.key;
        }   
        activeItem.selected = false;
        updateText(activeItem);   
        oldJsonText = activeItem.exportJSON(); 
        activeItem.selected = true; 
    }  
}

function loadPathes(){
    $.ajax({
        type: 'GET',
        url: "/Home/GetLines",
        success: function(lines) {
            lines.forEach(model => {
                var line = new Path();
                line.importJSON(model.data);
            })                                    
        }
    })
}

function loadTextes(){
    $.ajax({
        type: 'GET',
        url: "/Home/GetTextes",
        success: function(textes) {
            textes.forEach(model => {
                var text = new PointText();
                text.importJSON(model.data);
            })                                    
        }
    })
}

function createConfiguredPath(color, width){
    var path = new Path();
    path.strokeColor = color;
    path.strokeWidth = width;
    return path;
}

function updateText(item){
    hubConnectionText.invoke("UpdateText", oldJsonText, item.exportJSON());
}

function updateLine(item){
    hubConnectionDrawing.invoke("UpdateLine", oldJsonLine, item.exportJSON());
}