const BillingCycle = require('./billingCycle')
const errorHandler = require('../common/errorHandler')

BillingCycle.methods(['get', 'post', 'put', 'delete'])
BillingCycle.updateOptions({new: true, runValidators: true})// Quando atualizar trazer o objeto ja atualizado e Para também garantir que o mongoose faze as validações em metódos PUT
BillingCycle.after('post', errorHandler).after('put', errorHandler) // para aplicar o midware de tratamento de erro após a a resposta

module.exports = BillingCycle;