ActiveButton = { DRAW: 1, NOTE: 2, ERASER: 3, TEXT: 4 }
let activeTool = null;

var hubConnectionText = new signalR.HubConnectionBuilder()
.withUrl("/text")
.build();

hubConnectionText.start();

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/textArea")
    .build();
hubConnection.on("UpdateTextArea", function (textArea) {
    $('#' + textArea.id)[0].getElementsByTagName('textarea')[0].value = textArea.text;
});
hubConnection.on("AddTextArea", function (textArea) {
    drawTextAreaInCanvas(textArea.x, textArea.y, textArea.id, textArea.text);
});
hubConnection.on("DeleteTextArea", function (id) {
    let dash = document.getElementById('dashboard');
    let div = document.getElementById(id);
    dash.removeChild(div);
});
hubConnection.start();
 