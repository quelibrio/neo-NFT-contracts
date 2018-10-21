'use strict';

import Neon, {api, nep5, rpc, sc, wallet} from "@cityofzion/neon-js";
import * as util from "util";
let config = require('./config.js');

let account = Neon.create.account('KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr');
let realAddress =  sc.ContractParam.byteArray(account.address, 'address');
function callSmartContract(operation: string, args: Array<any> = []) {
    let hash = config.scriptHash;
    let networkUrl = 'http://192.168.99.100:30333';
    let neoscanUrl = 'http://192.168.99.100:4000/api/main_net';

    console.log(operation, args)

    let script = Neon.create.script(
        {
            scriptHash: hash,
            operation: operation,
            args: args
        }
    );

    let request = {
        net: neoscanUrl,
        url: networkUrl,
        script,
        account: account,
        address: account.address,
        privateKey: account.privateKey,
        publicKey: account.publicKey,
        gas: 0,
        balance: null
    };

    rpc.Query.invokeScript(script)
        .execute(networkUrl)
        .then(res => {
            console.dir(res.result.stack) // You should get a result with state: "HALT, BREAK"
        })
/*
    return api.neoscan.getBalance(neoscanUrl, account.address).then(data => {
        request.balance = data;
        console.log('Invocation of: ' + operation);
        console.log('Args: ', args);
        return Neon.doInvoke(request).then(res => console.log(res.response));
    });*/
}
let otherAddress = sc.ContractParam.byteArray('ASP3X76d9JunQosUds3npubiDsSpm3RMXF', 'address')

callSmartContract('totalSupply', [otherAddress.value]);
