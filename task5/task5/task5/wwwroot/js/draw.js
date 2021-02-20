let canvas = document.getElementById('canvas');
paper.install(window);
paper.setup(canvas);
var tool1 = new Tool();
var activeItem;
var hit;
var oldDragPosition;
var beginDragPosition;
var activeItemOldFirstPointX;
var activeItemOldFirstPointY;
var oldJson;

var hubConnectionDrawing = new signalR.HubConnectionBuilder()
.withUrl("/drawing")
.build();
var currentPath = new Path();
hubConnectionDrawing.on("AddPath", function (path) {
        currentPath = createConfiguredPath('black', 3);
        for (var i = 0; i < path.segments.length; i++){
                currentPath.add(new Point(path.segments[i].x, path.segments[i].y));
        }   
});

hubConnectionDrawing.on("DeletePath", function (pathPoint) {
    var h = project.hitTest(pathPoint);
    var item = h && h.item;
    item.remove(); 
});

hubConnectionDrawing.on("DragPath", function (points, pathPoint) {
    var h = project.hitTest(pathPoint);
    var item = h && h.item;
    item.remove(); 
    var curPath = createConfiguredPath('black', 3);
    for (var i = 0; i < points.result.length; i++){
            curPath.add(new Point(points.result[i].x, points.result[i].y));
    }   
    view.draw();
});

hubConnectionText.on("DeleteText", function (json) {
    var y = project.activeLayer.getItems({ className: 'PointText' })
    for(var i = 0; i<y.length; i+=1){
        if(y[i].exportJSON() == json){
            y[i].remove();
        }
    }
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
    if(activeTool == ActiveButton.DRAW){
        currentPath = createConfiguredPath('black', 3);
    } else if(activeTool == null){
        hit = project.hitTest(event.point);
        oldDragPosition = event.point;  
        beginDragPosition = event.point;    
        activeItem = hit && hit.item;          
        if(activeItem && activeItem.className == 'Path'){
            activeItemOldFirstPointX = activeItem.segments[0].point.x;
            activeItemOldFirstPointY = activeItem.segments[0].point.y;
            activeItem.selected = true; 
        }else if(activeItem && activeItem.className == 'PointText') {
            oldJson = activeItem.exportJSON();
            activeItem.selected = true;
        }       
    } else if(activeTool == ActiveButton.ERASER){
        hit = project.hitTest(event.point);     
        activeItem = hit && hit.item;          
        if(activeItem.className == 'PointText'){
            hubConnectionText.invoke("DeleteText", activeItem.exportJSON()); 
        } else{
            hubConnectionDrawing.invoke("DeletePath", {"x": activeItem.segments[0].point.x, "y": activeItemOldFirstPointY = activeItem.segments[0].point.y});
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
    if(activeItem && activeItem.className == 'Path'){  
        var deltaX = beginDragPosition.x - oldDragPosition.x;
        var deltaY = beginDragPosition.y - oldDragPosition.y;
        activeItem.selected = false;
        hubConnectionDrawing.invoke("DragPath", {"x": deltaX, "y": deltaY}, {"x": activeItemOldFirstPointX, "y": activeItemOldFirstPointY});
        activeItem = null;        
    }  else if(activeItem && activeItem.className == 'PointText'){
        activeItem.selected = false;
        updateText(activeItem);       
    }
    if(activeTool == ActiveButton.DRAW){
        var data = currentPath.segments.map(function(segment) {
            return {"x": parseInt(segment.point.x, 10), "y": parseInt(segment.point.y, 10)};
        });
        hubConnectionDrawing.invoke("AddPath", data);
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
        updateText(activeItem);   
        oldJson = activeItem.exportJSON();  
    }  
}

function loadPathes(){
    $.ajax({
        type: 'GET',
        url: "/Home/GetPathes",
        success: function(pathes) {
            pathes.forEach(function(path){
                var drawingPath = createConfiguredPath('black', 3);
                path.forEach(function(point){
                    drawingPath.add(new Point(point.x, point.y));
                    view.draw();
                })
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
    hubConnectionText.invoke("UpdateText", oldJson, item.exportJSON());
}