﻿"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/feature").build();

function checkSessionConnection() {
    console.log("checkSessionConnection");
    console.log(connection);
}

function EnterInGroupSession(group) {


    console.log("EnterInGroupSession");
    connection.start().then(function () {
        console.log("connection.start()");

        console.log("Inserting into SignalR Group : " + group);
        connection.invoke("JoinGroup", group).catch(err => console.error(err));
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("StatusFeatureUpdated", function (SessionId,Id) {
    console.log("StatusFeatureUpdated");
    console.log("Session ID : " + SessionId);
    console.log("User ID : " + Id);   

    CallFunctionFromJSSession();
});
