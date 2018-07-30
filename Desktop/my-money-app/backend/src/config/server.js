const port = 3003;

const bodyParser = require('body-parser')
const express = require('express')
const server = express()
const allowsCors = require('./cors') // middleware para permitir o acesso de outras origens ao server
const queryParser = require('express-query-int') // para fazer o parser de paginação de do skip

server.use(bodyParser.urlencoded({extend: true}))
server.use(bodyParser.json())
server.use(allowsCors) 
server.use(queryParser())

server.listen(port, function(){
    console.log(`BACKEND in running on port ${port}.`)
})

module.exports = server;