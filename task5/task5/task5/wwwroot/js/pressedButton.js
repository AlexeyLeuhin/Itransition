ActiveButton = { DRAW: 1, NOTE: 2, ERASER: 3, TEXT: 4 }
let activeTool = null;

var hubConnectionText = new signalR.HubConnectionBuilder()
.withUrl("/text")
.build();

hubConnectionText.start();
 