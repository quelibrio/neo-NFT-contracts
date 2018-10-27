let {default: Neon, api, nep5, rpc, sc, wallet} = require('@cityofzion/neon-js');

let prepare = require('./prepare');

module.exports = (call, args, hash) => {
    let request = prepare.getRequest(call, args, hash);
    return rpc.Query.invokeScript(request.script)
        .execute(request.url)
        .then(res => {
            return res.result.stack;
            // You should get a result with state: "HALT, BREAK"
        })

    // return api.neoscan.getBalance(neoscanUrl, account.address).then(data => {
    //     request.balance = data;
    //     console.log('Invocation of: ' + operation);
    //     console.log('Args: ', args);
    //     return Neon.doInvoke(request);
    // })
};
