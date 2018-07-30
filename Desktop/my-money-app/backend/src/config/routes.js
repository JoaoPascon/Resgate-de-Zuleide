const express = require('express')

module.exports = function(server) {

    // Definir URL para todas as rotas
    const router = express.Router();
    server.use('/api', router) // toao rota '/api' vai bater nesse router

    // rodas do ciclo de pagamento
    const BillingCycle = require('../api/billingCycle/billingCycleService')

    BillingCycle.route('count', (req, res, next) => {
        BillingCycle.count((error, value) => {
            if(error) {
                res.status(500).json({errors: [error]})
            } else {
                res.json({value})
            }
        })
    })

    BillingCycle.route('summary', (req, res, next) => {
        BillingCycle.aggregate([{
            $project: {credit: {$sum: "$credits.value"}, debt: {$sum: "$debts.value"}}
        }, {
            $group: {_id: null, credit: {$sum: "$credit"}, debt: {$sum: "$debt"}}
        }, {
            $project: {_id: 0, credit: 1, debt: 1}
        }]).exec((error, result) => {
            if(error) {
                res.status(500).json({errors: [error]})
            } else {
                res.json(result[0] || { credit: 0, debt: 0 })
            }
        })
    })

    BillingCycle.register(router, '/billingCycles')

}