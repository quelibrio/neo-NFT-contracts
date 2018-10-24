'use strict';
Object.defineProperty(exports, "__esModule", { value: true });
var neon_js_1 = require("@cityofzion/neon-js");
var config = require('./config.js');
var account = neon_js_1.default.create.account('KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr');
var realAddress = neon_js_1.sc.ContractParam.byteArray(account.address, 'address');
function callSmartContract(operation, args) {
    if (args === void 0) { args = []; }
    var hash = config.scriptHash;
    var networkUrl = 'http://192.168.99.100:30333';
    var neoscanUrl = 'http://192.168.99.100:4000/api/main_net';
    console.log(operation, args);
    var script = neon_js_1.default.create.script({
        scriptHash: hash,
        operation: operation,
        args: args
    });
    var request = {
        net: neoscanUrl,
        url: networkUrl,
        script: script,
        account: account,
        address: account.address,
        privateKey: account.privateKey,
        publicKey: account.publicKey,
        gas: 0,
        balance: null
    };
    neon_js_1.rpc.Query.invokeScript(script)
        .execute(networkUrl)
        .then(function (res) {
        console.dir(res.result.stack); // You should get a result with state: "HALT, BREAK"
    });
    return neon_js_1.api.neoscan.getBalance(neoscanUrl, account.address).then(function (data) {
        request.balance = data;
        console.log('Invocation of: ' + operation);
        console.log('Args: ', args);
        return neon_js_1.default.doInvoke(request).then(function (res) { return console.log(res.response); });
    });
}
var otherAddress = neon_js_1.sc.ContractParam.byteArray('ASP3X76d9JunQosUds3npubiDsSpm3RMXF', 'address');
callSmartContract('totalSupply', [otherAddress.value]);
module.exports = {
    sc: neon_js_1.sc,
    invoke: function () {
        var args = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            args[_i] = arguments[_i];
        }
        return callSmartContract.apply(null, args);
    }
};
