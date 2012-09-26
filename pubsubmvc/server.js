var express = require('express'),
    sio = require('socket.io'),
    redis = require('redis');

var client = redis.createClient();

client.on("message", function (channel, message) {
    client.socket.emit('update', message);
});
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



io.on('connection', function (socket) {
    socket.on('connect', function (user) {
        socket.user = user;
        client.socket = socket;
    });
});
