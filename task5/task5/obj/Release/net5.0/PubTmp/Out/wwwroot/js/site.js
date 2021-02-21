let drawBtn = document.querySelector('#drawButton');
let textBtn = document.querySelector('#textButton');
let noteBtn = document.querySelector('#noteButton');
let eraserBtn = document.querySelector('#eraserButton');
let defaultBtn = document.querySelector('#defaultButton');
let dash = document.getElementById('dashboard');

defaultBtn.addEventListener('click', (e) => {
    activeTool = null;
    document.getElementById('myElement').className = 'cursorDefaultClass';
});

textBtn.addEventListener('click', (e) => {
    activeTool = ActiveButton.NOTE;
    document.getElementById('myElement').className = 'cursorTextClass';
});

drawBtn.addEventListener('click', (e) => {
    document.getElementById('myElement').className = 'cursorDrawClass';
    activeTool = ActiveButton.DRAW;
});

noteBtn.addEventListener('click', (e) => {
    document.getElementById('myElement').className = 'cursorNoteClass';
    activeTool = ActiveButton.TEXT;
});

eraserBtn.addEventListener('click', (e) => {
    activeTool = ActiveButton.ERASER;
    document.getElementById('myElement').className = 'cursorDefaultClass';
});

$(document).ready(function () { 
    $("#dashboard").on("click", function (e) {
        if (activeTool == ActiveButton.TEXT) {
            activeTool = null;
            document.getElementById('myElement').className = 'cursorDefaultClass';
            saveTextArea(e.clientX - dash.offsetLeft, e.clientY - dash.offsetTop);
        }
        if (activeTool == ActiveButton.NOTE) {
            activeTool = null;
            document.getElementById('myElement').className = 'cursorDefaultClass';
            
            var text = new PointText(new Point(e.clientX - dash.offsetLeft, e.clientY - dash.offsetTop))
            text.fillColor = 'black';
            text.content =  "click to print";
            text.fontSize = '20px';
            text.fontFamily = "arial";
            hubConnectionText.invoke("AddText", text.exportJSON());
        }
    });  
    loadTextAreas();   
    loadPathes();
    loadTextes();
})
