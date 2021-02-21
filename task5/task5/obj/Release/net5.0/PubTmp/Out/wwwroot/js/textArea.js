function drawTextAreaInCanvas(x, y, id, text){
    let div = createTextAreaDiv();
    let textarea = createTextArea(text);   
    div.appendChild(textarea); 
    div.id = id;
    div.style.top = y + 'px';
    div.style.left = x + 'px';   
    $('#' + id).keyup(function(){
        let divText = div.getElementsByTagName('textarea')[0].value;
        hubConnection.invoke("UpdateTextArea", {"id": Number(div.id), "text": divText, "x": div.offsetLeft, "y": div.offsetTop});
    });
    $('#' + id).click(function(){
        if(activeTool == ActiveButton.ERASER){
            let dash = document.getElementById('dashboard');
            dash.removeChild(div);
            hubConnection.invoke("DeleteTextArea", Number(id));
        }       
    });
    return div;
}   

function saveTextArea(x,y){
    hubConnection.invoke("AddTextArea", {"id": 0, "text": "", "x": x, "y": y});
}

function createTextAreaDiv(){
    let div = document.createElement('div');
    let TODOtext = document.createElement('h6');
    TODOtext.innerHTML = "TODO";
    div.appendChild(TODOtext);
    div.className = 'dynamicTextArea';
    document.getElementById('dashboard').appendChild(div);
    return div
}

function createTextArea(text){
    let textarea = document.createElement('textarea');
    textarea.className = 'form-control';
    textarea.id='txt';
    textarea.value = text;
    return textarea;
}

function loadTextAreas(){
    $.ajax({
        type: 'GET',
        url: "/Home/GetTextAreas",
        success: function(res) {
            res.forEach(textArea => {
                drawTextAreaInCanvas(textArea.x, textArea.y, textArea.id, textArea.text);
            });
        }
    })
}
