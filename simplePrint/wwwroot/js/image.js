const connection = new signalR.HubConnectionBuilder()
    .withUrl("/imagehub")
    .build();

//Receive Image from server, in the end this will return the gcode file
connection.on("ReceiveMessage", (user, message) => {
    //Taken from a guide on chat, todo: Change this to the images
    const msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    const encodedMsg = user + " :: " + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

//Send Image To Server

document.getElementById("sendImage").addEventListener("click", event => {
    //Taken from a guide on chat, todo: Change this to the images
    const user = document.getElementById("userName").value;
    const message = document.getElementById("userMessage").value;
    connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
    event.preventDefault();
});   