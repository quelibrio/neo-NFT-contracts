let {default: Neon, api, nep5, rpc, sc, wallet} = require('@cityofzion/neon-js');

let prepare = require('./prepare');

module.exports = (call, args, hash) => {
    let request = prepare.getRequest(call, args, hash);

    return api.neoscan.getBalance(request.net, request.account.address)
        .then(data => {
            request.balance = data;
            console.log('Invocation of: ' + call);
            console.log('Args: ', request.script.args);
            return Neon.doInvoke(request);
        });
};
