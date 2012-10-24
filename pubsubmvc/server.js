var express = require('express'),
    sio = require('socket.io'),
    redis = require('redis');

var client = redis.createClient();

client.on("message", function (channel, message) {
    if (client.socket) {
        client.socket.emit('update', message);
    }
    console.log("message");
    console.log(message);
});

client.subscribe("bigjob");
var app = express.createServer();
app.listen(process.env.PORT || 80);

var io = sio.listen(app);

io.configure(function () {
    io.set('transports', [
                'xhr-polling',
                'jsonp-polling',
                'htmlfile'
            ]);
    io.set("polling duration", 10);
});



io.sockets.on('connection', function (socket) {
    console.log("wtf");
    socket.on('connect', function (user) {
        socket.user = user;
        client.socket = socket;
        console.log('connected');
        console.log(client.socket);
    });
    console.log(socket);
});
