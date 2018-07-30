const mongoose = require('mongoose')
mongoose.Promise = global.Promise // atribuindo o modulo de Promise global do node para o mongoose
module.exports = mongoose.connect('mongodb://localhost/mymoney')

//mudando o a mensagem de retorno de erro
mongoose.Error.messages.general.required = "O Atributo '{PATH}' é obrigatório."
mongoose.Error.messages.Number.min = "O '{VALUE}' informado é menor que o limite de '{MIN}'"
mongoose.Error.messages.Number.max = "O '{VALUE}' informado é maior que o limite de '{MAX}'"
mongoose.Error.messages.String.enum = "O '{VALUE}' não é valido para o atributo '{PATH}'"
