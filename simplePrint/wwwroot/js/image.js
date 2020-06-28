"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/imageHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    console.log("Saved Successfully!");
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendMessage", xyVals.join()).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("repeatLayer").addEventListener("click", function (event) {
    connection.invoke("SendMessage", "repeat").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});